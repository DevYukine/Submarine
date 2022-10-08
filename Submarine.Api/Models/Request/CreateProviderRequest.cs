using Submarine.Core.Provider;

namespace Submarine.Api.Models.Request;

public record CreateProviderRequest
{
	public string Name { get; set; }

	public Protocol Protocol { get; set; }

	public ProviderMode Mode { get; set; }

	public string Url { get; set; }

	public string ApiKey { get; set; }

	public short Priority { get; set; }

	public Provider ToProvider()
		=> Protocol switch
		{
			Protocol.BITTORRENT => new BittorrentProvider
			{
				Name = Name,
				Mode = Mode,
				Url = Url,
				ApiKey = ApiKey,
				Priority = Priority
			},
			Protocol.USENET => new UsenetIndexer
			{
				Name = Name,
				Mode = Mode,
				Url = Url,
				ApiKey = ApiKey,
				Priority = Priority
			},
			Protocol.XDCC => throw new NotImplementedException(),
			_ => throw new ArgumentOutOfRangeException()
		};
}
