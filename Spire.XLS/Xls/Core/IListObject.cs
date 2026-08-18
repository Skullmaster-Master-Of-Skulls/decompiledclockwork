using System;
using System.Collections.Generic;

namespace Spire.Xls.Core
{
	// Token: 0x0200017A RID: 378
	public interface IListObject
	{
		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001201 RID: 4609
		// (set) Token: 0x06001202 RID: 4610
		string Name { get; set; }

		// Token: 0x17000657 RID: 1623
		// (get) Token: 0x06001203 RID: 4611
		// (set) Token: 0x06001204 RID: 4612
		IXLSRange Location { get; set; }

		// Token: 0x17000658 RID: 1624
		// (get) Token: 0x06001205 RID: 4613
		IList<IListObjectColumn> Columns { get; }

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001206 RID: 4614
		int Index { get; }

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001207 RID: 4615
		// (set) Token: 0x06001208 RID: 4616
		TableBuiltInStyles BuiltInTableStyle { get; set; }

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001209 RID: 4617
		IWorksheet Worksheet { get; }

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600120A RID: 4618
		// (set) Token: 0x0600120B RID: 4619
		string DisplayName { get; set; }

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600120C RID: 4620
		int TotalsRowCount { get; }

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600120D RID: 4621
		// (set) Token: 0x0600120E RID: 4622
		bool DisplayTotalRow { get; set; }

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x0600120F RID: 4623
		// (set) Token: 0x06001210 RID: 4624
		bool ShowTableStyleRowStripes { get; set; }

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001211 RID: 4625
		// (set) Token: 0x06001212 RID: 4626
		bool ShowTableStyleColumnStripes { get; set; }

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001213 RID: 4627
		// (set) Token: 0x06001214 RID: 4628
		bool DisplayLastColumn { get; set; }

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001215 RID: 4629
		// (set) Token: 0x06001216 RID: 4630
		bool DisplayFirstColumn { get; set; }

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001217 RID: 4631
		// (set) Token: 0x06001218 RID: 4632
		bool DisplayHeaderRow { get; set; }
	}
}
