using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005CA RID: 1482
	internal abstract class RelOp : Op
	{
		// Token: 0x06003B37 RID: 15159 RVA: 0x00117FBC File Offset: 0x001161BC
		internal RelOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06003B38 RID: 15160 RVA: 0x00117FC5 File Offset: 0x001161C5
		internal override bool IsRelOp
		{
			get
			{
				return true;
			}
		}
	}
}
