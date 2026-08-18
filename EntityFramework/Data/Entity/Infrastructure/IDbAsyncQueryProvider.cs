using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200029E RID: 670
	public interface IDbAsyncQueryProvider : IQueryProvider
	{
		// Token: 0x060017DD RID: 6109
		Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken);

		// Token: 0x060017DE RID: 6110
		Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken);
	}
}
