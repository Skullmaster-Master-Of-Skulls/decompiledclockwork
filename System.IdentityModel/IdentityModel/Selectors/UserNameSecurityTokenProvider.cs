using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001AE RID: 430
	public class UserNameSecurityTokenProvider : SecurityTokenProvider
	{
		// Token: 0x06000E0F RID: 3599 RVA: 0x0003FE3A File Offset: 0x0003E03A
		public UserNameSecurityTokenProvider(string userName, string password)
		{
			if (userName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("userName");
			}
			this.userNameToken = new UserNameSecurityToken(userName, password);
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x0003FE62 File Offset: 0x0003E062
		protected override SecurityToken GetTokenCore(TimeSpan timeout)
		{
			return this.userNameToken;
		}

		// Token: 0x04000CE9 RID: 3305
		private UserNameSecurityToken userNameToken;
	}
}
