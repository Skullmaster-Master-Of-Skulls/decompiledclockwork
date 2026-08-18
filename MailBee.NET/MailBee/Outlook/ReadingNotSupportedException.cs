using System;

namespace MailBee.Outlook
{
	// Token: 0x0200059E RID: 1438
	[Serializable]
	internal class ReadingNotSupportedException : UnsupportedVariantTypeException
	{
		// Token: 0x0600303D RID: 12349 RVA: 0x000E2EFC File Offset: 0x000E1EFC
		public ReadingNotSupportedException(long A_0, object A_1) : base(A_0, A_1)
		{
		}
	}
}
