using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Instructors.Controls
{
	// Token: 0x02000004 RID: 4
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlCourseUpcomingTests runat=server></{0}:CtrlCourseUpcomingTests>")]
	public class CtrlCourseUpcomingTests : WebControl
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020E4 File Offset: 0x000002E4
		// (set) Token: 0x0600000A RID: 10 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x0600000B RID: 11 RVA: 0x00002111 File Offset: 0x00000311
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
