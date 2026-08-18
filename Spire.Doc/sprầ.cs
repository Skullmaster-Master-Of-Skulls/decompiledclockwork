using System;

// Token: 0x02000408 RID: 1032
internal class sprầ
{
	// Token: 0x0600396A RID: 14698 RVA: 0x0035674C File Offset: 0x0035574C
	internal static spr\u2262 ᜀ(spr\u2262 A_0, spr\u2262 A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					int num2;
					switch (num2)
					{
					case 1:
					{
						double num3 = (double)A_0.ᜄ();
						num3 = (255.0 - num3) / 255.0;
						spr᪅ spr᪅ = new spr᪅(A_1);
						spr᪅.ᜀ((float)spr\u2109.ᜂ(spr\u2109.ᜁ((double)spr᪅.ᜃ() - (double)spr᪅.ᜃ() * num3, 0.0, 255.0)));
						A_0 = spr᪅.ᜂ();
						num = 8;
						continue;
					}
					case 2:
					{
						double num4 = (double)A_0.ᜄ();
						num4 = (255.0 - num4) / 255.0;
						spr\u21F9 spr_u21F = new spr\u21F9(A_1.ᜈ());
						spr_u21F.ᜀ(spr_u21F.ᜃ() + (1.0 - spr_u21F.ᜃ()) * num4);
						A_0 = spr_u21F.ᜁ();
						if (true)
						{
						}
						num = 5;
						continue;
					}
					case 3:
						goto IL_13A;
					default:
						num = 1;
						continue;
					}
					break;
				}
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_13A;
				case 3:
				{
					int num5 = A_0.ᜃ();
					num = 9;
					continue;
				}
				case 4:
					return A_0;
				case 5:
					goto IL_DA;
				case 6:
				{
					int num2 = A_0.ᜆ();
					num = 0;
					continue;
				}
				case 8:
					goto IL_1FC;
				case 9:
				{
					int num5;
					if (num5 == 240)
					{
						num = 6;
						continue;
					}
					return A_0;
				}
				}
				if (A_0.ᜁ() == 239)
				{
					num = 3;
					continue;
				}
				return A_0;
				IL_13A:
				A_0 = A_1;
				num = 4;
			}
			IL_DA:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1FC:
				break;
			default:
				if (false)
				{
				}
				break;
			}
			return A_0;
		}
		}
	}
}
