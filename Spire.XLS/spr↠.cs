using System;
using Spire.Xls;
using Spire.Xls.Collections;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x020003AD RID: 941
internal class spr\u21A0 : CellBaseStyle
{
	// Token: 0x060038FB RID: 14587 RVA: 0x001FC308 File Offset: 0x001FB308
	public spr\u21A0(CellRange A_0) : base(A_0)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x060038FC RID: 14588 RVA: 0x001FC324 File Offset: 0x001FB324
	public spr\u21A0(XlsRange A_0, int A_1) : base(A_0, A_1)
	{
		this.ᜀ = A_0;
	}

	// Token: 0x060038FD RID: 14589 RVA: 0x001FC340 File Offset: 0x001FB340
	public BordersCollection ᜁ()
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

	// Token: 0x060038FE RID: 14590 RVA: 0x001FC388 File Offset: 0x001FB388
	public ExcelFont ᜂ()
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
		return new ExcelFont(base.Font);
	}

	// Token: 0x060038FF RID: 14591 RVA: 0x001FC3D0 File Offset: 0x001FB3D0
	public new Workbook ᜀ()
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
		return base.Workbook.InnerWorkBook;
	}

	// Token: 0x0400190F RID: 6415
	private new XlsRange ᜀ;
}
