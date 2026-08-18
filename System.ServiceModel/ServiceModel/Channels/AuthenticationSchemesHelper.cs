using System;
using System.Net;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000826 RID: 2086
	internal static class AuthenticationSchemesHelper
	{
		// Token: 0x06004DFD RID: 19965 RVA: 0x0011D114 File Offset: 0x0011B314
		public static bool DoesAuthTypeMatch(AuthenticationSchemes authScheme, string authType)
		{
			if (authType == null || authType.Length == 0)
			{
				return authScheme.IsSet(AuthenticationSchemes.Anonymous);
			}
			if (authType.Equals("kerberos", StringComparison.OrdinalIgnoreCase) || authType.Equals("negotiate", StringComparison.OrdinalIgnoreCase))
			{
				return authScheme.IsSet(AuthenticationSchemes.Negotiate);
			}
			if (authType.Equals("ntlm", StringComparison.OrdinalIgnoreCase))
			{
				return authScheme.IsSet(AuthenticationSchemes.Negotiate) || authScheme.IsSet(AuthenticationSchemes.Ntlm);
			}
			AuthenticationSchemes authenticationSchemes;
			return Enum.TryParse<AuthenticationSchemes>(authType, true, out authenticationSchemes) && authScheme.IsSet(authenticationSchemes);
		}

		// Token: 0x06004DFE RID: 19966 RVA: 0x0011D194 File Offset: 0x0011B394
		public static bool IsSingleton(this AuthenticationSchemes v)
		{
			if (v <= AuthenticationSchemes.Ntlm)
			{
				if (v - AuthenticationSchemes.Digest > 1 && v != AuthenticationSchemes.Ntlm)
				{
					goto IL_20;
				}
			}
			else if (v != AuthenticationSchemes.Basic && v != AuthenticationSchemes.Anonymous)
			{
				goto IL_20;
			}
			return true;
			IL_20:
			return false;
		}

		// Token: 0x06004DFF RID: 19967 RVA: 0x0011D1C4 File Offset: 0x0011B3C4
		public static bool IsSet(this AuthenticationSchemes thisPtr, AuthenticationSchemes authenticationSchemes)
		{
			return (thisPtr & authenticationSchemes) == authenticationSchemes;
		}

		// Token: 0x06004E00 RID: 19968 RVA: 0x0011D1CC File Offset: 0x0011B3CC
		public static bool IsNotSet(this AuthenticationSchemes thisPtr, AuthenticationSchemes authenticationSchemes)
		{
			return (thisPtr & authenticationSchemes) == AuthenticationSchemes.None;
		}

		// Token: 0x06004E01 RID: 19969 RVA: 0x0011D1D4 File Offset: 0x0011B3D4
		internal static string ToString(AuthenticationSchemes authScheme)
		{
			return authScheme.ToString().ToLowerInvariant();
		}
	}
}
