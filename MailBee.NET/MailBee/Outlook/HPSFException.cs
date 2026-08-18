using System;

namespace MailBee.Outlook
{
	// Token: 0x02000591 RID: 1425
	[Serializable]
	internal class HPSFException : Exception
	{
		// Token: 0x06002FE5 RID: 12261 RVA: 0x000E241E File Offset: 0x000E141E
		public HPSFException()
		{
		}

		// Token: 0x06002FE6 RID: 12262 RVA: 0x000E2426 File Offset: 0x000E1426
		public HPSFException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06002FE7 RID: 12263 RVA: 0x000E242F File Offset: 0x000E142F
		public HPSFException(Exception A_0) : base("", A_0)
		{
		}

		// Token: 0x06002FE8 RID: 12264 RVA: 0x000E243D File Offset: 0x000E143D
		public HPSFException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x000E2447 File Offset: 0x000E1447
		public Exception Reason
		{
			get
			{
				return base.InnerException;
			}
		}
	}
}
