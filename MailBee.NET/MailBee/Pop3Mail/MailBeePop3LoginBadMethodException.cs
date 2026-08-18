using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.Pop3Mail
{
	// Token: 0x02000584 RID: 1412
	[Serializable]
	public class MailBeePop3LoginBadMethodException : MailBeePop3LoginNegativeResponseException, IMailBeeLoginBadMethodException
	{
		// Token: 0x06002F51 RID: 12113 RVA: 0x000DFF60 File Offset: 0x000DEF60
		internal MailBeePop3LoginBadMethodException(int A_0, ai A_1, at A_2, AuthenticationMethods A_3) : base(A_0, A_1, A_2)
		{
			this.m_badMethod = A_3;
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x000DFF73 File Offset: 0x000DEF73
		protected MailBeePop3LoginBadMethodException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06002F53 RID: 12115 RVA: 0x000DFF7D File Offset: 0x000DEF7D
		public AuthenticationMethods BadMethod
		{
			get
			{
				return this.m_badMethod;
			}
		}

		// Token: 0x04002003 RID: 8195
		private AuthenticationMethods m_badMethod;
	}
}
