using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000408 RID: 1032
[spr\u2593(TBIFFRecord.ChartChartLine)]
[CLSCompliant(false)]
internal class spr\u233F : BiffRecordRaw
{
	// Token: 0x06003E18 RID: 15896 RVA: 0x00229060 File Offset: 0x00228060
	public DropLineStyleType ᜀ()
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
		return (DropLineStyleType)this.ᜁ;
	}

	// Token: 0x06003E19 RID: 15897 RVA: 0x002290A4 File Offset: 0x002280A4
	public void ᜀ(DropLineStyleType A_0)
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
		this.ᜁ = (ushort)A_0;
	}

	// Token: 0x06003E1A RID: 15898 RVA: 0x002290E8 File Offset: 0x002280E8
	public spr\u233F()
	{
	}

	// Token: 0x06003E1B RID: 15899 RVA: 0x002290FC File Offset: 0x002280FC
	public spr\u233F(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003E1C RID: 15900 RVA: 0x00229114 File Offset: 0x00228114
	public spr\u233F(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003E1D RID: 15901 RVA: 0x00229128 File Offset: 0x00228128
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
		this.ᜁ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x06003E1E RID: 15902 RVA: 0x00229170 File Offset: 0x00228170
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		A_0.WriteUInt16(A_1, this.ᜁ);
		this.m_iLength = 2;
	}

	// Token: 0x06003E1F RID: 15903 RVA: 0x002291C0 File Offset: 0x002281C0
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 2;
	}

	// Token: 0x06003E20 RID: 15904 RVA: 0x002291FC File Offset: 0x002281FC
	public static bool ᜁ(spr\u233F A_0, spr\u233F A_1)
	{
		for (;;)
		{
			bool flag = object.Equals(A_0, null);
			bool flag2 = object.Equals(A_1, null);
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					goto IL_5D;
				case 2:
					goto IL_68;
				case 3:
					if (true)
					{
					}
					num = 1;
					continue;
				case 4:
					if (flag2)
					{
						num = 5;
						continue;
					}
					goto IL_6A;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5D;
					default:
						goto IL_AE;
					}
					break;
				case 6:
					if (flag)
					{
						num = 0;
						continue;
					}
					goto IL_6A;
				case 7:
					if (!flag)
					{
						num = 3;
						continue;
					}
					return false;
				}
				break;
				IL_5D:
				if (flag2)
				{
					num = 2;
					continue;
				}
				goto IL_BA;
				IL_6A:
				num = 7;
			}
		}
		IL_68:
		return false;
		IL_AE:
		if (false)
		{
		}
		return true;
		IL_BA:
		return A_0.ᜁ == A_1.ᜁ;
	}

	// Token: 0x06003E21 RID: 15905 RVA: 0x002292DC File Offset: 0x002282DC
	public static bool ᜀ(spr\u233F A_0, spr\u233F A_1)
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
		return !spr\u233F.ᜁ(A_0, A_1);
	}

	// Token: 0x04001AAF RID: 6831
	private new const int ᜀ = 2;

	// Token: 0x04001AB0 RID: 6832
	[spr\u2429(0, 2)]
	private ushort ᜁ;
}
