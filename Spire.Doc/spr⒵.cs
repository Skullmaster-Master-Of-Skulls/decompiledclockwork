using System;
using System.Collections;

// Token: 0x0200037E RID: 894
internal class spr\u24B5
{
	// Token: 0x06003210 RID: 12816 RVA: 0x002E2930 File Offset: 0x002E1930
	internal spr\u24B5(spr\u24A6 A_0) : this(A_0, 2)
	{
	}

	// Token: 0x06003211 RID: 12817 RVA: 0x002E2948 File Offset: 0x002E1948
	internal spr\u24B5(spr\u24A6 A_0, int A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = new ArrayList();
	}

	// Token: 0x06003212 RID: 12818 RVA: 0x002E2974 File Offset: 0x002E1974
	internal void ᜀ(spr\u1B70 A_0, spr\u1B70[] A_1)
	{
		int num = 3;
		spr\u2015 spr_u;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.Length < this.ᜁ)
				{
					num = 5;
					continue;
				}
				spr_u = new spr\u2015();
				spr_u.ᜀ(this.ᜀ.ᜀ(A_0));
				num = 4;
				continue;
			case 1:
				return;
			case 2:
				if (true)
				{
				}
				num = 0;
				continue;
			case 4:
				if (spr_u.ᜀ() == -1)
				{
					num = 1;
					continue;
				}
				goto IL_BB;
			case 5:
				goto IL_70;
			}
			goto IL_28;
			IL_2B:
			num = 2;
			continue;
			IL_28:
			if (A_1 != null)
			{
				goto IL_2B;
			}
			IL_70:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_2B;
			default:
				goto IL_90;
			}
		}
		return;
		IL_90:
		if (false)
		{
		}
		return;
		IL_BB:
		spr_u.ᜀ(A_1);
		this.ᜂ.Add(spr_u);
	}

	// Token: 0x06003213 RID: 12819 RVA: 0x002E2A50 File Offset: 0x002E1A50
	internal void ᜀ(bool A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_12D:
				if (!A_0)
				{
					goto IL_158;
				}
				num = 0;
				break;
			case 1:
				goto IL_2E;
			default:
				goto IL_2E;
			}
			int num2;
			spr\u2015[] array;
			int num4;
			int num5;
			for (;;)
			{
				IL_36:
				switch (num)
				{
				case 0:
				{
					spr\u2015 spr_u;
					this.ᜀ.ᜁ(spr_u.ᜀ());
					num = 9;
					continue;
				}
				case 1:
					goto IL_81;
				case 2:
					goto IL_81;
				case 3:
				{
					if (num2 >= array.Length)
					{
						num = 4;
						continue;
					}
					spr\u2015 spr_u = array[num2];
					int num3 = spr_u.ᜁ().Length;
					num = 6;
					continue;
				}
				case 4:
					return;
				case 5:
					goto IL_F0;
				case 6:
					goto IL_12D;
				case 7:
					goto IL_F0;
				case 8:
				{
					int num3;
					num4 += num3 - 1;
					num2++;
					num = 5;
					continue;
				}
				case 9:
					goto IL_EE;
				case 10:
				{
					if (true)
					{
					}
					int num3;
					if (num5 >= num3)
					{
						num = 8;
						continue;
					}
					spr\u2015 spr_u;
					this.ᜀ.ᜀ(spr_u.ᜀ() + num4, spr_u.ᜁ()[num5]);
					num5++;
					num = 1;
					continue;
				}
				}
				goto IL_69;
				IL_81:
				num = 10;
				continue;
				IL_F0:
				num = 3;
			}
			IL_EE:
			goto IL_158;
			IL_2E:
			if (false)
			{
			}
			IL_69:
			num4 = 0;
			array = this.ᜀ();
			num2 = 0;
			num = 7;
			goto IL_36;
			IL_158:
			num5 = 0;
			num = 2;
			goto IL_36;
		}
		}
	}

	// Token: 0x06003214 RID: 12820 RVA: 0x002E2BC8 File Offset: 0x002E1BC8
	private spr\u2015[] ᜀ()
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
		this.ᜂ.Sort();
		return (spr\u2015[])this.ᜂ.ToArray(typeof(spr\u2015));
	}

	// Token: 0x04002745 RID: 10053
	private readonly spr\u24A6 ᜀ;

	// Token: 0x04002746 RID: 10054
	private readonly int ᜁ;

	// Token: 0x04002747 RID: 10055
	private readonly ArrayList ᜂ;
}
