using System;
using System.Globalization;

namespace System.Configuration
{
	// Token: 0x02000648 RID: 1608
	internal static class CommonConfigurationStrings
	{
		// Token: 0x060031D2 RID: 12754 RVA: 0x000D4D70 File Offset: 0x000D3D70
		private static string GetSectionPath(string sectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}", new object[]
			{
				sectionName
			});
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000D4D98 File Offset: 0x000D3D98
		private static string GetSectionPath(string sectionName, string subSectionName)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}/{1}", new object[]
			{
				sectionName,
				subSectionName
			});
		}

		// Token: 0x17000B6B RID: 2923
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x000D4DC4 File Offset: 0x000D3DC4
		internal static string UriSectionPath
		{
			get
			{
				return CommonConfigurationStrings.GetSectionPath("uri");
			}
		}

		// Token: 0x04002EED RID: 12013
		internal const string UriSectionName = "uri";

		// Token: 0x04002EEE RID: 12014
		internal const string IriParsing = "iriParsing";

		// Token: 0x04002EEF RID: 12015
		internal const string Idn = "idn";

		// Token: 0x04002EF0 RID: 12016
		internal const string Enabled = "enabled";
	}
}
