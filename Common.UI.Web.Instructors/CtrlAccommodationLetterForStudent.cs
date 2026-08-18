using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.Instructors.Controls
{
	// Token: 0x02000002 RID: 2
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlAccommodationLetterForStudent runat=server></{0}:CtrlAccommodationLetterForStudent>")]
	public class CtrlAccommodationLetterForStudent : WebControl, INamingContainer
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		// (set) Token: 0x06000002 RID: 2 RVA: 0x0000207D File Offset: 0x0000027D
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

		// Token: 0x06000003 RID: 3 RVA: 0x00002090 File Offset: 0x00000290
		protected override void RenderContents(HtmlTextWriter output)
		{
			output.Write(this.Text);
		}
	}
}
