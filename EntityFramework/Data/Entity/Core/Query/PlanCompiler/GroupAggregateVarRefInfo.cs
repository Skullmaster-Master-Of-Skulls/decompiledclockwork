using System;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000673 RID: 1651
	internal class GroupAggregateVarRefInfo
	{
		// Token: 0x0600406B RID: 16491 RVA: 0x00127A00 File Offset: 0x00125C00
		internal GroupAggregateVarRefInfo(GroupAggregateVarInfo groupAggregateVarInfo, Node computation, bool isUnnested)
		{
			this._groupAggregateVarInfo = groupAggregateVarInfo;
			this._computation = computation;
			this._isUnnested = isUnnested;
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x0600406C RID: 16492 RVA: 0x00127A1D File Offset: 0x00125C1D
		internal Node Computation
		{
			get
			{
				return this._computation;
			}
		}

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x00127A25 File Offset: 0x00125C25
		internal GroupAggregateVarInfo GroupAggregateVarInfo
		{
			get
			{
				return this._groupAggregateVarInfo;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x0600406E RID: 16494 RVA: 0x00127A2D File Offset: 0x00125C2D
		internal bool IsUnnested
		{
			get
			{
				return this._isUnnested;
			}
		}

		// Token: 0x04001806 RID: 6150
		private readonly Node _computation;

		// Token: 0x04001807 RID: 6151
		private readonly GroupAggregateVarInfo _groupAggregateVarInfo;

		// Token: 0x04001808 RID: 6152
		private readonly bool _isUnnested;
	}
}
