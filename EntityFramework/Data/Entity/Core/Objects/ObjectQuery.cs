using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005A6 RID: 1446
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public abstract class ObjectQuery : IOrderedQueryable, IQueryable, IEnumerable, IListSource, IDbAsyncEnumerable
	{
		// Token: 0x06003937 RID: 14647 RVA: 0x00110441 File Offset: 0x0010E641
		internal ObjectQuery(ObjectQueryState queryState)
		{
			this._state = queryState;
		}

		// Token: 0x06003938 RID: 14648 RVA: 0x00110450 File Offset: 0x0010E650
		internal ObjectQuery()
		{
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06003939 RID: 14649 RVA: 0x00110458 File Offset: 0x0010E658
		internal ObjectQueryState QueryState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x0600393A RID: 14650 RVA: 0x00110460 File Offset: 0x0010E660
		internal virtual ObjectQueryProvider ObjectQueryProvider
		{
			get
			{
				if (this._provider == null)
				{
					this._provider = new ObjectQueryProvider(this);
				}
				return this._provider;
			}
		}

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x0600393B RID: 14651 RVA: 0x0011047C File Offset: 0x0010E67C
		// (set) Token: 0x0600393C RID: 14652 RVA: 0x00110489 File Offset: 0x0010E689
		internal IDbExecutionStrategy ExecutionStrategy
		{
			get
			{
				return this.QueryState.ExecutionStrategy;
			}
			set
			{
				this.QueryState.ExecutionStrategy = value;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x0600393D RID: 14653 RVA: 0x00110497 File Offset: 0x0010E697
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x0600393E RID: 14654 RVA: 0x0011049C File Offset: 0x0010E69C
		public string CommandText
		{
			get
			{
				string result;
				if (!this._state.TryGetCommandText(out result))
				{
					return string.Empty;
				}
				return result;
			}
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x0600393F RID: 14655 RVA: 0x001104BF File Offset: 0x0010E6BF
		public ObjectContext Context
		{
			get
			{
				return this._state.ObjectContext;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06003940 RID: 14656 RVA: 0x001104CC File Offset: 0x0010E6CC
		// (set) Token: 0x06003941 RID: 14657 RVA: 0x001104D9 File Offset: 0x0010E6D9
		public MergeOption MergeOption
		{
			get
			{
				return this._state.EffectiveMergeOption;
			}
			set
			{
				EntityUtil.CheckArgumentMergeOption(value);
				this._state.UserSpecifiedMergeOption = new MergeOption?(value);
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06003942 RID: 14658 RVA: 0x001104F2 File Offset: 0x0010E6F2
		// (set) Token: 0x06003943 RID: 14659 RVA: 0x001104FF File Offset: 0x0010E6FF
		public bool Streaming
		{
			get
			{
				return this._state.EffectiveStreamingBehavior;
			}
			set
			{
				this._state.UserSpecifiedStreamingBehavior = new bool?(value);
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x06003944 RID: 14660 RVA: 0x00110512 File Offset: 0x0010E712
		public ObjectParameterCollection Parameters
		{
			get
			{
				return this._state.EnsureParameters();
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x06003945 RID: 14661 RVA: 0x0011051F File Offset: 0x0010E71F
		// (set) Token: 0x06003946 RID: 14662 RVA: 0x0011052C File Offset: 0x0010E72C
		public bool EnablePlanCaching
		{
			get
			{
				return this._state.PlanCachingEnabled;
			}
			set
			{
				this._state.PlanCachingEnabled = value;
			}
		}

		// Token: 0x06003947 RID: 14663 RVA: 0x0011053C File Offset: 0x0010E73C
		[Browsable(false)]
		public string ToTraceString()
		{
			return this._state.GetExecutionPlan(null).ToTraceString();
		}

		// Token: 0x06003948 RID: 14664 RVA: 0x00110564 File Offset: 0x0010E764
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public TypeUsage GetResultType()
		{
			if (this._resultType == null)
			{
				TypeUsage resultType = this._state.ResultType;
				TypeUsage typeUsage;
				if (!TypeHelpers.TryGetCollectionElementType(resultType, out typeUsage))
				{
					typeUsage = resultType;
				}
				typeUsage = this._state.ObjectContext.Perspective.MetadataWorkspace.GetOSpaceTypeUsage(typeUsage);
				if (typeUsage == null)
				{
					throw new InvalidOperationException(Strings.ObjectQuery_UnableToMapResultType);
				}
				this._resultType = typeUsage;
			}
			return this._resultType;
		}

		// Token: 0x06003949 RID: 14665 RVA: 0x001105C8 File Offset: 0x0010E7C8
		public ObjectResult Execute(MergeOption mergeOption)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			return this.ExecuteInternal(mergeOption);
		}

		// Token: 0x0600394A RID: 14666 RVA: 0x001105D7 File Offset: 0x0010E7D7
		public Task<ObjectResult> ExecuteAsync(MergeOption mergeOption)
		{
			return this.ExecuteAsync(mergeOption, CancellationToken.None);
		}

		// Token: 0x0600394B RID: 14667 RVA: 0x001105E5 File Offset: 0x0010E7E5
		public Task<ObjectResult> ExecuteAsync(MergeOption mergeOption, CancellationToken cancellationToken)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			cancellationToken.ThrowIfCancellationRequested();
			return this.ExecuteInternalAsync(mergeOption, cancellationToken);
		}

		// Token: 0x0600394C RID: 14668 RVA: 0x001105FC File Offset: 0x0010E7FC
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IList IListSource.GetList()
		{
			return this.GetIListSourceListInternal();
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x0600394D RID: 14669 RVA: 0x00110604 File Offset: 0x0010E804
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		Type IQueryable.ElementType
		{
			get
			{
				return this._state.ElementType;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x0600394E RID: 14670 RVA: 0x00110611 File Offset: 0x0010E811
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		Expression IQueryable.Expression
		{
			get
			{
				return this.GetExpression();
			}
		}

		// Token: 0x170008B9 RID: 2233
		// (get) Token: 0x0600394F RID: 14671 RVA: 0x00110619 File Offset: 0x0010E819
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IQueryProvider IQueryable.Provider
		{
			get
			{
				return this.ObjectQueryProvider;
			}
		}

		// Token: 0x06003950 RID: 14672 RVA: 0x00110621 File Offset: 0x0010E821
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorInternal();
		}

		// Token: 0x06003951 RID: 14673 RVA: 0x00110629 File Offset: 0x0010E829
		[SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
		IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
		{
			return this.GetAsyncEnumeratorInternal();
		}

		// Token: 0x06003952 RID: 14674
		internal abstract Expression GetExpression();

		// Token: 0x06003953 RID: 14675
		internal abstract IEnumerator GetEnumeratorInternal();

		// Token: 0x06003954 RID: 14676
		internal abstract IDbAsyncEnumerator GetAsyncEnumeratorInternal();

		// Token: 0x06003955 RID: 14677
		internal abstract Task<ObjectResult> ExecuteInternalAsync(MergeOption mergeOption, CancellationToken cancellationToken);

		// Token: 0x06003956 RID: 14678
		internal abstract IList GetIListSourceListInternal();

		// Token: 0x06003957 RID: 14679
		internal abstract ObjectResult ExecuteInternal(MergeOption mergeOption);

		// Token: 0x040015E1 RID: 5601
		private readonly ObjectQueryState _state;

		// Token: 0x040015E2 RID: 5602
		private TypeUsage _resultType;

		// Token: 0x040015E3 RID: 5603
		private ObjectQueryProvider _provider;
	}
}
