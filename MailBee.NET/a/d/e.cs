using System;
using System.Threading.Tasks;
using MailBee;
using MailBee.SmtpMail;

namespace a.d
{
	// Token: 0x02000472 RID: 1138
	internal class e : u
	{
		// Token: 0x06002767 RID: 10087 RVA: 0x000B67B7 File Offset: 0x000B57B7
		public e(ab A_0) : base(A_0)
		{
		}

		// Token: 0x06002768 RID: 10088 RVA: 0x000B67C0 File Offset: 0x000B57C0
		protected override IMailBeeLoginBadCredentialsException ea(int A_0, ai A_1, at A_2, string A_3, string A_4)
		{
			return new MailBeeSmtpLoginBadCredentialsException(A_0, A_1, A_2, A_3, A_4);
		}

		// Token: 0x06002769 RID: 10089 RVA: 0x000B67CE File Offset: 0x000B57CE
		protected override IMailBeeLoginBadMethodException eb(int A_0, ai A_1, at A_2, AuthenticationMethods A_3)
		{
			return new MailBeeSmtpLoginBadMethodException(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600276A RID: 10090 RVA: 0x000B67DA File Offset: 0x000B57DA
		protected override bool ec(AuthenticationMethods A_0, string A_1, string A_2, string A_3, bool A_4)
		{
			return false;
		}

		// Token: 0x0600276B RID: 10091 RVA: 0x000B67DD File Offset: 0x000B57DD
		protected override bool ed(string A_0, string A_1, string A_2)
		{
			return false;
		}

		// Token: 0x0600276C RID: 10092 RVA: 0x000B67E0 File Offset: 0x000B57E0
		protected override bool ee()
		{
			return false;
		}

		// Token: 0x0600276D RID: 10093 RVA: 0x000B67E3 File Offset: 0x000B57E3
		protected override Task<bool> ef(AuthenticationMethods A_0, string A_1, string A_2, string A_3, bool A_4)
		{
			return Task.FromResult<bool>(false);
		}

		// Token: 0x0600276E RID: 10094 RVA: 0x000B67EB File Offset: 0x000B57EB
		protected override Task<bool> eg(string A_0, string A_1, string A_2)
		{
			return Task.FromResult<bool>(false);
		}
	}
}
