using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x0200060F RID: 1551
	public class Open : StateManager, IDefaultCheck
	{
		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06003868 RID: 14440 RVA: 0x000B9962 File Offset: 0x000B7B62
		// (set) Token: 0x06003869 RID: 14441 RVA: 0x000B9982 File Offset: 0x000B7B82
		[DefaultValue("")]
		public string Effects
		{
			get
			{
				return (string)(base.ViewState["Effects"] ?? "");
			}
			set
			{
				base.ViewState["Effects"] = value;
			}
		}

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x0600386A RID: 14442 RVA: 0x000B9995 File Offset: 0x000B7B95
		// (set) Token: 0x0600386B RID: 14443 RVA: 0x000B99BE File Offset: 0x000B7BBE
		[DefaultValue(200.0)]
		public double Duration
		{
			get
			{
				return (double)(base.ViewState["Duration"] ?? 200.0);
			}
			set
			{
				base.ViewState["Duration"] = value;
			}
		}

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x0600386C RID: 14444 RVA: 0x000B99D6 File Offset: 0x000B7BD6
		public bool IsDefault
		{
			get
			{
				return this.Effects == "" && this.Duration == 200.0;
			}
		}
	}
}
