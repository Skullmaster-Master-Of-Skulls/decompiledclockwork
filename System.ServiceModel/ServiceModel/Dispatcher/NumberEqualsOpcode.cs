using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004E4 RID: 1252
	internal class NumberEqualsOpcode : LiteralRelationOpcode
	{
		// Token: 0x06002F99 RID: 12185 RVA: 0x000B7013 File Offset: 0x000B5213
		internal NumberEqualsOpcode(double literal) : base(OpcodeID.NumberEquals)
		{
			this.literal = literal;
		}

		// Token: 0x17000B4C RID: 2892
		// (get) Token: 0x06002F9A RID: 12186 RVA: 0x000B7024 File Offset: 0x000B5224
		internal override object Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x06002F9B RID: 12187 RVA: 0x000B7034 File Offset: 0x000B5234
		internal override void Add(Opcode op)
		{
			NumberEqualsOpcode numberEqualsOpcode = op as NumberEqualsOpcode;
			if (numberEqualsOpcode == null)
			{
				base.Add(op);
				return;
			}
			NumberEqualsBranchOpcode numberEqualsBranchOpcode = new NumberEqualsBranchOpcode();
			this.prev.Replace(this, numberEqualsBranchOpcode);
			numberEqualsBranchOpcode.Add(this);
			numberEqualsBranchOpcode.Add(numberEqualsOpcode);
		}

		// Token: 0x06002F9C RID: 12188 RVA: 0x000B7074 File Offset: 0x000B5274
		internal override bool Equals(Opcode op)
		{
			if (base.Equals(op))
			{
				NumberEqualsOpcode numberEqualsOpcode = (NumberEqualsOpcode)op;
				return numberEqualsOpcode.literal == this.literal;
			}
			return false;
		}

		// Token: 0x06002F9D RID: 12189 RVA: 0x000B70A4 File Offset: 0x000B52A4
		internal override Opcode Eval(ProcessingContext context)
		{
			Value[] values = context.Values;
			StackFrame topArg = context.TopArg;
			if (1 == topArg.Count)
			{
				values[topArg.basePtr].Update(context, values[topArg.basePtr].Equals(this.literal));
			}
			else
			{
				for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
				{
					values[i].Update(context, values[i].Equals(this.literal));
				}
			}
			return this.next;
		}

		// Token: 0x040025E5 RID: 9701
		private double literal;
	}
}
