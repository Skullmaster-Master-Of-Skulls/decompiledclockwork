using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000117 RID: 279
	internal abstract class BasicOpVisitorOfNode : BasicOpVisitorOfT<Node>
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x0003DA4C File Offset: 0x0003BC4C
		protected override void VisitChildren(Node n)
		{
			for (int i = 0; i < n.Children.Count; i++)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003DA90 File Offset: 0x0003BC90
		protected override void VisitChildrenReverse(Node n)
		{
			for (int i = n.Children.Count - 1; i >= 0; i--)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003DAD3 File Offset: 0x0003BCD3
		protected override Node VisitDefault(Node n)
		{
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0003DADD File Offset: 0x0003BCDD
		protected override Node VisitAncillaryOpDefault(AncillaryOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x0003DADD File Offset: 0x0003BCDD
		protected override Node VisitPhysicalOpDefault(PhysicalOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x0003DADD File Offset: 0x0003BCDD
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x0003DADD File Offset: 0x0003BCDD
		protected override Node VisitScalarOpDefault(ScalarOp op, Node n)
		{
			return this.VisitDefault(n);
		}
	}
}
