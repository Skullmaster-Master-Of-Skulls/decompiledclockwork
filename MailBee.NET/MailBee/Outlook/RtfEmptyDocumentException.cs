using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005C7 RID: 1479
	[Serializable]
	internal class RtfEmptyDocumentException : RtfStructureException
	{
		// Token: 0x06003160 RID: 12640 RVA: 0x000E6FE9 File Offset: 0x000E5FE9
		public RtfEmptyDocumentException()
		{
		}

		// Token: 0x06003161 RID: 12641 RVA: 0x000E6FF1 File Offset: 0x000E5FF1
		public RtfEmptyDocumentException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003162 RID: 12642 RVA: 0x000E6FFA File Offset: 0x000E5FFA
		public RtfEmptyDocumentException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003163 RID: 12643 RVA: 0x000E7004 File Offset: 0x000E6004
		protected RtfEmptyDocumentException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
