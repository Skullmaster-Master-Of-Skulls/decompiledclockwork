using System;
using System.IO;
using Spire.Xls.Core.Spreadsheet;

namespace Spire.Xls.Core
{
	// Token: 0x0200049B RID: 1179
	public interface IWorkbookSerializator
	{
		// Token: 0x060048FB RID: 18683
		void Serialize(string fullName, XlsWorkbook book, ExcelSaveType saveType);

		// Token: 0x060048FC RID: 18684
		void Serialize(Stream stream, XlsWorkbook book, ExcelSaveType saveType);
	}
}
