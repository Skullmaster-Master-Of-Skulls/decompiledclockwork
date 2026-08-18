using System;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000868 RID: 2152
	[BuildProviderAppliesTo(BuildProviderAppliesTo.Web | BuildProviderAppliesTo.Code)]
	internal class UserControlBuildProvider : TemplateControlBuildProvider
	{
		// Token: 0x06006591 RID: 26001 RVA: 0x00165B68 File Offset: 0x00163D68
		internal override DependencyParser CreateDependencyParser()
		{
			return new UserControlDependencyParser();
		}

		// Token: 0x06006592 RID: 26002 RVA: 0x00165B6F File Offset: 0x00163D6F
		protected override TemplateParser CreateParser()
		{
			return new UserControlParser();
		}

		// Token: 0x06006593 RID: 26003 RVA: 0x00165B76 File Offset: 0x00163D76
		internal override BaseCodeDomTreeGenerator CreateCodeDomTreeGenerator(TemplateParser parser)
		{
			return new UserControlCodeDomTreeGenerator((UserControlParser)parser);
		}

		// Token: 0x06006594 RID: 26004 RVA: 0x00165B83 File Offset: 0x00163D83
		internal override BuildResultNoCompileTemplateControl CreateNoCompileBuildResult()
		{
			return new BuildResultNoCompileUserControl(base.Parser.BaseType, base.Parser);
		}
	}
}
