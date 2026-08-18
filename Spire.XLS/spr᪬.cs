using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using Spire.Xls.Calculation;
using Spire.Xls.Core.Spreadsheet.Collections;

// Token: 0x020002FD RID: 765
[DefaultMember("Item")]
[Serializable]
internal class spr\u1AAC : ISerializable
{
	// Token: 0x06002F39 RID: 12089 RVA: 0x001A6FD8 File Offset: 0x001A5FD8
	public spr\u1AAC(sprᦶ[] A_0, Dictionary<string, string> A_1)
	{
		this.ᜃ = -1;
		base..ctor();
		this.ᜀ = new CalcSheetList(A_0, this);
		int count = this.ᜀ.Count;
		this.ᜂ = new Hashtable();
		this.ᜀ(count);
		if (count > 0)
		{
			Hashtable hashtable;
			for (;;)
			{
				hashtable = new Hashtable();
				if (A_1 == null)
				{
					break;
				}
				using (Dictionary<string, string>.KeyCollection.Enumerator enumerator = A_1.Keys.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string text = enumerator.Current;
						hashtable.Add(text.ToUpper(CultureInfo.InvariantCulture), A_1[text]);
					}
					break;
				}
			}
			this.ᜁ.ᜀ.ᜀ(hashtable);
		}
	}

	// Token: 0x06002F3A RID: 12090 RVA: 0x001A70A0 File Offset: 0x001A60A0
	protected spr\u1AAC(SerializationInfo A_0, StreamingContext A_1)
	{
		int a_ = 6;
		this.ᜃ = -1;
		base..ctor();
		this.ᜀ = new CalcSheetList((sprᦶ[])A_0.GetValue(RecordTableEnumerator.b("弻弽ⰿ⅁ᝃ⹅ⵇ⽉㡋㵍", a_), typeof(sprᦶ[])), this);
		Hashtable a_2 = (Hashtable)A_0.GetValue(RecordTableEnumerator.b("刻弽ⴿ❁⁃ᑅ⥇⑉⭋⭍⍏", a_), typeof(Hashtable));
		int count = this.ᜀ.Count;
		this.ᜂ = new Hashtable();
		this.ᜀ(count);
		this.ᜁ.ᜀ.ᜀ(a_2);
	}

	// Token: 0x06002F3B RID: 12091 RVA: 0x001A7148 File Offset: 0x001A6148
	public CalcSheetList ᜀ()
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

	// Token: 0x06002F3C RID: 12092 RVA: 0x001A718C File Offset: 0x001A618C
	public void ᜀ(CalcSheetList A_0)
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

	// Token: 0x06002F3D RID: 12093 RVA: 0x001A71D0 File Offset: 0x001A61D0
	public FormulaEngine ᜁ()
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

	// Token: 0x06002F3E RID: 12094 RVA: 0x001A7214 File Offset: 0x001A6214
	public void ᜀ(FormulaEngine A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				this.ᜁ = A_0;
				goto IL_35;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_35;
				default:
					goto IL_53;
				}
				break;
			}
			if (this.ᜁ == null)
			{
				num = 0;
				continue;
			}
			return;
			IL_35:
			num = 1;
		}
		IL_53:
		if (true)
		{
		}
		if (false)
		{
		}
	}

	// Token: 0x06002F3F RID: 12095 RVA: 0x001A7290 File Offset: 0x001A6290
	public int ᜃ()
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
		return this.ᜀ.Count;
	}

	// Token: 0x06002F40 RID: 12096 RVA: 0x001A72D8 File Offset: 0x001A62D8
	public sprᦶ ᜃ(string A_0)
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
		return this.ᜀ[this.ᜁ(A_0)];
	}

	// Token: 0x06002F41 RID: 12097 RVA: 0x001A7328 File Offset: 0x001A6328
	public void ᜀ(string A_0, sprᦶ A_1)
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
		this.ᜀ[this.ᜁ(A_0)].ᜁ = A_1.ᜁ;
	}

	// Token: 0x06002F42 RID: 12098 RVA: 0x001A7380 File Offset: 0x001A6380
	public sprᦶ ᜁ(int A_0)
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
		return this.ᜀ[A_0];
	}

	// Token: 0x06002F43 RID: 12099 RVA: 0x001A73C8 File Offset: 0x001A63C8
	public void ᜀ(int A_0, sprᦶ A_1)
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
		this.ᜀ[A_0].ᜁ = A_1.ᜁ;
	}

	// Token: 0x06002F44 RID: 12100 RVA: 0x001A741C File Offset: 0x001A641C
	public virtual void ᜂ()
	{
		switch (0)
		{
		default:
		{
			IEnumerator enumerator = this.ᜀ.GetEnumerator();
			try
			{
				int num = 3;
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
						sprᦶ sprᦶ = (sprᦶ)enumerator.Current;
						sprᦶ.ᜁ(false);
						num = 4;
						continue;
					}
					case 1:
						goto IL_2F1;
					case 2:
						goto IL_2E8;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E8;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					}
					IL_290:
					num = 0;
					continue;
					goto IL_290;
					IL_2E8:
					num = 1;
				}
				IL_2F1:
				goto IL_24C;
			}
			finally
			{
				if (true)
				{
				}
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_344;
						case 1:
							if (disposable != null)
							{
								num = 2;
								continue;
							}
							goto IL_346;
						case 2:
							disposable.Dispose();
							num = 0;
							continue;
						}
						break;
					}
				}
				IL_344:
				IL_346:;
			}
			return;
			for (;;)
			{
				IL_24C:
				IEnumerator enumerator2 = this.ᜀ.GetEnumerator();
				try
				{
					int num = 8;
					for (;;)
					{
						int num2;
						switch (num)
						{
						case 0:
							num = 3;
							continue;
						case 1:
						{
							sprᦶ sprᦶ2;
							if (num2 > sprᦶ2.ᜁ())
							{
								num = 16;
								continue;
							}
							int num3;
							object obj = sprᦶ2.ᜁ(num3, num2);
							num = 18;
							continue;
						}
						case 2:
							goto IL_80;
						case 3:
							goto IL_1FE;
						case 4:
						{
							sprᦶ sprᦶ2;
							int num3;
							string text;
							sprᦶ2.ᜀ(num3, num2, text);
							num = 9;
							continue;
						}
						case 5:
						{
							sprᦶ sprᦶ2;
							int num3;
							if (num3 > sprᦶ2.ᜀ())
							{
								num = 6;
								continue;
							}
							num2 = 1;
							num = 2;
							continue;
						}
						case 7:
						{
							if (!enumerator2.MoveNext())
							{
								num = 0;
								continue;
							}
							sprᦶ sprᦶ2 = (sprᦶ)enumerator2.Current;
							sprᦶ2.ᜃ().ᜀ.\u1735();
							int num3 = 1;
							num = 11;
							continue;
						}
						case 9:
							goto IL_1DD;
						case 10:
							goto IL_F1;
						case 11:
							goto IL_F1;
						case 12:
							num = 13;
							continue;
						case 13:
						{
							string text;
							if (text[0] == FormulaEngine.FormulaCharacter)
							{
								num = 4;
								continue;
							}
							goto IL_1DD;
						}
						case 14:
						{
							object obj;
							string text = obj.ToString();
							num = 17;
							continue;
						}
						case 15:
							goto IL_80;
						case 16:
						{
							int num3;
							num3++;
							num = 10;
							continue;
						}
						case 17:
						{
							string text;
							if (text.Length > 0)
							{
								num = 12;
								continue;
							}
							goto IL_1DD;
						}
						case 18:
						{
							object obj;
							if (obj != null)
							{
								num = 14;
								continue;
							}
							goto IL_1DD;
						}
						}
						goto IL_7E;
						IL_80:
						num = 1;
						continue;
						IL_A0:
						num = 7;
						continue;
						IL_7E:
						goto IL_A0;
						IL_F1:
						num = 5;
						continue;
						IL_1DD:
						num2++;
						num = 15;
					}
					IL_1FE:
					break;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator2 as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_249;
							case 1:
								disposable2.Dispose();
								num = 0;
								continue;
							case 2:
								if (disposable2 != null)
								{
									num = 1;
									continue;
								}
								goto IL_24B;
							}
							break;
						}
					}
					IL_249:
					IL_24B:;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06002F45 RID: 12101 RVA: 0x001A77A4 File Offset: 0x001A67A4
	public void ᜀ(sprᦶ A_0)
	{
		int a_ = 1;
		switch (0)
		{
		default:
		{
			DateTime now = DateTime.Now;
			string value = string.Format(RecordTableEnumerator.b("ᘶ䈸଺䀼Ḿ", a_), this.ᜁ(A_0.ᜄ()));
			ArrayList arrayList = new ArrayList();
			IEnumerator enumerator = this.ᜁ().ᜀ.\u1732().Keys.GetEnumerator();
			try
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_60B;
					case 1:
					{
						string text;
						arrayList.Add(text);
						num = 6;
						continue;
					}
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 5;
							continue;
						}
						string text = (string)enumerator.Current;
						num = 4;
						continue;
					}
					case 4:
					{
						string text;
						if (text.StartsWith(value))
						{
							num = 1;
							continue;
						}
						break;
					}
					case 5:
						num = 0;
						continue;
					}
					IL_5E2:
					num = 3;
					continue;
					goto IL_5E2;
				}
				IL_60B:
				goto IL_568;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							disposable.Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_656;
						case 2:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_658;
						}
						break;
					}
				}
				IL_656:
				IL_658:;
			}
			return;
			for (;;)
			{
				IL_568:
				IEnumerator enumerator2 = arrayList.GetEnumerator();
				try
				{
					int num = 5;
					for (;;)
					{
						ArrayList arrayList2;
						IEnumerator enumerator6;
						switch (num)
						{
						case 0:
							try
							{
								num = 3;
								for (;;)
								{
									switch (num)
									{
									case 1:
										num = 2;
										continue;
									case 2:
										goto IL_14F;
									case 4:
									{
										IEnumerator enumerator3;
										if (!enumerator3.MoveNext())
										{
											num = 1;
											continue;
										}
										string key = (string)enumerator3.Current;
										this.ᜁ().ᜀ.ᜎ().Remove(key);
										num = 0;
										continue;
									}
									}
									IL_129:
									num = 4;
									continue;
									goto IL_129;
								}
								IL_14F:;
							}
							finally
							{
								for (;;)
								{
									if (true)
									{
									}
									IEnumerator enumerator3;
									IDisposable disposable2 = enumerator3 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_19F;
										case 1:
											disposable2.Dispose();
											num = 0;
											continue;
										case 2:
											if (disposable2 != null)
											{
												num = 1;
												continue;
											}
											goto IL_1A1;
										}
										break;
									}
								}
								IL_19F:
								IL_1A1:;
							}
							break;
						case 1:
						{
							try
							{
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_249;
									case 2:
									{
										IEnumerator enumerator4;
										if (!enumerator4.MoveNext())
										{
											num = 4;
											continue;
										}
										string key2 = (string)enumerator4.Current;
										this.ᜁ().ᜀ.\u171D().Remove(key2);
										num = 3;
										continue;
									}
									case 4:
										num = 0;
										continue;
									}
									IL_1F3:
									num = 2;
									continue;
									goto IL_1F3;
								}
								IL_249:
								goto IL_A0;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator4;
									IDisposable disposable3 = enumerator4 as IDisposable;
									num = 1;
									for (;;)
									{
										switch (num)
										{
										case 0:
											disposable3.Dispose();
											num = 2;
											continue;
										case 1:
											if (disposable3 != null)
											{
												num = 0;
												continue;
											}
											goto IL_296;
										case 2:
											goto IL_294;
										}
										break;
									}
								}
								IL_294:
								IL_296:;
							}
							goto IL_297;
							IL_A0:
							arrayList2.Clear();
							IEnumerator enumerator5 = this.ᜁ().ᜀ.ᜎ().Keys.GetEnumerator();
							num = 6;
							continue;
						}
						case 2:
						{
							try
							{
								num = 5;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										string text2;
										arrayList2.Add(text2);
										num = 4;
										continue;
									}
									case 1:
										num = 6;
										continue;
									case 2:
									{
										if (!enumerator6.MoveNext())
										{
											num = 1;
											continue;
										}
										string text2 = (string)enumerator6.Current;
										num = 3;
										continue;
									}
									case 3:
									{
										string text2;
										if (text2.StartsWith(value))
										{
											num = 0;
											continue;
										}
										break;
									}
									case 6:
										goto IL_3A4;
									}
									IL_363:
									num = 2;
									continue;
									goto IL_363;
								}
								IL_3A4:;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable4 = enumerator6 as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											if (disposable4 != null)
											{
												num = 2;
												continue;
											}
											goto IL_3EE;
										case 1:
											goto IL_3EC;
										case 2:
											disposable4.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_3EC:
								IL_3EE:;
							}
							IEnumerator enumerator4 = arrayList2.GetEnumerator();
							num = 1;
							continue;
						}
						case 3:
							goto IL_51A;
						case 4:
							if (!enumerator2.MoveNext())
							{
								num = 7;
								continue;
							}
							goto IL_297;
						case 6:
							try
							{
								num = 5;
								for (;;)
								{
									switch (num)
									{
									case 0:
										num = 3;
										continue;
									case 1:
									{
										string text3;
										arrayList2.Add(text3);
										num = 4;
										continue;
									}
									case 2:
									{
										IEnumerator enumerator5;
										if (!enumerator5.MoveNext())
										{
											num = 0;
											continue;
										}
										string text3 = (string)enumerator5.Current;
										num = 6;
										continue;
									}
									case 3:
										goto IL_4C0;
									case 6:
									{
										string text3;
										if (text3.StartsWith(value))
										{
											num = 1;
											continue;
										}
										break;
									}
									}
									IL_494:
									num = 2;
									continue;
									goto IL_494;
								}
								IL_4C0:
								goto IL_409;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator5;
									IDisposable disposable5 = enumerator5 as IDisposable;
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_50B;
										case 1:
											disposable5.Dispose();
											num = 0;
											continue;
										case 2:
											if (disposable5 != null)
											{
												num = 1;
												continue;
											}
											goto IL_50D;
										}
										break;
									}
								}
								IL_50B:
								IL_50D:;
							}
							goto IL_50E;
						case 7:
							goto IL_50E;
						}
						IL_1A2:
						num = 4;
						continue;
						goto IL_1A2;
						IL_297:
						string key3 = (string)enumerator2.Current;
						this.ᜁ().ᜀ.\u1732().Remove(key3);
						arrayList2 = new ArrayList();
						enumerator6 = this.ᜁ().ᜀ.\u171D().Keys.GetEnumerator();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
						{
							IL_409:
							IEnumerator enumerator3 = arrayList2.GetEnumerator();
							num = 0;
							continue;
						}
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						IL_50E:
						num = 3;
					}
					IL_51A:
					break;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable6 = enumerator2 as IDisposable;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable6 != null)
								{
									num = 1;
									continue;
								}
								goto IL_567;
							case 1:
								disposable6.Dispose();
								num = 2;
								continue;
							case 2:
								goto IL_565;
							}
							break;
						}
					}
					IL_565:
					IL_567:;
				}
			}
			return;
		}
		}
	}

	// Token: 0x06002F46 RID: 12102 RVA: 0x001A7EA8 File Offset: 0x001A6EA8
	public void ᜀ(SerializationInfo A_0, StreamingContext A_1)
	{
		int a_ = 12;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0.AddValue(RecordTableEnumerator.b("⅁╃⩅⭇᥉⑋⭍㕏♑❓", a_), this.ᜀ.ᜀ());
		A_0.AddValue(RecordTableEnumerator.b("ⱁ╃⭅ⵇ⹉ṋ⽍㹏㕑ㅓ╕", a_), this.ᜁ().ᜀ.ᜩ());
	}

	// Token: 0x06002F47 RID: 12103 RVA: 0x001A7F30 File Offset: 0x001A6F30
	public int ᜁ(string A_0)
	{
		if (true)
		{
		}
		if (this.ᜂ.ContainsKey(A_0.ToLower()))
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return -1;
			}
			if (false)
			{
			}
			return (int)this.ᜂ[A_0.ToLower()];
		}
		return -1;
	}

	// Token: 0x06002F48 RID: 12104 RVA: 0x001A7F98 File Offset: 0x001A6F98
	private void ᜀ(string A_0, out int A_1, out int A_2, out int A_3)
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
		A_0 = A_0.Substring(1);
		int num = A_0.IndexOf('!');
		A_1 = int.Parse(A_0.Substring(0, num));
		A_0 = A_0.Substring(num + 1);
		A_2 = this.ᜁ().ᜀ.\u171C(A_0);
		A_3 = this.ᜁ().ᜀ.វ(A_0);
	}

	// Token: 0x06002F49 RID: 12105 RVA: 0x001A8028 File Offset: 0x001A7028
	private void ᜀ(int A_0)
	{
		for (;;)
		{
			FormulaEngine.ᜀ();
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_49;
				case 1:
				{
					int num2;
					if (num2 >= A_0)
					{
						num = 5;
						continue;
					}
					string text = this.ᜀ[num2].ᜄ();
					this.ᜁ.ᜀ.ᜀ(text, this.ᜀ[num2], this.ᜃ);
					this.ᜂ.Add(text.ToLower(), num2);
					this.ᜀ[num2].ᜃ = this.ᜁ;
					num2++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				case 2:
				{
					if (true)
					{
					}
					this.ᜁ = new FormulaEngine(this.ᜀ[0]);
					this.ᜁ.ᜀ.ᜇ(true);
					this.ᜃ = FormulaEngine.ᜁ();
					int num2 = 0;
					num = 0;
					continue;
				}
				case 3:
					if (A_0 > 0)
					{
						num = 2;
						continue;
					}
					return;
				case 4:
					goto IL_49;
				case 5:
					return;
				}
				break;
				IL_49:
				num = 1;
			}
		}
	}

	// Token: 0x06002F4A RID: 12106 RVA: 0x001A817C File Offset: 0x001A717C
	public static spr\u1AAC ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			spr\u1AAC result = null;
			try
			{
				StreamReader streamReader = new StreamReader(A_0);
				try
				{
					for (;;)
					{
						string text = streamReader.ReadLine();
						int.Parse(text);
						text = streamReader.ReadLine();
						int num = int.Parse(text);
						text = streamReader.ReadLine();
						int num2 = int.Parse(text);
						Dictionary<string, string> dictionary = new Dictionary<string, string>(num2);
						int num3 = 0;
						int num4 = 6;
						for (;;)
						{
							switch (num4)
							{
							case 0:
								goto IL_EE;
							case 1:
							{
								int num5;
								if (num5 >= num)
								{
									num4 = 5;
									continue;
								}
								sprᦶ[] array;
								array[num5] = sprᦶ.ᜀ(streamReader);
								num5++;
								num4 = 7;
								continue;
							}
							case 2:
							{
								if (num3 >= num2)
								{
									num4 = 4;
									continue;
								}
								text = streamReader.ReadLine();
								string[] array2 = text.Split(new char[]
								{
									'\t'
								});
								dictionary.Add(array2[0], array2[1]);
								num3++;
								num4 = 3;
								continue;
							}
							case 3:
								goto IL_126;
							case 4:
							{
								sprᦶ[] array = new sprᦶ[num];
								int num5 = 0;
								num4 = 0;
								continue;
							}
							case 5:
							{
								streamReader.Close();
								sprᦶ[] array;
								result = new spr\u1AAC(array, dictionary);
								num4 = 8;
								continue;
							}
							case 6:
								goto IL_126;
							case 7:
								goto IL_EE;
							case 8:
								goto IL_165;
							}
							break;
							IL_EE:
							num4 = 1;
							continue;
							IL_126:
							num4 = 2;
						}
					}
					IL_165:;
				}
				finally
				{
					int num4 = 0;
					for (;;)
					{
						switch (num4)
						{
						case 1:
							goto IL_193;
						case 2:
							goto IL_1A4;
						}
						if (streamReader != null)
						{
							num4 = 1;
							continue;
						}
						goto IL_1A4;
						IL_193:
						((IDisposable)streamReader).Dispose();
						num4 = 2;
						continue;
						IL_1A4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_193;
						default:
							goto IL_1BA;
						}
					}
					IL_1BA:
					if (false)
					{
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06002F4B RID: 12107 RVA: 0x001A83A4 File Offset: 0x001A73A4
	public void ᜂ(string A_0)
	{
		try
		{
			switch (0)
			{
			default:
			{
				StreamWriter streamWriter = new StreamWriter(A_0);
				try
				{
					for (;;)
					{
						streamWriter.WriteLine(this.ᜄ);
						int count = this.ᜀ.Count;
						streamWriter.WriteLine(count);
						streamWriter.WriteLine(this.ᜁ().ᜀ.ᜩ().Count);
						IEnumerator enumerator = this.ᜁ().ᜀ.ᜩ().Keys.GetEnumerator();
						int num = 1;
						for (;;)
						{
							int num2;
							switch (num)
							{
							case 0:
								num = 2;
								continue;
							case 1:
								try
								{
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 1:
											num = 3;
											continue;
										case 2:
										{
											if (!enumerator.MoveNext())
											{
												num = 1;
												continue;
											}
											string text = (string)enumerator.Current;
											streamWriter.Write(text);
											streamWriter.Write('\t');
											streamWriter.WriteLine(this.ᜁ().ᜀ.ᜩ()[text]);
											num = 4;
											continue;
										}
										case 3:
											goto IL_151;
										}
										IL_E5:
										num = 2;
										continue;
										goto IL_E5;
									}
									IL_151:
									goto IL_1E2;
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
												goto IL_19C;
											case 2:
												if (disposable != null)
												{
													num = 0;
													continue;
												}
												goto IL_19E;
											}
											break;
										}
									}
									IL_19C:
									IL_19E:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_19C;
									default:
										if (false)
										{
										}
										break;
									}
								}
								goto IL_1BB;
								IL_1E2:
								num2 = 0;
								num = 5;
								continue;
							case 2:
								goto IL_201;
							case 3:
								goto IL_99;
							case 4:
								if (num2 >= count)
								{
									num = 0;
									continue;
								}
								goto IL_1BB;
							case 5:
								goto IL_99;
							}
							break;
							IL_99:
							num = 4;
							continue;
							IL_1BB:
							this.ᜀ[num2].ᜀ(streamWriter);
							num2++;
							num = 3;
						}
					}
					IL_201:;
				}
				finally
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							((IDisposable)streamWriter).Dispose();
							num = 2;
							continue;
						case 2:
							goto IL_23E;
						}
						if (streamWriter == null)
						{
							goto IL_248;
						}
						num = 0;
					}
					IL_23E:
					if (true)
					{
					}
					IL_248:;
				}
				break;
			}
			}
		}
		catch (Exception value)
		{
			Console.WriteLine(value);
		}
	}

	// Token: 0x0400152E RID: 5422
	private CalcSheetList ᜀ;

	// Token: 0x0400152F RID: 5423
	private FormulaEngine ᜁ;

	// Token: 0x04001530 RID: 5424
	internal Hashtable ᜂ;

	// Token: 0x04001531 RID: 5425
	internal int ᜃ;

	// Token: 0x04001532 RID: 5426
	private int ᜄ;
}
