using System;
using Spire.CompoundFile.Doc;

// Token: 0x020002C8 RID: 712
internal class sprᠻ
{
	// Token: 0x060026A2 RID: 9890 RVA: 0x002624AC File Offset: 0x002614AC
	internal sprᠻ(double A_0, double A_1, double A_2, double A_3)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = A_2;
		this.ᜃ = A_3;
	}

	// Token: 0x060026A3 RID: 9891 RVA: 0x002624DC File Offset: 0x002614DC
	internal void ᜀ()
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
		this.ᜀ += (this.ᜂ - this.ᜃ) / 2.0;
		this.ᜁ += (this.ᜃ - this.ᜂ) / 2.0;
		double num = this.ᜂ;
		this.ᜂ = this.ᜃ;
		this.ᜃ = num;
	}

	// Token: 0x060026A4 RID: 9892 RVA: 0x0026257C File Offset: 0x0026157C
	internal static bool ᜀ(double A_0)
	{
		int a_ = 9;
		for (;;)
		{
			A_0 %= 360.0;
			int num = 13;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0 >= 225.0)
					{
						num = 4;
						continue;
					}
					goto IL_151;
				case 1:
					if (A_0 >= 315.0)
					{
						num = 17;
						continue;
					}
					goto IL_24D;
				case 2:
					if (A_0 < 135.0)
					{
						num = 9;
						continue;
					}
					goto IL_223;
				case 3:
					num = 18;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_9E;
					default:
						if (false)
						{
						}
						num = 11;
						continue;
					}
					break;
				case 5:
					if (A_0 >= 135.0)
					{
						num = 3;
						continue;
					}
					goto IL_A3;
				case 6:
					if (A_0 < 45.0)
					{
						if (true)
						{
						}
						num = 10;
						continue;
					}
					goto IL_1BB;
				case 7:
					num = 2;
					continue;
				case 8:
					goto IL_9E;
				case 9:
					return true;
				case 10:
					return false;
				case 11:
					if (A_0 < 315.0)
					{
						num = 15;
						continue;
					}
					goto IL_151;
				case 12:
					if (A_0 >= 45.0)
					{
						num = 7;
						continue;
					}
					goto IL_223;
				case 13:
					if (A_0 < 0.0)
					{
						num = 8;
						continue;
					}
					goto IL_122;
				case 14:
					return false;
				case 15:
					return true;
				case 16:
					num = 6;
					continue;
				case 17:
					return false;
				case 18:
					if (A_0 < 225.0)
					{
						num = 14;
						continue;
					}
					goto IL_A3;
				case 19:
					goto IL_122;
				case 20:
					if (A_0 >= 0.0)
					{
						num = 16;
						continue;
					}
					goto IL_1BB;
				}
				break;
				IL_9E:
				A_0 += 360.0;
				num = 19;
				continue;
				IL_A3:
				num = 0;
				continue;
				IL_122:
				num = 20;
				continue;
				IL_151:
				num = 1;
				continue;
				IL_1BB:
				num = 12;
				continue;
				IL_223:
				num = 5;
			}
		}
		return false;
		IL_24D:
		throw new InvalidOperationException(ClipboardData.b("㩮ὰᙲ൴ݶᱸ᡺ॼ᩾ꎂꆎ", a_));
	}

	// Token: 0x060026A5 RID: 9893 RVA: 0x002627EC File Offset: 0x002617EC
	internal static void ᜁ(sprᩍ A_0, double A_1, double A_2, double A_3, double A_4)
	{
		for (;;)
		{
			IL_20:
			double num = A_1;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_65:
				num2 = 2;
				break;
			default:
				if (false)
				{
				}
				num2 = 3;
				break;
			}
			for (;;)
			{
				switch (num2)
				{
				case 0:
					A_1 = A_2;
					A_2 = num;
					num2 = 4;
					continue;
				case 1:
					if (A_3 > A_4)
					{
						num2 = 5;
						continue;
					}
					goto IL_A6;
				case 2:
					goto IL_6D;
				case 3:
					if (A_1 > A_2)
					{
						num2 = 0;
						continue;
					}
					goto IL_6F;
				case 4:
					goto IL_6F;
				case 5:
					goto IL_89;
				}
				goto IL_20;
				IL_6F:
				num = A_3;
				num2 = 1;
			}
			IL_89:
			if (true)
			{
			}
			A_3 = A_4;
			A_4 = num;
			goto IL_65;
		}
		IL_6D:
		IL_A6:
		sprᠻ.ᜀ(A_0, spr\u23C4.ᜊ(A_1), spr\u23C4.ᜊ(A_3), spr\u23C4.ᜊ(A_2 - A_1), spr\u23C4.ᜊ(A_4 - A_3));
	}

	// Token: 0x060026A6 RID: 9894 RVA: 0x002628C4 File Offset: 0x002618C4
	internal static void ᜀ(sprᩍ A_0, double A_1, double A_2, double A_3, double A_4)
	{
		int num;
		sprᠻ sprᠻ;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_28:
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 1:
					if (sprᠻ.ᜀ(A_0.ម()))
					{
						num = 2;
						continue;
					}
					goto IL_7C;
				case 2:
					sprᠻ.ᜀ();
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_3A;
			}
			IL_7A:
			IL_7C:
			A_0.ᜋ(sprᠻ.ᜀ);
			A_0.ᜂ(sprᠻ.ᜁ);
			A_0.ᜅ(sprᠻ.ᜂ);
			A_0.ᜇ(sprᠻ.ᜃ);
			return;
		}
		if (false)
		{
		}
		IL_3A:
		sprᠻ = new sprᠻ(A_1, A_2, A_3, A_4);
		num = 1;
		goto IL_28;
	}

	// Token: 0x060026A7 RID: 9895 RVA: 0x00262980 File Offset: 0x00261980
	internal static sprᠻ ᜀ(sprᩍ A_0)
	{
		int num;
		sprᠻ sprᠻ;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_28:
				switch (num)
				{
				case 0:
					if (sprᠻ.ᜀ(A_0.ម()))
					{
						num = 2;
						continue;
					}
					return sprᠻ;
				case 1:
					return sprᠻ;
				case 2:
					if (true)
					{
					}
					sprᠻ.ᜀ();
					num = 1;
					continue;
				}
				goto IL_3A;
			}
			return sprᠻ;
		}
		if (false)
		{
		}
		IL_3A:
		sprᠻ = new sprᠻ(A_0.\u177A(), A_0.ᝣ(), A_0.\u177D(), A_0.ន());
		num = 0;
		goto IL_28;
	}

	// Token: 0x0400226B RID: 8811
	internal double ᜀ;

	// Token: 0x0400226C RID: 8812
	internal double ᜁ;

	// Token: 0x0400226D RID: 8813
	internal double ᜂ;

	// Token: 0x0400226E RID: 8814
	internal double ᜃ;
}
