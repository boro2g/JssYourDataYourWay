Setup Nuget feed (https://sitecore.myget.org/F/sc-packages/api/v3/index.json)
Followed https://doc.sitecore.com/developers/90/sitecore-experience-manager/en/set-up-sitecore-and-visual-studio-for-development.html
Installed JSS https://dev.sitecore.net/Downloads/Sitecore_JavaScript_Services.aspx and added dlls to library (rather than nuget) as they didn't work
https://sitecore93sc.dev.local/sitecore/api/layout/render/jss?item=/&sc_apikey={D9D0F9D9-2498-45BC-BF8E-371ED9529121}

GraphQl
https://sitecore93sc.dev.local/sitecore/api/graph/items/master/ui?sc_apikey=%7BD9D0F9D9-2498-45BC-BF8E-371ED9529121%7D&query=%7B%0A%20%20item(path%3A%20%22%2Fsitecore%2Fcontent%2Fhome%22)%20%7B%0A%20%20%20%20id%0A%20%20%20%20...%20on%20SampleItem%20%7B%0A%20%20%20%20%20%20text%20%7B%0A%20%20%20%20%20%20%20value%20%0A%20%20%20%20%20%20%7D%0A%20%20%20%20%7D%0A%20%20%7D%0A%7D

Azure function:
https://jssyourdatayourway.azure.boro2g.co.uk/api/JssYourDataYourWay
Lambda function:
https://jssyourdatayourway.aws.boro2g.co.uk/api/jss/content
https://eu-west-1.console.aws.amazon.com/lambda/home?region=eu-west-1#/functions/jssyourdatayourway-AspNetCoreFunction-1W8OG4SHRVSK4?tab=configuration

Jss
npm install -g @sitecore-jss/sitecore-jss-cli
if creating
 jss create jss-your-data-your-way react
 cd jss-your-data-your-way
 jss start

if cloning
 npm install

jss deploy app --acceptCertificate [cert thumbprint]

-------------------------------------------------------

Dynamic content resolver notes:

Individual instructions take the form: [<item>:<instruction>:<fields>]

<item> - the item to work on as detailed in below section.
<instruction> - details of the below options are in later sections
fields - takes fields from the <item>
children - works on the children of the <item>
linkedmultiitem - works on the multilist fields specified by the <fields> of the <item>
linkeditem - works on the droptree fields specified by the <fields> of the <item>
<fields> - the usage of this varies a little based on the <instruction> in use, but generally tells the resolver what field names to put in the output JSON
Multiple instructions can be present for the same rendering, the resulting output will be merged into one object.

The <item> specification
The item specification is made up of two parts. The first is a specifier for the item, the second is an optional sitecore query.

The specifier can be one of the following:

a Sitecore item path - e.g. /sitecore/content/Global Settings/MicroApps Settings
a Sitecore item ID - e.g. {48AFD1E3-0C64-49C5-B9FE-6BDB4F651A98}
"__datasource" - the datasource of the rendering selected on the page
"__page" - the page item itself
"__rendering" - the rendering parameters
"__item" - this is a special case for instructions that deal with sub-items (see below)
To add a sitecore query to the item specifier use a $ to separate the two as follows: <specifier>$<sitecorequery>.

Further information about sitecore queries can be found here.

Examples:
__datasource - This uses the datasource of the rendering as the item.
__datasource$./Disruption Content- This gets the Disruption Content item which is a child of the datasource.
__datasource$./Disruption Content//*[@ShowForEjHolidays!='1'] - This gets the child of the Disruption Content item where ShowForEjHolidays is not equal to '1'

The "fields" instruction
This instruction takes fields from the current item and includes them in the output.
The important part of this instruction is the <fields> section. This is a pipe ( | ) seperated list of field names to pull out from the item.
The fields can also contain the asterisk ( * ) character as a field name, this will pull out all fields that do not start with a double underscore (__) i.e. all built-in Sitecore fields.
It is also possible to pull the output of a field into a differently named output property, do this using the syntax "<source field name>-><output property name>".
It is possible to include the asterisk as well as specific field mappings.
It is possible to cast the value to a different type by preceding the field name with the type in "<>". Currently only bool is supported. A string value of "1" or "true" will map to true. All other values (and types) will map to false.

Examples:
[__datasource:fields:*] - this example is essentially a duplicate of the OOTB datasource resolver, it takes all the fields from the data source of the rendering and includes them
[/sitecore/content/Global Settings/Main III Settings/Form Settings/Form Validation Settings:fields:Email Regex->EmailRegex|Phone Number Regex->PhoneNumberRegex] - this one is a bit more complicated, this points to a fixed location in the Sitecore tree, pulls out only the "Email Regex" and "Phone Number Regex" fields, and also renames the in the JSON to "EmailRegex" and "PhoneNumberRegex" respectively.
[__rendering:fields:<bool>HideFlownFlights] -this example pulls the HideFlownFlights field off the rendering parameters and turns it into a boolean value.

The "children" instruction
This instructions takes the children of the current item and then applies a sub-instruction set to each of those child items. The results are put into an array and then placed into a property name specified by the <fields> section.
The sub-instructions are specified in parenthesis after the "children" instruction and can utilise the "__item" specification to pull information from the current child.

Examples:
[{F5E1BC52-48F3-46C5-BB34-A6E9D1B61771}:children([__item:fields:Name|Code]):Languages] - this example produces an array of objects based on the children of the given item GUID. For each child it will pull out the "Name" and "Code" fields. The resulting array is made available as the "Languages" property.

The "linkedmultiitem" instruction (formerly "linkeditems" which is depricated but still supported)
This instructions takes the items from a multilist field on the current item and then applies a sub-instruction set to the linked items. The results are put into an array and then placed into a property name specified by the <fields> section.
The sub-instructions are specified in parenthesis after the "linkedmultiitem" instruction and can utilise the "__item" specification to pull information from the current linked item.

Examples:
[__datasource:linkedmultiitem([__item:fields:Display Text->DisplayText|Value]):DocumentTypes] - this example produces an array of objects based on the items specified in the DocumentTypes field of the given item (in this case, the datasource). For each linked item it will pull out the "Display Text" and "Value" fields. The resulting array is made available as the "DocumentTypes" property.

The "linkeditem" instruction
This instructions takes the item from a droptree field on the current item and then applies a sub-instruction set to the linked items. The results are put into a property name specified by the <fields> section.
The sub-instructions are specified in parenthesis after the "linkeditems" instruction and can utilise the "__item" specification to pull information from the current linked item.

Examples:
[__datasource:linkeditem([__item:fields:*]):InfantFormDrawer] - this example produces an object based on the item specified in the InfantFormDrawer field of the given item (in this case, the datasource). For the linked item it will pull out all (non-sitecore-standard) fields. The resulting object is made available as the "InfantFormDrawer" property.


