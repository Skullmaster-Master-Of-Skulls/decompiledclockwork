using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000150 RID: 336
	internal class Operand : AstNode
	{
		// Token: 0x060012A9 RID: 4777 RVA: 0x0005127F File Offset: 0x0005027F
		public Operand(string val)
		{
			this.type = XPathResultType.String;
			this.val = val;
		}

		// Token: 0x060012AA RID: 4778 RVA: 0x00051295 File Offset: 0x00050295
		public Operand(double val)
		{
			this.type = XPathResultType.Number;
			this.val = val;
		}

		// Token: 0x060012AB RID: 4779 RVA: 0x000512B0 File Offset: 0x000502B0
		public Operand(bool val)
		{
			this.type = XPathResultType.Boolean;
			this.val = val;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x000512CB File Offset: 0x000502CB
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.ConstantOperand;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x060012AD RID: 4781 RVA: 0x000512CE File Offset: 0x000502CE
		public override XPathResultType ReturnType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x000512D6 File Offset: 0x000502D6
		public object OperandValue
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000BA5 RID: 2981
		private XPathResultType type;

		// Token: 0x04000BA6 RID: 2982
		private object val;
	}
}
