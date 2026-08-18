using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005A7 RID: 1447
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class ObjectQuery<T> : ObjectQuery, IOrderedQueryable<T>, IQueryable<T>, IOrderedQueryable, IQueryable, IEnumerable<!0>, IEnumerable, IDbAsyncEnumerable<T>, IDbAsyncEnumerable
	{
		// Token: 0x06003958 RID: 14680 RVA: 0x00110631 File Offset: 0x0010E831
		private static bool IsLinqQuery(ObjectQuery query)
		{
			return query.QueryState is ELinqQueryState;
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x00110641 File Offset: 0x0010E841
		public ObjectQuery(string commandText, ObjectContext context) : this(new EntitySqlQueryState(typeof(T), commandText, false, context, null, null))
		{
			context.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
		}

		// Token: 0x0600395A RID: 14682 RVA: 0x00110678 File Offset: 0x0010E878
		public ObjectQuery(string commandText, ObjectContext context, MergeOption mergeOption) : this(new EntitySqlQueryState(typeof(T), commandText, false, context, null, null))
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			base.QueryState.UserSpecifiedMergeOption = new MergeOption?(mergeOption);
			context.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
		}

		// Token: 0x0600395B RID: 14683 RVA: 0x001106D0 File Offset: 0x0010E8D0
		internal ObjectQuery(EntitySetBase entitySet, ObjectContext context, MergeOption mergeOption) : this(new EntitySqlQueryState(typeof(T), ObjectQuery<T>.BuildScanEntitySetEsql(entitySet), entitySet.Scan(), false, context, null, null, null))
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			base.QueryState.UserSpecifiedMergeOption = new MergeOption?(mergeOption);
			context.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(T), Assembly.GetCallingAssembly());
		}

		// Token: 0x0600395C RID: 14684 RVA: 0x00110734 File Offset: 0x0010E934
		private static string BuildScanEntitySetEsql(EntitySetBase entitySet)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
			{
				EntityUtil.QuoteIdentifier(entitySet.EntityContainer.Name),
				EntityUtil.QuoteIdentifier(entitySet.Name)
			});
		}

		// Token: 0x0600395D RID: 14685 RVA: 0x00110779 File Offset: 0x0010E979
		internal ObjectQuery(ObjectQueryState queryState)
		{
			this._name = "it";
			base..ctor(queryState);
		}

		// Token: 0x0600395E RID: 14686 RVA: 0x0011078D File Offset: 0x0010E98D
		internal ObjectQuery()
		{
			this._name = "it";
			base..ctor();
		}

		// Token: 0x170008BA RID: 2234
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x001107A0 File Offset: 0x0010E9A0
		// (set) Token: 0x06003960 RID: 14688 RVA: 0x001107A8 File Offset: 0x0010E9A8
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				Check.NotNull<string>(value, "value");
				if (!ObjectParameter.ValidateParameterName(value))
				{
					throw new ArgumentException(Strings.ObjectQuery_InvalidQueryName(value), "value");
				}
				this._name = value;
			}
		}

		// Token: 0x06003961 RID: 14689 RVA: 0x001107D6 File Offset: 0x0010E9D6
		public new ObjectResult<T> Execute(MergeOption mergeOption)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			return this.GetResults(new MergeOption?(mergeOption));
		}

		// Token: 0x06003962 RID: 14690 RVA: 0x001107EA File Offset: 0x0010E9EA
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public new Task<ObjectResult<T>> ExecuteAsync(MergeOption mergeOption)
		{
			return this.ExecuteAsync(mergeOption, CancellationToken.None);
		}

		// Token: 0x06003963 RID: 14691 RVA: 0x001107F8 File Offset: 0x0010E9F8
		[SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
		public new Task<ObjectResult<T>> ExecuteAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			return this.GetResultsAsync(new MergeOption?(mergeOption), cancellationToken);
		}

		// Token: 0x06003964 RID: 14692 RVA: 0x0011080D File Offset: 0x0010EA0D
		public ObjectQuery<T> Include(string path)
		{
			Check.NotEmpty(path, "path");
			return new ObjectQuery<T>(base.QueryState.Include<T>(this, path));
		}

		// Token: 0x06003965 RID: 14693 RVA: 0x0011082D File Offset: 0x0010EA2D
		public ObjectQuery<T> Distinct()
		{
			if (ObjectQuery<T>.IsLinqQuery(this))
			{
				return (ObjectQuery<T>)this.Distinct<T>();
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Distinct(base.QueryState));
		}

		// Token: 0x06003966 RID: 14694 RVA: 0x00110854 File Offset: 0x0010EA54
		public ObjectQuery<T> Except(ObjectQuery<T> query)
		{
			Check.NotNull<ObjectQuery<T>>(query, "query");
			if (ObjectQuery<T>.IsLinqQuery(this) || ObjectQuery<T>.IsLinqQuery(query))
			{
				return (ObjectQuery<T>)this.Except(query);
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Except(base.QueryState, query.QueryState));
		}

		// Token: 0x06003967 RID: 14695 RVA: 0x001108A0 File Offset: 0x0010EAA0
		public ObjectQuery<DbDataRecord> GroupBy(string keys, string projection, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(keys, "keys");
			Check.NotEmpty(projection, "projection");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<DbDataRecord>(EntitySqlQueryBuilder.GroupBy(base.QueryState, this.Name, keys, projection, parameters));
		}

		// Token: 0x06003968 RID: 14696 RVA: 0x001108E0 File Offset: 0x0010EAE0
		public ObjectQuery<T> Intersect(ObjectQuery<T> query)
		{
			Check.NotNull<ObjectQuery<T>>(query, "query");
			if (ObjectQuery<T>.IsLinqQuery(this) || ObjectQuery<T>.IsLinqQuery(query))
			{
				return (ObjectQuery<T>)this.Intersect(query);
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Intersect(base.QueryState, query.QueryState));
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x0011092C File Offset: 0x0010EB2C
		public ObjectQuery<TResultType> OfType<TResultType>()
		{
			if (ObjectQuery<T>.IsLinqQuery(this))
			{
				return (ObjectQuery<TResultType>)this.OfType<TResultType>();
			}
			base.QueryState.ObjectContext.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResultType), Assembly.GetCallingAssembly());
			Type typeFromHandle = typeof(TResultType);
			EdmType edmType;
			if (!base.QueryState.ObjectContext.MetadataWorkspace.GetItemCollection(DataSpace.OSpace).TryGetType(typeFromHandle.Name, typeFromHandle.NestingNamespace() ?? string.Empty, out edmType) || (!Helper.IsEntityType(edmType) && !Helper.IsComplexType(edmType)))
			{
				string message = Strings.ObjectQuery_QueryBuilder_InvalidResultType(typeof(TResultType).FullName);
				throw new EntitySqlException(message);
			}
			return new ObjectQuery<TResultType>(EntitySqlQueryBuilder.OfType(base.QueryState, edmType, typeFromHandle));
		}

		// Token: 0x0600396A RID: 14698 RVA: 0x001109EE File Offset: 0x0010EBEE
		public ObjectQuery<T> OrderBy(string keys, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(keys, "keys");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.OrderBy(base.QueryState, this.Name, keys, parameters));
		}

		// Token: 0x0600396B RID: 14699 RVA: 0x00110A20 File Offset: 0x0010EC20
		public ObjectQuery<DbDataRecord> Select(string projection, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(projection, "projection");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<DbDataRecord>(EntitySqlQueryBuilder.Select(base.QueryState, this.Name, projection, parameters));
		}

		// Token: 0x0600396C RID: 14700 RVA: 0x00110A54 File Offset: 0x0010EC54
		public ObjectQuery<TResultType> SelectValue<TResultType>(string projection, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(projection, "projection");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			base.QueryState.ObjectContext.MetadataWorkspace.ImplicitLoadAssemblyForType(typeof(TResultType), Assembly.GetCallingAssembly());
			return new ObjectQuery<TResultType>(EntitySqlQueryBuilder.SelectValue(base.QueryState, this.Name, projection, parameters, typeof(TResultType)));
		}

		// Token: 0x0600396D RID: 14701 RVA: 0x00110ABF File Offset: 0x0010ECBF
		public ObjectQuery<T> Skip(string keys, string count, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(keys, "keys");
			Check.NotEmpty(count, "count");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Skip(base.QueryState, this.Name, keys, count, parameters));
		}

		// Token: 0x0600396E RID: 14702 RVA: 0x00110AFE File Offset: 0x0010ECFE
		public ObjectQuery<T> Top(string count, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(count, "count");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Top(base.QueryState, this.Name, count, parameters));
		}

		// Token: 0x0600396F RID: 14703 RVA: 0x00110B24 File Offset: 0x0010ED24
		public ObjectQuery<T> Union(ObjectQuery<T> query)
		{
			Check.NotNull<ObjectQuery<T>>(query, "query");
			if (ObjectQuery<T>.IsLinqQuery(this) || ObjectQuery<T>.IsLinqQuery(query))
			{
				return (ObjectQuery<T>)this.Union(query);
			}
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Union(base.QueryState, query.QueryState));
		}

		// Token: 0x06003970 RID: 14704 RVA: 0x00110B70 File Offset: 0x0010ED70
		public ObjectQuery<T> UnionAll(ObjectQuery<T> query)
		{
			Check.NotNull<ObjectQuery<T>>(query, "query");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.UnionAll(base.QueryState, query.QueryState));
		}

		// Token: 0x06003971 RID: 14705 RVA: 0x00110B94 File Offset: 0x0010ED94
		public ObjectQuery<T> Where(string predicate, params ObjectParameter[] parameters)
		{
			Check.NotEmpty(predicate, "predicate");
			Check.NotNull<ObjectParameter[]>(parameters, "parameters");
			return new ObjectQuery<T>(EntitySqlQueryBuilder.Where(base.QueryState, this.Name, predicate, parameters));
		}

		// Token: 0x06003972 RID: 14706 RVA: 0x00110BE4 File Offset: 0x0010EDE4
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IEnumerator<T> IEnumerable<!0>.GetEnumerator()
		{
			base.QueryState.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return new LazyEnumerator<T>(() => this.GetResults(null));
		}

		// Token: 0x06003973 RID: 14707 RVA: 0x00110C29 File Offset: 0x0010EE29
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator<T> IDbAsyncEnumerable<!0>.GetAsyncEnumerator()
		{
			base.QueryState.ObjectContext.AsyncMonitor.EnsureNotEntered();
			return new LazyAsyncEnumerator<T>((CancellationToken cancellationToken) => this.GetResultsAsync(null, cancellationToken));
		}

		// Token: 0x06003974 RID: 14708 RVA: 0x00110C51 File Offset: 0x0010EE51
		internal override IEnumerator GetEnumeratorInternal()
		{
			return ((IEnumerable<T>)this).GetEnumerator();
		}

		// Token: 0x06003975 RID: 14709 RVA: 0x00110C59 File Offset: 0x0010EE59
		internal override IDbAsyncEnumerator GetAsyncEnumeratorInternal()
		{
			return ((IDbAsyncEnumerable<T>)this).GetAsyncEnumerator();
		}

		// Token: 0x06003976 RID: 14710 RVA: 0x00110C64 File Offset: 0x0010EE64
		internal override IList GetIListSourceListInternal()
		{
			return ((IListSource)this.GetResults(null)).GetList();
		}

		// Token: 0x06003977 RID: 14711 RVA: 0x00110C85 File Offset: 0x0010EE85
		internal override ObjectResult ExecuteInternal(MergeOption mergeOption)
		{
			return this.GetResults(new MergeOption?(mergeOption));
		}

		// Token: 0x06003978 RID: 14712 RVA: 0x00110D80 File Offset: 0x0010EF80
		internal override async Task<ObjectResult> ExecuteInternalAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			return await this.GetResultsAsync(new MergeOption?(mergeOption), cancellationToken).WithCurrentCulture<ObjectResult<T>>();
		}

		// Token: 0x06003979 RID: 14713 RVA: 0x00110DD8 File Offset: 0x0010EFD8
		internal override Expression GetExpression()
		{
			Expression expression;
			if (!base.QueryState.TryGetExpression(out expression))
			{
				expression = Expression.Constant(this);
			}
			if (base.QueryState.UserSpecifiedMergeOption != null)
			{
				expression = TypeSystem.EnsureType(expression, typeof(ObjectQuery<T>));
				expression = Expression.Call(expression, ObjectQuery<T>.MergeAsMethod, new Expression[]
				{
					Expression.Constant(base.QueryState.UserSpecifiedMergeOption.Value)
				});
			}
			if (base.QueryState.Span != null)
			{
				expression = TypeSystem.EnsureType(expression, typeof(ObjectQuery<T>));
				expression = Expression.Call(expression, ObjectQuery<T>.IncludeSpanMethod, new Expression[]
				{
					Expression.Constant(base.QueryState.Span)
				});
			}
			return expression;
		}

		// Token: 0x0600397A RID: 14714 RVA: 0x00110E9E File Offset: 0x0010F09E
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "mergeOption")]
		internal ObjectQuery<T> MergeAs(MergeOption mergeOption)
		{
			throw new InvalidOperationException(Strings.ELinq_MethodNotDirectlyCallable);
		}

		// Token: 0x0600397B RID: 14715 RVA: 0x00110EAA File Offset: 0x0010F0AA
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "span")]
		[SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic")]
		internal ObjectQuery<T> IncludeSpan(Span span)
		{
			throw new InvalidOperationException(Strings.ELinq_MethodNotDirectlyCallable);
		}

		// Token: 0x0600397C RID: 14716 RVA: 0x00110F38 File Offset: 0x0010F138
		private ObjectResult<T> GetResults(MergeOption? forMergeOption)
		{
			base.QueryState.ObjectContext.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy executionStrategy = base.ExecutionStrategy ?? DbProviderServices.GetExecutionStrategy(base.QueryState.ObjectContext.Connection, base.QueryState.ObjectContext.MetadataWorkspace);
			if (executionStrategy.RetriesOnFailure && base.QueryState.EffectiveStreamingBehavior)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(executionStrategy.GetType().Name));
			}
			return executionStrategy.Execute<ObjectResult<T>>(() => this.QueryState.ObjectContext.ExecuteInTransaction<ObjectResult<T>>(() => this.QueryState.GetExecutionPlan(forMergeOption).Execute<T>(this.QueryState.ObjectContext, this.QueryState.Parameters), executionStrategy, false, !this.QueryState.EffectiveStreamingBehavior));
		}

		// Token: 0x0600397D RID: 14717 RVA: 0x00110FF0 File Offset: 0x0010F1F0
		private Task<ObjectResult<T>> GetResultsAsync(MergeOption? forMergeOption, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			base.QueryState.ObjectContext.AsyncMonitor.EnsureNotEntered();
			IDbExecutionStrategy dbExecutionStrategy = base.ExecutionStrategy ?? DbProviderServices.GetExecutionStrategy(base.QueryState.ObjectContext.Connection, base.QueryState.ObjectContext.MetadataWorkspace);
			if (dbExecutionStrategy.RetriesOnFailure && base.QueryState.EffectiveStreamingBehavior)
			{
				throw new InvalidOperationException(Strings.ExecutionStrategy_StreamingNotSupported(dbExecutionStrategy.GetType().Name));
			}
			return this.GetResultsAsync(forMergeOption, dbExecutionStrategy, cancellationToken);
		}

		// Token: 0x0600397E RID: 14718 RVA: 0x0011134C File Offset: 0x0010F54C
		private async Task<ObjectResult<T>> GetResultsAsync(MergeOption? forMergeOption, IDbExecutionStrategy executionStrategy, CancellationToken cancellationToken)
		{
			MergeOption mergeOption = (forMergeOption != null) ? forMergeOption.Value : base.QueryState.EffectiveMergeOption;
			if (mergeOption != MergeOption.NoTracking)
			{
				base.QueryState.ObjectContext.AsyncMonitor.Enter();
			}
			ObjectResult<T> result;
			try
			{
				result = await executionStrategy.ExecuteAsync<ObjectResult<T>>(() => this.QueryState.ObjectContext.ExecuteInTransactionAsync<ObjectResult<T>>(() => this.QueryState.GetExecutionPlan(forMergeOption).ExecuteAsync<T>(this.QueryState.ObjectContext, this.QueryState.Parameters, cancellationToken), executionStrategy, false, !this.QueryState.EffectiveStreamingBehavior, cancellationToken), cancellationToken).WithCurrentCulture<ObjectResult<T>>();
			}
			finally
			{
				if (mergeOption != MergeOption.NoTracking)
				{
					base.QueryState.ObjectContext.AsyncMonitor.Exit();
				}
			}
			return result;
		}

		// Token: 0x040015E4 RID: 5604
		private const string DefaultName = "it";

		// Token: 0x040015E5 RID: 5605
		internal static readonly MethodInfo MergeAsMethod = typeof(ObjectQuery<T>).GetOnlyDeclaredMethod("MergeAs");

		// Token: 0x040015E6 RID: 5606
		internal static readonly MethodInfo IncludeSpanMethod = typeof(ObjectQuery<T>).GetOnlyDeclaredMethod("IncludeSpan");

		// Token: 0x040015E7 RID: 5607
		private string _name;
	}
}
