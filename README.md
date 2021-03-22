# [Enterspeed Sitecore Source](https://www.enterspeed.com/) &middot; [![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](./LICENSE) [![NuGet version](https://img.shields.io/nuget/v/Enterspeed.Source.SitecoreCms.V9)](https://www.nuget.org/packages/Enterspeed.Source.SitecoreCms.V9/) [![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg)](https://github.com/enterspeedhq/enterspeed-source-sitecore-cms/pulls) [![Twitter](https://img.shields.io/twitter/follow/enterspeedhq?style=social)](https://twitter.com/enterspeedhq)

## Installation

To get started with the Enterspeed Sitecore integration you should follow these steps:

### Install the NuGet package

Using the Package Manager

```powershell
Install-Package Enterspeed.Source.SitecoreCms.V9 -Version <version>
```

The NuGet package installs config files into this directory; verify that this folder contains config files

```powershell
~\App_Config\Include\Enterspeed
```

### Add required settings to your include file

You need to add ```<settings>``` to your preferred choice of include config file.

* ```Enterspeed.BaseUrl```
  * The value of this setting should be the Enterspeed API base URL supplied to you
* ```Enterspeed.ApiKey```
  * The value of this setting should be the Enterspeed API key supplied to you
* ```Enterspeed.EnabledSites```
  * The value of this setting should be a comma-separated list of site names (or one, if you only have a single site in the solution)

## How it works

This connector revolves a lot around the setting above, EnabledSites. The philosophy is that you can publish every item in your Sitecore solution and only items that belong to your enabled sites are being ingested to/deleted in Enterspeed.

### Content

Content items that are being sent to Enterspeed, will have references to the renderings inserted on them, along with information of the fields of the given datasources inserted on these renderings.

Each rendering reference sent to Enterspeed could have these properties:

* ```renderingId``` - the Enterspeed ID for this rendering
* ```renderingPlaceholder``` - the Sitecore placeholder inserted on either the presentation details or the rendering itself
* ```renderingParameters``` - an array of key/values inserted on the rendering options
* ```renderingDatasource``` - the properties (fields) and their values of the datasource item inserted on the rendering

### Renderings

Renderings are processed separately, as well, but only if the rendering is inserted on the presentation details of a content item which resides in an enabled site. This means that newly created renderings are not processed until they're inserted to be rendered on content for which you have enabled.

## Roadmap

* Improved logging.
* Functionality to trigger a push to Enterspeed from a contextaul ribbon/action in Sitecore.
* Moving settings to an Enterspeed configuration item within Sitecore, instead of using config files.
* Forms support.
* TBD.

## Contributing

Pull requests are very welcome.  
Please fork this repository and make a PR when you are ready.  

Otherwise you are welcome to open an Issue in our [issue tracker](https://github.com/enterspeedhq/enterspeed-source-sitecore-cms/issues).

## License

Enterspeed Sitecore Source is [MIT licensed](./LICENSE)
