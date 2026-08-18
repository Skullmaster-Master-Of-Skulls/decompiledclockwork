using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005BB RID: 1467
	[Serializable]
	internal class RtfColorException : RtfInterpreterException
	{
		// Token: 0x06003134 RID: 12596 RVA: 0x000E6E52 File Offset: 0x000E5E52
		public RtfColorException()
		{
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x000E6E5A File Offset: 0x000E5E5A
		public RtfColorException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000E6E63 File Offset: 0x000E5E63
		public RtfColorException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003137 RID: 12599 RVA: 0x000E6E6D File Offset: 0x000E5E6D
		protected RtfColorException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
