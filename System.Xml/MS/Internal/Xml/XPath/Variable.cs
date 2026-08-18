using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000161 RID: 353
	internal class Variable : AstNode
	{
		// Token: 0x06001320 RID: 4896 RVA: 0x00053051 File Offset: 0x00052051
		public Variable(string name, string prefix)
		{
			this.localname = name;
			this.prefix = prefix;
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06001321 RID: 4897 RVA: 0x00053067 File Offset: 0x00052067
		public override AstNode.AstType Type
		{
			get
			{
				return AstNode.AstType.Variable;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0005306A File Offset: 0x0005206A
		public override XPathResultType ReturnType
		{
			get
			{
				return XPathResultType.Any;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0005306D File Offset: 0x0005206D
		public string Localname
		{
			get
			{
				return this.localname;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x00053075 File Offset: 0x00052075
		public string Prefix
		{
			get
			{
				return this.prefix;
			}
		}

		// Token: 0x04000BE2 RID: 3042
		private string localname;

		// Token: 0x04000BE3 RID: 3043
		private string prefix;
	}
}
