using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005C1 RID: 1473
	[Serializable]
	internal class RtfUndefinedColorException : RtfInterpreterException
	{
		// Token: 0x0600314C RID: 12620 RVA: 0x000E6F30 File Offset: 0x000E5F30
		public RtfUndefinedColorException()
		{
		}

		// Token: 0x0600314D RID: 12621 RVA: 0x000E6F38 File Offset: 0x000E5F38
		public RtfUndefinedColorException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000E6F41 File Offset: 0x000E5F41
		public RtfUndefinedColorException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000E6F4B File Offset: 0x000E5F4B
		protected RtfUndefinedColorException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
