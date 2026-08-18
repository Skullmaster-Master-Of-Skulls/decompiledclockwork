using System;
using System.Data.Common.Utils;

namespace System.Data.Mapping.ViewGeneration.Validation
{
	// Token: 0x02000281 RID: 641
	internal abstract class CellRelation : InternalBase
	{
		// Token: 0x0600269A RID: 9882 RVA: 0x00094618 File Offset: 0x00092818
		protected CellRelation(int cellNumber)
		{
			this.m_cellNumber = cellNumber;
		}

		// Token: 0x17000791 RID: 1937
		// (get) Token: 0x0600269B RID: 9883 RVA: 0x00094627 File Offset: 0x00092827
		internal int CellNumber
		{
			get
			{
				return this.m_cellNumber;
			}
		}

		// Token: 0x0600269C RID: 9884
		protected abstract int GetHash();

		// Token: 0x040011DA RID: 4570
		internal int m_cellNumber;
	}
}
