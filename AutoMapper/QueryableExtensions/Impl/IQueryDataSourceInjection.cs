using System;
using System.Linq;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000067 RID: 103
	public interface IQueryDataSourceInjection<TSource>
	{
		// Token: 0x06000397 RID: 919
		IQueryable<TDestination> For<TDestination>(SourceInjectedQueryInspector inspector = null);
	}
}
