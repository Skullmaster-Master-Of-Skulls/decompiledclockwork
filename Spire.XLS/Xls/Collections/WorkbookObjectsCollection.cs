using System;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x02000036 RID: 54
	public class WorkbookObjectsCollection : XlsWorkbookObjectsCollection
	{
		// Token: 0x060003D4 RID: 980 RVA: 0x00022C7C File Offset: 0x00021C7C
		internal WorkbookObjectsCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060003D5 RID: 981 RVA: 0x00022C94 File Offset: 0x00021C94
		public new Workbook Workbook
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return ((XlsWorkbook)base.Workbook).InnerWorkBook;
			}
		}
	}
}
