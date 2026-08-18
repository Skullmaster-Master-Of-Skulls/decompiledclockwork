using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Instructors.Controls
{
	// Token: 0x02000005 RID: 5
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlInstructorCourseList runat=server></{0}:CtrlInstructorCourseList>")]
	public class CtrlInstructorCourseList : WebControl, INamingContainer
	{
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002120 File Offset: 0x00000320
		// (set) Token: 0x0600000E RID: 14 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x0600000F RID: 15 RVA: 0x0000214D File Offset: 0x0000034D
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
