using System;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002C8 RID: 712
	internal class LiteToolBarRenderer : EditorToolBarRendererBase
	{
		// Token: 0x060018C9 RID: 6345 RVA: 0x0005250B File Offset: 0x0005070B
		public LiteToolBarRenderer(EditorToolBar owner) : base(owner)
		{
		}

		// Token: 0x17000868 RID: 2152
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00052514 File Offset: 0x00050714
		public override string CssClassFormatString
		{
			get
			{
				return "reToolBar RadEditor_{0}";
			}
		}
	}
}
