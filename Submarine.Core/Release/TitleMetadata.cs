using System.Collections.Generic;

namespace Submarine.Core.Release;

public abstract record TitleMetadata(string MainTitle, IReadOnlyList<string> Aliases, string? Year, string? Group, string? Hash);
