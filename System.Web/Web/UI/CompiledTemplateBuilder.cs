using System;
using System.Security.Permissions;

namespace System.Web.UI
{
	// Token: 0x02000471 RID: 1137
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class CompiledTemplateBuilder : ITemplate
	{
		// Token: 0x060035AE RID: 13742 RVA: 0x000E7EB6 File Offset: 0x000E6EB6
		public CompiledTemplateBuilder(BuildTemplateMethod buildTemplateMethod)
		{
			this._buildTemplateMethod = buildTemplateMethod;
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000E7EC5 File Offset: 0x000E6EC5
		public void InstantiateIn(Control container)
		{
			this._buildTemplateMethod(container);
		}

		// Token: 0x04002544 RID: 9540
		private BuildTemplateMethod _buildTemplateMethod;
	}
}
