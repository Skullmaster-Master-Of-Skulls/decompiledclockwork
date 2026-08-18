using System;
using MailBee;

namespace a
{
	// Token: 0x0200047E RID: 1150
	internal class am : SaslMethod
	{
		// Token: 0x060027BA RID: 10170 RVA: 0x000B832D File Offset: 0x000B732D
		public am()
		{
			base.ExpectBase64Challenge = false;
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x000B833C File Offset: 0x000B733C
		public override string GetSaslID()
		{
			return "LOGIN";
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x000B8343 File Offset: 0x000B7343
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslLogin;
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x000B8348 File Offset: 0x000B7348
		public override void CreateNextClientAnswer()
		{
			int stage = base.Stage;
			if (stage == 0)
			{
				base.ClientAnswer = base.ClientAnswerEncoding.GetBytes(base.AccountName);
				stage = base.Stage;
				base.Stage = stage + 1;
				return;
			}
			if (stage != 1)
			{
				return;
			}
			base.ClientAnswer = base.ClientAnswerEncoding.GetBytes(base.Password);
			stage = base.Stage;
			base.Stage = stage + 1;
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x000B83B3 File Offset: 0x000B73B3
		public override bool IsSecure()
		{
			return false;
		}
	}
}
