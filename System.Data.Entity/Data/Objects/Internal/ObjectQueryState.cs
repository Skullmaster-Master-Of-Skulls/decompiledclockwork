using System;
using System.Data.Metadata.Edm;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Objects.Internal
{
	// Token: 0x02000161 RID: 353
	internal abstract class ObjectQueryState
	{
		// Token: 0x06001A62 RID: 6754 RVA: 0x0005AA20 File Offset: 0x00058C20
		protected ObjectQueryState(Type elementType, ObjectContext context, ObjectParameterCollection parameters, Span span)
		{
			EntityUtil.CheckArgumentNull<Type>(elementType, "elementType");
			EntityUtil.CheckArgumentNull<ObjectContext>(context, "context");
			this._elementType = elementType;
			this._context = context;
			this._span = span;
			this._parameters = parameters;
		}

		// Token: 0x06001A63 RID: 6755 RVA: 0x0005AA6F File Offset: 0x00058C6F
		protected ObjectQueryState(Type elementType, ObjectQuery query) : this(elementType, query.Context, null, null)
		{
			this._cachingEnabled = query.EnablePlanCaching;
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x0005AA8C File Offset: 0x00058C8C
		internal Type ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0005AA94 File Offset: 0x00058C94
		internal ObjectContext ObjectContext
		{
			get
			{
				return this._context;
			}
		}

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x0005AA9C File Offset: 0x00058C9C
		internal ObjectParameterCollection Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0005AAA4 File Offset: 0x00058CA4
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

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x0005AADE File Offset: 0x00058CDE
		internal Span Span
		{
			get
			{
				return this._span;
			}
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0005AAE8 File Offset: 0x00058CE8
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

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x0005AB24 File Offset: 0x00058D24
		// (set) Token: 0x06001A6B RID: 6763 RVA: 0x0005AB2C File Offset: 0x00058D2C
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

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x0005AB35 File Offset: 0x00058D35
		// (set) Token: 0x06001A6D RID: 6765 RVA: 0x0005AB3D File Offset: 0x00058D3D
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

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x0005AB48 File Offset: 0x00058D48
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

		// Token: 0x06001A6F RID: 6767 RVA: 0x0005AB6C File Offset: 0x00058D6C
		internal void ApplySettingsTo(ObjectQueryState other)
		{
			other.PlanCachingEnabled = this.PlanCachingEnabled;
			other.UserSpecifiedMergeOption = this.UserSpecifiedMergeOption;
		}

		// Token: 0x06001A70 RID: 6768
		internal abstract bool TryGetCommandText(out string commandText);

		// Token: 0x06001A71 RID: 6769
		internal abstract bool TryGetExpression(out Expression expression);

		// Token: 0x06001A72 RID: 6770
		internal abstract ObjectQueryExecutionPlan GetExecutionPlan(MergeOption? forMergeOption);

		// Token: 0x06001A73 RID: 6771
		internal abstract ObjectQueryState Include<TElementType>(ObjectQuery<TElementType> sourceQuery, string includePath);

		// Token: 0x06001A74 RID: 6772
		protected abstract TypeUsage GetResultType();

		// Token: 0x06001A75 RID: 6773 RVA: 0x0005AB88 File Offset: 0x00058D88
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

		// Token: 0x06001A76 RID: 6774 RVA: 0x0005ABC4 File Offset: 0x00058DC4
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

		// Token: 0x06001A77 RID: 6775 RVA: 0x0005AC08 File Offset: 0x00058E08
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal ObjectQuery CreateQuery()
		{
			MethodInfo methodInfo = typeof(ObjectQueryState).GetMethod("CreateObjectQuery", BindingFlags.Static | BindingFlags.Public);
			methodInfo = methodInfo.MakeGenericMethod(new Type[]
			{
				this._elementType
			});
			return (ObjectQuery)methodInfo.Invoke(null, new object[]
			{
				this
			});
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x0005AC58 File Offset: 0x00058E58
		public static ObjectQuery<TResultType> CreateObjectQuery<TResultType>(ObjectQueryState queryState)
		{
			return new ObjectQuery<TResultType>(queryState);
		}

		// Token: 0x04000B17 RID: 2839
		internal static readonly MergeOption DefaultMergeOption;

		// Token: 0x04000B18 RID: 2840
		private readonly ObjectContext _context;

		// Token: 0x04000B19 RID: 2841
		private readonly Type _elementType;

		// Token: 0x04000B1A RID: 2842
		private ObjectParameterCollection _parameters;

		// Token: 0x04000B1B RID: 2843
		private Span _span;

		// Token: 0x04000B1C RID: 2844
		private MergeOption? _userMergeOption;

		// Token: 0x04000B1D RID: 2845
		private bool _cachingEnabled = true;

		// Token: 0x04000B1E RID: 2846
		protected ObjectQueryExecutionPlan _cachedPlan;
	}
}
