using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004E3 RID: 1251
	internal class StringEqualsOpcode : LiteralRelationOpcode
	{
		// Token: 0x06002F94 RID: 12180 RVA: 0x000B6EFF File Offset: 0x000B50FF
		internal StringEqualsOpcode(string literal) : base(OpcodeID.StringEquals)
		{
			this.literal = literal;
		}

		// Token: 0x17000B4B RID: 2891
		// (get) Token: 0x06002F95 RID: 12181 RVA: 0x000B6F10 File Offset: 0x000B5110
		internal override object Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x06002F96 RID: 12182 RVA: 0x000B6F18 File Offset: 0x000B5118
		internal override void Add(Opcode op)
		{
			StringEqualsOpcode stringEqualsOpcode = op as StringEqualsOpcode;
			if (stringEqualsOpcode == null)
			{
				base.Add(op);
				return;
			}
			StringEqualsBranchOpcode stringEqualsBranchOpcode = new StringEqualsBranchOpcode();
			this.prev.Replace(this, stringEqualsBranchOpcode);
			stringEqualsBranchOpcode.Add(this);
			stringEqualsBranchOpcode.Add(stringEqualsOpcode);
		}

		// Token: 0x06002F97 RID: 12183 RVA: 0x000B6F58 File Offset: 0x000B5158
		internal override bool Equals(Opcode op)
		{
			if (base.Equals(op))
			{
				StringEqualsOpcode stringEqualsOpcode = (StringEqualsOpcode)op;
				return stringEqualsOpcode.literal == this.literal;
			}
			return false;
		}

		// Token: 0x06002F98 RID: 12184 RVA: 0x000B6F88 File Offset: 0x000B5188
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

		// Token: 0x040025E4 RID: 9700
		private string literal;
	}
}
