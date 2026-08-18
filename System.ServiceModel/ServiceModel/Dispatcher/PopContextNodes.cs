using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000490 RID: 1168
	internal class PopContextNodes : Opcode
	{
		// Token: 0x06002D1B RID: 11547 RVA: 0x000AFBF8 File Offset: 0x000ADDF8
		internal PopContextNodes() : base(OpcodeID.PopContextNodes)
		{
		}

		// Token: 0x06002D1C RID: 11548 RVA: 0x000AFC02 File Offset: 0x000ADE02
		internal override Opcode Eval(ProcessingContext context)
		{
			context.PopContextSequenceFrame();
			return this.next;
		}
	}
}
