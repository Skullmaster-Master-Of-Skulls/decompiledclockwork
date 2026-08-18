using System;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000474 RID: 1140
[spr\u2593(TBIFFRecord.SheetProtection)]
[CLSCompliant(false)]
internal class spr\u22A0 : BiffRecordRaw
{
	// Token: 0x060045DF RID: 17887 RVA: 0x002A92A8 File Offset: 0x002A82A8
	public int ᜁ()
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
		return (int)this.ᜄ;
	}

	// Token: 0x060045E0 RID: 17888 RVA: 0x002A92EC File Offset: 0x002A82EC
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
		this.ᜄ = (ushort)A_0;
	}

	// Token: 0x060045E1 RID: 17889 RVA: 0x002A9330 File Offset: 0x002A8330
	public bool ᜀ()
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
		return this.ᜅ;
	}

	// Token: 0x060045E2 RID: 17890 RVA: 0x002A9374 File Offset: 0x002A8374
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
		this.ᜅ = A_0;
	}

	// Token: 0x060045E3 RID: 17891 RVA: 0x002A93B8 File Offset: 0x002A83B8
	public virtual int ᜄ()
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
		return 19;
	}

	// Token: 0x060045E4 RID: 17892 RVA: 0x002A93F8 File Offset: 0x002A83F8
	public virtual int ᜂ()
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
		return 23;
	}

	// Token: 0x060045E5 RID: 17893 RVA: 0x002A9438 File Offset: 0x002A8438
	internal new short ᜃ()
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
		return this.ᜆ;
	}

	// Token: 0x060045E6 RID: 17894 RVA: 0x002A947C File Offset: 0x002A847C
	internal void ᜀ(short A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x060045E7 RID: 17895 RVA: 0x002A94C0 File Offset: 0x002A84C0
	public spr\u22A0()
	{
	}

	// Token: 0x060045E8 RID: 17896 RVA: 0x002A94F8 File Offset: 0x002A84F8
	public spr\u22A0(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060045E9 RID: 17897 RVA: 0x002A9530 File Offset: 0x002A8530
	public spr\u22A0(int A_0) : base(A_0)
	{
	}

	// Token: 0x060045EA RID: 17898 RVA: 0x002A9568 File Offset: 0x002A8568
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			if (true)
			{
			}
			this.ᜅ = (A_2 > 19);
			this.ᜆ = A_0.ReadInt16(A_1 + 12);
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜄ = (ushort)A_0.ReadInt32(A_1 + 19);
					num = 1;
					continue;
				case 1:
					goto IL_74;
				case 2:
					if (this.ᜅ)
					{
						num = 0;
						continue;
					}
					goto IL_76;
				}
				break;
			}
		}
		IL_74:
		IL_76:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_74;
		default:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x060045EB RID: 17899 RVA: 0x002A9608 File Offset: 0x002A8608
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			this.m_iLength = this.GetStoreSize(A_2);
			A_0.WriteBytes(A_1, new byte[this.m_iLength]);
			A_0.WriteUInt16(A_1, 2151);
			if (this.ᜆ != 3)
			{
				goto IL_92;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_4C;
			}
		}
		IL_4C:
		if (false)
		{
		}
		if (true)
		{
		}
		A_1 += 12;
		A_0.WriteInt16(A_1, this.ᜆ);
		A_1 += 2;
		A_0.WriteByte(A_1, 1);
		A_1++;
		A_0.WriteInt32(A_1, 0);
		return;
		IL_92:
		A_0.WriteBytes(A_1 + 11, this.ᜃ, 0, 8);
		A_0.WriteUInt16(A_1 + 19, this.ᜄ);
	}

	// Token: 0x060045EC RID: 17900 RVA: 0x002A96CC File Offset: 0x002A86CC
	public virtual int ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			if (this.ᜅ)
			{
				return 23;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_28;
			}
		}
		IL_28:
		if (false)
		{
		}
		return 19;
	}

	// Token: 0x04001FDC RID: 8156
	public new const int ᜀ = 3;

	// Token: 0x04001FDD RID: 8157
	private const int ᜁ = 19;

	// Token: 0x04001FDE RID: 8158
	private const int ᜂ = 23;

	// Token: 0x04001FDF RID: 8159
	private new readonly byte[] ᜃ = new byte[]
	{
		0,
		2,
		0,
		1,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue,
		byte.MaxValue
	};

	// Token: 0x04001FE0 RID: 8160
	[spr\u2429(19, 2)]
	private ushort ᜄ = 17408;

	// Token: 0x04001FE1 RID: 8161
	private bool ᜅ;

	// Token: 0x04001FE2 RID: 8162
	private short ᜆ;
}
