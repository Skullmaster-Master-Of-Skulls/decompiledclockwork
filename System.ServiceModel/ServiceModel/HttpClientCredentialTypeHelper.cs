using System;
using System.Net;

namespace System.ServiceModel
{
	// Token: 0x02000136 RID: 310
	internal static class HttpClientCredentialTypeHelper
	{
		// Token: 0x0600088C RID: 2188 RVA: 0x000228C9 File Offset: 0x00020AC9
		internal static bool IsDefined(HttpClientCredentialType value)
		{
			return value == HttpClientCredentialType.None || value == HttpClientCredentialType.Basic || value == HttpClientCredentialType.Digest || value == HttpClientCredentialType.Ntlm || value == HttpClientCredentialType.Windows || value == HttpClientCredentialType.Certificate || value == HttpClientCredentialType.InheritedFromHost;
		}

		// Token: 0x0600088D RID: 2189 RVA: 0x000228E8 File Offset: 0x00020AE8
		internal static AuthenticationSchemes MapToAuthenticationScheme(HttpClientCredentialType clientCredentialType)
		{
			AuthenticationSchemes result;
			switch (clientCredentialType)
			{
			case HttpClientCredentialType.None:
			case HttpClientCredentialType.Certificate:
				result = AuthenticationSchemes.Anonymous;
				break;
			case HttpClientCredentialType.Basic:
				result = AuthenticationSchemes.Basic;
				break;
			case HttpClientCredentialType.Digest:
				result = AuthenticationSchemes.Digest;
				break;
			case HttpClientCredentialType.Ntlm:
				result = AuthenticationSchemes.Ntlm;
				break;
			case HttpClientCredentialType.Windows:
				result = AuthenticationSchemes.Negotiate;
				break;
			case HttpClientCredentialType.InheritedFromHost:
				result = AuthenticationSchemes.None;
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			return result;
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00022948 File Offset: 0x00020B48
		internal static HttpClientCredentialType MapToClientCredentialType(AuthenticationSchemes authenticationSchemes)
		{
			switch (authenticationSchemes)
			{
			case AuthenticationSchemes.Digest:
				return HttpClientCredentialType.Digest;
			case AuthenticationSchemes.Negotiate:
				return HttpClientCredentialType.Windows;
			case AuthenticationSchemes.Digest | AuthenticationSchemes.Negotiate:
				break;
			case AuthenticationSchemes.Ntlm:
				return HttpClientCredentialType.Ntlm;
			default:
				if (authenticationSchemes == AuthenticationSchemes.Basic)
				{
					return HttpClientCredentialType.Basic;
				}
				if (authenticationSchemes == AuthenticationSchemes.Anonymous)
				{
					return HttpClientCredentialType.None;
				}
				break;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
		}
	}
}
