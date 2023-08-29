using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;
using Submarine.Core.Release;
using Submarine.Core.Release.Usenet;
using Submarine.Core.Release.Util;
using Submarine.Core.Util.Extensions;
using Submarine.Core.Util.RegEx;
using Submarine.Core.Validator;

namespace Submarine.Core.Parser.Release;

/// <summary>
/// Service to parse a release with Usenet standards
/// </summary>
public class UsenetReleaseParserService : IParser<UsenetRelease>
{
	private static readonly RegexReplace CleanReleaseGroupRegex = new(
		@"(-(RP|1|NZBGeek|Obfuscated|Scrambled|sample|Pre|postbot|xpost|Rakuv[a-z0-9]*|WhiteRev|BUYMORE|AsRequested|AlternativeToRequested|GEROV|Z0iDS3N|Chamele0n|4P|4Planet|AlteZachen|RePACKPOST))+$",
		string.Empty,
		RegexOptions.IgnoreCase | RegexOptions.Compiled);

	//Regex to detect whether the title was reversed.
	private static readonly Regex ReversedTitleRegex =
		new(@"(?:^|[-._ ])(p027|p0801|\d{2,3}E\d{2}S)[-._ ]", RegexOptions.Compiled);

	private readonly ILogger<UsenetReleaseParserService> _logger;

	private readonly IParser<BaseRelease> _releaseParserService;

	private readonly UsenetReleaseValidatorService _usenetReleaseValidatorService;

	/// <summary>
	/// Creates a new <see cref="UsenetReleaseParserService"/>
	/// </summary>
	/// <param name="logger">The <see cref="ILogger{TCategoryName}"/></param>
	/// <param name="usenetReleaseValidatorService">The <see cref="UsenetReleaseValidatorService"/></param>
	/// <param name="releaseParserService">The <see cref="ReleaseParserService"/></param>
	public UsenetReleaseParserService(
		ILogger<UsenetReleaseParserService> logger,
		UsenetReleaseValidatorService usenetReleaseValidatorService,
		IParser<BaseRelease> releaseParserService)
	{
		_logger = logger;
		_usenetReleaseValidatorService = usenetReleaseValidatorService;
		_releaseParserService = releaseParserService;
	}

	/// <summary>
	/// Parses a release with Usenet standards
	/// </summary>
	/// <param name="input">Release Title</param>
	/// <returns>a <see cref="UsenetRelease"/></returns>
	public UsenetRelease Parse(string input)
	{
		_logger.LogDebug("Starting parse of {Input} with Usenet standards", input);

		_usenetReleaseValidatorService.Validate(input);

		if (ReversedTitleRegex.IsMatch(input))
		{
			var titleWithoutExtension = ReleaseUtil.RemoveFileExtension(input).Reverse();

			// Rebuild String
			input = titleWithoutExtension + input[titleWithoutExtension.Length..];

			_logger.LogDebug("Reversed name detected. Converted to '{Input}'", input);
		}

		input = CleanReleaseGroupRegex.Replace(input);

		var baseRelease = _releaseParserService.Parse(input);

		return baseRelease.ToUsenet();
	}
}
