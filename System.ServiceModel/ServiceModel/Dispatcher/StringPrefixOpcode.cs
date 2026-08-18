using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004DA RID: 1242
	internal class StringPrefixOpcode : LiteralRelationOpcode
	{
		// Token: 0x06002F1C RID: 12060 RVA: 0x000B60AE File Offset: 0x000B42AE
		internal StringPrefixOpcode(string literal) : base(OpcodeID.StringPrefix)
		{
			this.literal = literal;
		}

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x000B60BF File Offset: 0x000B42BF
		internal override object Literal
		{
			get
			{
				return this.literal;
			}
		}

		// Token: 0x06002F1E RID: 12062 RVA: 0x000B60C8 File Offset: 0x000B42C8
		internal override void Add(Opcode op)
		{
			StringPrefixOpcode stringPrefixOpcode = op as StringPrefixOpcode;
			if (stringPrefixOpcode == null)
			{
				base.Add(op);
				return;
			}
			StringPrefixBranchOpcode stringPrefixBranchOpcode = new StringPrefixBranchOpcode();
			this.prev.Replace(this, stringPrefixBranchOpcode);
			stringPrefixBranchOpcode.Add(this);
			stringPrefixBranchOpcode.Add(stringPrefixOpcode);
		}

		// Token: 0x06002F1F RID: 12063 RVA: 0x000B6108 File Offset: 0x000B4308
		internal override bool Equals(Opcode op)
		{
			if (base.Equals(op))
			{
				StringPrefixOpcode stringPrefixOpcode = (StringPrefixOpcode)op;
				return stringPrefixOpcode.literal == this.literal;
			}
			return false;
		}

		// Token: 0x06002F20 RID: 12064 RVA: 0x000B6138 File Offset: 0x000B4338
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			if (1 == topArg.Count)
			{
				string @string = context.Values[topArg.basePtr].String;
				context.Values[topArg.basePtr].Boolean = @string.StartsWith(this.literal, StringComparison.Ordinal);
			}
			else
			{
				for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
				{
					string string2 = context.Values[i].String;
					context.Values[i].Boolean = string2.StartsWith(this.literal, StringComparison.Ordinal);
				}
			}
			return this.next;
		}

		// Token: 0x040025BB RID: 9659
		private string literal;
	}
}
