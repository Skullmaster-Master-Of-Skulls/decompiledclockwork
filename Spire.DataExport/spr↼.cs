using System;
using System.Collections;
using System.Text;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;
using Spire.DataExport.XLS;
using Spire.DataExport.XLS.Formula;

// Token: 0x0200010C RID: 268
internal class spr\u21BC
{
	// Token: 0x060005E8 RID: 1512 RVA: 0x0003891C File Offset: 0x0003791C
	public spr\u21BC(WorkSheet A_0)
	{
		this.ᜅ = A_0;
	}

	// Token: 0x060005E9 RID: 1513 RVA: 0x00038944 File Offset: 0x00037944
	public sprạ[] \u1712(string A_0)
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
		this.ᜂ(A_0);
		this.\u1715();
		return (sprạ[])this.ᜂ.ToArray(typeof(sprạ));
	}

	// Token: 0x060005EA RID: 1514 RVA: 0x000389A8 File Offset: 0x000379A8
	public void ᜀ(sprạ A_0)
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
		this.ᜂ.Add(A_0);
	}

	// Token: 0x060005EB RID: 1515 RVA: 0x000389F0 File Offset: 0x000379F0
	public void ᜀ(FormulaTokenCode A_0)
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
		this.ᜂ.Add(spr\u1C33.ᜀ(this.ᜅ, A_0));
	}

	// Token: 0x060005EC RID: 1516 RVA: 0x00038A44 File Offset: 0x00037A44
	public void ᜀ(FormulaTokenCode A_0, object A_1)
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
		this.ᜀ(A_0, new object[]
		{
			A_1
		});
	}

	// Token: 0x060005ED RID: 1517 RVA: 0x00038A94 File Offset: 0x00037A94
	public void ᜀ(FormulaTokenCode A_0, object[] A_1)
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
		sprạ sprạ = spr\u1C33.ᜀ(this.ᜅ, A_0);
		this.ᜂ.Add(sprạ);
		sprạ.ᜀ(A_1);
	}

	// Token: 0x060005EE RID: 1518 RVA: 0x00038AF0 File Offset: 0x00037AF0
	private void \u1715()
	{
		int a_ = 1;
		for (;;)
		{
			this.ᜁ('=');
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_51;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_AD;
					default:
						if (false)
						{
						}
						if (!this.ᜁ.ᜈ())
						{
							num = 5;
							continue;
						}
						return;
					}
					break;
				case 2:
					return;
				case 3:
					goto IL_51;
				case 4:
					if (this.ᜂ('{'))
					{
						num = 6;
						continue;
					}
					this.\u1714();
					num = 3;
					continue;
				case 5:
					this.ᜁ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("尜洞䘠倢稤渦䜨崪䰬䌮堰圲猴堶䬸嘺䠼匾⁀ق㵄㝆㭈⹊㹌㱎", a_)));
					num = 2;
					continue;
				case 6:
					goto IL_AD;
				}
				break;
				IL_51:
				if (true)
				{
				}
				num = 1;
				continue;
				IL_AD:
				this.ᜁ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("尜洞䘠倢稤渦䜨崪䰬䌮堰圲琴䔶䬸娺䐼社⹀ㅂ⡄㉆╈⩊", a_)));
				num = 0;
			}
		}
	}

	// Token: 0x060005EF RID: 1519 RVA: 0x00038C10 File Offset: 0x00037C10
	private void \u1714()
	{
		int a_ = 2;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_50;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_46;
				default:
					goto IL_DE;
				}
				break;
			case 2:
			{
				FormulaTokenCode formulaTokenCode;
				if (formulaTokenCode == FormulaTokenCode.Empty)
				{
					num = 1;
					continue;
				}
				this.ᜈ();
				this.\u1714();
				this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("弝刟䔡圣礥愧䐩娫伭尯嬱倳瀵圷䠹儻䬽ⰿ⍁Ń㹅㡇㡉⥋㵍⍏", a_)));
				this.ᜀ(formulaTokenCode);
				num = 0;
				continue;
			}
			case 3:
				return;
			case 5:
				goto IL_50;
			case 6:
				this.\u1713();
				this.ᜁ(')');
				this.ᜀ(FormulaTokenCode.Parentheses);
				num = 5;
				continue;
			case 7:
			{
				if (true)
				{
				}
				if (this.ᜁ.ᜁ() == ',')
				{
					num = 3;
					continue;
				}
				FormulaTokenCode formulaTokenCode = this.ᜁ();
				num = 2;
				continue;
			}
			}
			goto IL_39;
			IL_46:
			num = 6;
			continue;
			IL_39:
			if (this.ᜂ('('))
			{
				goto IL_46;
			}
			goto IL_127;
			IL_50:
			num = 7;
		}
		return;
		IL_DE:
		if (false)
		{
		}
		return;
		IL_127:
		this.\u1713();
	}

	// Token: 0x060005F0 RID: 1520 RVA: 0x00038D4C File Offset: 0x00037D4C
	private void \u1713()
	{
		int a_ = 4;
		for (;;)
		{
			this.\u1712();
			int num = 27;
			for (;;)
			{
				char c;
				switch (num)
				{
				case 0:
					c = '<';
					this.ᜁ.ᜀ();
					num = 24;
					continue;
				case 1:
					this.ᜀ(FormulaTokenCode.Ne);
					num = 6;
					continue;
				case 2:
					num = 15;
					continue;
				case 3:
					return;
				case 4:
					this.ᜀ(FormulaTokenCode.Le);
					num = 28;
					continue;
				case 5:
					num = 22;
					continue;
				case 6:
					goto IL_143;
				case 7:
					goto IL_35C;
				case 8:
					if (this.ᜁ.ᜁ() == '=')
					{
						num = 10;
						continue;
					}
					goto IL_188;
				case 9:
					goto IL_143;
				case 10:
				{
					if (true)
					{
					}
					char c2 = '=';
					this.ᜁ.ᜀ();
					num = 20;
					continue;
				}
				case 11:
				{
					char c2;
					if (c2 == '=')
					{
						num = 23;
						continue;
					}
					this.ᜀ(FormulaTokenCode.Gt);
					num = 26;
					continue;
				}
				case 12:
					goto IL_143;
				case 13:
					goto IL_28F;
				case 14:
					if (this.ᜁ.ᜁ() != '=')
					{
						num = 2;
						continue;
					}
					this.ᜁ.ᜀ();
					this.ᜈ();
					this.\u1712();
					this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧挩䈫堭儯帱崳刵縷唹主匽㔿⹁╃ͅぇ㩉㹋⭍⍏⅑", a_)));
					this.ᜀ(FormulaTokenCode.Eq);
					num = 16;
					continue;
				case 15:
					if (this.ᜁ.ᜁ() == '<')
					{
						num = 0;
						continue;
					}
					num = 18;
					continue;
				case 16:
					goto IL_143;
				case 17:
					if (c == '=')
					{
						num = 4;
						continue;
					}
					num = 19;
					continue;
				case 18:
				{
					if (this.ᜁ.ᜁ() != '>')
					{
						num = 3;
						continue;
					}
					char c2 = '>';
					this.ᜁ.ᜀ();
					num = 8;
					continue;
				}
				case 19:
					if (c == '>')
					{
						num = 1;
						continue;
					}
					this.ᜀ(FormulaTokenCode.Lt);
					num = 9;
					continue;
				case 20:
					goto IL_188;
				case 21:
					if (this.ᜁ.ᜁ() != '>')
					{
						num = 5;
						continue;
					}
					goto IL_28F;
				case 22:
					if (this.ᜁ.ᜁ() == '=')
					{
						num = 13;
						continue;
					}
					goto IL_35C;
				case 23:
					this.ᜀ(FormulaTokenCode.Ge);
					num = 12;
					continue;
				case 24:
					if (this.ᜁ.ᜁ() != '>')
					{
						num = 25;
						continue;
					}
					goto IL_28F;
				case 25:
					IL_122:
					num = 21;
					continue;
				case 26:
					goto IL_143;
				case 27:
					goto IL_143;
				case 28:
					goto IL_143;
				}
				break;
				IL_188:
				this.ᜈ();
				this.\u1712();
				this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧挩䈫堭儯帱崳刵縷唹主匽㔿⹁╃ͅぇ㩉㹋⭍⍏⅑", a_)));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_122;
				default:
					if (false)
					{
					}
					num = 11;
					continue;
				}
				IL_143:
				num = 14;
				continue;
				IL_28F:
				c = this.ᜁ.ᜁ();
				this.ᜁ.ᜀ();
				num = 7;
				continue;
				IL_35C:
				this.ᜈ();
				this.\u1712();
				this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧挩䈫堭儯帱崳刵縷唹主匽㔿⹁╃ͅぇ㩉㹋⭍⍏⅑", a_)));
				num = 17;
			}
		}
	}

	// Token: 0x060005F1 RID: 1521 RVA: 0x0003913C File Offset: 0x0003813C
	private void \u1712()
	{
		int a_ = 14;
		for (;;)
		{
			this.ᜑ();
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					IL_37:
					goto IL_39;
				case 2:
					if (!this.ᜂ('&'))
					{
						num = 0;
						continue;
					}
					this.ᜈ();
					this.ᜑ();
					this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("欩師䤭䌯洱紳堵丷嬹倻圽␿с⭃㑅╇㽉⁋⽍ᕏ⩑⑓⑕㵗⥙⽛", a_)));
					this.ᜀ(FormulaTokenCode.Concat);
					num = 3;
					continue;
				case 3:
					goto IL_39;
				}
				break;
				IL_39:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_37;
				default:
					if (false)
					{
					}
					num = 2;
					break;
				}
			}
		}
	}

	// Token: 0x060005F2 RID: 1522 RVA: 0x00039204 File Offset: 0x00038204
	private void ᜑ()
	{
		int a_ = 6;
		for (;;)
		{
			this.ᜐ();
			int num = 4;
			for (;;)
			{
				char c;
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (this.ᜁ.ᜁ() != '+')
					{
						num = 7;
						continue;
					}
					goto IL_77;
				case 2:
					if (c == '+')
					{
						num = 6;
						continue;
					}
					this.ᜀ(FormulaTokenCode.Sub);
					num = 5;
					continue;
				case 3:
					goto IL_45;
				case 4:
					goto IL_45;
				case 5:
					goto IL_45;
				case 6:
					this.ᜀ(FormulaTokenCode.Add);
					num = 3;
					continue;
				case 7:
					num = 8;
					continue;
				case 8:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_B9;
					default:
						if (false)
						{
						}
						if (this.ᜁ.ᜁ() != '-')
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_77;
					}
					break;
				}
				break;
				IL_45:
				num = 1;
				continue;
				IL_B9:
				num = 2;
				continue;
				IL_77:
				c = this.ᜁ.ᜁ();
				this.ᜁ.ᜀ();
				this.ᜈ();
				this.ᜐ();
				this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("挡嘣䄥嬧甩攫䀭䘯匱堳張尷簹医䰽ⴿ㝁⡃❅േ㉉㱋㱍㕏⅑❓", a_)));
				goto IL_B9;
			}
		}
	}

	// Token: 0x060005F3 RID: 1523 RVA: 0x00039358 File Offset: 0x00038358
	private void ᜐ()
	{
		int a_ = 14;
		for (;;)
		{
			this.ᜏ();
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				char c;
				switch (num)
				{
				case 0:
					goto IL_4D;
				case 1:
					goto IL_4D;
				case 2:
					if (c == '*')
					{
						num = 5;
						continue;
					}
					this.ᜀ(FormulaTokenCode.Div);
					num = 7;
					continue;
				case 3:
					num = 4;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C1;
					}
					if (false)
					{
					}
					if (this.ᜁ.ᜁ() != '/')
					{
						num = 6;
						continue;
					}
					goto IL_7F;
				case 5:
					this.ᜀ(FormulaTokenCode.Mul);
					num = 1;
					continue;
				case 6:
					return;
				case 7:
					goto IL_4D;
				case 8:
					if (this.ᜁ.ᜁ() != '*')
					{
						num = 3;
						continue;
					}
					goto IL_7F;
				}
				break;
				IL_4D:
				num = 8;
				continue;
				IL_C1:
				num = 2;
				continue;
				IL_7F:
				c = this.ᜁ.ᜁ();
				this.ᜁ.ᜀ();
				this.ᜈ();
				this.ᜏ();
				this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("欩師䤭䌯洱紳堵丷嬹倻圽␿с⭃㑅╇㽉⁋⽍ᕏ⩑⑓⑕㵗⥙⽛", a_)));
				goto IL_C1;
			}
		}
	}

	// Token: 0x060005F4 RID: 1524 RVA: 0x000394AC File Offset: 0x000384AC
	private void ᜏ()
	{
		int a_ = 1;
		for (;;)
		{
			this.ᜎ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					IL_2F:
					if (true)
					{
					}
					goto IL_39;
				case 1:
					if (!this.ᜂ('^'))
					{
						num = 2;
						continue;
					}
					this.ᜈ();
					this.ᜎ();
					this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("尜洞䘠倢稤渦䜨崪䰬䌮堰圲猴堶䬸嘺䠼匾⁀ق㵄㝆㭈⹊㹌㱎", a_)));
					this.ᜀ(FormulaTokenCode.Power);
					num = 3;
					continue;
				case 2:
					return;
				case 3:
					goto IL_39;
				}
				break;
				IL_39:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2F;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
			}
		}
	}

	// Token: 0x060005F5 RID: 1525 RVA: 0x00039574 File Offset: 0x00038574
	private void ᜎ()
	{
		for (;;)
		{
			this.\u170D();
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					IL_2E:
					goto IL_30;
				case 1:
					goto IL_30;
				case 2:
					return;
				case 3:
					if (!this.ᜂ('%'))
					{
						num = 2;
						continue;
					}
					this.ᜇ = true;
					this.ᜀ(FormulaTokenCode.Percent);
					this.\u170D();
					this.ᜇ = false;
					num = 1;
					continue;
				}
				break;
				IL_30:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2E;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
			}
		}
	}

	// Token: 0x060005F6 RID: 1526 RVA: 0x0003961C File Offset: 0x0003861C
	private void \u170D()
	{
		int a_ = 12;
		int num = 2;
		for (;;)
		{
			int num2;
			ArrayList arrayList;
			switch (num)
			{
			case 0:
				if (num2 >= arrayList.Count)
				{
					num = 13;
					continue;
				}
				num = 14;
				continue;
			case 1:
				return;
			case 3:
				if (arrayList.Count > 0)
				{
					num = 12;
					continue;
				}
				goto IL_82;
			case 4:
				goto IL_F5;
			case 5:
				goto IL_B2;
			case 6:
				if (true)
				{
				}
				goto IL_151;
			case 7:
				goto IL_B2;
			case 8:
				this.ᜀ(FormulaTokenCode.Uplus);
				num = 7;
				continue;
			case 9:
				goto IL_82;
			case 10:
				goto IL_F5;
			case 11:
				this.ᜈ();
				this.ᜌ();
				num = 3;
				continue;
			case 12:
				goto IL_123;
			case 13:
				return;
			case 14:
				if ((char)arrayList[num2] == '+')
				{
					num = 8;
					continue;
				}
				this.ᜀ(FormulaTokenCode.Uminus);
				num = 5;
				continue;
			case 15:
				if (!this.ᜀ(arrayList))
				{
					num = 11;
					continue;
				}
				goto IL_151;
			}
			if (this.ᜁ.ᜈ())
			{
				num = 1;
				continue;
			}
			arrayList = new ArrayList();
			num = 6;
			continue;
			IL_82:
			arrayList.Reverse();
			num2 = 0;
			num = 4;
			continue;
			IL_B2:
			num2++;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_123:
				this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("椧堩䬫崭漯笱娳䀵夷嘹唻娽ؿⵁ㙃⭅㵇♉ⵋ୍⡏≑♓㍕⭗⥙", a_)));
				num = 9;
				continue;
			default:
				if (false)
				{
				}
				num = 10;
				continue;
			}
			IL_F5:
			num = 0;
			continue;
			IL_151:
			num = 15;
		}
	}

	// Token: 0x060005F7 RID: 1527 RVA: 0x000397FC File Offset: 0x000387FC
	private bool ᜀ(ArrayList A_0)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜂ('-'))
					{
						num = 5;
						continue;
					}
					return result;
				case 1:
					goto IL_56;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						goto IL_AD;
					}
					break;
				case 3:
					goto IL_3C;
				case 4:
					if (this.ᜂ('+'))
					{
						num = 3;
						continue;
					}
					num = 0;
					continue;
				case 5:
					A_0.Add('-');
					result = true;
					num = 1;
					continue;
				}
				break;
				IL_3C:
				A_0.Add('+');
				result = true;
				num = 2;
			}
		}
		IL_56:
		if (true)
		{
		}
		return result;
		IL_AD:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x060005F8 RID: 1528 RVA: 0x000398CC File Offset: 0x000388CC
	private void ᜌ()
	{
		int a_ = 4;
		for (;;)
		{
			this.ᜋ();
			int num = 3;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (sprὶ.ᜀ(this.ᜂ()))
					{
						num = 5;
						continue;
					}
					return;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8C;
					default:
						if (false)
						{
						}
						if (!this.ᜃ)
						{
							num = 4;
							continue;
						}
						return;
					}
					break;
				case 3:
					goto IL_8C;
				case 4:
					num = 7;
					continue;
				case 5:
					num = 2;
					continue;
				case 6:
					goto IL_8C;
				case 7:
					if (!this.ᜂ(','))
					{
						num = 0;
						continue;
					}
					this.ᜈ();
					this.ᜋ();
					this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧挩䈫堭儯帱崳刵縷唹主匽㔿⹁╃ͅぇ㩉㹋⭍⍏⅑", a_)));
					this.ᜀ(FormulaTokenCode.List);
					num = 6;
					continue;
				}
				break;
				IL_8C:
				num = 1;
			}
		}
	}

	// Token: 0x060005F9 RID: 1529 RVA: 0x000399EC File Offset: 0x000389EC
	private void ᜋ()
	{
		int a_ = 17;
		for (;;)
		{
			this.ᜊ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_84;
				case 1:
					num = 2;
					continue;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_84;
					}
					if (false)
					{
					}
					if (!this.ᜃ)
					{
						num = 7;
						continue;
					}
					return;
				case 3:
					if (!this.ᜁ(' ', false))
					{
						num = 6;
						continue;
					}
					this.ᜈ();
					this.ᜊ();
					this.ᜐ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("氬崮嘰䀲樴縶圸䴺尼匾⡀❂̈́⡆㭈♊㡌⍎ぐᙒⵔ❖⭘㹚⹜ⱞ", a_)));
					this.ᜀ(FormulaTokenCode.Isect);
					num = 5;
					continue;
				case 4:
					if (sprὶ.ᜀ(this.ᜂ()))
					{
						num = 1;
						continue;
					}
					return;
				case 5:
					goto IL_84;
				case 6:
					return;
				case 7:
					if (true)
					{
					}
					num = 3;
					continue;
				}
				break;
				IL_84:
				num = 4;
			}
		}
	}

	// Token: 0x060005FA RID: 1530 RVA: 0x00039B0C File Offset: 0x00038B0C
	private void ᜊ()
	{
		int a_ = 4;
		int num = 0;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 1:
				return;
			case 2:
				goto IL_B8;
			case 3:
				if (spr\u21BC.\u170D(text))
				{
					num = 20;
					continue;
				}
				num = 13;
				continue;
			case 4:
				if (this.ᜁ.ᜁ() == '{')
				{
					num = 16;
					continue;
				}
				num = 5;
				continue;
			case 5:
			{
				if (this.ᜂ('('))
				{
					num = 9;
					continue;
				}
				text = this.ᜁ.ᜀ(false);
				bool flag = spr\u21BC.ᜀ.Contains(text.ToUpper());
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_210;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			}
			case 6:
				if (this.ᜋ(text))
				{
					num = 11;
					continue;
				}
				num = 12;
				continue;
			case 7:
				goto IL_1E5;
			case 8:
			{
				bool flag;
				if (flag)
				{
					num = 21;
					continue;
				}
				num = 15;
				continue;
			}
			case 9:
				goto IL_20B;
			case 10:
				goto IL_24A;
			case 11:
				goto IL_1B5;
			case 12:
				if (this.ᜄ(text))
				{
					num = 7;
					continue;
				}
				num = 8;
				continue;
			case 13:
				if (this.ᜅ())
				{
					num = 18;
					continue;
				}
				if (true)
				{
				}
				num = 14;
				continue;
			case 14:
				if (this.ᜇ())
				{
					num = 19;
					continue;
				}
				goto IL_2A5;
			case 15:
				if (this.ᜊ(text))
				{
					num = 2;
					continue;
				}
				num = 17;
				continue;
			case 16:
				goto IL_298;
			case 17:
				if (this.ᜉ(text))
				{
					num = 10;
					continue;
				}
				num = 3;
				continue;
			case 18:
				goto IL_E2;
			case 19:
				goto IL_152;
			case 20:
				goto IL_124;
			case 21:
				goto IL_26B;
			}
			if (this.ᜁ.ᜈ())
			{
				num = 1;
			}
			else
			{
				num = 4;
			}
		}
		return;
		IL_B8:
		this.ᜈ(text);
		return;
		IL_E2:
		this.ᜆ();
		return;
		IL_124:
		this.ᜏ(text);
		return;
		IL_152:
		this.ᜎ(text);
		return;
		IL_1B5:
		goto IL_210;
		IL_1E5:
		this.ᜅ(text);
		return;
		IL_20B:
		this.ᜄ();
		return;
		IL_210:
		this.ᜆ(text);
		return;
		IL_24A:
		this.ᜌ(text);
		return;
		IL_26B:
		this.ᜇ(text);
		return;
		IL_298:
		this.ᜁ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧挩䈫堭儯帱崳刵礷䠹主弽㤿с⭃㑅╇㽉⁋⽍", a_)));
		return;
		IL_2A5:
		this.ᜑ(text);
	}

	// Token: 0x060005FB RID: 1531 RVA: 0x00039DD0 File Offset: 0x00038DD0
	private byte ᜉ()
	{
		byte b;
		for (;;)
		{
			b = 0;
			bool flag = false;
			int num = 2;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_4C;
				case 1:
					goto IL_104;
				case 2:
					goto IL_A9;
				case 3:
					goto IL_4C;
				case 4:
					if (!flag)
					{
						num = 7;
						continue;
					}
					goto IL_A9;
				case 5:
					b += 1;
					num = 0;
					continue;
				case 6:
					if (this.ᜂ(','))
					{
						num = 9;
						continue;
					}
					this.\u1714();
					num = 10;
					continue;
				case 7:
					num = 8;
					continue;
				case 8:
					goto IL_EF;
				case 9:
					this.ᜀ(FormulaTokenCode.MissArg);
					b += 1;
					flag = true;
					num = 3;
					continue;
				case 10:
					if (this.ᜁ.ᜄ() <= num2)
					{
						goto IL_4C;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EF;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				}
				break;
				IL_4C:
				num = 4;
				continue;
				IL_A9:
				flag = false;
				this.ᜁ.ᜂ();
				num2 = this.ᜁ.ᜄ();
				num = 6;
				continue;
				IL_EF:
				if (this.ᜂ(','))
				{
					goto IL_A9;
				}
				num = 1;
			}
		}
		IL_104:
		if (true)
		{
		}
		return b;
	}

	// Token: 0x060005FC RID: 1532 RVA: 0x00039F20 File Offset: 0x00038F20
	private void ᜑ(string A_0)
	{
		int a_ = 9;
		int num = 9;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				if (this.ᜁ.ᜁ() == '(')
				{
					num = 3;
					continue;
				}
				num = 7;
				continue;
			case 1:
				goto IL_19A;
			case 2:
				num = 13;
				continue;
			case 3:
				goto IL_1EC;
			case 4:
				if (this.ᜁ.ᜁ() != ')')
				{
					num = 1;
					continue;
				}
				return;
			case 5:
				goto IL_10D;
			case 6:
				goto IL_1B8;
			case 7:
				if (this.ᜁ.ᜁ() == '@')
				{
					num = 10;
					continue;
				}
				num = 5;
				continue;
			case 8:
				num = 4;
				continue;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_10D;
				default:
					if (false)
					{
					}
					break;
				}
				break;
			case 10:
				goto IL_16B;
			case 11:
				goto IL_124;
			case 12:
				if (this.ᜇ)
				{
					num = 6;
					continue;
				}
				num = 0;
				continue;
			case 13:
				if (A_0.Length == 0)
				{
					num = 8;
					continue;
				}
				goto IL_19A;
			}
			if (this.ᜃ)
			{
				num = 2;
				continue;
			}
			goto IL_19A;
			IL_10D:
			if (A_0.Length > 0)
			{
				num = 11;
				continue;
			}
			goto IL_1F1;
			IL_19A:
			num = 12;
		}
		IL_124:
		this.ᜁ(HyperlinksCollectionEditor.b("瀤䤦䈨䐪娬䄮ᄰ䜲娴尶尸唺ᔼἾ", a_) + A_0 + HyperlinksCollectionEditor.b("Ԥฦ", a_));
		return;
		IL_16B:
		this.ᜁ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("搤唦丨堪爬昮弰䔲吴嬶倸强笼倾㍀⹂い⭆⡈๊㕌㽎⍐㙒♔⑖", a_)));
		return;
		IL_1B8:
		return;
		IL_1EC:
		this.ᜁ(HyperlinksCollectionEditor.b("瀤䤦䈨䐪娬䄮ᄰ䜲娴尶尸唺ᔼἾ", a_) + A_0 + HyperlinksCollectionEditor.b("Ԥฦ", a_));
		return;
		IL_1F1:
		this.ᜁ(HyperlinksCollectionEditor.b("瀤䤦䈨䐪娬䄮ᄰ䜲娴尶尸唺ᔼἾ", a_) + this.ᜁ.ᜁ() + HyperlinksCollectionEditor.b("Ԥฦ", a_));
	}

	// Token: 0x060005FD RID: 1533 RVA: 0x0003A158 File Offset: 0x00039158
	private void ᜈ()
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
		this.ᜁ.ᜂ();
		this.ᜆ = this.ᜁ.ᜄ();
	}

	// Token: 0x060005FE RID: 1534 RVA: 0x0003A1B0 File Offset: 0x000391B0
	private void ᜐ(string A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜀ(A_0);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 2:
				return;
			}
			if (this.ᜁ.ᜄ() != this.ᜆ)
			{
				break;
			}
			num = 0;
		}
	}

	// Token: 0x060005FF RID: 1535 RVA: 0x0003A238 File Offset: 0x00039238
	private void ᜏ(string A_0)
	{
		int num = 0;
		double num2;
		for (;;)
		{
			IL_12:
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				break;
			case 1:
				if (spr\u2177.ᜀ(num2))
				{
					num = 3;
					continue;
				}
				goto IL_92;
			case 2:
				return;
			case 3:
				goto IL_85;
			}
			while (A_0.Length == 0)
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
					num = 2;
					goto IL_12;
				}
			}
			num2 = spr\u2177.ᜀ(A_0);
			num = 1;
		}
		return;
		IL_85:
		this.ᜀ(FormulaTokenCode.Int, (ushort)num2);
		return;
		IL_92:
		this.ᜀ(FormulaTokenCode.Num, num2);
	}

	// Token: 0x06000600 RID: 1536 RVA: 0x0003A2E8 File Offset: 0x000392E8
	private void ᜎ(string A_0)
	{
		int a_ = 4;
		for (;;)
		{
			IL_1D:
			A_0 = '#' + this.ᜁ.ᜀ(new char[]
			{
				'!',
				'?'
			}) + this.ᜁ.ᜀ();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (!spr\u22D3.ᜂ.Contains(A_0.ToUpper()))
					{
						num = 1;
						continue;
					}
					goto IL_B1;
				case 1:
					this.ᜁ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧挩䈫堭儯帱崳刵縷唹主匽㔿⹁╃ͅぇ㩉㹋⭍⍏⅑", a_)));
					num = 2;
					continue;
				case 2:
					goto IL_AF;
				}
				goto IL_1D;
			}
			IL_B1:
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_CF;
			}
			IL_AF:
			goto IL_B1;
		}
		IL_CF:
		if (false)
		{
		}
		this.ᜀ(FormulaTokenCode.Err, A_0);
	}

	// Token: 0x06000601 RID: 1537 RVA: 0x0003A3D4 File Offset: 0x000393D4
	private bool ᜇ()
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
		return this.ᜂ('#');
	}

	// Token: 0x06000602 RID: 1538 RVA: 0x0003A418 File Offset: 0x00039418
	private void ᜆ()
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
		this.ᜀ(FormulaTokenCode.Str, this.ᜁ.ᜀ('"'));
		this.ᜁ('"');
	}

	// Token: 0x06000603 RID: 1539 RVA: 0x0003A470 File Offset: 0x00039470
	private bool ᜅ()
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
		return this.ᜂ('"');
	}

	// Token: 0x06000604 RID: 1540 RVA: 0x0003A4B4 File Offset: 0x000394B4
	private static bool \u170D(string A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_81;
			case 2:
				if (true)
				{
				}
				if (!char.IsDigit(A_0[0]))
				{
					num = 0;
					continue;
				}
				return true;
			case 3:
				return false;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4F;
			default:
				if (false)
				{
				}
				if (A_0.Length <= 0)
				{
					num = 3;
				}
				else
				{
					num = 2;
				}
				break;
			}
		}
		return false;
		IL_4F:
		return A_0[0] == '.';
		IL_81:
		goto IL_4F;
	}

	// Token: 0x06000605 RID: 1541 RVA: 0x0003A554 File Offset: 0x00039554
	private void ᜌ(string A_0)
	{
		int a_ = 4;
		int num = 6;
		string text;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (this.ᜂ(':'))
				{
					num = 1;
					continue;
				}
				goto IL_11E;
			case 1:
				text = this.ᜃ();
				num = 5;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A8;
				}
				goto Block_2;
			case 3:
				A_0 = this.ᜃ();
				num = 4;
				continue;
			case 4:
				goto IL_F1;
			case 5:
				goto IL_A8;
			}
			if (this.ᜁ.ᜁ() == sprᣴ.ᜅ)
			{
				num = 3;
				continue;
			}
			goto IL_F1;
			IL_A8:
			if (sprᣴ.ᜈ(text))
			{
				num = 2;
				continue;
			}
			goto IL_D2;
			IL_F1:
			if (true)
			{
			}
			num = 0;
		}
		Block_2:
		if (false)
		{
		}
		this.ᜃ(A_0 + HyperlinksCollectionEditor.b("᨟", a_) + text);
		return;
		IL_D2:
		this.ᜁ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("感倡䌣唥眧挩䈫堭儯帱崳刵縷唹主匽㔿⹁╃ͅぇ㩉㹋⭍⍏⅑", a_)));
		return;
		IL_11E:
		FormulaTokenCode a_2 = FormulaTokenCode.Ref2;
		this.ᜀ(a_2, A_0);
	}

	// Token: 0x06000606 RID: 1542 RVA: 0x0003A68C File Offset: 0x0003968C
	private bool ᜋ(string A_0)
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
		return false;
	}

	// Token: 0x06000607 RID: 1543 RVA: 0x0003A6C8 File Offset: 0x000396C8
	private bool ᜊ(string A_0)
	{
		int num = 6;
		for (;;)
		{
			IL_12:
			switch (num)
			{
			case 0:
			{
				bool flag;
				if (flag)
				{
					num = 2;
					continue;
				}
				return flag;
			}
			case 1:
				goto IL_A5;
			case 2:
				this.ᜀ('(', false);
				num = 3;
				continue;
			case 3:
			{
				bool flag;
				return flag;
			}
			case 4:
				num = 5;
				continue;
			case 5:
				while (A_0.Length != 0)
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
						bool flag = spr\u2006.ᜀ().ᜁ(A_0);
						num = 0;
						goto IL_12;
					}
					}
				}
				num = 1;
				continue;
			case 6:
				if (true)
				{
				}
				break;
			}
			if (A_0 == null)
			{
				break;
			}
			num = 4;
		}
		return false;
		IL_A5:
		return false;
	}

	// Token: 0x06000608 RID: 1544 RVA: 0x0003A794 File Offset: 0x00039794
	private bool ᜉ(string A_0)
	{
		while (this.ᜁ.ᜁ() != sprᣴ.ᜅ)
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
				return sprᣴ.ᜈ(A_0);
			}
		}
		return true;
	}

	// Token: 0x06000609 RID: 1545 RVA: 0x0003A7EC File Offset: 0x000397EC
	private void ᜈ(string A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
		{
			byte b;
			for (;;)
			{
				for (;;)
				{
					this.ᜃ = true;
					this.ᜄ = spr\u2006.ᜀ().ᜀ(A_0);
					b = this.ᜉ();
					this.ᜃ = false;
					this.ᜂ(')');
					spr\u2487 spr_u = spr\u2006.ᜀ().ᜀ(A_0);
					byte b2 = spr_u.ᜀ();
					int num = 6;
					for (;;)
					{
						string text;
						string text2;
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							num = 3;
							continue;
						case 1:
							this.ᜁ(string.Concat(new object[]
							{
								HyperlinksCollectionEditor.b("栭弯䀱夳䌵吷嬹᰻堽㔿ⱁ❃㉅ⅇ╉≋瑍灏", a_),
								spr\u2006.ᜀ().ᜀ(spr_u.ᜁ()).ᜄ(),
								HyperlinksCollectionEditor.b("อ帯圱儳刵䬷ᨹ", a_),
								b2,
								text
							}));
							num = 4;
							continue;
						case 2:
							text2 = HyperlinksCollectionEditor.b("อ䀯匱䘳圵唷弹䠻嬽㈿ㅁ橃", a_);
							goto IL_1A1;
						case 3:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								if (b2 == 1)
								{
									num = 8;
									continue;
								}
								num = 2;
								continue;
							}
							break;
						case 4:
							goto IL_12A;
						case 5:
							text2 = HyperlinksCollectionEditor.b("อ䀯匱䘳圵唷弹䠻嬽㈿汁", a_);
							goto IL_1A1;
						case 6:
							if (b2 != 255)
							{
								num = 0;
								continue;
							}
							goto IL_1C3;
						case 7:
							if (b != b2)
							{
								num = 1;
								continue;
							}
							goto IL_1C3;
						case 8:
							num = 5;
							continue;
						}
						break;
						IL_1A1:
						text = text2;
						num = 7;
					}
				}
			}
			IL_12A:
			IL_1C3:
			this.ᜀ(spr\u1C33.ᜀ(A_0, this.ᜄ.ᜅ(), b));
			return;
		}
		}
	}

	// Token: 0x0600060A RID: 1546 RVA: 0x0003A9D4 File Offset: 0x000399D4
	private void ᜇ(string A_0)
	{
		while (this.ᜂ('('))
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
				this.ᜁ(')');
				this.ᜀ(spr\u1C33.ᜀ(A_0, FormulaTokenClass.Reference, 0));
				return;
			}
		}
		this.ᜀ(FormulaTokenCode.Bool, bool.Parse(A_0));
	}

	// Token: 0x0600060B RID: 1547 RVA: 0x0003AA48 File Offset: 0x00039A48
	private void ᜆ(string A_0)
	{
		object[] a_;
		while (this.ᜃ)
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
				a_ = new object[]
				{
					A_0,
					this.ᜅ
				};
				this.ᜀ(FormulaTokenCode.Name1, a_);
				return;
			}
		}
		a_ = new object[]
		{
			A_0,
			this.ᜅ
		};
		this.ᜀ(FormulaTokenCode.Name2, a_);
	}

	// Token: 0x0600060C RID: 1548 RVA: 0x0003AACC File Offset: 0x00039ACC
	private void ᜅ(string A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			string text2;
			for (;;)
			{
				string text = this.ᜃ();
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜃ)
						{
							num = 12;
							continue;
						}
						num = 8;
						continue;
					case 1:
						goto IL_131;
					case 2:
						if (sprᣴ.ᜈ(text2))
						{
							num = 13;
							continue;
						}
						goto IL_99;
					case 3:
						if (this.ᜃ)
						{
							num = 9;
							continue;
						}
						num = 4;
						continue;
					case 4:
						goto IL_C4;
					case 5:
						if (text == string.Empty)
						{
							num = 10;
							continue;
						}
						goto IL_CB;
					case 6:
						goto IL_CB;
					case 7:
						if (this.ᜂ(':'))
						{
							num = 11;
							continue;
						}
						num = 0;
						continue;
					case 8:
						goto IL_195;
					case 9:
						num = 14;
						continue;
					case 10:
						if (true)
						{
						}
						this.ᜁ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("帞匠䐢嘤砦怨䔪嬬丮崰娲儴然嘸䤺值䨾ⵀ≂D㽆㥈㥊⡌㱎≐", a_)));
						num = 6;
						continue;
					case 11:
						text2 = this.ᜃ();
						num = 2;
						continue;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_195;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 13:
						num = 3;
						continue;
					case 14:
						goto IL_1A8;
					}
					break;
					IL_CB:
					A_0 = A_0 + HyperlinksCollectionEditor.b("㸞", a_) + text;
					num = 7;
				}
			}
			IL_99:
			this.ᜁ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("帞匠䐢嘤砦怨䔪嬬丮崰娲儴然嘸䤺值䨾ⵀ≂D㽆㥈㥊⡌㱎≐", a_)));
			return;
			IL_C4:
			FormulaTokenCode formulaTokenCode = FormulaTokenCode.Area3d2;
			goto IL_1D8;
			IL_131:
			FormulaTokenCode formulaTokenCode2 = FormulaTokenCode.Ref3d1;
			goto IL_238;
			IL_195:
			formulaTokenCode2 = FormulaTokenCode.Ref3d2;
			goto IL_238;
			IL_1A8:
			formulaTokenCode = FormulaTokenCode.Area3d1;
			IL_1D8:
			FormulaTokenCode a_2 = formulaTokenCode;
			object[] a_3 = new object[]
			{
				A_0 + HyperlinksCollectionEditor.b("┞", a_) + text2,
				this.ᜅ
			};
			this.ᜀ(a_2, a_3);
			return;
			IL_238:
			FormulaTokenCode a_4 = formulaTokenCode2;
			a_3 = new object[]
			{
				A_0,
				this.ᜅ
			};
			this.ᜀ(a_4, a_3);
			return;
		}
		}
	}

	// Token: 0x0600060D RID: 1549 RVA: 0x0003AD38 File Offset: 0x00039D38
	private bool ᜄ(string A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					IEnumerator enumerator = this.ᜅ.ᜀ.Sheets.GetEnumerator();
					num = 8;
					continue;
				}
				case 1:
					num = 6;
					continue;
				case 2:
					if (this.ᜅ.ᜀ.Sheets.Count > 0)
					{
						num = 0;
						continue;
					}
					goto IL_81;
				case 3:
					goto IL_AE;
				case 4:
					if (this.ᜅ.ᜀ.Sheets != null)
					{
						num = 10;
						continue;
					}
					goto IL_81;
				case 5:
					goto IL_19D;
				case 6:
					goto IL_1E9;
				case 7:
					if (this.ᜅ.ᜀ.SheetName.Equals(A_0))
					{
						num = 3;
						continue;
					}
					return false;
				case 8:
					try
					{
						num = 3;
						bool result;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 1;
									continue;
								}
								WorkSheet workSheet = (WorkSheet)enumerator.Current;
								num = 5;
								continue;
							}
							case 1:
								num = 4;
								continue;
							case 2:
								goto IL_111;
							case 4:
								goto IL_153;
							case 5:
							{
								WorkSheet workSheet;
								if (workSheet.SheetName.Equals(A_0))
								{
									num = 6;
									continue;
								}
								break;
							}
							case 6:
								result = this.ᜂ('!');
								num = 2;
								continue;
							}
							IL_E3:
							num = 0;
							continue;
							goto IL_E3;
						}
						IL_111:
						return result;
						IL_153:
						return false;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable = enumerator as IDisposable;
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_19A;
								case 1:
									disposable.Dispose();
									num = 0;
									continue;
								case 2:
									if (disposable != null)
									{
										num = 1;
										continue;
									}
									goto IL_19C;
								}
								break;
							}
						}
						IL_19A:
						IL_19C:;
					}
					goto IL_19D;
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E9;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 10:
					num = 2;
					continue;
				}
				if (this.ᜅ != null)
				{
					num = 1;
					continue;
				}
				return false;
				IL_81:
				num = 7;
				continue;
				IL_19D:
				if (true)
				{
				}
				num = 4;
				continue;
				IL_1E9:
				if (this.ᜅ.ᜀ == null)
				{
					return false;
				}
				num = 5;
			}
			IL_AE:
			return this.ᜂ('!');
		}
		}
	}

	// Token: 0x0600060E RID: 1550 RVA: 0x0003AFC8 File Offset: 0x00039FC8
	private void ᜄ()
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
		this.\u1713();
		this.ᜁ(')');
		this.ᜀ(FormulaTokenCode.Parentheses);
	}

	// Token: 0x0600060F RID: 1551 RVA: 0x0003B01C File Offset: 0x0003A01C
	private string ᜃ()
	{
		int a_ = 15;
		string text;
		for (;;)
		{
			IL_39:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_9B:
				num = 5;
				break;
			default:
				if (false)
				{
				}
				text = string.Empty;
				num = 7;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_90;
				case 1:
					text = sprᣴ.ᜅ + this.ᜁ.ᜀ(false);
					num = 3;
					continue;
				case 2:
					goto IL_7D;
				case 3:
					goto IL_A8;
				case 4:
					if (this.ᜁ(sprᣴ.ᜅ, false))
					{
						num = 9;
						continue;
					}
					goto IL_7D;
				case 5:
					this.ᜁ(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("横弬䠮䈰氲簴夶伸娺儼嘾╀Ղ⩄㕆⑈㹊⅌⹎ᑐ⭒╔╖㱘⡚⹜", a_)));
					num = 6;
					continue;
				case 6:
					return text;
				case 7:
					if (this.ᜂ(sprᣴ.ᜅ))
					{
						num = 1;
						continue;
					}
					text = this.ᜁ.ᜀ(false);
					num = 8;
					continue;
				case 8:
					goto IL_A8;
				case 9:
					text = text + sprᣴ.ᜅ + this.ᜁ.ᜀ(false);
					num = 2;
					continue;
				}
				goto IL_39;
				IL_7D:
				if (true)
				{
				}
				num = 0;
				continue;
				IL_A8:
				num = 4;
			}
			IL_90:
			if (!sprᣴ.ᜈ(text))
			{
				goto IL_9B;
			}
			break;
		}
		return text;
	}

	// Token: 0x06000610 RID: 1552 RVA: 0x0003B1A0 File Offset: 0x0003A1A0
	private ushort ᜂ()
	{
		while (this.ᜂ.Count != 0)
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
				return (ushort)(this.ᜂ[this.ᜂ.Count - 1] as sprạ).\u170D();
			}
		}
		if (true)
		{
		}
		return 0;
	}

	// Token: 0x06000611 RID: 1553 RVA: 0x0003B210 File Offset: 0x0003A210
	private void ᜃ(string A_0)
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
		this.ᜀ(FormulaTokenCode.Area1, A_0);
	}

	// Token: 0x06000612 RID: 1554 RVA: 0x0003B254 File Offset: 0x0003A254
	private FormulaTokenCode ᜁ()
	{
		switch (0)
		{
		default:
		{
			int num = 5;
			char c;
			int num2;
			char c2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (sprᦤ.ᜂ.Contains(c.ToString()))
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					num = 7;
					continue;
				case 1:
					return FormulaTokenCode.Empty;
				case 2:
					goto IL_CA;
				case 3:
					return FormulaTokenCode.Empty;
				case 4:
					if (!sprᦤ.ᜂ.Contains(num2.ToString()))
					{
						num = 1;
						continue;
					}
					goto IL_153;
				case 6:
					return FormulaTokenCode.Empty;
				case 7:
					if (this.ᜁ.ᜀ(1) != '@')
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
							c2 = this.ᜁ.ᜀ(1);
							num2 = (int)(c + c2);
							num = 4;
							continue;
						}
					}
					num = 6;
					continue;
				}
				if (this.ᜁ.ᜀ(1) == '@')
				{
					num = 3;
				}
				else
				{
					c = this.ᜁ.ᜁ();
					num = 0;
				}
			}
			return FormulaTokenCode.Empty;
			IL_CA:
			int num3 = (int)sprᦤ.ᜁ[c.ToString()];
			FormulaTokenCode result = (FormulaTokenCode)num3;
			this.ᜁ.ᜀ();
			return result;
			IL_153:
			num2 = (int)(c + c2);
			FormulaTokenCode result2 = (FormulaTokenCode)sprᦤ.ᜁ[num2.ToString()];
			this.ᜁ.ᜀ();
			this.ᜁ.ᜀ();
			return result2;
		}
		}
	}

	// Token: 0x06000613 RID: 1555 RVA: 0x0003B3EC File Offset: 0x0003A3EC
	private bool ᜂ(char A_0)
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
		return this.ᜁ(A_0, true);
	}

	// Token: 0x06000614 RID: 1556 RVA: 0x0003B430 File Offset: 0x0003A430
	private bool ᜁ(char A_0, bool A_1)
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
		return this.ᜁ.ᜀ(A_0, A_1) != '@';
	}

	// Token: 0x06000615 RID: 1557 RVA: 0x0003B480 File Offset: 0x0003A480
	private bool ᜀ(char[] A_0)
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
		return this.ᜁ.ᜁ(A_0) != '@';
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0003B4D0 File Offset: 0x0003A4D0
	private string ᜀ()
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			stringBuilder = new StringBuilder();
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜁ.ᜈ())
					{
						num = 1;
						continue;
					}
					stringBuilder.Append(this.ᜁ.ᜀ());
					num = 2;
					continue;
				case 1:
					goto IL_71;
				case 2:
					goto IL_73;
				case 3:
					num = 0;
					continue;
				case 4:
					goto IL_7B;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_7B;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						goto IL_73;
					}
					break;
				}
				break;
				IL_73:
				num = 4;
				continue;
				IL_7B:
				if (!char.IsLetterOrDigit(this.ᜁ.ᜁ()))
				{
					goto IL_C3;
				}
				num = 3;
			}
		}
		IL_71:
		IL_C3:
		return stringBuilder.ToString();
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0003B5A8 File Offset: 0x0003A5A8
	private void ᜁ(char A_0)
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
		this.ᜀ(A_0, true);
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0003B5EC File Offset: 0x0003A5EC
	private void ᜀ(char A_0, bool A_1)
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
					continue;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					break;
				}
				break;
			case 1:
				goto IL_85;
			case 2:
				if (this.ᜁ.ᜁ() != A_0)
				{
					num = 1;
					continue;
				}
				goto IL_9C;
			case 3:
				this.ᜁ.ᜂ();
				num = 4;
				continue;
			case 4:
				goto IL_67;
			}
			if (A_1)
			{
				num = 3;
				continue;
			}
			IL_67:
			num = 2;
		}
		IL_85:
		this.ᜀ(A_0);
		return;
		IL_9C:
		this.ᜁ.ᜀ();
	}

	// Token: 0x06000619 RID: 1561 RVA: 0x0003B6A4 File Offset: 0x0003A6A4
	private void ᜂ(string A_0)
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
		this.ᜁ = new spr\u25AE(A_0);
		this.ᜁ.ᜂ();
	}

	// Token: 0x0600061A RID: 1562 RVA: 0x0003B6F8 File Offset: 0x0003A6F8
	private void ᜁ(string A_0)
	{
		int a_ = 2;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		throw new ArgumentException(HyperlinksCollectionEditor.b("堝伟倡䤣匥䐧䬩ఫ欭䈯䀱嬳䐵ȷᨹ", a_) + this.ᜁ.ᜃ() + HyperlinksCollectionEditor.b("㈝\u001f", a_) + A_0);
	}

	// Token: 0x0600061B RID: 1563 RVA: 0x0003B770 File Offset: 0x0003A770
	private void ᜀ(char A_0)
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
		this.ᜀ(A_0.ToString());
	}

	// Token: 0x0600061C RID: 1564 RVA: 0x0003B7B8 File Offset: 0x0003A7B8
	private void ᜀ(string A_0)
	{
		int a_ = 0;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		this.ᜁ(HyperlinksCollectionEditor.b("䠛瘝䔟ȡ倣䤥䌧伩䈫อ匯匱娳ᘵ嘷唹䠻ḽ∿❁摃⁅❇㽉≋⩍灏㭑㩓癕㹗㕙⹛㍝ᕟ๡գ䙥൧ቩᱫᱭᕯűݳ噵䉷婹", a_) + A_0);
	}

	// Token: 0x0600061D RID: 1565 RVA: 0x0003B818 File Offset: 0x0003A818
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u21BC()
	{
		int a_ = 0;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		spr\u21BC.ᜀ = new ArrayList(new string[]
		{
			HyperlinksCollectionEditor.b("䠛䰝生朡", a_),
			HyperlinksCollectionEditor.b("娛弝氟無愣", a_)
		});
	}

	// Token: 0x04000596 RID: 1430
	private static readonly ArrayList ᜀ;

	// Token: 0x04000597 RID: 1431
	private spr\u25AE ᜁ;

	// Token: 0x04000598 RID: 1432
	private ArrayList ᜂ = new ArrayList();

	// Token: 0x04000599 RID: 1433
	private bool ᜃ;

	// Token: 0x0400059A RID: 1434
	private spr\u2487 ᜄ;

	// Token: 0x0400059B RID: 1435
	private WorkSheet ᜅ;

	// Token: 0x0400059C RID: 1436
	private int ᜆ;

	// Token: 0x0400059D RID: 1437
	private bool ᜇ;
}
