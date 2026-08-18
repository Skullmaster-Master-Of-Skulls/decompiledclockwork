using System;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	// Token: 0x0200000A RID: 10
	internal static class CryptoThrowHelper
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000023A9 File Offset: 0x000005A9
		public static CryptographicException ToCryptographicException(this int hr)
		{
			throw new CryptographicException(hr);
		}
	}
}
