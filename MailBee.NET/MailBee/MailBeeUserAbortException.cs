using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000022 RID: 34
	[Serializable]
	public class MailBeeUserAbortException : MailBeeLocalException, IMailBeeFatalException
	{
		// Token: 0x0600010E RID: 270 RVA: 0x000077ED File Offset: 0x000067ED
		internal MailBeeUserAbortException(int A_0) : base(A_0)
		{
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000077F6 File Offset: 0x000067F6
		protected MailBeeUserAbortException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
