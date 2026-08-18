using System;
using System.Net;

namespace System.ServiceModel
{
	// Token: 0x02000138 RID: 312
	internal static class HttpProxyCredentialTypeHelper
	{
		// Token: 0x0600088F RID: 2191 RVA: 0x0002299E File Offset: 0x00020B9E
		internal static bool IsDefined(HttpProxyCredentialType value)
		{
			return value == HttpProxyCredentialType.None || value == HttpProxyCredentialType.Basic || value == HttpProxyCredentialType.Digest || value == HttpProxyCredentialType.Ntlm || value == HttpProxyCredentialType.Windows;
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x000229B8 File Offset: 0x00020BB8
		internal static AuthenticationSchemes MapToAuthenticationScheme(HttpProxyCredentialType proxyCredentialType)
		{
			AuthenticationSchemes result;
			switch (proxyCredentialType)
			{
			case HttpProxyCredentialType.None:
				result = AuthenticationSchemes.Anonymous;
				break;
			case HttpProxyCredentialType.Basic:
				result = AuthenticationSchemes.Basic;
				break;
			case HttpProxyCredentialType.Digest:
				result = AuthenticationSchemes.Digest;
				break;
			case HttpProxyCredentialType.Ntlm:
				result = AuthenticationSchemes.Ntlm;
				break;
			case HttpProxyCredentialType.Windows:
				result = AuthenticationSchemes.Negotiate;
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			return result;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00022A0C File Offset: 0x00020C0C
		internal static HttpProxyCredentialType MapToProxyCredentialType(AuthenticationSchemes authenticationSchemes)
		{
			switch (authenticationSchemes)
			{
			case AuthenticationSchemes.Digest:
				return HttpProxyCredentialType.Digest;
			case AuthenticationSchemes.Negotiate:
				return HttpProxyCredentialType.Windows;
			case AuthenticationSchemes.Digest | AuthenticationSchemes.Negotiate:
				break;
			case AuthenticationSchemes.Ntlm:
				return HttpProxyCredentialType.Ntlm;
			default:
				if (authenticationSchemes == AuthenticationSchemes.Basic)
				{
					return HttpProxyCredentialType.Basic;
				}
				if (authenticationSchemes == AuthenticationSchemes.Anonymous)
				{
					return HttpProxyCredentialType.None;
				}
				break;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}
	}
}
