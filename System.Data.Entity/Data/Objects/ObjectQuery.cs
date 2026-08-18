using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Common.CommandTrees.ExpressionBuilder;
using System.Data.Common.Utils;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.ELinq;
using System.Data.Objects.Internal;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Objects
{
	// Token: 0x0200012F RID: 303
	public class ObjectQuery<T> : ObjectQuery, IEnumerable<T>, IEnumerable, IQueryable<T>, IQueryable, IOrderedQueryable<T>, IOrderedQueryable, IListSource
	{
		// Token: 0x060015FC RID: 5628 RVA: 0x0004A1F7 File Offset: 0x000483F7
		private static bool IsLinqQuery(ObjectQuery query)
		{
			return query.QueryState is ELinqQueryState;
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0004A207 File Offset: 0x00048407
		public ObjectQuery(string commandText, ObjectContext context) : this(new EntitySqlQueryState(typeof(T), commandText, false, context, null, null))
		{
			context.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x0004A240 File Offset: 0x00048440
		public ObjectQuery(string commandText, ObjectContext context, MergeOption mergeOption) : this(new EntitySqlQueryState(typeof(T), commandText, false, context, null, null))
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			base.QueryState.UserSpecifiedMergeOption = new MergeOption?(mergeOption);
			context.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0004A298 File Offset: 0x00048498
		internal ObjectQuery(EntitySetBase entitySet, ObjectContext context, MergeOption mergeOption) : this(new EntitySqlQueryState(typeof(T), ObjectQuery<T>.BuildScanEntitySetEsql(entitySet), entitySet.Scan(), false, context, null, null))
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			base.QueryState.UserSpecifiedMergeOption = new MergeOption?(mergeOption);
			context.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0004A2FC File Offset: 0x000484FC
		private static string BuildScanEntitySetEsql(EntitySetBase entitySet)
		{
			EntityUtil.CheckArgumentNull<EntitySetBase>(entitySet, "entitySet");
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				EntityUtil.QuoteIdentifier(entitySet.EntityContainer.Name),
				EntityUtil.QuoteIdentifier(entitySet.Name)
			});
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0004A34B File Offset: 0x0004854B
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x0004A353 File Offset: 0x00048553
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				EntityUtil.CheckArgumentNull<string>(value, "value");
				if (!ObjectParameter.ValidateParameterName(value))
				{
					throw EntityUtil.Argument(Strings.ObjectQuery_InvalidQueryName(value), "value");
				}
				this._name = value;
			}
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0004A381 File Offset: 0x00048581
		public ObjectQuery<T> Distinct()
		{
			if (ObjectQuery<T>.IsLinqQuery(this))
			{
				return (ObjectQuery<T>)this.Distinct<T>();
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Distinct(base.QueryState));
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0004A3A8 File Offset: 0x000485A8
		public ObjectQuery<T> Except(ObjectQuery<T> query)
		{
			EntityUtil.CheckArgumentNull<ObjectQuery<T>>(query, "query");
			if (ObjectQuery<T>.IsLinqQuery(this) || ObjectQuery<T>.IsLinqQuery(query))
			{
				return (ObjectQuery<T>)this.Except(query);
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Except(base.QueryState, query.QueryState));
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0004A3F4 File Offset: 0x000485F4
		public ObjectQuery<DbDataRecord> GroupBy(string keys, string projection, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckArgumentNull<string>(keys, "keys");
			EntityUtil.CheckArgumentNull<string>(projection, "projection");
			EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(keys))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidGroupKeyList, "keys");
			}
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(projection))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidProjectionList, "projection");
			}
			return new ObjectQuery<DbDataRecord>(EntitySqlQueryBuilder.GroupBy(base.QueryState, this.Name, keys, projection, parameters));
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0004A470 File Offset: 0x00048670
		public ObjectQuery<T> Intersect(ObjectQuery<T> query)
		{
			EntityUtil.CheckArgumentNull<ObjectQuery<T>>(query, "query");
			if (ObjectQuery<T>.IsLinqQuery(this) || ObjectQuery<T>.IsLinqQuery(query))
			{
				return (ObjectQuery<T>)this.Intersect(query);
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Intersect(base.QueryState, query.QueryState));
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0004A4BC File Offset: 0x000486BC
		public ObjectQuery<TResultType> OfType<TResultType>()
		{
			if (ObjectQuery<T>.IsLinqQuery(this))
			{
				return (ObjectQuery<TResultType>)this.OfType<TResultType>();
			}
			base.QueryState.ObjectContext.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResultType), Assembly.GetCallingAssembly());
			Type typeFromHandle = typeof(TResultType);
			EdmType edmType = null;
			if (!base.QueryState.ObjectContext.MetadataWorkspace.GetItemCollection(DataSpace.OSpace).TryGetType(typeFromHandle.Name, typeFromHandle.Namespace ?? string.Empty, out edmType) || (!Helper.IsEntityType(edmType) && !Helper.IsComplexType(edmType)))
			{
				throw EntityUtil.EntitySqlError(Strings.ObjectQuery_QueryBuilder_InvalidResultType(typeof(TResultType).FullName));
			}
			return new ObjectQuery<TResultType>(EntitySqlQueryBuilder.OfType(base.QueryState, edmType, typeFromHandle));
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0004A580 File Offset: 0x00048780
		public ObjectQuery<T> OrderBy(string keys, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckArgumentNull<string>(keys, "keys");
			EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(keys))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidSortKeyList, "keys");
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.OrderBy(base.QueryState, this.Name, keys, parameters));
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0004A5D8 File Offset: 0x000487D8
		public ObjectQuery<DbDataRecord> Select(string projection, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckArgumentNull<string>(projection, "projection");
			EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(projection))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidProjectionList, "projection");
			}
			return new ObjectQuery<DbDataRecord>(EntitySqlQueryBuilder.Select(base.QueryState, this.Name, projection, parameters));
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0004A630 File Offset: 0x00048830
		public ObjectQuery<TResultType> SelectValue<TResultType>(string projection, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckArgumentNull<string>(projection, "projection");
			EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(projection))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidProjectionList, "projection");
			}
			base.QueryState.ObjectContext.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResultType), Assembly.GetCallingAssembly());
			return new ObjectQuery<TResultType>(EntitySqlQueryBuilder.SelectValue(base.QueryState, this.Name, projection, parameters, typeof(TResultType)));
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0004A6B4 File Offset: 0x000488B4
		public ObjectQuery<T> Skip(string keys, string count, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckArgumentNull<string>(keys, "keys");
			EntityUtil.CheckArgumentNull<string>(count, "count");
			EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(keys))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidSortKeyList, "keys");
			}
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(count))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidSkipCount, "count");
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Skip(base.QueryState, this.Name, keys, count, parameters));
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0004A72E File Offset: 0x0004892E
		public ObjectQuery<T> Top(string count, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckArgumentNull<string>(count, "count");
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(count))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidTopCount, "count");
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Top(base.QueryState, this.Name, count, parameters));
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0004A76C File Offset: 0x0004896C
		public ObjectQuery<T> Union(ObjectQuery<T> query)
		{
			EntityUtil.CheckArgumentNull<ObjectQuery<T>>(query, "query");
			if (ObjectQuery<T>.IsLinqQuery(this) || ObjectQuery<T>.IsLinqQuery(query))
			{
				return (ObjectQuery<T>)this.Union(query);
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Union(base.QueryState, query.QueryState));
		}

		// Token: 0x0600160E RID: 5646 RVA: 0x0004A7B8 File Offset: 0x000489B8
		public ObjectQuery<T> UnionAll(ObjectQuery<T> query)
		{
			EntityUtil.CheckArgumentNull<ObjectQuery<T>>(query, "query");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.UnionAll(base.QueryState, query.QueryState));
		}

		// Token: 0x0600160F RID: 5647 RVA: 0x0004A7DC File Offset: 0x000489DC
		public ObjectQuery<T> Where(string predicate, params ObjectParameter[] parameters)
		{
			EntityUtil.CheckArgumentNull<string>(predicate, "predicate");
			EntityUtil.CheckArgumentNull<ObjectParameter[]>(parameters, "parameters");
			if (StringUtil.IsNullOrEmptyOrWhiteSpace(predicate))
			{
				throw EntityUtil.Argument(Strings.ObjectQuery_QueryBuilder_InvalidFilterPredicate, "predicate");
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Where(base.QueryState, this.Name, predicate, parameters));
		}

		// Token: 0x06001610 RID: 5648 RVA: 0x0004A831 File Offset: 0x00048A31
		internal ObjectQuery(ObjectQueryState queryState) : base(queryState)
		{
		}

		// Token: 0x06001611 RID: 5649 RVA: 0x0004A845 File Offset: 0x00048A45
		public new ObjectResult<T> Execute(MergeOption mergeOption)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			return this.GetResults(new MergeOption?(mergeOption));
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0004A859 File Offset: 0x00048A59
		public ObjectQuery<T> Include(string path)
		{
			EntityUtil.CheckStringArgument(path, "path");
			return new ObjectQuery<T>(base.QueryState.Include<T>(this, path));
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0004A878 File Offset: 0x00048A78
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			ObjectResult<T> results = this.GetResults(null);
			IEnumerator<T> result;
			try
			{
				IEnumerator<T> enumerator = results.GetEnumerator();
				result = enumerator;
			}
			catch
			{
				results.Dispose();
				throw;
			}
			return result;
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0004A8BC File Offset: 0x00048ABC
		internal override IEnumerator GetEnumeratorInternal()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0004A8C4 File Offset: 0x00048AC4
		internal override IList GetIListSourceListInternal()
		{
			return ((IListSource)this.GetResults(null)).GetList();
		}

		// Token: 0x06001616 RID: 5654 RVA: 0x0004A8E5 File Offset: 0x00048AE5
		internal override ObjectResult ExecuteInternal(MergeOption mergeOption)
		{
			return this.GetResults(new MergeOption?(mergeOption));
		}

		// Token: 0x06001617 RID: 5655 RVA: 0x0004A8F4 File Offset: 0x00048AF4
		internal override Expression GetExpression()
		{
			Expression expression;
			if (!base.QueryState.TryGetExpression(out expression))
			{
				expression = Expression.Constant(this);
			}
			Type typeFromHandle = typeof(ObjectQuery<T>);
			if (base.QueryState.UserSpecifiedMergeOption != null)
			{
				MethodInfo method = typeFromHandle.GetMethod("MergeAs", BindingFlags.Instance | BindingFlags.NonPublic);
				expression = TypeSystem.EnsureType(expression, typeFromHandle);
				expression = Expression.Call(expression, method, new Expression[]
				{
					Expression.Constant(base.QueryState.UserSpecifiedMergeOption.Value)
				});
			}
			if (base.QueryState.Span != null)
			{
				MethodInfo method2 = typeFromHandle.GetMethod("IncludeSpan", BindingFlags.Instance | BindingFlags.NonPublic);
				expression = TypeSystem.EnsureType(expression, typeFromHandle);
				expression = Expression.Call(expression, method2, new Expression[]
				{
					Expression.Constant(base.QueryState.Span)
				});
			}
			return expression;
		}

		// Token: 0x06001618 RID: 5656 RVA: 0x0004A9C2 File Offset: 0x00048BC2
		internal ObjectQuery<T> MergeAs(MergeOption mergeOption)
		{
			throw EntityUtil.InvalidOperation(Strings.ELinq_MethodNotDirectlyCallable);
		}

		// Token: 0x06001619 RID: 5657 RVA: 0x0004A9C2 File Offset: 0x00048BC2
		internal ObjectQuery<T> IncludeSpan(Span span)
		{
			throw EntityUtil.InvalidOperation(Strings.ELinq_MethodNotDirectlyCallable);
		}

		// Token: 0x0600161A RID: 5658 RVA: 0x0004A9D0 File Offset: 0x00048BD0
		private ObjectResult<T> GetResults(MergeOption? forMergeOption)
		{
			base.QueryState.ObjectContext.EnsureConnection();
			ObjectResult<T> result;
			try
			{
				ObjectQueryExecutionPlan executionPlan = base.QueryState.GetExecutionPlan(forMergeOption);
				result = executionPlan.Execute<T>(base.QueryState.ObjectContext, base.QueryState.Parameters);
			}
			catch
			{
				base.QueryState.ObjectContext.ReleaseConnection();
				throw;
			}
			return result;
		}

		// Token: 0x04000A45 RID: 2629
		private const string DefaultName = "it";

		// Token: 0x04000A46 RID: 2630
		private string _name = "it";
	}
}
