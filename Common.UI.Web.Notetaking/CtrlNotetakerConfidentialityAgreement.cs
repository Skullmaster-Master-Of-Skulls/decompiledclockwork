using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Notetaking.Controls
{
	// Token: 0x02000003 RID: 3
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlNotetakerConfidentialityAgreement runat=server></{0}:CtrlNotetakerConfidentialityAgreement>")]
	public class CtrlNotetakerConfidentialityAgreement : WebControl, INamingContainer
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x000020A8 File Offset: 0x000002A8
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000207D File Offset: 0x0000027D
		[Bindable(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Localizable(true)]
		public string Text
		{
			get
			{
				string text = (string)this.ViewState["Text"];
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["Text"] = value;
			}
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D5 File Offset: 0x000002D5
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
