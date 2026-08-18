using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000385 RID: 901
	public interface IListObjectColumn
	{
		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x060036BB RID: 14011
		// (set) Token: 0x060036BC RID: 14012
		string Name { get; set; }

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x060036BD RID: 14013
		int Index { get; }

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x060036BE RID: 14014
		int Id { get; }

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x060036BF RID: 14015
		// (set) Token: 0x060036C0 RID: 14016
		ExcelTotalsCalculation TotalsCalculation { get; set; }

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x060036C1 RID: 14017
		// (set) Token: 0x060036C2 RID: 14018
		string TotalsRowLabel { get; set; }

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x060036C3 RID: 14019
		// (set) Token: 0x060036C4 RID: 14020
		string CalculatedFormula { get; set; }
	}
}
