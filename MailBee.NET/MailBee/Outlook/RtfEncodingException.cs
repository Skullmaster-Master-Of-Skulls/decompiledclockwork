using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005C8 RID: 1480
	[Serializable]
	internal class RtfEncodingException : RtfParserException
	{
		// Token: 0x06003164 RID: 12644 RVA: 0x000E700E File Offset: 0x000E600E
		public RtfEncodingException()
		{
		}

		// Token: 0x06003165 RID: 12645 RVA: 0x000E7016 File Offset: 0x000E6016
		public RtfEncodingException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003166 RID: 12646 RVA: 0x000E701F File Offset: 0x000E601F
		public RtfEncodingException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003167 RID: 12647 RVA: 0x000E7029 File Offset: 0x000E6029
		protected RtfEncodingException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
