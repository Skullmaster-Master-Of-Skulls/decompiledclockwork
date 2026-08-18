using System;
using System.Collections.Generic;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;

namespace System.Web.Razor
{
	// Token: 0x02000085 RID: 133
	public abstract class RazorCodeLanguage
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00016399 File Offset: 0x00014599
		public static IDictionary<string, RazorCodeLanguage> Languages
		{
			get
			{
				return RazorCodeLanguage._services;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005A1 RID: 1441
		public abstract string LanguageName { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005A2 RID: 1442
		public abstract Type CodeDomProviderType { get; }

		// Token: 0x060005A3 RID: 1443 RVA: 0x000163A0 File Offset: 0x000145A0
		public static RazorCodeLanguage GetLanguageByExtension(string fileExtension)
		{
			RazorCodeLanguage result = null;
			RazorCodeLanguage.Languages.TryGetValue(fileExtension.TrimStart(new char[]
			{
				'.'
			}), out result);
			return result;
		}

		// Token: 0x060005A4 RID: 1444
		public abstract ParserBase CreateCodeParser();

		// Token: 0x060005A5 RID: 1445
		public abstract RazorCodeGenerator CreateCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host);

		// Token: 0x040002FB RID: 763
		private static IDictionary<string, RazorCodeLanguage> _services = new Dictionary<string, RazorCodeLanguage>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"cshtml",
				new CSharpRazorCodeLanguage()
			},
			{
				"vbhtml",
				new VBRazorCodeLanguage()
			}
		};
	}
}
