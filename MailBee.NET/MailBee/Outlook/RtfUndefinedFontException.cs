using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005C2 RID: 1474
	[Serializable]
	internal class RtfUndefinedFontException : RtfInterpreterException
	{
		// Token: 0x06003150 RID: 12624 RVA: 0x000E6F55 File Offset: 0x000E5F55
		public RtfUndefinedFontException()
		{
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000E6F5D File Offset: 0x000E5F5D
		public RtfUndefinedFontException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000E6F66 File Offset: 0x000E5F66
		public RtfUndefinedFontException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000E6F70 File Offset: 0x000E5F70
		protected RtfUndefinedFontException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
