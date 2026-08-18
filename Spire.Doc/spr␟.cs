using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Documents;
using Spire.Doc.Fields.Shape;
using Spire.Doc.Fields.Shape.Ps;

// Token: 0x020003CC RID: 972
internal class spr\u241F : spr\u22AF
{
	// Token: 0x060036B5 RID: 14005 RVA: 0x003337F4 File Offset: 0x003327F4
	internal spr\u241F(spr\u25AC A_0, sprά A_1, Hashtable A_2, bool A_3) : base(A_1, A_2)
	{
		this.ᜁ = new sprᣛ(A_0);
		this.ᜃ = A_3;
	}

	// Token: 0x060036B6 RID: 14006 RVA: 0x00333820 File Offset: 0x00332820
	internal new static spr\u24A6 ᜀ(spr\u1F9B A_0, sprά A_1)
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
		sprᩍ sprᩍ = A_0.ᜁ();
		Document document = sprᩍ.Document;
		spr\u241F spr_u241F = new spr\u241F(null, A_1, document.CanvasCache, false);
		return spr_u241F.ᜁ(A_0);
	}

	// Token: 0x060036B7 RID: 14007 RVA: 0x00333880 File Offset: 0x00332880
	internal new spr\u24A6 ᜁ(spr\u1F9B A_0)
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
		return this.ᜀ(A_0, true);
	}

	// Token: 0x060036B8 RID: 14008 RVA: 0x003338C4 File Offset: 0x003328C4
	internal new spr\u24A6 ᜀ(spr\u1F9B A_0, bool A_1)
	{
		for (;;)
		{
			IL_5A:
			sprᩍ sprᩍ = A_0.ᜁ();
			this.ᜂ = A_0;
			int num = 6;
			for (;;)
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
					switch (num)
					{
					case 0:
						base.ᜁ(sprᩍ);
						num = 7;
						continue;
					case 1:
					{
						spr\u24A6 spr_u24A;
						if (spr_u24A != null)
						{
							num = 2;
							continue;
						}
						goto IL_C7;
					}
					case 2:
					{
						spr\u24A6 spr_u24A;
						return spr_u24A;
					}
					case 3:
						num = 9;
						continue;
					case 4:
					{
						if (true)
						{
						}
						if (!A_1)
						{
							num = 0;
							continue;
						}
						spr\u24A6 result;
						return result;
					}
					case 5:
						if (sprᩍ.ᝏ())
						{
							num = 3;
							continue;
						}
						sprᩍ = (sprᩍ)sprᩍ.ParentObject;
						num = 10;
						continue;
					case 6:
					{
						if (sprᩍ.ᝐ())
						{
							goto IL_78;
						}
						spr\u24A6 spr_u24A = base.ᜁ(A_0, false);
						num = 1;
						continue;
					}
					case 7:
					{
						spr\u24A6 result;
						return result;
					}
					case 8:
						goto IL_80;
					case 9:
					{
						this.ᜂ((sprᩍ != A_0.ᜁ()) ? new spr\u1F9B(sprᩍ) : A_0, sprᾔ.ᜀ(), false);
						spr\u24A6 result = base.ᜁ(A_0, false);
						num = 4;
						continue;
					}
					case 10:
						goto IL_C7;
					}
					goto IL_5A;
					IL_C7:
					num = 5;
					continue;
				}
				IL_78:
				num = 8;
			}
		}
		IL_80:
		return new spr\u24A6();
	}

	// Token: 0x060036B9 RID: 14009 RVA: 0x00333A28 File Offset: 0x00332A28
	private spr\u24A6 ᜂ(spr\u1F9B A_0, sprᾔ A_1, bool A_2)
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
					goto IL_7A;
				default:
					goto IL_BC;
				}
				break;
			case 2:
				A_0 = this.ᜂ;
				num = 3;
				continue;
			case 3:
				goto IL_5C;
			case 4:
				goto IL_51;
			case 5:
				goto IL_7A;
			case 6:
				if (A_0.ᜁ().\u1774() != Spire.Doc.Fields.Shape.ShapeType.Group)
				{
					num = 5;
					continue;
				}
				num = 0;
				continue;
			}
			if (A_0.ᜁ() == this.ᜂ.ᜁ())
			{
				num = 2;
				continue;
			}
			IL_5C:
			num = 6;
			continue;
			IL_7A:
			num = 4;
		}
		IL_51:
		return this.ᜀ(A_0, A_1, A_2);
		IL_BC:
		if (true)
		{
		}
		if (false)
		{
		}
		return this.ᜁ(A_0, A_1, A_2);
	}

	// Token: 0x060036BA RID: 14010 RVA: 0x00333B0C File Offset: 0x00332B0C
	private new spr\u24A6 ᜁ(spr\u1F9B A_0, sprᾔ A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			PointF[] array;
			spr\u1D3C spr_u1D3C;
			spr\u24A6 spr_u24A2;
			spr\u2000 spr_u;
			for (;;)
			{
				sprᢋ sprᢋ = (sprᢋ)A_0.ᜁ();
				SizeF sizeF = A_0.ᜇ();
				int num = 0;
				for (;;)
				{
					SizeF sizeF2;
					IEnumerator enumerator;
					spr\u25FD spr_u25FD;
					float a_2;
					float a_3;
					float a_4;
					bool flag;
					switch (num)
					{
					case 0:
						if (!sizeF.IsEmpty)
						{
							num = 3;
							continue;
						}
						num = 1;
						continue;
					case 1:
						sizeF2 = sprᢋ.ᝡ();
						goto IL_2D9;
					case 2:
						if (true)
						{
						}
						sizeF2 = A_0.ᜇ();
						goto IL_2D9;
					case 3:
						num = 2;
						continue;
					case 4:
						try
						{
							num = 6;
							for (;;)
							{
								sprᩍ sprᩍ;
								spr\u1F9B spr_u1F9B;
								spr\u24A6 a_;
								switch (num)
								{
								case 0:
									if (!enumerator.MoveNext())
									{
										num = 7;
										continue;
									}
									sprᩍ = (sprᩍ)enumerator.Current;
									num = 13;
									continue;
								case 1:
									goto IL_26F;
								case 2:
									if (sprᩍ.ᝑ() == FlipOrientation.None)
									{
										num = 8;
										continue;
									}
									goto IL_18E;
								case 3:
								{
									a_ = this.ᜂ(spr_u1F9B, new sprᾔ(spr_u25FD, a_2, a_3, a_4), array == null && (A_2 || flag));
									spr\u24A6 spr_u24A = base.ᜁ(spr_u1F9B, false);
									sprᲨ sprᲨ = spr_u1F9B.ᜃ();
									num = 5;
									continue;
								}
								case 4:
									goto IL_225;
								case 5:
								{
									sprᲨ sprᲨ;
									if (sprᲨ != null)
									{
										num = 10;
										continue;
									}
									goto IL_225;
								}
								case 7:
									num = 1;
									continue;
								case 8:
									sprᩍ.ᜀ(sprᢋ.ᝑ());
									num = 11;
									continue;
								case 10:
								{
									spr\u24A6 spr_u24A;
									sprᲨ sprᲨ;
									sprᲨ.ᜀ(spr_u24A.ᜀ());
									spr_u1D3C.ᜂ(sprᲨ);
									num = 4;
									continue;
								}
								case 11:
									goto IL_18E;
								case 12:
									num = 2;
									continue;
								case 13:
									if (sprᢋ.ᝑ() != FlipOrientation.None)
									{
										num = 12;
										continue;
									}
									goto IL_18E;
								}
								goto IL_124;
								IL_18E:
								spr_u1F9B = new spr\u1F9B(sprᩍ);
								num = 3;
								continue;
								IL_225:
								spr_u24A2.ᜁ(a_);
								num = 9;
								continue;
								IL_23D:
								num = 0;
								continue;
								IL_124:
								goto IL_23D;
							}
							IL_26F:
							goto IL_6A;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									IL_2C4:
									disposable.Dispose();
									num = 1;
									break;
								default:
									if (false)
									{
									}
									num = 0;
									break;
								}
								for (;;)
								{
									switch (num)
									{
									case 0:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_2D8;
									case 1:
										goto IL_2D6;
									case 2:
										goto IL_2C4;
									}
									break;
								}
							}
							IL_2D6:
							IL_2D8:;
						}
						goto IL_2D9;
						IL_6A:
						spr_u = new spr\u2000(spr_u24A2);
						spr_u.ᜀ(A_0);
						num = 5;
						continue;
					case 5:
						goto IL_84;
					}
					break;
					IL_2D9:
					SizeF a_5 = sizeF2;
					spr_u25FD = spr\u1BA8.ᜀ(sprᢋ, a_5, false);
					spr_u25FD.ᜀ(A_1.ᜂ(), MatrixOrder.Append);
					spr\u25FD spr_u25FD2 = spr\u1BA8.ᜀ(sprᢋ, a_5, true);
					spr_u25FD2.ᜀ(A_1.ᜂ(), MatrixOrder.Append);
					SizeF sizeF3 = spr\u1BA8.ᜀ(sprᢋ, A_1.ᜃ(), A_1.ᜁ());
					float width = sizeF3.Width;
					float height = sizeF3.Height;
					a_2 = a_5.Width / (float)sprᢋ.\u1776() * width;
					a_3 = a_5.Height / (float)sprᢋ.ឍ() * height;
					a_4 = (float)(sprᢋ.ម() + (double)A_1.ᜄ());
					flag = A_0.ᜀ();
					spr_u1D3C = new spr\u1D3C();
					spr_u24A2 = new spr\u24A6();
					array = spr\u241F.ᜀ(sprᢋ, a_5, flag, a_4);
					enumerator = sprᢋ.ᝰ().GetEnumerator();
					num = 4;
				}
			}
			IL_84:
			spr_u.ᜀ((array != null) ? new sprᲨ(array) : spr\u1B69.ᜁ(spr_u1D3C));
			base.ᜀ(A_0.ᜁ(), spr_u);
			return spr_u24A2;
		}
		}
	}

	// Token: 0x060036BB RID: 14011 RVA: 0x00333EF4 File Offset: 0x00332EF4
	private new static PointF[] ᜀ(sprᢋ A_0, SizeF A_1, bool A_2, float A_3)
	{
		int num = 3;
		PointF[] array;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return array;
			default:
			{
				if (false)
				{
				}
				PointF[] array2;
				switch (num)
				{
				case 0:
					return array;
				case 1:
				{
					spr\u25FD spr_u25FD = new spr\u25FD();
					spr_u25FD.ᜁ(A_1.Width / 21600f, A_1.Height / 21600f);
					spr_u25FD.ᜀ(A_3, new PointF(A_1.Width / 2f, A_1.Height / 2f), MatrixOrder.Append);
					spr_u25FD.ᜀ(array);
					num = 0;
					continue;
				}
				case 2:
					array2 = A_0.ᝤ();
					goto IL_CE;
				case 4:
					array2 = null;
					goto IL_CE;
				case 5:
					num = 4;
					continue;
				case 6:
					if (array != null)
					{
						num = 1;
						continue;
					}
					return array;
				}
				if (true)
				{
				}
				if (!A_2)
				{
					num = 5;
					break;
				}
				num = 2;
				break;
				IL_CE:
				array = array2;
				num = 6;
				break;
			}
			}
		}
		return array;
	}

	// Token: 0x060036BC RID: 14012 RVA: 0x0033400C File Offset: 0x0033300C
	private new spr\u24A6 ᜀ(spr\u1F9B A_0, sprᾔ A_1, bool A_2)
	{
		switch (0)
		{
		default:
		{
			spr\u24A6 spr_u24A;
			for (;;)
			{
				spr\u1937 spr_u = (spr\u1937)A_0.ᜁ();
				this.ᜃ(spr_u);
				this.ᜀ(A_0, spr_u, A_1);
				int num = 9;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_2A8;
					case 1:
						goto IL_14A;
					case 2:
						goto IL_11A;
					case 3:
						goto IL_20F;
					case 4:
						goto IL_366;
					case 5:
						goto IL_366;
					case 6:
						if (true)
						{
						}
						spr_u24A = this.ᜀ(spr_u24A);
						num = 14;
						continue;
					case 7:
						goto IL_14A;
					case 8:
						goto IL_126;
					case 9:
						if (spr\u1D53.ᜀ(spr_u))
						{
							num = 15;
							continue;
						}
						num = 17;
						continue;
					case 10:
						if (spr_u.\u171D())
						{
							num = 21;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_126;
						default:
							if (false)
							{
							}
							this.ᜀ(A_0);
							num = 1;
							continue;
						}
						break;
					case 11:
						goto IL_2A8;
					case 12:
						spr_u24A.ᜀ(new spr\u25FD(1f, 0f, 0f, 1f, this.ᜀ.ᜐ().ᜀ().X, this.ᜀ.ᜐ().ᜀ().Y));
						num = 0;
						continue;
					case 13:
						goto IL_11A;
					case 14:
						goto IL_280;
					case 15:
						num = 10;
						continue;
					case 16:
						if (spr_u.\u171C())
						{
							num = 19;
							continue;
						}
						goto IL_39A;
					case 17:
						if (this.ᜀ.ᜋ() == null)
						{
							num = 20;
							continue;
						}
						num2 = 0;
						num3 = 0;
						num = 13;
						continue;
					case 18:
						if (spr_u.Owner is sprᢋ)
						{
							num = 12;
							continue;
						}
						spr_u24A.ᜀ(new spr\u25FD(1f, 0f, 0f, 1f, this.ᜀ.ᜈ().ᜀ().X, this.ᜀ.ᜈ().ᜀ().Y));
						num = 11;
						continue;
					case 19:
						this.ᜄ(spr_u);
						num = 3;
						continue;
					case 20:
						goto IL_34F;
					case 21:
						this.ᜁ();
						num = 7;
						continue;
					case 22:
						if (this.ᜀ.ᜉ().\u173A().ᜃ())
						{
							num = 6;
							continue;
						}
						goto IL_280;
					}
					break;
					IL_11A:
					num = 8;
					continue;
					IL_126:
					if (num3 >= this.ᜀ.ᜋ().Length)
					{
						num = 4;
						continue;
					}
					sprỬ a_ = this.ᜀ.ᜋ()[num3];
					this.ᜀ(a_, ref num2);
					num3++;
					num = 2;
					continue;
					IL_14A:
					this.ᜀ(spr_u);
					num = 5;
					continue;
					IL_280:
					num = 16;
					continue;
					IL_2A8:
					num = 22;
					continue;
					IL_366:
					spr_u24A = new spr\u24A6();
					num = 18;
				}
			}
			IL_20F:
			goto IL_39A;
			IL_34F:
			spr\u2000 spr_u2 = new spr\u2000(new spr\u24A6());
			spr_u2.ᜀ(A_0);
			base.ᜀ(A_0.ᜁ(), spr_u2);
			return spr_u2.ᜀ();
			IL_39A:
			spr_u2 = new spr\u2000(spr_u24A);
			this.ᜀ(A_0, A_1, spr_u2, A_2);
			spr_u2.ᜀ(A_0);
			base.ᜀ(A_0.ᜁ(), spr_u2);
			return spr_u24A;
		}
		}
	}

	// Token: 0x060036BD RID: 14013 RVA: 0x003343DC File Offset: 0x003333DC
	private new void ᜀ(spr\u1F9B A_0, spr\u1937 A_1, sprᾔ A_2)
	{
		int num = 10;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 7;
				continue;
			case 1:
				if (A_1.\u171F())
				{
					num = 4;
					continue;
				}
				return;
			case 2:
				if (A_1.ᝠ())
				{
					num = 9;
					continue;
				}
				return;
			case 3:
				A_0.ᜀ(this.ᜁ.ᜁ(this.ᜀ));
				this.ᜀ = new sprṏ(A_0, A_2, this.ᜁ);
				num = 6;
				continue;
			case 4:
				goto IL_D1;
			case 5:
				goto IL_16E;
			case 6:
				return;
			case 7:
				A_0.ᜀ((A_1.ᜡ().ᜁ() > 0.0) ? new SizeF((float)(A_1.ᜡ().ᜁ() * 0.009999999776482582) * A_0.ᜅ(), (float)A_1.ន()) : new SizeF((float)A_1.\u177D(), (float)A_1.ន()));
				num = 5;
				continue;
			case 8:
				if (this.ᜁ.ᜀ())
				{
					return;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_D1;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 9:
				num = 1;
				continue;
			}
			if (A_1.ᜡ().ᜀ())
			{
				num = 0;
				continue;
			}
			goto IL_16E;
			IL_D1:
			if (true)
			{
			}
			num = 8;
			continue;
			IL_16E:
			this.ᜀ = new sprṏ(A_0, A_2, this.ᜁ);
			num = 2;
		}
	}

	// Token: 0x060036BE RID: 14014 RVA: 0x003345A8 File Offset: 0x003335A8
	private new spr\u24A6 ᜀ(spr\u24A6 A_0)
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_58:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_46;
		}
		spr\u24A6 a_;
		for (;;)
		{
			IL_28:
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (this.ᜃ)
				{
					goto IL_58;
				}
				a_ = spr\u255A.ᜀ(this.ᜀ);
				num = 2;
				continue;
			case 1:
				A_0 = spr\u1CD2.ᜀ(this.ᜀ);
				num = 3;
				continue;
			case 2:
				goto IL_76;
			case 3:
				goto IL_8D;
			}
			goto IL_46;
		}
		IL_76:
		IL_8D:
		this.ᜀ.ᜀ(a_);
		return A_0;
		IL_46:
		a_ = null;
		num = 0;
		goto IL_28;
	}

	// Token: 0x060036BF RID: 14015 RVA: 0x00334654 File Offset: 0x00333654
	private void ᜄ(spr\u1937 A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_53;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				return;
			case 2:
				goto IL_53;
			}
			if (A_0.ᜪ() == ThreeDRenderMode.Wireframe)
			{
				num = 2;
				continue;
			}
			break;
			IL_53:
			spr\u1D68 spr_u1D = new spr\u1D68(this.ᜀ);
			this.ᜀ.ᜀ(spr_u1D.ᜀ(this.ᜀ.ᜏ(), A_0.ᜥ()));
			if (true)
			{
			}
			num = 1;
		}
	}

	// Token: 0x060036C0 RID: 14016 RVA: 0x003346F8 File Offset: 0x003336F8
	private void ᜃ(spr\u1937 A_0)
	{
		for (;;)
		{
			IL_1C:
			A_0.ᝂ();
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_61:
				A_0.ᜪ();
				num = 1;
				break;
			case 1:
				goto IL_43;
			default:
				goto IL_43;
			}
			for (;;)
			{
				IL_02:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_61;
				case 1:
					return;
				case 2:
					if (A_0.\u171C())
					{
						num = 0;
						continue;
					}
					return;
				}
				goto IL_1C;
			}
			IL_43:
			if (false)
			{
			}
			num = 2;
			goto IL_02;
		}
	}

	// Token: 0x060036C1 RID: 14017 RVA: 0x0033477C File Offset: 0x0033377C
	private new void ᜀ(spr\u1F9B A_0, sprᾔ A_1, spr\u2000 A_2, bool A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u24A6 spr_u24A = A_2.ᜀ();
				spr\u1937 spr_u = (spr\u1937)A_0.ᜁ();
				int num = 10;
				for (;;)
				{
					spr\u2248 spr_u2;
					spr\u236B a_;
					int a_2;
					PointF[] array;
					spr\u2248 spr_u3;
					switch (num)
					{
					case 0:
						num = 11;
						continue;
					case 1:
						spr_u24A.ᜀ(new spr\u252E(spr_u.ᝎ(), spr_u.ᝬ()));
						num = 14;
						continue;
					case 2:
						spr_u2 = null;
						goto IL_1F8;
					case 3:
						a_ = new spr\u236B();
						a_2 = this.ᜀ.ᜏ().ᜉ();
						num = 6;
						continue;
					case 4:
						if (array != null)
						{
							num = 13;
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
							num = 22;
							continue;
						}
						break;
					case 5:
						spr_u2 = null;
						goto IL_1F8;
					case 6:
						if (spr_u.ᜡ().ᜀ())
						{
							num = 17;
							continue;
						}
						goto IL_133;
					case 7:
						goto IL_133;
					case 8:
						spr_u2 = new spr\u2248(this.ᜁ);
						goto IL_1F8;
					case 9:
						num = 18;
						continue;
					case 10:
						if (this.ᜀ.ᜏ() != null)
						{
							num = 3;
							continue;
						}
						goto IL_25D;
					case 11:
						if (spr\u1CC6.ᜋ(spr_u.ᝬ()))
						{
							num = 1;
							continue;
						}
						return;
					case 12:
						num = 16;
						continue;
					case 13:
						if (true)
						{
						}
						num = 5;
						continue;
					case 14:
						return;
					case 15:
						goto IL_25D;
					case 16:
						if (!A_0.ᜀ())
						{
							num = 21;
							continue;
						}
						goto IL_1DF;
					case 17:
					{
						spr\u24A6 spr_u24A2 = new spr\u24A6();
						spr_u24A.ᜁ(spr_u24A2);
						spr_u24A = spr_u24A2;
						spr\u241F.ᜀ(spr_u.ᜡ().ᜃ(), A_0.ᜇ().Width, A_0.ᜅ(), spr_u24A2);
						num = 7;
						continue;
					}
					case 18:
						if (spr_u.ParentObject.DocumentObjectType == DocumentObjectType.ShapeGroup)
						{
							num = 0;
							continue;
						}
						return;
					case 19:
						A_2.ᜀ((array != null) ? new sprᲨ(array, true) : ((spr_u3 != null) ? spr_u3.ᜊ() : null));
						num = 15;
						continue;
					case 20:
						if (spr_u.ParentObject != null)
						{
							num = 9;
							continue;
						}
						return;
					case 21:
						num = 2;
						continue;
					case 22:
						if (!A_3)
						{
							num = 12;
							continue;
						}
						goto IL_1DF;
					}
					break;
					IL_133:
					array = this.ᜀ(spr_u);
					num = 4;
					continue;
					IL_1DF:
					num = 8;
					continue;
					IL_1F8:
					spr_u3 = spr_u2;
					this.ᜁ(spr_u24A, a_2, a_, spr_u3);
					this.ᜀ(spr_u24A, spr_u, A_1, spr_u3);
					this.ᜀ(spr_u24A, a_2, a_, spr_u3);
					num = 19;
					continue;
					IL_25D:
					this.ᜁ.ᜀ(this.ᜀ, spr_u24A);
					num = 20;
				}
			}
			return;
		}
	}

	// Token: 0x060036C2 RID: 14018 RVA: 0x00334A9C File Offset: 0x00333A9C
	private new PointF[] ᜀ(sprᩍ A_0)
	{
		PointF[] array;
		for (;;)
		{
			array = null;
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_54;
				case 1:
					return array;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_54;
					default:
						if (false)
						{
						}
						if (A_0.ᝤ() != null)
						{
							num = 0;
							continue;
						}
						return array;
					}
					break;
				}
				break;
				IL_54:
				array = A_0.ᝤ();
				this.ᜀ.ᜈ().ᜀ(array, false);
				num = 1;
			}
		}
		return array;
	}

	// Token: 0x060036C3 RID: 14019 RVA: 0x00334B2C File Offset: 0x00333B2C
	private new static void ᜀ(ShapeHorizontalAlignment A_0, float A_1, float A_2, spr\u24A6 A_3)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_45;
			case 2:
				switch (A_0)
				{
				case ShapeHorizontalAlignment.Center:
					goto IL_AA;
				case ShapeHorizontalAlignment.Right:
					goto IL_C6;
				}
				goto IL_85;
			case 3:
				return;
			case 4:
				if (true)
				{
				}
				A_3.ᜀ(new spr\u25FD());
				num = 1;
				continue;
			}
			if (spr\u25FD.ᜁ(A_3.ᜀ(), null))
			{
				num = 4;
				continue;
			}
			IL_45:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_85:
				num = 3;
				continue;
			}
			if (false)
			{
			}
			num = 2;
		}
		return;
		IL_AA:
		A_3.ᜀ().ᜀ((A_2 - A_1) * 0.5f, 0f, MatrixOrder.Append);
		return;
		IL_C6:
		A_3.ᜀ().ᜀ(A_2 - A_1, 0f, MatrixOrder.Append);
	}

	// Token: 0x060036C4 RID: 14020 RVA: 0x00334C14 File Offset: 0x00333C14
	private new void ᜁ(spr\u24A6 A_0, int A_1, spr\u236B A_2, spr\u2248 A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 13;
				for (;;)
				{
					spr\u1B70 spr_u1B;
					spr\u1B70 a_;
					switch (num2)
					{
					case 0:
						num2 = 16;
						continue;
					case 1:
						num2 = 10;
						continue;
					case 2:
						num2 = 7;
						continue;
					case 3:
						if (A_3 != null)
						{
							num2 = 19;
							continue;
						}
						goto IL_12C;
					case 4:
						goto IL_77;
					case 5:
						if (!this.ᜀ.ᜉ().ᝉ())
						{
							num2 = 0;
							continue;
						}
						goto IL_77;
					case 6:
						if (spr_u1B.ᜅ() != null)
						{
							num2 = 1;
							continue;
						}
						goto IL_12C;
					case 7:
						if (this.ᜀ.ᜉ().ម() != 0.0)
						{
							num2 = 9;
							continue;
						}
						goto IL_77;
					case 8:
						num2 = 5;
						continue;
					case 9:
						num2 = 17;
						continue;
					case 10:
						if (!(spr_u1B.ᜅ() is spr\u201C))
						{
							goto IL_77;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_278;
						default:
							if (false)
							{
							}
							num2 = 2;
							continue;
						}
						break;
					case 11:
						if (true)
						{
						}
						goto IL_19B;
					case 12:
					{
						spr\u197E spr_u197E = new spr\u197E();
						((spr\u201C)spr_u1B.ᜅ()).ᜀ(spr_u197E.ᜀ(spr_u1B));
						num2 = 4;
						continue;
					}
					case 13:
						goto IL_19B;
					case 14:
						goto IL_12C;
					case 15:
						if (num >= A_1)
						{
							num2 = 18;
							continue;
						}
						spr_u1B = (spr\u1B70)this.ᜀ.ᜏ().ᜀ(num);
						num2 = 6;
						continue;
					case 16:
						if (!this.ᜀ.ᜉ().ឃ())
						{
							num2 = 12;
							continue;
						}
						goto IL_77;
					case 17:
						if (!this.ᜀ.ᜉ().\u1719())
						{
							goto IL_278;
						}
						goto IL_77;
					case 18:
						return;
					case 19:
						A_3.ᜂ(a_);
						num2 = 14;
						continue;
					}
					break;
					IL_77:
					a_ = A_2.ᜀ(spr_u1B, false, true);
					A_0.ᜁ(a_);
					num2 = 3;
					continue;
					IL_12C:
					num++;
					num2 = 11;
					continue;
					IL_19B:
					num2 = 15;
					continue;
					IL_278:
					num2 = 8;
				}
			}
			return;
		}
	}

	// Token: 0x060036C5 RID: 14021 RVA: 0x00334EAC File Offset: 0x00333EAC
	private new void ᜀ(spr\u24A6 A_0, spr\u1937 A_1, sprᾔ A_2, spr\u2248 A_3)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 7;
			for (;;)
			{
				byte[] array;
				sprỏ sprỏ;
				byte[] array2;
				spr\u1DB3 spr_u1DB;
				spr\u24A6 spr_u24A;
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					return;
				case 2:
					array = spr\u241F.ᜁ(A_1);
					goto IL_105;
				case 3:
					array = spr\u1D53.ᜁ(sprỏ);
					goto IL_105;
				case 4:
					A_3.ᜀ(array2, spr_u1DB.ᜉ());
					num = 1;
					continue;
				case 5:
					if (A_3 != null)
					{
						num = 4;
						continue;
					}
					return;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						spr_u24A = new spr\u24A6();
						spr_u24A.ᜀ(spr\u1BA8.ᜀ(A_1, A_2));
						sprỏ = A_1.ᜮ();
						break;
					}
					num = 8;
					continue;
				case 8:
					if (!spr\u241F.ᜂ(A_1))
					{
						num = 0;
						continue;
					}
					num = 2;
					continue;
				}
				if (A_1.\u173C())
				{
					num = 6;
					continue;
				}
				break;
				IL_105:
				array2 = array;
				spr_u1DB = new spr\u1DB3(PointF.Empty, this.ᜀ.ᜌ(), array2, sprỏ.ᜃ(), spr\u241F.ᜀ(sprỏ));
				spr\u241F.ᜀ(A_1, spr_u1DB);
				spr_u24A.ᜁ(spr_u1DB);
				A_0.ᜁ(spr_u24A);
				num = 5;
			}
			return;
		}
		}
	}

	// Token: 0x060036C6 RID: 14022 RVA: 0x0033501C File Offset: 0x0033401C
	private static bool ᜂ(spr\u1937 A_0)
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
		return false;
	}

	// Token: 0x060036C7 RID: 14023 RVA: 0x00335058 File Offset: 0x00334058
	private new static byte[] ᜁ(spr\u1937 A_0)
	{
		if (A_0.\u173F() == null)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				break;
			}
			return null;
		}
		return A_0.\u173F().ᜁ();
	}

	// Token: 0x060036C8 RID: 14024 RVA: 0x003350AC File Offset: 0x003340AC
	private new static void ᜀ(spr\u1937 A_0, spr\u1DB3 A_1)
	{
		ArrayList arrayList;
		for (;;)
		{
			arrayList = new ArrayList();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5A;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5A;
					default:
						if (false)
						{
						}
						if (A_0.\u1732() != Color.Empty)
						{
							num = 0;
							continue;
						}
						goto IL_88;
					}
					break;
				case 2:
					goto IL_86;
				}
				break;
				IL_5A:
				arrayList.Add(new sprℱ(spr\u2262.ᜀ(A_0.\u1732())));
				num = 2;
			}
		}
		IL_86:
		IL_88:
		if (true)
		{
		}
		spr\u232E[] array = new spr\u232E[arrayList.Count];
		arrayList.CopyTo(array);
		A_1.ᜀ(array);
	}

	// Token: 0x060036C9 RID: 14025 RVA: 0x00335164 File Offset: 0x00334164
	private new static spr\u22DC ᜀ(sprỏ A_0)
	{
		if (!A_0.\u1715())
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_38;
			}
			if (false)
			{
			}
			IL_38:
			return null;
		}
		return new spr\u22DC(spr\u2262.ᜀ(A_0.ᜋ()), 10);
	}

	// Token: 0x060036CA RID: 14026 RVA: 0x003351C0 File Offset: 0x003341C0
	private new void ᜀ(spr\u24A6 A_0, int A_1, spr\u236B A_2, spr\u2248 A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 18;
				for (;;)
				{
					spr\u1B70[] array;
					int num3;
					spr\u1B70[] array2;
					spr\u1B70 spr_u1B;
					switch (num2)
					{
					case 0:
						goto IL_18F;
					case 1:
						if (array[0].ᜉ() > 0)
						{
							num2 = 3;
							continue;
						}
						goto IL_A5;
					case 2:
						if (A_3 != null)
						{
							num2 = 9;
							continue;
						}
						goto IL_18F;
					case 3:
						A_0.ᜁ(array[0]);
						num2 = 15;
						continue;
					case 4:
						goto IL_150;
					case 5:
						num2 = 1;
						continue;
					case 6:
						if (array != null)
						{
							num2 = 5;
							continue;
						}
						goto IL_119;
					case 7:
						goto IL_119;
					case 8:
						if (num3 >= array2.Length)
						{
							num2 = 20;
							continue;
						}
						A_0.ᜁ(A_2.ᜀ(array2[num3], true, false));
						num3++;
						num2 = 16;
						continue;
					case 9:
						A_3.ᜁ(spr_u1B);
						num2 = 0;
						continue;
					case 10:
						num2 = 2;
						continue;
					case 11:
						if (array[1].ᜉ() > 0)
						{
							num2 = 12;
							continue;
						}
						goto IL_119;
					case 12:
						A_0.ᜁ(array[1]);
						num2 = 7;
						continue;
					case 13:
						return;
					case 14:
						if (num >= A_1)
						{
							num2 = 13;
							continue;
						}
						spr_u1B = (spr\u1B70)this.ᜀ.ᜏ().ᜀ(num);
						num2 = 17;
						continue;
					case 15:
						goto IL_A5;
					case 16:
						goto IL_1D6;
					case 17:
						if (spr_u1B.ᜆ() != null)
						{
							num2 = 10;
							continue;
						}
						goto IL_E6;
					case 18:
						goto IL_150;
					case 19:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							goto IL_1D6;
						}
						break;
					case 20:
						goto IL_E6;
					}
					break;
					IL_A5:
					num2 = 11;
					continue;
					IL_E6:
					num++;
					num2 = 4;
					continue;
					IL_119:
					array2 = sprὝ.ᜃ(spr_u1B);
					num3 = 0;
					num2 = 19;
					continue;
					IL_150:
					num2 = 14;
					continue;
					IL_18F:
					if (true)
					{
					}
					array = spr\u220A.ᜁ(spr_u1B, this.ᜀ.ᜉ().\u1736().ᜎ());
					num2 = 6;
					continue;
					IL_1D6:
					num2 = 8;
				}
			}
			return;
		}
	}

	// Token: 0x060036CB RID: 14027 RVA: 0x0033545C File Offset: 0x0033445C
	private new void ᜀ(spr\u1937 A_0)
	{
		if (this.ᜀ.ᜎ() == null)
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
				break;
			}
			if (true)
			{
			}
			return;
		}
		PointF[] a_ = new PointF[]
		{
			PointF.Empty,
			new PointF((float)A_0.ព().Width, 0f),
			new PointF((float)A_0.ព().Width, (float)A_0.ព().Height),
			new PointF(0f, (float)A_0.ព().Height)
		};
		this.ᜀ.ᜈ().ᜀ(a_, false);
		spr\u1B70 spr_u1B = new spr\u1B70(null);
		spr_u1B.ᜀ(this.ᜀ.ᜎ());
		spr_u1B.ᜁ(spr\u1926.ᜀ(a_, true));
		this.ᜀ.ᜏ().ᜁ(spr_u1B);
	}

	// Token: 0x060036CC RID: 14028 RVA: 0x00335588 File Offset: 0x00334588
	private new void ᜁ()
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
		double num = this.ᜀ.ᜉ().ᝅ();
		PointF pointF = this.ᜀ.ᜈ().ᜁ();
		float a_ = (float)(num / (double)pointF.Y);
		float a_2 = (float)(num / (double)pointF.Y);
		float a_3 = (float)(num / (double)pointF.X);
		float a_4 = (float)(num / (double)pointF.X);
		PointF[] array = spr\u241F.ᜀ(this.ᜀ.ᜉ().ព(), a_, a_4, a_2, a_3);
		this.ᜀ.ᜈ().ᜀ(array, false);
		sprύ.ᜀ(this.ᜀ, array[0], array[1]);
		sprύ.ᜀ(this.ᜀ, array[2], array[3]);
		sprύ.ᜀ(this.ᜀ, array[4], array[5]);
		sprύ.ᜀ(this.ᜀ, array[6], array[7]);
	}

	// Token: 0x060036CD RID: 14029 RVA: 0x003356DC File Offset: 0x003346DC
	private new void ᜀ(spr\u1F9B A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u2587 spr_u2;
			spr\u2587 spr_u3;
			spr\u2587 spr_u4;
			spr\u2587 spr_u5;
			PointF pointF;
			float num5;
			float num6;
			float num7;
			for (;;)
			{
				spr\u2451 spr_u = this.ᜀ.ᜉ().ᜮ().ᜊ();
				spr_u2 = spr_u.ᜁ(BorderType.Top);
				spr_u3 = spr_u.ᜁ(BorderType.Left);
				spr_u4 = spr_u.ᜁ(BorderType.Right);
				spr_u5 = spr_u.ᜁ(BorderType.Bottom);
				pointF = this.ᜀ.ᜈ().ᜁ();
				int num = 5;
				for (;;)
				{
					float num2;
					float num3;
					float num4;
					switch (num)
					{
					case 0:
						if (!A_0.ᜆ())
						{
							if (true)
							{
							}
							num = 12;
							continue;
						}
						num = 15;
						continue;
					case 1:
						num2 = 0f;
						goto IL_1A9;
					case 2:
						if (!A_0.ᜋ())
						{
							num = 6;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_100;
						default:
							if (false)
							{
							}
							num = 13;
							continue;
						}
						break;
					case 3:
						goto IL_CE;
					case 4:
						num3 = 0f;
						goto IL_15F;
					case 5:
						if (!A_0.ᜈ())
						{
							num = 11;
							continue;
						}
						num = 4;
						continue;
					case 6:
						num = 3;
						continue;
					case 7:
						goto IL_100;
					case 8:
						num4 = spr_u3.\u171E() / pointF.X;
						goto IL_1D5;
					case 9:
						if (!A_0.ᜄ())
						{
							num = 10;
							continue;
						}
						num = 1;
						continue;
					case 10:
						num = 14;
						continue;
					case 11:
						num = 7;
						continue;
					case 12:
						num = 8;
						continue;
					case 13:
						goto IL_138;
					case 14:
						num2 = spr_u5.\u171E() / pointF.Y;
						goto IL_1A9;
					case 15:
						num4 = 0f;
						goto IL_1D5;
					}
					break;
					IL_15F:
					num5 = num3;
					num = 9;
					continue;
					IL_100:
					num3 = spr_u2.\u171E() / pointF.Y;
					goto IL_15F;
					IL_1A9:
					num6 = num2;
					num = 0;
					continue;
					IL_1D5:
					num7 = num4;
					num = 2;
				}
			}
			IL_CE:
			float num8 = spr_u4.\u171E() / pointF.X;
			goto IL_225;
			IL_138:
			num8 = 0f;
			IL_225:
			float num9 = num8;
			PointF[] array = new PointF[]
			{
				new PointF(-num7, -num5),
				new PointF((float)this.ᜀ.ᜉ().ព().Width + num9, (float)this.ᜀ.ᜉ().ព().Height + num6)
			};
			this.ᜀ.ᜈ().ᜀ(array, false);
			RectangleF a_ = new RectangleF(array[0], new SizeF(array[1].X - array[0].X, array[1].Y - array[0].Y));
			spr᧑ spr᧑ = new spr᧑(BorderGridType.Page);
			spr᧑.ᜄ();
			spr᧑.ᜁ(a_, spr_u3, spr_u4, spr_u2, spr_u5);
			spr᧑.ᜀ(this.ᜀ.ᜏ());
			return;
		}
		}
	}

	// Token: 0x060036CE RID: 14030 RVA: 0x00335A10 File Offset: 0x00334A10
	private new static PointF[] ᜀ(Size A_0, float A_1, float A_2, float A_3, float A_4)
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
		float num = A_1 * 0.5f;
		float num2 = A_4 * 0.5f;
		float num3 = A_2 * 0.5f;
		float num4 = A_3 * 0.5f;
		return new PointF[]
		{
			new PointF(-A_4, -num),
			new PointF((float)A_0.Width + A_2, -num),
			new PointF((float)A_0.Width + num3, 0f),
			new PointF((float)A_0.Width + num3, (float)A_0.Height),
			new PointF(-A_4, (float)A_0.Height + num4),
			new PointF((float)A_0.Width + A_2, (float)A_0.Height + num4),
			new PointF(-num2, 0f),
			new PointF(-num2, (float)A_0.Height)
		};
	}

	// Token: 0x060036CF RID: 14031 RVA: 0x00335B6C File Offset: 0x00334B6C
	private new void ᜀ(sprỬ A_0, ref int A_1)
	{
		int a_ = 5;
		for (;;)
		{
			PathType pathType = A_0.ᜀ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch (pathType)
					{
					case PathType.Unknown:
						return;
					case PathType.LineTo:
						goto IL_B2;
					case PathType.CurveTo:
						goto IL_93;
					case PathType.MoveTo:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_228;
						default:
							goto IL_133;
						}
						break;
					case PathType.Close:
						goto IL_235;
					case PathType.End:
						goto IL_19F;
					default:
						num = 1;
						continue;
					}
					break;
				case 1:
					num = 2;
					continue;
				case 2:
					switch (pathType)
					{
					case PathType.EscapeBase:
					case PathType.QuadraticBezier:
					case PathType.EscapeAutoLine:
					case PathType.EscapeAutoCurve:
					case PathType.EscapeCornerLine:
					case PathType.EscapeCornerCurve:
					case PathType.EscapeSmoothLine:
					case PathType.EscapeSmoothCurve:
					case PathType.EscapeSymmetricLine:
					case PathType.EscapeSymmetricCurve:
					case PathType.EscapeFreeForm:
					case PathType.LineColor:
						return;
					case PathType.AngleEllipseTo:
						goto IL_24C;
					case PathType.AngleEllipse:
						goto IL_2B4;
					case PathType.ArcTo:
						goto IL_290;
					case PathType.Arc:
						goto IL_104;
					case PathType.ClockwiseArcTo:
						goto IL_BE;
					case PathType.ClockwiseArc:
						goto IL_7A;
					case PathType.EllipticalQuadrantX:
						goto IL_61;
					case PathType.EllipticalQuadrantY:
						goto IL_277;
					case PathType.NoFill:
						goto IL_A0;
					case PathType.NoLine:
						goto IL_179;
					case PathType.FillColor:
						goto IL_E2;
					}
					goto IL_228;
				case 3:
					goto IL_247;
				case 4:
					num = 3;
					continue;
				}
				break;
				IL_228:
				num = 4;
			}
		}
		IL_61:
		this.ᜀ.ᜀ(null);
		A_1 = this.ᜂ(A_0, A_1, true);
		return;
		IL_7A:
		this.ᜀ.\u1717();
		A_1 = this.ᜀ(A_0, A_1, true, false);
		return;
		IL_93:
		A_1 = this.ᜀ(A_0, A_1, false);
		return;
		IL_A0:
		this.ᜀ.ᜊ().ᜀ(null);
		return;
		IL_B2:
		A_1 = this.ᜀ(A_0, A_1);
		return;
		IL_BE:
		this.ᜀ.ᜀ(null);
		A_1 = this.ᜀ(A_0, A_1, true, this.ᜀ.\u1718());
		return;
		IL_E2:
		A_1++;
		this.ᜀ.ᜊ().ᜀ(this.ᜀ.ᜎ());
		return;
		IL_104:
		this.ᜀ.\u1717();
		A_1 = this.ᜀ(A_0, A_1, false, false);
		return;
		IL_133:
		if (false)
		{
		}
		this.ᜀ.\u1717();
		this.ᜀ.ᜀ(this.ᜀ.ᜆ()[A_1]);
		this.ᜀ.ᜀ(true);
		A_1++;
		return;
		IL_179:
		this.ᜀ.ᜊ().ᜀ(null);
		return;
		IL_19F:
		this.ᜀ.\u1715();
		return;
		IL_235:
		this.ᜀ();
		return;
		IL_247:
		throw new ArgumentOutOfRangeException(ClipboardData.b("᭪౬᭮ᥰ㩲᭴ᅶᙸ", a_));
		IL_24C:
		if (true)
		{
		}
		this.ᜀ.ᜀ(null);
		A_1 = this.ᜁ(A_0, A_1, this.ᜀ.\u1718());
		return;
		IL_277:
		this.ᜀ.ᜀ(null);
		A_1 = this.ᜂ(A_0, A_1, false);
		return;
		IL_290:
		this.ᜀ.ᜀ(null);
		A_1 = this.ᜀ(A_0, A_1, false, this.ᜀ.\u1718());
		return;
		IL_2B4:
		this.ᜀ.\u1717();
		A_1 = this.ᜁ(A_0, A_1, false);
	}

	// Token: 0x060036D0 RID: 14032 RVA: 0x00335E48 File Offset: 0x00334E48
	private int ᜂ(sprỬ A_0, int A_1, bool A_2)
	{
		for (;;)
		{
			int num = 0;
			if (true)
			{
			}
			int num2 = 1;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_2C;
				case 1:
					goto IL_2C;
				case 2:
					goto IL_45;
				case 3:
					if (num >= A_0.ᜅ())
					{
						num2 = 2;
						continue;
					}
					this.ᜀ(this.ᜁ(ref A_1, A_2));
					A_2 = !A_2;
					num++;
					num2 = 0;
					continue;
				}
				break;
				IL_2C:
				num2 = 3;
			}
		}
		IL_45:
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
		this.ᜀ.ᜀ(false);
		return A_1;
	}

	// Token: 0x060036D1 RID: 14033 RVA: 0x00335EF8 File Offset: 0x00334EF8
	private new spr\u1A68[] ᜁ(ref int A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			float num4;
			float num5;
			PointF a_;
			SizeF a_2;
			for (;;)
			{
				PointF[] array = new PointF[]
				{
					this.ᜀ.\u1713(),
					this.ᜀ.ᜆ()[A_0]
				};
				this.ᜀ.ᜀ(this.ᜀ.ᜆ()[A_0]);
				A_0++;
				float num = Math.Abs(array[1].X - array[0].X);
				float num2 = Math.Abs(array[1].Y - array[0].Y);
				int num3 = 25;
				for (;;)
				{
					PointF pointF;
					switch (num3)
					{
					case 0:
						if (array[0].X > array[1].X)
						{
							num3 = 16;
							continue;
						}
						goto IL_33B;
					case 1:
						num3 = 4;
						continue;
					case 2:
						if (array[0].Y > array[1].Y)
						{
							goto IL_384;
						}
						goto IL_2E1;
					case 3:
						goto IL_4B2;
					case 4:
						pointF = new PointF(array[1].X - num, array[0].Y - num2);
						goto IL_3B1;
					case 5:
						num3 = 15;
						continue;
					case 6:
						if (A_1)
						{
							num3 = 11;
							continue;
						}
						num4 = 180f;
						num5 = 90f;
						num3 = 27;
						continue;
					case 7:
						goto IL_4B2;
					case 8:
						num3 = 29;
						continue;
					case 9:
						pointF = new PointF(array[0].X - num, array[1].Y - num2);
						goto IL_3B1;
					case 10:
						if (A_1)
						{
							num3 = 20;
							continue;
						}
						num4 = 0f;
						num5 = 90f;
						num3 = 17;
						continue;
					case 11:
						num4 = 90f;
						num5 = -90f;
						num3 = 30;
						continue;
					case 12:
						num4 = 270f;
						num5 = 90f;
						num3 = 21;
						continue;
					case 13:
						goto IL_4B2;
					case 14:
						if (array[0].X < array[1].X)
						{
							num3 = 28;
							continue;
						}
						goto IL_2E1;
					case 15:
						if (array[0].Y < array[1].Y)
						{
							num3 = 8;
							continue;
						}
						goto IL_259;
					case 16:
						num3 = 22;
						continue;
					case 17:
						goto IL_4B2;
					case 18:
						if (A_1)
						{
							num3 = 23;
							continue;
						}
						num4 = 0f;
						num5 = -90f;
						num3 = 7;
						continue;
					case 19:
						if (true)
						{
						}
						if (array[0].X < array[1].X)
						{
							num3 = 5;
							continue;
						}
						goto IL_259;
					case 20:
						num4 = 270f;
						num5 = -90f;
						num3 = 13;
						continue;
					case 21:
						goto IL_4B2;
					case 22:
						if (array[0].Y > array[1].Y)
						{
							num3 = 31;
							continue;
						}
						goto IL_33B;
					case 23:
						num4 = 90f;
						num5 = 90f;
						num3 = 26;
						continue;
					case 24:
						num3 = 6;
						continue;
					case 25:
						if (!A_1)
						{
							num3 = 1;
							continue;
						}
						num3 = 9;
						continue;
					case 26:
						goto IL_4B2;
					case 27:
						goto IL_4B2;
					case 28:
						num3 = 2;
						continue;
					case 29:
						if (A_1)
						{
							num3 = 12;
							continue;
						}
						num4 = 180f;
						num5 = -90f;
						num3 = 3;
						continue;
					case 30:
						goto IL_4B2;
					case 31:
						num3 = 18;
						continue;
					}
					break;
					IL_259:
					num3 = 14;
					continue;
					IL_2E1:
					num3 = 0;
					continue;
					IL_33B:
					num3 = 10;
					continue;
					IL_384:
					num3 = 24;
					continue;
					IL_4B2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_384;
					default:
						goto IL_4C8;
					}
					IL_3B1:
					a_ = pointF;
					a_2 = new SizeF(num * 2f, num2 * 2f);
					num3 = 19;
				}
			}
			IL_4C8:
			if (false)
			{
			}
			spr\u220E spr_u220E = new spr\u220E();
			spr_u220E.ᜀ(a_);
			spr_u220E.ᜀ(a_2);
			spr_u220E.ᜃ((double)num4);
			spr_u220E.ᜂ((double)num5);
			return this.ᜀ(spr_u220E);
		}
		}
	}

	// Token: 0x060036D2 RID: 14034 RVA: 0x00336408 File Offset: 0x00335408
	private new int ᜀ(sprỬ A_0, int A_1)
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
				IL_171:
				if (!this.ᜀ.\u1718())
				{
					goto IL_120;
				}
				num = 7;
				break;
			default:
				if (false)
				{
				}
				goto IL_57;
			}
			sprᴎ sprᴎ;
			int num2;
			int num3;
			PointF[] array;
			for (;;)
			{
				IL_2C:
				switch (num)
				{
				case 0:
					if (sprᴎ == null)
					{
						num = 3;
						continue;
					}
					goto IL_120;
				case 1:
					if (num2 >= num3)
					{
						if (true)
						{
						}
						num = 5;
						continue;
					}
					this.ᜀ.ᜀ(this.ᜀ.ᜆ()[A_1]);
					array[0] = this.ᜀ.\u1713();
					this.ᜀ.ᜈ().ᜀ(array, false);
					sprᴎ.ᜀ().Add(array[0]);
					A_1++;
					num2++;
					num = 8;
					continue;
				case 2:
					goto IL_207;
				case 3:
					sprᴎ = new sprᴎ();
					this.ᜀ.ᜅ().ᜁ(sprᴎ);
					this.ᜀ.ᜀ(sprᴎ);
					num = 4;
					continue;
				case 4:
					goto IL_171;
				case 5:
					return A_1;
				case 6:
					goto IL_18C;
				case 7:
					array[0] = this.ᜀ.\u1713();
					this.ᜀ.ᜈ().ᜀ(array, false);
					sprᴎ.ᜀ().Add(array[0]);
					num = 2;
					continue;
				case 8:
					goto IL_18C;
				}
				goto IL_57;
				IL_18C:
				num = 1;
			}
			return A_1;
			IL_207:
			goto IL_120;
			IL_57:
			array = new PointF[]
			{
				PointF.Empty
			};
			sprᴎ = (this.ᜀ.\u1716() as sprᴎ);
			num = 0;
			goto IL_2C;
			IL_120:
			num3 = A_0.ᜅ();
			num2 = 0;
			num = 6;
			goto IL_2C;
		}
		}
	}

	// Token: 0x060036D3 RID: 14035 RVA: 0x00336624 File Offset: 0x00335624
	private new int ᜀ(sprỬ A_0, int A_1, bool A_2, bool A_3)
	{
		for (;;)
		{
			int num = A_0.ᜅ() / 4;
			int num2 = 0;
			if (true)
			{
			}
			int num3 = 0;
			for (;;)
			{
				spr\u1A68[] a_;
				switch (num3)
				{
				case 0:
					goto IL_8D;
				case 1:
					if (A_3)
					{
						num3 = 2;
						continue;
					}
					goto IL_41;
				case 2:
					this.ᜁ(a_);
					A_3 = false;
					num3 = 4;
					continue;
				case 3:
					if (num2 >= num)
					{
						num3 = 5;
						continue;
					}
					a_ = this.ᜀ(ref A_1, A_2);
					num3 = 1;
					continue;
				case 4:
					goto IL_68;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_68;
					default:
						goto IL_BD;
					}
					break;
				case 6:
					goto IL_8D;
				}
				break;
				IL_41:
				this.ᜀ(a_);
				num2++;
				num3 = 6;
				continue;
				IL_68:
				goto IL_41;
				IL_8D:
				num3 = 3;
			}
		}
		IL_BD:
		if (false)
		{
		}
		this.ᜀ.ᜀ(false);
		return A_1;
	}

	// Token: 0x060036D4 RID: 14036 RVA: 0x00336710 File Offset: 0x00335710
	private new void ᜁ(spr\u1A68[] A_0)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 6;
			for (;;)
			{
				PointF pointF;
				switch (num)
				{
				case 0:
					goto IL_67;
				case 1:
					return;
				case 2:
					if (pointF.X == this.ᜀ.\u1713().X)
					{
						num = 5;
						continue;
					}
					goto IL_67;
				case 3:
					pointF = A_0[0].ᜂ();
					num = 2;
					continue;
				case 4:
					if (pointF.Y != this.ᜀ.\u1713().Y)
					{
						num = 0;
						continue;
					}
					return;
				case 5:
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_11B;
						}
					}
					IL_11B:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				if (this.ᜀ.ᜅ().ᜉ() == 0)
				{
					num = 3;
					continue;
				}
				break;
				IL_67:
				PointF[] array = new PointF[]
				{
					PointF.Empty
				};
				array[0] = this.ᜀ.\u1713();
				this.ᜀ.ᜈ().ᜀ(array, false);
				sprᴎ sprᴎ = new sprᴎ();
				sprᴎ.ᜀ().Add(array[0]);
				sprᴎ.ᜀ().Add(pointF);
				this.ᜀ.ᜅ().ᜁ(sprᴎ);
				num = 1;
			}
			return;
		}
		}
	}

	// Token: 0x060036D5 RID: 14037 RVA: 0x003368D0 File Offset: 0x003358D0
	private new void ᜀ(spr\u1A68[] A_0)
	{
		for (;;)
		{
			int num = 0;
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_24;
				case 1:
					goto IL_3A;
				case 2:
					goto IL_24;
				case 3:
				{
					if (num >= A_0.Length)
					{
						num2 = 1;
						continue;
					}
					spr\u17F0 spr_u17F = new spr\u17F0();
					spr_u17F.ᜀ(A_0[num]);
					this.ᜀ.ᜅ().ᜁ(spr_u17F);
					num++;
					num2 = 0;
					continue;
				}
				}
				break;
				IL_24:
				num2 = 3;
			}
		}
		IL_3A:
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (true)
			{
			}
			if (false)
			{
			}
			break;
		}
	}

	// Token: 0x060036D6 RID: 14038 RVA: 0x00336988 File Offset: 0x00335988
	private new int ᜁ(sprỬ A_0, int A_1, bool A_2)
	{
		for (;;)
		{
			int num = A_0.ᜅ() / 3;
			int num2 = 0;
			int num3 = 6;
			for (;;)
			{
				spr\u1A68[] a_;
				switch (num3)
				{
				case 0:
					this.ᜁ(a_);
					A_2 = false;
					num3 = 2;
					continue;
				case 1:
					if (num2 >= num)
					{
						num3 = 5;
						continue;
					}
					a_ = this.ᜀ(ref A_1);
					num3 = 4;
					continue;
				case 2:
					goto IL_68;
				case 3:
					goto IL_8B;
				case 4:
					if (A_2)
					{
						num3 = 0;
						continue;
					}
					goto IL_39;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_68;
					default:
						goto IL_BB;
					}
					break;
				case 6:
					goto IL_8B;
				}
				break;
				IL_39:
				if (true)
				{
				}
				this.ᜀ(a_);
				num2++;
				num3 = 3;
				continue;
				IL_68:
				goto IL_39;
				IL_8B:
				num3 = 1;
			}
		}
		IL_BB:
		if (false)
		{
		}
		this.ᜀ.ᜀ(false);
		return A_1;
	}

	// Token: 0x060036D7 RID: 14039 RVA: 0x00336A70 File Offset: 0x00335A70
	private new spr\u1A68[] ᜀ(ref int A_0)
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
		PointF[] array = new PointF[]
		{
			this.ᜀ.ᜆ()[A_0]
		};
		PointF pointF = array[0];
		A_0++;
		array[0] = this.ᜀ.ᜆ()[A_0];
		PointF pointF2 = array[0];
		A_0++;
		PointF pointF3 = this.ᜀ.ᜆ()[A_0];
		A_0++;
		spr\u220E spr_u220E = new spr\u220E();
		spr_u220E.ᜃ((double)(360f - pointF3.X / 65536f));
		spr_u220E.ᜂ((double)(-(double)pointF3.Y / 65536f));
		spr_u220E.ᜀ(new SizeF(pointF2.X * 2f, pointF2.Y * 2f));
		spr_u220E.ᜀ(new PointF(pointF.X - pointF2.X, pointF.Y - pointF2.Y));
		return this.ᜀ(spr_u220E);
	}

	// Token: 0x060036D8 RID: 14040 RVA: 0x00336BD0 File Offset: 0x00335BD0
	private new int ᜀ(sprỬ A_0, int A_1, bool A_2)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = 0;
				int num2 = 9;
				for (;;)
				{
					PointF[] array;
					switch (num2)
					{
					case 0:
						goto IL_203;
					case 1:
						goto IL_AE;
					case 2:
						goto IL_1F1;
					case 3:
						goto IL_203;
					case 4:
					{
						int num3;
						if (num3 >= 4)
						{
							num2 = 7;
							continue;
						}
						PointF pointF = array[num3];
						PointF pointF3;
						PointF pointF2 = new PointF(pointF.X + pointF3.X, pointF.Y + pointF3.Y);
						array[num3] = pointF2;
						num3++;
						num2 = 0;
						continue;
					}
					case 5:
					{
						int num3 = 1;
						num2 = 3;
						continue;
					}
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AE;
						default:
						{
							if (false)
							{
							}
							if (num >= A_0.ᜅ())
							{
								num2 = 2;
								continue;
							}
							array = new PointF[]
							{
								PointF.Empty,
								PointF.Empty,
								PointF.Empty,
								PointF.Empty
							};
							Array.Copy(this.ᜀ.ᜆ(), A_1, array, 1, 3);
							PointF pointF3 = this.ᜀ.\u1713();
							array[0] = pointF3;
							num2 = 8;
							continue;
						}
						}
						break;
					case 7:
						goto IL_59;
					case 8:
						if (A_2)
						{
							num2 = 5;
							continue;
						}
						goto IL_59;
					case 9:
						goto IL_1A9;
					}
					break;
					IL_59:
					A_1 += 3;
					this.ᜀ.ᜀ(array[3]);
					this.ᜀ.ᜈ().ᜀ(array, false);
					this.ᜀ.ᜅ().ᜁ(new spr\u17F0(array));
					num++;
					num2 = 1;
					continue;
					IL_1A9:
					if (true)
					{
					}
					num2 = 6;
					continue;
					IL_AE:
					goto IL_1A9;
					IL_203:
					num2 = 4;
				}
			}
			IL_1F1:
			this.ᜀ.ᜀ(null);
			return A_1;
		}
	}

	// Token: 0x060036D9 RID: 14041 RVA: 0x00336E14 File Offset: 0x00335E14
	private new void ᜀ()
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
		this.ᜀ.ᜅ().ᜀ(true);
	}

	// Token: 0x060036DA RID: 14042 RVA: 0x00336E60 File Offset: 0x00335E60
	private new spr\u1A68[] ᜀ(ref int A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			float x;
			float y;
			float x2;
			float y2;
			double a_;
			double a_2;
			for (;;)
			{
				PointF[] array = new PointF[]
				{
					PointF.Empty,
					PointF.Empty,
					PointF.Empty,
					PointF.Empty
				};
				Array.Copy(this.ᜀ.ᜆ(), A_0, array, 0, 4);
				A_0 += 4;
				PointF pointF = array[0];
				PointF pointF2 = array[1];
				x = pointF.X;
				y = pointF.Y;
				x2 = pointF2.X;
				y2 = pointF2.Y;
				int num = 10;
				for (;;)
				{
					double num2;
					double num3;
					double num4;
					double num5;
					PointF pointF3;
					PointF pointF4;
					switch (num)
					{
					case 0:
						num2 = 360.0 - num2;
						num = 3;
						continue;
					case 1:
						num = 5;
						continue;
					case 2:
						num = 15;
						continue;
					case 3:
						goto IL_3C0;
					case 4:
						if (num2 <= num3)
						{
							num = 2;
							continue;
						}
						num = 6;
						continue;
					case 5:
						if (num2 <= num3)
						{
							num = 7;
							continue;
						}
						num = 11;
						continue;
					case 6:
						num4 = num2 - 360.0 - num3;
						goto IL_44C;
					case 7:
						num = 22;
						continue;
					case 8:
						x = pointF2.X;
						x2 = pointF.X;
						num = 16;
						continue;
					case 9:
						goto IL_427;
					case 10:
						if (pointF.X > pointF2.X)
						{
							num = 8;
							continue;
						}
						goto IL_390;
					case 11:
						num5 = num2 - num3;
						goto IL_419;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_38B;
						default:
							goto IL_470;
						}
						break;
					case 13:
						if (A_1)
						{
							num = 1;
							continue;
						}
						num = 4;
						continue;
					case 14:
						if (pointF3.Y < 0f)
						{
							num = 0;
							continue;
						}
						goto IL_3C0;
					case 15:
						num4 = num2 - num3;
						goto IL_44C;
					case 16:
						goto IL_390;
					case 17:
						if (pointF4.Y < 0f)
						{
							num = 20;
							continue;
						}
						goto IL_195;
					case 18:
						if (pointF.Y > pointF2.Y)
						{
							num = 23;
							continue;
						}
						goto IL_24E;
					case 19:
						if (true)
						{
						}
						goto IL_24E;
					case 20:
						num3 = 360.0 - num3;
						num = 21;
						continue;
					case 21:
						goto IL_38B;
					case 22:
						num5 = 360.0 - num3 + num2;
						goto IL_419;
					case 23:
						y = pointF2.Y;
						y2 = pointF.Y;
						num = 19;
						continue;
					}
					break;
					IL_195:
					double num6 = (double)pointF3.X;
					double d = num6 / Math.Sqrt((double)pointF3.X * (double)pointF3.X + (double)pointF3.Y * (double)pointF3.Y);
					num2 = spr\u2109.ᜃ(Math.Acos(d));
					num = 14;
					continue;
					IL_38B:
					goto IL_195;
					IL_24E:
					float num7 = (x2 - x) * 0.5f;
					float num8 = (y2 - y) * 0.5f;
					float num9 = x + num7;
					float num10 = y + num8;
					pointF4 = new PointF(array[2].X - num9, array[2].Y - num10);
					pointF3 = new PointF(array[3].X - num9, array[3].Y - num10);
					double num11 = (double)pointF4.X;
					double d2 = num11 / Math.Sqrt((double)pointF4.X * (double)pointF4.X + (double)pointF4.Y * (double)pointF4.Y);
					num3 = spr\u2109.ᜃ(Math.Acos(d2));
					num = 17;
					continue;
					IL_390:
					num = 18;
					continue;
					IL_3C0:
					a_ = num3;
					num = 13;
					continue;
					IL_419:
					a_2 = num5;
					num = 9;
					continue;
					IL_44C:
					a_2 = num4;
					num = 12;
				}
			}
			IL_427:
			goto IL_478;
			IL_470:
			if (false)
			{
			}
			IL_478:
			spr\u220E spr_u220E = new spr\u220E();
			spr_u220E.ᜃ(a_);
			spr_u220E.ᜂ(a_2);
			spr_u220E.ᜀ(new SizeF(x2 - x, y2 - y));
			spr_u220E.ᜀ(new PointF(x, y));
			return this.ᜀ(spr_u220E);
		}
		}
	}

	// Token: 0x060036DB RID: 14043 RVA: 0x0033732C File Offset: 0x0033632C
	private new spr\u1A68[] ᜀ(spr\u220E A_0)
	{
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			spr\u1A68[] array;
			for (;;)
			{
				array = A_0.ᜃ();
				PointF[] array2 = new PointF[array.Length * 4];
				int num = 0;
				int num2 = 0;
				int num3 = 2;
				for (;;)
				{
					switch (num3)
					{
					case 0:
						goto IL_1A1;
					case 1:
					{
						int num4;
						if (num4 < array.Length)
						{
							spr\u1A68 spr_u1A = default(spr\u1A68);
							spr_u1A.ᜁ(array2[num]);
							num++;
							spr_u1A.ᜃ(array2[num]);
							num++;
							spr_u1A.ᜂ(array2[num]);
							num++;
							spr_u1A.ᜀ(array2[num]);
							num++;
							array[num4] = spr_u1A;
							num4++;
							num3 = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1E1;
						default:
							if (false)
							{
							}
							num3 = 3;
							continue;
						}
						break;
					}
					case 2:
						goto IL_1E1;
					case 3:
						return array;
					case 4:
						goto IL_1E1;
					case 5:
					{
						this.ᜀ.ᜈ().ᜀ(array2, false);
						num = 0;
						int num4 = 0;
						num3 = 7;
						continue;
					}
					case 6:
						if (num2 >= array.Length)
						{
							num3 = 5;
							continue;
						}
						array2[num] = array[num2].ᜂ();
						num++;
						array2[num] = array[num2].ᜄ();
						num++;
						array2[num] = array[num2].ᜃ();
						num++;
						array2[num] = array[num2].ᜀ();
						num++;
						num2++;
						num3 = 4;
						continue;
					case 7:
						goto IL_1A1;
					}
					break;
					IL_1A1:
					num3 = 1;
					continue;
					IL_1E1:
					num3 = 6;
				}
			}
			return array;
		}
		}
	}

	// Token: 0x040029D0 RID: 10704
	private new sprṏ ᜀ;

	// Token: 0x040029D1 RID: 10705
	private new readonly sprᣛ ᜁ;

	// Token: 0x040029D2 RID: 10706
	private spr\u1F9B ᜂ;

	// Token: 0x040029D3 RID: 10707
	private readonly bool ᜃ;
}
