using System;
using Submarine.Core.Parser;
using Submarine.Core.Parser.Release;
using Submarine.Core.Release;
using Xunit;
using Xunit.Abstractions;

namespace Submarine.Core.Test.Parser.Release;

public class ReleaseParserServiceTest
{
	private readonly IParser<BaseRelease> _instance;

	public ReleaseParserServiceTest(ITestOutputHelper output)
		=> _instance = new ReleaseParserService(new XunitLogger<ReleaseParserService>(output),
			new LanguageParserService(new XunitLogger<LanguageParserService>(output)),
			new StreamingProviderParserService(new XunitLogger<StreamingProviderParserService>(output)),
			new QualityParserService(new XunitLogger<QualityParserService>(output)),
			new ReleaseGroupParserService(new XunitLogger<ReleaseGroupParserService>(output)));


	[Theory]
	[InlineData("Anime Show AKA Japanese Name S01 1080p WEB-DL Dual-Audio AAC 2.0 H.264-ZR", "Japanese Name",
		new[] { "Anime Show" })]
	[InlineData("Wheel of Poverty and Reality AKA Other Language Title 2021 1080p BluRay REMUX AVC DTS-HD MA 5.1-C0M3T",
		"Other Language Title", new[] { "Wheel of Poverty and Reality" })]
	[InlineData("Title One AKA Title Two AKA Title Three AKA Title Four S04 1080p WEB-DL AAC 2.0 H.264-NOGROUP",
		"Title Four", new[] { "Title One", "Title Two", "Title Three" })]
	[InlineData("Title.Someone.Returns.AKA.Title.Someone.Returns.2010.DVDRip.x264-HANDJOB-BUYMORE",
		"Title Someone Returns", new[] { "Title Someone Returns" })]
	public void Parse_ShouldParseAliases_WhenTitleContainsAKA(string input, string title, string[] aliases)
	{
		var parsed = _instance.Parse(input);

		Assert.Equal(input, parsed.FullTitle);
		Assert.Equal(title, parsed.Title);
		Assert.Equal(aliases, parsed.Aliases);
	}
	
	[Theory]
	[InlineData("Anime S01 2021 1080p WEB-DL AVC AAC 2.0 Dual Audio -ZR-", "Anime")]
	[InlineData("The Anime Title (Japanese Alias) S01 2021 1080p WEB-DL AVC AAC 2.0 Dual Audio -ZR-", "The Anime Title")]
	[InlineData("The Anime Title (Japanese Alias) S04E18 2022 1080p WEB-DL AVC AAC 2.0 Dual Audio -ZR-", "The Anime Title")]
	public void Parse_ShouldParseTitle_WhenReleaseIsAnime(string input, string title)
	{
		var parsed = _instance.Parse(input);
		
		Assert.Equal(title, parsed.Title);
	}

	[Theory]
	[InlineData("[HatSubs] Anime Title 1004 [E63F2984].mkv", 1004)]
	public void Parse_ShouldParseAbsoluteEpisode_WhenReleaseIsAnime(string input, int absoluteEpisode)
	{
		var parsed = _instance.Parse(input);

		Assert.Contains(absoluteEpisode, parsed.SeriesReleaseData?.AbsoluteEpisodes ?? throw new InvalidOperationException());
	}
}
