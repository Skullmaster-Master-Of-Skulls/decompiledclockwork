using System;
using MailBee;

namespace a
{
	// Token: 0x02000490 RID: 1168
	internal class an : SaslMethod
	{
		// Token: 0x0600282C RID: 10284 RVA: 0x000BAF40 File Offset: 0x000B9F40
		public override string GetSaslID()
		{
			return "XOAUTH2";
		}

		// Token: 0x0600282D RID: 10285 RVA: 0x000BAF47 File Offset: 0x000B9F47
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslOAuth2;
		}

		// Token: 0x0600282E RID: 10286 RVA: 0x000BAF50 File Offset: 0x000B9F50
		public override void CreateNextClientAnswer()
		{
			if (base.Stage == 0)
			{
				base.ClientAnswer = base.ClientAnswerEncoding.GetBytes(base.Password);
				int stage = base.Stage;
				base.Stage = stage + 1;
			}
		}

		// Token: 0x0600282F RID: 10287 RVA: 0x000BAF8E File Offset: 0x000B9F8E
		public override bool AccountDataIsPassword()
		{
			return true;
		}

		// Token: 0x06002830 RID: 10288 RVA: 0x000BAF91 File Offset: 0x000B9F91
		public override bool IsSecure()
		{
			return false;
		}
	}
}
