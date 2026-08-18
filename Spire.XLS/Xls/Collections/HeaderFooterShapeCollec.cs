using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x02000026 RID: 38
	public class HeaderFooterShapeCollec : XlsHeaderFooterShapeCollection
	{
		// Token: 0x060002A7 RID: 679 RVA: 0x00017EAC File Offset: 0x00016EAC
		internal HeaderFooterShapeCollec(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060002A8 RID: 680 RVA: 0x00017EC4 File Offset: 0x00016EC4
		public new Worksheet Worksheet
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
				return (Worksheet)base.Worksheet;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00017F0C File Offset: 0x00016F0C
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
				return base.Workbook.InnerWorkBook;
			}
		}
	}
}
