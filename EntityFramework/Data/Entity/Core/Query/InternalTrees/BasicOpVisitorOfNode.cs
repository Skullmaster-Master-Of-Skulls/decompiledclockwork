using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020000D4 RID: 212
	internal abstract class BasicOpVisitorOfNode : BasicOpVisitorOfT<Node>
	{
		// Token: 0x0600054B RID: 1355 RVA: 0x00023FAC File Offset: 0x000221AC
		protected override void VisitChildren(Node n)
		{
			for (int i = 0; i < n.Children.Count; i++)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x0600054C RID: 1356 RVA: 0x00023FF0 File Offset: 0x000221F0
		protected override void VisitChildrenReverse(Node n)
		{
			for (int i = n.Children.Count - 1; i >= 0; i--)
			{
				n.Children[i] = base.VisitNode(n.Children[i]);
			}
		}

		// Token: 0x0600054D RID: 1357 RVA: 0x00024033 File Offset: 0x00022233
		protected override Node VisitDefault(Node n)
		{
			this.VisitChildren(n);
			return n;
		}

		// Token: 0x0600054E RID: 1358 RVA: 0x0002403D File Offset: 0x0002223D
		protected override Node VisitAncillaryOpDefault(AncillaryOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00024046 File Offset: 0x00022246
		protected override Node VisitPhysicalOpDefault(PhysicalOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x0002404F File Offset: 0x0002224F
		protected override Node VisitRelOpDefault(RelOp op, Node n)
		{
			return this.VisitDefault(n);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00024058 File Offset: 0x00022258
		protected override Node VisitScalarOpDefault(ScalarOp op, Node n)
		{
			return this.VisitDefault(n);
		}
	}
}
