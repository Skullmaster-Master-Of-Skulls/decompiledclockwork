using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000E3 RID: 227
	internal sealed class SimpleRule : Rule
	{
		// Token: 0x06000CB2 RID: 3250 RVA: 0x0003C48E File Offset: 0x0003A68E
		internal SimpleRule(OpType opType, Rule.ProcessNodeDelegate processDelegate) : base(opType, processDelegate)
		{
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x0003C498 File Offset: 0x0003A698
		internal override bool Match(Node node)
		{
			return node.Op.OpType == base.RuleOpType;
		}
	}
}
