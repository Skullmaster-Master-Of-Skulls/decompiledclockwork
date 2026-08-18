using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000CF RID: 207
	internal abstract class ApplyBaseOp : RelOp
	{
		// Token: 0x06000C49 RID: 3145 RVA: 0x0003BEC1 File Offset: 0x0003A0C1
		internal ApplyBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000C4A RID: 3146 RVA: 0x00033532 File Offset: 0x00031732
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}
	}
}
