using System;

namespace System.Xml
{
	// Token: 0x020000BA RID: 186
	internal enum ElementProperties : uint
	{
		// Token: 0x040002AE RID: 686
		DEFAULT,
		// Token: 0x040002AF RID: 687
		URI_PARENT,
		// Token: 0x040002B0 RID: 688
		BOOL_PARENT,
		// Token: 0x040002B1 RID: 689
		NAME_PARENT = 4U,
		// Token: 0x040002B2 RID: 690
		EMPTY = 8U,
		// Token: 0x040002B3 RID: 691
		NO_ENTITIES = 16U,
		// Token: 0x040002B4 RID: 692
		HEAD = 32U,
		// Token: 0x040002B5 RID: 693
		BLOCK_WS = 64U,
		// Token: 0x040002B6 RID: 694
		HAS_NS = 128U
	}
}
