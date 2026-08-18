using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000089 RID: 137
	internal class GroupAggregateVarInfo
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x00033C07 File Offset: 0x00031E07
		internal GroupAggregateVarInfo(Node defingingGroupNode, Var groupAggregateVar)
		{
			this._definingGroupByNode = defingingGroupNode;
			this._groupAggregateVar = groupAggregateVar;
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00033C1D File Offset: 0x00031E1D
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

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00033C38 File Offset: 0x00031E38
		internal bool HasCandidateAggregateNodes
		{
			get
			{
				return this._candidateAggregateNodes != null && this._candidateAggregateNodes.Count != 0;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00033C52 File Offset: 0x00031E52
		internal Node DefiningGroupNode
		{
			get
			{
				return this._definingGroupByNode;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00033C5A File Offset: 0x00031E5A
		internal Var GroupAggregateVar
		{
			get
			{
				return this._groupAggregateVar;
			}
		}

		// Token: 0x0400088F RID: 2191
		private readonly Node _definingGroupByNode;

		// Token: 0x04000890 RID: 2192
		private HashSet<KeyValuePair<Node, Node>> _candidateAggregateNodes;

		// Token: 0x04000891 RID: 2193
		private readonly Var _groupAggregateVar;
	}
}
