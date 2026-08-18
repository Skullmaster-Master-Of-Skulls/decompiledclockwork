using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005BD RID: 1469
	[Serializable]
	internal class RtfColorTableFormatException : RtfInterpreterException
	{
		// Token: 0x0600313C RID: 12604 RVA: 0x000E6E9C File Offset: 0x000E5E9C
		public RtfColorTableFormatException()
		{
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x000E6EA4 File Offset: 0x000E5EA4
		public RtfColorTableFormatException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x000E6EAD File Offset: 0x000E5EAD
		public RtfColorTableFormatException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600313F RID: 12607 RVA: 0x000E6EB7 File Offset: 0x000E5EB7
		protected RtfColorTableFormatException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
