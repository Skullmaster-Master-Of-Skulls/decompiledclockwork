using System;

namespace MailBee.Outlook
{
	// Token: 0x020005A1 RID: 1441
	[Serializable]
	internal class UnexpectedPropertySetTypeException : HPSFException
	{
		// Token: 0x06003077 RID: 12407 RVA: 0x000E33B8 File Offset: 0x000E23B8
		public UnexpectedPropertySetTypeException()
		{
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x000E33C0 File Offset: 0x000E23C0
		public UnexpectedPropertySetTypeException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x000E33C9 File Offset: 0x000E23C9
		public UnexpectedPropertySetTypeException(Exception A_0) : base(A_0)
		{
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x000E33D2 File Offset: 0x000E23D2
		public UnexpectedPropertySetTypeException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}
	}
}
