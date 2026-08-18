using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000038 RID: 56
	internal class Root : AstNode
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00006ED1 File Offset: 0x000050D1
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Root;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00006ED4 File Offset: 0x000050D4
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}
	}
}
