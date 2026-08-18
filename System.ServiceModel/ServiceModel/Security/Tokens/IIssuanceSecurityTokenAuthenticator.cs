using System;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000392 RID: 914
	public interface IIssuanceSecurityTokenAuthenticator
	{
		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x060021D9 RID: 8665
		// (set) Token: 0x060021DA RID: 8666
		IssuedSecurityTokenHandler IssuedSecurityTokenHandler { get; set; }

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x060021DB RID: 8667
		// (set) Token: 0x060021DC RID: 8668
		RenewedSecurityTokenHandler RenewedSecurityTokenHandler { get; set; }
	}
}
