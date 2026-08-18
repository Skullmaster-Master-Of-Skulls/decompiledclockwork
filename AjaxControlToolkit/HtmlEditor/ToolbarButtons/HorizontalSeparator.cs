using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit.HtmlEditor.ToolbarButtons
{
	// Token: 0x0200010C RID: 268
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("Sys.Extended.UI.HtmlEditor.ToolbarButtons.HorizontalSeparator", "HtmlEditor.ToolbarButtons.HorizontalSeparator")]
	public class HorizontalSeparator : DesignModeImageButton
	{
		// Token: 0x06000728 RID: 1832 RVA: 0x00013CC2 File Offset: 0x00011EC2
		protected override void OnPreRender(EventArgs e)
		{
			base.RegisterButtonImages("Ed-Separator");
			base.OnPreRender(e);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00013CD8 File Offset: 0x00011ED8
		protected override Style CreateControlStyle()
		{
			return new HorizontalSeparator.HorizontalSeparatorStyle(this.ViewState);
		}

		// Token: 0x0200010D RID: 269
		private sealed class HorizontalSeparatorStyle : Style
		{
			// Token: 0x0600072B RID: 1835 RVA: 0x00013CFA File Offset: 0x00011EFA
			public HorizontalSeparatorStyle(StateBag state) : base(state)
			{
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x00013D03 File Offset: 0x00011F03
			protected override void FillStyleAttributes(CssStyleCollection attributes, IUrlResolutionService urlResolver)
			{
				base.FillStyleAttributes(attributes, urlResolver);
				attributes.Add("background-color", "transparent");
				attributes.Add("cursor", "text");
				attributes.Add("width", "13px");
			}
		}
	}
}
