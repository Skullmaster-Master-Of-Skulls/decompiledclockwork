using System;

namespace System.Data.Query.InternalTrees
{
	// Token: 0x020000C6 RID: 198
	internal abstract class ScanTableBaseOp : RelOp
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0003BEB1 File Offset: 0x0003A0B1
		protected ScanTableBaseOp(OpType opType, Table table) : base(opType)
		{
			this.m_table = table;
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x0003BEC1 File Offset: 0x0003A0C1
		protected ScanTableBaseOp(OpType opType) : base(opType)
		{
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000C21 RID: 3105 RVA: 0x0003BECA File Offset: 0x0003A0CA
		internal Table Table
		{
			get
			{
				return this.m_table;
			}
		}

		// Token: 0x0400095F RID: 2399
		private Table m_table;
	}
}
