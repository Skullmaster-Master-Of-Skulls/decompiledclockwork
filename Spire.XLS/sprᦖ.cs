using System;
using System.Collections.Generic;
using System.IO;
using Spire.Xls;
using Spire.Xls.Core.Parser.Biff_Records;

// Token: 0x020002E9 RID: 745
[spr\u2593(TBIFFRecord.ExternSheet)]
[CLSCompliant(false)]
internal class sprᦖ : spr\u251F
{
	// Token: 0x06002E48 RID: 11848 RVA: 0x0019FF5C File Offset: 0x0019EF5C
	public ushort ᜅ()
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

	// Token: 0x06002E49 RID: 11849 RVA: 0x0019FFA0 File Offset: 0x0019EFA0
	public new void ᜀ(ushort A_0)
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
		this.ᜂ = A_0;
	}

	// Token: 0x06002E4A RID: 11850 RVA: 0x0019FFE4 File Offset: 0x0019EFE4
	public new sprᦖ.ᜀ[] ᜃ()
	{
		if (this.ᜃ != null)
		{
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
				return this.ᜃ.ToArray();
			}
		}
		return null;
	}

	// Token: 0x06002E4B RID: 11851 RVA: 0x001A0038 File Offset: 0x0019F038
	public new void ᜀ(sprᦖ.ᜀ[] A_0)
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
		this.ᜃ = new List<sprᦖ.ᜀ>();
		this.ᜃ.AddRange(A_0);
		this.ᜂ = (ushort)this.ᜃ.Count;
	}

	// Token: 0x06002E4C RID: 11852 RVA: 0x001A009C File Offset: 0x0019F09C
	public new List<sprᦖ.ᜀ> ᜀ()
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

	// Token: 0x06002E4D RID: 11853 RVA: 0x001A00E0 File Offset: 0x0019F0E0
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
		return 2;
	}

	// Token: 0x06002E4E RID: 11854 RVA: 0x001A011C File Offset: 0x0019F11C
	public sprᦖ()
	{
	}

	// Token: 0x06002E4F RID: 11855 RVA: 0x001A0130 File Offset: 0x0019F130
	public sprᦖ(Stream A_0, out int A_1) : base(A_0, out A_1)
	{
	}

	// Token: 0x06002E50 RID: 11856 RVA: 0x001A0148 File Offset: 0x0019F148
	public sprᦖ(int A_0) : base(A_0)
	{
	}

	// Token: 0x06002E51 RID: 11857 RVA: 0x001A015C File Offset: 0x0019F15C
	public new int ᜀ(int A_0, int A_1, int A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			int num2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (true)
					{
					}
					sprᦖ.ᜀ ᜀ;
					if ((int)ᜀ.ᜂ() == A_2)
					{
						num = 5;
						continue;
					}
					goto IL_123;
				}
				case 2:
					num = 7;
					continue;
				case 3:
				{
					int count;
					if (num2 >= count)
					{
						num = 6;
						continue;
					}
					sprᦖ.ᜀ ᜀ = this.ᜃ[num2];
					num = 11;
					continue;
				}
				case 4:
					num = 0;
					continue;
				case 5:
					return num2;
				case 6:
					goto IL_105;
				case 7:
				{
					sprᦖ.ᜀ ᜀ;
					if ((int)ᜀ.ᜀ() == A_1)
					{
						num = 4;
						continue;
					}
					goto IL_123;
				}
				case 8:
				{
					int count = this.ᜃ.Count;
					num2 = 0;
					num = 10;
					continue;
				}
				case 9:
					goto IL_CD;
				case 10:
					goto IL_CD;
				case 11:
				{
					sprᦖ.ᜀ ᜀ;
					if ((int)ᜀ.ᜁ() == A_0)
					{
						num = 2;
						continue;
					}
					goto IL_123;
				}
				}
				goto IL_5A;
				IL_65:
				num = 8;
				continue;
				IL_5A:
				if (this.ᜃ != null)
				{
					goto IL_65;
				}
				goto IL_15B;
				IL_123:
				num2++;
				num = 9;
				continue;
				IL_CD:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_65;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
			}
			return num2;
			IL_105:
			IL_15B:
			sprᦖ.ᜀ a_ = new sprᦖ.ᜀ(A_0, A_1, A_2);
			this.ᜀ(a_);
			return (int)(this.ᜂ - 1);
		}
		}
	}

	// Token: 0x06002E52 RID: 11858 RVA: 0x001A02DC File Offset: 0x0019F2DC
	public new int ᜀ(int A_0)
	{
		int num;
		for (;;)
		{
			num = 0;
			int count = this.ᜃ.Count;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_80;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_44;
					default:
						goto IL_B2;
					}
					break;
				case 2:
					if (num >= count)
					{
						num2 = 1;
						continue;
					}
					num2 = 4;
					continue;
				case 3:
					goto IL_44;
				case 4:
					if ((int)this.ᜃ[num].ᜁ() == A_0)
					{
						num2 = 5;
						continue;
					}
					num++;
					num2 = 3;
					continue;
				case 5:
					goto IL_76;
				}
				break;
				IL_80:
				num2 = 2;
				continue;
				IL_44:
				goto IL_80;
			}
		}
		IL_76:
		if (true)
		{
		}
		return num;
		IL_B2:
		if (false)
		{
		}
		return -1;
	}

	// Token: 0x06002E53 RID: 11859 RVA: 0x001A03A4 File Offset: 0x0019F3A4
	public new void ᜀ(sprᦖ.ᜀ A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_65;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					this.ᜃ = new List<sprᦖ.ᜀ>();
					num = 0;
					continue;
				}
				break;
			}
			if (true)
			{
			}
			if (this.ᜃ != null)
			{
				break;
			}
			num = 1;
		}
		IL_65:
		this.ᜃ.Add(A_0);
		this.ᜂ += 1;
	}

	// Token: 0x06002E54 RID: 11860 RVA: 0x001A0440 File Offset: 0x0019F440
	public void ᜁ(IList<sprᦖ.ᜀ> A_0)
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
		this.ᜃ.AddRange(A_0);
		this.ᜂ += (ushort)A_0.Count;
	}

	// Token: 0x06002E55 RID: 11861 RVA: 0x001A049C File Offset: 0x0019F49C
	public new void ᜀ(IList<sprᦖ.ᜀ> A_0)
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
		this.ᜃ.InsertRange(0, A_0);
		this.ᜂ += (ushort)A_0.Count;
	}

	// Token: 0x06002E56 RID: 11862 RVA: 0x001A04FC File Offset: 0x0019F4FC
	public virtual object ᜁ()
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
		sprᦖ sprᦖ = (sprᦖ)base.Clone();
		sprᦖ.ᜃ = spr\u1CD3.ᜀ<sprᦖ.ᜀ>(this.ᜃ);
		return sprᦖ;
	}

	// Token: 0x06002E57 RID: 11863 RVA: 0x001A0558 File Offset: 0x0019F558
	public override void ᜂ()
	{
		for (;;)
		{
			this.ᜂ = BitConverter.ToUInt16(this.ᜀ, 0);
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					if (num2 >= (int)this.ᜂ)
					{
						num = 5;
						continue;
					}
					int num3;
					sprᦖ.ᜀ item = new sprᦖ.ᜀ((int)base.ᜌ(num3), (int)base.ᜌ(num3 + 2), (int)base.ᜌ(num3 + 4));
					this.ᜃ.Add(item);
					num2++;
					num3 += 6;
					num = 3;
					continue;
				}
				case 1:
					goto IL_DD;
				case 2:
					goto IL_5C;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DD;
					default:
						if (false)
						{
						}
						goto IL_E5;
					}
					break;
				case 4:
				{
					if (this.m_iLength != (int)(this.ᜂ * 6 + 2))
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					this.ᜃ = new List<sprᦖ.ᜀ>((int)this.ᜂ);
					int num2 = 0;
					int num3 = 2;
					num = 1;
					continue;
				}
				case 5:
					return;
				}
				break;
				IL_E5:
				num = 0;
				continue;
				IL_DD:
				goto IL_E5;
			}
		}
		IL_5C:
		throw new sprῩ();
	}

	// Token: 0x06002E58 RID: 11864 RVA: 0x001A0678 File Offset: 0x0019F678
	public override void ᜀ(ExcelVersion A_0)
	{
		for (;;)
		{
			this.m_iLength = this.GetStoreSize(ExcelVersion.Version97to2003);
			this.ᜀ = new byte[this.m_iLength];
			base.ᜀ(0, this.ᜂ);
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					int count;
					if (num2 >= count)
					{
						num = 3;
						continue;
					}
					int num3;
					base.ᜀ(num3, this.ᜃ[num2].ᜁ());
					base.ᜀ(num3 + 2, this.ᜃ[num2].ᜀ());
					base.ᜀ(num3 + 4, this.ᜃ[num2].ᜂ());
					num2++;
					num3 += 6;
					num = 1;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10B;
					default:
						if (false)
						{
						}
						goto IL_10E;
					}
					break;
				case 2:
					goto IL_10B;
				case 3:
					return;
				case 4:
				{
					if (this.ᜃ == null)
					{
						num = 5;
						continue;
					}
					int num2 = 0;
					int num3 = 2;
					int count = this.ᜃ.Count;
					num = 2;
					continue;
				}
				case 5:
					goto IL_66;
				}
				break;
				IL_10E:
				num = 0;
				continue;
				IL_10B:
				goto IL_10E;
			}
		}
		IL_66:
		if (true)
		{
		}
	}

	// Token: 0x06002E59 RID: 11865 RVA: 0x001A07BC File Offset: 0x0019F7BC
	public override int ᜁ(ExcelVersion A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3C;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 2:
				goto IL_4C;
			case 3:
				goto IL_73;
			}
			if (this.ᜃ != null)
			{
				num = 1;
				continue;
			}
			IL_3C:
			if (true)
			{
			}
			num = 2;
		}
		IL_4C:
		int num2 = 0;
		goto IL_82;
		IL_73:
		num2 = this.ᜃ.Count * 6;
		IL_82:
		int num3 = num2;
		return 2 + num3;
	}

	// Token: 0x040014E0 RID: 5344
	private new const int ᜀ = 2;

	// Token: 0x040014E1 RID: 5345
	public new const int ᜁ = 1370;

	// Token: 0x040014E2 RID: 5346
	[spr\u2429(0, 2)]
	private new ushort ᜂ;

	// Token: 0x040014E3 RID: 5347
	private new List<sprᦖ.ᜀ> ᜃ;

	// Token: 0x020002EA RID: 746
	internal new class ᜀ
	{
		// Token: 0x06002E5A RID: 11866 RVA: 0x001A0850 File Offset: 0x0019F850
		public ushort ᜁ()
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

		// Token: 0x06002E5B RID: 11867 RVA: 0x001A0894 File Offset: 0x0019F894
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
			this.ᜁ = A_0;
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x001A08D8 File Offset: 0x0019F8D8
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
			return this.ᜂ;
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x001A091C File Offset: 0x0019F91C
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

		// Token: 0x06002E5E RID: 11870 RVA: 0x001A0960 File Offset: 0x0019F960
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
			return this.ᜃ;
		}

		// Token: 0x06002E5F RID: 11871 RVA: 0x001A09A4 File Offset: 0x0019F9A4
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
			this.ᜃ = A_0;
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x001A09E8 File Offset: 0x0019F9E8
		public ᜀ(int A_0, int A_1, int A_2)
		{
			this.ᜂ((ushort)A_1);
			this.ᜁ((ushort)A_2);
			this.ᜀ((ushort)A_0);
		}

		// Token: 0x040014E4 RID: 5348
		public const int ᜀ = 6;

		// Token: 0x040014E5 RID: 5349
		private ushort ᜁ;

		// Token: 0x040014E6 RID: 5350
		private ushort ᜂ;

		// Token: 0x040014E7 RID: 5351
		private ushort ᜃ;
	}
}
