using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000491 RID: 1169
	internal class PushStringOpcode : Opcode
	{
		// Token: 0x06002D1D RID: 11549 RVA: 0x000AFC10 File Offset: 0x000ADE10
		internal PushStringOpcode(string literal) : base(OpcodeID.PushString)
		{
			this.literal = literal;
			this.flags |= OpcodeFlags.Literal;
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000AFC30 File Offset: 0x000ADE30
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && this.literal == ((PushStringOpcode)op).literal;
		}

		// Token: 0x06002D1F RID: 11551 RVA: 0x000AFC54 File Offset: 0x000ADE54
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

		// Token: 0x0400245F RID: 9311
		private string literal;
	}
}
