using System;
using System.Collections.Generic;

namespace Telerik.Web.UI.PivotGrid.Core.Olap
{
	// Token: 0x02000CF6 RID: 3318
	internal interface IOlapResponseData
	{
		// Token: 0x17002796 RID: 10134
		// (get) Token: 0x06007BE1 RID: 31713
		IList<IOlapTuple> RowAxisTuples { get; }

		// Token: 0x17002797 RID: 10135
		// (get) Token: 0x06007BE2 RID: 31714
		IList<IOlapTuple> ColumnAxisTuples { get; }

		// Token: 0x17002798 RID: 10136
		// (get) Token: 0x06007BE3 RID: 31715
		OlapCellsDictionary Cells { get; }

		// Token: 0x17002799 RID: 10137
		// (get) Token: 0x06007BE4 RID: 31716
		IOlapPivotConfiguration Configuration { get; }
	}
}
