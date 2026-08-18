using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000BE RID: 190
	internal abstract class AncillaryOp : Op
	{
		// Token: 0x06000BFD RID: 3069 RVA: 0x0003BD16 File Offset: 0x00039F16
		internal AncillaryOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000BFE RID: 3070 RVA: 0x00017938 File Offset: 0x00015B38
		internal override bool IsAncillaryOp
		{
			get
			{
				return true;
			}
		}
	}
}
