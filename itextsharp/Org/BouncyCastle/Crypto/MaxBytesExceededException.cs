using System;

namespace Org.BouncyCastle.Crypto
{
	// Token: 0x02000248 RID: 584
	public class MaxBytesExceededException : CryptoException
	{
		// Token: 0x06001679 RID: 5753 RVA: 0x000829D9 File Offset: 0x000819D9
		public MaxBytesExceededException()
		{
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x000829E1 File Offset: 0x000819E1
		public MaxBytesExceededException(string message) : base(message)
		{
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x000829EA File Offset: 0x000819EA
		public MaxBytesExceededException(string message, Exception e) : base(message, e)
		{
		}
	}
}
