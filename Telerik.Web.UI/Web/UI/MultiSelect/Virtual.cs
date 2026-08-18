using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x02000614 RID: 1556
	public class Virtual : StateManager, IDefaultCheck
	{
		// Token: 0x1700128E RID: 4750
		// (get) Token: 0x0600387F RID: 14463 RVA: 0x000BA1A6 File Offset: 0x000B83A6
		// (set) Token: 0x06003880 RID: 14464 RVA: 0x000BA1CF File Offset: 0x000B83CF
		[DefaultValue(0.0)]
		public double ItemHeight
		{
			get
			{
				return (double)(base.ViewState["ItemHeight"] ?? 0.0);
			}
			set
			{
				base.ViewState["ItemHeight"] = value;
			}
		}

		// Token: 0x1700128F RID: 4751
		// (get) Token: 0x06003881 RID: 14465 RVA: 0x000BA1E7 File Offset: 0x000B83E7
		// (set) Token: 0x06003882 RID: 14466 RVA: 0x000BA207 File Offset: 0x000B8407
		[DefaultValue("index")]
		public string MapValueTo
		{
			get
			{
				return (string)(base.ViewState["MapValueTo"] ?? "index");
			}
			set
			{
				base.ViewState["MapValueTo"] = value;
			}
		}

		// Token: 0x17001290 RID: 4752
		// (get) Token: 0x06003883 RID: 14467 RVA: 0x000BA21A File Offset: 0x000B841A
		// (set) Token: 0x06003884 RID: 14468 RVA: 0x000BA236 File Offset: 0x000B8436
		[DefaultValue(null)]
		public string ValueMapper
		{
			get
			{
				return (string)(base.ViewState["ValueMapper"] ?? null);
			}
			set
			{
				base.ViewState["ValueMapper"] = value;
			}
		}

		// Token: 0x17001291 RID: 4753
		// (get) Token: 0x06003885 RID: 14469 RVA: 0x000BA249 File Offset: 0x000B8449
		public bool IsDefault
		{
			get
			{
				return this.ItemHeight == 0.0 && this.MapValueTo == "index" && this.ValueMapper == null;
			}
		}
	}
}
