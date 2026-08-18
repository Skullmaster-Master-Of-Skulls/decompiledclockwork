using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000692 RID: 1682
	internal class ProjectionPruner : BasicOpVisitorOfNode
	{
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06004274 RID: 17012 RVA: 0x0013AE08 File Offset: 0x00139008
		private Command m_command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x06004275 RID: 17013 RVA: 0x0013AE15 File Offset: 0x00139015
		private ProjectionPruner(PlanCompiler compilerState)
		{
			this.m_compilerState = compilerState;
			this.m_referencedVars = compilerState.Command.CreateVarVec();
		}

		// Token: 0x06004276 RID: 17014 RVA: 0x0013AE35 File Offset: 0x00139035
		internal static void Process(PlanCompiler compilerState)
		{
			compilerState.Command.Root = ProjectionPruner.Process(compilerState, compilerState.Command.Root);
		}

		// Token: 0x06004277 RID: 17015 RVA: 0x0013AE54 File Offset: 0x00139054
		internal static Node Process(PlanCompiler compilerState, Node node)
		{
			ProjectionPruner projectionPruner = new ProjectionPruner(compilerState);
			return projectionPruner.Process(node);
		}

		// Token: 0x06004278 RID: 17016 RVA: 0x0013AE6F File Offset: 0x0013906F
		private Node Process(Node node)
		{
			return base.VisitNode(node);
		}

		// Token: 0x06004279 RID: 17017 RVA: 0x0013AE78 File Offset: 0x00139078
		private void AddReference(Var v)
		{
			this.m_referencedVars.Set(v);
		}

		// Token: 0x0600427A RID: 17018 RVA: 0x0013AE88 File Offset: 0x00139088
		private void AddReference(IEnumerable<Var> varSet)
		{
			foreach (Var v in varSet)
			{
				this.AddReference(v);
			}
		}

		// Token: 0x0600427B RID: 17019 RVA: 0x0013AED0 File Offset: 0x001390D0
		private bool IsReferenced(Var v)
		{
			return this.m_referencedVars.IsSet(v);
		}

		// Token: 0x0600427C RID: 17020 RVA: 0x0013AEDE File Offset: 0x001390DE
		private bool IsUnreferenced(Var v)
		{
			return !this.IsReferenced(v);
		}

		// Token: 0x0600427D RID: 17021 RVA: 0x0013AEEC File Offset: 0x001390EC
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

		// Token: 0x0600427E RID: 17022 RVA: 0x0013AF9C File Offset: 0x0013919C
		private void PruneVarSet(VarVec varSet)
		{
			varSet.And(this.m_referencedVars);
		}

		// Token: 0x0600427F RID: 17023 RVA: 0x0013AFAA File Offset: 0x001391AA
		protected override void VisitChildren(Node n)
		{
			base.VisitChildren(n);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x06004280 RID: 17024 RVA: 0x0013AFBF File Offset: 0x001391BF
		protected override void VisitChildrenReverse(Node n)
		{
			base.VisitChildrenReverse(n);
			this.m_command.RecomputeNodeInfo(n);
		}

		// Token: 0x06004281 RID: 17025 RVA: 0x0013AFD4 File Offset: 0x001391D4
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

		// Token: 0x06004282 RID: 17026 RVA: 0x0013B05C File Offset: 0x0013925C
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

		// Token: 0x06004283 RID: 17027 RVA: 0x0013B0B6 File Offset: 0x001392B6
		protected override Node VisitNestOp(NestBaseOp op, Node n)
		{
			this.AddReference(op.Outputs);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x06004284 RID: 17028 RVA: 0x0013B0CC File Offset: 0x001392CC
		public override Node Visit(SingleStreamNestOp op, Node n)
		{
			this.AddReference(op.Discriminator);
			return this.VisitNestOp(op, n);
		}

		// Token: 0x06004285 RID: 17029 RVA: 0x0013B0E2 File Offset: 0x001392E2
		public override Node Visit(MultiStreamNestOp op, Node n)
		{
			return this.VisitNestOp(op, n);
		}

		// Token: 0x06004286 RID: 17030 RVA: 0x0013B0EC File Offset: 0x001392EC
		protected override Node VisitApplyOp(ApplyBaseOp op, Node n)
		{
			this.VisitChildrenReverse(n);
			return n;
		}

		// Token: 0x06004287 RID: 17031 RVA: 0x0013B0F8 File Offset: 0x001392F8
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

		// Token: 0x06004288 RID: 17032 RVA: 0x0013B168 File Offset: 0x00139368
		public override Node Visit(ElementOp op, Node n)
		{
			ExtendedNodeInfo extendedNodeInfo = this.m_command.GetExtendedNodeInfo(n.Child0);
			this.AddReference(extendedNodeInfo.Definitions);
			n.Child0 = base.VisitNode(n.Child0);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x06004289 RID: 17033 RVA: 0x0013B1B2 File Offset: 0x001393B2
		public override Node Visit(FilterOp op, Node n)
		{
			this.VisitChildrenReverse(n);
			return n;
		}

		// Token: 0x0600428A RID: 17034 RVA: 0x0013B1BC File Offset: 0x001393BC
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

		// Token: 0x0600428B RID: 17035 RVA: 0x0013B328 File Offset: 0x00139528
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

		// Token: 0x0600428C RID: 17036 RVA: 0x0013B428 File Offset: 0x00139628
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

		// Token: 0x0600428D RID: 17037 RVA: 0x0013B4A4 File Offset: 0x001396A4
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

		// Token: 0x0600428E RID: 17038 RVA: 0x0013B50C File Offset: 0x0013970C
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

		// Token: 0x0600428F RID: 17039 RVA: 0x0013B536 File Offset: 0x00139736
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "scanTable")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		public override Node Visit(ScanTableOp op, Node n)
		{
			PlanCompiler.Assert(!n.HasChild0, "scanTable with an input?");
			op.Table.ReferencedColumns.And(this.m_referencedVars);
			this.m_command.RecomputeNodeInfo(n);
			return n;
		}

		// Token: 0x06004290 RID: 17040 RVA: 0x0013B570 File Offset: 0x00139770
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

		// Token: 0x06004291 RID: 17041 RVA: 0x0013B5D4 File Offset: 0x001397D4
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

		// Token: 0x06004292 RID: 17042 RVA: 0x0013B668 File Offset: 0x00139868
		public override Node Visit(UnnestOp op, Node n)
		{
			this.AddReference(op.Var);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x06004293 RID: 17043 RVA: 0x0013B67E File Offset: 0x0013987E
		public override Node Visit(VarRefOp op, Node n)
		{
			this.AddReference(op.Var);
			return n;
		}

		// Token: 0x06004294 RID: 17044 RVA: 0x0013B690 File Offset: 0x00139890
		public override Node Visit(ExistsOp op, Node n)
		{
			ProjectOp projectOp = (ProjectOp)n.Child0.Op;
			this.AddReference(projectOp.Outputs.First);
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x0400189F RID: 6303
		private readonly PlanCompiler m_compilerState;

		// Token: 0x040018A0 RID: 6304
		private readonly VarVec m_referencedVars;

		// Token: 0x02000693 RID: 1683
		private class ColumnMapVarTracker : ColumnMapVisitor<VarVec>
		{
			// Token: 0x06004297 RID: 17047 RVA: 0x0013B6C8 File Offset: 0x001398C8
			internal static void FindVars(ColumnMap columnMap, VarVec vec)
			{
				ProjectionPruner.ColumnMapVarTracker visitor = new ProjectionPruner.ColumnMapVarTracker();
				columnMap.Accept<VarVec>(visitor, vec);
			}

			// Token: 0x06004298 RID: 17048 RVA: 0x0013B6E3 File Offset: 0x001398E3
			private ColumnMapVarTracker()
			{
			}

			// Token: 0x06004299 RID: 17049 RVA: 0x0013B6EB File Offset: 0x001398EB
			internal override void Visit(VarRefColumnMap columnMap, VarVec arg)
			{
				arg.Set(columnMap.Var);
				base.Visit(columnMap, arg);
			}
		}
	}
}
