using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.ELinq;
using System.Data.Objects.Internal;
using System.Linq;
using System.Linq.Expressions;

namespace System.Data.Objects
{
	// Token: 0x02000147 RID: 327
	public abstract class ObjectQuery : IEnumerable, IQueryable, IOrderedQueryable, IListSource
	{
		// Token: 0x060017A5 RID: 6053 RVA: 0x0004FA21 File Offset: 0x0004DC21
		internal ObjectQuery(ObjectQueryState queryState)
		{
			this._state = queryState;
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0004FA30 File Offset: 0x0004DC30
		internal ObjectQueryState QueryState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0004FA38 File Offset: 0x0004DC38
		Type IQueryable.ElementType
		{
			get
			{
				return this._state.ElementType;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0004FA45 File Offset: 0x0004DC45
		Expression IQueryable.Expression
		{
			get
			{
				return this.GetExpression();
			}
		}

		// Token: 0x060017A9 RID: 6057
		internal abstract Expression GetExpression();

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0004FA4D File Offset: 0x0004DC4D
		IQueryProvider IQueryable.Provider
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

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x000173E2 File Offset: 0x000155E2
		bool IListSource.ContainsListCollection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x060017AC RID: 6060 RVA: 0x0004FA6C File Offset: 0x0004DC6C
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

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0004FA8F File Offset: 0x0004DC8F
		public ObjectContext Context
		{
			get
			{
				return this._state.ObjectContext;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x060017AE RID: 6062 RVA: 0x0004FA9C File Offset: 0x0004DC9C
		// (set) Token: 0x060017AF RID: 6063 RVA: 0x0004FAA9 File Offset: 0x0004DCA9
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

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x060017B0 RID: 6064 RVA: 0x0004FAC2 File Offset: 0x0004DCC2
		public ObjectParameterCollection Parameters
		{
			get
			{
				return this._state.EnsureParameters();
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x060017B1 RID: 6065 RVA: 0x0004FACF File Offset: 0x0004DCCF
		// (set) Token: 0x060017B2 RID: 6066 RVA: 0x0004FADC File Offset: 0x0004DCDC
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

		// Token: 0x060017B3 RID: 6067 RVA: 0x0004FAEA File Offset: 0x0004DCEA
		IList IListSource.GetList()
		{
			return this.GetIListSourceListInternal();
		}

		// Token: 0x060017B4 RID: 6068 RVA: 0x0004FAF4 File Offset: 0x0004DCF4
		[Browsable(false)]
		public string ToTraceString()
		{
			return this._state.GetExecutionPlan(null).ToTraceString();
		}

		// Token: 0x060017B5 RID: 6069 RVA: 0x0004FB1C File Offset: 0x0004DD1C
		public TypeUsage GetResultType()
		{
			this.Context.EnsureMetadata();
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
					throw EntityUtil.InvalidOperation(Strings.ObjectQuery_UnableToMapResultType);
				}
				this._resultType = typeUsage;
			}
			return this._resultType;
		}

		// Token: 0x060017B6 RID: 6070 RVA: 0x0004FB8B File Offset: 0x0004DD8B
		public ObjectResult Execute(MergeOption mergeOption)
		{
			EntityUtil.CheckArgumentMergeOption(mergeOption);
			return this.ExecuteInternal(mergeOption);
		}

		// Token: 0x060017B7 RID: 6071 RVA: 0x0004FB9A File Offset: 0x0004DD9A
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumeratorInternal();
		}

		// Token: 0x060017B8 RID: 6072
		internal abstract IEnumerator GetEnumeratorInternal();

		// Token: 0x060017B9 RID: 6073
		internal abstract IList GetIListSourceListInternal();

		// Token: 0x060017BA RID: 6074
		internal abstract ObjectResult ExecuteInternal(MergeOption mergeOption);

		// Token: 0x04000A97 RID: 2711
		private ObjectQueryState _state;

		// Token: 0x04000A98 RID: 2712
		private TypeUsage _resultType;

		// Token: 0x04000A99 RID: 2713
		private IQueryProvider _provider;
	}
}
