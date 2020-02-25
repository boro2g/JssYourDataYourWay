Setup Nuget feed
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
jss create jss-your-data-your-way react
cd jss-your-data-your-way
jss start

-------------------------------------------------------

todo
- setup integrated graphql (https://jss.sitecore.com/docs/techniques/graphql/integrated-graphql)
- chained content resolvers
- dynamic content resolvers

