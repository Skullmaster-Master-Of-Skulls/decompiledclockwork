using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Web.DynamicData
{
	// Token: 0x02000108 RID: 264
	public interface IDynamicDataSource : IDataSource
	{
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06000DE0 RID: 3552
		// (set) Token: 0x06000DE1 RID: 3553
		bool AutoGenerateWhereClause { get; set; }

		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x06000DE2 RID: 3554
		// (set) Token: 0x06000DE3 RID: 3555
		Type ContextType { get; set; }

		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x06000DE4 RID: 3556
		// (set) Token: 0x06000DE5 RID: 3557
		bool EnableDelete { get; set; }

		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x06000DE6 RID: 3558
		// (set) Token: 0x06000DE7 RID: 3559
		bool EnableInsert { get; set; }

		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x06000DE8 RID: 3560
		// (set) Token: 0x06000DE9 RID: 3561
		bool EnableUpdate { get; set; }

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000DEA RID: 3562
		// (set) Token: 0x06000DEB RID: 3563
		string EntitySetName { get; set; }

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000DEC RID: 3564
		// (set) Token: 0x06000DED RID: 3565
		string Where { get; set; }

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000DEE RID: 3566
		ParameterCollection WhereParameters { get; }

		// Token: 0x14000046 RID: 70
		// (add) Token: 0x06000DEF RID: 3567
		// (remove) Token: 0x06000DF0 RID: 3568
		event EventHandler<DynamicValidatorEventArgs> Exception;
	}
}
