using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005BC RID: 1468
	[Serializable]
	internal class RtfInterpreterException : RtfException
	{
		// Token: 0x06003138 RID: 12600 RVA: 0x000E6E77 File Offset: 0x000E5E77
		public RtfInterpreterException()
		{
		}

		// Token: 0x06003139 RID: 12601 RVA: 0x000E6E7F File Offset: 0x000E5E7F
		public RtfInterpreterException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600313A RID: 12602 RVA: 0x000E6E88 File Offset: 0x000E5E88
		public RtfInterpreterException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600313B RID: 12603 RVA: 0x000E6E92 File Offset: 0x000E5E92
		protected RtfInterpreterException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
