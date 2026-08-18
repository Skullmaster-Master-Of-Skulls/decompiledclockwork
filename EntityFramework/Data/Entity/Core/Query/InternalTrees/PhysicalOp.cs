using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000603 RID: 1539
	internal abstract class PhysicalOp : Op
	{
		// Token: 0x06003CB2 RID: 15538 RVA: 0x00119610 File Offset: 0x00117810
		internal PhysicalOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x00119619 File Offset: 0x00117819
		internal override bool IsPhysicalOp
		{
			get
			{
				return true;
			}
		}
	}
}
