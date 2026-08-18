using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000143 RID: 323
	internal class Group : AstNode
	{
		// Token: 0x06001239 RID: 4665 RVA: 0x0004FD64 File Offset: 0x0004ED64
		public Group(AstNode groupNode)
		{
			this.groupNode = groupNode;
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x0004FD73 File Offset: 0x0004ED73
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Group;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x0004FD76 File Offset: 0x0004ED76
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600123C RID: 4668 RVA: 0x0004FD79 File Offset: 0x0004ED79
		public AstNode GroupNode
		{
			get
			{
				return this.groupNode;
			}
		}

		// Token: 0x04000B88 RID: 2952
		private AstNode groupNode;
	}
}
