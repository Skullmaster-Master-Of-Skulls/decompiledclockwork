using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x02000018 RID: 24
	public class RangesCollection : XlsRangesCollection
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00007F58 File Offset: 0x00006F58
		internal RangesCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007F70 File Offset: 0x00006F70
		public void Add(CellRange range)
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
			base.Add(range);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007FB4 File Offset: 0x00006FB4
		public new ExcelComment AddComment()
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
			return new ExcelComment(base.AddComment());
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007FFC File Offset: 0x00006FFC
		public void AddRange(CellRange range)
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
			base.AddRange(range);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00008040 File Offset: 0x00007040
		public CellRange Copy(CellRange destRange)
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
			return base.ᜀ(destRange, CopyRangeOptions.All) as CellRange;
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000808C File Offset: 0x0000708C
		public CellRange Copy(CellRange destRange, bool updateReference)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_23;
					default:
						goto IL_55;
					}
					break;
				case 2:
					goto IL_6E;
				}
				goto IL_20;
				IL_23:
				num = 0;
				continue;
				IL_20:
				if (!updateReference)
				{
					goto IL_23;
				}
				num = 1;
			}
			IL_55:
			if (true)
			{
			}
			if (false)
			{
			}
			CopyRangeOptions copyRangeOptions = CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges;
			goto IL_71;
			IL_6E:
			copyRangeOptions = CopyRangeOptions.None;
			IL_71:
			CopyRangeOptions a_ = copyRangeOptions;
			return base.ᜀ(destRange, a_) as CellRange;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00008118 File Offset: 0x00007118
		public CellRange Copy(CellRange destRange, bool updateReference, bool copyStyles)
		{
			int num = 2;
			CopyRangeOptions copyRangeOptions2;
			for (;;)
			{
				CopyRangeOptions copyRangeOptions;
				switch (num)
				{
				case 0:
					goto IL_7E;
				case 1:
					goto IL_2F;
				case 3:
					copyRangeOptions = CopyRangeOptions.None;
					goto IL_6B;
				case 4:
					copyRangeOptions = (CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges);
					goto IL_6B;
				}
				if (!updateReference)
				{
					num = 1;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				IL_2F:
				if (true)
				{
				}
				num = 3;
				continue;
				IL_6B:
				copyRangeOptions2 = copyRangeOptions;
				num = 0;
			}
			IL_7E:
			copyRangeOptions2 |= (copyStyles ? CopyRangeOptions.CopyStyles : CopyRangeOptions.None);
			return base.ᜀ(destRange, copyRangeOptions2) as CellRange;
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000081C0 File Offset: 0x000071C0
		public CellRange[] FindAllNumber(double doubleValue, bool formulaValue)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (formulaValue)
				{
					return base.FindAll(doubleValue, FindType.Number | FindType.FormulaValue);
				}
				break;
			}
			return base.FindAll(doubleValue, FindType.Number);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00008214 File Offset: 0x00007214
		public CellRange[] FindAllString(string stringValue, bool formula, bool formulaValue)
		{
			FindType findType;
			for (;;)
			{
				findType = FindType.Text;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_46;
					case 1:
						findType |= (FindType.Formula | FindType.FormulaStringValue);
						num = 0;
						continue;
					case 2:
						goto IL_37;
					case 3:
						if (formula)
						{
							num = 1;
							continue;
						}
						goto IL_46;
					case 4:
						if (formulaValue)
						{
							if (true)
							{
							}
							num = 2;
							continue;
						}
						goto IL_7B;
					case 5:
						goto IL_7B;
					}
					break;
					IL_37:
					findType |= FindType.FormulaValue;
					num = 5;
					continue;
					IL_7B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						goto IL_91;
					}
					IL_46:
					num = 4;
				}
			}
			IL_91:
			if (false)
			{
			}
			return base.FindAll(stringValue, findType).ToArray();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000082C8 File Offset: 0x000072C8
		public CellRange[] FindAllDateTime(DateTime dateTimeValue)
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
			return base.FindAll(dateTimeValue);
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000830C File Offset: 0x0000730C
		public CellRange[] FindAllTimeSpan(TimeSpan timeSpanValue)
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
			return base.FindAll(timeSpanValue);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x00008350 File Offset: 0x00007350
		public CellRange[] FindAllBool(bool boolValue)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return base.FindAll(boolValue);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00008394 File Offset: 0x00007394
		public CellRange FindBool(bool boolValue)
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
			return base.FindFirst(boolValue) as CellRange;
		}

		// Token: 0x0600014A RID: 330 RVA: 0x000083DC File Offset: 0x000073DC
		public CellRange FindNumber(double doubleValue, bool formulaValue)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				if (formulaValue)
				{
					return base.FindFirst(doubleValue, FindType.Number | FindType.FormulaValue) as CellRange;
				}
				break;
			}
			return base.FindFirst(doubleValue, FindType.Number) as CellRange;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000843C File Offset: 0x0000743C
		public CellRange FindString(string stringValue, bool formula, bool formulaValue)
		{
			FindType findType;
			for (;;)
			{
				findType = FindType.Text;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (formula)
						{
							num = 5;
							continue;
						}
						goto IL_4E;
					case 1:
						if (formulaValue)
						{
							num = 3;
							continue;
						}
						goto IL_7B;
					case 2:
						goto IL_4E;
					case 3:
						goto IL_3F;
					case 4:
						goto IL_7B;
					case 5:
						if (true)
						{
						}
						findType |= (FindType.Formula | FindType.FormulaStringValue);
						num = 2;
						continue;
					}
					break;
					IL_3F:
					findType |= FindType.FormulaValue;
					num = 4;
					continue;
					IL_7B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3F;
					default:
						goto IL_91;
					}
					IL_4E:
					num = 1;
				}
			}
			IL_91:
			if (false)
			{
			}
			return base.FindFirst(stringValue, findType) as CellRange;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000084F0 File Offset: 0x000074F0
		public CellRange FindDateTime(DateTime dateTimeValue)
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
			return base.FindFirst(dateTimeValue) as CellRange;
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00008538 File Offset: 0x00007538
		public CellRange FindTimeSpan(TimeSpan timeSpanValue)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			return base.FindFirst(timeSpanValue) as CellRange;
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00008580 File Offset: 0x00007580
		public CellRange Intersect(CellRange range)
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
			return base.Intersect(range) as CellRange;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x000085C8 File Offset: 0x000075C8
		public void Move(CellRange destRange)
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
			base.MoveTo(destRange);
		}

		// Token: 0x06000150 RID: 336 RVA: 0x0000860C File Offset: 0x0000760C
		public void Move(CellRange destRange, bool updateReference)
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
			base.MoveTo(destRange);
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00008650 File Offset: 0x00007650
		public void Remove(CellRange range)
		{
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			base.Remove(range);
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00008694 File Offset: 0x00007694
		public new RangesCollection EntireRow
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
				return (RangesCollection)base.EntireRow;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000086DC File Offset: 0x000076DC
		public new RangesCollection EntireColumn
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
				return (RangesCollection)base.EntireColumn;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00008724 File Offset: 0x00007724
		public new CellRange[] Cells
		{
			get
			{
				switch (0)
				{
				default:
				{
					if (true)
					{
					}
					List<CellRange> list;
					for (;;)
					{
						list = new List<CellRange>();
						int num = 0;
						int count = base.Count;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_7B;
							case 1:
							{
								if (num >= count)
								{
									num2 = 0;
									continue;
								}
								CellRange cellRange = (CellRange)base.InnerList[num];
								list.AddRange(cellRange.Cells);
								num++;
								goto IL_9F;
							}
							case 2:
								goto IL_49;
							case 3:
								goto IL_49;
							}
							break;
							IL_49:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_9F:
								num2 = 2;
								break;
							default:
								if (false)
								{
								}
								num2 = 1;
								break;
							}
						}
					}
					IL_7B:
					return list.ToArray();
				}
				}
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000087F0 File Offset: 0x000077F0
		public new CellRange[] Rows
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
				return base.GetColumnRows(false);
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00008834 File Offset: 0x00007834
		public new CellRange[] Columns
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
				return base.GetColumnRows(true);
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000157 RID: 343 RVA: 0x00008878 File Offset: 0x00007878
		public new ExcelComment Comment
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
				return new ExcelComment(base.Comment);
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000088C0 File Offset: 0x000078C0
		public new CellRange EndCell
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
				return (CellRange)base.EndCell;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000159 RID: 345 RVA: 0x00008908 File Offset: 0x00007908
		public new BordersCollection Borders
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
				return new BordersCollection(base.Borders);
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00008950 File Offset: 0x00007950
		public new RangesCollection MergeArea
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
				return (RangesCollection)base.MergeArea;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600015B RID: 347 RVA: 0x00008998 File Offset: 0x00007998
		public new RichText RichText
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return new RichText(base.RichText);
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000089E0 File Offset: 0x000079E0
		// (set) Token: 0x0600015D RID: 349 RVA: 0x00008A28 File Offset: 0x00007A28
		public new CellStyle Style
		{
			get
			{
				if (true)
				{
				}
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				return new CellStyle(base.Style);
			}
			set
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
				base.Style = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00008A6C File Offset: 0x00007A6C
		public new Worksheet Worksheet
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
				return (Worksheet)base.Worksheet;
			}
		}
	}
}
