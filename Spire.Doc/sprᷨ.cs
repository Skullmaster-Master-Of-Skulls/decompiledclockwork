using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Schema;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x020003B0 RID: 944
internal class spr\u1DE8 : spr\u2477
{
	// Token: 0x0600350D RID: 13581 RVA: 0x0030DDE8 File Offset: 0x0030CDE8
	protected string \u170D()
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
		return this.\u1715;
	}

	// Token: 0x0600350E RID: 13582 RVA: 0x0030DE2C File Offset: 0x0030CE2C
	protected void \u1712(string A_0)
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
		this.\u1715 = A_0;
	}

	// Token: 0x0600350F RID: 13583 RVA: 0x0030DE70 File Offset: 0x0030CE70
	protected spr\u1DE8.ᜂ ᜌ()
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (this.\u171A == null)
				{
					if (true)
					{
					}
					num = 2;
					continue;
				}
				goto IL_A8;
			case 2:
				this.\u171A = new spr\u1DE8.ᜂ();
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A8;
				default:
					if (false)
					{
					}
					num = 4;
					continue;
				}
				break;
			case 3:
				goto IL_3A;
			case 4:
				goto IL_75;
			}
			if (this.\u170D.Count > 0)
			{
				num = 3;
			}
			else
			{
				num = 1;
			}
		}
		IL_3A:
		return this.\u170D.Peek();
		IL_75:
		IL_A8:
		return this.\u171A;
	}

	// Token: 0x06003510 RID: 13584 RVA: 0x0030DF2C File Offset: 0x0030CF2C
	protected void ᜀ(spr\u1DE8.ᜂ A_0)
	{
		if (this.\u170D.Count > 0)
		{
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					goto IL_26;
				}
			}
			IL_26:
			if (false)
			{
			}
			if (true)
			{
			}
			this.\u170D.Pop();
			this.\u170D.Push(A_0);
			return;
		}
		this.\u171A = A_0;
	}

	// Token: 0x06003511 RID: 13585 RVA: 0x0030DF98 File Offset: 0x0030CF98
	protected Paragraph ᜋ()
	{
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_106;
			case 1:
				if (this.ᜨ != null)
				{
					num = 5;
					continue;
				}
				goto IL_112;
			case 2:
				goto IL_43;
			case 3:
				goto IL_45;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_43;
				default:
					if (false)
					{
					}
					this.ᜂ();
					num = 3;
					continue;
				}
				break;
			case 5:
				this.\u1712.ᜀ(this.ᜨ);
				num = 0;
				continue;
			case 7:
				if (this.ᜥ)
				{
					num = 4;
					continue;
				}
				goto IL_45;
			}
			if (this.\u1712 == null)
			{
				num = 2;
				continue;
			}
			break;
			IL_45:
			num = 1;
			continue;
			IL_43:
			this.\u1712 = new Paragraph(this.ᜏ.Document);
			this.ᜏ.Add(this.\u1712);
			if (true)
			{
			}
			num = 7;
		}
		IL_106:
		IL_112:
		return this.\u1712;
	}

	// Token: 0x06003512 RID: 13586 RVA: 0x0030E0C0 File Offset: 0x0030D0C0
	private Stack<ListStyle> ᜊ()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				this.\u1719 = new Stack<ListStyle>();
				num = 0;
				continue;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6F;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					break;
				}
				break;
			}
			if (this.\u1719 != null)
			{
				break;
			}
			num = 1;
		}
		IL_6F:
		return this.\u1719;
	}

	// Token: 0x06003513 RID: 13587 RVA: 0x0030E144 File Offset: 0x0030D144
	private ListStyle ᜉ()
	{
		int num = 2;
		for (;;)
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
				switch (num)
				{
				case 0:
					goto IL_7F;
				case 1:
					goto IL_60;
				case 3:
					num = 1;
					continue;
				}
				if (this.\u1719 != null)
				{
					num = 3;
					continue;
				}
				goto IL_56;
			}
			IL_60:
			if (this.\u1719.Count != 0)
			{
				goto IL_81;
			}
			num = 0;
		}
		IL_56:
		return null;
		IL_7F:
		goto IL_56;
		IL_81:
		return this.\u1719.Peek();
	}

	// Token: 0x06003514 RID: 13588 RVA: 0x0030E1E0 File Offset: 0x0030D1E0
	private string ᜀ(XmlElement A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				string attribute = A_0.GetAttribute(ClipboardData.b("ᩱٳ፵ṷ", a_));
				string attribute2 = A_0.GetAttribute(ClipboardData.b("ᅱᱳ᝵੷ॹ᥻੽", a_));
				Uri uri = null;
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						WebClient webClient = new WebClient();
						num = 11;
						continue;
					}
					case 1:
						if (this.\u1716 != null)
						{
							num = 17;
							continue;
						}
						goto IL_F2;
					case 2:
					{
						string text2;
						string text = text2;
						num = 3;
						continue;
					}
					case 3:
						goto IL_BD;
					case 4:
						goto IL_4C1;
					case 5:
					{
						string text;
						if (text != null)
						{
							num = 14;
							continue;
						}
						goto IL_2C1;
					}
					case 6:
					{
						if (uri != null)
						{
							num = 0;
							continue;
						}
						string text = null;
						num = 7;
						continue;
					}
					case 7:
					{
						if (File.Exists(attribute))
						{
							num = 16;
							continue;
						}
						string text2 = Path.Combine(this.\u170D(), attribute);
						num = 12;
						continue;
					}
					case 8:
						goto IL_F2;
					case 9:
						goto IL_BD;
					case 10:
						try
						{
							num = 0;
							for (;;)
							{
								Stream stream;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_1D3;
								default:
								{
									if (false)
									{
									}
									StreamReader streamReader;
									switch (num)
									{
									case 1:
										goto IL_1D3;
									case 2:
										try
										{
											return streamReader.ReadToEnd();
										}
										finally
										{
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 1:
													goto IL_220;
												case 2:
													((IDisposable)streamReader).Dispose();
													num = 1;
													continue;
												}
												if (streamReader == null)
												{
													break;
												}
												num = 2;
											}
											IL_220:;
										}
										goto IL_223;
									case 3:
										goto IL_223;
									}
									if (!string.IsNullOrEmpty(attribute2))
									{
										num = 3;
										continue;
									}
									break;
									IL_223:
									streamReader = new StreamReader(stream, Encoding.GetEncoding(attribute2), true);
									num = 2;
									continue;
								}
								}
								IL_1BD:
								StreamReader streamReader2 = new StreamReader(stream, true);
								num = 1;
								continue;
								IL_1D3:
								try
								{
									return streamReader2.ReadToEnd();
								}
								finally
								{
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 1:
											goto IL_1BA;
										case 2:
											((IDisposable)streamReader2).Dispose();
											num = 1;
											continue;
										}
										if (streamReader2 == null)
										{
											break;
										}
										num = 2;
									}
									IL_1BA:;
								}
								goto IL_1BD;
							}
						}
						finally
						{
							num = 1;
							for (;;)
							{
								Stream stream;
								switch (num)
								{
								case 0:
									((IDisposable)stream).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_27E;
								}
								if (stream == null)
								{
									break;
								}
								num = 0;
							}
							IL_27E:;
						}
						goto IL_281;
					case 11:
						try
						{
							WebClient webClient;
							Stream stream2 = webClient.OpenRead(uri);
							try
							{
								num = 3;
								for (;;)
								{
									StreamReader streamReader3;
									StreamReader streamReader4;
									switch (num)
									{
									case 0:
										try
										{
											return streamReader3.ReadToEnd();
										}
										finally
										{
											num = 2;
											for (;;)
											{
												switch (num)
												{
												case 0:
													((IDisposable)streamReader3).Dispose();
													num = 1;
													continue;
												case 1:
													goto IL_428;
												}
												if (streamReader3 == null)
												{
													break;
												}
												num = 0;
											}
											IL_428:;
										}
										goto IL_42B;
									case 1:
										try
										{
											return streamReader4.ReadToEnd();
										}
										finally
										{
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 1:
													((IDisposable)streamReader4).Dispose();
													num = 2;
													continue;
												case 2:
													goto IL_3BC;
												}
												if (streamReader4 == null)
												{
													break;
												}
												num = 1;
											}
											IL_3BC:;
										}
										goto IL_3BF;
									case 2:
										goto IL_3BF;
									}
									if (!string.IsNullOrEmpty(attribute2))
									{
										num = 2;
										continue;
									}
									goto IL_42B;
									IL_3BF:
									streamReader4 = new StreamReader(stream2, Encoding.GetEncoding(attribute2), true);
									num = 1;
									continue;
									IL_42B:
									streamReader3 = new StreamReader(stream2, true);
									num = 0;
								}
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										((IDisposable)stream2).Dispose();
										num = 1;
										continue;
									case 1:
										goto IL_480;
									}
									if (stream2 == null)
									{
										break;
									}
									num = 0;
								}
								IL_480:;
							}
						}
						finally
						{
							num = 0;
							for (;;)
							{
								WebClient webClient;
								switch (num)
								{
								case 1:
									((IDisposable)webClient).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_4BE;
								}
								if (webClient == null)
								{
									break;
								}
								num = 1;
							}
							IL_4BE:;
						}
						goto IL_4C1;
					case 12:
					{
						string text2;
						if (File.Exists(text2))
						{
							num = 2;
							continue;
						}
						goto IL_BD;
					}
					case 13:
						if (Uri.IsWellFormedUriString(attribute, UriKind.Absolute))
						{
							num = 4;
							continue;
						}
						goto IL_281;
					case 14:
					{
						string text;
						Stream stream = File.OpenRead(text);
						num = 10;
						continue;
					}
					case 15:
						goto IL_F2;
					case 16:
					{
						if (true)
						{
						}
						string text = attribute;
						num = 9;
						continue;
					}
					case 17:
						uri = new Uri(this.\u1716, attribute);
						num = 8;
						continue;
					}
					break;
					IL_BD:
					num = 5;
					continue;
					IL_F2:
					num = 6;
					continue;
					IL_281:
					num = 1;
					continue;
					IL_4C1:
					uri = new Uri(attribute);
					num = 15;
				}
			}
			IL_2C1:
			return null;
		}
	}

	// Token: 0x06003515 RID: 13589 RVA: 0x0030E7CC File Offset: 0x0030D7CC
	public void ᜀ(IBody A_0, string A_1, int A_2, int A_3, IParagraphStyle A_4, ListStyle A_5)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5A;
			case 1:
				if (A_5 != null)
				{
					num = 3;
					continue;
				}
				goto IL_A1;
			case 3:
				this.ᜬ = A_5;
				num = 5;
				continue;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_5A;
				default:
					if (false)
					{
					}
					this.ᜨ = A_4;
					num = 0;
					continue;
				}
				break;
			case 5:
				goto IL_58;
			}
			if (A_4 != null)
			{
				if (true)
				{
				}
				num = 4;
				continue;
			}
			IL_5A:
			num = 1;
		}
		IL_58:
		IL_A1:
		this.ᜀ(A_0, A_1, A_2, A_3);
		this.ᜨ = null;
		this.ᜬ = null;
	}

	// Token: 0x06003516 RID: 13590 RVA: 0x0030E894 File Offset: 0x0030D894
	public void ᜀ(IBody A_0, string A_1, int A_2, int A_3)
	{
		int a_ = 9;
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜀ();
				this.\u1715 = A_0.Document.HtmlBaseUrl;
				int num = 6;
				for (;;)
				{
					List<XmlElement>.Enumerator enumerator;
					XmlNode xmlNode;
					TextBodyPart textBodyPart;
					List<XmlElement> list;
					XmlNode xmlNode4;
					IEnumerator enumerator3;
					switch (num)
					{
					case 0:
						goto IL_2E7;
					case 1:
						goto IL_258;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_DB;
						default:
							goto IL_3B5;
						}
						break;
					case 3:
					{
						try
						{
							num = 4;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_245;
								case 1:
									num = 9;
									continue;
								case 2:
								{
									string a;
									if (!(a == ClipboardData.b("ᱮհੲᥴቶ", a_)))
									{
										num = 6;
										continue;
									}
									XmlElement xmlElement;
									this.ᜭ.ᜂ(xmlElement.InnerText);
									num = 10;
									continue;
								}
								case 5:
									num = 2;
									continue;
								case 6:
									num = 7;
									continue;
								case 7:
								{
									string a;
									if (!(a == ClipboardData.b("ͮᡰᵲṴ", a_)))
									{
										num = 1;
										continue;
									}
									XmlElement xmlElement;
									this.ᜭ.ᜂ(this.ᜀ(xmlElement));
									num = 3;
									continue;
								}
								case 8:
									num = 0;
									continue;
								case 11:
								{
									if (!enumerator.MoveNext())
									{
										num = 8;
										continue;
									}
									XmlElement xmlElement = enumerator.Current;
									num = 12;
									continue;
								}
								case 12:
								{
									string a;
									XmlElement xmlElement;
									if ((a = xmlElement.LocalName.ToLower()) != null)
									{
										num = 5;
										continue;
									}
									break;
								}
								}
								IL_1D2:
								num = 11;
								continue;
								goto IL_1D2;
							}
							IL_245:
							goto IL_97;
						}
						finally
						{
							((IDisposable)enumerator).Dispose();
						}
						goto IL_258;
						IL_97:
						bool a_2 = this.ᜅ(xmlNode);
						this.ᜀ(xmlNode.ChildNodes);
						this.ᜀ(a_2);
						textBodyPart.PasteAt(A_0, A_2, A_3);
						num = 4;
						continue;
					}
					case 4:
						if (textBodyPart.BodyItems.Count > 0)
						{
							goto IL_DB;
						}
						return;
					case 5:
					{
						Paragraph paragraph = textBodyPart.BodyItems[0] as Paragraph;
						ParagraphFormat format = paragraph.Format;
						(A_0.ChildObjects[A_2] as Paragraph).Format.ImportContainer(format);
						num = 7;
						continue;
					}
					case 6:
						if (Uri.IsWellFormedUriString(this.\u1715, UriKind.Absolute))
						{
							num = 13;
							continue;
						}
						goto IL_2E7;
					case 7:
					{
						Paragraph paragraph;
						if (!string.IsNullOrEmpty(paragraph.StyleName))
						{
							num = 12;
							continue;
						}
						return;
					}
					case 8:
						if (true)
						{
						}
						num = 9;
						continue;
					case 9:
						if (textBodyPart.BodyItems[0].DocumentObjectType == DocumentObjectType.Paragraph)
						{
							num = 5;
							continue;
						}
						return;
					case 10:
						if (xmlNode.LocalName.ToLower() == ClipboardData.b("ݮհṲᥴ", a_))
						{
							num = 1;
							continue;
						}
						goto IL_41E;
					case 11:
						try
						{
							num = 11;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_9BF;
								case 1:
									goto IL_9B3;
								case 2:
								{
									XmlNode xmlNode2;
									if (xmlNode2.LocalName.ToLower() == ClipboardData.b("ݮᑰቲᅴ", a_))
									{
										num = 3;
										continue;
									}
									goto IL_8A7;
								}
								case 3:
									num = 10;
									continue;
								case 4:
								{
									XmlNode xmlNode2;
									if (xmlNode2.LocalName.ToLower() == ClipboardData.b("൮Ṱᝲ౴", a_))
									{
										num = 5;
										continue;
									}
									break;
								}
								case 5:
								{
									XmlNode xmlNode2;
									xmlNode = xmlNode2;
									num = 9;
									continue;
								}
								case 6:
									try
									{
										num = 13;
										for (;;)
										{
											switch (num)
											{
											case 0:
												num = 29;
												continue;
											case 1:
											{
												string a2;
												if (!(a2 == ClipboardData.b("ͮᡰᵲṴ", a_)))
												{
													num = 6;
													continue;
												}
												XmlNode xmlNode3;
												XmlElement xmlElement2 = xmlNode3 as XmlElement;
												string text = xmlElement2.GetAttribute(ClipboardData.b("ᵮᑰὲ", a_));
												string text2 = xmlElement2.GetAttribute(ClipboardData.b("ݮͰᙲ፴", a_));
												num = 10;
												continue;
											}
											case 2:
												num = 1;
												continue;
											case 3:
												num = 5;
												continue;
											case 4:
												num = 23;
												continue;
											case 5:
											{
												string a2;
												XmlNode xmlNode3;
												if ((a2 = xmlNode3.LocalName.ToLower()) != null)
												{
													num = 0;
													continue;
												}
												break;
											}
											case 6:
												num = 18;
												continue;
											case 7:
												num = 8;
												continue;
											case 8:
											{
												string text;
												if (text.ToLower() == ClipboardData.b("ᱮհੲᥴቶ੸፺᡼᩾", a_))
												{
													num = 4;
													continue;
												}
												break;
											}
											case 9:
												goto IL_85C;
											case 10:
											{
												string text;
												if (!string.IsNullOrEmpty(text))
												{
													num = 14;
													continue;
												}
												break;
											}
											case 12:
												num = 17;
												continue;
											case 14:
												num = 31;
												continue;
											case 16:
												num = 9;
												continue;
											case 17:
											{
												string text2;
												if ((text2 = text2.Trim()).Length > 0)
												{
													num = 19;
													continue;
												}
												break;
											}
											case 19:
											{
												XmlNode xmlNode3;
												list.Add(xmlNode3 as XmlElement);
												num = 15;
												continue;
											}
											case 20:
												num = 26;
												continue;
											case 21:
												num = 25;
												continue;
											case 22:
												this.\u1716 = new Uri(this.\u170D());
												num = 28;
												continue;
											case 23:
											{
												string text2;
												if (!string.IsNullOrEmpty(text2))
												{
													num = 12;
													continue;
												}
												break;
											}
											case 24:
												if (this.\u1716 == null)
												{
													num = 21;
													continue;
												}
												break;
											case 25:
												if (Uri.IsWellFormedUriString(this.\u170D(), UriKind.Absolute))
												{
													num = 22;
													continue;
												}
												break;
											case 26:
											{
												string a2;
												if (!(a2 == ClipboardData.b("ᱮհੲᥴቶ", a_)))
												{
													num = 2;
													continue;
												}
												XmlNode xmlNode3;
												list.Add(xmlNode3 as XmlElement);
												num = 11;
												continue;
											}
											case 27:
											{
												XmlNode xmlNode3;
												if (xmlNode3.NodeType == XmlNodeType.Element)
												{
													num = 3;
													continue;
												}
												break;
											}
											case 29:
											{
												string a2;
												if (!(a2 == ClipboardData.b("൮ၰrၴ", a_)))
												{
													num = 20;
													continue;
												}
												XmlNode xmlNode3;
												this.\u1712(this.ᜀ(xmlNode3, ClipboardData.b("ݮͰᙲ፴", a_)));
												num = 24;
												continue;
											}
											case 30:
											{
												IEnumerator enumerator2;
												if (!enumerator2.MoveNext())
												{
													num = 16;
													continue;
												}
												XmlNode xmlNode3 = (XmlNode)enumerator2.Current;
												num = 27;
												continue;
											}
											case 31:
											{
												string text;
												if ((text = text.Trim()).Length > 0)
												{
													num = 7;
													continue;
												}
												break;
											}
											}
											IL_6A4:
											num = 30;
											continue;
											goto IL_6A4;
										}
										IL_85C:;
									}
									finally
									{
										for (;;)
										{
											IEnumerator enumerator2;
											IDisposable disposable = enumerator2 as IDisposable;
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
														num = 0;
														continue;
													}
													goto IL_8A6;
												case 2:
													goto IL_8A4;
												}
												break;
											}
										}
										IL_8A4:
										IL_8A6:;
									}
									goto IL_8A7;
								case 7:
								{
									XmlNode xmlNode2;
									xmlNode4 = xmlNode2;
									IEnumerator enumerator2 = xmlNode4.ChildNodes.GetEnumerator();
									num = 6;
									continue;
								}
								case 8:
								{
									if (!enumerator3.MoveNext())
									{
										num = 1;
										continue;
									}
									XmlNode xmlNode2 = (XmlNode)enumerator3.Current;
									num = 2;
									continue;
								}
								case 9:
									goto IL_9B3;
								case 10:
								{
									XmlNode xmlNode2;
									if (xmlNode2.NodeType == XmlNodeType.Element)
									{
										num = 7;
										continue;
									}
									goto IL_8A7;
								}
								}
								goto IL_479;
								IL_8A7:
								num = 4;
								continue;
								IL_92D:
								num = 8;
								continue;
								IL_479:
								goto IL_92D;
								IL_9B3:
								num = 0;
							}
							IL_9BF:
							goto IL_41E;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable2 = enumerator3 as IDisposable;
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
										goto IL_A0A;
									case 2:
										if (disposable2 != null)
										{
											num = 0;
											continue;
										}
										goto IL_A0C;
									}
									break;
								}
							}
							IL_A0A:
							IL_A0C:;
						}
						return;
					case 12:
					{
						Paragraph paragraph;
						(A_0.ChildObjects[A_2] as Paragraph).ApplyStyle(paragraph.StyleName);
						num = 2;
						continue;
					}
					case 13:
						this.\u1716 = new Uri(this.\u1715);
						num = 0;
						continue;
					}
					break;
					IL_DB:
					num = 8;
					continue;
					IL_258:
					enumerator3 = xmlNode.ChildNodes.GetEnumerator();
					num = 11;
					continue;
					IL_2E7:
					textBodyPart = new TextBodyPart(A_0.Document);
					this.ᜏ = textBodyPart.BodyItems;
					this.\u1712 = null;
					this.ᜈ(A_1);
					this.ᜩ = new spr\u1DE8.ᜃ();
					xmlNode = this.ᜌ.DocumentElement;
					xmlNode4 = this.ᜌ.DocumentElement;
					list = new List<XmlElement>();
					num = 10;
					continue;
					IL_41E:
					enumerator = list.GetEnumerator();
					num = 3;
				}
			}
			IL_3B5:
			if (false)
			{
			}
			return;
		}
	}

	// Token: 0x06003517 RID: 13591 RVA: 0x0030F2FC File Offset: 0x0030E2FC
	public bool ᜀ(string A_0, XHTMLValidationType A_1)
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
		string text;
		return this.ᜀ(A_0, A_1, out text);
	}

	// Token: 0x06003518 RID: 13592 RVA: 0x0030F344 File Offset: 0x0030E344
	public bool ᜀ(string A_0, XHTMLValidationType A_1, out string A_2)
	{
		int a_ = 14;
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_218:
				num = 1;
				break;
			default:
				if (false)
				{
				}
				goto IL_64;
			}
			string text;
			string str;
			Assembly executingAssembly;
			XmlSchema xmlSchema;
			for (;;)
			{
				IL_35:
				switch (num)
				{
				case 0:
					goto IL_A9;
				case 1:
					if (text.StartsWith(ClipboardData.b("䡳ṵ౷᝹ၻ", a_)))
					{
						num = 7;
						continue;
					}
					A_0 = str + A_0 + ClipboardData.b("䡳奵ၷ๹ᅻች빿", a_);
					num = 0;
					continue;
				case 2:
					goto IL_280;
				case 3:
					goto IL_A9;
				case 4:
					num = 9;
					continue;
				case 5:
					goto IL_138;
				case 6:
					goto IL_C6;
				case 7:
				{
					int num2 = text.IndexOf(ClipboardData.b("䩳", a_));
					A_0 = str + A_0.Remove(0, num2 + 1);
					num = 3;
					continue;
				}
				case 8:
					switch (A_1)
					{
					case XHTMLValidationType.Strict:
					{
						Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(ClipboardData.b("❳ٵᅷࡹ᥻偽쑿ꢅ\uda87ﾋ뒙풟쾡좣鞥薧\ud9a9\ud8ab\udcad\ud9af톱삳颵삷즹\ud8bb", a_));
						xmlSchema = XmlSchema.Read(manifestResourceStream, new ValidationEventHandler(this.ᜀ));
						num = 5;
						continue;
					}
					case XHTMLValidationType.Transitional:
					{
						Stream manifestResourceStream2 = executingAssembly.GetManifestResourceStream(ClipboardData.b("❳ٵᅷࡹ᥻偽쑿ꢅ\uda87ﾋ뒙풟쾡좣鞥薧\udea9\udeab쾭\udeaf솱\uddb3습톷햹튻\udfbd겿볃뗅곇", a_));
						xmlSchema = XmlSchema.Read(manifestResourceStream2, new ValidationEventHandler(this.ᜀ));
						num = 2;
						continue;
					}
					case XHTMLValidationType.None:
						return true;
					default:
						num = 4;
						continue;
					}
					break;
				case 9:
					goto IL_149;
				}
				goto IL_64;
				IL_A9:
				A_0 = this.ᜐ(A_0);
				if (true)
				{
				}
				num = 6;
			}
			IL_C6:
			try
			{
				for (;;)
				{
					XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(A_0, XmlNodeType.Document, new XmlParserContext(null, null, null, XmlSpace.None));
					xmlValidatingReader.Schemas.Add(xmlSchema);
					num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_1B9;
						case 2:
							if (!xmlValidatingReader.Read())
							{
								num = 0;
								continue;
							}
							goto IL_193;
						case 3:
							goto IL_193;
						}
						break;
						IL_193:
						num = 2;
					}
				}
				IL_1B9:;
			}
			catch (Exception ex)
			{
				A_2 = ex.Message;
				return false;
			}
			return true;
			IL_138:
			IL_149:
			IL_1D0:
			text = A_0.ToLower();
			this.ᜌ = new XmlDocument();
			this.ᜌ.PreserveWhitespace = true;
			str = ClipboardData.b("䡳ṵ౷᝹ၻ幽ﮇ랉꺋", a_) + xmlSchema.TargetNamespace + ClipboardData.b("噳䡵", a_);
			goto IL_218;
			IL_280:
			goto IL_1D0;
			IL_64:
			A_2 = string.Empty;
			executingAssembly = Assembly.GetExecutingAssembly();
			xmlSchema = null;
			A_0 = this.ᜏ(A_0);
			num = 8;
			goto IL_35;
		}
		}
	}

	// Token: 0x06003519 RID: 13593 RVA: 0x0030F620 File Offset: 0x0030E620
	private string ᜐ(string A_0)
	{
		int a_ = 13;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_E2:
			if (!A_0.Contains(ClipboardData.b("佲呴㍶㙸㡺⥼♾톀욂", a_)))
			{
				return A_0;
			}
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
			int num2;
			switch (num)
			{
			case 0:
				goto IL_A6;
			case 1:
				num2 = A_0.IndexOf(ClipboardData.b("佲呴፶ᙸ᡺ॼپ", a_));
				num = 3;
				continue;
			case 3:
				goto IL_105;
			case 4:
				num = 7;
				continue;
			case 5:
				if (num2 == -1)
				{
					num = 1;
					continue;
				}
				goto IL_105;
			case 6:
				return A_0;
			case 7:
				goto IL_E2;
			}
			if (true)
			{
			}
			if (!A_0.Contains(ClipboardData.b("佲呴፶ᙸ᡺ॼپ", a_)))
			{
				num = 4;
				continue;
			}
			IL_A6:
			num2 = A_0.IndexOf(ClipboardData.b("佲呴㍶㙸㡺⥼♾톀욂", a_));
			num = 5;
			continue;
			IL_105:
			int num3 = A_0.IndexOf(ClipboardData.b("䵲", a_));
			A_0 = A_0.Remove(num2, num3 + 1);
			num = 6;
		}
		return A_0;
	}

	// Token: 0x0600351A RID: 13594 RVA: 0x0030F764 File Offset: 0x0030E764
	private string ᜏ(string A_0)
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
		A_0 = this.ᜎ(A_0);
		A_0 = this.\u170D(A_0);
		A_0 = this.ᜌ(A_0);
		A_0 = this.ᜋ(A_0);
		A_0 = this.ᜊ(A_0);
		A_0 = this.ᜉ(A_0);
		return A_0;
	}

	// Token: 0x0600351B RID: 13595 RVA: 0x0030F7D8 File Offset: 0x0030E7D8
	private string ᜎ(string A_0)
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0 = A_0.Replace(ClipboardData.b("却ٶ౸ᑺॼ䑾", a_), ClipboardData.b("却呶䩸佺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᙶॸᑺ๼䑾", a_), ClipboardData.b("却呶䩸䉺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᙶᑸ୺䙼", a_), ClipboardData.b("却呶䩸䍺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("却᭶൸䁺", a_), ClipboardData.b("却呶佸䭺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ၶ൸䁺", a_), ClipboardData.b("却呶佸䥺䙼", a_));
		return A_0;
	}

	// Token: 0x0600351C RID: 13596 RVA: 0x0030F8D4 File Offset: 0x0030E8D4
	private string \u170D(string A_0)
	{
		int a_ = 13;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0 = A_0.Replace(ClipboardData.b("啲᭴ᕶ੸୺䙼", a_), ClipboardData.b("啲噴䙶佸䭺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᱴቶŸ᡺ᅼ䑾", a_), ClipboardData.b("啲噴䙶佸䩺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᙴቶ᝸ེ䙼", a_), ClipboardData.b("啲噴䙶佸䥺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲մᡶ౸ᕺ᥼䑾", a_), ClipboardData.b("啲噴䙶佸䡺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᙴɶ୸ॺ᡼ᅾ몀", a_), ClipboardData.b("啲噴䙶佸佺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲౴ቶ᝸䁺", a_), ClipboardData.b("啲噴䙶佸乺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲᝴նླྀ᥺ᱼൾ몀", a_), ClipboardData.b("啲噴䙶佸䵺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ٴቶེ᩸䙼", a_), ClipboardData.b("啲噴䙶佸䱺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲t᩶ᕸ䁺", a_), ClipboardData.b("啲噴䙶佸䍺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᙴᡶॸɺ䙼", a_), ClipboardData.b("啲噴䙶佸䉺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᩴնᵸᵺ䙼", a_), ClipboardData.b("啲噴䙶乸䭺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᥴᙶࡸ๺ቼ䑾", a_), ClipboardData.b("啲噴䙶乸䩺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲᭴ᡶ൸䁺", a_), ClipboardData.b("啲噴䙶乸䥺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ٴὶx䁺", a_), ClipboardData.b("啲噴䙶乸䡺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ݴቶṸ䁺", a_), ClipboardData.b("啲噴䙶乸佺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᡴᙶ᩸ॺ䙼", a_), ClipboardData.b("啲噴䙶乸乺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᅴቶṸ䁺", a_), ClipboardData.b("啲噴䙶乸䵺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲մ᭶౸ࡺၼᅾ몀", a_), ClipboardData.b("啲噴䙶乸䱺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ٴɶॸ䥺䙼", a_), ClipboardData.b("啲噴䙶乸䍺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ٴɶॸ䡺䙼", a_), ClipboardData.b("啲噴䙶乸䉺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᑴᑶ౸ེ᡼䑾", a_), ClipboardData.b("啲噴䙶䅸䭺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᡴṶ᩸ॺቼ䑾", a_), ClipboardData.b("啲噴䙶䅸䩺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲մᙶ୸᩺䙼", a_), ClipboardData.b("啲噴䙶䅸䥺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᡴṶᵸὺቼ୾몀", a_), ClipboardData.b("啲噴䙶䅸䡺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᙴቶᵸቺᅼ䑾", a_), ClipboardData.b("啲噴䙶䅸佺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ٴɶॸ䩺䙼", a_), ClipboardData.b("啲噴䙶䅸乺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᩴնᵸᙺ䙼", a_), ClipboardData.b("啲噴䙶䅸䵺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ݴᙶࡸ๺ቼ䑾", a_), ClipboardData.b("啲噴䙶䅸䱺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲፴նᡸ᡺䱼䭾몀", a_), ClipboardData.b("啲噴䙶䅸䍺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲፴նᡸ᡺䱼䵾몀", a_), ClipboardData.b("啲噴䙶䅸䉺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲፴նᡸ᡺乼䭾몀", a_), ClipboardData.b("啲噴䙶䁸䭺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᱴٶ౸Ṻ๼୾몀", a_), ClipboardData.b("啲噴䙶䁸䩺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ŴṶᑸṺ๼䑾", a_), ClipboardData.b("啲噴䕶䡸乺䙼", a_));
		A_0 = A_0.Replace(ClipboardData.b("啲ᅴṶླྀቺ᥼᩾몀", a_), ClipboardData.b("啲噴䕶䵸䱺䙼", a_));
		return A_0;
	}

	// Token: 0x0600351D RID: 13597 RVA: 0x0030FDE4 File Offset: 0x0030EDE4
	private string ᜌ(string A_0)
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		if (true)
		{
		}
		A_0 = A_0.Replace(ClipboardData.b("䱩⵫७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫彭䥯䁱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⵫཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫彭䥯䅱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⵫൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫彭䥯䙱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⵫ᩭ᥯ṱၳ፵䍷", a_), ClipboardData.b("䱩佫彭䥯䝱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⵫᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫彭䥯䑱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⵫ᱭ᥯ᱱ፳䵵", a_), ClipboardData.b("䱩佫彭䥯䕱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⵫⭭ᱯ᭱፳䵵", a_), ClipboardData.b("䱩佫彭䥯䩱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⽫൭ᕯᙱᵳ᩵䍷", a_), ClipboardData.b("䱩佫彭䥯䭱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⥫७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫屭䁯䉱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⥫཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䁯䍱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⥫൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫屭䁯䁱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⥫᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䁯䅱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩╫७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫屭䁯䙱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩╫཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䁯䝱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩╫൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫屭䁯䑱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩╫᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䁯䕱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⥫㩭㡯䥱", a_), ClipboardData.b("䱩佫屭䁯䩱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩≫ᩭ᥯ṱၳ፵䍷", a_), ClipboardData.b("䱩佫屭䁯䭱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⍫७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫屭䅯䉱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⍫཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䅯䍱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⍫൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫屭䅯䁱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⍫ᩭ᥯ṱၳ፵䍷", a_), ClipboardData.b("䱩佫屭䅯䅱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⍫᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䅯䙱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩⍫ᵭᱯ፱ݳṵ䍷", a_), ClipboardData.b("䱩佫屭䅯䑱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩㥫७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫屭䅯䕱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩㥫཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䅯䩱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩㥫൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫屭䅯䭱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩㥫᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䉯䉱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩㕫཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䉯䍱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩㡫♭㽯ⁱ㩳䵵", a_), ClipboardData.b("䱩佫屭䉯䁱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩Ὣᑭᱯ᭱፳䵵", a_), ClipboardData.b("䱩佫屭䉯䅱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩൫७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫屭䉯䙱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩൫཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䉯䝱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩൫൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫屭䉯䑱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩൫ᩭ᥯ṱၳ፵䍷", a_), ClipboardData.b("䱩佫屭䉯䕱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩൫᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䉯䩱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩൫ᱭ᥯ᱱ፳䵵", a_), ClipboardData.b("䱩佫屭䉯䭱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩൫୭ᱯ᭱፳䵵", a_), ClipboardData.b("䱩佫屭䍯䉱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ཫ൭ᕯᙱᵳ᩵䍷", a_), ClipboardData.b("䱩佫屭䍯䍱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩५७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫屭䍯䁱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩५཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䍯䅱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩५൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫屭䍯䙱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩५᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䍯䝱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ի७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫屭䍯䑱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ի཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䍯䕱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ի൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫屭䍯䩱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ի᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䍯䭱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩५ᩭᡯ䥱", a_), ClipboardData.b("䱩佫屭䑯䉱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ɫᩭ᥯ṱၳ፵䍷", a_), ClipboardData.b("䱩佫屭䑯䍱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ͫ७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫屭䑯䁱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ͫ཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䑯䅱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ͫ൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫屭䑯䙱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ͫᩭ᥯ṱၳ፵䍷", a_), ClipboardData.b("䱩佫屭䑯䝱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ͫ᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䑯䑱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ͫᵭᱯ፱ݳṵ䍷", a_), ClipboardData.b("䱩佫屭䑯䩱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ᥫ७ɯ፱ɳ፵䍷", a_), ClipboardData.b("䱩佫屭䑯䭱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ᥫ཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䕯䉱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ᥫ൭᥯qᝳ䵵", a_), ClipboardData.b("䱩佫屭䕯䍱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ᥫ᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䕯䁱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ᕫ཭፯ݱs፵䍷", a_), ClipboardData.b("䱩佫屭䕯䅱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ᡫ٭Ὧqᩳ䵵", a_), ClipboardData.b("䱩佫屭䕯䙱佳", a_));
		A_0 = A_0.Replace(ClipboardData.b("䱩ᕫ᭭ᵯṱ佳", a_), ClipboardData.b("䱩佫屭䕯䝱佳", a_));
		return A_0;
	}

	// Token: 0x0600351E RID: 13598 RVA: 0x003106E4 File Offset: 0x0030F6E4
	private string ᜋ(string A_0)
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		A_0 = A_0.Replace(ClipboardData.b("却ᅶᙸॺᱼ፾뢂", a_), ClipboardData.b("却呶䅸䱺䵼䭾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ݶᡸॺॼ䑾", a_), ClipboardData.b("却呶䅸䱺䵼䥾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ቶŸቺ๼୾몀", a_), ClipboardData.b("却呶䅸䱺䵼䡾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ቶᑸ୺ॼپ몀", a_), ClipboardData.b("却呶䅸䱺䵼䙾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却᥶ᡸ᥺ᅼṾ몀", a_), ClipboardData.b("却呶䅸䱺䱼乾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ṷ੸ቺ፼䑾", a_), ClipboardData.b("却呶䅸䱺䱼䵾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却᥶ᙸེᑼᅾ몀", a_), ClipboardData.b("却呶䅸䱺䱼䱾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却᥶ၸ䁺", a_), ClipboardData.b("却呶䅸䱺䱼䩾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ݶ୸ᑺ᥼䑾", a_), ClipboardData.b("却呶䅸䱺䱼䙾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ѷ౸ᙺ䙼", a_), ClipboardData.b("却呶䅸䱺佼乾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却᩶ၸᕺࡼ౾몀", a_), ClipboardData.b("却呶䅸䱺佼䵾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却᭶ᙸ౺ᱼ౾뢂", a_), ClipboardData.b("却呶䅸䱺佼䡾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却նᡸὺᑼ᱾몀", a_), ClipboardData.b("却呶䅸䱺乼佾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ݶ୸ᑺർ䑾", a_), ClipboardData.b("却呶䅸䱺乼䱾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ṷ᝸ᵺᑼᅾ몀", a_), ClipboardData.b("却呶䅸䱺乼䭾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᙶ᝸ᱺ䙼", a_), ClipboardData.b("却呶䅸䱺乼䥾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᙶ᝸ὺ䙼", a_), ClipboardData.b("却呶䅸䱺䥼䱾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᡶ୸䁺", a_), ClipboardData.b("却呶䅸䱺䥼䭾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᑶᡸ୺䙼", a_), ClipboardData.b("却呶䅸䱺䥼䩾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᑶ౸୺䙼", a_), ClipboardData.b("却呶䅸䱺䥼䥾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ṷ᝸ེ䙼", a_), ClipboardData.b("却呶䅸䱺䥼䡾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ͶᅸṺོ᩾떀뢂", a_), ClipboardData.b("却呶䅸䱺䡼䥾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ѷၸᙺ䙼", a_), ClipboardData.b("却呶䅸䱺䭼䭾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᑶᙸᕺ᩼䑾", a_), ClipboardData.b("却呶䅸䱺䩼䱾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᙶ੸ɺၼཾ몀", a_), ClipboardData.b("却呶䅸䱺䩼䥾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却᥶ᱸ䁺", a_), ClipboardData.b("却呶䅸䍺䵼佾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ቶࡸ๺ᑼॾ몀", a_), ClipboardData.b("却呶䅸䍺䵼乾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却᭶ᱸ䁺", a_), ClipboardData.b("却呶䅸䍺䵼䭾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ၶᱸ䁺", a_), ClipboardData.b("却呶䅸䍺䵼䩾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ѷ౸᥺䙼", a_), ClipboardData.b("却呶䅸䍺乼䭾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ѷ౸୺䙼", a_), ClipboardData.b("却呶䅸䍺乼䩾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却᥶੸๺ὼ䑾", a_), ClipboardData.b("却呶䅸䍺乼䥾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ѷ౸᥺᡼䑾", a_), ClipboardData.b("却呶䅸䍺乼䝾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ѷ౸୺᡼䑾", a_), ClipboardData.b("却呶䅸䍺乼䙾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᡶॸ᝺ࡼ౾몀", a_), ClipboardData.b("却呶䅸䍺䡼䱾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᡶ൸ቺၼ᩾뢂", a_), ClipboardData.b("却呶䅸䍺䡼䩾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ݶᱸॺർ䑾", a_), ClipboardData.b("却呶䅸䍺䭼䙾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却Ѷᵸᑺॼ䑾", a_), ClipboardData.b("却呶䅸䉺䵼乾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("却ᅶ୸᩺๼፾몀", a_), ClipboardData.b("却呶䅸䥺䭼佾몀", a_));
		return A_0;
	}

	// Token: 0x0600351F RID: 13599 RVA: 0x00310CA8 File Offset: 0x0030FCA8
	private string ᜊ(string A_0)
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
		A_0 = A_0.Replace(ClipboardData.b("乧⭩kṭᡯ፱佳", a_), ClipboardData.b("乧䥩啫彭䍯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧⡩५ᩭᅯ䥱", a_), ClipboardData.b("乧䥩啫彭䑯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧⵩൫ͭᵯ፱佳", a_), ClipboardData.b("乧䥩啫彭䕯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧⹩५ɭѯ፱佳", a_), ClipboardData.b("乧䥩啫彭䙯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧⽩ᱫᵭ᥯ṱ᭳ᡵ䍷", a_), ClipboardData.b("乧䥩啫彭䝯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ど५ᩭᅯ䥱", a_), ClipboardData.b("乧䥩啫彭䡯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧⽩ᡫ཭䭯", a_), ClipboardData.b("乧䥩啫彭䥯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧㹩ѫ୭ѯ፱佳", a_), ClipboardData.b("乧䥩啫屭䁯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧⍩ͫᩭᅯ䥱", a_), ClipboardData.b("乧䥩啫屭䅯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧Ⅹ൫ṭo፱佳", a_), ClipboardData.b("乧䥩啫屭䉯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧♩൫ͭቯᙱᕳ䵵", a_), ClipboardData.b("乧䥩啫屭䍯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧❩ᥫ啭", a_), ClipboardData.b("乧䥩啫屭䑯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧⑩ᥫ啭", a_), ClipboardData.b("乧䥩啫屭䕯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧㉩ի啭", a_), ClipboardData.b("乧䥩啫屭䙯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧╩ūݭ፯q᭳ᡵ䍷", a_), ClipboardData.b("乧䥩啫屭䝯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧㩩ի啭", a_), ClipboardData.b("乧䥩啫屭䡯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧㡩ѫŭ䭯", a_), ClipboardData.b("乧䥩啫屭䥯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧㥩ի७ᵯ፱佳", a_), ClipboardData.b("乧䥩啫嵭䅯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧㹩൫᭭䭯", a_), ClipboardData.b("乧䥩啫嵭䉯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧㽩ᱫᵭ᥯ṱ᭳ᡵ䍷", a_), ClipboardData.b("乧䥩啫嵭䍯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧㩩ѫݭ䭯", a_), ClipboardData.b("乧䥩啫嵭䑯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧⥩ѫݭ䭯", a_), ClipboardData.b("乧䥩啫嵭䕯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧㩩Ὣݭ䭯", a_), ClipboardData.b("乧䥩啫嵭䙯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧╩ū୭ᝯ፱佳", a_), ClipboardData.b("乧䥩啫嵭䝯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧୩kṭᡯ፱佳", a_), ClipboardData.b("乧䥩啫婭䕯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ࡩ५ᩭᅯ䥱", a_), ClipboardData.b("乧䥩啫婭䙯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧൩൫ͭᵯ፱佳", a_), ClipboardData.b("乧䥩啫婭䝯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧๩५ɭѯ፱佳", a_), ClipboardData.b("乧䥩啫婭䡯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ཀྵᱫᵭ᥯ṱ᭳ᡵ䍷", a_), ClipboardData.b("乧䥩啫婭䥯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ၩ५ᩭᅯ䥱", a_), ClipboardData.b("乧䥩啫孭䁯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ཀྵᡫ཭䭯", a_), ClipboardData.b("乧䥩啫孭䅯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ṩѫ୭ѯ፱佳", a_), ClipboardData.b("乧䥩啫孭䉯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ͩͫᩭᅯ䥱", a_), ClipboardData.b("乧䥩啫孭䍯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ũ൫ṭo፱佳", a_), ClipboardData.b("乧䥩啫孭䑯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧٩൫ͭቯᙱᕳ䵵", a_), ClipboardData.b("乧䥩啫孭䕯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ݩᥫ啭", a_), ClipboardData.b("乧䥩啫孭䙯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ѩᥫ啭", a_), ClipboardData.b("乧䥩啫孭䝯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ቩի啭", a_), ClipboardData.b("乧䥩啫孭䡯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧թūݭ፯q᭳ᡵ䍷", a_), ClipboardData.b("乧䥩啫孭䥯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ᩩի啭", a_), ClipboardData.b("乧䥩啫塭䁯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ᡩѫŭ䭯", a_), ClipboardData.b("乧䥩啫塭䅯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ᥩի७ᵯ፱ታ䵵", a_), ClipboardData.b("乧䥩啫塭䉯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ᥩի७ᵯ፱佳", a_), ClipboardData.b("乧䥩啫塭䍯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ṩ൫᭭䭯", a_), ClipboardData.b("乧䥩啫塭䑯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧Ὡᱫᵭ᥯ṱ᭳ᡵ䍷", a_), ClipboardData.b("乧䥩啫塭䕯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ᩩѫݭ䭯", a_), ClipboardData.b("乧䥩啫塭䙯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧३ѫݭ䭯", a_), ClipboardData.b("乧䥩啫塭䝯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ᩩὫݭ䭯", a_), ClipboardData.b("乧䥩啫塭䡯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧թū୭ᝯ፱佳", a_), ClipboardData.b("乧䥩啫塭䥯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ṩѫ୭ѯ፱ݳཱུᕷ䅹", a_), ClipboardData.b("乧䥩啫奭䝯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧Ὡᱫᵭ᥯ᩱ佳", a_), ClipboardData.b("乧䥩啫奭䡯䥱", a_));
		A_0 = A_0.Replace(ClipboardData.b("乧ᩩիᡭ䭯", a_), ClipboardData.b("乧䥩啫噭䉯䥱", a_));
		return A_0;
	}

	// Token: 0x06003520 RID: 13600 RVA: 0x00311440 File Offset: 0x00310440
	private string ᜉ(string A_0)
	{
		int a_ = 17;
		if (true)
		{
		}
		switch (1 == 1)
		{
		}
		if (false)
		{
		}
		A_0 = A_0.Replace(ClipboardData.b("其㙸㹺ᅼᙾ뢂", a_), ClipboardData.b("其婸䡺乼䝾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᙸṺᅼᙾ뢂", a_), ClipboardData.b("其婸䡺乼䙾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("其⩸᡺ᱼൾ뺄", a_), ClipboardData.b("其婸䡺䡼䵾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("其੸᡺ᱼൾ뺄", a_), ClipboardData.b("其婸䡺䡼䱾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("其⁸๺ၼ፾몀", a_), ClipboardData.b("其婸䡺䩼䥾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ὸᕺቼ᥾몀", a_), ClipboardData.b("其婸佺䵼䵾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("其᩸ቺོ᱾몀", a_), ClipboardData.b("其婸䱺䱼佾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("其൸ቺᅼ᭾뢂", a_), ClipboardData.b("其婸䱺乼䵾몀", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᱸᕺ๼ཾ몀", a_), ClipboardData.b("其婸䍺䱼䙾떀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᱸᙺ๼ཾ몀", a_), ClipboardData.b("其婸䍺䱼䙾뒀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其൸፺ᑼᅾ뺄", a_), ClipboardData.b("其婸䍺佼佾낀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其͸౺፼ᕾ몀", a_), ClipboardData.b("其婸䍺佼佾떀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其͸౺᝼䑾", a_), ClipboardData.b("其婸䍺佼佾뒀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸॺၼ䑾", a_), ClipboardData.b("其婸䍺佼佾란뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其୸᝺ၼ䑾", a_), ClipboardData.b("其婸䍺佼佾뚀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其᝸ὺᱼ౾뢂", a_), ClipboardData.b("其婸䍺佼乾낀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᑸὺᱼ౾뢂", a_), ClipboardData.b("其婸䍺佼乾뎀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸࡺ౼੾뢂", a_), ClipboardData.b("其婸䍺佼乾란뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其୸ࡺ౼੾뢂", a_), ClipboardData.b("其婸䍺佼乾뚀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其੸᥺౼੾뢂", a_), ClipboardData.b("其婸䍺佼乾릀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸὺ౼੾뢂", a_), ClipboardData.b("其婸䍺佼䵾놀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其୸ὺ౼੾뢂", a_), ClipboardData.b("其婸䍺佼䵾낀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其᭸ὺ౼੾뢂", a_), ClipboardData.b("其婸䍺佼䵾뎀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᵸ᩺᩼᡾뺄", a_), ClipboardData.b("其婸䍺佼䵾떀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其㵸᩺᩼᡾뺄", a_), ClipboardData.b("其婸䍺佼䵾뒀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其᭸๺ᅼ፾몀", a_), ClipboardData.b("其婸䍺佼䵾란뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᅸṺᅼ፾뺄", a_), ClipboardData.b("其婸䍺佼䱾놀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ॸṺོቾ뺄", a_), ClipboardData.b("其婸䍺佼䭾놀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ॸॺᑼቾ뢂", a_), ClipboardData.b("其婸䍺佼䭾뎀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其⥸ॺᑼቾ뢂", a_), ClipboardData.b("其婸䍺佼䭾늀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸࡺᱼ๾뺄", a_), ClipboardData.b("其婸䍺佼䭾뢀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其୸ࡺᱼ๾뺄", a_), ClipboardData.b("其婸䍺佼䩾놀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᙸ᝺ᑼᅾ뢂", a_), ClipboardData.b("其婸䍺佼䩾떀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᱸ๺ོၾ몀", a_), ClipboardData.b("其婸䍺乼䥾떀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其൸ॺᱼ᭾뢂", a_), ClipboardData.b("其婸䍺䥼䝾뎀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸོ᩺ൾ몀", a_), ClipboardData.b("其婸䍺䡼䙾뎀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其౸ོ᩺ൾ몀", a_), ClipboardData.b("其婸䍺䡼䙾늀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其୸ོ᩺ൾ몀", a_), ClipboardData.b("其婸䍺䡼䙾떀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᵸོ᩺ൾ몀", a_), ClipboardData.b("其婸䍺䡼䙾뒀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᅸོ᩺ൾ몀", a_), ClipboardData.b("其婸䍺䡼䙾란뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其᩸ॺᱼൾ뢂", a_), ClipboardData.b("其婸䍺䭼䵾뢀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸ㩺ོൾ몀", a_), ClipboardData.b("其婸䍺䭼䩾란뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其౸㩺ོൾ몀", a_), ClipboardData.b("其婸䍺䭼䩾뚀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其୸㩺ོൾ몀", a_), ClipboardData.b("其婸䍺䭼䩾릀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᵸ㩺ོൾ몀", a_), ClipboardData.b("其婸䍺䭼䩾뢀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᅸ㩺ོൾ몀", a_), ClipboardData.b("其婸䍺䭼䥾놀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸ᡺᡼ᙾ뢂", a_), ClipboardData.b("其婸䍺䑼䥾릀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其୸᡺᡼ᙾ뢂", a_), ClipboardData.b("其婸䍺䑼䥾뢀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸᵺᅼၾ뺄", a_), ClipboardData.b("其婸䍺䑼䡾놀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其୸ᵺᅼၾ뺄", a_), ClipboardData.b("其婸䍺䑼䡾낀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸᑺݼ䑾", a_), ClipboardData.b("其婸䉺䭼䡾떀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其੸୺ᱼ᭾뺄", a_), ClipboardData.b("其婸䉺䕼䵾떀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其᩸᝺ࡼᵾ뢂", a_), ClipboardData.b("其婸䉺䕼䵾뚀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᅸṺᱼൾ뺄", a_), ClipboardData.b("其婸䉺䕼䵾뢀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᵸቺᱼቾ뢂", a_), ClipboardData.b("其婸䉺䕼䱾놀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其ᕸ᩺፼᡾몀", a_), ClipboardData.b("其婸䉺䵼佾낀뢂", a_));
		A_0 = A_0.Replace(ClipboardData.b("其୸᩺፼᡾몀", a_), ClipboardData.b("其婸䉺䵼佾뎀뢂", a_));
		return A_0;
	}

	// Token: 0x06003521 RID: 13601 RVA: 0x00311C8C File Offset: 0x00310C8C
	private void ᜈ(string A_0)
	{
		int a_ = 14;
		for (;;)
		{
			IL_09:
			switch (0)
			{
			default:
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_09;
					default:
					{
						if (false)
						{
						}
						Assembly executingAssembly = Assembly.GetExecutingAssembly();
						XmlSchema a_2 = null;
						A_0 = A_0.Replace(ClipboardData.b("剳ᡵ᩷ॹ౻䕽", a_), ClipboardData.b("ᩳᑵ୷੹䝻", a_));
						XHTMLValidationType xhtmlvalidateOption = this.ᜏ.Document.XHTMLValidateOption;
						int num = 3;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 2;
								continue;
							case 1:
								goto IL_C5;
							case 2:
								goto IL_C5;
							case 3:
								switch (xhtmlvalidateOption)
								{
								case XHTMLValidationType.Strict:
								{
									Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(ClipboardData.b("❳ٵᅷࡹ᥻偽쑿ꢅ\uda87ﾋ뒙풟쾡좣鞥薧\ud9a9\ud8ab\udcad\ud9af톱삳颵삷즹\ud8bb", a_));
									a_2 = XmlSchema.Read(manifestResourceStream, new ValidationEventHandler(this.ᜀ));
									num = 4;
									continue;
								}
								case XHTMLValidationType.Transitional:
									goto IL_10D;
								case XHTMLValidationType.None:
									goto IL_C5;
								default:
									num = 0;
									continue;
								}
								break;
							case 4:
								goto IL_C5;
							}
							break;
							IL_10D:
							if (true)
							{
							}
							Stream manifestResourceStream2 = executingAssembly.GetManifestResourceStream(ClipboardData.b("❳ٵᅷࡹ᥻偽쑿ꢅ\uda87ﾋ뒙풟쾡좣鞥薧\udea9\udeab쾭\udeaf솱\uddb3습톷햹튻\udfbd겿볃뗅곇", a_));
							a_2 = XmlSchema.Read(manifestResourceStream2, new ValidationEventHandler(this.ᜀ));
							num = 1;
							continue;
							try
							{
								IL_C5:
								this.ᜌ = new XmlDocument();
								this.ᜌ.PreserveWhitespace = true;
								this.ᜀ(A_0, a_2);
								return;
							}
							catch (XmlException ex)
							{
								throw new NotSupportedException(ClipboardData.b("౳ṵ౷᝹ၻ幽ﺉ겋ﲑ뒓벛ﲝ얟芡힣펥\ud8a7\udaa9쎫\udcad쒯ힱ킳隵\udab7쎹鲻낿ꯁ뛃ꏅ軉ꏋ귍ﳏ\ud8d1釓ꓕ꫗뗙껛", a_) + ex.Message, ex);
							}
							goto IL_10D;
						}
						break;
					}
					}
				}
				break;
			}
		}
	}

	// Token: 0x06003522 RID: 13602 RVA: 0x00311E44 File Offset: 0x00310E44
	private void ᜀ(string A_0, XmlSchema A_1)
	{
		int a_ = 2;
		switch (0)
		{
		default:
			for (;;)
			{
				A_0 = this.ᜏ(A_0);
				int num = 31;
				for (;;)
				{
					string text;
					switch (num)
					{
					case 0:
						A_0 = A_0.Remove(0, 1);
						num = 17;
						continue;
					case 1:
						A_0 = A_0.Remove(0, 1);
						num = 12;
						continue;
					case 2:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 1:
									num = 4;
									continue;
								case 2:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 1;
										continue;
									}
									XmlNode xmlNode = (XmlNode)enumerator.Current;
									xmlNode.ParentNode.RemoveChild(xmlNode);
									num = 0;
									continue;
								}
								case 4:
									goto IL_272;
								}
								IL_24C:
								num = 2;
								continue;
								goto IL_24C;
							}
							IL_272:
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
										goto IL_2BD;
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
										goto IL_2BF;
									}
									break;
								}
							}
							IL_2BD:
							IL_2BF:;
						}
						goto IL_2C0;
					case 3:
						if (text.StartsWith(ClipboardData.b("呧ɩᡫͭᱯ", a_)))
						{
							num = 25;
							continue;
						}
						goto IL_2C0;
					case 4:
						if (this.ᜌ != null)
						{
							num = 11;
							continue;
						}
						return;
					case 5:
						goto IL_45C;
					case 6:
						if (A_0.StartsWith(ClipboardData.b("呧啩ᑫͭᱯѱᅳѵ୷፹፻ၽ", a_)))
						{
							num = 24;
							continue;
						}
						goto IL_45C;
					case 7:
						goto IL_11C;
					case 8:
						if (A_0.StartsWith(ClipboardData.b("敧", a_)))
						{
							num = 1;
							continue;
						}
						goto IL_2F6;
					case 9:
						if (!text.StartsWith(ClipboardData.b("呧ɩᡫͭᱯ", a_)))
						{
							num = 13;
							continue;
						}
						goto IL_486;
					case 10:
						A_0 = A_0.Remove(0, 1);
						num = 5;
						continue;
					case 11:
					{
						XmlNodeList xmlNodeList = this.ᜌ.SelectNodes(ClipboardData.b("䝧䕩ཫŭᵯάᅳᡵ౷剹啻", a_));
						IEnumerator enumerator = xmlNodeList.GetEnumerator();
						num = 2;
						continue;
					}
					case 12:
						goto IL_2F6;
					case 13:
						num = 29;
						continue;
					case 14:
						if (A_0.StartsWith(ClipboardData.b("䡧", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_40E;
					case 15:
						goto IL_11C;
					case 16:
					{
						string str = ClipboardData.b("呧ɩᡫͭᱯ剱౳᭵ᑷᑹཻ䍽ꉿ", a_) + A_1.TargetNamespace + ClipboardData.b("䩧呩", a_);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2E5;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					}
					case 17:
						goto IL_40E;
					case 18:
						goto IL_183;
					case 19:
						if (A_0.StartsWith(ClipboardData.b("执", a_)))
						{
							num = 10;
							continue;
						}
						goto IL_45C;
					case 20:
						num = 32;
						continue;
					case 21:
						goto IL_486;
					case 22:
						num = 6;
						continue;
					case 23:
						A_0 = ClipboardData.b("呧ࡩͫ੭९䱱", a_) + A_0 + ClipboardData.b("呧䕩๫ŭᑯୱ䩳", a_);
						num = 21;
						continue;
					case 24:
						goto IL_5AF;
					case 25:
					{
						int num2 = text.IndexOf(ClipboardData.b("噧", a_));
						string str;
						A_0 = str + A_0.Remove(0, num2 + 1);
						if (true)
						{
						}
						num = 15;
						continue;
					}
					case 26:
						goto IL_183;
					case 27:
						if (!text.StartsWith(ClipboardData.b("呧䭩࡫ŭ፯ٱ൳ٵᵷ", a_)))
						{
							goto IL_2E5;
						}
						goto IL_11C;
					case 28:
					{
						string str;
						A_0 = str + ClipboardData.b("呧ɩ५཭ᑯ䱱䡳ɵᅷ๹ၻ᭽빿뺁ꮃﺉ꺏꺑뮓ﺕﶗﮙꂝ", a_) + A_0 + ClipboardData.b("呧䕩ѫᩭᵯṱ䩳", a_);
						num = 7;
						continue;
					}
					case 29:
						if (!text.StartsWith(ClipboardData.b("呧ࡩͫ੭९", a_)))
						{
							num = 20;
							continue;
						}
						goto IL_486;
					case 30:
						if (A_1 != null)
						{
							num = 16;
							continue;
						}
						num = 9;
						continue;
					case 31:
						if (!A_0.StartsWith(ClipboardData.b("呧啩ᑫͭᱯ剱ɳ፵੷ॹᕻᅽ", a_)))
						{
							num = 22;
							continue;
						}
						goto IL_5AF;
					case 32:
						if (!text.StartsWith(ClipboardData.b("呧䭩࡫ŭ፯ٱ൳ٵᵷ", a_)))
						{
							num = 23;
							continue;
						}
						goto IL_486;
					}
					break;
					IL_11C:
					A_0 = this.ᜐ(A_0);
					sprὂ.ᜀ(ref A_0);
					XmlValidatingReader xmlValidatingReader = new XmlValidatingReader(A_0, XmlNodeType.Document, new XmlParserContext(null, null, null, XmlSpace.None));
					xmlValidatingReader.Schemas.Add(A_1);
					this.ᜌ.Load(xmlValidatingReader);
					num = 26;
					continue;
					IL_183:
					num = 4;
					continue;
					IL_2C0:
					num = 27;
					continue;
					IL_2E5:
					num = 28;
					continue;
					IL_2F6:
					num = 19;
					continue;
					IL_40E:
					num = 8;
					continue;
					IL_45C:
					text = A_0.ToLower();
					num = 30;
					continue;
					IL_486:
					A_0 = this.ᜐ(A_0);
					A_0 = A_0.Replace(ClipboardData.b("乧ѩ๫ᵭo䥱", a_), string.Empty);
					this.ᜌ = sprὂ.ᜁ(A_0);
					num = 18;
					continue;
					IL_5AF:
					int num3 = A_0.IndexOf(ClipboardData.b("噧", a_));
					A_0 = A_0.Remove(0, num3 + 1);
					num = 14;
				}
			}
			return;
		}
	}

	// Token: 0x06003523 RID: 13603 RVA: 0x00312468 File Offset: 0x00311468
	private void ᜀ(XmlNodeList A_0)
	{
		int a_ = 10;
		if (true)
		{
		}
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			break;
		default:
			if (false)
			{
			}
			switch (0)
			{
			}
			break;
		}
		XmlNode a_2 = null;
		IEnumerator enumerator = A_0.GetEnumerator();
		try
		{
			int num = 0;
			for (;;)
			{
				XmlNode xmlNode;
				switch (num)
				{
				case 1:
					goto IL_1F6;
				case 2:
				{
					ITextRange a_3 = this.ᜋ().AppendText(ClipboardData.b("偯", a_));
					this.ᜁ(a_3);
					num = 11;
					continue;
				}
				case 3:
					num = 15;
					continue;
				case 4:
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						num = 9;
						continue;
					}
					num = 13;
					continue;
				case 5:
					goto IL_BA;
				case 6:
					goto IL_BA;
				case 7:
					if (!enumerator.MoveNext())
					{
						num = 12;
						continue;
					}
					xmlNode = (XmlNode)enumerator.Current;
					num = 14;
					continue;
				case 9:
					this.ᜎ(xmlNode);
					num = 5;
					continue;
				case 10:
					this.ᜀ(xmlNode, a_2);
					num = 6;
					continue;
				case 11:
					goto IL_BA;
				case 12:
					num = 1;
					continue;
				case 13:
					if (xmlNode.NodeType == XmlNodeType.Whitespace)
					{
						num = 3;
						continue;
					}
					goto IL_BA;
				case 14:
					if (xmlNode.NodeType == XmlNodeType.Text)
					{
						num = 10;
						continue;
					}
					num = 4;
					continue;
				case 15:
					if (xmlNode.Value == ClipboardData.b("偯", a_))
					{
						num = 2;
						continue;
					}
					goto IL_BA;
				}
				goto IL_A2;
				IL_BA:
				a_2 = xmlNode;
				num = 8;
				continue;
				IL_128:
				num = 7;
				continue;
				IL_A2:
				goto IL_128;
			}
			IL_1F6:;
		}
		finally
		{
			for (;;)
			{
				IDisposable disposable = enumerator as IDisposable;
				int num = 1;
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
							num = 0;
							continue;
						}
						goto IL_23F;
					case 2:
						goto IL_23D;
					}
					break;
				}
			}
			IL_23D:
			IL_23F:;
		}
	}

	// Token: 0x06003524 RID: 13604 RVA: 0x003126D4 File Offset: 0x003116D4
	private void ᜀ(XmlNode A_0, XmlNode A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num = 28;
			for (;;)
			{
				string text;
				string text2;
				switch (num)
				{
				case 0:
					this.ᜃ();
					num = 19;
					continue;
				case 1:
					if (true)
					{
					}
					text = this.ᜊ.Replace(text, ClipboardData.b("卲", a_));
					num = 12;
					continue;
				case 2:
				{
					if (A_0.PreviousSibling != null)
					{
						num = 24;
						continue;
					}
					ITextRange a_2 = this.ᜋ().AppendText(text);
					this.ᜁ(a_2);
					num = 9;
					continue;
				}
				case 3:
					goto IL_344;
				case 4:
					goto IL_386;
				case 5:
					if (this.\u1712(A_1))
					{
						num = 16;
						continue;
					}
					goto IL_386;
				case 6:
					goto IL_117;
				case 7:
					if (this.ᜑ(A_0.ParentNode))
					{
						num = 3;
						continue;
					}
					goto IL_117;
				case 8:
					return;
				case 9:
					goto IL_1EC;
				case 10:
					if (A_0.ParentNode.LocalName == ClipboardData.b("Ͳ", a_))
					{
						num = 34;
						continue;
					}
					goto IL_190;
				case 11:
					if (text != ClipboardData.b("卲", a_))
					{
						num = 1;
						continue;
					}
					goto IL_219;
				case 12:
					goto IL_219;
				case 13:
					if (A_0.ParentNode.LocalName == ClipboardData.b("ᅲᩴ፶x", a_))
					{
						num = 20;
						continue;
					}
					goto IL_4D9;
				case 14:
					num = 15;
					continue;
				case 15:
					if (A_0.ParentNode.LocalName.ToLower() == ClipboardData.b("rմᙶ᝸", a_))
					{
						num = 21;
						continue;
					}
					goto IL_117;
				case 16:
					this.ᜈ();
					num = 4;
					continue;
				case 17:
					goto IL_3CB;
				case 18:
					goto IL_190;
				case 19:
					goto IL_140;
				case 20:
					num = 2;
					continue;
				case 21:
					num = 7;
					continue;
				case 22:
					num = 27;
					continue;
				case 23:
					if (text2 != string.Empty)
					{
						num = 26;
						continue;
					}
					return;
				case 24:
					goto IL_4D9;
				case 25:
					if (this.\u1718)
					{
						num = 33;
						continue;
					}
					goto IL_190;
				case 26:
					this.ᜋ().AppendBookmarkEnd(text2);
					num = 8;
					continue;
				case 27:
					if (!(A_0.ParentNode.LocalName.ToLower() == ClipboardData.b("ᅲᥴᡶ᩸ၺ౼੾", a_)))
					{
						num = 14;
						continue;
					}
					goto IL_344;
				case 29:
					if (text2 != string.Empty)
					{
						num = 32;
						continue;
					}
					goto IL_3CB;
				case 30:
					goto IL_1EC;
				case 31:
					if (A_0.ParentNode.LocalName == ClipboardData.b("ᅲᩴ፶x", a_))
					{
						num = 0;
						continue;
					}
					goto IL_140;
				case 32:
					this.ᜋ().AppendBookmarkStart(text2);
					num = 17;
					continue;
				case 33:
					IL_459:
					this.ᜈ();
					this.\u1718 = true;
					num = 18;
					continue;
				case 34:
					num = 25;
					continue;
				}
				if (this.ᜪ)
				{
					num = 22;
					continue;
				}
				IL_117:
				num = 5;
				continue;
				IL_140:
				ITextRange a_3 = this.ᜋ().AppendText(text);
				this.ᜁ(a_3);
				num = 30;
				continue;
				IL_190:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_459;
				default:
					if (false)
					{
					}
					num = 31;
					continue;
				}
				IL_1EC:
				num = 23;
				continue;
				IL_219:
				text = text.Replace(ClipboardData.b("ᵲ᝴Ѷॸ䁺", a_), '\u00a0'.ToString());
				text = this.ᜀ(text, this.ᜋ());
				num = 13;
				continue;
				IL_344:
				this.ᜈ();
				this.ᜋ().Format.LeftIndent = this.ᜋ().Format.LeftIndent + (float)(this.ᜫ * 36);
				num = 6;
				continue;
				IL_386:
				text2 = this.ᜀ(A_0.ParentNode, ClipboardData.b("ᩲᅴ", a_));
				num = 29;
				continue;
				IL_3CB:
				text = A_0.InnerText.Replace('\n', ' ').Replace('\r', ' ');
				num = 11;
				continue;
				IL_4D9:
				num = 10;
			}
			return;
		}
		}
	}

	// Token: 0x06003525 RID: 13605 RVA: 0x00312BFC File Offset: 0x00311BFC
	private bool \u1712(XmlNode A_0)
	{
		int a_ = 2;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("౧ͩᩫ", a_)))
				{
					num = 12;
					continue;
				}
				return true;
			case 2:
				num = 26;
				continue;
			case 3:
				num = 20;
				continue;
			case 4:
				num = 19;
				continue;
			case 5:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("g孩", a_)))
				{
					num = 8;
					continue;
				}
				return true;
			case 6:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("ᡧ", a_)))
				{
					num = 14;
					continue;
				}
				return true;
			case 7:
				num = 5;
				continue;
			case 8:
				num = 27;
				continue;
			case 9:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("g奩", a_)))
				{
					goto IL_35B;
				}
				return true;
			case 10:
				goto IL_2EA;
			case 11:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("౧ṩ", a_)))
				{
					num = 25;
					continue;
				}
				return true;
			case 12:
				num = 6;
				continue;
			case 13:
				num = 21;
				continue;
			case 14:
				num = 15;
				continue;
			case 15:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("ᵧ٩", a_)))
				{
					num = 4;
					continue;
				}
				return true;
			case 16:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_35B;
				default:
					if (false)
					{
					}
					if (!(A_0.LocalName.ToLower() == ClipboardData.b("౧๩", a_)))
					{
						num = 7;
						continue;
					}
					return true;
				}
				break;
			case 17:
				num = 9;
				continue;
			case 18:
				num = 11;
				continue;
			case 19:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("ݧ٩", a_)))
				{
					num = 2;
					continue;
				}
				return true;
			case 20:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("g彩", a_)))
				{
					num = 13;
					continue;
				}
				return true;
			case 21:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("g屩", a_)))
				{
					num = 24;
					continue;
				}
				return true;
			case 22:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("g幩", a_)))
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				return true;
			case 23:
				num = 22;
				continue;
			case 24:
				num = 1;
				continue;
			case 25:
				num = 16;
				continue;
			case 26:
				if (A_0.LocalName.ToLower() == ClipboardData.b("ᱧ୩๫ɭᕯ", a_))
				{
					num = 10;
					continue;
				}
				return false;
			case 27:
				if (!(A_0.LocalName.ToLower() == ClipboardData.b("g塩", a_)))
				{
					num = 17;
					continue;
				}
				return true;
			}
			if (A_0 != null)
			{
				num = 18;
				continue;
			}
			return false;
			IL_35B:
			num = 23;
		}
		return true;
		IL_2EA:
		return true;
	}

	// Token: 0x06003526 RID: 13606 RVA: 0x00312FF8 File Offset: 0x00311FF8
	private bool ᜑ(XmlNode A_0)
	{
		int a_ = 5;
		int num = 4;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_DC;
			case 1:
				if (true)
				{
				}
				if (A_0.PreviousSibling.Name == ClipboardData.b("䡪Ṭٮᙰᵲᱴᅶၸ᡺ᱼᅾ꺂ﾊﲎ", a_))
				{
					num = 8;
					continue;
				}
				return false;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_EF;
				default:
					if (false)
					{
					}
					if (!(A_0.PreviousSibling.Name == ClipboardData.b("䡪ᩬݮᡰݲၴѶॸ᩺Ṽ᩾", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_6F;
				}
				break;
			case 3:
				goto IL_DC;
			case 5:
				return true;
			case 6:
				num = 1;
				continue;
			case 7:
				if (A_0.PreviousSibling == null)
				{
					goto IL_EF;
				}
				num = 2;
				continue;
			case 8:
				goto IL_6F;
			}
			if (A_0.ParentNode.LocalName.ToLower() == ClipboardData.b("४ŬnተᡲѴɶᙸེ᡼", a_))
			{
				num = 3;
				continue;
			}
			return false;
			IL_6F:
			A_0 = A_0.PreviousSibling;
			num = 0;
			continue;
			IL_DC:
			num = 7;
			continue;
			IL_EF:
			num = 5;
		}
		return true;
	}

	// Token: 0x06003527 RID: 13607 RVA: 0x0031314C File Offset: 0x0031214C
	private string ᜀ(string A_0, Paragraph A_1)
	{
		int a_ = 6;
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 9;
				continue;
			case 1:
				if (A_1.ChildObjects.LastItem != null)
				{
					num = 8;
					continue;
				}
				goto IL_147;
			case 2:
				return A_0;
			case 3:
				num = 1;
				continue;
			case 4:
				return A_0;
			case 5:
				if (A_1.ChildObjects.LastItem.DocumentObjectType == DocumentObjectType.Break)
				{
					num = 11;
					continue;
				}
				goto IL_147;
			case 7:
				if (!(A_1.Text == ""))
				{
					num = 0;
					continue;
				}
				goto IL_FD;
			case 8:
				num = 5;
				continue;
			case 9:
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
					if (A_1.Text == null)
					{
						num = 10;
						continue;
					}
					return A_0;
				}
				break;
			case 10:
				goto IL_FD;
			case 11:
				A_0 = A_0.TrimStart(new char[0]);
				num = 4;
				continue;
			}
			if (A_0.StartsWith(ClipboardData.b("䱫", a_)))
			{
				num = 3;
				continue;
			}
			break;
			IL_FD:
			A_0 = A_0.TrimStart(new char[0]);
			num = 2;
			continue;
			IL_147:
			num = 7;
		}
		return A_0;
	}

	// Token: 0x06003528 RID: 13608 RVA: 0x003132D0 File Offset: 0x003122D0
	private void ᜈ()
	{
		for (;;)
		{
			this.\u1712 = new Paragraph(this.ᜏ.Document);
			this.ᜏ.Add(this.\u1712);
			this.\u1712.Format.BeforeSpacing = 0f;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜤ != null)
					{
						num = 1;
						continue;
					}
					return;
				case 1:
					for (;;)
					{
						ParagraphFormat format = this.\u1712.Format;
						format.BackColor = this.ᜤ.\u170D();
						format.LeftIndent = this.ᜤ.ᜎ();
						format.HorizontalAlignment = this.ᜤ.ᜌ();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_CB;
						}
					}
					IL_CB:
					if (false)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
				case 3:
					if (this.ᜥ)
					{
						num = 4;
						continue;
					}
					return;
				case 4:
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
		}
	}

	// Token: 0x06003529 RID: 13609 RVA: 0x003133F0 File Offset: 0x003123F0
	private void ᜐ(XmlNode A_0)
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
		this.ᜇ(A_0);
		this.ᜀ(A_0.ChildNodes);
		this.ᜃ();
	}

	// Token: 0x0600352A RID: 13610 RVA: 0x00313444 File Offset: 0x00312444
	private bool ᜏ(XmlNode A_0)
	{
		int a_ = 5;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				return true;
			case 1:
				if (A_0.PreviousSibling == null)
				{
					num = 6;
					continue;
				}
				num = 3;
				continue;
			case 2:
				goto IL_96;
			case 3:
				if (A_0.PreviousSibling.Name == ClipboardData.b("䡪ᩬݮᡰݲၴѶॸ᩺Ṽ᩾", a_))
				{
					num = 4;
					continue;
				}
				return false;
			case 4:
				A_0 = A_0.PreviousSibling;
				num = 8;
				continue;
			case 6:
				return true;
			case 7:
				if (!(A_0.ParentNode.Name.ToLower() == ClipboardData.b("ݪѬ", a_)))
				{
					return false;
				}
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
					continue;
				}
				break;
			case 8:
				goto IL_96;
			}
			IL_3D:
			if (true)
			{
			}
			if (A_0.ParentNode.Name.ToLower() == ClipboardData.b("Ὢ६", a_))
			{
				num = 2;
				continue;
			}
			num = 7;
			continue;
			goto IL_3D;
			IL_96:
			num = 1;
		}
		return false;
	}

	// Token: 0x0600352B RID: 13611 RVA: 0x003135A0 File Offset: 0x003125A0
	private void ᜎ(XmlNode A_0)
	{
		int a_ = 6;
		switch (0)
		{
		default:
		{
			for (;;)
			{
				if (true)
				{
				}
				string text = A_0.Name.ToLower();
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						spr᧓.\u17C8 = new Dictionary<string, int>(20)
						{
							{
								ClipboardData.b("࡫ݭɯ", a_),
								0
							},
							{
								ClipboardData.b("๫ŭᑯୱ", a_),
								1
							},
							{
								ClipboardData.b("ᱫ", a_),
								2
							},
							{
								ClipboardData.b("kݭ", a_),
								3
							},
							{
								ClipboardData.b("࡫ᩭ", a_),
								4
							},
							{
								ClipboardData.b("࡫੭", a_),
								5
							},
							{
								ClipboardData.b("k٭", a_),
								6
							},
							{
								ClipboardData.b("࡫ݭٯ", a_),
								7
							},
							{
								ClipboardData.b("ѫ彭", a_),
								8
							},
							{
								ClipboardData.b("ѫ屭", a_),
								9
							},
							{
								ClipboardData.b("ѫ嵭", a_),
								10
							},
							{
								ClipboardData.b("ѫ婭", a_),
								11
							},
							{
								ClipboardData.b("ѫ孭", a_),
								12
							},
							{
								ClipboardData.b("ѫ塭", a_),
								13
							},
							{
								ClipboardData.b("ѫ奭", a_),
								14
							},
							{
								ClipboardData.b("ᡫ཭ቯṱᅳ", a_),
								15
							},
							{
								ClipboardData.b("իͭᝯ", a_),
								16
							},
							{
								ClipboardData.b("൫", a_),
								17
							},
							{
								ClipboardData.b("๫ᱭ", a_),
								18
							},
							{
								ClipboardData.b("๫ɭὯᅱέݵ൷ᕹࡻ᭽", a_),
								19
							}
						};
						num = 2;
						continue;
					case 1:
						num = 12;
						continue;
					case 2:
						goto IL_EE;
					case 3:
						goto IL_1FF;
					case 4:
						num = 6;
						continue;
					case 5:
					{
						string key;
						if ((key = text) != null)
						{
							num = 4;
							continue;
						}
						goto IL_1EC;
					}
					case 6:
						if (spr᧓.\u17C8 == null)
						{
							num = 0;
							continue;
						}
						goto IL_EE;
					case 7:
						if (!(A_0.ParentNode.Name.ToLower() == ClipboardData.b("ᡫ੭", a_)))
						{
							goto IL_2DC;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D7;
						default:
							if (false)
							{
							}
							num = 13;
							continue;
						}
						break;
					case 8:
						goto IL_1EC;
					case 9:
						goto IL_31C;
					case 10:
					{
						string key;
						int num2;
						if (spr᧓.\u17C8.TryGetValue(key, out num2))
						{
							num = 1;
							continue;
						}
						goto IL_1EC;
					}
					case 11:
						num = 8;
						continue;
					case 12:
					{
						int num2;
						switch (num2)
						{
						case 0:
						case 1:
							goto IL_25B;
						case 2:
							num = 7;
							continue;
						case 3:
						case 4:
						case 5:
						case 6:
							goto IL_350;
						case 7:
							goto IL_370;
						case 8:
							goto IL_153;
						case 9:
							goto IL_AD;
						case 10:
							goto IL_5B3;
						case 11:
							goto IL_38A;
						case 12:
							goto IL_120;
						case 13:
							goto IL_94;
						case 14:
							goto IL_337;
						case 15:
							goto IL_59F;
						case 16:
							goto IL_D7;
						case 17:
							goto IL_1CD;
						case 18:
							goto IL_321;
						case 19:
							goto IL_139;
						default:
							num = 11;
							continue;
						}
						break;
					}
					case 13:
					{
						bool a_2 = this.ᜅ(A_0);
						this.ᜐ(A_0);
						this.ᜀ(a_2);
						this.\u1712 = null;
						num = 15;
						continue;
					}
					case 14:
						if (A_0.ParentNode.Name.ToLower() != ClipboardData.b("ᡫ੭", a_))
						{
							num = 9;
							continue;
						}
						return;
					case 15:
						goto IL_2DC;
					}
					break;
					IL_EE:
					num = 10;
					continue;
					IL_1EC:
					this.ᜉ(A_0);
					num = 3;
					continue;
					IL_2DC:
					num = 14;
				}
			}
			IL_94:
			spr\u1DE8.ᜂ ᜂ = this.ᜆ(A_0);
			this.ᜀ(ᜂ, 7f, BuiltinStyle.Heading6, A_0, true, false);
			return;
			IL_AD:
			ᜂ = this.ᜆ(A_0);
			this.ᜀ(ᜂ, 18f, BuiltinStyle.Heading2, A_0, true, false);
			return;
			IL_D7:
			ᜂ = this.ᜆ(A_0);
			this.ᜊ(A_0);
			this.ᜀ(true);
			return;
			IL_120:
			ᜂ = this.ᜆ(A_0);
			this.ᜀ(ᜂ, 10f, BuiltinStyle.Heading5, A_0, true, false);
			return;
			IL_139:
			this.\u170D(A_0);
			this.ᜀ(A_0.ChildNodes);
			this.ᜇ();
			return;
			IL_153:
			ᜂ = this.ᜆ(A_0);
			this.ᜀ(ᜂ, 24f, BuiltinStyle.Heading1, A_0, true, false);
			return;
			IL_1CD:
			ᜂ = this.ᜆ(A_0);
			ᜂ.\u1714 = BuiltinStyle.Hyperlink;
			this.ᜋ(A_0);
			this.ᜀ(true);
			return;
			IL_1FF:
			return;
			IL_25B:
			this.ᜀ(A_0.ChildNodes);
			return;
			IL_31C:
			bool a_3 = this.ᜅ(A_0);
			this.ᜈ();
			this.ᜐ(A_0);
			this.ᜀ(a_3);
			this.\u1712 = null;
			return;
			IL_321:
			this.ᜆ(A_0);
			this.ᜋ().AppendBreak(BreakType.LineBreak);
			return;
			IL_337:
			ᜂ = this.ᜆ(A_0);
			this.ᜀ(ᜂ, 12f, BuiltinStyle.Heading7, A_0, false, false);
			return;
			IL_350:
			bool a_4 = this.ᜅ(A_0);
			this.ᜈ(A_0);
			this.ᜀ(a_4);
			this.\u1712 = null;
			return;
			IL_370:
			this.ᜌ(A_0);
			this.ᜀ(A_0.ChildNodes);
			this.ᜆ();
			return;
			IL_38A:
			ᜂ = this.ᜆ(A_0);
			this.ᜀ(ᜂ, 12f, BuiltinStyle.Heading4, A_0, true, false);
			return;
			IL_59F:
			this.ᜄ();
			this.ᜀ(A_0);
			this.ᜅ();
			return;
			IL_5B3:
			ᜂ = this.ᜆ(A_0);
			this.ᜀ(ᜂ, 13f, BuiltinStyle.Heading3, A_0, true, false);
			return;
		}
		}
	}

	// Token: 0x0600352C RID: 13612 RVA: 0x00313B7C File Offset: 0x00312B7C
	private void ᜀ(spr\u1DE8.ᜂ A_0, float A_1, BuiltinStyle A_2, XmlNode A_3, bool A_4, bool A_5)
	{
		for (;;)
		{
			A_0.ᜄ(A_4);
			A_0.ᜂ(A_5);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_7B;
				case 1:
					if (A_1 > 0f)
					{
						num = 2;
						continue;
					}
					goto IL_7D;
				case 2:
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
						A_0.ᜁ(A_1);
						num = 0;
						continue;
					}
					break;
				}
				break;
			}
		}
		IL_7B:
		IL_7D:
		A_0.\u1714 = A_2;
		this.ᜈ(A_3);
		this.ᜀ(true);
		this.\u1712 = null;
	}

	// Token: 0x0600352D RID: 13613 RVA: 0x00313C24 File Offset: 0x00312C24
	private void \u170D(XmlNode A_0)
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
		this.ᜪ = true;
		this.ᜫ++;
		this.ᜌ(A_0);
	}

	// Token: 0x0600352E RID: 13614 RVA: 0x00313C7C File Offset: 0x00312C7C
	private void ᜇ()
	{
		for (;;)
		{
			IL_14:
			this.ᜫ--;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜫ == 0)
					{
						num = 2;
						continue;
					}
					goto IL_7B;
				case 1:
					goto IL_79;
				case 2:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_14;
					}
					if (false)
					{
					}
					this.ᜪ = false;
					num = 1;
					continue;
				}
				break;
			}
		}
		IL_79:
		IL_7B:
		this.ᜆ();
	}

	// Token: 0x0600352F RID: 13615 RVA: 0x00313D0C File Offset: 0x00312D0C
	private void ᜌ(XmlNode A_0)
	{
		int a_ = 7;
		int num = 15;
		HorizontalAlignment horizontalAlignment;
		for (;;)
		{
			bool flag;
			switch (num)
			{
			case 0:
				goto IL_97;
			case 1:
				if (this.ᜤ == null)
				{
					num = 6;
					continue;
				}
				return;
			case 2:
				goto IL_1AE;
			case 3:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_197;
				default:
					if (false)
					{
					}
					this.ᜤ = new spr\u1DE8.ᜂ();
					num = 14;
					continue;
				}
				break;
			case 4:
				num = 5;
				continue;
			case 5:
				if (!this.ᜤ.ᜀ(10))
				{
					num = 7;
					continue;
				}
				goto IL_171;
			case 6:
				num = 10;
				continue;
			case 7:
				goto IL_222;
			case 8:
				goto IL_1B0;
			case 9:
				if (this.ᜤ != null)
				{
					num = 4;
					continue;
				}
				goto IL_171;
			case 10:
				if (horizontalAlignment != HorizontalAlignment.Left)
				{
					num = 3;
					continue;
				}
				return;
			case 11:
				this.ᜤ = this.\u170D.Peek();
				num = 8;
				continue;
			case 12:
				goto IL_197;
			case 13:
				this.ᜦ.Push(this.ᜤ);
				this.\u1712 = null;
				if (true)
				{
				}
				num = 0;
				continue;
			case 14:
				if (!this.ᜤ.ᜀ(10))
				{
					num = 12;
					continue;
				}
				return;
			case 16:
				if (flag)
				{
					num = 11;
					continue;
				}
				goto IL_1B0;
			}
			if (this.ᜥ)
			{
				num = 13;
				continue;
			}
			IL_97:
			this.ᜥ = true;
			horizontalAlignment = this.ᜀ(this.ᜀ(A_0, ClipboardData.b("౬ͮᡰᑲ᭴", a_)), HorizontalAlignment.Left);
			flag = false;
			flag = this.ᜅ(A_0);
			num = 16;
			continue;
			IL_171:
			num = 1;
			continue;
			IL_197:
			this.ᜤ.ᜀ(horizontalAlignment);
			num = 2;
			continue;
			IL_1B0:
			this.ᜎ.Push(flag);
			num = 9;
		}
		IL_1AE:
		return;
		IL_222:
		this.ᜤ.ᜀ(horizontalAlignment);
	}

	// Token: 0x06003530 RID: 13616 RVA: 0x00313F40 File Offset: 0x00312F40
	private void ᜆ()
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_9D;
			case 1:
				this.ᜥ = (this.ᜦ.Count > 0);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_9D;
				default:
					if (false)
					{
					}
					num = 0;
					continue;
				}
				break;
			case 2:
				this.\u170D.Pop();
				num = 4;
				continue;
			case 4:
				goto IL_CE;
			}
			if (true)
			{
			}
			this.ᜤ = ((this.ᜦ.Count > 0) ? this.ᜦ.Pop() : null);
			num = 1;
			continue;
			IL_9D:
			if (!this.ᜎ.Pop())
			{
				break;
			}
			num = 2;
		}
		IL_CE:
		this.\u1712 = null;
	}

	// Token: 0x06003531 RID: 13617 RVA: 0x00314030 File Offset: 0x00313030
	private void ᜅ()
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
		this.\u1713 = this.ᜑ.Pop();
		this.ᜏ = this.ᜐ.Pop();
		this.ᜢ = this.ᜧ.Pop();
		this.\u1712 = null;
	}

	// Token: 0x06003532 RID: 13618 RVA: 0x003140A8 File Offset: 0x003130A8
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
		this.ᜐ.Push(this.ᜏ);
		this.ᜑ.Push(this.\u1713);
		this.ᜧ.Push(this.ᜢ);
	}

	// Token: 0x06003533 RID: 13619 RVA: 0x00314118 File Offset: 0x00313118
	private void ᜋ(XmlNode A_0)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			HyperlinkType a_2;
			string text;
			string text4;
			DocPicture docPicture;
			for (;;)
			{
				bool flag = false;
				a_2 = HyperlinkType.None;
				text = null;
				string text2 = null;
				string text3 = null;
				text4 = this.ᜀ(A_0, ClipboardData.b("๥ᩧཀྵ੫", a_));
				this.ᜀ(A_0, ClipboardData.b("ብ१ᡩ୫୭ѯ", a_));
				docPicture = new DocPicture(this.ᜋ().Document);
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (text != null)
						{
							num = 14;
							continue;
						}
						goto IL_577;
					case 1:
						text = A_0.InnerText;
						num = 0;
						continue;
					case 2:
						goto IL_127;
					case 3:
						a_2 = HyperlinkType.Bookmark;
						text4 = text4.Replace(ClipboardData.b("䕥", a_), string.Empty);
						num = 2;
						continue;
					case 4:
						if (!flag)
						{
							num = 1;
							continue;
						}
						goto IL_577;
					case 5:
						goto IL_4AC;
					case 6:
						if (A_0.HasChildNodes)
						{
							num = 15;
							continue;
						}
						goto IL_48C;
					case 7:
						if (!text4.StartsWith(ClipboardData.b("๥ᱧṩᱫ", a_)))
						{
							num = 17;
							continue;
						}
						goto IL_4AC;
					case 8:
						text = this.ᜁ(text);
						num = 10;
						continue;
					case 9:
						goto IL_46E;
					case 10:
						goto IL_577;
					case 11:
						goto IL_4BA;
					case 12:
						if (text4.StartsWith(ClipboardData.b("䕥", a_)))
						{
							num = 3;
							continue;
						}
						goto IL_408;
					case 13:
						a_2 = HyperlinkType.EMailLink;
						num = 16;
						continue;
					case 14:
						num = 18;
						continue;
					case 15:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_F8;
						default:
						{
							if (false)
							{
							}
							IEnumerator enumerator = A_0.ChildNodes.GetEnumerator();
							num = 21;
							continue;
						}
						}
						break;
					case 16:
						goto IL_F8;
					case 17:
						num = 19;
						continue;
					case 18:
						if (text.Length > 0)
						{
							num = 8;
							continue;
						}
						goto IL_577;
					case 19:
						if (text4.StartsWith(ClipboardData.b("ᅥὧᵩ", a_)))
						{
							num = 5;
							continue;
						}
						a_2 = HyperlinkType.FileLink;
						text4 = this.\u170D() + text4;
						num = 9;
						continue;
					case 20:
						if (text4.StartsWith(ClipboardData.b("୥१ͩkᩭὯ䡱", a_)))
						{
							num = 13;
							continue;
						}
						num = 7;
						continue;
					case 21:
						try
						{
							num = 7;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (File.Exists(text2))
									{
										num = 13;
										continue;
									}
									num = 17;
									continue;
								case 1:
									goto IL_1DE;
								case 2:
									goto IL_3AE;
								case 3:
									goto IL_1DE;
								case 4:
								{
									IEnumerator enumerator;
									if (!enumerator.MoveNext())
									{
										num = 18;
										continue;
									}
									XmlNode xmlNode = (XmlNode)enumerator.Current;
									num = 14;
									continue;
								}
								case 5:
									if (text3.Length > 0)
									{
										num = 8;
										continue;
									}
									goto IL_1EE;
								case 6:
								{
									XmlNode xmlNode;
									text2 = this.ᜀ(xmlNode, ClipboardData.b("ᕥᩧ३", a_));
									text3 = this.ᜀ(xmlNode, ClipboardData.b("ݥѧṩ", a_));
									num = 0;
									continue;
								}
								case 8:
									text = this.ᜁ(text3);
									num = 1;
									continue;
								case 9:
									if (text3 != null)
									{
										num = 11;
										continue;
									}
									goto IL_1EE;
								case 10:
									goto IL_1DE;
								case 11:
									num = 5;
									continue;
								case 12:
									goto IL_3BA;
								case 13:
									docPicture.LoadImage(Image.FromFile(text2));
									num = 10;
									continue;
								case 14:
								{
									XmlNode xmlNode;
									if (xmlNode.LocalName.ToLower() == ClipboardData.b("ཥէ൩", a_))
									{
										num = 6;
										continue;
									}
									break;
								}
								case 15:
									goto IL_1DE;
								case 16:
									docPicture.LoadImage(Image.FromFile(this.\u170D() + text2));
									num = 15;
									continue;
								case 17:
									if (File.Exists(this.\u170D() + text2))
									{
										num = 16;
										continue;
									}
									num = 9;
									continue;
								case 18:
									goto IL_3AE;
								}
								goto IL_1C1;
								IL_1DE:
								flag = true;
								num = 2;
								continue;
								IL_1EE:
								Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ClipboardData.b("㕥ᡧͩṫ୭幯㙱᭳ᕵ噷⡹᥻ൽ黎ꊋﺍ憐ﾙ쎛쾟횡슣즥\udda7쒩좫肭\udaaf슱펳", a_));
								docPicture.LoadImage(Image.FromStream(manifestResourceStream));
								num = 3;
								continue;
								IL_34A:
								num = 4;
								continue;
								IL_1C1:
								goto IL_34A;
								IL_3AE:
								num = 12;
							}
							IL_3BA:
							goto IL_48C;
						}
						finally
						{
							for (;;)
							{
								IEnumerator enumerator;
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_405;
									case 1:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_407;
									case 2:
										disposable.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_405:
							IL_407:;
						}
						goto IL_408;
					}
					break;
					IL_408:
					if (true)
					{
					}
					num = 20;
					continue;
					IL_48C:
					num = 4;
					continue;
					IL_4AC:
					a_2 = HyperlinkType.WebLink;
					num = 11;
					continue;
					IL_577:
					num = 12;
				}
			}
			IL_F8:
			IL_127:
			IL_46E:
			IL_4BA:
			this.ᜋ().ᜀ(text4, text, docPicture, a_2);
			return;
		}
		}
	}

	// Token: 0x06003534 RID: 13620 RVA: 0x00314704 File Offset: 0x00313704
	private void ᜀ(XmlNode A_0, IPicture A_1)
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = false;
				bool flag2 = false;
				IEnumerator enumerator = A_0.Attributes.GetEnumerator();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (true)
						{
						}
						if (!flag2)
						{
							num = 6;
							continue;
						}
						goto IL_68;
					case 2:
						try
						{
							num = 4;
							for (;;)
							{
								int num2;
								switch (num)
								{
								case 0:
									goto IL_16C;
								case 1:
									num = 16;
									continue;
								case 3:
								{
									string a;
									if (!(a == ClipboardData.b("๥൧ͩ୫٭ѯ", a_)))
									{
										num = 13;
										continue;
									}
									string a_2;
									A_1.Height = Convert.ToSingle(this.ᜑ(a_2));
									flag = true;
									num = 0;
									continue;
								}
								case 5:
									num = 17;
									continue;
								case 6:
									num = 27;
									continue;
								case 7:
									num = 26;
									continue;
								case 8:
									goto IL_16C;
								case 9:
								{
									if (!enumerator.MoveNext())
									{
										num = 7;
										continue;
									}
									XmlAttribute xmlAttribute = (XmlAttribute)enumerator.Current;
									num = 11;
									continue;
								}
								case 10:
									goto IL_2BA;
								case 11:
								{
									XmlAttribute xmlAttribute;
									string a2;
									if ((a2 = xmlAttribute.Name.ToLower()) != null)
									{
										num = 21;
										continue;
									}
									break;
								}
								case 12:
								{
									string a2;
									if (!(a2 == ClipboardData.b("๥൧ͩ୫٭ѯ", a_)))
									{
										num = 1;
										continue;
									}
									XmlAttribute xmlAttribute;
									A_1.Height = Convert.ToSingle(this.ᜑ(xmlAttribute.Value));
									flag = true;
									num = 2;
									continue;
								}
								case 13:
									num = 14;
									continue;
								case 14:
								{
									string a;
									if (!(a == ClipboardData.b("ᅥŧ๩ᡫ٭", a_)))
									{
										num = 5;
										continue;
									}
									string a_2;
									A_1.Width = Convert.ToSingle(this.ᜑ(a_2));
									flag2 = true;
									num = 8;
									continue;
								}
								case 15:
								{
									int num3;
									if (num2 >= num3 - 1)
									{
										num = 24;
										continue;
									}
									string[] array;
									string text = array[num2].ToLower().Trim();
									string a_2 = array[num2 + 1].ToLower().Trim();
									num = 22;
									continue;
								}
								case 16:
								{
									string a2;
									if (!(a2 == ClipboardData.b("ᅥŧ๩ᡫ٭", a_)))
									{
										num = 20;
										continue;
									}
									XmlAttribute xmlAttribute;
									A_1.Width = Convert.ToSingle(this.ᜑ(xmlAttribute.Value));
									flag2 = true;
									num = 18;
									continue;
								}
								case 17:
									goto IL_16C;
								case 19:
								{
									string a2;
									if (!(a2 == ClipboardData.b("ᕥᱧ፩k୭", a_)))
									{
										num = 6;
										continue;
									}
									XmlAttribute xmlAttribute;
									string value = xmlAttribute.Value;
									string[] array = value.Split(new char[]
									{
										';',
										':'
									});
									num2 = 0;
									int num3 = array.Length;
									num = 23;
									continue;
								}
								case 20:
									num = 19;
									continue;
								case 21:
									num = 12;
									continue;
								case 22:
								{
									string a;
									string text;
									if ((a = text) != null)
									{
										num = 25;
										continue;
									}
									goto IL_16C;
								}
								case 23:
									goto IL_2BA;
								case 25:
									num = 3;
									continue;
								case 26:
									goto IL_45C;
								}
								goto IL_156;
								IL_16C:
								num2 += 2;
								num = 10;
								continue;
								IL_183:
								num = 9;
								continue;
								IL_156:
								goto IL_183;
								IL_2BA:
								num = 15;
							}
							IL_45C:
							goto IL_520;
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
										goto IL_4A9;
									case 1:
										goto IL_4A7;
									case 2:
										disposable.Dispose();
										num = 1;
										continue;
									}
									break;
								}
							}
							IL_4A7:
							IL_4A9:;
						}
						goto IL_4AA;
						IL_520:
						num = 1;
						continue;
					case 3:
						goto IL_504;
					case 4:
						goto IL_68;
					case 5:
						if (!flag)
						{
							num = 10;
							continue;
						}
						return;
					case 6:
						num = 3;
						continue;
					case 7:
						if (A_1.Image == null)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_504;
						default:
							if (false)
							{
							}
							num = 8;
							continue;
						}
						break;
					case 8:
						A_1.Height = (float)A_1.Image.Height * 0.75f;
						num = 0;
						continue;
					case 9:
						A_1.Width = (float)A_1.Image.Width * 0.75f;
						num = 4;
						continue;
					case 10:
						goto IL_4AA;
					}
					break;
					IL_68:
					num = 5;
					continue;
					IL_4AA:
					num = 7;
					continue;
					IL_504:
					if (A_1.Image == null)
					{
						goto IL_68;
					}
					num = 9;
				}
			}
			return;
		}
	}

	// Token: 0x06003535 RID: 13621 RVA: 0x00314C78 File Offset: 0x00313C78
	private void ᜊ(XmlNode A_0)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			IPicture picture;
			for (;;)
			{
				string text = this.ᜀ(A_0, ClipboardData.b("Ṭᵮተ", a_));
				picture = new DocPicture(this.ᜏ.Document);
				int num = 17;
				for (;;)
				{
					switch (num)
					{
					case 0:
						try
						{
							WebClient webClient;
							Stream stream = webClient.OpenRead(text);
							try
							{
								picture = this.ᜋ().AppendPicture(Image.FromStream(stream));
							}
							finally
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_1A1;
									case 1:
										((IDisposable)stream).Dispose();
										num = 0;
										continue;
									}
									if (stream == null)
									{
										break;
									}
									num = 1;
								}
								IL_1A1:;
							}
							goto IL_116;
						}
						finally
						{
							num = 0;
							for (;;)
							{
								WebClient webClient;
								switch (num)
								{
								case 1:
									((IDisposable)webClient).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_1E4;
								}
								if (webClient == null)
								{
									break;
								}
								num = 1;
							}
							IL_1E4:;
						}
						goto IL_1E7;
					case 1:
						goto IL_116;
					case 2:
						try
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
								WebClient webClient2;
								Uri address;
								Stream stream2 = webClient2.OpenRead(address);
								try
								{
									picture = this.ᜋ().AppendPicture(Image.FromStream(stream2));
								}
								finally
								{
									num = 2;
									for (;;)
									{
										switch (num)
										{
										case 0:
											((IDisposable)stream2).Dispose();
											num = 1;
											continue;
										case 1:
											goto IL_3BB;
										}
										if (stream2 == null)
										{
											break;
										}
										num = 0;
									}
									IL_3BB:;
								}
								break;
							}
							}
							goto IL_116;
						}
						finally
						{
							num = 1;
							for (;;)
							{
								WebClient webClient2;
								switch (num)
								{
								case 0:
									((IDisposable)webClient2).Dispose();
									num = 2;
									continue;
								case 2:
									goto IL_400;
								}
								if (webClient2 == null)
								{
									break;
								}
								num = 0;
							}
							IL_400:;
						}
						goto IL_403;
					case 3:
					{
						WebClient webClient = new WebClient();
						num = 0;
						continue;
					}
					case 4:
					{
						TableCell tableCell = this.ᜋ().Owner as TableCell;
						TableRow ownerRow = tableCell.OwnerRow;
						num = 12;
						continue;
					}
					case 5:
					{
						if (File.Exists(this.\u170D() + ClipboardData.b("ㅬ", a_) + text))
						{
							num = 9;
							continue;
						}
						Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(ClipboardData.b("㹬ὮᡰŲၴ奶㵸ᑺṼ兾펀ﲈ力붒ﺖ滛쒠ﲢ쮤좦\udda8춪슬\udaae\udfb0ힲ鮴\uddb6즸\udcba", a_));
						picture = this.ᜋ().AppendPicture(Image.FromStream(manifestResourceStream));
						num = 14;
						continue;
					}
					case 6:
						picture = this.ᜋ().AppendPicture(Image.FromFile(text));
						num = 11;
						continue;
					case 7:
						if (this.ᜋ().Owner is TableCell)
						{
							num = 4;
							continue;
						}
						goto IL_42B;
					case 8:
					{
						Uri address = new Uri(this.\u1716, text);
						WebClient webClient2 = new WebClient();
						num = 2;
						continue;
					}
					case 9:
						picture = this.ᜋ().AppendPicture(Image.FromFile(this.\u170D() + ClipboardData.b("ㅬ", a_) + text));
						num = 1;
						continue;
					case 10:
						goto IL_241;
					case 11:
						goto IL_116;
					case 12:
					{
						TableRow ownerRow;
						if (ownerRow.Height < picture.Height)
						{
							num = 16;
							continue;
						}
						goto IL_42B;
					}
					case 13:
						if (this.\u1716 != null)
						{
							num = 8;
							continue;
						}
						goto IL_403;
					case 14:
						goto IL_116;
					case 15:
						if (File.Exists(text))
						{
							num = 6;
							continue;
						}
						goto IL_1E7;
					case 16:
					{
						TableRow ownerRow;
						ownerRow.Height = picture.Height;
						num = 10;
						continue;
					}
					case 17:
						if (Uri.IsWellFormedUriString(text, UriKind.Absolute))
						{
							if (true)
							{
							}
							num = 3;
							continue;
						}
						num = 13;
						continue;
					}
					break;
					IL_116:
					num = 7;
					continue;
					IL_1E7:
					num = 5;
					continue;
					IL_403:
					num = 15;
				}
			}
			IL_241:
			IL_42B:
			this.ᜀ(A_0, picture);
			return;
		}
		}
	}

	// Token: 0x06003536 RID: 13622 RVA: 0x003150EC File Offset: 0x003140EC
	private void ᜉ(XmlNode A_0)
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				string text = A_0.Name.ToLower();
				int num = 1;
				for (;;)
				{
					spr\u1DE8.ᜂ ᜂ;
					string text4;
					switch (num)
					{
					case 0:
						goto IL_26B;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							if (text != ClipboardData.b("ᕥ୧ᡩիṭѯ", a_))
							{
								num = 4;
								continue;
							}
							return;
						}
						break;
					case 2:
						goto IL_26B;
					case 3:
						return;
					case 4:
						ᜂ = this.ᜆ(A_0);
						num = 21;
						continue;
					case 5:
						goto IL_26B;
					case 6:
						ᜂ.ᜁ(10f);
						num = 18;
						continue;
					case 7:
						goto IL_26B;
					case 8:
					{
						string text2;
						ᜂ.ᜀ(spr᱈.ᜀ(text2));
						num = 32;
						continue;
					}
					case 9:
					{
						string key;
						int num2;
						if (spr᧓.\u17C9.TryGetValue(key, out num2))
						{
							num = 28;
							continue;
						}
						goto IL_26B;
					}
					case 10:
						goto IL_26B;
					case 11:
						goto IL_188;
					case 12:
						goto IL_26B;
					case 13:
						goto IL_26B;
					case 14:
						goto IL_26B;
					case 15:
						num = 23;
						continue;
					case 16:
						if (ᜂ.ᜁ() < 0f)
						{
							num = 6;
							continue;
						}
						goto IL_3EB;
					case 17:
					{
						int num2;
						switch (num2)
						{
						case 0:
						case 1:
							ᜂ.ᜄ(true);
							num = 2;
							continue;
						case 2:
						case 3:
						case 4:
						case 5:
						case 6:
							ᜂ.ᜂ(true);
							num = 12;
							continue;
						case 7:
							ᜂ.ᜀ(true);
							num = 5;
							continue;
						case 8:
						case 9:
							ᜂ.ᜃ(true);
							num = 19;
							continue;
						case 10:
							num = 16;
							continue;
						case 11:
						{
							spr\u1DE8.ᜂ ᜂ2 = ᜂ;
							ᜂ2.ᜁ(ᜂ2.ᜁ() + 2f);
							num = 25;
							continue;
						}
						case 12:
						case 13:
						case 14:
						case 15:
							ᜂ.ᜀ(ClipboardData.b("╥ݧὩṫݭᕯq味㡵ᵷ൹", a_));
							ᜂ.ᜁ(10f);
							num = 31;
							continue;
						case 16:
						{
							string text2 = this.ᜀ(A_0, ClipboardData.b("եݧ٩ͫᱭ", a_));
							string text3 = this.ᜀ(A_0, ClipboardData.b("e१३५", a_));
							num = 26;
							continue;
						}
						case 17:
							this.ᜀ(true, A_0, ᜂ);
							num = 22;
							continue;
						case 18:
							this.ᜀ(false, A_0, ᜂ);
							num = 0;
							continue;
						case 19:
							ᜂ.ᜀ(Color.Blue);
							ᜂ.ᜀ(true);
							num = 20;
							continue;
						case 20:
							ᜂ.ᜀ(SubSuperScript.SuperScript);
							num = 13;
							continue;
						case 21:
							ᜂ.ᜀ(SubSuperScript.SubScript);
							num = 14;
							continue;
						default:
							num = 29;
							continue;
						}
						break;
					}
					case 18:
						goto IL_3EB;
					case 19:
						goto IL_26B;
					case 20:
						goto IL_26B;
					case 21:
					{
						string key;
						if ((key = text) != null)
						{
							num = 15;
							continue;
						}
						goto IL_26B;
					}
					case 22:
						goto IL_26B;
					case 23:
						if (spr᧓.\u17C9 == null)
						{
							num = 30;
							continue;
						}
						goto IL_188;
					case 24:
					{
						string text3;
						if (text3.Length > 0)
						{
							num = 27;
							continue;
						}
						goto IL_1BA;
					}
					case 25:
						goto IL_26B;
					case 26:
					{
						string text2;
						if (text2.Length > 0)
						{
							num = 8;
							continue;
						}
						goto IL_6DA;
					}
					case 27:
					{
						string text3;
						ᜂ.ᜀ(text3);
						num = 36;
						continue;
					}
					case 28:
						if (true)
						{
						}
						num = 17;
						continue;
					case 29:
						num = 34;
						continue;
					case 30:
						spr᧓.\u17C9 = new Dictionary<string, int>(22)
						{
							{
								ClipboardData.b("ѥ", a_),
								0
							},
							{
								ClipboardData.b("ᕥᱧᡩͫmᝯ", a_),
								1
							},
							{
								ClipboardData.b("ཥ", a_),
								2
							},
							{
								ClipboardData.b("ͥէ", a_),
								3
							},
							{
								ClipboardData.b("եŧṩ५", a_),
								4
							},
							{
								ClipboardData.b("ɥ๧ѩ", a_),
								5
							},
							{
								ClipboardData.b("ၥ१ᡩ", a_),
								6
							},
							{
								ClipboardData.b("፥", a_),
								7
							},
							{
								ClipboardData.b("ᕥ", a_),
								8
							},
							{
								ClipboardData.b("ᕥᱧᡩիխᕯ", a_),
								9
							},
							{
								ClipboardData.b("ᕥէ୩kɭ", a_),
								10
							},
							{
								ClipboardData.b("ѥŧ൩", a_),
								11
							},
							{
								ClipboardData.b("եݧ๩५", a_),
								12
							},
							{
								ClipboardData.b("ብᱧ", a_),
								13
							},
							{
								ClipboardData.b("ᙥᩧཀྵ", a_),
								14
							},
							{
								ClipboardData.b("ᕥ१ݩᱫ", a_),
								15
							},
							{
								ClipboardData.b("eݧѩᡫ", a_),
								16
							},
							{
								ClipboardData.b("፥ѧ", a_),
								17
							},
							{
								ClipboardData.b("॥ѧ", a_),
								18
							},
							{
								ClipboardData.b("ݥ", a_),
								19
							},
							{
								ClipboardData.b("ᕥᵧᩩ", a_),
								20
							},
							{
								ClipboardData.b("ᕥᵧࡩ", a_),
								21
							}
						};
						num = 11;
						continue;
					case 31:
						goto IL_26B;
					case 32:
						goto IL_6DA;
					case 33:
						this.ᜀ(text4, ᜂ);
						num = 7;
						continue;
					case 34:
						goto IL_26B;
					case 35:
						if (text4.Length > 0)
						{
							num = 33;
							continue;
						}
						goto IL_26B;
					case 36:
						goto IL_1BA;
					}
					break;
					IL_188:
					num = 9;
					continue;
					IL_1BA:
					text4 = this.ᜀ(A_0, ClipboardData.b("ᕥŧၩ५", a_));
					num = 35;
					continue;
					IL_26B:
					this.ᜀ(A_0.ChildNodes);
					this.ᜀ(true);
					num = 3;
					continue;
					IL_3EB:
					spr\u1DE8.ᜂ ᜂ3 = ᜂ;
					ᜂ3.ᜁ(ᜂ3.ᜁ() - 2f);
					num = 10;
					continue;
					IL_6DA:
					num = 24;
				}
			}
			return;
		}
	}

	// Token: 0x06003537 RID: 13623 RVA: 0x003157FC File Offset: 0x003147FC
	private void ᜀ(string A_0, spr\u1DE8.ᜂ A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = false;
				bool flag2 = false;
				bool flag3 = false;
				int num = 4;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						if (flag)
						{
							num = 6;
							continue;
						}
						num = 16;
						continue;
					case 1:
						if (A_0.StartsWith(ClipboardData.b("干", a_)))
						{
							num = 19;
							continue;
						}
						goto IL_23E;
					case 2:
						goto IL_23E;
					case 3:
						flag3 = false;
						num = 23;
						continue;
					case 4:
						if (A_0.StartsWith(ClipboardData.b("塲", a_)))
						{
							num = 13;
							continue;
						}
						num = 1;
						continue;
					case 5:
					{
						if (num2 >= A_0.Length)
						{
							num = 11;
							continue;
						}
						char c = A_0[num2];
						num = 10;
						continue;
					}
					case 6:
					{
						int num3 = 3 + num3;
						num = 7;
						continue;
					}
					case 7:
						goto IL_2D0;
					case 8:
						goto IL_257;
					case 9:
						goto IL_EC;
					case 10:
					{
						char c;
						if (char.IsDigit(c))
						{
							num = 3;
							continue;
						}
						flag3 = true;
						num = 24;
						continue;
					}
					case 11:
						goto IL_1E7;
					case 12:
						return;
					case 13:
						flag = true;
						A_0 = A_0.Substring(1, A_0.Length - 1);
						num = 2;
						continue;
					case 14:
					{
						int num4;
						switch (num4)
						{
						case 2:
							goto IL_1C5;
						case 3:
							goto IL_281;
						case 4:
							goto IL_192;
						case 5:
							goto IL_1DB;
						case 6:
							goto IL_397;
						default:
							num = 12;
							continue;
						}
						break;
					}
					case 15:
					{
						int num3 = 3 - num3;
						num = 20;
						continue;
					}
					case 16:
						if (flag2)
						{
							num = 15;
							continue;
						}
						goto IL_2D0;
					case 17:
						goto IL_205;
					case 18:
					{
						int num3;
						if (num3 >= 7)
						{
							num = 9;
							continue;
						}
						int num4 = num3;
						num = 14;
						continue;
					}
					case 19:
						flag2 = true;
						A_0 = A_0.Substring(1, A_0.Length - 1);
						num = 27;
						continue;
					case 20:
						goto IL_2D0;
					case 21:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2F5;
						default:
							goto IL_180;
						}
						break;
					case 22:
					{
						if (flag3)
						{
							num = 17;
							continue;
						}
						int num3 = Convert.ToInt32(A_0);
						num = 0;
						continue;
					}
					case 23:
						goto IL_2F5;
					case 24:
						goto IL_1E7;
					case 25:
						goto IL_257;
					case 26:
					{
						int num3;
						if (num3 <= 1)
						{
							num = 21;
							continue;
						}
						num = 18;
						continue;
					}
					case 27:
						goto IL_23E;
					}
					break;
					IL_1E7:
					num = 22;
					continue;
					IL_23E:
					if (true)
					{
					}
					num2 = 0;
					num = 25;
					continue;
					IL_257:
					num = 5;
					continue;
					IL_2D0:
					num = 26;
					continue;
					IL_2F5:
					num2++;
					num = 8;
				}
			}
			IL_EC:
			A_1.ᜁ(36f);
			return;
			IL_180:
			if (false)
			{
			}
			A_1.ᜁ(7.5f);
			return;
			IL_192:
			A_1.ᜁ(13.5f);
			return;
			IL_1C5:
			A_1.ᜁ(10f);
			return;
			IL_1DB:
			A_1.ᜁ(18f);
			return;
			IL_205:
			A_1.ᜁ(12f);
			return;
			IL_281:
			A_1.ᜁ(12f);
			return;
			IL_397:
			A_1.ᜁ(24f);
			return;
		}
	}

	// Token: 0x06003538 RID: 13624 RVA: 0x00315BAC File Offset: 0x00314BAC
	private void ᜀ(bool A_0, XmlNode A_1, spr\u1DE8.ᜂ A_2)
	{
		int a_ = 1;
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_2.\u1713 = true;
				num = 10;
				continue;
			case 1:
				goto IL_16C;
			case 2:
				return;
			case 4:
				num = 6;
				continue;
			case 5:
				IL_177:
				if (A_1.ParentNode != null)
				{
					num = 4;
					continue;
				}
				return;
			case 6:
				if (A_1.ParentNode.Name != null)
				{
					num = 8;
					continue;
				}
				return;
			case 7:
				goto IL_D6;
			case 8:
				num = 11;
				continue;
			case 9:
				if (A_1.ParentNode.Name.ToLower() == ClipboardData.b("զ٨ཪᑬ", a_))
				{
					num = 7;
					continue;
				}
				return;
			case 10:
				goto IL_16C;
			case 11:
				if (!(A_1.ParentNode.Name.ToLower() == ClipboardData.b("སᵨ٪Ŭ", a_)))
				{
					num = 12;
					continue;
				}
				goto IL_D6;
			case 12:
				num = 9;
				continue;
			}
			if (A_0)
			{
				num = 0;
				continue;
			}
			A_2.\u1712 = true;
			num = 1;
			continue;
			IL_D6:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_177;
			default:
				if (false)
				{
				}
				if (true)
				{
				}
				this.\u1717 = -1;
				num = 2;
				continue;
			}
			IL_16C:
			num = 5;
		}
	}

	// Token: 0x06003539 RID: 13625 RVA: 0x00315D48 File Offset: 0x00314D48
	private void ᜈ(XmlNode A_0)
	{
		int a_ = 13;
		int num = 7;
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
					this.\u1717--;
					this.ᜊ().Pop();
					num = 3;
					continue;
				case 1:
					this.ᜈ();
					num = 4;
					continue;
				case 2:
					num = 9;
					continue;
				case 3:
					return;
				case 4:
					goto IL_124;
				case 5:
					if (!this.ᜌ().\u1712)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					goto IL_85;
				case 6:
					num = 5;
					continue;
				case 8:
					if (this.ᜃ(A_0))
					{
						num = 6;
						continue;
					}
					return;
				case 9:
					if (this.ᜌ().\u1713)
					{
						num = 11;
						continue;
					}
					return;
				case 10:
					if (A_0.LocalName.ToLower() == ClipboardData.b("ὲᱴ", a_))
					{
						num = 0;
						continue;
					}
					return;
				case 11:
					goto IL_85;
				}
				if (!this.ᜏ(A_0))
				{
					num = 1;
					continue;
				}
				goto IL_124;
				IL_85:
				num = 10;
				continue;
				IL_124:
				this.ᜇ(A_0);
				this.ᜀ(A_0.ChildNodes);
				this.ᜃ();
				break;
			}
			num = 8;
		}
	}

	// Token: 0x0600353A RID: 13626 RVA: 0x00315EE0 File Offset: 0x00314EE0
	private void ᜃ()
	{
		spr\u1DE8.ᜂ ᜂ;
		for (;;)
		{
			ᜂ = this.ᜌ();
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (this.\u1712 == null)
					{
						goto IL_37;
					}
					num = 4;
					continue;
				case 2:
					if (ᜂ.\u1714 != BuiltinStyle.Normal)
					{
						num = 5;
						continue;
					}
					goto IL_80;
				case 3:
					if (true)
					{
					}
					num = 2;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						if (false)
						{
						}
						if (this.ᜨ == null)
						{
							num = 3;
							continue;
						}
						goto IL_CA;
					}
					break;
				case 5:
					goto IL_7D;
				}
				break;
				IL_37:
				num = 0;
			}
		}
		return;
		IL_7D:
		this.\u1712.ApplyStyle(ᜂ.\u1714);
		return;
		IL_80:
		this.\u1712.ApplyStyle(BuiltinStyle.NormalWeb);
		return;
		IL_CA:
		this.\u1712.ᜀ(this.ᜨ);
	}

	// Token: 0x0600353B RID: 13627 RVA: 0x00315FC8 File Offset: 0x00314FC8
	private void ᜇ(XmlNode A_0)
	{
		int a_ = 3;
		int num = 37;
		for (;;)
		{
			ParagraphFormat format;
			spr\u1DE8.ᜂ ᜂ;
			switch (num)
			{
			case 0:
				format.AfterSpacing = ᜂ.ᜏ();
				num = 1;
				continue;
			case 1:
				goto IL_7DA;
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
					if (ᜂ.ᜇ() == 0f)
					{
						goto IL_295;
					}
					break;
				}
				num = 7;
				continue;
			case 3:
				this.\u1712.Format.HorizontalAlignment = ᜂ.ᜌ();
				num = 39;
				continue;
			case 4:
				if (ᜂ.ᜎ() > 0f)
				{
					num = 44;
					continue;
				}
				goto IL_75B;
			case 5:
				if (ᜂ.ᜑ() >= 0f)
				{
					num = 18;
					continue;
				}
				goto IL_22C;
			case 6:
				format.LineSpacingRule = LineSpacingRule.AtLeast;
				format.LineSpacing = ᜂ.ᜉ();
				num = 63;
				continue;
			case 7:
				format.FirstLineIndent = ᜂ.ᜇ();
				num = 50;
				continue;
			case 8:
				goto IL_22C;
			case 9:
				goto IL_185;
			case 10:
				if (ᜂ.ᜀ(10))
				{
					num = 60;
					continue;
				}
				goto IL_3E5;
			case 11:
				this.\u1712.ᜀ(this.ᜨ);
				num = 34;
				continue;
			case 12:
				format.IsSpacingAfterAuto = true;
				format.IsSpacingBeforeAuto = true;
				num = 54;
				continue;
			case 13:
				format.LeftIndent = this.ᜋ().Format.LeftIndent + (float)this.ᜫ * 36f;
				num = 14;
				continue;
			case 14:
				return;
			case 15:
				if (true)
				{
				}
				num = 47;
				continue;
			case 16:
				if (this.\u171F)
				{
					num = 64;
					continue;
				}
				goto IL_81E;
			case 17:
				if (ᜂ.ᜐ() > 0f)
				{
					num = 61;
					continue;
				}
				goto IL_5B6;
			case 18:
				format.BeforeSpacing = ᜂ.ᜑ();
				num = 8;
				continue;
			case 19:
				if (A_0.NextSibling != null)
				{
					num = 35;
					continue;
				}
				goto IL_7C3;
			case 20:
				goto IL_81E;
			case 21:
				if (A_0.PreviousSibling != null)
				{
					num = 32;
					continue;
				}
				goto IL_6C7;
			case 22:
				this.\u1712.ApplyStyle(ᜂ.\u1714);
				num = 46;
				continue;
			case 23:
				if (ᜂ.ᜏ() >= 0f)
				{
					num = 0;
					continue;
				}
				goto IL_7DA;
			case 24:
				goto IL_3E5;
			case 25:
				if (A_0.LocalName.ToLower() == ClipboardData.b("ըɪ", a_))
				{
					num = 42;
					continue;
				}
				goto IL_735;
			case 26:
				this.\u1712.Format.LeftIndent = 36f;
				this.\u1712.Format.LeftIndentBi = 36f;
				num = 36;
				continue;
			case 27:
				num = 19;
				continue;
			case 28:
				if (this.ᜪ)
				{
					num = 13;
					continue;
				}
				return;
			case 29:
				if (A_0.NextSibling.LocalName.ToLower() != ClipboardData.b("ըɪ", a_))
				{
					num = 59;
					continue;
				}
				goto IL_2E8;
			case 30:
				if (ᜂ.ᜀ())
				{
					num = 12;
					continue;
				}
				goto IL_35A;
			case 31:
				if (A_0.PreviousSibling.LocalName.ToLower() != ClipboardData.b("ըɪ", a_))
				{
					num = 62;
					continue;
				}
				goto IL_735;
			case 32:
				num = 31;
				continue;
			case 33:
				num = 52;
				continue;
			case 34:
				goto IL_185;
			case 35:
				num = 29;
				continue;
			case 36:
				goto IL_2C0;
			case 38:
				if (!this.\u1712.IsInCell)
				{
					num = 53;
					continue;
				}
				goto IL_6DB;
			case 39:
				goto IL_587;
			case 40:
				if (this.ᜠ)
				{
					num = 66;
					continue;
				}
				goto IL_644;
			case 41:
				num = 38;
				continue;
			case 42:
				num = 21;
				continue;
			case 43:
				goto IL_5B6;
			case 44:
				format.LeftIndent = ᜂ.ᜎ();
				num = 56;
				continue;
			case 45:
				if (A_0.LocalName.ToLower() == ClipboardData.b("൨ཪ", a_))
				{
					num = 26;
					continue;
				}
				goto IL_2C0;
			case 46:
				goto IL_185;
			case 47:
				if (ᜂ.ᜀ(10))
				{
					num = 3;
					continue;
				}
				goto IL_587;
			case 48:
				if (ᜂ.\u1714 != BuiltinStyle.Normal)
				{
					num = 22;
					continue;
				}
				num = 67;
				continue;
			case 49:
				if (this.\u1712.IsInCell)
				{
					num = 15;
					continue;
				}
				goto IL_587;
			case 50:
				goto IL_295;
			case 51:
				goto IL_735;
			case 52:
				if (this.ᜥ)
				{
					num = 41;
					continue;
				}
				goto IL_722;
			case 53:
				goto IL_722;
			case 54:
				goto IL_35A;
			case 55:
				goto IL_2E8;
			case 56:
				goto IL_75B;
			case 57:
				if (A_0.LocalName.ToLower() == ClipboardData.b("ըɪ", a_))
				{
					num = 27;
					continue;
				}
				goto IL_2E8;
			case 58:
				goto IL_6DB;
			case 59:
				goto IL_7C3;
			case 60:
				format.HorizontalAlignment = ᜂ.ᜌ();
				num = 24;
				continue;
			case 61:
				format.RightIndent = ᜂ.ᜐ();
				num = 43;
				continue;
			case 62:
				goto IL_6C7;
			case 63:
				goto IL_69C;
			case 64:
				format.PageBreakBefore = true;
				this.\u171F = false;
				num = 20;
				continue;
			case 65:
				goto IL_644;
			case 66:
				format.PageBreakAfter = true;
				this.ᜠ = false;
				num = 65;
				continue;
			case 67:
				if (this.ᜨ != null)
				{
					num = 11;
					continue;
				}
				this.\u1712.ApplyStyle(BuiltinStyle.NormalWeb);
				num = 9;
				continue;
			case 68:
				if (ᜂ.ᜀ(8))
				{
					num = 6;
					continue;
				}
				goto IL_69C;
			}
			if (this.\u1712 != null)
			{
				num = 33;
				continue;
			}
			break;
			IL_185:
			num = 45;
			continue;
			IL_22C:
			num = 16;
			continue;
			IL_295:
			num = 17;
			continue;
			IL_2C0:
			num = 10;
			continue;
			IL_2E8:
			num = 25;
			continue;
			IL_35A:
			num = 68;
			continue;
			IL_3E5:
			num = 49;
			continue;
			IL_587:
			this.ᜀ(format, ᜂ, A_0);
			num = 30;
			continue;
			IL_5B6:
			num = 23;
			continue;
			IL_644:
			ᜂ.\u170D();
			format.BackColor = ᜂ.\u170D();
			this.ᜄ(A_0);
			num = 57;
			continue;
			IL_69C:
			num = 4;
			continue;
			IL_6C7:
			format.IsSpacingBeforeAuto = true;
			num = 51;
			continue;
			IL_6DB:
			format = this.\u1712.Format;
			ᜂ = this.ᜌ();
			this.ᜃ();
			this.ᜀ(format, ᜂ);
			num = 48;
			continue;
			IL_722:
			this.ᜂ();
			num = 58;
			continue;
			IL_735:
			num = 28;
			continue;
			IL_75B:
			num = 2;
			continue;
			IL_7C3:
			format.IsSpacingAfterAuto = true;
			num = 55;
			continue;
			IL_7DA:
			num = 5;
			continue;
			IL_81E:
			num = 40;
		}
	}

	// Token: 0x0600353C RID: 13628 RVA: 0x0031681C File Offset: 0x0031581C
	private void ᜀ(ParagraphFormat A_0, spr\u1DE8.ᜂ A_1, XmlNode A_2)
	{
		int a_ = 7;
		switch (0)
		{
		default:
		{
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					try
					{
						num = 11;
						for (;;)
						{
							switch (num)
							{
							case 0:
								num = 2;
								continue;
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_422;
								default:
									if (false)
									{
									}
									num = 14;
									continue;
								}
								break;
							case 2:
								goto IL_479;
							case 3:
								goto IL_35B;
							case 4:
								goto IL_35B;
							case 5:
							{
								string value;
								if (!(value == ClipboardData.b("⑬", a_)))
								{
									num = 0;
									continue;
								}
								this.ᜂ(ListPatternType.UpRoman, A_2);
								num = 4;
								continue;
							}
							case 6:
								goto IL_35B;
							case 8:
							{
								string value;
								if (!(value == ClipboardData.b("ⱬ", a_)))
								{
									num = 1;
									continue;
								}
								this.ᜂ(ListPatternType.UpLetter, A_2);
								num = 6;
								continue;
							}
							case 9:
								goto IL_422;
							case 10:
								goto IL_54C;
							case 12:
								num = 10;
								continue;
							case 13:
							{
								IEnumerator enumerator;
								if (!enumerator.MoveNext())
								{
									num = 12;
									continue;
								}
								XmlAttribute xmlAttribute = (XmlAttribute)enumerator.Current;
								num = 17;
								continue;
							}
							case 14:
							{
								string value;
								if (!(value == ClipboardData.b("Ѭ", a_)))
								{
									num = 22;
									continue;
								}
								this.ᜂ(ListPatternType.LowRoman, A_2);
								num = 3;
								continue;
							}
							case 15:
								num = 8;
								continue;
							case 16:
							{
								string value;
								if (!(value == ClipboardData.b("౬", a_)))
								{
									num = 15;
									continue;
								}
								this.ᜂ(ListPatternType.LowLetter, A_2);
								num = 19;
								continue;
							}
							case 17:
							{
								XmlAttribute xmlAttribute;
								if (xmlAttribute.Name.ToLower() == ClipboardData.b("ᥬ᙮Űᙲ", a_))
								{
									num = 18;
									continue;
								}
								break;
							}
							case 18:
								num = 21;
								continue;
							case 19:
								goto IL_35B;
							case 20:
								goto IL_35B;
							case 21:
							{
								string value;
								XmlAttribute xmlAttribute;
								if ((value = xmlAttribute.Value) != null)
								{
									num = 9;
									continue;
								}
								goto IL_479;
							}
							case 22:
								num = 5;
								continue;
							}
							goto IL_326;
							IL_35B:
							bool flag = true;
							num = 7;
							continue;
							IL_422:
							num = 16;
							continue;
							IL_479:
							this.ᜂ(ListPatternType.Arabic, A_2);
							num = 20;
							continue;
							IL_502:
							num = 13;
							continue;
							IL_326:
							goto IL_502;
						}
						IL_54C:
						goto IL_1F7;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator;
							IDisposable disposable = enumerator as IDisposable;
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_596;
								case 1:
									if (disposable != null)
									{
										num = 2;
										continue;
									}
									goto IL_598;
								case 2:
									disposable.Dispose();
									num = 0;
									continue;
								}
								break;
							}
						}
						IL_596:
						IL_598:;
					}
					goto IL_599;
					IL_1F7:
					num = 21;
					continue;
				case 1:
					num = 7;
					continue;
				case 2:
					goto IL_1F2;
				case 3:
					goto IL_599;
				case 4:
					if (A_2.Name.ToUpper() == ClipboardData.b("Ⅼ❮", a_))
					{
						num = 9;
						continue;
					}
					return;
				case 5:
					if (A_2.Name.ToUpper() != ClipboardData.b("Ⅼ❮", a_))
					{
						num = 12;
						continue;
					}
					goto IL_E2;
				case 6:
					num = 20;
					continue;
				case 7:
					if (A_2.ParentNode.Name.ToLower() == ClipboardData.b("ɬͮ", a_))
					{
						num = 14;
						continue;
					}
					goto IL_E2;
				case 8:
					goto IL_215;
				case 9:
					A_0.LeftIndent = 35f;
					num = 2;
					continue;
				case 10:
					if (A_2.LocalName.ToLower() == ClipboardData.b("Ŭٮ", a_))
					{
						if (true)
						{
						}
						num = 15;
						continue;
					}
					goto IL_23B;
				case 12:
					num = 19;
					continue;
				case 13:
					if (A_2.ParentNode.Name.ToLower() == ClipboardData.b("ᡬͮ", a_))
					{
						num = 6;
						continue;
					}
					goto IL_23B;
				case 14:
				{
					bool flag = false;
					IEnumerator enumerator = A_2.ParentNode.Attributes.GetEnumerator();
					num = 0;
					continue;
				}
				case 15:
					num = 18;
					continue;
				case 16:
					num = 10;
					continue;
				case 17:
					goto IL_2B3;
				case 18:
					if (A_2.LocalName.ToLower() != ClipboardData.b("Ŭݮ", a_))
					{
						num = 17;
						continue;
					}
					goto IL_23B;
				case 19:
					if (A_2.Name.ToLower() == ClipboardData.b("Ŭٮ", a_))
					{
						num = 1;
						continue;
					}
					goto IL_E2;
				case 20:
					if (A_1.\u1713)
					{
						num = 16;
						continue;
					}
					goto IL_23B;
				case 21:
				{
					bool flag;
					if (!flag)
					{
						num = 8;
						continue;
					}
					return;
				}
				}
				if (A_1.\u1712)
				{
					num = 3;
					continue;
				}
				IL_E2:
				num = 13;
				continue;
				IL_23B:
				num = 4;
				continue;
				IL_599:
				num = 5;
			}
			IL_1F2:
			return;
			IL_215:
			this.ᜂ(ListPatternType.Arabic, A_2);
			return;
			IL_2B3:
			this.ᜂ(ListPatternType.Bullet, A_2);
			return;
		}
		}
	}

	// Token: 0x0600353D RID: 13629 RVA: 0x00316E34 File Offset: 0x00315E34
	private void ᜀ(ParagraphFormat A_0, spr\u1DE8.ᜂ A_1)
	{
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.Borders.Left.BorderType = A_1.\u1715.ᜉ;
				A_0.Borders.Left.LineWidth = A_1.\u1715.\u170D;
				A_0.Borders.Left.Color = A_1.\u1715.ᜅ;
				num = 13;
				continue;
			case 1:
				if (true)
				{
				}
				A_0.Borders.Bottom.LineWidth = A_1.\u1715.ᜌ;
				num = 12;
				continue;
			case 2:
				goto IL_38E;
			case 3:
				A_0.Borders.Left.LineWidth = A_1.\u1715.\u170D;
				num = 11;
				continue;
			case 4:
				if (A_1.\u1715.ᜊ != BorderStyle.None)
				{
					num = 33;
					continue;
				}
				goto IL_241;
			case 5:
				goto IL_48A;
			case 6:
				goto IL_45A;
			case 7:
				goto IL_48A;
			case 9:
				A_0.Borders.Bottom.BorderType = A_1.\u1715.ᜈ;
				A_0.Borders.Top.BorderType = A_1.\u1715.ᜇ;
				A_0.Borders.Left.BorderType = A_1.\u1715.ᜉ;
				A_0.Borders.Right.BorderType = A_1.\u1715.ᜊ;
				num = 24;
				continue;
			case 10:
				A_0.Borders.Bottom.LineWidth = A_1.\u1715.ᜌ;
				A_0.Borders.Top.LineWidth = A_1.\u1715.ᜋ;
				A_0.Borders.Left.LineWidth = A_1.\u1715.\u170D;
				A_0.Borders.Right.LineWidth = A_1.\u1715.ᜎ;
				num = 7;
				continue;
			case 11:
				goto IL_271;
			case 12:
				return;
			case 13:
				goto IL_E6;
			case 14:
				A_0.Borders.Bottom.Color = A_1.\u1715.ᜄ;
				A_0.Borders.Top.Color = A_1.\u1715.ᜃ;
				A_0.Borders.Left.Color = A_1.\u1715.ᜅ;
				A_0.Borders.Right.Color = A_1.\u1715.ᜆ;
				num = 25;
				continue;
			case 15:
				if (A_1.\u1715.ᜎ > 0f)
				{
					num = 26;
					continue;
				}
				goto IL_18D;
			case 16:
				if (A_1.\u1715.ᜉ != BorderStyle.None)
				{
					num = 0;
					continue;
				}
				goto IL_E6;
			case 17:
				if (A_1.\u1715.ᜌ > 0f)
				{
					num = 1;
					continue;
				}
				return;
			case 18:
				goto IL_53E;
			case 19:
				if (A_1.\u1715.ᜈ != BorderStyle.None)
				{
					num = 23;
					continue;
				}
				goto IL_38E;
			case 20:
				goto IL_241;
			case 21:
				goto IL_2A9;
			case 22:
				goto IL_18D;
			case 23:
				A_0.Borders.Bottom.BorderType = A_1.\u1715.ᜈ;
				A_0.Borders.Bottom.LineWidth = A_1.\u1715.ᜌ;
				A_0.Borders.Bottom.Color = A_1.\u1715.ᜄ;
				num = 2;
				continue;
			case 24:
				if (A_1.\u1715.ᜀ != Color.Empty)
				{
					num = 14;
					continue;
				}
				A_0.Borders.Bottom.Color = (A_0.Borders.Top.Color = (A_0.Borders.Left.Color = (A_0.Borders.Right.Color = Color.Silver)));
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 25:
				goto IL_45A;
			case 26:
				A_0.Borders.Right.LineWidth = A_1.\u1715.ᜎ;
				num = 22;
				continue;
			case 27:
				A_0.Borders.Top.LineWidth = A_1.\u1715.ᜋ;
				num = 18;
				continue;
			case 28:
				if (A_1.\u1715.ᜇ != BorderStyle.None)
				{
					num = 32;
					continue;
				}
				goto IL_2A9;
			case 29:
				if (A_1.\u1715.\u170D > 0f)
				{
					num = 3;
					continue;
				}
				goto IL_271;
			case 30:
				if (A_1.\u1715.ᜋ > 0f)
				{
					num = 27;
					continue;
				}
				goto IL_53E;
			case 31:
				if (A_1.\u1715.ᜁ != -1f)
				{
					num = 10;
					continue;
				}
				A_0.Borders.Bottom.LineWidth = 1f;
				A_0.Borders.Top.LineWidth = 1f;
				A_0.Borders.Left.LineWidth = 1f;
				A_0.Borders.Right.LineWidth = 1f;
				num = 5;
				continue;
			case 32:
				A_0.Borders.Top.BorderType = A_1.\u1715.ᜇ;
				A_0.Borders.Top.LineWidth = A_1.\u1715.ᜋ;
				A_0.Borders.Top.Color = A_1.\u1715.ᜃ;
				num = 21;
				continue;
			case 33:
				A_0.Borders.Right.BorderType = A_1.\u1715.ᜊ;
				A_0.Borders.Right.LineWidth = A_1.\u1715.ᜎ;
				A_0.Borders.Right.Color = A_1.\u1715.ᜆ;
				num = 20;
				continue;
			}
			IL_9B:
			if (A_1.\u1715.ᜂ != BorderStyle.None)
			{
				num = 9;
				continue;
			}
			goto IL_48A;
			goto IL_9B;
			IL_E6:
			num = 4;
			continue;
			IL_18D:
			num = 29;
			continue;
			IL_241:
			num = 30;
			continue;
			IL_271:
			num = 17;
			continue;
			IL_2A9:
			num = 16;
			continue;
			IL_38E:
			num = 28;
			continue;
			IL_45A:
			num = 31;
			continue;
			IL_48A:
			num = 19;
			continue;
			IL_53E:
			num = 15;
		}
	}

	// Token: 0x0600353E RID: 13630 RVA: 0x0031754C File Offset: 0x0031654C
	private void ᜂ()
	{
		switch (0)
		{
		default:
		{
			int num = 15;
			for (;;)
			{
				ParagraphFormat format;
				switch (num)
				{
				case 0:
					if (this.ᜤ.\u1715.ᜇ != BorderStyle.None)
					{
						num = 8;
						continue;
					}
					return;
				case 1:
					goto IL_255;
				case 2:
					format.Borders.Bottom.BorderType = this.ᜤ.\u1715.ᜈ;
					format.Borders.Bottom.LineWidth = this.ᜤ.\u1715.ᜌ;
					format.Borders.Bottom.Color = this.ᜤ.\u1715.ᜄ;
					num = 5;
					continue;
				case 3:
					if (this.ᜤ.\u1715.ᜂ != BorderStyle.None)
					{
						num = 4;
						continue;
					}
					goto IL_81;
				case 4:
					format.Borders.Bottom.BorderType = this.ᜤ.\u1715.ᜈ;
					format.Borders.Top.BorderType = this.ᜤ.\u1715.ᜇ;
					format.Borders.Left.BorderType = this.ᜤ.\u1715.ᜉ;
					format.Borders.Right.BorderType = this.ᜤ.\u1715.ᜊ;
					num = 10;
					continue;
				case 5:
					goto IL_219;
				case 6:
					return;
				case 7:
					goto IL_3A8;
				case 8:
					format.Borders.Top.BorderType = this.ᜤ.\u1715.ᜇ;
					format.Borders.Top.LineWidth = this.ᜤ.\u1715.ᜋ;
					format.Borders.Top.Color = this.ᜤ.\u1715.ᜃ;
					num = 6;
					continue;
				case 9:
					goto IL_3A8;
				case 10:
					if (this.ᜤ.\u1715.ᜀ != Color.Empty)
					{
						num = 17;
						continue;
					}
					format.Borders.Bottom.Color = (format.Borders.Top.Color = (format.Borders.Left.Color = (format.Borders.Right.Color = Color.Silver)));
					num = 7;
					continue;
				case 11:
					format.Borders.Bottom.LineWidth = this.ᜤ.\u1715.ᜌ;
					format.Borders.Top.LineWidth = this.ᜤ.\u1715.ᜋ;
					format.Borders.Left.LineWidth = this.ᜤ.\u1715.\u170D;
					format.Borders.Right.LineWidth = this.ᜤ.\u1715.ᜎ;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_255;
					default:
						if (false)
						{
						}
						num = 16;
						continue;
					}
					break;
				case 12:
					if (this.ᜤ.\u1715.ᜈ != BorderStyle.None)
					{
						num = 2;
						continue;
					}
					goto IL_219;
				case 13:
					if (this.ᜤ.\u1715.ᜁ != -1f)
					{
						if (true)
						{
						}
						num = 11;
						continue;
					}
					format.Borders.Bottom.LineWidth = 1f;
					format.Borders.Top.LineWidth = 1f;
					format.Borders.Left.LineWidth = 1f;
					format.Borders.Right.LineWidth = 1f;
					num = 14;
					continue;
				case 14:
					goto IL_81;
				case 16:
					goto IL_81;
				case 17:
					format.Borders.Bottom.Color = this.ᜤ.\u1715.ᜄ;
					format.Borders.Top.Color = this.ᜤ.\u1715.ᜃ;
					format.Borders.Left.Color = this.ᜤ.\u1715.ᜅ;
					format.Borders.Right.Color = this.ᜤ.\u1715.ᜆ;
					num = 9;
					continue;
				}
				if (this.ᜤ != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_81:
				num = 12;
				continue;
				IL_219:
				num = 0;
				continue;
				IL_255:
				format = this.\u1712.Format;
				format.BackColor = this.ᜤ.\u170D();
				format.LeftIndent = this.ᜤ.ᜎ();
				format.HorizontalAlignment = this.ᜤ.ᜌ();
				num = 3;
				continue;
				IL_3A8:
				num = 13;
			}
			return;
		}
		}
	}

	// Token: 0x0600353F RID: 13631 RVA: 0x00317A98 File Offset: 0x00316A98
	private void ᜁ(ITextRange A_0)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 7;
			for (;;)
			{
				spr\u1DE8.ᜂ ᜂ;
				switch (num)
				{
				case 0:
					num = 8;
					continue;
				case 1:
					num = 35;
					continue;
				case 2:
					num = 50;
					continue;
				case 3:
					goto IL_636;
				case 4:
					num = 23;
					continue;
				case 5:
					goto IL_1DD;
				case 6:
					if (this.ᜨ != null)
					{
						num = 19;
						continue;
					}
					goto IL_5D3;
				case 8:
					if (ᜂ.ᜂ())
					{
						num = 25;
						continue;
					}
					goto IL_236;
				case 9:
					if (ᜂ.ᜀ(0))
					{
						num = 39;
						continue;
					}
					num = 36;
					continue;
				case 10:
					goto IL_130;
				case 11:
					goto IL_5A7;
				case 12:
					goto IL_375;
				case 13:
					num = 40;
					continue;
				case 14:
					goto IL_66C;
				case 15:
					if (ᜂ.ᜄ() != Color.Empty)
					{
						num = 38;
						continue;
					}
					goto IL_281;
				case 16:
					A_0.CharacterFormat.Bold = ᜂ.ᜋ();
					num = 14;
					continue;
				case 17:
					goto IL_582;
				case 18:
				{
					ParagraphStyle paragraphStyle;
					if (paragraphStyle != null)
					{
						num = 1;
						continue;
					}
					A_0.CharacterFormat.FontSize = 12f;
					num = 12;
					continue;
				}
				case 19:
					num = 56;
					continue;
				case 20:
					if (ᜂ.ᜀ(7))
					{
						num = 4;
						continue;
					}
					goto IL_582;
				case 21:
					goto IL_5D3;
				case 22:
					A_0.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
					num = 10;
					continue;
				case 23:
					if (ᜂ.\u170D() != Color.Empty)
					{
						num = 47;
						continue;
					}
					goto IL_582;
				case 24:
					goto IL_375;
				case 25:
					A_0.CharacterFormat.IsStrikeout = true;
					goto IL_608;
				case 26:
				{
					char[] trimChars = new char[]
					{
						'\''
					};
					A_0.CharacterFormat.FontName = ᜂ.ᜅ().Trim(trimChars);
					num = 27;
					continue;
				}
				case 27:
					goto IL_3E5;
				case 28:
					if (this.ᜋ().ParaStyle.CharacterFormat.HasKey(3))
					{
						num = 30;
						continue;
					}
					goto IL_325;
				case 29:
					num = 15;
					continue;
				case 30:
					A_0.CharacterFormat.FontSize = this.ᜋ().ParaStyle.CharacterFormat.FontSize;
					num = 52;
					continue;
				case 31:
					A_0.CharacterFormat.SubSuperScript = ᜂ.ᜊ();
					num = 3;
					continue;
				case 32:
					if (ᜂ.ᜀ(2))
					{
						num = 16;
						continue;
					}
					goto IL_66C;
				case 33:
					if (!A_0.CharacterFormat.HasKey(3))
					{
						num = 46;
						continue;
					}
					goto IL_375;
				case 34:
					if (ᜂ.ᜀ(3))
					{
						num = 13;
						continue;
					}
					goto IL_130;
				case 35:
				{
					ParagraphStyle paragraphStyle;
					A_0.CharacterFormat.FontSize = ((paragraphStyle.CharacterFormat.FontSize != 12f) ? paragraphStyle.CharacterFormat.FontSize : 12f);
					num = 24;
					continue;
				}
				case 36:
					if (this.ᜋ().ParaStyle != null)
					{
						num = 48;
						continue;
					}
					goto IL_325;
				case 37:
					goto IL_281;
				case 38:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_608;
					default:
						if (false)
						{
						}
						A_0.CharacterFormat.TextColor = ᜂ.ᜄ();
						num = 37;
						continue;
					}
					break;
				case 39:
					A_0.CharacterFormat.FontSize = ᜂ.ᜁ();
					num = 42;
					continue;
				case 40:
					if (ᜂ.ᜃ())
					{
						num = 22;
						continue;
					}
					goto IL_130;
				case 41:
					if (ᜂ.ᜀ(1))
					{
						num = 49;
						continue;
					}
					goto IL_3E5;
				case 42:
					goto IL_375;
				case 43:
					goto IL_236;
				case 44:
					goto IL_643;
				case 45:
					if (ᜂ.ᜀ(6))
					{
						num = 29;
						continue;
					}
					goto IL_281;
				case 46:
				{
					ParagraphStyle paragraphStyle = A_0.Document.Styles.FindByName(ClipboardData.b("❨ѪὬɮၰὲ啴彶⹸Ṻὼ噾", a_)) as ParagraphStyle;
					num = 18;
					continue;
				}
				case 47:
					A_0.CharacterFormat.TextBackgroundColor = ᜂ.\u170D();
					num = 17;
					continue;
				case 48:
					num = 28;
					continue;
				case 49:
					num = 54;
					continue;
				case 50:
					if (!this.\u1712.IsInCell)
					{
						num = 5;
						continue;
					}
					goto IL_5A7;
				case 51:
					if (ᜂ.ᜀ(4))
					{
						num = 53;
						continue;
					}
					goto IL_643;
				case 52:
					goto IL_375;
				case 53:
					A_0.CharacterFormat.Italic = ᜂ.ᜆ();
					num = 44;
					continue;
				case 54:
					if (ᜂ.ᜅ().Length > 0)
					{
						num = 26;
						continue;
					}
					goto IL_3E5;
				case 55:
					if (ᜂ.ᜀ(5))
					{
						num = 0;
						continue;
					}
					goto IL_236;
				case 56:
					if (!this.ᜨ.CharacterFormat.HasKey(4))
					{
						num = 21;
						continue;
					}
					goto IL_66C;
				case 57:
					if (ᜂ.ᜊ() != SubSuperScript.None)
					{
						num = 31;
						continue;
					}
					return;
				}
				if (this.ᜥ)
				{
					num = 2;
					continue;
				}
				goto IL_1DD;
				IL_130:
				num = 55;
				continue;
				IL_1DD:
				this.ᜀ(A_0);
				num = 11;
				continue;
				IL_236:
				num = 45;
				continue;
				IL_281:
				num = 41;
				continue;
				IL_325:
				num = 33;
				continue;
				IL_375:
				num = 20;
				continue;
				IL_3E5:
				num = 9;
				continue;
				IL_582:
				num = 57;
				continue;
				IL_5A7:
				ᜂ = this.ᜌ();
				num = 6;
				continue;
				IL_5D3:
				num = 32;
				continue;
				IL_608:
				num = 43;
				continue;
				IL_643:
				num = 34;
				continue;
				IL_66C:
				num = 51;
			}
			IL_636:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x06003540 RID: 13632 RVA: 0x0031819C File Offset: 0x0031719C
	private void ᜀ(ITextRange A_0)
	{
		int num = 20;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.CharacterFormat.Bold = this.ᜤ.ᜋ();
				num = 9;
				continue;
			case 1:
				goto IL_2AB;
			case 2:
				return;
			case 3:
				A_0.CharacterFormat.IsStrikeout = this.ᜤ.ᜂ();
				num = 4;
				continue;
			case 4:
				goto IL_144;
			case 5:
				if (this.ᜤ.ᜀ(16))
				{
					num = 24;
					continue;
				}
				return;
			case 6:
				if (this.ᜤ.ᜄ() != Color.Empty)
				{
					num = 28;
					continue;
				}
				goto IL_328;
			case 7:
				goto IL_193;
			case 8:
				A_0.CharacterFormat.FontName = this.ᜤ.ᜅ();
				num = 1;
				continue;
			case 9:
				goto IL_F5;
			case 10:
				if (this.ᜤ.ᜀ(4))
				{
					num = 11;
					continue;
				}
				goto IL_2FA;
			case 11:
				A_0.CharacterFormat.Italic = this.ᜤ.ᜆ();
				num = 12;
				continue;
			case 12:
				goto IL_2FA;
			case 13:
				if (this.ᜤ.ᜁ() > 0f)
				{
					num = 27;
					continue;
				}
				goto IL_193;
			case 14:
				goto IL_CC;
			case 15:
				if (this.ᜤ.ᜀ(3))
				{
					num = 22;
					continue;
				}
				goto IL_CC;
			case 16:
				goto IL_328;
			case 17:
				if (this.ᜤ.ᜀ(5))
				{
					goto IL_E5;
				}
				goto IL_144;
			case 18:
				return;
			case 19:
				if (true)
				{
				}
				num = 6;
				continue;
			case 21:
				if (this.ᜤ.ᜀ(6))
				{
					num = 19;
					continue;
				}
				goto IL_328;
			case 22:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_E5;
				default:
					if (false)
					{
					}
					num = 26;
					continue;
				}
				break;
			case 23:
				if (this.ᜤ.ᜀ(2))
				{
					num = 0;
					continue;
				}
				goto IL_F5;
			case 24:
				A_0.CharacterFormat.SubSuperScript = this.ᜤ.ᜊ();
				num = 18;
				continue;
			case 25:
				if (this.ᜤ.ᜅ().Length > 0)
				{
					num = 8;
					continue;
				}
				goto IL_2AB;
			case 26:
				A_0.CharacterFormat.UnderlineStyle = (this.ᜤ.ᜃ() ? UnderlineStyle.Single : UnderlineStyle.None);
				num = 14;
				continue;
			case 27:
				A_0.CharacterFormat.FontSize = this.ᜤ.ᜁ();
				num = 7;
				continue;
			case 28:
				A_0.CharacterFormat.ForeColor = this.ᜤ.ᜄ();
				num = 16;
				continue;
			}
			if (this.ᜤ == null)
			{
				num = 2;
				continue;
			}
			num = 13;
			continue;
			IL_CC:
			num = 17;
			continue;
			IL_E5:
			num = 3;
			continue;
			IL_F5:
			num = 15;
			continue;
			IL_144:
			num = 10;
			continue;
			IL_193:
			num = 25;
			continue;
			IL_2AB:
			num = 21;
			continue;
			IL_2FA:
			num = 5;
			continue;
			IL_328:
			num = 23;
		}
	}

	// Token: 0x06003541 RID: 13633 RVA: 0x0031854C File Offset: 0x0031754C
	private spr\u1DE8.ᜂ ᜆ(XmlNode A_0)
	{
		while (!this.ᜅ(A_0))
		{
			if (true)
			{
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			}
			if (false)
			{
			}
			return this.ᜁ();
		}
		return this.ᜌ();
	}

	// Token: 0x06003542 RID: 13634 RVA: 0x003185A4 File Offset: 0x003175A4
	internal string ᜑ(string A_0)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			int num = 1;
			float num2;
			float num3;
			float num4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u1712 != null)
					{
						num = 14;
						continue;
					}
					num = 16;
					continue;
				case 2:
					goto IL_377;
				case 3:
					goto IL_EF;
				case 4:
					if (A_0.EndsWith(ClipboardData.b("ᑶᑸ", a_)))
					{
						num = 7;
						continue;
					}
					num = 18;
					continue;
				case 5:
					goto IL_3B3;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_119;
					default:
						if (false)
						{
						}
						num2 = Convert.ToSingle(A_0.Replace(ClipboardData.b("剶", a_), string.Empty));
						num = 0;
						continue;
					}
					break;
				case 7:
				{
					float cantimeter = Convert.ToSingle(A_0.Replace(ClipboardData.b("ᑶᑸ", a_), string.Empty));
					num3 = PointsConverter.FromCm(cantimeter);
					num = 24;
					continue;
				}
				case 8:
					if (A_0.EndsWith(ClipboardData.b("ቶᑸ", a_)))
					{
						goto IL_119;
					}
					num = 23;
					continue;
				case 9:
				{
					float inch = Convert.ToSingle(A_0.Replace(ClipboardData.b("Ṷ᝸", a_), string.Empty));
					num3 = PointsConverter.FromInch(inch);
					num = 3;
					continue;
				}
				case 10:
					if (A_0.EndsWith(ClipboardData.b("ݶ൸", a_)))
					{
						num = 5;
						continue;
					}
					num = 19;
					continue;
				case 11:
					goto IL_B6;
				case 12:
					goto IL_454;
				case 13:
					goto IL_311;
				case 14:
					num4 = this.\u1712.Document.Sections[0].PageSetup.ClientWidth;
					num = 2;
					continue;
				case 15:
				{
					float num5 = Convert.ToSingle(A_0.Replace(ClipboardData.b("ݶ᩸", a_), string.Empty));
					num3 = num5 * 12f;
					num = 17;
					continue;
				}
				case 16:
					if (this.\u1713 != null)
					{
						num = 22;
						continue;
					}
					num4 = 0f;
					num = 20;
					continue;
				case 17:
					goto IL_245;
				case 18:
				{
					if (A_0.EndsWith(ClipboardData.b("ݶ᩸", a_)))
					{
						num = 15;
						continue;
					}
					float num6 = Convert.ToSingle(A_0.Replace(ClipboardData.b("ݶŸ", a_), string.Empty));
					num3 = (float)((double)num6 * 0.75);
					num = 13;
					continue;
				}
				case 19:
					if (A_0.EndsWith(ClipboardData.b("剶", a_)))
					{
						num = 6;
						continue;
					}
					num = 8;
					continue;
				case 20:
					goto IL_46B;
				case 21:
				{
					float num7 = Convert.ToSingle(A_0.Replace(ClipboardData.b("ቶᑸ", a_), string.Empty));
					num3 = num7 * 12f;
					num = 12;
					continue;
				}
				case 22:
					num4 = this.\u1713.Document.Sections[0].PageSetup.ClientWidth;
					num = 25;
					continue;
				case 23:
					if (A_0.EndsWith(ClipboardData.b("Ṷ᝸", a_)))
					{
						num = 9;
						continue;
					}
					num = 4;
					continue;
				case 24:
					goto IL_1BA;
				case 25:
					goto IL_3E5;
				}
				if (A_0 == ClipboardData.b("ᙶ౸ེቼ", a_))
				{
					num = 11;
					continue;
				}
				num3 = float.MinValue;
				num = 10;
				continue;
				IL_119:
				num = 21;
			}
			IL_B6:
			return ClipboardData.b("䝶", a_);
			IL_EF:
			IL_1BA:
			IL_245:
			goto IL_470;
			IL_24A:
			num3 = num2 / 100f * num4;
			return num3.ToString();
			IL_311:
			goto IL_470;
			IL_377:
			goto IL_24A;
			IL_3B3:
			return A_0.Replace(ClipboardData.b("ݶ൸", a_), string.Empty);
			IL_3E5:
			goto IL_24A;
			IL_454:
			goto IL_470;
			IL_46B:
			goto IL_24A;
			IL_470:
			if (true)
			{
			}
			return num3.ToString();
		}
		}
	}

	// Token: 0x06003543 RID: 13635 RVA: 0x00318A30 File Offset: 0x00317A30
	private bool ᜅ(XmlNode A_0)
	{
		int a_ = 18;
		switch (0)
		{
		default:
			for (;;)
			{
				string text = this.ᜭ.ᜀ(A_0) + this.ᜀ(A_0, ClipboardData.b("୷๹ջች", a_));
				int num = 31;
				for (;;)
				{
					spr\u1DE8.ᜂ ᜂ;
					int num2;
					string[] array2;
					switch (num)
					{
					case 0:
						goto IL_1374;
					case 1:
					{
						string a;
						if (a != ClipboardData.b("౷ṹ", a_))
						{
							num = 88;
							continue;
						}
						goto IL_1374;
					}
					case 2:
					{
						ᜂ = this.ᜁ();
						ᜂ.\u1715 = new spr\u1DE8.ᜀ(null);
						string[] array = text.Split(new char[]
						{
							';',
							':'
						});
						char[] separator = new char[]
						{
							' '
						};
						num2 = 0;
						int num3 = array.Length;
						num = 78;
						continue;
					}
					case 3:
						if (true)
						{
						}
						this.\u171F = true;
						num = 0;
						continue;
					case 4:
						ᜂ.ᜃ(true);
						num = 37;
						continue;
					case 5:
						num = 61;
						continue;
					case 6:
					{
						string a;
						if (a != ClipboardData.b("౷᭹ṻች", a_))
						{
							num = 35;
							continue;
						}
						goto IL_1374;
					}
					case 7:
					{
						string text2;
						if (text2 == ClipboardData.b("୷๹๻᝽", a_))
						{
							num = 47;
							continue;
						}
						goto IL_1374;
					}
					case 8:
						goto IL_1374;
					case 9:
						goto IL_1374;
					case 10:
						goto IL_1374;
					case 11:
					{
						string text2;
						if (text2 == ClipboardData.b("᥷ᙹ୻ώ勵", a_))
						{
							num = 92;
							continue;
						}
						goto IL_1374;
					}
					case 12:
					{
						int num4;
						switch (num4)
						{
						case 1:
							ᜂ.ᜀ(Convert.ToSingle(this.ᜑ(array2[0])));
							num = 89;
							continue;
						case 2:
							ᜂ.ᜀ(Convert.ToSingle(this.ᜑ(array2[0])));
							ᜂ.ᜃ(Convert.ToSingle(this.ᜑ(array2[1])));
							num = 25;
							continue;
						case 3:
							goto IL_BD7;
						case 4:
							ᜂ.ᜀ(Convert.ToSingle(this.ᜑ(array2[0])));
							ᜂ.ᜃ(Convert.ToSingle(this.ᜑ(array2[1])));
							ᜂ.ᜂ(Convert.ToSingle(this.ᜑ(array2[2])));
							ᜂ.ᜅ(Convert.ToSingle(this.ᜑ(array2[3])));
							num = 83;
							continue;
						default:
							num = 28;
							continue;
						}
						break;
					}
					case 13:
					{
						string text2;
						if (text2 == ClipboardData.b("୷᝹ᵻች", a_))
						{
							num = 63;
							continue;
						}
						ᜂ.ᜁ((float)this.ᜀ(text2, ᜂ.ᜁ()));
						num = 41;
						continue;
					}
					case 14:
						goto IL_1374;
					case 15:
						goto IL_1374;
					case 16:
						goto IL_1374;
					case 17:
						goto IL_1374;
					case 18:
					{
						string text2;
						ᜂ.ᜄ(float.Parse(this.ᜑ(text2), CultureInfo.InvariantCulture));
						num = 32;
						continue;
					}
					case 19:
						goto IL_1374;
					case 20:
					{
						string text2;
						if (!(text2 == ClipboardData.b("ᅷ๹ᵻች", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_882;
					}
					case 21:
						goto IL_1374;
					case 22:
						goto IL_1374;
					case 23:
						goto IL_1374;
					case 24:
						goto IL_1374;
					case 25:
						goto IL_1374;
					case 26:
						num = 86;
						continue;
					case 27:
						goto IL_1374;
					case 28:
						num = 17;
						continue;
					case 29:
						goto IL_1374;
					case 30:
						goto IL_1374;
					case 31:
						if (text.Length != 0)
						{
							num = 2;
							continue;
						}
						return false;
					case 32:
						goto IL_1374;
					case 33:
						goto IL_1374;
					case 34:
						goto IL_1374;
					case 35:
						num = 45;
						continue;
					case 36:
						goto IL_1374;
					case 37:
						goto IL_1374;
					case 38:
						goto IL_1374;
					case 39:
						num = 1;
						continue;
					case 40:
						goto IL_1374;
					case 41:
						goto IL_1374;
					case 42:
						goto IL_3D1;
					case 43:
						goto IL_1374;
					case 44:
						goto IL_1374;
					case 45:
					{
						string a;
						if (a != ClipboardData.b("౷ቹ", a_))
						{
							num = 39;
							continue;
						}
						goto IL_1374;
					}
					case 46:
					{
						string text2;
						if (text2 == ClipboardData.b("ᑷ፹ቻ᭽굿ﾉ", a_))
						{
							num = 4;
							continue;
						}
						goto IL_1374;
					}
					case 47:
						ᜂ.ᜃ(true);
						num = 34;
						continue;
					case 48:
					{
						string text2;
						if (text2.ToLower() != ClipboardData.b("᥷ཹࡻᅽ", a_))
						{
							num = 62;
							continue;
						}
						ᜂ.ᜃ(-1f);
						num = 29;
						continue;
					}
					case 49:
						goto IL_1374;
					case 50:
						goto IL_936;
					case 51:
					{
						string text2;
						if (text2 == ClipboardData.b("൷ᑹ᡻᭽", a_))
						{
							num = 66;
							continue;
						}
						goto IL_6D3;
					}
					case 52:
						goto IL_1374;
					case 53:
						num = 70;
						continue;
					case 54:
						goto IL_1374;
					case 55:
					{
						int num5;
						switch (num5)
						{
						case 1:
							ᜂ.ᜀ(Convert.ToSingle(this.ᜑ(array2[0])));
							num = 84;
							continue;
						case 2:
							ᜂ.ᜀ(Convert.ToSingle(this.ᜑ(array2[0])));
							ᜂ.ᜃ(Convert.ToSingle(this.ᜑ(array2[1])));
							num = 64;
							continue;
						case 3:
							ᜂ.ᜀ(Convert.ToSingle(this.ᜑ(array2[0])));
							ᜂ.ᜃ(Convert.ToSingle(this.ᜑ(array2[1])));
							ᜂ.ᜂ(Convert.ToSingle(this.ᜑ(array2[2])));
							num = 10;
							continue;
						case 4:
							ᜂ.ᜀ(Convert.ToSingle(this.ᜑ(array2[0])));
							ᜂ.ᜃ(Convert.ToSingle(this.ᜑ(array2[1])));
							ᜂ.ᜂ(Convert.ToSingle(this.ᜑ(array2[2])));
							ᜂ.ᜅ(Convert.ToSingle(this.ᜑ(array2[3])));
							num = 21;
							continue;
						default:
							num = 53;
							continue;
						}
						break;
					}
					case 56:
						goto IL_1374;
					case 57:
						goto IL_1374;
					case 58:
						spr᧓.\u17CA = new Dictionary<string, int>(40)
						{
							{
								ClipboardData.b("ṷᕹቻ੽굿", a_),
								0
							},
							{
								ClipboardData.b("ṷᕹቻ੽굿ﾅ", a_),
								1
							},
							{
								ClipboardData.b("ṷᕹቻ੽굿", a_),
								2
							},
							{
								ClipboardData.b("ṷᕹቻ੽굿ﲅ", a_),
								3
							},
							{
								ClipboardData.b("ၷόᕻ᥽", a_),
								4
							},
							{
								ClipboardData.b("ᑷ፹ቻ᭽굿", a_),
								5
							},
							{
								ClipboardData.b("౷όѻ੽굿", a_),
								6
							},
							{
								ClipboardData.b("౷όѻ੽굿揄憐﶑望", a_),
								7
							},
							{
								ClipboardData.b("᭷ᕹၻᅽ", a_),
								8
							},
							{
								ClipboardData.b("᩷᭹ύᕽ", a_),
								9
							},
							{
								ClipboardData.b("᩷᭹ύᕽꆋﾏﺑﮓ", a_),
								10
							},
							{
								ClipboardData.b("ᕷ᭹๻᥽ꦃ", a_),
								11
							},
							{
								ClipboardData.b("౷όѻ੽굿", a_),
								12
							},
							{
								ClipboardData.b("ᕷ᭹๻᥽ꦃ揄", a_),
								13
							},
							{
								ClipboardData.b("ᕷ᭹๻᥽ꦃ憎", a_),
								14
							},
							{
								ClipboardData.b("ᕷ᭹๻᥽ꦃﺉﶏ", a_),
								15
							},
							{
								ClipboardData.b("ᕷ᭹๻᥽", a_),
								16
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃﺉﶏ", a_),
								17
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ憎", a_),
								18
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ", a_),
								19
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ揄", a_),
								20
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃﲍ", a_),
								21
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃꎍ﶑秊", a_),
								22
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ揄붏ﮓ歹", a_),
								23
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ憎ꆋﾏﺑﮓ", a_),
								24
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃﺉﶏ뾑秊", a_),
								25
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ", a_),
								26
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃꎍﮑ", a_),
								27
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ揄붏ﶓ", a_),
								28
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ憎ꆋ轢憐ﺕ", a_),
								29
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃﺉﶏ뾑ﾕﲗ", a_),
								30
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃﲇ", a_),
								31
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃꎍ歹ﶗ", a_),
								32
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ揄붏ﾙ", a_),
								33
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃ憎ꆋﶍ", a_),
								34
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽ꦃﺉﶏ뾑鍊", a_),
								35
							},
							{
								ClipboardData.b("᩷ᕹ๻᩽", a_),
								36
							},
							{
								ClipboardData.b("ࡷ᭹᭻᭽굿ꆋﮓﶗ", a_),
								37
							},
							{
								ClipboardData.b("ࡷ᭹᭻᭽굿ꆋ", a_),
								38
							},
							{
								ClipboardData.b("ࡷ᭹᡻᩽", a_),
								39
							}
						};
						num = 59;
						continue;
					case 59:
						goto IL_EE0;
					case 60:
						goto IL_1374;
					case 61:
					{
						string text2;
						if (text2 == ClipboardData.b("᝷᡹ၻ᝽", a_))
						{
							num = 91;
							continue;
						}
						goto IL_936;
					}
					case 62:
					{
						string text2;
						ᜂ.ᜃ(Convert.ToSingle(this.ᜑ(text2)));
						num = 22;
						continue;
					}
					case 63:
						ᜂ.ᜁ(10f);
						num = 9;
						continue;
					case 64:
						goto IL_1374;
					case 65:
					{
						string text2;
						if (text2.ToLower() != ClipboardData.b("᥷ཹࡻᅽ", a_))
						{
							num = 69;
							continue;
						}
						ᜂ.ᜅ(-1f);
						num = 23;
						continue;
					}
					case 66:
						ᜂ.ᜀ(true);
						num = 93;
						continue;
					case 67:
					{
						string text3;
						string key;
						if ((key = text3) != null)
						{
							num = 94;
							continue;
						}
						goto IL_1374;
					}
					case 68:
					{
						int num3;
						if (num2 >= num3 - 1)
						{
							num = 82;
							continue;
						}
						string[] array;
						string text3 = array[num2].ToLower().Trim();
						string text2 = array[num2 + 1].ToLower().Trim();
						num = 67;
						continue;
					}
					case 69:
					{
						string text2;
						ᜂ.ᜅ(Convert.ToSingle(this.ᜑ(text2)));
						num = 75;
						continue;
					}
					case 70:
						goto IL_1374;
					case 71:
						goto IL_1374;
					case 72:
						goto IL_1374;
					case 73:
						goto IL_1374;
					case 74:
					{
						string key;
						int num6;
						if (spr᧓.\u17CA.TryGetValue(key, out num6))
						{
							num = 26;
							continue;
						}
						goto IL_1374;
					}
					case 75:
						goto IL_1374;
					case 76:
						goto IL_1374;
					case 77:
					{
						string text2;
						if (text2 == ClipboardData.b("᥷ᙹ୻ώ勵", a_))
						{
							num = 3;
							continue;
						}
						goto IL_1374;
					}
					case 78:
						goto IL_3D1;
					case 79:
						goto IL_1374;
					case 80:
						goto IL_1374;
					case 81:
						goto IL_1374;
					case 82:
						return true;
					case 83:
						goto IL_1374;
					case 84:
						goto IL_1374;
					case 85:
						if (spr᧓.\u17CA == null)
						{
							num = 58;
							continue;
						}
						goto IL_EE0;
					case 86:
					{
						int num6;
						switch (num6)
						{
						case 0:
						{
							string text2;
							ᜂ.ᜀ(text2);
							num = 97;
							continue;
						}
						case 1:
							num = 20;
							continue;
						case 2:
							ᜂ.ᜄ(true);
							num = 15;
							continue;
						case 3:
							num = 13;
							continue;
						case 4:
						case 5:
							num = 90;
							continue;
						case 6:
						{
							string text2;
							ᜂ.ᜀ(this.ᜀ(text2, ᜂ.ᜌ()));
							num = 19;
							continue;
						}
						case 7:
							num = 51;
							continue;
						case 8:
						{
							string text2;
							ᜂ.ᜀ(spr᱈.ᜀ(text2));
							num = 81;
							continue;
						}
						case 9:
						case 10:
						{
							string a = A_0.Name.ToLower();
							num = 6;
							continue;
						}
						case 11:
						{
							string text2;
							ᜂ.ᜀ(Convert.ToSingle(this.ᜑ(text2)));
							num = 99;
							continue;
						}
						case 12:
						{
							string text2;
							ᜂ.ᜆ(Convert.ToSingle(this.ᜑ(text2)));
							num = 8;
							continue;
						}
						case 13:
						{
							string text2;
							ᜂ.ᜂ(Convert.ToSingle(this.ᜑ(text2)));
							num = 56;
							continue;
						}
						case 14:
							num = 48;
							continue;
						case 15:
							num = 65;
							continue;
						case 16:
						{
							char[] separator;
							string text2;
							array2 = text2.Split(separator);
							int num7 = array2.Length;
							int num4 = num7;
							num = 12;
							continue;
						}
						case 17:
						{
							string text2;
							this.ᜀ(text2, ᜂ.\u1715.ᜄ, ᜂ.\u1715.ᜌ, ᜂ.\u1715.ᜈ);
							num = 57;
							continue;
						}
						case 18:
						{
							string text2;
							this.ᜀ(text2, ᜂ.\u1715.ᜃ, ᜂ.\u1715.ᜋ, ᜂ.\u1715.ᜇ);
							num = 52;
							continue;
						}
						case 19:
						{
							string text2;
							this.ᜀ(text2, ᜂ.\u1715.ᜅ, ᜂ.\u1715.\u170D, ᜂ.\u1715.ᜉ);
							num = 16;
							continue;
						}
						case 20:
						{
							string text2;
							this.ᜀ(text2, ᜂ.\u1715.ᜆ, ᜂ.\u1715.ᜎ, ᜂ.\u1715.ᜊ);
							num = 73;
							continue;
						}
						case 21:
						{
							string text2;
							ᜂ.\u1715.ᜀ = spr᱈.ᜀ(text2);
							num = 72;
							continue;
						}
						case 22:
						{
							string text2;
							ᜂ.\u1715.ᜅ = spr᱈.ᜀ(text2);
							num = 38;
							continue;
						}
						case 23:
						{
							string text2;
							ᜂ.\u1715.ᜆ = spr᱈.ᜀ(text2);
							num = 49;
							continue;
						}
						case 24:
						{
							string text2;
							ᜂ.\u1715.ᜃ = spr᱈.ᜀ(text2);
							num = 14;
							continue;
						}
						case 25:
						{
							string text2;
							ᜂ.\u1715.ᜄ = spr᱈.ᜀ(text2);
							num = 95;
							continue;
						}
						case 26:
						{
							string text2;
							ᜂ.\u1715.ᜁ = this.ᜆ(text2);
							num = 79;
							continue;
						}
						case 27:
						{
							string text2;
							ᜂ.\u1715.\u170D = this.ᜆ(text2);
							num = 87;
							continue;
						}
						case 28:
						{
							string text2;
							ᜂ.\u1715.ᜎ = this.ᜆ(text2);
							num = 27;
							continue;
						}
						case 29:
						{
							string text2;
							ᜂ.\u1715.ᜋ = this.ᜆ(text2);
							num = 80;
							continue;
						}
						case 30:
						{
							string text2;
							ᜂ.\u1715.ᜌ = this.ᜆ(text2);
							num = 96;
							continue;
						}
						case 31:
						{
							string text2;
							ᜂ.\u1715.ᜂ = this.ᜄ(text2);
							num = 76;
							continue;
						}
						case 32:
						{
							string text2;
							ᜂ.\u1715.ᜉ = this.ᜄ(text2);
							num = 30;
							continue;
						}
						case 33:
						{
							string text2;
							ᜂ.\u1715.ᜊ = this.ᜄ(text2);
							num = 40;
							continue;
						}
						case 34:
						{
							string text2;
							ᜂ.\u1715.ᜇ = this.ᜄ(text2);
							num = 60;
							continue;
						}
						case 35:
						{
							string text2;
							ᜂ.\u1715.ᜈ = this.ᜄ(text2);
							num = 36;
							continue;
						}
						case 36:
						{
							string text2;
							this.ᜀ(text2, ᜂ.\u1715.ᜆ, ᜂ.\u1715.ᜎ, ᜂ.\u1715.ᜊ);
							this.ᜀ(text2, ᜂ.\u1715.ᜅ, ᜂ.\u1715.\u170D, ᜂ.\u1715.ᜉ);
							this.ᜀ(text2, ᜂ.\u1715.ᜄ, ᜂ.\u1715.ᜌ, ᜂ.\u1715.ᜈ);
							this.ᜀ(text2, ᜂ.\u1715.ᜃ, ᜂ.\u1715.ᜋ, ᜂ.\u1715.ᜇ);
							num = 43;
							continue;
						}
						case 37:
							num = 77;
							continue;
						case 38:
							num = 11;
							continue;
						case 39:
						{
							char[] separator;
							string text2;
							array2 = text2.Split(separator);
							int num8 = array2.Length;
							int num5 = num8;
							num = 55;
							continue;
						}
						default:
							num = 98;
							continue;
						}
						break;
					}
					case 87:
						goto IL_1374;
					case 88:
					{
						string text2;
						ᜂ.ᜁ(spr᱈.ᜀ(text2));
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_BD7;
						default:
							if (false)
							{
							}
							num = 33;
							continue;
						}
						break;
					}
					case 89:
						goto IL_1374;
					case 90:
					{
						string text2;
						if (text2 != ClipboardData.b("ᙷᕹ๻፽", a_))
						{
							num = 18;
							continue;
						}
						ᜂ.ᜁ(true);
						num = 71;
						continue;
					}
					case 91:
						goto IL_882;
					case 92:
						this.ᜠ = true;
						num = 44;
						continue;
					case 93:
						goto IL_6D3;
					case 94:
						num = 85;
						continue;
					case 95:
						goto IL_1374;
					case 96:
						goto IL_1374;
					case 97:
						goto IL_1374;
					case 98:
						num = 54;
						continue;
					case 99:
						goto IL_1374;
					}
					break;
					IL_3D1:
					num = 68;
					continue;
					IL_6D3:
					num = 46;
					continue;
					IL_882:
					ᜂ.ᜂ(true);
					num = 50;
					continue;
					IL_936:
					num = 7;
					continue;
					IL_BD7:
					ᜂ.ᜀ(Convert.ToSingle(this.ᜑ(array2[0])));
					ᜂ.ᜃ(Convert.ToSingle(this.ᜑ(array2[1])));
					ᜂ.ᜂ(Convert.ToSingle(this.ᜑ(array2[2])));
					num = 24;
					continue;
					IL_EE0:
					num = 74;
					continue;
					IL_1374:
					num2 += 2;
					num = 42;
				}
			}
			return true;
		}
	}

	// Token: 0x06003544 RID: 13636 RVA: 0x00319E40 File Offset: 0x00318E40
	private void ᜀ(string A_0, Color A_1, float A_2, BorderStyle A_3)
	{
		int a_ = 16;
		switch (0)
		{
		default:
			for (;;)
			{
				string[] array = new string[]
				{
					ClipboardData.b("ት᥷ॹᑻ᭽", a_),
					ClipboardData.b("ት᝷๹ࡻ᭽", a_),
					ClipboardData.b("ት᝷ཹṻች", a_),
					ClipboardData.b("ᅵ੷ᕹ፻ࡽ", a_),
					ClipboardData.b("ήᙷॹ᥻੽", a_),
					ClipboardData.b("᥵൷๹ཻ᭽", a_),
					ClipboardData.b("ѵᅷṹ᭻᭽", a_),
					ClipboardData.b("յ᝷ᙹᕻ᩽", a_),
					ClipboardData.b("ṵᅷṹ᡻᭽", a_)
				};
				string[] array2 = A_0.Split(new char[]
				{
					' '
				});
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						goto IL_2E6;
					case 1:
						num2 = 11;
						continue;
					case 2:
						if (!array2[num].StartsWith(ClipboardData.b("啵", a_)))
						{
							num2 = 18;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_263;
						default:
							if (false)
							{
							}
							num2 = 16;
							continue;
						}
						break;
					case 3:
						goto IL_2D2;
					case 4:
						goto IL_2E6;
					case 5:
						goto IL_271;
					case 6:
						goto IL_271;
					case 7:
					{
						string[] array3;
						if (num3 >= array3.Length)
						{
							num2 = 1;
							continue;
						}
						string b = array3[num3];
						num2 = 17;
						continue;
					}
					case 8:
						goto IL_2D2;
					case 9:
						goto IL_172;
					case 10:
						if (num >= array2.Length)
						{
							num2 = 19;
							continue;
						}
						num2 = 2;
						continue;
					case 11:
						if (this.ᜡ)
						{
							num2 = 14;
							continue;
						}
						A_1 = spr᱈.ᜀ(array2[num]);
						num2 = 3;
						continue;
					case 12:
						this.ᜡ = true;
						num2 = 9;
						continue;
					case 13:
						A_2 = this.ᜆ(array2[num]);
						num2 = 20;
						continue;
					case 14:
						A_3 = this.ᜄ(array2[num]);
						this.ᜡ = false;
						goto IL_263;
					case 15:
						goto IL_2D2;
					case 16:
					{
						array2[num] = array2[num].Replace(ClipboardData.b("啵", a_), string.Empty);
						int red = int.Parse(array2[num].Substring(0, 2), NumberStyles.AllowHexSpecifier);
						int green = int.Parse(array2[num].Substring(2, 2), NumberStyles.AllowHexSpecifier);
						int blue = int.Parse(array2[num].Substring(4, 2), NumberStyles.AllowHexSpecifier);
						A_1 = Color.FromArgb(red, green, blue);
						num2 = 8;
						continue;
					}
					case 17:
					{
						string b;
						if (array2[num] == b)
						{
							num2 = 12;
							continue;
						}
						goto IL_172;
					}
					case 18:
					{
						if (this.ᜇ(array2[num]))
						{
							num2 = 13;
							continue;
						}
						string[] array3 = array;
						num3 = 0;
						num2 = 0;
						continue;
					}
					case 19:
						return;
					case 20:
						goto IL_2D2;
					}
					break;
					IL_172:
					num3++;
					num2 = 4;
					continue;
					IL_263:
					num2 = 15;
					continue;
					IL_271:
					if (true)
					{
					}
					num2 = 10;
					continue;
					IL_2D2:
					num++;
					num2 = 5;
					continue;
					IL_2E6:
					num2 = 7;
				}
			}
			return;
		}
	}

	// Token: 0x06003545 RID: 13637 RVA: 0x0031A204 File Offset: 0x00319204
	private bool ᜇ(string A_0)
	{
		int a_ = 4;
		int num = 13;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 7;
				continue;
			case 1:
				if (!A_0.EndsWith(ClipboardData.b("ͩɫ", a_)))
				{
					num = 2;
					continue;
				}
				return true;
			case 2:
				num = 5;
				continue;
			case 3:
				goto IL_23C;
			case 4:
				num = 1;
				continue;
			case 5:
				if (!A_0.EndsWith(ClipboardData.b("ཀྵū", a_)))
				{
					num = 16;
					continue;
				}
				return true;
			case 6:
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
					num = 14;
					continue;
				}
				break;
			case 7:
				if (!A_0.EndsWith(ClipboardData.b("ᩩᑫ", a_)))
				{
					num = 4;
					continue;
				}
				return true;
			case 8:
				if (!(A_0 == ClipboardData.b("ݩ५੭᥯ݱᥳ", a_)))
				{
					num = 9;
					continue;
				}
				return true;
			case 9:
				num = 12;
				continue;
			case 10:
				num = 11;
				continue;
			case 11:
				if (A_0.EndsWith(ClipboardData.b("ᩩཫ", a_)))
				{
					num = 3;
					continue;
				}
				num = 8;
				continue;
			case 12:
				if (!(A_0 == ClipboardData.b("ṩѫݭ፯ᥱ", a_)))
				{
					num = 6;
					continue;
				}
				return true;
			case 14:
				if (A_0 == ClipboardData.b("ṩѫݭṯ", a_))
				{
					num = 17;
					continue;
				}
				return false;
			case 15:
				if (!A_0.EndsWith(ClipboardData.b("३ū", a_)))
				{
					num = 10;
					continue;
				}
				return true;
			case 16:
				num = 15;
				continue;
			case 17:
				return true;
			}
			IL_61:
			if (!A_0.EndsWith(ClipboardData.b("ᩩᡫ", a_)))
			{
				num = 0;
				continue;
			}
			break;
			goto IL_61;
		}
		return true;
		IL_23C:
		return true;
	}

	// Token: 0x06003546 RID: 13638 RVA: 0x0031A454 File Offset: 0x00319454
	private float ᜆ(string A_0)
	{
		int a_ = 16;
		float result;
		for (;;)
		{
			IL_6D:
			if (true)
			{
			}
			result = 0f;
			for (;;)
			{
				IL_7B:
				int num = 13;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						goto IL_2B3;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_7B;
						default:
							if (false)
							{
							}
							result = 4.5f;
							num = 18;
							continue;
						}
						break;
					case 3:
						if (!A_0.EndsWith(ClipboardData.b("ٵw", a_)))
						{
							num = 8;
							continue;
						}
						goto IL_2B3;
					case 4:
						return result;
					case 5:
						if (A_0 == ClipboardData.b("ɵၷ፹ύᕽ", a_))
						{
							num = 2;
							continue;
						}
						return result;
					case 6:
						num = 3;
						continue;
					case 7:
						num = 11;
						continue;
					case 8:
						num = 10;
						continue;
					case 9:
						if (A_0 == ClipboardData.b("᭵ᵷṹᕻ୽", a_))
						{
							num = 12;
							continue;
						}
						num = 5;
						continue;
					case 10:
						if (!A_0.EndsWith(ClipboardData.b("፵ᕷ", a_)))
						{
							num = 22;
							continue;
						}
						goto IL_2B3;
					case 11:
						if (A_0.EndsWith(ClipboardData.b("ٵ᭷", a_)))
						{
							num = 1;
							continue;
						}
						num = 21;
						continue;
					case 12:
						result = 3f;
						num = 16;
						continue;
					case 13:
						if (!A_0.EndsWith(ClipboardData.b("ٵ౷", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_2B3;
					case 14:
						result = 0.75f;
						num = 0;
						continue;
					case 15:
						return result;
					case 16:
						return result;
					case 17:
					{
						string[] array = this.ᜅ(A_0);
						num = 19;
						continue;
					}
					case 18:
						return result;
					case 19:
					{
						string[] array;
						if (array[1] == null)
						{
							num = 14;
							continue;
						}
						result = Convert.ToSingle(this.ᜑ(A_0));
						num = 15;
						continue;
					}
					case 20:
						if (!A_0.EndsWith(ClipboardData.b("ᕵᕷ", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_2B3;
					case 21:
						if (A_0.EndsWith(ClipboardData.b("ήᙷ", a_)))
						{
							num = 17;
							continue;
						}
						num = 9;
						continue;
					case 22:
						num = 20;
						continue;
					}
					goto IL_6D;
					IL_2B3:
					result = Convert.ToSingle(this.ᜑ(A_0));
					num = 4;
				}
			}
		}
		return result;
	}

	// Token: 0x06003547 RID: 13639 RVA: 0x0031A730 File Offset: 0x00319730
	private string[] ᜅ(string A_0)
	{
		switch (0)
		{
		default:
		{
			string[] array;
			for (;;)
			{
				IL_0E:
				for (;;)
				{
					array = new string[2];
					int num = 0;
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_A1;
						case 1:
							goto IL_4B;
						case 2:
						{
							if (true)
							{
							}
							string[] array2;
							char c;
							(array2 = array)[1] = array2[1] + c;
							num2 = 4;
							continue;
						}
						case 3:
						{
							char c;
							if (char.IsDigit(c))
							{
								num2 = 2;
								continue;
							}
							string[] array3;
							(array3 = array)[0] = array3[0] + c;
							num2 = 1;
							continue;
						}
						case 4:
							goto IL_4B;
						case 5:
							goto IL_A1;
						case 6:
							return array;
						case 7:
						{
							if (num >= A_0.Length)
							{
								num2 = 6;
								continue;
							}
							char c = A_0[num];
							num2 = 3;
							continue;
						}
						}
						break;
						IL_4B:
						num++;
						num2 = 5;
						continue;
						IL_A1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_0E;
						default:
							if (false)
							{
							}
							num2 = 7;
							break;
						}
					}
				}
			}
			return array;
		}
		}
	}

	// Token: 0x06003548 RID: 13640 RVA: 0x0031A854 File Offset: 0x00319854
	private BorderStyle ᜄ(string A_0)
	{
		int a_ = 12;
		int num = 8;
		for (;;)
		{
			string key;
			switch (num)
			{
			case 0:
				goto IL_60;
			case 1:
				if (spr᧓.\u17CB == null)
				{
					num = 2;
					continue;
				}
				goto IL_60;
			case 2:
				spr᧓.\u17CB = new Dictionary<string, int>(10)
				{
					{
						ClipboardData.b("ᙱᕳյၷό᡻", a_),
						0
					},
					{
						ClipboardData.b("ᙱ᭳ɵ౷ό᡻", a_),
						1
					},
					{
						ClipboardData.b("ᙱ᭳͵᩷ᙹ᥻", a_),
						2
					},
					{
						ClipboardData.b("ᕱٳ᥵᝷౹᥻", a_),
						3
					},
					{
						ClipboardData.b("᭱ᩳյᵷ๹", a_),
						4
					},
					{
						ClipboardData.b("ᵱųɵ୷όࡻ", a_),
						5
					},
					{
						ClipboardData.b("qᵳትίό", a_),
						6
					},
					{
						ClipboardData.b("ű᭳᩵ᅷṹ", a_),
						7
					},
					{
						ClipboardData.b("ᱱ᭳ᡵᵷ", a_),
						8
					},
					{
						ClipboardData.b("ᩱᵳትᱷόቻ", a_),
						9
					}
				};
				num = 0;
				continue;
			case 3:
				num = 1;
				continue;
			case 4:
				goto IL_200;
			case 5:
			{
				int num2;
				if (spr᧓.\u17CB.TryGetValue(key, out num2))
				{
					num = 9;
					continue;
				}
				return BorderStyle.None;
			}
			case 6:
				num = 4;
				continue;
			case 7:
			{
				int num2;
				switch (num2)
				{
				case 0:
					return BorderStyle.DashLargeGap;
				case 1:
					return BorderStyle.Dot;
				case 2:
					return BorderStyle.Double;
				case 3:
					return BorderStyle.Engrave3D;
				case 4:
					return BorderStyle.Inset;
				case 5:
					return BorderStyle.Outset;
				case 6:
					return BorderStyle.Emboss3D;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_184;
					default:
						goto IL_1E0;
					}
					break;
				case 8:
				case 9:
					return BorderStyle.None;
				default:
					num = 6;
					continue;
				}
				break;
			}
			case 9:
				goto IL_184;
			}
			if ((key = A_0.ToLower()) != null)
			{
				num = 3;
				continue;
			}
			return BorderStyle.None;
			IL_60:
			num = 5;
			continue;
			IL_184:
			num = 7;
		}
		return BorderStyle.Inset;
		IL_1E0:
		if (false)
		{
		}
		return BorderStyle.Single;
		IL_200:
		if (true)
		{
		}
		return BorderStyle.None;
	}

	// Token: 0x06003549 RID: 13641 RVA: 0x0031AA98 File Offset: 0x00319A98
	private void ᜀ(bool A_0)
	{
		if (true)
		{
		}
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					this.\u170D.Pop();
					num = 1;
					continue;
				}
				break;
			case 1:
				return;
			}
			IL_24:
			if (A_0)
			{
				num = 0;
				continue;
			}
			break;
			goto IL_24;
		}
	}

	// Token: 0x0600354A RID: 13642 RVA: 0x0031AB14 File Offset: 0x00319B14
	private void ᜄ(XmlNode A_0)
	{
		int a_ = 8;
		int num = 3;
		ParagraphFormat format;
		for (;;)
		{
			string a;
			switch (num)
			{
			case 0:
				num = 21;
				continue;
			case 1:
				if (this.\u171D == HorizontalAlignment.Center)
				{
					num = 9;
					continue;
				}
				num = 20;
				continue;
			case 2:
				num = 12;
				continue;
			case 4:
				num = 16;
				continue;
			case 5:
				if (!(a == ClipboardData.b("൭ᕯᱱs፵੷", a_)))
				{
					num = 18;
					continue;
				}
				goto IL_293;
			case 6:
				if (a != string.Empty)
				{
					num = 0;
					continue;
				}
				goto IL_89;
			case 7:
				num = 5;
				continue;
			case 8:
				if (this.\u1712.IsInCell)
				{
					num = 4;
					continue;
				}
				return;
			case 9:
				goto IL_293;
			case 10:
				goto IL_1D5;
			case 11:
				return;
			case 12:
				if (this.\u171D == HorizontalAlignment.Right)
				{
					num = 14;
					continue;
				}
				return;
			case 13:
				if (this.\u171D != HorizontalAlignment.Left)
				{
					num = 17;
					continue;
				}
				return;
			case 14:
				goto IL_D3;
			case 15:
				this.\u1712.Format.HorizontalAlignment = this.\u171E;
				num = 19;
				continue;
			case 16:
				if (this.\u171E != HorizontalAlignment.Left)
				{
					num = 15;
					continue;
				}
				goto IL_243;
			case 17:
				if (true)
				{
				}
				this.\u1712.Format.HorizontalAlignment = this.\u171D;
				num = 10;
				continue;
			case 18:
				num = 1;
				continue;
			case 19:
				goto IL_243;
			case 20:
				if (!(a == ClipboardData.b("ᱭ᥯ᕱᱳɵ", a_)))
				{
					num = 2;
					continue;
				}
				goto IL_266;
			case 21:
				if (A_0.Name.ToLower() != ClipboardData.b("ᩭᅯၱᡳ፵", a_))
				{
					goto IL_103;
				}
				goto IL_89;
			}
			if (this.\u1712 == null)
			{
				num = 11;
				continue;
			}
			format = this.\u1712.Format;
			a = this.ᜀ(A_0, ClipboardData.b("཭ᱯ᭱፳ᡵ", a_));
			num = 6;
			continue;
			IL_89:
			num = 8;
			continue;
			IL_103:
			num = 7;
			continue;
			IL_293:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_103;
			default:
				goto IL_2A9;
			}
			IL_243:
			num = 13;
		}
		return;
		IL_D3:
		goto IL_266;
		IL_1D5:
		return;
		IL_266:
		format.HorizontalAlignment = HorizontalAlignment.Right;
		return;
		IL_2A9:
		if (false)
		{
		}
		format.HorizontalAlignment = HorizontalAlignment.Center;
	}

	// Token: 0x0600354B RID: 13643 RVA: 0x0031ADD8 File Offset: 0x00319DD8
	private spr\u1DE8.ᜂ ᜁ()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 2:
				goto IL_66;
			case 3:
				goto IL_88;
			}
			IL_20:
			if (this.\u170D.Count <= 0)
			{
				num = 0;
				continue;
			}
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
				continue;
			}
			goto IL_20;
		}
		IL_66:
		if (true)
		{
		}
		spr\u1DE8.ᜂ ᜂ = this.\u170D.Peek().ᜈ();
		goto IL_8F;
		IL_88:
		ᜂ = new spr\u1DE8.ᜂ();
		IL_8F:
		spr\u1DE8.ᜂ ᜂ2 = ᜂ;
		this.\u170D.Push(ᜂ2);
		return ᜂ2;
	}

	// Token: 0x0600354C RID: 13644 RVA: 0x0031AE84 File Offset: 0x00319E84
	private string ᜀ(XmlNode A_0, string A_1)
	{
		XmlAttribute xmlAttribute;
		for (;;)
		{
			if (true)
			{
			}
			A_1 = A_1.ToLower();
			xmlAttribute = null;
			int num = 0;
			int num2 = 4;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_B1;
				case 1:
					if (xmlAttribute.LocalName.ToLower() == A_1)
					{
						goto IL_A4;
					}
					num++;
					num2 = 0;
					continue;
				case 2:
					goto IL_D5;
				case 3:
					if (num >= A_0.Attributes.Count)
					{
						num2 = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A4;
					default:
						if (false)
						{
						}
						xmlAttribute = A_0.Attributes[num];
						num2 = 1;
						continue;
					}
					break;
				case 4:
					goto IL_B1;
				case 5:
					goto IL_AF;
				}
				break;
				IL_A4:
				num2 = 5;
				continue;
				IL_B1:
				num2 = 3;
			}
		}
		IL_AF:
		return xmlAttribute.Value;
		IL_D5:
		return string.Empty;
	}

	// Token: 0x0600354D RID: 13645 RVA: 0x0031AF70 File Offset: 0x00319F70
	private double ᜀ(string A_0, float A_1)
	{
		int a_ = 3;
		switch (0)
		{
		default:
		{
			int num = 16;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.EndsWith(ClipboardData.b("hժ", a_)))
					{
						num = 28;
						continue;
					}
					num = 11;
					continue;
				case 1:
					if (A_0.EndsWith(ClipboardData.b("䱨", a_)))
					{
						num = 10;
						continue;
					}
					num = 13;
					continue;
				case 2:
					if (true)
					{
					}
					num = 25;
					continue;
				case 3:
					if (A_0.EndsWith(ClipboardData.b("౨፪", a_)))
					{
						num = 23;
						continue;
					}
					num = 29;
					continue;
				case 4:
					goto IL_32D;
				case 5:
					spr᧓.\u17CC = new Dictionary<string, int>(10)
					{
						{
							ClipboardData.b("ᅨ፪䁬ᱮᱰቲᥴ᭶", a_),
							0
						},
						{
							ClipboardData.b("ᅨ䙪Ṭɮၰὲᥴ", a_),
							1
						},
						{
							ClipboardData.b("ᩨ٪౬ͮᵰ", a_),
							2
						},
						{
							ClipboardData.b("Ѩ๪६ٮѰṲ", a_),
							3
						},
						{
							ClipboardData.b("ը੪Ὤ࡮ᑰ", a_),
							4
						},
						{
							ClipboardData.b("ᅨ䙪Ŭ๮Ͱᑲၴ", a_),
							5
						},
						{
							ClipboardData.b("ᅨ፪䁬ͮၰŲቴቶ", a_),
							6
						},
						{
							ClipboardData.b("ᩨ٪౬ͮᵰᙲݴ", a_),
							7
						},
						{
							ClipboardData.b("୨ɪ੬࡮ᑰŲ", a_),
							8
						},
						{
							ClipboardData.b("ը੪Ὤ࡮ᑰŲ", a_),
							9
						}
					};
					num = 30;
					continue;
				case 6:
					goto IL_634;
				case 7:
					if (A_0 != null)
					{
						num = 2;
						continue;
					}
					goto IL_564;
				case 8:
					if (A_0.EndsWith(ClipboardData.b("Ѩ٪", a_)))
					{
						num = 26;
						continue;
					}
					num = 22;
					continue;
				case 9:
					goto IL_514;
				case 10:
					goto IL_595;
				case 11:
					if (A_0.EndsWith(ClipboardData.b("੨٪", a_)))
					{
						num = 18;
						continue;
					}
					num = 8;
					continue;
				case 12:
					goto IL_1AC;
				case 13:
					if (A_0.EndsWith(ClipboardData.b("౨٪", a_)))
					{
						num = 17;
						continue;
					}
					num = 3;
					continue;
				case 14:
					A_1 = 3f;
					num = 9;
					continue;
				case 15:
				{
					int num2;
					if (spr᧓.\u17CC.TryGetValue(A_0, out num2))
					{
						num = 19;
						continue;
					}
					goto IL_564;
				}
				case 17:
					goto IL_10C;
				case 18:
					goto IL_4D2;
				case 19:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2F0;
					default:
						if (false)
						{
						}
						num = 27;
						continue;
					}
					break;
				case 20:
					if (A_0.EndsWith(ClipboardData.b("ᥨ፪", a_)))
					{
						num = 6;
						continue;
					}
					goto IL_662;
				case 21:
					goto IL_564;
				case 22:
					if (A_0.EndsWith(ClipboardData.b("ᥨࡪ", a_)))
					{
						num = 4;
						continue;
					}
					num = 20;
					continue;
				case 23:
					goto IL_3A0;
				case 24:
					goto IL_2F0;
				case 25:
					if (spr᧓.\u17CC == null)
					{
						num = 5;
						continue;
					}
					goto IL_14A;
				case 26:
					goto IL_5CB;
				case 27:
				{
					int num2;
					switch (num2)
					{
					case 0:
						goto IL_111;
					case 1:
						goto IL_1FF;
					case 2:
						goto IL_4E1;
					case 3:
						goto IL_2F2;
					case 4:
						goto IL_3A5;
					case 5:
						goto IL_4D7;
					case 6:
						goto IL_25B;
					case 7:
						goto IL_35B;
					case 8:
						goto IL_365;
					case 9:
						goto IL_4EB;
					default:
						num = 24;
						continue;
					}
					break;
				}
				case 28:
					goto IL_601;
				case 29:
					if (A_0.EndsWith(ClipboardData.b("ᥨὪ", a_)))
					{
						num = 12;
						continue;
					}
					num = 0;
					continue;
				case 30:
					goto IL_14A;
				}
				if (A_1 < 0f)
				{
					num = 14;
					continue;
				}
				goto IL_514;
				IL_14A:
				num = 15;
				continue;
				IL_2F0:
				num = 21;
				continue;
				IL_514:
				num = 7;
				continue;
				IL_564:
				num = 1;
			}
			IL_10C:
			return (double)(A_1 * this.ᜁ(A_0, ClipboardData.b("౨٪", a_)));
			IL_111:
			return 7.5;
			IL_1AC:
			return (double)this.ᜁ(A_0, ClipboardData.b("ᥨὪ", a_));
			IL_1FF:
			return 10.0;
			IL_25B:
			return 36.0;
			IL_2F2:
			return 13.5;
			IL_32D:
			Font font = new Font("", this.ᜁ(A_0, ClipboardData.b("ᥨࡪ", a_)) * 12f, GraphicsUnit.Point);
			return (double)font.SizeInPoints;
			IL_35B:
			return 10.0;
			IL_365:
			return 12.0;
			IL_3A0:
			return (double)(A_1 / 2f * this.ᜁ(A_0, ClipboardData.b("౨፪", a_)));
			IL_3A5:
			return 18.0;
			IL_4D2:
			font = new Font("", this.ᜁ(A_0, ClipboardData.b("੨٪", a_)) / 10f, GraphicsUnit.Millimeter);
			return (double)font.SizeInPoints;
			IL_4D7:
			return 24.0;
			IL_4E1:
			return 12.0;
			IL_4EB:
			return 13.5;
			IL_595:
			return (double)(A_1 * this.ᜁ(A_0, ClipboardData.b("䱨", a_)) / 100f);
			IL_5CB:
			font = new Font("", this.ᜁ(A_0, ClipboardData.b("Ѩ٪", a_)), GraphicsUnit.Millimeter);
			return (double)font.SizeInPoints;
			IL_601:
			font = new Font("", this.ᜁ(A_0, ClipboardData.b("hժ", a_)), GraphicsUnit.Inch);
			return (double)font.SizeInPoints;
			IL_634:
			font = new Font("", this.ᜁ(A_0, ClipboardData.b("ᥨ፪", a_)), GraphicsUnit.Pixel);
			return (double)font.SizeInPoints;
			IL_662:
			float emSize = float.Parse(A_0, CultureInfo.InvariantCulture);
			font = new Font("", emSize, GraphicsUnit.Pixel);
			return (double)font.SizeInPoints;
		}
		}
	}

	// Token: 0x0600354E RID: 13646 RVA: 0x0031B600 File Offset: 0x0031A600
	private HorizontalAlignment ᜀ(string A_0, HorizontalAlignment A_1)
	{
		int a_ = 17;
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				num = 5;
				continue;
			case 2:
				if (!(A_0 == ClipboardData.b("᭶ᱸᵺॼ", a_)))
				{
					num = 7;
					continue;
				}
				return HorizontalAlignment.Left;
			case 3:
				num = 2;
				continue;
			case 4:
				if (!(A_0 == ClipboardData.b("ᵶ౸ࡺॼᙾ廒", a_)))
				{
					num = 3;
					continue;
				}
				return HorizontalAlignment.Justify;
			case 5:
				if (!(A_0 == ClipboardData.b("ᑶᱸᕺॼ᩾", a_)))
				{
					num = 6;
					continue;
				}
				return HorizontalAlignment.Center;
			case 6:
				if (true)
				{
				}
				num = 4;
				continue;
			case 7:
				num = 8;
				continue;
			case 8:
				goto IL_FE;
			case 9:
				goto IL_87;
			case 10:
				num = 9;
				continue;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_FE:
				if (A_0 == ClipboardData.b("նၸᱺᕼ୾", a_))
				{
					return HorizontalAlignment.Right;
				}
				num = 10;
				break;
			default:
				if (false)
				{
				}
				if (A_0 == null)
				{
					return A_1;
				}
				num = 1;
				break;
			}
		}
		return HorizontalAlignment.Justify;
		IL_87:
		return A_1;
	}

	// Token: 0x0600354F RID: 13647 RVA: 0x0031B764 File Offset: 0x0031A764
	private float ᜁ(string A_0, string A_1)
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
		A_0 = A_0.Substring(0, A_0.IndexOf(A_1));
		return float.Parse(A_0, CultureInfo.InvariantCulture);
	}

	// Token: 0x06003550 RID: 13648 RVA: 0x0031B7BC File Offset: 0x0031A7BC
	private void ᜀ(object A_0, ValidationEventArgs A_1)
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
		throw new NotSupportedException(A_1.Message, A_1.Exception);
	}

	// Token: 0x06003551 RID: 13649 RVA: 0x0031B808 File Offset: 0x0031A808
	private void ᜂ(ListPatternType A_0, XmlNode A_1)
	{
		int a_ = 2;
		int num = 8;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_A7;
			case 1:
				goto IL_183;
			case 2:
				goto IL_143;
			case 3:
				if (this.ᜉ() != null)
				{
					num = 6;
					continue;
				}
				goto IL_1B6;
			case 4:
				goto IL_59;
			case 5:
			{
				this.ᜊ().Pop();
				ListStyle item = this.ᜀ(A_0, A_1);
				this.ᜊ().Push(item);
				num = 0;
				continue;
			}
			case 6:
				num = 7;
				continue;
			case 7:
				if (string.IsNullOrEmpty(this.ᜉ().Name))
				{
					goto IL_1B6;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_143;
				default:
					if (false)
					{
					}
					num = 2;
					continue;
				}
				break;
			case 9:
				if (this.\u1712.Document.ListStyles.FindByName(this.ᜉ().Name) != null)
				{
					num = 1;
					continue;
				}
				goto IL_1B6;
			case 10:
				if (!string.IsNullOrEmpty(this.ᜀ(A_1, ClipboardData.b("㹧⭩⁫㭭㕯", a_))))
				{
					num = 5;
					continue;
				}
				goto IL_A7;
			}
			if (this.ᜂ(A_1))
			{
				num = 4;
				continue;
			}
			num = 10;
			continue;
			IL_A7:
			num = 3;
			continue;
			IL_143:
			if (true)
			{
			}
			num = 9;
		}
		IL_59:
		this.ᜁ(A_0, A_1);
		return;
		IL_183:
		this.\u1712.ListFormat.ApplyStyle(this.ᜉ().Name);
		this.\u1712.ListFormat.ListLevelNumber = this.\u1717;
		return;
		IL_1B6:
		this.ᜁ(A_0, A_1);
	}

	// Token: 0x06003552 RID: 13650 RVA: 0x0031B9D4 File Offset: 0x0031A9D4
	private void ᜁ(ListPatternType A_0, XmlNode A_1)
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
		this.\u1717++;
		ListStyle listStyle = this.ᜀ(A_0, A_1);
		this.\u1712.ListFormat.ApplyStyle(listStyle.Name);
		this.\u1712.ListFormat.ListLevelNumber = this.\u1717;
		this.\u1712.ListFormat.IsRestartNumbering = true;
		this.ᜊ().Push(listStyle);
	}

	// Token: 0x06003553 RID: 13651 RVA: 0x0031BA70 File Offset: 0x0031AA70
	private ListStyle ᜀ(ListPatternType A_0, XmlNode A_1)
	{
		int a_ = 6;
		switch (0)
		{
		default:
			for (;;)
			{
				IL_52:
				int num;
				ListStyle listStyle;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_1FB:
					num = 0;
					break;
				default:
					if (false)
					{
					}
					listStyle = null;
					num = 9;
					break;
				}
				string styleName;
				for (;;)
				{
					ListLevel listLevel;
					string text;
					string text2;
					switch (num)
					{
					case 0:
						goto IL_209;
					case 1:
						goto IL_B2;
					case 2:
						try
						{
							listLevel.StartAt = Convert.ToInt32(text);
							return listStyle;
						}
						catch
						{
							listLevel.StartAt = this.ᜀ(text, this.ᜀ(A_1.ParentNode, ClipboardData.b("㡫㝭⁯㝱", a_)));
							return listStyle;
						}
						goto IL_F2;
					case 3:
						if (!string.IsNullOrEmpty(text2))
						{
							num = 1;
							continue;
						}
						return listStyle;
					case 4:
						if (A_0 == ListPatternType.Bullet)
						{
							if (true)
							{
							}
							num = 7;
							continue;
						}
						styleName = ClipboardData.b("≫᭭ᵯၱᅳѵᵷṹ⍻", a_) + this.\u1712.Document.ListStyles.Count.ToString();
						listStyle = this.\u1712.Document.AddListStyle(ListType.Numbered, styleName);
						num = 5;
						continue;
					case 5:
						goto IL_209;
					case 6:
						if (!string.IsNullOrEmpty(text))
						{
							num = 2;
							continue;
						}
						num = 3;
						continue;
					case 7:
						goto IL_117;
					case 8:
						goto IL_8C;
					case 9:
						if (this.ᜬ != null)
						{
							num = 8;
							continue;
						}
						goto IL_F2;
					}
					goto IL_52;
					IL_F2:
					num = 4;
					continue;
					IL_209:
					listLevel = listStyle.Levels[this.\u1717];
					listLevel.PatternType = A_0;
					listLevel.TabSpaceAfter = 10f;
					listLevel.FollowCharacter = FollowCharacterType.Tab;
					text = this.ᜀ(A_1, ClipboardData.b("㩫⽭㱯❱ㅳ", a_));
					text2 = this.ᜀ(A_1.ParentNode, ClipboardData.b("㽫㩭ㅯⁱ⁳", a_));
					num = 6;
				}
				IL_117:
				styleName = ClipboardData.b("⹫᭭ᱯṱᅳɵᵷṹ⍻", a_) + this.\u1712.Document.ListStyles.Count.ToString();
				listStyle = this.\u1712.Document.AddListStyle(ListType.Bulleted, styleName);
				goto IL_1FB;
			}
			IL_8C:
			goto IL_15C;
			IL_B2:
			try
			{
				ListLevel listLevel;
				string text2;
				listLevel.StartAt = Convert.ToInt32(text2);
				ListStyle listStyle;
				return listStyle;
			}
			catch
			{
				ListLevel listLevel;
				string text2;
				listLevel.StartAt = this.ᜀ(text2, this.ᜀ(A_1.ParentNode, ClipboardData.b("㡫㝭⁯㝱", a_)));
				ListStyle listStyle;
				return listStyle;
			}
			IL_15C:
			return this.ᜬ;
		}
	}

	// Token: 0x06003554 RID: 13652 RVA: 0x0031BD28 File Offset: 0x0031AD28
	private int ᜀ(string A_0, string A_1)
	{
		int a_ = 14;
		int num = 10;
		byte b;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_107;
			case 1:
				num = 9;
				continue;
			case 2:
				if (A_1 == ClipboardData.b("㵳", a_))
				{
					num = 4;
					continue;
				}
				b = (byte)A_0.ToCharArray()[0];
				num = 3;
				continue;
			case 3:
				if (b >= 65)
				{
					num = 11;
					continue;
				}
				goto IL_85;
			case 4:
				goto IL_E0;
			case 5:
				goto IL_127;
			case 6:
				goto IL_83;
			case 7:
				if (b <= 90)
				{
					num = 5;
					continue;
				}
				goto IL_85;
			case 8:
				if (b >= 97)
				{
					num = 1;
					continue;
				}
				return 1;
			case 9:
				if (b <= 122)
				{
					num = 0;
					continue;
				}
				return 1;
			case 11:
				num = 7;
				continue;
			}
			if (A_1 == ClipboardData.b("ᵳ", a_))
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (false)
				{
				}
				num = 6;
				continue;
			}
			IL_83:
			num = 2;
			continue;
			IL_85:
			num = 8;
		}
		IL_A7:
		return this.ᜀ(A_0);
		IL_E0:
		goto IL_A7;
		IL_107:
		return (int)(b - 96);
		IL_127:
		if (true)
		{
		}
		return (int)(b - 64);
	}

	// Token: 0x06003555 RID: 13653 RVA: 0x0031BE94 File Offset: 0x0031AE94
	private bool ᜃ(XmlNode A_0)
	{
		int a_ = 14;
		for (;;)
		{
			if (true)
			{
			}
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return true;
				case 1:
					if (A_0.NextSibling.Name == ClipboardData.b("坳ŵၷ፹ࡻ᭽", a_))
					{
						num = 3;
						continue;
					}
					return false;
				case 2:
					goto IL_7D;
				case 3:
					A_0 = A_0.NextSibling;
					num = 2;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return true;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				}
				if (A_0.NextSibling == null)
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
			}
			IL_7D:;
		}
		return true;
	}

	// Token: 0x06003556 RID: 13654 RVA: 0x0031BF58 File Offset: 0x0031AF58
	private bool ᜂ(XmlNode A_0)
	{
		int a_ = 2;
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return true;
				case 1:
					A_0 = A_0.PreviousSibling;
					num = 2;
					continue;
				case 2:
					goto IL_7D;
				case 3:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return true;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 4:
					if (A_0.PreviousSibling.Name == ClipboardData.b("䭧ᵩѫݭѯ᝱ݳٵ᥷᥹᥻", a_))
					{
						num = 1;
						continue;
					}
					return false;
				}
				if (A_0.PreviousSibling == null)
				{
					num = 0;
				}
				else
				{
					num = 4;
				}
			}
			IL_7D:;
		}
		return true;
	}

	// Token: 0x06003557 RID: 13655 RVA: 0x0031C01C File Offset: 0x0031B01C
	private bool ᜁ(XmlNode A_0)
	{
		int a_ = 10;
		int num = 5;
		for (;;)
		{
			switch (num)
			{
			case 0:
				num = 3;
				continue;
			case 1:
				if (A_0.ParentNode.ParentNode != null)
				{
					num = 2;
					continue;
				}
				goto IL_1B8;
			case 2:
				num = 11;
				continue;
			case 3:
				goto IL_13E;
			case 4:
				if (!(A_0.ParentNode.Name.ToUpper() == ClipboardData.b("㽯㹱", a_)))
				{
					num = 0;
					continue;
				}
				goto IL_BF;
			case 6:
				num = 4;
				continue;
			case 7:
				if (!(A_0.ParentNode.ParentNode.Name.ToUpper() == ClipboardData.b("㡯♱㥳㩵", a_)))
				{
					goto IL_1B8;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_13E;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 8:
				num = 7;
				continue;
			case 9:
				goto IL_BF;
			case 10:
				return false;
			case 11:
				if (!(A_0.ParentNode.ParentNode.Name.ToUpper() == ClipboardData.b("㉯㵱び⽵", a_)))
				{
					num = 8;
					continue;
				}
				return false;
			}
			if (A_0.ParentNode != null)
			{
				num = 6;
				continue;
			}
			goto IL_1B8;
			IL_BF:
			num = 1;
			continue;
			IL_13E:
			if (!(A_0.ParentNode.Name.ToUpper() == ClipboardData.b("╯㹱", a_)))
			{
				goto IL_1B8;
			}
			num = 9;
		}
		return false;
		IL_1B8:
		if (true)
		{
		}
		return true;
	}

	// Token: 0x06003558 RID: 13656 RVA: 0x0031C1EC File Offset: 0x0031B1EC
	private void ᜀ(XmlNode A_0)
	{
		int a_ = 13;
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u1DE8.ᜀ a_2 = new spr\u1DE8.ᜀ(null);
				spr\u1DE8.ᜁ ᜁ = new spr\u1DE8.ᜁ();
				ᜁ.ᜁ(new Dictionary<int, ArrayList>());
				this.ᜩ.ᜀ().Push(ᜁ.ᜁ());
				this.\u1713 = new Table(this.ᜏ.Document, false);
				int num = 19;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_10F;
					case 1:
						num = 5;
						continue;
					case 2:
						if (A_0.ParentNode.Name == ClipboardData.b("ݲᅴ", a_))
						{
							num = 12;
							continue;
						}
						this.\u171C = 0f;
						num = 8;
						continue;
					case 3:
					{
						Paragraph paragraph = new Paragraph(this.ᜏ.Document);
						paragraph.BreakCharacterFormat.Hidden = true;
						this.ᜏ.Add(paragraph);
						num = 7;
						continue;
					}
					case 4:
						goto IL_3C6;
					case 5:
						this.\u1713.TableFormat.HorizontalAlignment = RowAlignment.Left;
						num = 0;
						continue;
					case 6:
						if (this.ᜤ != null)
						{
							num = 18;
							continue;
						}
						goto IL_10F;
					case 7:
						goto IL_1AB;
					case 8:
						goto IL_306;
					case 9:
					{
						HorizontalAlignment horizontalAlignment;
						switch (horizontalAlignment)
						{
						case HorizontalAlignment.Center:
							this.\u1713.TableFormat.HorizontalAlignment = RowAlignment.Center;
							num = 10;
							continue;
						case HorizontalAlignment.Right:
							this.\u1713.TableFormat.HorizontalAlignment = RowAlignment.Right;
							num = 17;
							continue;
						default:
							num = 1;
							continue;
						}
						break;
					}
					case 10:
						goto IL_10F;
					case 11:
						goto IL_306;
					case 12:
						this.\u171C = this.\u1713.Width;
						num = 11;
						continue;
					case 13:
						num = 6;
						continue;
					case 14:
						goto IL_2B0;
					case 15:
						if (this.ᜥ)
						{
							num = 13;
							continue;
						}
						goto IL_10F;
					case 16:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3C6;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 17:
						goto IL_10F;
					case 18:
					{
						this.\u1713.TableFormat.BackColor = this.ᜤ.\u170D();
						this.\u1713.IndentFromLeft = this.ᜤ.ᜎ();
						HorizontalAlignment horizontalAlignment = this.ᜤ.ᜌ();
						num = 9;
						continue;
					}
					case 19:
						if (true)
						{
						}
						if (this.ᜏ.Count > 0)
						{
							num = 16;
							continue;
						}
						goto IL_1AB;
					case 20:
						this.\u1713.Rows.Add(this.\u1713.Rows[this.ᜣ]);
						this.ᜣ = -1;
						num = 14;
						continue;
					case 21:
						if (this.ᜣ != -1)
						{
							num = 20;
							continue;
						}
						goto IL_3FF;
					}
					break;
					IL_10F:
					this.ᜀ(A_0, ᜁ, a_2);
					this.ᜁ(A_0, ᜁ, a_2);
					BodyRegionCollection bodyRegionCollection;
					this.ᜏ = bodyRegionCollection;
					ᜁ.ᜀ(this.ᜏ.LastItem as Table, this.ᜩ.ᜀ());
					num = 2;
					continue;
					IL_1AB:
					this.ᜏ.Add(this.\u1713);
					this.\u1713.TableFormat.IsAutoResized = true;
					bodyRegionCollection = this.ᜏ;
					num = 15;
					continue;
					IL_3C6:
					if (this.ᜏ.LastItem.DocumentObjectType == DocumentObjectType.Table)
					{
						num = 3;
						continue;
					}
					goto IL_1AB;
					IL_306:
					num = 21;
				}
			}
			IL_2B0:
			IL_3FF:
			this.\u1714 = null;
			return;
		}
	}

	// Token: 0x06003559 RID: 13657 RVA: 0x0031C600 File Offset: 0x0031B600
	private void ᜁ(XmlNode A_0, spr\u1DE8.ᜁ A_1, spr\u1DE8.ᜀ A_2)
	{
		int a_ = 9;
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
			switch (0)
			{
			default:
			{
				this.\u1714 = null;
				int num = -1;
				IEnumerator enumerator = A_0.ChildNodes.GetEnumerator();
				try
				{
					int num2 = 5;
					for (;;)
					{
						XmlNode xmlNode;
						TableRow tableRow;
						int num4;
						switch (num2)
						{
						case 0:
							if (xmlNode.Name == ClipboardData.b("᭮ᝰᱲᩴͶ", a_))
							{
								num2 = 24;
								continue;
							}
							num2 = 18;
							continue;
						case 1:
							if (xmlNode.ParentNode.Name == ClipboardData.b("᭮ᥰᙲᑴ፶", a_))
							{
								num2 = 12;
								continue;
							}
							num2 = 36;
							continue;
						case 2:
							if (!enumerator.MoveNext())
							{
								num2 = 8;
								continue;
							}
							xmlNode = (XmlNode)enumerator.Current;
							num2 = 28;
							continue;
						case 3:
							this.\u1714 = new List<Dictionary<string, string>>();
							num2 = 31;
							continue;
						case 4:
							if (tableRow == this.\u1713.FirstRow)
							{
								num2 = 26;
								continue;
							}
							goto IL_19A;
						case 6:
							try
							{
								num2 = 5;
								for (;;)
								{
									XmlAttribute xmlAttribute;
									string text;
									switch (num2)
									{
									case 0:
										goto IL_2D3;
									case 1:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num2 = 3;
											continue;
										}
										xmlAttribute = (XmlAttribute)enumerator2.Current;
										text = xmlAttribute.Name.ToLower();
										num2 = 7;
										continue;
									}
									case 3:
										num2 = 4;
										continue;
									case 4:
										goto IL_2FF;
									case 6:
									{
										int num3 = int.Parse(xmlAttribute.Value);
										num2 = 0;
										continue;
									}
									case 7:
										if (text == ClipboardData.b("ᱮŰቲ᭴", a_))
										{
											num2 = 6;
											continue;
										}
										goto IL_2D3;
									}
									IL_266:
									num2 = 1;
									continue;
									goto IL_266;
									IL_2D3:
									Dictionary<string, string> dictionary;
									dictionary[text] = xmlAttribute.Value;
									num2 = 2;
								}
								IL_2FF:
								goto IL_95F;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator2;
									IDisposable disposable = enumerator2 as IDisposable;
									num2 = 2;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											goto IL_34A;
										case 1:
											disposable.Dispose();
											num2 = 0;
											continue;
										case 2:
											if (disposable != null)
											{
												num2 = 1;
												continue;
											}
											goto IL_34C;
										}
										break;
									}
								}
								IL_34A:
								IL_34C:;
							}
							goto Block_6;
							IL_95F:
							num2 = 25;
							continue;
						case 8:
							num2 = 10;
							continue;
						case 9:
							num2 = 19;
							continue;
						case 10:
							goto IL_C00;
						case 11:
							goto IL_A00;
						case 12:
							tableRow.IsHeader = true;
							num2 = 27;
							continue;
						case 13:
							goto IL_34D;
						case 15:
							num2 = 20;
							continue;
						case 16:
							goto IL_19A;
						case 17:
							num2 = 7;
							continue;
						case 18:
							if (xmlNode.Name.ToLower() == ClipboardData.b("౮Ṱὲ", a_))
							{
								num2 = 32;
								continue;
							}
							num2 = 21;
							continue;
						case 19:
							if (!(xmlNode.Name == ClipboardData.b("᭮፰ᱲᅴ๶", a_)))
							{
								num2 = 15;
								continue;
							}
							goto IL_945;
						case 20:
							if (!(xmlNode.Name == ClipboardData.b("᭮ᥰᙲᑴ፶", a_)))
							{
								num2 = 23;
								continue;
							}
							goto IL_945;
						case 21:
							if (xmlNode.Name.ToLower() != ClipboardData.b("᭮Ͱ", a_))
							{
								num2 = 22;
								continue;
							}
							tableRow = this.\u1713.AddRow(true, false);
							num++;
							num2 = 4;
							continue;
						case 22:
							goto IL_215;
						case 23:
							num2 = 0;
							continue;
						case 24:
							goto IL_945;
						case 25:
							if (this.\u1714 == null)
							{
								num2 = 3;
								continue;
							}
							goto IL_8BA;
						case 26:
							tableRow.RowFormat.ᜃ(this.\u1713.TableFormat);
							num2 = 16;
							continue;
						case 27:
							goto IL_987;
						case 28:
							if (xmlNode.NodeType != XmlNodeType.Whitespace)
							{
								num2 = 9;
								continue;
							}
							break;
						case 29:
							goto IL_987;
						case 30:
							goto IL_A00;
						case 31:
							goto IL_8BA;
						case 32:
						{
							int num3 = 1;
							Dictionary<string, string> dictionary = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
							IEnumerator enumerator2 = xmlNode.Attributes.GetEnumerator();
							num2 = 6;
							continue;
						}
						case 33:
						{
							int num3;
							if (num4 >= num3)
							{
								num2 = 17;
								continue;
							}
							Dictionary<string, string> dictionary;
							this.\u1714.Add(dictionary);
							num4++;
							num2 = 11;
							continue;
						}
						case 34:
							this.ᜣ = this.\u1713.Rows.Count - 1;
							num2 = 29;
							continue;
						case 36:
							if (xmlNode.ParentNode.Name == ClipboardData.b("᭮ᝰᱲᩴͶ", a_))
							{
								num2 = 34;
								continue;
							}
							goto IL_987;
						}
						goto IL_106;
						IL_19A:
						num2 = 1;
						continue;
						IL_891:
						num2 = 2;
						continue;
						Block_6:
						IEnumerator enumerator3;
						try
						{
							IL_34D:
							num2 = 29;
							for (;;)
							{
								XmlNode xmlNode2;
								bool flag;
								int num5;
								TableCell tableCell;
								switch (num2)
								{
								case 1:
									if (xmlNode2.Name.ToLower() != ClipboardData.b("᭮ᕰ", a_))
									{
										num2 = 14;
										continue;
									}
									goto IL_61D;
								case 2:
									num2 = 11;
									continue;
								case 3:
									num2 = 1;
									continue;
								case 4:
									goto IL_68F;
								case 5:
									goto IL_701;
								case 6:
									num2 = 25;
									continue;
								case 7:
									if (xmlNode2.Name.ToLower() != ClipboardData.b("᭮ᥰ", a_))
									{
										num2 = 23;
										continue;
									}
									goto IL_61D;
								case 8:
									num2 = 15;
									continue;
								case 9:
									goto IL_782;
								case 10:
									goto IL_5F3;
								case 11:
									goto IL_843;
								case 12:
									goto IL_68F;
								case 13:
									if (flag)
									{
										num2 = 18;
										continue;
									}
									goto IL_5F3;
								case 14:
									num2 = 7;
									continue;
								case 15:
									if (xmlNode2.Name.ToLower() == ClipboardData.b("᭮Ͱ", a_))
									{
										num2 = 6;
										continue;
									}
									goto IL_7D1;
								case 16:
									if (this.ᜨ != null)
									{
										num2 = 21;
										continue;
									}
									goto IL_782;
								case 17:
								{
									Table table = this.\u1713.LastCell.ChildObjects[this.\u1713.LastCell.ChildObjects.Count - 1] as Table;
									TableRow a_2 = table.AddRow(true, false);
									this.ᜀ(xmlNode2, a_2);
									this.ᜁ(xmlNode2, a_2);
									num2 = 22;
									continue;
								}
								case 18:
								{
									spr\u1DE8.ᜂ ᜂ = this.ᜁ();
									ᜂ.ᜀ(HorizontalAlignment.Center);
									this.\u171D = HorizontalAlignment.Center;
									ᜂ.ᜄ(true);
									num2 = 10;
									continue;
								}
								case 19:
									if (num5 >= xmlNode2.ChildNodes.Count)
									{
										num2 = 30;
										continue;
									}
									num2 = 26;
									continue;
								case 20:
									if (xmlNode2.NodeType != XmlNodeType.Whitespace)
									{
										num2 = 3;
										continue;
									}
									break;
								case 21:
									this.\u1712.ᜀ(this.ᜨ);
									num2 = 9;
									continue;
								case 22:
									goto IL_61D;
								case 23:
									num2 = 24;
									continue;
								case 24:
									if (this.\u1713.LastCell.ChildObjects[this.\u1713.LastCell.ChildObjects.Count - 1] is Table)
									{
										num2 = 8;
										continue;
									}
									goto IL_7D1;
								case 25:
									if (xmlNode2.ParentNode.Name.ToLower() == ClipboardData.b("᭮Ͱ", a_))
									{
										num2 = 17;
										continue;
									}
									goto IL_7D1;
								case 26:
									if (xmlNode2.ChildNodes.Item(num5).Name == ClipboardData.b("᭮ၰᅲᥴቶ", a_))
									{
										num2 = 27;
										continue;
									}
									goto IL_701;
								case 27:
									A_1.ᜁ.Add(this.\u171C);
									A_1.ᜂ(tableCell.Colspan);
									num2 = 5;
									continue;
								case 28:
									if (!enumerator3.MoveNext())
									{
										num2 = 2;
										continue;
									}
									xmlNode2 = (XmlNode)enumerator3.Current;
									num2 = 20;
									continue;
								case 30:
									this.ᜇ(A_0);
									this.ᜀ(flag);
									this.\u171D = HorizontalAlignment.Left;
									num2 = 0;
									continue;
								}
								goto IL_3DE;
								IL_5F3:
								this.ᜀ(xmlNode2.ChildNodes);
								this.ᜀ(this.ᜢ);
								num5 = 0;
								num2 = 12;
								continue;
								IL_61D:
								int a_3 = A_1.ᜀ(num);
								tableCell = tableRow.AddCell(false);
								tableCell.HTMLColIndex = a_3;
								this.ᜢ = false;
								this.ᜏ = tableCell.Items;
								tableCell.CellFormat.IsAutoResized = true;
								this.\u1712 = tableCell.AddParagraph();
								num2 = 16;
								continue;
								IL_68F:
								num2 = 19;
								continue;
								IL_701:
								num5++;
								num2 = 4;
								continue;
								IL_718:
								num2 = 28;
								continue;
								IL_3DE:
								goto IL_718;
								IL_782:
								this.ᜀ(xmlNode2, tableCell, num, A_1, A_2);
								flag = (xmlNode2.Name.ToLower() == ClipboardData.b("᭮ᥰ", a_));
								num2 = 13;
							}
							IL_7D1:
							throw new NotSupportedException(ClipboardData.b("❮հṲᥴ坶᩸ᑺ፼୾ꦈﮎ놐ﮖﶚ철슢톤펦첨쾪趬\udbae킰톲\ud9b4튶", a_));
							IL_843:
							goto IL_B83;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable2 = enumerator3 as IDisposable;
								num2 = 1;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_88E;
									case 1:
										if (disposable2 != null)
										{
											num2 = 2;
											continue;
										}
										goto IL_890;
									case 2:
										disposable2.Dispose();
										num2 = 0;
										continue;
									}
									break;
								}
							}
							IL_88E:
							IL_890:;
						}
						goto IL_891;
						IL_B83:
						this.ᜀ(this.\u171B.Pop());
						A_1.ᜁ(this.ᜩ.ᜀ().Pop());
						A_1.ᜁ().Add(A_1.ᜁ().Count, A_1.ᜁ);
						this.ᜩ.ᜀ().Push(A_1.ᜁ());
						this.\u171E = HorizontalAlignment.Left;
						num2 = 14;
						continue;
						IL_106:
						goto IL_891;
						IL_8BA:
						num4 = 0;
						num2 = 30;
						continue;
						IL_945:
						this.ᜁ(xmlNode, A_1, A_2);
						num2 = 35;
						continue;
						IL_987:
						this.\u171B.Push(this.ᜌ().ᜈ());
						this.ᜀ(xmlNode, tableRow);
						A_1.ᜂ();
						enumerator3 = xmlNode.ChildNodes.GetEnumerator();
						num2 = 13;
						continue;
						IL_A00:
						num2 = 33;
					}
					IL_215:
					throw new NotSupportedException(ClipboardData.b("❮հṲᥴ坶᩸ᑺ፼୾ꦈﮎ놐ﮖﶚ철슢톤펦첨쾪趬\udbae킰톲\ud9b4튶", a_));
					IL_C00:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable3 = enumerator as IDisposable;
						int num2 = 2;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_C48;
							case 1:
								disposable3.Dispose();
								num2 = 0;
								continue;
							case 2:
								if (disposable3 != null)
								{
									num2 = 1;
									continue;
								}
								goto IL_C4A;
							}
							break;
						}
					}
					IL_C48:
					IL_C4A:;
				}
				break;
			}
			}
			break;
		}
	}

	// Token: 0x0600355A RID: 13658 RVA: 0x0031D2A4 File Offset: 0x0031C2A4
	private void ᜁ(XmlNode A_0, TableRow A_1)
	{
		int a_ = 8;
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
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = A_0.ChildNodes.GetEnumerator();
				try
				{
					int num = 13;
					for (;;)
					{
						XmlNode xmlNode;
						bool flag;
						switch (num)
						{
						case 0:
							goto IL_266;
						case 1:
							num = 4;
							continue;
						case 2:
							num = 22;
							continue;
						case 3:
							if (xmlNode.NodeType != XmlNodeType.Whitespace)
							{
								num = 16;
								continue;
							}
							break;
						case 4:
							if (xmlNode.Name.ToLower() == ClipboardData.b("ᩭɯ", a_))
							{
								num = 23;
								continue;
							}
							goto IL_1D3;
						case 6:
							num = 14;
							continue;
						case 7:
						{
							Table table = this.\u1713.LastCell.ChildObjects[this.\u1713.LastCell.ChildObjects.Count - 1] as Table;
							TableRow a_2 = table.AddRow(true, false);
							this.ᜀ(xmlNode, a_2);
							this.ᜁ(xmlNode, a_2);
							num = 21;
							continue;
						}
						case 8:
							if (xmlNode.Name.ToLower() != ClipboardData.b("ᩭᑯ", a_))
							{
								num = 2;
								continue;
							}
							goto IL_3B9;
						case 9:
						{
							spr\u1DE8.ᜂ ᜂ = this.ᜁ();
							ᜂ.ᜀ(HorizontalAlignment.Center);
							this.\u171D = HorizontalAlignment.Center;
							ᜂ.ᜄ(true);
							num = 15;
							continue;
						}
						case 10:
							num = 18;
							continue;
						case 11:
							if (this.ᜨ != null)
							{
								num = 12;
								continue;
							}
							goto IL_266;
						case 12:
							this.\u1712.ᜀ(this.ᜨ);
							num = 0;
							continue;
						case 14:
							if (this.\u1713.LastCell.ChildObjects[this.\u1713.LastCell.ChildObjects.Count - 1] is Table)
							{
								num = 1;
								continue;
							}
							goto IL_1D3;
						case 15:
							goto IL_227;
						case 16:
							num = 8;
							continue;
						case 17:
							if (xmlNode.ParentNode.Name.ToLower() == ClipboardData.b("ᩭɯ", a_))
							{
								num = 7;
								continue;
							}
							goto IL_1D3;
						case 18:
							goto IL_420;
						case 19:
							if (!enumerator.MoveNext())
							{
								num = 10;
								continue;
							}
							xmlNode = (XmlNode)enumerator.Current;
							num = 3;
							continue;
						case 20:
							if (flag)
							{
								num = 9;
								continue;
							}
							goto IL_227;
						case 21:
							goto IL_3B9;
						case 22:
							if (xmlNode.Name.ToLower() != ClipboardData.b("ᩭᡯ", a_))
							{
								num = 6;
								continue;
							}
							goto IL_3B9;
						case 23:
							num = 17;
							continue;
						}
						IL_1AA:
						num = 19;
						continue;
						goto IL_1AA;
						IL_227:
						this.ᜀ(xmlNode.ChildNodes);
						this.ᜀ(this.ᜢ);
						this.ᜇ(A_0);
						this.ᜀ(flag);
						this.\u171D = HorizontalAlignment.Left;
						num = 5;
						continue;
						IL_266:
						flag = (xmlNode.Name.ToLower() == ClipboardData.b("ᩭᡯ", a_));
						num = 20;
						continue;
						IL_3B9:
						TableCell tableCell = A_1.AddCell(false);
						this.ᜢ = false;
						this.ᜏ = tableCell.Items;
						tableCell.CellFormat.IsAutoResized = true;
						this.\u1712 = tableCell.AddParagraph();
						num = 11;
					}
					IL_1D3:
					throw new NotSupportedException(ClipboardData.b("♭ѯάᡳ噵᭷ᕹቻ੽ꢇ揄낏歹ﲙ춟쎡킣튥춧캩貫\udaad톯킱\ud8b3펵", a_));
					IL_420:;
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
								goto IL_468;
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
								goto IL_46A;
							}
							break;
						}
					}
					IL_468:
					IL_46A:;
				}
				break;
			}
			}
			break;
		}
		this.\u1712 = null;
	}

	// Token: 0x0600355B RID: 13659 RVA: 0x0031D740 File Offset: 0x0031C740
	private void ᜀ(XmlNode A_0, TableCell A_1, int A_2, spr\u1DE8.ᜁ A_3, spr\u1DE8.ᜀ A_4)
	{
		int a_ = 0;
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				spr\u1DE8.ᜀ ᜀ = new spr\u1DE8.ᜀ(A_4);
				num = 1;
				int num2 = 1;
				CellFormat cellFormat = A_1.CellFormat;
				cellFormat.VerticalAlignment = VerticalAlignment.Middle;
				new List<XmlAttribute>();
				XmlAttribute a_2 = null;
				string text = null;
				IEnumerator enumerator = A_0.Attributes.GetEnumerator();
				int num3 = 15;
				for (;;)
				{
					int num4;
					string text2;
					switch (num3)
					{
					case 0:
						goto IL_A4;
					case 1:
					{
						if (num2 <= 1)
						{
							num3 = 14;
							continue;
						}
						TableCell tableCell = A_1.OwnerRow.AddCell(false);
						num4 = (tableCell.HTMLColIndex = num4 + 1);
						tableCell.CellFormat.ImportContainer(A_1.CellFormat);
						tableCell.CellFormat.HorizontalMerge = CellMerge.Continue;
						A_3.ᜁ(A_1.OwnerRow.Cells.Count);
						num2--;
						num3 = 0;
						continue;
					}
					case 2:
						goto IL_7A7;
					case 3:
						if (!string.IsNullOrEmpty(text))
						{
							num3 = 13;
							continue;
						}
						goto IL_7A7;
					case 4:
						goto IL_273;
					case 5:
						if (num > 1)
						{
							num3 = 8;
							continue;
						}
						A_1.CellFormat.VerticalMerge = CellMerge.None;
						num3 = 9;
						continue;
					case 6:
						goto IL_273;
					case 7:
						A_1.CellFormat.HorizontalMerge = CellMerge.Start;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A4;
						default:
							if (false)
							{
							}
							num3 = 6;
							continue;
						}
						break;
					case 8:
						A_1.CellFormat.VerticalMerge = CellMerge.Start;
						A_3.ᜂ(num);
						num3 = 12;
						continue;
					case 9:
						goto IL_1DB;
					case 10:
						goto IL_A4;
					case 11:
						if (num2 > 1)
						{
							num3 = 7;
							continue;
						}
						A_1.CellFormat.HorizontalMerge = CellMerge.None;
						num3 = 4;
						continue;
					case 12:
						goto IL_157;
					case 13:
						text2 += string.Format(ClipboardData.b("ᅥŧ๩ᡫ٭䩯剱ཱི䙵շ", a_), text);
						num3 = 2;
						continue;
					case 14:
						num3 = 5;
						continue;
					case 15:
						try
						{
							num3 = 13;
							for (;;)
							{
								switch (num3)
								{
								case 0:
								{
									string a;
									if (!(a == ClipboardData.b("ᑥŧ൩ѫᩭ", a_)))
									{
										num3 = 29;
										continue;
									}
									this.\u171D = HorizontalAlignment.Right;
									num3 = 35;
									continue;
								}
								case 1:
									num3 = 0;
									continue;
								case 3:
								{
									XmlAttribute xmlAttribute;
									if (xmlAttribute.Value.ToLower() == ClipboardData.b("ݥᵧṩͫ", a_))
									{
										num3 = 33;
										continue;
									}
									text = xmlAttribute.Value;
									num3 = 19;
									continue;
								}
								case 4:
									goto IL_759;
								case 8:
									num3 = 15;
									continue;
								case 10:
									num3 = 21;
									continue;
								case 11:
									num3 = 16;
									continue;
								case 12:
								{
									string a;
									XmlAttribute xmlAttribute;
									if ((a = xmlAttribute.Value.ToLower()) != null)
									{
										num3 = 10;
										continue;
									}
									goto IL_689;
								}
								case 14:
								{
									string key;
									int num5;
									if (spr᧓.\u17CD.TryGetValue(key, out num5))
									{
										num3 = 11;
										continue;
									}
									break;
								}
								case 15:
									if (spr᧓.\u17CD == null)
									{
										num3 = 25;
										continue;
									}
									goto IL_37D;
								case 16:
								{
									int num5;
									switch (num5)
									{
									case 0:
										num3 = 3;
										continue;
									case 1:
									{
										XmlAttribute xmlAttribute;
										ᜀ.ᜁ = Convert.ToSingle(this.ᜑ(xmlAttribute.Value));
										num3 = 6;
										continue;
									}
									case 2:
									{
										XmlAttribute xmlAttribute;
										ᜀ.ᜀ = spr᱈.ᜀ(xmlAttribute.Value);
										num3 = 2;
										continue;
									}
									case 3:
									{
										this.ᜢ = true;
										XmlAttribute xmlAttribute;
										a_2 = xmlAttribute;
										num3 = 7;
										continue;
									}
									case 4:
									{
										XmlAttribute xmlAttribute;
										num2 = Convert.ToInt32(xmlAttribute.Value);
										num3 = 17;
										continue;
									}
									case 5:
									{
										XmlAttribute xmlAttribute;
										num = Convert.ToInt32(xmlAttribute.Value);
										num3 = 18;
										continue;
									}
									case 6:
										num3 = 12;
										continue;
									default:
										num3 = 34;
										continue;
									}
									break;
								}
								case 21:
								{
									string a;
									if (!(a == ClipboardData.b("ե൧ѩᡫ୭ɯ", a_)))
									{
										num3 = 1;
										continue;
									}
									this.\u171D = HorizontalAlignment.Center;
									num3 = 9;
									continue;
								}
								case 22:
									goto IL_689;
								case 23:
								{
									if (!enumerator.MoveNext())
									{
										num3 = 32;
										continue;
									}
									XmlAttribute xmlAttribute = (XmlAttribute)enumerator.Current;
									num3 = 24;
									continue;
								}
								case 24:
								{
									XmlAttribute xmlAttribute;
									string key;
									if ((key = xmlAttribute.Name.ToLower()) != null)
									{
										num3 = 8;
										continue;
									}
									break;
								}
								case 25:
									spr᧓.\u17CD = new Dictionary<string, int>(7)
									{
										{
											ClipboardData.b("ᅥŧ๩ᡫ٭", a_),
											0
										},
										{
											ClipboardData.b("ѥݧᡩ࡫୭ɯ", a_),
											1
										},
										{
											ClipboardData.b("ѥݧᡩ࡫୭ɯᅱ᭳᩵᝷ࡹ", a_),
											2
										},
										{
											ClipboardData.b("ᕥᱧ፩k୭", a_),
											3
										},
										{
											ClipboardData.b("եݧ٩Ὣṭᅯᱱ", a_),
											4
										},
										{
											ClipboardData.b("ᑥݧᵩὫṭᅯᱱ", a_),
											5
										},
										{
											ClipboardData.b("ݥѧͩ୫m", a_),
											6
										}
									};
									num3 = 28;
									continue;
								case 28:
									goto IL_37D;
								case 29:
									num3 = 30;
									continue;
								case 30:
								{
									string a;
									if (!(a == ClipboardData.b("౥ᵧᥩᡫݭᙯୱ", a_)))
									{
										num3 = 31;
										continue;
									}
									this.\u171D = HorizontalAlignment.Justify;
									num3 = 5;
									continue;
								}
								case 31:
									num3 = 22;
									continue;
								case 32:
									num3 = 4;
									continue;
								case 33:
									A_1.CellFormat.IsAutoResized = true;
									num3 = 26;
									continue;
								case 34:
									num3 = 20;
									continue;
								}
								goto IL_338;
								IL_37D:
								num3 = 14;
								continue;
								IL_689:
								this.\u171D = HorizontalAlignment.Left;
								num3 = 27;
								continue;
								IL_6B9:
								num3 = 23;
								continue;
								IL_338:
								goto IL_6B9;
							}
							IL_759:
							goto IL_E5;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								num3 = 2;
								for (;;)
								{
									switch (num3)
									{
									case 0:
										goto IL_7A4;
									case 1:
										disposable.Dispose();
										num3 = 0;
										continue;
									case 2:
										if (disposable != null)
										{
											num3 = 1;
											continue;
										}
										goto IL_7A6;
									}
									break;
								}
							}
							IL_7A4:
							IL_7A6:;
						}
						goto IL_7A7;
						IL_E5:
						if (true)
						{
						}
						text2 = this.ᜀ(A_1, num2, A_3);
						text2 += this.ᜭ.ᜀ(A_0);
						num3 = 3;
						continue;
					}
					break;
					IL_A4:
					num3 = 1;
					continue;
					IL_273:
					A_1.Colspan = num2;
					num4 = A_1.HTMLColIndex;
					num3 = 10;
					continue;
					IL_7A7:
					this.ᜀ(a_2, text2, A_1, ᜀ);
					ᜀ.ᜀ(cellFormat);
					num3 = 11;
				}
			}
			IL_157:
			IL_1DB:
			A_3.ᜀ(A_2, A_1.HTMLColIndex, num, A_1.Colspan);
			A_3.ᜁ(A_1);
			A_3.ᜃ();
			return;
		}
		}
	}

	// Token: 0x0600355C RID: 13660 RVA: 0x0031DF6C File Offset: 0x0031CF6C
	private string ᜀ(int A_0)
	{
		int a_ = 12;
		int num = 1;
		Match match;
		for (;;)
		{
			Dictionary<string, string> dictionary;
			switch (num)
			{
			case 0:
				if (A_0 > this.\u1714.Count)
				{
					num = 6;
					continue;
				}
				goto IL_8F;
			case 2:
				goto IL_8A;
			case 3:
				if (match.Success)
				{
					num = 2;
					continue;
				}
				goto IL_148;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_115;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					if (dictionary.ContainsKey(ClipboardData.b("ձᵳት౷ቹ", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_148;
				}
				break;
			case 5:
				num = 0;
				continue;
			case 6:
				goto IL_115;
			case 7:
			{
				string input = dictionary[ClipboardData.b("ձᵳት౷ቹ", a_)];
				match = spr\u1DE8.ᜋ.Match(input);
				num = 3;
				continue;
			}
			}
			if (this.\u1714 == null)
			{
				num = 5;
				continue;
			}
			IL_8F:
			dictionary = this.\u1714[A_0];
			num = 4;
		}
		IL_8A:
		return string.Format(ClipboardData.b("ॱ䑳୵ࡷɹ", a_), match.Groups[1].Value);
		IL_115:
		return null;
		IL_148:
		return null;
	}

	// Token: 0x0600355D RID: 13661 RVA: 0x0031E0C4 File Offset: 0x0031D0C4
	private string ᜀ(TableCell A_0, int A_1, spr\u1DE8.ᜁ A_2)
	{
		int a_ = 1;
		string text;
		for (;;)
		{
			text = null;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
				{
					Dictionary<string, string> dictionary;
					if (dictionary.ContainsKey(ClipboardData.b("ၦhཪᥬݮ", a_)))
					{
						num = 4;
						continue;
					}
					return text;
				}
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
						if (A_0.HTMLColIndex < this.\u1714.Count)
						{
							num = 6;
							continue;
						}
						return text;
					}
					break;
				case 3:
					if (this.\u1714 != null)
					{
						num = 0;
						continue;
					}
					return text;
				case 4:
				{
					Dictionary<string, string> dictionary;
					string arg = dictionary[ClipboardData.b("ၦhཪᥬݮ", a_)];
					text += string.Format(ClipboardData.b("ၦhཪᥬݮ䭰卲๴䝶Ѹ䁺", a_), arg);
					num = 5;
					continue;
				}
				case 5:
					return text;
				case 6:
				{
					if (true)
					{
					}
					Dictionary<string, string> dictionary = this.\u1714[A_0.HTMLColIndex];
					num = 1;
					continue;
				}
				}
				break;
			}
		}
		return text;
	}

	// Token: 0x0600355E RID: 13662 RVA: 0x0031E200 File Offset: 0x0031D200
	private void ᜀ(XmlAttribute A_0, string A_1, TableCell A_2, spr\u1DE8.ᜀ A_3)
	{
		int a_ = 19;
		switch (0)
		{
		default:
			for (;;)
			{
				string text = A_1;
				int num = 20;
				for (;;)
				{
					spr\u1DE8.ᜂ ᜂ;
					int num3;
					switch (num)
					{
					case 0:
						this.\u171D = HorizontalAlignment.Right;
						num = 42;
						continue;
					case 1:
					{
						string text2;
						if (text2.ToLower() == ClipboardData.b("ᡸ๺ॼၾ", a_))
						{
							num = 49;
							continue;
						}
						A_2.Width = Convert.ToSingle(this.ᜑ(text2));
						A_2.WidthType = FtsWidth.Point;
						A_2.CellFormat.IsAutoResized = false;
						(A_2.Owner.Owner as Table).TableFormat.IsAutoResized = false;
						num = 48;
						continue;
					}
					case 2:
						num = 16;
						continue;
					case 3:
						goto IL_7A1;
					case 4:
						goto IL_349;
					case 5:
						goto IL_7A1;
					case 6:
						num = 47;
						continue;
					case 7:
						num = 44;
						continue;
					case 8:
						goto IL_7A1;
					case 9:
						goto IL_7A1;
					case 10:
						goto IL_5BF;
					case 11:
						goto IL_7A1;
					case 12:
						num = 36;
						continue;
					case 13:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							ᜂ.ᜁ(10f);
							num = 8;
							continue;
						}
						break;
					case 14:
						if (!string.IsNullOrEmpty(text))
						{
							num = 6;
							continue;
						}
						return;
					case 15:
						goto IL_52A;
					case 16:
						if (spr᧓.\u17CE == null)
						{
							num = 38;
							continue;
						}
						goto IL_5BF;
					case 17:
						goto IL_7A1;
					case 18:
						goto IL_380;
					case 19:
						this.\u171D = HorizontalAlignment.Center;
						num = 28;
						continue;
					case 20:
						if (A_0 != null)
						{
							num = 12;
							continue;
						}
						goto IL_7B8;
					case 21:
						goto IL_7A1;
					case 22:
						goto IL_7A1;
					case 23:
						goto IL_670;
					case 24:
						goto IL_7A1;
					case 25:
					{
						string text2;
						if (text2 == ClipboardData.b("ᙸ᥺ᅼᙾ", a_))
						{
							num = 15;
							continue;
						}
						goto IL_349;
					}
					case 26:
						goto IL_7B8;
					case 27:
					{
						string text2;
						if (!(text2 == ClipboardData.b("ၸེᱼ፾", a_)))
						{
							num = 45;
							continue;
						}
						goto IL_52A;
					}
					case 28:
						goto IL_7A1;
					case 29:
					{
						string text2;
						if (text2 == ClipboardData.b("੸ེོᙾ", a_))
						{
							num = 41;
							continue;
						}
						goto IL_7A1;
					}
					case 30:
					{
						string text2;
						if (text2 == ClipboardData.b("᩸Ṻ፼୾", a_))
						{
							num = 19;
							continue;
						}
						num = 40;
						continue;
					}
					case 31:
						goto IL_380;
					case 32:
					{
						string text3;
						string key;
						if ((key = text3.ToLower()) != null)
						{
							num = 2;
							continue;
						}
						goto IL_7A1;
					}
					case 33:
					{
						string key;
						int num2;
						if (spr᧓.\u17CE.TryGetValue(key, out num2))
						{
							num = 7;
							continue;
						}
						goto IL_7A1;
					}
					case 34:
						goto IL_7A1;
					case 35:
						return;
					case 36:
						if (A_0.Name.ToLower() == ClipboardData.b("੸ེѼ፾", a_))
						{
							num = 39;
							continue;
						}
						goto IL_7B8;
					case 37:
						num = 3;
						continue;
					case 38:
						spr᧓.\u17CE = new Dictionary<string, int>(10)
						{
							{
								ClipboardData.b("᭸᩺Ṽᑾꂌﺐﾒ杖", a_),
								0
							},
							{
								ClipboardData.b("᩸ᑺᅼၾ", a_),
								1
							},
							{
								ClipboardData.b("๸ቺ᥼୾", a_),
								2
							},
							{
								ClipboardData.b("ᅸṺᑼ᡾", a_),
								3
							},
							{
								ClipboardData.b("ླྀṺོ୾ꒈﶒ", a_),
								4
							},
							{
								ClipboardData.b("൸Ṻռ୾검", a_),
								5
							},
							{
								ClipboardData.b("ὸᑺ፼୾검ﶆ", a_),
								6
							},
							{
								ClipboardData.b("ὸᑺ፼୾검", a_),
								7
							},
							{
								ClipboardData.b("ὸᑺ፼୾검ﺆ", a_),
								8
							},
							{
								ClipboardData.b("ὸᑺ፼୾검歷", a_),
								9
							}
						};
						num = 10;
						continue;
					case 39:
						text += A_0.Value;
						num = 26;
						continue;
					case 40:
					{
						string text2;
						if (text2 == ClipboardData.b("୸ቺ᩼᝾", a_))
						{
							num = 0;
							continue;
						}
						this.\u171D = HorizontalAlignment.Justify;
						num = 17;
						continue;
					}
					case 41:
						ᜂ.ᜃ(true);
						num = 5;
						continue;
					case 42:
						goto IL_7A1;
					case 43:
					{
						string text2;
						if (text2 == ClipboardData.b("੸ᙺᱼ፾", a_))
						{
							num = 13;
							continue;
						}
						ᜂ.ᜁ((float)this.ᜀ(text2, ᜂ.ᜁ()));
						num = 46;
						continue;
					}
					case 44:
					{
						int num2;
						switch (num2)
						{
						case 0:
						{
							string text2;
							CellFormat cellFormat;
							cellFormat.BackColor = spr᱈.ᜀ(text2);
							num = 51;
							continue;
						}
						case 1:
						{
							string text2;
							ᜂ.ᜀ(spr᱈.ᜀ(text2));
							num = 21;
							continue;
						}
						case 2:
							num = 1;
							continue;
						case 3:
						{
							TableRow tableRow = A_2.Owner as TableRow;
							tableRow.HeightType = TableRowHeightType.Exactly;
							string text2;
							tableRow.Height = Convert.ToSingle(this.ᜑ(text2));
							num = 34;
							continue;
						}
						case 4:
						{
							string text2;
							A_2.CellFormat.VerticalAlignment = this.ᜂ(text2);
							num = 11;
							continue;
						}
						case 5:
							num = 30;
							continue;
						case 6:
							num = 43;
							continue;
						case 7:
						{
							string text2;
							ᜂ.ᜀ(text2);
							num = 9;
							continue;
						}
						case 8:
							num = 27;
							continue;
						case 9:
							ᜂ.ᜄ(true);
							num = 22;
							continue;
						default:
							num = 37;
							continue;
						}
						break;
					}
					case 45:
						num = 25;
						continue;
					case 46:
						goto IL_7A1;
					case 47:
					{
						if ((text = text.Trim()).Length == 0)
						{
							num = 23;
							continue;
						}
						ᜂ = this.ᜁ();
						this.ᜢ = true;
						ᜂ.\u1715 = new spr\u1DE8.ᜀ(null);
						CellFormat cellFormat = A_2.CellFormat;
						string[] array = text.Split(new char[]
						{
							';',
							':'
						});
						num3 = 0;
						int num4 = array.Length;
						num = 31;
						continue;
					}
					case 48:
						goto IL_7A1;
					case 49:
						A_2.CellFormat.IsAutoResized = true;
						num = 24;
						continue;
					case 50:
					{
						int num4;
						if (num3 >= num4 - 1)
						{
							num = 35;
							continue;
						}
						string[] array;
						string text3 = array[num3].ToLower().Trim();
						string text2 = array[num3 + 1].ToLower().Trim();
						A_3.ᜀ(text3, text2);
						num = 32;
						continue;
					}
					case 51:
						goto IL_7A1;
					}
					break;
					IL_349:
					num = 29;
					continue;
					IL_380:
					num = 50;
					continue;
					IL_52A:
					ᜂ.ᜂ(true);
					num = 4;
					continue;
					IL_5BF:
					num = 33;
					continue;
					IL_7A1:
					num3 += 2;
					num = 18;
					continue;
					IL_7B8:
					num = 14;
				}
			}
			return;
			IL_670:
			return;
		}
	}

	// Token: 0x0600355F RID: 13663 RVA: 0x0031EA10 File Offset: 0x0031DA10
	private void ᜀ(XmlNode A_0, TableRow A_1)
	{
		int a_ = 17;
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			IEnumerator enumerator = A_0.Attributes.GetEnumerator();
			try
			{
				int num = 46;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_1F6;
					case 1:
						goto IL_419;
					case 3:
						num = 10;
						continue;
					case 4:
					{
						string a;
						if (!(a == ClipboardData.b("ᑶᱸᕺॼ᩾", a_)))
						{
							num = 21;
							continue;
						}
						this.\u171E = HorizontalAlignment.Center;
						num = 28;
						continue;
					}
					case 5:
					{
						XmlAttribute xmlAttribute;
						string a2;
						if ((a2 = xmlAttribute.Name.ToLower()) != null)
						{
							num = 20;
							continue;
						}
						break;
					}
					case 6:
					{
						string a2;
						if (!(a2 == ClipboardData.b("ὶᱸቺ᩼᝾", a_)))
						{
							num = 16;
							continue;
						}
						XmlAttribute xmlAttribute;
						A_1.Height = Convert.ToSingle(this.ᜑ(xmlAttribute.Value));
						num = 43;
						continue;
					}
					case 7:
						num = 41;
						continue;
					case 8:
						goto IL_647;
					case 9:
					{
						string a3;
						if (!(a3 == ClipboardData.b("ᕶᡸ᡺ᙼ᡾Ꚋ﶐ﲒ", a_)))
						{
							num = 37;
							continue;
						}
						string[] array;
						A_1.RowFormat.BackColor = spr᱈.ᜀ(array[1]);
						num = 0;
						continue;
					}
					case 10:
					{
						string a3;
						string[] array;
						if ((a3 = array[0]) != null)
						{
							num = 7;
							continue;
						}
						goto IL_1F6;
					}
					case 11:
					{
						string a4;
						if (a4 == ClipboardData.b("ᕶᙸ᝺᥼", a_))
						{
							num = 13;
							continue;
						}
						goto IL_1F6;
					}
					case 13:
						this.ᜌ().ᜄ(true);
						num = 25;
						continue;
					case 14:
					{
						string a;
						XmlAttribute xmlAttribute;
						if ((a = xmlAttribute.Value.ToLower()) != null)
						{
							num = 24;
							continue;
						}
						goto IL_419;
					}
					case 15:
					{
						string[] array;
						if (array.Length == 2)
						{
							num = 3;
							continue;
						}
						goto IL_1F6;
					}
					case 16:
						num = 31;
						continue;
					case 17:
					{
						string[] array3;
						string[] array2 = array3;
						num2 = 0;
						num = 45;
						continue;
					}
					case 18:
					{
						string a2;
						if (!(a2 == ClipboardData.b("Ѷ൸ɺᅼ᩾", a_)))
						{
							num = 49;
							continue;
						}
						XmlAttribute xmlAttribute;
						string[] array3 = xmlAttribute.Value.ToLower().Split(new char[]
						{
							';'
						}, StringSplitOptions.RemoveEmptyEntries);
						num = 44;
						continue;
					}
					case 19:
						goto IL_1F6;
					case 20:
						num = 6;
						continue;
					case 21:
						num = 32;
						continue;
					case 24:
						num = 4;
						continue;
					case 25:
						goto IL_1F6;
					case 26:
					{
						string[] array2;
						if (num2 >= array2.Length)
						{
							num = 12;
							continue;
						}
						string text = array2[num2];
						string[] array = text.Split(new char[]
						{
							':'
						}, StringSplitOptions.RemoveEmptyEntries);
						num = 15;
						continue;
					}
					case 27:
						goto IL_5EF;
					case 29:
					{
						if (!enumerator.MoveNext())
						{
							num = 38;
							continue;
						}
						XmlAttribute xmlAttribute = (XmlAttribute)enumerator.Current;
						num = 5;
						continue;
					}
					case 30:
						goto IL_1F6;
					case 31:
					{
						string a2;
						if (!(a2 == ClipboardData.b("ᙶᕸቺ᩼ᅾ", a_)))
						{
							num = 34;
							continue;
						}
						num = 14;
						continue;
					}
					case 32:
					{
						string a;
						if (!(a == ClipboardData.b("նၸᱺᕼ୾", a_)))
						{
							num = 42;
							continue;
						}
						this.\u171E = HorizontalAlignment.Right;
						num = 48;
						continue;
					}
					case 33:
						num = 1;
						continue;
					case 34:
						num = 18;
						continue;
					case 35:
						goto IL_3F0;
					case 36:
					{
						string a;
						if (!(a == ClipboardData.b("ᵶ౸ࡺॼᙾ廒", a_)))
						{
							num = 33;
							continue;
						}
						this.\u171E = HorizontalAlignment.Justify;
						num = 22;
						continue;
					}
					case 37:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3F0;
						default:
							if (false)
							{
							}
							num = 40;
							continue;
						}
						break;
					case 38:
						num = 8;
						continue;
					case 39:
						num = 11;
						continue;
					case 40:
					{
						string a3;
						if (!(a3 == ClipboardData.b("ᅶᙸᕺॼ剾ﾊ", a_)))
						{
							num = 35;
							continue;
						}
						num = 47;
						continue;
					}
					case 41:
					{
						string a3;
						if (!(a3 == ClipboardData.b("ᑶᙸ᝺ቼൾ", a_)))
						{
							num = 50;
							continue;
						}
						string[] array;
						this.ᜌ().ᜀ(spr᱈.ᜀ(array[1]));
						num = 19;
						continue;
					}
					case 42:
						num = 36;
						continue;
					case 44:
					{
						string[] array3;
						if (array3.Length > 0)
						{
							num = 17;
							continue;
						}
						break;
					}
					case 45:
						goto IL_5EF;
					case 47:
					{
						string[] array;
						string a4;
						if ((a4 = array[1]) != null)
						{
							num = 39;
							continue;
						}
						goto IL_1F6;
					}
					case 49:
						num = 23;
						continue;
					case 50:
						num = 9;
						continue;
					}
					goto IL_10F;
					IL_1F6:
					num2++;
					num = 27;
					continue;
					IL_2BF:
					num = 29;
					continue;
					IL_10F:
					goto IL_2BF;
					IL_3F0:
					num = 30;
					continue;
					IL_419:
					this.\u171E = HorizontalAlignment.Left;
					num = 2;
					continue;
					IL_5EF:
					num = 26;
				}
				IL_647:;
			}
			finally
			{
				for (;;)
				{
					IDisposable disposable = enumerator as IDisposable;
					int num = 1;
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
								num = 0;
								continue;
							}
							goto IL_691;
						case 2:
							goto IL_68F;
						}
						break;
					}
				}
				IL_68F:
				IL_691:;
			}
			A_1.HeightType = TableRowHeightType.AtLeast;
			return;
		}
		}
	}

	// Token: 0x06003560 RID: 13664 RVA: 0x0031F0DC File Offset: 0x0031E0DC
	private void ᜀ(XmlNode A_0, spr\u1DE8.ᜁ A_1, spr\u1DE8.ᜀ A_2)
	{
		int a_ = 10;
		if (true)
		{
		}
		switch (0)
		{
		default:
		{
			int num = 1;
			RowFormat tableFormat;
			XmlAttribute a_2;
			for (;;)
			{
				IEnumerator enumerator;
				switch (num)
				{
				case 0:
					return;
				case 2:
					try
					{
						num = 37;
						for (;;)
						{
							switch (num)
							{
							case 2:
							{
								string value;
								if (!(value == ClipboardData.b("፯᝱ᩳɵᵷࡹ", a_)))
								{
									num = 3;
									continue;
								}
								this.\u1713.TableFormat.HorizontalAlignment = RowAlignment.Center;
								num = 21;
								continue;
							}
							case 3:
								num = 31;
								continue;
							case 5:
								num = 6;
								continue;
							case 6:
								goto IL_3A6;
							case 7:
								num = 23;
								continue;
							case 9:
								spr᧓.\u17CF = new Dictionary<string, int>(10)
								{
									{
										ClipboardData.b("ቯᵱٳትᵷࡹ", a_),
										0
									},
									{
										ClipboardData.b("ቯᵱٳትᵷࡹύᅽ", a_),
										1
									},
									{
										ClipboardData.b("፯᝱ᡳ᩵ࡷ᭹᡻᩽", a_),
										2
									},
									{
										ClipboardData.b("፯᝱ᡳ᩵୷੹ᵻᵽ", a_),
										3
									},
									{
										ClipboardData.b("ͯٱ൳᩵ᵷ", a_),
										4
									},
									{
										ClipboardData.b("ቯ፱ᝳᵵίࡹ፻୽", a_),
										5
									},
									{
										ClipboardData.b("ቯ፱ᝳᵵίࡹ፻୽ꦃﲍ", a_),
										6
									},
									{
										ClipboardData.b("ቯᕱᝳ᥵ᑷᕹ๻", a_),
										7
									},
									{
										ClipboardData.b("ᅯṱᵳᅵᙷ", a_),
										8
									},
									{
										ClipboardData.b("ݯ᭱ၳɵၷ", a_),
										9
									}
								};
								num = 10;
								continue;
							case 10:
								goto IL_10C;
							case 11:
							{
								string key;
								int num2;
								if (spr᧓.\u17CF.TryGetValue(key, out num2))
								{
									num = 20;
									continue;
								}
								break;
							}
							case 12:
							{
								string value;
								XmlAttribute xmlAttribute;
								if ((value = xmlAttribute.Value) != null)
								{
									num = 17;
									continue;
								}
								goto IL_3A6;
							}
							case 14:
								goto IL_691;
							case 15:
								this.\u1713.TableFormat.IsAutoResized = true;
								num = 27;
								continue;
							case 17:
								num = 2;
								continue;
							case 18:
								num = 14;
								continue;
							case 20:
								num = 38;
								continue;
							case 23:
								if (spr᧓.\u17CF == null)
								{
									num = 9;
									continue;
								}
								goto IL_10C;
							case 24:
							{
								XmlAttribute xmlAttribute;
								if (xmlAttribute.Value.ToLower() == ClipboardData.b("ᅯݱs᥵", a_))
								{
									num = 15;
									continue;
								}
								num = 25;
								continue;
							}
							case 25:
							{
								XmlAttribute xmlAttribute;
								if (xmlAttribute.Value.Contains(ClipboardData.b("啯", a_)))
								{
									num = 28;
									continue;
								}
								A_1.ᜄ = Convert.ToSingle(this.ᜑ(xmlAttribute.Value));
								this.\u1713.TableFormat.IsAutoResized = false;
								num = 0;
								continue;
							}
							case 26:
								A_2.ᜁ = -1f;
								A_2.ᜂ = BorderStyle.None;
								A_2.ᜀ = Color.Empty;
								num = 8;
								continue;
							case 28:
							{
								this.\u1713.PreferredTableWidth.ᜀ(FtsWidth.Percentage);
								XmlAttribute xmlAttribute;
								this.\u1713.PreferredTableWidth.ᜀ(Convert.ToInt32(xmlAttribute.Value.Replace(ClipboardData.b("啯", a_), string.Empty)));
								num = 16;
								continue;
							}
							case 29:
							{
								string key;
								XmlAttribute xmlAttribute;
								if ((key = xmlAttribute.Name.ToLower()) != null)
								{
									num = 7;
									continue;
								}
								break;
							}
							case 30:
							{
								if (!enumerator.MoveNext())
								{
									num = 18;
									continue;
								}
								XmlAttribute xmlAttribute = (XmlAttribute)enumerator.Current;
								num = 29;
								continue;
							}
							case 31:
							{
								string value;
								if (!(value == ClipboardData.b("ɯ᭱፳ṵ౷", a_)))
								{
									num = 5;
									continue;
								}
								this.\u1713.TableFormat.HorizontalAlignment = RowAlignment.Right;
								num = 19;
								continue;
							}
							case 33:
								if (A_2.ᜁ == 0f)
								{
									num = 26;
									continue;
								}
								break;
							case 36:
								num = 13;
								continue;
							case 38:
							{
								int num2;
								switch (num2)
								{
								case 0:
								{
									XmlAttribute xmlAttribute;
									A_2.ᜁ = Convert.ToSingle(this.ᜑ(xmlAttribute.Value));
									num = 33;
									continue;
								}
								case 1:
								{
									XmlAttribute xmlAttribute;
									A_2.ᜀ = spr᱈.ᜀ(xmlAttribute.Value);
									num = 34;
									continue;
								}
								case 2:
								{
									XmlAttribute xmlAttribute;
									float px = Convert.ToSingle(this.ᜑ(xmlAttribute.Value));
									tableFormat.Paddings.All = PointsConverter.FromPixel(px);
									num = 35;
									continue;
								}
								case 3:
								{
									XmlAttribute xmlAttribute;
									float px2 = Convert.ToSingle(this.ᜑ(xmlAttribute.Value));
									tableFormat.CellSpacing = PointsConverter.FromPixel(px2) / 2f;
									num = 1;
									continue;
								}
								case 4:
								{
									XmlAttribute xmlAttribute;
									a_2 = xmlAttribute;
									num = 22;
									continue;
								}
								case 5:
								case 6:
								case 7:
								{
									XmlAttribute xmlAttribute;
									tableFormat.BackColor = spr᱈.ᜀ(xmlAttribute.Value);
									num = 4;
									continue;
								}
								case 8:
									num = 12;
									continue;
								case 9:
									num = 24;
									continue;
								default:
									num = 36;
									continue;
								}
								break;
							}
							}
							goto IL_107;
							IL_10C:
							num = 11;
							continue;
							IL_26B:
							num = 30;
							continue;
							IL_107:
							goto IL_26B;
							IL_3A6:
							this.\u1713.TableFormat.HorizontalAlignment = RowAlignment.Left;
							num = 32;
						}
						IL_691:
						goto IL_753;
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
									goto IL_6DE;
								case 1:
									disposable.Dispose();
									num = 0;
									continue;
								case 2:
									if (disposable != null)
									{
										goto IL_6C1;
									}
									goto IL_6DE;
								}
								break;
								IL_6C1:
								num = 1;
								continue;
								IL_6DE:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_6C1;
								default:
									goto IL_6F4;
								}
							}
						}
						IL_6F4:
						if (false)
						{
						}
					}
					goto IL_6FB;
				}
				if (this.\u1713 == null)
				{
					num = 0;
					continue;
				}
				IL_6FB:
				tableFormat = this.\u1713.TableFormat;
				tableFormat.CellSpacing = PointsConverter.FromCm(0.05f) / 2f;
				tableFormat.Paddings.All = PointsConverter.FromPixel(1f);
				a_2 = null;
				enumerator = A_0.Attributes.GetEnumerator();
				num = 2;
			}
			return;
			IL_753:
			string a_3 = this.ᜭ.ᜀ(A_0);
			this.ᜀ(a_2, a_3, A_2, A_1);
			A_2.ᜀ(tableFormat);
			return;
		}
		}
	}

	// Token: 0x06003561 RID: 13665 RVA: 0x0031F884 File Offset: 0x0031E884
	private void ᜀ(XmlAttribute A_0, string A_1, spr\u1DE8.ᜀ A_2, spr\u1DE8.ᜁ A_3)
	{
		int a_ = 1;
		switch (0)
		{
		default:
			for (;;)
			{
				string text = A_1;
				int num = 19;
				for (;;)
				{
					int num2;
					string text2;
					string text3;
					switch (num)
					{
					case 0:
						num = 29;
						continue;
					case 1:
					{
						int num3;
						if (num2 >= num3 - 1)
						{
							num = 11;
							continue;
						}
						string[] array;
						text2 = array[num2].ToLower().Trim();
						text3 = array[num2 + 1].ToLower().Trim();
						num = 10;
						continue;
					}
					case 2:
					{
						if ((text = text.Trim()).Length == 0)
						{
							num = 9;
							continue;
						}
						RowFormat tableFormat = this.\u1713.TableFormat;
						string[] array = text.Split(new char[]
						{
							';',
							':'
						});
						num2 = 0;
						int num3 = array.Length;
						num = 18;
						continue;
					}
					case 3:
						goto IL_6D7;
					case 4:
						goto IL_6D7;
					case 5:
						if (true)
						{
						}
						goto IL_6D7;
					case 6:
						spr᧓.\u17D0 = new Dictionary<string, int>(12)
						{
							{
								ClipboardData.b("զࡨࡪ٬࡮Ͱᱲt᥶ᵸ", a_),
								0
							},
							{
								ClipboardData.b("զࡨࡪ٬࡮Ͱᱲt᥶ᵸ噺Ṽၾ", a_),
								1
							},
							{
								ClipboardData.b("զ๨ࡪɬͮṰŲ", a_),
								2
							},
							{
								ClipboardData.b("ၦhཪᥬݮ", a_),
								3
							},
							{
								ClipboardData.b("զ٨ᥪ६੮Ͱ干ɴṶᵸེᕼ", a_),
								4
							},
							{
								ClipboardData.b("զ٨ᥪ६੮Ͱ干ᙴᡶᕸᑺོ", a_),
								5
							},
							{
								ClipboardData.b("զ٨ᥪ६੮Ͱ干᝴ᡶ൸ེቼቾ", a_),
								6
							},
							{
								ClipboardData.b("զ٨ᥪ६੮Ͱ干Ŵᡶॸ", a_),
								7
							},
							{
								ClipboardData.b("զ٨ᥪ६੮Ͱ干ᥴቶὸེ", a_),
								8
							},
							{
								ClipboardData.b("զ٨ᥪ६੮Ͱ干ݴṶṸ፺ॼ", a_),
								9
							},
							{
								ClipboardData.b("զ٨ᥪ६੮Ͱ", a_),
								10
							},
							{
								ClipboardData.b("զ٨ᥪ६੮Ͱ干ᙴᡶᕸ᝺ᱼཾ", a_),
								11
							}
						};
						num = 26;
						continue;
					case 7:
						if (!string.IsNullOrEmpty(text))
						{
							num = 17;
							continue;
						}
						return;
					case 8:
						num = 28;
						continue;
					case 9:
						goto IL_1F4;
					case 10:
						if (!(text3 == ClipboardData.b("๦ݨͪ࡬ᵮᡰݲ", a_)))
						{
							num = 16;
							continue;
						}
						goto IL_6D7;
					case 11:
						return;
					case 12:
						goto IL_6D7;
					case 13:
						if (spr᧓.\u17D0 == null)
						{
							num = 6;
							continue;
						}
						goto IL_574;
					case 14:
						goto IL_2A7;
					case 15:
						num = 13;
						continue;
					case 16:
						num = 30;
						continue;
					case 17:
						num = 2;
						continue;
					case 18:
						goto IL_2A7;
					case 19:
						if (A_0 != null)
						{
							num = 0;
							continue;
						}
						goto IL_614;
					case 20:
						if (text3 == ClipboardData.b("Ѧ٨ݪŬ๮Űrၴ", a_))
						{
							num = 34;
							continue;
						}
						goto IL_6D7;
					case 21:
						num = 38;
						continue;
					case 22:
						goto IL_6D7;
					case 23:
						this.\u1713.TableFormat.IsAutoResized = true;
						num = 32;
						continue;
					case 24:
						goto IL_6D7;
					case 25:
						if (text3.ToLower() == ClipboardData.b("٦ᱨὪɬ", a_))
						{
							num = 23;
							continue;
						}
						A_3.ᜄ = Convert.ToSingle(this.ᜑ(text3));
						this.\u1713.TableFormat.IsAutoResized = false;
						num = 24;
						continue;
					case 26:
						goto IL_574;
					case 27:
						goto IL_6D7;
					case 28:
						goto IL_21F;
					case 29:
						if (A_0.Name.ToLower() == ClipboardData.b("ᑦᵨቪŬ੮", a_))
						{
							num = 36;
							continue;
						}
						goto IL_614;
					case 30:
					{
						string key;
						if ((key = text2) != null)
						{
							num = 15;
							continue;
						}
						goto IL_21F;
					}
					case 31:
						goto IL_6D7;
					case 32:
						goto IL_6D7;
					case 33:
					{
						string key;
						int num4;
						if (spr᧓.\u17D0.TryGetValue(key, out num4))
						{
							num = 21;
							continue;
						}
						goto IL_21F;
					}
					case 34:
					{
						RowFormat tableFormat;
						tableFormat.CellSpacing = -1f;
						num = 39;
						continue;
					}
					case 35:
						goto IL_6D7;
					case 36:
						IL_35D:
						text += A_0.Value;
						num = 37;
						continue;
					case 37:
						goto IL_614;
					case 38:
					{
						int num4;
						switch (num4)
						{
						case 0:
						case 1:
						case 2:
						{
							RowFormat tableFormat;
							tableFormat.BackColor = spr᱈.ᜀ(text3);
							num = 3;
							continue;
						}
						case 3:
							num = 25;
							continue;
						case 4:
						{
							RowFormat tableFormat;
							tableFormat.Borders.LineWidth = Convert.ToSingle(this.ᜑ(text3));
							num = 35;
							continue;
						}
						case 5:
						{
							RowFormat tableFormat;
							tableFormat.Borders.Color = spr᱈.ᜀ(text3);
							num = 40;
							continue;
						}
						case 6:
						{
							RowFormat tableFormat;
							this.ᜀ(text2, text3, tableFormat.Borders.Bottom);
							num = 12;
							continue;
						}
						case 7:
						{
							RowFormat tableFormat;
							this.ᜀ(text2, text3, tableFormat.Borders.Top);
							num = 22;
							continue;
						}
						case 8:
						{
							RowFormat tableFormat;
							this.ᜀ(text2, text3, tableFormat.Borders.Left);
							num = 27;
							continue;
						}
						case 9:
						{
							RowFormat tableFormat;
							this.ᜀ(text2, text3, tableFormat.Borders.Right);
							num = 4;
							continue;
						}
						case 10:
						{
							RowFormat tableFormat;
							this.ᜀ(text2, text3, tableFormat.Borders.Left);
							this.ᜀ(text2, text3, tableFormat.Borders.Right);
							this.ᜀ(text2, text3, tableFormat.Borders.Top);
							this.ᜀ(text2, text3, tableFormat.Borders.Bottom);
							num = 31;
							continue;
						}
						case 11:
							num = 20;
							continue;
						default:
							num = 8;
							continue;
						}
						break;
					}
					case 39:
						goto IL_6D7;
					case 40:
						goto IL_6D7;
					}
					break;
					IL_21F:
					A_2.ᜀ(text2, text3);
					num = 5;
					continue;
					IL_2A7:
					num = 1;
					continue;
					IL_574:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_35D;
					default:
						if (false)
						{
						}
						num = 33;
						continue;
					}
					IL_614:
					num = 7;
					continue;
					IL_6D7:
					num2 += 2;
					num = 14;
				}
			}
			IL_1F4:
			return;
		}
	}

	// Token: 0x06003562 RID: 13666 RVA: 0x0031FFA0 File Offset: 0x0031EFA0
	private void ᜀ(string A_0, string A_1, Border A_2)
	{
		int a_ = 0;
		switch (0)
		{
		default:
			for (;;)
			{
				string[] array = new string[]
				{
					ClipboardData.b("ɥ१ᥩѫ୭ᑯ", a_),
					ClipboardData.b("ɥݧṩᡫ୭ᑯ", a_),
					ClipboardData.b("ɥݧὩ๫ɭᕯ", a_),
					ClipboardData.b("ťᩧթͫᡭᕯ", a_),
					ClipboardData.b("ཥ٧ᥩ५ᩭ", a_),
					ClipboardData.b("॥ᵧṩὫ୭ѯ", a_),
					ClipboardData.b("ᑥŧ๩୫୭", a_),
					ClipboardData.b("ᕥݧ٩ի੭", a_),
					ClipboardData.b("๥ŧ๩࡫୭ṯ", a_),
					ClipboardData.b("ࡥݧѩ५", a_)
				};
				char[] separator = new char[]
				{
					' '
				};
				string[] array2 = A_1.Split(separator);
				int num = 0;
				int num2 = 6;
				for (;;)
				{
					int num3;
					switch (num2)
					{
					case 0:
						if (array2[num].StartsWith(ClipboardData.b("䕥", a_)))
						{
							num2 = 14;
							continue;
						}
						num2 = 18;
						continue;
					case 1:
					{
						string[] array3;
						if (num3 >= array3.Length)
						{
							num2 = 19;
							continue;
						}
						string b = array3[num3];
						goto IL_3A2;
					}
					case 2:
						goto IL_2B0;
					case 3:
						if (num >= array2.Length)
						{
							if (true)
							{
							}
							num2 = 5;
							continue;
						}
						num2 = 0;
						continue;
					case 4:
						goto IL_315;
					case 5:
						return;
					case 6:
						goto IL_2B0;
					case 7:
						goto IL_315;
					case 8:
					{
						string b;
						if (array2[num] == b)
						{
							num2 = 20;
							continue;
						}
						goto IL_1A3;
					}
					case 9:
						goto IL_315;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3A2;
						default:
							if (false)
							{
							}
							if (this.ᜡ)
							{
								num2 = 16;
								continue;
							}
							A_2.Color = spr᱈.ᜀ(array2[num]);
							num2 = 7;
							continue;
						}
						break;
					case 11:
						goto IL_329;
					case 12:
						goto IL_1A3;
					case 13:
						goto IL_329;
					case 14:
					{
						array2[num] = array2[num].Replace(ClipboardData.b("䕥", a_), string.Empty);
						int red = int.Parse(array2[num].Substring(0, 2), NumberStyles.AllowHexSpecifier);
						int green = int.Parse(array2[num].Substring(2, 2), NumberStyles.AllowHexSpecifier);
						int blue = int.Parse(array2[num].Substring(4, 2), NumberStyles.AllowHexSpecifier);
						A_2.Color = Color.FromArgb(red, green, blue);
						num2 = 15;
						continue;
					}
					case 15:
						goto IL_315;
					case 16:
						A_2.BorderType = this.ᜄ(array2[num]);
						this.ᜡ = false;
						num2 = 9;
						continue;
					case 17:
						A_2.LineWidth = this.ᜆ(array2[num]);
						num2 = 4;
						continue;
					case 18:
					{
						if (this.ᜇ(array2[num]))
						{
							num2 = 17;
							continue;
						}
						string[] array3 = array;
						num3 = 0;
						num2 = 13;
						continue;
					}
					case 19:
						num2 = 10;
						continue;
					case 20:
						this.ᜡ = true;
						num2 = 12;
						continue;
					}
					break;
					IL_1A3:
					num3++;
					num2 = 11;
					continue;
					IL_2B0:
					num2 = 3;
					continue;
					IL_315:
					num++;
					num2 = 2;
					continue;
					IL_329:
					num2 = 1;
					continue;
					IL_3A2:
					num2 = 8;
				}
			}
			return;
		}
	}

	// Token: 0x06003563 RID: 13667 RVA: 0x00320388 File Offset: 0x0031F388
	private float ᜃ(string A_0)
	{
		int a_ = 17;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_8C;
			case 1:
				A_0 = A_0.Substring(0, A_0.Length - 2);
				num = 0;
				continue;
			}
			if (!A_0.EndsWith(ClipboardData.b("ݶŸ", a_)))
			{
				break;
			}
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_8E;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				num = 1;
				break;
			}
		}
		IL_8C:
		IL_8E:
		float px = Convert.ToSingle(A_0);
		return PointsConverter.FromPixel(px);
	}

	// Token: 0x06003564 RID: 13668 RVA: 0x00320430 File Offset: 0x0031F430
	private VerticalAlignment ᜂ(string A_0)
	{
		int a_ = 13;
		int num = 8;
		for (;;)
		{
			string a;
			switch (num)
			{
			case 0:
				goto IL_70;
			case 1:
				if (!(a == ClipboardData.b("ݲᩴݶ", a_)))
				{
					num = 9;
					continue;
				}
				return VerticalAlignment.Top;
			case 2:
				if (!(a == ClipboardData.b("Ṳᱴ፶ᵸ᝺᡼", a_)))
				{
					num = 5;
					continue;
				}
				return VerticalAlignment.Middle;
			case 3:
				goto IL_107;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_107;
				default:
					if (false)
					{
					}
					num = 1;
					continue;
				}
				break;
			case 5:
				num = 3;
				continue;
			case 6:
				if (!(a == ClipboardData.b("ၲၴ᥶൸Ṻོ", a_)))
				{
					num = 7;
					continue;
				}
				return VerticalAlignment.Middle;
			case 7:
				num = 2;
				continue;
			case 9:
				num = 6;
				continue;
			case 10:
				num = 0;
				continue;
			}
			if ((a = A_0.ToLower()) != null)
			{
				num = 4;
				continue;
			}
			return VerticalAlignment.Top;
			IL_107:
			if (a == ClipboardData.b("ᅲᩴͶ൸ᑺၼ", a_))
			{
				return VerticalAlignment.Bottom;
			}
			num = 10;
		}
		return VerticalAlignment.Top;
		IL_70:
		if (true)
		{
		}
		return VerticalAlignment.Top;
	}

	// Token: 0x06003565 RID: 13669 RVA: 0x0032059C File Offset: 0x0031F59C
	private string ᜁ(string A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_A4:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				goto IL_53;
			}
			char[] array;
			int num2;
			int num3;
			for (;;)
			{
				IL_2C:
				switch (num)
				{
				case 0:
					goto IL_C4;
				case 1:
					goto IL_93;
				case 2:
					if (array[num2] != ' ')
					{
						num = 0;
						continue;
					}
					num2--;
					num = 5;
					continue;
				case 3:
					goto IL_C6;
				case 4:
					if (true)
					{
					}
					if (array[num2] != ' ')
					{
						num = 7;
						continue;
					}
					num2++;
					num = 6;
					continue;
				case 5:
					goto IL_75;
				case 6:
					goto IL_C6;
				case 7:
					num3 = num2;
					num2 = array.Length - 1;
					num = 1;
					continue;
				}
				goto IL_53;
				IL_C6:
				num = 4;
			}
			IL_75:
			IL_93:
			goto IL_A4;
			IL_C4:
			int num4 = num2;
			return A_0.Substring(num3, num4 - num3 + 1);
			IL_53:
			array = A_0.ToCharArray();
			num2 = 0;
			num = 3;
			goto IL_2C;
		}
		}
	}

	// Token: 0x06003566 RID: 13670 RVA: 0x003206AC File Offset: 0x0031F6AC
	private int ᜀ(string A_0)
	{
		switch (0)
		{
		default:
		{
			int num3;
			for (;;)
			{
				A_0 = A_0.ToUpper();
				char[] array = A_0.ToCharArray();
				int num = 0;
				int num2 = 0;
				num3 = 0;
				int num4 = array.Length - 1;
				int num5 = 18;
				for (;;)
				{
					switch (num5)
					{
					case 0:
						num = 1;
						num5 = 28;
						continue;
					case 1:
						num = 100;
						num5 = 20;
						continue;
					case 2:
						goto IL_28F;
					case 3:
						if (array[num4] == 'M')
						{
							num5 = 4;
							continue;
						}
						num5 = 26;
						continue;
					case 4:
						num = 1000;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1ED;
						default:
							if (false)
							{
							}
							num5 = 2;
							continue;
						}
						break;
					case 5:
						goto IL_28F;
					case 6:
						num = 500;
						num5 = 19;
						continue;
					case 7:
						num = 5;
						num5 = 5;
						continue;
					case 8:
						goto IL_1CB;
					case 9:
						num = 10;
						num5 = 29;
						continue;
					case 10:
						if (array[num4] == 'V')
						{
							num5 = 7;
							continue;
						}
						num5 = 17;
						continue;
					case 11:
						if (array[num4] == 'X')
						{
							num5 = 9;
							continue;
						}
						num5 = 10;
						continue;
					case 12:
						num3 = num2 - num;
						num5 = 16;
						continue;
					case 13:
						goto IL_28F;
					case 14:
						if (array[num4] == 'L')
						{
							num5 = 21;
							continue;
						}
						num5 = 11;
						continue;
					case 15:
						goto IL_16A;
					case 16:
						goto IL_16A;
					case 17:
						if (array[num4] == 'I')
						{
							num5 = 0;
							continue;
						}
						if (true)
						{
						}
						num = 0;
						num5 = 24;
						continue;
					case 18:
						goto IL_1CB;
					case 19:
						goto IL_28F;
					case 20:
						goto IL_28F;
					case 21:
						goto IL_1ED;
					case 22:
						if (num4 < 0)
						{
							num5 = 27;
							continue;
						}
						num5 = 3;
						continue;
					case 23:
						if (num2 > num)
						{
							num5 = 12;
							continue;
						}
						num3 += num;
						num5 = 15;
						continue;
					case 24:
						goto IL_28F;
					case 25:
						if (array[num4] == 'C')
						{
							num5 = 1;
							continue;
						}
						num5 = 14;
						continue;
					case 26:
						if (array[num4] == 'D')
						{
							num5 = 6;
							continue;
						}
						num5 = 25;
						continue;
					case 27:
						return num3;
					case 28:
						goto IL_28F;
					case 29:
						goto IL_28F;
					}
					break;
					IL_16A:
					num2 = num;
					num4--;
					num5 = 8;
					continue;
					IL_1CB:
					num5 = 22;
					continue;
					IL_1ED:
					num = 50;
					num5 = 13;
					continue;
					IL_28F:
					num5 = 23;
				}
			}
			return num3;
		}
		}
	}

	// Token: 0x06003567 RID: 13671 RVA: 0x003209D0 File Offset: 0x0031F9D0
	private void ᜀ()
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
		this.ᜠ = false;
		this.\u171F = false;
		this.\u1717 = -1;
		this.\u1719 = null;
	}

	// Token: 0x06003568 RID: 13672 RVA: 0x00320A28 File Offset: 0x0031FA28
	public spr\u1DE8()
	{
		int a_ = 9;
		this.ᜊ = new Regex(ClipboardData.b("㍮ɰ塲", a_));
		this.\u170D = new Stack<spr\u1DE8.ᜂ>();
		this.ᜎ = new Stack<bool>();
		this.ᜐ = new Stack<BodyRegionCollection>();
		this.ᜑ = new Stack<Table>();
		this.\u1717 = -1;
		this.\u171A = new spr\u1DE8.ᜂ();
		this.\u171B = new Stack<spr\u1DE8.ᜂ>();
		this.ᜣ = -1;
		this.ᜦ = new Stack<spr\u1DE8.ᜂ>();
		this.ᜧ = new Stack<bool>();
		this.ᜭ = new spr\u21FB();
		base..ctor();
	}

	// Token: 0x06003569 RID: 13673 RVA: 0x00320AD0 File Offset: 0x0031FAD0
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1DE8()
	{
		int a_ = 4;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1DE8.ᜋ = new Regex(ClipboardData.b("㑩䑫㉭ᑯ奱嵳幵⑷ॹ噻╽튁\ud983\udd85튉톋Ɥ꾏뚑", a_));
	}

	// Token: 0x04002895 RID: 10389
	private const string ᜀ = "Spire.Doc.Resources.xhtml1-strict.xsd";

	// Token: 0x04002896 RID: 10390
	private const string ᜁ = "Spire.Doc.Resources.xhtml1-transitional.xsd";

	// Token: 0x04002897 RID: 10391
	private const string ᜂ = " ";

	// Token: 0x04002898 RID: 10392
	private const string ᜃ = "Spire.Doc.Resources.picture_notfound.jpg";

	// Token: 0x04002899 RID: 10393
	private const float ᜄ = 35f;

	// Token: 0x0400289A RID: 10394
	private const float ᜅ = 3f;

	// Token: 0x0400289B RID: 10395
	private const float ᜆ = 4.5f;

	// Token: 0x0400289C RID: 10396
	private const float ᜇ = 0.75f;

	// Token: 0x0400289D RID: 10397
	private const float ᜈ = 36f;

	// Token: 0x0400289E RID: 10398
	private const float ᜉ = 3f;

	// Token: 0x0400289F RID: 10399
	private readonly Regex ᜊ;

	// Token: 0x040028A0 RID: 10400
	private static Regex ᜋ;

	// Token: 0x040028A1 RID: 10401
	private XmlDocument ᜌ;

	// Token: 0x040028A2 RID: 10402
	private Stack<spr\u1DE8.ᜂ> \u170D;

	// Token: 0x040028A3 RID: 10403
	private Stack<bool> ᜎ;

	// Token: 0x040028A4 RID: 10404
	private BodyRegionCollection ᜏ;

	// Token: 0x040028A5 RID: 10405
	private Stack<BodyRegionCollection> ᜐ;

	// Token: 0x040028A6 RID: 10406
	private Stack<Table> ᜑ;

	// Token: 0x040028A7 RID: 10407
	private Paragraph \u1712;

	// Token: 0x040028A8 RID: 10408
	private Table \u1713;

	// Token: 0x040028A9 RID: 10409
	private List<Dictionary<string, string>> \u1714;

	// Token: 0x040028AA RID: 10410
	private string \u1715;

	// Token: 0x040028AB RID: 10411
	private Uri \u1716;

	// Token: 0x040028AC RID: 10412
	private int \u1717;

	// Token: 0x040028AD RID: 10413
	private bool \u1718;

	// Token: 0x040028AE RID: 10414
	private Stack<ListStyle> \u1719;

	// Token: 0x040028AF RID: 10415
	private spr\u1DE8.ᜂ \u171A;

	// Token: 0x040028B0 RID: 10416
	private Stack<spr\u1DE8.ᜂ> \u171B;

	// Token: 0x040028B1 RID: 10417
	internal float \u171C;

	// Token: 0x040028B2 RID: 10418
	private HorizontalAlignment \u171D;

	// Token: 0x040028B3 RID: 10419
	private HorizontalAlignment \u171E;

	// Token: 0x040028B4 RID: 10420
	private bool \u171F;

	// Token: 0x040028B5 RID: 10421
	private bool ᜠ;

	// Token: 0x040028B6 RID: 10422
	private bool ᜡ;

	// Token: 0x040028B7 RID: 10423
	private bool ᜢ;

	// Token: 0x040028B8 RID: 10424
	private int ᜣ;

	// Token: 0x040028B9 RID: 10425
	private spr\u1DE8.ᜂ ᜤ;

	// Token: 0x040028BA RID: 10426
	private bool ᜥ;

	// Token: 0x040028BB RID: 10427
	private Stack<spr\u1DE8.ᜂ> ᜦ;

	// Token: 0x040028BC RID: 10428
	private Stack<bool> ᜧ;

	// Token: 0x040028BD RID: 10429
	public IParagraphStyle ᜨ;

	// Token: 0x040028BE RID: 10430
	private spr\u1DE8.ᜃ ᜩ;

	// Token: 0x040028BF RID: 10431
	private bool ᜪ;

	// Token: 0x040028C0 RID: 10432
	private int ᜫ;

	// Token: 0x040028C1 RID: 10433
	private ListStyle ᜬ;

	// Token: 0x040028C2 RID: 10434
	private spr\u21FB ᜭ;

	// Token: 0x020003B1 RID: 945
	internal class ᜃ
	{
		// Token: 0x0600356A RID: 13674 RVA: 0x00320B2C File Offset: 0x0031FB2C
		internal Stack<Dictionary<int, ArrayList>> ᜀ()
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

		// Token: 0x0600356B RID: 13675 RVA: 0x00320B70 File Offset: 0x0031FB70
		internal void ᜀ(Stack<Dictionary<int, ArrayList>> A_0)
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

		// Token: 0x040028C3 RID: 10435
		private Stack<Dictionary<int, ArrayList>> ᜀ = new Stack<Dictionary<int, ArrayList>>();
	}

	// Token: 0x020003B2 RID: 946
	internal class ᜁ
	{
		// Token: 0x0600356D RID: 13677 RVA: 0x00320BD4 File Offset: 0x0031FBD4
		internal ᜁ()
		{
		}

		// Token: 0x0600356E RID: 13678 RVA: 0x00320C20 File Offset: 0x0031FC20
		internal Dictionary<int, ArrayList> ᜁ()
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
			return this.ᜇ;
		}

		// Token: 0x0600356F RID: 13679 RVA: 0x00320C64 File Offset: 0x0031FC64
		internal void ᜁ(Dictionary<int, ArrayList> A_0)
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
			this.ᜇ = A_0;
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x00320CA8 File Offset: 0x0031FCA8
		internal void ᜂ()
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
			this.ᜀ = 0;
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x00320CEC File Offset: 0x0031FCEC
		internal void ᜁ(TableCell A_0)
		{
			switch (0)
			{
			default:
			{
				float num;
				for (;;)
				{
					num = A_0.Width;
					float num2 = 0f;
					int num3 = 28;
					for (;;)
					{
						float num4;
						int count;
						int num5;
						TableRow ownerRow;
						float num6;
						switch (num3)
						{
						case 0:
							num2 = num4 / (float)count;
							num3 = 26;
							continue;
						case 1:
							if (A_0.HTMLColIndex < this.ᜁ.Count)
							{
								num3 = 2;
								continue;
							}
							this.ᜁ.Add(3f);
							num3 = 9;
							continue;
						case 2:
							goto IL_17C;
						case 3:
							if ((double)num == 0.0)
							{
								num3 = 22;
								continue;
							}
							goto IL_CF;
						case 4:
							num3 = 10;
							continue;
						case 5:
							num = A_0.Width;
							num3 = 20;
							continue;
						case 6:
							goto IL_28E;
						case 7:
							if (num5 >= ownerRow.Cells.Count)
							{
								num3 = 27;
								continue;
							}
							num3 = 17;
							continue;
						case 8:
							goto IL_CF;
						case 9:
							goto IL_14E;
						case 10:
							goto IL_14E;
						case 11:
							this.ᜁ[A_0.HTMLColIndex] = num;
							num3 = 24;
							continue;
						case 12:
							goto IL_28E;
						case 13:
							if (this.ᜄ == 0f)
							{
								num3 = 15;
								continue;
							}
							num3 = 23;
							continue;
						case 14:
							if (A_0.Width != 0f)
							{
								num3 = 5;
								continue;
							}
							num = num2;
							num3 = 30;
							continue;
						case 15:
							num3 = 25;
							continue;
						case 16:
							if (A_0.HTMLColIndex >= this.ᜁ.Count)
							{
								num3 = 4;
								continue;
							}
							num3 = 18;
							continue;
						case 17:
							if (ownerRow.Cells[num5].Width == 0f)
							{
								num3 = 0;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_294;
							default:
								if (false)
								{
								}
								this.ᜁ.Add(ownerRow.Cells[num5].Width);
								num4 -= ownerRow.Cells[num5].Width;
								num3 = 6;
								continue;
							}
							break;
						case 18:
							if ((float)this.ᜁ[A_0.HTMLColIndex] < num)
							{
								num3 = 11;
								continue;
							}
							return;
						case 19:
							goto IL_3D8;
						case 20:
							goto IL_424;
						case 21:
							this.ᜁ.Add(num2);
							num3 = 12;
							continue;
						case 22:
							num = 3f;
							num3 = 8;
							continue;
						case 23:
							num6 = this.ᜄ;
							goto IL_2F6;
						case 24:
							return;
						case 25:
							num6 = 525f;
							goto IL_2F6;
						case 26:
							if (num5 < ownerRow.Cells.Count)
							{
								num3 = 21;
								continue;
							}
							goto IL_28E;
						case 27:
							goto IL_1EF;
						case 28:
							if (num == 0f)
							{
								num3 = 29;
								continue;
							}
							goto IL_1EF;
						case 29:
							this.ᜁ = new ArrayList();
							this.ᜁ.Clear();
							ownerRow = A_0.OwnerRow;
							ownerRow.OwnerTable;
							num3 = 13;
							continue;
						case 30:
							goto IL_424;
						case 31:
							goto IL_3D8;
						}
						break;
						IL_CF:
						num3 = 16;
						continue;
						IL_14E:
						num3 = 1;
						continue;
						IL_1EF:
						num3 = 14;
						continue;
						IL_294:
						num3 = 31;
						continue;
						IL_28E:
						num5++;
						goto IL_294;
						IL_2F6:
						num4 = num6;
						count = ownerRow.Cells.Count;
						num5 = 0;
						num3 = 19;
						continue;
						IL_3D8:
						if (true)
						{
						}
						num3 = 7;
						continue;
						IL_424:
						num3 = 3;
					}
				}
				IL_17C:
				this.ᜁ[A_0.HTMLColIndex] = num;
				return;
			}
			}
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x00321160 File Offset: 0x00320160
		internal void ᜃ()
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
			this.ᜀ++;
		}

		// Token: 0x06003573 RID: 13683 RVA: 0x003211AC File Offset: 0x003201AC
		internal void ᜂ(int A_0)
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
			this.ᜂ.Enqueue(A_0);
		}

		// Token: 0x06003574 RID: 13684 RVA: 0x003211F4 File Offset: 0x003201F4
		private ArrayList ᜀ(Dictionary<int, ArrayList> A_0)
		{
			switch (0)
			{
			default:
			{
				ArrayList arrayList;
				for (;;)
				{
					int num = 0;
					arrayList = new ArrayList();
					int num2 = 0;
					int num3 = 8;
					for (;;)
					{
						int num5;
						switch (num3)
						{
						case 0:
							goto IL_196;
						case 1:
							goto IL_15D;
						case 2:
							goto IL_1C1;
						case 3:
							goto IL_7F;
						case 4:
							num3 = 15;
							continue;
						case 5:
						{
							int num4;
							ArrayList arrayList2;
							if (num4 < arrayList2.Count)
							{
								num3 = 4;
								continue;
							}
							goto IL_7F;
						}
						case 6:
							if (num < this.ᜁ()[num2].Count)
							{
								num3 = 14;
								continue;
							}
							goto IL_C4;
						case 7:
						{
							if (num5 >= A_0.Count)
							{
								num3 = 11;
								continue;
							}
							ArrayList arrayList2 = A_0[num5];
							num3 = 5;
							continue;
						}
						case 8:
							goto IL_7A;
						case 9:
							goto IL_C4;
						case 10:
						{
							int num4;
							if (num4 >= num)
							{
								num3 = 17;
								continue;
							}
							float num6 = 0f;
							num5 = 0;
							num3 = 12;
							continue;
						}
						case 11:
						{
							float num6;
							arrayList.Add(num6);
							int num4;
							num4++;
							num3 = 1;
							continue;
						}
						case 12:
							goto IL_1C1;
						case 13:
						{
							int num4 = 0;
							num3 = 19;
							continue;
						}
						case 14:
							num = this.ᜁ()[num2].Count;
							num3 = 9;
							continue;
						case 15:
						{
							int num4;
							ArrayList arrayList2;
							float num6;
							if (num6 < (float)arrayList2[num4])
							{
								num3 = 18;
								continue;
							}
							goto IL_7F;
						}
						case 16:
							if (num2 >= this.ᜁ().Count)
							{
								num3 = 13;
								continue;
							}
							num3 = 6;
							continue;
						case 17:
							return arrayList;
						case 18:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_7A;
							default:
							{
								if (false)
								{
								}
								int num4;
								ArrayList arrayList2;
								float num6 = (float)arrayList2[num4];
								num3 = 3;
								continue;
							}
							}
							break;
						case 19:
							goto IL_15D;
						}
						break;
						IL_7F:
						num5++;
						num3 = 2;
						continue;
						IL_C4:
						num2++;
						if (true)
						{
						}
						num3 = 0;
						continue;
						IL_15D:
						num3 = 10;
						continue;
						IL_196:
						num3 = 16;
						continue;
						IL_7A:
						goto IL_196;
						IL_1C1:
						num3 = 7;
					}
				}
				return arrayList;
			}
			}
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x00321470 File Offset: 0x00320470
		internal void ᜀ(Table A_0, Stack<Dictionary<int, ArrayList>> A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜇ = A_1.Pop();
					int num = 0;
					IEnumerator enumerator = A_0.Rows.GetEnumerator();
					int num2 = 1;
					for (;;)
					{
						int num3;
						int num4;
						TableCell tableCell3;
						int count3;
						IEnumerator enumerator2;
						switch (num2)
						{
						case 0:
							if (A_0.Rows[num3].Cells[num4].Width == 3f)
							{
								num2 = 3;
								continue;
							}
							goto IL_D5;
						case 1:
						{
							try
							{
								num2 = 13;
								for (;;)
								{
									int num6;
									int num7;
									TableCell tableCell;
									int num8;
									int num10;
									switch (num2)
									{
									case 0:
										goto IL_761;
									case 1:
										goto IL_7AB;
									case 2:
										goto IL_856;
									case 3:
										goto IL_7AB;
									case 4:
										this.ᜁ.Add(3f);
										num2 = 10;
										continue;
									case 6:
									{
										int num5;
										if (num5 >= A_0.Rows[num6].Cells[num7].HTMLColIndex)
										{
											num2 = 12;
											continue;
										}
										goto IL_714;
									}
									case 7:
										goto IL_6B5;
									case 8:
										tableCell.CellFormat.HorizontalMerge = CellMerge.Start;
										num2 = 35;
										continue;
									case 9:
										goto IL_564;
									case 10:
										goto IL_7D1;
									case 11:
									{
										int num9;
										if (num8 >= num9)
										{
											num2 = 25;
											continue;
										}
										int cellIndex;
										int num5 = cellIndex + num8;
										TableCell tableCell2;
										tableCell = (TableCell)tableCell2.Clone();
										tableCell.Items.Clear();
										num7 = 0;
										num2 = 29;
										continue;
									}
									case 12:
										num7++;
										num2 = 7;
										continue;
									case 14:
										num2 = 32;
										continue;
									case 15:
										num++;
										num2 = 5;
										continue;
									case 16:
									{
										int count;
										if (num10 >= count)
										{
											num2 = 15;
											continue;
										}
										TableRow tableRow;
										TableCell tableCell2 = tableRow.Cells[num10];
										num2 = 23;
										continue;
									}
									case 17:
										num8 = 0;
										num2 = 33;
										continue;
									case 18:
									{
										int num9;
										if (num9 > 1)
										{
											num2 = 28;
											continue;
										}
										goto IL_856;
									}
									case 19:
									{
										if (!enumerator.MoveNext())
										{
											num2 = 14;
											continue;
										}
										TableRow tableRow = (TableRow)enumerator.Current;
										this.ᜁ = this.ᜇ[num];
										num10 = 0;
										int count = tableRow.Cells.Count;
										num2 = 0;
										continue;
									}
									case 20:
										goto IL_785;
									case 21:
										if (num8 == 0)
										{
											num2 = 8;
											continue;
										}
										tableCell.CellFormat.HorizontalMerge = CellMerge.Continue;
										num2 = 2;
										continue;
									case 22:
										goto IL_714;
									case 23:
									{
										TableCell tableCell2;
										if (tableCell2.CellFormat.VerticalMerge == CellMerge.Start)
										{
											num2 = 30;
											continue;
										}
										goto IL_564;
									}
									case 24:
										if (A_0.Rows.Count > num6)
										{
											num2 = 17;
											continue;
										}
										goto IL_564;
									case 25:
									{
										num6++;
										int num11;
										num11++;
										num2 = 1;
										continue;
									}
									case 26:
										if (A_0.Rows[num6].Cells.Count > this.ᜁ.Count)
										{
											num2 = 4;
											continue;
										}
										goto IL_7D1;
									case 27:
										goto IL_761;
									case 28:
										num2 = 21;
										continue;
									case 29:
										goto IL_6B5;
									case 30:
									{
										TableCell tableCell2;
										int cellIndex = tableCell2.GetCellIndex();
										num6 = tableCell2.OwnerRow.GetRowIndex() + 1;
										int num12 = this.ᜂ.Dequeue();
										int num9 = tableCell2.Colspan;
										int num11 = 1;
										int num13 = num12;
										num2 = 3;
										continue;
									}
									case 31:
									{
										int num11;
										int num13;
										if (num11 >= num13)
										{
											num2 = 9;
											continue;
										}
										num2 = 24;
										continue;
									}
									case 32:
										goto IL_927;
									case 33:
										goto IL_785;
									case 34:
										if (num7 >= A_0.Rows[num6].Cells.Count)
										{
											num2 = 22;
											continue;
										}
										num2 = 6;
										continue;
									case 35:
										goto IL_856;
									}
									goto IL_548;
									IL_564:
									num10++;
									num2 = 27;
									continue;
									IL_6B5:
									num2 = 34;
									continue;
									IL_714:
									A_0.Rows[num6].Cells.Insert(num7, tableCell);
									tableCell.CellFormat.VerticalMerge = CellMerge.Continue;
									num2 = 18;
									continue;
									IL_761:
									num2 = 16;
									continue;
									IL_785:
									num2 = 11;
									continue;
									IL_7AB:
									num2 = 31;
									continue;
									IL_7D1:
									num8++;
									num2 = 20;
									continue;
									IL_856:
									num2 = 26;
									continue;
									IL_8D7:
									num2 = 19;
									continue;
									IL_548:
									goto IL_8D7;
								}
								IL_927:
								goto IL_183;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num2 = 0;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											if (disposable != null)
											{
												num2 = 2;
												continue;
											}
											goto IL_974;
										case 1:
											goto IL_972;
										case 2:
											disposable.Dispose();
											num2 = 1;
											continue;
										}
										break;
									}
								}
								IL_972:
								IL_974:;
							}
							goto IL_975;
							IL_183:
							this.ᜁ = this.ᜀ(this.ᜇ);
							num3 = 0;
							int count2 = A_0.Rows.Count;
							num2 = 22;
							continue;
						}
						case 2:
							goto IL_A4C;
						case 3:
							num2 = 19;
							continue;
						case 4:
							if ((double)A_0.Rows[num3].Cells[num4].Width != 0.0)
							{
								num2 = 12;
								continue;
							}
							goto IL_A07;
						case 5:
							if (tableCell3.OwnerRow.PreviousSibling == null)
							{
								num2 = 6;
								continue;
							}
							goto IL_3E8;
						case 6:
							num2 = 7;
							continue;
						case 7:
							if (tableCell3.OwnerRow.NextSibling == null)
							{
								num2 = 17;
								continue;
							}
							goto IL_3E8;
						case 8:
							if (true)
							{
							}
							goto IL_AF;
						case 9:
							num2 = 5;
							continue;
						case 10:
							goto IL_D5;
						case 11:
							if (num4 >= count3)
							{
								num2 = 20;
								continue;
							}
							num2 = 4;
							continue;
						case 12:
							num2 = 0;
							continue;
						case 13:
							goto IL_D5;
						case 14:
							goto IL_AF;
						case 15:
						{
							int count2;
							if (num3 >= count2)
							{
								num2 = 26;
								continue;
							}
							goto IL_975;
						}
						case 16:
							try
							{
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										num2 = 1;
										continue;
									case 1:
										goto IL_B74;
									case 3:
									{
										if (!enumerator2.MoveNext())
										{
											num2 = 0;
											continue;
										}
										Table a_ = (Table)enumerator2.Current;
										this.ᜀ(a_, A_0.Rows[num3].Cells[num4].Width);
										num2 = 4;
										continue;
									}
									}
									IL_B4B:
									num2 = 3;
									continue;
									goto IL_B4B;
								}
								IL_B74:
								goto IL_1B6;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable2 = enumerator2 as IDisposable;
									num2 = 0;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											if (disposable2 != null)
											{
												num2 = 1;
												continue;
											}
											goto IL_BC1;
										case 1:
											disposable2.Dispose();
											num2 = 2;
											continue;
										case 2:
											goto IL_BBF;
										}
										break;
									}
								}
								IL_BBF:
								IL_BC1:;
							}
							goto IL_BC2;
							IL_1B6:
							num4++;
							goto IL_1BC;
						case 17:
							A_0.Rows[num3].Cells[num4].Width = 3f;
							this.ᜅ.Add(num4);
							num2 = 10;
							continue;
						case 18:
							if (A_0.Rows.Count > 1)
							{
								num2 = 21;
								continue;
							}
							goto IL_BF8;
						case 19:
							if (this.ᜁ.Count >= num4)
							{
								goto IL_D5;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_1BC;
							default:
								if (false)
								{
								}
								num2 = 24;
								continue;
							}
							break;
						case 20:
							goto IL_BC2;
						case 21:
						{
							IEnumerator enumerator3 = A_0.Rows.GetEnumerator();
							num2 = 25;
							continue;
						}
						case 22:
							goto IL_A4C;
						case 23:
							if (this.ᜀ(tableCell3))
							{
								num2 = 9;
								continue;
							}
							goto IL_3E8;
						case 24:
							goto IL_A07;
						case 25:
							try
							{
								num2 = 15;
								for (;;)
								{
									TableRow tableRow2;
									int num14;
									TableCell tableCell4;
									switch (num2)
									{
									case 0:
										goto IL_224;
									case 1:
										num2 = 13;
										continue;
									case 2:
									{
										IEnumerator enumerator3;
										if (!enumerator3.MoveNext())
										{
											num2 = 1;
											continue;
										}
										tableRow2 = (TableRow)enumerator3.Current;
										num2 = 7;
										continue;
									}
									case 3:
										if (tableRow2.Cells.Count < num14 + 1)
										{
											num2 = 5;
											continue;
										}
										goto IL_314;
									case 4:
										tableCell4.WidthUnit = 1;
										num2 = 10;
										continue;
									case 5:
										tableRow2.AddCell(true);
										num2 = 14;
										continue;
									case 6:
										if (tableCell4.Width == 3f)
										{
											num2 = 4;
											continue;
										}
										goto IL_2FD;
									case 7:
										if (A_0.TableGrid.Count - 1 > this.ᜁ.Count)
										{
											num2 = 9;
											continue;
										}
										break;
									case 8:
									{
										int count4;
										if (num14 >= count4)
										{
											num2 = 11;
											continue;
										}
										num2 = 3;
										continue;
									}
									case 9:
									{
										num14 = 0;
										int count4 = this.ᜁ.Count;
										num2 = 0;
										continue;
									}
									case 10:
										goto IL_2FD;
									case 12:
										goto IL_224;
									case 13:
										goto IL_39A;
									case 14:
										goto IL_314;
									}
									goto IL_21F;
									IL_224:
									num2 = 8;
									continue;
									IL_2FD:
									num14++;
									num2 = 12;
									continue;
									IL_314:
									tableCell4 = tableRow2.Cells[num14];
									tableCell4.Width = (float)this.ᜁ[num14];
									num2 = 6;
									continue;
									IL_368:
									num2 = 2;
									continue;
									IL_21F:
									goto IL_368;
								}
								IL_39A:
								goto IL_BF8;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator3;
									IDisposable disposable3 = enumerator3 as IDisposable;
									num2 = 1;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											goto IL_3E5;
										case 1:
											if (disposable3 != null)
											{
												num2 = 2;
												continue;
											}
											goto IL_3E7;
										case 2:
											disposable3.Dispose();
											num2 = 0;
											continue;
										}
										break;
									}
								}
								IL_3E5:
								IL_3E7:;
							}
							goto IL_3E8;
						case 26:
							num2 = 18;
							continue;
						}
						break;
						IL_AF:
						num2 = 11;
						continue;
						IL_D5:
						enumerator2 = A_0.Rows[num3].Cells[num4].Tables.GetEnumerator();
						num2 = 16;
						continue;
						IL_1BC:
						num2 = 14;
						continue;
						IL_3E8:
						tableCell3.Width = (float)this.ᜁ[num4];
						num2 = 13;
						continue;
						IL_975:
						this.ᜅ.Clear();
						num4 = 0;
						count3 = A_0.Rows[num3].Cells.Count;
						num2 = 8;
						continue;
						IL_A07:
						tableCell3 = A_0.Rows[num3].Cells[num4];
						num2 = 23;
						continue;
						IL_A4C:
						num2 = 15;
						continue;
						IL_BC2:
						this.ᜀ(this.ᜅ, A_0.Rows[num3], this.ᜁ);
						num3++;
						num2 = 2;
					}
				}
				IL_BF8:
				this.ᜀ(A_0);
				return;
			}
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x003220C8 File Offset: 0x003210C8
		private bool ᜀ(TableCell A_0)
		{
			bool result;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return result;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			switch (0)
			{
			default:
			{
				bool result2 = true;
				IEnumerator enumerator = A_0.ChildObjects.GetEnumerator();
				try
				{
					int num = 9;
					for (;;)
					{
						DocumentObject documentObject;
						IEnumerator enumerator2;
						switch (num)
						{
						case 0:
							goto IL_99;
						case 1:
							num = 4;
							continue;
						case 2:
							if (documentObject.DocumentObjectType == DocumentObjectType.Table)
							{
								num = 8;
								continue;
							}
							break;
						case 3:
							try
							{
								num = 16;
								for (;;)
								{
									switch (num)
									{
									case 0:
									{
										ParagraphBase paragraphBase;
										if ((paragraphBase as TextRange).Text != "")
										{
											num = 19;
											continue;
										}
										break;
									}
									case 1:
										num = 10;
										continue;
									case 2:
										num = 0;
										continue;
									case 3:
									{
										ParagraphBase paragraphBase;
										if (paragraphBase.DocumentObjectType == DocumentObjectType.Field)
										{
											num = 17;
											continue;
										}
										num = 15;
										continue;
									}
									case 4:
									{
										ParagraphBase paragraphBase;
										if (paragraphBase.DocumentObjectType != DocumentObjectType.Picture)
										{
											num = 5;
											continue;
										}
										goto IL_1A4;
									}
									case 5:
										num = 3;
										continue;
									case 6:
										goto IL_25C;
									case 7:
									{
										ParagraphBase paragraphBase;
										if (paragraphBase.DocumentObjectType == DocumentObjectType.TextRange)
										{
											num = 2;
											continue;
										}
										num = 4;
										continue;
									}
									case 8:
										num = 12;
										continue;
									case 10:
										goto IL_324;
									case 11:
										goto IL_1B2;
									case 12:
									{
										ParagraphBase paragraphBase;
										if (paragraphBase.DocumentObjectType != DocumentObjectType.CommentMark)
										{
											num = 14;
											continue;
										}
										goto IL_25C;
									}
									case 14:
										num = 18;
										continue;
									case 15:
									{
										ParagraphBase paragraphBase;
										if (paragraphBase.DocumentObjectType != DocumentObjectType.Break)
										{
											num = 8;
											continue;
										}
										goto IL_25C;
									}
									case 17:
										goto IL_1A4;
									case 18:
									{
										ParagraphBase paragraphBase;
										if (paragraphBase.DocumentObjectType == DocumentObjectType.FieldMark)
										{
											num = 6;
											continue;
										}
										result2 = false;
										num = 13;
										continue;
									}
									case 19:
										result = false;
										num = 20;
										continue;
									case 20:
										goto IL_1F9;
									case 21:
									{
										if (!enumerator2.MoveNext())
										{
											num = 1;
											continue;
										}
										ParagraphBase paragraphBase = (ParagraphBase)enumerator2.Current;
										num = 7;
										continue;
									}
									}
									IL_184:
									num = 21;
									continue;
									goto IL_184;
									IL_1A4:
									result = false;
									num = 11;
									continue;
									IL_25C:
									result2 = true;
									num = 9;
								}
								IL_1B2:
								IL_1F9:
								return result;
								IL_324:
								break;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator2 as IDisposable;
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
												num = 0;
												continue;
											}
											goto IL_371;
										case 2:
											goto IL_36F;
										}
										break;
									}
								}
								IL_36F:
								IL_371:;
							}
							goto IL_372;
						case 4:
							goto IL_3A1;
						case 5:
							if (documentObject.DocumentObjectType == DocumentObjectType.Paragraph)
							{
								num = 7;
								continue;
							}
							num = 2;
							continue;
						case 6:
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							documentObject = (DocumentObject)enumerator.Current;
							num = 5;
							continue;
						case 7:
							goto IL_372;
						case 8:
							result = false;
							num = 0;
							continue;
						}
						IL_EF:
						num = 6;
						continue;
						goto IL_EF;
						IL_372:
						enumerator2 = (documentObject as Paragraph).Items.GetEnumerator();
						num = 3;
					}
					IL_99:
					break;
					IL_3A1:
					return result2;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_3EC;
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
								goto IL_3EE;
							}
							break;
						}
					}
					IL_3EC:
					IL_3EE:;
				}
				break;
			}
			}
			return result;
		}

		// Token: 0x06003577 RID: 13687 RVA: 0x003224FC File Offset: 0x003214FC
		private void ᜀ(ArrayList A_0, TableRow A_1, ArrayList A_2)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					float num = 0f;
					IEnumerator enumerator = A_0.GetEnumerator();
					int num2 = 2;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
						{
							float num4;
							A_1.Cells[num3].Width += num4;
							num2 = 1;
							continue;
						}
						case 1:
							goto IL_1C5;
						case 2:
						{
							try
							{
								num2 = 2;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										num2 = 3;
										continue;
									case 3:
										goto IL_15B;
									case 4:
									{
										if (!enumerator.MoveNext())
										{
											num2 = 0;
											continue;
										}
										int index = (int)enumerator.Current;
										num += (float)this.ᜁ[index] - 3f;
										num2 = 1;
										continue;
									}
									}
									IL_103:
									num2 = 4;
									continue;
									goto IL_103;
								}
								IL_15B:
								goto IL_1E4;
							}
							finally
							{
								for (;;)
								{
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_1C4;
									default:
									{
										if (false)
										{
										}
										IDisposable disposable = enumerator as IDisposable;
										num2 = 2;
										for (;;)
										{
											switch (num2)
											{
											case 0:
												disposable.Dispose();
												num2 = 1;
												continue;
											case 1:
												goto IL_1C2;
											case 2:
												if (disposable != null)
												{
													num2 = 0;
													continue;
												}
												goto IL_1C4;
											}
											break;
										}
										break;
									}
									}
								}
								IL_1C2:
								IL_1C4:;
							}
							goto IL_1C5;
							IL_1E4:
							int count = A_1.Cells.Count;
							int num5 = count - A_0.Count;
							float num4 = num / (float)num5;
							num3 = 0;
							num2 = 6;
							continue;
						}
						case 3:
							goto IL_AF;
						case 4:
							if (num3 >= A_1.Cells.Count)
							{
								num2 = 5;
								continue;
							}
							num2 = 7;
							continue;
						case 5:
							return;
						case 6:
							goto IL_AF;
						case 7:
							if (!A_0.Contains(num3))
							{
								num2 = 0;
								continue;
							}
							goto IL_1C5;
						}
						break;
						IL_AF:
						num2 = 4;
						continue;
						IL_1C5:
						num3++;
						if (true)
						{
						}
						num2 = 3;
					}
				}
				return;
			}
		}

		// Token: 0x06003578 RID: 13688 RVA: 0x0032272C File Offset: 0x0032172C
		private void ᜀ(Table A_0, float A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					if (true)
					{
					}
					int num = 0;
					int num2 = 0;
					int num3 = 15;
					for (;;)
					{
						int num4;
						int num5;
						float num6;
						switch (num3)
						{
						case 0:
							goto IL_81;
						case 1:
							if (num4 >= A_0.Rows.Count)
							{
								num3 = 10;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_95;
							default:
								if (false)
								{
								}
								num5 = 0;
								num3 = 17;
								continue;
							}
							break;
						case 2:
							goto IL_1B3;
						case 3:
							num3 = 9;
							continue;
						case 4:
							num6 = A_1 / (float)num;
							num4 = 0;
							num3 = 7;
							continue;
						case 5:
							goto IL_118;
						case 6:
						{
							int count;
							num = count;
							num3 = 12;
							continue;
						}
						case 7:
							goto IL_1B3;
						case 8:
							num4++;
							num3 = 2;
							continue;
						case 9:
							if (A_0.Rows[num4].Cells[num5].Width == 3f)
							{
								num3 = 5;
								continue;
							}
							goto IL_81;
						case 10:
							goto IL_1DD;
						case 11:
							if ((double)A_0.Rows[num4].Cells[num5].Width != 0.0)
							{
								num3 = 3;
								continue;
							}
							goto IL_118;
						case 12:
							goto IL_DC;
						case 13:
						{
							int count;
							if (num < count)
							{
								num3 = 6;
								continue;
							}
							goto IL_DC;
						}
						case 14:
						{
							if (num2 >= A_0.Rows.Count)
							{
								num3 = 4;
								continue;
							}
							int count = A_0.Rows[num2].Cells.Count;
							num3 = 13;
							continue;
						}
						case 15:
							goto IL_212;
						case 16:
							if (num5 >= A_0.Rows[num4].Cells.Count)
							{
								num3 = 8;
								continue;
							}
							goto IL_95;
						case 17:
							goto IL_240;
						case 18:
							goto IL_240;
						case 19:
							goto IL_212;
						}
						break;
						IL_81:
						num5++;
						num3 = 18;
						continue;
						IL_95:
						num3 = 11;
						continue;
						IL_DC:
						num2++;
						num3 = 19;
						continue;
						IL_118:
						A_0.Rows[num4].Cells[num5].Width = (float)A_0.Rows[num4].Cells[num5].Colspan * num6;
						A_0.TableGrid[num5 + 1] = A_0.TableGrid[num5] + A_0.Rows[num4].Cells[num5].Width * 20f;
						num3 = 0;
						continue;
						IL_1B3:
						num3 = 1;
						continue;
						IL_212:
						num3 = 14;
						continue;
						IL_240:
						num3 = 16;
					}
				}
				IL_1DD:
				A_0.ᜑ();
				return;
			}
		}

		// Token: 0x06003579 RID: 13689 RVA: 0x00322A54 File Offset: 0x00321A54
		private void ᜀ(Table A_0)
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

		// Token: 0x0600357A RID: 13690 RVA: 0x00322A90 File Offset: 0x00321A90
		internal void ᜀ()
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
			this.ᜁ.Add(1f);
			this.ᜃ();
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x00322AE8 File Offset: 0x00321AE8
		internal void ᜁ(int A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ();
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3C;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 1:
					return;
				}
				goto IL_26;
				IL_3C:
				num = 0;
				continue;
				IL_26:
				if (true)
				{
				}
				if (this.ᜁ.Count < A_0)
				{
					goto IL_3C;
				}
				break;
			}
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x00322B68 File Offset: 0x00321B68
		internal void ᜀ(int A_0, int A_1, int A_2, int A_3)
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
			this.ᜆ.ᜀ(A_0, A_1, A_2, A_3);
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x00322BB4 File Offset: 0x00321BB4
		internal int ᜀ(int A_0)
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
			return this.ᜆ.ᜀ(A_0);
		}

		// Token: 0x040028C4 RID: 10436
		private int ᜀ;

		// Token: 0x040028C5 RID: 10437
		internal ArrayList ᜁ = new ArrayList();

		// Token: 0x040028C6 RID: 10438
		private Queue<int> ᜂ = new Queue<int>();

		// Token: 0x040028C7 RID: 10439
		internal float ᜃ;

		// Token: 0x040028C8 RID: 10440
		internal float ᜄ;

		// Token: 0x040028C9 RID: 10441
		private ArrayList ᜅ = new ArrayList();

		// Token: 0x040028CA RID: 10442
		private spr\u1DE8.ᜁ.ᜀ ᜆ = new spr\u1DE8.ᜁ.ᜀ();

		// Token: 0x040028CB RID: 10443
		private Dictionary<int, ArrayList> ᜇ = new Dictionary<int, ArrayList>();

		// Token: 0x020003B3 RID: 947
		internal class ᜀ
		{
			// Token: 0x0600357E RID: 13694 RVA: 0x00322BFC File Offset: 0x00321BFC
			public ᜀ()
			{
				this.ᜀ = new List<List<ushort>>();
				this.ᜁ = 16;
			}

			// Token: 0x0600357F RID: 13695 RVA: 0x00322C24 File Offset: 0x00321C24
			internal void ᜀ(int A_0, int A_1, int A_2, int A_3)
			{
				switch (0)
				{
				default:
					for (;;)
					{
						int num = A_1 + A_3 - 1;
						int num2 = A_1 / this.ᜁ;
						int num3 = A_1 % this.ᜁ;
						int num4 = num / this.ᜁ;
						int num5 = num % this.ᜁ;
						int num6 = A_0 + A_2 - 1;
						int num7 = 20;
						for (;;)
						{
							int num8;
							List<ushort> list;
							int num10;
							switch (num7)
							{
							case 0:
								return;
							case 1:
								goto IL_250;
							case 2:
								if (num8 > num6)
								{
									num7 = 0;
									continue;
								}
								list = this.ᜀ[num8];
								num7 = 17;
								continue;
							case 3:
								num7 = 22;
								continue;
							case 4:
							{
								int num9 = num3;
								num7 = 8;
								continue;
							}
							case 5:
								num8 = A_0;
								num7 = 6;
								continue;
							case 6:
								goto IL_250;
							case 7:
								goto IL_212;
							case 8:
								goto IL_E5;
							case 9:
							{
								int num9;
								if (num9 > num5)
								{
									num7 = 3;
									continue;
								}
								num10 |= 1 << this.ᜁ - num9 - 1;
								num9++;
								num7 = 18;
								continue;
							}
							case 10:
								num10 = (int)list[num2];
								num7 = 15;
								continue;
							case 11:
								goto IL_276;
							case 12:
								goto IL_212;
							case 13:
								if (list.Count > num4)
								{
									num7 = 10;
									continue;
								}
								list.Add(0);
								num7 = 11;
								continue;
							case 14:
							{
								int num11;
								if (num11 < num4)
								{
									list[num11] = ushort.MaxValue;
									num11++;
									num7 = 12;
									continue;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_327;
								default:
									if (false)
									{
									}
									num7 = 19;
									continue;
								}
								break;
							}
							case 15:
							{
								if (num2 == num4)
								{
									num7 = 4;
									continue;
								}
								num10 |= (1 << this.ᜁ - num3) - 1;
								int num11 = num2 + 1;
								num7 = 7;
								continue;
							}
							case 16:
								goto IL_1C9;
							case 17:
								goto IL_276;
							case 18:
								goto IL_327;
							case 19:
							{
								int num12 = (int)list[num4];
								num12 |= ~((1 << this.ᜁ - num5 - 1) - 1);
								list[num4] = (ushort)num12;
								num7 = 16;
								continue;
							}
							case 20:
								goto IL_170;
							case 21:
								goto IL_170;
							case 22:
								goto IL_1C9;
							case 23:
								if (this.ᜀ.Count > num6)
								{
									num7 = 5;
									continue;
								}
								this.ᜀ.Add(new List<ushort>());
								num7 = 21;
								continue;
							}
							break;
							IL_E5:
							num7 = 9;
							continue;
							IL_327:
							goto IL_E5;
							IL_170:
							if (true)
							{
							}
							num7 = 23;
							continue;
							IL_1C9:
							list[num2] = (ushort)num10;
							num8++;
							num7 = 1;
							continue;
							IL_212:
							num7 = 14;
							continue;
							IL_250:
							num7 = 2;
							continue;
							IL_276:
							num7 = 13;
						}
					}
					return;
				}
			}

			// Token: 0x06003580 RID: 13696 RVA: 0x00322F70 File Offset: 0x00321F70
			internal int ᜀ(int A_0)
			{
				switch (0)
				{
				default:
				{
					int num = 7;
					int num2;
					List<ushort> list;
					int num3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return 0;
						case 1:
							goto IL_DD;
						case 2:
							goto IL_B8;
						case 3:
							goto IL_BC;
						case 4:
						{
							if (num2 >= list.Count)
							{
								num = 1;
								continue;
							}
							ushort a_ = list[num2];
							num3 = this.ᜀ(a_);
							num = 5;
							continue;
						}
						case 5:
							if (num3 != -1)
							{
								num = 2;
								continue;
							}
							num2++;
							num = 3;
							continue;
						case 6:
							goto IL_BC;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (this.ᜀ.Count <= A_0)
							{
								if (true)
								{
								}
								num = 0;
								continue;
							}
							break;
						}
						list = this.ᜀ[A_0];
						num2 = 0;
						num = 6;
						continue;
						IL_BC:
						num = 4;
					}
					return 0;
					IL_B8:
					return num2 * this.ᜁ + num3;
					IL_DD:
					return list.Count * this.ᜁ;
				}
				}
			}

			// Token: 0x06003581 RID: 13697 RVA: 0x0032309C File Offset: 0x0032209C
			private int ᜀ(ushort A_0)
			{
				int num4;
				for (;;)
				{
					IL_00:
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							goto IL_AC;
						case 1:
							goto IL_112;
						case 3:
						{
							ushort num3;
							ushort num2 = num3;
							num4 += 8;
							num = 11;
							continue;
						}
						case 4:
						{
							ushort num2;
							if ((num2 & 1) > 0)
							{
								num = 15;
								continue;
							}
							goto IL_1DB;
						}
						case 5:
						{
							ushort num3;
							ushort num2;
							if ((num3 = (ushort)(num2 >> 1)) > 0)
							{
								num = 17;
								continue;
							}
							goto IL_AC;
						}
						case 6:
							goto IL_76;
						case 7:
						{
							ushort num3;
							ushort num2 = num3;
							num4 += 2;
							num = 6;
							continue;
						}
						case 8:
						{
							ushort num3;
							ushort num2;
							if ((num3 = (ushort)(num2 >> 2)) > 0)
							{
								num = 7;
								continue;
							}
							goto IL_76;
						}
						case 9:
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
								ushort num3;
								if (num3 > 0)
								{
									num = 3;
									continue;
								}
								goto IL_117;
							}
							}
							break;
						case 10:
						{
							ushort num3;
							ushort num2 = num3;
							num4 += 4;
							num = 14;
							continue;
						}
						case 11:
							goto IL_117;
						case 12:
						{
							if (A_0 == 65535)
							{
								num = 13;
								continue;
							}
							num4 = 0;
							ushort num2 = ~A_0;
							ushort num3 = (ushort)(num2 >> 8);
							num = 9;
							continue;
						}
						case 13:
							return -1;
						case 14:
							goto IL_19C;
						case 15:
							num4++;
							num = 1;
							continue;
						case 16:
						{
							ushort num3;
							ushort num2;
							if ((num3 = (ushort)(num2 >> 4)) > 0)
							{
								num = 10;
								continue;
							}
							goto IL_19C;
						}
						case 17:
						{
							ushort num3;
							ushort num2 = num3;
							num4++;
							num = 0;
							continue;
						}
						case 18:
							return 0;
						}
						if (A_0 == 0)
						{
							num = 18;
							continue;
						}
						num = 12;
						continue;
						IL_76:
						num = 5;
						continue;
						IL_AC:
						num = 4;
						continue;
						IL_117:
						num = 16;
						continue;
						IL_19C:
						num = 8;
					}
				}
				return 0;
				IL_112:
				IL_1DB:
				return this.ᜁ - num4;
			}

			// Token: 0x040028CC RID: 10444
			private List<List<ushort>> ᜀ;

			// Token: 0x040028CD RID: 10445
			private int ᜁ;
		}
	}

	// Token: 0x020003B4 RID: 948
	internal class ᜂ
	{
		// Token: 0x06003582 RID: 13698 RVA: 0x0032328C File Offset: 0x0032228C
		internal bool ᜋ()
		{
			if (this.ᜀ(2))
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
					break;
				}
				return (bool)this.ᜑ[2];
			}
			return false;
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x003232E8 File Offset: 0x003222E8
		internal void ᜄ(bool A_0)
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
			this.ᜀ(2, A_0);
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x0032332C File Offset: 0x0032232C
		internal bool ᜆ()
		{
			if (this.ᜀ(4))
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
					break;
				}
				if (true)
				{
				}
				return (bool)this.ᜑ[4];
			}
			return false;
		}

		// Token: 0x06003585 RID: 13701 RVA: 0x00323388 File Offset: 0x00322388
		internal void ᜂ(bool A_0)
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
			this.ᜀ(4, A_0);
		}

		// Token: 0x06003586 RID: 13702 RVA: 0x003233CC File Offset: 0x003223CC
		internal bool ᜃ()
		{
			if (this.ᜀ(3))
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
					break;
				}
				return (bool)this.ᜑ[3];
			}
			return false;
		}

		// Token: 0x06003587 RID: 13703 RVA: 0x00323428 File Offset: 0x00322428
		internal void ᜀ(bool A_0)
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
			this.ᜀ(3, A_0);
		}

		// Token: 0x06003588 RID: 13704 RVA: 0x0032346C File Offset: 0x0032246C
		public bool ᜂ()
		{
			if (this.ᜀ(5))
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
					break;
				}
				return (bool)this.ᜑ[5];
			}
			return false;
		}

		// Token: 0x06003589 RID: 13705 RVA: 0x003234C8 File Offset: 0x003224C8
		public void ᜃ(bool A_0)
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
			this.ᜀ(5, A_0);
		}

		// Token: 0x0600358A RID: 13706 RVA: 0x0032350C File Offset: 0x0032250C
		public Color ᜄ()
		{
			if (this.ᜀ(6))
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_39;
				}
				if (false)
				{
				}
				IL_39:
				return (Color)this.ᜑ[6];
			}
			return Color.Empty;
		}

		// Token: 0x0600358B RID: 13707 RVA: 0x0032356C File Offset: 0x0032256C
		public void ᜀ(Color A_0)
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
			this.ᜀ(6, A_0);
		}

		// Token: 0x0600358C RID: 13708 RVA: 0x003235B4 File Offset: 0x003225B4
		public Color \u170D()
		{
			if (true)
			{
			}
			if (this.ᜀ(7))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_39;
				}
				if (false)
				{
				}
				IL_39:
				return (Color)this.ᜑ[7];
			}
			return Color.Empty;
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x00323614 File Offset: 0x00322614
		public void ᜁ(Color A_0)
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
			this.ᜀ(7, A_0);
		}

		// Token: 0x0600358E RID: 13710 RVA: 0x0032365C File Offset: 0x0032265C
		public string ᜅ()
		{
			if (this.ᜀ(1))
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_39;
				}
				if (false)
				{
				}
				IL_39:
				return (string)this.ᜑ[1];
			}
			return string.Empty;
		}

		// Token: 0x0600358F RID: 13711 RVA: 0x003236BC File Offset: 0x003226BC
		public void ᜀ(string A_0)
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
			this.ᜀ(1, A_0);
		}

		// Token: 0x06003590 RID: 13712 RVA: 0x00323700 File Offset: 0x00322700
		public float ᜁ()
		{
			if (this.ᜀ(0))
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
					break;
				}
				return (float)this.ᜑ[0];
			}
			return 12f;
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x00323760 File Offset: 0x00322760
		public void ᜁ(float A_0)
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
			this.ᜀ(0, A_0);
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x003237A8 File Offset: 0x003227A8
		public float ᜉ()
		{
			if (this.ᜀ(8))
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
					break;
				}
				if (true)
				{
				}
				return (float)this.ᜑ[8];
			}
			return -1f;
		}

		// Token: 0x06003593 RID: 13715 RVA: 0x00323808 File Offset: 0x00322808
		public void ᜄ(float A_0)
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
			this.ᜀ(8, A_0);
		}

		// Token: 0x06003594 RID: 13716 RVA: 0x00323850 File Offset: 0x00322850
		public bool ᜀ()
		{
			if (this.ᜀ(9))
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
					break;
				}
				return (bool)this.ᜑ[9];
			}
			if (true)
			{
			}
			return false;
		}

		// Token: 0x06003595 RID: 13717 RVA: 0x003238AC File Offset: 0x003228AC
		public void ᜁ(bool A_0)
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
			this.ᜀ(9, A_0);
		}

		// Token: 0x06003596 RID: 13718 RVA: 0x003238F0 File Offset: 0x003228F0
		public HorizontalAlignment ᜌ()
		{
			if (this.ᜀ(10))
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
					break;
				}
				return (HorizontalAlignment)this.ᜑ[10];
			}
			return HorizontalAlignment.Left;
		}

		// Token: 0x06003597 RID: 13719 RVA: 0x0032394C File Offset: 0x0032294C
		public void ᜀ(HorizontalAlignment A_0)
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
			this.ᜀ(10, A_0);
		}

		// Token: 0x06003598 RID: 13720 RVA: 0x00323998 File Offset: 0x00322998
		public float ᜎ()
		{
			if (true)
			{
			}
			if (this.ᜀ(12))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				}
				if (false)
				{
				}
				IL_3A:
				return (float)this.ᜑ[12];
			}
			return 0f;
		}

		// Token: 0x06003599 RID: 13721 RVA: 0x003239F8 File Offset: 0x003229F8
		public void ᜀ(float A_0)
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
			this.ᜀ(12, A_0);
		}

		// Token: 0x0600359A RID: 13722 RVA: 0x00323A44 File Offset: 0x00322A44
		public float ᜇ()
		{
			if (this.ᜀ(15))
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
					break;
				}
				return (float)this.ᜑ[15];
			}
			if (true)
			{
			}
			return 0f;
		}

		// Token: 0x0600359B RID: 13723 RVA: 0x00323AA4 File Offset: 0x00322AA4
		public void ᜆ(float A_0)
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
			this.ᜀ(15, A_0);
		}

		// Token: 0x0600359C RID: 13724 RVA: 0x00323AF0 File Offset: 0x00322AF0
		public float ᜐ()
		{
			if (this.ᜀ(14))
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				}
				if (false)
				{
				}
				IL_3A:
				return (float)this.ᜑ[14];
			}
			return 0f;
		}

		// Token: 0x0600359D RID: 13725 RVA: 0x00323B50 File Offset: 0x00322B50
		public void ᜂ(float A_0)
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
			this.ᜀ(14, A_0);
		}

		// Token: 0x0600359E RID: 13726 RVA: 0x00323B9C File Offset: 0x00322B9C
		public float ᜑ()
		{
			if (this.ᜀ(11))
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
					break;
				}
				if (true)
				{
				}
				return (float)this.ᜑ[11];
			}
			return 0f;
		}

		// Token: 0x0600359F RID: 13727 RVA: 0x00323BFC File Offset: 0x00322BFC
		public void ᜃ(float A_0)
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
			this.ᜀ(11, A_0);
		}

		// Token: 0x060035A0 RID: 13728 RVA: 0x00323C48 File Offset: 0x00322C48
		public float ᜏ()
		{
			if (true)
			{
			}
			if (this.ᜀ(13))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				}
				if (false)
				{
				}
				IL_3A:
				return (float)this.ᜑ[13];
			}
			return -1f;
		}

		// Token: 0x060035A1 RID: 13729 RVA: 0x00323CA8 File Offset: 0x00322CA8
		public void ᜅ(float A_0)
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
			this.ᜀ(13, A_0);
		}

		// Token: 0x060035A2 RID: 13730 RVA: 0x00323CF4 File Offset: 0x00322CF4
		public SubSuperScript ᜊ()
		{
			if (this.ᜀ(16))
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3A;
				}
				if (false)
				{
				}
				IL_3A:
				return (SubSuperScript)this.ᜑ[16];
			}
			return SubSuperScript.None;
		}

		// Token: 0x060035A3 RID: 13731 RVA: 0x00323D50 File Offset: 0x00322D50
		public void ᜀ(SubSuperScript A_0)
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
			this.ᜀ(16, A_0);
		}

		// Token: 0x060035A4 RID: 13732 RVA: 0x00323D9C File Offset: 0x00322D9C
		internal ᜂ()
		{
			this.ᜑ = new Dictionary<int, object>();
		}

		// Token: 0x060035A5 RID: 13733 RVA: 0x00323DC8 File Offset: 0x00322DC8
		public spr\u1DE8.ᜂ ᜈ()
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
			return new spr\u1DE8.ᜂ
			{
				ᜑ = new Dictionary<int, object>(this.ᜑ),
				\u1712 = this.\u1712,
				\u1713 = this.\u1713,
				\u1715 = this.\u1715
			};
		}

		// Token: 0x060035A6 RID: 13734 RVA: 0x00323E40 File Offset: 0x00322E40
		private void ᜀ(int A_0, bool A_1)
		{
			if (!this.ᜑ.ContainsKey(A_0))
			{
				if (true)
				{
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E;
				}
				if (false)
				{
				}
				IL_3E:
				this.ᜑ.Add(A_0, A_1);
				return;
			}
			this.ᜑ[A_0] = A_1;
		}

		// Token: 0x060035A7 RID: 13735 RVA: 0x00323EB0 File Offset: 0x00322EB0
		internal bool ᜀ(int A_0)
		{
			if (this.ᜑ.ContainsKey(A_0))
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
					break;
				}
				return true;
			}
			return false;
		}

		// Token: 0x060035A8 RID: 13736 RVA: 0x00323F00 File Offset: 0x00322F00
		private void ᜀ(int A_0, object A_1)
		{
			if (true)
			{
			}
			if (!this.ᜑ.ContainsKey(A_0))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3E;
				}
				if (false)
				{
				}
				IL_3E:
				this.ᜑ.Add(A_0, A_1);
				return;
			}
			this.ᜑ[A_0] = A_1;
		}

		// Token: 0x040028CE RID: 10446
		internal const short ᜀ = 0;

		// Token: 0x040028CF RID: 10447
		internal const short ᜁ = 1;

		// Token: 0x040028D0 RID: 10448
		internal const short ᜂ = 2;

		// Token: 0x040028D1 RID: 10449
		internal const short ᜃ = 3;

		// Token: 0x040028D2 RID: 10450
		internal const short ᜄ = 4;

		// Token: 0x040028D3 RID: 10451
		internal const short ᜅ = 5;

		// Token: 0x040028D4 RID: 10452
		internal const short ᜆ = 6;

		// Token: 0x040028D5 RID: 10453
		internal const short ᜇ = 7;

		// Token: 0x040028D6 RID: 10454
		internal const short ᜈ = 8;

		// Token: 0x040028D7 RID: 10455
		internal const short ᜉ = 9;

		// Token: 0x040028D8 RID: 10456
		internal const short ᜊ = 10;

		// Token: 0x040028D9 RID: 10457
		internal const short ᜋ = 11;

		// Token: 0x040028DA RID: 10458
		internal const short ᜌ = 12;

		// Token: 0x040028DB RID: 10459
		internal const short \u170D = 13;

		// Token: 0x040028DC RID: 10460
		internal const short ᜎ = 14;

		// Token: 0x040028DD RID: 10461
		internal const short ᜏ = 15;

		// Token: 0x040028DE RID: 10462
		internal const short ᜐ = 16;

		// Token: 0x040028DF RID: 10463
		private Dictionary<int, object> ᜑ;

		// Token: 0x040028E0 RID: 10464
		public bool \u1712;

		// Token: 0x040028E1 RID: 10465
		public bool \u1713;

		// Token: 0x040028E2 RID: 10466
		public BuiltinStyle \u1714;

		// Token: 0x040028E3 RID: 10467
		public spr\u1DE8.ᜀ \u1715 = new spr\u1DE8.ᜀ(null);
	}

	// Token: 0x020003B5 RID: 949
	internal class ᜀ
	{
		// Token: 0x060035A9 RID: 13737 RVA: 0x00323F68 File Offset: 0x00322F68
		public ᜀ(spr\u1DE8.ᜀ A_0)
		{
			this.ᜏ = A_0;
		}

		// Token: 0x060035AA RID: 13738 RVA: 0x00323FF0 File Offset: 0x00322FF0
		internal void ᜀ(string A_0, string A_1)
		{
			int a_ = 12;
			spr\u1DE8 spr_u1DE;
			for (;;)
			{
				spr_u1DE = new spr\u1DE8();
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						spr᧓.\u17D1 = new Dictionary<string, int>(20)
						{
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽慎", a_),
								0
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ꖇﾏ", a_),
								1
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ﲇ꞉ﲏ﶑", a_),
								2
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ꮅ", a_),
								3
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ꆋﾏﺑﮓ", a_),
								4
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽", a_),
								5
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ꖇﶉ晴", a_),
								6
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ﲇ꞉ﮋﲓ", a_),
								7
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ꮅﾇ揄", a_),
								8
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ꆋ轢憐ﺕ", a_),
								9
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ﶃ", a_),
								10
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ꖇ黎ﲏ", a_),
								11
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ﲇ꞉ﾋ揄ﺑ", a_),
								12
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ꮅﮇﺉ", a_),
								13
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ꆋﶍ", a_),
								14
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽", a_),
								15
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽", a_),
								16
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽", a_),
								17
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻卽ﲇ", a_),
								18
							},
							{
								ClipboardData.b("ၱ᭳ѵᱷό๻", a_),
								19
							}
						};
						num = 1;
						continue;
					case 1:
						goto IL_94;
					case 2:
					{
						int num2;
						if (spr᧓.\u17D1.TryGetValue(A_0, out num2))
						{
							if (true)
							{
							}
							num = 8;
							continue;
						}
						return;
					}
					case 3:
						if (A_0 != null)
						{
							num = 9;
							continue;
						}
						return;
					case 4:
						for (;;)
						{
							int num2;
							switch (num2)
							{
							case 0:
								goto IL_1D6;
							case 1:
								goto IL_102;
							case 2:
								goto IL_2D0;
							case 3:
								goto IL_254;
							case 4:
								goto IL_1A2;
							case 5:
								goto IL_28A;
							case 6:
								goto IL_298;
							case 7:
								goto IL_F4;
							case 8:
								goto IL_6A;
							case 9:
								goto IL_4C2;
							case 10:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									continue;
								default:
									goto IL_2BC;
								}
								break;
							case 11:
								goto IL_CB;
							case 12:
								goto IL_5C;
							case 13:
								goto IL_27C;
							case 14:
								goto IL_4AA;
							case 15:
								goto IL_79;
							case 16:
								goto IL_10F;
							case 17:
								goto IL_261;
							case 18:
								goto IL_D9;
							case 19:
								goto IL_12A;
							}
							goto Block_4;
						}
						IL_12A:
						this.ᜀ(A_1, ref this.ᜄ, ref this.ᜌ, ref this.ᜈ, spr_u1DE);
						this.ᜀ(A_1, ref this.ᜆ, ref this.ᜎ, ref this.ᜊ, spr_u1DE);
						this.ᜀ(A_1, ref this.ᜃ, ref this.ᜋ, ref this.ᜇ, spr_u1DE);
						this.ᜀ(A_1, ref this.ᜅ, ref this.\u170D, ref this.ᜉ, spr_u1DE);
						num = 6;
						continue;
						Block_4:
						num = 7;
						continue;
					case 5:
						if (spr᧓.\u17D1 == null)
						{
							num = 0;
							continue;
						}
						goto IL_94;
					case 6:
						goto IL_19D;
					case 7:
						return;
					case 8:
						num = 4;
						continue;
					case 9:
						num = 5;
						continue;
					}
					break;
					IL_94:
					num = 2;
				}
			}
			IL_5C:
			this.ᜊ = this.ᜀ(A_1);
			return;
			IL_6A:
			this.ᜋ = spr_u1DE.ᜆ(A_1);
			return;
			IL_79:
			this.ᜀ(A_1, ref this.ᜄ, ref this.ᜌ, ref this.ᜈ, spr_u1DE);
			return;
			IL_CB:
			this.ᜉ = this.ᜀ(A_1);
			return;
			IL_D9:
			this.ᜀ(A_1, ref this.ᜆ, ref this.ᜎ, ref this.ᜊ, spr_u1DE);
			return;
			IL_F4:
			this.ᜎ = spr_u1DE.ᜆ(A_1);
			return;
			IL_102:
			this.ᜅ = spr᱈.ᜀ(A_1);
			return;
			IL_10F:
			this.ᜀ(A_1, ref this.ᜃ, ref this.ᜋ, ref this.ᜇ, spr_u1DE);
			return;
			IL_19D:
			return;
			IL_1A2:
			this.ᜄ = spr᱈.ᜀ(A_1);
			return;
			IL_1D6:
			this.ᜀ = spr᱈.ᜀ(A_1);
			return;
			IL_254:
			this.ᜃ = spr᱈.ᜀ(A_1);
			return;
			IL_261:
			this.ᜀ(A_1, ref this.ᜅ, ref this.\u170D, ref this.ᜉ, spr_u1DE);
			return;
			IL_27C:
			this.ᜇ = this.ᜀ(A_1);
			return;
			IL_28A:
			this.ᜁ = spr_u1DE.ᜆ(A_1);
			return;
			IL_298:
			this.\u170D = spr_u1DE.ᜆ(A_1);
			return;
			IL_2BC:
			if (false)
			{
			}
			this.ᜂ = this.ᜀ(A_1);
			return;
			IL_2D0:
			this.ᜆ = spr᱈.ᜀ(A_1);
			return;
			IL_4AA:
			this.ᜈ = this.ᜀ(A_1);
			return;
			IL_4C2:
			this.ᜌ = spr_u1DE.ᜆ(A_1);
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x003244D0 File Offset: 0x003234D0
		private void ᜀ(string A_0, ref Color A_1, ref float A_2, ref BorderStyle A_3, spr\u1DE8 A_4)
		{
			int a_ = 6;
			switch (0)
			{
			default:
				for (;;)
				{
					string[] array = new string[]
					{
						ClipboardData.b("࡫཭ͯᩱᅳት", a_),
						ClipboardData.b("࡫ŭѯٱᅳት", a_),
						ClipboardData.b("࡫ŭկၱᡳ፵", a_),
						ClipboardData.b("୫ᱭὯᵱɳ፵", a_),
						ClipboardData.b("իmͯ᝱s", a_),
						ClipboardData.b("ͫ᭭ѯűᅳɵ", a_),
						ClipboardData.b("ṫݭᑯᕱᅳ", a_),
						ClipboardData.b("Ὣŭᱯ᭱ၳ", a_),
						ClipboardData.b("ѫݭᑯᙱᅳᡵ", a_),
						ClipboardData.b("ɫŭṯ᝱", a_)
					};
					string[] array2 = A_0.Split(new char[]
					{
						' '
					});
					int num = 0;
					int num2 = 4;
					for (;;)
					{
						int num3;
						switch (num2)
						{
						case 0:
							goto IL_316;
						case 1:
						{
							array2[num] = array2[num].Replace(ClipboardData.b("佫", a_), string.Empty);
							int red = int.Parse(array2[num].Substring(0, 2), NumberStyles.AllowHexSpecifier);
							int green = int.Parse(array2[num].Substring(2, 2), NumberStyles.AllowHexSpecifier);
							int blue = int.Parse(array2[num].Substring(4, 2), NumberStyles.AllowHexSpecifier);
							A_1 = Color.FromArgb(red, green, blue);
							num2 = 0;
							continue;
						}
						case 2:
							if (this.ᜐ)
							{
								num2 = 5;
								continue;
							}
							A_1 = spr᱈.ᜀ(array2[num]);
							if (true)
							{
							}
							num2 = 7;
							continue;
						case 3:
							goto IL_2A0;
						case 4:
							goto IL_2A0;
						case 5:
							A_3 = this.ᜀ(array2[num]);
							this.ᜐ = false;
							num2 = 17;
							continue;
						case 6:
							this.ᜐ = true;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_314;
							default:
								if (false)
								{
								}
								num2 = 15;
								continue;
							}
							break;
						case 7:
							goto IL_316;
						case 8:
							goto IL_314;
						case 9:
						{
							string[] array3;
							if (num3 >= array3.Length)
							{
								num2 = 14;
								continue;
							}
							string b = array3[num3];
							num2 = 12;
							continue;
						}
						case 10:
							return;
						case 11:
							if (array2[num].StartsWith(ClipboardData.b("佫", a_)))
							{
								num2 = 1;
								continue;
							}
							num2 = 16;
							continue;
						case 12:
						{
							string b;
							if (array2[num] == b)
							{
								num2 = 6;
								continue;
							}
							goto IL_185;
						}
						case 13:
							if (num >= array2.Length)
							{
								num2 = 10;
								continue;
							}
							num2 = 11;
							continue;
						case 14:
							num2 = 2;
							continue;
						case 15:
							goto IL_185;
						case 16:
						{
							if (A_4.ᜇ(array2[num]))
							{
								num2 = 18;
								continue;
							}
							string[] array3 = array;
							num3 = 0;
							num2 = 19;
							continue;
						}
						case 17:
							goto IL_316;
						case 18:
							A_2 = A_4.ᜆ(array2[num]);
							num2 = 8;
							continue;
						case 19:
							goto IL_32D;
						case 20:
							goto IL_32D;
						}
						break;
						IL_185:
						num3++;
						num2 = 20;
						continue;
						IL_2A0:
						num2 = 13;
						continue;
						IL_316:
						num++;
						num2 = 3;
						continue;
						IL_314:
						goto IL_316;
						IL_32D:
						num2 = 9;
					}
				}
				return;
			}
		}

		// Token: 0x060035AC RID: 13740 RVA: 0x003248B0 File Offset: 0x003238B0
		internal void ᜀ(RowFormat A_0)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ(A_0.Borders, this.ᜀ);
					num = 13;
					continue;
				case 1:
					if (this.ᜁ == -1f)
					{
						num = 2;
						continue;
					}
					goto IL_15E;
				case 2:
					this.ᜀ(A_0.Borders, 1f);
					num = 6;
					continue;
				case 3:
					goto IL_198;
				case 5:
					goto IL_198;
				case 6:
					goto IL_15E;
				case 7:
					if (this.ᜂ == BorderStyle.None)
					{
						goto IL_15E;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_111;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 12;
						continue;
					}
					break;
				case 8:
					goto IL_111;
				case 9:
					if (this.ᜀ != Color.Empty)
					{
						num = 0;
						continue;
					}
					goto IL_255;
				case 10:
					if (this.ᜀ == Color.Empty)
					{
						num = 11;
						continue;
					}
					goto IL_198;
				case 11:
					this.ᜀ(A_0.Borders, Color.Silver);
					num = 3;
					continue;
				case 12:
					this.ᜀ(A_0.Borders, this.ᜂ);
					this.ᜀ(A_0.Borders, Color.Silver);
					num = 1;
					continue;
				case 13:
					goto IL_10C;
				}
				if (this.ᜁ != -1f)
				{
					num = 8;
					continue;
				}
				A_0.Borders.Left.HasNoneStyle = true;
				A_0.Borders.Right.HasNoneStyle = true;
				A_0.Borders.Top.HasNoneStyle = true;
				A_0.Borders.Bottom.HasNoneStyle = true;
				A_0.Borders.Vertical.HasNoneStyle = true;
				A_0.Borders.Horizontal.HasNoneStyle = true;
				num = 5;
				continue;
				IL_111:
				this.ᜀ(A_0.Borders, this.ᜁ);
				this.ᜀ(A_0.Borders, BorderStyle.Outset);
				num = 10;
				continue;
				IL_15E:
				num = 9;
				continue;
				IL_198:
				num = 7;
			}
			IL_10C:
			IL_255:
			this.ᜀ(A_0.Borders.Top, this.ᜇ, this.ᜋ, this.ᜃ);
			this.ᜀ(A_0.Borders.Bottom, this.ᜈ, this.ᜌ, this.ᜄ);
			this.ᜀ(A_0.Borders.Left, this.ᜉ, this.\u170D, this.ᜅ);
			this.ᜀ(A_0.Borders.Right, this.ᜊ, this.ᜎ, this.ᜆ);
		}

		// Token: 0x060035AD RID: 13741 RVA: 0x00324BA0 File Offset: 0x00323BA0
		internal void ᜀ(CellFormat A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					bool flag = true;
					int num = 6;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_441;
						case 1:
							if (this.ᜇ == BorderStyle.None)
							{
								num = 22;
								continue;
							}
							goto IL_1BB;
						case 2:
							if (this.ᜈ == BorderStyle.None)
							{
								num = 4;
								continue;
							}
							goto IL_2F0;
						case 3:
							goto IL_372;
						case 4:
						{
							Color a_ = this.ᜀ(this.ᜄ, this.ᜏ.ᜄ, this.ᜏ.ᜀ);
							this.ᜀ(A_0.Borders.Bottom, BorderStyle.None, this.ᜌ, a_);
							num = 17;
							continue;
						}
						case 5:
							flag = false;
							this.ᜀ(A_0.Borders, this.ᜁ);
							this.ᜀ(A_0.Borders, BorderStyle.Outset);
							num = 24;
							continue;
						case 6:
							if (this.ᜁ != -1f)
							{
								num = 5;
								continue;
							}
							goto IL_193;
						case 7:
						{
							Color a_2 = this.ᜀ(this.ᜆ, this.ᜏ.ᜆ, this.ᜏ.ᜀ);
							this.ᜀ(A_0.Borders.Right, BorderStyle.None, this.ᜎ, a_2);
							num = 26;
							continue;
						}
						case 8:
							if (this.ᜁ == -1f)
							{
								num = 16;
								continue;
							}
							goto IL_372;
						case 9:
							flag = false;
							this.ᜀ(A_0.Borders, this.ᜂ);
							this.ᜀ(A_0.Borders, Color.Silver);
							num = 8;
							continue;
						case 10:
							if (this.ᜉ == BorderStyle.None)
							{
								num = 20;
								continue;
							}
							goto IL_2AC;
						case 11:
							if (this.ᜊ != BorderStyle.None)
							{
								return;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_441;
							default:
								if (false)
								{
								}
								num = 7;
								continue;
							}
							break;
						case 12:
							num = 1;
							continue;
						case 13:
							if (this.ᜏ != null)
							{
								num = 12;
								continue;
							}
							return;
						case 14:
							goto IL_3A9;
						case 15:
							num = 13;
							continue;
						case 16:
							this.ᜀ(A_0.Borders, 1f);
							num = 3;
							continue;
						case 17:
							goto IL_2F0;
						case 18:
							if (true)
							{
							}
							if (this.ᜀ != Color.Empty)
							{
								num = 19;
								continue;
							}
							goto IL_3A9;
						case 19:
							this.ᜀ(A_0.Borders, this.ᜀ);
							num = 14;
							continue;
						case 20:
						{
							Color a_3 = this.ᜀ(this.ᜅ, this.ᜏ.ᜅ, this.ᜏ.ᜀ);
							this.ᜀ(A_0.Borders.Left, BorderStyle.None, this.\u170D, a_3);
							num = 23;
							continue;
						}
						case 21:
							goto IL_1BB;
						case 22:
						{
							Color a_4 = this.ᜀ(this.ᜃ, this.ᜏ.ᜃ, this.ᜏ.ᜀ);
							this.ᜀ(A_0.Borders.Top, BorderStyle.None, this.ᜋ, a_4);
							num = 21;
							continue;
						}
						case 23:
							goto IL_2AC;
						case 24:
							goto IL_193;
						case 25:
							if (this.ᜂ != BorderStyle.None)
							{
								num = 9;
								continue;
							}
							goto IL_372;
						case 26:
							return;
						}
						break;
						IL_193:
						num = 25;
						continue;
						IL_1BB:
						num = 2;
						continue;
						IL_2AC:
						num = 11;
						continue;
						IL_2F0:
						num = 10;
						continue;
						IL_372:
						num = 18;
						continue;
						IL_3A9:
						this.ᜀ(A_0.Borders.Top, this.ᜇ, this.ᜋ, this.ᜃ);
						this.ᜀ(A_0.Borders.Bottom, this.ᜈ, this.ᜌ, this.ᜄ);
						this.ᜀ(A_0.Borders.Left, this.ᜉ, this.\u170D, this.ᜅ);
						this.ᜀ(A_0.Borders.Right, this.ᜊ, this.ᜎ, this.ᜆ);
						num = 0;
						continue;
						IL_441:
						if (!flag)
						{
							return;
						}
						num = 15;
					}
				}
				return;
			}
		}

		// Token: 0x060035AE RID: 13742 RVA: 0x00325054 File Offset: 0x00324054
		private Color ᜀ(Color A_0, Color A_1, Color A_2)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 != Color.Empty)
					{
						num = 2;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_82;
					}
					break;
				case 1:
					return A_0;
				case 2:
					goto IL_60;
				}
				IL_20:
				if (A_0 != Color.Empty)
				{
					num = 1;
					continue;
				}
				num = 0;
				continue;
				goto IL_20;
			}
			return A_0;
			IL_60:
			if (true)
			{
			}
			return A_1;
			IL_82:
			if (false)
			{
			}
			return A_2;
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x003250EC File Offset: 0x003240EC
		private void ᜀ(Border A_0, BorderStyle A_1, float A_2, Color A_3)
		{
			int num = 11;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0.Color = Color.Silver;
					num = 7;
					continue;
				case 1:
					if (A_3 != Color.Empty)
					{
						num = 9;
						continue;
					}
					return;
				case 2:
					return;
				case 3:
					if (A_1 != BorderStyle.None)
					{
						num = 5;
						continue;
					}
					goto IL_141;
				case 4:
					A_0.LineWidth = A_2;
					A_0.BorderType = A_1;
					num = 12;
					continue;
				case 5:
					A_0.BorderType = A_1;
					A_0.Color = Color.Silver;
					num = 10;
					continue;
				case 6:
					goto IL_141;
				case 7:
					goto IL_16A;
				case 8:
					A_0.LineWidth = 1f;
					num = 6;
					continue;
				case 9:
					A_0.Color = A_3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_119;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 10:
					if (true)
					{
					}
					if (A_2 == -1f)
					{
						num = 8;
						continue;
					}
					goto IL_141;
				case 12:
					goto IL_119;
				}
				if (A_2 != -1f)
				{
					num = 4;
					continue;
				}
				goto IL_16A;
				IL_119:
				if (A_3 == Color.Empty)
				{
					num = 0;
					continue;
				}
				goto IL_16A;
				IL_141:
				num = 1;
				continue;
				IL_16A:
				num = 3;
			}
		}

		// Token: 0x060035B0 RID: 13744 RVA: 0x00325284 File Offset: 0x00324284
		private void ᜀ(Borders A_0, float A_1)
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
			A_0.LineWidth = A_1;
			A_0.Horizontal.LineWidth = A_1;
			A_0.Vertical.LineWidth = A_1;
		}

		// Token: 0x060035B1 RID: 13745 RVA: 0x003252E0 File Offset: 0x003242E0
		private void ᜀ(Borders A_0, Color A_1)
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
			A_0.Color = A_1;
			A_0.Horizontal.Color = A_1;
			A_0.Vertical.Color = A_1;
		}

		// Token: 0x060035B2 RID: 13746 RVA: 0x0032533C File Offset: 0x0032433C
		private void ᜀ(Borders A_0, BorderStyle A_1)
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
			A_0.BorderType = A_1;
			A_0.Horizontal.BorderType = A_1;
			A_0.Vertical.BorderType = A_1;
		}

		// Token: 0x060035B3 RID: 13747 RVA: 0x00325398 File Offset: 0x00324398
		private BorderStyle ᜀ(string A_0)
		{
			int a_ = 6;
			int num = 4;
			for (;;)
			{
				string key;
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
				{
					int num2;
					switch (num2)
					{
					case 0:
						return BorderStyle.DashLargeGap;
					case 1:
						return BorderStyle.Dot;
					case 2:
						return BorderStyle.Double;
					case 3:
						return BorderStyle.Engrave3D;
					case 4:
						return BorderStyle.Inset;
					case 5:
						return BorderStyle.Outset;
					case 6:
						return BorderStyle.Emboss3D;
					case 7:
						return BorderStyle.Single;
					case 8:
					case 9:
						return BorderStyle.None;
					default:
						num = 5;
						continue;
					}
					break;
				}
				case 2:
					spr᧓.\u17D2 = new Dictionary<string, int>(10)
					{
						{
							ClipboardData.b("࡫཭ͯᩱᅳት", a_),
							0
						},
						{
							ClipboardData.b("࡫ŭѯٱᅳት", a_),
							1
						},
						{
							ClipboardData.b("࡫ŭկၱᡳ፵", a_),
							2
						},
						{
							ClipboardData.b("୫ᱭὯᵱɳ፵", a_),
							3
						},
						{
							ClipboardData.b("իmͯ᝱s", a_),
							4
						},
						{
							ClipboardData.b("ͫ᭭ѯűᅳɵ", a_),
							5
						},
						{
							ClipboardData.b("ṫݭᑯᕱᅳ", a_),
							6
						},
						{
							ClipboardData.b("Ὣŭᱯ᭱ၳ", a_),
							7
						},
						{
							ClipboardData.b("ɫŭṯ᝱", a_),
							8
						},
						{
							ClipboardData.b("ѫݭᑯᙱᅳᡵ", a_),
							9
						}
					};
					num = 9;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return BorderStyle.None;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						num = 8;
						continue;
					}
					break;
				case 5:
					num = 6;
					continue;
				case 6:
					goto IL_1DA;
				case 7:
				{
					int num2;
					if (spr᧓.\u17D2.TryGetValue(key, out num2))
					{
						num = 0;
						continue;
					}
					return BorderStyle.None;
				}
				case 8:
					if (spr᧓.\u17D2 == null)
					{
						num = 2;
						continue;
					}
					goto IL_60;
				case 9:
					goto IL_60;
				}
				if ((key = A_0.ToLower()) != null)
				{
					num = 3;
					continue;
				}
				return BorderStyle.None;
				IL_60:
				num = 7;
			}
			return BorderStyle.Inset;
			IL_1DA:
			return BorderStyle.None;
		}

		// Token: 0x040028E4 RID: 10468
		public Color ᜀ = Color.Empty;

		// Token: 0x040028E5 RID: 10469
		public float ᜁ = -1f;

		// Token: 0x040028E6 RID: 10470
		public BorderStyle ᜂ;

		// Token: 0x040028E7 RID: 10471
		public Color ᜃ = Color.Empty;

		// Token: 0x040028E8 RID: 10472
		public Color ᜄ = Color.Empty;

		// Token: 0x040028E9 RID: 10473
		public Color ᜅ = Color.Empty;

		// Token: 0x040028EA RID: 10474
		public Color ᜆ = Color.Empty;

		// Token: 0x040028EB RID: 10475
		public BorderStyle ᜇ;

		// Token: 0x040028EC RID: 10476
		public BorderStyle ᜈ;

		// Token: 0x040028ED RID: 10477
		public BorderStyle ᜉ;

		// Token: 0x040028EE RID: 10478
		public BorderStyle ᜊ;

		// Token: 0x040028EF RID: 10479
		public float ᜋ = -1f;

		// Token: 0x040028F0 RID: 10480
		public float ᜌ = -1f;

		// Token: 0x040028F1 RID: 10481
		public float \u170D = -1f;

		// Token: 0x040028F2 RID: 10482
		public float ᜎ = -1f;

		// Token: 0x040028F3 RID: 10483
		public spr\u1DE8.ᜀ ᜏ;

		// Token: 0x040028F4 RID: 10484
		private bool ᜐ;
	}

	// Token: 0x020003B6 RID: 950
	internal enum ThreeState
	{
		// Token: 0x040028F6 RID: 10486
		False,
		// Token: 0x040028F7 RID: 10487
		True,
		// Token: 0x040028F8 RID: 10488
		Unknown
	}
}
