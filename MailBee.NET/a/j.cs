using System;
using System.Net.Security;
using MailBee;

namespace a
{
	// Token: 0x02000489 RID: 1161
	internal class j : bi
	{
		// Token: 0x06002803 RID: 10243 RVA: 0x000BA3A6 File Offset: 0x000B93A6
		public override string GetSaslID()
		{
			return "NTLM";
		}

		// Token: 0x06002804 RID: 10244 RVA: 0x000BA3AD File Offset: 0x000B93AD
		internal override AuthenticationMethods GetMethodEnumMember()
		{
			return AuthenticationMethods.SaslNtlm;
		}

		// Token: 0x06002805 RID: 10245 RVA: 0x000BA3B4 File Offset: 0x000B93B4
		internal override void set_TargetNameInternal(string value)
		{
			base.a(string.Empty);
		}

		// Token: 0x06002806 RID: 10246 RVA: 0x000BA3C1 File Offset: 0x000B93C1
		protected override ProtectionLevel cd()
		{
			return ProtectionLevel.None;
		}
	}
}
