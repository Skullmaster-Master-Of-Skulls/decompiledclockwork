using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000269 RID: 617
	internal sealed class ScopeRegion
	{
		// Token: 0x0600150A RID: 5386 RVA: 0x000631B3 File Offset: 0x000613B3
		internal ScopeRegion(ScopeManager scopeManager, int firstScopeIndex, int scopeRegionIndex)
		{
			this._scopeManager = scopeManager;
			this._firstScopeIndex = firstScopeIndex;
			this._scopeRegionIndex = scopeRegionIndex;
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x000631E6 File Offset: 0x000613E6
		internal int FirstScopeIndex
		{
			get
			{
				return this._firstScopeIndex;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x0600150C RID: 5388 RVA: 0x000631EE File Offset: 0x000613EE
		internal int ScopeRegionIndex
		{
			get
			{
				return this._scopeRegionIndex;
			}
		}

		// Token: 0x0600150D RID: 5389 RVA: 0x000631F6 File Offset: 0x000613F6
		internal bool ContainsScope(int scopeIndex)
		{
			return scopeIndex >= this._firstScopeIndex;
		}

		// Token: 0x0600150E RID: 5390 RVA: 0x00063204 File Offset: 0x00061404
		internal void EnterGroupOperation(DbExpressionBinding groupAggregateBinding)
		{
			this._groupAggregateBinding = groupAggregateBinding;
		}

		// Token: 0x0600150F RID: 5391 RVA: 0x0006320D File Offset: 0x0006140D
		internal void RollbackGroupOperation()
		{
			this._groupAggregateBinding = null;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00063216 File Offset: 0x00061416
		internal bool IsAggregating
		{
			get
			{
				return this._groupAggregateBinding != null;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x00063224 File Offset: 0x00061424
		internal DbExpressionBinding GroupAggregateBinding
		{
			get
			{
				return this._groupAggregateBinding;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x0006322C File Offset: 0x0006142C
		internal List<GroupAggregateInfo> GroupAggregateInfos
		{
			get
			{
				return this._groupAggregateInfos;
			}
		}

		// Token: 0x06001513 RID: 5395 RVA: 0x00063234 File Offset: 0x00061434
		internal void RegisterGroupAggregateName(string groupAggregateName)
		{
			this._groupAggregateNames.Add(groupAggregateName);
		}

		// Token: 0x06001514 RID: 5396 RVA: 0x00063243 File Offset: 0x00061443
		internal bool ContainsGroupAggregate(string groupAggregateName)
		{
			return this._groupAggregateNames.Contains(groupAggregateName);
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x00063251 File Offset: 0x00061451
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x00063259 File Offset: 0x00061459
		internal bool WasResolutionCorrelated { get; set; }

		// Token: 0x06001517 RID: 5399 RVA: 0x00063264 File Offset: 0x00061464
		internal void ApplyToScopeEntries(Action<ScopeEntry> action)
		{
			for (int i = this.FirstScopeIndex; i <= this._scopeManager.CurrentScopeIndex; i++)
			{
				foreach (KeyValuePair<string, ScopeEntry> keyValuePair in this._scopeManager.GetScopeByIndex(i))
				{
					action(keyValuePair.Value);
				}
			}
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00063304 File Offset: 0x00061504
		internal void ApplyToScopeEntries(Func<ScopeEntry, ScopeEntry> action)
		{
			for (int i = this.FirstScopeIndex; i <= this._scopeManager.CurrentScopeIndex; i++)
			{
				Scope scope = this._scopeManager.GetScopeByIndex(i);
				List<KeyValuePair<string, ScopeEntry>> list = null;
				foreach (KeyValuePair<string, ScopeEntry> keyValuePair in scope)
				{
					ScopeEntry scopeEntry = action(keyValuePair.Value);
					if (keyValuePair.Value != scopeEntry)
					{
						if (list == null)
						{
							list = new List<KeyValuePair<string, ScopeEntry>>();
						}
						list.Add(new KeyValuePair<string, ScopeEntry>(keyValuePair.Key, scopeEntry));
					}
				}
				if (list != null)
				{
					list.Each(delegate(KeyValuePair<string, ScopeEntry> updatedScopeEntry)
					{
						scope.Replace(updatedScopeEntry.Key, updatedScopeEntry.Value);
					});
				}
			}
		}

		// Token: 0x06001519 RID: 5401 RVA: 0x000633E4 File Offset: 0x000615E4
		internal void RollbackAllScopes()
		{
			this._scopeManager.RollbackToScope(this.FirstScopeIndex - 1);
		}

		// Token: 0x04000754 RID: 1876
		private readonly ScopeManager _scopeManager;

		// Token: 0x04000755 RID: 1877
		private readonly int _firstScopeIndex;

		// Token: 0x04000756 RID: 1878
		private readonly int _scopeRegionIndex;

		// Token: 0x04000757 RID: 1879
		private DbExpressionBinding _groupAggregateBinding;

		// Token: 0x04000758 RID: 1880
		private readonly List<GroupAggregateInfo> _groupAggregateInfos = new List<GroupAggregateInfo>();

		// Token: 0x04000759 RID: 1881
		private readonly HashSet<string> _groupAggregateNames = new HashSet<string>();
	}
}
