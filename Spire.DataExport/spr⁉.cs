using System;
using System.Collections;

// Token: 0x02000085 RID: 133
internal class spr\u2049 : spr\u1CE3
{
	// Token: 0x06000411 RID: 1041 RVA: 0x00027AE0 File Offset: 0x00026AE0
	public spr\u2049(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x06000412 RID: 1042 RVA: 0x00027B0C File Offset: 0x00026B0C
	protected override void ᜁ(spr\u1DEE A_0)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜂ != -1)
				{
					goto IL_97;
				}
				goto IL_CA;
			case 1:
				num = 6;
				continue;
			case 2:
				goto IL_FF;
			case 3:
				num = 4;
				continue;
			case 4:
				if ((int)A_0.\u171F() < this.ᜁ)
				{
					num = 2;
					continue;
				}
				goto IL_83;
			case 6:
				if (true)
				{
				}
				if ((int)A_0.\u171F() > this.ᜂ)
				{
					num = 7;
					continue;
				}
				return;
			case 7:
				goto IL_CA;
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_97;
				default:
					goto IL_F7;
				}
				break;
			case 9:
				goto IL_83;
			}
			if (this.ᜁ != -1)
			{
				num = 3;
				continue;
			}
			goto IL_FF;
			IL_83:
			num = 0;
			continue;
			IL_97:
			num = 1;
			continue;
			IL_CA:
			this.ᜂ = (int)A_0.\u171F();
			num = 8;
			continue;
			IL_FF:
			this.ᜁ = (int)A_0.\u171F();
			num = 9;
		}
		IL_F7:
		if (false)
		{
		}
	}

	// Token: 0x06000413 RID: 1043 RVA: 0x00027C34 File Offset: 0x00026C34
	protected override void ᜀ(spr\u1DEE A_0)
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
		this.ᜀ = (int)A_0.\u171E();
	}

	// Token: 0x06000414 RID: 1044 RVA: 0x00027C7C File Offset: 0x00026C7C
	public bool ᜀ(int A_0, ref int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			bool result;
			int num2;
			for (;;)
			{
				int num4;
				int num5;
				switch (num)
				{
				case 1:
				{
					result = true;
					int num3;
					num2 = num3;
					num = 10;
					continue;
				}
				case 2:
					goto IL_10C;
				case 3:
				{
					if (num4 < 0)
					{
						num = 7;
						continue;
					}
					int num3;
					num5 = num3 - 1;
					num = 9;
					continue;
				}
				case 4:
					goto IL_12D;
				case 5:
					this.ᜀ();
					num = 12;
					continue;
				case 6:
					num4 = -1;
					num = 8;
					continue;
				case 7:
				{
					int num3;
					num2 = num3 + 1;
					num = 4;
					continue;
				}
				case 8:
					goto IL_10C;
				case 9:
					if (num4 == 0)
					{
						num = 1;
						continue;
					}
					goto IL_12D;
				case 10:
					goto IL_12D;
				case 11:
					goto IL_17C;
				case 12:
					goto IL_EF;
				case 13:
					goto IL_14C;
				case 14:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17C;
					default:
						if (false)
						{
						}
						goto IL_10C;
					}
					break;
				case 15:
				{
					int num3;
					if ((int)base.ᜀ(num3).\u171F() < A_0)
					{
						num = 6;
						continue;
					}
					num = 17;
					continue;
				}
				case 16:
				{
					if (num2 > num5)
					{
						num = 13;
						continue;
					}
					int num3 = num2 + num5 >> 1;
					num = 15;
					continue;
				}
				case 17:
				{
					int num3;
					if ((int)base.ᜀ(num3).\u171F() > A_0)
					{
						num = 11;
						continue;
					}
					num4 = 0;
					num = 2;
					continue;
				}
				case 18:
					goto IL_12D;
				}
				if (!base.ᜄ())
				{
					num = 5;
					continue;
				}
				IL_EF:
				result = false;
				num2 = 0;
				num5 = base.ᜌ() - 1;
				num4 = 0;
				num = 18;
				continue;
				IL_10C:
				num = 3;
				continue;
				IL_12D:
				num = 16;
				continue;
				IL_17C:
				if (true)
				{
				}
				num4 = 1;
				num = 14;
			}
			IL_14C:
			A_1 = num2;
			return result;
		}
		}
	}

	// Token: 0x06000415 RID: 1045 RVA: 0x00027E9C File Offset: 0x00026E9C
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
		base.ᜀ(new spr\u2049.ᜀ());
		base.ᜀ(true);
	}

	// Token: 0x06000416 RID: 1046 RVA: 0x00027EEC File Offset: 0x00026EEC
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
		return this.ᜀ;
	}

	// Token: 0x06000417 RID: 1047 RVA: 0x00027F30 File Offset: 0x00026F30
	public int ᜂ()
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

	// Token: 0x06000418 RID: 1048 RVA: 0x00027F74 File Offset: 0x00026F74
	public int ᜃ()
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

	// Token: 0x04000292 RID: 658
	private new int ᜀ = -1;

	// Token: 0x04000293 RID: 659
	private new int ᜁ = -1;

	// Token: 0x04000294 RID: 660
	private new int ᜂ = -1;

	// Token: 0x02000086 RID: 134
	private new class ᜀ : IComparer
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x00027FB8 File Offset: 0x00026FB8
		int IComparer.ᜀ(object A_0, object A_1)
		{
			int num = 2;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if ((A_0 as spr\u1DEE).\u171F() > (A_1 as spr\u1DEE).\u171F())
					{
						num = 3;
						continue;
					}
					return 0;
				case 1:
					return -1;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return 0;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 3:
					return 1;
				}
				if ((A_0 as spr\u1DEE).\u171F() < (A_1 as spr\u1DEE).\u171F())
				{
					num = 1;
				}
				else
				{
					num = 0;
				}
			}
			return -1;
		}
	}
}
