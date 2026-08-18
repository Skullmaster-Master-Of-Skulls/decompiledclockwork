using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet.Sorting
{
	// Token: 0x02000175 RID: 373
	public class SortColumns : CollectionExtended<SortColumn>, ISortColumns
	{
		// Token: 0x060011CD RID: 4557 RVA: 0x000AE398 File Offset: 0x000AD398
		internal SortColumns(spr\u1DF5 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000AE3B0 File Offset: 0x000AD3B0
		public SortColumn Add(int key, SortComparsionType sortComparsionType, OrderBy orderBy)
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
			SortColumn sortColumn = new SortColumn(this);
			sortColumn.Key = key;
			sortColumn.ComparsionType = sortComparsionType;
			sortColumn.Order = orderBy;
			base.Add(sortColumn);
			return sortColumn;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000AE410 File Offset: 0x000AD410
		public SortColumn Add(int key, OrderBy orderBy)
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
			SortColumn sortColumn = new SortColumn(this);
			sortColumn.Key = key;
			sortColumn.Order = orderBy;
			base.Add(sortColumn);
			return sortColumn;
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000AE468 File Offset: 0x000AD468
		public new void Remove(SortColumn sortColumn)
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
			this.Remove(sortColumn.Key);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000AE4B0 File Offset: 0x000AD4B0
		public void Remove(int key)
		{
			int a_ = 16;
			int num = this.ᜀ(key);
			if (num == -1)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					break;
				}
				if (true)
				{
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("൅ⵇ㍉汋M㽏♑瑓さ㝗⽙㉛㩝", a_));
			}
			base.RemoveAt(num);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000AE520 File Offset: 0x000AD520
		internal new void ᜀ(SortColumn A_0, int A_1)
		{
			for (;;)
			{
				int num = this.ᜀ(A_0.Key);
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						base.RemoveAt(num);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_29;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							num2 = 1;
							continue;
						}
						break;
					case 1:
						goto IL_74;
					case 2:
						goto IL_29;
					}
					break;
					IL_29:
					if (num == -1)
					{
						goto IL_76;
					}
					num2 = 0;
				}
			}
			IL_74:
			IL_76:
			base.Insert(A_1, A_0);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000AE5AC File Offset: 0x000AD5AC
		internal new int ᜀ(int A_0)
		{
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				int num = 0;
				IEnumerator<SortColumn> enumerator = base.GetEnumerator();
				int result;
				try
				{
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 1:
							goto IL_DA;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								result = num;
								break;
							}
							num2 = 1;
							continue;
						case 3:
							goto IL_E8;
						case 4:
						{
							if (!enumerator.MoveNext())
							{
								num2 = 5;
								continue;
							}
							ISortColumn sortColumn = enumerator.Current;
							num2 = 7;
							continue;
						}
						case 5:
							num2 = 3;
							continue;
						case 7:
						{
							ISortColumn sortColumn;
							if (sortColumn.Key == A_0)
							{
								num2 = 2;
								continue;
							}
							num++;
							num2 = 6;
							continue;
						}
						}
						IL_61:
						num2 = 4;
						continue;
						goto IL_61;
					}
					IL_DA:
					return result;
					IL_E8:
					return -1;
				}
				finally
				{
					int num2 = 2;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_128;
						case 1:
							enumerator.Dispose();
							num2 = 0;
							continue;
						}
						if (enumerator == null)
						{
							break;
						}
						num2 = 1;
					}
					IL_128:;
				}
				return result;
			}
			}
		}
	}
}
