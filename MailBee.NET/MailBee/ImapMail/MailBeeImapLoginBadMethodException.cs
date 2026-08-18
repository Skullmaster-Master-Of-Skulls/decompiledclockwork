using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x0200018E RID: 398
	[Serializable]
	public class MailBeeImapLoginBadMethodException : MailBeeImapLoginNegativeResponseException, IMailBeeLoginBadMethodException
	{
		// Token: 0x06000E5D RID: 3677 RVA: 0x000359C6 File Offset: 0x000349C6
		internal MailBeeImapLoginBadMethodException(int A_0, ai A_1, at A_2, AuthenticationMethods A_3) : base(A_0, A_1, A_2)
		{
			this.m_badMethod = A_3;
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000359D9 File Offset: 0x000349D9
		protected MailBeeImapLoginBadMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x000359E3 File Offset: 0x000349E3
		public AuthenticationMethods BadMethod
		{
			get
			{
				return this.m_badMethod;
			}
		}

		// Token: 0x04000940 RID: 2368
		private AuthenticationMethods m_badMethod;
	}
}
