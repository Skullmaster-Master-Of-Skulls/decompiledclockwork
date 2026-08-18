using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.Internal;
using AutoMapper.Mappers;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x0200006D RID: 109
	public class SourceInjectedQueryProvider<TSource, TDestination> : IQueryProvider
	{
		// Token: 0x060003BA RID: 954 RVA: 0x0000961D File Offset: 0x0000781D
		public SourceInjectedQueryProvider(IMapper mapper, IQueryable<TSource> dataSource, IQueryable<TDestination> destQuery)
		{
			this._mapper = mapper;
			this._dataSource = dataSource;
			this._destQuery = destQuery;
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x060003BB RID: 955 RVA: 0x0000963A File Offset: 0x0000783A
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00009642 File Offset: 0x00007842
		public SourceInjectedQueryInspector Inspector { get; set; }

		// Token: 0x060003BD RID: 957 RVA: 0x0000964B File Offset: 0x0000784B
		public IQueryable CreateQuery(Expression expression)
		{
			return new SourceInjectedQuery<TSource, TDestination>(this, expression);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x00009654 File Offset: 0x00007854
		public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
		{
			return new SourceInjectedQuery<TSource, TElement>(this, expression);
		}

		// Token: 0x060003BF RID: 959 RVA: 0x00009660 File Offset: 0x00007860
		public object Execute(Expression expression)
		{
			this.Inspector.StartQueryExecuteInterceptor(null, expression);
			Expression expression2 = this.ConvertDestinationExpressionToSourceExpression(expression);
			object obj = this.InvokeSourceQuery(null, expression2);
			this.Inspector.SourceResult(expression2, obj);
			return obj;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x000096A4 File Offset: 0x000078A4
		public TResult Execute<TResult>(Expression expression)
		{
			Type typeFromHandle = typeof(TResult);
			this.Inspector.StartQueryExecuteInterceptor(typeFromHandle, expression);
			Expression expression2 = this.ConvertDestinationExpressionToSourceExpression(expression);
			Type typeFromHandle2 = typeof(TResult);
			Type type = SourceInjectedQueryProvider<TSource, TDestination>.CreateSourceResultType(typeFromHandle2);
			object obj = this.InvokeSourceQuery(type, expression2);
			this.Inspector.SourceResult(expression2, obj);
			IQueryable<TDestination> queryable = SourceInjectedQueryProvider<TSource, TDestination>.IsProjection<TDestination>(typeFromHandle) ? new ProjectionExpression(obj as IQueryable<TSource>, this._mapper.ConfigurationProvider.ExpressionBuilder).To<TDestination>(null) : this._mapper.Map(obj, type, typeFromHandle2);
			this.Inspector.DestResult(obj);
			return (TResult)((object)queryable);
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x00009757 File Offset: 0x00007957
		private object InvokeSourceQuery(Type sourceResultType, Expression sourceExpression)
		{
			if (!SourceInjectedQueryProvider<TSource, TDestination>.IsProjection<TSource>(sourceResultType))
			{
				return this._dataSource.Provider.Execute(sourceExpression);
			}
			return this._dataSource.Provider.CreateQuery(sourceExpression);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x00009784 File Offset: 0x00007984
		private static bool IsProjection<T>(Type resultType)
		{
			return resultType.IsEnumerableType() && !resultType.IsQueryableType() && resultType != typeof(string) && resultType.GetGenericElementType() == typeof(T);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000097BF File Offset: 0x000079BF
		private static Type CreateSourceResultType(Type destResultType)
		{
			return destResultType.ReplaceItemType(typeof(TDestination), typeof(TSource));
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x000097DC File Offset: 0x000079DC
		private Expression ConvertDestinationExpressionToSourceExpression(Expression expression)
		{
			TypeMap typeMap = this._mapper.ConfigurationProvider.FindTypeMapFor(typeof(TDestination), typeof(TSource));
			return new ExpressionMapper.MappingVisitor(this._mapper.ConfigurationProvider, typeMap, this._destQuery.Expression, this._dataSource.Expression, null, new Type[]
			{
				typeof(TSource)
			}).Visit(expression);
		}

		// Token: 0x040000C1 RID: 193
		private readonly IMapper _mapper;

		// Token: 0x040000C2 RID: 194
		private readonly IQueryable<TSource> _dataSource;

		// Token: 0x040000C3 RID: 195
		private readonly IQueryable<TDestination> _destQuery;
	}
}
