using System;
using Spire.Xls;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200030F RID: 783
internal sealed class sprᜑ
{
	// Token: 0x06003025 RID: 12325 RVA: 0x001B68DC File Offset: 0x001B58DC
	public static bool ᜀ(IInternalWorksheet A_0, int A_1, int A_2)
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
		XlsCellRecordCollection cellRecords = A_0.CellRecords;
		return cellRecords.Table.ᜈ(A_1, A_2);
	}

	// Token: 0x06003026 RID: 12326 RVA: 0x001B692C File Offset: 0x001B592C
	public static sprᱧ ᜀ(IInternalWorksheet A_0, int A_1, bool A_2)
	{
		XlsCellRecordCollection cellRecords;
		for (;;)
		{
			cellRecords = A_0.CellRecords;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_77;
				case 1:
					if (true)
					{
					}
					if (cellRecords != null)
					{
						goto IL_79;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_77;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 2:
					if (!A_2)
					{
						num = 0;
						continue;
					}
					goto IL_79;
				case 3:
					num = 2;
					continue;
				}
				break;
			}
		}
		IL_77:
		return null;
		IL_79:
		ExcelVersion version = A_0.Workbook.Version;
		int a_ = (A_0 as XlsObject).AppImplementation.ᜅ();
		return cellRecords.Table.ᜀ(A_1, a_, A_2, version);
	}

	// Token: 0x06003027 RID: 12327 RVA: 0x001B69E0 File Offset: 0x001B59E0
	[CLSCompliant(false)]
	public static spr\u2502 ᜂ(IInternalWorksheet A_0, int A_1)
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
		return sprᜑ.ᜀ(A_0, A_1 - 1, false);
	}

	// Token: 0x06003028 RID: 12328 RVA: 0x001B6A28 File Offset: 0x001B5A28
	public static void ᜁ(IInternalWorksheet A_0, int A_1)
	{
		for (;;)
		{
			int firstColumn = A_0.FirstColumn;
			int lastColumn = A_0.LastColumn;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (lastColumn >= A_1)
					{
						num = 8;
						continue;
					}
					goto IL_D9;
				case 1:
					goto IL_EC;
				case 2:
					if (firstColumn <= A_1)
					{
						num = 7;
						continue;
					}
					goto IL_F6;
				case 3:
					goto IL_F6;
				case 4:
					goto IL_D9;
				case 5:
					if (firstColumn == 2147483647)
					{
						num = 3;
						continue;
					}
					goto IL_93;
				case 6:
					goto IL_93;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FE;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 8:
					num = 9;
					continue;
				case 9:
					if (lastColumn == 2147483647)
					{
						num = 4;
						continue;
					}
					return;
				}
				break;
				IL_93:
				num = 0;
				continue;
				IL_D9:
				A_0.LastColumn = (int)((ushort)A_1);
				num = 1;
				continue;
				IL_FE:
				num = 6;
				continue;
				IL_F6:
				A_0.FirstColumn = (int)((ushort)A_1);
				goto IL_FE;
			}
		}
		IL_EC:
		if (true)
		{
		}
	}

	// Token: 0x06003029 RID: 12329 RVA: 0x001B6B40 File Offset: 0x001B5B40
	public static void ᜀ(IInternalWorksheet A_0, int A_1)
	{
		for (;;)
		{
			int firstRow = A_0.FirstRow;
			int lastRow = A_0.LastRow;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_ED;
				case 1:
					goto IL_D9;
				case 2:
					num = 8;
					continue;
				case 3:
					goto IL_8F;
				case 4:
					if (firstRow <= A_1)
					{
						num = 7;
						continue;
					}
					goto IL_ED;
				case 5:
					if (lastRow >= A_1)
					{
						num = 2;
						continue;
					}
					goto IL_D9;
				case 6:
					if (true)
					{
					}
					if (firstRow < 0)
					{
						num = 0;
						continue;
					}
					goto IL_8F;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F4;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 8:
					if (lastRow < 0)
					{
						num = 1;
						continue;
					}
					return;
				case 9:
					return;
				}
				break;
				IL_8F:
				num = 5;
				continue;
				IL_D9:
				A_0.LastRow = A_1;
				num = 9;
				continue;
				IL_F4:
				num = 3;
				continue;
				IL_ED:
				A_0.FirstRow = A_1;
				goto IL_F4;
			}
		}
	}
}
