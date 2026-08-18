using System;
using Telerik.Web.UI.Navigation;

namespace Telerik.Web.UI.Nav.Renderers
{
	// Token: 0x02000626 RID: 1574
	internal static class RendererFactory
	{
		// Token: 0x06003957 RID: 14679 RVA: 0x000BC3F9 File Offset: 0x000BA5F9
		public static IRenderer CreateNodeRenderer(NavigationNode navigationNode)
		{
			return new ItemRenderer(navigationNode);
		}
	}
}
