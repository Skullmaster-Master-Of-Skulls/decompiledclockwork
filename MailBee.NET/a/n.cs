using System;
using MailBee;

namespace a
{
	// Token: 0x0200048F RID: 1167
	internal class n : SaslMethod
	{
		// Token: 0x06002826 RID: 10278 RVA: 0x000BAEE6 File Offset: 0x000B9EE6
		public override string GetSaslID()
		{
			return "XOAUTH";
		}

		// Token: 0x06002827 RID: 10279 RVA: 0x000BAEED File Offset: 0x000B9EED
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslOAuth;
		}

		// Token: 0x06002828 RID: 10280 RVA: 0x000BAEF4 File Offset: 0x000B9EF4
		public override void CreateNextClientAnswer()
		{
			if (base.Stage == 0)
			{
				base.ClientAnswer = base.ClientAnswerEncoding.GetBytes(base.Password);
				int stage = base.Stage;
				base.Stage = stage + 1;
			}
		}

		// Token: 0x06002829 RID: 10281 RVA: 0x000BAF32 File Offset: 0x000B9F32
		public override bool AccountDataIsPassword()
		{
			return true;
		}

		// Token: 0x0600282A RID: 10282 RVA: 0x000BAF35 File Offset: 0x000B9F35
		public override bool IsSecure()
		{
			return false;
		}
	}
}
