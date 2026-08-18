using System;
using System.Runtime.Serialization;

namespace MailBee.SmtpMail
{
	// Token: 0x02000162 RID: 354
	[Serializable]
	public class MailBeeMXRecordsDisabledException : MailBeeDnsRecordsDisabledException
	{
		// Token: 0x06000C2C RID: 3116 RVA: 0x000319C4 File Offset: 0x000309C4
		internal MailBeeMXRecordsDisabledException(int A_0, string A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000319CE File Offset: 0x000309CE
		protected MailBeeMXRecordsDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
