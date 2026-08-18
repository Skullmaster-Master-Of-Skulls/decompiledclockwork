using System;
using System.IO;
using System.Text;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020003C7 RID: 967
[spr\u2593(TBIFFRecord.Note)]
[CLSCompliant(false)]
internal class spr\u2114 : BiffRecordRaw
{
	// Token: 0x06003ACC RID: 15052 RVA: 0x0020FCE4 File Offset: 0x0020ECE4
	public ushort ᜀ()
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

	// Token: 0x06003ACD RID: 15053 RVA: 0x0020FD28 File Offset: 0x0020ED28
	public void ᜁ(ushort A_0)
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
		this.ᜁ = A_0;
	}

	// Token: 0x06003ACE RID: 15054 RVA: 0x0020FD6C File Offset: 0x0020ED6C
	public ushort ᜁ()
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
		return this.ᜂ;
	}

	// Token: 0x06003ACF RID: 15055 RVA: 0x0020FDB0 File Offset: 0x0020EDB0
	public void ᜂ(ushort A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06003AD0 RID: 15056 RVA: 0x0020FDF4 File Offset: 0x0020EDF4
	public string ᜆ()
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
		return this.ᜇ;
	}

	// Token: 0x06003AD1 RID: 15057 RVA: 0x0020FE38 File Offset: 0x0020EE38
	public void ᜀ(string A_0)
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
		this.ᜇ = A_0;
		this.ᜆ = ((A_0 != null) ? ((ushort)this.ᜇ.Length) : 0);
	}

	// Token: 0x06003AD2 RID: 15058 RVA: 0x0020FE98 File Offset: 0x0020EE98
	public ushort ᜄ()
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

	// Token: 0x06003AD3 RID: 15059 RVA: 0x0020FEDC File Offset: 0x0020EEDC
	public void ᜀ(ushort A_0)
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

	// Token: 0x06003AD4 RID: 15060 RVA: 0x0020FF20 File Offset: 0x0020EF20
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
		return this.ᜄ;
	}

	// Token: 0x06003AD5 RID: 15061 RVA: 0x0020FF64 File Offset: 0x0020EF64
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
		this.ᜄ = A_0;
	}

	// Token: 0x06003AD6 RID: 15062 RVA: 0x0020FFA8 File Offset: 0x0020EFA8
	public new ushort ᜃ()
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
		return this.ᜃ;
	}

	// Token: 0x06003AD7 RID: 15063 RVA: 0x0020FFEC File Offset: 0x0020EFEC
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
		return 8;
	}

	// Token: 0x06003AD8 RID: 15064 RVA: 0x00210028 File Offset: 0x0020F028
	public spr\u2114()
	{
		this.ᜄ = false;
	}

	// Token: 0x06003AD9 RID: 15065 RVA: 0x00210050 File Offset: 0x0020F050
	public spr\u2114(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06003ADA RID: 15066 RVA: 0x00210070 File Offset: 0x0020F070
	public spr\u2114(int A_0) : base(A_0)
	{
	}

	// Token: 0x06003ADB RID: 15067 RVA: 0x00210090 File Offset: 0x0020F090
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			this.ᜁ = A_0.ReadUInt16(A_1);
			this.ᜂ = A_0.ReadUInt16(A_1 + 2);
			this.ᜃ = A_0.ReadUInt16(A_1 + 4);
			this.ᜄ = A_0.ReadBit(A_1 + 4, 1);
			this.ᜅ = A_0.ReadUInt16(A_1 + 6);
			this.ᜆ = A_0.ReadUInt16(A_1 + 8);
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					this.ᜇ = A_0.ReadString(10, (int)this.ᜆ, out num2, false);
					num = 2;
					continue;
				}
				case 1:
					if (this.ᜆ > 0)
					{
						goto IL_86;
					}
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						goto IL_D5;
					}
					break;
				}
				break;
				IL_86:
				num = 0;
			}
		}
		IL_D5:
		if (false)
		{
		}
	}

	// Token: 0x06003ADC RID: 15068 RVA: 0x0021017C File Offset: 0x0020F17C
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			IL_1C:
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_14E:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				this.GetStoreSize(ExcelVersion.Version97to2003);
				this.m_iLength = 0;
				num2 = A_1;
				A_0.WriteUInt16(A_1, this.ᜁ);
				A_0.WriteUInt16(A_1 + 2, this.ᜂ);
				A_0.WriteUInt16(A_1 + 4, this.ᜃ);
				A_0.WriteBit(A_1 + 4, this.ᜄ, 1);
				A_0.WriteUInt16(A_1 + 6, this.ᜅ);
				A_0.WriteUInt16(A_1 + 8, this.ᜆ);
				A_1 += 10;
				num = 3;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_109;
				case 1:
					goto IL_144;
				case 2:
					goto IL_159;
				case 3:
					if (this.ᜆ > 0)
					{
						num = 4;
						continue;
					}
					A_0.WriteByte(A_1++, 0);
					A_0.WriteByte(A_1++, 0);
					this.m_iLength = A_1 - num2;
					num = 0;
					continue;
				case 4:
					A_0.WriteStringNoLenUpdateOffset(ref A_1, this.ᜇ);
					this.m_iLength = A_1 - num2;
					num = 1;
					continue;
				}
				goto IL_1C;
			}
			IL_144:
			if (this.m_iLength % 2 != 0)
			{
				goto IL_14E;
			}
			break;
		}
		IL_109:
		return;
		IL_159:
		A_0.WriteByte(A_1, 0);
		this.m_iLength++;
	}

	// Token: 0x06003ADD RID: 15069 RVA: 0x002102E4 File Offset: 0x0020F2E4
	public virtual int ᜀ(ExcelVersion A_0)
	{
		int num = 4;
		int num2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_94;
			case 1:
				if (num2 % 2 != 0)
				{
					num = 3;
					continue;
				}
				goto IL_94;
			case 2:
				num2 = Encoding.Unicode.GetByteCount(this.ᜇ) + 1;
				num = 1;
				continue;
			case 3:
				if (true)
				{
				}
				num2++;
				num = 0;
				continue;
			case 5:
				goto IL_94;
			}
			if (this.ᜆ > 0)
			{
				num = 2;
				continue;
			}
			IL_3B:
			num2 = 2;
			num = 5;
			continue;
			IL_94:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3B;
			default:
				goto IL_AA;
			}
		}
		IL_AA:
		if (false)
		{
		}
		return 10 + num2;
	}

	// Token: 0x0400199E RID: 6558
	private new const int ᜀ = 10;

	// Token: 0x0400199F RID: 6559
	[spr\u2429(0, 2)]
	private ushort ᜁ;

	// Token: 0x040019A0 RID: 6560
	[spr\u2429(2, 2)]
	private ushort ᜂ;

	// Token: 0x040019A1 RID: 6561
	[spr\u2429(4, 2)]
	private new ushort ᜃ;

	// Token: 0x040019A2 RID: 6562
	[spr\u2429(4, 1, TFieldType.Bit)]
	private bool ᜄ;

	// Token: 0x040019A3 RID: 6563
	[spr\u2429(6, 2)]
	private ushort ᜅ;

	// Token: 0x040019A4 RID: 6564
	[spr\u2429(8, 2)]
	private ushort ᜆ;

	// Token: 0x040019A5 RID: 6565
	private string ᜇ = string.Empty;
}
