# Localized Data Annotations Validator Component for Blazor [![NuGet Package](https://img.shields.io/nuget/v/Toolbelt.Blazor.LocalizedDataAnnotationsValidator.svg)](https://www.nuget.org/packages/Toolbelt.Blazor.LocalizedDataAnnotationsValidator/)

## Summary

This is the Data Annotations Validator Component for Blazor to localize the validation error messages, based on `Microsoft.Extensions.Localization`.

_**Live Demo**_ - [https://demo-blazor-localized-data-annotations-validator.azurewebsites.net/](https://demo-blazor-localized-data-annotations-validator.azurewebsites.net/)

![fig1](https://raw.githubusercontent.com/jsakamoto/Toolbelt.Blazor.LocalizedDataAnnotationsValidator/master/.assets/fig1.png)

### [NOTICE]

This component will works fine on "Server-side Blazor", not "Client-side Blazor" at this time.

The supporting of `Microsoft.Extensions.Localization` based localization on Client-side Blazor is weak, therefore it will need some hacks. (see also: https://dev.to/5argon/comment/gdch)

Another way, ["Blazor I18n Text"](https://www.nuget.org/packages/Toolbelt.Blazor.I18nText/) service will work fine on both Server-side Blazor and Client-side Blazor. However, it is not integrated `Microsoft.Extensions.Localization` based localization at this time.

## How to use?

### Step 1. Setup "IStringLocalizer" infrastructure on your Blazor app 

See also:

- ["Globalization and localization in ASP.NET Core | Microsoft Docs"](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/localization?view=aspnetcore-3.0)

Of course, you should create resource files (.resx) for data annotation validation error messages, too.

![fig.2](https://raw.githubusercontent.com/jsakamoto/Toolbelt.Blazor.LocalizedDataAnnotationsValidator/master/.assets/fig2.png)

### Step 2. Install this package

Like this:

```shell
> dotnet add package Toolbelt.Blazor.LocalizedDataAnnotationsValidator
```

### Step3. Replace the data annotations validator component

Rewrite the implementation of the validation on your app.

At first, open the "Toolbelt.Blazor.Forms" namespace, in the `_Imports.razor`.

```razor
...
@using Toolbelt.Blazor.Forms
```


Next, replace the `DataAnnotationsValidator` to `LocalizedDataAnnotationsValidator`.

For example, in your `.razor` file, rewrite it from:

```html
<EditForm Model="..." ...>
  <DataAnnotationsValidator />
  ...
```

to:

```html
<EditForm Model="..." ...>
  <!-- « Replace this component! -->
  <LocalizedDataAnnotationsValidator />
  ...
```

That's all!

## Sample Source Code

You can get the source code of Live Demo site from this repository.

- [https://github.com/jsakamoto/Toolbelt.Blazor.LocalizedDataAnnotationsValidator/tree/master/SampleApp](https://github.com/jsakamoto/Toolbelt.Blazor.LocalizedDataAnnotationsValidator/tree/master/SampleApp)

## Release Note

- **v.1.0.1**
  - Fix: concurrent access problem
  - Change license from MPL-2.0 to Apache-2.0
- **v.1.0.0.** - Initial release.

## License

[Apache License Version 2.0](https://github.com/jsakamoto/Toolbelt.Blazor.LocalizedDataAnnotationsValidator/blob/master/LICENSE)