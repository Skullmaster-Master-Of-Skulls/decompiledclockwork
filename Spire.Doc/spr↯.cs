using System;
using System.Collections;
using System.IO;
using System.Text;
using Spire.CompoundFile.Doc;
using Spire.Doc.Fields.Shape;

// Token: 0x020002CA RID: 714
internal class spr\u21AF
{
	// Token: 0x060026A9 RID: 9897 RVA: 0x00262A34 File Offset: 0x00261A34
	internal spr\u21AF(spr\u1B02 A_0)
	{
		this.ᜀ(A_0);
		this.ᜀ.FinalUpdate();
	}

	// Token: 0x060026AA RID: 9898 RVA: 0x00262A64 File Offset: 0x00261A64
	private void ᜀ(spr\u1B02 A_0)
	{
		int a_ = 11;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_2C:
				this.ᜀ(A_0.ᜀ().ToByteArray());
				SortedList sortedList = new SortedList(new spr\u21AF.ᜀ());
				SortedList sortedList2 = new SortedList(new spr\u21AF.ᜀ());
				IDictionaryEnumerator enumerator = A_0.GetEnumerator();
				for (;;)
				{
					IL_5F:
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_3F3;
						case 1:
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_5F;
							default:
								if (false)
								{
								}
								try
								{
									num = 3;
									for (;;)
									{
										switch (num)
										{
										case 1:
										{
											string text;
											if (text != ClipboardData.b("⹰rᱴၶ᝸᩺ॼ੾", a_))
											{
												num = 5;
												continue;
											}
											break;
										}
										case 2:
										{
											string text;
											if (text != ClipboardData.b("硰㝲❴㩶㩸ᑺ፼୾", a_))
											{
												num = 16;
												continue;
											}
											break;
										}
										case 4:
										{
											DictionaryEntry dictionaryEntry;
											if (dictionaryEntry.Value is Stream)
											{
												num = 12;
												continue;
											}
											num = 15;
											continue;
										}
										case 5:
											num = 2;
											continue;
										case 6:
										{
											string text;
											object value;
											sortedList2.Add(text, value);
											num = 0;
											continue;
										}
										case 7:
										{
											string text;
											if (text != ClipboardData.b("瑰ㅲᑴၶᡸ᩺౼پ뎀낂戀튒겘꾜ﲞ즠솤", a_))
											{
												num = 6;
												continue;
											}
											break;
										}
										case 8:
											num = 14;
											continue;
										case 9:
											goto IL_2CC;
										case 10:
											num = 7;
											continue;
										case 12:
											num = 1;
											continue;
										case 13:
										{
											if (!enumerator.MoveNext())
											{
												num = 17;
												continue;
											}
											DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
											string text = dictionaryEntry.Key.ToString();
											object value = dictionaryEntry.Value;
											num = 4;
											continue;
										}
										case 14:
										{
											string text;
											if (text != ClipboardData.b("睰㝲ᑴͶᡸ⡺ർṾ", a_))
											{
												num = 10;
												continue;
											}
											break;
										}
										case 15:
										{
											DictionaryEntry dictionaryEntry;
											if (dictionaryEntry.Value is spr\u1B02)
											{
												num = 8;
												continue;
											}
											goto IL_13D;
										}
										case 16:
										{
											string text;
											object value;
											sortedList.Add(text, value);
											num = 11;
											continue;
										}
										case 17:
											num = 9;
											continue;
										}
										IL_151:
										num = 13;
										continue;
										goto IL_151;
									}
									IL_13D:
									throw new InvalidOperationException(ClipboardData.b("⑰ᵲṴ᥶ᙸ౺፼彾ﾊ권ﾐ뎒膠킢톤좦\udba8쪪쪬쪮龰", a_));
									IL_2CC:
									goto IL_4E3;
								}
								finally
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
													num = 2;
													continue;
												}
												goto IL_319;
											case 1:
												goto IL_317;
											case 2:
												disposable.Dispose();
												num = 1;
												continue;
											}
											break;
										}
									}
									IL_317:
									IL_319:;
								}
								goto Block_2;
							}
							break;
						case 2:
							goto IL_31A;
						}
						goto IL_2C;
						Block_3:
						IDictionaryEnumerator enumerator2;
						try
						{
							IL_3F3:
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
								{
									if (!enumerator2.MoveNext())
									{
										num = 3;
										continue;
									}
									DictionaryEntry dictionaryEntry2 = (DictionaryEntry)enumerator2.Current;
									this.ᜁ((string)dictionaryEntry2.Key);
									MemoryStream memoryStream = (MemoryStream)dictionaryEntry2.Value;
									this.ᜀ(memoryStream.GetBuffer(), (int)memoryStream.Length);
									num = 1;
									continue;
								}
								case 3:
									num = 4;
									continue;
								case 4:
									goto IL_495;
								}
								IL_41B:
								num = 0;
								continue;
								goto IL_41B;
							}
							IL_495:
							goto IL_98;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable2 = enumerator2 as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										disposable2.Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_4E0;
									case 2:
										if (disposable2 != null)
										{
											num = 0;
											continue;
										}
										goto IL_4E2;
									}
									break;
								}
							}
							IL_4E0:
							IL_4E2:;
						}
						goto IL_4E3;
						IL_98:
						IDictionaryEnumerator enumerator3 = sortedList2.GetEnumerator();
						num = 2;
						continue;
						Block_2:
						try
						{
							IL_31A:
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_3A5;
								case 3:
								{
									if (!enumerator3.MoveNext())
									{
										num = 4;
										continue;
									}
									DictionaryEntry dictionaryEntry3 = (DictionaryEntry)enumerator3.Current;
									this.ᜁ((string)dictionaryEntry3.Key);
									this.ᜀ((spr\u1B02)dictionaryEntry3.Value);
									num = 2;
									continue;
								}
								case 4:
									num = 1;
									continue;
								}
								IL_37F:
								num = 3;
								continue;
								goto IL_37F;
							}
							IL_3A5:
							return;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable3 = enumerator3 as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_3F0;
									case 1:
										disposable3.Dispose();
										num = 0;
										continue;
									case 2:
										if (disposable3 != null)
										{
											num = 1;
											continue;
										}
										goto IL_3F2;
									}
									break;
								}
							}
							IL_3F0:
							IL_3F2:;
						}
						goto Block_3;
						IL_4E3:
						enumerator2 = sortedList.GetEnumerator();
						num = 0;
					}
				}
			}
			return;
		}
	}

	// Token: 0x060026AB RID: 9899 RVA: 0x00262FBC File Offset: 0x00261FBC
	private void ᜀ(byte[] A_0)
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
		this.ᜀ(A_0, A_0.Length);
	}

	// Token: 0x060026AC RID: 9900 RVA: 0x00263004 File Offset: 0x00262004
	private void ᜀ(byte[] A_0, int A_1)
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
		this.ᜀ.Update(A_0, A_1);
	}

	// Token: 0x060026AD RID: 9901 RVA: 0x0026304C File Offset: 0x0026204C
	private void ᜁ(string A_0)
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
		this.ᜀ(Encoding.Unicode.GetBytes(A_0));
	}

	// Token: 0x060026AE RID: 9902 RVA: 0x00263098 File Offset: 0x00262098
	private static string ᜀ(string A_0)
	{
		StringBuilder stringBuilder;
		for (;;)
		{
			stringBuilder = new StringBuilder();
			int num = 0;
			int num2 = 4;
			for (;;)
			{
				char c;
				switch (num2)
				{
				case 0:
					if (num >= A_0.Length)
					{
						num2 = 5;
						continue;
					}
					c = A_0[num];
					num2 = 8;
					continue;
				case 1:
					goto IL_A6;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						num2 = 6;
						continue;
					}
					break;
				case 3:
					goto IL_3E;
				case 4:
					goto IL_A6;
				case 5:
					goto IL_CD;
				case 6:
					if (c == '_')
					{
						num2 = 7;
						continue;
					}
					goto IL_3E;
				case 7:
					goto IL_4C;
				case 8:
					if (!char.IsLetterOrDigit(c))
					{
						num2 = 2;
						continue;
					}
					goto IL_4C;
				}
				break;
				IL_3E:
				num++;
				num2 = 1;
				continue;
				IL_4C:
				stringBuilder.Append(c);
				num2 = 3;
				continue;
				IL_A6:
				if (true)
				{
				}
				num2 = 0;
			}
		}
		IL_CD:
		return stringBuilder.ToString();
	}

	// Token: 0x060026AF RID: 9903 RVA: 0x002631A0 File Offset: 0x002621A0
	internal byte[] ᜀ()
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
		return this.ᜀ.Digest;
	}

	// Token: 0x04002280 RID: 8832
	private readonly MD5 ᜀ = new MD5();

	// Token: 0x020002CB RID: 715
	private class ᜀ : IComparer
	{
		// Token: 0x060026B0 RID: 9904 RVA: 0x002631E8 File Offset: 0x002621E8
		int IComparer.ᜀ(object A_0, object A_1)
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
			string strA = spr\u21AF.ᜀ((string)A_0);
			string strB = spr\u21AF.ᜀ((string)A_1);
			return string.CompareOrdinal(strA, strB);
		}
	}
}
