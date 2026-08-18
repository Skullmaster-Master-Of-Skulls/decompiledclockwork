using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000920 RID: 2336
	public class TimelineClientEvents : StateManager, IDefaultCheck
	{
		// Token: 0x17001D33 RID: 7475
		// (get) Token: 0x06005869 RID: 22633 RVA: 0x0010DF54 File Offset: 0x0010C154
		// (set) Token: 0x0600586A RID: 22634 RVA: 0x0010DF74 File Offset: 0x0010C174
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("initialize")]
		public string OnInitialize
		{
			get
			{
				return (string)(base.ViewState["OnInitialize"] ?? "");
			}
			set
			{
				base.ViewState["OnInitialize"] = value;
			}
		}

		// Token: 0x17001D34 RID: 7476
		// (get) Token: 0x0600586B RID: 22635 RVA: 0x0010DF87 File Offset: 0x0010C187
		// (set) Token: 0x0600586C RID: 22636 RVA: 0x0010DFA7 File Offset: 0x0010C1A7
		[ClientPropertyName("load")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnLoad
		{
			get
			{
				return (string)(base.ViewState["OnLoad"] ?? "");
			}
			set
			{
				base.ViewState["OnLoad"] = value;
			}
		}

		// Token: 0x17001D35 RID: 7477
		// (get) Token: 0x0600586D RID: 22637 RVA: 0x0010DFBA File Offset: 0x0010C1BA
		// (set) Token: 0x0600586E RID: 22638 RVA: 0x0010DFDA File Offset: 0x0010C1DA
		[ClientPropertyName("change")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnChange
		{
			get
			{
				return (string)(base.ViewState["OnChange"] ?? "");
			}
			set
			{
				base.ViewState["OnChange"] = value;
			}
		}

		// Token: 0x17001D36 RID: 7478
		// (get) Token: 0x0600586F RID: 22639 RVA: 0x0010DFED File Offset: 0x0010C1ED
		// (set) Token: 0x06005870 RID: 22640 RVA: 0x0010E00D File Offset: 0x0010C20D
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("dataBound")]
		public string OnDataBound
		{
			get
			{
				return (string)(base.ViewState["OnDataBound"] ?? "");
			}
			set
			{
				base.ViewState["OnDataBound"] = value;
			}
		}

		// Token: 0x17001D37 RID: 7479
		// (get) Token: 0x06005871 RID: 22641 RVA: 0x0010E020 File Offset: 0x0010C220
		// (set) Token: 0x06005872 RID: 22642 RVA: 0x0010E040 File Offset: 0x0010C240
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[ClientPropertyName("expand")]
		public string OnExpand
		{
			get
			{
				return (string)(base.ViewState["OnExpand"] ?? "");
			}
			set
			{
				base.ViewState["OnExpand"] = value;
			}
		}

		// Token: 0x17001D38 RID: 7480
		// (get) Token: 0x06005873 RID: 22643 RVA: 0x0010E053 File Offset: 0x0010C253
		// (set) Token: 0x06005874 RID: 22644 RVA: 0x0010E073 File Offset: 0x0010C273
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("collapse")]
		[Category("Client-side events")]
		public string OnCollapse
		{
			get
			{
				return (string)(base.ViewState["OnCollapse"] ?? "");
			}
			set
			{
				base.ViewState["OnCollapse"] = value;
			}
		}

		// Token: 0x17001D39 RID: 7481
		// (get) Token: 0x06005875 RID: 22645 RVA: 0x0010E086 File Offset: 0x0010C286
		// (set) Token: 0x06005876 RID: 22646 RVA: 0x0010E0A6 File Offset: 0x0010C2A6
		[DefaultValue("")]
		[ClientPropertyName("actionClick")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnActionClick
		{
			get
			{
				return (string)(base.ViewState["OnActionClick"] ?? "");
			}
			set
			{
				base.ViewState["OnActionClick"] = value;
			}
		}

		// Token: 0x17001D3A RID: 7482
		// (get) Token: 0x06005877 RID: 22647 RVA: 0x0010E0B9 File Offset: 0x0010C2B9
		// (set) Token: 0x06005878 RID: 22648 RVA: 0x0010E0D9 File Offset: 0x0010C2D9
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientPropertyName("navigate")]
		[ClientControlEvent]
		public string OnNavigate
		{
			get
			{
				return (string)(base.ViewState["OnNavigate"] ?? "");
			}
			set
			{
				base.ViewState["OnNavigate"] = value;
			}
		}

		// Token: 0x17001D3B RID: 7483
		// (get) Token: 0x06005879 RID: 22649 RVA: 0x0010E0EC File Offset: 0x0010C2EC
		public bool IsDefault
		{
			get
			{
				return this.OnChange == "" && this.OnDataBound == "" && this.OnExpand == "" && this.OnCollapse == "" && this.OnActionClick == "" && this.OnNavigate == "";
			}
		}
	}
}
