using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SpeechLib
{
	// Token: 0x0200019A RID: 410
	[CompilerGenerated]
	[TypeIdentifier("c866ca3a-32f7-11d2-9602-00c04f8ee628", "SpeechLib.SpeechVoiceSpeakFlags")]
	public enum SpeechVoiceSpeakFlags
	{
		// Token: 0x040008EA RID: 2282
		SVSFDefault,
		// Token: 0x040008EB RID: 2283
		SVSFlagsAsync,
		// Token: 0x040008EC RID: 2284
		SVSFPurgeBeforeSpeak,
		// Token: 0x040008ED RID: 2285
		SVSFIsFilename = 4,
		// Token: 0x040008EE RID: 2286
		SVSFIsXML = 8,
		// Token: 0x040008EF RID: 2287
		SVSFIsNotXML = 16,
		// Token: 0x040008F0 RID: 2288
		SVSFPersistXML = 32,
		// Token: 0x040008F1 RID: 2289
		SVSFNLPSpeakPunc = 64,
		// Token: 0x040008F2 RID: 2290
		SVSFParseSapi = 128,
		// Token: 0x040008F3 RID: 2291
		SVSFParseSsml = 256,
		// Token: 0x040008F4 RID: 2292
		SVSFParseAutodetect = 0,
		// Token: 0x040008F5 RID: 2293
		SVSFNLPMask = 64,
		// Token: 0x040008F6 RID: 2294
		SVSFParseMask = 384,
		// Token: 0x040008F7 RID: 2295
		SVSFVoiceMask = 511,
		// Token: 0x040008F8 RID: 2296
		SVSFUnusedFlags = -512
	}
}
