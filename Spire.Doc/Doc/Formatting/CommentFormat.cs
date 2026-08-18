using System;
using System.Collections;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Fields;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x02000470 RID: 1136
	public class CommentFormat : DocumentSerializable
	{
		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06003E86 RID: 16006 RVA: 0x0039C424 File Offset: 0x0039B424
		// (set) Token: 0x06003E87 RID: 16007 RVA: 0x0039C468 File Offset: 0x0039B468
		public string Initial
		{
			get
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
			set
			{
				int a_ = 7;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_65;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				if (value.Length > 9)
				{
					throw new ArgumentOutOfRangeException(ClipboardData.b("⑬Ůᡰݲᱴᙶᕸ", a_), ClipboardData.b("㡬ᱮᑰŲٴ坶ၸᕺᑼ୾ꦈﶔ랖膠솢삤螦얨캪\udeac\udcae醰잲\uddb4횶ힸ鮺貼达냂별꫆ꯈ꓊ꇌ볎￐", a_));
				}
				IL_65:
				this.ᜁ = value;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06003E88 RID: 16008 RVA: 0x0039C4E4 File Offset: 0x0039B4E4
		// (set) Token: 0x06003E89 RID: 16009 RVA: 0x0039C528 File Offset: 0x0039B528
		public string Author
		{
			get
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
				return this.ᜀ;
			}
			set
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
				this.ᜀ = value;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06003E8A RID: 16010 RVA: 0x0039C56C File Offset: 0x0039B56C
		// (set) Token: 0x06003E8B RID: 16011 RVA: 0x0039C5B0 File Offset: 0x0039B5B0
		internal int BookmarkStartOffset
		{
			get
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
				return this.ᜂ;
			}
			set
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
				this.ᜂ = value;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06003E8C RID: 16012 RVA: 0x0039C5F4 File Offset: 0x0039B5F4
		// (set) Token: 0x06003E8D RID: 16013 RVA: 0x0039C638 File Offset: 0x0039B638
		internal int BookmarkEndOffset
		{
			get
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
			set
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
				this.ᜃ = value;
			}
		}

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06003E8E RID: 16014 RVA: 0x0039C67C File Offset: 0x0039B67C
		// (set) Token: 0x06003E8F RID: 16015 RVA: 0x0039C6C0 File Offset: 0x0039B6C0
		internal int TagBkmk
		{
			get
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
				return this.ᜄ;
			}
			set
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
				this.ᜄ = value;
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06003E90 RID: 16016 RVA: 0x0039C704 File Offset: 0x0039B704
		// (set) Token: 0x06003E91 RID: 16017 RVA: 0x0039C748 File Offset: 0x0039B748
		internal int Position
		{
			get
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
			set
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
				this.ᜅ = value;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06003E92 RID: 16018 RVA: 0x0039C78C File Offset: 0x0039B78C
		internal int StartTextPos
		{
			get
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
				return this.ᜅ - this.ᜂ;
			}
		}

		// Token: 0x06003E93 RID: 16019 RVA: 0x0039C7D4 File Offset: 0x0039B7D4
		public CommentFormat() : base(null, null)
		{
		}

		// Token: 0x06003E94 RID: 16020 RVA: 0x0039C814 File Offset: 0x0039B814
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 5;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_104;
					case 1:
						writer.WriteValue(ClipboardData.b("⥪ɬnᩰṲᑴնቸ⡺ॼṾ햄愈", a_), this.ᜂ);
						num = 9;
						continue;
					case 2:
						writer.WriteValue(ClipboardData.b("㹪Ṭ੮Ͱ㩲᭴Ṷ൸ቺᱼ፾", a_), this.ᜁ);
						num = 13;
						continue;
					case 3:
						if (this.ᜂ != -1)
						{
							num = 1;
							continue;
						}
						goto IL_152;
					case 4:
						goto IL_1A7;
					case 5:
						goto IL_1F2;
					case 6:
						if (this.ᜄ != -1)
						{
							num = 8;
							continue;
						}
						goto IL_1F2;
					case 7:
						if (this.ᜁ != "")
						{
							num = 2;
							continue;
						}
						goto IL_CC;
					case 8:
						writer.WriteValue(ClipboardData.b("㽪౬࡮㍰ᡲᡴᱶ", a_), this.ᜄ);
						num = 5;
						continue;
					case 9:
						goto IL_152;
					case 10:
						writer.WriteValue(ClipboardData.b("㹪Ṭ੮Ͱ", a_), this.ᜀ);
						num = 4;
						continue;
					case 11:
						writer.WriteValue(ClipboardData.b("⥪ɬnᩰṲᑴնቸ㹺፼᭾톀", a_), this.ᜃ);
						num = 0;
						continue;
					case 12:
						if (this.ᜃ != -1)
						{
							num = 11;
							continue;
						}
						goto IL_104;
					case 13:
						goto IL_CC;
					case 14:
						if (this.ᜀ != "")
						{
							goto IL_F4;
						}
						goto IL_1A7;
					}
					break;
					IL_CC:
					if (true)
					{
					}
					num = 14;
					continue;
					IL_F4:
					num = 10;
					continue;
					IL_1F2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F4;
					default:
						goto IL_208;
					}
					IL_104:
					num = 6;
					continue;
					IL_152:
					num = 12;
					continue;
					IL_1A7:
					num = 3;
				}
			}
			IL_208:
			if (false)
			{
			}
		}

		// Token: 0x06003E95 RID: 16021 RVA: 0x0039CA30 File Offset: 0x0039BA30
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 17;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 12;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (reader.HasAttribute(ClipboardData.b("⍶ᡸᱺ㽼ᑾ", a_)))
						{
							if (true)
							{
							}
							num = 10;
							continue;
						}
						goto IL_227;
					case 1:
						if (reader.HasAttribute(ClipboardData.b("㕶ᙸᑺᙼቾ풆ﶈﾌﮎ손ﲒ", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_167;
					case 2:
						this.ᜂ = reader.ReadInt(ClipboardData.b("㕶ᙸᑺᙼቾ풆ﶈﾌﮎ손ﲒ", a_));
						num = 7;
						continue;
					case 3:
						goto IL_227;
					case 4:
						goto IL_1CC;
					case 5:
						goto IL_D0;
					case 6:
						this.ᜃ = reader.ReadInt(ClipboardData.b("㕶ᙸᑺᙼቾ슆\udd8c", a_));
						num = 14;
						continue;
					case 7:
						goto IL_167;
					case 8:
						if (reader.HasAttribute(ClipboardData.b("㕶ᙸᑺᙼቾ슆\udd8c", a_)))
						{
							num = 6;
							continue;
						}
						goto IL_104;
					case 9:
						this.ᜁ = reader.ReadString(ClipboardData.b("≶੸Ṻོ㙾ﺌ", a_));
						num = 4;
						continue;
					case 10:
						this.ᜄ = reader.ReadInt(ClipboardData.b("⍶ᡸᱺ㽼ᑾ", a_));
						num = 3;
						continue;
					case 11:
						this.ᜀ = reader.ReadString(ClipboardData.b("≶੸Ṻོ", a_));
						num = 5;
						continue;
					case 12:
						if (reader.HasAttribute(ClipboardData.b("≶੸Ṻོ", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_D0;
					case 13:
						if (reader.HasAttribute(ClipboardData.b("≶੸Ṻོ㙾ﺌ", a_)))
						{
							goto IL_F4;
						}
						goto IL_1CC;
					case 14:
						goto IL_104;
					}
					break;
					IL_D0:
					num = 13;
					continue;
					IL_F4:
					num = 9;
					continue;
					IL_227:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F4;
					default:
						goto IL_23D;
					}
					IL_104:
					num = 0;
					continue;
					IL_167:
					num = 8;
					continue;
					IL_1CC:
					num = 1;
				}
			}
			IL_23D:
			if (false)
			{
			}
		}

		// Token: 0x06003E96 RID: 16022 RVA: 0x0039CC80 File Offset: 0x0039BC80
		public CommentFormat Clone(IDocument doc)
		{
			CommentFormat commentFormat;
			for (;;)
			{
				IL_28:
				commentFormat = new CommentFormat();
				commentFormat.ᜁ = this.ᜁ;
				commentFormat.ᜀ = this.ᜀ;
				commentFormat.ᜃ = this.ᜃ;
				commentFormat.ᜂ = this.ᜂ;
				for (;;)
				{
					IL_5E:
					int num = 4;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							return commentFormat;
						case 1:
							if (this.ᜀ(doc, this.ᜄ))
							{
								num = 3;
								continue;
							}
							commentFormat.ᜄ = this.ᜄ;
							num = 5;
							continue;
						case 2:
							num = 1;
							continue;
						case 3:
							goto IL_B1;
						case 4:
							if (doc == base.Document)
							{
								goto IL_B1;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_5E;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						case 5:
							return commentFormat;
						}
						goto IL_28;
						IL_B1:
						commentFormat.ᜄ = spr\u180D.ᜁ(this.ᜄ);
						num = 0;
					}
				}
			}
			return commentFormat;
		}

		// Token: 0x06003E97 RID: 16023 RVA: 0x0039CD90 File Offset: 0x0039BD90
		private bool ᜀ(IDocument A_0, int A_1)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					IEnumerator enumerator = A_0.Sections.GetEnumerator();
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							try
							{
								num = 0;
								for (;;)
								{
									IEnumerator enumerator3;
									switch (num)
									{
									case 1:
										try
										{
											num = 0;
											for (;;)
											{
												switch (num)
												{
												case 1:
													goto IL_260;
												case 2:
													goto IL_26C;
												case 3:
													try
													{
														num = 4;
														bool result;
														for (;;)
														{
															switch (num)
															{
															case 0:
																goto IL_212;
															case 1:
															{
																Comment comment;
																if (comment != null)
																{
																	num = 8;
																	continue;
																}
																break;
															}
															case 2:
															{
																IEnumerator enumerator2;
																if (!enumerator2.MoveNext())
																{
																	num = 5;
																	continue;
																}
																IParagraphBase paragraphBase = (IParagraphBase)enumerator2.Current;
																Comment comment = paragraphBase as Comment;
																num = 1;
																continue;
															}
															case 3:
															{
																Comment comment;
																if (comment.Format.TagBkmk == A_1)
																{
																	num = 7;
																	continue;
																}
																break;
															}
															case 5:
																num = 0;
																continue;
															case 6:
																goto IL_1DE;
															case 7:
																result = true;
																num = 6;
																continue;
															case 8:
																num = 3;
																continue;
															}
															IL_1E3:
															num = 2;
															continue;
															goto IL_1E3;
														}
														IL_1DE:
														return result;
														IL_212:
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
																		num = 1;
																		continue;
																	}
																	goto IL_25F;
																case 1:
																	disposable.Dispose();
																	num = 2;
																	continue;
																case 2:
																	goto IL_25D;
																}
																break;
															}
														}
														IL_25D:
														IL_25F:;
													}
													goto IL_260;
												case 4:
												{
													if (!enumerator3.MoveNext())
													{
														num = 1;
														continue;
													}
													Paragraph paragraph = (Paragraph)enumerator3.Current;
													IEnumerator enumerator2 = paragraph.Items.GetEnumerator();
													num = 3;
													continue;
												}
												}
												IL_105:
												num = 4;
												continue;
												goto IL_105;
												IL_260:
												num = 2;
											}
											IL_26C:
											break;
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
														goto IL_2B7;
													case 2:
														if (disposable2 != null)
														{
															num = 0;
															continue;
														}
														goto IL_2B9;
													}
													break;
												}
											}
											IL_2B7:
											IL_2B9:;
										}
										goto IL_2BA;
									case 2:
										num = 4;
										continue;
									case 3:
										if (!enumerator.MoveNext())
										{
											num = 2;
											continue;
										}
										goto IL_2BA;
									case 4:
										goto IL_2F6;
									}
									IL_B2:
									if (true)
									{
									}
									num = 3;
									continue;
									goto IL_B2;
									IL_2BA:
									Section section = (Section)enumerator.Current;
									enumerator3 = section.Body.Paragraphs.GetEnumerator();
									num = 1;
								}
								IL_2F6:
								goto IL_3D;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable3 = enumerator as IDisposable;
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
											goto IL_343;
										case 2:
											goto IL_341;
										}
										break;
									}
								}
								IL_341:
								IL_343:;
							}
							return false;
							IL_3D:
							num = 1;
							continue;
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
								if (spr\u180D.ᜂ().ContainsKey(A_1))
								{
									num = 2;
									continue;
								}
								return false;
							}
							break;
						case 2:
							return true;
						}
						break;
					}
				}
				return true;
			}
		}

		// Token: 0x06003E98 RID: 16024 RVA: 0x0039D134 File Offset: 0x0039C134
		internal void ᜁ()
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
			this.ᜄ = spr\u180D.ᜃ().Next();
		}

		// Token: 0x04002DA2 RID: 11682
		private bool \u2609\u0098\u0099\u009F;

		// Token: 0x04002DA3 RID: 11683
		private new string ᜀ = "";

		// Token: 0x04002DA4 RID: 11684
		private string ᜁ = "";

		// Token: 0x04002DA5 RID: 11685
		private byte \u25D8\u00A3\u00A2\u00AC;

		// Token: 0x04002DA6 RID: 11686
		private bool \u25D8\u0093\u0087\u00AB;

		// Token: 0x04002DA7 RID: 11687
		private byte[] \u2609\u00A4\u0091\u00B0;

		// Token: 0x04002DA8 RID: 11688
		private byte \u25D8\u00A6\u00A0\u009A;

		// Token: 0x04002DA9 RID: 11689
		private float[] \u25D9\u00A2\u0086\u0089;

		// Token: 0x04002DAA RID: 11690
		private int ᜂ = -1;

		// Token: 0x04002DAB RID: 11691
		private int ᜃ = -1;

		// Token: 0x04002DAC RID: 11692
		private int ᜄ = -1;

		// Token: 0x04002DAD RID: 11693
		private int ᜅ;
	}
}
