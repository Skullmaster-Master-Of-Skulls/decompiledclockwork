using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000092 RID: 146
	internal abstract class SubqueryTrackingVisitor : BasicOpVisitorOfNode
	{
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x00034E21 File Offset: 0x00033021
		protected Command m_command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x060009CF RID: 2511 RVA: 0x00034E2E File Offset: 0x0003302E
		protected SubqueryTrackingVisitor(PlanCompiler planCompilerState)
		{
			this.m_compilerState = planCompilerState;
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x00034E54 File Offset: 0x00033054
		protected void AddSubqueryToRelOpNode(Node relOpNode, Node subquery)
		{
			List<Node> list;
			if (!this.m_nodeSubqueries.TryGetValue(relOpNode, out list))
			{
				list = new List<Node>();
				this.m_nodeSubqueries[relOpNode] = list;
			}
			list.Add(subquery);
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00034E8C File Offset: 0x0003308C
		protected Node AddSubqueryToParentRelOp(Var outputVar, Node subquery)
		{
			Node node = this.FindRelOpAncestor();
			PlanCompiler.Assert(node != null, "no ancestors found?");
			this.AddSubqueryToRelOpNode(node, subquery);
			subquery = this.m_command.CreateNode(this.m_command.CreateVarRefOp(outputVar));
			return subquery;
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00034ED0 File Offset: 0x000330D0
		protected Node FindRelOpAncestor()
		{
			foreach (Node node in this.m_ancestors)
			{
				if (node.Op.IsRelOp)
				{
					return node;
				}
				if (node.Op.IsPhysicalOp)
				{
					return null;
				}
			}
			return null;
		}

		// Token: 0x060009D3 RID: 2515 RVA: 0x00034F44 File Offset: 0x00033144
		protected override void VisitChildren(Node n)
		{
			this.m_ancestors.Push(n);
			for (int i = 0; i < n.Children.Count; i++)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
			this.m_ancestors.Pop();
		}

		// Token: 0x060009D4 RID: 2516 RVA: 0x00034FA0 File Offset: 0x000331A0
		private Node AugmentWithSubqueries(Node input, List<Node> subqueries, bool inputFirst)
		{
			Node node;
			int num;
			if (inputFirst)
			{
				node = input;
				num = 0;
			}
			else
			{
				node = subqueries[0];
				num = 1;
			}
			for (int i = num; i < subqueries.Count; i++)
			{
				OuterApplyOp op = this.m_command.CreateOuterApplyOp();
				node = this.m_command.CreateNode(op, node, subqueries[i]);
			}
			if (!inputFirst)
			{
				node = this.m_command.CreateNode(this.m_command.CreateCrossApplyOp(), node, input);
			}
			this.m_compilerState.MarkPhaseAsNeeded(PlanCompilerPhase.JoinElimination);
			return node;
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x0003501C File Offset: 0x0003321C
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			this.VisitChildren(n);
			List<Node> list;
			if (this.m_nodeSubqueries.TryGetValue(n, out list) && list.Count > 0)
			{
				PlanCompiler.Assert(n.Op.OpType == OpType.Project || n.Op.OpType == OpType.Filter || n.Op.OpType == OpType.GroupBy || n.Op.OpType == OpType.GroupByInto, "VisitRelOpDefault: Unexpected op?" + n.Op.OpType.ToString());
				Node child = this.AugmentWithSubqueries(n.Child0, list, true);
				n.Child0 = child;
			}
			return n;
		}

		// Token: 0x060009D6 RID: 2518 RVA: 0x000350CC File Offset: 0x000332CC
		protected bool ProcessJoinOp(JoinBaseOp op, Node n)
		{
			this.VisitChildren(n);
			List<Node> subqueries;
			if (!this.m_nodeSubqueries.TryGetValue(n, out subqueries))
			{
				return false;
			}
			PlanCompiler.Assert(n.Op.OpType == OpType.InnerJoin || n.Op.OpType == OpType.LeftOuterJoin || n.Op.OpType == OpType.FullOuterJoin, "unexpected op?");
			PlanCompiler.Assert(n.HasChild2, "missing second child to JoinOp?");
			Node child = n.Child2;
			Node node = this.m_command.CreateNode(this.m_command.CreateSingleRowTableOp());
			node = this.AugmentWithSubqueries(node, subqueries, true);
			Node arg = this.m_command.CreateNode(this.m_command.CreateFilterOp(), node, child);
			Node child2 = this.m_command.CreateNode(this.m_command.CreateExistsOp(), arg);
			n.Child2 = child2;
			return true;
		}

		// Token: 0x060009D7 RID: 2519 RVA: 0x000351A0 File Offset: 0x000333A0
		public override Node Visit(UnnestOp op, Node n)
		{
			this.VisitChildren(n);
			List<Node> subqueries;
			if (this.m_nodeSubqueries.TryGetValue(n, out subqueries))
			{
				return this.AugmentWithSubqueries(n, subqueries, false);
			}
			return n;
		}

		// Token: 0x040008A1 RID: 2209
		protected readonly PlanCompiler m_compilerState;

		// Token: 0x040008A2 RID: 2210
		protected readonly Stack<Node> m_ancestors = new Stack<Node>();

		// Token: 0x040008A3 RID: 2211
		private readonly Dictionary<Node, List<Node>> m_nodeSubqueries = new Dictionary<Node, List<Node>>();
	}
}
