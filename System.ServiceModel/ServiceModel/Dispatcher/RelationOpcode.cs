using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004E1 RID: 1249
	internal class RelationOpcode : Opcode
	{
		// Token: 0x06002F8E RID: 12174 RVA: 0x000B6E1E File Offset: 0x000B501E
		internal RelationOpcode(RelationOperator op) : this(OpcodeID.Relation, op)
		{
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000B6E29 File Offset: 0x000B5029
		protected RelationOpcode(OpcodeID id, RelationOperator op) : base(id)
		{
			this.op = op;
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x000B6E39 File Offset: 0x000B5039
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && this.op == ((RelationOpcode)op).op;
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x000B6E5C File Offset: 0x000B505C
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			StackFrame secondArg = context.SecondArg;
			Value[] values = context.Values;
			while (topArg.basePtr <= topArg.endPtr)
			{
				values[secondArg.basePtr].Update(context, values[secondArg.basePtr].CompareTo(ref values[topArg.basePtr], this.op));
				topArg.basePtr++;
				secondArg.basePtr++;
			}
			context.PopFrame();
			return this.next;
		}

		// Token: 0x040025E3 RID: 9699
		protected RelationOperator op;
	}
}
