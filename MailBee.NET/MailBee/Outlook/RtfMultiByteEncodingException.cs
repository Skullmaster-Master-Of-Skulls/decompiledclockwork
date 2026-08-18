using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005CC RID: 1484
	[Serializable]
	internal class RtfMultiByteEncodingException : RtfEncodingException
	{
		// Token: 0x06003174 RID: 12660 RVA: 0x000E70A2 File Offset: 0x000E60A2
		public RtfMultiByteEncodingException()
		{
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x000E70AA File Offset: 0x000E60AA
		public RtfMultiByteEncodingException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x000E70B3 File Offset: 0x000E60B3
		public RtfMultiByteEncodingException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x000E70BD File Offset: 0x000E60BD
		protected RtfMultiByteEncodingException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
