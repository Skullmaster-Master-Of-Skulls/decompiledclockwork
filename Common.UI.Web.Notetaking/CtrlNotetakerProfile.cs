using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Notetaking.Controls
{
	// Token: 0x02000006 RID: 6
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlNotetakerProfile runat=server></{0}:CtrlNotetakerProfile>")]
	public class CtrlNotetakerProfile : WebControl, INamingContainer
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000215C File Offset: 0x0000035C
		// (set) Token: 0x06000012 RID: 18 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x06000013 RID: 19 RVA: 0x00002189 File Offset: 0x00000389
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
