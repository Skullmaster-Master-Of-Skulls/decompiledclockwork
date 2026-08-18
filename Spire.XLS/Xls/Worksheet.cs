using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using Spire.Xls.Collections;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Security;

namespace Spire.Xls
{
	// Token: 0x0200004C RID: 76
	public class Worksheet : XlsWorksheet
	{
		// Token: 0x06000510 RID: 1296 RVA: 0x00029B04 File Offset: 0x00028B04
		internal Worksheet(spr\u2158 A_0, object A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000511 RID: 1297 RVA: 0x00029B19 File Offset: 0x00028B19
		internal Worksheet(spr\u2158 A_0, object A_1, sprἛ A_2, ExcelParseOptions A_3, bool A_4, Dictionary<int, int> A_5, IDecryptor A_6) : base(A_0, A_1, A_2, A_3, A_4, A_5, A_6)
		{
		}

		// Token: 0x06000512 RID: 1298 RVA: 0x00029B2C File Offset: 0x00028B2C
		public CellRange[] FindAllNumber(double doubleValue, bool formulaValue)
		{
			if (formulaValue)
			{
				for (;;)
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
						goto IL_23;
					}
				}
				IL_23:
				if (false)
				{
				}
				return base.FindAll(doubleValue, FindType.Number | FindType.FormulaValue);
			}
			return base.FindAll(doubleValue, FindType.Number);
		}

		// Token: 0x06000513 RID: 1299 RVA: 0x00029B80 File Offset: 0x00028B80
		public CellRange[] FindAllString(string stringValue, bool formula, bool formulaValue)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_97;
			}
			if (false)
			{
			}
			FindType findType;
			for (;;)
			{
				findType = FindType.Text;
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (formula)
						{
							num = 1;
							continue;
						}
						goto IL_74;
					case 1:
						findType |= (FindType.Formula | FindType.FormulaStringValue);
						num = 2;
						continue;
					case 2:
						goto IL_74;
					case 3:
						if (formulaValue)
						{
							num = 5;
							continue;
						}
						goto IL_97;
					case 4:
						goto IL_72;
					case 5:
						findType |= FindType.FormulaValue;
						num = 4;
						continue;
					}
					break;
					IL_74:
					num = 3;
				}
			}
			IL_72:
			IL_97:
			return base.FindAll(stringValue, findType);
		}

		// Token: 0x06000514 RID: 1300 RVA: 0x00029C2C File Offset: 0x00028C2C
		public CellRange[] FindAllDateTime(DateTime dateTimeValue)
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
			return base.FindAll(dateTimeValue);
		}

		// Token: 0x06000515 RID: 1301 RVA: 0x00029C70 File Offset: 0x00028C70
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

		// Token: 0x06000516 RID: 1302 RVA: 0x00029CB4 File Offset: 0x00028CB4
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
			return base.FindAll(boolValue).ToArray();
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x00029CFC File Offset: 0x00028CFC
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
			return base.FindOne(boolValue) as CellRange;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00029D44 File Offset: 0x00028D44
		public CellRange FindNumber(double doubleValue, bool formulaValue)
		{
			if (formulaValue)
			{
				for (;;)
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
						goto IL_23;
					}
				}
				IL_23:
				if (false)
				{
				}
				return base.FindOne(doubleValue, FindType.Number | FindType.FormulaValue) as CellRange;
			}
			return base.FindOne(doubleValue, FindType.Number) as CellRange;
		}

		// Token: 0x06000519 RID: 1305 RVA: 0x00029DA4 File Offset: 0x00028DA4
		public CellRange FindString(string stringValue, bool formula, bool formulaValue)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_97;
			}
			if (false)
			{
			}
			FindType findType;
			for (;;)
			{
				findType = FindType.Text;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 1:
						if (formula)
						{
							num = 3;
							continue;
						}
						goto IL_74;
					case 2:
						goto IL_74;
					case 3:
						findType |= (FindType.Formula | FindType.FormulaStringValue);
						num = 2;
						continue;
					case 4:
						if (formulaValue)
						{
							num = 5;
							continue;
						}
						goto IL_97;
					case 5:
						findType |= FindType.FormulaValue;
						num = 0;
						continue;
					}
					break;
					IL_74:
					num = 4;
				}
			}
			IL_6A:
			if (true)
			{
			}
			IL_97:
			return base.FindOne(stringValue, findType) as CellRange;
		}

		// Token: 0x0600051A RID: 1306 RVA: 0x00029E58 File Offset: 0x00028E58
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
			return base.FindOne(dateTimeValue) as CellRange;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x00029EA0 File Offset: 0x00028EA0
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
			return base.FindOne(timeSpanValue) as CellRange;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x00029EE8 File Offset: 0x00028EE8
		public void CopyFrom(Worksheet worksheet)
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
			base.ᜀ(worksheet, null, null, null, WorksheetCopyType.CopyAll);
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x00029F34 File Offset: 0x00028F34
		public void Copy(CellRange sourceRange, CellRange destRange)
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
			base.CopyRange(destRange, sourceRange);
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x00029F78 File Offset: 0x00028F78
		public void Copy(CellRange sourceRange, CellRange destRange, bool copyStyle)
		{
			CopyRangeOptions a_;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_59:
				a_ = CopyRangeOptions.CopyStyles;
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_38;
			}
			for (;;)
			{
				IL_1E:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (copyStyle)
					{
						num = 2;
						continue;
					}
					goto IL_65;
				case 1:
					goto IL_63;
				case 2:
					goto IL_57;
				}
				goto IL_38;
			}
			IL_57:
			goto IL_59;
			IL_63:
			IL_65:
			base.ᜁ(destRange, sourceRange, a_);
			return;
			IL_38:
			a_ = CopyRangeOptions.None;
			num = 0;
			goto IL_1E;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x00029FF4 File Offset: 0x00028FF4
		public void Copy(CellRange sourceRange, CellRange destRange, bool copyStyle, bool updateReference, bool ignoreSize)
		{
			CopyRangeOptions copyRangeOptions;
			for (;;)
			{
				IL_44:
				copyRangeOptions = CopyRangeOptions.None;
				int num = 3;
				for (;;)
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
						switch (num)
						{
						case 0:
							copyRangeOptions = CopyRangeOptions.CopyStyles;
							num = 2;
							continue;
						case 1:
							copyRangeOptions = (copyRangeOptions | CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges);
							num = 4;
							continue;
						case 2:
							goto IL_AF;
						case 3:
							if (true)
							{
							}
							if (copyStyle)
							{
								num = 0;
								continue;
							}
							goto IL_AF;
						case 4:
							goto IL_63;
						case 5:
							if (updateReference)
							{
								num = 1;
								continue;
							}
							goto IL_63;
						case 6:
							goto IL_75;
						case 7:
							goto IL_81;
						}
						goto IL_44;
						IL_63:
						num = 6;
						continue;
						IL_AF:
						num = 5;
						continue;
					}
					IL_75:
					if (!ignoreSize)
					{
						goto IL_CB;
					}
					num = 7;
				}
			}
			IL_81:
			base.ᜀ(destRange, sourceRange, copyRangeOptions);
			return;
			IL_CB:
			base.ᜁ(destRange, sourceRange, copyRangeOptions);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0002A0D8 File Offset: 0x000290D8
		public void Copy(CellRange sourceRange, Worksheet worksheet, int destRow, int destColumn)
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
			base.ᜁ(worksheet[destRow, destColumn], sourceRange, CopyRangeOptions.All);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0002A128 File Offset: 0x00029128
		public void Copy(CellRange sourceRange, Worksheet worksheet, int destRow, int destColumn, bool copyStyle)
		{
			if (true)
			{
			}
			CopyRangeOptions a_;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5A:
				a_ = CopyRangeOptions.CopyStyles;
				num = 2;
				break;
			default:
				if (false)
				{
				}
				goto IL_38;
			}
			for (;;)
			{
				IL_26:
				switch (num)
				{
				case 0:
					if (copyStyle)
					{
						num = 1;
						continue;
					}
					goto IL_66;
				case 1:
					goto IL_58;
				case 2:
					goto IL_64;
				}
				goto IL_38;
			}
			IL_58:
			goto IL_5A;
			IL_64:
			IL_66:
			base.ᜁ(worksheet[destRow, destColumn], sourceRange, a_);
			return;
			IL_38:
			a_ = CopyRangeOptions.None;
			num = 0;
			goto IL_26;
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0002A1B0 File Offset: 0x000291B0
		public void Copy(CellRange sourceRange, Worksheet worksheet, int destRow, int destColumn, bool copyStyle, bool updateRerence)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_98;
			}
			if (false)
			{
			}
			CopyRangeOptions copyRangeOptions;
			for (;;)
			{
				copyRangeOptions = CopyRangeOptions.None;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						copyRangeOptions = (copyRangeOptions | CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges);
						num = 1;
						continue;
					case 1:
						goto IL_6C;
					case 2:
						goto IL_6E;
					case 3:
						if (copyStyle)
						{
							num = 5;
							continue;
						}
						goto IL_6E;
					case 4:
						if (updateRerence)
						{
							num = 0;
							continue;
						}
						goto IL_98;
					case 5:
						if (true)
						{
						}
						copyRangeOptions = CopyRangeOptions.CopyStyles;
						num = 2;
						continue;
					}
					break;
					IL_6E:
					num = 4;
				}
			}
			IL_6C:
			IL_98:
			base.ᜁ(worksheet[destRow, destColumn], sourceRange, copyRangeOptions);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0002A268 File Offset: 0x00029268
		public void Move(CellRange sourceRange, CellRange destRange)
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
			base.ᜀ(destRange, sourceRange, CopyRangeOptions.All, false);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0002A2B0 File Offset: 0x000292B0
		public void Move(CellRange sourceRange, CellRange destRange, bool updateReference, bool copyStyle)
		{
			CopyRangeOptions copyRangeOptions;
			for (;;)
			{
				IL_00:
				if (true)
				{
				}
				for (;;)
				{
					copyRangeOptions = CopyRangeOptions.None;
					int num = 4;
					for (;;)
					{
						CopyRangeOptions copyRangeOptions2;
						switch (num)
						{
						case 0:
							copyRangeOptions2 = CopyRangeOptions.None;
							goto IL_6D;
						case 1:
							copyRangeOptions2 = (CopyRangeOptions.UpdateFormulas | CopyRangeOptions.UpdateMerges);
							goto IL_6D;
						case 2:
							num = 0;
							continue;
						case 3:
							goto IL_76;
						case 4:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								if (!updateReference)
								{
									num = 2;
									continue;
								}
								num = 1;
								continue;
							}
							break;
						}
						break;
						IL_6D:
						copyRangeOptions = copyRangeOptions2;
						num = 3;
					}
				}
			}
			IL_76:
			copyRangeOptions |= (copyStyle ? CopyRangeOptions.CopyStyles : CopyRangeOptions.None);
			base.ᜀ(destRange, sourceRange, copyRangeOptions, false);
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0002A358 File Offset: 0x00029358
		public DataTable ExportDataTable(CellRange range, bool exportColumnNames)
		{
			ExportDataTableOptions a_;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_59:
				a_ = ExportDataTableOptions.ColumnNames;
				num = 2;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_38;
			}
			for (;;)
			{
				IL_26:
				switch (num)
				{
				case 0:
					if (exportColumnNames)
					{
						num = 1;
						continue;
					}
					goto IL_65;
				case 1:
					goto IL_57;
				case 2:
					goto IL_63;
				}
				goto IL_38;
			}
			IL_57:
			goto IL_59;
			IL_63:
			IL_65:
			return base.ᜀ(range, a_);
			IL_38:
			a_ = ExportDataTableOptions.None;
			num = 0;
			goto IL_26;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0002A3D4 File Offset: 0x000293D4
		public DataTable ExportDataTable(CellRange range, bool exportColumnNames, bool computedFormulaValue)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_96;
			}
			if (false)
			{
			}
			ExportDataTableOptions exportDataTableOptions;
			for (;;)
			{
				exportDataTableOptions = ExportDataTableOptions.None;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						exportDataTableOptions |= ExportDataTableOptions.ComputedFormulaValues;
						num = 5;
						continue;
					case 1:
						if (exportColumnNames)
						{
							num = 3;
							continue;
						}
						goto IL_6B;
					case 2:
						goto IL_6B;
					case 3:
						exportDataTableOptions |= ExportDataTableOptions.ColumnNames;
						num = 2;
						continue;
					case 4:
						if (computedFormulaValue)
						{
							num = 0;
							continue;
						}
						goto IL_96;
					case 5:
						goto IL_69;
					}
					break;
					IL_6B:
					if (true)
					{
					}
					num = 4;
				}
			}
			IL_69:
			IL_96:
			return base.ᜀ(range, exportDataTableOptions);
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002A480 File Offset: 0x00029480
		public DataTable ExportDataTable(int firstRow, int firstColumn, int maxRows, int maxColumns, bool exportColumnNames)
		{
			ExportDataTableOptions a_;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_52:
				a_ = ExportDataTableOptions.ColumnNames;
				if (true)
				{
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				goto IL_30;
			}
			for (;;)
			{
				IL_1E:
				switch (num)
				{
				case 0:
					goto IL_64;
				case 1:
					goto IL_50;
				case 2:
					if (exportColumnNames)
					{
						num = 1;
						continue;
					}
					goto IL_66;
				}
				goto IL_30;
			}
			IL_50:
			goto IL_52;
			IL_64:
			IL_66:
			return base.ᜀ(firstRow, firstColumn, maxRows, maxColumns, a_);
			IL_30:
			a_ = ExportDataTableOptions.None;
			num = 2;
			goto IL_1E;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002A500 File Offset: 0x00029500
		public CellRange GetIntersectRanges(CellRange range1, CellRange range2)
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
			return base.IntersectRanges(range1, range2) as CellRange;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0002A548 File Offset: 0x00029548
		public CellRange Merge(CellRange range1, CellRange range2)
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
			return base.MergeRanges(range1, range2) as CellRange;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0002A590 File Offset: 0x00029590
		public void SetDefaultColumnStyle(int columnIndex, CellStyle defaultStyle)
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
			base.SetDefaultColumnStyle(columnIndex, defaultStyle.Wrapped);
		}

		// Token: 0x0600052B RID: 1323 RVA: 0x0002A5D8 File Offset: 0x000295D8
		public void SetDefaultColumnStyle(int firstColumnIndex, int lastColumnIndex, CellStyle defaultStyle)
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
			base.SetDefaultColumnStyle(firstColumnIndex, lastColumnIndex, defaultStyle.Wrapped);
		}

		// Token: 0x0600052C RID: 1324 RVA: 0x0002A624 File Offset: 0x00029624
		public void SetDefaultRowStyle(int rowIndex, CellStyle defaultStyle)
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
			base.SetDefaultRowStyle(rowIndex, defaultStyle.Wrapped);
		}

		// Token: 0x0600052D RID: 1325 RVA: 0x0002A66C File Offset: 0x0002966C
		public void SetDefaultRowStyle(int firstRowIndex, int lastRowIndex, CellStyle defaultStyle)
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
			base.SetDefaultRowStyle(firstRowIndex, lastRowIndex, defaultStyle.Wrapped);
		}

		// Token: 0x0600052E RID: 1326 RVA: 0x0002A6B8 File Offset: 0x000296B8
		public new CellStyle GetDefaultColumnStyle(int columnIndex)
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
			return new CellStyle(base.GetDefaultColumnStyle(columnIndex));
		}

		// Token: 0x0600052F RID: 1327 RVA: 0x0002A700 File Offset: 0x00029700
		public new CellStyle GetDefaultRowStyle(int rowIndex)
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
			return new CellStyle(base.GetDefaultColumnStyle(rowIndex));
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x0002A748 File Offset: 0x00029748
		public void RemoveMergedCells(CellRange range)
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
			base.RemoveMergedCells(range);
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x0002A78C File Offset: 0x0002978C
		public void RemoveRange(CellRange range)
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
			base.FreeRange(range);
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0002A7D0 File Offset: 0x000297D0
		public void RemoveRange(int rowIndex, int columnIndex)
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
			base.FreeRange(rowIndex, columnIndex);
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0002A814 File Offset: 0x00029814
		public void FreezePanes(int rowIndex, int columnIndex)
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
			this.Range[rowIndex, columnIndex].FreezePanes();
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x0002A864 File Offset: 0x00029864
		public void SetActiveCell(CellRange range)
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
			base.SetActiveCell(range);
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x0002A8A8 File Offset: 0x000298A8
		protected override void OnDispose()
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
			((IDisposable)this.Pictures).Dispose();
			base.OnDispose();
		}

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0002A8F4 File Offset: 0x000298F4
		public new AutoFiltersCollection AutoFilters
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
				return (AutoFiltersCollection)base.AutoFilters;
			}
		}

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0002A93C File Offset: 0x0002993C
		public new CellRange[] Cells
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
				return this.AllocatedRange.Cells;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000538 RID: 1336 RVA: 0x0002A984 File Offset: 0x00029984
		public new CellRange[] Columns
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
				return this.AllocatedRange.Columns;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0002A9CC File Offset: 0x000299CC
		public new WorksheetChartsCollection Charts
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
				return (WorksheetChartsCollection)base.Charts;
			}
		}

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0002AA14 File Offset: 0x00029A14
		public new CommentsCollection Comments
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
				return (CommentsCollection)base.Comments;
			}
		}

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0002AA5C File Offset: 0x00029A5C
		public new HPageBreaksCollection HPageBreaks
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
				return (HPageBreaksCollection)base.HPageBreaks;
			}
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0002AAA4 File Offset: 0x00029AA4
		public new HyperLinksCollection HyperLinks
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
				return (HyperLinksCollection)base.HyperLinks;
			}
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600053D RID: 1341 RVA: 0x0002AAEC File Offset: 0x00029AEC
		public new PageSetup PageSetup
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
				return (PageSetup)base.PageSetup;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0002AB34 File Offset: 0x00029B34
		public new PicturesCollection Pictures
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
				return (PicturesCollection)base.Pictures;
			}
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0002AB7C File Offset: 0x00029B7C
		public new CellRange Range
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
				return this.AllocatedRange;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0002ABC0 File Offset: 0x00029BC0
		public new CellRange[] Rows
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
				return this.AllocatedRange.Rows;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0002AC08 File Offset: 0x00029C08
		public new VPageBreaksCollection VPageBreaks
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
				return (VPageBreaksCollection)base.VPageBreaks;
			}
		}

		// Token: 0x170001CB RID: 459
		public CellRange this[int row, int column]
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
				return this.Range[row, column];
			}
		}

		// Token: 0x170001CC RID: 460
		public CellRange this[int row, int column, int lastRow, int lastColumn]
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
				return this.Range[row, column, lastRow, lastColumn];
			}
		}

		// Token: 0x170001CD RID: 461
		public CellRange this[string name]
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
				return this.Range[name];
			}
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0002AD2C File Offset: 0x00029D2C
		public new CellRange AllocatedRange
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
				return (CellRange)base.AllocatedRange;
			}
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0002AD74 File Offset: 0x00029D74
		public CellRange PrintRange
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
				return (CellRange)base.PrintArea;
			}
		}

		// Token: 0x170001D0 RID: 464
		// (get) Token: 0x06000547 RID: 1351 RVA: 0x0002ADBC File Offset: 0x00029DBC
		public new CellRange[] MergedCells
		{
			get
			{
				switch (0)
				{
				default:
				{
					CellRange[] array;
					for (;;)
					{
						IL_4F:
						base.ParseData();
						for (;;)
						{
							IL_55:
							int num = 5;
							for (;;)
							{
								int num2;
								int num3;
								CellRange[] array2;
								switch (num)
								{
								case 0:
									goto IL_14D;
								case 1:
									if (num2 <= 0)
									{
										num = 10;
										continue;
									}
									num = 4;
									continue;
								case 2:
									num3 = 0;
									goto IL_1A3;
								case 3:
									if (array != null)
									{
										num = 8;
										continue;
									}
									goto IL_1C5;
								case 4:
									array2 = new CellRange[num2];
									goto IL_129;
								case 5:
									if (this.\u1714 == null)
									{
										num = 9;
										continue;
									}
									num = 7;
									continue;
								case 6:
									goto IL_14D;
								case 7:
									num3 = this.\u1714.ᜅ();
									goto IL_1A3;
								case 8:
								{
									List<Rectangle> list = this.\u1714.ᜄ();
									int num4 = 0;
									num = 0;
									continue;
								}
								case 9:
									num = 2;
									continue;
								case 10:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_55;
									default:
										if (false)
										{
										}
										num = 12;
										continue;
									}
									break;
								case 11:
								{
									int num4;
									if (num4 >= num2)
									{
										num = 13;
										continue;
									}
									List<Rectangle> list;
									Rectangle rectangle = list[num4];
									XlsRange xlsRange = base.AppImplementation.ᜀ(this, rectangle.X + 1, rectangle.Y + 1, rectangle.Right + 1, rectangle.Bottom + 1);
									array[num4] = (CellRange)xlsRange;
									num4++;
									num = 6;
									continue;
								}
								case 12:
									array2 = null;
									goto IL_129;
								case 13:
									goto IL_16C;
								}
								goto IL_4F;
								IL_129:
								array = array2;
								num = 3;
								continue;
								IL_14D:
								num = 11;
								continue;
								IL_1A3:
								num2 = num3;
								num = 1;
							}
						}
					}
					IL_16C:
					IL_1C5:
					if (true)
					{
					}
					return array;
				}
				}
			}
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0002AF98 File Offset: 0x00029F98
		public new Workbook Workbook
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
				return base.ParentWorkbook.InnerWorkBook;
			}
		}
	}
}
