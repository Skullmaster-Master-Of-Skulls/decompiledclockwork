using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000493 RID: 1171
	internal class PushBooleanOpcode : Opcode
	{
		// Token: 0x06002D23 RID: 11555 RVA: 0x000AFCF9 File Offset: 0x000ADEF9
		internal PushBooleanOpcode(bool literal) : base(OpcodeID.PushBool)
		{
			this.literal = literal;
			this.flags |= OpcodeFlags.Literal;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x000AFD19 File Offset: 0x000ADF19
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && this.literal == ((PushBooleanOpcode)op).literal;
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000AFD3C File Offset: 0x000ADF3C
		internal override Opcode Eval(ProcessingContext context)
		{
			context.PushFrame();
			int iterationCount = context.IterationCount;
			if (iterationCount > 0)
			{
				context.Push(this.literal, iterationCount);
			}
			return this.next;
		}

		// Token: 0x04002461 RID: 9313
		private bool literal;
	}
}
