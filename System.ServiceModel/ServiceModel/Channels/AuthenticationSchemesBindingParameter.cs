using System;
using System.Net;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000829 RID: 2089
	internal class AuthenticationSchemesBindingParameter
	{
		// Token: 0x06004E08 RID: 19976 RVA: 0x0011D282 File Offset: 0x0011B482
		public AuthenticationSchemesBindingParameter(AuthenticationSchemes authenticationSchemes)
		{
			this.authenticationSchemes = authenticationSchemes;
		}

		// Token: 0x17001382 RID: 4994
		// (get) Token: 0x06004E09 RID: 19977 RVA: 0x0011D291 File Offset: 0x0011B491
		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this.authenticationSchemes;
			}
		}

		// Token: 0x06004E0A RID: 19978 RVA: 0x0011D29C File Offset: 0x0011B49C
		public static bool TryExtract(BindingParameterCollection collection, out AuthenticationSchemes authenticationSchemes)
		{
			authenticationSchemes = AuthenticationSchemes.None;
			AuthenticationSchemesBindingParameter authenticationSchemesBindingParameter = collection.Find<AuthenticationSchemesBindingParameter>();
			if (authenticationSchemesBindingParameter != null)
			{
				authenticationSchemes = authenticationSchemesBindingParameter.AuthenticationSchemes;
				return true;
			}
			return false;
		}

		// Token: 0x040030C5 RID: 12485
		private AuthenticationSchemes authenticationSchemes;
	}
}
