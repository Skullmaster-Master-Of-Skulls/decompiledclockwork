using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x020005EC RID: 1516
	public class MultiColumnComboBoxClientEvents : StateManager, IDefaultCheck
	{
		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x060036E8 RID: 14056 RVA: 0x000B5EA8 File Offset: 0x000B40A8
		// (set) Token: 0x060036E9 RID: 14057 RVA: 0x000B5EC8 File Offset: 0x000B40C8
		[Category("Client-side events")]
		[ClientPropertyName("initialize")]
		[ClientControlEvent]
		[DefaultValue("")]
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

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x060036EA RID: 14058 RVA: 0x000B5EDB File Offset: 0x000B40DB
		// (set) Token: 0x060036EB RID: 14059 RVA: 0x000B5EFB File Offset: 0x000B40FB
		[Category("Client-side events")]
		[ClientPropertyName("load")]
		[ClientControlEvent]
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

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x060036EC RID: 14060 RVA: 0x000B5F0E File Offset: 0x000B410E
		// (set) Token: 0x060036ED RID: 14061 RVA: 0x000B5F2E File Offset: 0x000B412E
		[DefaultValue("")]
		[ClientPropertyName("change")]
		[ClientControlEvent]
		[Category("Client-side events")]
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

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x060036EE RID: 14062 RVA: 0x000B5F41 File Offset: 0x000B4141
		// (set) Token: 0x060036EF RID: 14063 RVA: 0x000B5F61 File Offset: 0x000B4161
		[ClientPropertyName("close")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnClose
		{
			get
			{
				return (string)(base.ViewState["OnClose"] ?? "");
			}
			set
			{
				base.ViewState["OnClose"] = value;
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x000B5F74 File Offset: 0x000B4174
		// (set) Token: 0x060036F1 RID: 14065 RVA: 0x000B5F94 File Offset: 0x000B4194
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
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

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000B5FA7 File Offset: 0x000B41A7
		// (set) Token: 0x060036F3 RID: 14067 RVA: 0x000B5FC7 File Offset: 0x000B41C7
		[ClientPropertyName("filtering")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnFiltering
		{
			get
			{
				return (string)(base.ViewState["OnFiltering"] ?? "");
			}
			set
			{
				base.ViewState["OnFiltering"] = value;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000B5FDA File Offset: 0x000B41DA
		// (set) Token: 0x060036F5 RID: 14069 RVA: 0x000B5FFA File Offset: 0x000B41FA
		[DefaultValue("")]
		[ClientPropertyName("open")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnOpen
		{
			get
			{
				return (string)(base.ViewState["OnOpen"] ?? "");
			}
			set
			{
				base.ViewState["OnOpen"] = value;
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000B600D File Offset: 0x000B420D
		// (set) Token: 0x060036F7 RID: 14071 RVA: 0x000B602D File Offset: 0x000B422D
		[DefaultValue("")]
		[ClientPropertyName("select")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnSelect
		{
			get
			{
				return (string)(base.ViewState["OnSelect"] ?? "");
			}
			set
			{
				base.ViewState["OnSelect"] = value;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000B6040 File Offset: 0x000B4240
		// (set) Token: 0x060036F9 RID: 14073 RVA: 0x000B6060 File Offset: 0x000B4260
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
		[ClientPropertyName("cascade")]
		public string OnCascade
		{
			get
			{
				return (string)(base.ViewState["OnCascade"] ?? "");
			}
			set
			{
				base.ViewState["OnCascade"] = value;
			}
		}

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000B6074 File Offset: 0x000B4274
		public bool IsDefault
		{
			get
			{
				return this.OnChange == "" && this.OnClose == "" && this.OnDataBound == "" && this.OnFiltering == "" && this.OnOpen == "" && this.OnSelect == "" && this.OnCascade == "";
			}
		}
	}
}
