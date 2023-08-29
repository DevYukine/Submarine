using System;

namespace Submarine.Core.Release.Torrent;

/// <summary>
/// Flags for Torrent Releases
/// </summary>
[Flags]
public enum TorrentReleaseFlags
{
	/// <summary>
	/// No flags
	/// </summary>
	NONE = 0,

	FREELEECH = 1 << 1,
	HALFLEECH = 1 << 2,
	DOUBLE_UPLOAD = 1 << 3,
	OTHER_PROMOTION = 1 << 4,
	SCENE = 1 << 5
}
