using System;

namespace System.IdentityModel
{
	// Token: 0x020000B5 RID: 181
	internal static class UriUtil
	{
		// Token: 0x06000579 RID: 1401 RVA: 0x000149DC File Offset: 0x00012BDC
		public static bool CanCreateValidUri(string uriString, UriKind uriKind)
		{
			Uri uri;
			return UriUtil.TryCreateValidUri(uriString, uriKind, out uri);
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x000149F2 File Offset: 0x00012BF2
		public static bool TryCreateValidUri(string uriString, UriKind uriKind, out Uri result)
		{
			return Uri.TryCreate(uriString, uriKind, out result);
		}
	}
}
