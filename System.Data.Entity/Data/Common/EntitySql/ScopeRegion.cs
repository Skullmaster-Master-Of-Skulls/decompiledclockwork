using System;
using System.Collections.Generic;
using System.Data.Common.CommandTrees;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000341 RID: 833
	internal sealed class ScopeRegion
	{
		// Token: 0x0600314B RID: 12619 RVA: 0x000C26F9 File Offset: 0x000C08F9
		internal ScopeRegion(ScopeManager scopeManager, int firstScopeIndex, int scopeRegionIndex)
		{
			this._scopeManager = scopeManager;
			this._firstScopeIndex = firstScopeIndex;
			this._scopeRegionIndex = scopeRegionIndex;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000C272C File Offset: 0x000C092C
		internal int FirstScopeIndex
		{
			get
			{
				return this._firstScopeIndex;
			}
		}

		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x000C2734 File Offset: 0x000C0934
		internal int ScopeRegionIndex
		{
			get
			{
				return this._scopeRegionIndex;
			}
		}

		// Token: 0x0600314E RID: 12622 RVA: 0x000C273C File Offset: 0x000C093C
		internal bool ContainsScope(int scopeIndex)
		{
			return scopeIndex >= this._firstScopeIndex;
		}

		// Token: 0x0600314F RID: 12623 RVA: 0x000C274A File Offset: 0x000C094A
		internal void EnterGroupOperation(DbExpressionBinding groupAggregateBinding)
		{
			this._groupAggregateBinding = groupAggregateBinding;
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000C2753 File Offset: 0x000C0953
		internal void RollbackGroupOperation()
		{
			this._groupAggregateBinding = null;
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000C275C File Offset: 0x000C095C
		internal bool IsAggregating
		{
			get
			{
				return this._groupAggregateBinding != null;
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x000C2767 File Offset: 0x000C0967
		internal DbExpressionBinding GroupAggregateBinding
		{
			get
			{
				return this._groupAggregateBinding;
			}
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000C276F File Offset: 0x000C096F
		internal List<GroupAggregateInfo> GroupAggregateInfos
		{
			get
			{
				return this._groupAggregateInfos;
			}
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000C2777 File Offset: 0x000C0977
		internal void RegisterGroupAggregateName(string groupAggregateName)
		{
			this._groupAggregateNames.Add(groupAggregateName);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000C2786 File Offset: 0x000C0986
		internal bool ContainsGroupAggregate(string groupAggregateName)
		{
			return this._groupAggregateNames.Contains(groupAggregateName);
		}

		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000C2794 File Offset: 0x000C0994
		// (set) Token: 0x06003157 RID: 12631 RVA: 0x000C279C File Offset: 0x000C099C
		internal bool WasResolutionCorrelated
		{
			get
			{
				return this._wasResolutionCorrelated;
			}
			set
			{
				this._wasResolutionCorrelated = value;
			}
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000C27A8 File Offset: 0x000C09A8
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

		// Token: 0x06003159 RID: 12633 RVA: 0x000C2824 File Offset: 0x000C0A24
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
					list.ForEach(delegate(KeyValuePair<string, ScopeEntry> updatedScopeEntry)
					{
						scope.Replace(updatedScopeEntry.Key, updatedScopeEntry.Value);
					});
				}
			}
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000C28F8 File Offset: 0x000C0AF8
		internal void RollbackAllScopes()
		{
			this._scopeManager.RollbackToScope(this.FirstScopeIndex - 1);
		}

		// Token: 0x04001569 RID: 5481
		private readonly ScopeManager _scopeManager;

		// Token: 0x0400156A RID: 5482
		private readonly int _firstScopeIndex;

		// Token: 0x0400156B RID: 5483
		private readonly int _scopeRegionIndex;

		// Token: 0x0400156C RID: 5484
		private DbExpressionBinding _groupAggregateBinding;

		// Token: 0x0400156D RID: 5485
		private List<GroupAggregateInfo> _groupAggregateInfos = new List<GroupAggregateInfo>();

		// Token: 0x0400156E RID: 5486
		private HashSet<string> _groupAggregateNames = new HashSet<string>();

		// Token: 0x0400156F RID: 5487
		private bool _wasResolutionCorrelated;
	}
}
