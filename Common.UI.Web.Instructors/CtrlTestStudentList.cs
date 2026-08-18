using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Instructors.Controls
{
	// Token: 0x0200000B RID: 11
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlTestStudentList runat=server></{0}:CtrlTestStudentList>")]
	public class CtrlTestStudentList : WebControl, INamingContainer
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002288 File Offset: 0x00000488
		// (set) Token: 0x06000026 RID: 38 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x06000027 RID: 39 RVA: 0x000022B5 File Offset: 0x000004B5
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
