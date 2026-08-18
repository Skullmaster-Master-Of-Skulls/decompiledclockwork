using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TechnoPro.Common.UI.Web.ClockWork.Controls
{
	// Token: 0x02000007 RID: 7
	[DefaultProperty("Text")]
	[ToolboxData("<{0}:CtrlMessage runat=server></{0}:CtrlMessage>")]
	public class CtrlMessage : WebControl
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00002A99 File Offset: 0x00000C99
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002AA4 File Offset: 0x00000CA4
		protected override void Render(HtmlTextWriter writer)
		{
			bool flag = !string.IsNullOrEmpty(this.Message);
			if (flag)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "Alert");
				writer.RenderBeginTag(HtmlTextWriterTag.Div);
				writer.Write(this.Message);
				writer.RenderEndTag();
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00002AF1 File Offset: 0x00000CF1
		// (set) Token: 0x06000051 RID: 81 RVA: 0x00002AF9 File Offset: 0x00000CF9
		public string Message { get; set; }

		// Token: 0x06000052 RID: 82 RVA: 0x00002B02 File Offset: 0x00000D02
		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}
	}
}
