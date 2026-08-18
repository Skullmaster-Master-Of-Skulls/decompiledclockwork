using System;
using System.Data.Entity.Core.Common.Utils;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Validation
{
	// Token: 0x0200048E RID: 1166
	internal abstract class CellRelation : InternalBase
	{
		// Token: 0x06002B1E RID: 11038 RVA: 0x000D0975 File Offset: 0x000CEB75
		protected CellRelation(int cellNumber)
		{
			this.m_cellNumber = cellNumber;
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06002B1F RID: 11039 RVA: 0x000D0984 File Offset: 0x000CEB84
		internal int CellNumber
		{
			get
			{
				return this.m_cellNumber;
			}
		}

		// Token: 0x06002B20 RID: 11040
		protected abstract int GetHash();

		// Token: 0x04000FEF RID: 4079
		internal int m_cellNumber;
	}
}
