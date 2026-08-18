using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x02000005 RID: 5
	public class BreadcrumbClientEvents : StateManager, IDefaultCheck
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x000021A5 File Offset: 0x000003A5
		// (set) Token: 0x06000011 RID: 17 RVA: 0x000021C5 File Offset: 0x000003C5
		[DefaultValue("")]
		public string OnClick
		{
			get
			{
				return (string)(base.ViewState["OnClick"] ?? "");
			}
			set
			{
				base.ViewState["OnClick"] = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021D8 File Offset: 0x000003D8
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000021F8 File Offset: 0x000003F8
		[DefaultValue("")]
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

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000220B File Offset: 0x0000040B
		public bool IsDefault
		{
			get
			{
				return this.OnClick == "" && this.OnChange == "";
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00002231 File Offset: 0x00000431
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00002251 File Offset: 0x00000451
		[DefaultValue("")]
		[ClientPropertyName("initialize")]
		[ClientControlEvent]
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

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002264 File Offset: 0x00000464
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00002284 File Offset: 0x00000484
		[DefaultValue("")]
		[ClientPropertyName("load")]
		[ClientControlEvent]
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
	}
}
