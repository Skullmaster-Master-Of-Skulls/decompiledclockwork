using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Notetaking.Controls
{
	// Token: 0x0200000A RID: 10
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlUploadLectureNotes runat=server></{0}:CtrlUploadLectureNotes>")]
	public class CtrlUploadLectureNotes : WebControl, INamingContainer
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000021 RID: 33 RVA: 0x0000224C File Offset: 0x0000044C
		// (set) Token: 0x06000022 RID: 34 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x06000023 RID: 35 RVA: 0x00002279 File Offset: 0x00000479
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
