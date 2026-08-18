using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200003D RID: 61
	public class DrawerClientEvents : StateManager, IDefaultCheck
	{
		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000059AE File Offset: 0x00003BAE
		// (set) Token: 0x06000204 RID: 516 RVA: 0x000059CE File Offset: 0x00003BCE
		[ClientPropertyName("hide")]
		[ClientControlEvent]
		[DefaultValue("")]
		[Category("Client-side events")]
		public string OnHide
		{
			get
			{
				return (string)(base.ViewState["OnHide"] ?? "");
			}
			set
			{
				base.ViewState["OnHide"] = value;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000059E1 File Offset: 0x00003BE1
		// (set) Token: 0x06000206 RID: 518 RVA: 0x00005A01 File Offset: 0x00003C01
		[ClientControlEvent]
		[Category("Client-side events")]
		[ClientPropertyName("show")]
		[DefaultValue("")]
		public string OnShow
		{
			get
			{
				return (string)(base.ViewState["OnShow"] ?? "");
			}
			set
			{
				base.ViewState["OnShow"] = value;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000207 RID: 519 RVA: 0x00005A14 File Offset: 0x00003C14
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00005A34 File Offset: 0x00003C34
		[Category("Client-side events")]
		[ClientPropertyName("itemClick")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnItemClick
		{
			get
			{
				return (string)(base.ViewState["OnItemClick"] ?? "");
			}
			set
			{
				base.ViewState["OnItemClick"] = value;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000209 RID: 521 RVA: 0x00005A47 File Offset: 0x00003C47
		public bool IsDefault
		{
			get
			{
				return this.OnHide == "" && this.OnShow == "" && this.OnItemClick == "";
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00005A7F File Offset: 0x00003C7F
		// (set) Token: 0x0600020B RID: 523 RVA: 0x00005A9F File Offset: 0x00003C9F
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientPropertyName("initialize")]
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

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00005AB2 File Offset: 0x00003CB2
		// (set) Token: 0x0600020D RID: 525 RVA: 0x00005AD2 File Offset: 0x00003CD2
		[DefaultValue("")]
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("load")]
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
	}
}
