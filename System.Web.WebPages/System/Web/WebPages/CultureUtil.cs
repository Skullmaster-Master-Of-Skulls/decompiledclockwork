using System;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace System.Web.WebPages
{
	// Token: 0x0200007F RID: 127
	internal static class CultureUtil
	{
		// Token: 0x060003CA RID: 970 RVA: 0x0000C890 File Offset: 0x0000AA90
		internal static void SetCulture(Thread thread, HttpContextBase context, string cultureName)
		{
			CultureInfo culture = CultureUtil.GetCulture(context, cultureName);
			if (culture != null)
			{
				thread.CurrentCulture = culture;
			}
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000C8B0 File Offset: 0x0000AAB0
		internal static void SetUICulture(Thread thread, HttpContextBase context, string cultureName)
		{
			CultureInfo culture = CultureUtil.GetCulture(context, cultureName);
			if (culture != null)
			{
				thread.CurrentUICulture = culture;
			}
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000C8CF File Offset: 0x0000AACF
		private static CultureInfo GetCulture(HttpContextBase context, string cultureName)
		{
			if (cultureName.Equals("auto", StringComparison.OrdinalIgnoreCase))
			{
				return CultureUtil.DetermineAutoCulture(context);
			}
			return CultureInfo.GetCultureInfo(cultureName);
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000C8EC File Offset: 0x0000AAEC
		private static CultureInfo DetermineAutoCulture(HttpContextBase context)
		{
			HttpRequestBase request = context.Request;
			CultureInfo result = null;
			if (request.UserLanguages != null)
			{
				string text = request.UserLanguages.FirstOrDefault<string>();
				if (!string.IsNullOrWhiteSpace(text))
				{
					int num = text.IndexOf(';');
					if (num != -1)
					{
						text = text.Substring(0, num);
					}
					try
					{
						result = new CultureInfo(text);
					}
					catch (CultureNotFoundException)
					{
					}
				}
			}
			return result;
		}
	}
}
