using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

// Token: 0x0200024F RID: 591
internal class spr\u25C5
{
	// Token: 0x06001DC0 RID: 7616 RVA: 0x001D6660 File Offset: 0x001D5660
	public static spr\u25C5 ᜀ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_65:
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
				if (true)
				{
				}
				break;
			case 1:
				goto IL_6D;
			case 2:
				goto IL_59;
			}
			if (spr\u25C5.ᜁ != null)
			{
				goto IL_6F;
			}
			num = 2;
		}
		IL_59:
		spr\u25C5.ᜁ = new spr\u25C5();
		goto IL_65;
		IL_6D:
		IL_6F:
		return spr\u25C5.ᜁ;
	}

	// Token: 0x06001DC1 RID: 7617 RVA: 0x001D66E4 File Offset: 0x001D56E4
	internal List<Paragraph> ᜁ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_67:
			num = 0;
			break;
		default:
			if (false)
			{
			}
			num = 2;
			break;
		}
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				goto IL_5A;
			case 2:
				if (true)
				{
				}
				break;
			}
			if (this.ᜀ != null)
			{
				goto IL_71;
			}
			num = 1;
		}
		IL_5A:
		this.ᜀ = new List<Paragraph>();
		goto IL_67;
		IL_6F:
		IL_71:
		return this.ᜀ;
	}

	// Token: 0x06001DC2 RID: 7618 RVA: 0x001D6768 File Offset: 0x001D5768
	public spr\u226E ᜀ(Paragraph A_0, Regex A_1, bool A_2)
	{
		spr\u226E spr_u226E;
		for (;;)
		{
			IL_00:
			switch (0)
			{
			default:
				for (;;)
				{
					string text = A_0.Text;
					MatchCollection matchCollection = A_1.Matches(text);
					spr_u226E = new spr\u226E();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
					{
						if (false)
						{
						}
						int num = 2;
						for (;;)
						{
							IEnumerator enumerator;
							switch (num)
							{
							case 0:
								try
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 1:
											goto IL_127;
										case 3:
										{
											if (!enumerator.MoveNext())
											{
												num = 1;
												continue;
											}
											Match match = (Match)enumerator.Current;
											int index = match.Index;
											int endCharPos = match.Index + match.Length;
											spr_u226E.Add(new TextSelection(A_0, index, endCharPos)
											{
												ᜉ = spr_u226E
											});
											num = 4;
											continue;
										}
										case 4:
											if (!A_2)
											{
												num = 0;
												continue;
											}
											goto IL_127;
										case 5:
											goto IL_133;
										}
										IL_AD:
										num = 3;
										continue;
										goto IL_AD;
										IL_127:
										num = 5;
									}
									IL_133:
									goto IL_19F;
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
												goto IL_17D;
											case 1:
												disposable.Dispose();
												num = 2;
												continue;
											case 2:
												goto IL_17B;
											}
											break;
										}
									}
									IL_17B:
									IL_17D:;
								}
								goto IL_17E;
							case 1:
								goto IL_17E;
							case 2:
								if (matchCollection.Count > 0)
								{
									num = 1;
									continue;
								}
								goto IL_19F;
							}
							break;
							IL_17E:
							if (true)
							{
							}
							enumerator = matchCollection.GetEnumerator();
							num = 0;
						}
						break;
					}
					}
				}
				break;
			}
		}
		IL_19F:
		spr\u25C5.ᜀ(A_0, A_1, A_2, spr_u226E);
		return spr_u226E;
	}

	// Token: 0x06001DC3 RID: 7619 RVA: 0x001D6930 File Offset: 0x001D5930
	private static void ᜀ(Paragraph A_0, Regex A_1, bool A_2, spr\u226E A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_0E:
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_219;
					case 1:
						num = 3;
						continue;
					case 3:
						if (true)
						{
						}
						if (!A_2)
						{
							num = 0;
							continue;
						}
						return;
					case 4:
						try
						{
							num = 14;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_1CB;
								case 1:
								{
									TextSelection textSelection;
									if (textSelection != null)
									{
										num = 2;
										continue;
									}
									break;
								}
								case 2:
								{
									TextSelection textSelection;
									A_3.Add(textSelection);
									num = 15;
									continue;
								}
								case 3:
									num = 4;
									continue;
								case 4:
								{
									if (A_2)
									{
										num = 9;
										continue;
									}
									Body body;
									spr\u226E spr_u226E = body.ᜁ(A_1);
									num = 7;
									continue;
								}
								case 5:
									num = 11;
									continue;
								case 6:
								{
									Body body;
									if (body != null)
									{
										num = 3;
										continue;
									}
									break;
								}
								case 7:
								{
									spr\u226E spr_u226E;
									if (spr_u226E != null)
									{
										num = 5;
										continue;
									}
									break;
								}
								case 8:
								{
									spr\u226E spr_u226E;
									A_3.AddRange(spr_u226E);
									num = 13;
									continue;
								}
								case 9:
								{
									Body body;
									TextSelection textSelection = body.ᜀ(A_1);
									num = 1;
									continue;
								}
								case 10:
									goto IL_1BF;
								case 11:
								{
									spr\u226E spr_u226E;
									if (spr_u226E.Count > 0)
									{
										num = 8;
										continue;
									}
									break;
								}
								case 12:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 10;
										continue;
									}
									ParagraphBase a_ = (ParagraphBase)enumerator.Current;
									Body body = spr\u25C5.ᜀ(a_);
									num = 6;
									continue;
								}
								case 15:
									goto IL_1BF;
								}
								IL_DF:
								num = 12;
								continue;
								goto IL_DF;
								IL_1BF:
								num = 0;
							}
							IL_1CB:
							return;
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
										goto IL_216;
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
										goto IL_218;
									}
									break;
								}
							}
							IL_216:
							IL_218:;
						}
						goto IL_219;
					}
					if (A_3.Count > 0)
					{
						num = 1;
						continue;
					}
					IL_219:
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
						IEnumerator enumerator = A_0.Items.GetEnumerator();
						num = 4;
						break;
					}
					}
				}
			}
			return;
		}
	}

	// Token: 0x06001DC4 RID: 7620 RVA: 0x001D6BDC File Offset: 0x001D5BDC
	private static Body ᜀ(ParagraphBase A_0)
	{
		switch (0)
		{
		default:
		{
			Body result;
			for (;;)
			{
				result = null;
				DocumentObjectType documentObjectType = A_0.DocumentObjectType;
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D9;
					case 1:
					{
						TextBox textBox = (TextBox)(A_0 as spr\u248F).ᜎ()[0];
						result = textBox.Body;
						num = 8;
						continue;
					}
					case 2:
						num = 0;
						continue;
					case 3:
						if ((A_0 as spr\u248F).ᜎ() != null)
						{
							num = 13;
							continue;
						}
						return result;
					case 4:
						return result;
					case 5:
						if ((A_0 as spr\u248F).ᜎ().Count > 0)
						{
							num = 1;
							continue;
						}
						return result;
					case 6:
						if (A_0 is spr\u248F)
						{
							num = 11;
							continue;
						}
						return result;
					case 7:
						switch (documentObjectType)
						{
						case DocumentObjectType.Comment:
						{
							Comment comment = (Comment)A_0;
							result = comment.Body;
							num = 4;
							continue;
						}
						case DocumentObjectType.Footnote:
						{
							Footnote footnote = (Footnote)A_0;
							result = footnote.TextBody;
							num = 9;
							continue;
						}
						case DocumentObjectType.TextBox:
						{
							TextBox textBox2 = (TextBox)A_0;
							result = textBox2.Body;
							goto IL_BC;
						}
						default:
							num = 2;
							continue;
						}
						break;
					case 8:
						goto IL_19A;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BC;
						default:
							goto IL_1CC;
						}
						break;
					case 10:
						goto IL_C8;
					case 11:
						if (true)
						{
						}
						num = 3;
						continue;
					case 12:
						if (documentObjectType != DocumentObjectType.Shape)
						{
							num = 14;
							continue;
						}
						num = 6;
						continue;
					case 13:
						num = 5;
						continue;
					case 14:
						num = 7;
						continue;
					}
					break;
					IL_BC:
					num = 10;
				}
			}
			IL_C8:
			IL_D9:
			IL_19A:
			return result;
			IL_1CC:
			if (false)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06001DC5 RID: 7621 RVA: 0x001D6DDC File Offset: 0x001D5DDC
	public TextSelection[] ᜀ(Body A_0, Regex A_1)
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
			if (A_0.Items.Count != 0)
			{
				return this.ᜀ(A_0, A_1, 0, A_0.Items.Count - 1);
			}
			break;
		}
		return null;
	}

	// Token: 0x06001DC6 RID: 7622 RVA: 0x001D6E40 File Offset: 0x001D5E40
	public TextSelection[] ᜀ(Body A_0, Regex A_1, int A_2, int A_3)
	{
		switch (0)
		{
		default:
		{
			TextSelection[] array;
			for (;;)
			{
				array = null;
				int num = A_2;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						if (array != null)
						{
							num2 = 11;
							continue;
						}
						array = this.ᜀ(A_1);
						num2 = 13;
						continue;
					case 1:
						goto IL_16C;
					case 2:
						if (array != null)
						{
							num2 = 10;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_11C;
						default:
							if (false)
							{
							}
							num++;
							num2 = 7;
							continue;
						}
						break;
					case 3:
						if (A_0.Items[num] is Paragraph)
						{
							num2 = 4;
							continue;
						}
						num2 = 9;
						continue;
					case 4:
					{
						Paragraph paragraph = A_0.Items[num] as Paragraph;
						array = this.ᜀ(paragraph, A_1, 0, paragraph.Items.Count - 1);
						if (true)
						{
						}
						num2 = 0;
						continue;
					}
					case 5:
					{
						Table a_ = A_0.Items[num] as Table;
						array = this.ᜀ(a_, A_1);
						num2 = 8;
						continue;
					}
					case 6:
						if (num > A_3)
						{
							num2 = 12;
							continue;
						}
						goto IL_11C;
					case 7:
						goto IL_16C;
					case 8:
						goto IL_14C;
					case 9:
						if (A_0.Items[num] is Table)
						{
							num2 = 5;
							continue;
						}
						goto IL_14C;
					case 10:
						return array;
					case 11:
						return array;
					case 12:
						goto IL_18C;
					case 13:
						goto IL_14C;
					}
					break;
					IL_11C:
					num2 = 3;
					continue;
					IL_14C:
					num2 = 2;
					continue;
					IL_16C:
					num2 = 6;
				}
			}
			return array;
			IL_18C:
			return this.ᜀ(A_1);
		}
		}
	}

	// Token: 0x06001DC7 RID: 7623 RVA: 0x001D702C File Offset: 0x001D602C
	internal TextSelection[] ᜀ(Paragraph A_0, Regex A_1, int A_2, int A_3)
	{
		switch (0)
		{
		default:
		{
			int num = 9;
			TextSelection[] array;
			for (;;)
			{
				int num2;
				Body body;
				switch (num)
				{
				case 0:
					goto IL_6B;
				case 1:
					goto IL_85;
				case 2:
					return array;
				case 3:
				{
					if (num2 > A_3)
					{
						num = 10;
						continue;
					}
					ParagraphBase a_ = A_0[num2];
					body = spr\u25C5.ᜀ(a_);
					num = 4;
					continue;
				}
				case 4:
					if (body != null)
					{
						num = 11;
						continue;
					}
					goto IL_6B;
				case 5:
					goto IL_98;
				case 6:
					goto IL_98;
				case 7:
					if (array != null)
					{
						num = 2;
						continue;
					}
					num2++;
					num = 6;
					continue;
				case 8:
					if (true)
					{
					}
					this.ᜁ().Add(A_0);
					num = 1;
					continue;
				case 10:
					goto IL_B5;
				case 11:
					array = this.ᜀ(body, A_1);
					num = 0;
					continue;
				}
				if (!this.ᜁ().Contains(A_0))
				{
					num = 8;
					continue;
				}
				goto IL_85;
				IL_6B:
				num = 7;
				continue;
				IL_85:
				array = null;
				body = null;
				num2 = A_2;
				num = 5;
				continue;
				IL_98:
				num = 3;
			}
			return array;
			IL_B5:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return array;
			default:
				if (false)
				{
				}
				return array;
			}
			break;
		}
		}
	}

	// Token: 0x06001DC8 RID: 7624 RVA: 0x001D7198 File Offset: 0x001D6198
	internal TextSelection[] ᜀ(Regex A_0)
	{
		switch (0)
		{
		default:
		{
			int num = 13;
			spr\u226E spr_u226E;
			for (;;)
			{
				int num2;
				Match match;
				int num3;
				int index;
				switch (num)
				{
				case 0:
					goto IL_37A;
				case 1:
				{
					if (true)
					{
					}
					int count;
					if (num2 == count - 1)
					{
						num = 24;
						continue;
					}
					goto IL_37A;
				}
				case 2:
					if (match != null)
					{
						num = 19;
						continue;
					}
					goto IL_3DE;
				case 3:
					goto IL_2FF;
				case 4:
				{
					string text;
					if (num3 == text.Length)
					{
						num = 23;
						continue;
					}
					goto IL_2C2;
				}
				case 5:
					if (spr_u226E.Count > 0)
					{
						num = 11;
						continue;
					}
					goto IL_3DE;
				case 6:
					num = 5;
					continue;
				case 7:
				{
					if (this.ᜀ.Count == 0)
					{
						num = 15;
						continue;
					}
					string text = string.Empty;
					match = null;
					StringBuilder stringBuilder = new StringBuilder();
					num2 = 0;
					int count = this.ᜀ.Count;
					num = 3;
					continue;
				}
				case 8:
					goto IL_2FF;
				case 9:
					goto IL_20B;
				case 10:
					num = 7;
					continue;
				case 11:
					goto IL_278;
				case 12:
					num = 2;
					continue;
				case 14:
					if (spr_u226E != null)
					{
						num = 6;
						continue;
					}
					goto IL_3DE;
				case 15:
					goto IL_347;
				case 16:
					if (match.Success)
					{
						num = 22;
						continue;
					}
					goto IL_3DE;
				case 17:
					if (index == 0)
					{
						num = 18;
						continue;
					}
					goto IL_2C2;
				case 18:
					num = 4;
					continue;
				case 19:
					num = 16;
					continue;
				case 20:
					try
					{
						num = 2;
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
									goto IL_176;
								case 1:
									goto IL_168;
								case 3:
								{
									Paragraph paragraph;
									if (match.Length == paragraph.Text.Length)
									{
										num = 6;
										continue;
									}
									break;
								}
								case 5:
								{
									List<Paragraph>.Enumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 1;
										continue;
									}
									Paragraph paragraph = enumerator.Current;
									num = 3;
									continue;
								}
								case 6:
								{
									Paragraph paragraph;
									TextSelection item = new TextSelection(paragraph, 0, paragraph.Text.Length);
									spr_u226E.Add(item);
									num = 4;
									continue;
								}
								}
								IL_147:
								num = 5;
								continue;
								goto IL_147;
							}
							IL_168:
							num = 0;
						}
						IL_176:
						goto IL_20B;
					}
					finally
					{
						List<Paragraph>.Enumerator enumerator;
						((IDisposable)enumerator).Dispose();
					}
					goto IL_189;
				case 21:
				{
					int count;
					if (num2 >= count)
					{
						num = 12;
						continue;
					}
					Paragraph paragraph2 = this.ᜀ[num2];
					StringBuilder stringBuilder;
					stringBuilder.Append(paragraph2.Text);
					num = 1;
					continue;
				}
				case 22:
					goto IL_189;
				case 23:
				{
					spr_u226E = new spr\u226E();
					List<Paragraph>.Enumerator enumerator = this.ᜀ.GetEnumerator();
					num = 20;
					continue;
				}
				case 24:
				{
					StringBuilder stringBuilder;
					string text = stringBuilder.ToString();
					match = A_0.Match(text);
					num = 0;
					continue;
				}
				}
				if (this.ᜀ != null)
				{
					num = 10;
					continue;
				}
				break;
				IL_189:
				index = match.Index;
				num3 = index + match.Length;
				num = 17;
				continue;
				IL_20B:
				num = 14;
				continue;
				IL_2C2:
				spr_u226E = this.ᜀ(this.ᜀ, match);
				num = 9;
				continue;
				IL_2FF:
				num = 21;
				continue;
				IL_37A:
				num2++;
				num = 8;
			}
			IL_22C:
			return null;
			IL_278:
			this.ᜀ.Clear();
			return spr_u226E.ToArray();
			IL_347:
			goto IL_22C;
			IL_3DE:
			return null;
		}
		}
	}

	// Token: 0x06001DC9 RID: 7625 RVA: 0x001D7594 File Offset: 0x001D6594
	internal TextSelection[] ᜀ(Table A_0, Regex A_1)
	{
		switch (0)
		{
		default:
		{
			TextSelection[] array = null;
			IEnumerator enumerator = A_0.Rows.GetEnumerator();
			TextSelection[] result;
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
							num = 4;
							continue;
						}
						TableRow tableRow = (TableRow)enumerator.Current;
						IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
						num = 2;
						continue;
					}
					case 1:
						goto IL_1AA;
					case 2:
						try
						{
							num = 5;
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
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 4;
											continue;
										}
										TableCell a_ = (TableCell)enumerator2.Current;
										array = this.ᜀ(a_, A_1);
										num = 1;
										continue;
									}
									case 1:
										if (array != null)
										{
											num = 2;
											continue;
										}
										break;
									case 2:
										result = array;
										num = 6;
										continue;
									case 3:
										goto IL_150;
									case 4:
										goto IL_142;
									case 6:
										goto IL_EF;
									}
									IL_121:
									num = 0;
									continue;
									goto IL_121;
								}
								IL_142:
								num = 3;
							}
							IL_EF:
							return result;
							IL_150:
							break;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator2;
								IDisposable disposable = enumerator2 as IDisposable;
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
										goto IL_19D;
									case 1:
										goto IL_19B;
									case 2:
										disposable.Dispose();
										num = 1;
										continue;
									}
									break;
								}
							}
							IL_19B:
							IL_19D:;
						}
						goto IL_19E;
					case 4:
						goto IL_19E;
					}
					IL_53:
					num = 0;
					continue;
					goto IL_53;
					IL_19E:
					num = 1;
				}
				IL_1AA:
				return array;
			}
			finally
			{
				if (true)
				{
				}
				for (;;)
				{
					IDisposable disposable2 = enumerator as IDisposable;
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (disposable2 != null)
							{
								num = 2;
								continue;
							}
							goto IL_1FF;
						case 1:
							goto IL_1FD;
						case 2:
							disposable2.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_1FD:
				IL_1FF:;
			}
			return result;
		}
		}
	}

	// Token: 0x06001DCA RID: 7626 RVA: 0x001D77D8 File Offset: 0x001D67D8
	private spr\u226E ᜀ(List<Paragraph> A_0, Match A_1)
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int index = A_1.Index;
			int num = index + A_1.Length;
			string text = string.Empty;
			int num2 = 0;
			int num3 = 0;
			spr\u226E spr_u226E = null;
			using (List<Paragraph>.Enumerator enumerator = A_0.GetEnumerator())
			{
				int num4 = 16;
				for (;;)
				{
					switch (num4)
					{
					case 0:
						num4 = 12;
						continue;
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num4 = 15;
							continue;
						}
						Paragraph paragraph = enumerator.Current;
						int num5 = -1;
						int num6 = -1;
						num3 = text.Length;
						text += paragraph.Text;
						num2 = text.Length;
						num4 = 19;
						continue;
					}
					case 2:
						goto IL_282;
					case 3:
						if (num2 < num)
						{
							num4 = 23;
							continue;
						}
						break;
					case 4:
					{
						Paragraph paragraph;
						int num6;
						spr_u226E.Add(new TextSelection(paragraph, 0, num6));
						num4 = 5;
						continue;
					}
					case 5:
						goto IL_47C;
					case 6:
						num4 = 20;
						continue;
					case 7:
					{
						int num6;
						if (num6 != -1)
						{
							num4 = 26;
							continue;
						}
						goto IL_438;
					}
					case 8:
						num4 = 13;
						continue;
					case 9:
						if (num3 > index)
						{
							num4 = 31;
							continue;
						}
						break;
					case 10:
						goto IL_47C;
					case 11:
						goto IL_317;
					case 12:
						if (index <= num2)
						{
							num4 = 39;
							continue;
						}
						goto IL_317;
					case 13:
						if (num <= num2 + 1)
						{
							num4 = 33;
							continue;
						}
						goto IL_2F5;
					case 14:
					{
						int num5;
						if (num5 != -1)
						{
							num4 = 17;
							continue;
						}
						goto IL_380;
					}
					case 15:
						goto IL_47C;
					case 17:
						num4 = 32;
						continue;
					case 18:
						goto IL_488;
					case 19:
						if (num3 <= index)
						{
							num4 = 0;
							continue;
						}
						goto IL_317;
					case 20:
					{
						Paragraph paragraph;
						int num6;
						if (num6 <= paragraph.Text.Length)
						{
							num4 = 4;
							continue;
						}
						break;
					}
					case 21:
						num4 = 7;
						continue;
					case 22:
					{
						Paragraph paragraph;
						if (paragraph.Text != string.Empty)
						{
							num4 = 37;
							continue;
						}
						break;
					}
					case 23:
						num4 = 22;
						continue;
					case 24:
					{
						int num6;
						if (num6 != -1)
						{
							num4 = 2;
							continue;
						}
						num4 = 9;
						continue;
					}
					case 25:
						num4 = 24;
						continue;
					case 26:
					{
						Paragraph paragraph;
						int num5;
						int num6;
						spr_u226E.Add(new TextSelection(paragraph, num5, num6));
						num4 = 10;
						continue;
					}
					case 27:
						goto IL_2F5;
					case 28:
					{
						int num5;
						if (num5 != -1)
						{
							num4 = 21;
							continue;
						}
						goto IL_438;
					}
					case 31:
						num4 = 3;
						continue;
					case 32:
					{
						Paragraph paragraph;
						int num5;
						if (num5 < paragraph.Text.Length)
						{
							num4 = 35;
							continue;
						}
						goto IL_380;
					}
					case 33:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_309;
						default:
						{
							if (false)
							{
							}
							int num6 = num - num3;
							num4 = 27;
							continue;
						}
						}
						break;
					case 34:
					{
						int num5;
						if (num5 == -1)
						{
							goto IL_309;
						}
						goto IL_282;
					}
					case 35:
					{
						Paragraph paragraph;
						int num5;
						spr_u226E.Add(new TextSelection(paragraph, num5, paragraph.Text.Length));
						num4 = 30;
						continue;
					}
					case 36:
					{
						int num6;
						if (num6 != -1)
						{
							num4 = 6;
							continue;
						}
						break;
					}
					case 37:
					{
						Paragraph paragraph;
						spr_u226E.Add(new TextSelection(paragraph, 0, paragraph.Text.Length));
						num4 = 29;
						continue;
					}
					case 38:
						if (num3 <= num)
						{
							num4 = 8;
							continue;
						}
						goto IL_2F5;
					case 39:
					{
						spr_u226E = new spr\u226E();
						int num5 = index - num3;
						num4 = 11;
						continue;
					}
					}
					IL_259:
					num4 = 1;
					continue;
					goto IL_259;
					IL_282:
					num4 = 28;
					continue;
					IL_2F5:
					num4 = 34;
					continue;
					IL_309:
					num4 = 25;
					continue;
					IL_317:
					num4 = 38;
					continue;
					IL_380:
					num4 = 36;
					continue;
					IL_438:
					num4 = 14;
					continue;
					IL_47C:
					num4 = 18;
				}
				IL_488:;
			}
			return spr_u226E;
		}
		}
	}

	// Token: 0x04001F79 RID: 8057
	private List<Paragraph> ᜀ;

	// Token: 0x04001F7A RID: 8058
	[ThreadStatic]
	public static spr\u25C5 ᜁ;
}
