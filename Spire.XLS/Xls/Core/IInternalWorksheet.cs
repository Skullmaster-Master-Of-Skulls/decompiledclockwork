using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x020005D4 RID: 1492
	public interface IInternalWorksheet : IWorksheet
	{
		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x060058A7 RID: 22695
		int DefaultPrintRowHeight { get; }

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x060058A8 RID: 22696
		// (set) Token: 0x060058A9 RID: 22697
		int FirstRow { get; set; }

		// Token: 0x17000D81 RID: 3457
		// (get) Token: 0x060058AA RID: 22698
		// (set) Token: 0x060058AB RID: 22699
		int FirstColumn { get; set; }

		// Token: 0x17000D82 RID: 3458
		// (get) Token: 0x060058AC RID: 22700
		// (set) Token: 0x060058AD RID: 22701
		int LastRow { get; set; }

		// Token: 0x17000D83 RID: 3459
		// (get) Token: 0x060058AE RID: 22702
		// (set) Token: 0x060058AF RID: 22703
		int LastColumn { get; set; }

		// Token: 0x17000D84 RID: 3460
		// (get) Token: 0x060058B0 RID: 22704
		XlsCellRecordCollection CellRecords { get; }

		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060058B1 RID: 22705
		XlsWorkbook ParentWorkbook { get; }

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060058B2 RID: 22706
		ExcelVersion Version { get; }

		// Token: 0x060058B3 RID: 22707
		bool IsArrayFormula(long index);

		// Token: 0x060058B4 RID: 22708
		IInternalWorksheet GetClonedObject(Dictionary<string, string> hashNewNames, XlsWorkbook book);
	}
}
