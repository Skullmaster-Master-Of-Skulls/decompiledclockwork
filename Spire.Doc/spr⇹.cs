using System;
using System.Drawing;

// Token: 0x0200040C RID: 1036
internal class spr\u21F9
{
	// Token: 0x0600399E RID: 14750 RVA: 0x00359CC4 File Offset: 0x00358CC4
	public spr\u21F9(double A_0, double A_1, double A_2)
	{
		this.ᜂ(A_0);
		this.ᜁ(A_1);
		this.ᜀ(A_2);
	}

	// Token: 0x0600399F RID: 14751 RVA: 0x00359CEC File Offset: 0x00358CEC
	public spr\u21F9(Color A_0)
	{
		double num = (double)A_0.R / 255.0;
		double num2 = (double)A_0.G / 255.0;
		double num3 = (double)A_0.B / 255.0;
		double num4 = Math.Max(Math.Max(num, num2), num3);
		double num5 = Math.Min(Math.Min(num, num2), num3);
		double num6 = (num4 + num5) / 2.0;
		this.ᜂ = num6;
		this.ᜀ = num6;
		this.ᜁ = num6;
		if (num4 == num5)
		{
			this.ᜀ = 0.0;
			this.ᜁ = 0.0;
		}
		else
		{
			double num7 = num4 - num5;
			this.ᜁ = ((this.ᜂ > 0.5) ? (num7 / (2.0 - num4 - num5)) : (num7 / (num4 + num5)));
			if (num == num4)
			{
				this.ᜀ = (num2 - num3) / num7 + (double)((num2 < num3) ? 6 : 0);
			}
			else if (num2 == num4)
			{
				this.ᜀ = (num3 - num) / num7 + 2.0;
			}
			else if (num3 == num4)
			{
				this.ᜀ = (num - num2) / num7 + 4.0;
			}
		}
		this.ᜀ /= 6.0;
	}

	// Token: 0x060039A0 RID: 14752 RVA: 0x00359E68 File Offset: 0x00358E68
	internal spr\u2262 ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			double num3;
			double num4;
			double num5;
			for (;;)
			{
				double num2;
				switch (num)
				{
				case 0:
					if (this.ᜂ >= 0.5)
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				case 1:
					if (true)
					{
					}
					num2 = this.ᜂ + this.ᜁ - this.ᜂ * this.ᜁ;
					goto IL_111;
				case 2:
					goto IL_175;
				case 3:
					num = 1;
					continue;
				case 5:
					num3 = this.ᜂ;
					num4 = this.ᜂ;
					num5 = this.ᜂ;
					num = 7;
					continue;
				case 6:
					num2 = this.ᜂ * (1.0 + this.ᜁ);
					goto IL_111;
				case 7:
					goto IL_1ED;
				case 8:
					goto IL_19F;
				case 9:
					goto IL_173;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_175;
					default:
						if (false)
						{
						}
						if (this.ᜁ == 0.0)
						{
							num = 5;
							continue;
						}
						num = 0;
						continue;
					}
					break;
				}
				if (this.ᜂ == 0.0)
				{
					num = 2;
					continue;
				}
				num = 10;
				continue;
				IL_111:
				double num6 = num2;
				double a_ = 2.0 * this.ᜂ - num6;
				num3 = spr\u21F9.ᜀ(a_, num6, this.ᜀ + 0.3333333333333333);
				num4 = spr\u21F9.ᜀ(a_, num6, this.ᜀ);
				num5 = spr\u21F9.ᜀ(a_, num6, this.ᜀ - 0.3333333333333333);
				num = 9;
				continue;
				IL_175:
				num3 = 0.0;
				num4 = 0.0;
				num5 = 0.0;
				num = 8;
			}
			IL_173:
			IL_19F:
			IL_1ED:
			return spr\u2262.ᜀ((int)(255.0 * num3), (int)(255.0 * num4), (int)(255.0 * num5));
		}
		}
	}

	// Token: 0x060039A1 RID: 14753 RVA: 0x0035A09C File Offset: 0x0035909C
	public Color ᜄ()
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			double num3;
			double num4;
			double num5;
			for (;;)
			{
				double num2;
				switch (num)
				{
				case 0:
					goto IL_182;
				case 1:
					goto IL_1FA;
				case 2:
					goto IL_1AC;
				case 3:
					goto IL_180;
				case 5:
					if (this.ᜂ >= 0.5)
					{
						num = 8;
						continue;
					}
					num = 7;
					continue;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_182;
					default:
						if (false)
						{
						}
						if (this.ᜁ == 0.0)
						{
							num = 9;
							continue;
						}
						if (true)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 7:
					num2 = this.ᜂ * (1.0 + this.ᜁ);
					goto IL_11E;
				case 8:
					num = 10;
					continue;
				case 9:
					num3 = this.ᜂ;
					num4 = this.ᜂ;
					num5 = this.ᜂ;
					num = 1;
					continue;
				case 10:
					num2 = this.ᜂ + this.ᜁ - this.ᜂ * this.ᜁ;
					goto IL_11E;
				}
				if (this.ᜂ == 0.0)
				{
					num = 0;
					continue;
				}
				num = 6;
				continue;
				IL_11E:
				double num6 = num2;
				double a_ = 2.0 * this.ᜂ - num6;
				num3 = spr\u21F9.ᜀ(a_, num6, this.ᜀ + 0.3333333333333333);
				num4 = spr\u21F9.ᜀ(a_, num6, this.ᜀ);
				num5 = spr\u21F9.ᜀ(a_, num6, this.ᜀ - 0.3333333333333333);
				num = 3;
				continue;
				IL_182:
				num3 = 0.0;
				num4 = 0.0;
				num5 = 0.0;
				num = 2;
			}
			IL_180:
			IL_1AC:
			IL_1FA:
			return Color.FromArgb((int)(255.0 * num3), (int)(255.0 * num4), (int)(255.0 * num5));
		}
		}
	}

	// Token: 0x060039A2 RID: 14754 RVA: 0x0035A2D4 File Offset: 0x003592D4
	private static double ᜀ(double A_0, double A_1, double A_2)
	{
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_77;
			case 1:
				goto IL_141;
			case 2:
				if (A_2 > 1.0)
				{
					num = 6;
					continue;
				}
				goto IL_141;
			case 3:
				if (true)
				{
				}
				if (A_2 < 0.16666666666666666)
				{
					num = 7;
					continue;
				}
				num = 10;
				continue;
			case 4:
				goto IL_DF;
			case 5:
				goto IL_E1;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E1;
				default:
					if (false)
					{
					}
					A_2 -= 1.0;
					num = 1;
					continue;
				}
				break;
			case 7:
				goto IL_16B;
			case 9:
				return A_1;
			case 10:
				if (A_2 < 0.5)
				{
					num = 9;
					continue;
				}
				num = 11;
				continue;
			case 11:
				if (A_2 < 0.6666666666666666)
				{
					num = 4;
					continue;
				}
				return A_0;
			}
			if (A_2 < 0.0)
			{
				num = 5;
				continue;
			}
			IL_77:
			num = 2;
			continue;
			IL_E1:
			A_2 += 1.0;
			num = 0;
			continue;
			IL_141:
			num = 3;
		}
		IL_DF:
		return A_0 + (A_1 - A_0) * (0.6666666666666666 - A_2) * 6.0;
		IL_16B:
		return A_0 + (A_1 - A_0) * 6.0 * A_2;
	}

	// Token: 0x060039A3 RID: 14755 RVA: 0x0035A46C File Offset: 0x0035946C
	public double ᜂ()
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

	// Token: 0x060039A4 RID: 14756 RVA: 0x0035A4B0 File Offset: 0x003594B0
	public void ᜂ(double A_0)
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
		this.ᜀ = spr\u2109.ᜁ(A_0, 0.0, 1.0);
	}

	// Token: 0x060039A5 RID: 14757 RVA: 0x0035A50C File Offset: 0x0035950C
	public double ᜀ()
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

	// Token: 0x060039A6 RID: 14758 RVA: 0x0035A550 File Offset: 0x00359550
	public void ᜁ(double A_0)
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
		this.ᜁ = spr\u2109.ᜁ(A_0, 0.0, 1.0);
	}

	// Token: 0x060039A7 RID: 14759 RVA: 0x0035A5AC File Offset: 0x003595AC
	public double ᜃ()
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

	// Token: 0x060039A8 RID: 14760 RVA: 0x0035A5F0 File Offset: 0x003595F0
	public void ᜀ(double A_0)
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
		this.ᜂ = spr\u2109.ᜁ(A_0, 0.0, 1.0);
	}

	// Token: 0x04002AC3 RID: 10947
	private double ᜀ;

	// Token: 0x04002AC4 RID: 10948
	private double ᜁ;

	// Token: 0x04002AC5 RID: 10949
	private double ᜂ;
}
