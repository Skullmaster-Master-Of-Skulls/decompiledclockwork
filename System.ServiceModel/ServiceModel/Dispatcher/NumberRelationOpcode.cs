using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004AB RID: 1195
	internal class NumberRelationOpcode : LiteralRelationOpcode
	{
		// Token: 0x06002DC3 RID: 11715 RVA: 0x000B2848 File Offset: 0x000B0A48
		internal NumberRelationOpcode(double literal, RelationOperator op) : this(OpcodeID.NumberRelation, literal, op)
		{
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000B2854 File Offset: 0x000B0A54
		protected NumberRelationOpcode(OpcodeID id, double literal, RelationOperator op) : base(id)
		{
			this.literal = literal;
			this.op = op;
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06002DC5 RID: 11717 RVA: 0x000B286B File Offset: 0x000B0A6B
		internal override object Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000B2878 File Offset: 0x000B0A78
		internal override bool Equals(Opcode opcode)
		{
			if (base.Equals(opcode))
			{
				NumberRelationOpcode numberRelationOpcode = (NumberRelationOpcode)opcode;
				return numberRelationOpcode.op == this.op && numberRelationOpcode.literal == this.literal;
			}
			return false;
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000B28B8 File Offset: 0x000B0AB8
		internal override Opcode Eval(ProcessingContext context)
		{
			Value[] values = context.Values;
			StackFrame topArg = context.TopArg;
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				values[i].Update(context, values[i].CompareTo(this.literal, this.op));
			}
			return this.next;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000B2914 File Offset: 0x000B0B14
		internal Interval ToInterval()
		{
			return new Interval(this.literal, this.op);
		}

		// Token: 0x040024E3 RID: 9443
		private double literal;

		// Token: 0x040024E4 RID: 9444
		private RelationOperator op;
	}
}
