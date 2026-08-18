using System;
using System.ComponentModel;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200065A RID: 1626
	[Serializable]
	public class Scale
	{
		// Token: 0x06003BA7 RID: 15271 RVA: 0x000C2433 File Offset: 0x000C0633
		public Scale()
		{
			this.Text = "Automatic Width";
		}

		// Token: 0x06003BA8 RID: 15272 RVA: 0x000C2446 File Offset: 0x000C0646
		public Scale(string text)
		{
			this.Text = text;
		}

		// Token: 0x06003BA9 RID: 15273 RVA: 0x000C2455 File Offset: 0x000C0655
		public Scale(double value)
		{
			this.Text = "Automatic Width";
			this.Value = value;
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06003BAA RID: 15274 RVA: 0x000C246F File Offset: 0x000C066F
		// (set) Token: 0x06003BAB RID: 15275 RVA: 0x000C2477 File Offset: 0x000C0677
		[DefaultValue(0.0)]
		public double Value { get; set; }

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06003BAC RID: 15276 RVA: 0x000C2480 File Offset: 0x000C0680
		// (set) Token: 0x06003BAD RID: 15277 RVA: 0x000C2488 File Offset: 0x000C0688
		[DefaultValue("Automatic Width")]
		public string Text { get; set; }

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06003BAE RID: 15278 RVA: 0x000C2491 File Offset: 0x000C0691
		public bool IsDefault
		{
			get
			{
				return this.Value == 0.0 && this.Text == "Automatic Width";
			}
		}
	}
}
