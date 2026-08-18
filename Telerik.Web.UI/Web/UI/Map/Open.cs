using System;
using System.ComponentModel;

namespace Telerik.Web.UI.Map
{
	// Token: 0x020005AB RID: 1451
	public class Open : StateManager, IDefaultCheck
	{
		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x060033E9 RID: 13289 RVA: 0x000AC666 File Offset: 0x000AA866
		// (set) Token: 0x060033EA RID: 13290 RVA: 0x000AC686 File Offset: 0x000AA886
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

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x060033EB RID: 13291 RVA: 0x000AC699 File Offset: 0x000AA899
		// (set) Token: 0x060033EC RID: 13292 RVA: 0x000AC6C2 File Offset: 0x000AA8C2
		[DefaultValue(0.0)]
		public double Duration
		{
			get
			{
				return (double)(base.ViewState["Duration"] ?? 0.0);
			}
			set
			{
				base.ViewState["Duration"] = value;
			}
		}

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x060033ED RID: 13293 RVA: 0x000AC6DA File Offset: 0x000AA8DA
		public bool IsDefault
		{
			get
			{
				return this.Effects == "" && this.Duration == 0.0;
			}
		}
	}
}
