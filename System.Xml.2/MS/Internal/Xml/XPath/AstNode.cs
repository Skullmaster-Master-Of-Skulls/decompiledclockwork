using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000007 RID: 7
	internal abstract class AstNode
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12
		public abstract AstNode.AstType Type { get; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13
		public abstract XPathResultType ReturnType { get; }

		// Token: 0x020002F9 RID: 761
		public enum AstType
		{
			// Token: 0x040013C5 RID: 5061
			Axis,
			// Token: 0x040013C6 RID: 5062
			Operator,
			// Token: 0x040013C7 RID: 5063
			Filter,
			// Token: 0x040013C8 RID: 5064
			ConstantOperand,
			// Token: 0x040013C9 RID: 5065
			Function,
			// Token: 0x040013CA RID: 5066
			Group,
			// Token: 0x040013CB RID: 5067
			Root,
			// Token: 0x040013CC RID: 5068
			Variable,
			// Token: 0x040013CD RID: 5069
			Error
		}
	}
}
