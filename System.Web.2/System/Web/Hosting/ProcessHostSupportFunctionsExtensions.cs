using System;
using System.Text;

namespace System.Web.Hosting
{
	// Token: 0x02000796 RID: 1942
	internal static class ProcessHostSupportFunctionsExtensions
	{
		// Token: 0x06005C9C RID: 23708 RVA: 0x001407E0 File Offset: 0x0013E9E0
		public static string MapPathInternal(this IProcessHostSupportFunctions supportFunctions, string appId, string appVirtualPath, string relativePath)
		{
			StringBuilder stringBuilder = new StringBuilder(appVirtualPath.Length + relativePath.Length + 2);
			if (appVirtualPath[0] != '/')
			{
				stringBuilder.Append('/');
			}
			stringBuilder.Append(appVirtualPath);
			if (stringBuilder[stringBuilder.Length - 1] != '/')
			{
				stringBuilder.Append('/');
			}
			if (relativePath.Length > 0)
			{
				if (relativePath[0] == '/')
				{
					stringBuilder.Append(relativePath, 1, relativePath.Length - 1);
				}
				else
				{
					stringBuilder.Append(relativePath);
				}
			}
			string result;
			supportFunctions.MapPath(appId, stringBuilder.ToString(), out result);
			return result;
		}
	}
}
