using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005C9 RID: 1481
	internal abstract class AncillaryOp : Op
	{
		// Token: 0x06003B35 RID: 15157 RVA: 0x00117FB0 File Offset: 0x001161B0
		internal AncillaryOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000909 RID: 2313
		// (get) Token: 0x06003B36 RID: 15158 RVA: 0x00117FB9 File Offset: 0x001161B9
		internal override bool IsAncillaryOp
		{
			get
			{
				return true;
			}
		}
	}
}
