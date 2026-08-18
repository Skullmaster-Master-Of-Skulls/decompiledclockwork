using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Utilities;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x0200054F RID: 1359
	internal abstract class ObjectQueryState
	{
		// Token: 0x060034B1 RID: 13489 RVA: 0x000F9156 File Offset: 0x000F7356
		protected ObjectQueryState(Type elementType, ObjectContext context, ObjectParameterCollection parameters, Span span)
		{
			this._elementType = elementType;
			this._context = context;
			this._span = span;
			this._parameters = parameters;
		}

		// Token: 0x060034B2 RID: 13490 RVA: 0x000F9182 File Offset: 0x000F7382
		protected ObjectQueryState(Type elementType, ObjectQuery query) : this(elementType, query.Context, null, null)
		{
			this._cachingEnabled = query.EnablePlanCaching;
			this.UserSpecifiedStreamingBehavior = query.QueryState.UserSpecifiedStreamingBehavior;
			this.ExecutionStrategy = query.QueryState.ExecutionStrategy;
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060034B3 RID: 13491 RVA: 0x000F91C4 File Offset: 0x000F73C4
		internal bool EffectiveStreamingBehavior
		{
			get
			{
				bool? userSpecifiedStreamingBehavior = this.UserSpecifiedStreamingBehavior;
				if (userSpecifiedStreamingBehavior == null)
				{
					return this.DefaultStreamingBehavior;
				}
				return userSpecifiedStreamingBehavior.GetValueOrDefault();
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060034B4 RID: 13492 RVA: 0x000F91EF File Offset: 0x000F73EF
		// (set) Token: 0x060034B5 RID: 13493 RVA: 0x000F91F7 File Offset: 0x000F73F7
		internal bool? UserSpecifiedStreamingBehavior { get; set; }

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060034B6 RID: 13494 RVA: 0x000F9200 File Offset: 0x000F7400
		internal bool DefaultStreamingBehavior
		{
			get
			{
				IDbExecutionStrategy dbExecutionStrategy = this.ExecutionStrategy ?? DbProviderServices.GetExecutionStrategy(this.ObjectContext.Connection, this.ObjectContext.MetadataWorkspace);
				return !dbExecutionStrategy.RetriesOnFailure;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060034B7 RID: 13495 RVA: 0x000F923C File Offset: 0x000F743C
		// (set) Token: 0x060034B8 RID: 13496 RVA: 0x000F9244 File Offset: 0x000F7444
		internal IDbExecutionStrategy ExecutionStrategy { get; set; }

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060034B9 RID: 13497 RVA: 0x000F924D File Offset: 0x000F744D
		internal Type ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060034BA RID: 13498 RVA: 0x000F9255 File Offset: 0x000F7455
		internal ObjectContext ObjectContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000F925D File Offset: 0x000F745D
		internal ObjectParameterCollection Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x060034BC RID: 13500 RVA: 0x000F9265 File Offset: 0x000F7465
		internal ObjectParameterCollection EnsureParameters()
		{
			if (this._parameters == null)
			{
				this._parameters = new ObjectParameterCollection(this.ObjectContext.Perspective);
				if (this._cachedPlan != null)
				{
					this._parameters.SetReadOnly(true);
				}
			}
			return this._parameters;
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000F929F File Offset: 0x000F749F
		internal Span Span
		{
			get
			{
				return this._span;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x060034BE RID: 13502 RVA: 0x000F92A8 File Offset: 0x000F74A8
		internal MergeOption EffectiveMergeOption
		{
			get
			{
				if (this._userMergeOption != null)
				{
					return this._userMergeOption.Value;
				}
				ObjectQueryExecutionPlan cachedPlan = this._cachedPlan;
				if (cachedPlan != null)
				{
					return cachedPlan.MergeOption;
				}
				return ObjectQueryState.DefaultMergeOption;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x000F92E4 File Offset: 0x000F74E4
		// (set) Token: 0x060034C0 RID: 13504 RVA: 0x000F92EC File Offset: 0x000F74EC
		internal MergeOption? UserSpecifiedMergeOption
		{
			get
			{
				return this._userMergeOption;
			}
			set
			{
				this._userMergeOption = value;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000F92F5 File Offset: 0x000F74F5
		// (set) Token: 0x060034C2 RID: 13506 RVA: 0x000F92FD File Offset: 0x000F74FD
		internal bool PlanCachingEnabled
		{
			get
			{
				return this._cachingEnabled;
			}
			set
			{
				this._cachingEnabled = value;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x000F9308 File Offset: 0x000F7508
		internal TypeUsage ResultType
		{
			get
			{
				ObjectQueryExecutionPlan cachedPlan = this._cachedPlan;
				if (cachedPlan != null)
				{
					return cachedPlan.ResultType;
				}
				return this.GetResultType();
			}
		}

		// Token: 0x060034C4 RID: 13508 RVA: 0x000F932C File Offset: 0x000F752C
		internal void ApplySettingsTo(ObjectQueryState other)
		{
			other.PlanCachingEnabled = this.PlanCachingEnabled;
			other.UserSpecifiedMergeOption = this.UserSpecifiedMergeOption;
		}

		// Token: 0x060034C5 RID: 13509
		internal abstract bool TryGetCommandText(out string commandText);

		// Token: 0x060034C6 RID: 13510
		internal abstract bool TryGetExpression(out Expression expression);

		// Token: 0x060034C7 RID: 13511
		internal abstract ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption);

		// Token: 0x060034C8 RID: 13512
		internal abstract ObjectQueryState Include<TElementType>(ObjectQuery<TElementType> sourceQuery, string includePath);

		// Token: 0x060034C9 RID: 13513
		protected abstract TypeUsage GetResultType();

		// Token: 0x060034CA RID: 13514 RVA: 0x000F9348 File Offset: 0x000F7548
		protected static MergeOption EnsureMergeOption(params MergeOption?[] preferredMergeOptions)
		{
			foreach (MergeOption mergeOption in preferredMergeOptions)
			{
				if (mergeOption != null)
				{
					return mergeOption.Value;
				}
			}
			return ObjectQueryState.DefaultMergeOption;
		}

		// Token: 0x060034CB RID: 13515 RVA: 0x000F938C File Offset: 0x000F758C
		protected static MergeOption? GetMergeOption(params MergeOption?[] preferredMergeOptions)
		{
			foreach (MergeOption mergeOption in preferredMergeOptions)
			{
				if (mergeOption != null)
				{
					return new MergeOption?(mergeOption.Value);
				}
			}
			return null;
		}

		// Token: 0x060034CC RID: 13516 RVA: 0x000F93DC File Offset: 0x000F75DC
		public ObjectQuery CreateQuery()
		{
			MethodInfo methodInfo = ObjectQueryState.CreateObjectQueryMethod.MakeGenericMethod(new Type[]
			{
				this._elementType
			});
			return (ObjectQuery)methodInfo.Invoke(this, new object[0]);
		}

		// Token: 0x060034CD RID: 13517 RVA: 0x000F9417 File Offset: 0x000F7617
		public ObjectQuery<TResultType> CreateObjectQuery<TResultType>()
		{
			return new ObjectQuery<TResultType>(this);
		}

		// Token: 0x040013B8 RID: 5048
		internal static readonly MergeOption DefaultMergeOption = MergeOption.AppendOnly;

		// Token: 0x040013B9 RID: 5049
		internal static readonly MethodInfo CreateObjectQueryMethod = typeof(ObjectQueryState).GetOnlyDeclaredMethod("CreateObjectQuery");

		// Token: 0x040013BA RID: 5050
		private readonly ObjectContext _context;

		// Token: 0x040013BB RID: 5051
		private readonly Type _elementType;

		// Token: 0x040013BC RID: 5052
		private ObjectParameterCollection _parameters;

		// Token: 0x040013BD RID: 5053
		private readonly Span _span;

		// Token: 0x040013BE RID: 5054
		private MergeOption? _userMergeOption;

		// Token: 0x040013BF RID: 5055
		private bool _cachingEnabled = true;

		// Token: 0x040013C0 RID: 5056
		protected ObjectQueryExecutionPlan _cachedPlan;
	}
}
