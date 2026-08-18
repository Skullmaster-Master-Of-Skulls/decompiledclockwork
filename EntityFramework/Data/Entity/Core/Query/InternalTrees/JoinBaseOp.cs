using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005E3 RID: 1507
	internal abstract class JoinBaseOp : RelOp
	{
		// Token: 0x06003BF7 RID: 15351 RVA: 0x0011898F File Offset: 0x00116B8F
		internal JoinBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06003BF8 RID: 15352 RVA: 0x00118998 File Offset: 0x00116B98
		internal override int Arity
		{
			get
			{
				return 3;
			}
		}
	}
}
