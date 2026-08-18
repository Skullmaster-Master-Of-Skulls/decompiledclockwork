using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Query.InternalTrees;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x020006A0 RID: 1696
	internal class SortRemover : BasicOpVisitorOfNode
	{
		// Token: 0x06004328 RID: 17192 RVA: 0x0013E786 File Offset: 0x0013C986
		private SortRemover(Command command, Node topMostSort)
		{
			this.m_command = command;
			this.m_topMostSort = topMostSort;
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x0013E7A8 File Offset: 0x0013C9A8
		internal static void Process(Command command)
		{
			Node topMostSort;
			if (command.Root.Child0 != null && command.Root.Child0.Op.OpType == OpType.Sort)
			{
				topMostSort = command.Root.Child0;
			}
			else
			{
				topMostSort = null;
			}
			SortRemover sortRemover = new SortRemover(command, topMostSort);
			command.Root = sortRemover.VisitNode(command.Root);
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x0013E808 File Offset: 0x0013CA08
		protected override void VisitChildren(Node n)
		{
			bool flag = false;
			for (int i = 0; i < n.Children.Count; i++)
			{
				Node node = n.Children[i];
				n.Children[i] = base.VisitNode(n.Children[i]);
				if (!object.ReferenceEquals(node, n.Children[i]) || this.changedNodes.Contains(node))
				{
					flag = true;
				}
			}
			if (flag)
			{
				this.m_command.RecomputeNodeInfo(n);
				this.changedNodes.Add(n);
			}
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x0013E898 File Offset: 0x0013CA98
		public override Node Visit(ConstrainedSortOp op, Node n)
		{
			if (op.Keys.Count > 0 || n.Children.Count != 3 || n.Child0 == null || n.Child1 == null || n.Child0.Op.OpType != OpType.Sort || n.Child1.Op.OpType != OpType.Null || n.Child0.Children.Count != 1)
			{
				return n;
			}
			return this.m_command.CreateNode(this.m_command.CreateConstrainedSortOp(((SortOp)n.Child0.Op).Keys, op.WithTies), n.Child0.Child0, n.Child1, n.Child2);
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x0013E958 File Offset: 0x0013CB58
		public override Node Visit(SortOp op, Node n)
		{
			this.VisitChildren(n);
			Node result;
			if (object.ReferenceEquals(n, this.m_topMostSort))
			{
				result = n;
			}
			else
			{
				result = n.Child0;
			}
			return result;
		}

		// Token: 0x040018DD RID: 6365
		private readonly Command m_command;

		// Token: 0x040018DE RID: 6366
		private readonly Node m_topMostSort;

		// Token: 0x040018DF RID: 6367
		private readonly HashSet<Node> changedNodes = new HashSet<Node>();
	}
}
