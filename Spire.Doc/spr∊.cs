using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Spire.Pdf.General.Paper.Drawing;

// Token: 0x020002F3 RID: 755
internal class spr\u220A : sprᢿ
{
	// Token: 0x0600296F RID: 10607 RVA: 0x0029333C File Offset: 0x0029233C
	internal static spr\u1B70[] ᜁ(spr\u1B70 A_0, spr\u200F A_1)
	{
		if (A_1 != null)
		{
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
				spr\u220A spr_u220A = new spr\u220A();
				return spr_u220A.ᜀ(A_0, A_1);
			}
			}
		}
		if (true)
		{
		}
		return null;
	}

	// Token: 0x06002970 RID: 10608 RVA: 0x0029338C File Offset: 0x0029238C
	private spr\u1B70[] ᜀ(spr\u1B70 A_0, spr\u200F A_1)
	{
		for (;;)
		{
			this.ᜏ = A_1.ᜁ();
			int num = 8;
			for (;;)
			{
				spr\u1926 spr_u;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_119;
				case 1:
					goto IL_128;
				case 2:
					if (true)
					{
					}
					num = 0;
					continue;
				case 3:
					goto IL_154;
				case 4:
					goto IL_299;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_159;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					break;
				case 6:
					if (!spr_u.ᜁ())
					{
						num = 5;
						continue;
					}
					goto IL_299;
				case 7:
					goto IL_159;
				case 8:
					if (this.ᜏ.ᜂ() == PsLineEndingCapType.FlatLineCap)
					{
						num = 14;
						continue;
					}
					goto IL_A2;
				case 9:
					goto IL_128;
				case 10:
					if (spr_u.ᜉ() != 0)
					{
						num = 7;
						continue;
					}
					goto IL_299;
				case 11:
					num = 15;
					continue;
				case 12:
					if (this.ᜏ.ᜃ().ᜃ() == PsLineEndType.None)
					{
						num = 11;
						continue;
					}
					goto IL_A2;
				case 13:
					if (num2 >= A_0.ᜉ())
					{
						num = 3;
						continue;
					}
					spr_u = (spr\u1926)A_0.ᜀ(num2);
					num = 6;
					continue;
				case 14:
					num = 12;
					continue;
				case 15:
					if (this.ᜏ.ᜀ().ᜃ() == PsLineEndType.None)
					{
						num = 2;
						continue;
					}
					goto IL_A2;
				case 16:
					this.\u1712.ᜆ().ᜂ((this.ᜏ.ᜄ() <= 0.75f) ? 0.75f : this.ᜏ.ᜄ());
					this.\u1712.ᜆ().ᜀ(DashStyle.Solid);
					this.\u1712.ᜆ().ᜀ(LineJoin.Miter);
					this.\u1712.ᜆ().ᜁ(LineCap.Round);
					this.\u1712.ᜆ().ᜀ(LineCap.Round);
					num2 = 0;
					num = 1;
					continue;
				}
				break;
				IL_A2:
				this.ᜑ = new spr\u1B70();
				this.ᜑ.ᜀ(A_0.ᜆ().ᜌ());
				this.\u1712 = new spr\u1B70(A_0.ᜆ().ᜈ());
				num = 16;
				continue;
				IL_128:
				num = 13;
				continue;
				IL_159:
				this.ᜐ = true;
				spr_u.ᜀ(0).ᜀ(this);
				this.ᜐ = false;
				spr_u.ᜀ(spr_u.ᜉ() - 1).ᜀ(this);
				num = 4;
				continue;
				IL_299:
				num2++;
				num = 9;
			}
		}
		IL_119:
		return null;
		IL_154:
		return new spr\u1B70[]
		{
			this.ᜑ,
			this.\u1712
		};
	}

	// Token: 0x06002971 RID: 10609 RVA: 0x00293660 File Offset: 0x00292660
	public override void ᜀ(sprᴎ A_0)
	{
		switch (0)
		{
		default:
		{
			float num3;
			PsLineEndSize a_;
			PsLineEndSize a_2;
			PointF a_3;
			for (;;)
			{
				PsLineEndType psLineEndType = this.ᜂ();
				PsLineEndSize psLineEndSize = this.ᜀ();
				PsLineEndSize psLineEndSize2 = this.ᜁ();
				PsLineEndType psLineEndType2 = psLineEndType;
				int num = 0;
				for (;;)
				{
					float num2;
					PointF pointF;
					PointF a_4;
					switch (num)
					{
					case 0:
						switch (psLineEndType2)
						{
						case PsLineEndType.Arrow:
						case PsLineEndType.Stealth:
							num2 = 1.5f * this.ᜏ.ᜄ() * spr\u220A.\u170D[(int)psLineEndSize2];
							num = 8;
							continue;
						case PsLineEndType.Diamond:
						case PsLineEndType.Oval:
							num2 = 0f;
							num = 17;
							continue;
						case PsLineEndType.Open:
						{
							SizeF sizeF = spr\u220A.ᜎ[(int)psLineEndSize][(int)psLineEndSize2];
							num2 = this.ᜏ.ᜄ() * sizeF.Height;
							num = 23;
							continue;
						}
						default:
							num = 21;
							continue;
						}
						break;
					case 1:
						num3 = 360f - num3;
						num = 16;
						continue;
					case 2:
						num = 19;
						continue;
					case 3:
						goto IL_2A6;
					case 4:
					{
						IL_23E:
						if (this.ᜐ)
						{
							num = 9;
							continue;
						}
						int num4 = A_0.ᜀ().Count - 1;
						int index = num4 - 1;
						a_ = this.ᜏ.ᜀ().ᜂ();
						a_2 = this.ᜏ.ᜀ().ᜀ();
						num = 6;
						continue;
					}
					case 5:
						if (pointF.Y != 0f)
						{
							num = 3;
							continue;
						}
						goto IL_41B;
					case 6:
						goto IL_1AB;
					case 7:
						num = 5;
						continue;
					case 8:
						goto IL_232;
					case 9:
					{
						int num4 = 0;
						int index = 1;
						a_ = this.ᜏ.ᜃ().ᜂ();
						a_2 = this.ᜏ.ᜃ().ᜀ();
						num = 22;
						continue;
					}
					case 10:
						num2 *= 2.5f;
						num = 15;
						continue;
					case 11:
						if (pointF.X < 0f)
						{
							num = 1;
							continue;
						}
						goto IL_41B;
					case 12:
						if (num2 > 0f)
						{
							num = 18;
							continue;
						}
						goto IL_325;
					case 13:
						goto IL_325;
					case 14:
						if (pointF.X == 0f)
						{
							num = 7;
							continue;
						}
						goto IL_2A6;
					case 15:
						goto IL_232;
					case 16:
						goto IL_22D;
					case 17:
						goto IL_232;
					case 18:
					{
						int num4;
						A_0.ᜀ()[num4] = spr\u220A.ᜀ(a_3, a_4, num2);
						num = 13;
						continue;
					}
					case 19:
						if (psLineEndSize == PsLineEndSize.Small)
						{
							num = 10;
							continue;
						}
						goto IL_37F;
					case 20:
						goto IL_232;
					case 21:
						return;
					case 22:
						goto IL_1AB;
					case 23:
						if (psLineEndSize2 == PsLineEndSize.Large)
						{
							num = 2;
							continue;
						}
						goto IL_37F;
					}
					break;
					IL_1AB:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_23E;
					default:
					{
						if (false)
						{
						}
						int num4;
						a_3 = (PointF)A_0.ᜀ()[num4];
						int index;
						a_4 = (PointF)A_0.ᜀ()[index];
						num = 12;
						continue;
					}
					}
					IL_232:
					num = 4;
					continue;
					IL_2A6:
					float num5 = -pointF.Y;
					float num6 = num5 / (float)Math.Sqrt((double)(pointF.X * pointF.X + pointF.Y * pointF.Y));
					num3 = (float)spr\u2109.ᜃ(Math.Acos((double)num6));
					num = 11;
					continue;
					IL_325:
					pointF = new PointF(a_3.X - a_4.X, a_3.Y - a_4.Y);
					num3 = 0f;
					num = 14;
					continue;
					IL_37F:
					num2 *= 1.3f;
					if (true)
					{
					}
					num = 20;
				}
			}
			return;
			IL_22D:
			IL_41B:
			this.ᜀ(a_3, num3, a_, a_2);
			return;
		}
		}
	}

	// Token: 0x06002972 RID: 10610 RVA: 0x00293A98 File Offset: 0x00292A98
	public override void ᜀ(spr\u17F0 A_0)
	{
		switch (0)
		{
		default:
		{
			PointF a_;
			float num3;
			PsLineEndSize a_5;
			PsLineEndSize a_6;
			for (;;)
			{
				IL_5B:
				PsLineEndType psLineEndType = this.ᜂ();
				PsLineEndType psLineEndType2 = psLineEndType;
				for (;;)
				{
					IL_65:
					int num = 6;
					for (;;)
					{
						PointF pointF;
						PointF a_2;
						switch (num)
						{
						case 0:
							goto IL_10E;
						case 1:
							if (pointF.X < 0f)
							{
								num = 10;
								continue;
							}
							goto IL_3B1;
						case 2:
							goto IL_389;
						case 3:
						{
							float num2;
							if (num2 > 0f)
							{
								num = 11;
								continue;
							}
							goto IL_10E;
						}
						case 4:
							goto IL_285;
						case 5:
							goto IL_2D0;
						case 6:
							switch (psLineEndType2)
							{
							case PsLineEndType.Arrow:
							case PsLineEndType.Stealth:
							case PsLineEndType.Open:
							{
								float num2 = this.ᜏ.ᜄ();
								num = 13;
								continue;
							}
							case PsLineEndType.Diamond:
							case PsLineEndType.Oval:
							{
								float num2 = 0f;
								num = 2;
								continue;
							}
							default:
								num = 14;
								continue;
							}
							break;
						case 7:
							goto IL_15A;
						case 8:
							if (true)
							{
							}
							goto IL_15A;
						case 9:
							if (this.ᜐ)
							{
								num = 15;
								continue;
							}
							a_ = A_0.ᜀ().ᜀ();
							a_2 = A_0.ᜀ().ᜃ();
							num = 12;
							continue;
						case 10:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_65;
							default:
								if (false)
								{
								}
								num3 = 360f - num3;
								num = 4;
								continue;
							}
							break;
						case 11:
						{
							spr\u1A68 a_3 = default(spr\u1A68);
							float num2;
							a_3.ᜁ(spr\u220A.ᜀ(a_, a_2, num2));
							a_3.ᜃ(A_0.ᜀ().ᜄ());
							a_3.ᜂ(A_0.ᜀ().ᜃ());
							a_3.ᜀ(A_0.ᜀ().ᜀ());
							A_0.ᜀ(a_3);
							num = 0;
							continue;
						}
						case 12:
						{
							float num2;
							if (num2 > 0f)
							{
								num = 16;
								continue;
							}
							goto IL_2D0;
						}
						case 13:
							goto IL_389;
						case 14:
							return;
						case 15:
							a_ = A_0.ᜀ().ᜂ();
							a_2 = A_0.ᜀ().ᜄ();
							num = 3;
							continue;
						case 16:
						{
							spr\u1A68 a_4 = default(spr\u1A68);
							float num2;
							a_4.ᜀ(spr\u220A.ᜀ(a_, a_2, num2));
							a_4.ᜃ(A_0.ᜀ().ᜄ());
							a_4.ᜂ(A_0.ᜀ().ᜃ());
							a_4.ᜁ(A_0.ᜀ().ᜂ());
							A_0.ᜀ(a_4);
							num = 5;
							continue;
						}
						}
						goto IL_5B;
						IL_10E:
						a_5 = this.ᜏ.ᜃ().ᜂ();
						a_6 = this.ᜏ.ᜃ().ᜀ();
						num = 7;
						continue;
						IL_15A:
						pointF = new PointF(a_.X - a_2.X, a_.Y - a_2.Y);
						float num4 = -pointF.Y;
						float num5 = num4 / (float)Math.Sqrt((double)(pointF.X * pointF.X + pointF.Y * pointF.Y));
						num3 = (float)spr\u2109.ᜃ(Math.Acos((double)num5));
						num = 1;
						continue;
						IL_2D0:
						a_5 = this.ᜏ.ᜀ().ᜂ();
						a_6 = this.ᜏ.ᜀ().ᜀ();
						num = 8;
						continue;
						IL_389:
						num = 9;
					}
				}
			}
			return;
			IL_285:
			IL_3B1:
			this.ᜀ(a_, num3, a_5, a_6);
			return;
		}
		}
	}

	// Token: 0x06002973 RID: 10611 RVA: 0x00293E64 File Offset: 0x00292E64
	private static PointF ᜀ(PointF A_0, PointF A_1, float A_2)
	{
		switch (0)
		{
		default:
		{
			double num;
			double num2;
			double num4;
			for (;;)
			{
				num = (double)(A_1.X - A_0.X);
				num2 = (double)(A_1.Y - A_0.Y);
				double num3 = Math.Sqrt(spr\u220A.ᜀ(num) + spr\u220A.ᜀ(num2));
				num4 = 0.0;
				int num5 = 1;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						goto IL_C1;
					case 1:
						if (num3 != 0.0)
						{
							num5 = 2;
							continue;
						}
						goto IL_C3;
					case 2:
						for (;;)
						{
							num4 = (double)A_2 / num3;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_A7;
							}
						}
						IL_A7:
						if (true)
						{
						}
						if (false)
						{
						}
						num5 = 0;
						continue;
					}
					break;
				}
			}
			IL_C1:
			IL_C3:
			return new PointF((float)((double)A_0.X + num * num4), (float)((double)A_0.Y + num2 * num4));
		}
		}
	}

	// Token: 0x06002974 RID: 10612 RVA: 0x00293F54 File Offset: 0x00292F54
	private void ᜀ(PointF A_0, float A_1, PsLineEndSize A_2, PsLineEndSize A_3)
	{
		spr\u1926 spr_u;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					PsLineEndType psLineEndType = this.ᜂ();
					spr_u = new spr\u1926();
					ArrayList arrayList = new ArrayList();
					int num = 7;
					for (;;)
					{
						float num3;
						float num4;
						float num5;
						float num7;
						PsLineEndType psLineEndType2;
						switch (num)
						{
						case 0:
							return;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								goto IL_425;
							}
							break;
						case 2:
							goto IL_124;
						case 3:
						{
							int num2;
							if (num2 >= arrayList.Count)
							{
								num = 15;
								continue;
							}
							spr\u24AB spr_u24AB = (spr\u24AB)arrayList[num2];
							spr\u25FD spr_u25FD;
							spr_u24AB.ᜀ(spr_u25FD);
							spr_u.ᜁ((spr᪑)spr_u24AB);
							num2++;
							num = 2;
							continue;
						}
						case 4:
							goto IL_169;
						case 5:
						{
							if (arrayList.Count == 0)
							{
								num = 0;
								continue;
							}
							num3 *= num4;
							num5 *= num4;
							spr\u25FD spr_u25FD = new spr\u25FD();
							spr_u25FD.ᜁ(num3, num5, MatrixOrder.Prepend);
							spr_u25FD.ᜀ(0f, this.ᜀ(psLineEndType, A_2, A_3), MatrixOrder.Append);
							spr_u25FD.ᜀ(A_1, MatrixOrder.Append);
							spr_u25FD.ᜀ(A_0.X, A_0.Y, MatrixOrder.Append);
							int num2 = 0;
							num = 9;
							continue;
						}
						case 6:
						{
							int num6;
							spr\u1A68[] array;
							if (num6 >= array.Length)
							{
								num = 10;
								continue;
							}
							spr\u17F0 spr_u17F = new spr\u17F0();
							spr_u17F.ᜀ(array[num6]);
							arrayList.Add(spr_u17F);
							num6++;
							num = 1;
							continue;
						}
						case 7:
							if ((double)this.ᜏ.ᜄ() > 2.0)
							{
								num = 19;
								continue;
							}
							num = 14;
							continue;
						case 8:
							goto IL_1B4;
						case 9:
							goto IL_124;
						case 10:
							goto IL_169;
						case 11:
							goto IL_169;
						case 12:
							goto IL_169;
						case 13:
							goto IL_169;
						case 14:
							num7 = 2f;
							goto IL_314;
						case 15:
							if (true)
							{
							}
							num = 21;
							continue;
						case 16:
							goto IL_425;
						case 17:
							goto IL_169;
						case 18:
							num7 = this.ᜏ.ᜄ();
							goto IL_314;
						case 19:
							num = 18;
							continue;
						case 20:
							num = 13;
							continue;
						case 21:
							if (spr_u.ᜁ())
							{
								num = 8;
								continue;
							}
							goto IL_44D;
						case 22:
							switch (psLineEndType2)
							{
							case PsLineEndType.None:
								goto IL_169;
							case PsLineEndType.Arrow:
								arrayList.Add(new sprᴎ(spr\u220A.ᜉ));
								num = 17;
								continue;
							case PsLineEndType.Stealth:
								arrayList.Add(new sprᴎ(spr\u220A.ᜊ));
								num = 4;
								continue;
							case PsLineEndType.Diamond:
								arrayList.Add(new sprᴎ(spr\u220A.ᜌ));
								num = 11;
								continue;
							case PsLineEndType.Oval:
							{
								spr\u220E spr_u220E = new spr\u220E();
								spr_u220E.ᜀ(new PointF(-1.5f, -1.5f));
								spr_u220E.ᜀ(new SizeF(3f, 3f));
								spr_u220E.ᜃ(0.0);
								spr_u220E.ᜂ(360.0);
								spr\u1A68[] array = spr_u220E.ᜃ();
								int num6 = 0;
								num = 16;
								continue;
							}
							case PsLineEndType.Open:
							{
								sprᴎ value = new sprᴎ(spr\u220A.ᜋ);
								arrayList.Add(value);
								spr_u.ᜀ(false);
								num4 = this.\u1712.ᜆ().ᜀ();
								SizeF sizeF = spr\u220A.ᜎ[(int)A_2][(int)A_3];
								num3 = sizeF.Width;
								num5 = sizeF.Height;
								num = 12;
								continue;
							}
							default:
								num = 20;
								continue;
							}
							break;
						}
						break;
						IL_124:
						num = 3;
						continue;
						IL_169:
						num = 5;
						continue;
						IL_314:
						num4 = num7;
						spr_u.ᜀ(true);
						num3 = spr\u220A.\u170D[(int)A_2];
						num5 = spr\u220A.\u170D[(int)A_3];
						psLineEndType2 = psLineEndType;
						num = 22;
						continue;
						IL_425:
						num = 6;
					}
				}
				break;
			}
		}
		return;
		IL_1B4:
		this.ᜑ.ᜁ(spr_u);
		return;
		IL_44D:
		this.\u1712.ᜁ(spr_u);
	}

	// Token: 0x06002975 RID: 10613 RVA: 0x002943BC File Offset: 0x002933BC
	private float ᜀ(PsLineEndType A_0, PsLineEndSize A_1, PsLineEndSize A_2)
	{
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_62;
				case 1:
					goto IL_6E;
				case 2:
					num = 0;
					continue;
				case 3:
					if (A_0 == PsLineEndType.Open)
					{
						if (true)
						{
						}
						num = 4;
						continue;
					}
					goto IL_BF;
				case 4:
					num = 5;
					continue;
				case 5:
					if (A_1 != PsLineEndSize.Small)
					{
						goto IL_AE;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_62;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				break;
				IL_62:
				if (A_2 != PsLineEndSize.Large)
				{
					goto IL_AE;
				}
				num = 1;
			}
		}
		IL_6E:
		return this.\u1712.ᜆ().ᜀ() * 2.5f;
		IL_AE:
		return this.\u1712.ᜆ().ᜀ();
		IL_BF:
		return 0f;
	}

	// Token: 0x06002976 RID: 10614 RVA: 0x00294490 File Offset: 0x00293490
	private PsLineEndType ᜂ()
	{
		if (!this.ᜐ)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜏ.ᜀ().ᜃ();
			}
		}
		return this.ᜏ.ᜃ().ᜃ();
	}

	// Token: 0x06002977 RID: 10615 RVA: 0x002944F8 File Offset: 0x002934F8
	private PsLineEndSize ᜁ()
	{
		if (!this.ᜐ)
		{
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
				return this.ᜏ.ᜀ().ᜀ();
			}
		}
		return this.ᜏ.ᜃ().ᜀ();
	}

	// Token: 0x06002978 RID: 10616 RVA: 0x00294560 File Offset: 0x00293560
	private PsLineEndSize ᜀ()
	{
		if (!this.ᜐ)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				return this.ᜏ.ᜀ().ᜂ();
			}
		}
		return this.ᜏ.ᜃ().ᜂ();
	}

	// Token: 0x06002979 RID: 10617 RVA: 0x002945C8 File Offset: 0x002935C8
	private static double ᜀ(double A_0)
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
		return A_0 * A_0;
	}

	// Token: 0x0600297B RID: 10619 RVA: 0x0029461C File Offset: 0x0029361C
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u220A()
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
		spr\u220A.ᜉ = new float[]
		{
			-1.5f,
			3f,
			0f,
			0f,
			1.5f,
			3f
		};
		spr\u220A.ᜊ = new float[]
		{
			-1.5f,
			3f,
			0f,
			0f,
			1.5f,
			3f,
			0f,
			1.8000001f
		};
		spr\u220A.ᜋ = new float[]
		{
			-1.75f,
			3f,
			0f,
			0f,
			1.75f,
			3f
		};
		spr\u220A.ᜌ = new float[]
		{
			0f,
			-1.5f,
			1.5f,
			0f,
			0f,
			1.5f,
			-1.5f,
			0f
		};
		spr\u220A.\u170D = new float[]
		{
			0.65f,
			1f,
			1.68f
		};
		spr\u220A.ᜎ = new SizeF[][]
		{
			new SizeF[]
			{
				new SizeF(0.7f, 0.65f),
				new SizeF(0.7f, 1f),
				new SizeF(0.7f, 1.5f)
			},
			new SizeF[]
			{
				new SizeF(1f, 0.65f),
				new SizeF(1f, 1f),
				new SizeF(1f, 1.5f)
			},
			new SizeF[]
			{
				new SizeF(1.42f, 0.65f),
				new SizeF(1.42f, 1f),
				new SizeF(1.42f, 1.5f)
			}
		};
	}

	// Token: 0x040023FB RID: 9211
	private new const float ᜀ = 3f;

	// Token: 0x040023FC RID: 9212
	private new const float ᜁ = 3f;

	// Token: 0x040023FD RID: 9213
	private new const float ᜂ = 3f;

	// Token: 0x040023FE RID: 9214
	private const float ᜃ = 3f;

	// Token: 0x040023FF RID: 9215
	private const float ᜄ = 1.8000001f;

	// Token: 0x04002400 RID: 9216
	private const float ᜅ = 3.5f;

	// Token: 0x04002401 RID: 9217
	private const float ᜆ = 3f;

	// Token: 0x04002402 RID: 9218
	private const float ᜇ = 1.5f;

	// Token: 0x04002403 RID: 9219
	private const float ᜈ = 1.5f;

	// Token: 0x04002404 RID: 9220
	private static readonly float[] ᜉ;

	// Token: 0x04002405 RID: 9221
	private static readonly float[] ᜊ;

	// Token: 0x04002406 RID: 9222
	private static readonly float[] ᜋ;

	// Token: 0x04002407 RID: 9223
	private static readonly float[] ᜌ;

	// Token: 0x04002408 RID: 9224
	private static readonly float[] \u170D;

	// Token: 0x04002409 RID: 9225
	private static readonly SizeF[][] ᜎ;

	// Token: 0x0400240A RID: 9226
	private spr\u200F ᜏ;

	// Token: 0x0400240B RID: 9227
	private bool ᜐ;

	// Token: 0x0400240C RID: 9228
	private spr\u1B70 ᜑ;

	// Token: 0x0400240D RID: 9229
	private spr\u1B70 \u1712;
}
