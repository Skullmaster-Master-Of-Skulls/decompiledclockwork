using System;
using System.Collections;
using System.Drawing;

// Token: 0x02000375 RID: 885
internal class sprṽ : sprᢿ
{
	// Token: 0x060031AD RID: 12717 RVA: 0x002DF9E4 File Offset: 0x002DE9E4
	internal static spr\u1D3C ᜅ(spr\u1B70 A_0)
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
		sprṽ sprṽ = new sprṽ();
		return sprṽ.ᜄ(A_0);
	}

	// Token: 0x060031AE RID: 12718 RVA: 0x002DFA2C File Offset: 0x002DEA2C
	public override void ᜀ(spr\u1926 A_0)
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
		this.ᜁ = new sprᲨ();
		this.ᜀ = new ArrayList();
	}

	// Token: 0x060031AF RID: 12719 RVA: 0x002DFA80 File Offset: 0x002DEA80
	public override void ᜁ(spr\u1926 A_0)
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
		this.ᜃ();
		this.ᜂ.ᜂ(this.ᜁ);
	}

	// Token: 0x060031B0 RID: 12720 RVA: 0x002DFAD4 File Offset: 0x002DEAD4
	public override void ᜀ(sprᴎ A_0)
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
		this.ᜁ.ᜀ(A_0.ᜀ(), true);
	}

	// Token: 0x060031B1 RID: 12721 RVA: 0x002DFB24 File Offset: 0x002DEB24
	public override void ᜀ(spr\u17F0 A_0)
	{
		spr\u187D[] array;
		for (;;)
		{
			array = A_0.ᜀ().ᜁ();
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					if (num >= array.Length)
					{
						num2 = 2;
						continue;
					}
					this.ᜁ.ᜁ(array[num].ᜁ());
					this.ᜁ.ᜁ(array[num].ᜂ());
					this.ᜀ.Add(this.ᜁ.ᜅ() - 1);
					num++;
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_33;
					default:
						if (false)
						{
						}
						num2 = 3;
						continue;
					}
					break;
				case 1:
					goto IL_33;
				case 2:
					goto IL_49;
				case 3:
					goto IL_33;
				}
				break;
				IL_33:
				num2 = 0;
			}
		}
		IL_49:
		this.ᜁ.ᜁ(array[array.Length - 1].ᜀ());
	}

	// Token: 0x060031B2 RID: 12722 RVA: 0x002DFC28 File Offset: 0x002DEC28
	private spr\u1D3C ᜄ(spr\u1B70 A_0)
	{
		for (;;)
		{
			this.ᜂ = new spr\u1D3C();
			spr\u1B70 spr_u1B = this.ᜃ(A_0);
			int num = 6;
			for (;;)
			{
				sprᲨ sprᲨ;
				sprᲨ sprᲨ2;
				switch (num)
				{
				case 0:
					goto IL_D2;
				case 1:
					this.ᜂ.ᜀ(A_0.ᜂ());
					num = 13;
					continue;
				case 2:
				{
					spr\u1D3C spr_u1D3C = sprṽ.ᜅ(A_0.ᜇ());
					num = 11;
					continue;
				}
				case 3:
					sprᲨ = null;
					goto IL_12C;
				case 4:
					if (!spr\u25FD.ᜀ(A_0.ᜂ(), null))
					{
						goto IL_1A7;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 5:
					num = 3;
					continue;
				case 6:
					if (!sprṽ.ᜁ(spr_u1B))
					{
						num = 8;
						continue;
					}
					if (true)
					{
					}
					spr_u1B.ᜀ(this);
					num = 12;
					continue;
				case 7:
					if (sprᲨ2 != null)
					{
						num = 9;
						continue;
					}
					goto IL_D2;
				case 8:
					goto IL_6B;
				case 9:
					this.ᜂ.ᜁ(sprᲨ2);
					num = 0;
					continue;
				case 10:
				{
					spr\u1D3C spr_u1D3C;
					sprᲨ = spr_u1D3C.ᜀ(0);
					goto IL_12C;
				}
				case 11:
				{
					spr\u1D3C spr_u1D3C;
					if (spr_u1D3C.ᜀ() <= 0)
					{
						num = 5;
						continue;
					}
					num = 10;
					continue;
				}
				case 12:
					if (A_0.ᜇ() != null)
					{
						num = 2;
						continue;
					}
					goto IL_D2;
				case 13:
					goto IL_167;
				}
				break;
				IL_D2:
				num = 4;
				continue;
				IL_12C:
				sprᲨ2 = sprᲨ;
				num = 7;
			}
		}
		IL_6B:
		return new spr\u1D3C(new sprᲨ());
		IL_167:
		IL_1A7:
		this.ᜂ.ᜀ(true);
		return this.ᜂ;
	}

	// Token: 0x060031B3 RID: 12723 RVA: 0x002DFDF0 File Offset: 0x002DEDF0
	private spr\u1B70 ᜃ(spr\u1B70 A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 10;
			spr\u1B70 spr_u1B;
			spr\u1B70 spr_u1B2;
			for (;;)
			{
				bool flag;
				float num2;
				bool flag2;
				switch (num)
				{
				case 0:
					return A_0;
				case 1:
					num = 6;
					continue;
				case 2:
					if (flag)
					{
						num = 3;
						continue;
					}
					goto IL_20B;
				case 3:
					return spr_u1B;
				case 4:
					num2 = A_0.ᜆ().ᜀ();
					goto IL_189;
				case 5:
					if (true)
					{
					}
					num2 = 0.75f;
					goto IL_189;
				case 6:
					if (A_0.ᜆ().ᜀ() == 0f)
					{
						num = 8;
						continue;
					}
					goto IL_DA;
				case 7:
					flag2 = A_0.ᜁ();
					goto IL_1AF;
				case 8:
					return spr_u1B2;
				case 9:
					flag2 = true;
					goto IL_1AF;
				case 11:
					goto IL_196;
				case 12:
					num = 4;
					continue;
				case 13:
					if (A_0.ᜆ().ᜀ() != 0f)
					{
						num = 12;
						continue;
					}
					num = 5;
					continue;
				case 14:
					num = 7;
					continue;
				case 15:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_196;
					default:
						if (false)
						{
						}
						if (A_0.ᜅ() == null)
						{
							num = 14;
							continue;
						}
						num = 9;
						continue;
					}
					break;
				}
				if (A_0.ᜆ() == null)
				{
					num = 0;
					continue;
				}
				spr_u1B2 = this.ᜃ.ᜀ(A_0, false, false);
				spr_u1B2.ᜀ(new spr\u23F1(spr\u2262.ᜋ));
				spr_u1B2.ᜆ().ᜂ(1f);
				num = 15;
				continue;
				IL_DA:
				float num3;
				spr_u1B = sprὝ.ᜀ(spr_u1B2, -num3 * 0.5f);
				num = 2;
				continue;
				IL_196:
				if (flag)
				{
					num = 1;
					continue;
				}
				goto IL_DA;
				IL_189:
				num3 = num2;
				num = 11;
				continue;
				IL_1AF:
				flag = flag2;
				num = 13;
			}
			return A_0;
			IL_20B:
			return sprṽ.ᜀ(A_0.ᜆ().ᜀ(), spr_u1B2, spr_u1B);
		}
		}
	}

	// Token: 0x060031B4 RID: 12724 RVA: 0x002E001C File Offset: 0x002DF01C
	private static spr\u1B70 ᜀ(float A_0, spr\u1B70 A_1, spr\u1B70 A_2)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_198:
			goto IL_14F;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				goto IL_63;
			}
			break;
		}
		int num;
		int num3;
		spr\u1B70 spr_u1B;
		for (;;)
		{
			IL_2C:
			switch (num)
			{
			case 0:
			{
				int num2;
				spr\u1926 spr_u;
				if (num2 >= spr_u.ᜉ())
				{
					num = 1;
					continue;
				}
				spr\u1926 spr_u2;
				spr_u2.ᜀ(num2, spr_u.ᜀ(num2));
				num2++;
				num = 4;
				continue;
			}
			case 1:
				num = 7;
				continue;
			case 2:
				if (num3 == spr_u1B.ᜉ() - 1)
				{
					num = 5;
					continue;
				}
				A_2.ᜁ(spr_u1B.ᜀ(num3));
				num = 9;
				continue;
			case 3:
				if (num3 >= spr_u1B.ᜉ())
				{
					num = 6;
					continue;
				}
				if (true)
				{
				}
				num = 2;
				continue;
			case 4:
				goto IL_198;
			case 5:
			{
				spr\u1926 spr_u = (spr\u1926)spr_u1B.ᜀ(num3);
				spr\u1926 spr_u2 = (spr\u1926)A_2.ᜀ(0);
				int num2 = 0;
				num = 8;
				continue;
			}
			case 6:
				return A_2;
			case 7:
				goto IL_13D;
			case 8:
				goto IL_D8;
			case 9:
				goto IL_13D;
			case 10:
				goto IL_DA;
			case 11:
				goto IL_DA;
			}
			goto IL_63;
			IL_DA:
			num = 3;
			continue;
			IL_13D:
			num3++;
			num = 10;
		}
		IL_D8:
		goto IL_14F;
		IL_63:
		spr_u1B = sprὝ.ᜀ(A_1, A_0 * 0.5f);
		spr_u1B = spr\u2287.ᜁ(spr_u1B);
		num3 = 0;
		num = 11;
		goto IL_2C;
		IL_14F:
		num = 0;
		goto IL_2C;
	}

	// Token: 0x060031B5 RID: 12725 RVA: 0x002E01C4 File Offset: 0x002DF1C4
	private void ᜃ()
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
		this.ᜂ();
		this.ᜀ();
	}

	// Token: 0x060031B6 RID: 12726 RVA: 0x002E020C File Offset: 0x002DF20C
	private void ᜂ()
	{
		for (;;)
		{
			ArrayList arrayList = this.ᜁ();
			int num = 0;
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return;
				case 1:
					goto IL_2B;
				case 2:
					goto IL_2B;
				case 3:
					if (num >= arrayList.Count)
					{
						num2 = 0;
						continue;
					}
					this.ᜁ.ᜀ((int)arrayList[num] - num);
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2B;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num2 = 2;
						continue;
					}
					break;
				}
				break;
				IL_2B:
				num2 = 3;
			}
		}
	}

	// Token: 0x060031B7 RID: 12727 RVA: 0x002E02B8 File Offset: 0x002DF2B8
	private ArrayList ᜁ()
	{
		ArrayList arrayList;
		for (;;)
		{
			IL_00:
			for (;;)
			{
				arrayList = new ArrayList();
				int num = 0;
				int num2 = 5;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_41;
					case 1:
					{
						int num3;
						if (sprṽ.ᜀ(this.ᜁ.ᜃ(num3 - 1).ᜁ(), this.ᜁ.ᜃ(num3).ᜁ(), this.ᜁ.ᜃ(num3 + 1).ᜁ(), this.ᜁ.ᜂ()))
						{
							num2 = 4;
							continue;
						}
						goto IL_41;
					}
					case 2:
						return arrayList;
					case 3:
					{
						if (num >= this.ᜀ.Count)
						{
							num2 = 2;
							continue;
						}
						int num3 = (int)this.ᜀ[num];
						num2 = 1;
						continue;
					}
					case 4:
					{
						int num3;
						arrayList.Add(num3);
						num2 = 0;
						continue;
					}
					case 5:
						goto IL_E2;
					case 6:
						goto IL_E2;
					}
					break;
					IL_41:
					num++;
					num2 = 6;
					continue;
					IL_E2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					}
					if (false)
					{
					}
					num2 = 3;
				}
			}
		}
		return arrayList;
	}

	// Token: 0x060031B8 RID: 12728 RVA: 0x002E03F8 File Offset: 0x002DF3F8
	private static bool ᜀ(PointF A_0, PointF A_1, PointF A_2, bool A_3)
	{
		bool flag;
		bool flag2;
		for (;;)
		{
			spr\u1B7C spr_u1B7C = new spr\u1B7C(A_0, A_2);
			flag = !spr_u1B7C.ᜂ(A_1);
			flag2 = (A_0.X - A_2.X > 0f);
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 4;
					continue;
				case 1:
					goto IL_8C;
				case 2:
					goto IL_E8;
				case 3:
					goto IL_CF;
				case 4:
					if (!flag)
					{
						num = 3;
						continue;
					}
					return true;
				case 5:
					if (!flag2)
					{
						num = 2;
						continue;
					}
					return false;
				case 6:
					if (flag2)
					{
						num = 0;
						continue;
					}
					goto IL_CF;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_CF;
					default:
						if (false)
						{
						}
						if (A_3)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						num = 6;
						continue;
					}
					break;
				}
				break;
				IL_CF:
				num = 5;
			}
		}
		IL_8C:
		return flag ^ flag2;
		IL_E8:
		return !flag;
	}

	// Token: 0x060031B9 RID: 12729 RVA: 0x002E04FC File Offset: 0x002DF4FC
	private void ᜀ()
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
				IL_1CA:
				num = 15;
				break;
			default:
				if (false)
				{
				}
				num = 14;
				break;
			}
			ArrayList arrayList;
			int num3;
			for (;;)
			{
				int num2;
				int num4;
				switch (num)
				{
				case 0:
					return;
				case 1:
					arrayList.Add(num2);
					num = 4;
					continue;
				case 2:
					num = 11;
					continue;
				case 3:
					if (this.ᜁ.ᜅ() > 2)
					{
						num = 2;
						continue;
					}
					return;
				case 4:
					goto IL_204;
				case 5:
					return;
				case 6:
					if (sprὍ.ᜀ(this.ᜁ.ᜃ(num2 - 1).ᜁ(), this.ᜁ.ᜃ(num2).ᜁ()))
					{
						num = 1;
						continue;
					}
					goto IL_204;
				case 7:
					goto IL_242;
				case 8:
					if (true)
					{
					}
					num = 3;
					continue;
				case 9:
					if (num3 >= arrayList.Count)
					{
						num = 8;
						continue;
					}
					goto IL_1AD;
				case 10:
					if (num2 >= num4 - 1)
					{
						num = 12;
						continue;
					}
					num = 6;
					continue;
				case 11:
					if (sprὍ.ᜀ(this.ᜁ.ᜃ(0).ᜁ(), this.ᜁ.ᜃ(this.ᜁ.ᜅ() - 1).ᜁ()))
					{
						num = 16;
						continue;
					}
					return;
				case 12:
					num3 = 0;
					num = 17;
					continue;
				case 13:
					goto IL_242;
				case 15:
					goto IL_A3;
				case 16:
					this.ᜁ.ᜀ(this.ᜁ.ᜅ() - 1);
					num = 0;
					continue;
				case 17:
					goto IL_A3;
				}
				if (this.ᜁ.ᜅ() < 2)
				{
					num = 5;
					continue;
				}
				arrayList = new ArrayList();
				num4 = this.ᜁ.ᜅ();
				num2 = 1;
				num = 7;
				continue;
				IL_A3:
				num = 9;
				continue;
				IL_204:
				num2++;
				num = 13;
				continue;
				IL_242:
				num = 10;
			}
			return;
			IL_1AD:
			this.ᜁ.ᜀ((int)arrayList[num3] - num3);
			num3++;
			goto IL_1CA;
		}
		}
	}

	// Token: 0x060031BA RID: 12730 RVA: 0x002E078C File Offset: 0x002DF78C
	private static bool ᜁ(spr\u1B70 A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				return true;
			case 3:
				if (!sprṽ.ᜀ(A_0.ᜆ().ᜌ()))
				{
					goto IL_86;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			}
			if (A_0.ᜆ() == null)
			{
				goto IL_86;
			}
			if (true)
			{
			}
			num = 0;
		}
		return true;
		IL_86:
		return sprṽ.ᜀ(A_0.ᜅ());
	}

	// Token: 0x060031BB RID: 12731 RVA: 0x002E082C File Offset: 0x002DF82C
	private static bool ᜀ(sprᤕ A_0)
	{
		spr\u253E spr_u253E;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_7C:
			if (spr_u253E == null)
			{
				return true;
			}
			num = 2;
			break;
		default:
			if (false)
			{
			}
			num = 1;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				return false;
			case 2:
				goto IL_87;
			case 3:
				goto IL_7C;
			}
			if (A_0 == null)
			{
				if (true)
				{
				}
				num = 0;
			}
			else
			{
				spr_u253E = (A_0 as spr\u253E);
				num = 3;
			}
		}
		return false;
		IL_87:
		return spr_u253E.ᜀ().ᜁ() != 0;
	}

	// Token: 0x0400271A RID: 10010
	private new ArrayList ᜀ;

	// Token: 0x0400271B RID: 10011
	private new sprᲨ ᜁ;

	// Token: 0x0400271C RID: 10012
	private new spr\u1D3C ᜂ;

	// Token: 0x0400271D RID: 10013
	private readonly spr\u236B ᜃ = new spr\u236B();
}
