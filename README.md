HandlebarsDotNet
================

Provides a customizable IBundleTransform for precompilation of handlebars.js views.

```
PM> Install-Package HandlebarsDotNet
```

## Usage

In your ASP.NET MVC 4 application's `global.asax` file, register a new `HandlebarsBundleTransform` specifying options in the constructor:

```csharp
public class MvcApplication : System.Web.HttpApplication
{
  protected void Application_Start()
	{
	    BundleTable.Bundles.Add(new Bundle("~/bundle/js/app/templates/", 
	        new HandlebarsBundleTransform("templates", true, "/assets/js/app/templates/"))
	            .IncludeDirectory("~/assets/js/app/templates/", "*.hbs")
	            .IncludeDirectory("~/assets/js/app/templates/sub/", "*.hbs"));

	    BundleTable.EnableOptimizations = true;
	}	
}
```

- The first constructor parameter is the name of the JavaScript variable on which you wish to store the templates (default = "templates").
- The second constructor parameter is a boolean indicating whether or not the compiled views should be minified (default = true).
- The third constructor parameter is the root virtual path where the templates are stored in your project (default = "/").

In your ASP.NET view, render the templates scripts to the page:

```
@Scripts.Render("~/bundle/js/app/templates/")
```

And finally, a variable with the name you provided in the constructor will be available in script. Note that the dictionary keys/property names follow the directory structure of the rootDirectory (also supplied in the `HandlebarsBundleTransform` constructor).

```javascript
$(function () {
    var context = {
        exampleTitle: "Hello World",
        exampleText: "This is rendered using a handlebars template precompiled by an ASP.NET BundleTransform."
    };

    $('#main').html(templates['example'](context));
    $('#sub').html(templates['sub/another'](context));
});
```

## Thanks

This project is largely based on https://github.com/Myslik/csharp-ember-handlebars/ and is just a dumbed-down version for my own personal use with backbone.js instead of ember.js. Putting it up in hopes it helps someone else with a similar project.
