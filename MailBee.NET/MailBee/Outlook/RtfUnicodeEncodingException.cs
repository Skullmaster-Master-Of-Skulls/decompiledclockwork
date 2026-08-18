using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005CD RID: 1485
	[Serializable]
	internal class RtfUnicodeEncodingException : RtfEncodingException
	{
		// Token: 0x06003178 RID: 12664 RVA: 0x000E70C7 File Offset: 0x000E60C7
		public RtfUnicodeEncodingException()
		{
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x000E70CF File Offset: 0x000E60CF
		public RtfUnicodeEncodingException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600317A RID: 12666 RVA: 0x000E70D8 File Offset: 0x000E60D8
		public RtfUnicodeEncodingException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600317B RID: 12667 RVA: 0x000E70E2 File Offset: 0x000E60E2
		protected RtfUnicodeEncodingException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
