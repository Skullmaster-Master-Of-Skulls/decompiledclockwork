using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200015C RID: 348
	internal class Root : AstNode
	{
		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x00052482 File Offset: 0x00051482
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Root;
			}
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00052485 File Offset: 0x00051485
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}
	}
}
