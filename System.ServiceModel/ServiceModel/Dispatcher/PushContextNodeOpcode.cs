using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200048C RID: 1164
	internal class PushContextNodeOpcode : Opcode
	{
		// Token: 0x06002D13 RID: 11539 RVA: 0x000AFB61 File Offset: 0x000ADD61
		internal PushContextNodeOpcode() : base(OpcodeID.PushContextNode)
		{
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000AFB6C File Offset: 0x000ADD6C
		internal override Opcode Eval(ProcessingContext context)
		{
			context.PushContextSequenceFrame();
			NodeSequence nodeSequence = context.CreateSequence();
			nodeSequence.StartNodeset();
			nodeSequence.Add(context.Processor.ContextNode);
			nodeSequence.StopNodeset();
			context.PushSequence(nodeSequence);
			return this.next;
		}
	}
}
