using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200048D RID: 1165
	internal class PushContextPositionOpcode : Opcode
	{
		// Token: 0x06002D15 RID: 11541 RVA: 0x000AFBB0 File Offset: 0x000ADDB0
		internal PushContextPositionOpcode() : base(OpcodeID.PushPosition)
		{
		}

		// Token: 0x06002D16 RID: 11542 RVA: 0x000AFBBA File Offset: 0x000ADDBA
		internal override Opcode Eval(ProcessingContext context)
		{
			context.TransferSequencePositions();
			return this.next;
		}
	}
}
