using System;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000484 RID: 1156
	internal class QueryBranch
	{
		// Token: 0x06002CC4 RID: 11460 RVA: 0x000AEAB3 File Offset: 0x000ACCB3
		internal QueryBranch(Opcode branch, int id)
		{
			this.branch = branch;
			this.id = id;
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000AEAC9 File Offset: 0x000ACCC9
		internal Opcode Branch
		{
			get
			{
				return this.branch;
			}
		}

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000AEAD1 File Offset: 0x000ACCD1
		internal int ID
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04002450 RID: 9296
		internal Opcode branch;

		// Token: 0x04002451 RID: 9297
		internal int id;
	}
}
