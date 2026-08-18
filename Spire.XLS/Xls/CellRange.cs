using System;
using System.Collections.Generic;
using Spire.Xls.Collections;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet;

namespace Spire.Xls
{
	// Token: 0x0200012A RID: 298
	public class CellRange : XlsRange
	{
		// Token: 0x06000D04 RID: 3332 RVA: 0x0007EA84 File Offset: 0x0007DA84
		internal CellRange(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0007EA9C File Offset: 0x0007DA9C
		internal CellRange(spr\u2158 A_0, object A_1, int A_2, int A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0007EAB4 File Offset: 0x0007DAB4
		internal CellRange(spr\u2158 A_0, object A_1, int A_2, int A_3, int A_4, int A_5) : base(A_0, A_1, A_2, A_3, A_4, A_5)
		{
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0007EAD0 File Offset: 0x0007DAD0
		internal CellRange(spr\u2158 A_0, object A_1, BiffRecordRaw A_2, bool A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0007EAE8 File Offset: 0x0007DAE8
		internal CellRange(spr\u2158 A_0, object A_1, BiffRecordRaw[] A_2, ref int A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0007EB04 File Offset: 0x0007DB04
		internal CellRange(spr\u2158 A_0, object A_1, BiffRecordRaw[] A_2, ref int A_3, bool A_4) : base(A_0, A_1, A_2, ref A_3, A_4)
		{
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0007EB20 File Offset: 0x0007DB20
		internal CellRange(spr\u2158 A_0, object A_1, List<BiffRecordRaw> A_2, ref int A_3, bool A_4) : base(A_0, A_1, A_2, ref A_3, A_4)
		{
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0007EB3C File Offset: 0x0007DB3C
		public new CellRange Activate()
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
			return (CellRange)base.Activate();
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0007EB84 File Offset: 0x0007DB84
		public void AddComment(ExcelComment comment)
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
			base.AddComment(comment.Wrapped);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0007EBCC File Offset: 0x0007DBCC
		public new ExcelComment AddComment()
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
			return new ExcelComment(base.AddComment());
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0007EC14 File Offset: 0x0007DC14
		public CellRange Clone(object parent, Dictionary<string, string> rangeNames, Workbook book)
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
			return (CellRange)base.Clone(parent, rangeNames, book.excelWorkbook);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0007EC64 File Offset: 0x0007DC64
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
			base.ᜀ(destRange, CopyRangeOptions.All);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0007ECA8 File Offset: 0x0007DCA8
		public void Move(CellRange destRange, bool copyStyle, bool updateReference)
		{
			CopyRangeOptions copyRangeOptions2;
			for (;;)
			{
				IL_00:
				int num = 2;
				for (;;)
				{
					CopyRangeOptions copyRangeOptions;
					switch (num)
					{
					case 0:
						goto IL_7E;
					case 1:
						copyRangeOptions = (CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges);
						goto IL_6B;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							copyRangeOptions = CopyRangeOptions.None;
							goto IL_6B;
						}
						break;
					case 4:
						if (true)
						{
						}
						num = 3;
						continue;
					}
					if (!updateReference)
					{
						num = 4;
						continue;
					}
					num = 1;
					continue;
					IL_6B:
					copyRangeOptions2 = copyRangeOptions;
					num = 0;
				}
			}
			IL_7E:
			copyRangeOptions2 |= (copyStyle ? CopyRangeOptions.CopyStyles : CopyRangeOptions.None);
			base.ᜀ(destRange, copyRangeOptions2);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0007ED4C File Offset: 0x0007DD4C
		public CellRange Copy(CellRange destRange)
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
			return base.ᜁ(destRange, CopyRangeOptions.All) as CellRange;
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0007ED98 File Offset: 0x0007DD98
		public CellRange Copy(CellRange destRange, bool updateReference)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_46:
				if (!updateReference)
				{
					num = 2;
				}
				else
				{
					num = 3;
				}
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			for (;;)
			{
				IL_30:
				switch (num)
				{
				case 1:
					goto IL_6E;
				case 2:
					if (true)
					{
					}
					num = 1;
					continue;
				case 3:
					goto IL_63;
				}
				break;
			}
			goto IL_46;
			IL_63:
			CopyRangeOptions copyRangeOptions = CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges;
			goto IL_71;
			IL_6E:
			copyRangeOptions = CopyRangeOptions.None;
			IL_71:
			CopyRangeOptions a_ = copyRangeOptions;
			return base.ᜁ(destRange, a_) as CellRange;
			IL_20:
			if (false)
			{
			}
			num = 0;
			goto IL_30;
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0007EE24 File Offset: 0x0007DE24
		public CellRange Copy(CellRange destRange, bool updateReference, bool copyStyles)
		{
			CopyRangeOptions copyRangeOptions2;
			for (;;)
			{
				IL_00:
				int num = 2;
				for (;;)
				{
					CopyRangeOptions copyRangeOptions;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							copyRangeOptions = CopyRangeOptions.None;
							goto IL_6B;
						}
						break;
					case 1:
						copyRangeOptions = (CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges);
						goto IL_6B;
					case 3:
						num = 0;
						continue;
					case 4:
						goto IL_7E;
					}
					if (!updateReference)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					num = 1;
					continue;
					IL_6B:
					copyRangeOptions2 = copyRangeOptions;
					num = 4;
				}
			}
			IL_7E:
			copyRangeOptions2 |= (copyStyles ? CopyRangeOptions.CopyStyles : CopyRangeOptions.None);
			return base.ᜁ(destRange, copyRangeOptions2) as CellRange;
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0007EECC File Offset: 0x0007DECC
		public CellRange[] FindAllNumber(double doubleValue, bool formulaValue)
		{
			if (formulaValue)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				return base.FindAll(doubleValue, FindType.Number | FindType.FormulaValue);
			}
			return base.FindAll(doubleValue, FindType.Number);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0007EF20 File Offset: 0x0007DF20
		public CellRange[] FindAllString(string stringValue, bool formula, bool formulaValue)
		{
			FindType findType;
			for (;;)
			{
				findType = FindType.Text;
				int num = 4;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_68;
					case 1:
						goto IL_6A;
					case 2:
						if (formulaValue)
						{
							num = 5;
							continue;
						}
						goto IL_9A;
					case 3:
						findType |= (FindType.Formula | FindType.FormulaStringValue);
						num = 1;
						continue;
					case 4:
						if (!formula)
						{
							goto IL_6A;
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
							num = 3;
							continue;
						}
						break;
					case 5:
						findType |= FindType.FormulaValue;
						num = 0;
						continue;
					}
					break;
					IL_6A:
					num = 2;
				}
			}
			IL_68:
			IL_9A:
			return base.FindAll(stringValue, findType).ToArray();
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0007EFD4 File Offset: 0x0007DFD4
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

		// Token: 0x06000D17 RID: 3351 RVA: 0x0007F018 File Offset: 0x0007E018
		public CellRange[] FindAllTimeSpan(TimeSpan timeSpanValue)
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
			return base.FindAll(timeSpanValue);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0007F05C File Offset: 0x0007E05C
		public CellRange[] FindAllBool(bool boolValue)
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
			return base.FindAll(boolValue);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0007F0A0 File Offset: 0x0007E0A0
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

		// Token: 0x06000D1A RID: 3354 RVA: 0x0007F0E8 File Offset: 0x0007E0E8
		public CellRange FindNumber(double doubleValue, bool formulaValue)
		{
			for (;;)
			{
				if (true)
				{
				}
				if (!formulaValue)
				{
					goto IL_42;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_2D;
				}
			}
			IL_2D:
			if (false)
			{
			}
			return base.FindFirst(doubleValue, FindType.Number | FindType.FormulaValue) as CellRange;
			IL_42:
			return base.FindFirst(doubleValue, FindType.Number) as CellRange;
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0007F148 File Offset: 0x0007E148
		public CellRange FindString(string stringValue, bool formula, bool formulaValue)
		{
			FindType findType;
			for (;;)
			{
				findType = FindType.Text;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2D;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							findType |= (FindType.Formula | FindType.FormulaStringValue);
							num = 5;
							continue;
						}
						break;
					case 1:
						if (formulaValue)
						{
							num = 4;
							continue;
						}
						goto IL_9A;
					case 2:
						if (formula)
						{
							goto IL_2D;
						}
						goto IL_46;
					case 3:
						goto IL_44;
					case 4:
						findType |= FindType.FormulaValue;
						num = 3;
						continue;
					case 5:
						goto IL_46;
					}
					break;
					IL_2D:
					num = 0;
					continue;
					IL_46:
					num = 1;
				}
			}
			IL_44:
			IL_9A:
			return base.FindFirst(stringValue, findType) as CellRange;
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0007F1FC File Offset: 0x0007E1FC
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

		// Token: 0x06000D1D RID: 3357 RVA: 0x0007F244 File Offset: 0x0007E244
		public CellRange FindTimeSpan(TimeSpan timeSpanValue)
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
			return base.FindFirst(timeSpanValue) as CellRange;
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x0007F28C File Offset: 0x0007E28C
		public CellRange Intersect(CellRange range)
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
			return base.Intersect(range) as CellRange;
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0007F2D4 File Offset: 0x0007E2D4
		public CellRange Merge(CellRange range)
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
			return base.Merge(range) as CellRange;
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0007F31C File Offset: 0x0007E31C
		public void SetDataValidation(Validation dataValidation)
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
			base.SetDataValidation(dataValidation.Wrapped);
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000D21 RID: 3361 RVA: 0x0007F364 File Offset: 0x0007E364
		public new BordersCollection Borders
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
				return new BordersCollection(base.Borders);
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06000D22 RID: 3362 RVA: 0x0007F3AC File Offset: 0x0007E3AC
		// (set) Token: 0x06000D23 RID: 3363 RVA: 0x0007F3F4 File Offset: 0x0007E3F4
		public new CellStyle Style
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
				return base.Style as CellStyle;
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
				base.Style = value;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06000D24 RID: 3364 RVA: 0x0007F438 File Offset: 0x0007E438
		public new CellRange[] Cells
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
				return base.Cells;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06000D25 RID: 3365 RVA: 0x0007F47C File Offset: 0x0007E47C
		public new CellRange[] Columns
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_DF;
					case 2:
						num = 6;
						continue;
					case 3:
					{
						int num2;
						if (num2 <= base.LastColumn)
						{
							if (true)
							{
							}
							CellRange[] array;
							array[num2 - base.FirstColumn] = this.Worksheet.Range[base.FirstRow, num2, base.LastRow, num2];
							num2++;
							num = 5;
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
						break;
					}
					case 4:
					{
						CellRange[] array;
						return array;
					}
					case 5:
						goto IL_E1;
					case 6:
					{
						if (base.FirstColumn > this.m_book.MaxColumnCount)
						{
							num = 0;
							continue;
						}
						CellRange[] array = new CellRange[base.LastColumn - base.FirstColumn + 1];
						int num2 = base.FirstColumn;
						num = 7;
						continue;
					}
					case 7:
						goto IL_E1;
					}
					if (base.FirstColumn != 0)
					{
						num = 2;
						continue;
					}
					break;
					IL_E1:
					num = 3;
				}
				IL_DF:
				return new CellRange[0];
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000D26 RID: 3366 RVA: 0x0007F5B4 File Offset: 0x0007E5B4
		public new CellRange[] Rows
		{
			get
			{
				CellRange[] array;
				for (;;)
				{
					for (;;)
					{
						array = new CellRange[base.LastRow - base.FirstRow + 1];
						int num = base.FirstRow;
						int num2 = 0;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_3E;
							case 1:
								if (num > base.LastRow)
								{
									num2 = 2;
									continue;
								}
								array[num - base.FirstRow] = this.Worksheet.Range[num, base.FirstColumn, num, base.LastColumn];
								num++;
								num2 = 3;
								continue;
							case 2:
								goto IL_57;
							case 3:
								if (true)
								{
								}
								goto IL_3E;
							}
							break;
							IL_3E:
							num2 = 1;
						}
					}
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_BA;
					}
				}
				IL_BA:
				if (false)
				{
				}
				return array;
			}
		}

		// Token: 0x1700047E RID: 1150
		public CellRange this[int row, int column]
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
				return base[row, column] as CellRange;
			}
			set
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
				base[row, column] = value;
			}
		}

		// Token: 0x1700047F RID: 1151
		public CellRange this[int row, int column, int lastRow, int lastColumn]
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
				return base[row, column, lastRow, lastColumn] as CellRange;
			}
		}

		// Token: 0x17000480 RID: 1152
		public CellRange this[string name]
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
				return base[name] as CellRange;
			}
		}

		// Token: 0x17000481 RID: 1153
		public CellRange this[string name, bool IsR1C1Notation]
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
				return base[name, IsR1C1Notation] as CellRange;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000D2C RID: 3372 RVA: 0x0007F7EC File Offset: 0x0007E7EC
		public new ExcelComment Comment
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
				return new ExcelComment(base.Comment);
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000D2D RID: 3373 RVA: 0x0007F834 File Offset: 0x0007E834
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
				return base.Worksheet as Worksheet;
			}
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000D2E RID: 3374 RVA: 0x0007F87C File Offset: 0x0007E87C
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

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000D2F RID: 3375 RVA: 0x0007F8C4 File Offset: 0x0007E8C4
		public new CellRange EntireColumn
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
				return base.EntireColumn as CellRange;
			}
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000D30 RID: 3376 RVA: 0x0007F90C File Offset: 0x0007E90C
		public new CellRange EndCell
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
				return base.EndCell as CellRange;
			}
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000D31 RID: 3377 RVA: 0x0007F954 File Offset: 0x0007E954
		public new CellRange MergeArea
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
				return base.MergeArea as CellRange;
			}
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000D32 RID: 3378 RVA: 0x0007F99C File Offset: 0x0007E99C
		public new CellRange EntireRow
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
				return base.EntireRow as CellRange;
			}
		}
	}
}
