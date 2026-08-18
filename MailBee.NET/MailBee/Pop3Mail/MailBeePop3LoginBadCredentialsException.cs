using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.Pop3Mail
{
	// Token: 0x02000583 RID: 1411
	[Serializable]
	public class MailBeePop3LoginBadCredentialsException : MailBeePop3LoginNegativeResponseException, IMailBeeLoginBadCredentialsException
	{
		// Token: 0x06002F4D RID: 12109 RVA: 0x000DFF2B File Offset: 0x000DEF2B
		internal MailBeePop3LoginBadCredentialsException(int A_0, ai A_1, at A_2, string A_3, string A_4) : base(A_0, A_1, A_2)
		{
			this.m_accountName = A_3;
			this.m_password = A_4;
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x000DFF46 File Offset: 0x000DEF46
		protected MailBeePop3LoginBadCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06002F4F RID: 12111 RVA: 0x000DFF50 File Offset: 0x000DEF50
		public string AccountName
		{
			get
			{
				return this.m_accountName;
			}
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x000DFF58 File Offset: 0x000DEF58
		public string Password
		{
			get
			{
				return this.m_password;
			}
		}

		// Token: 0x04002001 RID: 8193
		private string m_accountName;

		// Token: 0x04002002 RID: 8194
		private string m_password;
	}
}
