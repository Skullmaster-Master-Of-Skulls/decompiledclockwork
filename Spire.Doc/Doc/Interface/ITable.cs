using System;
using Spire.Doc.Collections;
using Spire.Doc.Formatting;

namespace Spire.Doc.Interface
{
	// Token: 0x020000DD RID: 221
	public interface ITable : ICompositeObject
	{
		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000323 RID: 803
		RowCollection Rows { get; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000324 RID: 804
		RowFormat TableFormat { get; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000325 RID: 805
		TableCell LastCell { get; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000326 RID: 806
		TableRow FirstRow { get; }

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000327 RID: 807
		TableRow LastRow { get; }

		// Token: 0x1700011C RID: 284
		TableCell this[int row, int column]
		{
			get;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000329 RID: 809
		float Width { get; }

		// Token: 0x0600032A RID: 810
		TableRow AddRow();

		// Token: 0x0600032B RID: 811
		TableRow AddRow(bool isCopyFormat);

		// Token: 0x0600032C RID: 812
		TableRow AddRow(bool isCopyFormat, bool autoPopulateCells);

		// Token: 0x0600032D RID: 813
		void ResetCells(int rowsNum, int columnsNum);

		// Token: 0x0600032E RID: 814
		void ResetCells(int rowsNum, int columnsNum, RowFormat format, float cellWidth);

		// Token: 0x0600032F RID: 815
		void ApplyVerticalMerge(int columnIndex, int startRowIndex, int endRowIndex);

		// Token: 0x06000330 RID: 816
		void ApplyHorizontalMerge(int rowIndex, int startCellIndex, int endCellIndex);

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000331 RID: 817
		// (set) Token: 0x06000332 RID: 818
		float IndentFromLeft { get; set; }

		// Token: 0x06000333 RID: 819
		void RemoveAbsPosition();
	}
}
