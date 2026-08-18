using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class BreadcrumbItem : StateManager, IAttributeAccessor
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000229F File Offset: 0x0000049F
		public BreadcrumbItem()
		{
			if (this.Attributes == null)
			{
				this.Attributes = new Dictionary<string, string>();
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022BA File Offset: 0x000004BA
		// (set) Token: 0x0600001C RID: 28 RVA: 0x000022DB File Offset: 0x000004DB
		[ClientPropertyName("type")]
		[Category("Behavior")]
		[ClientControlProperty]
		[Bindable(false)]
		[DefaultValue(BreadcrumbItemType.Item)]
		public BreadcrumbItemType Type
		{
			get
			{
				return (BreadcrumbItemType)(base.ViewState["Type"] ?? BreadcrumbItemType.Item);
			}
			set
			{
				base.ViewState["Type"] = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600001D RID: 29 RVA: 0x000022F3 File Offset: 0x000004F3
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00002313 File Offset: 0x00000513
		[DefaultValue("")]
		public string Href
		{
			get
			{
				return (string)(base.ViewState["Href"] ?? "");
			}
			set
			{
				base.ViewState["Href"] = value;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002326 File Offset: 0x00000526
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00002346 File Offset: 0x00000546
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return (string)(base.ViewState["Text"] ?? "");
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002359 File Offset: 0x00000559
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00002379 File Offset: 0x00000579
		[DefaultValue("")]
		public string ToolTip
		{
			get
			{
				return (string)(base.ViewState["ToolTip"] ?? "");
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000023 RID: 35 RVA: 0x0000238C File Offset: 0x0000058C
		// (set) Token: 0x06000024 RID: 36 RVA: 0x000023AC File Offset: 0x000005AC
		[DefaultValue("")]
		public string Icon
		{
			get
			{
				return (string)(base.ViewState["Icon"] ?? "");
			}
			set
			{
				base.ViewState["Icon"] = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000025 RID: 37 RVA: 0x000023BF File Offset: 0x000005BF
		// (set) Token: 0x06000026 RID: 38 RVA: 0x000023DF File Offset: 0x000005DF
		[DefaultValue("")]
		public string ItemClass
		{
			get
			{
				return (string)(base.ViewState["ItemClass"] ?? "");
			}
			set
			{
				base.ViewState["ItemClass"] = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000023F2 File Offset: 0x000005F2
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002412 File Offset: 0x00000612
		[DefaultValue("")]
		public string LinkClass
		{
			get
			{
				return (string)(base.ViewState["LinkClass"] ?? "");
			}
			set
			{
				base.ViewState["LinkClass"] = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002425 File Offset: 0x00000625
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002445 File Offset: 0x00000645
		[DefaultValue("")]
		public string IconClass
		{
			get
			{
				return (string)(base.ViewState["IconClass"] ?? "");
			}
			set
			{
				base.ViewState["IconClass"] = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002458 File Offset: 0x00000658
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002481 File Offset: 0x00000681
		[DefaultValue(false)]
		public bool ShowIcon
		{
			get
			{
				return (bool)(base.ViewState["ShowIcon"] ?? (this.Type == BreadcrumbItemType.RootItem));
			}
			set
			{
				base.ViewState["ShowIcon"] = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002499 File Offset: 0x00000699
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000024C2 File Offset: 0x000006C2
		[DefaultValue(false)]
		public bool ShowText
		{
			get
			{
				return (bool)(base.ViewState["ShowText"] ?? (this.Type == BreadcrumbItemType.Item));
			}
			set
			{
				base.ViewState["ShowText"] = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000024DA File Offset: 0x000006DA
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000024FB File Offset: 0x000006FB
		[DefaultValue(false)]
		public bool Disabled
		{
			get
			{
				return (bool)(base.ViewState["Disabled"] ?? false);
			}
			set
			{
				base.ViewState["Disabled"] = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002513 File Offset: 0x00000713
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000252F File Offset: 0x0000072F
		[Browsable(false)]
		public Dictionary<string, string> Attributes
		{
			get
			{
				return (Dictionary<string, string>)(base.ViewState["Attributes"] ?? null);
			}
			set
			{
				base.ViewState["Attributes"] = value;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002542 File Offset: 0x00000742
		public string GetAttribute(string key)
		{
			return this.Attributes[key];
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002550 File Offset: 0x00000750
		public void SetAttribute(string key, string value)
		{
			this.Attributes[key] = value;
		}
	}
}
