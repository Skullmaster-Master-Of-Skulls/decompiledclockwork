using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004AE RID: 1198
	internal class NumberIntervalBranchOpcode : QueryConditionalBranchOpcode
	{
		// Token: 0x06002DD5 RID: 11733 RVA: 0x000B2B2A File Offset: 0x000B0D2A
		internal NumberIntervalBranchOpcode() : base(OpcodeID.NumberIntervalBranch, new IntervalBranchIndex())
		{
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000B2B3C File Offset: 0x000B0D3C
		internal override LiteralRelationOpcode ValidateOpcode(Opcode opcode)
		{
			NumberIntervalOpcode numberIntervalOpcode = opcode as NumberIntervalOpcode;
			if (numberIntervalOpcode != null)
			{
				return numberIntervalOpcode;
			}
			return null;
		}
	}
}
