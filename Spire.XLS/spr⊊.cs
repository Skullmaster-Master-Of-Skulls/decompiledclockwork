using System;
using System.Collections.Generic;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x02000516 RID: 1302
internal class spr\u228A
{
	// Token: 0x06004F28 RID: 20264 RVA: 0x002FEC78 File Offset: 0x002FDC78
	public spr\u228A(double A_0, double A_1)
	{
		int a_ = 3;
		base..ctor();
		if (A_1 == 0.0)
		{
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("尸唺䠼刾⑀ㅂ⑄㍆♈㥊", a_));
		}
		this.ᜂ = A_0;
		this.ᜃ = A_1;
	}

	// Token: 0x06004F29 RID: 20265 RVA: 0x002FECC4 File Offset: 0x002FDCC4
	public spr\u228A(double A_0) : this(A_0, 1.0)
	{
	}

	// Token: 0x06004F2A RID: 20266 RVA: 0x002FECE4 File Offset: 0x002FDCE4
	public double ᜂ()
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

	// Token: 0x06004F2B RID: 20267 RVA: 0x002FED28 File Offset: 0x002FDD28
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
		this.ᜂ = A_0;
	}

	// Token: 0x06004F2C RID: 20268 RVA: 0x002FED6C File Offset: 0x002FDD6C
	public double ᜄ()
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
		return this.ᜃ;
	}

	// Token: 0x06004F2D RID: 20269 RVA: 0x002FEDB0 File Offset: 0x002FDDB0
	public void ᜁ(double A_0)
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
		this.ᜃ = A_0;
	}

	// Token: 0x06004F2E RID: 20270 RVA: 0x002FEDF4 File Offset: 0x002FDDF4
	public int ᜀ()
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
		return (int)Math.Log10(this.ᜄ()) + 1;
	}

	// Token: 0x06004F2F RID: 20271 RVA: 0x002FEE40 File Offset: 0x002FDE40
	public static spr\u228A ᜀ(spr\u228A A_0, spr\u228A A_1)
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
		double num = A_0.ᜂ() * A_1.ᜄ() + A_0.ᜄ() * A_1.ᜂ();
		double num2 = A_1.ᜄ() * A_0.ᜄ();
		double num3 = spr\u228A.ᜀ(num, num2);
		num /= num3;
		num2 /= num3;
		return new spr\u228A(num, num2);
	}

	// Token: 0x06004F30 RID: 20272 RVA: 0x002FEEBC File Offset: 0x002FDEBC
	public static double ᜀ(spr\u228A A_0)
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
		return A_0.ᜂ() / A_0.ᜄ();
	}

	// Token: 0x06004F31 RID: 20273 RVA: 0x002FEF04 File Offset: 0x002FDF04
	public static spr\u228A ᜀ(List<double> A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				spr\u228A spr_u228A;
				int count;
				switch (num)
				{
				case 0:
					return spr_u228A;
				case 1:
				{
					double num2 = A_0[count - 1];
					spr_u228A = new spr\u228A(num2, 1.0);
					int num3 = count - 2;
					num = 6;
					continue;
				}
				case 2:
				{
					int num3;
					if (num3 < 0)
					{
						num = 0;
						continue;
					}
					double num2 = A_0[num3];
					spr_u228A = spr\u228A.ᜀ(spr_u228A.ᜃ(), spr\u228A.ᜀ(num2));
					num3--;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_58;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				case 3:
					goto IL_58;
				case 5:
					goto IL_F2;
				case 6:
					goto IL_F2;
				case 7:
					if (count > 0)
					{
						num = 1;
						continue;
					}
					return spr_u228A;
				}
				if (A_0 == null)
				{
					num = 3;
					continue;
				}
				count = A_0.Count;
				spr_u228A = null;
				num = 7;
				continue;
				IL_F2:
				num = 2;
			}
			IL_58:
			if (true)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("⹇㡉ⵋⵍ⑏㭑㭓㡕", a_));
		}
		}
	}

	// Token: 0x06004F32 RID: 20274 RVA: 0x002FF050 File Offset: 0x002FE050
	public static spr\u228A ᜀ(double A_0)
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
		return new spr\u228A(A_0);
	}

	// Token: 0x06004F33 RID: 20275 RVA: 0x002FF094 File Offset: 0x002FE094
	public spr\u228A ᜃ()
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
		double num = this.ᜂ;
		this.ᜂ = this.ᜃ;
		this.ᜃ = num;
		return this;
	}

	// Token: 0x06004F34 RID: 20276 RVA: 0x002FF0EC File Offset: 0x002FE0EC
	public static spr\u228A ᜀ(double A_0, int A_1)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				double num2;
				List<double> a_2;
				spr\u228A spr_u228A;
				spr\u228A spr_u228A2;
				double num4;
				switch (num)
				{
				case 0:
					if (Math.Abs(num2) <= 1E-09)
					{
						num = 9;
						continue;
					}
					num2 = spr\u228A.ᜀ(a_2, num2);
					spr_u228A = spr\u228A.ᜀ(a_2);
					num = 3;
					continue;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_CE;
				case 3:
					if (spr_u228A.ᜀ() <= A_1)
					{
						num = 6;
						continue;
					}
					return spr_u228A2;
				case 4:
					goto IL_73;
				case 5:
				{
					double num3;
					if (num3 >= num4)
					{
						goto IL_CE;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				}
				case 6:
				{
					double num3 = spr\u228A.ᜀ(spr_u228A, A_0);
					num = 5;
					continue;
				}
				case 7:
				{
					spr_u228A2 = spr_u228A;
					double num3;
					num4 = num3;
					goto IL_7B;
				}
				case 8:
					goto IL_CE;
				case 9:
					return spr_u228A2;
				}
				if (A_1 < 1)
				{
					num = 4;
					continue;
				}
				A_1 = Math.Min(A_1, 9);
				a_2 = new List<double>();
				num2 = A_0;
				num2 = spr\u228A.ᜀ(a_2, num2);
				spr_u228A2 = spr\u228A.ᜀ(a_2);
				num4 = spr\u228A.ᜀ(spr_u228A2, A_0);
				spr_u228A = spr_u228A2;
				num = 2;
				continue;
				IL_7B:
				num = 8;
				continue;
				IL_CE:
				num = 0;
			}
			IL_73:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⅄⹆⹈≊㥌㱎ὐ♒㡔㕖㱘⥚", a_));
		}
		}
	}

	// Token: 0x06004F35 RID: 20277 RVA: 0x002FF27C File Offset: 0x002FE27C
	private static double ᜀ(double A_0, double A_1)
	{
		for (;;)
		{
			double num = Math.Round(Math.Max(A_0, A_1));
			double num2 = Math.Round(Math.Min(A_0, A_1));
			double num3 = num % num2;
			int num4 = 0;
			for (;;)
			{
				switch (num4)
				{
				case 0:
					if (num2 == 0.0)
					{
						if (true)
						{
						}
						num4 = 3;
						continue;
					}
					goto IL_7C;
				case 1:
					if (num3 == 0.0)
					{
						num4 = 2;
						continue;
					}
					num = num2;
					num2 = num3;
					num3 = Math.Round(num % num2);
					num4 = 4;
					continue;
				case 2:
					return num2;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						goto IL_74;
					}
					break;
				case 4:
					goto IL_7C;
				}
				break;
				IL_7C:
				num4 = 1;
			}
		}
		IL_74:
		if (false)
		{
		}
		return 1.0;
	}

	// Token: 0x06004F36 RID: 20278 RVA: 0x002FF358 File Offset: 0x002FE358
	private static double ᜀ(spr\u228A A_0, double A_1)
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
		return Math.Abs(spr\u228A.ᜀ(A_0) - A_1);
	}

	// Token: 0x06004F37 RID: 20279 RVA: 0x002FF3A4 File Offset: 0x002FE3A4
	private static double ᜀ(List<double> A_0, double A_1)
	{
		int num = 1;
		double num2;
		for (;;)
		{
			int count;
			switch (num)
			{
			case 0:
				goto IL_3D;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num2 = 1.0 / A_1;
					A_1 = num2;
					num = 4;
					continue;
				}
				break;
			case 3:
				if (count != 0)
				{
					num = 2;
					continue;
				}
				goto IL_B2;
			case 4:
				goto IL_7A;
			}
			IL_24:
			if (Math.Abs(A_1) < 1E-09)
			{
				num = 0;
				continue;
			}
			count = A_0.Count;
			num = 3;
			continue;
			goto IL_24;
		}
		IL_3D:
		if (true)
		{
		}
		return 0.0;
		IL_7A:
		IL_B2:
		num2 = Math.Floor(A_1);
		A_0.Add(num2);
		return A_1 - num2;
	}

	// Token: 0x06004F38 RID: 20280 RVA: 0x002FF474 File Offset: 0x002FE474
	public virtual string ᜁ()
	{
		int a_ = 19;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜂ.ToString() + RecordTableEnumerator.b("楈摊浌", a_) + this.ᜃ.ToString();
	}

	// Token: 0x040023BA RID: 9146
	private const int ᜀ = 9;

	// Token: 0x040023BB RID: 9147
	private const double ᜁ = 1E-09;

	// Token: 0x040023BC RID: 9148
	private double ᜂ;

	// Token: 0x040023BD RID: 9149
	private double ᜃ;
}
