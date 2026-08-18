using System;
using Spire.DataExport.XLS;

// Token: 0x02000082 RID: 130
internal class spr\u17ED
{
	// Token: 0x060003D7 RID: 983 RVA: 0x00023A7C File Offset: 0x00022A7C
	public spr\u17ED(CellFont A_0, sprḓ A_1)
	{
		this.ᜁ = A_0;
		this.ᜂ = A_1;
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00023AC0 File Offset: 0x00022AC0
	public void ᜀ(spr\u1885 A_0)
	{
		for (;;)
		{
			A_0.ᜨ();
			byte[] array = spr\u2074.ᜀ()[15].ᜀ();
			Array.Copy(array, A_0.ᜢ(), array.Length);
			A_0.ᜁ(A_0.ᜀ() | 16384);
			A_0.ᜆ((ushort)(((int)A_0.ᜆ() & -128) | (int)spr\u2009.᠓[(int)((byte)this.ᜄ().Background)]));
			A_0.ᜀ((int)((ulong)-67108864 & (ulong)((long)((long)((byte)this.ᜄ().Pattern) << 26))));
			A_0.ᜆ((ushort)(((int)A_0.ᜆ() & -16257) | (int)spr\u2009.᠓[(int)((byte)this.ᜄ().Foreground)] << 7));
			int num = 17;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜃ.Bottom.Style == CellBorderStyle.None)
					{
						num = 15;
						continue;
					}
					goto IL_50C;
				case 1:
					goto IL_305;
				case 2:
					A_0.ᜀ(A_0.ᜅ() | (31457280 & (int)((ushort)this.ᜃ.DiagUp.Style) << 21));
					A_0.ᜀ(A_0.ᜅ() | (2080768 & (int)spr\u2009.᠓[(int)((byte)this.ᜃ.DiagUp.Color)] << 14));
					num = 25;
					continue;
				case 3:
					goto IL_366;
				case 4:
					goto IL_18C;
				case 5:
					goto IL_616;
				case 6:
					if (this.ᜃ.DiagUp.Style != CellBorderStyle.None)
					{
						num = 2;
						continue;
					}
					goto IL_305;
				case 7:
					if (this.ᜃ.DiagUp.Style != CellBorderStyle.None)
					{
						num = 18;
						continue;
					}
					goto IL_6E3;
				case 8:
					if (this.ᜃ.Top.Style != CellBorderStyle.None)
					{
						num = 39;
						continue;
					}
					goto IL_37E;
				case 9:
					A_0.ᜄ(A_0.ᜃ() | (ushort)(240 & (int)((ushort)this.ᜃ.Right.Style) << 4));
					A_0.ᜅ(A_0.ᜄ() | (ushort)(16256 & (int)spr\u2009.᠓[(int)((byte)this.ᜃ.Right.Color)] << 7));
					num = 5;
					continue;
				case 10:
					A_0.ᜄ(A_0.ᜃ() | (15 & (ushort)this.ᜃ.Left.Style));
					A_0.ᜅ(A_0.ᜄ() | (ushort)(127 & spr\u2009.᠓[(int)((byte)this.ᜃ.Left.Color)]));
					num = 14;
					continue;
				case 11:
					A_0.ᜀ(A_0.ᜅ() | (31457280 & (int)((ushort)this.ᜃ.DiagDown.Style) << 21));
					A_0.ᜀ(A_0.ᜅ() | (2080768 & (int)spr\u2009.᠓[(int)((byte)this.ᜃ.DiagDown.Color)] << 14));
					num = 1;
					continue;
				case 12:
					A_0.ᜃ(this.ᜂ.ᜀ());
					num = 31;
					continue;
				case 13:
					if (this.ᜃ.Right.Style == CellBorderStyle.None)
					{
						num = 32;
						continue;
					}
					goto IL_50C;
				case 14:
					goto IL_450;
				case 15:
					num = 16;
					continue;
				case 16:
					if (this.ᜃ.DiagDown.Style == CellBorderStyle.None)
					{
						num = 43;
						continue;
					}
					goto IL_50C;
				case 17:
					if (this.ᜁ != null)
					{
						num = 20;
						continue;
					}
					goto IL_7CE;
				case 18:
					A_0.ᜅ(A_0.ᜄ() | 32768);
					num = 28;
					continue;
				case 19:
					if (this.ᜃ.DiagUp.Style != CellBorderStyle.None)
					{
						num = 46;
						continue;
					}
					goto IL_305;
				case 20:
					A_0.ᜁ(A_0.ᜀ() | 2048);
					A_0.ᜂ((ushort)this.ᜁ.FontIndex);
					num = 27;
					continue;
				case 21:
					A_0.ᜄ(A_0.ᜃ() | (ushort)(61440 & (int)((ushort)this.ᜃ.Bottom.Style) << 12));
					A_0.ᜀ(A_0.ᜅ() | (16256 & (int)spr\u2009.᠓[(int)((byte)this.ᜃ.Bottom.Color)] << 7));
					num = 24;
					continue;
				case 22:
					if (this.ᜃ.Left.Style != CellBorderStyle.None)
					{
						num = 10;
						continue;
					}
					goto IL_450;
				case 23:
					if (this.ᜃ.Left.Style == CellBorderStyle.None)
					{
						num = 30;
						continue;
					}
					goto IL_50C;
				case 24:
					goto IL_824;
				case 25:
					goto IL_305;
				case 26:
					A_0.ᜅ(A_0.ᜄ() | 16384);
					num = 4;
					continue;
				case 27:
					goto IL_7CE;
				case 28:
					goto IL_6E3;
				case 29:
					if (this.ᜃ.Top.Style == CellBorderStyle.None)
					{
						num = 40;
						continue;
					}
					goto IL_50C;
				case 30:
					num = 13;
					continue;
				case 31:
					goto IL_2B2;
				case 32:
					num = 29;
					continue;
				case 33:
					if (this.ᜃ.DiagDown.Style == CellBorderStyle.None)
					{
						num = 35;
						continue;
					}
					goto IL_7F4;
				case 34:
					goto IL_37E;
				case 35:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_305;
					default:
						if (false)
						{
						}
						num = 44;
						continue;
					}
					break;
				case 36:
					if (this.ᜃ.DiagDown.Style != CellBorderStyle.None)
					{
						num = 26;
						continue;
					}
					goto IL_18C;
				case 37:
					if (this.ᜂ != null)
					{
						num = 12;
						continue;
					}
					goto IL_2B2;
				case 38:
					if (this.ᜃ.DiagDown.Style != CellBorderStyle.None)
					{
						num = 11;
						continue;
					}
					num = 6;
					continue;
				case 39:
					A_0.ᜄ(A_0.ᜃ() | (ushort)(3840 & (int)((ushort)this.ᜃ.Top.Style) << 8));
					A_0.ᜀ(A_0.ᜅ() | (int)(127 & spr\u2009.᠓[(int)((byte)this.ᜃ.Top.Color)]));
					num = 34;
					continue;
				case 40:
					num = 0;
					continue;
				case 41:
					goto IL_7F4;
				case 42:
					if (this.ᜃ.Right.Style != CellBorderStyle.None)
					{
						num = 9;
						continue;
					}
					goto IL_616;
				case 43:
					num = 19;
					continue;
				case 44:
					if (this.ᜃ.DiagUp.Style != CellBorderStyle.None)
					{
						num = 41;
						continue;
					}
					goto IL_305;
				case 45:
					if (true)
					{
					}
					if (this.ᜃ.Bottom.Style != CellBorderStyle.None)
					{
						num = 21;
						continue;
					}
					goto IL_824;
				case 46:
					goto IL_50C;
				}
				break;
				IL_18C:
				num = 7;
				continue;
				IL_2B2:
				num = 23;
				continue;
				IL_305:
				A_0.ᜀ(0);
				A_0.ᜁ(A_0.ᜀ() | 4096);
				A_0.ᜀ(A_0.ᜂ() | (ushort)((byte)this.ᜅ.Horizontal));
				A_0.ᜀ((ushort)((int)(A_0.ᜂ() & 65423) | (int)((byte)this.ᜅ.Vertical) << 4));
				num = 3;
				continue;
				IL_37E:
				num = 45;
				continue;
				IL_450:
				num = 42;
				continue;
				IL_50C:
				A_0.ᜁ(A_0.ᜀ() | 8192);
				A_0.ᜄ(0);
				num = 22;
				continue;
				IL_616:
				num = 8;
				continue;
				IL_6E3:
				num = 38;
				continue;
				IL_7CE:
				num = 37;
				continue;
				IL_7F4:
				num = 36;
				continue;
				IL_824:
				num = 33;
			}
		}
		IL_366:
		A_0.ᜀ(A_0.ᜂ() | (ushort)(8 & (int)(this.ᜆ ? 1 : 0) << 3));
		A_0.ᜀ((ushort)((int)(A_0.ᜂ() & 255) | (int)this.ᜇ << 8));
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0002434C File Offset: 0x0002334C
	public bool ᜀ(spr\u17ED A_0)
	{
		int num = 24;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜂ.ᜀ(A_0.ᜂ()))
				{
					num = 3;
					continue;
				}
				return false;
			case 1:
				if (this.ᜂ != null)
				{
					num = 16;
					continue;
				}
				return false;
			case 2:
				return false;
			case 3:
				goto IL_2A3;
			case 4:
				if (A_0.ᜅ() == null)
				{
					goto IL_98;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2AE;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 5:
				if (true)
				{
				}
				num = 14;
				continue;
			case 6:
				if (this.ᜁ.IsEqual(A_0.ᜅ()))
				{
					num = 25;
					continue;
				}
				return false;
			case 7:
				goto IL_C9;
			case 8:
				goto IL_240;
			case 9:
				if (this.ᜂ == null)
				{
					num = 12;
					continue;
				}
				goto IL_1F6;
			case 10:
				if (this.ᜆ == A_0.ᜁ())
				{
					num = 8;
					continue;
				}
				return false;
			case 11:
				num = 10;
				continue;
			case 12:
				num = 15;
				continue;
			case 13:
				if (this.ᜃ.IsEqual(A_0.ᜃ()))
				{
					num = 23;
					continue;
				}
				return false;
			case 14:
				if (this.ᜅ.IsEqual(A_0.ᜇ()))
				{
					num = 11;
					continue;
				}
				return false;
			case 15:
				if (A_0.ᜂ() != null)
				{
					num = 19;
					continue;
				}
				goto IL_2A3;
			case 16:
				num = 0;
				continue;
			case 17:
				num = 4;
				continue;
			case 18:
				num = 6;
				continue;
			case 19:
				goto IL_1F6;
			case 20:
				if (this.ᜄ.IsEqual(A_0.ᜄ()))
				{
					num = 5;
					continue;
				}
				return false;
			case 21:
				if (this.ᜁ != null)
				{
					num = 18;
					continue;
				}
				return false;
			case 22:
				goto IL_2AE;
			case 23:
				num = 20;
				continue;
			case 25:
				goto IL_98;
			}
			if (A_0 == null)
			{
				num = 2;
				continue;
			}
			num = 9;
			continue;
			IL_98:
			num = 13;
			continue;
			IL_C9:
			num = 21;
			continue;
			IL_2AE:
			if (this.ᜁ == null)
			{
				num = 17;
				continue;
			}
			goto IL_C9;
			IL_1F6:
			num = 1;
			continue;
			IL_2A3:
			num = 22;
		}
		return false;
		IL_240:
		return this.ᜇ == A_0.ᜆ();
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00024624 File Offset: 0x00023624
	public ushort ᜀ()
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
		return this.ᜀ;
	}

	// Token: 0x060003DB RID: 987 RVA: 0x00024668 File Offset: 0x00023668
	public void ᜀ(ushort A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x060003DC RID: 988 RVA: 0x000246AC File Offset: 0x000236AC
	public CellFont ᜅ()
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

	// Token: 0x060003DD RID: 989 RVA: 0x000246F0 File Offset: 0x000236F0
	public void ᜀ(CellFont A_0)
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

	// Token: 0x060003DE RID: 990 RVA: 0x00024734 File Offset: 0x00023734
	public sprḓ ᜂ()
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
		return this.ᜂ;
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00024778 File Offset: 0x00023778
	public void ᜀ(sprḓ A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				break;
			case 1:
				if (A_0 != this.ᜂ)
				{
					num = 3;
					continue;
				}
				return;
			case 2:
				num = 1;
				continue;
			case 3:
				this.ᜂ = A_0;
				if (true)
				{
				}
				num = 4;
				continue;
			case 4:
				return;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 2;
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x00024810 File Offset: 0x00023810
	public Borders ᜃ()
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
		return this.ᜃ;
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x00024854 File Offset: 0x00023854
	public void ᜀ(Borders A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				break;
			case 2:
				return;
			case 3:
				if (true)
				{
				}
				if (A_0 != this.ᜃ)
				{
					num = 4;
					continue;
				}
				return;
			case 4:
				this.ᜃ = A_0;
				num = 2;
				continue;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x000248EC File Offset: 0x000238EC
	public FillType ᜄ()
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
		return this.ᜄ;
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00024930 File Offset: 0x00023930
	public void ᜀ(FillType A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				break;
			case 1:
				if (A_0 != this.ᜄ)
				{
					num = 3;
					continue;
				}
				return;
			case 2:
				return;
			case 3:
				this.ᜄ = A_0;
				num = 2;
				continue;
			case 4:
				num = 1;
				continue;
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				break;
			}
			num = 4;
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000249C8 File Offset: 0x000239C8
	public TextAlignment ᜇ()
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

	// Token: 0x060003E5 RID: 997 RVA: 0x00024A0C File Offset: 0x00023A0C
	public void ᜀ(TextAlignment A_0)
	{
		int num = 1;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				this.ᜅ = A_0;
				num = 3;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				}
				if (false)
				{
				}
				break;
			case 2:
				if (A_0 != this.ᜅ)
				{
					num = 0;
					continue;
				}
				return;
			case 3:
				return;
			case 4:
				num = 2;
				continue;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 4;
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00024AA4 File Offset: 0x00023AA4
	public bool ᜁ()
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

	// Token: 0x060003E7 RID: 999 RVA: 0x00024AE8 File Offset: 0x00023AE8
	public void ᜀ(bool A_0)
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
		this.ᜆ = A_0;
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x00024B2C File Offset: 0x00023B2C
	public byte ᜆ()
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
		return this.ᜇ;
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x00024B70 File Offset: 0x00023B70
	public void ᜀ(byte A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				this.ᜇ = A_0;
				num = 0;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_53;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				break;
			}
			goto IL_4A;
			IL_53:
			num = 1;
			continue;
			IL_4A:
			if (A_0 != this.ᜇ)
			{
				goto IL_53;
			}
			break;
		}
	}

	// Token: 0x04000284 RID: 644
	private ushort ᜀ;

	// Token: 0x04000285 RID: 645
	private CellFont ᜁ;

	// Token: 0x04000286 RID: 646
	private sprḓ ᜂ;

	// Token: 0x04000287 RID: 647
	private Borders ᜃ = new Borders();

	// Token: 0x04000288 RID: 648
	private FillType ᜄ = new FillType();

	// Token: 0x04000289 RID: 649
	private TextAlignment ᜅ = new TextAlignment();

	// Token: 0x0400028A RID: 650
	private bool ᜆ;

	// Token: 0x0400028B RID: 651
	private byte ᜇ;
}
