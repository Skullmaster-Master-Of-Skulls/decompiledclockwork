using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000461 RID: 1121
	[Flags]
	public enum X500DistinguishedNameFlags
	{
		// Token: 0x040025A5 RID: 9637
		None = 0,
		// Token: 0x040025A6 RID: 9638
		Reversed = 1,
		// Token: 0x040025A7 RID: 9639
		UseSemicolons = 16,
		// Token: 0x040025A8 RID: 9640
		DoNotUsePlusSign = 32,
		// Token: 0x040025A9 RID: 9641
		DoNotUseQuotes = 64,
		// Token: 0x040025AA RID: 9642
		UseCommas = 128,
		// Token: 0x040025AB RID: 9643
		UseNewLines = 256,
		// Token: 0x040025AC RID: 9644
		UseUTF8Encoding = 4096,
		// Token: 0x040025AD RID: 9645
		UseT61Encoding = 8192,
		// Token: 0x040025AE RID: 9646
		ForceUTF8Encoding = 16384
	}
}
