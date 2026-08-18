using System;
using System.Globalization;

namespace System.Web.Util
{
	// Token: 0x020001CD RID: 461
	internal static class CultureUtil
	{
		// Token: 0x0600176B RID: 5995 RVA: 0x0004983F File Offset: 0x00047A3F
		public static CultureInfo CreateReadOnlyCulture(string cultureName, bool requireSpecific)
		{
			if (requireSpecific)
			{
				return HttpServerUtility.CreateReadOnlySpecificCultureInfo(cultureName);
			}
			return HttpServerUtility.CreateReadOnlyCultureInfo(cultureName);
		}

		// Token: 0x0600176C RID: 5996 RVA: 0x00049851 File Offset: 0x00047A51
		public static CultureInfo CreateReadOnlyCulture(string[] cultureNames, bool requireSpecific)
		{
			return CultureUtil.ExtractCultureImpl(cultureNames, requireSpecific, AppSettings.MaxAcceptLanguageFallbackCount);
		}

		// Token: 0x0600176D RID: 5997 RVA: 0x00049860 File Offset: 0x00047A60
		internal static CultureInfo ExtractCultureImpl(string[] cultureNames, bool requireSpecific, int maxCount)
		{
			int num = Math.Min(cultureNames.Length, maxCount) - 1;
			for (int i = 0; i < cultureNames.Length; i++)
			{
				string cultureName = CultureUtil.StripQValue(cultureNames[i]);
				try
				{
					return CultureUtil.CreateReadOnlyCulture(cultureName, requireSpecific);
				}
				catch (CultureNotFoundException)
				{
					if (i == num)
					{
						throw;
					}
				}
			}
			return null;
		}

		// Token: 0x0600176E RID: 5998 RVA: 0x000498B8 File Offset: 0x00047AB8
		private static string StripQValue(string input)
		{
			if (input != null)
			{
				int num = input.IndexOf(';');
				if (num >= 0)
				{
					return input.Substring(0, num);
				}
			}
			return input;
		}
	}
}
