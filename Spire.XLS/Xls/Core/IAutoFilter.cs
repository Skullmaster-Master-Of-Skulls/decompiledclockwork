using System;

namespace Spire.Xls.Core
{
	// Token: 0x020005E0 RID: 1504
	public interface IAutoFilter
	{
		// Token: 0x17000DD3 RID: 3539
		// (get) Token: 0x06005974 RID: 22900
		IAutoFilterCondition FirstCondition { get; }

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06005975 RID: 22901
		IAutoFilterCondition SecondCondition { get; }

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06005976 RID: 22902
		bool IsFiltered { get; }

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06005977 RID: 22903
		// (set) Token: 0x06005978 RID: 22904
		bool IsAnd { get; set; }

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06005979 RID: 22905
		// (set) Token: 0x0600597A RID: 22906
		bool IsTop10Percent { get; set; }

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x0600597B RID: 22907
		// (set) Token: 0x0600597C RID: 22908
		bool IsSimple1 { get; set; }

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x0600597D RID: 22909
		// (set) Token: 0x0600597E RID: 22910
		bool IsSimple2 { get; set; }

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x0600597F RID: 22911
		// (set) Token: 0x06005980 RID: 22912
		bool ShowTopItem { get; set; }

		// Token: 0x17000DDB RID: 3547
		// (get) Token: 0x06005981 RID: 22913
		// (set) Token: 0x06005982 RID: 22914
		bool IsTop10Items { get; set; }

		// Token: 0x17000DDC RID: 3548
		// (get) Token: 0x06005983 RID: 22915
		// (set) Token: 0x06005984 RID: 22916
		int Top10Items { get; set; }
	}
}
