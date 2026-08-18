using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000CA RID: 202
	internal abstract class JoinBaseOp : RelOp
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x0003BEC1 File Offset: 0x0003A0C1
		internal JoinBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000C37 RID: 3127 RVA: 0x0003BF8C File Offset: 0x0003A18C
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}
	}
}
