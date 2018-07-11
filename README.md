# ASP.NET Core MVC URL Shortener
A basic implementation of an URL shortener web application using ASP.NET Core MVC and Entity Framework Core.

## Goal

I've started this little project in order to have a support during my approach of ASP.NET Core MVC.

My idea was to implement a simple web application using the framework.

I've chosen to implement an URL shortener application because it's pretty simple.

## Algorithm

So, how works an URL shortener?

Basicaly, we store the URL in database, so it has a numeric ID, an we convert it to a another base in order to have a "stringified" version of the ID.

When we have the short URL the process is:
- convert the "stringified" ID to the numeric ID.
- load the data from DB.
- redirect to the original URL using an HTTP redirection.

More theory here in [this stackoverflow topic](https://stackoverflow.com/questions/742013/how-to-code-a-url-shortener).

## Implementation

For the stringification, I've chosen base 62 with this alphabet : "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_".

I've used the ShortURL class by delight.im to do this work. See [this link](https://github.com/delight-im/ShortURL) for more information about it.

You can also see this link: [https://gist.github.com/dgritsko/9554733](https://gist.github.com/dgritsko/9554733).

## Usage

First, you have to type `dotnet restore` in order to retrieve the dependancies of the project.

The projet is using SQLite as DB backed. The data file is named `shorturls.db` by default.
You can change this by modifying the line 35 of the file `Startup.cs`.

In order to init the DB schema, you have to rune the command `dotnet ef database update`.

Then, simply type `dotnet run` on your command prompt and then browse to http://localhost:5000.

**Screenshot of the web application:**

![Screenshot of the ASP.NET Core MVC URL Shortener web application](https://github.com/fxmauricard/aspnetcore-url-shortener/blob/master/UrlShortener-screenshot.png)

## Conclusion

ASP.NET Core allowed me to do this simple app in the quickest way. So that, I've doing more investigations about the framework.
