using System;

namespace System.Data
{
	// Token: 0x0200011A RID: 282
	internal enum RBTreeError
	{
		// Token: 0x040005A5 RID: 1445
		InvalidPageSize = 1,
		// Token: 0x040005A6 RID: 1446
		PagePositionInSlotInUse = 3,
		// Token: 0x040005A7 RID: 1447
		NoFreeSlots,
		// Token: 0x040005A8 RID: 1448
		InvalidStateinInsert,
		// Token: 0x040005A9 RID: 1449
		InvalidNextSizeInDelete = 7,
		// Token: 0x040005AA RID: 1450
		InvalidStateinDelete,
		// Token: 0x040005AB RID: 1451
		InvalidNodeSizeinDelete,
		// Token: 0x040005AC RID: 1452
		InvalidStateinEndDelete,
		// Token: 0x040005AD RID: 1453
		CannotRotateInvalidsuccessorNodeinDelete,
		// Token: 0x040005AE RID: 1454
		IndexOutOFRangeinGetNodeByIndex = 13,
		// Token: 0x040005AF RID: 1455
		RBDeleteFixup,
		// Token: 0x040005B0 RID: 1456
		UnsupportedAccessMethod1,
		// Token: 0x040005B1 RID: 1457
		UnsupportedAccessMethod2,
		// Token: 0x040005B2 RID: 1458
		UnsupportedAccessMethodInNonNillRootSubtree,
		// Token: 0x040005B3 RID: 1459
		AttachedNodeWithZerorbTreeNodeId,
		// Token: 0x040005B4 RID: 1460
		CompareNodeInDataRowTree,
		// Token: 0x040005B5 RID: 1461
		CompareSateliteTreeNodeInDataRowTree,
		// Token: 0x040005B6 RID: 1462
		NestedSatelliteTreeEnumerator
	}
}
