using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x02000020 RID: 32
	public class AutoFiltersCollection : XlsAutoFiltersCollection
	{
		// Token: 0x06000260 RID: 608 RVA: 0x00014F3C File Offset: 0x00013F3C
		internal AutoFiltersCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000261 RID: 609 RVA: 0x00014F54 File Offset: 0x00013F54
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
				return base.Worksheet;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000262 RID: 610 RVA: 0x00014F98 File Offset: 0x00013F98
		// (set) Token: 0x06000263 RID: 611 RVA: 0x00014FE0 File Offset: 0x00013FE0
		public new CellRange Range
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
				return base.Range as CellRange;
			}
			set
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				base.Range = value;
			}
		}

		// Token: 0x170000F9 RID: 249
		public AutoFilter this[int columnIndex]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return (AutoFilter)base.InnerList[columnIndex];
			}
		}
	}
}
