using System;

namespace MailBee.Outlook
{
	// Token: 0x0200059D RID: 1437
	[Serializable]
	internal class NoSingleSectionException : HPSFRuntimeException
	{
		// Token: 0x06003039 RID: 12345 RVA: 0x000E2ED8 File Offset: 0x000E1ED8
		public NoSingleSectionException()
		{
		}

		// Token: 0x0600303A RID: 12346 RVA: 0x000E2EE0 File Offset: 0x000E1EE0
		public NoSingleSectionException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600303B RID: 12347 RVA: 0x000E2EE9 File Offset: 0x000E1EE9
		public NoSingleSectionException(Exception A_0) : base(A_0)
		{
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000E2EF2 File Offset: 0x000E1EF2
		public NoSingleSectionException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}
	}
}
