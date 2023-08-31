# Submarine

Submarine is a **Work in Progress** PVR for Usenet and BitTorrent users.
It can monitor multiple feeds for new releases of your favorite shows and movies and will grab, sort and rename them. It can also be configured to automatically upgrade the quality of files already downloaded when a better quality format becomes available (DVD -> Bluray as example).

Submarine is also based on [Sonarr](https://github.com/Sonarr/Sonarr/) & [Radarr](https://github.com/Radarr/Radarr/) but is completly written from scratch to have a fresh and up to date codebase.

## Planned Features

### Sonarr/Radarr like Features
- [ ] Support for major Platforms (Windows, Linux, macOS, Docker)
- [ ] Monitoring multiple feeds for new Releases
- [ ] Automatic Quality upgrade
- [ ] Failed download handling
- [ ] Manual searching
- [ ] Full integration for popular Usenet download clients (SABnzbd & NZBGet)
- [ ] Full integration for popular Bittorrent download clients (QBittorrent, Deluge, rTorrent, Transmission)
- [ ] Full integration with Kodi & Plex (notification, library update)

### Standalone Features
- [ ] Multiple profiles for the Series/Movie to allow grabbing multiple Releases for the same Episode/Movie
- [ ] Swapping out the metadata provider for different epsiode/season numbering
- [ ] A nicer UI which is responsive even when Submarine does heavy work in the background
- [ ] No long running HTTP requests to the frontend which could timeout due to Reverse Proxy settings
- [x] Fully Documented API with OpenAPI implementation


## Getting Started

TBA

### Prerequisites

TBA

### Installing

TBA

## Deployment

TBA

## Built With

* [C#](https://docs.microsoft.com/en-us/dotnet/csharp/) - Programming Language 
* [.NET 7](https://docs.microsoft.com/en-us/dotnet/) - Runtime
* [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/) - Web Framework
* [EntityFramework Core](https://docs.microsoft.com/en-us/ef/core/) - Database ORM
* [xUnit](https://github.com/xunit/xunit) - Unit Tests

## Contributing

TBA

## Versioning

We use [SemVer](http://semver.org/) for versioning. For the versions available, see the [tags on this repository](https://github.com/SimplyMedia/Submarine/tags). 

## Authors

* **DevYukine** - *Initial work* - [DevYukine](https://github.com/DevYukine)

See also the list of [contributors](https://github.com/SimplyMedia/Submarine/contributors) who participated in this project.

## License

TBA

## Acknowledgments

TBA
