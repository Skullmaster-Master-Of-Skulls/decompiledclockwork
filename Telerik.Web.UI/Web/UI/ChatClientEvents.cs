using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200007E RID: 126
	public class ChatClientEvents : StateManager, IDefaultCheck
	{
		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x0000CC1A File Offset: 0x0000AE1A
		// (set) Token: 0x06000517 RID: 1303 RVA: 0x0000CC3A File Offset: 0x0000AE3A
		[Category("Client-side events")]
		[ClientControlEvent]
		[ClientPropertyName("actionClick")]
		[DefaultValue("")]
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

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x0000CC4D File Offset: 0x0000AE4D
		// (set) Token: 0x06000519 RID: 1305 RVA: 0x0000CC6D File Offset: 0x0000AE6D
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("post")]
		public string OnPost
		{
			get
			{
				return (string)(base.ViewState["OnPost"] ?? "");
			}
			set
			{
				base.ViewState["OnPost"] = value;
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x0000CC80 File Offset: 0x0000AE80
		// (set) Token: 0x0600051B RID: 1307 RVA: 0x0000CCA0 File Offset: 0x0000AEA0
		[ClientPropertyName("sendMessage")]
		[Category("Client-side events")]
		[DefaultValue("")]
		[ClientControlEvent]
		public string OnSendMessage
		{
			get
			{
				return (string)(base.ViewState["OnSendMessage"] ?? "");
			}
			set
			{
				base.ViewState["OnSendMessage"] = value;
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x0000CCB3 File Offset: 0x0000AEB3
		// (set) Token: 0x0600051D RID: 1309 RVA: 0x0000CCD3 File Offset: 0x0000AED3
		[DefaultValue("")]
		[ClientPropertyName("typingEnd")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnTypingEnd
		{
			get
			{
				return (string)(base.ViewState["OnTypingEnd"] ?? "");
			}
			set
			{
				base.ViewState["OnTypingEnd"] = value;
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x0000CCE6 File Offset: 0x0000AEE6
		// (set) Token: 0x0600051F RID: 1311 RVA: 0x0000CD06 File Offset: 0x0000AF06
		[ClientControlEvent]
		[ClientPropertyName("typingStart")]
		[Category("Client-side events")]
		[DefaultValue("")]
		public string OnTypingStart
		{
			get
			{
				return (string)(base.ViewState["OnTypingStart"] ?? "");
			}
			set
			{
				base.ViewState["OnTypingStart"] = value;
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0000CD19 File Offset: 0x0000AF19
		// (set) Token: 0x06000521 RID: 1313 RVA: 0x0000CD39 File Offset: 0x0000AF39
		[DefaultValue("")]
		[ClientPropertyName("toolClick")]
		[ClientControlEvent]
		[Category("Client-side events")]
		public string OnToolClick
		{
			get
			{
				return (string)(base.ViewState["OnToolClick"] ?? "");
			}
			set
			{
				base.ViewState["OnToolClick"] = value;
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x0000CD4C File Offset: 0x0000AF4C
		public bool IsDefault
		{
			get
			{
				return this.OnLoad == "" && this.OnActionClick == "" && this.OnPost == "" && this.OnSendMessage == "" && this.OnTypingEnd == "" && this.OnTypingStart == "" && this.OnToolClick == "";
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x0000CDD7 File Offset: 0x0000AFD7
		// (set) Token: 0x06000524 RID: 1316 RVA: 0x0000CDF7 File Offset: 0x0000AFF7
		[ClientControlEvent]
		[DefaultValue("")]
		[ClientPropertyName("initialize")]
		[Category("Client-side events")]
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

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x0000CE0A File Offset: 0x0000B00A
		// (set) Token: 0x06000526 RID: 1318 RVA: 0x0000CE2A File Offset: 0x0000B02A
		[ClientControlEvent]
		[Category("Client-side events")]
		[ClientPropertyName("load")]
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
	}
}
