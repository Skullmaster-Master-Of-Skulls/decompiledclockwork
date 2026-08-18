using System;

namespace System.Web.Util
{
	// Token: 0x02000033 RID: 51
	internal static class HeaderUtility
	{
		// Token: 0x06000202 RID: 514 RVA: 0x0000D090 File Offset: 0x0000B290
		public static bool IsEncodingInAcceptList(string acceptEncodingHeader, string expectedEncoding)
		{
			if (string.IsNullOrEmpty(acceptEncodingHeader))
			{
				return false;
			}
			foreach (string text in acceptEncodingHeader.Split(new char[]
			{
				','
			}))
			{
				string a = text.Trim();
				if (string.Equals(a, expectedEncoding, StringComparison.Ordinal))
				{
					return true;
				}
			}
			return false;
		}
	}
}
