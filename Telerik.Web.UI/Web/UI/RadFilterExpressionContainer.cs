using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020018A3 RID: 6307
	[ToolboxItem(false)]
	public class RadFilterExpressionContainer : Control
	{
		// Token: 0x0600F3E9 RID: 62441 RVA: 0x003778DB File Offset: 0x00375ADB
		protected override void Render(HtmlTextWriter writer)
		{
			if (this.ShowLineImages)
			{
				writer.AddAttribute(HtmlTextWriterAttribute.Class, "rfLines");
			}
			writer.RenderBeginTag(HtmlTextWriterTag.Ul);
			base.Render(writer);
			writer.RenderEndTag();
		}

		// Token: 0x1700497D RID: 18813
		// (get) Token: 0x0600F3EA RID: 62442 RVA: 0x00377907 File Offset: 0x00375B07
		// (set) Token: 0x0600F3EB RID: 62443 RVA: 0x0037790F File Offset: 0x00375B0F
		public bool ShowLineImages { get; set; }
	}
}
