using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001A0 RID: 416
	internal static class EmptySecurityTokenResolver
	{
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000D8D RID: 3469 RVA: 0x0003ECDD File Offset: 0x0003CEDD
		public static SecurityTokenResolver Instance
		{
			get
			{
				return EmptySecurityTokenResolver._instance;
			}
		}

		// Token: 0x04000CD2 RID: 3282
		private static readonly SecurityTokenResolver _instance = SecurityTokenResolver.CreateDefaultSecurityTokenResolver(EmptyReadOnlyCollection<SecurityToken>.Instance, false);
	}
}
