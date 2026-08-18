using System;
using System.Collections.Generic;
using System.Linq;

namespace WebGrease
{
	// Token: 0x020001BA RID: 442
	internal static class Strings
	{
		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001690 RID: 5776 RVA: 0x00081B1C File Offset: 0x0007FD1C
		internal static char[] SemicolonSeparator
		{
			get
			{
				return Strings.SemicolonSeparatorField;
			}
		}

		// Token: 0x04000BD3 RID: 3027
		internal const string Css = "css";

		// Token: 0x04000BD4 RID: 3028
		internal const string DpiResourcePivotKey = "dpi";

		// Token: 0x04000BD5 RID: 3029
		internal const string ThemesResourcePivotKey = "themes";

		// Token: 0x04000BD6 RID: 3030
		internal const string LocalesResourcePivotKey = "locales";

		// Token: 0x04000BD7 RID: 3031
		internal const string CssFilter = "*.css";

		// Token: 0x04000BD8 RID: 3032
		internal const string JsFilter = "*.js";

		// Token: 0x04000BD9 RID: 3033
		internal const string JS = "js";

		// Token: 0x04000BDA RID: 3034
		internal const string Px = "px";

		// Token: 0x04000BDB RID: 3035
		internal const string ScanLogExtension = ".scan.xml";

		// Token: 0x04000BDC RID: 3036
		internal const string ResxExtension = ".resx";

		// Token: 0x04000BDD RID: 3037
		internal const string Semicolon = ";";

		// Token: 0x04000BDE RID: 3038
		internal const string DefaultLocale = "generic-generic";

		// Token: 0x04000BDF RID: 3039
		internal const string DefaultResx = "generic-generic.resx";

		// Token: 0x04000BE0 RID: 3040
		internal const string GlobalsToIgnoreArg = "/global:";

		// Token: 0x04000BE1 RID: 3041
		internal const string DefaultGlobalsToIgnore = "jQuery";

		// Token: 0x04000BE2 RID: 3042
		internal const string DefaultMinifyArgs = "";

		// Token: 0x04000BE3 RID: 3043
		internal const string DefaultAnalyzeArgs = "-analyze -WARN:4";

		// Token: 0x04000BE4 RID: 3044
		internal const string CssLocalizedOutput = "CssLocalizedOutput";

		// Token: 0x04000BE5 RID: 3045
		internal const string JsLocalizedOutput = "JsLocalizedOutput";

		// Token: 0x04000BE6 RID: 3046
		internal const string ImagesLogFile = "images_log.xml";

		// Token: 0x04000BE7 RID: 3047
		internal const string CssLogFile = "css_log.xml";

		// Token: 0x04000BE8 RID: 3048
		internal const string JsLogFile = "js_log.xml";

		// Token: 0x04000BE9 RID: 3049
		internal static readonly char[] FileFilterSeparator = ",".ToCharArray();

		// Token: 0x04000BEA RID: 3050
		internal static readonly List<string> DefaultImageExtensions = new string[]
		{
			"png",
			"jpg",
			"jpeg",
			"gif"
		}.ToList<string>();

		// Token: 0x04000BEB RID: 3051
		private static readonly char[] SemicolonSeparatorField = new char[]
		{
			';'
		};
	}
}
