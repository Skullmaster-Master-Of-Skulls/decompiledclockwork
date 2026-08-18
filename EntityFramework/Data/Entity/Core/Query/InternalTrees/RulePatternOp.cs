using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200063D RID: 1597
	internal abstract class RulePatternOp : Op
	{
		// Token: 0x06003EC3 RID: 16067 RVA: 0x0011FADA File Offset: 0x0011DCDA
		internal RulePatternOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x0011FAE3 File Offset: 0x0011DCE3
		internal override bool IsRulePatternOp
		{
			get
			{
				return true;
			}
		}
	}
}
