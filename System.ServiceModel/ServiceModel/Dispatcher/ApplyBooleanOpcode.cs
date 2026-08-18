using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200047D RID: 1149
	internal class ApplyBooleanOpcode : JumpIfOpcode
	{
		// Token: 0x06002CB0 RID: 11440 RVA: 0x000AE710 File Offset: 0x000AC910
		internal ApplyBooleanOpcode(Opcode jump, bool test) : this(OpcodeID.ApplyBoolean, jump, test)
		{
		}

		// Token: 0x06002CB1 RID: 11441 RVA: 0x000AE71C File Offset: 0x000AC91C
		protected ApplyBooleanOpcode(OpcodeID id, Opcode jump, bool test) : base(id, jump, test)
		{
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x000AE728 File Offset: 0x000AC928
		internal override Opcode Eval(ProcessingContext context)
		{
			int num = this.UpdateResultMask(context);
			context.PopFrame();
			if (num == 0)
			{
				return base.Jump;
			}
			return this.next;
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x000AE754 File Offset: 0x000AC954
		protected int UpdateResultMask(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			Value[] values = context.Values;
			int num = 0;
			int i = secondArg.basePtr;
			int num2 = topArg.basePtr;
			while (i <= secondArg.endPtr)
			{
				if (this.test == values[i].Boolean)
				{
					bool boolean = values[num2].Boolean;
					if (this.test == boolean)
					{
						num++;
					}
					values[i].Boolean = boolean;
					num2++;
				}
				i++;
			}
			return num;
		}
	}
}
