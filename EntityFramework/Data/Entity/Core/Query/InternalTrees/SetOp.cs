using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020005EE RID: 1518
	internal abstract class SetOp : RelOp
	{
		// Token: 0x06003C2D RID: 15405 RVA: 0x00118CBB File Offset: 0x00116EBB
		internal SetOp(OpType opType, VarVec outputs, VarMap left, VarMap right) : this(opType)
		{
			this.m_varMap = new VarMap[2];
			this.m_varMap[0] = left;
			this.m_varMap[1] = right;
			this.m_outputVars = outputs;
		}

		// Token: 0x06003C2E RID: 15406 RVA: 0x00118CEA File Offset: 0x00116EEA
		protected SetOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06003C2F RID: 15407 RVA: 0x00118CF3 File Offset: 0x00116EF3
		internal override int Arity
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06003C30 RID: 15408 RVA: 0x00118CF6 File Offset: 0x00116EF6
		internal VarMap[] VarMap
		{
			get
			{
				return this.m_varMap;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06003C31 RID: 15409 RVA: 0x00118CFE File Offset: 0x00116EFE
		internal VarVec Outputs
		{
			get
			{
				return this.m_outputVars;
			}
		}

		// Token: 0x0400168E RID: 5774
		private readonly VarMap[] m_varMap;

		// Token: 0x0400168F RID: 5775
		private readonly VarVec m_outputVars;
	}
}
