using System;

namespace MailBee.Outlook
{
	// Token: 0x020005A2 RID: 1442
	[Serializable]
	internal class WritingNotSupportedException : UnsupportedVariantTypeException
	{
		// Token: 0x0600307B RID: 12411 RVA: 0x000E33DC File Offset: 0x000E23DC
		public WritingNotSupportedException(long A_0, object A_1) : base(A_0, A_1)
		{
		}
	}
}
