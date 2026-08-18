using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005C0 RID: 1472
	[Serializable]
	internal class RtfInvalidDataException : RtfInterpreterException
	{
		// Token: 0x06003148 RID: 12616 RVA: 0x000E6F0B File Offset: 0x000E5F0B
		public RtfInvalidDataException()
		{
		}

		// Token: 0x06003149 RID: 12617 RVA: 0x000E6F13 File Offset: 0x000E5F13
		public RtfInvalidDataException(string A_0) : base(A_0)
		{
		}

		// Token: 0x0600314A RID: 12618 RVA: 0x000E6F1C File Offset: 0x000E5F1C
		public RtfInvalidDataException(string A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600314B RID: 12619 RVA: 0x000E6F26 File Offset: 0x000E5F26
		protected RtfInvalidDataException(SerializationInfo A_0, StreamingContext A_1) : base(A_0, A_1)
		{
		}
	}
}
