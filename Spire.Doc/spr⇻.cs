using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Spire.CompoundFile.Doc;

// Token: 0x02000003 RID: 3
[DefaultMember("Item")]
internal class spr\u21FB : Dictionary<string, Dictionary<string, string>>
{
	// Token: 0x06000005 RID: 5 RVA: 0x00004BEC File Offset: 0x00003BEC
	public spr\u21FB()
	{
		int a_ = 12;
		this.ᜅ = new Regex(ClipboardData.b("婱䭳䩵୷όၻ᭽뚇ꊉ뎋뒍뢏궑꺓축욗뚙쎝讟计袣馥膧肩鎫螭즱鲳覵芷銹莻芽꺿ꏁ꧃ꏅ釉鋋돍近ￓￕ\udcdf铡藣諥鷧迩틫뗭껯迱쿳꯵폷폹쟻쇽⧿⠁㬃娅甇", a_), RegexOptions.IgnoreCase | RegexOptions.Compiled);
		base..ctor();
	}

	// Token: 0x06000006 RID: 6 RVA: 0x00004C24 File Offset: 0x00003C24
	public void ᜃ(string A_0)
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
		this.ᜂ(File.ReadAllText(A_0));
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00004C6C File Offset: 0x00003C6C
	public void ᜂ(string A_0)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					try
					{
						num = 8;
						for (;;)
						{
							Dictionary<string, string> dictionary;
							string text3;
							Dictionary<string, string> dictionary2;
							int num2;
							int num3;
							switch (num)
							{
							case 0:
								goto IL_642;
							case 1:
								goto IL_5DE;
							case 3:
								num = 22;
								continue;
							case 4:
							{
								string text;
								if (!string.IsNullOrEmpty(text))
								{
									num = 40;
									continue;
								}
								goto IL_163;
							}
							case 5:
							{
								Match match;
								if (!string.IsNullOrEmpty(match.Groups[ClipboardData.b("ݳ፵ᑷόύ੽", a_)].Value))
								{
									num = 25;
									continue;
								}
								break;
							}
							case 6:
							{
								string text2;
								string[] array = text2.Split(new char[]
								{
									','
								});
								num = 41;
								continue;
							}
							case 7:
								num = 5;
								continue;
							case 9:
							{
								Match match;
								if (match.Groups[ClipboardData.b("ݳ፵ᑷόύ੽", a_)].Captures != null)
								{
									num = 3;
									continue;
								}
								break;
							}
							case 10:
							{
								string text2;
								if (!base.TryGetValue(text2, out dictionary))
								{
									num = 43;
									continue;
								}
								goto IL_419;
							}
							case 11:
								num = 35;
								continue;
							case 12:
								goto IL_2F9;
							case 13:
								base[text3] = dictionary2;
								num = 19;
								continue;
							case 14:
							{
								dictionary2 = new Dictionary<string, string>();
								bool flag = true;
								num = 12;
								continue;
							}
							case 15:
								goto IL_676;
							case 16:
								num = 15;
								continue;
							case 17:
								num = 38;
								continue;
							case 18:
								goto IL_5FB;
							case 19:
								goto IL_14C;
							case 20:
							{
								text3 = spr\u21FB.ᜀ(text3);
								dictionary2 = null;
								bool flag = false;
								num = 42;
								continue;
							}
							case 21:
								num = 9;
								continue;
							case 22:
							{
								Match match;
								if (match.Groups[ClipboardData.b("ݳ፵ᑷόύ੽", a_)].Captures[0] != null)
								{
									num = 7;
									continue;
								}
								break;
							}
							case 23:
								goto IL_419;
							case 24:
							{
								bool flag;
								if (flag)
								{
									num = 13;
									continue;
								}
								goto IL_14C;
							}
							case 25:
							{
								Match match;
								string text2 = match.Groups[ClipboardData.b("ݳ፵ᑷόύ੽", a_)].Captures[0].Value.Trim();
								dictionary = null;
								num = 10;
								continue;
							}
							case 26:
							{
								Match match;
								if (match.Groups != null)
								{
									num = 17;
									continue;
								}
								break;
							}
							case 27:
							{
								string text4;
								if (!string.IsNullOrEmpty(text4))
								{
									num = 11;
									continue;
								}
								goto IL_163;
							}
							case 28:
							{
								string text4;
								if (!string.IsNullOrEmpty(text4))
								{
									num = 36;
									continue;
								}
								goto IL_163;
							}
							case 29:
								goto IL_5FB;
							case 30:
							{
								Match match;
								if (match != null)
								{
									num = 33;
									continue;
								}
								break;
							}
							case 31:
							{
								Match match;
								if (num2 >= match.Groups[ClipboardData.b("ᩳ᝵ᕷό", a_)].Captures.Count)
								{
									num = 6;
									continue;
								}
								string text4 = match.Groups[ClipboardData.b("ᩳ᝵ᕷό", a_)].Captures[num2].Value;
								string text = match.Groups[ClipboardData.b("ɳ᝵ᑷཹ᥻", a_)].Captures[num2].Value;
								num = 28;
								continue;
							}
							case 32:
							{
								if (!enumerator.MoveNext())
								{
									num = 16;
									continue;
								}
								Match match = (Match)enumerator.Current;
								num = 30;
								continue;
							}
							case 33:
								num = 26;
								continue;
							case 34:
							{
								string text;
								string text4;
								dictionary[text4] = text;
								num = 45;
								continue;
							}
							case 35:
							{
								string text;
								if (!string.IsNullOrEmpty(text))
								{
									num = 34;
									continue;
								}
								goto IL_163;
							}
							case 36:
								num = 4;
								continue;
							case 37:
							{
								string text2;
								string key = spr\u21FB.ᜀ(text2);
								base[key] = dictionary;
								num = 44;
								continue;
							}
							case 38:
							{
								Match match;
								if (match.Groups[ClipboardData.b("ݳ፵ᑷόύ੽", a_)] != null)
								{
									num = 21;
									continue;
								}
								break;
							}
							case 39:
								goto IL_642;
							case 40:
							{
								string text4 = spr\u21FB.ᜁ(text4);
								string text = spr\u21FB.ᜁ(text);
								num = 27;
								continue;
							}
							case 41:
							{
								string[] array;
								if (array.Length == 1)
								{
									num = 37;
									continue;
								}
								string[] array2 = array;
								num3 = 0;
								num = 0;
								continue;
							}
							case 42:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_5DE;
								default:
									if (false)
									{
									}
									if (!base.TryGetValue(text3, out dictionary2))
									{
										num = 14;
										continue;
									}
									goto IL_2F9;
								}
								break;
							case 43:
								dictionary = new Dictionary<string, string>();
								num = 23;
								continue;
							case 45:
								goto IL_163;
							case 46:
							{
								string[] array2;
								if (num3 >= array2.Length)
								{
									num = 2;
									continue;
								}
								string text5 = array2[num3];
								text3 = text5.Trim();
								num = 1;
								continue;
							}
							}
							goto IL_12F;
							IL_14C:
							num3++;
							num = 39;
							continue;
							IL_163:
							num2++;
							num = 29;
							continue;
							IL_1DA:
							num = 32;
							continue;
							IL_12F:
							goto IL_1DA;
							IL_2F9:
							spr\u21FB.ᜀ(dictionary, dictionary2);
							num = 24;
							continue;
							IL_419:
							num2 = 0;
							num = 18;
							continue;
							IL_5FB:
							num = 31;
							continue;
							IL_642:
							num = 46;
							continue;
							IL_5DE:
							if (text3.Length == 0)
							{
								goto IL_14C;
							}
							num = 20;
						}
						IL_676:
						return;
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
										num = 1;
										continue;
									}
									goto IL_6C3;
								case 1:
									disposable.Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_6C1;
								}
								break;
							}
						}
						IL_6C1:
						IL_6C3:;
					}
					goto IL_6C4;
				case 2:
					return;
				case 3:
					if ((A_0 = A_0.Trim()).Length == 0)
					{
						num = 2;
						continue;
					}
					goto IL_6C4;
				}
				if (!string.IsNullOrEmpty(A_0))
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
				IL_6C4:
				A_0 = A_0.Replace(ClipboardData.b("䡳坵啷坹", a_), "").Replace(ClipboardData.b("女孵䙷", a_), "").Trim();
				MatchCollection matchCollection = this.ᜅ.Matches(Regex.Replace(A_0, ClipboardData.b("屳䥵䑷孹幻坽\udc7f궁\ud883겅ꚇꆉ뎋튍몏캑뮓뺕ꞗ뮙뺛랝", a_), string.Empty));
				enumerator = matchCollection.GetEnumerator();
				num = 1;
			}
			return;
		}
		}
	}

	// Token: 0x06000008 RID: 8 RVA: 0x00005408 File Offset: 0x00004408
	public static string ᜁ(string A_0)
	{
		if (A_0 == null)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_1B;
				}
			}
			IL_1B:
			if (false)
			{
			}
			if (true)
			{
			}
			return null;
		}
		char[] trimChars = new char[]
		{
			'\r',
			'\n',
			'\f',
			'\t',
			'\v'
		};
		return A_0.Trim(trimChars).Trim();
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00005468 File Offset: 0x00004468
	private static string ᜀ(string A_0)
	{
		string[] array;
		for (;;)
		{
			array = A_0.Split(new char[]
			{
				'.',
				'#'
			});
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					for (;;)
					{
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_8C;
						}
					}
					IL_8C:
					if (false)
					{
					}
					int num2 = A_0.Length - array[0].Length;
					num = 2;
					continue;
				}
				case 1:
					goto IL_C4;
				case 2:
				{
					int num2;
					if (num2 > 0)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					goto IL_51;
				}
				case 3:
					if (array[0].Length > 0)
					{
						num = 0;
						continue;
					}
					return A_0;
				}
				break;
			}
		}
		IL_51:
		return array[0].ToLower();
		IL_C4:
		return array[0].ToLower() + A_0.Substring(array[0].Length);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00005548 File Offset: 0x00004548
	private static void ᜀ(Dictionary<string, string> A_0, Dictionary<string, string> A_1)
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
			using (Dictionary<string, string>.Enumerator enumerator = A_0.GetEnumerator())
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 4;
						continue;
					case 1:
						goto IL_83;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						KeyValuePair<string, string> keyValuePair = enumerator.Current;
						A_1[keyValuePair.Key] = keyValuePair.Value;
						num = 1;
						continue;
					}
					case 4:
						goto IL_A6;
					}
					goto IL_53;
					IL_83:
					num = 3;
					continue;
					IL_53:
					if (true)
					{
					}
					goto IL_83;
				}
				IL_A6:;
			}
			break;
		}
	}

	// Token: 0x0600000B RID: 11 RVA: 0x0000561C File Offset: 0x0000461C
	public string ᜀ(XmlNode A_0)
	{
		int a_ = 10;
		switch (0)
		{
		default:
		{
			int num = 30;
			StringBuilder stringBuilder;
			for (;;)
			{
				Dictionary<string, string> dictionary;
				string text;
				string text2;
				string text3;
				string text4;
				int num2;
				string text6;
				IEnumerator enumerator2;
				switch (num)
				{
				case 0:
				{
					stringBuilder = new StringBuilder();
					Dictionary<string, string>.Enumerator enumerator = dictionary.GetEnumerator();
					num = 22;
					continue;
				}
				case 1:
					spr\u21FB.ᜀ(base[text], dictionary);
					num = 23;
					continue;
				case 2:
					if (base.ContainsKey(text))
					{
						num = 5;
						continue;
					}
					goto IL_3E9;
				case 3:
					if (base.ContainsKey(text2))
					{
						num = 4;
						continue;
					}
					goto IL_19B;
				case 4:
					goto IL_5C3;
				case 5:
					spr\u21FB.ᜀ(base[text], dictionary);
					num = 16;
					continue;
				case 6:
					goto IL_46A;
				case 7:
					text = ClipboardData.b("卯", a_) + text3;
					num = 2;
					continue;
				case 8:
					if (!string.IsNullOrEmpty(text3))
					{
						num = 9;
						continue;
					}
					goto IL_113;
				case 9:
					num = 32;
					continue;
				case 10:
					goto IL_C7;
				case 11:
					goto IL_41A;
				case 12:
					if (text2.Length > 0)
					{
						num = 26;
						continue;
					}
					goto IL_19B;
				case 13:
					goto IL_370;
				case 14:
					if (base.ContainsKey(text4))
					{
						num = 25;
						continue;
					}
					goto IL_370;
				case 15:
					if (base.ContainsKey(text2))
					{
						num = 18;
						continue;
					}
					goto IL_41A;
				case 16:
					goto IL_3E9;
				case 17:
				{
					string[] array;
					if (num2 >= array.Length)
					{
						num = 6;
						continue;
					}
					string text5 = array[num2];
					text2 = text5.Trim();
					num = 12;
					continue;
				}
				case 18:
					spr\u21FB.ᜀ(base[text2], dictionary);
					num = 11;
					continue;
				case 19:
					if (dictionary.Count > 0)
					{
						num = 0;
						continue;
					}
					goto IL_614;
				case 20:
					try
					{
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 6;
								continue;
							case 1:
							{
								XmlAttribute xmlAttribute;
								text6 = xmlAttribute.Value;
								num = 3;
								continue;
							}
							case 4:
							{
								XmlAttribute xmlAttribute;
								if (xmlAttribute.Name.ToLower() == ClipboardData.b("፯ṱᕳյ୷", a_))
								{
									num = 1;
									continue;
								}
								break;
							}
							case 5:
							{
								if (!enumerator2.MoveNext())
								{
									num = 0;
									continue;
								}
								XmlAttribute xmlAttribute = (XmlAttribute)enumerator2.Current;
								num = 4;
								continue;
							}
							case 6:
								goto IL_306;
							}
							IL_2C4:
							num = 5;
							continue;
							goto IL_2C4;
						}
						IL_306:
						goto IL_1BA;
					}
					finally
					{
						for (;;)
						{
							IDisposable disposable = enumerator2 as IDisposable;
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
									goto IL_36F;
								case 1:
									goto IL_33F;
								case 2:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_33F;
									default:
										goto IL_367;
									}
									break;
								}
								break;
								IL_33F:
								disposable.Dispose();
								num = 2;
							}
						}
						IL_367:
						if (false)
						{
						}
						IL_36F:;
					}
					goto IL_370;
					IL_1BA:
					num = 28;
					continue;
				case 21:
					goto IL_EB;
				case 22:
					try
					{
						num = 1;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 4;
								continue;
							case 3:
							{
								Dictionary<string, string>.Enumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								KeyValuePair<string, string> keyValuePair = enumerator.Current;
								stringBuilder.AppendFormat(ClipboardData.b("୯䉱ॳ䱵塷Ź䵻ͽ뭿", a_), keyValuePair.Key, keyValuePair.Value);
								num = 2;
								continue;
							}
							case 4:
								goto IL_5B0;
							}
							IL_58A:
							num = 3;
							continue;
							goto IL_58A;
						}
						IL_5B0:
						goto IL_202;
					}
					finally
					{
						Dictionary<string, string>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					goto IL_5C3;
				case 23:
					goto IL_113;
				case 24:
					goto IL_19B;
				case 25:
					spr\u21FB.ᜀ(base[text4], dictionary);
					num = 13;
					continue;
				case 26:
					text2 = ClipboardData.b("幯", a_) + text2;
					num = 15;
					continue;
				case 27:
					goto IL_EB;
				case 28:
					if (!string.IsNullOrEmpty(text6))
					{
						num = 29;
						continue;
					}
					goto IL_46A;
				case 29:
				{
					string[] array2 = text6.Trim().Split(new char[]
					{
						' '
					});
					string[] array = array2;
					num2 = 0;
					num = 21;
					continue;
				}
				case 31:
					if (base.ContainsKey(text))
					{
						num = 1;
						continue;
					}
					goto IL_113;
				case 32:
					if ((text3 = text3.Trim()).Length > 0)
					{
						num = 7;
						continue;
					}
					goto IL_113;
				}
				if (!(A_0 is XmlElement))
				{
					num = 10;
					continue;
				}
				dictionary = new Dictionary<string, string>();
				XmlElement xmlElement = A_0 as XmlElement;
				text4 = xmlElement.LocalName.ToLower();
				num = 14;
				continue;
				IL_EB:
				num = 17;
				continue;
				IL_113:
				num = 19;
				continue;
				IL_19B:
				if (true)
				{
				}
				num2++;
				num = 27;
				continue;
				IL_370:
				text6 = null;
				enumerator2 = xmlElement.Attributes.GetEnumerator();
				num = 20;
				continue;
				IL_3E9:
				text = text4 + text;
				num = 31;
				continue;
				IL_41A:
				text2 = text4 + text2;
				num = 3;
				continue;
				IL_46A:
				text3 = xmlElement.GetAttribute(ClipboardData.b("᥯ᙱ", a_));
				num = 8;
				continue;
				IL_5C3:
				spr\u21FB.ᜀ(base[text2], dictionary);
				num = 24;
			}
			IL_C7:
			return string.Empty;
			IL_202:
			return stringBuilder.ToString();
			IL_614:
			return string.Empty;
		}
		}
	}

	// Token: 0x04000004 RID: 4
	public const string ᜀ = "(?<selector>(?:(?:[^,{]+),?)*?)\\{(?:(?<name>[^}:]+):?(?<value>[^};]+);?)*?\\}";

	// Token: 0x04000005 RID: 5
	public const string ᜁ = "(?<!\")\\/\\*.+?\\*\\/(?!\")";

	// Token: 0x04000006 RID: 6
	private const string ᜂ = "selector";

	// Token: 0x04000007 RID: 7
	private const string ᜃ = "name";

	// Token: 0x04000008 RID: 8
	private const string ᜄ = "value";

	// Token: 0x04000009 RID: 9
	private Regex ᜅ;
}
