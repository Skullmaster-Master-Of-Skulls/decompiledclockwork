using System;
using System.Drawing;

// Token: 0x020003CD RID: 973
internal class spr\u23E4 : spr\u1B7C
{
	// Token: 0x060036DC RID: 14044 RVA: 0x00337544 File Offset: 0x00336544
	internal spr\u23E4(PointF A_0, PointF A_1) : base(A_0, A_1)
	{
		this.ᜀ = A_0;
		this.ᜁ = A_1;
		this.ᜂ = sprὍ.ᜁ(this.ᜀ, this.ᜁ);
	}

	// Token: 0x060036DD RID: 14045 RVA: 0x00337580 File Offset: 0x00336580
	internal float ᜁ()
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
		return sprὍ.ᜁ(this.ᜀ, this.ᜁ);
	}

	// Token: 0x060036DE RID: 14046 RVA: 0x003375CC File Offset: 0x003365CC
	internal PointF ᜂ()
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

	// Token: 0x060036DF RID: 14047 RVA: 0x00337610 File Offset: 0x00336610
	internal PointF ᜀ()
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
		return this.ᜁ;
	}

	// Token: 0x060036E0 RID: 14048 RVA: 0x00337654 File Offset: 0x00336654
	internal bool ᜀ(PointF A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (base.ᜁ(A_0))
					{
						float num2 = sprὍ.ᜁ(this.ᜀ, A_0);
						num = 6;
						continue;
					}
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_FD;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 2:
					num = 3;
					continue;
				case 3:
					if (sprὍ.ᜀ(this.ᜁ, A_0))
					{
						goto IL_FD;
					}
					num = 1;
					continue;
				case 4:
					return false;
				case 5:
					if (base.ᜃ())
					{
						num = 9;
						continue;
					}
					goto IL_17C;
				case 6:
				{
					float num2;
					if (num2 > this.ᜂ)
					{
						num = 8;
						continue;
					}
					num = 5;
					continue;
				}
				case 7:
					goto IL_109;
				case 8:
					return false;
				case 9:
					goto IL_12B;
				}
				if (!sprὍ.ᜀ(this.ᜀ, A_0))
				{
					num = 2;
					continue;
				}
				return true;
				IL_FD:
				num = 7;
			}
			return false;
			IL_109:
			return true;
			IL_12B:
			return this.ᜁ.Y - this.ᜀ.Y > 0f == A_0.Y - this.ᜀ.Y > 0f;
			IL_17C:
			return this.ᜁ.X - this.ᜀ.X > 0f == A_0.X - this.ᜀ.X > 0f;
		}
		}
	}

	// Token: 0x060036E1 RID: 14049 RVA: 0x00337824 File Offset: 0x00336824
	internal PointF ᜀ(float A_0)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			PointF[] array;
			for (;;)
			{
				float num = this.ᜁ();
				int num2 = 7;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						num2 = 3;
						continue;
					case 1:
					{
						PointF pointF;
						if (pointF.X <= this.ᜀ().X)
						{
							num2 = 10;
							continue;
						}
						goto IL_200;
					}
					case 2:
					{
						PointF pointF = this.ᜂ();
						num2 = 1;
						continue;
					}
					case 3:
						if (A_0 > num)
						{
							num2 = 15;
							continue;
						}
						num2 = 9;
						continue;
					case 4:
						goto IL_200;
					case 5:
					{
						PointF pointF2;
						if (pointF2.Y > this.ᜀ().Y)
						{
							goto IL_ED;
						}
						goto IL_12F;
					}
					case 6:
					{
						PointF pointF3;
						if (pointF3.X >= this.ᜀ().X)
						{
							num2 = 20;
							continue;
						}
						goto IL_200;
					}
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_ED;
						default:
							if (false)
							{
							}
							if (A_0 >= 0f)
							{
								num2 = 0;
								continue;
							}
							goto IL_168;
						}
						break;
					case 8:
						goto IL_1F5;
					case 9:
						if (A_0 == 0f)
						{
							num2 = 8;
							continue;
						}
						num2 = 12;
						continue;
					case 10:
						goto IL_FE;
					case 11:
						if (base.ᜃ())
						{
							num2 = 18;
							continue;
						}
						goto IL_12F;
					case 12:
						if (sprὍ.ᜀ(A_0, num))
						{
							num2 = 14;
							continue;
						}
						flag = false;
						num2 = 13;
						continue;
					case 13:
						if (base.ᜄ() >= 0f)
						{
							num2 = 2;
							continue;
						}
						goto IL_FE;
					case 14:
						goto IL_264;
					case 15:
						goto IL_23E;
					case 16:
						goto IL_158;
					case 17:
					{
						PointF pointF3 = this.ᜂ();
						num2 = 6;
						continue;
					}
					case 18:
					{
						PointF pointF2 = this.ᜂ();
						num2 = 5;
						continue;
					}
					case 19:
						goto IL_12F;
					case 20:
						goto IL_16E;
					case 21:
						if (base.ᜄ() < 0f)
						{
							num2 = 17;
							continue;
						}
						goto IL_16E;
					}
					break;
					IL_ED:
					num2 = 4;
					continue;
					IL_FE:
					num2 = 21;
					continue;
					IL_12F:
					array = new PointF[]
					{
						PointF.Empty
					};
					num2 = 16;
					continue;
					IL_16E:
					num2 = 11;
					continue;
					IL_200:
					flag = true;
					num2 = 19;
				}
			}
			IL_158:
			base.ᜀ(this.ᜂ(), A_0 * (float)(flag ? -1 : 1), array);
			return array[0];
			IL_168:
			return PointF.Empty;
			IL_1F5:
			return this.ᜂ();
			IL_23E:
			goto IL_168;
			IL_264:
			if (true)
			{
			}
			return this.ᜀ();
		}
		}
	}

	// Token: 0x060036E2 RID: 14050 RVA: 0x00337B1C File Offset: 0x00336B1C
	internal static bool ᜀ(spr\u23E4 A_0, spr\u23E4 A_1, PointF[] A_2, bool A_3)
	{
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.ᜀ(A_2[0]))
				{
					num = 3;
					continue;
				}
				return false;
			case 1:
				if (A_0.ᜀ(A_2[0]))
				{
					num = 4;
					continue;
				}
				return false;
			case 2:
				return false;
			case 3:
				return true;
			case 4:
				num = 0;
				continue;
			}
			if (!spr\u1B7C.ᜀ(A_0, A_1, A_2, A_3))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					return true;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
			else
			{
				num = 1;
			}
		}
		return false;
	}

	// Token: 0x060036E3 RID: 14051 RVA: 0x00337BF0 File Offset: 0x00336BF0
	internal static bool ᜀ(spr\u23E4 A_0, spr\u1B7C A_1, PointF[] A_2, bool A_3)
	{
		for (;;)
		{
			IL_00:
			int num = 3;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_00;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						return false;
					case 1:
						if (A_0.ᜀ(A_2[0]))
						{
							num = 2;
							continue;
						}
						return false;
					case 2:
						return true;
					}
					if (!spr\u1B7C.ᜀ(A_0, A_1, A_2, A_3))
					{
						num = 0;
					}
					else
					{
						num = 1;
					}
					break;
				}
			}
		}
		return false;
	}

	// Token: 0x060036E4 RID: 14052 RVA: 0x00337C8C File Offset: 0x00336C8C
	internal static bool ᜀ(spr\u23E4 A_0, spr\u23E4 A_1, PointF[] A_2)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (A_1.ᜀ(A_2[0]))
				{
					num = 4;
					continue;
				}
				return false;
			case 2:
				num = 0;
				continue;
			case 3:
				if (A_0.ᜀ(A_2[0]))
				{
					num = 2;
					continue;
				}
				return false;
			case 4:
				goto IL_86;
			case 5:
				return false;
			}
			if (!spr\u1B7C.ᜀ(A_0, A_1, A_2))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_86;
				default:
					if (false)
					{
					}
					num = 5;
					break;
				}
			}
			else
			{
				num = 3;
			}
		}
		return false;
		IL_86:
		if (true)
		{
		}
		return true;
	}

	// Token: 0x060036E5 RID: 14053 RVA: 0x00337D5C File Offset: 0x00336D5C
	internal static bool ᜀ(spr\u23E4 A_0, spr\u23E4 A_1)
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
		return spr\u23E4.ᜀ(A_0, A_1, new PointF[]
		{
			PointF.Empty
		});
	}

	// Token: 0x040029D4 RID: 10708
	private new readonly PointF ᜀ;

	// Token: 0x040029D5 RID: 10709
	private new readonly PointF ᜁ;

	// Token: 0x040029D6 RID: 10710
	private new readonly float ᜂ;
}
