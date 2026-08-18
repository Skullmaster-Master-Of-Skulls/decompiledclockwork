using System;
using System.ComponentModel;

namespace Telerik.Web.UI.PdfViewer
{
	// Token: 0x0200066A RID: 1642
	public class Pager : StateManager, IDefaultCheck
	{
		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x06003C0D RID: 15373 RVA: 0x000C31C6 File Offset: 0x000C13C6
		// (set) Token: 0x06003C0E RID: 15374 RVA: 0x000C31E6 File Offset: 0x000C13E6
		[DefaultValue("Go to the first page")]
		public string First
		{
			get
			{
				return (string)(base.ViewState["First"] ?? "Go to the first page");
			}
			set
			{
				base.ViewState["First"] = value;
			}
		}

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x06003C0F RID: 15375 RVA: 0x000C31F9 File Offset: 0x000C13F9
		// (set) Token: 0x06003C10 RID: 15376 RVA: 0x000C3219 File Offset: 0x000C1419
		[DefaultValue("Go to the previous page")]
		public string Previous
		{
			get
			{
				return (string)(base.ViewState["Previous"] ?? "Go to the previous page");
			}
			set
			{
				base.ViewState["Previous"] = value;
			}
		}

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x06003C11 RID: 15377 RVA: 0x000C322C File Offset: 0x000C142C
		// (set) Token: 0x06003C12 RID: 15378 RVA: 0x000C324C File Offset: 0x000C144C
		[DefaultValue("Go to the next page")]
		public string Next
		{
			get
			{
				return (string)(base.ViewState["Next"] ?? "Go to the next page");
			}
			set
			{
				base.ViewState["Next"] = value;
			}
		}

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x06003C13 RID: 15379 RVA: 0x000C325F File Offset: 0x000C145F
		// (set) Token: 0x06003C14 RID: 15380 RVA: 0x000C327F File Offset: 0x000C147F
		[DefaultValue("Go to the last page")]
		public string Last
		{
			get
			{
				return (string)(base.ViewState["Last"] ?? "Go to the last page");
			}
			set
			{
				base.ViewState["Last"] = value;
			}
		}

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x06003C15 RID: 15381 RVA: 0x000C3292 File Offset: 0x000C1492
		// (set) Token: 0x06003C16 RID: 15382 RVA: 0x000C32B2 File Offset: 0x000C14B2
		[DefaultValue(" of {0} ")]
		public string Of
		{
			get
			{
				return (string)(base.ViewState["Of"] ?? " of {0} ");
			}
			set
			{
				base.ViewState["Of"] = value;
			}
		}

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000C32C5 File Offset: 0x000C14C5
		// (set) Token: 0x06003C18 RID: 15384 RVA: 0x000C32E5 File Offset: 0x000C14E5
		[DefaultValue("page")]
		public string Page
		{
			get
			{
				return (string)(base.ViewState["Page"] ?? "page");
			}
			set
			{
				base.ViewState["Page"] = value;
			}
		}

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x06003C19 RID: 15385 RVA: 0x000C32F8 File Offset: 0x000C14F8
		// (set) Token: 0x06003C1A RID: 15386 RVA: 0x000C3318 File Offset: 0x000C1518
		[DefaultValue("pages")]
		public string Pages
		{
			get
			{
				return (string)(base.ViewState["Pages"] ?? "pages");
			}
			set
			{
				base.ViewState["Pages"] = value;
			}
		}

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x06003C1B RID: 15387 RVA: 0x000C332C File Offset: 0x000C152C
		public bool IsDefault
		{
			get
			{
				return this.First == "Go to the first page" && this.Previous == "Go to the previous page" && this.Next == "Go to the next page" && this.Last == "Go to the last page" && this.Of == " of {0} " && this.Page == "page" && this.Pages == "pages";
			}
		}
	}
}
