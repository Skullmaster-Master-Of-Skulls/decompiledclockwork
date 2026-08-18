using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x0200003E RID: 62
	public class HPageBreaksCollection : XlsHPageBreaksCollection
	{
		// Token: 0x06000429 RID: 1065 RVA: 0x00025F2C File Offset: 0x00024F2C
		internal HPageBreaksCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x1700015C RID: 348
		public HPageBreak this[int index]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (HPageBreak)base.List[index];
			}
		}

		// Token: 0x1700015D RID: 349
		public HPageBreak this[CellRange location]
		{
			get
			{
				switch (1 == 1)
				{
				}
				if (true)
				{
				}
				if (false)
				{
				}
				return (HPageBreak)base[location];
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x00025FD8 File Offset: 0x00024FD8
		public HPageBreak Add(CellRange range)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return (HPageBreak)base.Add(range);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x00026020 File Offset: 0x00025020
		public void Remove(CellRange range)
		{
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.Remove(range);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00026064 File Offset: 0x00025064
		public new HPageBreak GetPageBreak(int rowIndex)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return this.GetPageBreak(rowIndex);
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000260A8 File Offset: 0x000250A8
		public HPageBreak GetPageBreak(CellRange range)
		{
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			return (HPageBreak)base[range];
		}
	}
}
