using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000115 RID: 277
	internal static class SHA384CngFactory
	{
		// Token: 0x060008EF RID: 2287 RVA: 0x0001F066 File Offset: 0x0001D266
		internal static SHA384Cng CreateNew()
		{
			return new SHA384Cng();
		}
	}
}
