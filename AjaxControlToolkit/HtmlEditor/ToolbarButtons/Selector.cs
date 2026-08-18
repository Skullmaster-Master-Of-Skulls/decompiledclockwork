using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x020000F2 RID: 242
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.Selector", "HtmlEditor.ToolbarButtons.Selector")]
	[RequiredScript(typeof(CommonToolkitScripts))]
	public abstract class Selector : DesignModePopupImageButton
	{
		// Token: 0x060006DF RID: 1759 RVA: 0x000133A3 File Offset: 0x000115A3
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Selector");
			base.OnPreRender(e);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x000133B7 File Offset: 0x000115B7
		protected override Style CreateControlStyle()
		{
			return new Selector.SelectorStyle(this.ViewState);
		}

		// Token: 0x020000F3 RID: 243
		private sealed class SelectorStyle : Style
		{
			// Token: 0x060006E2 RID: 1762 RVA: 0x000133CC File Offset: 0x000115CC
			public SelectorStyle(StateBag state) : base(state)
			{
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x000133D5 File Offset: 0x000115D5
			protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
			{
				base.FillStyleAttributes(attributes, urlResolver);
				attributes.Add("width", "11px");
			}
		}
	}
}
