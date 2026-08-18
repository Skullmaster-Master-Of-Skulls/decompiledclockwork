using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper.QueryableExtensions.Impl;

namespace AutoMapper.QueryableExtensions
{
	// Token: 0x02000058 RID: 88
	public static class Extensions
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0000862D File Offset: 0x0000682D
		public static IQueryable<TDestination> Map<TSource, TDestination>(this IQueryable<TSource> sourceQuery, IQueryable<TDestination> destQuery)
		{
			return sourceQuery.Map(destQuery, Mapper.ConfigurationProvider);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000863B File Offset: 0x0000683B
		public static IQueryable<TDestination> Map<TSource, TDestination>(this IQueryable<TSource> sourceQuery, IQueryable<TDestination> destQuery, IConfigurationProvider config)
		{
			return QueryMapperVisitor.Map<TSource, TDestination>(sourceQuery, destQuery, config);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00008645 File Offset: 0x00006845
		public static IQueryDataSourceInjection<TSource> UseAsDataSource<TSource>(this IQueryable<TSource> dataSource)
		{
			return dataSource.UseAsDataSource(Mapper.Instance);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00008652 File Offset: 0x00006852
		public static IQueryDataSourceInjection<TSource> UseAsDataSource<TSource>(this IQueryable<TSource> dataSource, IMapper mapper)
		{
			return new QueryDataSourceInjection<TSource>(dataSource, mapper);
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000865B File Offset: 0x0000685B
		public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, object parameters, params Expression<Func<TDestination, object>>[] membersToExpand)
		{
			return source.ProjectTo(Mapper.ConfigurationProvider, parameters, membersToExpand);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000866A File Offset: 0x0000686A
		public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, IConfigurationProvider configuration, object parameters, params Expression<Func<TDestination, object>>[] membersToExpand)
		{
			return new ProjectionExpression(source, configuration.ExpressionBuilder).To<TDestination>(parameters, membersToExpand);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000867F File Offset: 0x0000687F
		public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, IConfigurationProvider configuration, params Expression<Func<TDestination, object>>[] membersToExpand)
		{
			return source.ProjectTo(configuration, null, membersToExpand);
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000868A File Offset: 0x0000688A
		public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, params Expression<Func<TDestination, object>>[] membersToExpand)
		{
			return source.ProjectTo(Mapper.ConfigurationProvider, null, membersToExpand);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00008699 File Offset: 0x00006899
		public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, IDictionary<string, object> parameters, params string[] membersToExpand)
		{
			return source.ProjectTo(Mapper.ConfigurationProvider, parameters, membersToExpand);
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000086A8 File Offset: 0x000068A8
		public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, IConfigurationProvider configuration, IDictionary<string, object> parameters, params string[] membersToExpand)
		{
			return new ProjectionExpression(source, configuration.ExpressionBuilder).To<TDestination>(parameters, membersToExpand);
		}
	}
}
