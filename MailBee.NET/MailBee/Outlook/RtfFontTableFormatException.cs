using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005BE RID: 1470
	[Serializable]
	internal class RtfFontTableFormatException : RtfInterpreterException
	{
		// Token: 0x06003140 RID: 12608 RVA: 0x000E6EC1 File Offset: 0x000E5EC1
		public RtfFontTableFormatException()
		{
		}

		// Token: 0x06003141 RID: 12609 RVA: 0x000E6EC9 File Offset: 0x000E5EC9
		public RtfFontTableFormatException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003142 RID: 12610 RVA: 0x000E6ED2 File Offset: 0x000E5ED2
		public RtfFontTableFormatException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003143 RID: 12611 RVA: 0x000E6EDC File Offset: 0x000E5EDC
		protected RtfFontTableFormatException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
