using System;
using System.Drawing;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x02000375 RID: 885
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.ChartAreaFormat)]
internal class sprᨓ : BiffRecordRaw
{
	// Token: 0x060035DE RID: 13790 RVA: 0x001EA7B4 File Offset: 0x001E97B4
	public int ᜈ()
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
		return this.ᜁ;
	}

	// Token: 0x060035DF RID: 13791 RVA: 0x001EA7F8 File Offset: 0x001E97F8
	public void ᜀ(int A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					this.ᜁ = A_0;
					num = 0;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (A_0 == this.ᜁ)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x060035E0 RID: 13792 RVA: 0x001EA874 File Offset: 0x001E9874
	public Color ᜉ()
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
		return spr\u1D39.ᜀ(this.ᜂ);
	}

	// Token: 0x060035E1 RID: 13793 RVA: 0x001EA8BC File Offset: 0x001E98BC
	public void ᜀ(Color A_0)
	{
		for (;;)
		{
			int num = A_0.ToArgb() & 16777215;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜂ = num;
						break;
					}
					if (true)
					{
					}
					num2 = 0;
					continue;
				case 2:
					if (num != this.ᜂ)
					{
						num2 = 1;
						continue;
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x060035E2 RID: 13794 RVA: 0x001EA948 File Offset: 0x001E9948
	public ExcelPatternType ᜁ()
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
		return (ExcelPatternType)this.ᜃ;
	}

	// Token: 0x060035E3 RID: 13795 RVA: 0x001EA98C File Offset: 0x001E998C
	public void ᜀ(ExcelPatternType A_0)
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
		this.ᜃ = (ushort)A_0;
	}

	// Token: 0x060035E4 RID: 13796 RVA: 0x001EA9D0 File Offset: 0x001E99D0
	public ushort ᜇ()
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
		return this.ᜄ;
	}

	// Token: 0x060035E5 RID: 13797 RVA: 0x001EAA14 File Offset: 0x001E9A14
	public ExcelColors ᜄ()
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
		return (ExcelColors)this.ᜇ;
	}

	// Token: 0x060035E6 RID: 13798 RVA: 0x001EAA58 File Offset: 0x001E9A58
	public void ᜁ(ExcelColors A_0)
	{
		for (;;)
		{
			if (true)
			{
			}
			ushort num = (ushort)A_0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜇ = num;
						break;
					}
					num2 = 1;
					continue;
				case 1:
					return;
				case 2:
					if (num != this.ᜇ)
					{
						num2 = 0;
						continue;
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x060035E7 RID: 13799 RVA: 0x001EAAD8 File Offset: 0x001E9AD8
	public ExcelColors ᜂ()
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
		return (ExcelColors)this.ᜈ;
	}

	// Token: 0x060035E8 RID: 13800 RVA: 0x001EAB1C File Offset: 0x001E9B1C
	public void ᜀ(ExcelColors A_0)
	{
		for (;;)
		{
			ushort num = (ushort)A_0;
			int num2 = 1;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (num != this.ᜈ)
					{
						num2 = 2;
						continue;
					}
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.ᜈ = num;
						break;
					}
					num2 = 0;
					continue;
				}
				break;
			}
		}
	}

	// Token: 0x060035E9 RID: 13801 RVA: 0x001EAB9C File Offset: 0x001E9B9C
	public bool ᜅ()
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
		return this.ᜅ;
	}

	// Token: 0x060035EA RID: 13802 RVA: 0x001EABE0 File Offset: 0x001E9BE0
	public void ᜁ(bool A_0)
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
		this.ᜅ = A_0;
	}

	// Token: 0x060035EB RID: 13803 RVA: 0x001EAC24 File Offset: 0x001E9C24
	public bool ᜀ()
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

	// Token: 0x060035EC RID: 13804 RVA: 0x001EAC68 File Offset: 0x001E9C68
	public void ᜀ(bool A_0)
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

	// Token: 0x060035ED RID: 13805 RVA: 0x001EACAC File Offset: 0x001E9CAC
	public virtual int ᜃ()
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
		return 16;
	}

	// Token: 0x060035EE RID: 13806 RVA: 0x001EACEC File Offset: 0x001E9CEC
	public virtual int ᜆ()
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
		return 16;
	}

	// Token: 0x060035EF RID: 13807 RVA: 0x001EAD2C File Offset: 0x001E9D2C
	public sprᨓ()
	{
	}

	// Token: 0x060035F0 RID: 13808 RVA: 0x001EAD48 File Offset: 0x001E9D48
	public sprᨓ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x060035F1 RID: 13809 RVA: 0x001EAD64 File Offset: 0x001E9D64
	public sprᨓ(int A_0) : base(A_0)
	{
	}

	// Token: 0x060035F2 RID: 13810 RVA: 0x001EAD80 File Offset: 0x001E9D80
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
		return 16;
	}

	// Token: 0x060035F3 RID: 13811 RVA: 0x001EADC0 File Offset: 0x001E9DC0
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
		this.ᜁ = this.ᜀ(A_0, ref A_1);
		this.ᜂ = this.ᜀ(A_0, ref A_1);
		this.ᜃ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜄ = A_0.ReadUInt16(A_1);
		this.ᜅ = A_0.ReadBit(A_1, 0);
		this.ᜆ = A_0.ReadBit(A_1, 1);
		A_1 += 2;
		this.ᜇ = A_0.ReadUInt16(A_1);
		A_1 += 2;
		this.ᜈ = A_0.ReadUInt16(A_1);
	}

	// Token: 0x060035F4 RID: 13812 RVA: 0x001EAE78 File Offset: 0x001E9E78
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
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
		this.ᜄ &= 3;
		this.m_iLength = this.GetStoreSize(A_2);
		this.ᜀ(A_0, ref A_1, this.ᜁ);
		this.ᜀ(A_0, ref A_1, this.ᜂ);
		A_0.WriteUInt16(A_1, this.ᜃ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜄ);
		A_0.WriteBit(A_1, this.ᜅ, 0);
		A_0.WriteBit(A_1, this.ᜆ, 1);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜇ);
		A_1 += 2;
		A_0.WriteUInt16(A_1, this.ᜈ);
	}

	// Token: 0x060035F5 RID: 13813 RVA: 0x001EAF4C File Offset: 0x001E9F4C
	private int ᜀ(DataProvider A_0, ref int A_1)
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
		byte red = A_0.ReadByte(A_1++);
		byte green = A_0.ReadByte(A_1++);
		byte blue = A_0.ReadByte(A_1++);
		A_1++;
		return Color.FromArgb(255, (int)red, (int)green, (int)blue).ToArgb();
	}

	// Token: 0x060035F6 RID: 13814 RVA: 0x001EAFD8 File Offset: 0x001E9FD8
	private void ᜀ(DataProvider A_0, ref int A_1, int A_2)
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
		Color color = spr\u1D39.ᜀ(A_2);
		A_0.WriteByte(A_1++, color.R);
		A_0.WriteByte(A_1++, color.G);
		A_0.WriteByte(A_1++, color.B);
		A_0.WriteByte(A_1++, 0);
	}

	// Token: 0x04001781 RID: 6017
	public new const int ᜀ = 16;

	// Token: 0x04001782 RID: 6018
	[spr\u2429(0, 4, true)]
	private int ᜁ;

	// Token: 0x04001783 RID: 6019
	[spr\u2429(4, 4, true)]
	private int ᜂ;

	// Token: 0x04001784 RID: 6020
	[spr\u2429(8, 2)]
	private new ushort ᜃ;

	// Token: 0x04001785 RID: 6021
	[spr\u2429(10, 2)]
	private ushort ᜄ;

	// Token: 0x04001786 RID: 6022
	[spr\u2429(10, 0, TFieldType.Bit)]
	private bool ᜅ = true;

	// Token: 0x04001787 RID: 6023
	[spr\u2429(10, 1, TFieldType.Bit)]
	private bool ᜆ;

	// Token: 0x04001788 RID: 6024
	[spr\u2429(12, 2)]
	private ushort ᜇ;

	// Token: 0x04001789 RID: 6025
	[spr\u2429(14, 2)]
	private ushort ᜈ;
}
