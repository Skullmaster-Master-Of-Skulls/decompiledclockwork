using System;
using System.Collections.Generic;
using System.Data.Metadata.Edm;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x0200004F RID: 79
	internal class JoinElimination : BasicOpVisitorOfNode
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x0001EF69 File Offset: 0x0001D169
		private Command Command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0001EF76 File Offset: 0x0001D176
		private ConstraintManager ConstraintManager
		{
			get
			{
				return this.m_compilerState.ConstraintManager;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0001EF84 File Offset: 0x0001D184
		private JoinElimination(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
			this.m_varRemapper = new VarRemapper(this.m_compilerState.Command);
			this.m_varRefManager = new VarRefManager(this.m_compilerState.Command);
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0001EFD8 File Offset: 0x0001D1D8
		internal static bool Process(PlanCompiler compilerState)
		{
			JoinElimination joinElimination = new JoinElimination(compilerState);
			joinElimination.Process();
			return joinElimination.m_treeModified;
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		private void Process()
		{
			this.Command.Root = base.VisitNode(this.Command.Root);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x0001F016 File Offset: 0x0001D216
		private bool NeedsJoinGraph(Node joinNode)
		{
			return !this.m_joinGraphUnnecessaryMap.ContainsKey(joinNode);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0001F028 File Offset: 0x0001D228
		private Node ProcessJoinGraph(Node joinNode)
		{
			JoinGraph joinGraph = new JoinGraph(this.Command, this.ConstraintManager, this.m_varRefManager, joinNode, this.IsSqlCeProvider);
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060006D6 RID: 1750 RVA: 0x0001F104 File Offset: 0x0001D304
		private bool IsSqlCeProvider
		{
			get
			{
				if (this.m_isSqlCe == null)
				{
					PlanCompiler.Assert(this.m_compilerState != null, "Plan compiler cannot be null");
					StoreItemCollection storeItemCollection = (StoreItemCollection)this.m_compilerState.MetadataWorkspace.GetItemCollection(DataSpace.SSpace);
					if (storeItemCollection != null)
					{
						this.m_isSqlCe = new bool?(storeItemCollection.StoreProviderManifest.NamespaceName == "SqlServerCe");
					}
				}
				return this.m_isSqlCe != null && this.m_isSqlCe.Value;
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0001F185 File Offset: 0x0001D385
		private Node VisitDefaultForAllNodes(Node n)
		{
			this.VisitChildren(n);
			this.m_varRemapper.RemapNode(n);
			this.Command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x060006D8 RID: 1752 RVA: 0x0001F1A7 File Offset: 0x0001D3A7
		protected override Node VisitDefault(Node n)
		{
			this.m_varRefManager.AddChildren(n);
			return this.VisitDefaultForAllNodes(n);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0001F1BC File Offset: 0x0001D3BC
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

		// Token: 0x04000791 RID: 1937
		private const string SqlServerCeNamespaceName = "SqlServerCe";

		// Token: 0x04000792 RID: 1938
		private PlanCompiler m_compilerState;

		// Token: 0x04000793 RID: 1939
		private Dictionary<Node, Node> m_joinGraphUnnecessaryMap = new Dictionary<Node, Node>();

		// Token: 0x04000794 RID: 1940
		private VarRemapper m_varRemapper;

		// Token: 0x04000795 RID: 1941
		private bool m_treeModified;

		// Token: 0x04000796 RID: 1942
		private VarRefManager m_varRefManager;

		// Token: 0x04000797 RID: 1943
		private bool? m_isSqlCe;
	}
}
