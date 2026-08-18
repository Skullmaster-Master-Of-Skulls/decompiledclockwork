using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004EB RID: 1259
	internal class MatchResultOpcode : ResultOpcode
	{
		// Token: 0x06002FAE RID: 12206 RVA: 0x000B7355 File Offset: 0x000B5555
		internal MatchResultOpcode() : base(OpcodeID.MatchResult)
		{
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000B735F File Offset: 0x000B555F
		internal override Opcode Eval(ProcessingContext context)
		{
			context.Processor.Result = this.IsSuccess(context);
			context.PopFrame();
			return this.next;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x000B7380 File Offset: 0x000B5580
		protected bool IsSuccess(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			if (1 == topArg.Count)
			{
				return context.Values[topArg.basePtr].ToBoolean();
			}
			context.Processor.Result = false;
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				if (context.Values[i].ToBoolean())
				{
					return true;
				}
			}
			return false;
		}
	}
}
