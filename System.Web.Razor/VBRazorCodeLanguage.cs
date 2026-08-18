using System;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;
using Microsoft.VisualBasic;

namespace System.Web.Razor
{
	// Token: 0x02000086 RID: 134
	public class VBRazorCodeLanguage : RazorCodeLanguage
	{
		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00016416 File Offset: 0x00014616
		public override string LanguageName
		{
			get
			{
				return "vb";
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005A9 RID: 1449 RVA: 0x0001641D File Offset: 0x0001461D
		public override Type CodeDomProviderType
		{
			get
			{
				return typeof(VBCodeProvider);
			}
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00016429 File Offset: 0x00014629
		public override ParserBase CreateCodeParser()
		{
			return new VBCodeParser();
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00016430 File Offset: 0x00014630
		public override RazorCodeGenerator CreateCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
		{
			return new VBRazorCodeGenerator(className, rootNamespaceName, sourceFileName, host);
		}

		// Token: 0x040002FC RID: 764
		private const string VBLanguageName = "vb";
	}
}
