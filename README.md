Mongo Shell
=============
Mongo Shell - Helps you to validate your mongo connection, and do basic mongo checks. Like Test Connection, Get Collection Names, Get Collection, Get Stats, Find All Users

For few projects, I noticed that me and few of my colleagues/friends had issue validating whether mongo connection is right or not, and if yes weather data is going through or not. Mongo Client tools (e.g. Mongo Management Studio) are there. But what If you can't install Mongo Client Tool on server and Firewall is blocked to open mongo connection out of network?

This tool will help you for that!

Basically, this tool is inspired from. SQL Shell - Sitecore tool to query SQL Data, https://github.com/SitecoreSupport/TestMongoDBConnection

BIG Thanks to them!

![Mongo Shell](https://sitecorebasics.files.wordpress.com/2016/08/mongo-shell-beta-demo.gif "Mongo Shell")

##Main Features

1. To validate your mongo connection directly from your Sitecore application
2. You can Get collection names of particular DB
3. You can view collection records
4. You can view Mongo stats
5. You can find all users
6. Security has been applied -- User needs to be logged in to access it.
7. Responsive UI

##How to Download and Install?

### Option 1
1. If you would like to do it manually you can download files from here [Sitecore/admin directory]
2. Copy-Paste MongoShell.*.* files under <WEBROOT>\Sitecore\Admin folder.
3. Access your page using  http://<YOURHOSTNAME>/sitecore/admin/Mongoshell.aspx
4. That's it! Enjoy! :-)

***

### Option 2
1. Download Sitecore Package from Data\Packages\V1 folder, Which will copy the files at required place.
2. Install using Sitecore Installation Wizard. Once done, Just access your page using  http://<YOURHOSTNAME>/sitecore/admin/Mongoshell.aspx
3. That's it! Enjoy! :-)

## Caveats
1. This tool is in BETA
2. Haven't been able to check it with Mongo, which has authentication. If you do so, and face any issues or not face any issues. Please do share your findings
3. Few Mongo Driver methods are deprecated. But they works so far!
4. Validated with Sitecore 8.2 only
5. Haven't validated with PRODUCTION Data. All checkes happened with plain Sitecore instance. So, please double check before you deploy this tool on PRODUCTION
6. Uses C#6 Features. So, if you see any error related to interpolated strings -- Please refer : http://stackoverflow.com/questions/31548699/how-to-use-c-sharp-6-with-web-site-project-type

>Found any bug? Got suggestion/feedback/comment, Share it here!
