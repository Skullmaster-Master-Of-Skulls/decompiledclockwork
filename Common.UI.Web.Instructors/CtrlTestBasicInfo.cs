using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Instructors.Controls
{
	// Token: 0x02000009 RID: 9
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlTestBasicInfo runat=server></{0}:CtrlTestBasicInfo>")]
	public class CtrlTestBasicInfo : WebControl
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002210 File Offset: 0x00000410
		// (set) Token: 0x0600001E RID: 30 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x0600001F RID: 31 RVA: 0x0000223D File Offset: 0x0000043D
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
