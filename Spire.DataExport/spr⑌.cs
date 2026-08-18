using System;
using System.Collections;
using System.Reflection;

// Token: 0x020000F1 RID: 241
[DefaultMember("Item")]
internal class spr\u244C : spr\u2574
{
	// Token: 0x0600051D RID: 1309 RVA: 0x00031E6C File Offset: 0x00030E6C
	public spr\u244C(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00031E80 File Offset: 0x00030E80
	public int ᜀ(spr\u2049 A_0)
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
		return base.ᜁ(A_0);
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00031ECC File Offset: 0x00030ECC
	public void ᜁ(int A_0, spr\u2049 A_1)
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

	// Token: 0x06000520 RID: 1312 RVA: 0x00031F18 File Offset: 0x00030F18
	public bool ᜀ(int A_0, ref int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			int num2;
			bool result;
			for (;;)
			{
				int num3;
				int num5;
				switch (num)
				{
				case 1:
				{
					if (num2 > num3)
					{
						num = 4;
						continue;
					}
					int num4 = num2 + num3 >> 1;
					num = 15;
					continue;
				}
				case 2:
					goto IL_11C;
				case 3:
				{
					if (num5 < 0)
					{
						num = 18;
						continue;
					}
					int num4;
					num3 = num4 - 1;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_77;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				}
				case 4:
					goto IL_15C;
				case 5:
				{
					result = true;
					int num4;
					num2 = num4;
					num = 11;
					continue;
				}
				case 6:
					num5 = 1;
					num = 2;
					continue;
				case 7:
				{
					int num4;
					if (this.ᜀ(num4).ᜁ() > A_0)
					{
						num = 6;
						continue;
					}
					num5 = 0;
					num = 17;
					continue;
				}
				case 8:
					goto IL_13D;
				case 9:
					goto IL_FF;
				case 10:
					goto IL_11C;
				case 11:
					goto IL_13D;
				case 12:
					goto IL_13D;
				case 13:
					num5 = -1;
					num = 10;
					continue;
				case 14:
					this.ᜀ();
					num = 9;
					continue;
				case 15:
				{
					int num4;
					if (this.ᜀ(num4).ᜁ() < A_0)
					{
						num = 13;
						continue;
					}
					num = 7;
					continue;
				}
				case 16:
					if (true)
					{
					}
					if (num5 == 0)
					{
						num = 5;
						continue;
					}
					goto IL_13D;
				case 17:
					goto IL_11C;
				case 18:
				{
					int num4;
					num2 = num4 + 1;
					num = 12;
					continue;
				}
				}
				goto IL_6C;
				IL_77:
				num = 14;
				continue;
				IL_6C:
				if (!this.ᜀ)
				{
					goto IL_77;
				}
				IL_FF:
				result = false;
				num2 = 0;
				num3 = base.ᜌ() - 1;
				num5 = 0;
				num = 8;
				continue;
				IL_11C:
				num = 3;
				continue;
				IL_13D:
				num = 1;
			}
			IL_15C:
			A_1 = num2;
			return result;
		}
		}
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00032140 File Offset: 0x00031140
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
		base.ᜀ(new spr\u244C.ᜀ());
		this.ᜀ = true;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00032190 File Offset: 0x00031190
	public new spr\u2049 ᜀ(int A_0)
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
		return base.ᜀ(A_0) as spr\u2049;
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x000321D8 File Offset: 0x000311D8
	public void ᜀ(int A_0, spr\u2049 A_1)
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

	// Token: 0x04000572 RID: 1394
	private new bool ᜀ;

	// Token: 0x020000F2 RID: 242
	private new class ᜀ : IComparer
	{
		// Token: 0x06000524 RID: 1316 RVA: 0x0003221C File Offset: 0x0003121C
		int IComparer.ᜀ(object A_0, object A_1)
		{
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
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						return 1;
					case 2:
						return -1;
					case 3:
						if ((A_0 as spr\u2049).ᜁ() > (A_1 as spr\u2049).ᜁ())
						{
							num = 1;
							continue;
						}
						return 0;
					}
					if ((A_0 as spr\u2049).ᜁ() < (A_1 as spr\u2049).ᜁ())
					{
						num = 2;
					}
					else
					{
						num = 3;
					}
				}
				return 1;
			}
			}
			return -1;
		}
	}
}
