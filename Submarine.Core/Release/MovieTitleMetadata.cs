using System.Collections.Generic;

namespace Submarine.Core.Release;

public record MovieTitleMetadata(string MainTitle, IReadOnlyList<string> Aliases, string? Year, string? Group, string? Hash, string? Edition) 
	: TitleMetadata(MainTitle, Aliases, Year, Group, Hash);
