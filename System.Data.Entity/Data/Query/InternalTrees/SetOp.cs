using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000DB RID: 219
	internal abstract class SetOp : RelOp
	{
		// Token: 0x06000C88 RID: 3208 RVA: 0x0003C2C3 File Offset: 0x0003A4C3
		internal SetOp(OpType opType, VarVec outputs, VarMap left, VarMap right) : this(opType)
		{
			this.m_varMap = new VarMap[2];
			this.m_varMap[0] = left;
			this.m_varMap[1] = right;
			this.m_outputVars = outputs;
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0003BEC1 File Offset: 0x0003A0C1
		protected SetOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x00033532 File Offset: 0x00031732
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0003C2F2 File Offset: 0x0003A4F2
		internal VarMap[] VarMap
		{
			get
			{
				return this.m_varMap;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x0003C2FA File Offset: 0x0003A4FA
		internal VarVec Outputs
		{
			get
			{
				return this.m_outputVars;
			}
		}

		// Token: 0x04000981 RID: 2433
		private VarMap[] m_varMap;

		// Token: 0x04000982 RID: 2434
		private VarVec m_outputVars;
	}
}
