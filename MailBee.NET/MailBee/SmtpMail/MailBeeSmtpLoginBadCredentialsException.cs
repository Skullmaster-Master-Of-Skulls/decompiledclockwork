using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.SmtpMail
{
	// Token: 0x0200015F RID: 351
	[Serializable]
	public class MailBeeSmtpLoginBadCredentialsException : MailBeeSmtpLoginNegativeResponseException, IMailBeeLoginBadCredentialsException
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x0003190A File Offset: 0x0003090A
		internal MailBeeSmtpLoginBadCredentialsException(int A_0, ai A_1, at A_2, string A_3, string A_4) : base(A_0, A_1, A_2)
		{
			this.m_accountName = A_3;
			this.m_password = A_4;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00031925 File Offset: 0x00030925
		protected MailBeeSmtpLoginBadCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0003192F File Offset: 0x0003092F
		public string AccountName
		{
			get
			{
				return this.m_accountName;
			}
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x00031937 File Offset: 0x00030937
		public string Password
		{
			get
			{
				return this.m_password;
			}
		}

		// Token: 0x04000889 RID: 2185
		private string m_accountName;

		// Token: 0x0400088A RID: 2186
		private string m_password;
	}
}
