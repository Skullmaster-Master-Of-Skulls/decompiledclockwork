using System;

namespace OracleInternal.BinXml
{
	// Token: 0x0200001E RID: 30
	internal enum NodeTypes
	{
		// Token: 0x04000136 RID: 310
		None = -1,
		// Token: 0x04000137 RID: 311
		Element = 1,
		// Token: 0x04000138 RID: 312
		Attribute,
		// Token: 0x04000139 RID: 313
		Cdata = 4,
		// Token: 0x0400013A RID: 314
		Comment = 8,
		// Token: 0x0400013B RID: 315
		Namespace = 22
	}
}
