using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x0200084A RID: 2122
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web | BuildProviderAppliesTo.Code)]
	internal class MasterPageBuildProvider : UserControlBuildProvider
	{
		// Token: 0x060064B9 RID: 25785 RVA: 0x00160D13 File Offset: 0x0015EF13
		internal override DependencyParser CreateDependencyParser()
		{
			return new MasterPageDependencyParser();
		}

		// Token: 0x060064BA RID: 25786 RVA: 0x00160D1A File Offset: 0x0015EF1A
		protected override TemplateParser CreateParser()
		{
			return new MasterPageParser();
		}

		// Token: 0x060064BB RID: 25787 RVA: 0x00160D21 File Offset: 0x0015EF21
		internal override BaseCodeDomTreeGenerator CreateCodeDomTreeGenerator(TemplateParser parser)
		{
			return new MasterPageCodeDomTreeGenerator((MasterPageParser)parser);
		}

		// Token: 0x060064BC RID: 25788 RVA: 0x00160D2E File Offset: 0x0015EF2E
		internal override BuildResultNoCompileTemplateControl CreateNoCompileBuildResult()
		{
			return new BuildResultNoCompileMasterPage(base.Parser.BaseType, base.Parser);
		}
	}
}
