using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;
using System.Linq;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000061 RID: 97
	internal class ProjectionPruner : BasicOpVisitorOfNode
	{
		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x0002B9B0 File Offset: 0x00029BB0
		private Command m_command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x0002B9BD File Offset: 0x00029BBD
		private ProjectionPruner(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
			this.m_referencedVars = compilerState.Command.CreateVarVec();
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x0002B9DD File Offset: 0x00029BDD
		internal static void Process(PlanCompiler compilerState)
		{
			compilerState.Command.Root = ProjectionPruner.Process(compilerState, compilerState.Command.Root);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x0002B9FC File Offset: 0x00029BFC
		internal static Node Process(PlanCompiler compilerState, Node node)
		{
			ProjectionPruner projectionPruner = new ProjectionPruner(compilerState);
			return projectionPruner.Process(node);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x0002BA17 File Offset: 0x00029C17
		private Node Process(Node node)
		{
			return base.VisitNode(node);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x0002BA20 File Offset: 0x00029C20
		private void AddReference(Var v)
		{
			this.m_referencedVars.Set(v);
		}

		// Token: 0x06000835 RID: 2101 RVA: 0x0002BA30 File Offset: 0x00029C30
		private void AddReference(IEnumerable<Var> varSet)
		{
			foreach (Var v in varSet)
			{
				this.AddReference(v);
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0002BA78 File Offset: 0x00029C78
		private bool IsReferenced(Var v)
		{
			return this.m_referencedVars.IsSet(v);
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0002BA86 File Offset: 0x00029C86
		private bool IsUnreferenced(Var v)
		{
			return !this.IsReferenced(v);
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0002BA94 File Offset: 0x00029C94
		private void PruneVarMap(VarMap varMap)
		{
			List<Var> list = new List<Var>();
			foreach (Var var in varMap.Keys)
			{
				if (!this.IsReferenced(var))
				{
					list.Add(var);
				}
				else
				{
					this.AddReference(varMap[var]);
				}
			}
			foreach (Var key in list)
			{
				varMap.Remove(key);
			}
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0002BB48 File Offset: 0x00029D48
		private void PruneVarSet(VarVec varSet)
		{
			varSet.And(this.m_referencedVars);
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0002BB56 File Offset: 0x00029D56
		protected override void VisitChildren(Node n)
		{
			base.VisitChildren(n);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0002BB6B File Offset: 0x00029D6B
		protected override void VisitChildrenReverse(Node n)
		{
			base.VisitChildrenReverse(n);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0002BB80 File Offset: 0x00029D80
		public override Node Visit(VarDefListOp op, Node n)
		{
			List<Node> list = new List<Node>();
			foreach (Node node in n.Children)
			{
				VarDefOp varDefOp = node.Op as VarDefOp;
				if (this.IsReferenced(varDefOp.Var))
				{
					list.Add(base.VisitNode(node));
				}
			}
			return this.m_command.CreateNode(op, list);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0002BC08 File Offset: 0x00029E08
		public override Node Visit(PhysicalProjectOp op, Node n)
		{
			if (n == this.m_command.Root)
			{
				ProjectionPruner.ColumnMapVarTracker.FindVars(op.ColumnMap, this.m_referencedVars);
				op.Outputs.RemoveAll(new Predicate<Var>(this.IsUnreferenced));
			}
			else
			{
				this.AddReference(op.Outputs);
			}
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x0600083E RID: 2110 RVA: 0x0002BC62 File Offset: 0x00029E62
		protected override Node VisitNestOp(NestBaseOp op, Node n)
		{
			this.AddReference(op.Outputs);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x0600083F RID: 2111 RVA: 0x0002BC78 File Offset: 0x00029E78
		public override Node Visit(SingleStreamNestOp op, Node n)
		{
			this.AddReference(op.Discriminator);
			return this.VisitNestOp(op, n);
		}

		// Token: 0x06000840 RID: 2112 RVA: 0x0002BC8E File Offset: 0x00029E8E
		public override Node Visit(MultiStreamNestOp op, Node n)
		{
			return this.VisitNestOp(op, n);
		}

		// Token: 0x06000841 RID: 2113 RVA: 0x0002BC98 File Offset: 0x00029E98
		protected override Node VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.VisitChildrenReverse(n);
			return n;
		}

		// Token: 0x06000842 RID: 2114 RVA: 0x0002BCA4 File Offset: 0x00029EA4
		public override Node Visit(DistinctOp op, Node n)
		{
			if (op.Keys.Count > 1 && n.Child0.Op.OpType == OpType.Project)
			{
				this.RemoveRedundantConstantKeys(op.Keys, ((ProjectOp)n.Child0.Op).Outputs, n.Child0.Child1);
			}
			this.AddReference(op.Keys);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x06000843 RID: 2115 RVA: 0x0002BD14 File Offset: 0x00029F14
		public override Node Visit(ElementOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.m_command.GetExtendedNodeInfo(n.Child0);
			this.AddReference(extendedNodeInfo.Definitions);
			n.Child0 = base.VisitNode(n.Child0);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x06000844 RID: 2116 RVA: 0x0002BC98 File Offset: 0x00029E98
		public override Node Visit(FilterOp op, Node n)
		{
			this.VisitChildrenReverse(n);
			return n;
		}

		// Token: 0x06000845 RID: 2117 RVA: 0x0002BD60 File Offset: 0x00029F60
		protected override Node VisitGroupByOp(GroupByBaseOp op, Node n)
		{
			for (int i = n.Children.Count - 1; i >= 2; i--)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
			if (op.Keys.Count > 1)
			{
				this.RemoveRedundantConstantKeys(op.Keys, op.Outputs, n.Child1);
			}
			this.AddReference(op.Keys);
			n.Children[1] = base.VisitNode(n.Children[1]);
			n.Children[0] = base.VisitNode(n.Children[0]);
			this.PruneVarSet(op.Outputs);
			if (op.Keys.Count == 0 && op.Outputs.Count == 0)
			{
				return this.m_command.CreateNode(this.m_command.CreateSingleRowTableOp());
			}
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0002BE5C File Offset: 0x0002A05C
		private void RemoveRedundantConstantKeys(VarVec keyVec, VarVec outputVec, Node varDefListNode)
		{
			List<Node> constantKeys = (from d in varDefListNode.Children
			where d.Op.OpType == OpType.VarDef && PlanCompilerUtil.IsConstantBaseOp(d.Child0.Op.OpType)
			select d).ToList<Node>();
			VarVec constantKeyVars = this.m_command.CreateVarVec(from d in constantKeys
			select ((VarDefOp)d.Op).Var);
			constantKeyVars.Minus(this.m_referencedVars);
			keyVec.Minus(constantKeyVars);
			outputVec.Minus(constantKeyVars);
			varDefListNode.Children.RemoveAll((Node c) => constantKeys.Contains(c) && constantKeyVars.IsSet(((VarDefOp)c.Op).Var));
			if (keyVec.Count == 0)
			{
				Node node = constantKeys.First<Node>();
				Var var = ((VarDefOp)node.Op).Var;
				keyVec.Set(var);
				outputVec.Set(var);
				varDefListNode.Children.Add(node);
			}
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0002BF60 File Offset: 0x0002A160
		public override Node Visit(GroupByIntoOp op, Node n)
		{
			Node node = this.VisitGroupByOp(op, n);
			if (node.Op.OpType == OpType.GroupByInto && n.Child3.Children.Count == 0)
			{
				GroupByIntoOp groupByIntoOp = (GroupByIntoOp)node.Op;
				node = this.m_command.CreateNode(this.m_command.CreateGroupByOp(groupByIntoOp.Keys, groupByIntoOp.Outputs), node.Child0, node.Child1, node.Child2);
			}
			return node;
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0002BFDC File Offset: 0x0002A1DC
		protected override Node VisitJoinOp(JoinBaseOp op, Node n)
		{
			if (n.Op.OpType == OpType.CrossJoin)
			{
				this.VisitChildren(n);
				return n;
			}
			n.Child2 = base.VisitNode(n.Child2);
			n.Child0 = base.VisitNode(n.Child0);
			n.Child1 = base.VisitNode(n.Child1);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0002C044 File Offset: 0x0002A244
		public override Node Visit(ProjectOp op, Node n)
		{
			this.PruneVarSet(op.Outputs);
			this.VisitChildrenReverse(n);
			if (!op.Outputs.IsEmpty)
			{
				return n;
			}
			return n.Child0;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0002C06E File Offset: 0x0002A26E
		public override Node Visit(ScanTableOp op, Node n)
		{
			PlanCompiler.Assert(!n.HasChild0, "scanTable with an input?");
			op.Table.ReferencedColumns.And(this.m_referencedVars);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0002C0A8 File Offset: 0x0002A2A8
		protected override Node VisitSetOp(SetOp op, Node n)
		{
			if (OpType.Intersect == op.OpType || OpType.Except == op.OpType)
			{
				this.AddReference(op.Outputs);
			}
			this.PruneVarSet(op.Outputs);
			foreach (VarMap varMap2 in op.VarMap)
			{
				this.PruneVarMap(varMap2);
			}
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x0002C10C File Offset: 0x0002A30C
		protected override Node VisitSortOp(SortBaseOp op, Node n)
		{
			foreach (SortKey sortKey in op.Keys)
			{
				this.AddReference(sortKey.Var);
			}
			if (n.HasChild1)
			{
				n.Child1 = base.VisitNode(n.Child1);
			}
			n.Child0 = base.VisitNode(n.Child0);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x0600084D RID: 2125 RVA: 0x0002C1A0 File Offset: 0x0002A3A0
		public override Node Visit(UnnestOp op, Node n)
		{
			this.AddReference(op.Var);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x0600084E RID: 2126 RVA: 0x0002C1B6 File Offset: 0x0002A3B6
		public override Node Visit(VarRefOp op, Node n)
		{
			this.AddReference(op.Var);
			return n;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0002C1C8 File Offset: 0x0002A3C8
		public override Node Visit(ExistsOp op, Node n)
		{
			ProjectOp projectOp = (ProjectOp)n.Child0.Op;
			this.AddReference(projectOp.Outputs.First);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x040007F2 RID: 2034
		private PlanCompiler m_compilerState;

		// Token: 0x040007F3 RID: 2035
		private VarVec m_referencedVars;

		// Token: 0x0200047C RID: 1148
		private class ColumnMapVarTracker : ColumnMapVisitor<VarVec>
		{
			// Token: 0x06003B5F RID: 15199 RVA: 0x000E04AC File Offset: 0x000DE6AC
			internal static void FindVars(ColumnMap columnMap, VarVec vec)
			{
				ProjectionPruner.ColumnMapVarTracker visitor = new ProjectionPruner.ColumnMapVarTracker();
				columnMap.Accept<VarVec>(visitor, vec);
			}

			// Token: 0x06003B60 RID: 15200 RVA: 0x000E04C7 File Offset: 0x000DE6C7
			private ColumnMapVarTracker()
			{
			}

			// Token: 0x06003B61 RID: 15201 RVA: 0x000E04CF File Offset: 0x000DE6CF
			internal override void Visit(VarRefColumnMap columnMap, VarVec arg)
			{
				arg.Set(columnMap.Var);
				base.Visit(columnMap, arg);
			}
		}
	}
}
