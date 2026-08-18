using System;
using System.Linq;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000068 RID: 104
	public class QueryDataSourceInjection<TSource> : IQueryDataSourceInjection<TSource>
	{
		// Token: 0x06000398 RID: 920 RVA: 0x00008F74 File Offset: 0x00007174
		public QueryDataSourceInjection(IQueryable<TSource> dataSource, IMapper mapper)
		{
			this._dataSource = dataSource;
			this._mapper = mapper;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x00008F8A File Offset: 0x0000718A
		public IQueryable<TDestination> For<TDestination>(SourceInjectedQueryInspector inspector = null)
		{
			return new SourceInjectedQuery<TSource, TDestination>(this._dataSource, new TDestination[0].AsQueryable<TDestination>(), this._mapper, inspector);
		}

		// Token: 0x040000B2 RID: 178
		private readonly IQueryable<TSource> _dataSource;

		// Token: 0x040000B3 RID: 179
		private readonly IMapper _mapper;
	}
}
