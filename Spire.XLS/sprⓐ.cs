using System;
using Spire.Xls.Core.Spreadsheet;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020003BC RID: 956
internal class spr\u24D0 : sprᯣ
{
	// Token: 0x06003A69 RID: 14953 RVA: 0x0020E1AC File Offset: 0x0020D1AC
	public spr\u24D0(XlsWorksheet A_0, int A_1, int A_2)
	{
		int a_ = 3;
		base..ctor();
		if (A_0 == null)
		{
			throw new ArgumentNullException(RecordTableEnumerator.b("䨸区堼娾㕀", a_));
		}
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
	}

	// Token: 0x06003A6A RID: 14954 RVA: 0x0020E1F8 File Offset: 0x0020D1F8
	public void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u223C spr_u223C = this.ᜀ.CellRecords.Table.ᜄ();
				int num = 0;
				int num2 = this.ᜁ - 1;
				int num3 = 0;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_DA;
					case 1:
					{
						sprᱧ sprᱧ;
						if (sprᱧ != null)
						{
							num3 = 6;
							continue;
						}
						goto IL_5F;
					}
					case 2:
					{
						if (num >= this.ᜂ)
						{
							goto IL_EF;
						}
						sprᱧ sprᱧ = spr_u223C.ᜁ(num2);
						spr_u223C.ᜀ(num2, null);
						num3 = 1;
						continue;
					}
					case 3:
						return;
					case 4:
						goto IL_DA;
					case 5:
						goto IL_5F;
					case 6:
					{
						sprᱧ sprᱧ;
						sprᱧ.ᜋ();
						num3 = 5;
						continue;
					}
					}
					break;
					IL_5F:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_EF:
						num3 = 3;
						continue;
					default:
						if (false)
						{
						}
						num++;
						num2++;
						num3 = 4;
						continue;
					}
					IL_DA:
					num3 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x04001982 RID: 6530
	private XlsWorksheet ᜀ;

	// Token: 0x04001983 RID: 6531
	private int ᜁ;

	// Token: 0x04001984 RID: 6532
	private int ᜂ;
}
