using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200008C RID: 140
	public class InvalidCipherTextException : CryptoException
	{
		// Token: 0x06000461 RID: 1121 RVA: 0x00016E15 File Offset: 0x00015E15
		public InvalidCipherTextException()
		{
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x00016E1D File Offset: 0x00015E1D
		public InvalidCipherTextException(string message) : base(message)
		{
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00016E26 File Offset: 0x00015E26
		public InvalidCipherTextException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
