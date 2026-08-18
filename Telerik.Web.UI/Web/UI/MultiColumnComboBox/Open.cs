using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005F0 RID: 1520
	public class Open : StateManager, IDefaultCheck
	{
		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x000B633E File Offset: 0x000B453E
		// (set) Token: 0x0600370D RID: 14093 RVA: 0x000B635E File Offset: 0x000B455E
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

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x000B6371 File Offset: 0x000B4571
		// (set) Token: 0x0600370F RID: 14095 RVA: 0x000B639A File Offset: 0x000B459A
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

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x000B63B2 File Offset: 0x000B45B2
		public bool IsDefault
		{
			get
			{
				return this.Effects == "" && this.Duration == 200.0;
			}
		}
	}
}
