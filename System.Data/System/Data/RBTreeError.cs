using System;

namespace System.Data
{
	// Token: 0x020000D0 RID: 208
	internal enum RBTreeError
	{
		// Token: 0x040008D5 RID: 2261
		InvalidPageSize = 1,
		// Token: 0x040008D6 RID: 2262
		PagePositionInSlotInUse = 3,
		// Token: 0x040008D7 RID: 2263
		NoFreeSlots,
		// Token: 0x040008D8 RID: 2264
		InvalidStateinInsert,
		// Token: 0x040008D9 RID: 2265
		InvalidNextSizeInDelete = 7,
		// Token: 0x040008DA RID: 2266
		InvalidStateinDelete,
		// Token: 0x040008DB RID: 2267
		InvalidNodeSizeinDelete,
		// Token: 0x040008DC RID: 2268
		InvalidStateinEndDelete,
		// Token: 0x040008DD RID: 2269
		CannotRotateInvalidsuccessorNodeinDelete,
		// Token: 0x040008DE RID: 2270
		IndexOutOFRangeinGetNodeByIndex = 13,
		// Token: 0x040008DF RID: 2271
		RBDeleteFixup,
		// Token: 0x040008E0 RID: 2272
		UnsupportedAccessMethod1,
		// Token: 0x040008E1 RID: 2273
		UnsupportedAccessMethod2,
		// Token: 0x040008E2 RID: 2274
		UnsupportedAccessMethodInNonNillRootSubtree,
		// Token: 0x040008E3 RID: 2275
		AttachedNodeWithZerorbTreeNodeId,
		// Token: 0x040008E4 RID: 2276
		CompareNodeInDataRowTree,
		// Token: 0x040008E5 RID: 2277
		CompareSateliteTreeNodeInDataRowTree,
		// Token: 0x040008E6 RID: 2278
		NestedSatelliteTreeEnumerator
	}
}
