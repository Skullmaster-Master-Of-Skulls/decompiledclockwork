using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200048F RID: 1167
	internal class PopSequenceToSequenceStackOpcode : Opcode
	{
		// Token: 0x06002D19 RID: 11545 RVA: 0x000AFBE0 File Offset: 0x000ADDE0
		internal PopSequenceToSequenceStackOpcode() : base(OpcodeID.PopSequenceToSequenceStack)
		{
		}

		// Token: 0x06002D1A RID: 11546 RVA: 0x000AFBEA File Offset: 0x000ADDEA
		internal override Opcode Eval(ProcessingContext context)
		{
			context.PushSequenceFrameFromValueStack();
			return this.next;
		}
	}
}
