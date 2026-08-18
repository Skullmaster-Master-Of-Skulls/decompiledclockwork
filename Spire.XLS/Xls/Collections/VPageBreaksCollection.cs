using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x02000022 RID: 34
	public class VPageBreaksCollection : XlsVPageBreaksCollection
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00016238 File Offset: 0x00015238
		internal VPageBreaksCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x17000100 RID: 256
		public VPageBreak this[int index]
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
				return (VPageBreak)base.List[index];
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0001629C File Offset: 0x0001529C
		public VPageBreak Add(CellRange range)
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
			return (VPageBreak)base.Add(range);
		}

		// Token: 0x06000277 RID: 631 RVA: 0x000162E4 File Offset: 0x000152E4
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

		// Token: 0x06000278 RID: 632 RVA: 0x00016328 File Offset: 0x00015328
		public new VPageBreak GetPageBreak(int rowIndex)
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
			return this.GetPageBreak(rowIndex);
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0001636C File Offset: 0x0001536C
		public VPageBreak GetPageBreak(CellRange range)
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
			return (VPageBreak)base[range];
		}
	}
}
