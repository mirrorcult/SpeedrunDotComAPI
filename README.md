# SpeedrunDotComAPI

`SpeedrunDotComAPI` is a .NET wrapper for the speedrun.com API.

The [other C# wrapper](https://github.com/LiveSplit/SpeedrunComSharp) made by the LiveSplit team is quite old and made for .NET Framework. I also just personally think this wrapper is quite a bit cleaner and more modern in general.

This API binding is around 95% complete. The things that are omitted are either things that I don't think could be implemented cleanly or which I felt would never be used by any sane human being (the `/guests` endpoint, for instance). If the unimplemented behavior is desirable to you, feel free to submit a pull request (or just yell at me).

## How to Use

Import the project and create a new instance of `SRCApiClient`. If you have an SRC API token, you can pass it into the constructor here.

```csharp
using SpeedrunDotComAPI;

...

SRCApiClient src = new SRCApiClient(cfg.SpeedrunToken);
```

This client has sub-clients for the main API endpoints such as `GameApiClient`, `RunApiClient`, etc, which then have functions for accessing the API. There also exists `RawDataQuery` which just takes in a query after the main `/api/v1/` endpoint and returns `dynamic`, if you really need to use it.

```csharp
CategoryModel[] sm64Categories = await src.Games.GetSingleGameCategories("sm64");
CategoryModel seventyStar = sm64Categories.First(c => c.Name == "70 Star");
LeaderboardModel[] leaderboards = await src.Categories.GetSingleCategoryRecords(seventyStar.Id); // Returns a list of leaderboards because of categories with ILs.
RunPlaceModel worldRecord = leaderboards[0].Runs.First(r => r.Place == 1);
```

Returned data from these functions is in the form of pure data struct `Models` and have no helper functions for navigating them. This may change in the future for things that make sense but right now this wrapper is attempting to be very simple.

For the small-scape sub-APIs like platforms, regions, engines, etc, these are contained in `MetadataApiClient` as it was easiest to deduplicate them that way.

Methods that deal with API calls directly do not do any internal exception handling. It's expected of the user to handle `HttpRequestException` if you are capable of passing in an invalid ID to one of these functions. Methods like `GetAllGames` just return an empty list for bad filter options, so you don't need to handle it there. `JsonException` is not expected to be thrown, so this should be reported here as a bug if it does happen.

`ProfileApiClient` and `NotificationsApiClient` both require a proper API key passed into the initial client in order to function.

### Filter and Embed Options

API methods that query over large sets of elements can have filter options passed into them, in accordance with the SRC API. For paginated resources, you can change the `Max` elements returned or the page `Offset`. All filter options can be ordered by certain values (see the SRC API for what these are, sorry see issue #1), and have the order direction changed.

```csharp
GameModel[] filtered = await src.Games.GetAllGames(new() { Name = "super mario", _Bulk = true });
```

API methods that can have resources embedded (e.g. embedding the category resources into a game resource) can specify which resources to embed.

```csharp
GameModel sm64 = await src.Games.GetSingleGame("sm64", new() { Categories = true, Genres = true });
```

## Contact 

Contact me at `mirrorcult#9528` on Discord if you have questions or concerns, or just create a GitHub issue.