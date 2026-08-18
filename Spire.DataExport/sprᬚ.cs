using System;
using System.Collections;
using System.Reflection;

// Token: 0x02000117 RID: 279
[DefaultMember("Item")]
internal class sprᬚ : sprᠪ
{
	// Token: 0x0600067B RID: 1659 RVA: 0x0003E7AC File Offset: 0x0003D7AC
	public sprᬚ(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0003E7C0 File Offset: 0x0003D7C0
	public int ᜀ(sprᵾ A_0)
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
		this.ᜀ = false;
		return base.ᜀ(A_0);
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0003E80C File Offset: 0x0003D80C
	public void ᜁ(int A_0, sprᵾ A_1)
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
		this.ᜀ = false;
		base.ᜁ(A_0, A_1);
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0003E858 File Offset: 0x0003D858
	public bool ᜀ(int A_0, ref int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 10;
			int num5;
			bool result;
			for (;;)
			{
				int num2;
				int num3;
				switch (num)
				{
				case 0:
					goto IL_152;
				case 1:
					if (true)
					{
					}
					if (num2 == 0)
					{
						goto IL_1E3;
					}
					goto IL_152;
				case 2:
				{
					if (num2 < 0)
					{
						num = 12;
						continue;
					}
					int num4;
					num3 = num4 - 1;
					num = 1;
					continue;
				}
				case 3:
					this.ᜀ();
					num = 6;
					continue;
				case 4:
				{
					if (num5 > num3)
					{
						num = 7;
						continue;
					}
					int num4 = num5 + num3 >> 1;
					num = 5;
					continue;
				}
				case 5:
				{
					int num4;
					if ((int)this.ᜀ(num4).\u170D() >= A_0)
					{
						num = 13;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E3;
					default:
						if (false)
						{
						}
						num = 17;
						continue;
					}
					break;
				}
				case 6:
					goto IL_111;
				case 7:
					goto IL_171;
				case 8:
					goto IL_12E;
				case 9:
					num2 = 1;
					num = 18;
					continue;
				case 11:
					goto IL_152;
				case 12:
				{
					int num4;
					num5 = num4 + 1;
					num = 11;
					continue;
				}
				case 13:
				{
					int num4;
					if ((int)this.ᜀ(num4).\u170D() > A_0)
					{
						num = 9;
						continue;
					}
					num2 = 0;
					num = 8;
					continue;
				}
				case 14:
					goto IL_152;
				case 15:
				{
					result = true;
					int num4;
					num5 = num4;
					num = 0;
					continue;
				}
				case 16:
					goto IL_12E;
				case 17:
					num2 = -1;
					num = 16;
					continue;
				case 18:
					goto IL_12E;
				}
				if (!this.ᜀ)
				{
					num = 3;
					continue;
				}
				IL_111:
				result = false;
				num5 = 0;
				num3 = base.ᜌ() - 1;
				num2 = 0;
				num = 14;
				continue;
				IL_12E:
				num = 2;
				continue;
				IL_152:
				num = 4;
				continue;
				IL_1E3:
				num = 15;
			}
			IL_171:
			A_1 = num5;
			return result;
		}
		}
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x0003EA84 File Offset: 0x0003DA84
	public new void ᜀ()
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
		base.ᜀ(new sprᬚ.ᜀ());
		this.ᜀ = true;
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0003EAD4 File Offset: 0x0003DAD4
	public string ᜁ(int A_0)
	{
		int a_;
		for (;;)
		{
			a_ = 0;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 <= spr\u2009.᠔.GetUpperBound(0))
					{
						num = 1;
						continue;
					}
					goto IL_C5;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8A;
					default:
						goto IL_B7;
					}
					break;
				case 2:
					if (A_0 >= spr\u2009.᠔.GetLowerBound(0))
					{
						num = 5;
						continue;
					}
					goto IL_C5;
				case 3:
					goto IL_3D;
				case 4:
					if (this.ᜀ(A_0, ref a_))
					{
						num = 3;
						continue;
					}
					num = 2;
					continue;
				case 5:
					goto IL_8A;
				}
				break;
				IL_8A:
				if (true)
				{
				}
				num = 0;
			}
		}
		IL_3D:
		return this.ᜀ(a_).ᜁ();
		IL_B7:
		if (false)
		{
		}
		return spr\u2009.᠔[A_0];
		IL_C5:
		return string.Empty;
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x0003EBAC File Offset: 0x0003DBAC
	public new sprᵾ ᜀ(int A_0)
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
		return base.ᜀ(A_0) as sprᵾ;
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0003EBF4 File Offset: 0x0003DBF4
	public void ᜀ(int A_0, sprᵾ A_1)
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
		base.ᜀ(A_0, A_1);
	}

	// Token: 0x040005AE RID: 1454
	private new bool ᜀ;

	// Token: 0x02000118 RID: 280
	private new class ᜀ : IComparer
	{
		// Token: 0x06000683 RID: 1667 RVA: 0x0003EC38 File Offset: 0x0003DC38
		int IComparer.ᜀ(object A_0, object A_1)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 3;
				break;
			}
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					return -1;
				case 1:
					return 1;
				case 2:
					if ((A_0 as sprᵾ).\u170D() > (A_1 as sprᵾ).\u170D())
					{
						num = 1;
						continue;
					}
					return 0;
				}
				if ((A_0 as sprᵾ).\u170D() < (A_1 as sprᵾ).\u170D())
				{
					num = 0;
				}
				else
				{
					num = 2;
				}
			}
			return -1;
		}
	}
}
