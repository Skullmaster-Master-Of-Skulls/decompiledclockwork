using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x0200008B RID: 139
	public class CryptoException : Exception
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x00016DFA File Offset: 0x00015DFA
		public CryptoException()
		{
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x00016E02 File Offset: 0x00015E02
		public CryptoException(string message) : base(message)
		{
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x00016E0B File Offset: 0x00015E0B
		public CryptoException(string message, Exception exception) : base(message, exception)
		{
		}
	}
}
