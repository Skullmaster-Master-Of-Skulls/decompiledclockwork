using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000BD RID: 189
	internal abstract class RelOp : Op
	{
		// Token: 0x06000BFB RID: 3067 RVA: 0x0003BD16 File Offset: 0x00039F16
		internal RelOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000BFC RID: 3068 RVA: 0x00017938 File Offset: 0x00015B38
		internal override bool IsRelOp
		{
			get
			{
				return true;
			}
		}
	}
}
