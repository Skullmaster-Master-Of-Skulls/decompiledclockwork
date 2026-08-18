using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000540 RID: 1344
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartFormatLink)]
internal class spr\u1CE1 : BiffRecordRaw
{
	// Token: 0x060051C8 RID: 20936 RVA: 0x003307F4 File Offset: 0x0032F7F4
	public spr\u1CE1()
	{
	}

	// Token: 0x060051C9 RID: 20937 RVA: 0x00330808 File Offset: 0x0032F808
	public spr\u1CE1(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060051CA RID: 20938 RVA: 0x00330820 File Offset: 0x0032F820
	public spr\u1CE1(int A_0) : base(A_0)
	{
	}

	// Token: 0x060051CB RID: 20939 RVA: 0x00330834 File Offset: 0x0032F834
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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

	// Token: 0x060051CC RID: 20940 RVA: 0x00330870 File Offset: 0x0032F870
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		this.m_iLength = 10;
		A_0.WriteBytes(A_1, spr\u1CE1.ᜁ, 0, this.m_iLength);
	}

	// Token: 0x060051CD RID: 20941 RVA: 0x003308C8 File Offset: 0x0032F8C8
	public virtual int ᜀ(ExcelVersion A_0)
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
		return 10;
	}

	// Token: 0x060051CE RID: 20942 RVA: 0x00330908 File Offset: 0x0032F908
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1CE1()
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
		byte[] array = new byte[10];
		array[8] = 15;
		spr\u1CE1.ᜁ = array;
	}

	// Token: 0x04002471 RID: 9329
	public new const int ᜀ = 10;

	// Token: 0x04002472 RID: 9330
	public static readonly byte[] ᜁ;
}
