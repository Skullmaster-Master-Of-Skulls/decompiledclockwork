using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000067 RID: 103
	public class MultiSelectClientEvents : StateManager, IDefaultCheck
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000452 RID: 1106 RVA: 0x0000B485 File Offset: 0x00009685
		// (set) Token: 0x06000453 RID: 1107 RVA: 0x0000B4A5 File Offset: 0x000096A5
		[ClientPropertyName("initialize")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
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

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000B4B8 File Offset: 0x000096B8
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0000B4D8 File Offset: 0x000096D8
		[ClientControlEvent]
		[ClientPropertyName("load")]
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

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x06000456 RID: 1110 RVA: 0x0000B4EB File Offset: 0x000096EB
		// (set) Token: 0x06000457 RID: 1111 RVA: 0x0000B50B File Offset: 0x0000970B
		[DefaultValue("")]
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

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000B51E File Offset: 0x0000971E
		// (set) Token: 0x06000459 RID: 1113 RVA: 0x0000B53E File Offset: 0x0000973E
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
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

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x0600045A RID: 1114 RVA: 0x0000B551 File Offset: 0x00009751
		// (set) Token: 0x0600045B RID: 1115 RVA: 0x0000B571 File Offset: 0x00009771
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
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

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000B584 File Offset: 0x00009784
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x0000B5A4 File Offset: 0x000097A4
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
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

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000B5B7 File Offset: 0x000097B7
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x0000B5D7 File Offset: 0x000097D7
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
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

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000B5EA File Offset: 0x000097EA
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x0000B60A File Offset: 0x0000980A
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
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

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000B61D File Offset: 0x0000981D
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x0000B63D File Offset: 0x0000983D
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnDeselect
		{
			get
			{
				return (string)(base.ViewState["OnDeselect"] ?? "");
			}
			set
			{
				base.ViewState["OnDeselect"] = value;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0000B650 File Offset: 0x00009850
		public bool IsDefault
		{
			get
			{
				return this.OnChange == "" && this.OnClose == "" && this.OnDataBound == "" && this.OnFiltering == "" && this.OnOpen == "" && this.OnSelect == "" && this.OnDeselect == "";
			}
		}
	}
}
