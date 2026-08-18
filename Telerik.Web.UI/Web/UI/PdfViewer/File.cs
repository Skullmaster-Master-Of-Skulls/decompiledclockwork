using System;
using System.ComponentModel;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x02000664 RID: 1636
	public class File : StateManager, IDefaultCheck
	{
		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x06003BEB RID: 15339 RVA: 0x000C2D1E File Offset: 0x000C0F1E
		// (set) Token: 0x06003BEC RID: 15340 RVA: 0x000C2D3E File Offset: 0x000C0F3E
		[DefaultValue("")]
		public string Data
		{
			get
			{
				return (string)(base.ViewState["Data"] ?? "");
			}
			set
			{
				base.ViewState["Data"] = value;
			}
		}

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x06003BED RID: 15341 RVA: 0x000C2D51 File Offset: 0x000C0F51
		// (set) Token: 0x06003BEE RID: 15342 RVA: 0x000C2D71 File Offset: 0x000C0F71
		[DefaultValue("")]
		public string Url
		{
			get
			{
				return (string)(base.ViewState["Url"] ?? "");
			}
			set
			{
				base.ViewState["Url"] = value;
			}
		}

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x06003BEF RID: 15343 RVA: 0x000C2D84 File Offset: 0x000C0F84
		public bool IsDefault
		{
			get
			{
				return this.Data == "" && this.Url == "";
			}
		}
	}
}
