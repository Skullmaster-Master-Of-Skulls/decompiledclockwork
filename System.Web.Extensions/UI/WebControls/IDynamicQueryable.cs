using System;
using System.Linq;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000097 RID: 151
	internal interface IDynamicQueryable
	{
		// Token: 0x060006A6 RID: 1702
		IQueryable Where(IQueryable source, string predicate, params object[] values);

		// Token: 0x060006A7 RID: 1703
		IQueryable Select(IQueryable source, string selector, params object[] values);

		// Token: 0x060006A8 RID: 1704
		IQueryable OrderBy(IQueryable source, string ordering, params object[] values);

		// Token: 0x060006A9 RID: 1705
		IQueryable Take(IQueryable source, int count);

		// Token: 0x060006AA RID: 1706
		IQueryable Skip(IQueryable source, int count);

		// Token: 0x060006AB RID: 1707
		IQueryable GroupBy(IQueryable source, string keySelector, string elementSelector, params object[] values);

		// Token: 0x060006AC RID: 1708
		int Count(IQueryable source);
	}
}
