using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200047F RID: 1151
	internal class EndBooleanOpcode : ApplyBooleanOpcode
	{
		// Token: 0x06002CB7 RID: 11447 RVA: 0x000AE938 File Offset: 0x000ACB38
		internal EndBooleanOpcode(Opcode jump, bool test) : base(OpcodeID.EndBoolean, jump, test)
		{
		}

		// Token: 0x06002CB8 RID: 11448 RVA: 0x000AE944 File Offset: 0x000ACB44
		internal override Opcode Eval(ProcessingContext context)
		{
			int num = base.UpdateResultMask(context);
			context.PopFrame();
			context.PopSequenceFrame();
			if (num == 0)
			{
				return base.Jump;
			}
			return this.next;
		}
	}
}
