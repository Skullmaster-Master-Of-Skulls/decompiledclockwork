using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200063B RID: 1595
	internal class NodeCounter : BasicOpVisitorOfT<int>
	{
		// Token: 0x06003EAB RID: 16043 RVA: 0x0011F850 File Offset: 0x0011DA50
		internal static int Count(Node subTree)
		{
			NodeCounter nodeCounter = new NodeCounter();
			return nodeCounter.VisitNode(subTree);
		}

		// Token: 0x06003EAC RID: 16044 RVA: 0x0011F86C File Offset: 0x0011DA6C
		protected override int VisitDefault(Node n)
		{
			int num = 1;
			foreach (Node n2 in n.Children)
			{
				num += base.VisitNode(n2);
			}
			return num;
		}
	}
}
