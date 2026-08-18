using System;
using System.Text.RegularExpressions;

namespace TechnoPro.Common.Public.Adapters
{
	// Token: 0x020005E7 RID: 1511
	public static class AzureStorageContainerNameConventionAdapter
	{
		// Token: 0x060030C3 RID: 12483 RVA: 0x000425DC File Offset: 0x000407DC
		public static string ApplyAzureStorageContainerNamingConventionRules(this string name)
		{
			Regex regex = new Regex("[^a-z0-9-]");
			string text = regex.Replace(name.ToLower(), "");
			string text2 = text.Substring(0, Math.Min(text.Length, 63));
			return text2.EndsWith("-") ? text2.Substring(0, text2.Length - 1) : text2;
		}
	}
}
