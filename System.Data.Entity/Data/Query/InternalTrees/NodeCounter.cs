using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x02000118 RID: 280
	internal class NodeCounter : BasicOpVisitorOfT<int>
	{
		// Token: 0x06000E6A RID: 3690 RVA: 0x0003DAF0 File Offset: 0x0003BCF0
		internal static int Count(Node subTree)
		{
			NodeCounter nodeCounter = new NodeCounter();
			return nodeCounter.VisitNode(subTree);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x0003DB0C File Offset: 0x0003BD0C
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
