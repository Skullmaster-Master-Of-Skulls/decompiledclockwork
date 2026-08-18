using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200001C RID: 28
	internal class Filter : AstNode
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00003C88 File Offset: 0x00001E88
		public Filter(AstNode input, AstNode condition)
		{
			this.input = input;
			this.condition = condition;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00003C9E File Offset: 0x00001E9E
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Filter;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00003CA1 File Offset: 0x00001EA1
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.NodeSet;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003CA4 File Offset: 0x00001EA4
		public AstNode Input
		{
			get
			{
				return this.input;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003CAC File Offset: 0x00001EAC
		public AstNode Condition
		{
			get
			{
				return this.condition;
			}
		}

		// Token: 0x04000083 RID: 131
		private AstNode input;

		// Token: 0x04000084 RID: 132
		private AstNode condition;
	}
}
