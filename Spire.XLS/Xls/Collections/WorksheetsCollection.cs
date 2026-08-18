using System;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Collections
{
	// Token: 0x0200002A RID: 42
	public class WorksheetsCollection : XlsWorksheetsCollection
	{
		// Token: 0x060002C2 RID: 706 RVA: 0x00018E74 File Offset: 0x00017E74
		internal WorksheetsCollection(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00018E8C File Offset: 0x00017E8C
		public new Worksheet Add(string name)
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
			return (Worksheet)base.Add(name);
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00018ED4 File Offset: 0x00017ED4
		public new Worksheet AddCopy(int sheetIndex)
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
			return (Worksheet)base.AddCopy(sheetIndex);
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00018F1C File Offset: 0x00017F1C
		public Worksheet AddCopy(Worksheet sheet)
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
			return (Worksheet)base.AddCopy(sheet);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00018F64 File Offset: 0x00017F64
		public void AddCopy(WorksheetsCollection sheets)
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
			base.AddCopy(sheets);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00018FA8 File Offset: 0x00017FA8
		public new Worksheet Create(string name)
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
			return (Worksheet)base.Create(name);
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00018FF0 File Offset: 0x00017FF0
		public new Worksheet Create()
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
			return (Worksheet)base.Create();
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00019038 File Offset: 0x00018038
		public CellRange[] FindAllNumber(double doubleValue, bool formulaValue)
		{
			if (!formulaValue)
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
					return base.FindAll(doubleValue, FindType.Number);
				}
			}
			if (true)
			{
			}
			return base.FindAll(doubleValue, FindType.Number | FindType.FormulaValue);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0001908C File Offset: 0x0001808C
		public CellRange[] FindAllString(string stringValue, bool formula, bool formulaValue)
		{
			FindType findType;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_87:
				findType |= FindType.FormulaValue;
				num = 0;
				break;
			default:
				if (false)
				{
				}
				goto IL_3C;
			}
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_6A;
				case 1:
					goto IL_74;
				case 2:
					findType |= (FindType.Formula | FindType.FormulaStringValue);
					num = 1;
					continue;
				case 3:
					goto IL_87;
				case 4:
					if (formula)
					{
						num = 2;
						continue;
					}
					goto IL_74;
				case 5:
					if (formulaValue)
					{
						num = 3;
						continue;
					}
					goto IL_97;
				}
				goto IL_3C;
				IL_74:
				num = 5;
			}
			IL_6A:
			if (true)
			{
			}
			IL_97:
			return base.FindAll(stringValue, findType);
			IL_3C:
			findType = FindType.Text;
			num = 4;
			goto IL_1E;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00019138 File Offset: 0x00018138
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

		// Token: 0x060002CC RID: 716 RVA: 0x0001917C File Offset: 0x0001817C
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

		// Token: 0x060002CD RID: 717 RVA: 0x000191C0 File Offset: 0x000181C0
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

		// Token: 0x060002CE RID: 718 RVA: 0x00019204 File Offset: 0x00018204
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

		// Token: 0x060002CF RID: 719 RVA: 0x0001924C File Offset: 0x0001824C
		public CellRange FindNumber(double doubleValue, bool formulaValue)
		{
			if (formulaValue)
			{
				if (true)
				{
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
					break;
				}
				return base.FindFirst(doubleValue, FindType.Number | FindType.FormulaValue) as CellRange;
			}
			return base.FindFirst(doubleValue, FindType.Number) as CellRange;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000192AC File Offset: 0x000182AC
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
						goto IL_4E;
					case 1:
						if (formulaValue)
						{
							num = 5;
							continue;
						}
						goto IL_7B;
					case 2:
						if (formula)
						{
							num = 4;
							continue;
						}
						goto IL_4E;
					case 3:
						goto IL_4C;
					case 4:
						if (true)
						{
						}
						findType |= (FindType.Formula | FindType.FormulaStringValue);
						num = 0;
						continue;
					case 5:
						findType |= FindType.FormulaValue;
						num = 3;
						continue;
					}
					break;
					IL_4E:
					num = 1;
				}
			}
			IL_4C:
			IL_7B:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4C;
			default:
				if (false)
				{
				}
				return base.FindFirst(stringValue, findType) as CellRange;
			}
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00019360 File Offset: 0x00018360
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

		// Token: 0x060002D2 RID: 722 RVA: 0x000193A8 File Offset: 0x000183A8
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

		// Token: 0x060002D3 RID: 723 RVA: 0x000193F0 File Offset: 0x000183F0
		public void Remove(Worksheet sheet)
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
			base.Remove(sheet);
		}

		// Token: 0x17000113 RID: 275
		public Worksheet this[int Index]
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
				return base[Index] as Worksheet;
			}
		}

		// Token: 0x17000114 RID: 276
		public Worksheet this[string sheetName]
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
				return base[sheetName] as Worksheet;
			}
		}
	}
}
