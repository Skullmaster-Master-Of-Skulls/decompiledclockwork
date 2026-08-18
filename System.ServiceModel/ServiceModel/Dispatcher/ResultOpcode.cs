using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004EA RID: 1258
	internal abstract class ResultOpcode : Opcode
	{
		// Token: 0x06002FAD RID: 12205 RVA: 0x000B733E File Offset: 0x000B553E
		internal ResultOpcode(OpcodeID id) : base(id)
		{
			this.flags |= OpcodeFlags.Result;
		}
	}
}
