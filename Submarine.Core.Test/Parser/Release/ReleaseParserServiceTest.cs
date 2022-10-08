using System;
using Submarine.Core.Parser;
using Submarine.Core.Parser.Release;
using Submarine.Core.Quality;
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

	[Theory]
	[InlineData("[HorribleSubs] Anime - 12 [1080p].mkv", QualitySource.WEB_DL)]
	[InlineData("[SubsPlease] Anime - 14 (1080p) [3168B4D7].mkv", QualitySource.WEB_DL)]
	[InlineData("[Erai-raws] Anime 2nd Season - 11 [1080p][Multiple Subtitle] [ENG][POR-BR][SPA-LA][SPA][GER][ITA][RUS]", QualitySource.WEB_DL)]
	public void Parse_ShouldApplyEdgeCaseReleaseGroupQualitySourceMapping_WhenReleaseHasUnknownSourceAndGroupMatches(
		string input, QualitySource expected)
	{
		var parsed = _instance.Parse(input);
		
		Assert.Equal(expected, parsed.Quality.Resolution.Source);
	}

	[Theory]
	[InlineData("[Erai-raws] Anime - 01 ~ 24 [BD 720p][Multiple Subtitle]", QualitySource.BLURAY)]
	public void Parse_ShouldNotApplyEdgeCaseReleaseGroupQualitySourceMapping_IfReleaseSpecifiesQualitySource(string input, QualitySource expected)
	{
		var parsed = _instance.Parse(input);
		
		Assert.Equal(expected, parsed.Quality.Resolution.Source);
	}
}
