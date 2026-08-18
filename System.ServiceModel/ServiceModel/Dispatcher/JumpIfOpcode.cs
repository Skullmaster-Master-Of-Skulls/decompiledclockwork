using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200047C RID: 1148
	internal class JumpIfOpcode : JumpOpcode
	{
		// Token: 0x06002CAB RID: 11435 RVA: 0x000AE679 File Offset: 0x000AC879
		internal JumpIfOpcode(Opcode jump, bool test) : this(OpcodeID.JumpIfNot, jump, test)
		{
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x000AE684 File Offset: 0x000AC884
		protected JumpIfOpcode(OpcodeID id, Opcode jump, bool test) : base(id, jump)
		{
			this.test = test;
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000AE695 File Offset: 0x000AC895
		internal bool Test
		{
			get
			{
				return this.test;
			}
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x000AE69D File Offset: 0x000AC89D
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && this.test == ((JumpIfOpcode)op).test;
		}

		// Token: 0x06002CAF RID: 11439 RVA: 0x000AE6C0 File Offset: 0x000AC8C0
		internal override Opcode Eval(ProcessingContext context)
		{
			StackFrame topArg = context.TopArg;
			for (int i = topArg.basePtr; i <= topArg.endPtr; i++)
			{
				if (this.test == context.Values[i].Boolean)
				{
					return this.next;
				}
			}
			return base.Jump;
		}

		// Token: 0x0400244A RID: 9290
		protected bool test;
	}
}
