using System;
using System.Web.Razor.Generator;
using System.Web.Razor.Parser;
using System.Web.WebPages.Razor;

namespace System.Web.Mvc.Razor
{
	// Token: 0x020000D0 RID: 208
	public class MvcWebPageRazorHost : WebPageRazorHost
	{
		// Token: 0x06000561 RID: 1377 RVA: 0x0000F10E File Offset: 0x0000D30E
		public MvcWebPageRazorHost(string virtualPath, string physicalPath) : base(virtualPath, physicalPath)
		{
			base.RegisterSpecialFile(RazorViewEngine.ViewStartFileName, typeof(ViewStartPage));
			base.DefaultPageBaseClass = typeof(WebViewPage).FullName;
			this.GetRidOfNamespace("System.Web.WebPages.Html");
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0000F14D File Offset: 0x0000D34D
		public override RazorCodeGenerator DecorateCodeGenerator(RazorCodeGenerator incomingCodeGenerator)
		{
			if (incomingCodeGenerator is CSharpRazorCodeGenerator)
			{
				return new MvcCSharpRazorCodeGenerator(incomingCodeGenerator.ClassName, incomingCodeGenerator.RootNamespaceName, incomingCodeGenerator.SourceFileName, incomingCodeGenerator.Host);
			}
			return base.DecorateCodeGenerator(incomingCodeGenerator);
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x0000F17C File Offset: 0x0000D37C
		public override ParserBase DecorateCodeParser(ParserBase incomingCodeParser)
		{
			if (incomingCodeParser is CSharpCodeParser)
			{
				return new MvcCSharpRazorCodeParser();
			}
			if (incomingCodeParser is VBCodeParser)
			{
				return new MvcVBRazorCodeParser();
			}
			return base.DecorateCodeParser(incomingCodeParser);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x0000F1A1 File Offset: 0x0000D3A1
		private void GetRidOfNamespace(string ns)
		{
			if (this.NamespaceImports.Contains(ns))
			{
				this.NamespaceImports.Remove(ns);
			}
		}
	}
}
