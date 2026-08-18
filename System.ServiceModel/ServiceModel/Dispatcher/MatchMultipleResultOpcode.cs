using System;
using System.Collections.Generic;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004EF RID: 1263
	internal class MatchMultipleResultOpcode : MultipleResultOpcode
	{
		// Token: 0x06002FBD RID: 12221 RVA: 0x000B7751 File Offset: 0x000B5951
		internal MatchMultipleResultOpcode() : base(OpcodeID.MatchMultipleResult)
		{
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000B775C File Offset: 0x000B595C
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			bool flag = false;
			if (1 == topArg.Count)
			{
				flag = context.Values[topArg.basePtr].ToBoolean();
			}
			else
			{
				context.Processor.Result = false;
				for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
				{
					if (context.Values[i].ToBoolean())
					{
						flag = true;
						break;
					}
				}
			}
			if (flag)
			{
				ICollection<MessageFilter> matchSet = context.Processor.MatchSet;
				int j = 0;
				int count = this.results.Count;
				while (j < count)
				{
					matchSet.Add((MessageFilter)this.results[j]);
					j++;
				}
			}
			context.PopFrame();
			return this.next;
		}
	}
}
