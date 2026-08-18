using System;
using System.Collections;
using System.Drawing;

// Token: 0x020003A1 RID: 929
internal class spr\u24F7
{
	// Token: 0x06003474 RID: 13428 RVA: 0x00301890 File Offset: 0x00300890
	internal static void ᜀ(sprᴎ A_0, sprᴎ A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int count = A_1.ᜀ().Count;
				int count2 = A_0.ᜀ().Count;
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
					{
						if (sprὍ.ᜀ((PointF)A_1.ᜀ()[count - 1], (PointF)A_0.ᜀ()[0]))
						{
							num = 6;
							continue;
						}
						spr\u1B7C a_ = new spr\u1B7C((PointF)A_1.ᜀ()[count - 2], (PointF)A_1.ᜀ()[count - 1]);
						spr\u1B7C a_2 = new spr\u1B7C((PointF)A_0.ᜀ()[0], (PointF)A_0.ᜀ()[1]);
						PointF[] array = new PointF[]
						{
							PointF.Empty
						};
						num = 2;
						continue;
					}
					case 2:
					{
						spr\u1B7C a_;
						spr\u1B7C a_2;
						PointF[] array;
						if (spr\u1B7C.ᜀ(a_, a_2, array, true))
						{
							num = 7;
							continue;
						}
						return;
					}
					case 3:
						if (count2 > 1)
						{
							goto IL_63;
						}
						return;
					case 4:
						if (count > 1)
						{
							num = 0;
							continue;
						}
						return;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_63;
						default:
							goto IL_196;
						}
						break;
					case 6:
						return;
					case 7:
					{
						PointF[] array;
						A_1.ᜀ()[count - 1] = array[0];
						A_0.ᜀ()[0] = array[0];
						num = 5;
						continue;
					}
					case 8:
						if (true)
						{
						}
						num = 4;
						continue;
					}
					break;
					IL_63:
					num = 8;
				}
			}
			IL_196:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06003475 RID: 13429 RVA: 0x00301A90 File Offset: 0x00300A90
	internal static spr\u187D[] ᜀ(sprᴎ A_0, spr\u187D[] A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			int num = 1;
			ArrayList arrayList;
			for (;;)
			{
				bool flag;
				PointF pointF;
				PointF pointF2;
				PointF[] a_;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					if (!flag)
					{
						num = 5;
						continue;
					}
					goto IL_1A9;
				case 2:
					if (!A_2)
					{
						pointF = (PointF)A_0.ᜀ()[A_0.ᜀ().Count - 2];
						pointF2 = (PointF)A_0.ᜀ()[A_0.ᜀ().Count - 1];
						num = 6;
						continue;
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
						num = 7;
						continue;
					}
					break;
				case 3:
					goto IL_115;
				case 4:
					return A_1;
				case 5:
					spr\u24F7.ᜀ(A_0, A_1, A_2, a_);
					num = 3;
					continue;
				case 6:
					goto IL_63;
				case 7:
					pointF = (PointF)A_0.ᜀ()[0];
					pointF2 = (PointF)A_0.ᜀ()[1];
					num = 8;
					continue;
				case 8:
					goto IL_63;
				}
				if (A_0.ᜀ().Count < 2)
				{
					num = 4;
					continue;
				}
				arrayList = new ArrayList();
				num = 2;
				continue;
				IL_63:
				a_ = new PointF[]
				{
					pointF,
					pointF2
				};
				flag = spr\u24F7.ᜀ(A_0, A_1, A_2, a_, arrayList);
				num = 0;
			}
			return A_1;
			IL_115:
			IL_1A9:
			return (spr\u187D[])arrayList.ToArray(typeof(spr\u187D));
		}
		}
	}

	// Token: 0x06003476 RID: 13430 RVA: 0x00301C5C File Offset: 0x00300C5C
	private static void ᜀ(sprᴎ A_0, spr\u187D[] A_1, bool A_2, PointF[] A_3)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			PointF[] array;
			for (;;)
			{
				spr\u187D spr_u187D;
				spr\u187D spr_u187D2;
				spr\u1B7C spr_u1B7C;
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					spr_u187D = A_1[A_1.Length - 1];
					goto IL_173;
				case 2:
					num = 7;
					continue;
				case 3:
					spr_u1B7C = new spr\u1B7C(spr_u187D2.ᜂ(), spr_u187D2.ᜀ());
					goto IL_EE;
				case 4:
					if (!A_2)
					{
						num = 2;
						continue;
					}
					num = 3;
					continue;
				case 5:
					spr_u187D = A_1[0];
					goto IL_173;
				case 6:
					goto IL_152;
				case 7:
					spr_u1B7C = new spr\u1B7C(spr_u187D2.ᜂ(), spr_u187D2.ᜁ());
					goto IL_EE;
				case 9:
					if (A_2)
					{
						num = 6;
						continue;
					}
					goto IL_1C7;
				}
				if (!A_2)
				{
					num = 0;
					continue;
				}
				num = 1;
				continue;
				IL_EE:
				spr\u1B7C a_ = spr_u1B7C;
				array = new PointF[]
				{
					PointF.Empty
				};
				spr\u1B7C a_2;
				spr\u1B7C.ᜁ(a_2, a_, array);
				array[0] = spr\u24F7.ᜀ(A_1, A_2, A_3, array[0]);
				num = 9;
				continue;
				IL_173:
				spr_u187D2 = spr_u187D;
				a_2 = new spr\u1B7C(A_3[0], A_3[1]);
				if (true)
				{
				}
				num = 4;
			}
			IL_152:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_1C7:
				A_0.ᜀ()[A_0.ᜀ().Count - 1] = array[0];
				return;
			default:
				if (false)
				{
				}
				A_0.ᜀ()[0] = array[0];
				return;
			}
			break;
		}
		}
	}

	// Token: 0x06003477 RID: 13431 RVA: 0x00301E5C File Offset: 0x00300E5C
	internal static PointF ᜀ(spr\u187D[] A_0, bool A_1, PointF[] A_2, PointF A_3)
	{
		switch (0)
		{
		default:
		{
			float num3;
			spr\u1B7C spr_u1B7C;
			PointF[] array;
			for (;;)
			{
				float num = sprὍ.ᜀ(A_0);
				int num2 = 5;
				for (;;)
				{
					PointF pointF;
					float num4;
					switch (num2)
					{
					case 0:
						goto IL_1A1;
					case 1:
						pointF = A_0[0].ᜁ();
						goto IL_9D;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_53;
						default:
							if (false)
							{
							}
							num3 *= -1f;
							num2 = 10;
							continue;
						}
						break;
					case 3:
					{
						bool flag = A_3.X < (A_1 ? A_2[0].X : A_2[1].X);
						num2 = 7;
						continue;
					}
					case 4:
						pointF = A_0[A_0.Length - 1].ᜀ();
						goto IL_9D;
					case 5:
						goto IL_53;
					case 6:
						num2 = 1;
						continue;
					case 7:
					{
						bool flag;
						if (flag)
						{
							num2 = 2;
							continue;
						}
						goto IL_16F;
					}
					case 8:
						if (num4 <= num3)
						{
							num2 = 9;
							continue;
						}
						spr_u1B7C = new spr\u1B7C(A_2[0], A_2[1]);
						num2 = 3;
						continue;
					case 9:
						return A_3;
					case 10:
						goto IL_16F;
					}
					break;
					IL_53:
					if (!A_1)
					{
						num2 = 6;
						continue;
					}
					num2 = 4;
					continue;
					IL_9D:
					PointF a_ = pointF;
					num4 = sprὍ.ᜁ(A_3, a_);
					num3 = num;
					num2 = 8;
					continue;
					IL_16F:
					if (true)
					{
					}
					array = new PointF[]
					{
						PointF.Empty
					};
					num2 = 0;
				}
			}
			return A_3;
			IL_1A1:
			spr_u1B7C.ᜀ(A_1 ? A_2[0] : A_2[1], num3, array);
			return array[0];
		}
		}
	}

	// Token: 0x06003478 RID: 13432 RVA: 0x00302044 File Offset: 0x00301044
	private static bool ᜀ(sprᴎ A_0, spr\u187D[] A_1, bool A_2, PointF[] A_3, ArrayList A_4)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				flag = false;
				int num = 6;
				for (;;)
				{
					int num2;
					bool flag2;
					spr\u1D62 spr_u1D;
					int num3;
					spr\u187D spr_u187D;
					switch (num)
					{
					case 0:
						goto IL_1FD;
					case 1:
						if (A_2)
						{
							num = 5;
							continue;
						}
						num2--;
						num = 0;
						continue;
					case 2:
						flag2 = (num2 < A_1.Length);
						goto IL_175;
					case 3:
						num = 14;
						continue;
					case 4:
						num = 10;
						continue;
					case 5:
						num2++;
						goto IL_D4;
					case 6:
						if (!A_2)
						{
							num = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D4;
						}
						if (true)
						{
						}
						if (false)
						{
						}
						num = 13;
						continue;
					case 7:
						goto IL_1FD;
					case 8:
						goto IL_147;
					case 9:
						if (spr_u1D.ᜃ())
						{
							num = 15;
							continue;
						}
						goto IL_147;
					case 10:
						num3 = A_1.Length - 1;
						goto IL_1C8;
					case 11:
						num = 1;
						continue;
					case 12:
						goto IL_1FD;
					case 13:
						num3 = 0;
						goto IL_1C8;
					case 14:
						flag2 = (num2 > -1);
						goto IL_175;
					case 15:
						spr_u187D = spr\u24F7.ᜀ(A_2, spr_u187D, spr_u1D, A_0);
						flag = true;
						num = 8;
						continue;
					case 16:
						if (!A_2)
						{
							num = 3;
							continue;
						}
						num = 2;
						continue;
					case 17:
						if (!flag)
						{
							num = 11;
							continue;
						}
						return flag;
					case 18:
						return flag;
					}
					break;
					IL_175:
					if (!flag2)
					{
						num = 18;
						continue;
					}
					spr_u187D = A_1[num2];
					spr_u1D = spr\u24F7.ᜀ(A_3[0], A_3[1], spr_u187D);
					num = 9;
					continue;
					IL_D4:
					num = 12;
					continue;
					IL_147:
					A_4.Add(spr_u187D);
					num = 17;
					continue;
					IL_1C8:
					num2 = num3;
					num = 7;
					continue;
					IL_1FD:
					num = 16;
				}
			}
			return flag;
		}
		}
	}

	// Token: 0x06003479 RID: 13433 RVA: 0x00302270 File Offset: 0x00301270
	private static spr\u187D ᜀ(bool A_0, spr\u187D A_1, spr\u1D62 A_2, sprᴎ A_3)
	{
		int num = 3;
		for (;;)
		{
			spr\u187D[] array;
			spr\u187D spr_u187D;
			switch (num)
			{
			case 0:
				if (array.Length <= 1)
				{
					goto IL_A8;
				}
				num = 9;
				continue;
			case 1:
				goto IL_165;
			case 2:
				num = 4;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A8;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					spr_u187D = array[0];
					goto IL_F0;
				}
				break;
			case 5:
				return A_1;
			case 6:
				return A_1;
			case 7:
				A_3.ᜀ()[0] = A_2.ᜅ()[0];
				num = 1;
				continue;
			case 8:
				if (A_0)
				{
					num = 10;
					continue;
				}
				num = 0;
				continue;
			case 9:
				spr_u187D = array[1];
				goto IL_F0;
			case 10:
				A_1 = array[0];
				num = 5;
				continue;
			case 11:
				goto IL_165;
			}
			if (A_0)
			{
				num = 7;
				continue;
			}
			A_3.ᜀ()[A_3.ᜀ().Count - 1] = A_2.ᜅ()[0];
			num = 11;
			continue;
			IL_A8:
			num = 2;
			continue;
			IL_F0:
			A_1 = spr_u187D;
			num = 6;
			continue;
			IL_165:
			array = spr\u24F7.ᜀ(A_1, A_2.ᜁ()[0]);
			num = 8;
		}
		return A_1;
	}

	// Token: 0x0600347A RID: 13434 RVA: 0x00302410 File Offset: 0x00301410
	internal static spr\u2415 ᜀ(spr\u187D[] A_0, spr\u187D[] A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			spr\u2415 spr_u;
			for (;;)
			{
				IL_23:
				if (true)
				{
				}
				spr_u = new spr\u2415();
				spr_u.ᜀ(A_2);
				spr_u.ᜂ(!A_2);
				ArrayList arrayList = new ArrayList();
				ArrayList arrayList2 = new ArrayList();
				bool flag = spr\u24F7.ᜀ(A_0, A_1, arrayList, arrayList2, A_2);
				spr_u.ᜁ((spr\u187D[])arrayList.ToArray(typeof(spr\u187D)));
				spr_u.ᜀ((spr\u187D[])arrayList2.ToArray(typeof(spr\u187D)));
				for (;;)
				{
					IL_99:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!flag)
							{
								num = 2;
								continue;
							}
							return spr_u;
						case 1:
							return spr_u;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_99;
							default:
								if (false)
								{
								}
								spr\u24F7.ᜀ(A_0, A_1, A_2, spr_u);
								num = 1;
								continue;
							}
							break;
						}
						goto IL_23;
					}
				}
			}
			return spr_u;
		}
		}
	}

	// Token: 0x0600347B RID: 13435 RVA: 0x00302508 File Offset: 0x00301508
	private static void ᜀ(spr\u187D[] A_0, spr\u187D[] A_1, bool A_2, spr\u2415 A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				PointF[] array = new PointF[]
				{
					PointF.Empty
				};
				int num = 1;
				for (;;)
				{
					float a_;
					PointF a_2;
					switch (num)
					{
					case 0:
						if (!sprὍ.ᜀ(a_, 0f))
						{
							num = 6;
							continue;
						}
						goto IL_187;
					case 1:
						if (A_2)
						{
							num = 7;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_103;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 2:
						A_3.ᜁ(true);
						A_3.ᜀ(array[0]);
						num = 3;
						continue;
					case 3:
						return;
					case 4:
						a_2 = A_0[A_0.Length - 1].ᜀ();
						goto IL_E0;
					case 5:
						if (array[0] != PointF.Empty)
						{
							num = 2;
							continue;
						}
						return;
					case 6:
					{
						spr\u1B7C a_3 = spr\u24F7.ᜀ(A_0, A_2);
						spr\u1B7C a_4 = spr\u24F7.ᜀ(A_1, !A_2);
						spr\u1B7C.ᜁ(a_3, a_4, array);
						goto IL_103;
					}
					case 7:
						a_2 = A_0[0].ᜁ();
						goto IL_E0;
					case 8:
						if (true)
						{
						}
						num = 4;
						continue;
					case 9:
						goto IL_187;
					}
					break;
					IL_E0:
					a_ = sprὍ.ᜁ(a_2, (!A_2) ? A_1[0].ᜁ() : A_1[A_1.Length - 1].ᜀ());
					num = 0;
					continue;
					IL_103:
					num = 9;
					continue;
					IL_187:
					num = 5;
				}
			}
			return;
		}
	}

	// Token: 0x0600347C RID: 13436 RVA: 0x003026D4 File Offset: 0x003016D4
	private static spr\u1B7C ᜀ(spr\u187D[] A_0, bool A_1)
	{
		if (!A_1)
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
				return new spr\u1B7C(A_0[A_0.Length - 1].ᜀ(), A_0[A_0.Length - 1].ᜂ());
			}
		}
		return new spr\u1B7C(A_0[0].ᜂ(), A_0[0].ᜁ());
	}

	// Token: 0x0600347D RID: 13437 RVA: 0x00302758 File Offset: 0x00301758
	private static bool ᜀ(spr\u187D[] A_0, spr\u187D[] A_1, ArrayList A_2, ArrayList A_3, bool A_4)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				flag = false;
				int num = 4;
				for (;;)
				{
					int num2;
					int num3;
					bool flag2;
					switch (num)
					{
					case 0:
						if (A_4)
						{
							num = 8;
							continue;
						}
						num2++;
						num = 5;
						continue;
					case 1:
						if (!A_4)
						{
							num = 14;
							continue;
						}
						num = 11;
						continue;
					case 2:
						num3 = 0;
						goto IL_117;
					case 3:
						goto IL_71;
					case 4:
						if (!A_4)
						{
							num = 9;
							continue;
						}
						num = 6;
						continue;
					case 5:
						goto IL_71;
					case 6:
						goto IL_A7;
					case 7:
						num = 0;
						continue;
					case 8:
						num2--;
						num = 3;
						continue;
					case 9:
						num = 2;
						continue;
					case 10:
						return flag;
					case 11:
						flag2 = (num2 > -1);
						goto IL_E3;
					case 12:
						flag2 = (num2 < A_0.Length);
						goto IL_E3;
					case 13:
						if (flag)
						{
							return flag;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A7;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 14:
						num = 12;
						continue;
					case 15:
						goto IL_71;
					}
					break;
					IL_71:
					num = 1;
					continue;
					IL_E3:
					if (!flag2)
					{
						num = 10;
						continue;
					}
					if (true)
					{
					}
					spr\u187D spr_u187D = A_0[num2];
					A_3.Clear();
					spr\u23BC spr_u23BC = spr\u23BC.ᜀ();
					spr_u187D = spr\u24F7.ᜀ(spr_u187D, A_1, A_3, A_4, spr_u23BC);
					flag = spr_u23BC.ᜄ();
					A_2.Add(spr_u187D);
					num = 13;
					continue;
					IL_117:
					num2 = num3;
					num = 15;
					continue;
					IL_A7:
					num3 = A_0.Length - 1;
					goto IL_117;
				}
			}
			return flag;
		}
		}
	}

	// Token: 0x0600347E RID: 13438 RVA: 0x0030292C File Offset: 0x0030192C
	private static spr\u187D ᜀ(spr\u187D A_0, spr\u187D[] A_1, ArrayList A_2, bool A_3, spr\u23BC A_4)
	{
		int num = 10;
		for (;;)
		{
			int num2;
			int num3;
			spr\u23BC spr_u23BC;
			spr\u187D spr_u187D;
			bool flag;
			switch (num)
			{
			case 0:
				goto IL_183;
			case 1:
				if (A_3)
				{
					num = 16;
					continue;
				}
				num = 15;
				continue;
			case 2:
				num2--;
				num = 14;
				continue;
			case 3:
				num3 = 0;
				goto IL_11E;
			case 4:
				if (spr_u23BC.ᜄ())
				{
					num = 7;
					continue;
				}
				A_2.Add(spr_u187D);
				num = 12;
				continue;
			case 5:
				num = 3;
				continue;
			case 6:
				return A_0;
			case 7:
				A_0 = spr\u24F7.ᜀ(A_0, spr_u187D, A_2, A_3, spr_u23BC);
				A_4.ᜀ(true);
				num = 6;
				continue;
			case 8:
				num3 = A_1.Length - 1;
				goto IL_11E;
			case 9:
				return A_0;
			case 11:
				goto IL_183;
			case 12:
				if (!A_3)
				{
					num = 2;
					continue;
				}
				num2++;
				num = 11;
				continue;
			case 13:
				flag = (num2 < A_1.Length);
				goto IL_1B4;
			case 14:
				goto IL_183;
			case 15:
				IL_E1:
				flag = (num2 > -1);
				goto IL_1B4;
			case 16:
				num = 13;
				continue;
			}
			if (true)
			{
			}
			if (A_3)
			{
				num = 5;
				continue;
			}
			num = 8;
			continue;
			IL_11E:
			num2 = num3;
			num = 0;
			continue;
			IL_1B4:
			if (!flag)
			{
				num = 9;
				continue;
			}
			spr_u187D = A_1[num2];
			spr_u23BC = spr\u24F7.ᜀ(A_0, spr_u187D);
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_E1;
			default:
				if (false)
				{
				}
				num = 4;
				continue;
			}
			IL_183:
			num = 1;
		}
		return A_0;
	}

	// Token: 0x0600347F RID: 13439 RVA: 0x00302B00 File Offset: 0x00301B00
	private static spr\u187D ᜀ(spr\u187D A_0, spr\u187D A_1, ArrayList A_2, bool A_3, spr\u23BC A_4)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				float num = float.MinValue;
				int num2 = 0;
				int num3 = 0;
				int num4 = 12;
				for (;;)
				{
					spr\u187D[] array;
					spr\u187D spr_u187D;
					spr\u187D[] array2;
					spr\u187D spr_u187D2;
					switch (num4)
					{
					case 0:
						goto IL_E4;
					case 1:
						if (A_3)
						{
							num4 = 7;
							continue;
						}
						goto IL_169;
					case 2:
						num4 = 20;
						continue;
					case 3:
						goto IL_2EF;
					case 4:
						spr_u187D = array[1];
						goto IL_2D2;
					case 5:
						if (A_4.ᜁ()[num3] <= num)
						{
							num4 = 22;
							continue;
						}
						goto IL_1C3;
					case 6:
						if (array2.Length <= 1)
						{
							num4 = 16;
							continue;
						}
						num4 = 9;
						continue;
					case 7:
						num4 = 5;
						continue;
					case 8:
						goto IL_D2;
					case 9:
						spr_u187D2 = array2[1];
						goto IL_28F;
					case 10:
						if (!A_3)
						{
							num4 = 15;
							continue;
						}
						num4 = 14;
						continue;
					case 11:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BA;
						default:
							if (false)
							{
							}
							spr_u187D = array[0];
							goto IL_2D2;
						}
						break;
					case 12:
						goto IL_E4;
					case 13:
						goto IL_1C3;
					case 14:
						goto IL_BA;
					case 15:
						A_0 = array[0];
						num4 = 6;
						continue;
					case 16:
						num4 = 19;
						continue;
					case 17:
					{
						float a_ = A_4.ᜁ()[num2];
						float a_2 = A_4.ᜅ()[num2];
						array = spr\u24F7.ᜀ(A_0, a_);
						array2 = spr\u24F7.ᜀ(A_1, a_2);
						num4 = 10;
						continue;
					}
					case 18:
						if (num3 >= A_4.ᜁ().Length)
						{
							num4 = 17;
							continue;
						}
						num4 = 1;
						continue;
					case 19:
						if (true)
						{
						}
						spr_u187D2 = array2[0];
						goto IL_28F;
					case 20:
						if (A_4.ᜁ()[num3] <= num)
						{
							num4 = 13;
							continue;
						}
						goto IL_D2;
					case 21:
						goto IL_29D;
					case 22:
						goto IL_169;
					case 23:
						if (!A_3)
						{
							num4 = 2;
							continue;
						}
						goto IL_D2;
					case 24:
						num4 = 11;
						continue;
					}
					break;
					IL_BA:
					if (array.Length <= 1)
					{
						num4 = 24;
						continue;
					}
					num4 = 4;
					continue;
					IL_D2:
					num3++;
					num4 = 0;
					continue;
					IL_E4:
					num4 = 18;
					continue;
					IL_169:
					num4 = 23;
					continue;
					IL_1C3:
					num = A_4.ᜁ()[num3];
					num2 = num3;
					num4 = 8;
					continue;
					IL_28F:
					A_1 = spr_u187D2;
					num4 = 21;
					continue;
					IL_2D2:
					A_0 = spr_u187D;
					A_1 = array2[0];
					num4 = 3;
				}
			}
			IL_29D:
			IL_2EF:
			A_2.Add(A_1);
			return A_0;
		}
	}

	// Token: 0x06003480 RID: 13440 RVA: 0x00302E0C File Offset: 0x00301E0C
	internal static void ᜀ(spr\u187D[] A_0)
	{
		int num = 0;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 1:
				return;
			case 2:
				if (num2 < A_0.Length - 1)
				{
					spr\u23BC spr_u23BC = spr\u24F7.ᜀ(A_0[num2], A_0[num2 + 1]);
					num = 3;
					continue;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4E;
				default:
					if (false)
					{
					}
					num = 7;
					continue;
				}
				break;
			case 3:
			{
				spr\u23BC spr_u23BC;
				if (spr_u23BC.ᜄ())
				{
					num = 4;
					continue;
				}
				goto IL_4E;
			}
			case 4:
			{
				spr\u23BC spr_u23BC;
				spr\u187D[] array = spr\u24F7.ᜀ(A_0[num2], spr_u23BC.ᜁ()[0]);
				A_0[num2] = array[0];
				array = spr\u24F7.ᜀ(A_0[num2 + 1], spr_u23BC.ᜅ()[0]);
				num = 5;
				continue;
			}
			case 5:
			{
				spr\u187D[] array;
				A_0[num2 + 1] = ((array.Length > 1) ? array[1] : array[0]);
				num = 8;
				continue;
			}
			case 6:
				goto IL_76;
			case 7:
				return;
			case 8:
				goto IL_4E;
			case 9:
				goto IL_76;
			}
			if (A_0.Length < 2)
			{
				num = 1;
				continue;
			}
			num2 = 0;
			num = 9;
			continue;
			IL_4E:
			A_0[num2].ᜂ(A_0[num2 + 1].ᜁ());
			num2++;
			num = 6;
			continue;
			IL_76:
			if (true)
			{
			}
			num = 2;
		}
	}

	// Token: 0x06003481 RID: 13441 RVA: 0x00302FD8 File Offset: 0x00301FD8
	private static spr\u23BC ᜀ(spr\u187D A_0, spr\u187D A_1)
	{
		ArrayList arrayList = new ArrayList();
		sprᯓ sprᯓ = new sprᯓ();
		sprᯓ sprᯓ2 = new sprᯓ();
		spr᪕ a_ = new spr᪕(A_0);
		spr᪕ a_2 = new spr᪕(A_1);
		spr\u24F7.ᜀ(a_, a_2, arrayList, sprᯓ);
		spr\u24F7.ᜀ(a_, a_2, sprᯓ, sprᯓ2);
		if (sprᯓ.ᜆ() == 0)
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
				return spr\u23BC.ᜀ();
			}
		}
		return new spr\u23BC(true, sprᯓ.ᜆ(), sprᯓ2.ᜌ(), sprᯓ.ᜌ(), (PointF[])arrayList.ToArray(typeof(PointF)));
	}

	// Token: 0x06003482 RID: 13442 RVA: 0x00303088 File Offset: 0x00302088
	private static void ᜀ(spr᪕ A_0, spr᪕ A_1, sprᯓ A_2, sprᯓ A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				PointF pointF = A_0.ᜂ();
				PointF pointF2 = A_0.ᜃ();
				PointF pointF3 = A_0.ᜁ();
				PointF pointF4 = A_1.ᜂ();
				PointF pointF5 = A_1.ᜃ();
				PointF pointF6 = A_1.ᜁ();
				int num = 0;
				int num2 = 10;
				for (;;)
				{
					int num4;
					switch (num2)
					{
					case 0:
						return;
					case 1:
						goto IL_1A3;
					case 2:
						goto IL_1A3;
					case 3:
						goto IL_88;
					case 4:
					{
						float num3;
						if (num3 >= 0f)
						{
							goto IL_DE;
						}
						goto IL_AD;
					}
					case 5:
					{
						double[] array;
						if (num4 >= array.Length)
						{
							num2 = 1;
							continue;
						}
						float num3 = (float)array[num4];
						num2 = 4;
						continue;
					}
					case 6:
					{
						float num3;
						A_3.ᜄ(num3);
						num2 = 2;
						continue;
					}
					case 7:
						if (true)
						{
						}
						num2 = 12;
						continue;
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
							if (num >= A_2.ᜆ())
							{
								num2 = 0;
								continue;
							}
							float num5 = A_2.ᜃ(num);
							double[] array = new spr\u2210((double)(-(double)pointF.X), (double)(-(double)pointF2.X), (double)(-(double)pointF3.X + pointF6.X + pointF5.X * num5 + pointF4.X * num5 * num5)).ᜄ();
							num4 = 0;
							num2 = 3;
							continue;
						}
						}
						break;
					case 9:
						goto IL_F7;
					case 10:
						goto IL_F7;
					case 11:
						goto IL_88;
					case 12:
					{
						float num3;
						if (num3 <= 1f)
						{
							num2 = 6;
							continue;
						}
						goto IL_AD;
					}
					}
					break;
					IL_88:
					num2 = 5;
					continue;
					IL_AD:
					num4++;
					num2 = 11;
					continue;
					IL_DE:
					num2 = 7;
					continue;
					IL_F7:
					num2 = 8;
					continue;
					IL_1A3:
					num++;
					num2 = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06003483 RID: 13443 RVA: 0x00303298 File Offset: 0x00302298
	private static void ᜀ(spr᪕ A_0, spr᪕ A_1, ArrayList A_2, sprᯓ A_3)
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
		PointF pointF = A_0.ᜂ();
		PointF pointF2 = A_0.ᜃ();
		PointF pointF3 = A_0.ᜁ();
		PointF pointF4 = A_1.ᜂ();
		PointF pointF5 = A_1.ᜃ();
		PointF pointF6 = A_1.ᜁ();
		double num = (double)(pointF.X * pointF2.Y - pointF2.X * pointF.Y);
		double num2 = (double)(pointF4.X * pointF2.Y - pointF2.X * pointF4.Y);
		double num3 = (double)(pointF5.X * pointF2.Y - pointF2.X * pointF5.Y);
		double num4 = (double)(pointF2.X * (pointF3.Y - pointF6.Y) + pointF2.Y * (-(double)pointF3.X + pointF6.X));
		double num5 = (double)(pointF4.X * pointF.Y - pointF.X * pointF4.Y);
		double num6 = (double)(pointF5.X * pointF.Y - pointF.X * pointF5.Y);
		double num7 = (double)(pointF.X * (pointF3.Y - pointF6.Y) + pointF.Y * (-(double)pointF3.X + pointF6.X));
		spr\u2210 spr_u = new spr\u2210(-num5 * num5, -2.0 * num5 * num6, num * num2 - num6 * num6 - 2.0 * num5 * num7, num * num3 - 2.0 * num6 * num7, num * num4 - num7 * num7);
		double[] a_ = spr_u.ᜄ();
		spr\u24F7.ᜀ(A_0, A_1, a_, A_2, A_3);
	}

	// Token: 0x06003484 RID: 13444 RVA: 0x0030347C File Offset: 0x0030247C
	private static void ᜀ(spr᪕ A_0, spr᪕ A_1, double[] A_2, ArrayList A_3, sprᯓ A_4)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				PointF pointF = A_0.ᜂ();
				PointF pointF2 = A_0.ᜃ();
				PointF pointF3 = A_0.ᜁ();
				PointF pointF4 = A_1.ᜂ();
				PointF pointF5 = A_1.ᜃ();
				PointF pointF6 = A_1.ᜁ();
				int num = 0;
				int num2 = 8;
				for (;;)
				{
					int num4;
					switch (num2)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B1;
						default:
						{
							if (false)
							{
							}
							double num3;
							if (num3 <= 1.0)
							{
								num2 = 16;
								continue;
							}
							goto IL_142;
						}
						}
						break;
					case 2:
					{
						if (num >= A_2.Length)
						{
							num2 = 0;
							continue;
						}
						double num3 = A_2[num];
						num2 = 17;
						continue;
					}
					case 3:
						goto IL_9F;
					case 4:
						goto IL_142;
					case 5:
					{
						bool flag;
						if (!flag)
						{
							num2 = 12;
							continue;
						}
						goto IL_142;
					}
					case 6:
						if (true)
						{
						}
						goto IL_2B1;
					case 7:
					{
						double[] array;
						if (array.Length > 0)
						{
							num2 = 6;
							continue;
						}
						goto IL_142;
					}
					case 8:
						goto IL_1A0;
					case 9:
						num2 = 1;
						continue;
					case 10:
						goto IL_9F;
					case 11:
					{
						double[] array2;
						if (num4 >= array2.Length)
						{
							num2 = 4;
							continue;
						}
						double num3;
						double[] array;
						bool flag = spr\u24F7.ᜀ(A_1, array2[num4], array, A_3, A_4, num3);
						num2 = 5;
						continue;
					}
					case 12:
						num4++;
						num2 = 10;
						continue;
					case 13:
					{
						double[] array2;
						if (array2.Length > 0)
						{
							num2 = 14;
							continue;
						}
						goto IL_142;
					}
					case 14:
						num2 = 7;
						continue;
					case 15:
						goto IL_1A0;
					case 16:
					{
						double num3;
						double[] array2 = new spr\u2210((double)(-(double)pointF.X), (double)(-(double)pointF2.X), (double)(-(double)pointF3.X + pointF6.X) + num3 * (double)pointF5.X + num3 * num3 * (double)pointF4.X).ᜄ();
						double[] array = new spr\u2210((double)(-(double)pointF.Y), (double)(-(double)pointF2.Y), (double)(-(double)pointF3.Y + pointF6.Y) + num3 * (double)pointF5.Y + num3 * num3 * (double)pointF4.Y).ᜄ();
						num2 = 13;
						continue;
					}
					case 17:
					{
						double num3;
						if (num3 >= 0.0)
						{
							num2 = 9;
							continue;
						}
						goto IL_142;
					}
					}
					break;
					IL_9F:
					num2 = 11;
					continue;
					IL_142:
					num++;
					num2 = 15;
					continue;
					IL_1A0:
					num2 = 2;
					continue;
					IL_2B1:
					num4 = 0;
					num2 = 3;
				}
			}
			return;
		}
	}

	// Token: 0x06003485 RID: 13445 RVA: 0x00303758 File Offset: 0x00302758
	private static bool ᜀ(spr᪕ A_0, double A_1, double[] A_2, ArrayList A_3, sprᯓ A_4, double A_5)
	{
		bool result;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
			{
				int num = 0;
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
						switch (num)
						{
						case 1:
						{
							int num2;
							if (num2 >= A_2.Length)
							{
								num = 2;
								continue;
							}
							num = 10;
							continue;
						}
						case 2:
							goto IL_163;
						case 3:
							goto IL_141;
						case 4:
							goto IL_107;
						case 5:
						{
							if (A_1 > 1.0)
							{
								num = 6;
								continue;
							}
							PointF pointF = A_0.ᜂ();
							PointF pointF2 = A_0.ᜃ();
							PointF pointF3 = A_0.ᜁ();
							result = false;
							int num2 = 0;
							num = 8;
							continue;
						}
						case 6:
							goto IL_193;
						case 7:
							num = 5;
							continue;
						case 8:
							goto IL_141;
						case 9:
						{
							PointF pointF;
							PointF pointF2;
							PointF pointF3;
							float x = (float)((double)pointF.X * A_5 * A_5 + (double)pointF2.X * A_5 + (double)pointF3.X);
							float y = (float)((double)pointF.Y * A_5 * A_5 + (double)pointF2.Y * A_5 + (double)pointF3.Y);
							A_3.Add(new PointF(x, y));
							A_4.ᜄ((float)A_5);
							result = true;
							if (true)
							{
							}
							num = 4;
							continue;
						}
						case 10:
						{
							int num2;
							if (Math.Abs(A_1 - A_2[num2]) < 0.0001)
							{
								num = 9;
								continue;
							}
							num2++;
							num = 3;
							continue;
						}
						}
						if (0.0 <= A_1)
						{
							num = 7;
							break;
						}
						return false;
						IL_141:
						num = 1;
						break;
					}
				}
				break;
			}
			}
		}
		IL_107:
		return result;
		IL_163:
		return result;
		IL_193:
		return false;
	}

	// Token: 0x06003486 RID: 13446 RVA: 0x0030393C File Offset: 0x0030293C
	private static spr\u1D62 ᜀ(PointF A_0, PointF A_1, spr\u187D A_2)
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
		spr᪕ a_ = new spr᪕(A_2);
		bool flag = sprὍ.ᜀ(A_1.Y, A_0.Y);
		double[] a_2 = spr\u24F7.ᜀ(A_0, A_1, a_, flag);
		return spr\u24F7.ᜀ(A_0, A_1, a_, a_2, flag);
	}

	// Token: 0x06003487 RID: 13447 RVA: 0x003039A8 File Offset: 0x003029A8
	private static double[] ᜀ(PointF A_0, PointF A_1, spr᪕ A_2, bool A_3)
	{
		switch (0)
		{
		default:
		{
			int num = 0;
			double[] result;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					break;
				case 1:
					result = new spr\u2210((double)A_2.ᜂ().Y, (double)A_2.ᜃ().Y, (double)(A_2.ᜁ().Y - A_0.Y)).ᜄ();
					goto IL_161;
				case 2:
					return result;
				case 3:
					return result;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_161:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					if (A_3)
					{
						num = 1;
					}
					else
					{
						float num2 = (A_1.X - A_0.X) / (A_1.Y - A_0.Y);
						result = new spr\u2210((double)(num2 * A_2.ᜂ().Y - A_2.ᜂ().X), (double)(num2 * A_2.ᜃ().Y - A_2.ᜃ().X), (double)(num2 * A_2.ᜁ().Y - num2 * A_0.Y + A_0.X - A_2.ᜁ().X)).ᜄ();
						num = 3;
					}
					break;
				}
			}
			return result;
		}
		}
	}

	// Token: 0x06003488 RID: 13448 RVA: 0x00303B28 File Offset: 0x00302B28
	private static spr\u1D62 ᜀ(PointF A_0, PointF A_1, spr᪕ A_2, double[] A_3, bool A_4)
	{
		switch (0)
		{
		default:
		{
			int num3;
			float[] array;
			bool a_;
			bool a_2;
			PointF[] array2;
			for (;;)
			{
				int num = A_3.Length;
				int num2 = 12;
				for (;;)
				{
					float num4;
					bool flag;
					bool flag2;
					int num6;
					switch (num2)
					{
					case 0:
						goto IL_F5;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_CC;
						default:
						{
							if (false)
							{
							}
							if (true)
							{
							}
							num3++;
							array[num3 - 1] = num4;
							float num5 = spr\u24F7.ᜀ(num4, A_0, A_1, A_2, A_4);
							num2 = 3;
							continue;
						}
						}
						break;
					case 2:
						num2 = 4;
						continue;
					case 3:
					{
						float num5;
						if (num5 <= 0f)
						{
							num2 = 2;
							continue;
						}
						num2 = 18;
						continue;
					}
					case 4:
					{
						float num5;
						flag = (num5 < 1f);
						goto IL_27A;
					}
					case 5:
						if (num3 == 1)
						{
							num2 = 19;
							continue;
						}
						a_ = flag2;
						num2 = 17;
						continue;
					case 6:
						goto IL_206;
					case 7:
						goto IL_8C;
					case 8:
						goto IL_201;
					case 9:
						goto IL_F5;
					case 10:
						if (num3 == 0)
						{
							num2 = 8;
							continue;
						}
						goto IL_2A0;
					case 11:
						num2 = 10;
						continue;
					case 12:
						if (num == 0)
						{
							num2 = 14;
							continue;
						}
						a_2 = false;
						a_ = false;
						num3 = 0;
						array2 = new PointF[]
						{
							PointF.Empty,
							PointF.Empty
						};
						array = new float[2];
						num6 = 0;
						num2 = 9;
						continue;
					case 13:
						num2 = 16;
						continue;
					case 14:
						goto IL_87;
					case 15:
						if (num6 >= num)
						{
							num2 = 11;
							continue;
						}
						num4 = (float)A_3[num6];
						goto IL_CC;
					case 16:
						if (num4 <= 1f)
						{
							num2 = 1;
							continue;
						}
						goto IL_206;
					case 17:
						goto IL_8C;
					case 18:
						flag = true;
						goto IL_27A;
					case 19:
						a_2 = flag2;
						num2 = 7;
						continue;
					case 20:
						if (num4 >= 0f)
						{
							num2 = 13;
							continue;
						}
						goto IL_206;
					}
					break;
					IL_8C:
					array2[num3 - 1] = A_2.ᜀ(num4);
					num2 = 6;
					continue;
					IL_CC:
					num2 = 20;
					continue;
					IL_F5:
					num2 = 15;
					continue;
					IL_206:
					num6++;
					num2 = 0;
					continue;
					IL_27A:
					flag2 = flag;
					num2 = 5;
				}
			}
			IL_87:
			return spr\u1D62.ᜀ();
			IL_201:
			return spr\u1D62.ᜀ();
			IL_2A0:
			return new spr\u1D62(true, a_2, a_, num3, array2, array);
		}
		}
	}

	// Token: 0x06003489 RID: 13449 RVA: 0x00303DE8 File Offset: 0x00302DE8
	private static float ᜀ(float A_0, PointF A_1, PointF A_2, spr᪕ A_3, bool A_4)
	{
		int num = 9;
		float num4;
		float num5;
		PointF pointF;
		for (;;)
		{
			IL_0A:
			float num2;
			float num3;
			switch (num)
			{
			case 0:
				goto IL_90;
			case 1:
				num = 11;
				continue;
			case 2:
				num2 = A_1.Y;
				goto IL_10E;
			case 3:
				if (!A_4)
				{
					num = 8;
					continue;
				}
				num = 6;
				continue;
			case 4:
				if (!A_4)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
			case 5:
				num = 7;
				continue;
			case 6:
				num2 = A_1.X;
				goto IL_10E;
			case 7:
				goto IL_A7;
			case 8:
				num = 2;
				continue;
			case 10:
				num3 = A_2.X;
				goto IL_DD;
			case 11:
				num3 = A_2.Y;
				goto IL_DD;
			}
			while (!A_4)
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
					num = 1;
					goto IL_0A;
				}
			}
			num = 10;
			continue;
			IL_DD:
			num4 = num3;
			num = 3;
			continue;
			IL_10E:
			num5 = num2;
			pointF = A_3.ᜀ(A_0);
			if (true)
			{
			}
			num = 4;
		}
		IL_90:
		float num6 = pointF.X;
		goto IL_141;
		IL_A7:
		num6 = pointF.Y - num5;
		IL_141:
		return num6 / (num4 - num5);
	}

	// Token: 0x0600348A RID: 13450 RVA: 0x00303F3C File Offset: 0x00302F3C
	private static spr\u187D[] ᜀ(spr\u187D A_0, float A_1)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E2;
				case 1:
					goto IL_9E;
				case 2:
					goto IL_66;
				case 4:
					if (A_1 == 1f)
					{
						num = 1;
						continue;
					}
					goto IL_FF;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_66;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 6:
					if (A_1 != 0f)
					{
						num = 5;
						continue;
					}
					goto IL_E4;
				case 7:
					if (A_1 > 1f)
					{
						num = 0;
						continue;
					}
					num = 6;
					continue;
				}
				if (A_1 >= 0f)
				{
					num = 2;
					continue;
				}
				goto IL_FD;
				IL_66:
				num = 7;
			}
			IL_9E:
			goto IL_E4;
			IL_E2:
			goto IL_FD;
			IL_E4:
			return new spr\u187D[]
			{
				A_0
			};
			IL_FD:
			return null;
			IL_FF:
			spr\u1B7C a_ = new spr\u1B7C(A_0.ᜁ(), A_0.ᜂ());
			spr\u1B7C a_2 = new spr\u1B7C(A_0.ᜂ(), A_0.ᜀ());
			float a_3 = sprὍ.ᜁ(A_0.ᜁ(), A_0.ᜂ());
			float a_4 = sprὍ.ᜁ(A_0.ᜂ(), A_0.ᜀ());
			PointF a_5 = spr\u24F7.ᜀ(a_, A_0, A_1, a_3, true);
			PointF a_6 = spr\u24F7.ᜀ(a_2, A_0, A_1, a_4, false);
			spr᪕ spr᪕ = new spr᪕(A_0);
			PointF pointF = spr᪕.ᜀ(A_1);
			spr\u187D spr_u187D = spr\u24F7.ᜀ(A_0.ᜁ(), a_5, pointF);
			spr\u187D spr_u187D2 = spr\u24F7.ᜀ(pointF, a_6, A_0.ᜀ());
			return new spr\u187D[]
			{
				spr_u187D,
				spr_u187D2
			};
		}
		}
	}

	// Token: 0x0600348B RID: 13451 RVA: 0x00304110 File Offset: 0x00303110
	private static spr\u187D ᜀ(PointF A_0, PointF A_1, PointF A_2)
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
		spr\u187D result = default(spr\u187D);
		result.ᜀ(A_0);
		result.ᜁ(A_1);
		result.ᜂ(A_2);
		return result;
	}

	// Token: 0x0600348C RID: 13452 RVA: 0x0030416C File Offset: 0x0030316C
	private static PointF ᜀ(spr\u1B7C A_0, spr\u187D A_1, float A_2, float A_3, bool A_4)
	{
		switch (0)
		{
		default:
		{
			int num = 7;
			PointF[] array;
			float num2;
			for (;;)
			{
				bool flag;
				bool flag2;
				bool flag3;
				switch (num)
				{
				case 0:
					goto IL_86;
				case 1:
				{
					PointF pointF;
					flag = (pointF.Y > A_1.ᜁ().Y);
					goto IL_214;
				}
				case 2:
					goto IL_147;
				case 3:
					num = 12;
					continue;
				case 4:
				{
					if (!A_4)
					{
						num = 13;
						continue;
					}
					PointF pointF2 = A_1.ᜂ();
					num = 8;
					continue;
				}
				case 5:
				{
					PointF pointF3;
					flag = (pointF3.Y > A_1.ᜂ().Y);
					goto IL_214;
				}
				case 6:
					goto IL_86;
				case 8:
				{
					PointF pointF2;
					flag2 = (pointF2.X > A_1.ᜁ().X);
					goto IL_226;
				}
				case 9:
				{
					if (true)
					{
					}
					PointF pointF3 = A_1.ᜀ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_16F;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				case 10:
					goto IL_16F;
				case 11:
					num = 4;
					continue;
				case 12:
					if (!A_0.ᜃ())
					{
						num = 11;
						continue;
					}
					goto IL_25D;
				case 13:
				{
					PointF pointF4 = A_1.ᜀ();
					num = 15;
					continue;
				}
				case 14:
					if (flag3)
					{
						num = 10;
						continue;
					}
					goto IL_13B;
				case 15:
				{
					PointF pointF4;
					flag2 = (pointF4.X > A_1.ᜂ().X);
					goto IL_226;
				}
				case 16:
					goto IL_13B;
				case 17:
				{
					if (!A_4)
					{
						num = 9;
						continue;
					}
					PointF pointF = A_1.ᜂ();
					num = 1;
					continue;
				}
				}
				if (A_0.ᜄ() == 0f)
				{
					num = 3;
					continue;
				}
				goto IL_25D;
				IL_86:
				array = new PointF[]
				{
					PointF.Empty
				};
				num2 = -1f;
				num = 14;
				continue;
				IL_13B:
				num = 2;
				continue;
				IL_16F:
				num2 = 1f;
				num = 16;
				continue;
				IL_214:
				flag3 = flag;
				num = 0;
				continue;
				IL_226:
				flag3 = flag2;
				num = 6;
				continue;
				IL_25D:
				num = 17;
			}
			IL_147:
			A_0.ᜀ(A_4 ? A_1.ᜁ() : A_1.ᜂ(), num2 * A_2 * A_3, array);
			return array[0];
		}
		}
	}
}
