using System;
using Spire.Xls;
using Spire.Xls.Collections;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x0200054F RID: 1359
internal class spr\u23A6 : XlsStyle
{
	// Token: 0x0600526B RID: 21099 RVA: 0x00335DFC File Offset: 0x00334DFC
	internal spr\u23A6(XlsWorkbook A_0, string A_1, spr\u23A6 A_2) : base(A_0, A_1, A_2)
	{
	}

	// Token: 0x0600526C RID: 21100 RVA: 0x00335E14 File Offset: 0x00334E14
	internal spr\u23A6(XlsWorkbook A_0, string A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x0600526D RID: 21101 RVA: 0x00335E2C File Offset: 0x00334E2C
	internal spr\u23A6(XlsWorkbook A_0, string A_1, spr\u23A6 A_2, bool A_3) : base(A_0, A_1, A_2, A_3)
	{
	}

	// Token: 0x0600526E RID: 21102 RVA: 0x00335E44 File Offset: 0x00334E44
	internal spr\u23A6(XlsWorkbook A_0, sprᬐ A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x0600526F RID: 21103 RVA: 0x00335E5C File Offset: 0x00334E5C
	public new Workbook ᜀ()
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
		return base.Workbook.InnerWorkBook;
	}

	// Token: 0x06005270 RID: 21104 RVA: 0x00335EA4 File Offset: 0x00334EA4
	public new BordersCollection ᜁ()
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

	// Token: 0x06005271 RID: 21105 RVA: 0x00335EEC File Offset: 0x00334EEC
	public new ExcelFont ᜂ()
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
		return new ExcelFont(base.Font);
	}
}
