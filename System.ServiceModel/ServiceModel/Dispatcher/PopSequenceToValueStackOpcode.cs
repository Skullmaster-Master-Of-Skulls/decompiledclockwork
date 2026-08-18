using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200048E RID: 1166
	internal class PopSequenceToValueStackOpcode : Opcode
	{
		// Token: 0x06002D17 RID: 11543 RVA: 0x000AFBC8 File Offset: 0x000ADDC8
		internal PopSequenceToValueStackOpcode() : base(OpcodeID.PopSequenceToValueStack)
		{
		}

		// Token: 0x06002D18 RID: 11544 RVA: 0x000AFBD2 File Offset: 0x000ADDD2
		internal override Opcode Eval(ProcessingContext context)
		{
			context.PopSequenceFrameToValueStack();
			return this.next;
		}
	}
}
