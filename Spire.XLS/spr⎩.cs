using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000501 RID: 1281
[spr\u2593(TBIFFRecord.RowColumnFieldId)]
[CLSCompliant(false)]
internal class spr\u23A9 : BiffRecordRaw
{
	// Token: 0x06004E3C RID: 20028 RVA: 0x002F9CE8 File Offset: 0x002F8CE8
	public spr\u23A9()
	{
	}

	// Token: 0x06004E3D RID: 20029 RVA: 0x002F9CFC File Offset: 0x002F8CFC
	public spr\u23A9(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06004E3E RID: 20030 RVA: 0x002F9D14 File Offset: 0x002F8D14
	public spr\u23A9(int A_0) : base(A_0)
	{
	}

	// Token: 0x06004E3F RID: 20031 RVA: 0x002F9D28 File Offset: 0x002F8D28
	public ushort[] ᜀ()
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

	// Token: 0x06004E40 RID: 20032 RVA: 0x002F9D6C File Offset: 0x002F8D6C
	public void ᜀ(ushort[] A_0)
	{
		int a_ = 3;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("伸娺儼䨾⑀", a_));
			}
			break;
		}
		this.ᜀ = A_0;
	}

	// Token: 0x06004E41 RID: 20033 RVA: 0x002F9DD0 File Offset: 0x002F8DD0
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
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
			break;
		}
		for (;;)
		{
			int num = this.m_iLength / 2;
			this.ᜀ = new ushort[num];
			int num2 = 0;
			int num3 = 2;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					goto IL_55;
				case 2:
					goto IL_55;
				case 3:
					if (num2 >= num)
					{
						num3 = 0;
						continue;
					}
					if (true)
					{
					}
					this.ᜀ[num2] = A_0.ReadUInt16(A_1);
					num2++;
					A_1 += 2;
					num3 = 1;
					continue;
				}
				break;
				IL_55:
				num3 = 3;
			}
		}
	}

	// Token: 0x06004E42 RID: 20034 RVA: 0x002F9E7C File Offset: 0x002F8E7C
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
			break;
		}
		for (;;)
		{
			this.m_iLength = this.ᜀ.Length * 2;
			A_0.WriteByte(A_1 + this.m_iLength - 1, 0);
			int num = 0;
			int num2 = this.ᜀ.Length;
			int num3 = 3;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					return;
				case 1:
					if (num >= num2)
					{
						num3 = 0;
						continue;
					}
					A_0.WriteUInt16(A_1, this.ᜀ[num]);
					num++;
					A_1 += 2;
					if (true)
					{
					}
					num3 = 2;
					continue;
				case 2:
					goto IL_6A;
				case 3:
					goto IL_6A;
				}
				break;
				IL_6A:
				num3 = 1;
			}
		}
	}

	// Token: 0x06004E43 RID: 20035 RVA: 0x002F9F40 File Offset: 0x002F8F40
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
		return this.ᜀ.Length * 2;
	}

	// Token: 0x04002361 RID: 9057
	private new ushort[] ᜀ;
}
