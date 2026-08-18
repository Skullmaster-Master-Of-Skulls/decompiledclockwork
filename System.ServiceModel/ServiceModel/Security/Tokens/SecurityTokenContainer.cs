using System;
using System.IdentityModel.Tokens;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000389 RID: 905
	internal class SecurityTokenContainer
	{
		// Token: 0x06002178 RID: 8568 RVA: 0x0007BA63 File Offset: 0x00079C63
		public SecurityTokenContainer(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			this.token = token;
		}

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002179 RID: 8569 RVA: 0x0007BA85 File Offset: 0x00079C85
		public SecurityToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x04001F53 RID: 8019
		private SecurityToken token;
	}
}
