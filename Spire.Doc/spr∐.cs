using System;
using System.Collections;
using Spire.CompoundFile.Doc;

// Token: 0x0200043B RID: 1083
internal class spr\u2210
{
	// Token: 0x06003C5F RID: 15455 RVA: 0x00386000 File Offset: 0x00385000
	internal spr\u2210(double A_0, double A_1, double A_2)
	{
		this.ᜂ = new double[3];
		this.ᜂ[0] = A_2;
		this.ᜂ[1] = A_1;
		this.ᜂ[2] = A_0;
	}

	// Token: 0x06003C60 RID: 15456 RVA: 0x0038603C File Offset: 0x0038503C
	internal spr\u2210(double A_0, double A_1, double A_2, double A_3)
	{
		this.ᜂ = new double[4];
		this.ᜂ[0] = A_3;
		this.ᜂ[1] = A_2;
		this.ᜂ[2] = A_1;
		this.ᜂ[3] = A_0;
	}

	// Token: 0x06003C61 RID: 15457 RVA: 0x00386080 File Offset: 0x00385080
	internal spr\u2210(double A_0, double A_1, double A_2, double A_3, double A_4)
	{
		this.ᜂ = new double[5];
		this.ᜂ[0] = A_4;
		this.ᜂ[1] = A_3;
		this.ᜂ[2] = A_2;
		this.ᜂ[3] = A_1;
		this.ᜂ[4] = A_0;
	}

	// Token: 0x06003C62 RID: 15458 RVA: 0x003860D0 File Offset: 0x003850D0
	internal double[] ᜄ()
	{
		int a_ = 0;
		for (;;)
		{
			int num = this.ᜃ();
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_AD;
			default:
			{
				if (true)
				{
				}
				if (false)
				{
				}
				int num2 = 2;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_AD;
					case 1:
						num2 = 0;
						continue;
					case 2:
						switch (num)
						{
						case 0:
							goto IL_B6;
						case 1:
							goto IL_7B;
						case 2:
							goto IL_BD;
						case 3:
							goto IL_74;
						case 4:
							goto IL_AF;
						default:
							num2 = 1;
							continue;
						}
						break;
					}
					break;
				}
				break;
			}
			}
		}
		IL_74:
		return this.ᜁ();
		IL_7B:
		return new double[]
		{
			-1.0 * this.ᜂ[0] / this.ᜂ[1]
		};
		IL_AD:
		throw new NotSupportedException(ClipboardData.b("⍥ṧ୩k᭭ᅯٱᵳᡵί婹๻ᅽꚅ겋揄뚕ﲗﾙﮛ얟잡蒣쾥\udba7誩슫솭쒯銱잳쎵좷쪹펻첽뒿ꟁꃃ", a_));
		IL_AF:
		return this.ᜀ();
		IL_B6:
		return new double[0];
		IL_BD:
		return this.ᜂ();
	}

	// Token: 0x06003C63 RID: 15459 RVA: 0x003861C0 File Offset: 0x003851C0
	internal int ᜃ()
	{
		int num2;
		for (;;)
		{
			int num = this.ᜂ.Length - 1;
			num2 = num;
			int num3 = 4;
			for (;;)
			{
				switch (num3)
				{
				case 0:
					if (true)
					{
					}
					goto IL_99;
				case 1:
					if (Math.Abs(this.ᜂ[num2]) < 9.999999974752427E-07)
					{
						num3 = 6;
						continue;
					}
					return num2;
				case 2:
					return 0;
				case 3:
					if (num2 <= -1)
					{
						num3 = 2;
						continue;
					}
					num3 = 1;
					continue;
				case 4:
					goto IL_99;
				case 5:
					goto IL_DB;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DB;
					default:
						if (false)
						{
						}
						this.ᜂ[num2] = 0.0;
						num3 = 5;
						continue;
					}
					break;
				}
				break;
				IL_99:
				num3 = 3;
				continue;
				IL_DB:
				num2--;
				num3 = 0;
			}
		}
		return num2;
	}

	// Token: 0x06003C64 RID: 15460 RVA: 0x003862B0 File Offset: 0x003852B0
	private double[] ᜂ()
	{
		switch (0)
		{
		default:
		{
			double[] result;
			for (;;)
			{
				result = new double[0];
				double num = this.ᜂ[2];
				double num2 = this.ᜂ[1] / num;
				double num3 = this.ᜂ[0] / num;
				double num4 = num2 * num2 - 4.0 * num3;
				int num5 = 1;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						result = new double[]
						{
							0.5 * -num2
						};
						num5 = 3;
						continue;
					case 1:
						if (num4 > 0.0)
						{
							num5 = 2;
							continue;
						}
						num5 = 4;
						continue;
					case 2:
					{
						double num6 = Math.Sqrt(num4);
						result = new double[]
						{
							0.5 * (-num2 + num6),
							0.5 * (-num2 - num6)
						};
						num5 = 5;
						continue;
					}
					case 3:
						goto IL_AE;
					case 4:
						if (num4 == 0.0)
						{
							num5 = 0;
							continue;
						}
						return result;
					case 5:
						goto IL_12A;
					}
					break;
				}
			}
			IL_AE:
			return result;
			IL_12A:
			if (true)
			{
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
				break;
			}
			return result;
		}
		}
	}

	// Token: 0x06003C65 RID: 15461 RVA: 0x00386410 File Offset: 0x00385410
	private double[] ᜁ()
	{
		switch (0)
		{
		default:
		{
			double[] result;
			for (;;)
			{
				double num = this.ᜂ[3];
				double num2 = this.ᜂ[2] / num;
				double num3 = this.ᜂ[1] / num;
				double num4 = this.ᜂ[0] / num;
				double num5 = (3.0 * num3 - num2 * num2) / 3.0;
				double num6 = (2.0 * num2 * num2 * num2 - 9.0 * num3 * num2 + 27.0 * num4) / 27.0;
				double num7 = num2 / 3.0;
				double num8 = num6 * num6 / 4.0 + num5 * num5 * num5 / 27.0;
				double num9 = num6 / 2.0;
				int num10 = 8;
				for (;;)
				{
					double num11;
					double num12;
					double num13;
					double num14;
					switch (num10)
					{
					case 0:
						num11 = -Math.Pow(num9, 0.3333333333333333);
						num10 = 10;
						continue;
					case 1:
						goto IL_206;
					case 2:
						if (num12 >= 0.0)
						{
							num10 = 4;
							continue;
						}
						num13 -= Math.Pow(-num12, 0.3333333333333333);
						num10 = 7;
						continue;
					case 3:
						if (num8 < 0.0)
						{
							num10 = 19;
							continue;
						}
						num10 = 5;
						continue;
					case 4:
						goto IL_44D;
					case 5:
						if (num9 >= 0.0)
						{
							num10 = 0;
							continue;
						}
						num11 = Math.Pow(-num9, 0.3333333333333333);
						num10 = 13;
						continue;
					case 6:
						num14 = Math.Sqrt(num8);
						num12 = -num9 + num14;
						num10 = 14;
						continue;
					case 7:
						goto IL_206;
					case 8:
						if (Math.Abs(num8) <= 9.999999974752427E-07)
						{
							num10 = 16;
							continue;
						}
						goto IL_2B0;
					case 9:
						num13 = Math.Pow(num12, 0.3333333333333333);
						num10 = 17;
						continue;
					case 10:
						goto IL_30A;
					case 11:
						return result;
					case 12:
						goto IL_2B0;
					case 13:
						goto IL_30A;
					case 14:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_44D;
						default:
							if (false)
							{
							}
							if (num12 >= 0.0)
							{
								num10 = 9;
								continue;
							}
							num13 = -Math.Pow(-num12, 0.3333333333333333);
							num10 = 18;
							continue;
						}
						break;
					case 15:
						if (num8 > 0.0)
						{
							num10 = 6;
							continue;
						}
						num10 = 3;
						continue;
					case 16:
						num8 = 0.0;
						if (true)
						{
						}
						num10 = 12;
						continue;
					case 17:
						goto IL_420;
					case 18:
						goto IL_420;
					case 19:
					{
						double num15 = Math.Sqrt(-num5 / 3.0);
						double num16 = Math.Atan2(Math.Sqrt(-num8), -num9) / 3.0;
						double num17 = Math.Cos(num16);
						double num18 = Math.Sin(num16);
						double num19 = Math.Sqrt(3.0);
						result = new double[]
						{
							2.0 * num15 * num17 - num7,
							-num15 * (num17 + num19 * num18) - num7,
							-num15 * (num17 - num19 * num18) - num7
						};
						num10 = 11;
						continue;
					}
					case 20:
						return result;
					case 21:
						return result;
					}
					break;
					IL_206:
					result = new double[]
					{
						num13 - num7
					};
					num10 = 20;
					continue;
					IL_2B0:
					num10 = 15;
					continue;
					IL_30A:
					result = new double[]
					{
						2.0 * num11 - num7,
						-num11 - num7
					};
					num10 = 21;
					continue;
					IL_420:
					num12 = -num9 - num14;
					num10 = 2;
					continue;
					IL_44D:
					num13 += Math.Pow(num12, 0.3333333333333333);
					num10 = 1;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06003C66 RID: 15462 RVA: 0x00386898 File Offset: 0x00385898
	private double[] ᜀ()
	{
		switch (0)
		{
		default:
		{
			double[] result;
			for (;;)
			{
				result = new double[0];
				double num = this.ᜂ[4];
				double num2 = this.ᜂ[3] / num;
				double num3 = this.ᜂ[2] / num;
				double num4 = this.ᜂ[1] / num;
				double num5 = this.ᜂ[0] / num;
				double[] array = new spr\u2210(1.0, -1.0 * num3, num2 * num4 - 4.0 * num5, -1.0 * num2 * num2 * num5 + 4.0 * num3 * num5 - num4 * num4).ᜁ();
				double num6 = array[0];
				double num7 = num2 * num2 / 4.0 - num3 + num6;
				int num8 = 8;
				for (;;)
				{
					switch (num8)
					{
					case 0:
						if (num7 > 0.0)
						{
							goto IL_1D9;
						}
						num8 = 4;
						continue;
					case 1:
						return result;
					case 2:
						num7 = 0.0;
						if (true)
						{
						}
						num8 = 6;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1D9;
						default:
							if (false)
							{
							}
							result = spr\u2210.ᜀ(num2, num3, num4, num7);
							num8 = 5;
							continue;
						}
						break;
					case 4:
						if (num7 >= 0.0)
						{
							num8 = 7;
							continue;
						}
						return result;
					case 5:
						return result;
					case 6:
						goto IL_1BD;
					case 7:
						result = spr\u2210.ᜁ(num2, num3, num5, num6);
						num8 = 1;
						continue;
					case 8:
						if (Math.Abs(num7) <= 9.999999974752427E-07)
						{
							num8 = 2;
							continue;
						}
						goto IL_1BD;
					}
					break;
					IL_1BD:
					num8 = 0;
					continue;
					IL_1D9:
					num8 = 3;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06003C67 RID: 15463 RVA: 0x00386A90 File Offset: 0x00385A90
	private static double[] ᜁ(double A_0, double A_1, double A_2, double A_3)
	{
		switch (0)
		{
		default:
		{
			double[] array;
			for (;;)
			{
				array = new double[0];
				double num = A_3 * A_3 - 4.0 * A_2;
				int num2 = 9;
				for (;;)
				{
					int num3;
					double num5;
					int num7;
					switch (num2)
					{
					case 0:
						num2 = 8;
						continue;
					case 1:
						num3 = 2;
						goto IL_16E;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_16F;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							num = 0.0;
							num2 = 3;
							continue;
						}
						break;
					case 3:
						goto IL_F3;
					case 4:
					{
						double num4 = Math.Sqrt(num5 + num);
						int num6;
						array[num6++] = -A_0 / 4.0 + num4 / 2.0;
						array[num6++] = -A_0 / 4.0 - num4 / 2.0;
						num2 = 5;
						continue;
					}
					case 5:
						goto IL_22C;
					case 6:
					{
						num7 += ((num5 - num >= 9.999999974752427E-07) ? 2 : 0);
						array = new double[num7];
						int num6 = 0;
						num2 = 11;
						continue;
					}
					case 7:
						if (num5 - num >= 9.999999974752427E-07)
						{
							num2 = 14;
							continue;
						}
						return array;
					case 8:
						num3 = 0;
						goto IL_16E;
					case 9:
						if (num >= -9.999999974752427E-07)
						{
							num2 = 12;
							continue;
						}
						return array;
					case 10:
						if (num5 + num < 9.999999974752427E-07)
						{
							num2 = 0;
							continue;
						}
						num2 = 1;
						continue;
					case 11:
						if (num5 + num >= 9.999999974752427E-07)
						{
							num2 = 4;
							continue;
						}
						goto IL_22C;
					case 12:
						num2 = 13;
						continue;
					case 13:
						if (num < 0.0)
						{
							num2 = 2;
							continue;
						}
						goto IL_F3;
					case 14:
					{
						double num8 = Math.Sqrt(num5 - num);
						int num6;
						array[num6++] = -A_0 / 4.0 + num8 / 2.0;
						array[num6++] = -A_0 / 4.0 - num8 / 2.0;
						num2 = 15;
						continue;
					}
					case 15:
						return array;
					}
					break;
					IL_F3:
					num = 2.0 * Math.Sqrt(num);
					num5 = 3.0 * A_0 * A_0 / 4.0 - 2.0 * A_1;
					num2 = 10;
					continue;
					IL_16F:
					num2 = 6;
					continue;
					IL_16E:
					num7 = num3;
					goto IL_16F;
					IL_22C:
					num2 = 7;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06003C68 RID: 15464 RVA: 0x00386D50 File Offset: 0x00385D50
	private static void ᜀ(double A_0, double A_1, ArrayList A_2)
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
		A_2.Add(-A_0 / 4.0 + A_1 / 2.0);
		A_2.Add(-A_0 / 4.0 - A_1 / 2.0);
	}

	// Token: 0x06003C69 RID: 15465 RVA: 0x00386DD4 File Offset: 0x00385DD4
	private static double[] ᜀ(double A_0, double A_1, double A_2, double A_3)
	{
		switch (0)
		{
		default:
		{
			double[] array;
			for (;;)
			{
				double num = Math.Sqrt(A_3);
				double num2 = 3.0 * A_0 * A_0 / 4.0 - num * num - 2.0 * A_1;
				double num3 = (4.0 * A_0 * A_1 - 8.0 * A_2 - A_0 * A_0 * A_0) / (4.0 * num);
				double num4 = num2 + num3;
				double num5 = num2 - num3;
				int num6 = 16;
				for (;;)
				{
					int num9;
					int num10;
					switch (num6)
					{
					case 0:
						num5 = 0.0;
						num6 = 1;
						continue;
					case 1:
						goto IL_21B;
					case 2:
						num6 = 12;
						continue;
					case 3:
						if (num5 >= 0.0)
						{
							num6 = 11;
							continue;
						}
						return array;
					case 4:
						goto IL_154;
					case 5:
					{
						double num7 = Math.Sqrt(num4);
						int num8;
						array[num8++] = -A_0 / 4.0 + (num + num7) / 2.0;
						array[num8++] = -A_0 / 4.0 + (num - num7) / 2.0;
						num6 = 4;
						continue;
					}
					case 6:
						if (num4 < 0.0)
						{
							goto IL_154;
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
							num6 = 5;
							continue;
						}
						break;
					case 7:
						if (num4 < 0.0)
						{
							num6 = 2;
							continue;
						}
						num6 = 9;
						continue;
					case 8:
					{
						num9 += ((num5 >= 0.0) ? 2 : 0);
						array = new double[num9];
						int num8 = 0;
						num6 = 6;
						continue;
					}
					case 9:
						num10 = 2;
						goto IL_1E4;
					case 10:
						num4 = 0.0;
						num6 = 15;
						continue;
					case 11:
					{
						double num11 = Math.Sqrt(num5);
						int num8;
						array[num8++] = -A_0 / 4.0 + (-num - num11) / 2.0;
						array[num8++] = -A_0 / 4.0 + (num11 - num) / 2.0;
						num6 = 13;
						continue;
					}
					case 12:
						num10 = 0;
						goto IL_1E4;
					case 13:
						return array;
					case 14:
						if (Math.Abs(num5) <= 9.999999974752427E-07)
						{
							num6 = 0;
							continue;
						}
						goto IL_21B;
					case 15:
						goto IL_109;
					case 16:
						if (Math.Abs(num4) <= 9.999999974752427E-07)
						{
							num6 = 10;
							continue;
						}
						goto IL_109;
					}
					break;
					IL_109:
					num6 = 14;
					continue;
					IL_154:
					num6 = 3;
					continue;
					IL_1E4:
					num9 = num10;
					num6 = 8;
					continue;
					IL_21B:
					if (true)
					{
					}
					num6 = 7;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06003C6A RID: 15466 RVA: 0x00387108 File Offset: 0x00386108
	private static void ᜀ(double A_0, double A_1, double A_2, bool A_3, ArrayList A_4)
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
		A_4.Add(-A_0 / 4.0 + (A_3 ? (A_1 + A_2) : (-A_1 - A_2)) / 2.0);
		A_4.Add(-A_0 / 4.0 + (A_3 ? (A_1 - A_2) : (A_2 - A_1)) / 2.0);
	}

	// Token: 0x04002BE3 RID: 11235
	internal const float ᜀ = 1E-06f;

	// Token: 0x04002BE4 RID: 11236
	internal const float ᜁ = 6f;

	// Token: 0x04002BE5 RID: 11237
	private readonly double[] ᜂ;
}
