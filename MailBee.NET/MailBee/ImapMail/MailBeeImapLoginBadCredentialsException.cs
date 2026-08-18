using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x0200018D RID: 397
	[Serializable]
	public class MailBeeImapLoginBadCredentialsException : MailBeeImapLoginNegativeResponseException, IMailBeeLoginBadCredentialsException
	{
		// Token: 0x06000E59 RID: 3673 RVA: 0x00035991 File Offset: 0x00034991
		internal MailBeeImapLoginBadCredentialsException(int A_0, ai A_1, at A_2, string A_3, string A_4) : base(A_0, A_1, A_2)
		{
			this.m_accountName = A_3;
			this.m_password = A_4;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x000359AC File Offset: 0x000349AC
		protected MailBeeImapLoginBadCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x000359B6 File Offset: 0x000349B6
		public string AccountName
		{
			get
			{
				return this.m_accountName;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x000359BE File Offset: 0x000349BE
		public string Password
		{
			get
			{
				return this.m_password;
			}
		}

		// Token: 0x0400093E RID: 2366
		private string m_accountName;

		// Token: 0x0400093F RID: 2367
		private string m_password;
	}
}
