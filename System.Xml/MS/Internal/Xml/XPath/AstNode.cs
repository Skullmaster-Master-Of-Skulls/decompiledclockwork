using System;
using System.Xml.XPath;

namespace MS.Internal.Xml.XPath
{
	// Token: 0x02000123 RID: 291
	internal abstract class AstNode
	{
		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001156 RID: 4438
		public abstract AstNode.AstType Type { get; }

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001157 RID: 4439
		public abstract XPathResultType ReturnType { get; }

		// Token: 0x02000124 RID: 292
		public enum AstType
		{
			// Token: 0x04000B16 RID: 2838
			Axis,
			// Token: 0x04000B17 RID: 2839
			Operator,
			// Token: 0x04000B18 RID: 2840
			Filter,
			// Token: 0x04000B19 RID: 2841
			ConstantOperand,
			// Token: 0x04000B1A RID: 2842
			Function,
			// Token: 0x04000B1B RID: 2843
			Group,
			// Token: 0x04000B1C RID: 2844
			Root,
			// Token: 0x04000B1D RID: 2845
			Variable,
			// Token: 0x04000B1E RID: 2846
			Error
		}
	}
}
