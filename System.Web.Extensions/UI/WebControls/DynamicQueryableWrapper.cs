using System;
using System.Linq;
using System.Web.Query.Dynamic;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000096 RID: 150
	internal class DynamicQueryableWrapper : IDynamicQueryable
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x0001C924 File Offset: 0x0001AB24
		public IQueryable Where(IQueryable source, string predicate, params object[] values)
		{
			return source.Where(predicate, values);
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x0001C92E File Offset: 0x0001AB2E
		public IQueryable Select(IQueryable source, string selector, params object[] values)
		{
			return source.Select(selector, values);
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x0001C938 File Offset: 0x0001AB38
		public IQueryable OrderBy(IQueryable source, string ordering, params object[] values)
		{
			return source.OrderBy(ordering, values);
		}

		// Token: 0x060006A1 RID: 1697 RVA: 0x0001C942 File Offset: 0x0001AB42
		public IQueryable Take(IQueryable source, int count)
		{
			return source.Take(count);
		}

		// Token: 0x060006A2 RID: 1698 RVA: 0x0001C94B File Offset: 0x0001AB4B
		public IQueryable Skip(IQueryable source, int count)
		{
			return source.Skip(count);
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x0001C954 File Offset: 0x0001AB54
		public IQueryable GroupBy(IQueryable source, string keySelector, string elementSelector, params object[] values)
		{
			return source.GroupBy(keySelector, elementSelector, values);
		}

		// Token: 0x060006A4 RID: 1700 RVA: 0x0001C960 File Offset: 0x0001AB60
		public int Count(IQueryable source)
		{
			return source.Count();
		}
	}
}
