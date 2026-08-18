using System;
using System.Collections;
using System.Reflection;
using System.Text;
using Spire.CompoundFile.Doc;

// Token: 0x02000321 RID: 801
[DefaultMember("Item")]
internal class spr᮸
{
	// Token: 0x06002B61 RID: 11105 RVA: 0x002A7324 File Offset: 0x002A6324
	internal spr᮸()
	{
	}

	// Token: 0x06002B62 RID: 11106 RVA: 0x002A7344 File Offset: 0x002A6344
	internal spr᮸(string A_0)
	{
		this.ᜃ(A_0);
	}

	// Token: 0x06002B63 RID: 11107 RVA: 0x002A736C File Offset: 0x002A636C
	internal void ᜃ(string A_0)
	{
		int num;
		int num2;
		string[] array;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			for (;;)
			{
				IL_2C:
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_6F;
				case 1:
				{
					if (num2 >= array.Length)
					{
						num = 2;
						continue;
					}
					string a_ = array[num2];
					this.ᜁ(a_);
					num2++;
					num = 3;
					continue;
				}
				case 2:
					return;
				case 3:
					goto IL_6F;
				}
				goto IL_4B;
				IL_6F:
				num = 1;
			}
			return;
		default:
			if (false)
			{
			}
			num = 0;
			switch (num)
			{
			}
			break;
		}
		IL_4B:
		string[] array2 = A_0.Split(new char[]
		{
			';'
		});
		array = array2;
		num2 = 0;
		num = 0;
		goto IL_2C;
	}

	// Token: 0x06002B64 RID: 11108 RVA: 0x002A7430 File Offset: 0x002A6430
	internal void ᜀ(spr᮸ A_0)
	{
		for (;;)
		{
			IL_18:
			int num;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4B:
				goto IL_4D;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = A_0.ᜀ();
				num2 = 2;
				break;
			}
			for (;;)
			{
				IL_02:
				switch (num2)
				{
				case 0:
					return;
				case 1:
					if (--num < 0)
					{
						num2 = 0;
						continue;
					}
					this.ᜀ(A_0.ᜀ(num), A_0.ᜁ(num));
					num2 = 3;
					continue;
				case 2:
					goto IL_4B;
				case 3:
					goto IL_90;
				}
				goto IL_18;
			}
			IL_90:
			IL_4D:
			num2 = 1;
			goto IL_02;
		}
	}

	// Token: 0x06002B65 RID: 11109 RVA: 0x002A74D0 File Offset: 0x002A64D0
	internal void ᜀ(string A_0, string A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				this.ᜀ(A_0, spr\u21DA.ᜈ(A_1));
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
					num = 0;
					continue;
				}
				break;
			}
			IL_26:
			if (spr\u1CC6.ᜋ(A_1))
			{
				num = 1;
				continue;
			}
			break;
			goto IL_26;
		}
	}

	// Token: 0x06002B66 RID: 11110 RVA: 0x002A7550 File Offset: 0x002A6550
	internal void ᜁ(string A_0, string A_1)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return;
			case 1:
				this.ᜀ(A_0, spr\u21DA.ᜇ(A_1));
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
					num = 0;
					continue;
				}
				break;
			}
			IL_26:
			if (spr\u1CC6.ᜋ(A_1))
			{
				num = 1;
				continue;
			}
			break;
			goto IL_26;
		}
	}

	// Token: 0x06002B67 RID: 11111 RVA: 0x002A75D0 File Offset: 0x002A65D0
	internal void ᜁ(string A_0, double A_1)
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
		this.ᜀ(A_0, spr\u21DA.ᜁ(A_1));
	}

	// Token: 0x06002B68 RID: 11112 RVA: 0x002A7618 File Offset: 0x002A6618
	internal void ᜀ(string A_0, double A_1)
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
		this.ᜀ(A_0, spr\u21DA.ᜀ(A_1));
	}

	// Token: 0x06002B69 RID: 11113 RVA: 0x002A7660 File Offset: 0x002A6660
	internal string ᜂ(string A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
		{
			if (true)
			{
			}
			if (false)
			{
			}
			spr\u21DA spr_u21DA = this.ᜅ(A_0);
			if (spr_u21DA != null)
			{
				return spr_u21DA.ᜂ();
			}
			break;
		}
		}
		return string.Empty;
	}

	// Token: 0x06002B6A RID: 11114 RVA: 0x002A76B4 File Offset: 0x002A66B4
	internal string ᜁ(bool A_0)
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
			if (this.ᜄ.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				this.ᜀ(stringBuilder, A_0);
				return stringBuilder.ToString();
			}
			if (true)
			{
			}
			break;
		}
		return "";
	}

	// Token: 0x06002B6B RID: 11115 RVA: 0x002A7718 File Offset: 0x002A6718
	private void ᜀ(StringBuilder A_0, bool A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num = 0;
			for (;;)
			{
				if (true)
				{
				}
				bool flag;
				IDictionaryEnumerator enumerator;
				switch (num)
				{
				case 1:
					return;
				case 2:
					try
					{
						num = 1;
						for (;;)
						{
							DictionaryEntry dictionaryEntry;
							switch (num)
							{
							case 0:
								if (!flag)
								{
									num = 5;
									continue;
								}
								flag = false;
								num = 6;
								continue;
							case 2:
								goto IL_14A;
							case 3:
								if (!enumerator.MoveNext())
								{
									num = 4;
									continue;
								}
								dictionaryEntry = (DictionaryEntry)enumerator.Current;
								num = 0;
								continue;
							case 4:
								num = 2;
								continue;
							case 5:
								A_0.Append(ClipboardData.b("嵥䡧", a_));
								num = 8;
								continue;
							case 6:
								goto IL_C0;
							case 8:
								goto IL_C0;
							}
							goto IL_9B;
							IL_C0:
							spr᮸.ᜀ((string)dictionaryEntry.Key, (spr\u21DA)dictionaryEntry.Value, A_0);
							num = 7;
							continue;
							IL_E9:
							num = 3;
							continue;
							IL_9B:
							goto IL_E9;
						}
						IL_14A:
						return;
					}
					finally
					{
						for (;;)
						{
							IL_164:
							IDisposable disposable;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								IL_1A7:
								num = 0;
								break;
							default:
								if (false)
								{
								}
								disposable = (enumerator as IDisposable);
								num = 2;
								break;
							}
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_1B0;
								case 1:
									goto IL_19E;
								case 2:
									if (disposable != null)
									{
										num = 1;
										continue;
									}
									goto IL_1B2;
								}
								goto IL_164;
							}
							IL_19E:
							disposable.Dispose();
							goto IL_1A7;
						}
						IL_1B0:
						IL_1B2:;
					}
					goto IL_1B3;
				}
				if (this.ᜄ.Count == 0)
				{
					num = 1;
					continue;
				}
				IL_1B3:
				SortedList sortedList = this.ᜀ(A_1);
				flag = true;
				enumerator = sortedList.GetEnumerator();
				num = 2;
			}
			return;
		}
		}
	}

	// Token: 0x06002B6C RID: 11116 RVA: 0x002A790C File Offset: 0x002A690C
	internal string ᜀ(int A_0)
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
		return (string)this.ᜄ.GetKey(A_0);
	}

	// Token: 0x06002B6D RID: 11117 RVA: 0x002A7958 File Offset: 0x002A6958
	internal void ᜄ(string A_0)
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
		this.ᜄ.Remove(A_0.ToLower());
	}

	// Token: 0x06002B6E RID: 11118 RVA: 0x002A79A4 File Offset: 0x002A69A4
	private void ᜁ(string A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				for (;;)
				{
					int num = A_0.IndexOf(':');
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
						if (true)
						{
						}
						int num2 = 1;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								return;
							case 1:
							{
								if (num < 0)
								{
									num2 = 0;
									continue;
								}
								int length = num;
								string a_ = spr᮸.ᜀ(A_0.Substring(0, length));
								int num3 = num + 1;
								int length2 = A_0.Length - num3;
								string text = spr᮸.ᜀ(A_0.Substring(num3, length2));
								num2 = 5;
								continue;
							}
							case 2:
							{
								string a_;
								string text;
								this.ᜀ(a_, spr\u21DA.ᜀ(a_, text));
								num2 = 6;
								continue;
							}
							case 3:
								num2 = 4;
								continue;
							case 4:
							{
								string text;
								if (spr\u1CC6.ᜋ(text))
								{
									num2 = 2;
									continue;
								}
								return;
							}
							case 5:
							{
								string a_;
								if (spr\u1CC6.ᜋ(a_))
								{
									num2 = 3;
									continue;
								}
								return;
							}
							case 6:
								return;
							}
							break;
						}
						break;
					}
					}
				}
			}
			return;
		}
	}

	// Token: 0x06002B6F RID: 11119 RVA: 0x002A7AC4 File Offset: 0x002A6AC4
	private static string ᜀ(string A_0)
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
		return A_0.Trim(spr᮸.ᜅ);
	}

	// Token: 0x06002B70 RID: 11120 RVA: 0x002A7B0C File Offset: 0x002A6B0C
	private static void ᜀ(string A_0, spr\u21DA A_1, StringBuilder A_2)
	{
		int a_ = 13;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_2.Append(A_0);
		A_2.Append(ClipboardData.b("䥲", a_));
		A_1.ᜀ(A_2);
	}

	// Token: 0x06002B71 RID: 11121 RVA: 0x002A7B74 File Offset: 0x002A6B74
	private SortedList ᜀ(bool A_0)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			spr\u177C spr_u177C;
			for (;;)
			{
				string[] array = new string[]
				{
					ClipboardData.b("᩶ᡸॺ᩼ᙾ꺂麗", a_),
					ClipboardData.b("᩶ᡸॺ᩼ᙾ꺂歷", a_),
					ClipboardData.b("᩶ᡸॺ᩼ᙾ꺂ﶈﾊ", a_),
					ClipboardData.b("᩶ᡸॺ᩼ᙾ꺂ﾊ", a_)
				};
				spr_u177C = new spr\u177C(true);
				IDictionaryEnumerator enumerator = this.ᜄ.GetEnumerator();
				int num = 15;
				for (;;)
				{
					int num3;
					switch (num)
					{
					case 0:
					{
						int num2;
						if (num2 == array.Length)
						{
							num = 8;
							continue;
						}
						goto IL_1D0;
					}
					case 1:
						goto IL_17D;
					case 2:
						goto IL_3AF;
					case 3:
						return spr_u177C;
					case 4:
						goto IL_1D0;
					case 5:
					{
						sprᨢ sprᨢ;
						spr\u21DA spr_u21DA;
						sprᨢ.ᜀ(num3, spr᮸.ᜀ(spr_u21DA, num3));
						num = 19;
						continue;
					}
					case 6:
						goto IL_117;
					case 7:
						goto IL_1D0;
					case 8:
					{
						sprᨢ sprᨢ;
						spr\u21DA value = spr\u21DA.ᜀ(sprᨢ);
						spr_u177C.Add(ClipboardData.b("᩶ᡸॺ᩼ᙾ", a_), value);
						int num4 = 0;
						num = 1;
						continue;
					}
					case 9:
					{
						spr\u21DA spr_u21DA;
						if (spr_u21DA != null)
						{
							num = 20;
							continue;
						}
						num = 0;
						continue;
					}
					case 10:
					{
						int num4;
						if (num4 >= array.Length)
						{
							num = 4;
							continue;
						}
						spr_u177C.Remove(array[num4]);
						num4++;
						num = 13;
						continue;
					}
					case 11:
					{
						sprᨢ sprᨢ;
						if (sprᨢ.ᜀ(num3) == null)
						{
							num = 5;
							continue;
						}
						spr_u177C.Remove(array[num3]);
						num = 2;
						continue;
					}
					case 12:
					{
						sprᨢ sprᨢ;
						if (num3 >= sprᨢ.Count)
						{
							num = 16;
							continue;
						}
						num = 11;
						continue;
					}
					case 13:
						goto IL_17D;
					case 14:
					{
						spr\u21DA spr_u21DA = spr_u177C[ClipboardData.b("᩶ᡸॺ᩼ᙾ", a_)] as spr\u21DA;
						num = 9;
						continue;
					}
					case 15:
					{
						try
						{
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 1:
									goto IL_2EB;
								case 2:
								{
									if (!enumerator.MoveNext())
									{
										num = 3;
										continue;
									}
									DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
									spr_u177C.Add(dictionaryEntry.Key, dictionaryEntry.Value);
									num = 0;
									continue;
								}
								case 3:
									num = 1;
									continue;
								}
								IL_299:
								num = 2;
								continue;
								goto IL_299;
							}
							IL_2EB:;
						}
						finally
						{
							for (;;)
							{
								IL_302:
								IDisposable disposable;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									IL_346:
									num = 2;
									break;
								default:
									if (false)
									{
									}
									disposable = (enumerator as IDisposable);
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
											num = 1;
											continue;
										}
										goto IL_351;
									case 1:
										goto IL_33D;
									case 2:
										goto IL_34F;
									}
									goto IL_302;
								}
								IL_33D:
								disposable.Dispose();
								goto IL_346;
							}
							IL_34F:
							IL_351:;
						}
						sprᨢ sprᨢ = new sprᨢ();
						int num2 = spr᮸.ᜀ(array, spr_u177C, sprᨢ);
						num = 18;
						continue;
					}
					case 16:
					{
						sprᨢ sprᨢ;
						spr_u177C[ClipboardData.b("᩶ᡸॺ᩼ᙾ", a_)] = spr\u21DA.ᜀ(sprᨢ);
						num = 7;
						continue;
					}
					case 17:
						if (!A_0)
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						goto IL_3F1;
					case 18:
					{
						int num2;
						if (num2 != 0)
						{
							num = 14;
							continue;
						}
						goto IL_1D0;
					}
					case 19:
						goto IL_3AF;
					case 20:
						num3 = 0;
						num = 6;
						continue;
					case 21:
						goto IL_117;
					}
					break;
					IL_117:
					num = 12;
					continue;
					IL_17D:
					num = 10;
					continue;
					IL_1D0:
					num = 17;
					continue;
					IL_3AF:
					num3++;
					num = 21;
				}
			}
			return spr_u177C;
			IL_3F1:
			return new SortedList(spr_u177C, new spr\u245B());
		}
		}
	}

	// Token: 0x06002B72 RID: 11122 RVA: 0x002A7F90 File Offset: 0x002A6F90
	private static int ᜀ(string[] A_0, spr\u177C A_1, sprᨢ A_2)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				IL_0E:
				if (true)
				{
				}
				for (;;)
				{
					num = 0;
					int num2 = 0;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_0E;
					default:
					{
						if (false)
						{
						}
						int num3 = 3;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								goto IL_73;
							case 1:
							{
								if (num2 >= A_0.Length)
								{
									num3 = 4;
									continue;
								}
								string key = A_0[num2];
								spr\u21DA spr_u21DA = A_1[key] as spr\u21DA;
								A_2.Add(spr_u21DA);
								num3 = 5;
								continue;
							}
							case 2:
								num++;
								num3 = 0;
								continue;
							case 3:
								goto IL_CA;
							case 4:
								return num;
							case 5:
							{
								spr\u21DA spr_u21DA;
								if (spr_u21DA != null)
								{
									num3 = 2;
									continue;
								}
								goto IL_73;
							}
							case 6:
								goto IL_CA;
							}
							break;
							IL_73:
							num2++;
							num3 = 6;
							continue;
							IL_CA:
							num3 = 1;
						}
						break;
					}
					}
				}
			}
			return num;
		}
		}
	}

	// Token: 0x06002B73 RID: 11123 RVA: 0x002A808C File Offset: 0x002A708C
	private static spr\u21DA ᜀ(spr\u21DA A_0, int A_1)
	{
		sprᨢ sprᨢ;
		for (;;)
		{
			sprᨢ = A_0.ᜀ();
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (sprᨢ.Count == 2)
					{
						num = 1;
						continue;
					}
					goto IL_53;
				case 1:
					goto IL_150;
				case 2:
					switch (A_1)
					{
					case 0:
						goto IL_AE;
					case 1:
						goto IL_5B;
					case 2:
						if (true)
						{
						}
						num = 0;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_46;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					default:
						num = 5;
						continue;
					}
					break;
				case 3:
					return A_0;
				case 4:
					goto IL_9C;
				case 5:
					num = 7;
					continue;
				case 6:
					if (sprᨢ.Count == 1)
					{
						num = 4;
						continue;
					}
					num = 2;
					continue;
				case 7:
					goto IL_162;
				case 8:
					if (sprᨢ.Count == 3)
					{
						num = 10;
						continue;
					}
					goto IL_A6;
				case 9:
					if (sprᨢ == null)
					{
						goto IL_46;
					}
					num = 6;
					continue;
				case 10:
					goto IL_121;
				}
				break;
				IL_46:
				num = 3;
			}
		}
		return A_0;
		IL_53:
		return sprᨢ.ᜀ(2);
		IL_5B:
		return sprᨢ.ᜀ(1);
		IL_9C:
		return sprᨢ.ᜀ(0);
		IL_A6:
		return sprᨢ.ᜀ(3);
		IL_AE:
		return sprᨢ.ᜀ(0);
		IL_121:
		return sprᨢ.ᜀ(1);
		IL_150:
		return sprᨢ.ᜀ(0);
		IL_162:
		return null;
	}

	// Token: 0x06002B74 RID: 11124 RVA: 0x002A8200 File Offset: 0x002A7200
	internal spr\u21DA ᜅ(string A_0)
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
		return (spr\u21DA)this.ᜄ[A_0.ToLower()];
	}

	// Token: 0x06002B75 RID: 11125 RVA: 0x002A8254 File Offset: 0x002A7254
	internal void ᜀ(string A_0, spr\u21DA A_1)
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
		this.ᜄ[A_0.ToLower()] = A_1;
	}

	// Token: 0x06002B76 RID: 11126 RVA: 0x002A82A4 File Offset: 0x002A72A4
	internal spr\u21DA ᜁ(int A_0)
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
		return (spr\u21DA)this.ᜄ.GetByIndex(A_0);
	}

	// Token: 0x06002B77 RID: 11127 RVA: 0x002A82F0 File Offset: 0x002A72F0
	internal int ᜀ()
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
		return this.ᜄ.Count;
	}

	// Token: 0x06002B78 RID: 11128 RVA: 0x002A8338 File Offset: 0x002A7338
	// Note: this type is marked as 'beforefieldinit'.
	static spr᮸()
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
		spr᮸.ᜅ = new char[]
		{
			' ',
			'\r',
			'\n',
			'\t',
			'\f'
		};
	}

	// Token: 0x04002592 RID: 9618
	private const int ᜀ = 0;

	// Token: 0x04002593 RID: 9619
	private const int ᜁ = 1;

	// Token: 0x04002594 RID: 9620
	private const int ᜂ = 2;

	// Token: 0x04002595 RID: 9621
	private const int ᜃ = 3;

	// Token: 0x04002596 RID: 9622
	private readonly spr\u177C ᜄ = new spr\u177C();

	// Token: 0x04002597 RID: 9623
	private static readonly char[] ᜅ;
}
