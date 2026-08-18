using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using Telerik.Web.Spreadsheet;

namespace Telerik.Web.UI
{
	// Token: 0x020008C7 RID: 2247
	public abstract class SpreadsheetProviderBase : ProviderBase
	{
		// Token: 0x06005491 RID: 21649 RVA: 0x00102D45 File Offset: 0x00100F45
		public SpreadsheetProviderBase()
		{
		}

		// Token: 0x06005492 RID: 21650
		public abstract List<Worksheet> GetSheets();

		// Token: 0x06005493 RID: 21651
		public abstract void SaveWorkbook(Workbook workbook);
	}
}
