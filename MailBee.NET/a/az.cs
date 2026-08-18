using System;
using MailBee;

namespace a
{
	// Token: 0x0200048E RID: 1166
	internal class az : a6
	{
		// Token: 0x06002820 RID: 10272 RVA: 0x000BAE7E File Offset: 0x000B9E7E
		protected override string k3()
		{
			if (base.TargetName != null && base.TargetName != string.Empty)
			{
				return "Negotiate";
			}
			return "NTLM";
		}

		// Token: 0x06002821 RID: 10273 RVA: 0x000BAEA5 File Offset: 0x000B9EA5
		public override string GetSaslID()
		{
			return "GSSAPI";
		}

		// Token: 0x06002822 RID: 10274 RVA: 0x000BAEAC File Offset: 0x000B9EAC
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslGssApi;
		}

		// Token: 0x06002823 RID: 10275 RVA: 0x000BAEB3 File Offset: 0x000B9EB3
		internal override void set_TargetNameInternal(string value)
		{
			base.a((value == null) ? (base.ServiceName + "/" + base.ServerName) : value);
		}

		// Token: 0x06002824 RID: 10276 RVA: 0x000BAED7 File Offset: 0x000B9ED7
		protected override int k4()
		{
			return 2078;
		}
	}
}
