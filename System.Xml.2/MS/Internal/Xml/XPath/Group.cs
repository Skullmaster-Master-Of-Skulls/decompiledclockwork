using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000022 RID: 34
	internal class Group : AstNode
	{
		// Token: 0x060000E4 RID: 228 RVA: 0x00004527 File Offset: 0x00002727
		public Group(AstNode groupNode)
		{
			this.groupNode = groupNode;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00004536 File Offset: 0x00002736
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Group;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000E6 RID: 230 RVA: 0x00004539 File Offset: 0x00002739
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x0000453C File Offset: 0x0000273C
		public AstNode GroupNode
		{
			get
			{
				return this.groupNode;
			}
		}

		// Token: 0x04000091 RID: 145
		private AstNode groupNode;
	}
}
