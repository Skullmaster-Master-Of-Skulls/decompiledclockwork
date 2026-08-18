using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005C9 RID: 1481
	[Serializable]
	internal class RtfParserException : RtfException
	{
		// Token: 0x06003168 RID: 12648 RVA: 0x000E7033 File Offset: 0x000E6033
		public RtfParserException()
		{
		}

		// Token: 0x06003169 RID: 12649 RVA: 0x000E703B File Offset: 0x000E603B
		public RtfParserException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600316A RID: 12650 RVA: 0x000E7044 File Offset: 0x000E6044
		public RtfParserException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x000E704E File Offset: 0x000E604E
		protected RtfParserException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
