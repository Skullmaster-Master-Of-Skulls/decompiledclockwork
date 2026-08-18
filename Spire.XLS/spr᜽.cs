using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Parser.Biff_Records.Formula;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000590 RID: 1424
internal class spr\u173D : CommonWrapper, spr\u20B0
{
	// Token: 0x06005637 RID: 22071 RVA: 0x0036EE10 File Offset: 0x0036DE10
	public spr\u173D(IXLSRange A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x06005638 RID: 22072 RVA: 0x0036EE38 File Offset: 0x0036DE38
	public string \u1712()
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_D3:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_55;
			}
			int num2;
			int num3;
			IXLSRange[] cells;
			string inputTitle;
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
					goto IL_9F;
				case 1:
				{
					if (num2 >= num3)
					{
						num = 2;
						continue;
					}
					IXLSRange ixlsrange = cells[num2];
					num = 3;
					continue;
				}
				case 2:
					return inputTitle;
				case 3:
				{
					IXLSRange ixlsrange;
					if (ixlsrange.DataValidation.InputTitle != inputTitle)
					{
						num = 5;
						continue;
					}
					num2++;
					num = 0;
					continue;
				}
				case 4:
					goto IL_90;
				case 5:
					goto IL_D1;
				}
				goto IL_55;
			}
			IL_90:
			IL_9F:
			goto IL_D3;
			IL_D1:
			return null;
			IL_55:
			if (true)
			{
			}
			inputTitle = this.ᜀ.Cells[0].DataValidation.InputTitle;
			cells = this.ᜀ.Cells;
			num2 = 0;
			num3 = cells.Length;
			num = 4;
			goto IL_36;
		}
		}
	}

	// Token: 0x06005639 RID: 22073 RVA: 0x0036EF38 File Offset: 0x0036DF38
	public void ᜄ(string A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.\u1717), A_0);
	}

	// Token: 0x0600563A RID: 22074 RVA: 0x0036EF88 File Offset: 0x0036DF88
	public string ᜉ()
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_CB:
				if (true)
				{
				}
				num = 0;
				break;
			default:
				if (false)
				{
				}
				goto IL_55;
			}
			int num2;
			int num3;
			IXLSRange[] cells;
			string inputMessage;
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
				{
					if (num2 >= num3)
					{
						num = 4;
						continue;
					}
					IXLSRange ixlsrange = cells[num2];
					num = 2;
					continue;
				}
				case 1:
					goto IL_C9;
				case 2:
				{
					IXLSRange ixlsrange;
					if (ixlsrange.DataValidation.InputMessage != inputMessage)
					{
						num = 1;
						continue;
					}
					num2++;
					num = 5;
					continue;
				}
				case 3:
					goto IL_88;
				case 4:
					return inputMessage;
				case 5:
					goto IL_97;
				}
				goto IL_55;
			}
			IL_88:
			IL_97:
			goto IL_CB;
			IL_C9:
			return null;
			IL_55:
			inputMessage = this.ᜀ.Cells[0].DataValidation.InputMessage;
			cells = this.ᜀ.Cells;
			num2 = 0;
			num3 = cells.Length;
			num = 3;
			goto IL_36;
		}
		}
	}

	// Token: 0x0600563B RID: 22075 RVA: 0x0036F088 File Offset: 0x0036E088
	public void ᜁ(string A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.\u1716), A_0);
	}

	// Token: 0x0600563C RID: 22076 RVA: 0x0036F0D8 File Offset: 0x0036E0D8
	public string ᜐ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string errorTitle = this.ᜀ.Cells[0].DataValidation.ErrorTitle;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 2;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						goto IL_CC;
					case 1:
						if (true)
						{
						}
						if (ixlsrange.DataValidation.ErrorTitle != errorTitle)
						{
							num3 = 4;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D8;
						default:
							if (false)
							{
							}
							num3 = 0;
							continue;
						}
						break;
					case 2:
						goto IL_CC;
					case 3:
						return errorTitle;
					case 4:
						goto IL_CA;
					case 5:
						goto IL_D8;
					}
					break;
					IL_D8:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 1;
					continue;
					IL_CC:
					num3 = 5;
				}
			}
			IL_CA:
			return null;
		}
	}

	// Token: 0x0600563D RID: 22077 RVA: 0x0036F1DC File Offset: 0x0036E1DC
	public void ᜃ(string A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.\u1715), A_0);
	}

	// Token: 0x0600563E RID: 22078 RVA: 0x0036F22C File Offset: 0x0036E22C
	public string \u1713()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string errorMessage = this.ᜀ.Cells[0].DataValidation.ErrorMessage;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 3;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						goto IL_CD;
					case 1:
						goto IL_DB;
					case 2:
						if (ixlsrange.DataValidation.ErrorMessage != errorMessage)
						{
							num3 = 0;
							continue;
						}
						if (true)
						{
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DB;
						default:
							if (false)
							{
							}
							num3 = 5;
							continue;
						}
						break;
					case 3:
						goto IL_CF;
					case 4:
						return errorMessage;
					case 5:
						goto IL_CF;
					}
					break;
					IL_DB:
					if (num >= num2)
					{
						num3 = 4;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 2;
					continue;
					IL_CF:
					num3 = 1;
				}
			}
			IL_CD:
			return null;
		}
	}

	// Token: 0x0600563F RID: 22079 RVA: 0x0036F334 File Offset: 0x0036E334
	public void ᜅ(string A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.\u1714), A_0);
	}

	// Token: 0x06005640 RID: 22080 RVA: 0x0036F384 File Offset: 0x0036E384
	public string ᜇ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string formula = this.ᜀ[this.ᜀ.Row, this.ᜀ.Column].DataValidation.Formula1;
				int num = this.ᜀ.Row;
				int lastRow = this.ᜀ.LastRow;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_91;
					case 1:
						return formula;
					case 2:
						goto IL_12A;
					case 3:
						num++;
						goto IL_178;
					case 4:
						goto IL_12C;
					case 5:
					{
						int num3;
						if (this.ᜀ[num, num3].DataValidation.Formula1 != formula)
						{
							num2 = 2;
							continue;
						}
						num3++;
						num2 = 9;
						continue;
					}
					case 6:
					{
						if (num > lastRow)
						{
							num2 = 1;
							continue;
						}
						int num3 = this.ᜀ.Column;
						int lastColumn = this.ᜀ.LastColumn;
						num2 = 0;
						continue;
					}
					case 7:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_178;
						default:
						{
							if (false)
							{
							}
							int num3;
							int lastColumn;
							if (num3 > lastColumn)
							{
								num2 = 3;
								continue;
							}
							num2 = 5;
							continue;
						}
						}
						break;
					case 8:
						goto IL_12C;
					case 9:
						goto IL_91;
					}
					break;
					IL_91:
					num2 = 7;
					continue;
					IL_12C:
					num2 = 6;
					continue;
					IL_178:
					num2 = 8;
				}
			}
			IL_12A:
			return null;
		}
	}

	// Token: 0x06005641 RID: 22081 RVA: 0x0036F51C File Offset: 0x0036E51C
	public void ᜂ(string A_0)
	{
		XlsWorkbook xlsWorkbook;
		FormulaUtil formulaUtil;
		for (;;)
		{
			xlsWorkbook = (XlsWorkbook)this.ᜀ.Worksheet.Workbook;
			formulaUtil = xlsWorkbook.FormulaUtil;
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					if (true)
					{
					}
					num = 4;
					continue;
				case 2:
					A_0 = UtilityMethods.ᜀ(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_92;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 3:
					if (A_0[0] == '=')
					{
						num = 2;
						continue;
					}
					goto IL_E0;
				case 4:
					if (A_0.Length > 0)
					{
						num = 0;
						continue;
					}
					goto IL_E0;
				case 5:
					goto IL_92;
				case 6:
					if (A_0 != null)
					{
						num = 1;
						continue;
					}
					goto IL_E0;
				}
				break;
			}
		}
		IL_92:
		IL_E0:
		Ptg[] array = XlsValidation.ᜀ(ref A_0, null, (XlsWorksheet)this.ᜀ.Worksheet, 0, 0);
		array = formulaUtil.ᜀ(array, this.ᜀ.Row, this.ᜀ.Column, xlsWorkbook);
		this.ᜀ(new spr\u173D.ᜀ(this.ᜁ), array);
	}

	// Token: 0x06005642 RID: 22082 RVA: 0x0036F658 File Offset: 0x0036E658
	public DateTime ᜊ()
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				DateTime dateTime = this.ᜀ.Cells[0].DataValidation.DateTime1;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 3;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						goto IL_DC;
					case 1:
						if (ixlsrange.DataValidation.DateTime1 != dateTime)
						{
							num3 = 2;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DC;
						default:
							if (false)
							{
							}
							num3 = 5;
							continue;
						}
						break;
					case 2:
						goto IL_CE;
					case 3:
						goto IL_D0;
					case 4:
						return dateTime;
					case 5:
						goto IL_D0;
					}
					break;
					IL_DC:
					if (num >= num2)
					{
						num3 = 4;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 1;
					continue;
					IL_D0:
					num3 = 0;
				}
			}
			IL_CE:
			return DateTime.MinValue;
		}
	}

	// Token: 0x06005643 RID: 22083 RVA: 0x0036F760 File Offset: 0x0036E760
	public void ᜀ(DateTime A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.\u1712), A_0);
	}

	// Token: 0x06005644 RID: 22084 RVA: 0x0036F7B4 File Offset: 0x0036E7B4
	public string ᜑ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				string formula = this.ᜀ.Cells[0].DataValidation.Formula2;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 3;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						goto IL_CD;
					case 1:
						return formula;
					case 2:
						goto IL_DB;
					case 3:
						if (true)
						{
						}
						goto IL_CF;
					case 4:
						goto IL_CF;
					case 5:
						if (ixlsrange.DataValidation.Formula2 != formula)
						{
							num3 = 0;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DB;
						default:
							if (false)
							{
							}
							num3 = 4;
							continue;
						}
						break;
					}
					break;
					IL_DB:
					if (num >= num2)
					{
						num3 = 1;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 5;
					continue;
					IL_CF:
					num3 = 2;
				}
			}
			IL_CD:
			return null;
		}
	}

	// Token: 0x06005645 RID: 22085 RVA: 0x0036F8BC File Offset: 0x0036E8BC
	public void ᜀ(string A_0)
	{
		XlsWorkbook xlsWorkbook;
		FormulaUtil formulaUtil;
		for (;;)
		{
			xlsWorkbook = (XlsWorkbook)this.ᜀ.Worksheet.Workbook;
			formulaUtil = xlsWorkbook.FormulaUtil;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0[0] == '=')
					{
						num = 3;
						continue;
					}
					goto IL_E0;
				case 1:
					num = 0;
					continue;
				case 2:
					goto IL_9A;
				case 3:
					A_0 = UtilityMethods.ᜀ(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9A;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 4:
					if (A_0 != null)
					{
						num = 6;
						continue;
					}
					goto IL_E0;
				case 5:
					if (A_0.Length > 0)
					{
						num = 1;
						continue;
					}
					goto IL_E0;
				case 6:
					if (true)
					{
					}
					num = 5;
					continue;
				}
				break;
			}
		}
		IL_9A:
		IL_E0:
		Ptg[] array = XlsValidation.ᜀ(ref A_0, null, (XlsWorksheet)this.ᜀ.Worksheet, 0, 0);
		array = formulaUtil.ᜀ(array, this.ᜀ.Row, this.ᜀ.Column, xlsWorkbook);
		this.ᜀ(new spr\u173D.ᜀ(this.ᜀ), array);
	}

	// Token: 0x06005646 RID: 22086 RVA: 0x0036F9F8 File Offset: 0x0036E9F8
	public DateTime ᜌ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				DateTime dateTime = this.ᜀ.Cells[0].DataValidation.DateTime2;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 0;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						goto IL_D3;
					case 1:
						if (ixlsrange.DataValidation.DateTime2 != dateTime)
						{
							num3 = 4;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DF;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num3 = 5;
							continue;
						}
						break;
					case 2:
						return dateTime;
					case 3:
						goto IL_DF;
					case 4:
						goto IL_D1;
					case 5:
						goto IL_D3;
					}
					break;
					IL_DF:
					if (num >= num2)
					{
						num3 = 2;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 1;
					continue;
					IL_D3:
					num3 = 3;
				}
			}
			IL_D1:
			return DateTime.MinValue;
		}
	}

	// Token: 0x06005647 RID: 22087 RVA: 0x0036FB04 File Offset: 0x0036EB04
	public void ᜁ(DateTime A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜐ), A_0);
	}

	// Token: 0x06005648 RID: 22088 RVA: 0x0036FB58 File Offset: 0x0036EB58
	public CellDataType ᜂ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				CellDataType allowType = this.ᜀ.Cells[0].DataValidation.AllowType;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 3;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						return CellDataType.Any;
					case 1:
						return allowType;
					case 2:
						if (ixlsrange.DataValidation.AllowType != allowType)
						{
							num3 = 0;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D3;
						default:
							if (false)
							{
							}
							num3 = 4;
							continue;
						}
						break;
					case 3:
						goto IL_BF;
					case 4:
						goto IL_BF;
					case 5:
						goto IL_D3;
					}
					break;
					IL_D3:
					if (num >= num2)
					{
						num3 = 1;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 2;
					continue;
					IL_BF:
					if (true)
					{
					}
					num3 = 5;
				}
			}
			return CellDataType.Any;
		}
	}

	// Token: 0x06005649 RID: 22089 RVA: 0x0036FC58 File Offset: 0x0036EC58
	public void ᜀ(CellDataType A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜏ), A_0);
	}

	// Token: 0x0600564A RID: 22090 RVA: 0x0036FCAC File Offset: 0x0036ECAC
	public ValidationComparisonOperator ᜄ()
	{
		switch (0)
		{
		default:
		{
			ValidationComparisonOperator compareOperator;
			for (;;)
			{
				compareOperator = this.ᜀ.Cells[0].DataValidation.CompareOperator;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 0;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						goto IL_BF;
					case 1:
						goto IL_CB;
					case 2:
						goto IL_DB;
					case 3:
						if (ixlsrange.DataValidation.CompareOperator != compareOperator)
						{
							num3 = 4;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CB;
						default:
							if (false)
							{
							}
							num3 = 5;
							continue;
						}
						break;
					case 4:
						return ValidationComparisonOperator.Between;
					case 5:
						goto IL_BF;
					}
					break;
					IL_CB:
					if (num >= num2)
					{
						num3 = 2;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 3;
					continue;
					IL_BF:
					num3 = 1;
				}
			}
			return ValidationComparisonOperator.Between;
			IL_DB:
			if (true)
			{
			}
			return compareOperator;
		}
		}
	}

	// Token: 0x0600564B RID: 22091 RVA: 0x0036FDAC File Offset: 0x0036EDAC
	public void ᜀ(ValidationComparisonOperator A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜎ), A_0);
	}

	// Token: 0x0600564C RID: 22092 RVA: 0x0036FE00 File Offset: 0x0036EE00
	public bool \u1716()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool isListInFormula = this.ᜀ.Cells[0].DataValidation.IsListInFormula;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 2;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						if (ixlsrange.DataValidation.IsListInFormula != isListInFormula)
						{
							num3 = 1;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D3;
						default:
							if (false)
							{
							}
							num3 = 3;
							continue;
						}
						break;
					case 1:
						return false;
					case 2:
						goto IL_C7;
					case 3:
						goto IL_C7;
					case 4:
						goto IL_D3;
					case 5:
						return isListInFormula;
					}
					break;
					IL_D3:
					if (num >= num2)
					{
						num3 = 5;
						continue;
					}
					ixlsrange = cells[num];
					if (true)
					{
					}
					num3 = 0;
					continue;
					IL_C7:
					num3 = 4;
				}
			}
			return false;
		}
	}

	// Token: 0x0600564D RID: 22093 RVA: 0x0036FF00 File Offset: 0x0036EF00
	public void ᜅ(bool A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.\u170D), A_0);
	}

	// Token: 0x0600564E RID: 22094 RVA: 0x0036FF54 File Offset: 0x0036EF54
	public bool ᜃ()
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				bool ignoreBlank = this.ᜀ.Cells[0].DataValidation.IgnoreBlank;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 1;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						return false;
					case 1:
						goto IL_C7;
					case 2:
						goto IL_C7;
					case 3:
						return ignoreBlank;
					case 4:
						goto IL_D3;
					case 5:
						if (ixlsrange.DataValidation.IgnoreBlank != ignoreBlank)
						{
							num3 = 0;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D3;
						default:
							if (false)
							{
							}
							num3 = 2;
							continue;
						}
						break;
					}
					break;
					IL_D3:
					if (num >= num2)
					{
						num3 = 3;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 5;
					continue;
					IL_C7:
					num3 = 4;
				}
			}
			return false;
		}
	}

	// Token: 0x0600564F RID: 22095 RVA: 0x00370054 File Offset: 0x0036F054
	public void ᜁ(bool A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜌ), A_0);
	}

	// Token: 0x06005650 RID: 22096 RVA: 0x003700A8 File Offset: 0x0036F0A8
	public bool ᜅ()
	{
		switch (0)
		{
		default:
		{
			bool isSuppressDropDownArrow;
			for (;;)
			{
				isSuppressDropDownArrow = this.ᜀ.Cells[0].DataValidation.IsSuppressDropDownArrow;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 4;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						goto IL_DB;
					case 1:
						return false;
					case 2:
						goto IL_BF;
					case 3:
						if (ixlsrange.DataValidation.IsSuppressDropDownArrow != isSuppressDropDownArrow)
						{
							num3 = 1;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CB;
						default:
							if (false)
							{
							}
							num3 = 2;
							continue;
						}
						break;
					case 4:
						goto IL_BF;
					case 5:
						goto IL_CB;
					}
					break;
					IL_CB:
					if (num >= num2)
					{
						num3 = 0;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 3;
					continue;
					IL_BF:
					num3 = 5;
				}
			}
			return false;
			IL_DB:
			if (true)
			{
			}
			return isSuppressDropDownArrow;
		}
		}
	}

	// Token: 0x06005651 RID: 22097 RVA: 0x003701A8 File Offset: 0x0036F1A8
	public void ᜀ(bool A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜋ), A_0);
	}

	// Token: 0x06005652 RID: 22098 RVA: 0x003701FC File Offset: 0x0036F1FC
	public bool \u1718()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool showInput = this.ᜀ.Cells[0].DataValidation.ShowInput;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 5;
				for (;;)
				{
					IXLSRange ixlsrange;
					switch (num3)
					{
					case 0:
						return showInput;
					case 1:
						goto IL_D6;
					case 2:
						goto IL_CA;
					case 3:
						if (ixlsrange.DataValidation.ShowInput != showInput)
						{
							num3 = 4;
							continue;
						}
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D6;
						default:
							if (false)
							{
							}
							num3 = 2;
							continue;
						}
						break;
					case 4:
						return false;
					case 5:
						if (true)
						{
						}
						goto IL_CA;
					}
					break;
					IL_D6:
					if (num >= num2)
					{
						num3 = 0;
						continue;
					}
					ixlsrange = cells[num];
					num3 = 3;
					continue;
					IL_CA:
					num3 = 1;
				}
			}
			return false;
		}
	}

	// Token: 0x06005653 RID: 22099 RVA: 0x003702FC File Offset: 0x0036F2FC
	public void ᜃ(bool A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜊ), A_0);
	}

	// Token: 0x06005654 RID: 22100 RVA: 0x00370350 File Offset: 0x0036F350
	public bool ᜁ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2F:
				bool showError = this.ᜀ.Cells[0].DataValidation.ShowError;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				for (;;)
				{
					IL_59:
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_AA;
						case 1:
							return showError;
						case 2:
							goto IL_AA;
						case 3:
							return false;
						case 4:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.DataValidation.ShowError != showError)
							{
								num3 = 3;
								continue;
							}
							num++;
							num3 = 0;
							continue;
						}
						case 5:
							if (num < num2)
							{
								IXLSRange ixlsrange = cells[num];
								num3 = 4;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_59;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								num3 = 1;
								continue;
							}
							break;
						}
						goto IL_2F;
						IL_AA:
						num3 = 5;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x06005655 RID: 22101 RVA: 0x0037044C File Offset: 0x0036F44C
	public void ᜆ(bool A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜉ), A_0);
	}

	// Token: 0x06005656 RID: 22102 RVA: 0x003704A0 File Offset: 0x0036F4A0
	public int \u1717()
	{
		switch (0)
		{
		default:
			if (true)
			{
			}
			for (;;)
			{
				IL_37:
				int promptBoxHPosition = this.ᜀ.Cells[0].DataValidation.PromptBoxHPosition;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				for (;;)
				{
					IL_61:
					int num3 = 5;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							return promptBoxHPosition;
						case 1:
							if (num < num2)
							{
								IXLSRange ixlsrange = cells[num];
								num3 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_61;
							default:
								if (false)
								{
								}
								num3 = 0;
								continue;
							}
							break;
						case 2:
							return int.MinValue;
						case 3:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.DataValidation.PromptBoxHPosition != promptBoxHPosition)
							{
								num3 = 2;
								continue;
							}
							num++;
							num3 = 4;
							continue;
						}
						case 4:
							goto IL_B9;
						case 5:
							goto IL_B9;
						}
						goto IL_37;
						IL_B9:
						num3 = 1;
					}
				}
			}
			return int.MinValue;
		}
	}

	// Token: 0x06005657 RID: 22103 RVA: 0x003705A4 File Offset: 0x0036F5A4
	public void ᜀ(int A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜈ), A_0);
	}

	// Token: 0x06005658 RID: 22104 RVA: 0x003705F8 File Offset: 0x0036F5F8
	public int \u170D()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2F:
				int promptBoxVPosition = this.ᜀ.Cells[0].DataValidation.PromptBoxVPosition;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				for (;;)
				{
					IL_59:
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.DataValidation.PromptBoxVPosition != promptBoxVPosition)
							{
								num3 = 1;
								continue;
							}
							num++;
							num3 = 3;
							continue;
						}
						case 1:
							return int.MinValue;
						case 2:
							if (true)
							{
							}
							goto IL_B9;
						case 3:
							goto IL_B9;
						case 4:
							return promptBoxVPosition;
						case 5:
							if (num < num2)
							{
								IXLSRange ixlsrange = cells[num];
								num3 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_59;
							default:
								if (false)
								{
								}
								num3 = 4;
								continue;
							}
							break;
						}
						goto IL_2F;
						IL_B9:
						num3 = 5;
					}
				}
			}
			return int.MinValue;
		}
	}

	// Token: 0x06005659 RID: 22105 RVA: 0x003706FC File Offset: 0x0036F6FC
	public void ᜁ(int A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜇ), A_0);
	}

	// Token: 0x0600565A RID: 22106 RVA: 0x00370750 File Offset: 0x0036F750
	public bool \u1719()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2F:
				bool isInputVisible = this.ᜀ.Cells[0].DataValidation.IsInputVisible;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				for (;;)
				{
					IL_59:
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							return isInputVisible;
						case 1:
							goto IL_B2;
						case 2:
							goto IL_B2;
						case 3:
							return false;
						case 4:
							if (num < num2)
							{
								IXLSRange ixlsrange = cells[num];
								num3 = 5;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_59;
							default:
								if (false)
								{
								}
								num3 = 0;
								continue;
							}
							break;
						case 5:
						{
							if (true)
							{
							}
							IXLSRange ixlsrange;
							if (ixlsrange.DataValidation.IsInputVisible != isInputVisible)
							{
								num3 = 3;
								continue;
							}
							num++;
							num3 = 1;
							continue;
						}
						}
						goto IL_2F;
						IL_B2:
						num3 = 4;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x0600565B RID: 22107 RVA: 0x0037084C File Offset: 0x0036F84C
	public void ᜂ(bool A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜆ), A_0);
	}

	// Token: 0x0600565C RID: 22108 RVA: 0x003708A0 File Offset: 0x0036F8A0
	public bool ᜎ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2F:
				bool isInputPositionFixed = this.ᜀ.Cells[0].DataValidation.IsInputPositionFixed;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				for (;;)
				{
					IL_59:
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.DataValidation.IsInputPositionFixed != isInputPositionFixed)
							{
								num3 = 4;
								continue;
							}
							num++;
							num3 = 5;
							continue;
						}
						case 1:
							return isInputPositionFixed;
						case 2:
							goto IL_B5;
						case 3:
							if (num < num2)
							{
								if (true)
								{
								}
								IXLSRange ixlsrange = cells[num];
								num3 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_59;
							default:
								if (false)
								{
								}
								num3 = 1;
								continue;
							}
							break;
						case 4:
							return false;
						case 5:
							goto IL_B5;
						}
						goto IL_2F;
						IL_B5:
						num3 = 3;
					}
				}
			}
			return false;
		}
	}

	// Token: 0x0600565D RID: 22109 RVA: 0x003709A0 File Offset: 0x0036F9A0
	public void ᜄ(bool A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜅ), A_0);
	}

	// Token: 0x0600565E RID: 22110 RVA: 0x003709F4 File Offset: 0x0036F9F4
	public AlertStyleType ᜋ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2F:
				AlertStyleType alertStyle = this.ᜀ.Cells[0].DataValidation.AlertStyle;
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				for (;;)
				{
					IL_59:
					int num3 = 2;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_B5;
						case 1:
							return alertStyle;
						case 2:
							goto IL_B5;
						case 3:
							return AlertStyleType.Stop;
						case 4:
						{
							IXLSRange ixlsrange;
							if (ixlsrange.DataValidation.AlertStyle != alertStyle)
							{
								num3 = 3;
								continue;
							}
							num++;
							if (true)
							{
							}
							num3 = 0;
							continue;
						}
						case 5:
							if (num < num2)
							{
								IXLSRange ixlsrange = cells[num];
								num3 = 4;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_59;
							default:
								if (false)
								{
								}
								num3 = 1;
								continue;
							}
							break;
						}
						goto IL_2F;
						IL_B5:
						num3 = 5;
					}
				}
			}
			return AlertStyleType.Stop;
		}
	}

	// Token: 0x0600565F RID: 22111 RVA: 0x00370AF4 File Offset: 0x0036FAF4
	public void ᜀ(AlertStyleType A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜄ), A_0);
	}

	// Token: 0x06005660 RID: 22112 RVA: 0x00370B48 File Offset: 0x0036FB48
	public string[] \u171C()
	{
		for (;;)
		{
			if (true)
			{
			}
			string[] values = this.ᜀ.Cells[0].DataValidation.Values;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜇ() == this.ᜀ.Cells[0].DataValidation.Formula1)
					{
						num = 3;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_43;
					default:
						goto IL_AA;
					}
					break;
				case 1:
					goto IL_55;
				case 2:
					if (values == null)
					{
						goto IL_43;
					}
					num = 0;
					continue;
				case 3:
					return values;
				}
				break;
				IL_43:
				num = 1;
			}
		}
		IL_55:
		return null;
		IL_AA:
		if (false)
		{
		}
		return null;
	}

	// Token: 0x06005661 RID: 22113 RVA: 0x00370C08 File Offset: 0x0036FC08
	public void ᜀ(string[] A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜃ), A_0);
	}

	// Token: 0x06005662 RID: 22114 RVA: 0x00370C58 File Offset: 0x0036FC58
	public IXLSRange \u1715()
	{
		for (;;)
		{
			IXLSRange dataRange = this.ᜀ.Cells[0].DataValidation.DataRange;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return dataRange;
				case 1:
					goto IL_43;
				case 2:
					if (true)
					{
					}
					if (this.ᜇ() == this.ᜀ.Cells[0].DataValidation.Formula1)
					{
						num = 0;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					default:
						goto IL_AA;
					}
					break;
				case 3:
					if (dataRange == null)
					{
						goto IL_3B;
					}
					num = 2;
					continue;
				}
				break;
				IL_3B:
				num = 1;
			}
		}
		IL_43:
		return null;
		IL_AA:
		if (false)
		{
		}
		return null;
	}

	// Token: 0x06005663 RID: 22115 RVA: 0x00370D18 File Offset: 0x0036FD18
	public void ᜀ(IXLSRange A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜂ), A_0);
	}

	// Token: 0x06005664 RID: 22116 RVA: 0x00370D68 File Offset: 0x0036FD68
	public spr\u1DF5 \u1714()
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
		return (this.ᜀ as XlsRange).Application;
	}

	// Token: 0x06005665 RID: 22117 RVA: 0x00370DB4 File Offset: 0x0036FDB4
	public object ᜆ()
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
		return this.ᜀ;
	}

	// Token: 0x06005666 RID: 22118 RVA: 0x00370DF8 File Offset: 0x0036FDF8
	public Ptg[] \u171A()
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
		throw new NotImplementedException();
	}

	// Token: 0x06005667 RID: 22119 RVA: 0x00370E38 File Offset: 0x0036FE38
	public void ᜁ(Ptg[] A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜁ), A_0);
	}

	// Token: 0x06005668 RID: 22120 RVA: 0x00370E88 File Offset: 0x0036FE88
	public Ptg[] \u171B()
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
		throw new NotImplementedException();
	}

	// Token: 0x06005669 RID: 22121 RVA: 0x00370EC8 File Offset: 0x0036FEC8
	public void ᜀ(Ptg[] A_0)
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
		this.ᜀ(new spr\u173D.ᜀ(this.ᜀ), A_0);
	}

	// Token: 0x0600566A RID: 22122 RVA: 0x00370F18 File Offset: 0x0036FF18
	private void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				ICombinedRange combinedRange = this.ᜀ as ICombinedRange;
				XlsWorksheet xlsWorksheet = this.ᜀ.Worksheet as XlsWorksheet;
				XlsValidationWrapper dataValidation = xlsWorksheet[this.ᜀ.Row, this.ᜀ.Column].DataValidation;
				XlsValidation xlsValidation = dataValidation.Wrapped;
				XlsDataValidationTable xlsDataValidationTable = xlsWorksheet.InnerDVTable;
				Rectangle[] rectangles = combinedRange.GetRectangles();
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_10A;
					case 1:
						goto IL_108;
					case 2:
						return;
					case 3:
						if (combinedRange.Column != combinedRange.LastColumn)
						{
							num = 0;
							continue;
						}
						goto IL_B5;
					case 4:
						if (combinedRange.Row == combinedRange.LastRow)
						{
							num = 6;
							continue;
						}
						goto IL_10A;
					case 5:
						goto IL_B5;
					case 6:
						num = 3;
						continue;
					case 7:
						if (xlsDataValidationTable.ᜀ(combinedRange.Row, combinedRange.Column) != null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_108;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					break;
					IL_B5:
					xlsValidation.ᜀ(combinedRange);
					num = 7;
					continue;
					IL_10A:
					xlsDataValidationTable.Remove(rectangles);
					num = 5;
					continue;
					IL_108:
					if (true)
					{
					}
					xlsValidation.ParentCollection.Add(xlsValidation);
					num = 2;
				}
			}
			return;
		}
	}

	// Token: 0x0600566B RID: 22123 RVA: 0x003710A0 File Offset: 0x003700A0
	public virtual void ᜏ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				for (;;)
				{
					if (true)
					{
					}
					this.ᜀ(new spr\u173D.ᜁ(this.ᜁ));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_5E;
					}
				}
				IL_5E:
				if (false)
				{
				}
				num = 2;
				continue;
			case 2:
				goto IL_6C;
			}
			if (base.BeginCallsCount != 0)
			{
				break;
			}
			num = 0;
		}
		IL_6C:
		base.BeginUpdate();
	}

	// Token: 0x0600566C RID: 22124 RVA: 0x0037112C File Offset: 0x0037012C
	public virtual void ᜈ()
	{
		for (;;)
		{
			base.EndUpdate();
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.BeginCallsCount == 0)
					{
						num = 5;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					goto IL_4F;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4F;
					default:
					{
						if (false)
						{
						}
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 4;
							continue;
						}
						this.ᜁ[num2].EndUpdate();
						num2++;
						num = 2;
						continue;
					}
					}
					break;
				case 4:
					this.ᜁ.Clear();
					num = 1;
					continue;
				case 5:
				{
					int num2 = 0;
					int count = this.ᜁ.Count;
					num = 6;
					continue;
				}
				case 6:
					goto IL_4F;
				}
				break;
				IL_4F:
				num = 3;
			}
		}
	}

	// Token: 0x0600566D RID: 22125 RVA: 0x0037121C File Offset: 0x0037021C
	private void ᜀ(spr\u173D.ᜁ A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Rectangle[] rectangles = (this.ᜀ as ICombinedRange).GetRectangles();
				int num = 0;
				int num2 = rectangles.Length;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
					{
						if (num >= num2)
						{
							num3 = 1;
							continue;
						}
						Rectangle a_ = rectangles[num];
						this.ᜀ(a_, A_0);
						num++;
						num3 = 3;
						continue;
					}
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_47;
						default:
							goto IL_A5;
						}
						break;
					case 2:
						goto IL_47;
					case 3:
						goto IL_49;
					}
					break;
					IL_49:
					num3 = 0;
					continue;
					IL_47:
					goto IL_49;
				}
			}
			IL_A5:
			if (false)
			{
			}
			if (true)
			{
			}
			return;
		}
	}

	// Token: 0x0600566E RID: 22126 RVA: 0x003712DC File Offset: 0x003702DC
	private void ᜀ(Rectangle A_0, spr\u173D.ᜁ A_1)
	{
		int num3;
		int num5;
		int num6;
		int num8;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_A3:
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_1B7;
				case 2:
				{
					int num2 = num3;
					num = 16;
					continue;
				}
				case 3:
					goto IL_195;
				case 4:
					goto IL_149;
				case 5:
					goto IL_195;
				case 6:
					goto IL_149;
				case 7:
				{
					if (A_0.Height >= A_0.Width)
					{
						num = 2;
						continue;
					}
					int num4 = num5;
					num = 4;
					continue;
				}
				case 8:
					goto IL_16B;
				case 9:
				{
					int num2;
					if (num2 > num6)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					int num7 = num5;
					num = 5;
					continue;
				}
				case 10:
				{
					int num4;
					num4++;
					num = 6;
					continue;
				}
				case 11:
				{
					int num4;
					if (num4 > num8)
					{
						num = 12;
						continue;
					}
					int num9 = num3;
					num = 13;
					continue;
				}
				case 12:
					return;
				case 13:
					goto IL_1B7;
				case 14:
				{
					int num2;
					num2++;
					num = 8;
					continue;
				}
				case 15:
				{
					int num7;
					if (num7 > num8)
					{
						num = 14;
						continue;
					}
					int num2;
					A_1(num7, num2);
					num7++;
					num = 3;
					continue;
				}
				case 16:
					goto IL_16B;
				case 17:
				{
					int num9;
					if (num9 > num6)
					{
						num = 10;
						continue;
					}
					int num4;
					A_1(num4, num9);
					num9++;
					num = 1;
					continue;
				}
				}
				goto IL_7B;
				IL_149:
				num = 11;
				continue;
				IL_16B:
				num = 9;
				continue;
				IL_195:
				num = 15;
				continue;
				IL_1B7:
				num = 17;
			}
			return;
		}
		default:
			if (false)
			{
			}
			switch (0)
			{
			}
			break;
		}
		IL_7B:
		num5 = A_0.Top + 1;
		num8 = A_0.Bottom + 1;
		num3 = A_0.Left + 1;
		num6 = A_0.Right + 1;
		goto IL_A3;
	}

	// Token: 0x0600566F RID: 22127 RVA: 0x00371510 File Offset: 0x00370510
	private void ᜁ(int A_0, int A_1)
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
		IDataValidation dataValidation = this.ᜀ.Worksheet[A_0, A_1].DataValidation;
		dataValidation.BeginUpdate();
		this.ᜁ.Add(dataValidation);
	}

	// Token: 0x06005670 RID: 22128 RVA: 0x00371578 File Offset: 0x00370578
	private void ᜀ(int A_0, int A_1)
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
	}

	// Token: 0x06005671 RID: 22129 RVA: 0x003715B4 File Offset: 0x003705B4
	private void ᜁ(spr\u173D.ᜀ A_0, object A_1)
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
			for (;;)
			{
				IXLSRange[] cells = this.ᜀ.Cells;
				int num = 0;
				int num2 = cells.Length;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_4C;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
						default:
							if (false)
							{
							}
							goto IL_4C;
						}
						break;
					case 2:
						if (num >= num2)
						{
							num3 = 3;
							continue;
						}
						goto IL_6E;
					case 3:
						return;
					}
					break;
					IL_4C:
					num3 = 2;
					continue;
					IL_6E:
					IXLSRange ixlsrange = cells[num];
					A_0(ixlsrange.DataValidation, A_1);
					num++;
					num3 = 1;
				}
			}
			return;
		}
	}

	// Token: 0x06005672 RID: 22130 RVA: 0x0037166C File Offset: 0x0037066C
	private void ᜀ(spr\u173D.ᜀ A_0, object A_1)
	{
		for (;;)
		{
			IL_34:
			this.BeginUpdate();
			int num = 0;
			int count = this.ᜁ.Count;
			int num2 = 3;
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
					switch (num2)
					{
					case 0:
						goto IL_94;
					case 1:
						goto IL_66;
					case 2:
					{
						if (num >= count)
						{
							num2 = 1;
							continue;
						}
						IDataValidation a_ = this.ᜁ[num];
						A_0(a_, A_1);
						num++;
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					case 3:
						goto IL_52;
					}
					goto IL_34;
				}
				IL_52:
				num2 = 2;
				continue;
				IL_94:
				goto IL_52;
			}
		}
		IL_66:
		this.EndUpdate();
	}

	// Token: 0x06005673 RID: 22131 RVA: 0x00371720 File Offset: 0x00370720
	private void \u1717(IDataValidation A_0, object A_1)
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
		A_0.InputTitle = (A_1 as string);
	}

	// Token: 0x06005674 RID: 22132 RVA: 0x00371768 File Offset: 0x00370768
	private void \u1716(IDataValidation A_0, object A_1)
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
		A_0.InputMessage = (A_1 as string);
	}

	// Token: 0x06005675 RID: 22133 RVA: 0x003717B0 File Offset: 0x003707B0
	private void \u1715(IDataValidation A_0, object A_1)
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
		A_0.ErrorTitle = (A_1 as string);
	}

	// Token: 0x06005676 RID: 22134 RVA: 0x003717F8 File Offset: 0x003707F8
	private void \u1714(IDataValidation A_0, object A_1)
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
		A_0.ErrorMessage = (A_1 as string);
	}

	// Token: 0x06005677 RID: 22135 RVA: 0x00371840 File Offset: 0x00370840
	private void \u1713(IDataValidation A_0, object A_1)
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
		A_0.Formula1 = (A_1 as string);
	}

	// Token: 0x06005678 RID: 22136 RVA: 0x00371888 File Offset: 0x00370888
	private void \u1712(IDataValidation A_0, object A_1)
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
		A_0.DateTime1 = (DateTime)A_1;
	}

	// Token: 0x06005679 RID: 22137 RVA: 0x003718D0 File Offset: 0x003708D0
	private void ᜑ(IDataValidation A_0, object A_1)
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
		A_0.Formula2 = (A_1 as string);
	}

	// Token: 0x0600567A RID: 22138 RVA: 0x00371918 File Offset: 0x00370918
	private void ᜐ(IDataValidation A_0, object A_1)
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
		A_0.DateTime2 = (DateTime)A_1;
	}

	// Token: 0x0600567B RID: 22139 RVA: 0x00371960 File Offset: 0x00370960
	private void ᜏ(IDataValidation A_0, object A_1)
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
		A_0.AllowType = (CellDataType)A_1;
	}

	// Token: 0x0600567C RID: 22140 RVA: 0x003719A8 File Offset: 0x003709A8
	private void ᜎ(IDataValidation A_0, object A_1)
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
		A_0.CompareOperator = (ValidationComparisonOperator)A_1;
	}

	// Token: 0x0600567D RID: 22141 RVA: 0x003719F0 File Offset: 0x003709F0
	private void \u170D(IDataValidation A_0, object A_1)
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
		A_0.IsListInFormula = (bool)A_1;
	}

	// Token: 0x0600567E RID: 22142 RVA: 0x00371A38 File Offset: 0x00370A38
	private void ᜌ(IDataValidation A_0, object A_1)
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
		A_0.IgnoreBlank = (bool)A_1;
	}

	// Token: 0x0600567F RID: 22143 RVA: 0x00371A80 File Offset: 0x00370A80
	private void ᜋ(IDataValidation A_0, object A_1)
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
		A_0.IsSuppressDropDownArrow = (bool)A_1;
	}

	// Token: 0x06005680 RID: 22144 RVA: 0x00371AC8 File Offset: 0x00370AC8
	private void ᜊ(IDataValidation A_0, object A_1)
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
		A_0.ShowInput = (bool)A_1;
	}

	// Token: 0x06005681 RID: 22145 RVA: 0x00371B10 File Offset: 0x00370B10
	private void ᜉ(IDataValidation A_0, object A_1)
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
		A_0.ShowError = (bool)A_1;
	}

	// Token: 0x06005682 RID: 22146 RVA: 0x00371B58 File Offset: 0x00370B58
	private void ᜈ(IDataValidation A_0, object A_1)
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
		A_0.PromptBoxHPosition = (int)A_1;
	}

	// Token: 0x06005683 RID: 22147 RVA: 0x00371BA0 File Offset: 0x00370BA0
	private void ᜇ(IDataValidation A_0, object A_1)
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
		A_0.PromptBoxVPosition = (int)A_1;
	}

	// Token: 0x06005684 RID: 22148 RVA: 0x00371BE8 File Offset: 0x00370BE8
	private void ᜆ(IDataValidation A_0, object A_1)
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
		A_0.IsInputVisible = (bool)A_1;
	}

	// Token: 0x06005685 RID: 22149 RVA: 0x00371C30 File Offset: 0x00370C30
	private void ᜅ(IDataValidation A_0, object A_1)
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
		A_0.IsInputPositionFixed = (bool)A_1;
	}

	// Token: 0x06005686 RID: 22150 RVA: 0x00371C78 File Offset: 0x00370C78
	private void ᜄ(IDataValidation A_0, object A_1)
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
		A_0.AlertStyle = (AlertStyleType)A_1;
	}

	// Token: 0x06005687 RID: 22151 RVA: 0x00371CC0 File Offset: 0x00370CC0
	private void ᜃ(IDataValidation A_0, object A_1)
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
		A_0.Values = (string[])A_1;
	}

	// Token: 0x06005688 RID: 22152 RVA: 0x00371D08 File Offset: 0x00370D08
	private void ᜂ(IDataValidation A_0, object A_1)
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
		A_0.DataRange = (A_1 as IXLSRange);
	}

	// Token: 0x06005689 RID: 22153 RVA: 0x00371D50 File Offset: 0x00370D50
	private void ᜁ(IDataValidation A_0, object A_1)
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
		((spr\u20B0)A_0).ᜀ(A_1 as Ptg[]);
	}

	// Token: 0x0600568A RID: 22154 RVA: 0x00371D9C File Offset: 0x00370D9C
	private void ᜀ(IDataValidation A_0, object A_1)
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
		((spr\u20B0)A_0).ᜁ(A_1 as Ptg[]);
	}

	// Token: 0x0400293E RID: 10558
	private IXLSRange ᜀ;

	// Token: 0x0400293F RID: 10559
	private List<IDataValidation> ᜁ = new List<IDataValidation>();

	// Token: 0x02000591 RID: 1425
	// (Invoke) Token: 0x0600568C RID: 22156
	private delegate void ᜁ(int A_0, int A_1);

	// Token: 0x02000592 RID: 1426
	// (Invoke) Token: 0x06005690 RID: 22160
	private delegate void ᜀ(IDataValidation A_0, object A_1);
}
