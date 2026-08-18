using System;

namespace MailBee.Outlook
{
	// Token: 0x02000592 RID: 1426
	[Serializable]
	internal class HPSFRuntimeException : RuntimeException
	{
		// Token: 0x06002FEA RID: 12266 RVA: 0x000E244F File Offset: 0x000E144F
		public HPSFRuntimeException()
		{
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000E2457 File Offset: 0x000E1457
		public HPSFRuntimeException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000E2460 File Offset: 0x000E1460
		public HPSFRuntimeException(Exception A_0) : base(A_0.Message, A_0)
		{
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000E246F File Offset: 0x000E146F
		public HPSFRuntimeException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}
	}
}
