using System;
using System.Collections.Specialized;

namespace System.Web.Http.WebHost
{
	// Token: 0x02000021 RID: 33
	internal static class SuppressFormsAuthRedirectHelper
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00004CD8 File Offset: 0x00002ED8
		internal static bool GetEnabled(NameValueCollection appSettings)
		{
			string value = appSettings.Get(SuppressFormsAuthRedirectHelper.AppSettingsSuppressFormsAuthenticationRedirectKey);
			bool flag;
			return string.IsNullOrEmpty(value) || !bool.TryParse(value, out flag) || flag;
		}

		// Token: 0x04000038 RID: 56
		internal static readonly string AppSettingsSuppressFormsAuthenticationRedirectKey = "webapi:EnableSuppressRedirect";
	}
}
