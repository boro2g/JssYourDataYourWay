using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;
using System.Collections.Generic;
using System.Text;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Parsers
{
	public class InstructionSpecificationParser
	{
		private const char _tokenInstructionStart = '[';
		private const char _tokenInstructionEnd = ']';
		private const char _tokenInstructionPartDelimiter = ':';

		public static List<InstructionSpecification> Parse(string specificationString)
		{
			if(string.IsNullOrWhiteSpace(specificationString))
			{
				return new List<InstructionSpecification>();
			}

			int instructionDepth = 0;
			int partIndex = 0;
			List<InstructionSpecification> instructionSpecification = new List<InstructionSpecification>();
			InstructionSpecification currentSpecification = null;
			StringBuilder partBuilder = null;

			for (int i = 0; i < specificationString.Length; i++)
			{
				char character = specificationString[i];
				bool characterHandled = false;

				if (character == _tokenInstructionStart)
				{
					instructionDepth++;

					if (instructionDepth == 1)
					{
						currentSpecification = new InstructionSpecification();
						partBuilder = new StringBuilder();
						characterHandled = true;
					}
				}
				else if (character == _tokenInstructionEnd)
				{
					instructionDepth--;

					if (instructionDepth == 0 && partIndex == 2)
					{
						currentSpecification.Field = partBuilder.ToString();

						instructionSpecification.Add(currentSpecification);
						currentSpecification = null;
						partBuilder = null;
						partIndex = 0;
						characterHandled = true;
					}
				}
				else if (character == _tokenInstructionPartDelimiter && instructionDepth == 1)
				{
					partIndex++;

					if (partIndex == 1)
					{
						currentSpecification.Item = partBuilder.ToString();
					}
					else if (partIndex == 2)
					{
						currentSpecification.Instruction = partBuilder.ToString();
					}

					partBuilder.Clear();
					characterHandled = true;
				}
				else if(instructionDepth == 0 && char.IsWhiteSpace(character))
				{
					characterHandled = true;
				}

				if (!characterHandled && partBuilder != null)
				{
					partBuilder.Append(character);
				}
			}

			return instructionSpecification;
		}
	}
}
