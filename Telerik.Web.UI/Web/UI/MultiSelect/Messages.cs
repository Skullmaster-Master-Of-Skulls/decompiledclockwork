using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x0200060D RID: 1549
	public class Messages : StateManager, IDefaultCheck
	{
		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x0600385B RID: 14427 RVA: 0x000B979E File Offset: 0x000B799E
		// (set) Token: 0x0600385C RID: 14428 RVA: 0x000B97BE File Offset: 0x000B79BE
		[DefaultValue("clear")]
		public string Clear
		{
			get
			{
				return (string)(base.ViewState["Clear"] ?? "clear");
			}
			set
			{
				base.ViewState["Clear"] = value;
			}
		}

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x0600385D RID: 14429 RVA: 0x000B97D1 File Offset: 0x000B79D1
		// (set) Token: 0x0600385E RID: 14430 RVA: 0x000B97F1 File Offset: 0x000B79F1
		[DefaultValue("delete")]
		public string DeleteTag
		{
			get
			{
				return (string)(base.ViewState["DeleteTag"] ?? "delete");
			}
			set
			{
				base.ViewState["DeleteTag"] = value;
			}
		}

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x0600385F RID: 14431 RVA: 0x000B9804 File Offset: 0x000B7A04
		// (set) Token: 0x06003860 RID: 14432 RVA: 0x000B9824 File Offset: 0x000B7A24
		[DefaultValue("No data found.")]
		public string NoData
		{
			get
			{
				return (string)(base.ViewState["NoData"] ?? "No data found.");
			}
			set
			{
				base.ViewState["NoData"] = value;
			}
		}

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000B9837 File Offset: 0x000B7A37
		// (set) Token: 0x06003862 RID: 14434 RVA: 0x000B9857 File Offset: 0x000B7A57
		[DefaultValue("item's selected")]
		public string SingleTag
		{
			get
			{
				return (string)(base.ViewState["SingleTag"] ?? "item's selected");
			}
			set
			{
				base.ViewState["SingleTag"] = value;
			}
		}

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x000B986C File Offset: 0x000B7A6C
		public bool IsDefault
		{
			get
			{
				return this.Clear == "clear" && this.DeleteTag == "delete" && this.NoData == "No data found." && this.SingleTag == "item's selected";
			}
		}
	}
}
