using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Instructors.Controls
{
	// Token: 0x0200000C RID: 12
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlUploadTestCopy runat=server></{0}:CtrlUploadTestCopy>")]
	public class CtrlUploadTestCopy : WebControl, INamingContainer
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022C4 File Offset: 0x000004C4
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x0600002B RID: 43 RVA: 0x000022F1 File Offset: 0x000004F1
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
