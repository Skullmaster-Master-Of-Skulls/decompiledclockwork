using System;

namespace Telerik.Web.UI.RibbonBar.Renderers
{
	// Token: 0x020007A7 RID: 1959
	internal class RibbonBarToggleButtonClassicRenderer : RibbonBarClickableItemClassicRenderBase
	{
		// Token: 0x0600448B RID: 17547 RVA: 0x000D7E2E File Offset: 0x000D602E
		public RibbonBarToggleButtonClassicRenderer(RibbonBarItem owner) : base(owner)
		{
		}

		// Token: 0x1700162C RID: 5676
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x000D7E38 File Offset: 0x000D6038
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
