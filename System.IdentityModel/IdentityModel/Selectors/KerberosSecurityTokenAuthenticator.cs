using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A1 RID: 417
	public class KerberosSecurityTokenAuthenticator : WindowsSecurityTokenAuthenticator
	{
		// Token: 0x06000D8F RID: 3471 RVA: 0x0003ECF6 File Offset: 0x0003CEF6
		public KerberosSecurityTokenAuthenticator()
		{
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x0003ECFE File Offset: 0x0003CEFE
		public KerberosSecurityTokenAuthenticator(bool includeWindowsGroups) : base(includeWindowsGroups)
		{
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x0003ED07 File Offset: 0x0003CF07
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is KerberosReceiverSecurityToken;
		}
	}
}
