using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000671 RID: 1649
	internal class GroupAggregateVarInfo
	{
		// Token: 0x06004060 RID: 16480 RVA: 0x00127899 File Offset: 0x00125A99
		internal GroupAggregateVarInfo(Node defingingGroupNode, Var groupAggregateVar)
		{
			this._definingGroupByNode = defingingGroupNode;
			this._groupAggregateVar = groupAggregateVar;
		}

		// Token: 0x170009E6 RID: 2534
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x001278AF File Offset: 0x00125AAF
		internal HashSet<KeyValuePair<Node, Node>> CandidateAggregateNodes
		{
			get
			{
				if (this._candidateAggregateNodes == null)
				{
					this._candidateAggregateNodes = new HashSet<KeyValuePair<Node, Node>>();
				}
				return this._candidateAggregateNodes;
			}
		}

		// Token: 0x170009E7 RID: 2535
		// (get) Token: 0x06004062 RID: 16482 RVA: 0x001278CA File Offset: 0x00125ACA
		internal bool HasCandidateAggregateNodes
		{
			get
			{
				return this._candidateAggregateNodes != null && this._candidateAggregateNodes.Count != 0;
			}
		}

		// Token: 0x170009E8 RID: 2536
		// (get) Token: 0x06004063 RID: 16483 RVA: 0x001278E7 File Offset: 0x00125AE7
		internal Node DefiningGroupNode
		{
			get
			{
				return this._definingGroupByNode;
			}
		}

		// Token: 0x170009E9 RID: 2537
		// (get) Token: 0x06004064 RID: 16484 RVA: 0x001278EF File Offset: 0x00125AEF
		internal Var GroupAggregateVar
		{
			get
			{
				return this._groupAggregateVar;
			}
		}

		// Token: 0x04001800 RID: 6144
		private readonly Node _definingGroupByNode;

		// Token: 0x04001801 RID: 6145
		private HashSet<KeyValuePair<Node, Node>> _candidateAggregateNodes;

		// Token: 0x04001802 RID: 6146
		private readonly Var _groupAggregateVar;
	}
}
