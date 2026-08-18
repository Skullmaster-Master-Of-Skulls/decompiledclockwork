using System;

namespace System.Security.Cryptography
{
	// Token: 0x02000112 RID: 274
	internal static class SHA256CngFactory
	{
		// Token: 0x060008E4 RID: 2276 RVA: 0x0001EF5E File Offset: 0x0001D15E
		internal static SHA256Cng CreateNew()
		{
			return new SHA256Cng();
		}
	}
}
