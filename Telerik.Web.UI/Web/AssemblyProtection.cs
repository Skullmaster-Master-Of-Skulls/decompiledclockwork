using System;
using System.Web;

namespace Telerik.Web
{
	// Token: 0x020019D9 RID: 6617
	internal static class AssemblyProtection
	{
		// Token: 0x06010041 RID: 65601 RVA: 0x0039773B File Offset: 0x0039593B
		public static void Validate()
		{
		}

		// Token: 0x06010042 RID: 65602 RVA: 0x00397740 File Offset: 0x00395940
		private static void ValidatePassPhrase()
		{
			if (HttpContext.Current == null)
			{
				return;
			}
			string text = string.Format("This version of Telerik UI ASP.NET Ajax is licensed only for use by {0}", "MyApp");
			string text2 = (string)HttpContext.Current.Application["Telerik.Web.UI.Key"];
			if (text2 != null)
			{
				string a = string.Format("This version of Telerik UI ASP.NET Ajax is licensed only for use by {0}", text2);
				if (a == text)
				{
					return;
				}
			}
			throw new NotSupportedException(text);
		}

		// Token: 0x04004880 RID: 18560
		private const string Key = "Telerik.Web.UI.Key";

		// Token: 0x04004881 RID: 18561
		private const string ApplicationName = "MyApp";

		// Token: 0x04004882 RID: 18562
		private const string PassPhraseFormat = "This version of Telerik UI ASP.NET Ajax is licensed only for use by {0}";
	}
}
