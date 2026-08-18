using System;
using System.ComponentModel;

namespace Telerik.Web.UI.MultiColumnComboBox
{
	// Token: 0x020005E9 RID: 1513
	public class Close : StateManager, IDefaultCheck
	{
		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x060036C8 RID: 14024 RVA: 0x000B5955 File Offset: 0x000B3B55
		// (set) Token: 0x060036C9 RID: 14025 RVA: 0x000B5975 File Offset: 0x000B3B75
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

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x060036CA RID: 14026 RVA: 0x000B5988 File Offset: 0x000B3B88
		// (set) Token: 0x060036CB RID: 14027 RVA: 0x000B59B1 File Offset: 0x000B3BB1
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

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x000B59C9 File Offset: 0x000B3BC9
		public bool IsDefault
		{
			get
			{
				return this.Effects == "" && this.Duration == 100.0;
			}
		}
	}
}
