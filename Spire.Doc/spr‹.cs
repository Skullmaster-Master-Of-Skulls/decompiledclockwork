using System;
using System.IO;
using Spire.CompoundFile.Doc;

// Token: 0x020002BE RID: 702
[CLSCompliant(false)]
internal class spr\u2039 : spr\u23F8
{
	// Token: 0x0600263D RID: 9789 RVA: 0x0025E05C File Offset: 0x0025D05C
	internal spr\u2039()
	{
	}

	// Token: 0x0600263E RID: 9790 RVA: 0x0025E070 File Offset: 0x0025D070
	internal spr\u2039(byte[] A_0) : base(A_0)
	{
	}

	// Token: 0x0600263F RID: 9791 RVA: 0x0025E084 File Offset: 0x0025D084
	internal spr\u2039(Stream A_0, int A_1) : base(A_0, A_1)
	{
	}

	// Token: 0x06002640 RID: 9792 RVA: 0x0025E09C File Offset: 0x0025D09C
	internal override void ᜀ(byte[] A_0, int A_1, int A_2)
	{
		int a_ = 5;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				goto IL_F0;
			case 2:
				goto IL_18D;
			case 3:
			{
				if (A_2 != A_0.Length)
				{
					num = 2;
					continue;
				}
				int num2 = (A_2 - 4) / 8;
				this.ᜀ = new uint[num2 + 1];
				this.ᜁ = new spr\u2490[num2];
				int num3 = (num2 + 1) * 4;
				Buffer.BlockCopy(A_0, 0, this.ᜀ, 0, num3);
				A_1 = num3;
				int num4 = 0;
				num = 0;
				continue;
			}
			case 4:
				return;
			case 5:
				goto IL_F0;
			case 6:
				goto IL_59;
			case 7:
				if (A_1 != 0)
				{
					num = 9;
					continue;
				}
				num = 3;
				continue;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (false)
					{
					}
					int num2;
					int num4;
					if (num4 >= num2)
					{
						num = 4;
						continue;
					}
					this.ᜁ[num4] = new spr\u2490();
					A_1 = this.ᜁ[num4].ᜁ(A_0, A_1);
					num4++;
					num = 5;
					continue;
				}
				}
				break;
			case 9:
				goto IL_EB;
			}
			if (A_0 == null)
			{
				num = 6;
				continue;
			}
			num = 7;
			continue;
			IL_F0:
			num = 8;
		}
		IL_59:
		throw new ArgumentNullException(ClipboardData.b("੪Ὤᵮ㕰ቲŴᙶ", a_));
		IL_EB:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ɪ≬८ᝰrၴͶ", a_), ClipboardData.b("㵪౬ͮѰᙲ啴ᑶᡸᕺ፼ၾꎂꦈﲎ뎒떔ﾚ붜펠욢쒤펦첨\ud9aa趬", a_));
		IL_18D:
		throw new ArgumentOutOfRangeException(ClipboardData.b("ɪⅬ੮ὰᑲŴὶ", a_), ClipboardData.b("㵪౬ͮѰᙲ啴ᑶᡸᕺ፼ၾꎂꦈﲎ뎒떔ﾚ붜펠욢쒤펦첨\ud9aa趬", a_));
	}

	// Token: 0x06002641 RID: 9793 RVA: 0x0025E260 File Offset: 0x0025D260
	internal override int ᜀ(byte[] A_0, int A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 6;
			int num2;
			for (;;)
			{
				int num4;
				int num5;
				switch (num)
				{
				case 0:
					goto IL_151;
				case 1:
					goto IL_151;
				case 2:
				{
					if (A_1 + num2 > A_0.Length)
					{
						num = 4;
						continue;
					}
					int num3;
					Buffer.BlockCopy(this.ᜀ, 0, A_0, A_1, num3);
					A_1 += num3;
					num4 = 0;
					num5 = this.ᜁ.Length;
					num = 0;
					continue;
				}
				case 3:
				{
					if (A_1 > A_0.Length)
					{
						num = 11;
						continue;
					}
					int num3 = 4 * this.ᜀ.Length;
					int num6 = this.ᜁ.Length * 4;
					num2 = num3 + num6;
					if (true)
					{
					}
					num = 2;
					continue;
				}
				case 4:
					goto IL_DD;
				case 5:
					goto IL_68;
				case 7:
					goto IL_15D;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_15D;
					}
					goto Block_5;
				case 9:
					num = 3;
					continue;
				case 10:
					if (A_1 >= 0)
					{
						num = 9;
						continue;
					}
					goto IL_196;
				case 11:
					goto IL_1CB;
				}
				if (A_0 == null)
				{
					num = 5;
					continue;
				}
				num = 10;
				continue;
				IL_15D:
				if (num4 >= num5)
				{
					num = 8;
					continue;
				}
				this.ᜁ[num4].ᜀ(A_0, A_1);
				A_1 += 4;
				num4++;
				num = 1;
				continue;
				IL_151:
				num = 7;
			}
			IL_68:
			throw new ArgumentNullException(ClipboardData.b("ࡨᥪὬ⭮ၰݲᑴ", a_));
			IL_DD:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ࡨᥪὬ⭮ၰݲᑴ奶㕸Ṻ፼᡾", a_));
			Block_5:
			if (false)
			{
			}
			return num2;
			IL_196:
			throw new ArgumentOutOfRangeException(ClipboardData.b("h⑪୬८ɰᙲŴ", a_));
			IL_1CB:
			goto IL_196;
		}
		}
	}

	// Token: 0x06002642 RID: 9794 RVA: 0x0025E43C File Offset: 0x0025D43C
	internal uint[] ᜁ()
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

	// Token: 0x06002643 RID: 9795 RVA: 0x0025E480 File Offset: 0x0025D480
	internal spr\u2490[] ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x06002644 RID: 9796 RVA: 0x0025E4C4 File Offset: 0x0025D4C4
	internal int ᜂ()
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
		return this.ᜁ.Length;
	}

	// Token: 0x06002645 RID: 9797 RVA: 0x0025E508 File Offset: 0x0025D508
	internal void ᜀ(int A_0)
	{
		int a_ = 14;
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
			if (A_0 < 0)
			{
				throw new ArgumentOutOfRangeException(ClipboardData.b("㡳፵ᙷᵹࡻᙽ", a_));
			}
			break;
		}
		this.ᜀ = new uint[A_0 + 1];
		this.ᜁ = new spr\u2490[A_0];
	}

	// Token: 0x06002646 RID: 9798 RVA: 0x0025E580 File Offset: 0x0025D580
	internal override int ᜇ()
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
		int num = 4 * this.ᜀ.Length;
		int num2 = this.ᜁ.Length * 4;
		return num + num2;
	}

	// Token: 0x0400222B RID: 8747
	private new uint[] ᜀ;

	// Token: 0x0400222C RID: 8748
	private new spr\u2490[] ᜁ;
}
