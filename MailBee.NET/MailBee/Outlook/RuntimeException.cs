using System;

namespace MailBee.Outlook
{
	// Token: 0x02000593 RID: 1427
	[Serializable]
	internal class RuntimeException : Exception
	{
		// Token: 0x06002FEE RID: 12270 RVA: 0x000E2479 File Offset: 0x000E1479
		public RuntimeException()
		{
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000E2481 File Offset: 0x000E1481
		public RuntimeException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000E248A File Offset: 0x000E148A
		public RuntimeException(Exception A_0) : base("", A_0)
		{
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000E2498 File Offset: 0x000E1498
		public RuntimeException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}
	}
}
