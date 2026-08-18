using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004E7 RID: 1255
	internal class StringEqualsBranchOpcode : QueryConditionalBranchOpcode
	{
		// Token: 0x06002FA7 RID: 12199 RVA: 0x000B7268 File Offset: 0x000B5468
		internal StringEqualsBranchOpcode() : base(OpcodeID.StringEqualsBranch, new StringBranchIndex())
		{
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x000B7278 File Offset: 0x000B5478
		internal override LiteralRelationOpcode ValidateOpcode(Opcode opcode)
		{
			StringEqualsOpcode stringEqualsOpcode = opcode as StringEqualsOpcode;
			if (stringEqualsOpcode != null)
			{
				return stringEqualsOpcode;
			}
			return null;
		}
	}
}
