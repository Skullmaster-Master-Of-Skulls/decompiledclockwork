using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000482 RID: 1154
	internal class TypecastOpcode : Opcode
	{
		// Token: 0x06002CBE RID: 11454 RVA: 0x000AE9DF File Offset: 0x000ACBDF
		internal TypecastOpcode(ValueDataType newType) : base(OpcodeID.Cast)
		{
			this.newType = newType;
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x000AE9EF File Offset: 0x000ACBEF
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && this.newType == ((TypecastOpcode)op).newType;
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x000AEA10 File Offset: 0x000ACC10
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			Value[] values = context.Values;
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				values[i].ConvertTo(context, this.newType);
			}
			return this.next;
		}

		// Token: 0x0400244D RID: 9293
		private ValueDataType newType;
	}
}
