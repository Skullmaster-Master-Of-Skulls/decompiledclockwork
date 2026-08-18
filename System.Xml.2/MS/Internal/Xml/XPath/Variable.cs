using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x0200003E RID: 62
	internal class Variable : AstNode
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x00007AE4 File Offset: 0x00005CE4
		public Variable(string name, string prefix)
		{
			this.localname = name;
			this.prefix = prefix;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00007AFA File Offset: 0x00005CFA
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Variable;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00007AFD File Offset: 0x00005CFD
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00007B00 File Offset: 0x00005D00
		public string Localname
		{
			get
			{
				return this.localname;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00007B08 File Offset: 0x00005D08
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x040000D1 RID: 209
		private string localname;

		// Token: 0x040000D2 RID: 210
		private string prefix;
	}
}
