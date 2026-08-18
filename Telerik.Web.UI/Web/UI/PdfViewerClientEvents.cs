using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000655 RID: 1621
	public class PdfViewerClientEvents : StateManager, IDefaultCheck
	{
		// Token: 0x17001391 RID: 5009
		// (get) Token: 0x06003B8B RID: 15243 RVA: 0x000C1FB2 File Offset: 0x000C01B2
		// (set) Token: 0x06003B8C RID: 15244 RVA: 0x000C1FD2 File Offset: 0x000C01D2
		[DefaultValue("")]
		[ClientControlEvent]
		[Category("Client-side events")]
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

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06003B8D RID: 15245 RVA: 0x000C1FE5 File Offset: 0x000C01E5
		// (set) Token: 0x06003B8E RID: 15246 RVA: 0x000C2005 File Offset: 0x000C0205
		[DefaultValue("")]
		[ClientControlEvent]
		[ClientPropertyName("load")]
		[Category("Client-side events")]
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

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06003B8F RID: 15247 RVA: 0x000C2018 File Offset: 0x000C0218
		// (set) Token: 0x06003B90 RID: 15248 RVA: 0x000C2038 File Offset: 0x000C0238
		[DefaultValue("")]
		public string OnRender
		{
			get
			{
				return (string)(base.ViewState["OnRender"] ?? "");
			}
			set
			{
				base.ViewState["OnRender"] = value;
			}
		}

		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06003B91 RID: 15249 RVA: 0x000C204B File Offset: 0x000C024B
		// (set) Token: 0x06003B92 RID: 15250 RVA: 0x000C206B File Offset: 0x000C026B
		[DefaultValue("")]
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

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06003B93 RID: 15251 RVA: 0x000C207E File Offset: 0x000C027E
		// (set) Token: 0x06003B94 RID: 15252 RVA: 0x000C209E File Offset: 0x000C029E
		[DefaultValue("")]
		public string OnError
		{
			get
			{
				return (string)(base.ViewState["OnError"] ?? "");
			}
			set
			{
				base.ViewState["OnError"] = value;
			}
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06003B95 RID: 15253 RVA: 0x000C20B1 File Offset: 0x000C02B1
		public bool IsDefault
		{
			get
			{
				return this.OnRender == "" && this.OnOpen == "" && this.OnError == "";
			}
		}
	}
}
