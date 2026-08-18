using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.SmtpMail
{
	// Token: 0x02000160 RID: 352
	[Serializable]
	public class MailBeeSmtpLoginBadMethodException : MailBeeSmtpLoginNegativeResponseException, IMailBeeLoginBadMethodException
	{
		// Token: 0x06000C25 RID: 3109 RVA: 0x0003193F File Offset: 0x0003093F
		internal MailBeeSmtpLoginBadMethodException(int A_0, ai A_1, at A_2, AuthenticationMethods A_3) : base(A_0, A_1, A_2)
		{
			this.m_badMethod = A_3;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00031952 File Offset: 0x00030952
		protected MailBeeSmtpLoginBadMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0003195C File Offset: 0x0003095C
		public AuthenticationMethods BadMethod
		{
			get
			{
				return this.m_badMethod;
			}
		}

		// Token: 0x0400088B RID: 2187
		private AuthenticationMethods m_badMethod;
	}
}
