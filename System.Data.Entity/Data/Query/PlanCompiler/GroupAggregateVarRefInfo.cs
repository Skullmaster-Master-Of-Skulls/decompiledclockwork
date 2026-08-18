using System;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200008A RID: 138
	internal class GroupAggregateVarRefInfo
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x00033C62 File Offset: 0x00031E62
		internal GroupAggregateVarRefInfo(GroupAggregateVarInfo groupAggregateVarInfo, Node computation, bool isUnnested)
		{
			this._groupAggregateVarInfo = groupAggregateVarInfo;
			this._computation = computation;
			this._isUnnested = isUnnested;
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600099B RID: 2459 RVA: 0x00033C7F File Offset: 0x00031E7F
		internal Node Computation
		{
			get
			{
				return this._computation;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00033C87 File Offset: 0x00031E87
		internal GroupAggregateVarInfo GroupAggregateVarInfo
		{
			get
			{
				return this._groupAggregateVarInfo;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600099D RID: 2461 RVA: 0x00033C8F File Offset: 0x00031E8F
		internal bool IsUnnested
		{
			get
			{
				return this._isUnnested;
			}
		}

		// Token: 0x04000892 RID: 2194
		private readonly Node _computation;

		// Token: 0x04000893 RID: 2195
		private readonly GroupAggregateVarInfo _groupAggregateVarInfo;

		// Token: 0x04000894 RID: 2196
		private readonly bool _isUnnested;
	}
}
