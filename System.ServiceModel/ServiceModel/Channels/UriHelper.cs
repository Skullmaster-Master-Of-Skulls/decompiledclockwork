using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000827 RID: 2087
	internal static class UriHelper
	{
		// Token: 0x06004E02 RID: 19970 RVA: 0x0011D1E8 File Offset: 0x0011B3E8
		internal static string NormalizedHost(this Uri uri)
		{
			return uri.GetComponents(UriComponents.NormalizedHost, UriFormat.UriEscaped);
		}

		// Token: 0x06004E03 RID: 19971 RVA: 0x0011D1F6 File Offset: 0x0011B3F6
		internal static string NormalizedAbsoluteUri(this Uri uri)
		{
			return uri.GetComponents(UriComponents.Scheme | UriComponents.UserInfo | UriComponents.Host | UriComponents.Port | UriComponents.Path | UriComponents.Query | UriComponents.Fragment | UriComponents.NormalizedHost, UriFormat.UriEscaped);
		}
	}
}
