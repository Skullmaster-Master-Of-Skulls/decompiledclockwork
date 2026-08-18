using System;
using System.Collections;
using Spire.DataExport.CollectionEditors;
using Spire.DataExport.ResourceMgr;

// Token: 0x0200002B RID: 43
internal class spr᱁ : sprᲤ
{
	// Token: 0x0600015C RID: 348 RVA: 0x0000C374 File Offset: 0x0000B374
	public spr᱁(spr\u219E A_0) : base(A_0)
	{
		this.ᜀ = new spr\u2168(A_0);
		this.ᜁ = new spr\u244C(A_0);
		this.ᜂ = new spr\u1A1D(A_0);
		this.ᜃ = new sprᠪ(A_0);
		this.ᜄ = new sprᠪ(A_0);
		this.ᜅ = new sprᠪ(A_0);
		this.ᜆ = new sprᠪ(A_0);
		this.ᜇ = new sprᥞ(A_0);
	}

	// Token: 0x0600015D RID: 349 RVA: 0x0000C404 File Offset: 0x0000B404
	private void ᜀ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_4D;
			case 2:
				this.ᜌ.ᜀ((ushort)this.ᜅ().ᜀ(0).ᜁ());
				this.ᜌ.ᜂ((ushort)(this.ᜅ().ᜀ(this.ᜅ().ᜌ() - 1).ᜁ() + 1));
				num = 7;
				continue;
			case 3:
				return;
			case 4:
				goto IL_163;
			case 5:
				if (this.ᜅ().ᜌ() > 0)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				this.ᜌ.ᜀ(0);
				this.ᜌ.ᜂ(0);
				num = 6;
				continue;
			case 6:
				goto IL_108;
			case 7:
				goto IL_161;
			case 8:
				goto IL_4D;
			case 9:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_163;
				default:
					if (false)
					{
					}
					if (this.ᜊ().ᜌ() > 0)
					{
						num = 4;
						continue;
					}
					this.ᜌ.ᜁ(0);
					this.ᜌ.ᜀ(0);
					num = 1;
					continue;
				}
				break;
			}
			if (this.ᜌ == null)
			{
				num = 3;
				continue;
			}
			num = 9;
			continue;
			IL_4D:
			num = 5;
			continue;
			IL_163:
			this.ᜌ.ᜁ(this.ᜊ().ᜀ(0).ᜃ());
			this.ᜌ.ᜀ(this.ᜊ().ᜀ(this.ᜊ().ᜌ() - 1).ᜃ() + 1);
			num = 8;
		}
		return;
		IL_108:
		IL_161:
		this.ᜌ.ᜁ(0);
	}

	// Token: 0x0600015E RID: 350 RVA: 0x0000C5E0 File Offset: 0x0000B5E0
	public spr\u1DEE ᜀ(int A_0, int A_1)
	{
		int a_ = 10;
		int num = 8;
		int a_2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_18A;
			case 1:
				goto IL_10A;
			case 2:
				goto IL_12A;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_19B;
				default:
					if (false)
					{
					}
					if (this.ᜀ.ᜀ(A_0).ᜀ(A_1, ref a_2))
					{
						num = 1;
						continue;
					}
					goto IL_1D1;
				}
				break;
			case 4:
				num = 9;
				continue;
			case 5:
				if (A_1 > 255)
				{
					num = 0;
					continue;
				}
				num = 12;
				continue;
			case 6:
				if (A_0 > 65535)
				{
					num = 2;
					continue;
				}
				num = 13;
				continue;
			case 7:
				num = 6;
				continue;
			case 9:
				if (A_1 >= this.ᜎ())
				{
					num = 11;
					continue;
				}
				a_2 = 0;
				num = 3;
				continue;
			case 10:
				num = 5;
				continue;
			case 11:
				goto IL_151;
			case 12:
				if (A_0 < this.ᜋ())
				{
					num = 4;
					continue;
				}
				goto IL_62;
			case 13:
				if (A_1 >= 0)
				{
					goto IL_19B;
				}
				goto IL_1A8;
			}
			if (A_0 >= 0)
			{
				num = 7;
				continue;
			}
			goto IL_8C;
			IL_19B:
			num = 10;
		}
		IL_62:
		return null;
		IL_8C:
		if (true)
		{
		}
		throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("朥娧䴩弫焭礯就䈳圵吷匹堻氽⼿㕁", a_)), A_0));
		IL_10A:
		return this.ᜀ.ᜀ(A_0).ᜀ(a_2);
		IL_12A:
		goto IL_8C;
		IL_151:
		goto IL_62;
		IL_18A:
		IL_1A8:
		throw new Exception(string.Format(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("朥娧䴩弫焭礯就䈳圵吷匹堻紽⼿⹁", a_)), A_1));
		IL_1D1:
		return null;
	}

	// Token: 0x0600015F RID: 351 RVA: 0x0000C7C0 File Offset: 0x0000B7C0
	public new void ᜂ()
	{
		int num = 17;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_166;
			case 1:
				if (this.ᜅ != null)
				{
					num = 7;
					continue;
				}
				goto IL_1CC;
			case 2:
				goto IL_1B4;
			case 3:
				this.ᜂ.ᜊ();
				num = 16;
				continue;
			case 4:
				this.ᜄ.ᜊ();
				num = 6;
				continue;
			case 5:
				if (this.ᜂ != null)
				{
					num = 3;
					continue;
				}
				goto IL_234;
			case 6:
				goto IL_12A;
			case 7:
				this.ᜅ.ᜊ();
				num = 19;
				continue;
			case 8:
				goto IL_EE;
			case 9:
				goto IL_186;
			case 10:
				if (this.ᜃ != null)
				{
					num = 20;
					continue;
				}
				goto IL_257;
			case 11:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1B4;
				default:
					if (false)
					{
					}
					if (this.ᜁ != null)
					{
						num = 22;
						continue;
					}
					goto IL_186;
				}
				break;
			case 12:
				goto IL_257;
			case 13:
				if (true)
				{
				}
				this.ᜀ.ᜊ();
				num = 8;
				continue;
			case 14:
				goto IL_1A9;
			case 15:
				this.ᜆ.ᜊ();
				num = 14;
				continue;
			case 16:
				goto IL_234;
			case 18:
				this.ᜇ.ᜊ();
				num = 0;
				continue;
			case 19:
				goto IL_1CC;
			case 20:
				this.ᜃ.ᜊ();
				num = 12;
				continue;
			case 21:
				if (this.ᜄ != null)
				{
					num = 4;
					continue;
				}
				goto IL_12A;
			case 22:
				this.ᜁ.ᜊ();
				num = 9;
				continue;
			case 23:
				if (this.ᜆ != null)
				{
					num = 15;
					continue;
				}
				goto IL_1A9;
			}
			if (this.ᜀ != null)
			{
				num = 13;
				continue;
			}
			IL_EE:
			num = 11;
			continue;
			IL_12A:
			num = 1;
			continue;
			IL_186:
			num = 5;
			continue;
			IL_1A9:
			num = 2;
			continue;
			IL_1B4:
			if (this.ᜇ != null)
			{
				num = 18;
				continue;
			}
			break;
			IL_1CC:
			num = 23;
			continue;
			IL_234:
			num = 10;
			continue;
			IL_257:
			num = 21;
		}
		IL_166:
		this.ᜌ = null;
		base.ᜂ();
	}

	// Token: 0x06000160 RID: 352 RVA: 0x0000CA58 File Offset: 0x0000BA58
	public void ᜀ(spr\u1DEE A_0)
	{
		for (;;)
		{
			for (;;)
			{
				int num = this.ᜀ.ᜌ();
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
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_10C;
						case 1:
							goto IL_10A;
						case 2:
							goto IL_E8;
						case 3:
						{
							this.ᜀ.ᜀ((int)A_0.\u171F()).ᜂ(A_0);
							int num3 = this.ᜁ.ᜌ();
							num2 = 6;
							continue;
						}
						case 4:
						{
							int num3;
							if (num3 > (int)A_0.\u171E())
							{
								num2 = 1;
								continue;
							}
							this.ᜁ.ᜀ(new spr\u2049(base.\u1712()));
							num3++;
							num2 = 2;
							continue;
						}
						case 5:
							if (num > (int)A_0.\u171F())
							{
								num2 = 3;
								continue;
							}
							this.ᜀ.ᜀ(new spr᥋(base.\u1712()));
							num++;
							num2 = 7;
							continue;
						case 6:
							goto IL_E8;
						case 7:
							goto IL_10C;
						}
						break;
						IL_E8:
						num2 = 4;
						continue;
						IL_10C:
						if (true)
						{
						}
						num2 = 5;
					}
					break;
				}
				}
			}
		}
		IL_10A:
		this.ᜁ.ᜀ((int)A_0.\u171E()).ᜂ(A_0);
	}

	// Token: 0x06000161 RID: 353 RVA: 0x0000CBB8 File Offset: 0x0000BBB8
	public void ᜀ(sprᡙ A_0)
	{
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_82;
				case 2:
				{
					if (A_0.ᜁ())
					{
						num = 0;
						continue;
					}
					spr\u1DEE a_ = A_0.ᜀ();
					this.ᜀ(a_);
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_82;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				case 3:
					goto IL_24;
				}
				break;
				IL_24:
				num = 2;
				continue;
				IL_82:
				goto IL_24;
			}
		}
	}

	// Token: 0x06000162 RID: 354 RVA: 0x0000CC4C File Offset: 0x0000BC4C
	public override void ᜀ(sprḗ A_0, spr\u1F46 A_1)
	{
		int a_ = 9;
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u1DCF a_2;
				a_2.ᜀ = 0;
				a_2.ᜁ = 0;
				byte[] array = new byte[spr\u1DCF.ᜀ()];
				this.ᜂ();
				sprᠪ sprᠪ = this.ᜃ;
				spr\u21B9 spr_u21B = new spr\u21B9(null);
				spr᠗ spr᠗ = null;
				goto IL_55;
				try
				{
					for (;;)
					{
						IL_55:
						int num = 2;
						for (;;)
						{
							spr\u2320 spr_u;
							switch (num)
							{
							case 0:
								try
								{
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 1:
											this.ᜉ = (int)(spr_u as sprℝ).ᜀ();
											num = 33;
											continue;
										case 2:
											if (spr_u is sprẴ)
											{
												num = 30;
												continue;
											}
											num = 24;
											continue;
										case 3:
											goto IL_30E;
										case 4:
											goto IL_520;
										case 5:
											goto IL_520;
										case 6:
											goto IL_52C;
										case 7:
											goto IL_4B6;
										case 8:
											if (spr_u is spr\u1F46)
											{
												num = 3;
												continue;
											}
											num = 13;
											continue;
										case 9:
											spr᠗ = (spr_u as spr᠗);
											num = 16;
											continue;
										case 10:
											if (spr᠗ == null)
											{
												num = 18;
												continue;
											}
											spr᠗.ᜂ((spr_u as sprᮕ).ᜀ());
											spr_u.Close();
											spr_u = null;
											num = 4;
											continue;
										case 11:
											if (spr_u is spr᠗)
											{
												num = 9;
												continue;
											}
											goto IL_2C4;
										case 12:
											goto IL_520;
										case 13:
											if (spr_u is sprᬱ)
											{
												num = 32;
												continue;
											}
											num = 35;
											continue;
										case 14:
											base.ᜀ(spr_u as spr\u1809);
											num = 22;
											continue;
										case 15:
											this.ᜈ = (int)(spr_u as spr\u21B3).ᜀ();
											num = 29;
											continue;
										case 16:
											goto IL_2C4;
										case 17:
											if (spr_u is sprℝ)
											{
												num = 1;
												continue;
											}
											goto IL_2EA;
										case 18:
											goto IL_35D;
										case 19:
											if (spr_u is spr\u21B3)
											{
												num = 15;
												continue;
											}
											goto IL_1F0;
										case 20:
											goto IL_520;
										case 21:
											if (spr_u is sprᡙ)
											{
												num = 23;
												continue;
											}
											num = 2;
											continue;
										case 22:
											goto IL_520;
										case 23:
											this.ᜀ(spr_u as sprᡙ);
											spr_u.Close();
											spr_u = null;
											num = 20;
											continue;
										case 24:
											if (spr_u is sprᮕ)
											{
												num = 25;
												continue;
											}
											num = 31;
											continue;
										case 25:
											num = 10;
											continue;
										case 26:
											goto IL_520;
										case 27:
											this.ᜀ(spr_u as spr\u1DEE);
											num = 26;
											continue;
										case 28:
											goto IL_520;
										case 29:
											goto IL_1F0;
										case 30:
											spr_u21B.ᜀ(spr_u as sprẴ);
											num = 5;
											continue;
										case 31:
											if (spr_u is spr\u1809)
											{
												num = 14;
												continue;
											}
											sprᠪ.ᜀ(spr_u);
											num = 28;
											continue;
										case 32:
											this.ᜂ.ᜀ(spr_u as sprᬱ);
											num = 12;
											continue;
										case 33:
											goto IL_2EA;
										case 34:
											sprᠪ = this.ᜄ;
											this.ᜋ = (spr_u as spr\u1DE6);
											num = 7;
											continue;
										case 35:
											if (spr_u is spr\u1DEE)
											{
												num = 27;
												continue;
											}
											num = 21;
											continue;
										}
										if (a_2.ᜀ == 574)
										{
											num = 34;
											continue;
										}
										goto IL_4B6;
										IL_1F0:
										num = 17;
										continue;
										IL_2C4:
										num = 19;
										continue;
										IL_2EA:
										num = 8;
										continue;
										IL_4B6:
										num = 11;
										continue;
										IL_520:
										num = 6;
									}
									IL_30E:
									throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("氤䤦弨䨪䄬䘮唰簲䔴制䬸娺䤼嘾⹀ⵂᩄɆㅈ⡊⡌⍎͐㙒㙔㡖⭘㽚", a_)));
									IL_35D:
									throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("氤䤦弨䨪䄬䘮唰簲䔴制䬸娺䤼嘾⹀ⵂᩄɆㅈ⡊⡌⍎͐㙒㙔㡖⭘㽚", a_)));
									IL_52C:
									goto IL_BE;
								}
								catch
								{
									if (spr_u != null)
									{
										spr_u.Close();
									}
									throw;
								}
								goto IL_543;
								IL_BE:
								num = 1;
								continue;
							case 1:
								if (a_2.ᜀ == 10)
								{
									num = 4;
									continue;
								}
								goto IL_55;
							case 3:
								goto IL_9F;
							case 4:
								goto IL_543;
							case 5:
								goto IL_54F;
							}
							if (A_0.ᜀ(array, array.Length) != array.Length)
							{
								if (true)
								{
								}
								num = 3;
								continue;
							}
							spr\u1DCF.ᜀ(array, ref a_2);
							spr_u = sprᮌ.ᜀ(this, A_0, a_2);
							num = 0;
							continue;
							IL_543:
							num = 5;
						}
					}
					IL_9F:
					throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("氤䤦弨䨪䄬䘮唰簲䔴制䬸娺䤼嘾⹀ⵂᩄɆㅈ⡊⡌⍎͐㙒㙔㡖⭘㽚", a_)));
					IL_54F:;
				}
				finally
				{
					spr_u21B = null;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_56A;
				}
			}
			IL_56A:
			if (false)
			{
			}
			base.ᜀ(A_1);
			return;
		}
	}

	// Token: 0x06000163 RID: 355 RVA: 0x0000D204 File Offset: 0x0000C204
	public override void ᜀ(sprḗ A_0)
	{
		int a_ = 12;
		int num = 4;
		for (;;)
		{
			IEnumerator enumerator;
			switch (num)
			{
			case 0:
				goto IL_18E;
			case 1:
				if (true)
				{
				}
				try
				{
					num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							spr᥋ spr᥋ = (spr᥋)enumerator.Current;
							spr᥋.ᜀ(A_0);
							num = 3;
							continue;
						}
						case 2:
							num = 4;
							continue;
						case 4:
							goto IL_AA;
						}
						IL_6B:
						num = 0;
						continue;
						goto IL_6B;
					}
					IL_AA:
					goto IL_1AE;
				}
				finally
				{
					for (;;)
					{
						for (;;)
						{
							IDisposable disposable = enumerator as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable != null)
									{
										num = 1;
										continue;
									}
									goto IL_10B;
								case 1:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										break;
									default:
										if (false)
										{
										}
										disposable.Dispose();
										num = 2;
										continue;
									}
									break;
								case 2:
									goto IL_109;
								}
								break;
							}
						}
					}
					IL_109:
					IL_10B:;
				}
				goto IL_10C;
			case 2:
				if (base.\u1715() == null)
				{
					num = 0;
					continue;
				}
				goto IL_10C;
			case 3:
				num = 2;
				continue;
			}
			if (base.\u1714() != null)
			{
				num = 3;
				continue;
			}
			break;
			IL_10C:
			base.\u1714().ᜀ(A_0);
			this.ᜃ.ᜀ(A_0);
			this.ᜂ.ᜀ(A_0);
			this.ᜀ();
			this.ᜌ.ᜀ(A_0);
			enumerator = this.ᜊ().ᜇ();
			num = 1;
		}
		IL_18E:
		throw new Exception(ResManager.GetResourceManager().GetString(HyperlinksCollectionEditor.b("椧堩䬫崭漯愱儳唵䰷匹医倽฿ⵁぃ੅❇⭉⡋⭍㑏", a_)));
		IL_1AE:
		this.ᜄ.ᜀ(A_0);
		this.ᜅ.ᜀ(A_0);
		this.ᜆ.ᜀ(A_0);
		base.\u1715().ᜀ(A_0);
	}

	// Token: 0x06000164 RID: 356 RVA: 0x0000D400 File Offset: 0x0000C400
	public override int ᜁ()
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
		return base.ᜁ() + this.ᜃ.ᜆ() + this.ᜂ.ᜁ() + sizeof(spr\u2069) + sizeof(spr\u1DCF) + this.ᜀ.ᜁ() + this.ᜄ.ᜆ() + this.ᜅ.ᜆ() + this.ᜆ.ᜆ();
	}

	// Token: 0x06000165 RID: 357 RVA: 0x0000D498 File Offset: 0x0000C498
	public string \u170D()
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
		return base.\u1712().ᜂ().ᜀ().ᜀ(this.ᜊ).ᜄ();
	}

	// Token: 0x06000166 RID: 358 RVA: 0x0000D4F4 File Offset: 0x0000C4F4
	public spr\u1DE6 ᜄ()
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
		return this.ᜋ;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x0000D538 File Offset: 0x0000C538
	public void ᜀ(spr\u1DE6 A_0)
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
		this.ᜋ = A_0;
	}

	// Token: 0x06000168 RID: 360 RVA: 0x0000D57C File Offset: 0x0000C57C
	public spr\u2168 ᜊ()
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

	// Token: 0x06000169 RID: 361 RVA: 0x0000D5C0 File Offset: 0x0000C5C0
	public int ᜋ()
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
		return this.ᜀ.ᜌ();
	}

	// Token: 0x0600016A RID: 362 RVA: 0x0000D608 File Offset: 0x0000C608
	public spr\u244C ᜅ()
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
		return this.ᜁ;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x0000D64C File Offset: 0x0000C64C
	public int ᜎ()
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
		return this.ᜁ.ᜌ();
	}

	// Token: 0x0600016C RID: 364 RVA: 0x0000D694 File Offset: 0x0000C694
	public spr\u1A1D ᜈ()
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
		return this.ᜂ;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x0000D6D8 File Offset: 0x0000C6D8
	public sprᠪ ᜉ()
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
		return this.ᜃ;
	}

	// Token: 0x0600016E RID: 366 RVA: 0x0000D71C File Offset: 0x0000C71C
	public sprᠪ ᜑ()
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
		return this.ᜄ;
	}

	// Token: 0x0600016F RID: 367 RVA: 0x0000D760 File Offset: 0x0000C760
	public sprᠪ ᜌ()
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
		return this.ᜅ;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x0000D7A4 File Offset: 0x0000C7A4
	public sprᠪ ᜆ()
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
		return this.ᜆ;
	}

	// Token: 0x06000171 RID: 369 RVA: 0x0000D7E8 File Offset: 0x0000C7E8
	public sprᥞ ᜏ()
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
		return this.ᜇ;
	}

	// Token: 0x06000172 RID: 370 RVA: 0x0000D82C File Offset: 0x0000C82C
	public sprἶ ᜇ()
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

	// Token: 0x06000173 RID: 371 RVA: 0x0000D870 File Offset: 0x0000C870
	public void ᜀ(sprἶ A_0)
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
		this.ᜌ = A_0;
	}

	// Token: 0x06000174 RID: 372 RVA: 0x0000D8B4 File Offset: 0x0000C8B4
	public int ᜃ()
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
		return this.ᜈ;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x0000D8F8 File Offset: 0x0000C8F8
	public int ᜐ()
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

	// Token: 0x04000075 RID: 117
	private new spr\u2168 ᜀ;

	// Token: 0x04000076 RID: 118
	private spr\u244C ᜁ;

	// Token: 0x04000077 RID: 119
	private new spr\u1A1D ᜂ;

	// Token: 0x04000078 RID: 120
	private sprᠪ ᜃ;

	// Token: 0x04000079 RID: 121
	private sprᠪ ᜄ;

	// Token: 0x0400007A RID: 122
	private sprᠪ ᜅ;

	// Token: 0x0400007B RID: 123
	private sprᠪ ᜆ;

	// Token: 0x0400007C RID: 124
	private sprᥞ ᜇ;

	// Token: 0x0400007D RID: 125
	private int ᜈ = 8;

	// Token: 0x0400007E RID: 126
	private int ᜉ = 255;

	// Token: 0x0400007F RID: 127
	private int ᜊ = -1;

	// Token: 0x04000080 RID: 128
	private spr\u1DE6 ᜋ;

	// Token: 0x04000081 RID: 129
	private sprἶ ᜌ;
}
