using System;

namespace Telerik.Web.UI.Editor
{
	// Token: 0x020002DD RID: 733
	internal class LiteRibbonBarToolAdapter : RibbonBarToolAdapter
	{
		// Token: 0x06001984 RID: 6532 RVA: 0x000545E9 File Offset: 0x000527E9
		public LiteRibbonBarToolAdapter()
		{
			if (base.Editor != null)
			{
				this._ribbonbar = base.Editor.RibbonBar;
			}
		}

		// Token: 0x06001985 RID: 6533 RVA: 0x0005460A File Offset: 0x0005280A
		public LiteRibbonBarToolAdapter(RadEditor editor)
		{
			this._ribbonbar = editor.RibbonBar;
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x0005461E File Offset: 0x0005281E
		public override string ClientType
		{
			get
			{
				return "Telerik.Web.UI.Editor.RibbonBarToolAdapterLite";
			}
		}

		// Token: 0x06001987 RID: 6535 RVA: 0x00054625 File Offset: 0x00052825
		protected override void InitializeRibbonBarClickableItem(RibbonBarClickableItem button, EditorTool tool)
		{
			button.CssClass = "reTool reRibbonTool";
			base.SetRibbonBarButtonText(button, tool);
		}
	}
}
