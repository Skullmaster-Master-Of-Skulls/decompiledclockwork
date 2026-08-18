using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x0200006C RID: 108
	[Serializable]
	public class MailBeeLoginNoCredentialsException : MailBeeLoginNotPossibleException
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x00009028 File Offset: 0x00008028
		internal MailBeeLoginNoCredentialsException(int A_0, ai A_1, string A_2, string A_3) : base(A_0, A_1)
		{
			this.m_accountName = A_2;
			this.m_password = A_3;
		}

		// Token: 0x060003AA RID: 938 RVA: 0x00009041 File Offset: 0x00008041
		protected MailBeeLoginNoCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060003AB RID: 939 RVA: 0x0000904B File Offset: 0x0000804B
		public string AccountName
		{
			get
			{
				return this.m_accountName;
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060003AC RID: 940 RVA: 0x00009053 File Offset: 0x00008053
		public string Password
		{
			get
			{
				return this.m_password;
			}
		}

		// Token: 0x0400016E RID: 366
		private string m_accountName;

		// Token: 0x0400016F RID: 367
		private string m_password;
	}
}
