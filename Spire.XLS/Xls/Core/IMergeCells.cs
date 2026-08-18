using System;

namespace Spire.Xls.Core
{
	// Token: 0x020005E5 RID: 1509
	public interface IMergeCells
	{
		// Token: 0x0600599B RID: 22939
		void AddMerge(int RowFrom, int RowTo, int ColFrom, int ColTo, MergeOperationType operation);

		// Token: 0x0600599C RID: 22940
		void DeleteMerge(int CellIndex);
	}
}
