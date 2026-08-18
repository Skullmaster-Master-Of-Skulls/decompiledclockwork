using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C0 RID: 192
	internal abstract class RulePatternOp : Op
	{
		// Token: 0x06000C01 RID: 3073 RVA: 0x0003BD16 File Offset: 0x00039F16
		internal RulePatternOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000C02 RID: 3074 RVA: 0x00017938 File Offset: 0x00015B38
		internal override bool IsRulePatternOp
		{
			get
			{
				return true;
			}
		}
	}
}
