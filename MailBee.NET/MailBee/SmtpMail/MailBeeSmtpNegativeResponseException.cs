using System;
using System.Runtime.Serialization;
using a;
using a.d;

namespace MailBee.SmtpMail
{
	// Token: 0x0200015C RID: 348
	[Serializable]
	public class MailBeeSmtpNegativeResponseException : MailBeeEmailProtocolNegativeResponseException, IMailBeeNegativeSmtpResponseException
	{
		// Token: 0x06000C19 RID: 3097 RVA: 0x000318A7 File Offset: 0x000308A7
		internal MailBeeSmtpNegativeResponseException(int A_0, ai A_1, at A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000318B2 File Offset: 0x000308B2
		protected MailBeeSmtpNegativeResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000C1B RID: 3099 RVA: 0x000318BC File Offset: 0x000308BC
		public int ResponseCode
		{
			get
			{
				return ((global::a.d.j)this.a).a;
			}
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06000C1C RID: 3100 RVA: 0x000318CE File Offset: 0x000308CE
		public bool IsTransientError
		{
			get
			{
				return ((global::a.d.j)this.a).b();
			}
		}
	}
}
