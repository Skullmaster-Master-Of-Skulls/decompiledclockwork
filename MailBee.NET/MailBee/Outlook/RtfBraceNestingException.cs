using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005C5 RID: 1477
	[Serializable]
	internal class RtfBraceNestingException : RtfStructureException
	{
		// Token: 0x06003158 RID: 12632 RVA: 0x000E6F9F File Offset: 0x000E5F9F
		public RtfBraceNestingException()
		{
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x000E6FA7 File Offset: 0x000E5FA7
		public RtfBraceNestingException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000E6FB0 File Offset: 0x000E5FB0
		public RtfBraceNestingException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600315B RID: 12635 RVA: 0x000E6FBA File Offset: 0x000E5FBA
		protected RtfBraceNestingException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
