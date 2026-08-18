using System;
using MailBee;

namespace a
{
	// Token: 0x0200047F RID: 1151
	internal class t : SaslMethod
	{
		// Token: 0x060027BF RID: 10175 RVA: 0x000B83B6 File Offset: 0x000B73B6
		public t()
		{
			base.ExpectBase64Challenge = false;
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x000B83C5 File Offset: 0x000B73C5
		public override string GetSaslID()
		{
			return "PLAIN";
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x000B83CC File Offset: 0x000B73CC
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslPlain;
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x000B83D0 File Offset: 0x000B73D0
		public override void CreateNextClientAnswer()
		{
			if (base.Stage == 0)
			{
				base.ClientAnswer = base.ClientAnswerEncoding.GetBytes(string.Format("\0{0}\0{1}", base.AccountName, base.Password));
				int stage = base.Stage;
				base.Stage = stage + 1;
			}
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x000B841E File Offset: 0x000B741E
		public override bool IsSecure()
		{
			return false;
		}
	}
}
