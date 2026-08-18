using System;
using System.ComponentModel;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200065C RID: 1628
	public class DefaultPageSize : StateManager, IDefaultCheck
	{
		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06003BB3 RID: 15283 RVA: 0x000C2575 File Offset: 0x000C0775
		// (set) Token: 0x06003BB4 RID: 15284 RVA: 0x000C259E File Offset: 0x000C079E
		[DefaultValue(794.0)]
		public double Width
		{
			get
			{
				return (double)(base.ViewState["Width"] ?? 794.0);
			}
			set
			{
				base.ViewState["Width"] = value;
			}
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x06003BB5 RID: 15285 RVA: 0x000C25B6 File Offset: 0x000C07B6
		// (set) Token: 0x06003BB6 RID: 15286 RVA: 0x000C25DF File Offset: 0x000C07DF
		[DefaultValue(1123.0)]
		public double Height
		{
			get
			{
				return (double)(base.ViewState["Height"] ?? 1123.0);
			}
			set
			{
				base.ViewState["Height"] = value;
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x000C25F7 File Offset: 0x000C07F7
		public bool IsDefault
		{
			get
			{
				return this.Width == 794.0 && this.Height == 1123.0;
			}
		}
	}
}
