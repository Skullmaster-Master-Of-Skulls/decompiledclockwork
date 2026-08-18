using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Notetaking.Controls
{
	// Token: 0x02000007 RID: 7
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlPotentialNotetakersListForStudents runat=server></{0}:CtrlPotentialNotetakersListForStudents>")]
	public class CtrlPotentialNotetakersListForStudents : WebControl, INamingContainer
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002198 File Offset: 0x00000398
		// (set) Token: 0x06000016 RID: 22 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x06000017 RID: 23 RVA: 0x000021C5 File Offset: 0x000003C5
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
