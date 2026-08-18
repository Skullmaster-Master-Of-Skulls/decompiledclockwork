using System;

namespace Spire.Doc.Reporting
{
	// Token: 0x020000FD RID: 253
	public interface IRowsEnumerator
	{
		// Token: 0x060006BB RID: 1723
		void Reset();

		// Token: 0x060006BC RID: 1724
		bool NextRow();

		// Token: 0x060006BD RID: 1725
		object GetCellValue(string columnName);

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060006BE RID: 1726
		string[] ColumnNames { get; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060006BF RID: 1727
		int RowsCount { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060006C0 RID: 1728
		int CurrentRowIndex { get; }

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x060006C1 RID: 1729
		string TableName { get; }

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060006C2 RID: 1730
		bool IsEnd { get; }

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060006C3 RID: 1731
		bool IsLast { get; }
	}
}
