using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiSelect
{
	// Token: 0x0200060B RID: 1547
	public class Close : StateManager, IDefaultCheck
	{
		// Token: 0x1700127A RID: 4730
		// (get) Token: 0x06003852 RID: 14418 RVA: 0x000B967E File Offset: 0x000B787E
		// (set) Token: 0x06003853 RID: 14419 RVA: 0x000B969E File Offset: 0x000B789E
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

		// Token: 0x1700127B RID: 4731
		// (get) Token: 0x06003854 RID: 14420 RVA: 0x000B96B1 File Offset: 0x000B78B1
		// (set) Token: 0x06003855 RID: 14421 RVA: 0x000B96DA File Offset: 0x000B78DA
		[DefaultValue(100.0)]
		public double Duration
		{
			get
			{
				return (double)(base.ViewState["Duration"] ?? 100.0);
			}
			set
			{
				base.ViewState["Duration"] = value;
			}
		}

		// Token: 0x1700127C RID: 4732
		// (get) Token: 0x06003856 RID: 14422 RVA: 0x000B96F2 File Offset: 0x000B78F2
		public bool IsDefault
		{
			get
			{
				return this.Effects == "" && this.Duration == 100.0;
			}
		}
	}
}
