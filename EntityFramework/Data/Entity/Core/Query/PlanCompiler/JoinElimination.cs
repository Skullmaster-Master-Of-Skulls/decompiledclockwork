using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x0200067E RID: 1662
	internal class JoinElimination : BasicOpVisitorOfNode
	{
		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x0012C051 File Offset: 0x0012A251
		private Command Command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x0012C05E File Offset: 0x0012A25E
		private ConstraintManager ConstraintManager
		{
			get
			{
				return this.m_compilerState.ConstraintManager;
			}
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x0012C06C File Offset: 0x0012A26C
		private JoinElimination(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
			this.m_varRemapper = new VarRemapper(this.m_compilerState.Command);
			this.m_varRefManager = new VarRefManager(this.m_compilerState.Command);
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x0012C0C0 File Offset: 0x0012A2C0
		internal static bool Process(PlanCompiler compilerState)
		{
			JoinElimination joinElimination = new JoinElimination(compilerState);
			joinElimination.Process();
			return joinElimination.m_treeModified;
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x0012C0E0 File Offset: 0x0012A2E0
		private void Process()
		{
			this.Command.Root = base.VisitNode(this.Command.Root);
		}

		// Token: 0x06004109 RID: 16649 RVA: 0x0012C0FE File Offset: 0x0012A2FE
		private bool NeedsJoinGraph(Node joinNode)
		{
			return !this.m_joinGraphUnnecessaryMap.ContainsKey(joinNode);
		}

		// Token: 0x0600410A RID: 16650 RVA: 0x0012C110 File Offset: 0x0012A310
		private Node ProcessJoinGraph(Node joinNode)
		{
			JoinGraph joinGraph = new JoinGraph(this.Command, this.ConstraintManager, this.m_varRefManager, joinNode);
			VarMap varMap;
			Dictionary<Node, Node> dictionary;
			Node result = joinGraph.DoJoinElimination(out varMap, out dictionary);
			foreach (KeyValuePair<Var, Var> keyValuePair in varMap)
			{
				this.m_varRemapper.AddMapping(keyValuePair.Key, keyValuePair.Value);
			}
			foreach (Node node in dictionary.Keys)
			{
				this.m_joinGraphUnnecessaryMap[node] = node;
			}
			return result;
		}

		// Token: 0x0600410B RID: 16651 RVA: 0x0012C1E4 File Offset: 0x0012A3E4
		private Node VisitDefaultForAllNodes(Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			this.Command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x0600410C RID: 16652 RVA: 0x0012C206 File Offset: 0x0012A406
		protected override Node VisitDefault(Node n)
		{
			this.m_varRefManager.AddChildren(n);
			return this.VisitDefaultForAllNodes(n);
		}

		// Token: 0x0600410D RID: 16653 RVA: 0x0012C21C File Offset: 0x0012A41C
		protected override Node VisitJoinOp(JoinBaseOp op, Node joinNode)
		{
			Node node;
			if (this.NeedsJoinGraph(joinNode))
			{
				node = this.ProcessJoinGraph(joinNode);
				if (node != joinNode)
				{
					this.m_treeModified = true;
				}
			}
			else
			{
				node = joinNode;
			}
			return this.VisitDefaultForAllNodes(node);
		}

		// Token: 0x0400182F RID: 6191
		private readonly PlanCompiler m_compilerState;

		// Token: 0x04001830 RID: 6192
		private readonly Dictionary<Node, Node> m_joinGraphUnnecessaryMap = new Dictionary<Node, Node>();

		// Token: 0x04001831 RID: 6193
		private readonly VarRemapper m_varRemapper;

		// Token: 0x04001832 RID: 6194
		private bool m_treeModified;

		// Token: 0x04001833 RID: 6195
		private readonly VarRefManager m_varRefManager;
	}
}
