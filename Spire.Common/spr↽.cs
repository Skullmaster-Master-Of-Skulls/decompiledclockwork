using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Xml;
using Spire.Xls;
using Spire.Xls.Converter;
using Spire.Xls.Core;
using Spire.Xls.Core.Spreadsheet;

// Token: 0x02000017 RID: 23
internal class spr\u21BD
{
	// Token: 0x06000070 RID: 112 RVA: 0x00006438 File Offset: 0x00004638
	static spr\u21BD()
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
		spr\u21BD.ᜀ = null;
		spr\u21BD.ᜁ = new string[]
		{
			SheetFinishedEventHandler.b("첶覸욺", a_),
			SheetFinishedEventHandler.b("첶覸욺펾꓀ꗂ뇄", a_),
			SheetFinishedEventHandler.b("첶覸욺춾ꣀ꓂귄돆", a_)
		};
		spr\u21BD.ᜂ = null;
		spr\u21BD.ᜃ = new object();
		spr\u21BD.ᜂ = new Dictionary<BordersLineType, string>
		{
			{
				BordersLineType.EdgeLeft,
				SheetFinishedEventHandler.b("\udbb6\udcb8\uddba즼", a_)
			},
			{
				BordersLineType.EdgeTop,
				SheetFinishedEventHandler.b("쎶횸쮺", a_)
			},
			{
				BordersLineType.EdgeRight,
				SheetFinishedEventHandler.b("얶킸\udcba햼쮾", a_)
			},
			{
				BordersLineType.EdgeBottom,
				SheetFinishedEventHandler.b("햶횸쾺즼킾곀", a_)
			}
		};
	}

	// Token: 0x06000071 RID: 113 RVA: 0x00006534 File Offset: 0x00004734
	private spr\u21BD()
	{
	}

	// Token: 0x06000072 RID: 114 RVA: 0x00006548 File Offset: 0x00004748
	public static spr\u21BD ᜁ(Worksheet A_0)
	{
		spr\u21BD spr_u21BD;
		for (;;)
		{
			IL_38:
			spr_u21BD = new spr\u21BD();
			spr_u21BD.ᜄ = A_0;
			for (;;)
			{
				IL_45:
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0.ListObjects.Count == 0)
						{
							num = 3;
							continue;
						}
						spr_u21BD.ᜅ = new List<IListObject>(A_0.ListObjects);
						num = 8;
						continue;
					case 1:
						num = 5;
						continue;
					case 2:
						num = 0;
						continue;
					case 3:
						if (true)
						{
						}
						goto IL_180;
					case 4:
						goto IL_8D;
					case 5:
						if (A_0.ListObjects != null)
						{
							num = 2;
							continue;
						}
						goto IL_180;
					case 6:
						return spr_u21BD;
					case 7:
					{
						int num2;
						if (num2 < spr_u21BD.ᜅ.Count)
						{
							spr_u21BD.ᜆ[num2 * 2] = spr_u21BD.ᜅ[num2].Location.Row;
							spr_u21BD.ᜆ[num2 * 2 + 1] = spr_u21BD.ᜅ[num2].Location.LastRow;
							num2++;
							num = 9;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_45;
						default:
							if (false)
							{
							}
							num = 11;
							continue;
						}
						break;
					}
					case 8:
					{
						List<IListObject> list = spr_u21BD.ᜅ;
						if (spr\u21BD.ᜇ == null)
						{
							spr\u21BD.ᜇ = new Comparison<IListObject>(spr\u21BD.ᜀ);
						}
						list.Sort(spr\u21BD.ᜇ);
						spr_u21BD.ᜆ = new int[spr_u21BD.ᜅ.Count * 2];
						int num2 = 0;
						num = 4;
						continue;
					}
					case 9:
						goto IL_8D;
					case 10:
						if (A_0 != null)
						{
							num = 1;
							continue;
						}
						goto IL_180;
					case 11:
						goto IL_CD;
					}
					goto IL_38;
					IL_8D:
					num = 7;
					continue;
					IL_180:
					num = 6;
				}
			}
		}
		IL_CD:
		spr\u21BD.ᜀ(A_0);
		return spr_u21BD;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00006730 File Offset: 0x00004930
	private static void ᜀ(Worksheet A_0)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 1;
			for (;;)
			{
				object obj;
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					try
					{
						num = 4;
						for (;;)
						{
							Dictionary<TableBuiltInStyles, spr\u233E> dictionary4;
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
									goto IL_4E4;
								case 1:
									num = 2;
									continue;
								case 2:
									goto IL_AD;
								case 3:
									try
									{
										Assembly assembly = A_0.GetType().Assembly;
										Stream manifestResourceStream = assembly.GetManifestResourceStream(SheetFinishedEventHandler.b("낿ꯁ뛃ꏅ鋉ꃋ뷍ﻏ金믓ꓕ뷗进껝鋟蟡藣若鯧苩觫语蓯\udcf1ꃳ韵髷雹駻跽⻿瘁攃搅搇漉匋納搏欑砓猕欗㐙搛猝䰟", a_));
										try
										{
											XmlDocument xmlDocument = new XmlDocument();
											xmlDocument.Load(manifestResourceStream);
											IEnumerator enumerator = xmlDocument.DocumentElement.SelectNodes(SheetFinishedEventHandler.b("ꆿꃁꣃꏅ鯇뻉뗋ꋍ뗏", a_)).GetEnumerator();
											try
											{
												num = 4;
												for (;;)
												{
													switch (num)
													{
													case 0:
													{
														if (!enumerator.MoveNext())
														{
															num = 5;
															continue;
														}
														XmlNode xmlNode = (XmlNode)enumerator.Current;
														XmlElement xmlElement = xmlNode as XmlElement;
														string attribute = xmlElement.GetAttribute(SheetFinishedEventHandler.b("ꆿ꿁ꇃ", a_));
														TableBuiltInStyles tableBuiltInStyles = (TableBuiltInStyles)Enum.Parse(typeof(TableBuiltInStyles), attribute);
														Dictionary<string, Dictionary<string, string>> dictionary = new Dictionary<string, Dictionary<string, string>>();
														IEnumerator enumerator2 = xmlElement.SelectNodes(SheetFinishedEventHandler.b("뒿믁ꣃꏅ", a_)).GetEnumerator();
														num = 2;
														continue;
													}
													case 1:
														goto IL_42B;
													case 2:
													{
														Dictionary<string, Dictionary<string, string>> dictionary;
														try
														{
															num = 2;
															for (;;)
															{
																switch (num)
																{
																case 0:
																	num = 3;
																	continue;
																case 1:
																{
																	IEnumerator enumerator2;
																	if (!enumerator2.MoveNext())
																	{
																		num = 0;
																		continue;
																	}
																	XmlNode xmlNode2 = (XmlNode)enumerator2.Current;
																	XmlElement xmlElement2 = xmlNode2 as XmlElement;
																	string attribute2 = xmlElement2.GetAttribute(SheetFinishedEventHandler.b("ꆿ꿁ꇃ", a_));
																	Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
																	IEnumerator enumerator3 = xmlElement2.SelectNodes(SheetFinishedEventHandler.b("늿귁듃ꏅ뫇뻉뗋", a_)).GetEnumerator();
																	num = 4;
																	continue;
																}
																case 3:
																	goto IL_37D;
																case 4:
																{
																	Dictionary<string, string> dictionary2;
																	try
																	{
																		num = 2;
																		for (;;)
																		{
																			switch (num)
																			{
																			case 0:
																				goto IL_2A8;
																			case 1:
																				num = 0;
																				continue;
																			case 3:
																			{
																				IEnumerator enumerator3;
																				if (!enumerator3.MoveNext())
																				{
																					num = 1;
																					continue;
																				}
																				XmlNode xmlNode3 = (XmlNode)enumerator3.Current;
																				XmlElement xmlElement3 = xmlNode3 as XmlElement;
																				dictionary2[xmlElement3.GetAttribute(SheetFinishedEventHandler.b("ꆿ꿁ꇃ", a_))] = xmlElement3.SelectSingleNode(SheetFinishedEventHandler.b("쪽ꖿ뫁냃", a_)).Value;
																				num = 4;
																				continue;
																			}
																			}
																			IL_27C:
																			num = 3;
																			continue;
																			goto IL_27C;
																		}
																		IL_2A8:;
																	}
																	finally
																	{
																		for (;;)
																		{
																			IEnumerator enumerator3;
																			IDisposable disposable = enumerator3 as IDisposable;
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
																					goto IL_2F2;
																				case 1:
																					goto IL_2F0;
																				case 2:
																					disposable.Dispose();
																					num = 1;
																					continue;
																				}
																				break;
																			}
																		}
																		IL_2F0:
																		IL_2F2:;
																	}
																	string attribute2;
																	dictionary[attribute2] = dictionary2;
																	num = 5;
																	continue;
																}
																}
																IL_1D9:
																num = 1;
																continue;
																goto IL_1D9;
															}
															IL_37D:;
														}
														finally
														{
															for (;;)
															{
																IEnumerator enumerator2;
																IDisposable disposable2 = enumerator2 as IDisposable;
																num = 1;
																for (;;)
																{
																	switch (num)
																	{
																	case 0:
																		disposable2.Dispose();
																		num = 2;
																		continue;
																	case 1:
																		if (disposable2 != null)
																		{
																			num = 0;
																			continue;
																		}
																		goto IL_3C7;
																	case 2:
																		goto IL_3C5;
																	}
																	break;
																}
															}
															IL_3C5:
															IL_3C7:;
														}
														Dictionary<TableBuiltInStyles, spr\u233E> dictionary3 = dictionary4;
														TableBuiltInStyles tableBuiltInStyles;
														TableBuiltInStyles key = tableBuiltInStyles;
														spr\u233E spr_u233E = new spr\u233E();
														spr_u233E.ᜀ(tableBuiltInStyles);
														spr_u233E.ᜀ(dictionary);
														dictionary3[key] = spr_u233E;
														num = 3;
														continue;
													}
													case 5:
														num = 1;
														continue;
													}
													IL_3F9:
													num = 0;
													continue;
													goto IL_3F9;
												}
												IL_42B:;
											}
											finally
											{
												for (;;)
												{
													IDisposable disposable3 = enumerator as IDisposable;
													num = 0;
													for (;;)
													{
														switch (num)
														{
														case 0:
															if (disposable3 != null)
															{
																num = 2;
																continue;
															}
															goto IL_475;
														case 1:
															goto IL_473;
														case 2:
															disposable3.Dispose();
															num = 1;
															continue;
														}
														break;
													}
												}
												IL_473:
												IL_475:;
											}
										}
										finally
										{
											num = 1;
											for (;;)
											{
												switch (num)
												{
												case 0:
													((IDisposable)manifestResourceStream).Dispose();
													num = 2;
													continue;
												case 2:
													goto IL_4B3;
												}
												if (manifestResourceStream == null)
												{
													break;
												}
												num = 0;
											}
											IL_4B3:;
										}
										goto IL_4D2;
									}
									catch (Exception)
									{
										goto IL_4D2;
									}
									goto IL_4BB;
									IL_4D2:
									spr\u21BD.ᜀ = dictionary4;
									num = 0;
									continue;
								}
								if (spr\u21BD.ᜀ != null)
								{
									num = 1;
									continue;
								}
								break;
							}
							IL_4BB:
							dictionary4 = new Dictionary<TableBuiltInStyles, spr\u233E>();
							num = 3;
						}
						IL_AD:
						IL_4E4:
						return;
					}
					finally
					{
						Monitor.Exit(obj);
					}
					goto IL_4EE;
				case 2:
					return;
				}
				if (spr\u21BD.ᜀ != null)
				{
					num = 2;
					continue;
				}
				IL_4EE:
				Monitor.Enter(obj = spr\u21BD.ᜃ);
				num = 0;
			}
			return;
		}
		}
	}

	// Token: 0x06000074 RID: 116 RVA: 0x00006CF0 File Offset: 0x00004EF0
	public spr\u192F ᜀ(XlsRange A_0, spr\u192F A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 39;
			spr\u192F spr_u192F;
			for (;;)
			{
				List<string> list;
				Dictionary<string, string> dictionary;
				List<string>.Enumerator enumerator;
				IListObject listObject;
				int num4;
				switch (num)
				{
				case 0:
					goto IL_352;
				case 1:
					list.Add(SheetFinishedEventHandler.b("좹펻즽", a_));
					num = 34;
					continue;
				case 2:
					if (spr_u192F.ᜤ() == ExcelPatternType.None)
					{
						num = 6;
						continue;
					}
					goto IL_2ED;
				case 3:
					goto IL_28F;
				case 4:
					if (dictionary.ContainsKey(SheetFinishedEventHandler.b("\ud9b9펻튽꾿냁", a_)))
					{
						num = 17;
						continue;
					}
					goto IL_6A8;
				case 5:
					goto IL_2ED;
				case 6:
					num = 24;
					continue;
				case 7:
					goto IL_30A;
				case 8:
					list.Add(SheetFinishedEventHandler.b("특\ud9bb\udfbd꒿ꟁ뛃", a_));
					num = 7;
					continue;
				case 9:
					spr_u192F.ᜀ().IsBold = true;
					num = 3;
					continue;
				case 10:
				{
					try
					{
						num = 9;
						for (;;)
						{
							int num2;
							switch (num)
							{
							case 0:
								num = 4;
								continue;
							case 1:
								goto IL_204;
							case 2:
							{
								spr\u233E spr_u233E;
								string key;
								if (spr_u233E.ᜁ().ContainsKey(key))
								{
									num = 5;
									continue;
								}
								goto IL_229;
							}
							case 3:
							{
								string[] array;
								if (num2 >= array.Length)
								{
									num = 7;
									continue;
								}
								string format = array[num2];
								string arg;
								string key = string.Format(format, arg);
								num = 2;
								continue;
							}
							case 4:
								goto IL_249;
							case 5:
							{
								spr\u233E spr_u233E;
								string key;
								this.ᜀ<string, string>(dictionary, spr_u233E.ᜁ()[key]);
								num = 8;
								continue;
							}
							case 6:
							{
								if (!enumerator.MoveNext())
								{
									num = 0;
									continue;
								}
								string arg = enumerator.Current;
								string[] array = spr\u21BD.ᜁ;
								num2 = 0;
								num = 1;
								continue;
							}
							case 8:
								goto IL_229;
							case 10:
								goto IL_204;
							}
							IL_185:
							num = 6;
							continue;
							goto IL_185;
							IL_204:
							num = 3;
							continue;
							IL_229:
							num2++;
							num = 10;
						}
						IL_249:
						goto IL_524;
					}
					finally
					{
						((IDisposable)enumerator).Dispose();
					}
					goto IL_25C;
					IL_524:
					IFont font = A_0.Workbook.InnerExtFormats.ᜁ(A_0.Workbook.DefaultXFIndex).ᜀ();
					num = 16;
					continue;
				}
				case 11:
					if (listObject.Location.Column <= A_0.Column)
					{
						num = 29;
						continue;
					}
					return A_1;
				case 12:
					num = 19;
					continue;
				case 13:
					list.Add(SheetFinishedEventHandler.b("좹펻즽龿껁ꗃ뗅볇", a_));
					num = 35;
					continue;
				case 14:
				{
					if (listObject.Location.LastColumn < A_0.Column)
					{
						num = 0;
						continue;
					}
					spr\u233E spr_u233E = spr\u21BD.ᜀ[listObject.BuiltInTableStyle];
					spr_u192F = new spr\u22FD(A_1);
					dictionary = new Dictionary<string, string>();
					int num3 = A_0.Row - listObject.Location.Row;
					list = new List<string>();
					num = 28;
					continue;
				}
				case 15:
					return A_1;
				case 16:
				{
					IFont font;
					if (spr_u192F.ᜀ() != font)
					{
						num = 30;
						continue;
					}
					goto IL_EC;
				}
				case 17:
					goto IL_EC;
				case 18:
					num4 = ~num4;
					num = 22;
					continue;
				case 19:
					if (dictionary[SheetFinishedEventHandler.b("\udcb9펻킽뒿돃ꏅꇇ귉꓋뫍", a_)] == SheetFinishedEventHandler.b("趹費躽", a_))
					{
						num = 9;
						continue;
					}
					goto IL_28F;
				case 20:
					if (num4 < 0)
					{
						num = 18;
						continue;
					}
					goto IL_3B9;
				case 21:
					if (dictionary.ContainsKey(SheetFinishedEventHandler.b("\udcb9펻킽뒿돃ꏅꇇ귉꓋뫍", a_)))
					{
						num = 12;
						continue;
					}
					goto IL_28F;
				case 22:
					if (num4 % 2 == 0)
					{
						num = 32;
						continue;
					}
					goto IL_3B9;
				case 23:
					goto IL_6A8;
				case 24:
					if (dictionary.ContainsKey(SheetFinishedEventHandler.b("\ud8b9\uddbb\uddbdꮿꗁ뛃꧅뷇꓉꣋", a_)))
					{
						num = 33;
						continue;
					}
					goto IL_2ED;
				case 25:
					if (A_0.Row == listObject.Location.LastRow)
					{
						num = 13;
						continue;
					}
					goto IL_30A;
				case 26:
					if (listObject.BuiltInTableStyle == TableBuiltInStyles.None)
					{
						num = 38;
						continue;
					}
					goto IL_25C;
				case 27:
				{
					IFont font;
					if (spr_u192F.ᜀ().Color == font.Color)
					{
						num = 40;
						continue;
					}
					goto IL_6A8;
				}
				case 28:
				{
					int num3;
					if (num3 == 0)
					{
						num = 8;
						continue;
					}
					num = 36;
					continue;
				}
				case 29:
					num = 14;
					continue;
				case 30:
					num = 27;
					continue;
				case 31:
					goto IL_305;
				case 32:
					return A_1;
				case 33:
					spr_u192F.ᜀ(ExcelPatternType.Solid);
					spr_u192F.ᜃ(ColorTranslator.FromHtml(dictionary[SheetFinishedEventHandler.b("\ud8b9\uddbb\uddbdꮿꗁ뛃꧅뷇꓉꣋", a_)]));
					num = 5;
					continue;
				case 34:
					goto IL_4F1;
				case 35:
					if (true)
					{
					}
					goto IL_30A;
				case 36:
				{
					int num3;
					if (num3 % 2 == 1)
					{
						num = 1;
						continue;
					}
					list.Add(SheetFinishedEventHandler.b("좹펻즽", a_));
					num = 37;
					continue;
				}
				case 37:
					goto IL_4F1;
				case 38:
					return A_1;
				case 40:
					num = 4;
					continue;
				}
				if (this.ᜅ == null)
				{
					num = 15;
					continue;
				}
				num4 = Array.BinarySearch<int>(this.ᜆ, A_0.Row);
				num = 20;
				continue;
				IL_EC:
				spr_u192F.ᜀ().Color = ColorTranslator.FromHtml(dictionary[SheetFinishedEventHandler.b("\ud9b9펻튽꾿냁", a_)]);
				num = 23;
				continue;
				IL_25C:
				num = 11;
				continue;
				IL_28F:
				num = 2;
				continue;
				IL_2ED:
				Dictionary<BordersLineType, string>.Enumerator enumerator2 = spr\u21BD.ᜂ.GetEnumerator();
				num = 31;
				continue;
				IL_30A:
				enumerator = list.GetEnumerator();
				num = 10;
				continue;
				IL_3B9:
				num4 /= 2;
				listObject = this.ᜅ[num4];
				num = 26;
				continue;
				IL_4F1:
				num = 25;
				continue;
				IL_6A8:
				num = 21;
			}
			return A_1;
			IL_305:
			try
			{
				num = 1;
				for (;;)
				{
					Dictionary<string, string> dictionary;
					KeyValuePair<BordersLineType, string> keyValuePair;
					string key3;
					switch (num)
					{
					case 0:
						goto IL_768;
					case 2:
						goto IL_8F0;
					case 3:
						goto IL_768;
					case 4:
						if (spr_u192F.ᜪ()[keyValuePair.Key].LineStyle == LineStyleType.None)
						{
							num = 11;
							continue;
						}
						break;
					case 5:
						num = 2;
						continue;
					case 6:
						spr_u192F.ᜪ()[keyValuePair.Key].LineStyle = LineStyleType.Thin;
						num = 3;
						continue;
					case 7:
					{
						string key2 = string.Format(SheetFinishedEventHandler.b("\ud8b9펻첽꒿ꟁ뛃돇韛뇋꟏믑냓ꋕ냗", a_), keyValuePair.Value);
						key3 = string.Format(SheetFinishedEventHandler.b("\ud8b9펻첽꒿ꟁ뛃돇韛뇋돏뷑룓맕꫗", a_), keyValuePair.Value);
						num = 10;
						continue;
					}
					case 9:
					{
						Dictionary<BordersLineType, string>.Enumerator enumerator2;
						if (!enumerator2.MoveNext())
						{
							num = 5;
							continue;
						}
						keyValuePair = enumerator2.Current;
						num = 4;
						continue;
					}
					case 10:
					{
						string key2;
						if (dictionary[key2] == SheetFinishedEventHandler.b("钹覻캽뒿", a_))
						{
							num = 6;
							continue;
						}
						spr_u192F.ᜪ()[keyValuePair.Key].LineStyle = LineStyleType.Medium;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					case 11:
					{
						string key4 = string.Format(SheetFinishedEventHandler.b("\ud8b9펻첽꒿ꟁ뛃돇韛뇋ꏏꛑ귓뫕뷗", a_), keyValuePair.Value);
						num = 12;
						continue;
					}
					case 12:
					{
						string key4;
						if (dictionary.ContainsKey(key4))
						{
							num = 7;
							continue;
						}
						break;
					}
					}
					goto IL_725;
					IL_768:
					spr_u192F.ᜪ()[keyValuePair.Key].Color = ColorTranslator.FromHtml(dictionary[key3]);
					num = 8;
					continue;
					IL_8BE:
					num = 9;
					continue;
					IL_725:
					goto IL_8BE;
				}
				IL_8F0:;
			}
			finally
			{
				Dictionary<BordersLineType, string>.Enumerator enumerator2;
				((IDisposable)enumerator2).Dispose();
			}
			return spr_u192F;
			IL_352:
			return A_1;
		}
		}
	}

	// Token: 0x06000075 RID: 117 RVA: 0x00007634 File Offset: 0x00005834
	private void ᜀ<ᜀ, ᜁ>(Dictionary<ᜀ, ᜁ> A_0, Dictionary<ᜀ, ᜁ> A_1)
	{
		Dictionary<ᜀ, ᜁ>.Enumerator enumerator = A_1.GetEnumerator();
		try
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_97;
				case 1:
					num = 0;
					continue;
				case 2:
				{
					if (!enumerator.MoveNext())
					{
						num = 1;
						continue;
					}
					KeyValuePair<ᜀ, ᜁ> keyValuePair = enumerator.Current;
					A_0[keyValuePair.Key] = keyValuePair.Value;
					num = 3;
					continue;
				}
				case 3:
					goto IL_71;
				}
				goto IL_2D;
				IL_71:
				num = 2;
				continue;
				IL_2D:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_97;
				default:
					if (false)
					{
					}
					goto IL_71;
				}
			}
			IL_97:;
		}
		finally
		{
			if (true)
			{
			}
			((IDisposable)enumerator).Dispose();
		}
	}

	// Token: 0x06000076 RID: 118 RVA: 0x0000770C File Offset: 0x0000590C
	[CompilerGenerated]
	private static int ᜀ(IListObject A_0, IListObject A_1)
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
		return A_0.Location.Row - A_1.Location.Row;
	}

	// Token: 0x0400003C RID: 60
	private static Dictionary<TableBuiltInStyles, spr\u233E> ᜀ;

	// Token: 0x0400003D RID: 61
	private static string[] ᜁ;

	// Token: 0x0400003E RID: 62
	private static Dictionary<BordersLineType, string> ᜂ;

	// Token: 0x0400003F RID: 63
	private static object ᜃ;

	// Token: 0x04000040 RID: 64
	private Worksheet ᜄ;

	// Token: 0x04000041 RID: 65
	private List<IListObject> ᜅ;

	// Token: 0x04000042 RID: 66
	private int[] ᜆ;

	// Token: 0x04000043 RID: 67
	[CompilerGenerated]
	private static Comparison<IListObject> ᜇ;
}
