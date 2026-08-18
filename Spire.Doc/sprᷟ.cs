using System;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020001E0 RID: 480
internal class spr\u1DDF
{
	// Token: 0x060014DF RID: 5343 RVA: 0x001536D8 File Offset: 0x001526D8
	internal spr\u1DDF(sprᩍ A_0, sprᤎ A_1)
	{
		this.ᜀ = A_1;
		this.ᜁ = A_0;
	}

	// Token: 0x060014E0 RID: 5344 RVA: 0x00153724 File Offset: 0x00152724
	internal void ᜀ(int A_0, object A_1)
	{
		for (;;)
		{
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					num = 3;
					continue;
				case 2:
					if (A_0 != 507)
					{
						num = 0;
						continue;
					}
					goto IL_DE;
				case 3:
					switch (A_0)
					{
					case 378:
						goto IL_1EF;
					case 379:
						goto IL_147;
					case 380:
						goto IL_190;
					case 381:
						goto IL_113;
					case 382:
						goto IL_1C3;
					case 383:
						goto IL_120;
					default:
						num = 4;
						continue;
					}
					break;
				case 4:
					num = 2;
					continue;
				case 5:
					switch (A_0)
					{
					case 325:
						goto IL_12D;
					case 326:
						goto IL_EB;
					case 327:
					case 328:
					case 329:
					case 330:
					case 331:
					case 332:
					case 333:
					case 334:
					case 335:
					case 336:
						goto IL_209;
					case 337:
						goto IL_F9;
					case 338:
						goto IL_A0;
					case 339:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							goto IL_C3;
						}
						break;
					case 340:
						goto IL_1FC;
					case 341:
						goto IL_13A;
					case 342:
						goto IL_106;
					case 343:
						goto IL_1D0;
					case 344:
						goto IL_1DD;
					default:
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_A0:
		this.ᜎ = (int[])A_1;
		return;
		IL_C3:
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜉ = (int)A_1;
		return;
		IL_DE:
		this.ᜂ = (bool)A_1;
		return;
		IL_EB:
		this.ᜐ = (sprỬ[])A_1;
		return;
		IL_F9:
		this.\u170D = (spr\u2055[])A_1;
		return;
		IL_106:
		this.\u1712 = (spr\u2528[])A_1;
		return;
		IL_113:
		this.ᜈ = (bool)A_1;
		return;
		IL_120:
		this.ᜃ = (bool)A_1;
		return;
		IL_12D:
		this.ᜏ = (spr\u2055[])A_1;
		return;
		IL_13A:
		this.\u1713 = (sprᥴ[])A_1;
		return;
		IL_147:
		this.ᜆ = (bool)A_1;
		return;
		IL_190:
		this.ᜄ = (bool)A_1;
		return;
		IL_1C3:
		this.ᜇ = (bool)A_1;
		return;
		IL_1D0:
		this.ᜌ = (spr\u1D34[])A_1;
		return;
		IL_1DD:
		this.ᜋ = sprᥜ.ᜀ((ConnectionSiteType)A_1);
		return;
		IL_1EF:
		this.ᜅ = (bool)A_1;
		return;
		IL_1FC:
		this.ᜊ = (int)A_1;
		return;
		IL_209:
		this.ᜑ[A_0 - 327] = sprᜌ.\u170D((int)A_1);
	}

	// Token: 0x060014E1 RID: 5345 RVA: 0x00153954 File Offset: 0x00152954
	internal void ᜆ()
	{
		int a_ = 1;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_B0:
			this.ᜑ[2] = ClipboardData.b("剦嵨孪嵬", a_);
			if (true)
			{
			}
			num = 4;
			break;
		default:
			if (false)
			{
			}
			num = 12;
			break;
		}
		StringBuilder stringBuilder;
		for (;;)
		{
			int num2;
			switch (num)
			{
			case 0:
				goto IL_160;
			case 1:
				goto IL_185;
			case 2:
				if (num2 > 9)
				{
					num = 0;
					continue;
				}
				num = 3;
				continue;
			case 3:
				if (this.ᜑ[num2] != null)
				{
					num = 8;
					continue;
				}
				goto IL_10B;
			case 4:
				goto IL_1B6;
			case 5:
				return;
			case 6:
				goto IL_142;
			case 7:
				if (this.ᜑ[2] == null)
				{
					num = 1;
					continue;
				}
				goto IL_1B6;
			case 8:
				stringBuilder.Append(this.ᜑ[num2]);
				num = 10;
				continue;
			case 9:
				num = 13;
				continue;
			case 10:
				goto IL_10B;
			case 11:
				goto IL_142;
			case 13:
				if (this.ᜑ[0] == null)
				{
					num = 14;
					continue;
				}
				goto IL_165;
			case 14:
				this.ᜑ[0] = ClipboardData.b("䩦塨婪婬噮䝰䝲䵴䝶", a_);
				num = 15;
				continue;
			case 15:
				goto IL_165;
			case 16:
				if (this.ᜁ.\u1774() == ShapeType.CustomShape)
				{
					num = 9;
					continue;
				}
				goto IL_1B6;
			}
			if (this.ᜁ.\u1774() == ShapeType.RoundRectangle)
			{
				num = 5;
				continue;
			}
			num = 16;
			continue;
			IL_10B:
			stringBuilder.Append(',');
			num2++;
			num = 11;
			continue;
			IL_142:
			num = 2;
			continue;
			IL_165:
			num = 7;
			continue;
			IL_1B6:
			stringBuilder = new StringBuilder();
			num2 = 0;
			num = 6;
		}
		return;
		IL_160:
		this.ᜀ.ᜀ(ClipboardData.b("٦൨Ū", a_), stringBuilder.ToString().TrimEnd(new char[]
		{
			','
		}));
		return;
		IL_185:
		goto IL_B0;
	}

	// Token: 0x060014E2 RID: 5346 RVA: 0x00153B88 File Offset: 0x00152B88
	internal void ᜃ()
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			StringBuilder stringBuilder;
			for (;;)
			{
				IL_17:
				int num = 9;
				for (;;)
				{
					int num4;
					switch (num)
					{
					case 0:
						num = 12;
						continue;
					case 1:
						return;
					case 2:
					{
						int num2;
						if (num2 <= 0)
						{
							goto IL_1FF;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_17;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					}
					case 3:
						if (this.ᜐ != null)
						{
							num = 6;
							continue;
						}
						return;
					case 4:
						goto IL_176;
					case 5:
						goto IL_2D0;
					case 6:
						num = 7;
						continue;
					case 7:
					{
						if (this.ᜐ.Length == 0)
						{
							num = 1;
							continue;
						}
						stringBuilder = new StringBuilder();
						int num3 = 0;
						num4 = 0;
						num = 4;
						continue;
					}
					case 8:
						stringBuilder.Remove(stringBuilder.Length - 1, 1);
						num = 15;
						continue;
					case 10:
						goto IL_176;
					case 11:
						goto IL_19C;
					case 12:
						if (true)
						{
						}
						if (this.ᜏ.Length == 0)
						{
							num = 17;
							continue;
						}
						num = 3;
						continue;
					case 13:
					{
						if (num4 >= this.ᜐ.Length)
						{
							num = 11;
							continue;
						}
						sprỬ sprỬ = this.ᜐ[num4];
						stringBuilder.Append(sprᥜ.ᜀ(sprỬ.ᜀ()));
						int num2 = sprỬ.ᜁ();
						int num5 = 0;
						num = 14;
						continue;
					}
					case 14:
						goto IL_96;
					case 15:
						goto IL_1FF;
					case 16:
						goto IL_96;
					case 17:
						goto IL_280;
					case 18:
						num = 2;
						continue;
					case 19:
					{
						int num3;
						if (num3 >= this.ᜏ.Length)
						{
							num = 5;
							continue;
						}
						spr\u2055 spr_u = this.ᜏ[num3];
						stringBuilder.Append(this.ᜁ(spr_u.ᜂ()));
						stringBuilder.Append(',');
						stringBuilder.Append(this.ᜁ(spr_u.ᜁ()));
						stringBuilder.Append(',');
						num3++;
						int num5;
						num5++;
						num = 16;
						continue;
					}
					case 20:
					{
						int num2;
						int num5;
						if (num5 >= num2)
						{
							num = 18;
							continue;
						}
						num = 19;
						continue;
					}
					}
					if (this.ᜏ != null)
					{
						num = 0;
						continue;
					}
					return;
					IL_96:
					num = 20;
					continue;
					IL_176:
					num = 13;
					continue;
					IL_1FF:
					num4++;
					num = 10;
				}
			}
			return;
			IL_19C:
			this.ᜀ.ᜁ(ClipboardData.b("ѳ᝵౷ቹ", a_), stringBuilder.ToString());
			return;
			IL_280:
			return;
			IL_2D0:
			throw new InvalidOperationException(ClipboardData.b("㩳᥵౷婹᥻ၽꢇ憎ﲍﾑﶗ뺝즟첡蒣솥춧얩솫쮭쒯삱춳隵좷\udbb9좻횽", a_));
		}
		}
	}

	// Token: 0x060014E3 RID: 5347 RVA: 0x00153E8C File Offset: 0x00152E8C
	private string ᜁ(sprṚ A_0)
	{
		int a_ = 16;
		while (A_0.ᜁ())
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
				return ClipboardData.b("㙵", a_) + A_0.ᜂ();
			}
		}
		return sprᜌ.ᜇ(A_0.ᜂ());
	}

	// Token: 0x060014E4 RID: 5348 RVA: 0x00153F04 File Offset: 0x00152F04
	internal void ᜅ()
	{
		int a_ = 15;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				goto IL_73;
			case 1:
			{
				int num2;
				if (num2 >= this.\u1712.Length)
				{
					num = 6;
					continue;
				}
				spr\u2528 a_2 = this.\u1712[num2];
				this.ᜀ.ᜉ(ClipboardData.b("ʹ䵶ὸ", a_));
				this.ᜀ.ᜅ(ClipboardData.b("ၴٶ᝸", a_), this.ᜀ(a_2));
				this.ᜀ.ᜈ();
				num2++;
				num = 0;
				continue;
			}
			case 2:
			{
				this.ᜀ.ᜉ(ClipboardData.b("ʹ䵶ὸᑺོቾ", a_));
				int num2 = 0;
				num = 4;
				continue;
			}
			case 4:
				goto IL_73;
			case 5:
				return;
			case 6:
				this.ᜀ.ᜈ();
				num = 5;
				continue;
			}
			if (this.\u1712 == null)
			{
				break;
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
				num = 2;
				continue;
			}
			IL_73:
			num = 1;
		}
	}

	// Token: 0x060014E5 RID: 5349 RVA: 0x00154050 File Offset: 0x00153050
	internal void ᜇ()
	{
		int a_ = 2;
		int num = 9;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.\u170D != null)
				{
					num = 23;
					continue;
				}
				goto IL_1F6;
			case 1:
				if (!this.ᜃ)
				{
					goto IL_46A;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_16C;
				default:
					if (false)
					{
					}
					num = 22;
					continue;
				}
				break;
			case 2:
				if (this.ᜋ != null)
				{
					num = 8;
					continue;
				}
				goto IL_2E8;
			case 3:
				goto IL_46A;
			case 4:
				if (!(this.ᜋ != ClipboardData.b("٧թɫ୭", a_)))
				{
					num = 13;
					continue;
				}
				goto IL_46A;
			case 5:
				if (this.ᜉ == 0)
				{
					num = 32;
					continue;
				}
				goto IL_DF;
			case 6:
				if (this.ᜊ != 0)
				{
					num = 28;
					continue;
				}
				goto IL_3FB;
			case 7:
				num = 38;
				continue;
			case 8:
				num = 4;
				continue;
			case 10:
				goto IL_2BB;
			case 11:
				if (this.ᜆ)
				{
					num = 7;
					continue;
				}
				goto IL_46A;
			case 12:
				if (this.ᜅ)
				{
					num = 20;
					continue;
				}
				goto IL_46A;
			case 13:
				goto IL_2E8;
			case 14:
				goto IL_3FB;
			case 15:
				return;
			case 16:
				goto IL_131;
			case 17:
				goto IL_2BB;
			case 18:
				num = 34;
				continue;
			case 19:
				if (this.\u170D != null)
				{
					num = 39;
					continue;
				}
				goto IL_23B;
			case 20:
				num = 11;
				continue;
			case 21:
				if (this.ᜄ)
				{
					num = 36;
					continue;
				}
				goto IL_46A;
			case 22:
				num = 21;
				continue;
			case 23:
				num = 31;
				continue;
			case 24:
				num = 1;
				continue;
			case 25:
				if (this.\u170D.Length <= 0)
				{
					num = 26;
					continue;
				}
				goto IL_46A;
			case 26:
				goto IL_23B;
			case 27:
				if (this.ᜎ != null)
				{
					num = 35;
					continue;
				}
				goto IL_131;
			case 28:
				goto IL_DF;
			case 29:
				this.ᜀ.ᜁ(ClipboardData.b("ݧ偩ཫŭṯᱱᅳᕵ౷๹ջ๽", a_), ClipboardData.b("୧ὩὫᩭὯά", a_));
				num = 17;
				continue;
			case 30:
				num = 2;
				continue;
			case 31:
				if (this.\u170D.Length > 0)
				{
					num = 40;
					continue;
				}
				goto IL_1F6;
			case 32:
				num = 6;
				continue;
			case 33:
				if (this.ᜌ != null)
				{
					num = 3;
					continue;
				}
				return;
			case 34:
				if (!this.ᜈ)
				{
					num = 30;
					continue;
				}
				goto IL_46A;
			case 35:
				num = 41;
				continue;
			case 36:
				num = 12;
				continue;
			case 37:
				if (this.ᜋ == null)
				{
					num = 29;
					continue;
				}
				goto IL_1F6;
			case 38:
				goto IL_16C;
			case 39:
				num = 25;
				continue;
			case 40:
				num = 37;
				continue;
			case 41:
				if (this.ᜎ.Length <= 0)
				{
					num = 16;
					continue;
				}
				goto IL_46A;
			}
			if (!this.ᜂ)
			{
				num = 24;
				continue;
			}
			goto IL_46A;
			IL_DF:
			this.ᜀ.ᜁ(ClipboardData.b("ѧͩūŭ", a_), string.Format(ClipboardData.b("፧婩ᅫ䉭୯䍱ॳ", a_), this.ᜉ, this.ᜊ));
			num = 14;
			continue;
			IL_131:
			num = 33;
			continue;
			IL_16C:
			if (!this.ᜇ)
			{
				num = 18;
				continue;
			}
			goto IL_46A;
			IL_1F6:
			this.ᜀ.ᜀ(ClipboardData.b("ݧ偩ཫŭṯᱱᅳᕵ౷๹ջ๽", a_), this.ᜋ, ClipboardData.b("٧թɫ୭", a_));
			if (true)
			{
			}
			num = 10;
			continue;
			IL_23B:
			num = 27;
			continue;
			IL_2BB:
			this.ᜂ();
			this.ᜁ();
			this.ᜀ();
			this.ᜀ.ᜈ();
			num = 15;
			continue;
			IL_2E8:
			num = 19;
			continue;
			IL_3FB:
			num = 0;
			continue;
			IL_46A:
			this.ᜀ.ᜉ(ClipboardData.b("ṧ偩ᱫ཭ѯᩱ", a_));
			this.ᜀ.ᜀ(ClipboardData.b("१ᡩṫŭݯᵱέ", a_), this.ᜂ, false);
			this.ᜀ.ᜀ(ClipboardData.b("๧ͩkɭὯᥱ", a_), this.ᜃ, true);
			this.ᜀ.ᜀ(ClipboardData.b("᭧ṩṫŭ᭯᝱᭳ᵵ", a_), this.ᜄ, true);
			this.ᜀ.ᜀ(ClipboardData.b("᭧ɩ൫੭Ὧձ᭳ᵵ", a_), this.ᜅ, true);
			this.ᜀ.ᜀ(ClipboardData.b("ݧ偩५᙭ѯqųյᅷᕹቻᅽ", a_), this.ᜆ, true);
			this.ᜀ.ᜀ(ClipboardData.b("ཧᡩ൫੭᥯᝱ᩳɵ୷ቹᵻ๽", a_), this.ᜇ, false);
			this.ᜀ.ᜀ(ClipboardData.b("ᱧཀྵᑫᩭo፱sṵ᝷ᅹ", a_), this.ᜈ, false);
			num = 5;
		}
	}

	// Token: 0x060014E6 RID: 5350 RVA: 0x0015460C File Offset: 0x0015360C
	private void ᜂ()
	{
		int a_ = 0;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ.ᜁ(ClipboardData.b("॥剧३ͫmṯ᝱ᝳɵᑷᕹύൽ", a_), sprṍ.ᜀ(this.\u170D, ',', ';'));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BB;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 1:
				num = 4;
				continue;
			case 2:
				return;
			case 4:
				if (this.\u170D.Length > 0)
				{
					goto IL_BB;
				}
				return;
			}
			if (this.\u170D != null)
			{
				if (true)
				{
				}
				num = 1;
				continue;
			}
			break;
			IL_BB:
			num = 0;
		}
	}

	// Token: 0x060014E7 RID: 5351 RVA: 0x001546E4 File Offset: 0x001536E4
	private void ᜁ()
	{
		int a_ = 18;
		int num = 8;
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
				{
					int num2;
					if (num2 >= this.ᜎ.Length)
					{
						goto IL_81;
					}
					int a_2 = this.ᜎ[num2];
					StringBuilder stringBuilder;
					stringBuilder.Append(sprṍ.ᜂ(a_2));
					stringBuilder.Append(',');
					num2++;
					num = 5;
					continue;
				}
				case 1:
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					if (this.ᜎ.Length > 0)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
				{
					StringBuilder stringBuilder;
					stringBuilder.Remove(stringBuilder.Length - 1, 1);
					this.ᜀ.ᜁ(ClipboardData.b("᝷䁹ύᅽﲇﲏ", a_), stringBuilder.ToString());
					num = 7;
					continue;
				}
				case 4:
				{
					StringBuilder stringBuilder = new StringBuilder(32);
					int num2 = 0;
					num = 6;
					continue;
				}
				case 5:
					goto IL_6E;
				case 6:
					goto IL_6E;
				case 7:
					return;
				}
				if (this.ᜎ != null)
				{
					num = 1;
					continue;
				}
				return;
				IL_6E:
				num = 0;
				continue;
			}
			IL_81:
			num = 3;
		}
	}

	// Token: 0x060014E8 RID: 5352 RVA: 0x00154838 File Offset: 0x00153838
	private void ᜀ()
	{
		int a_ = 0;
		int num = 4;
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
					num = 5;
					continue;
				case 1:
					goto IL_194;
				case 2:
					num = 3;
					continue;
				case 3:
					goto IL_BD;
				case 5:
					if (this.ᜁ.\u1774() == ShapeType.TextBox)
					{
						num = 1;
						continue;
					}
					this.ᜀ.ᜁ(ClipboardData.b("ብ൧ቩᡫ౭Ὧੱٳ፵᭷๹", a_), ClipboardData.b("啥奧屩彫䉭䍯䍱䉳䕵呷䭹䑻䩽덿떁ꢃ랅낇뺉뾋릍", a_));
					num = 8;
					continue;
				case 6:
					if (!this.ᜁ.ណ())
					{
						num = 2;
						continue;
					}
					return;
				case 7:
					goto IL_74;
				case 8:
					goto IL_142;
				}
				if (true)
				{
				}
				if (this.ᜌ != null)
				{
					num = 7;
					continue;
				}
				num = 6;
				continue;
			}
			IL_BD:
			if (this.ᜁ.OwnerParagraph.ChildObjects.Count <= 0)
			{
				return;
			}
			num = 0;
		}
		IL_74:
		string a_2 = this.ᜀ(this.ᜌ);
		this.ᜀ.ᜁ(ClipboardData.b("ብ൧ቩᡫ౭Ὧੱٳ፵᭷๹", a_), a_2);
		return;
		IL_142:
		return;
		IL_194:
		string a_3 = this.ᜀ(((spr\u1937)this.ᜁ).\u173D());
		this.ᜀ.ᜁ(ClipboardData.b("ብ൧ቩᡫ౭Ὧੱٳ፵᭷๹", a_), a_3);
	}

	// Token: 0x060014E9 RID: 5353 RVA: 0x001549E0 File Offset: 0x001539E0
	private string ᜀ(spr\u1D34[] A_0)
	{
		int a_ = 4;
		StringBuilder stringBuilder;
		for (;;)
		{
			stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = 3;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					stringBuilder.Append(ClipboardData.b("兩", a_));
					num2 = 1;
					continue;
				case 1:
					goto IL_42;
				case 2:
				{
					if (num >= A_0.Length)
					{
						num2 = 5;
						continue;
					}
					spr\u1D34 spr_u1D = A_0[num];
					stringBuilder.Append(sprṍ.ᜀ(spr_u1D.ᜀ));
					stringBuilder.Append(',');
					stringBuilder.Append(sprṍ.ᜀ(spr_u1D.ᜁ));
					stringBuilder.Append(',');
					stringBuilder.Append(sprṍ.ᜀ(spr_u1D.ᜂ));
					stringBuilder.Append(',');
					stringBuilder.Append(sprṍ.ᜀ(spr_u1D.ᜃ));
					num2 = 6;
					continue;
				}
				case 3:
					goto IL_3D;
				case 4:
					goto IL_109;
				case 5:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3D;
					default:
						goto IL_148;
					}
					break;
				case 6:
					if (num < A_0.Length - 1)
					{
						num2 = 0;
						continue;
					}
					goto IL_42;
				}
				break;
				IL_42:
				num++;
				num2 = 4;
				continue;
				IL_109:
				num2 = 2;
				continue;
				IL_3D:
				goto IL_109;
			}
		}
		IL_148:
		if (false)
		{
		}
		return stringBuilder.ToString();
	}

	// Token: 0x060014EA RID: 5354 RVA: 0x00154B44 File Offset: 0x00153B44
	internal void ᜄ()
	{
		int a_ = 7;
		int num = 28;
		for (;;)
		{
			sprᥴ sprᥴ;
			int num2;
			switch (num)
			{
			case 0:
				goto IL_222;
			case 1:
				num = 5;
				continue;
			case 2:
				if (sprᥴ.ᜀ)
				{
					num = 6;
					continue;
				}
				goto IL_F0;
			case 3:
				goto IL_3B2;
			case 4:
				if (sprᥴ.ᜂ)
				{
					num = 13;
					continue;
				}
				goto IL_3B2;
			case 5:
				if (sprᥴ.ᜋ.ᜂ() == -2147483648)
				{
					num = 16;
					continue;
				}
				goto IL_1F2;
			case 6:
				this.ᜀ(ClipboardData.b("Ὤ๮ᕰᩲtѶ୸᩺፼᡾", a_), sprᥴ.ᜋ, sprᥴ.ᜌ);
				num = 23;
				continue;
			case 7:
				goto IL_144;
			case 8:
				this.ᜀ.ᜉ(ClipboardData.b("᭬啮ᥰቲ᭴፶ᕸṺ๼", a_));
				num2 = 0;
				num = 11;
				continue;
			case 9:
				goto IL_1F2;
			case 10:
				goto IL_438;
			case 11:
				goto IL_222;
			case 12:
				goto IL_C3;
			case 13:
				goto IL_301;
			case 14:
				if (sprᥴ.ᜄ)
				{
					num = 32;
					continue;
				}
				goto IL_297;
			case 15:
				this.ᜀ(ClipboardData.b("ᵬnᵰቲݴ", a_), sprᥴ.ᜉ, sprᥴ.ᜊ);
				num = 10;
				continue;
			case 16:
				num = 18;
				continue;
			case 17:
				goto IL_2BD;
			case 18:
				if (sprᥴ.ᜌ.ᜂ() != 2147483647)
				{
					num = 9;
					continue;
				}
				goto IL_C3;
			case 19:
				if (sprᥴ.ᜁ)
				{
					num = 1;
					continue;
				}
				goto IL_2BD;
			case 20:
				if (sprᥴ.ᜎ.ᜂ() != 2147483647)
				{
					num = 7;
					continue;
				}
				goto IL_2BD;
			case 21:
				return;
			case 22:
				num = 20;
				continue;
			case 23:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_301;
				default:
					if (false)
					{
					}
					goto IL_F0;
				}
				break;
			case 24:
				goto IL_297;
			case 25:
				if (this.\u1713.Length > 0)
				{
					num = 8;
					continue;
				}
				return;
			case 26:
				if (sprᥴ.ᜃ)
				{
					num = 15;
					continue;
				}
				goto IL_438;
			case 27:
				num = 25;
				continue;
			case 29:
				this.ᜀ.ᜈ();
				num = 21;
				continue;
			case 30:
				if (sprᥴ.\u170D.ᜂ() == -2147483648)
				{
					num = 22;
					continue;
				}
				goto IL_144;
			case 31:
				if (num2 >= this.\u1713.Length)
				{
					num = 29;
					continue;
				}
				sprᥴ = this.\u1713[num2];
				this.ᜀ.ᜉ(ClipboardData.b("᭬啮ᥰ", a_));
				this.ᜀ(ClipboardData.b("ᵬnɰᩲŴṶᙸᕺ", a_), new sprṚ(sprᥴ.ᜇ.ᜁ(), true), new sprṚ(sprᥴ.ᜈ.ᜁ(), true));
				num = 14;
				continue;
			case 32:
				this.ᜀ.ᜅ(ClipboardData.b("Ṭᡮᡰݲᙴὶ", a_), "");
				num = 24;
				continue;
			}
			if (this.\u1713 != null)
			{
				if (true)
				{
				}
				num = 27;
				continue;
			}
			break;
			IL_C3:
			num = 30;
			continue;
			IL_F0:
			num = 19;
			continue;
			IL_144:
			this.ᜀ(ClipboardData.b("ᑬᵮၰᵲቴቶ", a_), sprᥴ.\u170D, sprᥴ.ᜎ);
			num = 17;
			continue;
			IL_1F2:
			this.ᜀ(ClipboardData.b("ᕬᵮၰᵲቴቶ", a_), sprᥴ.ᜋ, sprᥴ.ᜌ);
			num = 12;
			continue;
			IL_222:
			num = 31;
			continue;
			IL_297:
			num = 26;
			continue;
			IL_2BD:
			this.ᜀ.ᜈ();
			num2++;
			num = 0;
			continue;
			IL_301:
			this.ᜀ(ClipboardData.b("l๮Ű", a_), sprᥴ.ᜉ, sprᥴ.ᜊ);
			num = 3;
			continue;
			IL_3B2:
			num = 2;
			continue;
			IL_438:
			num = 4;
		}
	}

	// Token: 0x060014EB RID: 5355 RVA: 0x00154FE0 File Offset: 0x00153FE0
	private void ᜀ(string A_0, sprṚ A_1, sprṚ A_2)
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
		this.ᜀ.ᜅ(A_0, spr\u1DDF.ᜀ(spr\u1DDF.ᜀ(A_1), spr\u1DDF.ᜀ(A_2)));
	}

	// Token: 0x060014EC RID: 5356 RVA: 0x00155038 File Offset: 0x00154038
	private static string ᜀ(string A_0, string A_1)
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
		return string.Format(ClipboardData.b("ɸ䭺|卾婢늂", a_), A_0, A_1);
	}

	// Token: 0x060014ED RID: 5357 RVA: 0x00155094 File Offset: 0x00154094
	private static string ᜀ(sprṚ A_0)
	{
		int a_ = 6;
		int num = 3;
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
					num = 4;
					continue;
				case 1:
				{
					int num2 = A_0.ᜂ();
					num = 2;
					continue;
				}
				case 2:
				{
					int num2;
					switch (num2)
					{
					case 0:
						goto IL_139;
					case 1:
						goto IL_12A;
					case 2:
						goto IL_8D;
					}
					goto IL_FC;
				}
				case 4:
					num = 6;
					continue;
				case 5:
					goto IL_8B;
				case 6:
					if (A_0.ᜂ() < 256)
					{
						num = 5;
						continue;
					}
					goto IL_A9;
				}
				if (A_0.ᜁ())
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_148;
			}
			IL_FC:
			num = 0;
		}
		IL_8B:
		return ClipboardData.b("Ⱬ", a_) + (A_0.ᜂ() - 3);
		IL_8D:
		return ClipboardData.b("ཫ୭ṯٱᅳѵ", a_);
		IL_A9:
		return ClipboardData.b("佫", a_) + (A_0.ᜂ() - 256);
		IL_12A:
		return ClipboardData.b("๫ŭѯٱ᭳᭵⩷፹᭻ᙽ", a_);
		IL_139:
		return ClipboardData.b("ᡫŭo㹱ᅳၵ౷", a_);
		IL_148:
		return A_0.ᜂ().ToString();
	}

	// Token: 0x060014EE RID: 5358 RVA: 0x001551F8 File Offset: 0x001541F8
	private string ᜀ(spr\u2528 A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			int num = 4;
			string text;
			for (;;)
			{
				Operation operation;
				switch (num)
				{
				case 0:
					switch (operation)
					{
					case Operation.Mid:
					case Operation.Min:
					case Operation.Max:
					case Operation.Atan2:
					case Operation.Sin:
					case Operation.Cos:
						goto IL_12F;
					case Operation.Abs:
					case Operation.Sqrt:
						goto IL_198;
					case Operation.If:
					case Operation.Mod:
					case Operation.CosAtan2:
					case Operation.SinAtan2:
						goto IL_1F3;
					default:
						num = 5;
						continue;
					}
					break;
				case 1:
					IL_5E:
					num = 7;
					continue;
				case 2:
					if (A_0.ᜄ == 0)
					{
						num = 8;
						continue;
					}
					goto IL_71;
				case 3:
					goto IL_6C;
				case 5:
					num = 3;
					continue;
				case 6:
					num = 2;
					continue;
				case 7:
					if (A_0.ᜃ == 0)
					{
						num = 6;
						continue;
					}
					goto IL_71;
				case 8:
					goto IL_12A;
				}
				if (A_0.ᜀ == Operation.Sum)
				{
					num = 1;
					continue;
				}
				IL_71:
				text = sprᥜ.ᜀ(A_0.ᜀ);
				operation = A_0.ᜀ;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5E;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 0;
					break;
				}
			}
			IL_6C:
			goto IL_1F3;
			IL_12A:
			return spr\u1DDF.ᜀ(new string[]
			{
				ClipboardData.b("ʹᙶᕸ", a_),
				this.ᜀ(A_0.ᜂ, A_0.ᜂ())
			});
			IL_12F:
			return spr\u1DDF.ᜀ(new string[]
			{
				text,
				this.ᜀ(A_0.ᜂ, A_0.ᜂ()),
				this.ᜀ(A_0.ᜃ, A_0.ᜀ())
			});
			IL_198:
			return spr\u1DDF.ᜀ(new string[]
			{
				text,
				this.ᜀ(A_0.ᜂ, A_0.ᜂ())
			});
			IL_1F3:
			return spr\u1DDF.ᜀ(new string[]
			{
				text,
				this.ᜀ(A_0.ᜂ, A_0.ᜂ()),
				this.ᜀ(A_0.ᜃ, A_0.ᜀ()),
				this.ᜀ(A_0.ᜄ, A_0.ᜁ())
			});
		}
		}
	}

	// Token: 0x060014EF RID: 5359 RVA: 0x00155450 File Offset: 0x00154450
	private static string ᜀ(params string[] A_0)
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			stringBuilder = new StringBuilder(32);
			stringBuilder.Append(A_0[0]);
			int num = 1;
			int num2 = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_6C;
				case 1:
					goto IL_64;
				case 2:
					goto IL_7A;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6C;
					default:
						if (false)
						{
						}
						goto IL_64;
					}
					break;
				}
				break;
				IL_64:
				num2 = 0;
				continue;
				IL_6C:
				if (num >= A_0.Length)
				{
					num2 = 2;
				}
				else
				{
					stringBuilder.Append(' ');
					stringBuilder.Append(A_0[num]);
					num++;
					num2 = 1;
				}
			}
		}
		IL_7A:
		return stringBuilder.ToString();
	}

	// Token: 0x060014F0 RID: 5360 RVA: 0x00155504 File Offset: 0x00154504
	private string ᜀ(int A_0, bool A_1)
	{
		int a_ = 9;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_168;
			case 1:
				num = 9;
				continue;
			case 3:
				if (A_0 != 508)
				{
					num = 17;
					continue;
				}
				goto IL_16D;
			case 4:
				num = 13;
				continue;
			case 5:
				if (A_0 >= 1024)
				{
					num = 19;
					continue;
				}
				num = 18;
				continue;
			case 6:
				num = 10;
				continue;
			case 7:
				num = 12;
				continue;
			case 8:
				num = 16;
				continue;
			case 9:
				goto IL_238;
			case 10:
				if (true)
				{
				}
				switch (A_0)
				{
				case 339:
					goto IL_26D;
				case 340:
					goto IL_27C;
				default:
					num = 1;
					continue;
				}
				break;
			case 11:
				switch (A_0)
				{
				case 320:
					goto IL_21A;
				case 321:
					goto IL_229;
				case 322:
					goto IL_2D1;
				case 323:
					goto IL_1C0;
				default:
					num = 6;
					continue;
				}
				break;
			case 12:
				if (A_0 <= 334)
				{
					num = 0;
					continue;
				}
				goto IL_1AC;
			case 13:
				goto IL_238;
			case 14:
				num = 11;
				continue;
			case 15:
				switch (A_0)
				{
				case 1271:
					goto IL_B4;
				case 1272:
					goto IL_337;
				case 1273:
					goto IL_28B;
				case 1274:
				case 1275:
					goto IL_238;
				case 1276:
					goto IL_346;
				case 1277:
					goto IL_A5;
				case 1278:
					goto IL_13B;
				case 1279:
					goto IL_25E;
				default:
					num = 4;
					continue;
				}
				break;
			case 16:
				if (A_0 <= 340)
				{
					num = 14;
					continue;
				}
				num = 3;
				continue;
			case 17:
				num = 15;
				continue;
			case 18:
				if (A_0 >= 327)
				{
					num = 7;
					continue;
				}
				goto IL_1AC;
			case 19:
				goto IL_259;
			}
			IL_69:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_69;
			default:
				if (false)
				{
				}
				if (A_1)
				{
					num = 8;
					continue;
				}
				goto IL_355;
			}
			IL_238:
			num = 5;
		}
		IL_A5:
		return ClipboardData.b("੮ᱰٲ㵴ቶၸᱺᕼ୾", a_);
		IL_B4:
		return ClipboardData.b("Ὦᡰ୲ၴ᭶㕸ቺ፼᩾횀", a_);
		IL_13B:
		return ClipboardData.b("੮ᱰٲ≴Ṷᵸེᕼ䵾", a_);
		IL_168:
		return ClipboardData.b("䱮", a_) + (A_0 - 327);
		IL_16D:
		return ClipboardData.b("ͮᡰᵲၴ㍶୸᩺੼ᅾ", a_);
		IL_1AC:
		throw new InvalidOperationException(ClipboardData.b("㩮ὰŲၴᑶᙸᱺ፼ᙾﮀꞆﮊﶎﶒ랖漢쒠莢첤즦覨\udfaa얬쪮醰햲\udab4얶풸캺톼\udebe", a_));
		IL_1C0:
		return ClipboardData.b("ݮᑰᩲቴὶ൸", a_);
		IL_21A:
		return ClipboardData.b("ᝮተᙲ᭴Ͷᱸॺ", a_);
		IL_229:
		return ClipboardData.b("᙮ተᙲ᭴Ͷᱸॺ", a_);
		IL_259:
		return ClipboardData.b("⽮", a_) + (A_0 - 1024);
		IL_25E:
		return ClipboardData.b("੮ᱰٲ㵴ቶၸᱺᕼ୾뎀", a_);
		IL_26D:
		return ClipboardData.b("ᝮᵰᩲᡴᡶ", a_);
		IL_27C:
		return ClipboardData.b("᙮ᵰᩲᡴᡶ", a_);
		IL_28B:
		return ClipboardData.b("Ὦᡰ୲ၴ᭶ㅸṺᑼ᡾", a_);
		IL_2D1:
		return ClipboardData.b("ᡮᡰᝲŴὶ", a_);
		IL_337:
		return ClipboardData.b("Ὦᡰ୲ၴ᭶⹸ቺ᥼୾", a_);
		IL_346:
		return ClipboardData.b("੮ᱰٲ≴Ṷᵸེᕼ", a_);
		IL_355:
		return sprᜌ.\u170D(A_0);
	}

	// Token: 0x04001954 RID: 6484
	private readonly sprᤎ ᜀ;

	// Token: 0x04001955 RID: 6485
	private readonly sprᩍ ᜁ;

	// Token: 0x04001956 RID: 6486
	private bool ᜂ;

	// Token: 0x04001957 RID: 6487
	private bool ᜃ = true;

	// Token: 0x04001958 RID: 6488
	private bool ᜄ = true;

	// Token: 0x04001959 RID: 6489
	private bool ᜅ = true;

	// Token: 0x0400195A RID: 6490
	private bool ᜆ = true;

	// Token: 0x0400195B RID: 6491
	private bool ᜇ;

	// Token: 0x0400195C RID: 6492
	private bool ᜈ;

	// Token: 0x0400195D RID: 6493
	private int ᜉ;

	// Token: 0x0400195E RID: 6494
	private int ᜊ;

	// Token: 0x0400195F RID: 6495
	private string ᜋ;

	// Token: 0x04001960 RID: 6496
	private spr\u1D34[] ᜌ;

	// Token: 0x04001961 RID: 6497
	private spr\u2055[] \u170D;

	// Token: 0x04001962 RID: 6498
	private int[] ᜎ;

	// Token: 0x04001963 RID: 6499
	private spr\u2055[] ᜏ;

	// Token: 0x04001964 RID: 6500
	private sprỬ[] ᜐ;

	// Token: 0x04001965 RID: 6501
	private readonly string[] ᜑ = new string[10];

	// Token: 0x04001966 RID: 6502
	private spr\u2528[] \u1712;

	// Token: 0x04001967 RID: 6503
	private sprᥴ[] \u1713;
}
