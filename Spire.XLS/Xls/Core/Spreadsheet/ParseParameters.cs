using System;
using System.Collections.Generic;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x020005FA RID: 1530
	public class ParseParameters
	{
		// Token: 0x06005A0E RID: 23054 RVA: 0x00386838 File Offset: 0x00385838
		public ParseParameters(IWorksheet sheet, Dictionary<string, string> worksheetNames, bool r1C1, int cellRow, int cellColumn, FormulaUtil formulaUtility, IWorkbook book)
		{
			this.Worksheet = sheet;
			this.WorksheetNames = worksheetNames;
			this.IsR1C1 = r1C1;
			this.CellRow = cellRow;
			this.CellColumn = cellColumn;
			this.FormulaUtility = formulaUtility;
			this.Workbook = book;
			this.Version = ((XlsWorkbook)this.Workbook).Version;
		}

		// Token: 0x04002C40 RID: 11328
		private bool \u25D8\u0083\u0086\u008E;

		// Token: 0x04002C41 RID: 11329
		public readonly FormulaUtil FormulaUtility;

		// Token: 0x04002C42 RID: 11330
		public readonly IWorksheet Worksheet;

		// Token: 0x04002C43 RID: 11331
		public readonly Dictionary<string, string> WorksheetNames;

		// Token: 0x04002C44 RID: 11332
		private bool \u2460\u009F\u00A3\u0092;

		// Token: 0x04002C45 RID: 11333
		private string[] \u25D8\u00AF\u0080\u0098;

		// Token: 0x04002C46 RID: 11334
		public readonly bool IsR1C1;

		// Token: 0x04002C47 RID: 11335
		public readonly int CellRow;

		// Token: 0x04002C48 RID: 11336
		public readonly int CellColumn;

		// Token: 0x04002C49 RID: 11337
		private float \u25D8\u0088\u00B0\u00A4;

		// Token: 0x04002C4A RID: 11338
		private long[] \u25D8\u00A0\u0081\u008F;

		// Token: 0x04002C4B RID: 11339
		private string \u25D8\u00A1\u00AE\u009E;

		// Token: 0x04002C4C RID: 11340
		public readonly IWorkbook Workbook;

		// Token: 0x04002C4D RID: 11341
		public readonly ExcelVersion Version;
	}
}
