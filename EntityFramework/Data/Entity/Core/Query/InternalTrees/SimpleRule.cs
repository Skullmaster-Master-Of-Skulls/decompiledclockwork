using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000626 RID: 1574
	internal sealed class SimpleRule : Rule
	{
		// Token: 0x06003D66 RID: 15718 RVA: 0x0011B28C File Offset: 0x0011948C
		internal SimpleRule(OpType opType, Rule.ProcessNodeDelegate processDelegate) : base(opType, processDelegate)
		{
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x0011B296 File Offset: 0x00119496
		internal override bool Match(Node node)
		{
			return node.Op.OpType == base.RuleOpType;
		}
	}
}
