using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.Internal;

namespace System.Data.Common.QueryCache
{
	// Token: 0x020003DA RID: 986
	internal sealed class CompiledQueryCacheEntry : QueryCacheEntry
	{
		// Token: 0x06003520 RID: 13600 RVA: 0x000CEE82 File Offset: 0x000CD082
		internal CompiledQueryCacheEntry(QueryCacheKey queryCacheKey, MergeOption? mergeOption) : base(queryCacheKey, null)
		{
			this.PropagatedMergeOption = mergeOption;
			this._plans = new ConcurrentDictionary<string, ObjectQueryExecutionPlan>();
		}

		// Token: 0x06003521 RID: 13601 RVA: 0x000CEEA0 File Offset: 0x000CD0A0
		internal ObjectQueryExecutionPlan GetExecutionPlan(MergeOption mergeOption, bool useCSharpNullComparisonBehavior)
		{
			string key = this.GenerateLocalCacheKey(mergeOption, useCSharpNullComparisonBehavior);
			ObjectQueryExecutionPlan result;
			this._plans.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06003522 RID: 13602 RVA: 0x000CEEC8 File Offset: 0x000CD0C8
		internal ObjectQueryExecutionPlan SetExecutionPlan(ObjectQueryExecutionPlan newPlan, bool useCSharpNullComparisonBehavior)
		{
			string key = this.GenerateLocalCacheKey(newPlan.MergeOption, useCSharpNullComparisonBehavior);
			return this._plans.GetOrAdd(key, newPlan);
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000CEEF0 File Offset: 0x000CD0F0
		internal bool TryGetResultType(out TypeUsage resultType)
		{
			using (IEnumerator<ObjectQueryExecutionPlan> enumerator = this._plans.Values.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					ObjectQueryExecutionPlan objectQueryExecutionPlan = enumerator.Current;
					resultType = objectQueryExecutionPlan.ResultType;
					return true;
				}
			}
			resultType = null;
			return false;
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x00048AC0 File Offset: 0x00046CC0
		internal override object GetTarget()
		{
			return this;
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000CEF50 File Offset: 0x000CD150
		private string GenerateLocalCacheKey(MergeOption mergeOption, bool useCSharpNullComparisonBehavior)
		{
			if (mergeOption <= MergeOption.NoTracking)
			{
				return string.Join("", new object[]
				{
					Enum.GetName(typeof(MergeOption), mergeOption),
					useCSharpNullComparisonBehavior
				});
			}
			throw EntityUtil.ArgumentOutOfRange("newPlan.MergeOption");
		}

		// Token: 0x0400177B RID: 6011
		public readonly MergeOption? PropagatedMergeOption;

		// Token: 0x0400177C RID: 6012
		private ConcurrentDictionary<string, ObjectQueryExecutionPlan> _plans;
	}
}
