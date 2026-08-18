using System;

namespace MailBee.Outlook
{
	// Token: 0x02000598 RID: 1432
	[Serializable]
	internal class MissingSectionException : HPSFRuntimeException
	{
		// Token: 0x06002FFF RID: 12287 RVA: 0x000E257A File Offset: 0x000E157A
		public MissingSectionException()
		{
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000E2582 File Offset: 0x000E1582
		public MissingSectionException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000E258B File Offset: 0x000E158B
		public MissingSectionException(Exception A_0) : base(A_0)
		{
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000E2594 File Offset: 0x000E1594
		public MissingSectionException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}
	}
}
