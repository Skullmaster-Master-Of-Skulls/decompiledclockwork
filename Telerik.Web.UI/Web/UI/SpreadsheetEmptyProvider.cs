using System;
using System.Collections.Generic;
using Telerik.Web.Spreadsheet;

namespace Telerik.Web.UI
{
	// Token: 0x020008C9 RID: 2249
	public class SpreadsheetEmptyProvider : SpreadsheetProviderBase
	{
		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x0600549F RID: 21663 RVA: 0x00102EE9 File Offset: 0x001010E9
		public override string Name
		{
			get
			{
				return "Integrated";
			}
		}

		// Token: 0x060054A1 RID: 21665 RVA: 0x00102EF8 File Offset: 0x001010F8
		public override List<Worksheet> GetSheets()
		{
			return new List<Worksheet>();
		}

		// Token: 0x060054A2 RID: 21666 RVA: 0x00102EFF File Offset: 0x001010FF
		public override void SaveWorkbook(Workbook workbook)
		{
		}
	}
}
