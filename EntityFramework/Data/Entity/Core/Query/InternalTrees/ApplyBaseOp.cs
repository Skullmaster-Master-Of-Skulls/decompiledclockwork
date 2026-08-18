using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005CB RID: 1483
	internal abstract class ApplyBaseOp : RelOp
	{
		// Token: 0x06003B39 RID: 15161 RVA: 0x00117FC8 File Offset: 0x001161C8
		internal ApplyBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06003B3A RID: 15162 RVA: 0x00117FD1 File Offset: 0x001161D1
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}
	}
}
