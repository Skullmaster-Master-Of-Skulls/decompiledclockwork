using System;
using System.Reflection;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x02000115 RID: 277
[DefaultMember("Item")]
internal class spr\u2504 : spr\u2574
{
	// Token: 0x06000669 RID: 1641 RVA: 0x0003DD10 File Offset: 0x0003CD10
	public spr\u2504(spr\u219E A_0) : base(A_0)
	{
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0003DD24 File Offset: 0x0003CD24
	private void ᜀ(int A_0, ref int A_1, ref ushort A_2)
	{
		int a_ = 14;
		for (;;)
		{
			int num = 0;
			A_1 = A_0;
			int num2 = 13;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					num2 = 8;
					continue;
				case 1:
					goto IL_103;
				case 2:
					goto IL_8B;
				case 3:
					num = this.ᜀ(A_1).ᜃ();
					num2 = 5;
					continue;
				case 4:
					goto IL_71;
				case 5:
					goto IL_8B;
				case 6:
					if (A_1 < base.ᜌ())
					{
						num2 = 0;
						continue;
					}
					goto IL_71;
				case 7:
					if (A_1 < base.ᜌ())
					{
						num2 = 1;
						continue;
					}
					return;
				case 8:
					if ((int)A_2 + num >= 8224)
					{
						num2 = 4;
						continue;
					}
					A_2 += (ushort)num;
					A_1++;
					num2 = 14;
					continue;
				case 9:
					goto IL_8B;
				case 10:
					num = this.ᜀ(A_1).ᜃ();
					goto IL_17F;
				case 11:
					if (A_0 == A_1)
					{
						num2 = 12;
						continue;
					}
					return;
				case 12:
					num2 = 7;
					continue;
				case 13:
					if (A_1 < base.ᜌ())
					{
						num2 = 3;
						continue;
					}
					num = 0;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_17F;
					default:
						if (false)
						{
						}
						num2 = 2;
						continue;
					}
					break;
				case 14:
					if (A_1 < base.ᜌ())
					{
						num2 = 10;
						continue;
					}
					goto IL_8B;
				}
				break;
				IL_71:
				num2 = 11;
				continue;
				IL_8B:
				if (true)
				{
				}
				num2 = 6;
				continue;
				IL_17F:
				num2 = 9;
			}
		}
		IL_103:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("挩䈫堭儯帱崳刵眷䨹夻䰽ℿ㙁ⵃ⥅♇ᕉὋ㩍≏㭑㩓ㅕᑗ㭙⹛㥝՟", a_)));
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0003DEF4 File Offset: 0x0003CEF4
	public int ᜀ(sprặ A_0)
	{
		int num;
		for (;;)
		{
			num = base.ᜁ(A_0);
			A_0.ᜀ(num);
			if (true)
			{
			}
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					return num;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					case 1:
						goto IL_65;
					default:
						goto IL_65;
					}
					IL_78:
					num2 = 0;
					continue;
					IL_65:
					if (false)
					{
					}
					this.ᜀ.ᜀ(A_0);
					goto IL_78;
				case 2:
					if (this.ᜀ != null)
					{
						num2 = 1;
						continue;
					}
					return num;
				}
				break;
			}
		}
		return num;
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0003DF84 File Offset: 0x0003CF84
	public void ᜁ(int A_0, sprặ A_1)
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
		base.ᜁ(A_0, A_1);
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0003DFC8 File Offset: 0x0003CFC8
	public void ᜀ(spr\u2422 A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_27:
				int num = 8;
				spr\u2320 spr_u = A_0;
				int num2 = 0;
				int num3;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_A3:
					goto IL_54;
				default:
					if (false)
					{
					}
					num3 = 3;
					break;
				}
				for (;;)
				{
					IL_10:
					switch (num3)
					{
					case 0:
						return;
					case 1:
					{
						if ((long)num2 >= (long)((ulong)A_0.ᜀ()))
						{
							num3 = 0;
							continue;
						}
						sprᶀ a_ = new sprᶀ(true, ref spr_u, ref num);
						this.ᜀ(new sprặ(a_));
						num2++;
						if (true)
						{
						}
						num3 = 2;
						continue;
					}
					case 2:
						goto IL_A3;
					case 3:
						goto IL_52;
					}
					goto IL_27;
				}
				IL_52:
				IL_54:
				num3 = 1;
				goto IL_10;
			}
			return;
		}
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0003E084 File Offset: 0x0003D084
	public void ᜀ(sprḗ A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int a_ = (int)A_0.Position;
				spr\u1DCF a_2;
				a_2.ᜀ = 252;
				int num = 0;
				int num2 = 0;
				int num3 = 12;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_1BE;
					case 1:
						goto IL_186;
					case 2:
						if (num2 < base.ᜌ())
						{
							this.ᜀ(num2).ᜁ(num2);
							num += this.ᜀ(num2).ᜀ();
							num2++;
							num3 = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							num3 = 5;
							continue;
						}
						break;
					case 3:
						goto IL_C3;
					case 4:
					{
						int num4;
						if (num4 < base.ᜌ())
						{
							num3 = 7;
							continue;
						}
						goto IL_1BE;
					}
					case 5:
					{
						int num4 = 0;
						int num5 = 0;
						a_2.ᜁ = 8;
						this.ᜀ(num4, ref num5, ref a_2.ᜁ);
						byte[] array = spr\u1DCF.ᜀ(a_2);
						A_0.ᜁ(array, array.Length);
						array = BitConverter.GetBytes(num);
						A_0.ᜁ(array, array.Length);
						array = BitConverter.GetBytes(base.ᜌ());
						A_0.ᜁ(array, array.Length);
						num3 = 13;
						continue;
					}
					case 6:
					{
						int num4;
						if (num4 >= base.ᜌ())
						{
							num3 = 10;
							continue;
						}
						int num6 = num4;
						num3 = 11;
						continue;
					}
					case 7:
					{
						a_ = (int)A_0.Position;
						a_2.ᜀ = 60;
						a_2.ᜁ = 0;
						int num4;
						int num5;
						this.ᜀ(num4, ref num5, ref a_2.ᜁ);
						byte[] array = spr\u1DCF.ᜀ(a_2);
						A_0.ᜁ(array, array.Length);
						num3 = 0;
						continue;
					}
					case 8:
					{
						if (true)
						{
						}
						int num5;
						int num6;
						if (num6 >= num5)
						{
							num3 = 9;
							continue;
						}
						this.ᜀ(num6).ᜀ(A_0, a_);
						num6++;
						num3 = 1;
						continue;
					}
					case 9:
					{
						int num5;
						int num4 = num5;
						num3 = 4;
						continue;
					}
					case 10:
						return;
					case 11:
						goto IL_186;
					case 12:
						goto IL_C3;
					case 13:
						goto IL_1BE;
					}
					break;
					IL_C3:
					num3 = 2;
					continue;
					IL_186:
					num3 = 8;
					continue;
					IL_1BE:
					num3 = 6;
				}
			}
			return;
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0003E2F0 File Offset: 0x0003D2F0
	public int ᜀ(string A_0)
	{
		int result;
		for (;;)
		{
			sprᶀ a_ = new sprᶀ(true, A_0);
			result = 0;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					a_ = null;
					num = 2;
					continue;
				case 1:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return result;
					default:
					{
						if (false)
						{
						}
						if (this.ᜀ.ᜀ(a_, ref result))
						{
							num = 0;
							continue;
						}
						sprặ a_2 = new sprặ(a_);
						result = this.ᜀ(a_2);
						num = 3;
						continue;
					}
					}
					break;
				case 2:
					return result;
				case 3:
					return result;
				}
				break;
			}
		}
		return result;
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0003E398 File Offset: 0x0003D398
	public void ᜃ()
	{
		for (;;)
		{
			int num = base.ᜌ() - 1;
			int num2 = 0;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_A8;
				case 1:
					goto IL_A8;
				case 2:
					base.ᜂ(num);
					num2 = 3;
					continue;
				case 3:
					goto IL_37;
				case 4:
					if (num < 0)
					{
						num2 = 6;
						continue;
					}
					num2 = 5;
					continue;
				case 5:
					if (this.ᜀ(num).ᜀ() <= 0)
					{
						num2 = 2;
						continue;
					}
					goto IL_37;
				case 6:
					return;
				}
				break;
				IL_37:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num--;
					num2 = 1;
					continue;
				}
				IL_A8:
				num2 = 4;
			}
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0003E46C File Offset: 0x0003D46C
	public int ᜂ()
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				num = 8;
				ushort num2 = 0;
				int num3 = 0;
				int num4 = 0;
				int num5 = 2;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_56;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (num4 < base.ᜌ())
							{
								num5 = 5;
								continue;
							}
							goto IL_49;
						}
						break;
					case 1:
						goto IL_C9;
					case 2:
						goto IL_C9;
					case 3:
						goto IL_49;
					case 4:
						goto IL_EA;
					case 5:
						goto IL_56;
					case 6:
						if (num3 >= base.ᜌ())
						{
							num5 = 4;
							continue;
						}
						this.ᜀ(num3, ref num4, ref num2);
						num3 = num4;
						num += (int)num2;
						num5 = 0;
						continue;
					}
					break;
					IL_49:
					num2 = 0;
					num5 = 1;
					continue;
					IL_56:
					num += sizeof(spr\u1DCF);
					num5 = 3;
					continue;
					IL_C9:
					num5 = 6;
				}
			}
			IL_EA:
			return num + sizeof(spr\u1DCF);
		}
		}
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0003E570 File Offset: 0x0003D570
	public new sprặ ᜀ(int A_0)
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
		return base.ᜀ(A_0) as sprặ;
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0003E5B8 File Offset: 0x0003D5B8
	public void ᜀ(int A_0, sprặ A_1)
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
		base.ᜀ(A_0, A_1);
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0003E5FC File Offset: 0x0003D5FC
	public sprḉ ᜁ()
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
		return this.ᜀ;
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0003E640 File Offset: 0x0003D640
	public void ᜀ(sprḉ A_0)
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
		this.ᜀ = A_0;
	}

	// Token: 0x040005AD RID: 1453
	private new sprḉ ᜀ;
}
