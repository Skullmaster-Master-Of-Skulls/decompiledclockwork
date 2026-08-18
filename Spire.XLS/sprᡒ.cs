using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Threading;
using Spire.Xls.Calculation;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x0200054A RID: 1354
[DefaultMember("Item")]
internal class sprᡒ : IFormulaEngine, IDisposable
{
	// Token: 0x06005234 RID: 21044 RVA: 0x003335B4 File Offset: 0x003325B4
	public sprᡒ()
	{
		int a_ = 13;
		this.ᜎ = RecordTableEnumerator.b("扂畄晆ࡈ", a_);
		this.ᜏ = true;
		this.ᜐ = true;
		base..ctor();
		this.ᜂ(false);
	}

	// Token: 0x06005235 RID: 21045 RVA: 0x003335FC File Offset: 0x003325FC
	public sprᡒ(bool A_0)
	{
		int a_ = 0;
		this.ᜎ = RecordTableEnumerator.b("᜵࠷ᬹ紻", a_);
		this.ᜏ = true;
		this.ᜐ = true;
		base..ctor();
		this.ᜂ(A_0);
	}

	// Token: 0x06005236 RID: 21046 RVA: 0x00333644 File Offset: 0x00332644
	public void ᜀ(ValueChangedEventHandler A_0)
	{
		for (;;)
		{
			ValueChangedEventHandler valueChangedEventHandler = this.\u1712;
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					ValueChangedEventHandler valueChangedEventHandler2;
					if (valueChangedEventHandler == valueChangedEventHandler2)
					{
						num = 2;
						continue;
					}
					goto IL_2D;
				}
				case 1:
					IL_2B:
					goto IL_2D;
				case 2:
					return;
				}
				break;
				IL_2D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2B;
				default:
				{
					if (false)
					{
					}
					ValueChangedEventHandler valueChangedEventHandler2 = valueChangedEventHandler;
					ValueChangedEventHandler value = (ValueChangedEventHandler)Delegate.Combine(valueChangedEventHandler2, A_0);
					valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.\u1712, value, valueChangedEventHandler2);
					num = 0;
					break;
				}
				}
			}
		}
	}

	// Token: 0x06005237 RID: 21047 RVA: 0x003336DC File Offset: 0x003326DC
	public void ᜁ(ValueChangedEventHandler A_0)
	{
		for (;;)
		{
			ValueChangedEventHandler valueChangedEventHandler = this.\u1712;
			if (true)
			{
			}
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					IL_2B:
					goto IL_2D;
				case 1:
				{
					ValueChangedEventHandler valueChangedEventHandler2;
					if (valueChangedEventHandler == valueChangedEventHandler2)
					{
						num = 2;
						continue;
					}
					goto IL_2D;
				}
				case 2:
					return;
				}
				break;
				IL_2D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2B;
				default:
				{
					if (false)
					{
					}
					ValueChangedEventHandler valueChangedEventHandler2 = valueChangedEventHandler;
					ValueChangedEventHandler value = (ValueChangedEventHandler)Delegate.Remove(valueChangedEventHandler2, A_0);
					valueChangedEventHandler = Interlocked.CompareExchange<ValueChangedEventHandler>(ref this.\u1712, value, valueChangedEventHandler2);
					num = 1;
					break;
				}
				}
			}
		}
	}

	// Token: 0x06005238 RID: 21048 RVA: 0x00333774 File Offset: 0x00332774
	public void ᜀ(spr\u1775 A_0)
	{
		for (;;)
		{
			spr\u1775 spr_u = this.\u1713;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					IL_23:
					goto IL_25;
				case 2:
				{
					spr\u1775 spr_u2;
					if (spr_u == spr_u2)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_25;
				}
				}
				break;
				IL_25:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_23;
				default:
				{
					if (false)
					{
					}
					spr\u1775 spr_u2 = spr_u;
					spr\u1775 value = (spr\u1775)Delegate.Combine(spr_u2, A_0);
					spr_u = Interlocked.CompareExchange<spr\u1775>(ref this.\u1713, value, spr_u2);
					num = 2;
					break;
				}
				}
			}
		}
	}

	// Token: 0x06005239 RID: 21049 RVA: 0x0033380C File Offset: 0x0033280C
	public void ᜁ(spr\u1775 A_0)
	{
		for (;;)
		{
			spr\u1775 spr_u = this.\u1713;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
				{
					spr\u1775 spr_u2;
					if (spr_u == spr_u2)
					{
						if (true)
						{
						}
						num = 0;
						continue;
					}
					goto IL_25;
				}
				case 2:
					IL_23:
					goto IL_25;
				}
				break;
				IL_25:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_23;
				default:
				{
					if (false)
					{
					}
					spr\u1775 spr_u2 = spr_u;
					spr\u1775 value = (spr\u1775)Delegate.Remove(spr_u2, A_0);
					spr_u = Interlocked.CompareExchange<spr\u1775>(ref this.\u1713, value, spr_u2);
					num = 1;
					break;
				}
				}
			}
		}
	}

	// Token: 0x0600523A RID: 21050 RVA: 0x003338A4 File Offset: 0x003328A4
	public bool ᜂ()
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
		return this.\u170D;
	}

	// Token: 0x0600523B RID: 21051 RVA: 0x003338E8 File Offset: 0x003328E8
	public void ᜄ(bool A_0)
	{
		for (;;)
		{
			this.\u170D = A_0;
			this.ᜌ().ᜀ.ᜄ(!A_0);
			this.ᜌ().ᜀ.ᝡ = !A_0;
			this.ᜌ().ᜀ.ᜇ(A_0);
			if (true)
			{
			}
			int num = 2;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					this.ᜏ();
					num = 1;
					continue;
				case 1:
					return;
				case 2:
					while (A_0)
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
							num = 0;
							goto IL_02;
						}
					}
					return;
				}
				break;
			}
		}
	}

	// Token: 0x0600523C RID: 21052 RVA: 0x003339A4 File Offset: 0x003329A4
	private int ᜀ()
	{
		for (;;)
		{
			this.ᜅ++;
			if (true)
			{
			}
			int num = 1;
			for (;;)
			{
				IL_02:
				switch (num)
				{
				case 0:
					goto IL_7E;
				case 1:
					while (this.ᜅ == 2147483647)
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
							goto IL_02;
						}
					}
					goto IL_80;
				case 2:
					this.ᜅ = 1;
					num = 0;
					continue;
				}
				break;
			}
		}
		IL_7E:
		IL_80:
		return this.ᜅ;
	}

	// Token: 0x0600523D RID: 21053 RVA: 0x00333A38 File Offset: 0x00332A38
	public bool \u170D()
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
		return this.ᜏ;
	}

	// Token: 0x0600523E RID: 21054 RVA: 0x00333A7C File Offset: 0x00332A7C
	public void ᜁ(bool A_0)
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
		this.ᜏ = A_0;
	}

	// Token: 0x0600523F RID: 21055 RVA: 0x00333AC0 File Offset: 0x00332AC0
	protected Hashtable ᜆ()
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
		return this.ᜆ;
	}

	// Token: 0x06005240 RID: 21056 RVA: 0x00333B04 File Offset: 0x00332B04
	protected spr\u227A ᜊ()
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
		return this.ᜇ;
	}

	// Token: 0x06005241 RID: 21057 RVA: 0x00333B48 File Offset: 0x00332B48
	public bool ᜈ()
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
		return this.ᜐ;
	}

	// Token: 0x06005242 RID: 21058 RVA: 0x00333B8C File Offset: 0x00332B8C
	public void ᜀ(bool A_0)
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
		this.ᜐ = A_0;
	}

	// Token: 0x06005243 RID: 21059 RVA: 0x00333BD0 File Offset: 0x00332BD0
	public FormulaEngine ᜌ()
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
		return this.ᜈ;
	}

	// Token: 0x06005244 RID: 21060 RVA: 0x00333C14 File Offset: 0x00332C14
	public char ᜃ()
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
		return FormulaEngine.FormulaCharacter;
	}

	// Token: 0x06005245 RID: 21061 RVA: 0x00333C54 File Offset: 0x00332C54
	public void ᜀ(char A_0)
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
		FormulaEngine.FormulaCharacter = A_0;
	}

	// Token: 0x06005246 RID: 21062 RVA: 0x00333C98 File Offset: 0x00332C98
	protected Hashtable ᜅ()
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
		return this.ᜉ;
	}

	// Token: 0x06005247 RID: 21063 RVA: 0x00333CDC File Offset: 0x00332CDC
	protected Hashtable ᜉ()
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
		return this.ᜊ;
	}

	// Token: 0x06005248 RID: 21064 RVA: 0x00333D20 File Offset: 0x00332D20
	protected Hashtable ᜑ()
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
		return this.ᜋ;
	}

	// Token: 0x06005249 RID: 21065 RVA: 0x00333D64 File Offset: 0x00332D64
	protected Hashtable ᜋ()
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
		return this.ᜌ;
	}

	// Token: 0x0600524A RID: 21066 RVA: 0x00333DA8 File Offset: 0x00332DA8
	public string ᜃ(string A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				A_0 = A_0.ToUpper();
				int num = 10;
				for (;;)
				{
					string text;
					spr\u20D1 spr_u20D;
					switch (num)
					{
					case 0:
						goto IL_A4;
					case 1:
						goto IL_1B2;
					case 2:
						num = 6;
						continue;
					case 3:
						if (this.ᜉ().ContainsKey(A_0))
						{
							num = 5;
							continue;
						}
						goto IL_14C;
					case 4:
						if (this.\u1713 != null)
						{
							num = 8;
							continue;
						}
						goto IL_13A;
					case 5:
						goto IL_107;
					case 6:
						if (text[0] == FormulaEngine.FormulaCharacter)
						{
							num = 11;
							continue;
						}
						goto IL_13A;
					case 7:
						if (text.Length > 0)
						{
							num = 2;
							continue;
						}
						goto IL_13A;
					case 8:
						this.\u1713(this, new sprᝩ(A_0, spr_u20D.ᜂ(), FormulaInfoSetAction.CalculatedValueSet));
						num = 0;
						continue;
					case 9:
						goto IL_2F9;
					case 10:
						if (this.ᜊ().ContainsKey(A_0))
						{
							num = 9;
							continue;
						}
						num = 3;
						continue;
					case 11:
						num = 12;
						continue;
					case 12:
						if (spr_u20D.ᜃ != this.ᜌ().ᜀ.\u171F())
						{
							num = 1;
							continue;
						}
						goto IL_13A;
					case 13:
						try
						{
							spr_u20D.ᜁ(this.ᜌ().ᜀ.\u177B(this.ᜀ(text)));
							goto IL_152;
						}
						catch (Exception ex)
						{
							if (this.\u170D())
							{
								spr_u20D.ᜀ(ex.Message);
								spr_u20D.ᜃ = this.ᜌ().ᜀ.\u171F();
								if (this.\u1713 != null)
								{
									goto IL_291;
								}
							}
							else
							{
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_291;
								default:
									if (false)
									{
									}
									goto IL_152;
								}
							}
							IL_279:
							return this.ᜊ().ᜀ(A_0).ᜂ();
							IL_291:
							this.\u1713(this, new sprᝩ(A_0, spr_u20D.ᜂ(), FormulaInfoSetAction.CalculatedValueSet));
							goto IL_279;
						}
						goto IL_2F9;
						try
						{
							IL_152:
							spr_u20D.ᜀ(this.ᜌ().ᜀ.អ(spr_u20D.ᜀ()));
							goto IL_20E;
						}
						catch (Exception ex2)
						{
							if (this.ᜄ() && ex2.Message.StartsWith(this.ᜌ().ᜀ.\u1759[this.ᜌ().ᜀ.ᝇ]))
							{
								throw ex2;
							}
							goto IL_20E;
						}
						goto IL_1B2;
						IL_20E:
						spr_u20D.ᜃ = this.ᜌ().ᜀ.\u171F();
						num = 4;
						continue;
					}
					break;
					IL_1B2:
					if (true)
					{
					}
					this.ᜌ().ᜀ.\u173E = this.ᜎ + this.ᜅ()[A_0].ToString();
					text = text.Substring(1);
					num = 13;
					continue;
					IL_2F9:
					spr_u20D = this.ᜊ().ᜀ(A_0);
					text = spr_u20D.ᜁ();
					num = 7;
				}
			}
			IL_A4:
			goto IL_13A;
			IL_107:
			return this.ᜉ()[A_0].ToString();
			IL_13A:
			return this.ᜊ().ᜀ(A_0).ᜂ();
			IL_14C:
			return string.Empty;
		}
	}

	// Token: 0x0600524B RID: 21067 RVA: 0x00334114 File Offset: 0x00333114
	public void ᜀ(string A_0, string A_1)
	{
		int a_ = 1;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0 = A_0.ToUpper();
				string text = A_1.ToString().Trim();
				int num = 33;
				for (;;)
				{
					spr\u20D1 spr_u20D;
					int num2;
					string[] array;
					switch (num)
					{
					case 0:
						if (!this.ᜉ().ContainsKey(A_0))
						{
							num = 13;
							continue;
						}
						goto IL_41F;
					case 1:
						goto IL_41F;
					case 2:
						spr_u20D.ᜂ(text);
						num = 11;
						continue;
					case 3:
						num = 12;
						continue;
					case 4:
						this.ᜉ().Remove(A_0);
						num = 25;
						continue;
					case 5:
						if (!this.ᜑ)
						{
							num = 23;
							continue;
						}
						goto IL_29D;
					case 6:
						num = 41;
						continue;
					case 7:
						return;
					case 8:
						goto IL_29D;
					case 9:
						if (spr_u20D.ᜁ() != null)
						{
							num = 6;
							continue;
						}
						goto IL_29D;
					case 10:
						if (text.StartsWith(RecordTableEnumerator.b("䰶", a_)))
						{
							num = 38;
							continue;
						}
						this.ᜊ().Add(A_0, new spr\u20D1());
						this.ᜅ().Add(A_0, this.ᜅ().Count + 1);
						this.ᜋ().Add(this.ᜋ().Count + 1, A_0);
						num = 16;
						continue;
					case 11:
						if (this.\u1713 != null)
						{
							num = 27;
							continue;
						}
						goto IL_2C3;
					case 12:
						if (text.StartsWith(RecordTableEnumerator.b("䰶", a_)))
						{
							num = 26;
							continue;
						}
						goto IL_3BB;
					case 13:
						this.ᜉ().Add(A_0, string.Empty);
						num = 1;
						continue;
					case 14:
					{
						if (num2 >= array.Length)
						{
							num = 28;
							continue;
						}
						string a_2 = array[num2];
						string text2 = string.Format(RecordTableEnumerator.b("昶昸䀺഼䈾", a_), this.ᜅ().Count + 1);
						this.ᜊ().Add(text2, new spr\u20D1());
						this.ᜅ().Add(text2, this.ᜅ().Count + 1);
						this.ᜋ().Add(this.ᜋ().Count + 1, text2);
						spr\u20D1 spr_u20D2 = this.ᜊ().ᜀ(text2);
						spr_u20D2.ᜂ(string.Empty);
						spr_u20D2.ᜁ(string.Empty);
						spr_u20D2.ᜀ(this.ᜅ(a_2));
						num2++;
						num = 20;
						continue;
					}
					case 15:
						if (this.ᜂ())
						{
							num = 35;
							continue;
						}
						return;
					case 16:
						goto IL_3BB;
					case 17:
					{
						if (true)
						{
						}
						object obj;
						if (obj != null)
						{
							num = 31;
							continue;
						}
						goto IL_29D;
					}
					case 18:
						goto IL_2C3;
					case 19:
						this.\u1713(this, new sprᝩ(A_0, text, FormulaInfoSetAction.NonFormulaSet));
						num = 18;
						continue;
					case 20:
						goto IL_317;
					case 21:
						goto IL_545;
					case 22:
						if (spr_u20D.ᜁ() != text)
						{
							num = 39;
							continue;
						}
						goto IL_29D;
					case 23:
						num = 9;
						continue;
					case 24:
						num = 34;
						continue;
					case 25:
						goto IL_577;
					case 26:
						goto IL_62B;
					case 27:
						this.\u1713(this, new sprᝩ(A_0, text, FormulaInfoSetAction.FormulaSet));
						num = 21;
						continue;
					case 28:
						return;
					case 29:
						goto IL_317;
					case 30:
						if (this.\u1713 != null)
						{
							num = 19;
							continue;
						}
						goto IL_2C3;
					case 31:
					{
						string text3;
						this.ᜌ().ᜀ.\u1734(text3);
						num = 8;
						continue;
					}
					case 32:
						spr_u20D.ᜂ(string.Empty);
						spr_u20D.ᜁ(string.Empty);
						spr_u20D.ᜀ(text);
						num = 30;
						continue;
					case 33:
						if (this.ᜊ().ContainsKey(A_0))
						{
							num = 3;
							continue;
						}
						goto IL_62B;
					case 34:
						if (text[0] == FormulaEngine.FormulaCharacter)
						{
							num = 2;
							continue;
						}
						goto IL_239;
					case 35:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_545;
						default:
							if (false)
							{
							}
							this.ᜂ(A_0);
							num = 7;
							continue;
						}
						break;
					case 36:
						if (spr_u20D.ᜂ() != text)
						{
							num = 32;
							continue;
						}
						goto IL_2C3;
					case 37:
						num = 22;
						continue;
					case 38:
						num = 0;
						continue;
					case 39:
					{
						string text3 = this.ᜎ + this.ᜅ()[A_0].ToString();
						object obj = this.ᜌ().ᜀ.ᜎ()[text3];
						num = 17;
						continue;
					}
					case 40:
						if (text.Length > 0)
						{
							num = 24;
							continue;
						}
						goto IL_239;
					case 41:
						if (spr_u20D.ᜁ().Length > 0)
						{
							num = 37;
							continue;
						}
						goto IL_29D;
					case 42:
						if (this.ᜉ().ContainsKey(A_0))
						{
							num = 4;
							continue;
						}
						goto IL_577;
					}
					break;
					IL_239:
					num = 36;
					continue;
					IL_29D:
					num = 40;
					continue;
					IL_2C3:
					num = 15;
					continue;
					IL_545:
					goto IL_2C3;
					IL_317:
					num = 14;
					continue;
					IL_3BB:
					num = 42;
					continue;
					IL_41F:
					text = text.Substring(1, text.Length - 2);
					int num3 = this.ᜅ().Count + 1;
					string[] array2 = text.Split(new char[]
					{
						FormulaEngine.ParseArgumentSeparator
					});
					string value = string.Format(RecordTableEnumerator.b("瘶䈸଺䀼Ծ@㡂瑄㩆", a_), num3, num3 + array2.GetLength(0) - 1);
					this.ᜉ()[A_0] = value;
					array = array2;
					num2 = 0;
					num = 29;
					continue;
					IL_577:
					spr_u20D = this.ᜊ().ᜀ(A_0);
					num = 5;
					continue;
					IL_62B:
					num = 10;
				}
			}
			return;
		}
	}

	// Token: 0x0600524C RID: 21068 RVA: 0x003347F4 File Offset: 0x003337F4
	public bool ᜄ()
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
		return this.ᜌ().ᜀ.ᜭ();
	}

	// Token: 0x0600524D RID: 21069 RVA: 0x00334840 File Offset: 0x00333840
	public void ᜃ(bool A_0)
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
		this.ᜌ().ᜀ.ᜉ(A_0);
	}

	// Token: 0x0600524E RID: 21070 RVA: 0x0033488C File Offset: 0x0033388C
	private bool ᜀ(string A_0, string A_1, bool A_2)
	{
		bool result;
		for (;;)
		{
			result = true;
			A_0 = A_0.Trim();
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Length > 0)
					{
						num = 2;
						continue;
					}
					goto IL_9A;
				case 1:
					goto IL_98;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_98;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 3:
					result = (A_1.IndexOf(A_0[A_2 ? 0 : (A_0.Length - 1)]) > -1);
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_98:
		IL_9A:
		if (true)
		{
		}
		return result;
	}

	// Token: 0x0600524F RID: 21071 RVA: 0x0033493C File Offset: 0x0033393C
	public virtual FormulaEngine ᜐ()
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
		return new FormulaEngine(this);
	}

	// Token: 0x06005250 RID: 21072 RVA: 0x00334980 File Offset: 0x00333980
	public void ᜁ()
	{
		for (;;)
		{
			this.ᜇ = null;
			this.ᜌ = null;
			this.ᜉ = null;
			this.ᜊ = null;
			this.ᜆ = null;
			this.ᜋ = null;
			this.ᜁ(new ValueChangedEventHandler(this.ᜈ.ᜀ.ᜀ));
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_98;
				case 1:
					if (this.ᜈ != null)
					{
						num = 2;
						continue;
					}
					goto IL_98;
				case 2:
					this.ᜈ.Dispose();
					num = 0;
					continue;
				case 3:
					this.ᜈ.ᜀ.ᜎ().Clear();
					this.ᜈ.ᜀ.\u171D().Clear();
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
				case 4:
					if (this.ᜈ())
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
				case 5:
					return;
				}
				break;
				IL_98:
				this.ᜈ = null;
				num = 5;
			}
		}
	}

	// Token: 0x06005251 RID: 21073 RVA: 0x00334AC0 File Offset: 0x00333AC0
	public string ᜄ(string A_0)
	{
		string result;
		for (;;)
		{
			result = "";
			try
			{
				result = this.ᜅ(A_0);
			}
			catch (Exception ex)
			{
				result = ex.Message;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				goto IL_32;
			}
		}
		IL_32:
		if (true)
		{
		}
		if (false)
		{
		}
		return result;
	}

	// Token: 0x06005252 RID: 21074 RVA: 0x00334B28 File Offset: 0x00333B28
	public string ᜁ(string A_0)
	{
		A_0 = A_0.ToUpper();
		if (!this.ᜊ().ContainsKey(A_0))
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
				return string.Empty;
			}
		}
		return this.ᜊ().ᜀ(A_0).ᜁ();
	}

	// Token: 0x06005253 RID: 21075 RVA: 0x00334B94 File Offset: 0x00333B94
	public object ᜀ(int A_0, int A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			string text;
			for (;;)
			{
				if (true)
				{
				}
				string a_2 = this.ᜋ()[A_0].ToString();
				text = this.ᜃ(a_2).ToString();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (text != null)
						{
							num = 2;
							continue;
						}
						return text;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DA;
						default:
							goto IL_158;
						}
						break;
					case 2:
						num = 3;
						continue;
					case 3:
						if (text.EndsWith(RecordTableEnumerator.b("᰸", a_)))
						{
							num = 6;
							continue;
						}
						return text;
					case 4:
						if (text.Length > 1)
						{
							goto IL_DA;
						}
						return text;
					case 5:
					{
						double num2;
						if (double.TryParse(text.Substring(0, text.Length - 1), NumberStyles.Any, null, out num2))
						{
							num = 7;
							continue;
						}
						return text;
					}
					case 6:
						num = 4;
						continue;
					case 7:
					{
						double num2;
						text = (num2 / 100.0).ToString();
						num = 1;
						continue;
					}
					case 8:
						num = 5;
						continue;
					}
					break;
					IL_DA:
					num = 8;
				}
			}
			IL_158:
			if (false)
			{
			}
			return text;
		}
		}
	}

	// Token: 0x06005254 RID: 21076 RVA: 0x00334D04 File Offset: 0x00333D04
	protected void ᜂ(bool A_0)
	{
		int a_ = 11;
		if (true)
		{
		}
		for (;;)
		{
			this.ᜇ = new spr\u227A();
			this.ᜌ = new Hashtable();
			this.ᜉ = new Hashtable();
			this.ᜊ = new Hashtable();
			this.ᜆ = new Hashtable();
			this.ᜋ = new Hashtable();
			this.ᜈ = this.ᜐ();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E8;
				case 1:
					if (A_0)
					{
						num = 2;
						continue;
					}
					goto IL_EA;
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
						FormulaEngine.ᜀ();
						this.ᜈ.ᜀ.ᜎ().Clear();
						this.ᜈ.ᜀ.\u171D().Clear();
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_E8:
		IL_EA:
		int num2 = FormulaEngine.ᜁ();
		this.ᜎ = string.Format(RecordTableEnumerator.b("恀㡂畄㩆案੊", a_), num2);
		this.ᜈ.ᜀ.ᜀ(sprḅ.ᜀ(this.ᜀ()), this, num2);
		this.ᜈ.ᜀ.ᜄ(true);
		this.ᜈ.ᜀ.ᝡ = true;
	}

	// Token: 0x06005255 RID: 21077 RVA: 0x00334E60 File Offset: 0x00333E60
	private string ᜀ(string A_0)
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			string text6;
			for (;;)
			{
				IL_D3:
				int num;
				int num2;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_360:
					num = 3;
					break;
				default:
					if (false)
					{
					}
					num2 = A_0.IndexOf('[');
					num = 39;
					break;
				}
				for (;;)
				{
					string text;
					string text2;
					string text3;
					string text4;
					string text5;
					string text7;
					string text8;
					string text9;
					switch (num)
					{
					case 0:
						num = 11;
						continue;
					case 1:
						if (this.\u170D())
						{
							num = 27;
							continue;
						}
						goto IL_245;
					case 2:
						goto IL_2DE;
					case 3:
						text = string.Empty;
						goto IL_48B;
					case 4:
						if (num2 <= 0)
						{
							num = 23;
							continue;
						}
						num = 15;
						continue;
					case 5:
						if (!this.ᜀ(text2, RecordTableEnumerator.b("湄橆捈摊獌獎汐൒籔策罘", a_), true))
						{
							num = 24;
							continue;
						}
						goto IL_109;
					case 6:
						num = 29;
						continue;
					case 7:
						num = 21;
						continue;
					case 8:
						text3 = A_0.Substring(0, num2);
						goto IL_461;
					case 9:
						num = 13;
						continue;
					case 10:
					{
						int num3;
						text4 = A_0.Substring(num2 + num3 + 2);
						goto IL_31E;
					}
					case 11:
						if (!this.ᜀ(text5, RecordTableEnumerator.b("湄橆捈摊獌獎汐൒絔策罘", a_), false))
						{
							num = 2;
							continue;
						}
						goto IL_3C3;
					case 12:
						goto IL_286;
					case 13:
					{
						int num3;
						if (num2 + num3 + 2 >= A_0.Length)
						{
							num = 6;
							continue;
						}
						num = 10;
						continue;
					}
					case 14:
						if (this.\u170D())
						{
							num = 0;
							continue;
						}
						goto IL_3C3;
					case 15:
						if (true)
						{
						}
						text = A_0.Substring(0, num2);
						goto IL_48B;
					case 16:
						if (this.\u170D())
						{
							num = 30;
							continue;
						}
						goto IL_109;
					case 17:
						goto IL_286;
					case 18:
						goto IL_176;
					case 19:
						if (this.ᜅ().Contains(text6))
						{
							num = 22;
							continue;
						}
						goto IL_1E4;
					case 20:
						text3 = string.Empty;
						goto IL_461;
					case 21:
						if (!this.ᜀ(text7, RecordTableEnumerator.b("湄橆捈摊獌獎汐൒籔策罘", a_), true))
						{
							num = 18;
							continue;
						}
						goto IL_440;
					case 22:
						num = 40;
						continue;
					case 23:
						goto IL_45C;
					case 24:
						goto IL_54E;
					case 25:
					{
						int num3;
						text6 = A_0.Substring(num2 + 1, num3).ToUpper();
						num = 34;
						continue;
					}
					case 26:
						num = 20;
						continue;
					case 27:
						num = 33;
						continue;
					case 28:
					{
						int num3;
						if (num3 > 0)
						{
							num = 25;
							continue;
						}
						num2 = -1;
						num = 12;
						continue;
					}
					case 29:
						text4 = string.Empty;
						goto IL_31E;
					case 30:
						num = 5;
						continue;
					case 31:
						goto IL_286;
					case 32:
						if (this.\u170D())
						{
							num = 7;
							continue;
						}
						goto IL_440;
					case 33:
						if (!this.ᜀ(text8, RecordTableEnumerator.b("湄橆捈摊獌獎汐൒絔策罘", a_), false))
						{
							num = 35;
							continue;
						}
						goto IL_245;
					case 34:
						if (this.ᜉ().Contains(text6))
						{
							num = 9;
							continue;
						}
						num = 19;
						continue;
					case 35:
						goto IL_1AF;
					case 36:
						num = 41;
						continue;
					case 37:
					{
						int num3;
						text9 = A_0.Substring(num2 + num3 + 2);
						goto IL_3F8;
					}
					case 38:
					{
						if (num2 <= -1)
						{
							num = 42;
							continue;
						}
						int num3 = A_0.Substring(num2).IndexOf(']') - 1;
						text6 = string.Empty;
						num = 28;
						continue;
					}
					case 39:
						goto IL_286;
					case 40:
					{
						int num3;
						if (num2 + num3 + 2 >= A_0.Length)
						{
							num = 36;
							continue;
						}
						num = 37;
						continue;
					}
					case 41:
						text9 = string.Empty;
						goto IL_3F8;
					case 42:
						return A_0;
					case 43:
						if (num2 <= 0)
						{
							num = 26;
							continue;
						}
						num = 8;
						continue;
					}
					goto IL_D3;
					IL_109:
					num = 43;
					continue;
					IL_245:
					A_0 = text8 + RecordTableEnumerator.b("ф", a_) + this.ᜅ()[text6].ToString() + text2;
					num2 = A_0.IndexOf('[');
					num = 31;
					continue;
					IL_286:
					num = 38;
					continue;
					IL_31E:
					text7 = text4;
					num = 32;
					continue;
					IL_3C3:
					A_0 = text5 + this.ᜉ()[text6].ToString() + text7;
					num2 = A_0.IndexOf('[');
					num = 17;
					continue;
					IL_3F8:
					text2 = text9;
					num = 16;
					continue;
					IL_440:
					num = 4;
					continue;
					IL_461:
					text8 = text3;
					num = 1;
					continue;
					IL_48B:
					text5 = text;
					num = 14;
				}
				IL_45C:
				goto IL_360;
			}
			IL_176:
			throw new ArgumentException(string.Format(RecordTableEnumerator.b("Ṅ㱆祈㙊၌潎㽐㱒⅔睖㽘㑚ㅜ㍞๠ᑢdͦ䥨᭪ὬnŰᙲݴ᭶x", a_), text6));
			IL_1AF:
			throw new ArgumentException(string.Format(RecordTableEnumerator.b("Ṅ㱆祈㙊၌潎㽐㱒⅔睖⥘⥚㡜㱞Ѡݢdͦ䥨᭪ὬnŰᙲݴ᭶x", a_), text6));
			IL_1E4:
			throw new ArgumentException(RecordTableEnumerator.b("၄⥆≈╊≌㡎㽐獒㹔㉖⁘慚絜", a_) + text6);
			IL_2DE:
			throw new ArgumentException(string.Format(RecordTableEnumerator.b("Ṅ㱆祈㙊၌潎㽐㱒⅔睖⥘⥚㡜㱞Ѡݢdͦ䥨᭪ὬnŰᙲݴ᭶x", a_), text6));
			IL_54E:
			throw new ArgumentException(string.Format(RecordTableEnumerator.b("Ṅ㱆祈㙊၌潎㽐㱒⅔睖㽘㑚ㅜ㍞๠ᑢdͦ䥨᭪ὬnŰᙲݴ᭶x", a_), text6));
		}
		}
	}

	// Token: 0x06005256 RID: 21078 RVA: 0x00335444 File Offset: 0x00334444
	public string ᜅ(string A_0)
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_99;
			case 2:
				if (A_0[0] != FormulaEngine.FormulaCharacter)
				{
					goto IL_9B;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_99;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 3:
				goto IL_5A;
			case 4:
				num = 2;
				continue;
			}
			if (true)
			{
			}
			if (A_0.Length > 0)
			{
				num = 4;
				continue;
			}
			break;
			IL_99:
			A_0 = A_0.Substring(1);
			num = 3;
		}
		IL_5A:
		IL_9B:
		return this.ᜌ().ᜀ.ឥ(this.ᜀ(A_0));
	}

	// Token: 0x06005257 RID: 21079 RVA: 0x00335504 File Offset: 0x00334504
	public void ᜎ()
	{
		for (;;)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					IEnumerator enumerator;
					switch (num)
					{
					case 0:
						try
						{
							num = 14;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (this.\u1713 != null)
									{
										num = 1;
										continue;
									}
									break;
								case 1:
								{
									string text;
									spr\u20D1 spr_u20D;
									this.\u1713(this, new sprᝩ(text, spr_u20D.ᜂ(), FormulaInfoSetAction.CalculatedValueSet));
									num = 3;
									continue;
								}
								case 2:
									num = 8;
									continue;
								case 4:
									num = 6;
									continue;
								case 5:
								{
									string text2 = text2.Substring(1);
									string text;
									this.ᜌ().ᜀ.\u173E = this.ᜎ + this.ᜅ()[text].ToString();
									spr\u20D1 spr_u20D;
									spr_u20D.ᜁ(this.ᜌ().ᜀ.\u177B(this.ᜀ(text2)));
									spr_u20D.ᜀ(this.ᜌ().ᜀ.អ(spr_u20D.ᜀ()));
									spr_u20D.ᜃ = this.ᜌ().ᜀ.\u171F();
									num = 12;
									continue;
								}
								case 6:
									goto IL_2B1;
								case 7:
								{
									if (!enumerator.MoveNext())
									{
										num = 4;
										continue;
									}
									string text = (string)enumerator.Current;
									spr\u20D1 spr_u20D = this.ᜊ().ᜀ(text);
									string text2 = spr_u20D.ᜁ();
									num = 9;
									continue;
								}
								case 8:
								{
									string text2;
									if (text2[0] == FormulaEngine.FormulaCharacter)
									{
										num = 10;
										continue;
									}
									goto IL_CE;
								}
								case 9:
								{
									string text2;
									if (text2.Length > 0)
									{
										num = 2;
										continue;
									}
									goto IL_CE;
								}
								case 10:
									num = 15;
									continue;
								case 11:
									goto IL_CE;
								case 12:
									if (this.\u1712 != null)
									{
										num = 13;
										continue;
									}
									goto IL_CE;
								case 13:
								{
									string text;
									spr\u20D1 spr_u20D;
									this.\u1712(this, new ValueChangedEventArgs((int)this.ᜅ()[text], 1, spr_u20D.ᜂ()));
									num = 11;
									continue;
								}
								case 15:
								{
									spr\u20D1 spr_u20D;
									if (spr_u20D.ᜃ != this.ᜌ().ᜀ.\u171F())
									{
										num = 5;
										continue;
									}
									goto IL_CE;
								}
								}
								goto IL_A1;
								IL_CE:
								num = 0;
								continue;
								IL_F0:
								num = 7;
								continue;
								IL_A1:
								goto IL_F0;
							}
							IL_2B1:
							goto IL_336;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable.Dispose();
										num = 2;
										continue;
									case 1:
										if (disposable != null)
										{
											if (true)
											{
											}
											num = 0;
											continue;
										}
										goto IL_305;
									case 2:
										goto IL_303;
									}
									break;
								}
							}
							IL_303:
							IL_305:;
						}
						goto IL_306;
					case 2:
						return;
					}
					if (!this.ᜂ())
					{
						num = 2;
						continue;
					}
					IL_306:
					this.ᜏ();
					this.ᜑ = true;
					enumerator = this.ᜊ().Keys.GetEnumerator();
					num = 0;
				}
				IL_336:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_34C;
				}
				break;
			}
			}
		}
		return;
		IL_34C:
		if (false)
		{
		}
		this.ᜑ = false;
	}

	// Token: 0x06005258 RID: 21080 RVA: 0x00335888 File Offset: 0x00334888
	public void ᜏ()
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
		this.ᜌ().ᜀ.\u1735();
	}

	// Token: 0x06005259 RID: 21081 RVA: 0x003358D4 File Offset: 0x003348D4
	public void ᜀ(object A_0, int A_1, int A_2)
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
	}

	// Token: 0x0600525A RID: 21082 RVA: 0x00335910 File Offset: 0x00334910
	public void ᜂ(string A_0)
	{
		if (true)
		{
		}
		ArrayList arrayList;
		IEnumerator enumerator;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_1A1:
			enumerator = arrayList.GetEnumerator();
			num = 2;
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			default:
				num = 1;
				break;
			}
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				string key = this.ᜎ + this.ᜅ()[A_0].ToString();
				arrayList = (this.ᜌ().ᜀ.\u171D()[key] as ArrayList);
				this.ᜏ();
				num = 4;
				continue;
			}
			case 2:
				goto IL_1B5;
			case 3:
				goto IL_217;
			case 4:
				if (arrayList != null)
				{
					num = 3;
					continue;
				}
				return;
			}
			if (!this.ᜂ())
			{
				return;
			}
			num = 0;
		}
		IL_1B5:
		try
		{
			num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_153;
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 4;
						continue;
					}
					string text = (string)enumerator.Current;
					int num2 = text.IndexOf('A');
					num = 3;
					continue;
				}
				case 3:
				{
					int num2;
					if (num2 > -1)
					{
						num = 6;
						continue;
					}
					break;
				}
				case 4:
					num = 0;
					continue;
				case 6:
				{
					string text;
					int num2 = int.Parse(text.Substring(num2 + 1));
					A_0 = this.ᜋ()[num2].ToString();
					this.ᜑ = true;
					this.ᜀ(A_0, this.ᜃ(A_0));
					this.ᜑ = false;
					num = 5;
					continue;
				}
				}
				IL_D9:
				num = 2;
				continue;
				goto IL_D9;
			}
			IL_153:
			return;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						disposable.Dispose();
						num = 1;
						continue;
					case 1:
						goto IL_19E;
					case 2:
						if (disposable != null)
						{
							num = 0;
							continue;
						}
						goto IL_1A0;
					}
					break;
				}
			}
			IL_19E:
			IL_1A0:;
		}
		IL_217:
		goto IL_1A1;
	}

	// Token: 0x0600525B RID: 21083 RVA: 0x00335B48 File Offset: 0x00334B48
	public void ᜇ()
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
	}

	// Token: 0x040024A5 RID: 9381
	private const string ᜀ = "{";

	// Token: 0x040024A6 RID: 9382
	private const char ᜁ = '[';

	// Token: 0x040024A7 RID: 9383
	private const char ᜂ = ']';

	// Token: 0x040024A8 RID: 9384
	private const string ᜃ = "+-*/><=^(,&";

	// Token: 0x040024A9 RID: 9385
	private const string ᜄ = "+-*/><=^),&";

	// Token: 0x040024AA RID: 9386
	private int ᜅ;

	// Token: 0x040024AB RID: 9387
	private Hashtable ᜆ;

	// Token: 0x040024AC RID: 9388
	private spr\u227A ᜇ;

	// Token: 0x040024AD RID: 9389
	private FormulaEngine ᜈ;

	// Token: 0x040024AE RID: 9390
	private Hashtable ᜉ;

	// Token: 0x040024AF RID: 9391
	private Hashtable ᜊ;

	// Token: 0x040024B0 RID: 9392
	private Hashtable ᜋ;

	// Token: 0x040024B1 RID: 9393
	private Hashtable ᜌ;

	// Token: 0x040024B2 RID: 9394
	private bool \u170D;

	// Token: 0x040024B3 RID: 9395
	private string ᜎ;

	// Token: 0x040024B4 RID: 9396
	private bool ᜏ;

	// Token: 0x040024B5 RID: 9397
	private bool ᜐ;

	// Token: 0x040024B6 RID: 9398
	protected bool ᜑ;

	// Token: 0x040024B7 RID: 9399
	private ValueChangedEventHandler \u1712;

	// Token: 0x040024B8 RID: 9400
	private spr\u1775 \u1713;
}
