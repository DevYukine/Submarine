using System.Collections.Generic;

namespace Submarine.Core.Release;

public record TitleMetadata(string MainTitle, IReadOnlyList<string> Aliases, IReadOnlyCollection<int>? Seasons, IReadOnlyCollection<int>? Episodes, IReadOnlyCollection<int>? AbsoluteEpisodes, string? Year, string? Group, string? Hash);
