using System;

namespace MailBee.Outlook
{
	// Token: 0x0200059C RID: 1436
	[Serializable]
	internal class NoPropertySetStreamException : HPSFException
	{
		// Token: 0x06003035 RID: 12341 RVA: 0x000E2EB4 File Offset: 0x000E1EB4
		public NoPropertySetStreamException()
		{
		}

		// Token: 0x06003036 RID: 12342 RVA: 0x000E2EBC File Offset: 0x000E1EBC
		public NoPropertySetStreamException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003037 RID: 12343 RVA: 0x000E2EC5 File Offset: 0x000E1EC5
		public NoPropertySetStreamException(Exception A_0) : base(A_0)
		{
		}

		// Token: 0x06003038 RID: 12344 RVA: 0x000E2ECE File Offset: 0x000E1ECE
		public NoPropertySetStreamException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}
	}
}
