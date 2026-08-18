using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020004B7 RID: 1207
	internal class MathOpcode : Opcode
	{
		// Token: 0x06002E0A RID: 11786 RVA: 0x000B3A28 File Offset: 0x000B1C28
		internal MathOpcode(OpcodeID id, MathOperator op) : base(id)
		{
			this.mathOp = op;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000B3A38 File Offset: 0x000B1C38
		internal override bool Equals(Opcode op)
		{
			return base.Equals(op) && this.mathOp == ((MathOpcode)op).mathOp;
		}

		// Token: 0x04002508 RID: 9480
		private MathOperator mathOp;
	}
}
