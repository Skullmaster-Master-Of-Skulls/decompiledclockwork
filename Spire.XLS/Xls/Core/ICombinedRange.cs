using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Spreadsheet;

namespace Spire.Xls.Core
{
	// Token: 0x020001E2 RID: 482
	public interface ICombinedRange : IXLSRange
	{
		// Token: 0x06001B11 RID: 6929
		string GetNewRangeLocation(Dictionary<string, string> names, out string strSheetName);

		// Token: 0x06001B12 RID: 6930
		IXLSRange Clone(object parent, Dictionary<string, string> hashNewNames, XlsWorkbook book);

		// Token: 0x06001B13 RID: 6931
		void ClearConditionalFormats();

		// Token: 0x06001B14 RID: 6932
		Rectangle[] GetRectangles();

		// Token: 0x06001B15 RID: 6933
		int GetRectanglesCount();

		// Token: 0x17000A2B RID: 2603
		// (get) Token: 0x06001B16 RID: 6934
		int CellsCount { get; }

		// Token: 0x17000A2C RID: 2604
		// (get) Token: 0x06001B17 RID: 6935
		string RangeGlobalAddress2007 { get; }

		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06001B18 RID: 6936
		string WorksheetName { get; }
	}
}
