using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Web.UI;

namespace System.Web.Compilation
{
	// Token: 0x02000864 RID: 2148
	internal abstract class TemplateControlBuildProvider : BaseTemplateBuildProvider
	{
		// Token: 0x06006573 RID: 25971 RVA: 0x0000298D File Offset: 0x00000B8D
		internal virtual DependencyParser CreateDependencyParser()
		{
			return null;
		}

		// Token: 0x06006574 RID: 25972 RVA: 0x00164E98 File Offset: 0x00163098
		internal override ICollection GetBuildResultVirtualPathDependencies()
		{
			DependencyParser dependencyParser = this.CreateDependencyParser();
			if (dependencyParser == null)
			{
				return null;
			}
			dependencyParser.Init(base.VirtualPathObject);
			return dependencyParser.GetVirtualPathDependencies();
		}

		// Token: 0x06006575 RID: 25973 RVA: 0x00164EC3 File Offset: 0x001630C3
		internal override BuildResult CreateBuildResult(CompilerResults results)
		{
			if (base.Parser.RequiresCompilation)
			{
				return base.CreateBuildResult(results);
			}
			return this.CreateNoCompileBuildResult();
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x00164EE0 File Offset: 0x001630E0
		public override Type GetGeneratedType(CompilerResults results)
		{
			return base.GetGeneratedType(results, true);
		}

		// Token: 0x06006577 RID: 25975
		internal abstract BuildResultNoCompileTemplateControl CreateNoCompileBuildResult();
	}
}
