using System;
using System.Collections.Generic;

namespace System.Web.Http.Filters
{
	// Token: 0x020000F7 RID: 247
	internal sealed class FilterInfoComparer : IComparer<FilterInfo>
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x00014381 File Offset: 0x00012581
		public static FilterInfoComparer Instance
		{
			get
			{
				return FilterInfoComparer._instance;
			}
		}

		// Token: 0x06000618 RID: 1560 RVA: 0x00014388 File Offset: 0x00012588
		public int Compare(FilterInfo x, FilterInfo y)
		{
			if (x == null && y == null)
			{
				return 0;
			}
			if (x == null)
			{
				return -1;
			}
			if (y == null)
			{
				return 1;
			}
			return x.Scope - y.Scope;
		}

		// Token: 0x040001B3 RID: 435
		private static readonly FilterInfoComparer _instance = new FilterInfoComparer();
	}
}
