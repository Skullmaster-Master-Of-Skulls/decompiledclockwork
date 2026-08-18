using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.Internal;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Common.QueryCache
{
	// Token: 0x020002F5 RID: 757
	internal sealed class CompiledQueryCacheEntry : QueryCacheEntry
	{
		// Token: 0x06001AB4 RID: 6836 RVA: 0x0008561E File Offset: 0x0008381E
		internal CompiledQueryCacheEntry(QueryCacheKey queryCacheKey, MergeOption? mergeOption) : base(queryCacheKey, null)
		{
			this.PropagatedMergeOption = mergeOption;
			this._plans = new ConcurrentDictionary<string, ObjectQueryExecutionPlan>();
		}

		// Token: 0x06001AB5 RID: 6837 RVA: 0x0008563C File Offset: 0x0008383C
		internal ObjectQueryExecutionPlan GetExecutionPlan(MergeOption mergeOption, bool useCSharpNullComparisonBehavior)
		{
			string key = CompiledQueryCacheEntry.GenerateLocalCacheKey(mergeOption, useCSharpNullComparisonBehavior);
			ObjectQueryExecutionPlan result;
			this._plans.TryGetValue(key, out result);
			return result;
		}

		// Token: 0x06001AB6 RID: 6838 RVA: 0x00085664 File Offset: 0x00083864
		internal ObjectQueryExecutionPlan SetExecutionPlan(ObjectQueryExecutionPlan newPlan, bool useCSharpNullComparisonBehavior)
		{
			string key = CompiledQueryCacheEntry.GenerateLocalCacheKey(newPlan.MergeOption, useCSharpNullComparisonBehavior);
			return this._plans.GetOrAdd(key, newPlan);
		}

		// Token: 0x06001AB7 RID: 6839 RVA: 0x0008568C File Offset: 0x0008388C
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

		// Token: 0x06001AB8 RID: 6840 RVA: 0x000856EC File Offset: 0x000838EC
		internal override object GetTarget()
		{
			return this;
		}

		// Token: 0x06001AB9 RID: 6841 RVA: 0x000856F0 File Offset: 0x000838F0
		[SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly")]
		private static string GenerateLocalCacheKey(MergeOption mergeOption, bool useCSharpNullComparisonBehavior)
		{
			switch (mergeOption)
			{
			case MergeOption.AppendOnly:
			case MergeOption.OverwriteChanges:
			case MergeOption.PreserveChanges:
			case MergeOption.NoTracking:
				return string.Join("", new object[]
				{
					Enum.GetName(typeof(MergeOption), mergeOption),
					useCSharpNullComparisonBehavior
				});
			default:
				throw new ArgumentOutOfRangeException("newPlan.MergeOption");
			}
		}

		// Token: 0x04000942 RID: 2370
		public readonly MergeOption? PropagatedMergeOption;

		// Token: 0x04000943 RID: 2371
		private readonly ConcurrentDictionary<string, ObjectQueryExecutionPlan> _plans;
	}
}
