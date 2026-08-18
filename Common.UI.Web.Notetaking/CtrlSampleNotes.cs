using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Notetaking.Controls
{
	// Token: 0x02000008 RID: 8
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlSampleNotes runat=server></{0}:CtrlSampleNotes>")]
	public class CtrlSampleNotes : WebControl
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000021D4 File Offset: 0x000003D4
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x0600001B RID: 27 RVA: 0x00002201 File Offset: 0x00000401
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
