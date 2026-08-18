using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace AutoMapper.QueryableExtensions
{
	// Token: 0x0200005B RID: 91
	public interface IProjectionExpression
	{
		// Token: 0x0600035C RID: 860
		IQueryable<TResult> To<TResult>(object parameters = null);

		// Token: 0x0600035D RID: 861
		IQueryable<TResult> To<TResult>(IDictionary<string, object> parameters);

		// Token: 0x0600035E RID: 862
		IQueryable<TResult> To<TResult>(object parameters = null, params string[] membersToExpand);

		// Token: 0x0600035F RID: 863
		IQueryable<TResult> To<TResult>(IDictionary<string, object> parameters, params string[] membersToExpand);

		// Token: 0x06000360 RID: 864
		IQueryable<TResult> To<TResult>(object parameters = null, params Expression<Func<TResult, object>>[] membersToExpand);

		// Token: 0x06000361 RID: 865
		IQueryable<TResult> To<TResult>(IDictionary<string, object> parameters, params Expression<Func<TResult, object>>[] membersToExpand);
	}
}
