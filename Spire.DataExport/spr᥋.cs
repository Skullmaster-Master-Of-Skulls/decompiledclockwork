using System;
using System.Collections;

// Token: 0x0200013D RID: 317
internal class spr᥋ : spr\u1CE3
{
	// Token: 0x060007C7 RID: 1991 RVA: 0x0004E0E4 File Offset: 0x0004D0E4
	public spr᥋(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x060007C8 RID: 1992 RVA: 0x0004E110 File Offset: 0x0004D110
	protected override void ᜁ(spr\u1DEE A_0)
	{
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_95;
			case 1:
				return;
			case 2:
				num = 9;
				continue;
			case 3:
				if (this.ᜂ != -1)
				{
					num = 2;
					continue;
				}
				goto IL_E3;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_108;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 5:
				goto IL_FC;
			case 6:
				num = 7;
				continue;
			case 7:
				if ((int)A_0.\u171E() < this.ᜁ)
				{
					num = 5;
					continue;
				}
				goto IL_95;
			case 8:
				goto IL_E3;
			case 9:
				if ((int)A_0.\u171E() > this.ᜂ)
				{
					num = 8;
					continue;
				}
				return;
			}
			if (this.ᜁ != -1)
			{
				if (true)
				{
				}
				num = 6;
				continue;
			}
			goto IL_FC;
			IL_95:
			num = 3;
			continue;
			IL_E3:
			this.ᜂ = (int)A_0.\u171E();
			num = 1;
			continue;
			IL_108:
			num = 0;
			continue;
			IL_FC:
			this.ᜁ = (int)A_0.\u171E();
			goto IL_108;
		}
	}

	// Token: 0x060007C9 RID: 1993 RVA: 0x0004E234 File Offset: 0x0004D234
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
		this.ᜀ = (int)A_0.\u171F();
	}

	// Token: 0x060007CA RID: 1994 RVA: 0x0004E27C File Offset: 0x0004D27C
	public bool ᜀ(int A_0, ref int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 3;
			int num2;
			bool result;
			for (;;)
			{
				int num3;
				int num5;
				switch (num)
				{
				case 0:
				{
					if (num2 > num3)
					{
						num = 1;
						continue;
					}
					int num4 = num2 + num3 >> 1;
					num = 12;
					continue;
				}
				case 1:
					goto IL_174;
				case 2:
					goto IL_1A4;
				case 4:
					num5 = -1;
					num = 17;
					continue;
				case 5:
					goto IL_155;
				case 6:
				{
					int num4;
					num2 = num4 + 1;
					num = 5;
					continue;
				}
				case 7:
					goto IL_155;
				case 8:
					if (true)
					{
					}
					if (num5 == 0)
					{
						num = 10;
						continue;
					}
					goto IL_155;
				case 9:
					this.ᜀ();
					num = 15;
					continue;
				case 10:
				{
					result = true;
					int num4;
					num2 = num4;
					num = 16;
					continue;
				}
				case 11:
				{
					int num4;
					if ((int)base.ᜀ(num4).\u171E() > A_0)
					{
						num = 2;
						continue;
					}
					num5 = 0;
					num = 13;
					continue;
				}
				case 12:
				{
					int num4;
					if ((int)base.ᜀ(num4).\u171E() < A_0)
					{
						num = 4;
						continue;
					}
					num = 11;
					continue;
				}
				case 13:
					goto IL_131;
				case 14:
				{
					if (num5 < 0)
					{
						num = 6;
						continue;
					}
					int num4;
					num3 = num4 - 1;
					num = 8;
					continue;
				}
				case 15:
					goto IL_114;
				case 16:
					goto IL_155;
				case 17:
					goto IL_131;
				case 18:
					goto IL_131;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1A4:
					num5 = 1;
					num = 18;
					continue;
				default:
					if (false)
					{
					}
					if (!base.ᜄ())
					{
						num = 9;
						continue;
					}
					break;
				}
				IL_114:
				result = false;
				num2 = 0;
				num3 = base.ᜌ() - 1;
				num5 = 0;
				num = 7;
				continue;
				IL_131:
				num = 14;
				continue;
				IL_155:
				num = 0;
			}
			IL_174:
			A_1 = num2;
			return result;
		}
		}
	}

	// Token: 0x060007CB RID: 1995 RVA: 0x0004E4AC File Offset: 0x0004D4AC
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
		base.ᜀ(new spr᥋.ᜀ());
		base.ᜀ(true);
	}

	// Token: 0x060007CC RID: 1996 RVA: 0x0004E4FC File Offset: 0x0004D4FC
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
		return this.ᜀ;
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x0004E540 File Offset: 0x0004D540
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
		return this.ᜁ;
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x0004E584 File Offset: 0x0004D584
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
		return this.ᜂ;
	}

	// Token: 0x0400061F RID: 1567
	private new int ᜀ = -1;

	// Token: 0x04000620 RID: 1568
	private new int ᜁ = -1;

	// Token: 0x04000621 RID: 1569
	private new int ᜂ = -1;

	// Token: 0x0200013E RID: 318
	private new class ᜀ : IComparer
	{
		// Token: 0x060007CF RID: 1999 RVA: 0x0004E5C8 File Offset: 0x0004D5C8
		int IComparer.ᜀ(object A_0, object A_1)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7E;
				case 1:
					if ((A_0 as spr\u1DEE).\u171E() > (A_1 as spr\u1DEE).\u171E())
					{
						num = 0;
						continue;
					}
					return 0;
				case 3:
					return -1;
				}
				if ((A_0 as spr\u1DEE).\u171E() < (A_1 as spr\u1DEE).\u171E())
				{
					if (true)
					{
					}
					num = 3;
				}
				else
				{
					num = 1;
				}
			}
			return -1;
			IL_7E:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return 0;
			default:
				if (false)
				{
				}
				return 1;
			}
		}
	}
}
