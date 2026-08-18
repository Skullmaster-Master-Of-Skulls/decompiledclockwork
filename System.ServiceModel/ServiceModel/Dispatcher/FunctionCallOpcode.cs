using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000499 RID: 1177
	internal class FunctionCallOpcode : Opcode
	{
		// Token: 0x06002D2F RID: 11567 RVA: 0x000AFFEF File Offset: 0x000AE1EF
		internal FunctionCallOpcode(QueryFunction function) : base(OpcodeID.Function)
		{
			this.function = function;
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x000B0000 File Offset: 0x000AE200
		internal override bool Equals(Opcode op)
		{
			if (base.Equals(op))
			{
				FunctionCallOpcode functionCallOpcode = (FunctionCallOpcode)op;
				return functionCallOpcode.function.Equals(this.function);
			}
			return false;
		}

		// Token: 0x06002D31 RID: 11569 RVA: 0x000B0030 File Offset: 0x000AE230
		internal override Opcode Eval(ProcessingContext context)
		{
			this.function.Eval(context);
			return this.next;
		}

		// Token: 0x04002495 RID: 9365
		private QueryFunction function;
	}
}
