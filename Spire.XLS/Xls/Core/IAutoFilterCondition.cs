using System;

namespace Spire.Xls.Core
{
	// Token: 0x020005E2 RID: 1506
	public interface IAutoFilterCondition
	{
		// Token: 0x17000DDF RID: 3551
		// (get) Token: 0x06005989 RID: 22921
		// (set) Token: 0x0600598A RID: 22922
		FilterDataType DataType { get; set; }

		// Token: 0x17000DE0 RID: 3552
		// (get) Token: 0x0600598B RID: 22923
		// (set) Token: 0x0600598C RID: 22924
		FilterConditionType ConditionOperator { get; set; }

		// Token: 0x17000DE1 RID: 3553
		// (get) Token: 0x0600598D RID: 22925
		// (set) Token: 0x0600598E RID: 22926
		string String { get; set; }

		// Token: 0x17000DE2 RID: 3554
		// (get) Token: 0x0600598F RID: 22927
		// (set) Token: 0x06005990 RID: 22928
		bool Boolean { get; set; }

		// Token: 0x17000DE3 RID: 3555
		// (get) Token: 0x06005991 RID: 22929
		// (set) Token: 0x06005992 RID: 22930
		byte ErrorCode { get; set; }

		// Token: 0x17000DE4 RID: 3556
		// (get) Token: 0x06005993 RID: 22931
		// (set) Token: 0x06005994 RID: 22932
		double Double { get; set; }
	}
}
