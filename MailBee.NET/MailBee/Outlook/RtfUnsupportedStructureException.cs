using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005C3 RID: 1475
	[Serializable]
	internal class RtfUnsupportedStructureException : RtfInterpreterException
	{
		// Token: 0x06003154 RID: 12628 RVA: 0x000E6F7A File Offset: 0x000E5F7A
		public RtfUnsupportedStructureException()
		{
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000E6F82 File Offset: 0x000E5F82
		public RtfUnsupportedStructureException(string A_0) : base(A_0)
		{
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000E6F8B File Offset: 0x000E5F8B
		public RtfUnsupportedStructureException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000E6F95 File Offset: 0x000E5F95
		protected RtfUnsupportedStructureException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
