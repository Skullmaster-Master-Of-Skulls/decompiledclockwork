using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004E9 RID: 1257
	internal class NumberEqualsBranchOpcode : QueryConditionalBranchOpcode
	{
		// Token: 0x06002FAB RID: 12203 RVA: 0x000B7312 File Offset: 0x000B5512
		internal NumberEqualsBranchOpcode() : base(OpcodeID.NumberEqualsBranch, new NumberBranchIndex())
		{
		}

		// Token: 0x06002FAC RID: 12204 RVA: 0x000B7324 File Offset: 0x000B5524
		internal override LiteralRelationOpcode ValidateOpcode(Opcode opcode)
		{
			NumberEqualsOpcode numberEqualsOpcode = opcode as NumberEqualsOpcode;
			if (numberEqualsOpcode != null)
			{
				return numberEqualsOpcode;
			}
			return null;
		}
	}
}
