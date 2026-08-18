using System;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007AE RID: 1966
	internal class RibbonBarToggleButtonLiteRenderer : RibbonBarClickableItemLiteRenderBase
	{
		// Token: 0x060044B6 RID: 17590 RVA: 0x000D8CA2 File Offset: 0x000D6EA2
		public RibbonBarToggleButtonLiteRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x17001635 RID: 5685
		// (get) Token: 0x060044B7 RID: 17591 RVA: 0x000D8CAC File Offset: 0x000D6EAC
		public override string CssClassFormatString
		{
			get
			{
				string text = base.CssClassFormatString;
				if (((RibbonBarToggleButton)base.Owner).Toggled)
				{
					text = RibbonBarStyles.Combine(new string[]
					{
						text,
						"rrbToggled"
					});
				}
				return text;
			}
		}
	}
}
