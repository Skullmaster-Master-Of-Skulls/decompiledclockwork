using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000687 RID: 1671
	internal abstract class SubqueryTrackingVisitor : BasicOpVisitorOfNode
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x060041E8 RID: 16872 RVA: 0x00136E1C File Offset: 0x0013501C
		protected Command m_command
		{
			get
			{
				return this.m_compilerState.Command;
			}
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x00136E29 File Offset: 0x00135029
		protected SubqueryTrackingVisitor(PlanCompiler planCompilerState)
		{
			this.m_compilerState = planCompilerState;
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x00136E50 File Offset: 0x00135050
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

		// Token: 0x060041EB RID: 16875 RVA: 0x00136E88 File Offset: 0x00135088
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		protected Node AddSubqueryToParentRelOp(Var outputVar, Node subquery)
		{
			Node node = this.FindRelOpAncestor();
			PlanCompiler.Assert(node != null, "no ancestors found?");
			this.AddSubqueryToRelOpNode(node, subquery);
			subquery = this.m_command.CreateNode(this.m_command.CreateVarRefOp(outputVar));
			return subquery;
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x00136ED0 File Offset: 0x001350D0
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

		// Token: 0x060041ED RID: 16877 RVA: 0x00136F44 File Offset: 0x00135144
		protected override void VisitChildren(Node n)
		{
			this.m_ancestors.Push(n);
			for (int i = 0; i < n.Children.Count; i++)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
			this.m_ancestors.Pop();
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x00136FA0 File Offset: 0x001351A0
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

		// Token: 0x060041EF RID: 16879 RVA: 0x0013701C File Offset: 0x0013521C
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "VisitRelOpDefault")]
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			this.VisitChildren(n);
			List<Node> list;
			if (this.m_nodeSubqueries.TryGetValue(n, out list) && list.Count > 0)
			{
				PlanCompiler.Assert(n.Op.OpType == OpType.Project || n.Op.OpType == OpType.Filter || n.Op.OpType == OpType.GroupBy || n.Op.OpType == OpType.GroupByInto, "VisitRelOpDefault: Unexpected op?" + n.Op.OpType);
				Node child = this.AugmentWithSubqueries(n.Child0, list, true);
				n.Child0 = child;
			}
			return n;
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x001370C0 File Offset: 0x001352C0
		[SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "JoinOp")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Data.Entity.Core.Query.PlanCompiler.PlanCompiler.Assert(System.Boolean,System.String)")]
		protected bool ProcessJoinOp(Node n)
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

		// Token: 0x060041F1 RID: 16881 RVA: 0x00137194 File Offset: 0x00135394
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

		// Token: 0x0400186E RID: 6254
		protected readonly PlanCompiler m_compilerState;

		// Token: 0x0400186F RID: 6255
		protected readonly Stack<Node> m_ancestors = new Stack<Node>();

		// Token: 0x04001870 RID: 6256
		private readonly Dictionary<Node, List<Node>> m_nodeSubqueries = new Dictionary<Node, List<Node>>();
	}
}
