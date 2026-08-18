using System;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x0200061F RID: 1567
	internal abstract class ScanTableBaseOp : RelOp
	{
		// Token: 0x06003D4A RID: 15690 RVA: 0x0011B000 File Offset: 0x00119200
		protected ScanTableBaseOp(OpType opType, Table table) : base(opType)
		{
			this.m_table = table;
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x0011B010 File Offset: 0x00119210
		protected ScanTableBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x06003D4C RID: 15692 RVA: 0x0011B019 File Offset: 0x00119219
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x0400172B RID: 5931
		private readonly Table m_table;
	}
}
