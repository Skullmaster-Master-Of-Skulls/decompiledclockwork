using System;

namespace MailBee.Outlook
{
	// Token: 0x0200059B RID: 1435
	[Serializable]
	internal class NoFormatIDException : HPSFRuntimeException
	{
		// Token: 0x06003031 RID: 12337 RVA: 0x000E2E90 File Offset: 0x000E1E90
		public NoFormatIDException()
		{
		}

		// Token: 0x06003032 RID: 12338 RVA: 0x000E2E98 File Offset: 0x000E1E98
		public NoFormatIDException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003033 RID: 12339 RVA: 0x000E2EA1 File Offset: 0x000E1EA1
		public NoFormatIDException(Exception A_0) : base(A_0)
		{
		}

		// Token: 0x06003034 RID: 12340 RVA: 0x000E2EAA File Offset: 0x000E1EAA
		public NoFormatIDException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}
	}
}
