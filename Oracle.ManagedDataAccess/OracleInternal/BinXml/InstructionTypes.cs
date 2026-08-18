using System;

namespace OracleInternal.BinXml
{
	// Token: 0x0200001B RID: 27
	internal enum InstructionTypes
	{
		// Token: 0x04000104 RID: 260
		None = -1,
		// Token: 0x04000105 RID: 261
		Element = 1,
		// Token: 0x04000106 RID: 262
		Attribute,
		// Token: 0x04000107 RID: 263
		Cdata = 4,
		// Token: 0x04000108 RID: 264
		Comment = 8,
		// Token: 0x04000109 RID: 265
		Namespace = 22,
		// Token: 0x0400010A RID: 266
		Token = 50
	}
}
