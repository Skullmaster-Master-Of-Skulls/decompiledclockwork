using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200002E RID: 46
	internal class Operand : AstNode
	{
		// Token: 0x06000161 RID: 353 RVA: 0x00005C18 File Offset: 0x00003E18
		public Operand(string val)
		{
			this.type = XPathResultType.String;
			this.val = val;
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00005C2E File Offset: 0x00003E2E
		public Operand(double val)
		{
			this.type = XPathResultType.Number;
			this.val = val;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00005C49 File Offset: 0x00003E49
		public Operand(bool val)
		{
			this.type = XPathResultType.Boolean;
			this.val = val;
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00005C64 File Offset: 0x00003E64
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.ConstantOperand;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005C67 File Offset: 0x00003E67
		public override XPathResultType ReturnType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000166 RID: 358 RVA: 0x00005C6F File Offset: 0x00003E6F
		public object OperandValue
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x040000AC RID: 172
		private XPathResultType type;

		// Token: 0x040000AD RID: 173
		private object val;
	}
}
