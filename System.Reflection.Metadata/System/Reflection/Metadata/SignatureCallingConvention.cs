using System;

namespace System.Reflection.Metadata
{
	// Token: 0x020000AA RID: 170
	public enum SignatureCallingConvention : byte
	{
		// Token: 0x0400043C RID: 1084
		Default,
		// Token: 0x0400043D RID: 1085
		CDecl,
		// Token: 0x0400043E RID: 1086
		StdCall,
		// Token: 0x0400043F RID: 1087
		ThisCall,
		// Token: 0x04000440 RID: 1088
		FastCall,
		// Token: 0x04000441 RID: 1089
		VarArgs
	}
}
