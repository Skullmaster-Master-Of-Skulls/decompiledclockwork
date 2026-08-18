using System;
using System.Drawing;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002E4 RID: 740
[CLSCompliant(false)]
[spr\u2593(TBIFFRecord.Palette)]
internal class spr\u20A4 : BiffRecordRaw
{
	// Token: 0x06002E20 RID: 11808 RVA: 0x0019F0B4 File Offset: 0x0019E0B4
	public ushort ᜂ()
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

	// Token: 0x06002E21 RID: 11809 RVA: 0x0019F0F8 File Offset: 0x0019E0F8
	public spr\u20A4.ᜀ[] ᜀ()
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

	// Token: 0x06002E22 RID: 11810 RVA: 0x0019F13C File Offset: 0x0019E13C
	public void ᜀ(spr\u20A4.ᜀ[] A_0)
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
		this.ᜁ = A_0;
		this.ᜀ = ((A_0 != null) ? ((ushort)A_0.Length) : 0);
	}

	// Token: 0x06002E23 RID: 11811 RVA: 0x0019F194 File Offset: 0x0019E194
	public virtual int ᜃ()
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
		return 2;
	}

	// Token: 0x06002E24 RID: 11812 RVA: 0x0019F1D0 File Offset: 0x0019E1D0
	public virtual int ᜁ()
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
		return 226;
	}

	// Token: 0x06002E25 RID: 11813 RVA: 0x0019F210 File Offset: 0x0019E210
	public spr\u20A4()
	{
	}

	// Token: 0x06002E26 RID: 11814 RVA: 0x0019F22C File Offset: 0x0019E22C
	public spr\u20A4(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002E27 RID: 11815 RVA: 0x0019F24C File Offset: 0x0019E24C
	public spr\u20A4(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002E28 RID: 11816 RVA: 0x0019F268 File Offset: 0x0019E268
	public virtual void ᜀ(DataProvider A_0, int A_1, int A_2, ExcelVersion A_3)
	{
		for (;;)
		{
			this.ᜀ = A_0.ReadUInt16(A_1);
			this.ᜁ = new spr\u20A4.ᜀ[(int)this.ᜀ];
			A_1 += 2;
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= (int)this.ᜀ)
					{
						num2 = 1;
						continue;
					}
					for (;;)
					{
						this.ᜁ[num].ᜀ = A_0.ReadByte(A_1);
						this.ᜁ[num].ᜁ = A_0.ReadByte(A_1 + 1);
						this.ᜁ[num].ᜂ = A_0.ReadByte(A_1 + 2);
						this.ᜁ[num].ᜃ = A_0.ReadByte(A_1 + 3);
						num++;
						A_1 += 4;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_F2;
						}
					}
					IL_F2:
					if (false)
					{
					}
					num2 = 2;
					continue;
				case 1:
					return;
				case 2:
					goto IL_4F;
				case 3:
					if (true)
					{
					}
					goto IL_4F;
				}
				break;
				IL_4F:
				num2 = 0;
			}
		}
	}

	// Token: 0x06002E29 RID: 11817 RVA: 0x0019F388 File Offset: 0x0019E388
	public virtual void ᜀ(DataProvider A_0, int A_1, ExcelVersion A_2)
	{
		for (;;)
		{
			A_0.WriteUInt16(A_1, this.ᜀ);
			this.m_iLength = 2;
			int num = 0;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (true)
					{
					}
					goto IL_43;
				case 1:
					goto IL_43;
				case 2:
					if (num >= (int)this.ᜀ)
					{
						num2 = 3;
						continue;
					}
					for (;;)
					{
						A_0.WriteByte(A_1 + this.m_iLength, this.ᜁ[num].ᜀ);
						this.m_iLength++;
						A_0.WriteByte(A_1 + this.m_iLength, this.ᜁ[num].ᜁ);
						this.m_iLength++;
						A_0.WriteByte(A_1 + this.m_iLength, this.ᜁ[num].ᜂ);
						this.m_iLength++;
						A_0.WriteByte(A_1 + this.m_iLength, this.ᜁ[num].ᜃ);
						this.m_iLength++;
						num++;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_132;
						}
					}
					IL_132:
					if (false)
					{
					}
					num2 = 1;
					continue;
				case 3:
					return;
				}
				break;
				IL_43:
				num2 = 2;
			}
		}
	}

	// Token: 0x06002E2A RID: 11818 RVA: 0x0019F4E8 File Offset: 0x0019E4E8
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
		return (int)(2 + this.ᜀ * 4);
	}

	// Token: 0x040014D4 RID: 5332
	[spr\u2429(0, 2)]
	private new ushort ᜀ = 56;

	// Token: 0x040014D5 RID: 5333
	private spr\u20A4.ᜀ[] ᜁ;

	// Token: 0x020002E5 RID: 741
	internal new struct ᜀ
	{
		// Token: 0x06002E2B RID: 11819 RVA: 0x0019F530 File Offset: 0x0019E530
		public string ᜀ()
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
			return Color.FromArgb((int)this.ᜃ, (int)this.ᜀ, (int)this.ᜁ, (int)this.ᜂ).ToString();
		}

		// Token: 0x040014D6 RID: 5334
		public byte ᜀ;

		// Token: 0x040014D7 RID: 5335
		public byte ᜁ;

		// Token: 0x040014D8 RID: 5336
		public byte ᜂ;

		// Token: 0x040014D9 RID: 5337
		public byte ᜃ;
	}
}
