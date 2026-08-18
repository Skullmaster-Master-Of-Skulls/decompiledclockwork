using System;
using System.Collections.Generic;
using System.Data.Query.InternalTrees;

namespace System.Data.Query.PlanCompiler
{
	// Token: 0x02000059 RID: 89
	internal class SortRemover : BasicOpVisitorOfNode
	{
		// Token: 0x06000779 RID: 1913 RVA: 0x0002515F File Offset: 0x0002335F
		private SortRemover(Command command, Node topMostSort)
		{
			this.m_command = command;
			this.m_topMostSort = topMostSort;
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x00025180 File Offset: 0x00023380
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

		// Token: 0x0600077B RID: 1915 RVA: 0x000251E0 File Offset: 0x000233E0
		protected override void VisitChildren(Node n)
		{
			bool flag = false;
			for (int i = 0; i < n.Children.Count; i++)
			{
				Node node = n.Children[i];
				n.Children[i] = base.VisitNode(n.Children[i]);
				if (node != n.Children[i] || this.changedNodes.Contains(node))
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

		// Token: 0x0600077C RID: 1916 RVA: 0x0002526C File Offset: 0x0002346C
		public override Node Visit(SortOp op, Node n)
		{
			this.VisitChildren(n);
			Node result;
			if (n == this.m_topMostSort)
			{
				result = n;
			}
			else
			{
				result = n.Child0;
			}
			return result;
		}

		// Token: 0x040007C2 RID: 1986
		private Command m_command;

		// Token: 0x040007C3 RID: 1987
		private Node m_topMostSort;

		// Token: 0x040007C4 RID: 1988
		private HashSet<Node> changedNodes = new HashSet<Node>();
	}
}
