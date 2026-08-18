using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004E2 RID: 1250
	internal abstract class LiteralRelationOpcode : Opcode
	{
		// Token: 0x06002F92 RID: 12178 RVA: 0x000B6EE7 File Offset: 0x000B50E7
		internal LiteralRelationOpcode(OpcodeID id) : base(id)
		{
			this.flags |= OpcodeFlags.Literal;
		}

		// Token: 0x17000B4A RID: 2890
		// (get) Token: 0x06002F93 RID: 12179
		internal abstract object Literal { get; }
	}
}
