using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000118 RID: 280
	internal static class SHA512CngFactory
	{
		// Token: 0x060008FA RID: 2298 RVA: 0x0001F16E File Offset: 0x0001D36E
		internal static SHA512Cng CreateNew()
		{
			return new SHA512Cng();
		}
	}
}
