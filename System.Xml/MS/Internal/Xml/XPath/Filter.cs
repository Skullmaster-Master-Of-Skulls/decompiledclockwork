using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200013C RID: 316
	internal class Filter : AstNode
	{
		// Token: 0x0600120D RID: 4621 RVA: 0x0004F464 File Offset: 0x0004E464
		public Filter(AstNode input, AstNode condition)
		{
			this.input = input;
			this.condition = condition;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600120E RID: 4622 RVA: 0x0004F47A File Offset: 0x0004E47A
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Filter;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x0004F47D File Offset: 0x0004E47D
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x0004F480 File Offset: 0x0004E480
		public AstNode Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x0004F488 File Offset: 0x0004E488
		public AstNode Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x04000B5D RID: 2909
		private AstNode input;

		// Token: 0x04000B5E RID: 2910
		private AstNode condition;
	}
}
