using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200050F RID: 1295
	internal enum XPathExprType : byte
	{
		// Token: 0x0400264A RID: 9802
		Unknown,
		// Token: 0x0400264B RID: 9803
		Or,
		// Token: 0x0400264C RID: 9804
		And,
		// Token: 0x0400264D RID: 9805
		Relational,
		// Token: 0x0400264E RID: 9806
		Union,
		// Token: 0x0400264F RID: 9807
		LocationPath,
		// Token: 0x04002650 RID: 9808
		RelativePath,
		// Token: 0x04002651 RID: 9809
		PathStep,
		// Token: 0x04002652 RID: 9810
		XsltVariable,
		// Token: 0x04002653 RID: 9811
		String,
		// Token: 0x04002654 RID: 9812
		Number,
		// Token: 0x04002655 RID: 9813
		Function,
		// Token: 0x04002656 RID: 9814
		XsltFunction,
		// Token: 0x04002657 RID: 9815
		Math,
		// Token: 0x04002658 RID: 9816
		Filter,
		// Token: 0x04002659 RID: 9817
		Path
	}
}
