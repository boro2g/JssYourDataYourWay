using System.Collections.Generic;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Data;
using SitecoreApplication.JssYourDataYourWay.DynamicResolver.Instructions.Implementations;

namespace SitecoreApplication.JssYourDataYourWay.DynamicResolver.Instructions
{
    public class InstructionFactory
    {
        private const string _fieldsInstuctionName = "fields";
        private const string _childrenInstuctionName = "children";
        private const string _linkedItemInstructionName = "linkeditem";
        private const string _linkedMultiItemInstructionName = "linkedmultiitem";
        private const string _depricatedLinkedMultiItemInstructionName = "linkeditems";

        public static IEnumerable<IDynamicResolverInstruction> CreateInstuctions(IEnumerable<InstructionSpecification> instructionSpecifications)
        {
            List<IDynamicResolverInstruction> instructions = new List<IDynamicResolverInstruction>();

            foreach (InstructionSpecification specification in instructionSpecifications)
            {
                if (specification.Instruction == _fieldsInstuctionName)
                {
                    instructions.Add(new FieldsInstruction(specification));
                }
                else if (specification.Instruction.StartsWith(_childrenInstuctionName))
                {
                    instructions.Add(new ChildrenInstruction(specification));
                }
                else if (specification.Instruction.StartsWith(_linkedMultiItemInstructionName) ||
                         specification.Instruction.StartsWith(_depricatedLinkedMultiItemInstructionName))
                {
                    instructions.Add(new LinkedMultiItemInstruction(specification));
                }
                else if (specification.Instruction.StartsWith(_linkedItemInstructionName))
                {
                    instructions.Add(new LinkedItemInstruction(specification));
                }
            }

            return instructions;
        }
    }
}
