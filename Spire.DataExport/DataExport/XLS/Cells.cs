using System;
using Spire.DataExport.Collections;

namespace Spire.DataExport.XLS
{
	// Token: 0x020001A3 RID: 419
	public class Cells : Collection
	{
		// Token: 0x06000B79 RID: 2937 RVA: 0x00079860 File Offset: 0x00078860
		public Cells(object Holder)
		{
			this.m_holder = Holder;
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0007987C File Offset: 0x0007887C
		public Cell Add(Cell Item)
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
			base.Add(Item);
			return Item;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x000798C0 File Offset: 0x000788C0
		public int IndexOf(ushort Col, ushort Row)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					int num = 0;
					int num2 = 6;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_B6;
						case 1:
						{
							if (num >= base.Count)
							{
								num2 = 2;
								continue;
							}
							Cell cell = this[num];
							int column = cell.Column;
							num2 = 5;
							continue;
						}
						case 2:
							return -1;
						case 3:
							return num;
						case 4:
						{
							Cell cell;
							int row = cell.Row;
							num2 = 7;
							continue;
						}
						case 5:
						{
							IL_9E:
							int column;
							if (column.Equals((int)Col))
							{
								num2 = 4;
								continue;
							}
							goto IL_44;
						}
						case 6:
							goto IL_B6;
						case 7:
						{
							int row;
							if (row.Equals((int)Row))
							{
								num2 = 3;
								continue;
							}
							goto IL_44;
						}
						}
						break;
						IL_44:
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_9E;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						IL_B6:
						num2 = 1;
					}
				}
				return -1;
			}
		}

		// Token: 0x170000EE RID: 238
		public Cell this[int Index]
		{
			get
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
				return base[Index] as Cell;
			}
			set
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
				base[Index] = value;
			}
		}
	}
}
