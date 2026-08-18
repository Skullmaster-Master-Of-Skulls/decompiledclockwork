using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005CA RID: 1482
	[Serializable]
	internal class RtfHexEncodingException : RtfEncodingException
	{
		// Token: 0x0600316C RID: 12652 RVA: 0x000E7058 File Offset: 0x000E6058
		public RtfHexEncodingException()
		{
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x000E7060 File Offset: 0x000E6060
		public RtfHexEncodingException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x000E7069 File Offset: 0x000E6069
		public RtfHexEncodingException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x000E7073 File Offset: 0x000E6073
		protected RtfHexEncodingException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
