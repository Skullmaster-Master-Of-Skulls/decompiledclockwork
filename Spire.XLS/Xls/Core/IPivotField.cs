using System;

namespace Spire.Xls.Core
{
	// Token: 0x0200022E RID: 558
	public interface IPivotField
	{
		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06002210 RID: 8720
		string Name { get; }

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06002211 RID: 8721
		// (set) Token: 0x06002212 RID: 8722
		AxisTypes Axis { get; set; }

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06002213 RID: 8723
		// (set) Token: 0x06002214 RID: 8724
		string NumberFormat { get; set; }

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06002215 RID: 8725
		// (set) Token: 0x06002216 RID: 8726
		SubtotalTypes Subtotals { get; set; }

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06002217 RID: 8727
		// (set) Token: 0x06002218 RID: 8728
		bool CanDragToRow { get; set; }

		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x06002219 RID: 8729
		// (set) Token: 0x0600221A RID: 8730
		bool CanDragToColumn { get; set; }

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600221B RID: 8731
		// (set) Token: 0x0600221C RID: 8732
		bool CanDragToPage { get; set; }

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600221D RID: 8733
		// (set) Token: 0x0600221E RID: 8734
		bool CanDragOff { get; set; }

		// Token: 0x17000C55 RID: 3157
		// (get) Token: 0x0600221F RID: 8735
		// (set) Token: 0x06002220 RID: 8736
		bool ShowBlankRow { get; set; }

		// Token: 0x17000C56 RID: 3158
		// (get) Token: 0x06002221 RID: 8737
		// (set) Token: 0x06002222 RID: 8738
		bool CanDragToData { get; set; }

		// Token: 0x17000C57 RID: 3159
		// (get) Token: 0x06002223 RID: 8739
		bool IsFormulaField { get; }

		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06002224 RID: 8740
		// (set) Token: 0x06002225 RID: 8741
		string Formula { get; set; }
	}
}
