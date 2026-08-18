using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000850 RID: 2128
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web)]
	internal class PageBuildProvider : TemplateControlBuildProvider
	{
		// Token: 0x060064F2 RID: 25842 RVA: 0x00161C9E File Offset: 0x0015FE9E
		internal override DependencyParser CreateDependencyParser()
		{
			return new PageDependencyParser();
		}

		// Token: 0x060064F3 RID: 25843 RVA: 0x00161CA5 File Offset: 0x0015FEA5
		protected override TemplateParser CreateParser()
		{
			return new PageParser();
		}

		// Token: 0x060064F4 RID: 25844 RVA: 0x00161CAC File Offset: 0x0015FEAC
		internal override BaseCodeDomTreeGenerator CreateCodeDomTreeGenerator(TemplateParser parser)
		{
			return new PageCodeDomTreeGenerator((PageParser)parser);
		}

		// Token: 0x060064F5 RID: 25845 RVA: 0x00161CB9 File Offset: 0x0015FEB9
		internal override BuildResultNoCompileTemplateControl CreateNoCompileBuildResult()
		{
			return new BuildResultNoCompilePage(base.Parser.BaseType, base.Parser);
		}
	}
}
