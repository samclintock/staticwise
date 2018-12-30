# StaticWise - Static Blog Generator #

An open source static blog generator built using .NET and C#.

![StaticWise.Console](https://github.com/stevenmclintock/StaticWise/blob/master/Resources/StaticWise.Console.PNG?raw=true)

# Why Use StaticWise?

If you are serious about blogging, are interested in the ownership of your content, or are conscious of the loading time of your website, StaticWise is an open source project for you.

StaticWise will enable you to write blog articles, or website pages in the popular [Markdown](https://daringfireball.net/projects/markdown/ "Markdown on Daring Fireball") publishing format and compile these files to a blog or website. Once compiled using the StaticWise executable, HTML will be generated that you can upload to a web hosting company *([GoDaddy](https://www.godaddy.com "GoDaddy"), [DreamHost](https://www.dreamhost.com/ "DreamHost"), etc.)*, or publish to a [GitHub](https://github.com/ "GitHub") repository and share using [GitHub Pages](https://pages.github.com/ "GitHub Pages").

## .NET and C# ##

StaticWise is built using the popular [Microsoft .NET Framework](https://www.microsoft.com/net/ ".NET") and will *currently* target version 4.5.2.

If you are a .NET developer and use the [C# programming language](https://github.com/dotnet/csharplang "C# official GitHub repository"), you can get started in a few minutes by using [Visual Studio IDE](https://www.visualstudio.com/ "Visual Studio IDE") for Windows or Mac.

![StaticWise.SolutionExplorer](https://github.com/stevenmclintock/StaticWise/blob/master/Resources/StaticWise.SolutionExplorer.PNG?raw=true)

## Extensible Components ##

StaticWise includes functionality to compile a static blog or website, and is built on a foundation that can be extended using components.

![StaticWise.Components](https://github.com/stevenmclintock/StaticWise/blob/master/Resources/StaticWise.Components.PNG?raw=true)

Inherit a C# Interface to easily build new functionality into StaticWise. If StaticWise does not meet your requirements, use the [C# programming language](https://github.com/dotnet/csharplang "C# official GitHub repository") to quickly build new functionality.

![StaticWise.Components.IArchivePage](https://github.com/stevenmclintock/StaticWise/blob/master/Resources/StaticWise.Components.IArchivePage.PNG?raw=true)

# Getting Started

StaticWise is built to read a centralized [JSON](https://www.json.org/ "JavaScript Object Notation") *(JavaScript Object Notation)* configuration file. The StaticWise executable will prompt you for a path to your configuration file *(C:\AwesomeBlog\config.json)* and use the settings inside to compile your blog or website.

## Demo Configuration File

Please use our demo configuration file to get started and change the settings as necessary to compile a custom static blog or website.

    {
  		"title": "My Awesome Blog",
  		"description": "An awesome blog built using the StaticWise open source project",
  		"domainName": "http://www.staticwise.com",
  		"favicon": "favicon.ico",
  		"feedEntryCount": 10,
  		"indexDestinationName": "index",
  		"feedDestinationName": "feed",
  		"mediaDestinationName": "media",
  		"archiveDirectoryName": "archive",
  		"archivePageName": "page",
  		"meta": [
    		{
      			"attribute": [
        			{
          				"key": "charset",
          				"value": "utf-8"
        			}
      			]
    		},
    		{
		      "attribute": [
		        {
		          "key": "http-equiv",
		          "value": "X-UA-Compatible"
		        },
		        {
		          "key": "content",
		          "value": "IE=edge,chrome=1"
		        }
		      ]
		    },
		    {
		      "attribute": [
		        {
		          "key": "name",
		          "value": "viewport"
		        },
		        {
		          "key": "content",
		          "value": "width=device-width, initial-scale=1.0"
		        }
		      ]
		    }
  		],
  		"includes": {
    		"siteHeader": "header.inc",
    		"siteFooter": "footer.inc",
    		"postHeader": "post-header.inc",
    		"postFooter": "post-footer.inc"
  		},
  		"scripts": {
    		"externalCSS": [
	      		"https://fonts.googleapis.com/css?family=Merriweather:400,400i,700|Kaushan+Script",
	      		"https://maxcdn.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css"
    		],
    		"internalCSS": [
      			"style.css"
    		],
    		"externalJS": [],
    		"internalJS": [
      			"analytics.js"
    		]
  		},
		"code": {
			"documentType": "<!DOCTYPE html>",
			"html": [ "<html lang=\"en\">", "</html>" ],
			"head": [ "<head>", "</head>" ],
			"body": [ "<body>", "</body>" ],
			"archivePostContainer": [ "<article class=\"post-short\">", "</article>" ],
			"archivePostTitle": [ "<h2>", "</h2>" ],
			"individualPostContainer": [ "<article class=\"post-long\">", "</article>" ],
			"individualPostTitle": [ "<h1>", "</h1>" ],
			"pageContainer": [ "<article class=\"post-long\">", "</article>" ],
			"pageTitle": [ "<h1>", "</h1>" ],
			"paginationContainer": [ "<ul class=\"pagination\">", "</ul>" ],
			"paginationItem": [ "<li>", "</li>" ],
			"publishedDateFormat": "D",
			"publishedOnText": "<i class=\"fa fa-clock-o\" aria-hidden=\"true\"></i> Published on ",
			"readMoreText": "<strong>Read More</strong>"
		},
		"includesDirectory": "Includes",
		"scriptsDirectory": "Scripts",
		"postsDirectory": "Posts",
		"draftsDirectory": "Drafts",
		"pagesDirectory": "Pages",
		"mediaDirectory": "Media",
		"outputDirectory": "Output"
	}

## Demo Blog Directory

Once your configuration file is ready, you can create a new blog directory on your PC using the structure below.

	C:\AwesomeBlog
	│   config.json
	│   favicon.ico
	│
	├───Includes
	│       footer.inc (HTML for the footer of your blog or website)
	│       header.inc (HTML for the header of your blog or website)
	│       post-footer.inc (HTML for the post footer of your blog)
	│       post-header.inc (HTML for the post header of your blog)
	│
	├───Media
	│       logo.png
	│
	├───Output (HTML output of your compiled blog or website)
	├───Pages (Markdown formatted website pages)
	│       about.md
	│       contact.md
	│
	├───Posts (Markdown formatted blog posts)
	│   │   20180201-first-blog-post.md (Published date is part of filename)
	│   │
	│   └───Drafts (Not ready to be published)
	│           20180219-second-blog-post.md
	│
	└───Scripts (JavaScript or CSS)
	        analytics.js
	        style.css

# Demo Post or Page #

Please use the document structure below for StaticWise to consume [Markdown](https://daringfireball.net/projects/markdown/ "Markdown on Daring Fireball") formatted documents.

	{
		"title": "First Blog Post!",
		"friendlyUrl": "first-blog-post",
		"description": "My first blog post on my awesome blog"
	}
	
	This is my **first** blog post!

# Contributions #

Please contribute to the source code and make StaticWise even better than it is today! If you would like to contribute, please submit a new issue or pull request.

# Contact #

Please contact [@samclintock](https://www.twitter.com/samclintock "Steven McLintock on Twitter") if you have any questions or feedback.
