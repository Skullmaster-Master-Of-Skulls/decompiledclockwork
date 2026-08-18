using System;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls
{
	// Token: 0x0200010C RID: 268
	public class AutoFilter : XlsAutoFilter
	{
		// Token: 0x06000C17 RID: 3095 RVA: 0x00076168 File Offset: 0x00075168
		internal AutoFilter(XlsAutoFiltersCollection A_0) : base(A_0)
		{
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x0007617C File Offset: 0x0007517C
		internal AutoFilter(XlsAutoFiltersCollection A_0, int A_1, int A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000C19 RID: 3097 RVA: 0x00076194 File Offset: 0x00075194
		public new AutoFilterCondition FirstCondition
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
				return base.FirstCondition as AutoFilterCondition;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000C1A RID: 3098 RVA: 0x000761DC File Offset: 0x000751DC
		public new AutoFilterCondition SecondCondition
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
				return base.SecondCondition as AutoFilterCondition;
			}
		}
	}
}
