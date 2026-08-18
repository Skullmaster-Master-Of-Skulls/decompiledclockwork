using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000492 RID: 1170
	internal class PushNumberOpcode : Opcode
	{
		// Token: 0x06002D20 RID: 11552 RVA: 0x000AFC85 File Offset: 0x000ADE85
		internal PushNumberOpcode(double literal) : base(OpcodeID.PushDouble)
		{
			this.literal = literal;
			this.flags |= OpcodeFlags.Literal;
		}

		// Token: 0x06002D21 RID: 11553 RVA: 0x000AFCA5 File Offset: 0x000ADEA5
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && this.literal == ((PushNumberOpcode)op).literal;
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000AFCC8 File Offset: 0x000ADEC8
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

		// Token: 0x04002460 RID: 9312
		private double literal;
	}
}
