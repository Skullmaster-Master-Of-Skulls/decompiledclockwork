using System;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;
using Microsoft.CSharp;

namespace System.Web.Razor
{
	// Token: 0x02000090 RID: 144
	public class CSharpRazorCodeLanguage : RazorCodeLanguage
	{
		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x0001752F File Offset: 0x0001572F
		public override string LanguageName
		{
			get
			{
				return "csharp";
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00017536 File Offset: 0x00015736
		public override Type CodeDomProviderType
		{
			get
			{
				return typeof(CSharpCodeProvider);
			}
		}

		// Token: 0x06000622 RID: 1570 RVA: 0x00017542 File Offset: 0x00015742
		public override ParserBase CreateCodeParser()
		{
			return new CSharpCodeParser();
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00017549 File Offset: 0x00015749
		public override RazorCodeGenerator CreateCodeGenerator(string className, string rootNamespaceName, string sourceFileName, RazorEngineHost host)
		{
			return new CSharpRazorCodeGenerator(className, rootNamespaceName, sourceFileName, host);
		}

		// Token: 0x0400032B RID: 811
		private const string CSharpLanguageName = "csharp";
	}
}
