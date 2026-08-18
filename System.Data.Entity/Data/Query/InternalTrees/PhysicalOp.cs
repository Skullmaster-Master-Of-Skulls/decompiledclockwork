using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000BF RID: 191
	internal abstract class PhysicalOp : Op
	{
		// Token: 0x06000BFF RID: 3071 RVA: 0x0003BD16 File Offset: 0x00039F16
		internal PhysicalOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000C00 RID: 3072 RVA: 0x00017938 File Offset: 0x00015B38
		internal override bool IsPhysicalOp
		{
			get
			{
				return true;
			}
		}
	}
}
