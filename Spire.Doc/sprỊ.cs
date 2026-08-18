using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x0200015F RID: 351
internal abstract class sprỊ : SortedIntegerList
{
	// Token: 0x060009B4 RID: 2484 RVA: 0x00081F58 File Offset: 0x00080F58
	public object ᜁ(int A_0)
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
		return base[A_0];
	}

	// Token: 0x060009B5 RID: 2485 RVA: 0x00081F9C File Offset: 0x00080F9C
	internal object ᜃ(int A_0)
	{
		for (;;)
		{
			object obj = this.ᜁ(A_0);
			if (obj == null)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			return obj;
		}
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜂ(A_0);
	}

	// Token: 0x060009B6 RID: 2486 RVA: 0x00081FF0 File Offset: 0x00080FF0
	public object ᜂ(int A_0)
	{
		int a_ = 12;
		sprỊ sprỊ;
		int num;
		for (;;)
		{
			sprỊ = this.ᜀ();
			num = sprỊ.IndexOfKey(A_0);
			if (num < 0)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				}
				break;
			}
			goto IL_60;
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new InvalidOperationException(ClipboardData.b("ⁱᅳݵ൷όཻ੽ꒃﮍﲏ뒓聯ﮝ肟쒡쮣풥袧쮩슫躭얯\udcb1\udfb3\ud8b5ힷ춹튻麽ꆿ뛁냃듅ꇇ꣉맋뫍뗏ﳑ", a_));
		IL_60:
		return sprỊ.GetByIndex(num);
	}

	// Token: 0x060009B7 RID: 2487 RVA: 0x00082064 File Offset: 0x00081064
	public void ᜁ(int A_0, object A_1)
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
		sprỊ.ᜁ(new object[]
		{
			A_0,
			A_1
		});
		base[A_0] = A_1;
		sprỊ.ᜀ(new object[]
		{
			this,
			this
		});
	}

	// Token: 0x060009B8 RID: 2488 RVA: 0x000820D8 File Offset: 0x000810D8
	public void ᜀ(int A_0, object A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_08;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜁ(A_0, A_1);
				num = 2;
				continue;
			case 2:
				return;
			}
			IL_1C:
			if (A_1 != null)
			{
				num = 0;
				continue;
			}
			break;
			IL_08:
			goto IL_1C;
		}
	}

	// Token: 0x060009B9 RID: 2489 RVA: 0x00082150 File Offset: 0x00081150
	internal void ᜀ(int A_0, string A_1)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
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
					this.ᜁ(A_0, A_1);
					num = 1;
					continue;
				}
				break;
			}
			IL_1C:
			if (true)
			{
			}
			if (spr\u1CC6.ᜋ(A_1))
			{
				num = 2;
				continue;
			}
			break;
			goto IL_1C;
		}
	}

	// Token: 0x060009BA RID: 2490 RVA: 0x000821CC File Offset: 0x000811CC
	protected void ᜀ(int A_0, object A_1, bool A_2)
	{
		while (A_2)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜁ(A_0, A_1);
			return;
		}
		base.Remove(A_0);
	}

	// Token: 0x060009BB RID: 2491 RVA: 0x0008221C File Offset: 0x0008121C
	internal void ᜁ(int A_0, sprỊ A_1)
	{
		for (;;)
		{
			IL_14:
			object obj;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_5F:
				base.Remove(A_0);
				A_1.ᜁ(A_0, obj);
				num = 1;
				break;
			default:
				if (false)
				{
				}
				obj = this.ᜁ(A_0);
				num = 0;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (obj != null)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					return;
				case 1:
					return;
				case 2:
					goto IL_5D;
				}
				goto IL_14;
			}
			IL_5D:
			goto IL_5F;
		}
	}

	// Token: 0x060009BC RID: 2492 RVA: 0x000822A4 File Offset: 0x000812A4
	internal void ᜀ(int A_0, sprỊ A_1)
	{
		object obj;
		for (;;)
		{
			obj = this.ᜁ(A_0);
			if (obj == null)
			{
				goto IL_44;
			}
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_33;
			}
		}
		IL_33:
		if (false)
		{
		}
		A_1.ᜁ(A_0, obj);
		return;
		IL_44:
		A_1.Remove(A_0);
	}

	// Token: 0x060009BD RID: 2493 RVA: 0x000822FC File Offset: 0x000812FC
	public virtual void ᜎ()
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
		base.Clear();
	}

	// Token: 0x060009BE RID: 2494
	protected abstract sprỊ ᜀ();

	// Token: 0x060009BF RID: 2495 RVA: 0x00082340 File Offset: 0x00081340
	internal virtual sprỊ \u170D()
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
				IL_EF:
				num = 2;
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				goto IL_67;
			}
			sprỊ sprỊ;
			int num2;
			for (;;)
			{
				IL_34:
				object value;
				int key;
				switch (num)
				{
				case 0:
				{
					spr\u21AE spr_u21AE;
					value = spr_u21AE.ᜁ();
					num = 3;
					continue;
				}
				case 1:
				{
					object byIndex;
					spr\u21AE spr_u21AE = (spr\u21AE)byIndex;
					num = 5;
					continue;
				}
				case 2:
					return sprỊ;
				case 3:
					goto IL_80;
				case 4:
				{
					if (num2 >= base.Count)
					{
						goto IL_EF;
					}
					key = base.GetKey(num2);
					object byIndex = base.GetByIndex(num2);
					num = 7;
					continue;
				}
				case 5:
				{
					spr\u21AE spr_u21AE;
					if (!spr_u21AE.ᜀ())
					{
						num = 0;
						continue;
					}
					goto IL_135;
				}
				case 6:
					goto IL_80;
				case 7:
				{
					object byIndex;
					if (byIndex is spr\u21AE)
					{
						num = 1;
						continue;
					}
					value = byIndex;
					num = 6;
					continue;
				}
				case 8:
					goto IL_DA;
				case 9:
					goto IL_135;
				case 10:
					goto IL_DA;
				}
				goto IL_67;
				IL_80:
				sprỊ[key] = value;
				num = 9;
				continue;
				IL_DA:
				num = 4;
				continue;
				IL_135:
				num2++;
				num = 8;
			}
			return sprỊ;
			IL_67:
			sprỊ = (sprỊ)base.CreateEmptyCopy();
			num2 = 0;
			num = 10;
			goto IL_34;
		}
		}
	}

	// Token: 0x060009C0 RID: 2496 RVA: 0x000824B0 File Offset: 0x000814B0
	internal void ᜀ(sprỊ A_0)
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
		this.ᜀ(A_0, null);
	}

	// Token: 0x060009C1 RID: 2497 RVA: 0x000824F4 File Offset: 0x000814F4
	internal void ᜀ(sprỊ A_0, bool A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
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
					sprỊ sprỊ = this.ᜀ();
					sprỊ.ᜀ(A_0);
					num = 1;
					continue;
				}
				}
				break;
			case 1:
				goto IL_6D;
			}
			IL_1C:
			if (true)
			{
			}
			if (A_1)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_1C;
		}
		IL_6D:
		this.ᜀ(A_0);
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00082578 File Offset: 0x00081578
	internal void ᜀ(sprỊ A_0, spr\u253F A_1)
	{
		int a_ = 2;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_B7;
			case 1:
				goto IL_B7;
			case 2:
				return;
			case 3:
			{
				int num2;
				if (num2 >= base.Count)
				{
					num = 2;
					continue;
				}
				int key = base.GetKey(num2);
				object byIndex = base.GetByIndex(num2);
				this.ᜀ(A_0, key, byIndex, A_1);
				num2++;
				num = 0;
				continue;
			}
			case 5:
				goto IL_60;
			}
			if (A_0 != null)
			{
				int num2 = 0;
				num = 1;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 5;
				continue;
			}
			IL_B7:
			num = 3;
		}
		IL_60:
		throw new ArgumentNullException(ClipboardData.b("౧ᥩᡫ⽭ѯٱٳյ", a_));
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x00082660 File Offset: 0x00081660
	private void ᜀ(sprỊ A_0, int A_1, object A_2, spr\u253F A_3)
	{
		switch (0)
		{
		default:
		{
			int num = 11;
			object obj;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9F;
				case 1:
					if (A_3 == null)
					{
						goto IL_19E;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11D;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 2:
					goto IL_9F;
				case 3:
					return;
				case 4:
				{
					spr\u1A8F a_ = (spr\u1A8F)A_0[A_1];
					spr\u21AE spr_u21AE;
					obj = ((spr\u1A8F)spr_u21AE).ᜀ(a_);
					num = 13;
					continue;
				}
				case 5:
					goto IL_9F;
				case 6:
				{
					spr\u21AE spr_u21AE;
					if (spr_u21AE is spr\u1A8F)
					{
						num = 4;
						continue;
					}
					obj = spr_u21AE.ᜁ();
					num = 0;
					continue;
				}
				case 7:
				{
					spr\u21AE spr_u21AE;
					if (spr_u21AE.ᜀ())
					{
						num = 3;
						continue;
					}
					num = 6;
					continue;
				}
				case 8:
					if (A_2 is spr\u1CDD)
					{
						num = 12;
						continue;
					}
					obj = A_2;
					num = 2;
					continue;
				case 9:
				{
					spr\u21AE spr_u21AE = (spr\u21AE)A_2;
					num = 7;
					continue;
				}
				case 10:
					goto IL_E2;
				case 12:
				{
					spr\u1CDD spr_u1CDD = (spr\u1CDD)A_2;
					obj = spr_u1CDD.ᜀ(A_0, A_1);
					goto IL_11D;
				}
				case 13:
					goto IL_9F;
				}
				if (A_2 is spr\u21AE)
				{
					num = 9;
					continue;
				}
				num = 8;
				continue;
				IL_9F:
				num = 1;
				continue;
				IL_11D:
				num = 5;
			}
			IL_E2:
			A_3.ᜀ(A_0, A_1, obj);
			return;
			IL_19E:
			A_0[A_1] = obj;
			return;
		}
		}
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00082814 File Offset: 0x00081814
	internal void ᜁ(sprỊ A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		this.ᜀ(A_0, -1);
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x00082858 File Offset: 0x00081858
	internal void ᜀ(sprỊ A_0, int A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			int num = 14;
			for (;;)
			{
				int num2;
				int key;
				object obj;
				switch (num)
				{
				case 0:
					if (num2 >= A_0.Count)
					{
						num = 12;
						continue;
					}
					key = A_0.GetKey(num2);
					goto IL_DE;
				case 1:
					goto IL_1CB;
				case 2:
					goto IL_FF;
				case 3:
				{
					obj = ((spr\u21AE)obj).ᜁ();
					object byIndex = A_0.GetByIndex(num2);
					sprỊ.ᜀ(obj, byIndex);
					num = 1;
					continue;
				}
				case 4:
					if (true)
					{
					}
					goto IL_FF;
				case 5:
				{
					object byIndex2;
					object byIndex3;
					if (byIndex2.Equals(byIndex3))
					{
						num = 10;
						continue;
					}
					sprỊ.ᜀ(byIndex3, byIndex2);
					num = 16;
					continue;
				}
				case 6:
				{
					int num3 = base.IndexOfKey(key);
					num = 8;
					continue;
				}
				case 7:
					goto IL_8A;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DE;
					default:
					{
						if (false)
						{
						}
						int num3;
						if (num3 >= 0)
						{
							num = 11;
							continue;
						}
						obj = this.ᜂ(key);
						num = 9;
						continue;
					}
					}
					break;
				case 9:
					if (obj is spr\u21AE)
					{
						num = 3;
						continue;
					}
					goto IL_1CB;
				case 10:
				{
					int num3;
					base.RemoveAt(num3);
					num = 4;
					continue;
				}
				case 11:
				{
					object byIndex2 = A_0.GetByIndex(num2);
					int num3;
					object byIndex3 = base.GetByIndex(num3);
					num = 5;
					continue;
				}
				case 12:
					return;
				case 13:
					goto IL_156;
				case 15:
					if (key != A_1)
					{
						num = 6;
						continue;
					}
					goto IL_FF;
				case 16:
					goto IL_FF;
				case 17:
					goto IL_156;
				}
				if (A_0 == null)
				{
					num = 7;
					continue;
				}
				num2 = 0;
				num = 13;
				continue;
				IL_DE:
				num = 15;
				continue;
				IL_FF:
				num2++;
				num = 17;
				continue;
				IL_156:
				num = 0;
				continue;
				IL_1CB:
				base[key] = obj;
				num = 2;
			}
			IL_8A:
			throw new ArgumentNullException(ClipboardData.b("๫཭ͯ᝱㕳ɵ౷ࡹཻ", a_));
		}
		}
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x00082AB4 File Offset: 0x00081AB4
	internal bool ᜀ(sprỊ A_0, int[] A_1)
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
		return sprỊ.ᜀ(this, A_0, A_1);
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x00082AF8 File Offset: 0x00081AF8
	internal bool ᜀ(int[] A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 5;
			for (;;)
			{
				switch (num2)
				{
				case 0:
				{
					object byIndex = base.GetByIndex(num);
					num2 = 1;
					continue;
				}
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_67;
					default:
					{
						if (false)
						{
						}
						object byIndex;
						if (byIndex is spr\u21AE)
						{
							num2 = 6;
							continue;
						}
						return true;
					}
					}
					break;
				case 2:
				{
					object byIndex;
					if (!((spr\u21AE)byIndex).ᜀ())
					{
						num2 = 8;
						continue;
					}
					goto IL_3C;
				}
				case 3:
				{
					if (num >= base.Count)
					{
						num2 = 7;
						continue;
					}
					int key = base.GetKey(num);
					num2 = 4;
					continue;
				}
				case 4:
				{
					int key;
					if (sprὊ.ᜀ(A_0, 0, A_0.Length, key) < 0)
					{
						num2 = 0;
						continue;
					}
					goto IL_3C;
				}
				case 5:
					goto IL_97;
				case 6:
					num2 = 2;
					continue;
				case 7:
					goto IL_C0;
				case 8:
					goto IL_67;
				case 9:
					goto IL_97;
				}
				break;
				IL_3C:
				num++;
				num2 = 9;
				continue;
				IL_97:
				num2 = 3;
			}
		}
		IL_67:
		return true;
		IL_C0:
		if (true)
		{
		}
		return false;
	}

	// Token: 0x060009C8 RID: 2504 RVA: 0x00082C1C File Offset: 0x00081C1C
	internal static bool ᜀ(sprỊ A_0, sprỊ A_1, int[] A_2)
	{
		switch (0)
		{
		default:
		{
			bool flag2;
			for (;;)
			{
				int num = 0;
				int num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 10;
				for (;;)
				{
					bool flag;
					bool flag3;
					bool flag4;
					bool flag5;
					bool flag6;
					switch (num5)
					{
					case 0:
						if (flag)
						{
							num5 = 4;
							continue;
						}
						num5 = 2;
						continue;
					case 1:
						return flag2;
					case 2:
						flag3 = false;
						goto IL_1F0;
					case 3:
						flag4 = A_0.GetByIndex(num++).Equals(A_1.GetByIndex(num2++));
						goto IL_E4;
					case 4:
						num5 = 5;
						continue;
					case 5:
						flag3 = flag5;
						goto IL_1F0;
					case 6:
						if (flag2)
						{
							num5 = 14;
							continue;
						}
						return flag2;
					case 7:
						if (true)
						{
						}
						num5 = 3;
						continue;
					case 8:
						flag4 = false;
						goto IL_E4;
					case 9:
						if (!flag6)
						{
							num5 = 1;
							continue;
						}
						goto IL_180;
					case 10:
						goto IL_180;
					case 11:
						num5 = 18;
						continue;
					case 12:
						if (num3 == num4)
						{
							num5 = 7;
							continue;
						}
						num5 = 8;
						continue;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A7;
						default:
							if (false)
							{
							}
							flag4 = false;
							goto IL_E4;
						}
						break;
					case 14:
						num5 = 9;
						continue;
					case 15:
						if (!flag6)
						{
							num5 = 11;
							continue;
						}
						num5 = 12;
						continue;
					case 16:
						num5 = 17;
						continue;
					case 17:
						flag4 = !flag5;
						goto IL_E4;
					case 18:
						if (!flag)
						{
							num5 = 16;
							continue;
						}
						goto IL_A7;
					}
					break;
					IL_A7:
					num5 = 13;
					continue;
					IL_E4:
					flag2 = flag4;
					num5 = 6;
					continue;
					IL_180:
					flag = sprỊ.ᜀ(A_0, ref num, ref num3, A_2);
					flag5 = sprỊ.ᜀ(A_1, ref num2, ref num4, A_2);
					num5 = 0;
					continue;
					IL_1F0:
					flag6 = flag3;
					num5 = 15;
				}
			}
			return flag2;
		}
		}
	}

	// Token: 0x060009C9 RID: 2505 RVA: 0x00082E44 File Offset: 0x00081E44
	private static bool ᜀ(sprỊ A_0, ref int A_1, ref int A_2, int[] A_3)
	{
		for (;;)
		{
			int count = A_0.Count;
			int num = 0;
			for (;;)
			{
				int a_;
				int num2;
				switch (num)
				{
				case 0:
					if (A_3 == null)
					{
						num = 7;
						continue;
					}
					num = 9;
					continue;
				case 1:
					if (sprὊ.ᜀ(A_3, 0, a_, A_2) < 0)
					{
						num = 10;
						continue;
					}
					A_1++;
					num = 5;
					continue;
				case 2:
					return false;
				case 3:
					if (A_1 >= count)
					{
						num = 2;
						continue;
					}
					A_2 = A_0.GetKey(A_1);
					num = 8;
					continue;
				case 4:
					num = 1;
					continue;
				case 5:
					goto IL_AB;
				case 6:
					if (true)
					{
					}
					num2 = 0;
					goto IL_11C;
				case 7:
					num = 6;
					continue;
				case 8:
					if (A_3 != null)
					{
						num = 4;
						continue;
					}
					return true;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return true;
					default:
						if (false)
						{
						}
						num2 = A_3.Length;
						goto IL_11C;
					}
					break;
				case 10:
					goto IL_7D;
				case 11:
					goto IL_AB;
				}
				break;
				IL_AB:
				num = 3;
				continue;
				IL_11C:
				a_ = num2;
				num = 11;
			}
		}
		return true;
		IL_7D:
		return true;
	}

	// Token: 0x060009CA RID: 2506 RVA: 0x00082F7C File Offset: 0x00081F7C
	private static void ᜀ(object A_0, object A_1)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				((spr\u1A8F)A_0).ᜁ((spr\u1A8F)A_1);
				if (true)
				{
				}
				num = 2;
				continue;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 2:
				return;
			}
			if (!(A_0 is spr\u1A8F))
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x060009CB RID: 2507 RVA: 0x00083000 File Offset: 0x00082000
	internal static void ᜁ(params object[] A_0)
	{
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x060009CC RID: 2508 RVA: 0x0008303C File Offset: 0x0008203C
	internal static void ᜀ(params object[] A_0)
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
	}
}
