using System;

namespace System.Linq.Expressions.Compiler
{
	// Token: 0x02000281 RID: 641
	internal enum LabelScopeKind
	{
		// Token: 0x04000B57 RID: 2903
		Statement,
		// Token: 0x04000B58 RID: 2904
		Block,
		// Token: 0x04000B59 RID: 2905
		Switch,
		// Token: 0x04000B5A RID: 2906
		Lambda,
		// Token: 0x04000B5B RID: 2907
		Try,
		// Token: 0x04000B5C RID: 2908
		Catch,
		// Token: 0x04000B5D RID: 2909
		Finally,
		// Token: 0x04000B5E RID: 2910
		Filter,
		// Token: 0x04000B5F RID: 2911
		Expression
	}
}
