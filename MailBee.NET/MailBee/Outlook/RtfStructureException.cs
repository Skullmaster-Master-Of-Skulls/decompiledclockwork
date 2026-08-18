using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005C6 RID: 1478
	[Serializable]
	internal class RtfStructureException : RtfParserException
	{
		// Token: 0x0600315C RID: 12636 RVA: 0x000E6FC4 File Offset: 0x000E5FC4
		public RtfStructureException()
		{
		}

		// Token: 0x0600315D RID: 12637 RVA: 0x000E6FCC File Offset: 0x000E5FCC
		public RtfStructureException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600315E RID: 12638 RVA: 0x000E6FD5 File Offset: 0x000E5FD5
		public RtfStructureException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600315F RID: 12639 RVA: 0x000E6FDF File Offset: 0x000E5FDF
		protected RtfStructureException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
