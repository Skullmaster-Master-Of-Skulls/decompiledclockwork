using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Core;
using Spire.Doc.Core.Biff_Records;
using Spire.Doc.Core.DataStreamParser.Escher;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x020001CA RID: 458
internal sealed class sprᴠ
{
	// Token: 0x06001364 RID: 4964 RVA: 0x0013E0DC File Offset: 0x0013D0DC
	internal static spr\u1C39 \u171D()
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
		return spr\u1C39.ᜁ();
	}

	// Token: 0x06001365 RID: 4965 RVA: 0x0013E11C File Offset: 0x0013D11C
	private spr\u2370 \u171C()
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

	// Token: 0x06001366 RID: 4966 RVA: 0x0013E160 File Offset: 0x0013D160
	private void ᜀ(spr\u2370 A_0)
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
		this.ᜈ = A_0;
	}

	// Token: 0x06001367 RID: 4967 RVA: 0x0013E1A4 File Offset: 0x0013D1A4
	private Field \u171B()
	{
		if (this.\u1713.Count <= 0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_40;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			return null;
		}
		IL_40:
		return this.\u1713.Peek();
	}

	// Token: 0x06001368 RID: 4968 RVA: 0x0013E1FC File Offset: 0x0013D1FC
	private List<DocPicture> \u171A()
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_6F;
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_24;
				default:
					if (false)
					{
					}
					if (true)
					{
					}
					this.\u1715 = new List<DocPicture>();
					num = 0;
					continue;
				}
				break;
			}
			goto IL_1C;
			IL_24:
			num = 1;
			continue;
			IL_1C:
			if (this.\u1715 == null)
			{
				goto IL_24;
			}
			break;
		}
		IL_6F:
		return this.\u1715;
	}

	// Token: 0x06001369 RID: 4969 RVA: 0x0013E280 File Offset: 0x0013D280
	private Paragraph \u1719()
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

	// Token: 0x0600136A RID: 4970 RVA: 0x0013E2C4 File Offset: 0x0013D2C4
	private List<Comment> \u1718()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				}
				if (false)
				{
				}
				this.\u170D = new List<Comment>();
				num = 2;
				continue;
			case 2:
				goto IL_6F;
			}
			goto IL_1C;
			IL_2C:
			num = 0;
			continue;
			IL_1C:
			if (true)
			{
			}
			if (this.\u170D == null)
			{
				goto IL_2C;
			}
			break;
		}
		IL_6F:
		return this.\u170D;
	}

	// Token: 0x0600136B RID: 4971 RVA: 0x0013E348 File Offset: 0x0013D348
	private List<Footnote> \u1717()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_24;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					this.ᜎ = new List<Footnote>();
					num = 2;
					continue;
				}
				break;
			case 2:
				goto IL_6F;
			}
			goto IL_1C;
			IL_24:
			num = 0;
			continue;
			IL_1C:
			if (this.ᜎ == null)
			{
				goto IL_24;
			}
			break;
		}
		IL_6F:
		return this.ᜎ;
	}

	// Token: 0x0600136C RID: 4972 RVA: 0x0013E3CC File Offset: 0x0013D3CC
	private List<Footnote> \u1716()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				}
				if (false)
				{
				}
				this.ᜏ = new List<Footnote>();
				num = 2;
				continue;
			case 2:
				goto IL_6F;
			}
			goto IL_1C;
			IL_2C:
			num = 0;
			continue;
			IL_1C:
			if (true)
			{
			}
			if (this.ᜏ == null)
			{
				goto IL_2C;
			}
			break;
		}
		IL_6F:
		return this.ᜏ;
	}

	// Token: 0x0600136D RID: 4973 RVA: 0x0013E450 File Offset: 0x0013D450
	private TextBoxItemCollection \u1715()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_75;
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_24;
				default:
					if (false)
					{
					}
					this.ᜌ = new TextBoxItemCollection(this.ᜉ);
					if (true)
					{
					}
					num = 0;
					continue;
				}
				break;
			}
			goto IL_1C;
			IL_24:
			num = 2;
			continue;
			IL_1C:
			if (this.ᜌ == null)
			{
				goto IL_24;
			}
			break;
		}
		IL_75:
		return this.ᜌ;
	}

	// Token: 0x0600136E RID: 4974 RVA: 0x0013E4DC File Offset: 0x0013D4DC
	private TextBoxItemCollection \u1714()
	{
		if (true)
		{
		}
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				}
				if (false)
				{
				}
				this.ᜋ = new TextBoxItemCollection(this.ᜉ);
				num = 2;
				continue;
			case 2:
				goto IL_75;
			}
			goto IL_24;
			IL_2C:
			num = 1;
			continue;
			IL_24:
			if (this.ᜋ == null)
			{
				goto IL_2C;
			}
			break;
		}
		IL_75:
		return this.ᜋ;
	}

	// Token: 0x0600136F RID: 4975 RVA: 0x0013E568 File Offset: 0x0013D568
	private Dictionary<int, DictionaryEntry> \u1713()
	{
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				}
				if (false)
				{
				}
				this.\u1714 = new Dictionary<int, DictionaryEntry>();
				num = 2;
				continue;
			case 2:
				goto IL_6F;
			}
			goto IL_1C;
			IL_2C:
			num = 0;
			continue;
			IL_1C:
			if (true)
			{
			}
			if (this.\u1714 == null)
			{
				goto IL_2C;
			}
			break;
		}
		IL_6F:
		return this.\u1714;
	}

	// Token: 0x06001370 RID: 4976 RVA: 0x0013E5EC File Offset: 0x0013D5EC
	internal sprᴠ()
	{
		sprᣄ.ᜀ().ᜂ().Clear();
	}

	// Token: 0x06001371 RID: 4977 RVA: 0x0013E658 File Offset: 0x0013D658
	public void ᜀ(sprច A_0, Document A_1)
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
		this.ᜉ = A_1;
		this.ᜀ(A_0);
		this.ᜇ.\u1714();
		this.\u1712();
		this.ᜇ.ᜀ(this.ᜉ.\u171A, A_1.IsEncrypted);
	}

	// Token: 0x06001372 RID: 4978 RVA: 0x0013E6D0 File Offset: 0x0013D6D0
	private void ᜀ(sprច A_0)
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
		this.ᜀ();
		this.ᜃ = 0;
		this.ᜀ(A_0);
		this.ᜇ = A_0;
		A_0.ᜨ().ᜀ(this.ᜉ.FontSubstitutionTable);
		this.ᜆ = this.ᜉ.LastParagraph;
		this.ᜇ.ᜦ().ᜂ(false);
		this.ᜇ.ᜫ().ᜁ(false);
		this.ᜇ.ᜊ().ᜇ(false);
		this.ᜀ(A_0);
		this.ᜅ();
		spr\u1B67.ᜀ(A_0.ᜊ(), this.ᜉ.Sections[0]);
	}

	// Token: 0x06001373 RID: 4979 RVA: 0x0013E7AC File Offset: 0x0013D7AC
	private void \u1712()
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
		this.ᜃ = 0;
		this.ᜂ();
		this.ᜊ();
		this.ᜑ();
		this.ᜏ();
		this.ᜐ();
		this.ᜎ();
		this.\u170D();
		this.ᜌ();
		this.ᜃ();
	}

	// Token: 0x06001374 RID: 4980 RVA: 0x0013E824 File Offset: 0x0013D824
	private void ᜑ()
	{
		for (;;)
		{
			IL_30:
			Section section = null;
			int num = 0;
			int count = this.ᜉ.Sections.Count;
			int num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_B5:
				goto IL_78;
			default:
				if (false)
				{
				}
				num2 = 1;
				break;
			}
			for (;;)
			{
				IL_02:
				if (true)
				{
				}
				switch (num2)
				{
				case 0:
					goto IL_FB;
				case 1:
					goto IL_FB;
				case 2:
					if (num >= count)
					{
						num2 = 5;
						continue;
					}
					section = this.ᜉ.Sections[num];
					this.ᜀ(section);
					num2 = 6;
					continue;
				case 3:
					this.ᜀ(section.Body.Items, false);
					num2 = 4;
					continue;
				case 4:
					goto IL_134;
				case 5:
					return;
				case 6:
					if (section.Body.Items.Count > 0)
					{
						num2 = 3;
						continue;
					}
					this.ᜇ.ᜫ().ᜪ().ᜄ();
					this.ᜇ.ᜅ(0);
					num2 = 7;
					continue;
				case 7:
					goto IL_B5;
				}
				goto IL_30;
				IL_FB:
				num2 = 2;
			}
			IL_134:
			IL_78:
			num++;
			num2 = 0;
			goto IL_02;
		}
	}

	// Token: 0x06001375 RID: 4981 RVA: 0x0013E96C File Offset: 0x0013D96C
	private void ᜐ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				bool flag = false;
				bool flag2 = true;
				int num = 14;
				for (;;)
				{
					sprᮞ sprᮞ;
					Section section;
					BodyRegionCollection bodyRegionCollection;
					int num3;
					int num4;
					int count2;
					switch (num)
					{
					case 0:
						goto IL_2C6;
					case 1:
						goto IL_2C1;
					case 2:
						goto IL_1DB;
					case 3:
						num = 12;
						continue;
					case 4:
						goto IL_320;
					case 5:
						goto IL_37E;
					case 6:
					{
						flag = true;
						int num2;
						num2++;
						num = 30;
						continue;
					}
					case 7:
						goto IL_CC;
					case 8:
						return;
					case 9:
						if (this.ᜉ.Watermark.Type != WatermarkType.NoWatermark)
						{
							num = 17;
							continue;
						}
						goto IL_CC;
					case 10:
						goto IL_2A1;
					case 11:
						goto IL_2A1;
					case 12:
						if (sprᮞ != null)
						{
							num = 26;
							continue;
						}
						return;
					case 13:
						flag2 = false;
						num = 5;
						continue;
					case 14:
						if (this.ᜉ.Watermark.Type != WatermarkType.NoWatermark)
						{
							num = 27;
							continue;
						}
						goto IL_2C6;
					case 15:
						goto IL_EF;
					case 16:
						if (!flag2)
						{
							num = 19;
							continue;
						}
						return;
					case 17:
						num = 25;
						continue;
					case 18:
						goto IL_320;
					case 19:
					{
						sprᮞ = (this.ᜇ.ᜀ(WordSubdocument.HeaderFooter) as sprᮞ);
						sprᮞ.ᜦ().ᜂ(false);
						this.ᜀ(sprᮞ);
						sprᮞ.ᜫ().ᜁ(false);
						int num2 = 0;
						int count = this.ᜉ.Sections.Count;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2C1;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					}
					case 20:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						section = this.ᜉ.Sections[num2];
						num = 22;
						continue;
					}
					case 21:
						this.ᜀ(bodyRegionCollection, (HeaderType)num3);
						num = 7;
						continue;
					case 22:
						if (flag)
						{
							num = 24;
							continue;
						}
						goto IL_EF;
					case 23:
						if (num4 >= count2)
						{
							num = 1;
							continue;
						}
						section = this.ᜉ.Sections[num4];
						num = 29;
						continue;
					case 24:
						sprᮞ.ᜅ();
						num = 15;
						continue;
					case 25:
						if (section.HeadersFooters[num3].WriteWatermark)
						{
							num = 21;
							continue;
						}
						goto IL_CC;
					case 26:
						sprᮞ.ᜄ();
						num = 8;
						continue;
					case 27:
						this.ᜉ();
						num = 0;
						continue;
					case 28:
						if (num3 >= 6)
						{
							if (true)
							{
							}
							num = 6;
							continue;
						}
						bodyRegionCollection = (BodyRegionCollection)section.HeadersFooters[num3].ChildObjects;
						num = 9;
						continue;
					case 29:
						if (!section.HeadersFooters.IsEmpty)
						{
							num = 13;
							continue;
						}
						num4++;
						num = 11;
						continue;
					case 30:
						goto IL_1DB;
					}
					break;
					IL_CC:
					this.ᜀ(sprᮞ, bodyRegionCollection, (HeaderType)num3);
					num3++;
					num = 4;
					continue;
					IL_EF:
					num3 = 0;
					num = 18;
					continue;
					IL_1DB:
					num = 20;
					continue;
					IL_2A1:
					num = 23;
					continue;
					IL_2C6:
					section = null;
					num4 = 0;
					count2 = this.ᜉ.Sections.Count;
					num = 10;
					continue;
					IL_320:
					num = 28;
					continue;
					IL_37E:
					num = 16;
					continue;
					IL_2C1:
					goto IL_37E;
				}
			}
			return;
		}
	}

	// Token: 0x06001376 RID: 4982 RVA: 0x0013ED68 File Offset: 0x0013DD68
	private void ᜀ(BodyRegionCollection A_0, HeaderType A_1)
	{
		int num = 0;
		for (;;)
		{
			Paragraph paragraph;
			switch (num)
			{
			case 1:
			{
				Section section = A_0.Owner.OwnerBase as Section;
				paragraph = new Paragraph(section.Document);
				section.HeadersFooters[(int)A_1].Items.Insert(0, paragraph);
				num = 4;
				continue;
			}
			case 2:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4A;
				default:
					if (false)
					{
					}
					num = 10;
					continue;
				}
				break;
			case 3:
				return;
			case 4:
				goto IL_FA;
			case 5:
				if (paragraph == null)
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_FA;
			case 6:
				goto IL_FA;
			case 7:
				num = 12;
				continue;
			case 8:
				goto IL_176;
			case 9:
			{
				Table a_ = A_0[0] as Table;
				paragraph = this.ᜀ(a_);
				num = 5;
				continue;
			}
			case 10:
				if (A_1 == HeaderType.FirstPageHeader)
				{
					num = 8;
					continue;
				}
				return;
			case 11:
				if (!(A_0[0] is Paragraph))
				{
					num = 9;
					continue;
				}
				paragraph = (A_0[0] as Paragraph);
				num = 6;
				continue;
			case 12:
				if (A_1 != HeaderType.OddHeader)
				{
					num = 2;
					continue;
				}
				goto IL_176;
			}
			goto IL_44;
			IL_4A:
			num = 7;
			continue;
			IL_44:
			if (A_1 != HeaderType.EvenHeader)
			{
				goto IL_4A;
			}
			goto IL_176;
			IL_FA:
			paragraph.Items.Insert(0, this.ᜉ.Watermark);
			num = 3;
			continue;
			IL_176:
			paragraph = null;
			num = 11;
		}
	}

	// Token: 0x06001377 RID: 4983 RVA: 0x0013EF18 File Offset: 0x0013DF18
	private Paragraph ᜀ(Table A_0)
	{
		switch (0)
		{
		default:
		{
			Paragraph result;
			for (;;)
			{
				IEnumerator enumerator = A_0.Rows.GetEnumerator();
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0.Rows.Count > 0)
						{
							num = 1;
							continue;
						}
						goto IL_328;
					case 1:
						goto IL_85;
					case 2:
						try
						{
							num = 4;
							for (;;)
							{
								IEnumerator enumerator3;
								switch (num)
								{
								case 0:
									if (!enumerator.MoveNext())
									{
										num = 2;
										continue;
									}
									goto IL_2A3;
								case 1:
									try
									{
										num = 1;
										for (;;)
										{
											switch (num)
											{
											case 0:
												goto IL_255;
											case 2:
												try
												{
													num = 3;
													for (;;)
													{
														switch (num)
														{
														case 0:
															goto IL_1FB;
														case 1:
														{
															IEnumerator enumerator2;
															if (!enumerator2.MoveNext())
															{
																num = 6;
																continue;
															}
															BodyRegion bodyRegion = (BodyRegion)enumerator2.Current;
															num = 2;
															continue;
														}
														case 2:
														{
															BodyRegion bodyRegion;
															if (bodyRegion is Paragraph)
															{
																num = 4;
																continue;
															}
															break;
														}
														case 4:
															switch ((1 == 1) ? 1 : 0)
															{
															case 0:
															case 2:
																goto IL_1EF;
															default:
															{
																if (false)
																{
																}
																BodyRegion bodyRegion;
																result = (bodyRegion as Paragraph);
																num = 5;
																continue;
															}
															}
															break;
														case 5:
															goto IL_1C7;
														case 6:
															goto IL_1EF;
														}
														IL_1CC:
														num = 1;
														continue;
														goto IL_1CC;
														IL_1EF:
														num = 0;
													}
													IL_1C7:
													goto IL_32A;
													IL_1FB:
													break;
												}
												finally
												{
													for (;;)
													{
														IEnumerator enumerator2;
														IDisposable disposable = enumerator2 as IDisposable;
														num = 2;
														for (;;)
														{
															switch (num)
															{
															case 0:
																goto IL_246;
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
																goto IL_248;
															}
															break;
														}
													}
													IL_246:
													IL_248:;
												}
												goto IL_249;
											case 3:
												goto IL_249;
											case 4:
											{
												if (!enumerator3.MoveNext())
												{
													num = 3;
													continue;
												}
												TableCell tableCell = (TableCell)enumerator3.Current;
												IEnumerator enumerator2 = tableCell.Items.GetEnumerator();
												num = 2;
												continue;
											}
											}
											IL_FD:
											num = 4;
											continue;
											goto IL_FD;
											IL_249:
											num = 0;
										}
										IL_255:
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
													goto IL_2A0;
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
													goto IL_2A2;
												}
												break;
											}
										}
										IL_2A0:
										IL_2A2:;
									}
									goto IL_2A3;
								case 2:
									num = 3;
									continue;
								case 3:
									goto IL_2DA;
								}
								IL_B2:
								num = 0;
								continue;
								goto IL_B2;
								IL_2A3:
								TableRow tableRow = (TableRow)enumerator.Current;
								enumerator3 = tableRow.Cells.GetEnumerator();
								num = 1;
							}
							IL_2DA:
							goto IL_62;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable3 = enumerator as IDisposable;
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_325;
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
										goto IL_327;
									}
									break;
								}
							}
							IL_325:
							IL_327:;
						}
						goto IL_328;
						IL_62:
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_85:
			return A_0.Rows[0].Cells[0].AddParagraph();
			IL_328:
			return null;
			IL_32A:
			if (true)
			{
			}
			return result;
		}
		}
	}

	// Token: 0x06001378 RID: 4984 RVA: 0x0013F2A4 File Offset: 0x0013E2A4
	private void ᜀ(TextBoxItemCollection A_0, WordSubdocument A_1)
	{
		int num = 6;
		for (;;)
		{
			switch (num)
			{
			case 0:
			{
				sprᰗ sprᰗ;
				sprᰗ.ᜄ();
				num = 5;
				continue;
			}
			case 1:
				goto IL_120;
			case 2:
				if (A_0.Count > 0)
				{
					num = 7;
					continue;
				}
				return;
			case 3:
				goto IL_4C;
			case 4:
				num = 2;
				continue;
			case 5:
				return;
			case 7:
			{
				sprᰗ sprᰗ = this.ᜇ.ᜀ(A_1);
				sprᰗ.ᜈ().ᜂ(false);
				sprᰗ.ᜉ().ᜁ(false);
				this.ᜀ(sprᰗ);
				int count = A_0.Count;
				int num2 = 0;
				num = 1;
				continue;
			}
			case 8:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_120;
				default:
				{
					if (false)
					{
					}
					int count;
					int num2;
					if (num2 >= count)
					{
						num = 0;
						continue;
					}
					sprᰗ sprᰗ;
					this.ᜀ(sprᰗ, A_0[num2] as TextBox);
					num2++;
					num = 3;
					continue;
				}
				}
				break;
			}
			if (true)
			{
			}
			if (A_0 != null)
			{
				num = 4;
				continue;
			}
			break;
			IL_4C:
			num = 8;
			continue;
			IL_120:
			goto IL_4C;
		}
	}

	// Token: 0x06001379 RID: 4985 RVA: 0x0013F3D8 File Offset: 0x0013E3D8
	private void ᜀ(sprᰗ A_0, TextBox A_1)
	{
		int a_ = 7;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_5B;
			case 1:
				goto IL_B1;
			case 3:
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
					if (A_0 is spr\u202D)
					{
						num = 1;
						continue;
					}
					goto IL_EC;
				}
				break;
			case 4:
				A_1.Body.AddParagraph().AppendText(ClipboardData.b("恬", a_));
				num = 0;
				continue;
			}
			IL_2D:
			if (A_1.Body.ChildObjects.Count == 0)
			{
				num = 4;
				continue;
			}
			goto IL_5B;
			goto IL_2D;
			IL_5B:
			this.ᜀ((BodyRegionCollection)A_1.Body.ChildObjects, false);
			num = 3;
		}
		IL_B1:
		((spr\u202D)A_0).ᜁ(A_1.Spid);
		return;
		IL_EC:
		((spr\u235C)A_0).ᜂ(A_1.Spid);
	}

	// Token: 0x0600137A RID: 4986 RVA: 0x0013F4E4 File Offset: 0x0013E4E4
	private void ᜏ()
	{
		switch (0)
		{
		default:
		{
			int num = 6;
			for (;;)
			{
				int count;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_5A;
				case 1:
					return;
				case 2:
				{
					count = this.ᜎ.Count;
					sprᰗ sprᰗ = this.ᜇ.ᜀ(WordSubdocument.Footnote);
					sprᰗ.ᜈ().ᜂ(false);
					sprᰗ.ᜉ().ᜁ(false);
					this.ᜀ(sprᰗ);
					num2 = 0;
					num = 7;
					continue;
				}
				case 3:
					if (this.ᜎ.Count > 0)
					{
						num = 2;
						continue;
					}
					return;
				case 4:
					num = 3;
					continue;
				case 5:
				{
					sprᰗ sprᰗ;
					sprᰗ.ᜄ();
					num = 1;
					continue;
				}
				case 7:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_63;
					default:
						if (false)
						{
						}
						goto IL_5A;
					}
					break;
				case 8:
					goto IL_63;
				}
				if (this.ᜎ != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_5A:
				num = 8;
				continue;
				IL_63:
				if (num2 >= count)
				{
					num = 5;
				}
				else
				{
					Footnote footnote = this.ᜎ[num2];
					sprᰗ sprᰗ;
					this.ᜀ(sprᰗ, footnote.TextBody);
					num2++;
					num = 0;
				}
			}
			return;
		}
		}
	}

	// Token: 0x0600137B RID: 4987 RVA: 0x0013F644 File Offset: 0x0013E644
	private void ᜎ()
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			int num = 7;
			for (;;)
			{
				int count;
				int num2;
				switch (num)
				{
				case 0:
					goto IL_62;
				case 1:
				{
					sprᰗ sprᰗ;
					sprᰗ.ᜄ();
					num = 2;
					continue;
				}
				case 2:
					return;
				case 3:
				{
					count = this.\u170D.Count;
					sprᰗ sprᰗ = this.ᜇ.ᜀ(WordSubdocument.Annotation);
					sprᰗ.ᜈ().ᜂ(false);
					sprᰗ.ᜉ().ᜁ(false);
					this.ᜀ(sprᰗ);
					num2 = 0;
					num = 5;
					continue;
				}
				case 4:
					num = 6;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					default:
						if (false)
						{
						}
						goto IL_62;
					}
					break;
				case 6:
					if (this.\u170D.Count > 0)
					{
						num = 3;
						continue;
					}
					return;
				case 8:
					goto IL_6B;
				}
				if (this.\u170D != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_62:
				num = 8;
				continue;
				IL_6B:
				if (num2 >= count)
				{
					num = 1;
				}
				else
				{
					Comment comment = this.\u170D[num2];
					sprᰗ sprᰗ;
					this.ᜀ(sprᰗ, comment.Body);
					num2++;
					num = 0;
				}
			}
			return;
		}
		}
	}

	// Token: 0x0600137C RID: 4988 RVA: 0x0013F7A4 File Offset: 0x0013E7A4
	private void \u170D()
	{
		switch (0)
		{
		default:
		{
			int num = 2;
			for (;;)
			{
				int count;
				int num2;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_63;
					default:
						if (false)
						{
						}
						goto IL_5A;
					}
					break;
				case 1:
					num = 4;
					continue;
				case 3:
				{
					sprᰗ sprᰗ;
					sprᰗ.ᜄ();
					num = 6;
					continue;
				}
				case 4:
					if (this.ᜏ.Count > 0)
					{
						num = 8;
						continue;
					}
					return;
				case 5:
					goto IL_63;
				case 6:
					return;
				case 7:
					goto IL_5A;
				case 8:
				{
					if (true)
					{
					}
					count = this.ᜏ.Count;
					sprᰗ sprᰗ = this.ᜇ.ᜀ(WordSubdocument.Endnote);
					sprᰗ.ᜈ().ᜂ(false);
					sprᰗ.ᜉ().ᜁ(false);
					this.ᜀ(sprᰗ);
					num2 = 0;
					num = 0;
					continue;
				}
				}
				if (this.ᜏ != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_5A:
				num = 5;
				continue;
				IL_63:
				if (num2 >= count)
				{
					num = 3;
				}
				else
				{
					Footnote footnote = this.ᜏ[num2];
					sprᰗ sprᰗ;
					this.ᜀ(sprᰗ, footnote.TextBody);
					num2++;
					num = 7;
				}
			}
			return;
		}
		}
	}

	// Token: 0x0600137D RID: 4989 RVA: 0x0013F904 File Offset: 0x0013E904
	private void ᜌ()
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
		this.ᜀ(this.ᜋ, WordSubdocument.TextBox);
		this.ᜀ(this.ᜌ, WordSubdocument.HeaderTextBox);
	}

	// Token: 0x0600137E RID: 4990 RVA: 0x0013F95C File Offset: 0x0013E95C
	private void ᜀ(sprᮞ A_0, BodyRegionCollection A_1, HeaderType A_2)
	{
		for (;;)
		{
			A_0.ᜀ(A_2);
			this.ᜀ(A_1, false);
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (A_1.Count >= 1)
					{
						num = 4;
						continue;
					}
					return;
				case 2:
					if (!(A_1[A_1.Count - 1] is Table))
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
				case 3:
					A_0.ᜀ(WordChunkType.ParagraphEnd);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_44;
					}
					if (false)
					{
					}
					num = 0;
					continue;
				case 4:
					goto IL_44;
				}
				break;
				IL_44:
				num = 2;
			}
		}
	}

	// Token: 0x0600137F RID: 4991 RVA: 0x0013FA1C File Offset: 0x0013EA1C
	private void ᜀ(sprᰗ A_0, Body A_1)
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
		A_0.ᜏ();
		this.ᜀ((BodyRegionCollection)A_1.ChildObjects, false);
		A_0.ᜐ();
	}

	// Token: 0x06001380 RID: 4992 RVA: 0x0013FA78 File Offset: 0x0013EA78
	private void ᜀ(BodyRegionCollection A_0, bool A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IDocumentObject documentObject = null;
				spr\u2370 spr_u = this.\u171C();
				int num = 0;
				int count = A_0.Count;
				int num2 = 23;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						this.ᜀ(documentObject as Paragraph, BreakType.PageBreak);
						num2 = 38;
						continue;
					case 1:
						goto IL_34A;
					case 2:
						goto IL_55F;
					case 3:
						if (documentObject is IParagraph)
						{
							num2 = 13;
							continue;
						}
						goto IL_27B;
					case 4:
						if (documentObject is IParagraph)
						{
							num2 = 10;
							continue;
						}
						num2 = 24;
						continue;
					case 5:
						if (A_0.Count == num + 1)
						{
							num2 = 47;
							continue;
						}
						goto IL_55F;
					case 6:
					{
						Paragraph paragraph;
						if (paragraph.RemoveEmpty)
						{
							num2 = 27;
							continue;
						}
						goto IL_34A;
					}
					case 7:
						goto IL_27B;
					case 8:
						this.ᜀ(documentObject as ITable);
						num2 = 16;
						continue;
					case 9:
						if (documentObject.NextSibling != null)
						{
							num2 = 19;
							continue;
						}
						goto IL_55F;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_36A;
						default:
						{
							if (false)
							{
							}
							Paragraph paragraph2 = documentObject as Paragraph;
							num2 = 21;
							continue;
						}
						}
						break;
					case 11:
					{
						bool flag;
						if (flag)
						{
							num2 = 36;
							continue;
						}
						goto IL_55F;
					}
					case 12:
						num2 = 40;
						continue;
					case 13:
					{
						Paragraph paragraph = documentObject as Paragraph;
						goto IL_36A;
					}
					case 14:
						if (A_1)
						{
							num2 = 18;
							continue;
						}
						goto IL_1C4;
					case 15:
						goto IL_2D1;
					case 16:
						goto IL_55F;
					case 17:
						goto IL_4E9;
					case 18:
						this.\u171C().ᜉ().ᜃ(this.ᜄ);
						num2 = 35;
						continue;
					case 19:
						num2 = 11;
						continue;
					case 20:
						if ((documentObject as IParagraph).Format.IsColumnBreakAfter)
						{
							num2 = 44;
							continue;
						}
						goto IL_4E9;
					case 21:
					{
						Paragraph paragraph2;
						if (paragraph2.RemoveEmpty)
						{
							num2 = 34;
							continue;
						}
						goto IL_574;
					}
					case 22:
						if (num >= count)
						{
							num2 = 30;
							continue;
						}
						num2 = 29;
						continue;
					case 23:
						goto IL_2D1;
					case 24:
						if (documentObject is ITable)
						{
							num2 = 8;
							continue;
						}
						num2 = 37;
						continue;
					case 25:
						goto IL_22F;
					case 26:
						num2 = 5;
						continue;
					case 27:
						num2 = 39;
						continue;
					case 28:
					{
						bool flag = this.ᜀ(documentObject as spr\u1AE7, A_1);
						num2 = 9;
						continue;
					}
					case 29:
						if (num != 0)
						{
							num2 = 49;
							continue;
						}
						goto IL_4E9;
					case 30:
						return;
					case 31:
						if ((documentObject as Paragraph).Format.PageBreakAfter)
						{
							num2 = 0;
							continue;
						}
						goto IL_2F5;
					case 32:
						goto IL_55F;
					case 33:
					{
						if (true)
						{
						}
						Paragraph paragraph2;
						if (!(paragraph2.Text == string.Empty))
						{
							num2 = 43;
							continue;
						}
						goto IL_55F;
					}
					case 34:
						num2 = 33;
						continue;
					case 35:
						goto IL_1C4;
					case 36:
						spr_u.ᜀ(WordChunkType.ParagraphEnd);
						num2 = 2;
						continue;
					case 37:
						if (documentObject is spr\u1AE7)
						{
							num2 = 28;
							continue;
						}
						goto IL_55F;
					case 38:
						goto IL_2F5;
					case 39:
					{
						Paragraph paragraph;
						if (!(paragraph.Text == string.Empty))
						{
							num2 = 1;
							continue;
						}
						goto IL_27B;
					}
					case 40:
						if (documentObject is IParagraph)
						{
							num2 = 48;
							continue;
						}
						goto IL_4E9;
					case 41:
						if (A_1)
						{
							num2 = 45;
							continue;
						}
						goto IL_22F;
					case 42:
					{
						Paragraph paragraph2;
						if (paragraph2.Format.PageBreakAfter)
						{
							num2 = 26;
							continue;
						}
						goto IL_55F;
					}
					case 43:
						goto IL_574;
					case 44:
						this.ᜀ(documentObject as Paragraph, BreakType.ColumnBreak);
						num2 = 17;
						continue;
					case 45:
						spr_u.ᜉ().ᜌ(true);
						this.\u171C().ᜉ().ᜃ(this.ᜄ);
						num2 = 25;
						continue;
					case 46:
						if (spr_u is spr\u19E9)
						{
							num2 = 12;
							continue;
						}
						goto IL_4E9;
					case 47:
						spr_u.ᜀ(WordChunkType.ParagraphEnd);
						this.ᜇ.ᜀ(WordChunkType.PageBreak);
						num2 = 32;
						continue;
					case 48:
						num2 = 31;
						continue;
					case 49:
						num2 = 14;
						continue;
					}
					break;
					IL_1C4:
					num2 = 3;
					continue;
					IL_22F:
					documentObject = A_0[num];
					num2 = 4;
					continue;
					IL_27B:
					num2 = 46;
					continue;
					IL_2D1:
					num2 = 22;
					continue;
					IL_2F5:
					num2 = 20;
					continue;
					IL_34A:
					spr_u.ᜀ(WordChunkType.ParagraphEnd);
					num2 = 7;
					continue;
					IL_36A:
					num2 = 6;
					continue;
					IL_4E9:
					num2 = 41;
					continue;
					IL_55F:
					num++;
					num2 = 15;
					continue;
					IL_574:
					this.ᜁ(documentObject as IParagraph);
					num2 = 42;
				}
			}
			return;
		}
	}

	// Token: 0x06001381 RID: 4993 RVA: 0x00140030 File Offset: 0x0013F030
	private bool ᜀ(spr\u1AE7 A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			bool flag;
			for (;;)
			{
				BodyRegionCollection bodyRegionCollection = A_0.ᜆ().ᜂ().Items;
				flag = (bodyRegionCollection.LastItem is Paragraph);
				IDocumentObject documentObject = null;
				spr\u2370 spr_u = this.\u171C();
				int num = 0;
				int num2 = 31;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_2F1;
					case 1:
					{
						Paragraph paragraph;
						if (paragraph.Format.PageBreakAfter)
						{
							num2 = 15;
							continue;
						}
						goto IL_582;
					}
					case 2:
						if ((documentObject as IParagraph).Format.IsColumnBreakAfter)
						{
							num2 = 20;
							continue;
						}
						goto IL_50C;
					case 3:
						spr_u.ᜀ(WordChunkType.ParagraphEnd);
						this.ᜇ.ᜀ(WordChunkType.PageBreak);
						num2 = 17;
						continue;
					case 4:
						num2 = 38;
						continue;
					case 5:
						return flag;
					case 6:
						num2 = 40;
						continue;
					case 7:
						if (documentObject is IParagraph)
						{
							num2 = 35;
							continue;
						}
						goto IL_27C;
					case 8:
					{
						Paragraph paragraph;
						if (!(paragraph.Text == string.Empty))
						{
							num2 = 14;
							continue;
						}
						goto IL_582;
					}
					case 9:
						goto IL_2C7;
					case 10:
						if (num != 0)
						{
							num2 = 4;
							continue;
						}
						goto IL_50C;
					case 11:
						goto IL_27C;
					case 12:
						if (num >= bodyRegionCollection.Count)
						{
							num2 = 5;
							continue;
						}
						num2 = 10;
						continue;
					case 13:
						if ((documentObject as Paragraph).Format.PageBreakAfter)
						{
							num2 = 19;
							continue;
						}
						goto IL_2F1;
					case 14:
						goto IL_599;
					case 15:
						num2 = 36;
						continue;
					case 16:
						this.ᜀ(documentObject as ITable);
						if (true)
						{
						}
						num2 = 45;
						continue;
					case 17:
						goto IL_582;
					case 18:
					{
						Paragraph paragraph = documentObject as Paragraph;
						num2 = 44;
						continue;
					}
					case 19:
						this.ᜀ(documentObject as Paragraph, BreakType.PageBreak);
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_52A;
						default:
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
					case 20:
						this.ᜀ(documentObject as Paragraph, BreakType.ColumnBreak);
						num2 = 42;
						continue;
					case 21:
						if (documentObject is IParagraph)
						{
							num2 = 18;
							continue;
						}
						num2 = 41;
						continue;
					case 22:
						this.\u171C().ᜉ().ᜃ(this.ᜄ);
						num2 = 47;
						continue;
					case 23:
						if (documentObject.NextSibling != null)
						{
							num2 = 37;
							continue;
						}
						goto IL_582;
					case 24:
					{
						Paragraph paragraph2;
						if (!(paragraph2.Text == string.Empty))
						{
							num2 = 29;
							continue;
						}
						goto IL_27C;
					}
					case 25:
						num2 = 13;
						continue;
					case 26:
						goto IL_582;
					case 27:
						goto IL_24B;
					case 28:
						spr_u.ᜀ(WordChunkType.ParagraphEnd);
						flag = false;
						num2 = 26;
						continue;
					case 29:
						goto IL_347;
					case 30:
						if (documentObject is spr\u1AE7)
						{
							num2 = 49;
							continue;
						}
						goto IL_582;
					case 31:
						goto IL_2C7;
					case 32:
						if (flag)
						{
							num2 = 28;
							continue;
						}
						goto IL_582;
					case 33:
						goto IL_52A;
					case 34:
						num2 = 8;
						continue;
					case 35:
					{
						Paragraph paragraph2 = documentObject as Paragraph;
						num2 = 39;
						continue;
					}
					case 36:
						if (bodyRegionCollection.Count == num + 1)
						{
							num2 = 3;
							continue;
						}
						goto IL_582;
					case 37:
						num2 = 32;
						continue;
					case 38:
						if (A_1)
						{
							num2 = 22;
							continue;
						}
						goto IL_1DD;
					case 39:
					{
						Paragraph paragraph2;
						if (paragraph2.RemoveEmpty)
						{
							num2 = 43;
							continue;
						}
						goto IL_347;
					}
					case 40:
						if (documentObject is IParagraph)
						{
							num2 = 25;
							continue;
						}
						goto IL_50C;
					case 41:
						if (documentObject is ITable)
						{
							num2 = 16;
							continue;
						}
						num2 = 30;
						continue;
					case 42:
						goto IL_50C;
					case 43:
						num2 = 24;
						continue;
					case 44:
					{
						Paragraph paragraph;
						if (paragraph.RemoveEmpty)
						{
							num2 = 34;
							continue;
						}
						goto IL_599;
					}
					case 45:
						goto IL_582;
					case 46:
						if (spr_u is spr\u19E9)
						{
							num2 = 6;
							continue;
						}
						goto IL_50C;
					case 47:
						goto IL_1DD;
					case 48:
						if (A_1)
						{
							num2 = 33;
							continue;
						}
						goto IL_24B;
					case 49:
						flag = this.ᜀ(documentObject as spr\u1AE7, A_1);
						num2 = 23;
						continue;
					}
					break;
					IL_1DD:
					num2 = 7;
					continue;
					IL_24B:
					documentObject = bodyRegionCollection[num];
					num2 = 21;
					continue;
					IL_27C:
					num2 = 46;
					continue;
					IL_2C7:
					num2 = 12;
					continue;
					IL_2F1:
					num2 = 2;
					continue;
					IL_347:
					spr_u.ᜀ(WordChunkType.ParagraphEnd);
					num2 = 11;
					continue;
					IL_50C:
					num2 = 48;
					continue;
					IL_52A:
					spr_u.ᜉ().ᜌ(true);
					this.\u171C().ᜉ().ᜃ(this.ᜄ);
					num2 = 27;
					continue;
					IL_582:
					num++;
					num2 = 9;
					continue;
					IL_599:
					this.ᜁ(documentObject as IParagraph);
					num2 = 1;
				}
			}
			return flag;
		}
		}
	}

	// Token: 0x06001382 RID: 4994 RVA: 0x00140610 File Offset: 0x0013F610
	private bool ᜀ(bool A_0, BodyRegionCollection A_1, int A_2)
	{
		bool result;
		for (;;)
		{
			result = false;
			int num = 10;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_135;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				case 1:
				{
					Paragraph paragraph;
					this.\u1712.Add((paragraph.Items[0] as BookmarkEnd).Name);
					result = true;
					goto IL_135;
				}
				case 2:
				{
					IDocumentObject documentObject;
					Paragraph paragraph = documentObject as Paragraph;
					num = 5;
					continue;
				}
				case 3:
					return result;
				case 4:
				{
					IDocumentObject documentObject = A_1[A_2];
					num = 6;
					continue;
				}
				case 5:
				{
					Paragraph paragraph;
					if (paragraph.Items[0] is BookmarkEnd)
					{
						num = 0;
						continue;
					}
					return result;
				}
				case 6:
				{
					IDocumentObject documentObject;
					if (documentObject is Paragraph)
					{
						num = 7;
						continue;
					}
					return result;
				}
				case 7:
					num = 8;
					continue;
				case 8:
				{
					IDocumentObject documentObject;
					if ((documentObject as Paragraph).Items.Count == 1)
					{
						num = 2;
						continue;
					}
					return result;
				}
				case 9:
				{
					Paragraph paragraph;
					if ((paragraph.Items[0] as BookmarkEnd).IsCellGroupBkmk)
					{
						num = 1;
						continue;
					}
					return result;
				}
				case 10:
					if (true)
					{
					}
					if (A_0)
					{
						num = 4;
						continue;
					}
					return result;
				}
				break;
				IL_135:
				num = 3;
			}
		}
		return result;
	}

	// Token: 0x06001383 RID: 4995 RVA: 0x00140790 File Offset: 0x0013F790
	private void ᜁ(IParagraph A_0)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_15F:
				if (this.\u171A().Count <= 0)
				{
					return;
				}
				num = 6;
				break;
			case 1:
				goto IL_37;
			default:
				goto IL_37;
			}
			for (;;)
			{
				IL_50:
				int num2;
				int count;
				bool flag;
				bool flag2;
				switch (num)
				{
				case 0:
				{
					if (num2 >= count)
					{
						num = 8;
						continue;
					}
					ParagraphBase a_2 = A_0[num2];
					this.ᜀ(a_2, A_0);
					num2++;
					num = 5;
					continue;
				}
				case 1:
					goto IL_BF;
				case 3:
					num = 10;
					continue;
				case 4:
					flag = false;
					goto IL_1A3;
				case 5:
					goto IL_BF;
				case 6:
					this.ᜀ(new BookmarkStart(this.ᜉ, ClipboardData.b("Ⱳ╴Ṷེ᩸ࡼൾ솂歷ﲎ", a_)));
					this.ᜋ();
					this.ᜀ(new BookmarkEnd(this.ᜉ, ClipboardData.b("Ⱳ╴Ṷེ᩸ࡼൾ솂歷ﲎ", a_)));
					num = 11;
					continue;
				case 7:
					num = 4;
					continue;
				case 8:
					num = 9;
					continue;
				case 9:
					if (flag2)
					{
						num = 3;
						continue;
					}
					return;
				case 10:
					goto IL_15F;
				case 11:
					goto IL_13F;
				case 12:
					flag = true;
					goto IL_1A3;
				}
				if (A_0 != this.\u1719())
				{
					num = 7;
					continue;
				}
				num = 12;
				continue;
				IL_BF:
				num = 0;
				continue;
				IL_1A3:
				flag2 = flag;
				this.ᜀ(A_0);
				num2 = 0;
				count = A_0.Items.Count;
				num = 1;
			}
			IL_13F:
			return;
			IL_37:
			if (true)
			{
			}
			if (false)
			{
			}
			num = 2;
			goto IL_50;
		}
		}
	}

	// Token: 0x06001384 RID: 4996 RVA: 0x00140968 File Offset: 0x0013F968
	private void ᜋ()
	{
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			IL_98:
			num = 1;
			break;
		default:
			if (false)
			{
			}
			goto IL_3E;
		}
		int num2;
		int count;
		for (;;)
		{
			IL_28:
			switch (num)
			{
			case 0:
				return;
			case 1:
				goto IL_56;
			case 2:
				if (num2 >= count)
				{
					if (true)
					{
					}
					num = 0;
					continue;
				}
				goto IL_74;
			case 3:
				goto IL_56;
			}
			goto IL_3E;
			IL_56:
			num = 2;
		}
		return;
		IL_74:
		DocPicture docPicture = this.\u171A()[num2];
		docPicture.PictureCharacterFormat.Hidden = true;
		this.ᜀ(docPicture);
		num2++;
		goto IL_98;
		IL_3E:
		num2 = 0;
		count = this.\u171A().Count;
		num = 3;
		goto IL_28;
	}

	// Token: 0x06001385 RID: 4997 RVA: 0x00140A18 File Offset: 0x0013FA18
	private void ᜀ(ParagraphBase A_0, IParagraph A_1)
	{
		TextRange textRange;
		for (;;)
		{
			textRange = (A_0 as TextRange);
			int num = 42;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 5;
					continue;
				case 1:
					if (A_0 is FieldMark)
					{
						num = 25;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_428;
					default:
						if (false)
						{
						}
						num = 23;
						continue;
					}
					break;
				case 2:
					if (A_0 is DocPicture)
					{
						num = 43;
						continue;
					}
					goto IL_201;
				case 3:
					goto IL_222;
				case 4:
					if (A_0 is IField)
					{
						num = 0;
						continue;
					}
					goto IL_192;
				case 5:
					if (!(A_0 as Field).ConvertedToText)
					{
						num = 41;
						continue;
					}
					goto IL_192;
				case 6:
					goto IL_328;
				case 7:
					goto IL_256;
				case 8:
					if (A_0 is Symbol)
					{
						num = 26;
						continue;
					}
					num = 16;
					continue;
				case 9:
					goto IL_27C;
				case 10:
					goto IL_3DD;
				case 11:
					goto IL_1AE;
				case 12:
					if (A_0 is BookmarkEnd)
					{
						num = 36;
						continue;
					}
					num = 8;
					continue;
				case 13:
				{
					int num2;
					ParagraphItemCollection paragraphItemCollection;
					if (num2 >= paragraphItemCollection.Count)
					{
						num = 7;
						continue;
					}
					this.ᜀ(paragraphItemCollection[num2], A_1);
					num2++;
					num = 19;
					continue;
				}
				case 14:
					if (A_0 is DocOleObject)
					{
						num = 31;
						continue;
					}
					num = 27;
					continue;
				case 15:
					goto IL_E3;
				case 16:
					if (A_0 is ITextBox)
					{
						num = 9;
						continue;
					}
					num = 37;
					continue;
				case 17:
					if (textRange != null)
					{
						num = 11;
						continue;
					}
					num = 2;
					continue;
				case 18:
					goto IL_400;
				case 19:
					goto IL_234;
				case 20:
					if (A_0 is TableOfContent)
					{
						num = 6;
						continue;
					}
					num = 32;
					continue;
				case 21:
					if (A_0 is Break)
					{
						num = 40;
						continue;
					}
					num = 30;
					continue;
				case 22:
					if (A_0 is Footnote)
					{
						num = 18;
						continue;
					}
					num = 21;
					continue;
				case 23:
					if (A_0 is Comment)
					{
						num = 34;
						continue;
					}
					num = 22;
					continue;
				case 24:
					goto IL_38A;
				case 25:
					goto IL_470;
				case 26:
					goto IL_3B0;
				case 27:
					if (A_0 is sprờ)
					{
						if (true)
						{
						}
						num = 28;
						continue;
					}
					return;
				case 28:
				{
					ParagraphItemCollection paragraphItemCollection = (A_0 as sprờ).ᜇ().ᜂ();
					int num2 = 0;
					num = 35;
					continue;
				}
				case 29:
					if (A_0 is BookmarkStart)
					{
						num = 3;
						continue;
					}
					num = 12;
					continue;
				case 30:
					if (A_0 is WatermarkBase)
					{
						num = 39;
						continue;
					}
					num = 20;
					continue;
				case 31:
					goto IL_305;
				case 32:
					if (A_0 is CommentMark)
					{
						num = 24;
						continue;
					}
					num = 14;
					continue;
				case 33:
					goto IL_180;
				case 34:
					goto IL_155;
				case 35:
					goto IL_234;
				case 36:
					goto IL_2E2;
				case 37:
					if (A_0 is spr\u248F)
					{
						num = 10;
						continue;
					}
					num = 1;
					continue;
				case 38:
					if ((A_0 as DocPicture).ImageBytes != null)
					{
						num = 33;
						continue;
					}
					goto IL_201;
				case 39:
					goto IL_113;
				case 40:
					goto IL_50D;
				case 41:
					goto IL_4BA;
				case 42:
					if (A_0 is FormField)
					{
						num = 15;
						continue;
					}
					num = 4;
					continue;
				case 43:
					num = 38;
					continue;
				}
				break;
				IL_192:
				num = 17;
				continue;
				IL_201:
				num = 29;
				continue;
				IL_234:
				num = 13;
			}
		}
		IL_E3:
		this.ᜀ(A_0 as FormField);
		return;
		IL_113:
		this.ᜀ(A_0 as WatermarkBase);
		return;
		IL_155:
		this.ᜂ(A_0 as Comment);
		return;
		IL_180:
		this.ᜀ(A_0 as DocPicture);
		return;
		IL_1AE:
		this.ᜀ(textRange);
		return;
		IL_222:
		this.ᜀ(A_0 as BookmarkStart);
		return;
		IL_256:
		return;
		IL_27C:
		this.ᜁ(A_0 as TextBox);
		return;
		IL_2E2:
		this.ᜀ(A_0 as BookmarkEnd);
		return;
		IL_305:
		this.ᜀ(A_0 as DocOleObject);
		return;
		IL_328:
		this.ᜀ(A_0 as TableOfContent);
		return;
		IL_38A:
		this.ᜀ(A_0 as CommentMark);
		return;
		IL_3B0:
		this.ᜀ(A_0 as Symbol);
		return;
		IL_3DD:
		this.ᜁ(A_0 as spr\u248F);
		return;
		IL_400:
		this.ᜀ(A_0 as Footnote);
		return;
		IL_428:
		this.ᜀ(A_0 as FieldMark);
		return;
		IL_470:
		goto IL_428;
		IL_4BA:
		this.ᜂ(A_0 as Field);
		return;
		IL_50D:
		this.ᜀ(A_0 as Break, (Paragraph)A_1);
	}

	// Token: 0x06001386 RID: 4998 RVA: 0x00140F44 File Offset: 0x0013FF44
	private void ᜊ()
	{
		for (;;)
		{
			spr\u24E3 spr_u24E = this.ᜉ.Escher;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_63;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_63;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						if (spr_u24E != null)
						{
							num = 1;
							continue;
						}
						return;
					}
					break;
				}
				break;
				IL_63:
				(this.ᜈ as spr\u21B0).ᜀ(spr_u24E);
				num = 0;
			}
		}
	}

	// Token: 0x06001387 RID: 4999 RVA: 0x00140FD0 File Offset: 0x0013FFD0
	private void ᜉ()
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
						num = 10;
						for (;;)
						{
							Section section;
							int num2;
							switch (num)
							{
							case 0:
								if (!enumerator.MoveNext())
								{
									num = 5;
									continue;
								}
								section = (Section)enumerator.Current;
								num2 = 0;
								num = 4;
								continue;
							case 1:
								if (num2 >= 6)
								{
									num = 3;
									continue;
								}
								num = 8;
								continue;
							case 2:
								goto IL_16A;
							case 4:
								goto IL_B6;
							case 5:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_168;
								default:
									if (false)
									{
									}
									num = 6;
									continue;
								}
								break;
							case 6:
								goto IL_18B;
							case 7:
								goto IL_168;
							case 8:
								if (section.HeadersFooters[num2].ChildObjects.Count == 0)
								{
									num = 7;
									continue;
								}
								goto IL_16A;
							case 9:
								goto IL_B6;
							}
							goto IL_9B;
							IL_B6:
							num = 1;
							continue;
							IL_CE:
							num = 0;
							continue;
							IL_9B:
							goto IL_CE;
							IL_168:
							Paragraph entity = new Paragraph(this.ᜉ);
							section.HeadersFooters[num2].ChildObjects.Add(entity);
							num = 2;
							continue;
							IL_16A:
							num2++;
							num = 9;
						}
						IL_18B:
						return;
					}
					finally
					{
						for (;;)
						{
							if (true)
							{
							}
							IDisposable disposable = enumerator as IDisposable;
							num = 1;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_1DD;
								case 1:
									if (disposable != null)
									{
										num = 2;
										continue;
									}
									goto IL_1DF;
								case 2:
									disposable.Dispose();
									num = 0;
									continue;
								}
								break;
							}
						}
						IL_1DD:
						IL_1DF:;
					}
					goto IL_1E0;
				case 2:
					goto IL_1E0;
				case 3:
					this.ᜉ.AddSection();
					num = 2;
					continue;
				}
				if (this.ᜉ.Sections.Count == 0)
				{
					num = 3;
					continue;
				}
				IL_1E0:
				enumerator = this.ᜉ.Sections.GetEnumerator();
				num = 0;
			}
			return;
		}
		}
	}

	// Token: 0x06001388 RID: 5000 RVA: 0x00141218 File Offset: 0x00140218
	private void ᜀ(TextRange A_0)
	{
		for (;;)
		{
			this.ᜀ(A_0.CharacterFormat.CharStyleName, false);
			spr\u1AFF.ᜀ(A_0.CharacterFormat, this.\u171C().ᜈ());
			int num = 7;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.ᜀ(A_0);
					num = 10;
					continue;
				case 1:
					num = 8;
					continue;
				case 2:
					if (A_0.Text.Trim().Length > 0)
					{
						num = 0;
						continue;
					}
					goto IL_150;
				case 3:
					num = 2;
					continue;
				case 4:
					goto IL_120;
				case 5:
					goto IL_96;
				case 6:
					if (A_0.Text != sprឍ.ᜏ)
					{
						num = 9;
						continue;
					}
					this.\u171C().ᜀ(WordChunkType.Footnote);
					num = 4;
					continue;
				case 7:
					if (A_0 is MergeField)
					{
						num = 1;
						continue;
					}
					goto IL_150;
				case 8:
					if (!(A_0 as MergeField).ConvertedToText)
					{
						goto IL_150;
					}
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
						num = 3;
						continue;
					}
					break;
				case 9:
					this.ᜁ(A_0.Text, A_0.SafeText);
					num = 5;
					continue;
				case 10:
					goto IL_AD;
				}
				break;
				IL_150:
				num = 6;
			}
		}
		IL_96:
		IL_AD:
		IL_120:
		this.\u171C().ᜈ().ᜢ().ᜄ();
	}

	// Token: 0x06001389 RID: 5001 RVA: 0x001413B8 File Offset: 0x001403B8
	private void ᜁ(string A_0, bool A_1)
	{
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
			if (!A_1)
			{
				this.\u171C().ᜀ(A_0);
				return;
			}
			break;
		}
		this.\u171C().ᜂ(A_0);
	}

	// Token: 0x0600138A RID: 5002 RVA: 0x00141414 File Offset: 0x00140414
	private void ᜂ(Field A_0)
	{
		switch (0)
		{
		default:
		{
			string a_4;
			for (;;)
			{
				this.\u1713.Push(A_0);
				int num = 34;
				for (;;)
				{
					string text;
					bool a_;
					string a_2;
					string text2;
					string a_3;
					switch (num)
					{
					case 0:
						goto IL_281;
					case 1:
						if (A_0.Type != FieldType.FieldMergeField)
						{
							num = 33;
							continue;
						}
						goto IL_182;
					case 2:
						goto IL_115;
					case 3:
						text = A_0.Code;
						goto IL_212;
					case 4:
						goto IL_257;
					case 5:
						goto IL_140;
					case 6:
						if (true)
						{
						}
						num = 20;
						continue;
					case 7:
						goto IL_4BD;
					case 8:
						goto IL_1AA;
					case 9:
						num = 8;
						continue;
					case 10:
						if (A_0.Type != FieldType.FieldMergeField)
						{
							num = 6;
							continue;
						}
						goto IL_115;
					case 11:
						if (A_0.Type == FieldType.FieldTOCEntry)
						{
							num = 0;
							continue;
						}
						a_ = false;
						num = 10;
						continue;
					case 12:
						goto IL_182;
					case 13:
						goto IL_3F3;
					case 14:
						if (!string.IsNullOrEmpty(A_0.Code))
						{
							num = 22;
							continue;
						}
						num = 35;
						continue;
					case 15:
						if (this.\u1713.Count > 0)
						{
							num = 30;
							continue;
						}
						return;
					case 16:
						goto IL_1AA;
					case 17:
						if (A_0.Type == FieldType.FieldIndexEntry)
						{
							num = 13;
							continue;
						}
						num = 11;
						continue;
					case 18:
						if (A_0.Type == FieldType.FieldNext)
						{
							num = 12;
							continue;
						}
						return;
					case 19:
						goto IL_3CF;
					case 20:
						if (A_0.Type == FieldType.FieldNext)
						{
							num = 2;
							continue;
						}
						goto IL_4BD;
					case 21:
						if ((A_0 as MergeField).TextItems.Count > 0)
						{
							num = 32;
							continue;
						}
						goto IL_2F4;
					case 22:
						num = 3;
						continue;
					case 23:
						goto IL_3CF;
					case 24:
						goto IL_3F8;
					case 25:
						if (A_0 is MergeField)
						{
							num = 28;
							continue;
						}
						goto IL_2F4;
					case 26:
						if (A_0.Type == FieldType.FieldHyperlink)
						{
							num = 36;
							continue;
						}
						num = 14;
						continue;
					case 27:
					{
						int num2;
						int count;
						if (num2 >= count)
						{
							num = 9;
							continue;
						}
						MergeField mergeField;
						TextRange textRange = mergeField.TextItems[num2] as TextRange;
						spr\u1AFF.ᜀ(textRange.CharacterFormat, this.\u171C().ᜈ());
						this.\u171C().ᜀ(textRange.Text);
						num2++;
						num = 5;
						continue;
					}
					case 28:
						num = 21;
						continue;
					case 29:
						(A_0 as IfField).ᜇ();
						num = 24;
						continue;
					case 30:
						this.\u1713.Pop();
						num = 4;
						continue;
					case 31:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2F4;
						default:
							if (false)
							{
							}
							goto IL_140;
						}
						break;
					case 32:
					{
						MergeField mergeField = A_0 as MergeField;
						int num2 = 0;
						int count = mergeField.TextItems.Count;
						num = 31;
						continue;
					}
					case 33:
						num = 18;
						continue;
					case 34:
						if (A_0 is IfField)
						{
							num = 29;
							continue;
						}
						goto IL_3F8;
					case 35:
						text = this.ᜀ(A_0, a_2, text2, a_3);
						goto IL_212;
					case 36:
						a_4 = this.ᜀ(A_0, a_2, text2, a_3);
						num = 23;
						continue;
					}
					break;
					IL_115:
					a_ = true;
					num = 7;
					continue;
					IL_140:
					num = 27;
					continue;
					IL_182:
					num = 25;
					continue;
					IL_1AA:
					this.\u171C().ᜌ();
					num = 15;
					continue;
					IL_212:
					a_4 = text;
					num = 19;
					continue;
					IL_2F4:
					spr\u1AFF.ᜀ(A_0.CharacterFormat, this.\u171C().ᜈ());
					this.\u171C().ᜂ(text2);
					num = 16;
					continue;
					IL_3CF:
					num = 17;
					continue;
					IL_3F8:
					this.ᜀ(A_0.CharacterFormat.CharStyleName, false);
					spr\u1AFF.ᜀ(A_0.CharacterFormat, this.\u171C().ᜈ());
					a_3 = A_0.ConvertSwitchesToString();
					a_2 = this.ᜀ(A_0);
					text2 = this.ᜀ(A_0, a_2);
					a_4 = string.Empty;
					num = 26;
					continue;
					IL_4BD:
					this.\u171C().ᜀ(a_4, A_0, a_);
					num = 1;
				}
			}
			IL_257:
			return;
			IL_281:
			this.ᜀ(a_4);
			return;
			IL_3F3:
			this.\u171C().ᜃ(a_4);
			return;
		}
		}
	}

	// Token: 0x0600138B RID: 5003 RVA: 0x00141918 File Offset: 0x00140918
	private void ᜁ(Field A_0)
	{
		int num = 3;
		for (;;)
		{
			int a_;
			DocOleObject docOleObject;
			switch (num)
			{
			case 0:
				goto IL_A4;
			case 1:
				this.\u171C().ᜈ().ᜄ(a_);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_A4;
				default:
					if (false)
					{
					}
					num = 5;
					continue;
				}
				break;
			case 2:
				return;
			case 4:
				if (int.TryParse(docOleObject.OleStorageName, out a_))
				{
					if (true)
					{
					}
					num = 1;
					continue;
				}
				goto IL_42;
			case 5:
				goto IL_42;
			}
			if (A_0.Owner is DocOleObject)
			{
				num = 0;
				continue;
			}
			break;
			IL_42:
			this.\u171C().ᜈ().ᜋ(true);
			num = 2;
			continue;
			IL_A4:
			docOleObject = (A_0.Owner as DocOleObject);
			a_ = 0;
			num = 4;
		}
	}

	// Token: 0x0600138C RID: 5004 RVA: 0x00141A0C File Offset: 0x00140A0C
	private void ᜀ(ITextRange A_0)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			bool a_2;
			for (;;)
			{
				a_2 = this.\u171C().ᜈ().ᜥ();
				this.\u171C().ᜈ().ᜂ(true);
				MergeField mergeField = A_0 as MergeField;
				int num = 20;
				for (;;)
				{
					int num2;
					int num4;
					switch (num)
					{
					case 0:
					{
						int num3;
						if (num2 < num3 - 1)
						{
							num = 31;
							continue;
						}
						goto IL_63C;
					}
					case 1:
						mergeField.Text = mergeField.Text.ToLower();
						num = 18;
						continue;
					case 2:
						this.\u171C().ᜀ(mergeField.TextBefore);
						num = 3;
						continue;
					case 3:
						goto IL_3F3;
					case 4:
						this.\u171C().ᜀ(mergeField.TextAfter);
						num = 32;
						continue;
					case 5:
					{
						string[] array = mergeField.Text.Split(new char[]
						{
							' '
						});
						num4 = 0;
						num = 9;
						continue;
					}
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_393;
						default:
						{
							if (false)
							{
							}
							string text;
							string str = text[0].ToString().ToUpper();
							text = text.Remove(0, 1);
							string[] array;
							array[num4] = str + text;
							num = 15;
							continue;
						}
						}
						break;
					case 7:
						if (mergeField.TextAfter != "")
						{
							num = 4;
							continue;
						}
						goto IL_6C0;
					case 8:
					{
						mergeField.Text = "";
						string[] array;
						int num3 = array.Length;
						num2 = 0;
						num = 21;
						continue;
					}
					case 9:
						goto IL_603;
					case 10:
						goto IL_393;
					case 11:
						num = 7;
						continue;
					case 12:
						num = 28;
						continue;
					case 13:
						goto IL_603;
					case 14:
						if (mergeField.TextAfter != null)
						{
							num = 11;
							continue;
						}
						goto IL_6C0;
					case 15:
						goto IL_628;
					case 16:
						goto IL_5DD;
					case 17:
					{
						string text;
						if (text != string.Empty)
						{
							num = 6;
							continue;
						}
						goto IL_628;
					}
					case 18:
						goto IL_101;
					case 19:
					{
						int num3;
						if (num2 >= num3)
						{
							num = 30;
							continue;
						}
						MergeField mergeField2 = mergeField;
						string[] array;
						mergeField2.Text += array[num2];
						num = 0;
						continue;
					}
					case 20:
						if (mergeField.TextFormat == TextFormat.Uppercase)
						{
							num = 25;
							continue;
						}
						num = 27;
						continue;
					case 21:
						goto IL_5DD;
					case 22:
						goto IL_101;
					case 23:
						goto IL_101;
					case 24:
						if (mergeField.TextFormat == TextFormat.FirstCapital)
						{
							num = 10;
							continue;
						}
						num = 26;
						continue;
					case 25:
						mergeField.Text = mergeField.Text.ToUpper();
						num = 22;
						continue;
					case 26:
						if (mergeField.TextFormat == TextFormat.Titlecase)
						{
							num = 5;
							continue;
						}
						goto IL_101;
					case 27:
						if (mergeField.TextFormat == TextFormat.Lowercase)
						{
							num = 1;
							continue;
						}
						num = 24;
						continue;
					case 28:
						if (mergeField.TextBefore != "")
						{
							num = 2;
							continue;
						}
						goto IL_3F3;
					case 29:
					{
						string[] array;
						if (num4 >= array.Length)
						{
							num = 8;
							continue;
						}
						string text = array[num4];
						num = 17;
						continue;
					}
					case 30:
						goto IL_101;
					case 31:
					{
						MergeField mergeField3 = mergeField;
						mergeField3.Text += ClipboardData.b("䩩", a_);
						if (true)
						{
						}
						num = 33;
						continue;
					}
					case 32:
						goto IL_2D5;
					case 33:
						goto IL_63C;
					case 34:
						if (mergeField.TextBefore != null)
						{
							num = 12;
							continue;
						}
						goto IL_3F3;
					}
					break;
					IL_101:
					num = 34;
					continue;
					try
					{
						IL_3F3:
						num = 8;
						for (;;)
						{
							double num5;
							switch (num)
							{
							case 0:
							{
								string a_3 = DateTime.Parse(mergeField.Text).ToString(mergeField.DateFormat, DateTimeFormatInfo.CurrentInfo);
								this.\u171C().ᜀ(a_3);
								num = 4;
								continue;
							}
							case 1:
								goto IL_5B5;
							case 2:
								goto IL_5B5;
							case 3:
								if (mergeField.NumberFormat.Contains(ClipboardData.b("佩", a_)))
								{
									num = 6;
									continue;
								}
								goto IL_46A;
							case 4:
								goto IL_5B5;
							case 5:
								mergeField.Text = mergeField.Text.Replace(ClipboardData.b("䙩", a_), ClipboardData.b("䑩", a_)).Replace(ClipboardData.b("䩩", a_), "");
								num5 = double.Parse(mergeField.Text, CultureInfo.InvariantCulture);
								num = 3;
								continue;
							case 6:
								num5 /= 100.0;
								num = 7;
								continue;
							case 7:
								goto IL_46A;
							case 9:
								goto IL_5C1;
							case 10:
								if (mergeField.DateFormat != "")
								{
									num = 0;
									continue;
								}
								this.\u171C().ᜀ(mergeField.Text);
								num = 1;
								continue;
							}
							if (mergeField.NumberFormat != "")
							{
								num = 5;
								continue;
							}
							num = 10;
							continue;
							IL_46A:
							string a_4 = num5.ToString(mergeField.NumberFormat, CultureInfo.InvariantCulture);
							this.\u171C().ᜀ(a_4);
							num = 2;
							continue;
							IL_5B5:
							num = 9;
						}
						IL_5C1:
						goto IL_290;
					}
					catch
					{
						this.\u171C().ᜀ(mergeField.Text);
						goto IL_290;
					}
					goto IL_5DD;
					IL_290:
					num = 14;
					continue;
					IL_393:
					string text2 = mergeField.Text;
					string str2 = text2[0].ToString().ToUpper();
					mergeField.Text = str2 + text2.Remove(0, 1);
					num = 23;
					continue;
					IL_5DD:
					num = 19;
					continue;
					IL_603:
					num = 29;
					continue;
					IL_628:
					num4++;
					num = 13;
					continue;
					IL_63C:
					num2++;
					num = 16;
				}
			}
			IL_2D5:
			IL_6C0:
			this.\u171C().ᜈ().ᜂ(a_2);
			return;
		}
		}
	}

	// Token: 0x0600138D RID: 5005 RVA: 0x00142108 File Offset: 0x00141108
	private void ᜀ(FormField A_0)
	{
		int a_ = 12;
		switch (0)
		{
		default:
			for (;;)
			{
				this.\u1713.Push(A_0);
				spr\u258D spr_u258D = null;
				int num = 11;
				for (;;)
				{
					string text2;
					string text3;
					switch (num)
					{
					case 0:
						num = 12;
						continue;
					case 1:
						goto IL_2B1;
					case 2:
						goto IL_173;
					case 3:
						if ((A_0 as TextFormField).TextRange.Text.Length != 0)
						{
							num = 7;
							continue;
						}
						goto IL_173;
					case 4:
						if ((A_0 as TextFormField).TextRange.Text == ClipboardData.b("灑癓瑕穗硙", a_))
						{
							num = 2;
							continue;
						}
						return;
					case 5:
						return;
					case 6:
						goto IL_106;
					case 7:
						goto IL_99;
					case 8:
						if (spr_u258D != null)
						{
							num = 15;
							continue;
						}
						return;
					case 9:
					{
						string text = text2;
						text2 = string.Concat(new string[]
						{
							text,
							ClipboardData.b("剱", a_),
							A_0.Value,
							ClipboardData.b("剱", a_),
							text3
						});
						num = 1;
						continue;
					}
					case 10:
						spr_u258D = new spr\u258D(A_0.Type);
						sprអ.ᜀ(spr_u258D, A_0);
						num = 6;
						continue;
					case 11:
						if (A_0.HasFFData)
						{
							num = 10;
							continue;
						}
						goto IL_106;
					case 12:
						if (A_0.Value.Length != 0)
						{
							num = 9;
							continue;
						}
						goto IL_2B1;
					case 13:
						if (A_0.Value == null)
						{
							goto IL_2B1;
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
					case 14:
						num = 3;
						continue;
					case 15:
						num = 16;
						continue;
					case 16:
						if (A_0.FormFieldType == FormFieldType.TextInput)
						{
							num = 14;
							continue;
						}
						return;
					case 17:
						if (true)
						{
						}
						(A_0 as TextFormField).TextRange.Text = spr_u258D.ᜆ();
						num = 5;
						continue;
					case 18:
						if (spr_u258D.ᜆ().Length > 0)
						{
							num = 17;
							continue;
						}
						return;
					}
					break;
					IL_99:
					num = 4;
					continue;
					IL_106:
					text2 = spr\u1C8B.ᜀ(A_0.Type);
					spr\u1AFF.ᜀ(A_0.CharacterFormat, this.\u171C().ᜈ());
					text3 = A_0.ConvertSwitchesToString();
					num = 13;
					continue;
					IL_173:
					num = 18;
					continue;
					IL_2B1:
					text2 = ClipboardData.b("剱", a_) + text2 + ClipboardData.b("剱", a_);
					this.\u171C().ᜀ(text2, spr_u258D);
					num = 8;
				}
			}
			return;
		}
	}

	// Token: 0x0600138E RID: 5006 RVA: 0x00142418 File Offset: 0x00141418
	private void ᜀ(ITable A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.\u171C().ᜉ().ᜪ().ᜄ();
				int num = 11;
				for (;;)
				{
					int num2;
					TableRow tableRow;
					int count2;
					switch (num)
					{
					case 0:
						goto IL_37D;
					case 1:
					{
						int count;
						if (num2 >= count)
						{
							num = 20;
							continue;
						}
						this.ᜄ++;
						tableRow = A_0.Rows[num2];
						count2 = tableRow.Cells.Count;
						int num3 = 0;
						goto IL_295;
					}
					case 2:
						if (this.ᜄ == 1)
						{
							num = 21;
							continue;
						}
						this.\u171C().ᜉ().ᜌ(true);
						this.\u171C().ᜉ().ᜃ(this.ᜄ);
						this.\u171C().ᜉ().\u1712(true);
						this.\u171C().ᜉ().ᜇ(true);
						num = 18;
						continue;
					case 3:
						num = 2;
						continue;
					case 4:
					{
						int num4;
						int count3;
						if (num4 >= count3)
						{
							num = 5;
							continue;
						}
						this.\u171C().ᜁ(this.\u1712[num4]);
						num4++;
						num = 15;
						continue;
					}
					case 5:
					{
						this.\u1712.Clear();
						int num3;
						num3++;
						num = 8;
						continue;
					}
					case 6:
						goto IL_324;
					case 7:
					{
						num2 = 0;
						int count = A_0.Rows.Count;
						num = 17;
						continue;
					}
					case 8:
						goto IL_324;
					case 9:
						goto IL_300;
					case 10:
						goto IL_37D;
					case 11:
						if (A_0 != null)
						{
							num = 7;
							continue;
						}
						goto IL_47D;
					case 12:
						goto IL_3F0;
					case 13:
					{
						if (true)
						{
						}
						int num3;
						if (num3 >= count2)
						{
							num = 3;
							continue;
						}
						TableCell tableCell = tableRow.Cells[num3];
						this.\u171C().ᜈ().ᜢ().ᜄ();
						this.\u171C().ᜉ().ᜃ(this.ᜄ);
						this.ᜀ((BodyRegionCollection)tableCell.ChildObjects, true);
						spr\u1AFF.ᜀ(tableCell.CharacterFormat, this.\u171C().ᜈ());
						this.ᜀ(tableCell.CharacterFormat.CharStyleName, false);
						this.\u171C().ᜉ().ᜃ(this.ᜄ);
						this.\u171C().ᜁ(this.ᜄ);
						int num4 = 0;
						int count3 = this.\u1712.Count;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_295;
						default:
							if (false)
							{
							}
							num = 12;
							continue;
						}
						break;
					}
					case 14:
						goto IL_AE;
					case 15:
						goto IL_3F0;
					case 16:
						this.\u171C().ᜉ().ᜌ(true);
						this.\u171C().ᜉ().ᜂ(true);
						num = 0;
						continue;
					case 17:
						goto IL_300;
					case 18:
						goto IL_AE;
					case 19:
						if (this.ᜄ == 1)
						{
							num = 16;
							continue;
						}
						this.\u171C().ᜉ().ᜌ(true);
						this.\u171C().ᜉ().ᜃ(this.ᜄ);
						this.\u171C().ᜉ().\u1712(true);
						this.\u171C().ᜉ().ᜇ(true);
						num = 10;
						continue;
					case 20:
						goto IL_31F;
					case 21:
						this.\u171C().ᜉ().ᜌ(true);
						this.\u171C().ᜉ().ᜂ(true);
						this.\u171C().ᜉ().ᜃ(this.ᜄ);
						num = 14;
						continue;
					}
					break;
					IL_AE:
					this.\u171C().ᜉ().ᜃ(this.ᜄ);
					num = 19;
					continue;
					IL_295:
					num = 6;
					continue;
					IL_300:
					num = 1;
					continue;
					IL_324:
					num = 13;
					continue;
					IL_37D:
					this.ᜀ(this.\u171C(), tableRow, A_0);
					this.\u171C().ᜀ(this.ᜄ, count2);
					this.ᜄ--;
					num2++;
					num = 9;
					continue;
					IL_3F0:
					num = 4;
				}
			}
			IL_31F:
			IL_47D:
			this.\u171C().ᜉ().ᜪ().ᜄ();
			return;
		}
	}

	// Token: 0x0600138F RID: 5007 RVA: 0x001428B8 File Offset: 0x001418B8
	private void ᜀ(spr\u2370 A_0, TableRow A_1, ITable A_2)
	{
		int num = 3;
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_6B;
			default:
				if (false)
				{
				}
				switch (num)
				{
				case 0:
					goto IL_76;
				case 1:
					if (A_1.RowFormat.Sprms == null)
					{
						num = 2;
						continue;
					}
					goto IL_FD;
				case 2:
					goto IL_D2;
				case 4:
					if (A_1.RowFormat.RowDescriptor != null)
					{
						num = 6;
						continue;
					}
					num = 5;
					continue;
				case 5:
					goto IL_6B;
				case 6:
					num = 0;
					continue;
				case 7:
					if (true)
					{
					}
					num = 1;
					continue;
				}
				if (!A_1.RowFormat.HasInvalidSprms)
				{
					num = 7;
					break;
				}
				IL_D2:
				num = 4;
				break;
			}
		}
		IL_6B:
		bool flag = false;
		goto IL_B0;
		IL_76:
		flag = true;
		IL_B0:
		bool a_ = flag;
		this.ᜀ(this.\u171C(), A_1, A_2, a_);
		A_0.ᜉ().ᜃ(this.ᜄ);
		return;
		IL_FD:
		spr\u1B3A.ᜀ(this.\u171C().ᜉ(), A_1.RowFormat);
		spr\u1AFF.ᜀ(A_1.CharacterFormat, A_0.ᜈ());
	}

	// Token: 0x06001390 RID: 5008 RVA: 0x001429EC File Offset: 0x001419EC
	private void ᜀ(IPicture A_0)
	{
		DocPicture docPicture;
		int a_;
		int a_2;
		for (;;)
		{
			docPicture = (A_0 as DocPicture);
			int num = 6;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_E6;
				case 1:
					num = 2;
					continue;
				case 2:
					if (docPicture.ShapeBase == null)
					{
						goto IL_E6;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_60;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 3:
					if (true)
					{
					}
					if (docPicture.TextWrappingStyle == TextWrappingStyle.Inline)
					{
						num = 1;
						continue;
					}
					goto IL_155;
				case 4:
					if (docPicture.ShapeBase.ᜁ().\u1716() != TextWrappingStyle.Inline)
					{
						num = 0;
						continue;
					}
					goto IL_DC;
				case 5:
					goto IL_104;
				case 6:
					docPicture.IsHeaderPicture = (this.\u171C() is sprᮞ || this.\u171C() is spr\u202D);
					a_ = (int)Math.Round((double)(A_0.Height * 20f));
					a_2 = (int)Math.Round((double)(A_0.Width * 20f));
					num = 3;
					continue;
				case 7:
					goto IL_60;
				case 8:
					if (docPicture.ShapeBase == null)
					{
						num = 5;
						continue;
					}
					goto IL_155;
				}
				break;
				IL_60:
				num = 4;
				continue;
				IL_E6:
				num = 8;
			}
		}
		IL_DC:
		this.ᜀ(docPicture, a_, a_2);
		return;
		IL_104:
		goto IL_DC;
		IL_155:
		this.ᜁ(docPicture, a_, a_2);
	}

	// Token: 0x06001391 RID: 5009 RVA: 0x00142B58 File Offset: 0x00141B58
	private void ᜁ(DocPicture A_0, int A_1, int A_2)
	{
		for (;;)
		{
			this.ᜀ(A_0);
			int a_ = (int)Math.Round((double)(A_0.VerticalPosition * 20f));
			int a_2 = (int)Math.Round((double)(A_0.HorizontalPosition * 20f));
			sprᱱ sprᱱ = new sprᱱ();
			sprᱱ.ᜀ(A_0.HorizontalOrigin);
			sprᱱ.ᜀ(A_0.VerticalOrigin);
			sprᱱ.ᜂ(a_2);
			sprᱱ.ᜇ(a_);
			sprᱱ.ᜆ((int)((float)A_2 / 100f * A_0.WidthScale));
			sprᱱ.ᜃ((int)((float)A_1 / 100f * A_0.HeightScale));
			sprᱱ.ᜀ(A_0.TextWrappingType);
			int num = 9;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (true)
					{
					}
					sprᱱ.ᜀ(A_0.ShapeBase.ᜁ().\u1716());
					num = 1;
					continue;
				case 1:
					goto IL_181;
				case 2:
					if (A_0.TextWrappingStyle == TextWrappingStyle.Inline)
					{
						num = 4;
						continue;
					}
					goto IL_FB;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1E8;
					default:
						if (false)
						{
						}
						goto IL_181;
					}
					break;
				case 4:
					num = 8;
					continue;
				case 5:
					if (A_0.ShapeBase.ᜁ().\u1716() != TextWrappingStyle.Inline)
					{
						num = 0;
						continue;
					}
					goto IL_FB;
				case 6:
					if (A_0.EmbedBody != null)
					{
						num = 12;
						continue;
					}
					return;
				case 7:
					num = 5;
					continue;
				case 8:
					if (A_0.ShapeBase != null)
					{
						num = 7;
						continue;
					}
					goto IL_FB;
				case 9:
					if (A_0.TextWrappingStyle == TextWrappingStyle.Behind)
					{
						num = 14;
						continue;
					}
					sprᱱ.ᜃ(A_0.IsUnderText);
					num = 13;
					continue;
				case 10:
					return;
				case 11:
					goto IL_271;
				case 12:
					this.ᜀ(A_0.EmbedBody, sprᱱ.ᜡ());
					num = 10;
					continue;
				case 13:
					goto IL_271;
				case 14:
					goto IL_1E8;
				}
				break;
				IL_FB:
				sprᱱ.ᜀ(A_0.TextWrappingStyle);
				num = 3;
				continue;
				IL_181:
				sprᱱ.ᜀ(A_0.HorizontalAlignment);
				sprᱱ.ᜀ(A_0.VerticalAlignment);
				sprᱱ.ᜄ(A_0.ShapeId);
				sprᱱ.ᜅ(0);
				sprᱱ.ᜀ(A_0.AlternativeText);
				this.\u171C().ᜀ(A_0, sprᱱ);
				num = 6;
				continue;
				IL_1E8:
				sprᱱ.ᜃ(true);
				num = 11;
				continue;
				IL_271:
				num = 2;
			}
		}
	}

	// Token: 0x06001392 RID: 5010 RVA: 0x00142DFC File Offset: 0x00141DFC
	private void ᜀ(Body A_0, int A_1)
	{
		int a_ = 13;
		switch (0)
		{
		default:
		{
			TextBox textBox;
			spr\u2459 spr_u;
			sprẖ sprẖ;
			for (;;)
			{
				textBox = new TextBox(this.ᜉ);
				textBox.ᜀ(A_0);
				int num = 0;
				int count = A_0.Items.Count;
				int num2 = 4;
				for (;;)
				{
					ParagraphBase paragraphBase;
					Paragraph paragraph;
					switch (num2)
					{
					case 0:
						goto IL_242;
					case 1:
					{
						if (num >= count)
						{
							num2 = 20;
							continue;
						}
						BodyRegion bodyRegion = A_0.Items[num];
						num2 = 3;
						continue;
					}
					case 2:
						if ((paragraphBase as BookmarkStart).Name.StartsWith(ClipboardData.b("㱲㥴㉶♸㝺㑼ㅾ쪀", a_)))
						{
							num2 = 5;
							continue;
						}
						goto IL_18D;
					case 3:
					{
						BodyRegion bodyRegion;
						if (bodyRegion is Paragraph)
						{
							num2 = 7;
							continue;
						}
						goto IL_15C;
					}
					case 4:
						goto IL_B8;
					case 5:
						goto IL_2AD;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2B9;
						default:
							if (false)
							{
							}
							goto IL_346;
						}
						break;
					case 7:
					{
						BodyRegion bodyRegion;
						paragraph = (bodyRegion as Paragraph);
						int index = 0;
						int count2 = paragraph.Items.Count;
						num2 = 19;
						continue;
					}
					case 8:
						num2 = 13;
						continue;
					case 9:
						if (spr_u == null)
						{
							num2 = 17;
							continue;
						}
						sprẖ = (spr_u.ᜌ().ᜅ()[267] as sprẖ);
						num2 = 11;
						continue;
					case 10:
						num2 = 2;
						continue;
					case 11:
						if (sprẖ != null)
						{
							num2 = 0;
							continue;
						}
						goto IL_35B;
					case 12:
						goto IL_2B9;
					case 13:
						if (!(paragraphBase as BookmarkEnd).Name.StartsWith(ClipboardData.b("㱲㥴㉶♸㝺㑼ㅾ쪀", a_)))
						{
							num2 = 14;
							continue;
						}
						goto IL_346;
					case 14:
						goto IL_18D;
					case 15:
					{
						int count2;
						if (num >= count2)
						{
							num2 = 18;
							continue;
						}
						int index;
						paragraphBase = paragraph.Items[index];
						num2 = 22;
						continue;
					}
					case 16:
						goto IL_DC;
					case 17:
						return;
					case 18:
						goto IL_15C;
					case 19:
						goto IL_DC;
					case 20:
						textBox.Spid = A_1;
						textBox.Format.TextBoxIdentificator = (float)(this.ᜈ as spr\u21B0).\u171C();
						this.ᜀ(textBox);
						spr_u = (this.ᜉ.Escher.ᜀ(A_1) as spr\u2459);
						num2 = 9;
						continue;
					case 21:
						goto IL_B8;
					case 22:
						if (paragraphBase is BookmarkStart)
						{
							num2 = 10;
							continue;
						}
						goto IL_2AD;
					}
					break;
					IL_B8:
					num2 = 1;
					continue;
					IL_DC:
					num2 = 15;
					continue;
					IL_15C:
					num++;
					if (true)
					{
					}
					num2 = 21;
					continue;
					IL_18D:
					paragraph.Items.Remove(paragraphBase);
					num2 = 6;
					continue;
					IL_2AD:
					num2 = 12;
					continue;
					IL_2B9:
					if (paragraphBase is BookmarkEnd)
					{
						num2 = 8;
						continue;
					}
					IL_346:
					num++;
					num2 = 16;
				}
			}
			IL_242:
			sprẖ.ᜀ((uint)textBox.Format.TextBoxIdentificator);
			return;
			IL_35B:
			spr_u.ᜌ().ᜅ().ᜀ(new sprẖ(267, false, (uint)textBox.Format.TextBoxIdentificator));
			return;
		}
		}
	}

	// Token: 0x06001393 RID: 5011 RVA: 0x0014318C File Offset: 0x0014218C
	private void ᜀ(DocPicture A_0, int A_1, int A_2)
	{
		for (;;)
		{
			IL_5C:
			if (true)
			{
			}
			this.ᜀ(A_0);
			spr\u1AFF.ᜀ(A_0.PictureCharacterFormat, this.\u171C().ᜈ());
			int num = 3;
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
						goto IL_16B;
					case 1:
						num = 8;
						continue;
					case 2:
						if (A_0.Document.\u173C)
						{
							num = 7;
							continue;
						}
						goto IL_1CA;
					case 3:
						if (A_0.PictureShape.ᜀ() != null)
						{
							num = 12;
							continue;
						}
						goto IL_132;
					case 4:
						goto IL_E3;
					case 5:
						if (A_0.PictureShape.ᜀ().ᜅ() != null)
						{
							num = 11;
							continue;
						}
						goto IL_132;
					case 6:
						if (!(A_0.PictureShape.ᜀ().ᜊ().ᜄ() is sprᱪ))
						{
							num = 0;
							continue;
						}
						goto IL_132;
					case 7:
						goto IL_18E;
					case 8:
						if (!A_0.PictureShape.ᜀ().ᜊ().ᜄ().ᜆ())
						{
							num = 4;
							continue;
						}
						goto IL_132;
					case 9:
						if (A_0.IsMetaFile)
						{
							num = 10;
							continue;
						}
						goto IL_16B;
					case 10:
						num = 6;
						continue;
					case 11:
						num = 13;
						continue;
					case 12:
						num = 5;
						continue;
					case 13:
						if (A_0.PictureShape.ᜀ() != null)
						{
							goto IL_125;
						}
						goto IL_E3;
					}
					goto IL_5C;
					IL_E3:
					num = 9;
					continue;
					IL_16B:
					num = 2;
					continue;
				}
				IL_125:
				num = 1;
			}
		}
		IL_132:
		this.\u171C().ᜀ(A_0, A_1, A_2);
		return;
		IL_18E:
		goto IL_132;
		IL_1CA:
		A_0.PictureShape.ᜀ().ᜈ();
		sprᱱ sprᱱ = new sprᱱ();
		sprᱱ.ᜀ(A_0.AlternativeText);
		sprᱱ.ᜀ(A_0.Chromakey);
		sprᱱ.ᜃ(A_0.CropFromBottom);
		sprᱱ.ᜀ(A_0.CropFromLeft);
		sprᱱ.ᜁ(A_0.CropFromRight);
		sprᱱ.ᜂ(A_0.CropFromTop);
		A_0.PictureShape.ᜀ().ᜀ(sprᱱ);
		A_0.PictureShape.ᜊ().ᜀ(A_1, A_2, A_0.HeightScale, A_0.WidthScale);
		(this.ᜈ as spr\u21B0).ᜀ(A_0.PictureShape);
	}

	// Token: 0x06001394 RID: 5012 RVA: 0x00143408 File Offset: 0x00142408
	private void ᜁ(spr\u248F A_0)
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
			this.ᜀ(A_0);
			spr\u1AFF.ᜀ(A_0.ᜌ(), this.\u171C().ᜈ());
			if (!(A_0 is sprẛ))
			{
				(this.ᜈ as spr\u21B0).ᜀ(A_0);
				this.ᜀ(A_0);
				return;
			}
			break;
		}
		(this.ᜈ as spr\u21B0).ᜀ(A_0 as sprẛ);
	}

	// Token: 0x06001395 RID: 5013 RVA: 0x0014349C File Offset: 0x0014249C
	private void ᜀ(spr\u248F A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_0E:
				int num = 0;
				for (;;)
				{
					int num2;
					int count;
					switch (num)
					{
					case 1:
						goto IL_B6;
					case 2:
						goto IL_B6;
					case 3:
						return;
					case 4:
						if (num2 >= count)
						{
							num = 3;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_0E;
						default:
							if (false)
							{
							}
							if (true)
							{
							}
							this.\u1714().Add(A_0.ᜎ()[num2] as TextBox);
							num2++;
							num = 1;
							continue;
						}
						break;
					case 5:
						return;
					case 6:
						goto IL_149;
					case 7:
					{
						int num3;
						int count2;
						if (num3 >= count2)
						{
							num = 5;
							continue;
						}
						this.\u1715().Add(A_0.ᜎ()[num3] as TextBox);
						num3++;
						num = 6;
						continue;
					}
					case 8:
					{
						int num3 = 0;
						int count2 = A_0.ᜎ().Count;
						num = 9;
						continue;
					}
					case 9:
						goto IL_149;
					}
					if (this.ᜈ is sprᮞ)
					{
						num = 8;
						continue;
					}
					num2 = 0;
					count = A_0.ᜎ().Count;
					num = 2;
					continue;
					IL_B6:
					num = 4;
					continue;
					IL_149:
					num = 7;
				}
			}
			return;
		}
	}

	// Token: 0x06001396 RID: 5014 RVA: 0x00143618 File Offset: 0x00142618
	private void ᜀ(ISection A_0)
	{
		int num = 0;
		for (;;)
		{
			switch (num)
			{
			case 1:
				goto IL_42;
			case 2:
				return;
			case 3:
				if (this.ᜃ != 0)
				{
					if (true)
					{
					}
					num = 4;
					continue;
				}
				goto IL_42;
			case 4:
				spr\u1B67.ᜀ(this.ᜇ.ᜊ(), A_0 as Section);
				this.ᜇ.ᜀ(WordChunkType.SectionEnd);
				num = 1;
				continue;
			case 5:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					continue;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			}
			if (this.\u171C() is sprច)
			{
				num = 5;
				continue;
			}
			break;
			IL_42:
			this.ᜊ = A_0;
			this.ᜃ++;
			num = 2;
		}
	}

	// Token: 0x06001397 RID: 5015 RVA: 0x00143708 File Offset: 0x00142708
	private void ᜀ(BookmarkStart A_0)
	{
		int a_ = 7;
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				A_0.ᜀ(ClipboardData.b("≬⍮㑰Ⱳ㥴㹶㝸ぺ", a_) + this.ᜂ);
				num = 1;
				continue;
			case 1:
				goto IL_A7;
			}
			IL_25:
			if (!A_0.Name.StartsWith(ClipboardData.b("≬⍮㑰Ⱳ㥴㹶㝸ぺ", a_)))
			{
				break;
			}
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
				num = 0;
				continue;
			}
			goto IL_25;
		}
		IL_A7:
		this.ᜇ.ᜀ(A_0.Name, A_0);
	}

	// Token: 0x06001398 RID: 5016 RVA: 0x001437D0 File Offset: 0x001427D0
	private void ᜀ(BookmarkEnd A_0)
	{
		int a_ = 5;
		int num = 2;
		for (;;)
		{
			if (true)
			{
			}
			switch (num)
			{
			case 0:
				goto IL_B5;
			case 1:
				A_0.ᜀ(ClipboardData.b("⑪Ⅼ⩮⹰㽲㱴㥶㉸", a_) + this.ᜂ);
				this.ᜂ++;
				num = 0;
				continue;
			}
			IL_2D:
			if (A_0.Name.StartsWith(ClipboardData.b("⑪Ⅼ⩮⹰㽲㱴㥶㉸", a_)))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_11;
				}
				if (false)
				{
				}
				num = 1;
				continue;
			}
			break;
			IL_11:
			goto IL_2D;
		}
		IL_B5:
		this.ᜇ.ᜈ(A_0.Name);
	}

	// Token: 0x06001399 RID: 5017 RVA: 0x001438A8 File Offset: 0x001428A8
	private void ᜀ(Break A_0, Paragraph A_1)
	{
		for (;;)
		{
			spr\u1AFF.ᜀ(A_0.TextRange.CharacterFormat, this.\u171C().ᜈ());
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.\u171C() is sprច)
					{
						num = 6;
						continue;
					}
					goto IL_11A;
				case 1:
					goto IL_F5;
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
						num = 0;
						continue;
					}
					break;
				case 3:
					goto IL_5B;
				case 4:
					if (A_0.BreakType == BreakType.ColumnBreak)
					{
						num = 3;
						continue;
					}
					num = 5;
					continue;
				case 5:
					if (A_0.BreakType == BreakType.PageBreak)
					{
						num = 2;
						continue;
					}
					this.\u171C().ᜀ(WordChunkType.LineBreak);
					num = 1;
					continue;
				case 6:
					goto IL_C4;
				}
				break;
			}
		}
		IL_5B:
		this.ᜇ.ᜀ(WordChunkType.ColumnBreak);
		return;
		IL_C4:
		spr\u192A.ᜀ(this.\u171C().ᜉ(), A_1.Format, A_1);
		(this.\u171C() as sprច).ᜑ();
		return;
		IL_F5:
		IL_11A:
		if (true)
		{
		}
	}

	// Token: 0x0600139A RID: 5018 RVA: 0x001439D8 File Offset: 0x001429D8
	private void ᜀ(Symbol A_0)
	{
		int num = 2;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_98;
			case 1:
				this.\u171C().ᜈ().ᜄ().ᜁ(A_0.FontName);
				num = 0;
				continue;
			}
			IL_1C:
			if (this.\u171C().ᜈ().ᜄ().ᜀ(A_0.FontName) != -1)
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
				if (true)
				{
				}
				num = 1;
				continue;
			}
			goto IL_1C;
		}
		IL_98:
		this.ᜀ(A_0.CharacterFormat.CharStyleName, false);
		spr\u1AFF.ᜀ(A_0.CharacterFormat, this.\u171C().ᜈ());
		sprᣂ sprᣂ = new sprᣂ();
		sprᣂ.ᜁ(A_0.CharacterCode);
		sprᣂ.ᜀ(A_0.CharCodeExt);
		sprᣂ.ᜀ((short)this.\u171C().ᜆ().ᜀ(A_0.FontName));
		this.\u171C().ᜈ().ᜀ(sprᣂ);
		this.\u171C().ᜀ(WordChunkType.Symbol);
	}

	// Token: 0x0600139B RID: 5019 RVA: 0x00143B00 File Offset: 0x00142B00
	private void ᜁ(TextBox A_0)
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
		A_0.Format.IsHeaderTextBox = (this.\u171C() is sprᮞ);
		this.ᜀ(A_0);
		new spr\u252D();
		A_0.Spid = this.\u171C().ᜀ(A_0.Format);
		this.ᜀ(A_0);
	}

	// Token: 0x0600139C RID: 5020 RVA: 0x00143B88 File Offset: 0x00142B88
	private void ᜀ(TextBox A_0)
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
			if (!(this.ᜈ is sprᮞ))
			{
				this.\u1714().Add(A_0);
				return;
			}
			break;
		}
		this.\u1715().Add(A_0);
	}

	// Token: 0x0600139D RID: 5021 RVA: 0x00143BF0 File Offset: 0x00142BF0
	private void ᜀ(ParagraphBase A_0)
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
				this.ᜉ.ᜀ(this.ᜉ, A_0);
				num = 1;
				continue;
			case 1:
				return;
			}
			IL_24:
			if (A_0.ᜁ)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_10;
				}
				if (false)
				{
				}
				num = 0;
				continue;
			}
			break;
			IL_10:
			goto IL_24;
		}
	}

	// Token: 0x0600139E RID: 5022 RVA: 0x00143C78 File Offset: 0x00142C78
	private void ᜀ(FieldMark A_0)
	{
		int num = 3;
		for (;;)
		{
			CharacterFormat characterFormat;
			switch (num)
			{
			case 0:
				goto IL_180;
			case 1:
				goto IL_323;
			case 2:
				if (A_0.PreviousSibling is Field)
				{
					num = 23;
					continue;
				}
				goto IL_323;
			case 4:
				goto IL_323;
			case 5:
				if (this.\u171B() is TextFormField)
				{
					num = 26;
					continue;
				}
				goto IL_1E8;
			case 6:
				if (this.\u171B().Range.ᜁ().Count == 0)
				{
					num = 12;
					continue;
				}
				goto IL_323;
			case 7:
				characterFormat = this.\u171B().CharacterFormat;
				goto IL_1B0;
			case 8:
				if (this.ᜉ.IsUpdateFields)
				{
					num = 21;
					continue;
				}
				goto IL_323;
			case 9:
				if (this.\u171B().Type == FieldType.FieldTOCEntry)
				{
					num = 0;
					continue;
				}
				goto IL_11C;
			case 10:
				num = 15;
				continue;
			case 11:
				characterFormat = A_0.CharacterFormat;
				goto IL_1B0;
			case 12:
				goto IL_310;
			case 13:
				num = 6;
				continue;
			case 14:
				if (this.\u171B().Type == FieldType.FieldDocVariable)
				{
					num = 16;
					continue;
				}
				goto IL_323;
			case 15:
				if (this.\u171B().Type != FieldType.FieldIndexEntry)
				{
					num = 19;
					continue;
				}
				goto IL_2D4;
			case 16:
				num = 2;
				continue;
			case 17:
				if (A_0.Type == FieldMarkType.FieldSeparator)
				{
					num = 25;
					continue;
				}
				goto IL_37E;
			case 18:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_1E8;
				default:
					if (false)
					{
					}
					if (this.\u171B() != null)
					{
						num = 29;
						continue;
					}
					goto IL_323;
				}
				break;
			case 19:
				num = 9;
				continue;
			case 20:
				if (this.\u171B().Document.DetectedFormatType == FileFormat.Rtf)
				{
					num = 13;
					continue;
				}
				goto IL_310;
			case 21:
			{
				bool a_ = this.\u171C().ᜈ().ᜥ();
				this.\u171C().ᜈ().ᜂ(true);
				this.ᜈ();
				this.\u171C().ᜈ().ᜂ(a_);
				num = 1;
				continue;
			}
			case 22:
				num = 5;
				continue;
			case 23:
				num = 8;
				continue;
			case 24:
				if (A_0.Type == FieldMarkType.FieldEnd)
				{
					num = 22;
					continue;
				}
				goto IL_323;
			case 25:
				goto IL_1E3;
			case 26:
				num = 20;
				continue;
			case 27:
				if (!(this.\u171B() is FormField))
				{
					num = 28;
					continue;
				}
				num = 7;
				continue;
			case 28:
				num = 11;
				continue;
			case 29:
				num = 14;
				continue;
			}
			if (this.\u171B() != null)
			{
				num = 10;
				continue;
			}
			IL_11C:
			num = 24;
			continue;
			IL_1B0:
			CharacterFormat a_2 = characterFormat;
			spr\u1AFF.ᜀ(a_2, this.\u171C().ᜈ());
			num = 17;
			continue;
			IL_1E8:
			num = 18;
			continue;
			IL_310:
			this.ᜆ();
			num = 4;
			continue;
			IL_323:
			if (true)
			{
			}
			num = 27;
		}
		IL_180:
		goto IL_2D4;
		IL_1E3:
		this.ᜈ();
		return;
		IL_2D4:
		this.\u1713.Pop();
		return;
		IL_37E:
		this.ᜇ();
	}

	// Token: 0x0600139F RID: 5023 RVA: 0x0014400C File Offset: 0x0014300C
	private void ᜈ()
	{
		for (;;)
		{
			IL_00:
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					num = 3;
					continue;
				case 2:
					if (this.\u171B().Type == FieldType.FieldOCX)
					{
						num = 6;
						continue;
					}
					goto IL_139;
				case 3:
					if (this.\u171B().Type == FieldType.FieldLink)
					{
						num = 8;
						continue;
					}
					num = 2;
					continue;
				case 4:
					goto IL_85;
				case 5:
					if (this.\u171B().Type != FieldType.FieldEmbed)
					{
						num = 1;
						continue;
					}
					goto IL_71;
				case 6:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_00;
					default:
						if (false)
						{
						}
						this.\u171C().ᜈ().ᜄ((this.\u171B() as ControlField).StoragePicLocation);
						num = 7;
						continue;
					}
					break;
				case 7:
					goto IL_F8;
				case 8:
					goto IL_71;
				case 9:
					num = 5;
					continue;
				}
				if (this.\u171B() != null)
				{
					num = 9;
					continue;
				}
				goto IL_139;
				IL_71:
				this.ᜁ(this.\u171B());
				num = 4;
			}
		}
		IL_85:
		goto IL_139;
		IL_F8:
		if (true)
		{
		}
		IL_139:
		this.\u171C().\u170D();
	}

	// Token: 0x060013A0 RID: 5024 RVA: 0x00144160 File Offset: 0x00143160
	private void ᜇ()
	{
		if (true)
		{
		}
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			default:
			{
				if (false)
				{
				}
				this.\u171C().ᜌ();
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.\u1713.Count > 0)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						this.\u1713.Pop();
						num = 1;
						continue;
					}
					break;
				}
				break;
			}
			}
		}
	}

	// Token: 0x060013A1 RID: 5025 RVA: 0x001441F0 File Offset: 0x001431F0
	private void ᜆ()
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
		TextFormField textFormField = this.\u171B() as TextFormField;
		spr\u1AFF.ᜀ(textFormField.TextRange.CharacterFormat, this.\u171C().ᜈ());
		this.ᜁ(textFormField.TextRange.Text, textFormField.TextRange.SafeText);
	}

	// Token: 0x060013A2 RID: 5026 RVA: 0x00144270 File Offset: 0x00143270
	private void ᜂ(Comment A_0)
	{
		int num = 3;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_67;
			case 1:
				if (true)
				{
				}
				this.ᜁ(A_0);
				num = 2;
				continue;
			case 2:
				goto IL_80;
			}
			if (A_0.AppendItems)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_80;
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
			}
			else
			{
				this.ᜀ(A_0);
				num = 0;
			}
		}
		IL_67:
		IL_80:
		this.\u1718().Add(A_0);
		CommentFormat format = A_0.Format;
		(this.\u171C() as sprច).ᜀ(format);
	}

	// Token: 0x060013A3 RID: 5027 RVA: 0x00144324 File Offset: 0x00143324
	private void ᜀ(Footnote A_0)
	{
		for (;;)
		{
			IL_2A:
			A_0.ᜁ();
			for (;;)
			{
				IL_30:
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_93;
					case 1:
						goto IL_7A;
					case 2:
						if (A_0.FootnoteType != FootnoteType.Footnote)
						{
							this.\u1716().Add(A_0);
							num = 1;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_30;
						default:
							if (false)
							{
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						this.\u1717().Add(A_0);
						num = 0;
						continue;
					}
					goto IL_2A;
				}
			}
		}
		IL_7A:
		IL_93:
		this.ᜀ(A_0.MarkerCharacterFormat.CharStyleName, false);
		spr\u1AFF.ᜀ(A_0.MarkerCharacterFormat, this.\u171C().ᜈ());
		(this.\u171C() as sprច).ᜀ(A_0);
	}

	// Token: 0x060013A4 RID: 5028 RVA: 0x00144400 File Offset: 0x00143400
	private void ᜀ(WatermarkBase A_0)
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
		SizeF pageSize = this.ᜉ.LastSection.PageSetup.PageSize;
		MarginsF margins = this.ᜉ.LastSection.PageSetup.Margins;
		float a_ = pageSize.Width - margins.Left - margins.Right;
		this.\u171C().ᜀ(A_0, sprᴠ.\u171D(), a_);
	}

	// Token: 0x060013A5 RID: 5029 RVA: 0x00144490 File Offset: 0x00143490
	private void ᜀ(TableOfContent A_0)
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
		A_0.\u1718();
		this.ᜂ(A_0.TOCField);
	}

	// Token: 0x060013A6 RID: 5030 RVA: 0x001444E0 File Offset: 0x001434E0
	private void ᜀ(string A_0)
	{
		if (true)
		{
		}
		for (;;)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_98;
			default:
			{
				if (false)
				{
				}
				this.\u171C().ᜈ().ᜂ(true);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.\u171C().ᜈ().ᜀ(true);
						num = 2;
						continue;
					case 1:
						if (!this.\u171C().ᜈ().ᜂ(2050))
						{
							num = 0;
							continue;
						}
						goto IL_9A;
					case 2:
						goto IL_98;
					}
					break;
				}
				break;
			}
			}
		}
		IL_98:
		IL_9A:
		this.\u171C().ᜀ(WordChunkType.FieldBeginMark);
		this.\u171C().ᜂ(A_0);
		this.\u171C().ᜈ().ᜂ(false);
		this.\u171C().ᜀ(WordChunkType.FieldEndMark);
	}

	// Token: 0x060013A7 RID: 5031 RVA: 0x001445C0 File Offset: 0x001435C0
	private void ᜀ(CommentMark A_0)
	{
		int num = 0;
		DictionaryEntry value;
		for (;;)
		{
			switch (num)
			{
			case 1:
				if (this.\u1713().ContainsKey(A_0.CommentId))
				{
					num = 3;
					continue;
				}
				return;
			case 2:
				goto IL_D1;
			case 3:
				for (;;)
				{
					value = this.\u1713()[A_0.CommentId];
					value.Value = (this.\u171C() as sprច).\u171F();
					this.\u1713()[A_0.CommentId] = value;
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_131;
					}
				}
				IL_131:
				if (false)
				{
				}
				num = 4;
				continue;
			case 4:
				return;
			case 5:
				value = new DictionaryEntry((this.\u171C() as sprច).\u171F(), 0);
				num = 6;
				continue;
			case 6:
				if (!this.\u1713().ContainsKey(A_0.CommentId))
				{
					num = 2;
					continue;
				}
				return;
			}
			if (A_0.Type == CommentMarkType.CommentStart)
			{
				num = 5;
			}
			else
			{
				num = 1;
			}
		}
		IL_D1:
		this.\u1713().Add(A_0.CommentId, value);
	}

	// Token: 0x060013A8 RID: 5032 RVA: 0x00144714 File Offset: 0x00143714
	private void ᜀ(DocOleObject A_0)
	{
		int a_ = 5;
		Field field2;
		int num;
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
		{
			IL_74:
			Field field = field2;
			field.m_fieldValue += A_0.ObjectType;
			num = 1;
			break;
		}
		default:
			if (false)
			{
			}
			goto IL_45;
		}
		for (;;)
		{
			IL_27:
			switch (num)
			{
			case 0:
				goto IL_D8;
			case 1:
				goto IL_DA;
			case 2:
				if (field2.Type == FieldType.FieldLink)
				{
					if (true)
					{
					}
					num = 3;
					continue;
				}
				goto IL_135;
			case 3:
			{
				Field field3 = field2;
				field3.m_fieldValue = field3.m_fieldValue + ClipboardData.b("䭪佬", a_) + A_0.LinkPath.Replace(ClipboardData.b("㝪", a_), ClipboardData.b("㝪ㅬ", a_)) + ClipboardData.b("䥪", a_);
				num = 0;
				continue;
			}
			case 4:
				goto IL_74;
			case 5:
				if (!string.IsNullOrEmpty(A_0.ObjectType))
				{
					num = 4;
					continue;
				}
				goto IL_DA;
			}
			goto IL_45;
			IL_DA:
			num = 2;
		}
		IL_D8:
		IL_135:
		this.ᜂ(field2);
		return;
		IL_45:
		field2 = A_0.Field;
		field2.m_fieldValue = string.Empty;
		num = 5;
		goto IL_27;
	}

	// Token: 0x060013A9 RID: 5033 RVA: 0x00144860 File Offset: 0x00143860
	private void ᜅ()
	{
		switch (0)
		{
		default:
		{
			if (true)
			{
			}
			IEnumerator enumerator = this.ᜉ.ListStyles.GetEnumerator();
			try
			{
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_B3:
					num = 3;
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
					case 1:
					{
						ListStyle listStyle;
						if (listStyle != null)
						{
							num = 16;
							continue;
						}
						goto IL_A5;
					}
					case 3:
					{
						int count;
						if (num2 >= count)
						{
							num = 0;
							continue;
						}
						ListStyle listStyle;
						ListLevel listLevel = listStyle.Levels[num2];
						num = 17;
						continue;
					}
					case 4:
					{
						ListStyle listStyle;
						if (listStyle.Levels == null)
						{
							num = 8;
							continue;
						}
						num2 = 0;
						int count = listStyle.Levels.Count;
						num = 12;
						continue;
					}
					case 5:
						num = 13;
						continue;
					case 6:
					{
						ListLevel listLevel;
						this.\u171A().Add(listLevel.PicBullet);
						int a_ = this.\u171A().Count - 1;
						listLevel.CharacterFormat.ListPictureIndex = a_;
						listLevel.CharacterFormat.ListHasPicture = true;
						num = 11;
						continue;
					}
					case 7:
						goto IL_AE;
					case 8:
						goto IL_A5;
					case 9:
					{
						if (!enumerator.MoveNext())
						{
							num = 14;
							continue;
						}
						ListStyle listStyle = (ListStyle)enumerator.Current;
						num = 1;
						continue;
					}
					case 10:
						goto IL_225;
					case 11:
						goto IL_1C0;
					case 12:
						goto IL_214;
					case 13:
					{
						ListLevel listLevel;
						if (listLevel.PicBullet != null)
						{
							num = 6;
							continue;
						}
						goto IL_1C0;
					}
					case 14:
						num = 10;
						continue;
					case 15:
						goto IL_1D0;
					case 16:
						num = 4;
						continue;
					case 17:
					{
						ListLevel listLevel;
						if (listLevel != null)
						{
							num = 5;
							continue;
						}
						goto IL_1C0;
					}
					}
					goto IL_A0;
					IL_A5:
					num = 7;
					continue;
					IL_13D:
					num = 9;
					continue;
					IL_A0:
					goto IL_13D;
					IL_1C0:
					num2++;
					num = 15;
				}
				IL_AE:
				return;
				IL_1D0:
				IL_214:
				goto IL_B3;
				IL_225:;
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
							goto IL_26D;
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
							goto IL_26F;
						}
						break;
					}
				}
				IL_26D:
				IL_26F:;
			}
			return;
		}
		}
	}

	// Token: 0x060013AA RID: 5034 RVA: 0x00144B04 File Offset: 0x00143B04
	private void ᜀ(ListFormat A_0)
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

	// Token: 0x060013AB RID: 5035 RVA: 0x00144B40 File Offset: 0x00143B40
	private void ᜀ(IParagraph A_0)
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
		this.ᜂ(A_0.ListFormat, this.\u171C() as spr\u21B0);
		spr\u192A.ᜀ(this.\u171C().ᜉ(), A_0.Format, A_0 as Paragraph);
		this.\u171C().ᜊ().ᜢ().ᜄ();
		this.ᜀ(A_0.BreakCharacterFormat.CharStyleName, true);
		spr\u1AFF.ᜀ(A_0.BreakCharacterFormat, this.\u171C().ᜊ());
		this.ᜀ(this.\u171C(), A_0);
	}

	// Token: 0x060013AC RID: 5036 RVA: 0x00144BF8 File Offset: 0x00143BF8
	private void ᜀ(spr\u2370 A_0, IParagraph A_1)
	{
		if (true)
		{
		}
		int num2;
		for (;;)
		{
			string styleName = A_1.StyleName;
			int num = 5;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num2 = A_0.ᜆ().ᜁ(styleName, false);
					num = 4;
					continue;
				case 1:
					num = 2;
					continue;
				case 2:
					if (styleName.Trim().Length > 0)
					{
						goto IL_94;
					}
					goto IL_A1;
				case 3:
					return;
				case 4:
					if (num2 > -1)
					{
						num = 6;
						continue;
					}
					return;
				case 5:
					if (styleName != null)
					{
						num = 1;
						continue;
					}
					goto IL_A1;
				case 6:
					goto IL_7C;
				}
				break;
				IL_94:
				num = 0;
				continue;
				IL_A1:
				A_0.ᜀ(0);
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_94;
				default:
					if (false)
					{
					}
					num = 3;
					break;
				}
			}
		}
		IL_7C:
		A_0.ᜀ(num2);
	}

	// Token: 0x060013AD RID: 5037 RVA: 0x00144CD8 File Offset: 0x00143CD8
	private void ᜀ(spr\u2370 A_0, TableRow A_1, ITable A_2, bool A_3)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u1739 spr_u = null;
				int num = 23;
				for (;;)
				{
					RowFormat rowFormat2;
					RowFormat rowFormat;
					RowFormat rowFormat3;
					switch (num)
					{
					case 0:
						if (A_2 is Table)
						{
							num = 20;
							continue;
						}
						goto IL_CC;
					case 1:
					{
						Table table = A_2 as Table;
						spr_u.ᜀ(table.PreferredTableWidth.ᜀ());
						FtsWidth ftsWidth = table.PreferredTableWidth.ᜀ();
						num = 12;
						continue;
					}
					case 2:
						rowFormat = rowFormat2;
						goto IL_12B;
					case 3:
						if (!A_1.RowFormat.Borders.NoBorder)
						{
							num = 15;
							continue;
						}
						num = 11;
						continue;
					case 4:
						A_1.CharacterFormat.IsDeleteRevision = true;
						num = 22;
						continue;
					case 5:
						num = 28;
						continue;
					case 6:
						goto IL_CC;
					case 7:
						A_1.CharacterFormat.IsInsertRevision = true;
						num = 21;
						continue;
					case 8:
						rowFormat3 = A_1.RowFormat;
						goto IL_37F;
					case 9:
						num = 13;
						continue;
					case 10:
						goto IL_11C;
					case 11:
						rowFormat3 = A_2.TableFormat;
						goto IL_37F;
					case 12:
					{
						FtsWidth ftsWidth;
						switch (ftsWidth)
						{
						case FtsWidth.Auto:
							spr_u.ᜄ(true);
							spr_u.ᜉ(0);
							num = 14;
							continue;
						case FtsWidth.Percentage:
						{
							Table table;
							spr_u.ᜉ((short)(table.PreferredTableWidth.ᜁ() * 50));
							num = 24;
							continue;
						}
						case FtsWidth.Point:
						{
							Table table;
							spr_u.ᜉ((short)table.PreferredTableWidth.ᜁ());
							num = 26;
							continue;
						}
						default:
							num = 5;
							continue;
						}
						break;
					}
					case 13:
						if ((A_2 as Table).PreferredTableWidth.ᜀ() != FtsWidth.None)
						{
							num = 1;
							continue;
						}
						goto IL_229;
					case 14:
						goto IL_229;
					case 15:
						goto IL_274;
					case 16:
						if (A_1.IsDeleteRevision)
						{
							num = 4;
							continue;
						}
						goto IL_3C9;
					case 17:
						if (A_2 is Table)
						{
							num = 9;
							continue;
						}
						goto IL_229;
					case 18:
						num = 31;
						continue;
					case 19:
						goto IL_1CF;
					case 20:
						num = 32;
						continue;
					case 21:
						goto IL_323;
					case 22:
						goto IL_3C9;
					case 23:
						if (A_3)
						{
							if (true)
							{
							}
							num = 29;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_274;
						default:
							if (false)
							{
							}
							A_1.RowFormat.ᜏ();
							spr_u = (A_0 as spr\u21B0).ᜄ(A_1.Cells.Count);
							spr\u1B3A.ᜀ(spr_u, A_1);
							spr\u1B3A.ᜀ(spr_u, A_1, this.ᜊ);
							num = 19;
							continue;
						}
						break;
					case 24:
						goto IL_229;
					case 25:
						rowFormat = (A_2 as Table).DocxTableFormat.Format;
						goto IL_12B;
					case 26:
						goto IL_229;
					case 27:
						if (A_1.IsInsertRevision)
						{
							num = 7;
							continue;
						}
						goto IL_471;
					case 28:
						goto IL_229;
					case 29:
						spr_u = A_1.RowFormat.RowDescriptor;
						num = 30;
						continue;
					case 30:
						goto IL_1CF;
					case 31:
						if (!(A_2 as Table).DocxTableFormat.HasFormat)
						{
							num = 10;
							continue;
						}
						num = 25;
						continue;
					case 32:
						if (rowFormat2.Borders.NoBorder)
						{
							num = 18;
							continue;
						}
						goto IL_11C;
					}
					break;
					IL_CC:
					sprᯚ a_;
					spr\u1B3A.ᜀ(a_, rowFormat2);
					A_0.ᜉ().ᜁ(a_);
					A_0.ᜉ().ᜀ(a_);
					A_0.ᜉ().ᜀ(spr_u);
					num = 16;
					continue;
					IL_11C:
					num = 2;
					continue;
					IL_12B:
					rowFormat2 = rowFormat;
					num = 6;
					continue;
					IL_1CF:
					num = 17;
					continue;
					IL_229:
					a_ = (A_0 as spr\u21B0).ᜥ();
					this.ᜀ(A_0.ᜉ(), A_1.RowFormat);
					num = 3;
					continue;
					IL_274:
					num = 8;
					continue;
					IL_37F:
					rowFormat2 = rowFormat3;
					num = 0;
					continue;
					IL_3C9:
					num = 27;
				}
			}
			IL_323:
			IL_471:
			spr\u192A.ᜀ(A_0.ᜉ(), A_1);
			spr\u1AFF.ᜀ(A_1.CharacterFormat, A_0.ᜈ());
			return;
		}
	}

	// Token: 0x060013AE RID: 5038 RVA: 0x00145174 File Offset: 0x00144174
	private void ᜀ(sprᨽ A_0, RowFormat A_1)
	{
		for (;;)
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					A_0.ᜪ().ᜆ(A_1.Sprms.ᜇ(29801));
					num = 2;
					continue;
				case 1:
					num = 4;
					continue;
				case 2:
					goto IL_63;
				case 4:
					if (A_1.Sprms.ᜂ(29801))
					{
						num = 0;
						continue;
					}
					goto IL_89;
				}
				if (A_1.Sprms == null)
				{
					break;
				}
				num = 1;
			}
			IL_89:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				continue;
			default:
				goto IL_9F;
			}
			IL_63:
			goto IL_89;
		}
		IL_9F:
		if (false)
		{
		}
		if (true)
		{
		}
	}

	// Token: 0x060013AF RID: 5039 RVA: 0x00145230 File Offset: 0x00144230
	private void ᜀ(spr\u19E9 A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				spr\u2305 spr_u = A_0.ᜆ();
				this.ᜄ();
				List<string> list = new List<string>();
				List<int> list2 = new List<int>();
				Style style = null;
				int num = 0;
				int count = this.ᜉ.Styles.Count;
				int num2 = 80;
				for (;;)
				{
					int num3;
					sprᲵ sprᲵ;
					int num4;
					bool a_4;
					switch (num2)
					{
					case 0:
						if (this.ᜉ.Styles.FixedIndex14StyleName != null)
						{
							num2 = 44;
							continue;
						}
						goto IL_2F9;
					case 1:
						goto IL_D3B;
					case 2:
						if (style.NextStyle != null)
						{
							num2 = 63;
							continue;
						}
						goto IL_8A0;
					case 3:
						if (this.ᜉ.Styles.FixedIndex14StyleName != style.Name)
						{
							num2 = 41;
							continue;
						}
						goto IL_5F7;
					case 4:
						if (this.ᜉ.Styles.FixedIndex13StyleName != null)
						{
							num2 = 73;
							continue;
						}
						goto IL_3BD;
					case 5:
						goto IL_5F7;
					case 6:
						goto IL_C59;
					case 7:
					{
						num3 = 0;
						int count2 = this.ᜉ.Styles.Count;
						num2 = 1;
						continue;
					}
					case 8:
						goto IL_B8B;
					case 9:
						goto IL_5F7;
					case 10:
						if (style.StyleId > 0)
						{
							num2 = 45;
							continue;
						}
						goto IL_848;
					case 11:
						num2 = 62;
						continue;
					case 12:
						if (!string.IsNullOrEmpty(style.Name))
						{
							num2 = 42;
							continue;
						}
						goto IL_3A6;
					case 13:
						goto IL_848;
					case 14:
						if (this.ᜉ.Styles.FixedIndex14StyleName != null)
						{
							num2 = 23;
							continue;
						}
						goto IL_C59;
					case 15:
						sprᲵ.ᜀ(new byte[style.TableStyleData.Length]);
						Buffer.BlockCopy(style.TableStyleData, 0, sprᲵ.ᜃ(), 0, style.TableStyleData.Length);
						num2 = 71;
						continue;
					case 16:
					{
						ParagraphStyle paragraphStyle;
						this.ᜀ(A_0, paragraphStyle);
						spr\u192A.ᜀ(sprᲵ.ᜋ(), paragraphStyle.ParagraphFormat, null);
						num2 = 28;
						continue;
					}
					case 17:
						goto IL_5F7;
					case 18:
						return;
					case 19:
						if (style.TypeCode == WordStyleType.TableStyle)
						{
							num2 = 15;
							continue;
						}
						goto IL_929;
					case 20:
						goto IL_A9A;
					case 21:
						if (style.StyleId == 107)
						{
							num2 = 36;
							continue;
						}
						goto IL_61B;
					case 22:
						goto IL_5F7;
					case 23:
						num2 = 75;
						continue;
					case 24:
						if (style.StyleId < 10)
						{
							num2 = 92;
							continue;
						}
						goto IL_231;
					case 25:
						if (style.CharacterFormat.Sprms.ᜇ(19023) != null)
						{
							num2 = 48;
							continue;
						}
						goto IL_B8B;
					case 26:
						num2 = 31;
						continue;
					case 27:
						spr_u.ᜀ(11);
						spr_u.ᜀ(11, sprᲵ);
						num2 = 33;
						continue;
					case 28:
						goto IL_9B0;
					case 29:
						goto IL_66A;
					case 30:
						if (num4 < 0)
						{
							num2 = 58;
							continue;
						}
						sprᲵ = spr_u.ᜁ(num4);
						num2 = 76;
						continue;
					case 31:
						if (!list2.Contains(style.StyleId))
						{
							num2 = 27;
							continue;
						}
						goto IL_509;
					case 32:
						if (this.ᜉ.Styles.FixedIndex13HasStyle)
						{
							num2 = 95;
							continue;
						}
						goto IL_3BD;
					case 33:
						goto IL_66A;
					case 34:
						if (!list2.Contains(style.StyleId))
						{
							num2 = 97;
							continue;
						}
						goto IL_61B;
					case 35:
						if (style.StyleId >= 10)
						{
							num2 = 13;
							continue;
						}
						goto IL_8FB;
					case 36:
						if (true)
						{
						}
						goto IL_8FB;
					case 37:
					{
						bool a_2;
						int a_ = spr_u.ᜁ(style.BaseStyle.Name, a_2);
						int a_3;
						spr_u.ᜁ(a_3).ᜂ(a_);
						num2 = 91;
						continue;
					}
					case 38:
						num2 = 24;
						continue;
					case 39:
						spr_u.ᜀ(12);
						spr_u.ᜀ(12, sprᲵ);
						num2 = 29;
						continue;
					case 40:
						try
						{
							this.ᜐ.Add(style.Name, num4);
							goto IL_25B;
						}
						catch
						{
							goto IL_25B;
						}
						goto IL_586;
					case 41:
						num--;
						num2 = 5;
						continue;
					case 42:
					{
						bool a_2 = style.StyleType == StyleType.CharacterStyle;
						int a_3 = spr_u.ᜁ(style.Name, a_2);
						num2 = 46;
						continue;
					}
					case 43:
						if (style.TableStyleData != null)
						{
							num2 = 79;
							continue;
						}
						goto IL_929;
					case 44:
						num2 = 89;
						continue;
					case 45:
						num2 = 35;
						continue;
					case 46:
						if (style.BaseStyle != null)
						{
							num2 = 37;
							continue;
						}
						goto IL_2D1;
					case 47:
						num2 = 59;
						continue;
					case 48:
						goto IL_1F3;
					case 49:
						spr_u.ᜀ(sprᲵ.ᜂ());
						spr_u.ᜀ(sprᲵ.ᜂ(), sprᲵ);
						num2 = 82;
						continue;
					case 50:
						goto IL_5F7;
					case 51:
					{
						if (num >= count)
						{
							num2 = 7;
							continue;
						}
						style = (this.ᜉ.Styles[num] as Style);
						ParagraphStyle paragraphStyle = style as ParagraphStyle;
						sprᯉ sprᯉ = style as sprᯉ;
						a_4 = (style.StyleType == StyleType.CharacterStyle);
						num4 = 0;
						num2 = 74;
						continue;
					}
					case 52:
						goto IL_94D;
					case 53:
						num2 = 77;
						continue;
					case 54:
						num2 = 32;
						continue;
					case 55:
						num2 = 66;
						continue;
					case 56:
						goto IL_8A0;
					case 57:
						if (style.StyleId > 0)
						{
							num2 = 38;
							continue;
						}
						goto IL_231;
					case 58:
						num4 = spr_u.ᜆ();
						num2 = 101;
						continue;
					case 59:
						if (this.ᜉ.Styles.FixedIndex14HasStyle)
						{
							num2 = 68;
							continue;
						}
						goto IL_2F9;
					case 60:
						goto IL_94D;
					case 61:
						num2 = 14;
						continue;
					case 62:
						if (!list.Contains(style.Name))
						{
							num2 = 69;
							continue;
						}
						goto IL_D90;
					case 63:
					{
						bool a_2;
						int a_5 = spr_u.ᜁ(style.NextStyle, a_2);
						int a_3;
						spr_u.ᜁ(a_3).ᜁ(a_5);
						num2 = 56;
						continue;
					}
					case 64:
						goto IL_3A6;
					case 65:
					{
						int a_6 = spr_u.ᜃ(style.LinkStyle);
						int a_3;
						spr_u.ᜁ(a_3).ᜀ(a_6);
						num2 = 64;
						continue;
					}
					case 66:
						if (style.CharacterFormat.Sprms != null)
						{
							num2 = 86;
							continue;
						}
						goto IL_B8B;
					case 67:
						if (num4 == 14)
						{
							num2 = 47;
							continue;
						}
						num4 = spr_u.ᜆ();
						sprᲵ = spr_u.ᜀ(style.Name, a_4);
						sprᲵ.ᜃ(style.StyleId);
						num2 = 17;
						continue;
					case 68:
						num2 = 0;
						continue;
					case 69:
						num4 = spr_u.ᜁ(style.Name, a_4);
						num2 = 60;
						continue;
					case 70:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_B05;
						default:
							if (false)
							{
							}
							if (this.ᜉ.Styles.FixedIndex14HasStyle)
							{
								num2 = 61;
								continue;
							}
							goto IL_C59;
						}
						break;
					case 71:
						goto IL_929;
					case 72:
						if (!style.CharacterFormat.HasKey(68))
						{
							num2 = 55;
							continue;
						}
						goto IL_1F3;
					case 73:
						goto IL_586;
					case 74:
						if (style.Name.Trim().Length > 0)
						{
							num2 = 11;
							continue;
						}
						goto IL_D90;
					case 75:
						if (!(this.ᜉ.Styles.FixedIndex14StyleName != string.Empty))
						{
							num2 = 6;
							continue;
						}
						goto IL_25B;
					case 76:
						goto IL_5F7;
					case 77:
						if (!list2.Contains(style.StyleId))
						{
							num2 = 39;
							continue;
						}
						goto IL_66A;
					case 78:
						if (!list2.Contains(style.StyleId))
						{
							num2 = 49;
							continue;
						}
						goto IL_231;
					case 79:
						num2 = 19;
						continue;
					case 80:
						goto IL_A9A;
					case 81:
						if (this.ᜉ.Styles.FixedIndex13StyleName != string.Empty)
						{
							num2 = 99;
							continue;
						}
						goto IL_3BD;
					case 82:
						goto IL_66A;
					case 83:
						goto IL_5F7;
					case 84:
						sprᲵ = spr_u.ᜀ(this.ᜉ.Styles.FixedIndex14StyleName, a_4);
						sprᲵ.ᜃ(style.StyleId);
						num2 = 3;
						continue;
					case 85:
						goto IL_D3B;
					case 86:
						num2 = 25;
						continue;
					case 87:
					{
						int count2;
						if (num3 >= count2)
						{
							num2 = 18;
							continue;
						}
						style = (this.ᜉ.Styles[num3] as Style);
						num2 = 12;
						continue;
					}
					case 88:
						if (style.StyleId == 107)
						{
							num2 = 53;
							continue;
						}
						goto IL_66A;
					case 89:
						if (this.ᜉ.Styles.FixedIndex14StyleName != string.Empty)
						{
							num2 = 84;
							continue;
						}
						goto IL_2F9;
					case 90:
					{
						ParagraphStyle paragraphStyle;
						if (paragraphStyle != null)
						{
							num2 = 16;
							continue;
						}
						goto IL_9B0;
					}
					case 91:
						goto IL_2D1;
					case 92:
						num2 = 78;
						continue;
					case 93:
						num2 = 21;
						continue;
					case 94:
						if (!string.IsNullOrEmpty(style.LinkStyle))
						{
							num2 = 65;
							continue;
						}
						goto IL_3A6;
					case 95:
						num2 = 4;
						continue;
					case 96:
						if (style.StyleId == 105)
						{
							num2 = 26;
							continue;
						}
						goto IL_509;
					case 97:
						sprᲵ = new sprᲵ(spr_u, style.Name);
						sprᲵ.ᜃ(style.StyleId);
						num2 = 57;
						continue;
					case 98:
						if (style.StyleId != 105)
						{
							num2 = 93;
							continue;
						}
						goto IL_8FB;
					case 99:
						sprᲵ = spr_u.ᜀ(this.ᜉ.Styles.FixedIndex13StyleName, a_4);
						sprᲵ.ᜃ(style.StyleId);
						num2 = 9;
						continue;
					case 100:
					{
						sprᯉ sprᯉ;
						if (sprᯉ != null)
						{
							num2 = 40;
							continue;
						}
						goto IL_25B;
					}
					case 101:
						if (num4 == 13)
						{
							num2 = 54;
							continue;
						}
						goto IL_B05;
					}
					break;
					IL_1F3:
					sprᲵ.ᜅ().ᜢ().ᜆ(19023);
					num2 = 8;
					continue;
					IL_231:
					num2 = 96;
					continue;
					IL_25B:
					num++;
					num2 = 20;
					continue;
					IL_2D1:
					num2 = 2;
					continue;
					IL_2F9:
					sprᲵ = sprᲵ.ᜆ;
					spr_u.ᜀ(14, sprᲵ);
					num4 = spr_u.ᜆ();
					sprᲵ = spr_u.ᜀ(style.Name, a_4);
					sprᲵ.ᜃ(style.StyleId);
					num2 = 22;
					continue;
					IL_3A6:
					num3++;
					num2 = 85;
					continue;
					IL_3BD:
					sprᲵ = sprᲵ.ᜆ;
					spr_u.ᜀ(13, sprᲵ);
					num4 = spr_u.ᜆ();
					num2 = 70;
					continue;
					IL_509:
					num2 = 88;
					continue;
					IL_586:
					num2 = 81;
					continue;
					IL_5F7:
					num2 = 90;
					continue;
					IL_61B:
					num2 = 30;
					continue;
					IL_66A:
					list2.Add(style.StyleId);
					num2 = 83;
					continue;
					IL_848:
					num2 = 98;
					continue;
					IL_8A0:
					num2 = 94;
					continue;
					IL_8FB:
					num2 = 34;
					continue;
					IL_929:
					num2 = 100;
					continue;
					IL_94D:
					list.Add(style.Name);
					num2 = 10;
					continue;
					IL_9B0:
					num2 = 72;
					continue;
					IL_A9A:
					num2 = 51;
					continue;
					IL_B05:
					num2 = 67;
					continue;
					IL_B8B:
					spr\u1AFF.ᜀ(style.CharacterFormat, sprᲵ.ᜅ());
					sprᲵ.ᜂ(style.IsPrimaryStyle);
					sprᲵ.ᜃ(style.IsSemiHidden);
					sprᲵ.ᜄ(style.UnhideWhenUsed);
					sprᲵ.ᜀ(style.TypeCode);
					num2 = 43;
					continue;
					IL_C59:
					sprᲵ = sprᲵ.ᜆ;
					spr_u.ᜀ(14, sprᲵ);
					num4 = spr_u.ᜆ();
					sprᲵ = spr_u.ᜀ(style.Name, a_4);
					sprᲵ.ᜃ(style.StyleId);
					num2 = 50;
					continue;
					IL_D3B:
					num2 = 87;
					continue;
					IL_D90:
					num4 = -1;
					num2 = 52;
				}
			}
			return;
		}
	}

	// Token: 0x060013B0 RID: 5040 RVA: 0x00146020 File Offset: 0x00145020
	private void ᜄ()
	{
		int a_ = 15;
		switch (0)
		{
		default:
		{
			ParagraphStyle paragraphStyle;
			for (;;)
			{
				paragraphStyle = (this.ᜉ.Styles.FindByName(ClipboardData.b("㭴ᡶ୸ᙺᱼ፾", a_)) as ParagraphStyle);
				int num = 34;
				for (;;)
				{
					IEnumerator enumerator;
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (!paragraphStyle.CharacterFormat.HasKey(69))
						{
							num = 29;
							continue;
						}
						goto IL_727;
					case 2:
						goto IL_3A5;
					case 3:
						if (this.ᜉ.ᜬ.Sprms != null)
						{
							num = 38;
							continue;
						}
						goto IL_3FE;
					case 4:
						goto IL_6F5;
					case 5:
						if (paragraphStyle.CharacterFormat.IsDefault)
						{
							num = 4;
							continue;
						}
						goto IL_136;
					case 6:
						paragraphStyle.CharacterFormat.FontNameAscii = this.ᜉ.DefCharFormat.FontNameAscii;
						num = 22;
						continue;
					case 7:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 1:
								{
									if (!enumerator.MoveNext())
									{
										num = 4;
										continue;
									}
									spr\u1CC1 spr_u1CC = (spr\u1CC1)enumerator.Current;
									num = 2;
									continue;
								}
								case 2:
								{
									spr\u1CC1 spr_u1CC;
									if (paragraphStyle.CharacterFormat.Sprms.ᜇ((int)spr_u1CC.ᜂ()) == null)
									{
										num = 6;
										continue;
									}
									break;
								}
								case 4:
									num = 5;
									continue;
								case 5:
									goto IL_4DF;
								case 6:
								{
									spr\u1CC1 spr_u1CC;
									paragraphStyle.CharacterFormat.Sprms.ᜆ(spr_u1CC);
									num = 0;
									continue;
								}
								}
								IL_4B0:
								num = 1;
								continue;
								goto IL_4B0;
							}
							IL_4DF:
							goto IL_1EB;
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
										goto IL_52C;
									case 1:
										disposable.Dispose();
										num = 2;
										continue;
									case 2:
										goto IL_52A;
									}
									break;
								}
							}
							IL_52A:
							IL_52C:;
						}
						goto Block_13;
						IL_1EB:
						num = 24;
						continue;
					case 8:
						if (this.ᜉ.DefCharFormat.Sprms != null)
						{
							num = 15;
							continue;
						}
						return;
					case 9:
						if (paragraphStyle.CharacterFormat.Sprms != null)
						{
							num = 17;
							continue;
						}
						goto IL_6CD;
					case 10:
						paragraphStyle.CharacterFormat.FontNameNonFarEast = this.ᜉ.DefCharFormat.FontNameNonFarEast;
						num = 30;
						continue;
					case 11:
						if (!paragraphStyle.CharacterFormat.HasKey(70))
						{
							num = 10;
							continue;
						}
						return;
					case 12:
						goto IL_727;
					case 13:
						if (!paragraphStyle.CharacterFormat.HasKey(61))
						{
							num = 16;
							continue;
						}
						goto IL_28C;
					case 14:
						goto IL_28C;
					case 15:
						num = 9;
						continue;
					case 16:
						paragraphStyle.CharacterFormat.FontNameBidi = this.ᜉ.DefCharFormat.FontNameBidi;
						num = 14;
						continue;
					case 17:
						num = 31;
						continue;
					case 18:
						if (this.ᜉ.ᜬ != null)
						{
							num = 32;
							continue;
						}
						goto IL_3FE;
					case 19:
						if (paragraphStyle.ParagraphFormat.IsDefault)
						{
							num = 33;
							continue;
						}
						goto IL_31A;
					case 20:
						goto IL_3FE;
					case 21:
						num = 8;
						continue;
					case 22:
						goto IL_376;
					case 23:
						num = 36;
						continue;
					case 24:
						if (paragraphStyle.CharacterFormat.CharStyleName == null)
						{
							num = 35;
							continue;
						}
						goto IL_3A5;
					case 25:
						if (true)
						{
						}
						goto IL_34C;
					case 26:
						if (!paragraphStyle.CharacterFormat.HasKey(68))
						{
							num = 6;
							continue;
						}
						goto IL_376;
					case 27:
						goto IL_6CD;
					case 28:
						goto IL_52D;
					case 29:
						paragraphStyle.CharacterFormat.FontNameFarEast = this.ᜉ.DefCharFormat.FontNameFarEast;
						num = 12;
						continue;
					case 30:
						goto IL_2E2;
					case 31:
						if (paragraphStyle.CharacterFormat.Sprms.ᜈ() == 0)
						{
							num = 27;
							continue;
						}
						goto IL_136;
					case 32:
						goto IL_645;
					case 33:
						paragraphStyle.ParagraphFormat.ImportContainer(this.ᜉ.DefParaFormat);
						num = 20;
						continue;
					case 34:
						if (paragraphStyle == null)
						{
							num = 0;
							continue;
						}
						num = 18;
						continue;
					case 35:
						paragraphStyle.CharacterFormat.CharStyleName = this.ᜉ.DefCharFormat.CharStyleName;
						num = 2;
						continue;
					case 36:
						if (paragraphStyle.ParagraphFormat.Sprms.ᜈ() == 0)
						{
							num = 25;
							continue;
						}
						goto IL_31A;
					case 37:
						if (paragraphStyle.ParagraphFormat.Sprms != null)
						{
							num = 23;
							continue;
						}
						goto IL_34C;
					case 38:
					{
						IEnumerator enumerator2 = this.ᜉ.ᜬ.Sprms.GetEnumerator();
						num = 28;
						continue;
					}
					case 39:
						if (this.ᜉ.DefCharFormat != null)
						{
							num = 21;
							continue;
						}
						return;
					}
					break;
					IL_136:
					enumerator = this.ᜉ.DefCharFormat.Sprms.GetEnumerator();
					num = 7;
					continue;
					IL_28C:
					num = 1;
					continue;
					IL_31A:
					num = 3;
					continue;
					IL_34C:
					num = 19;
					continue;
					IL_376:
					num = 13;
					continue;
					IL_3A5:
					num = 26;
					continue;
					IL_3FE:
					num = 39;
					continue;
					Block_13:
					try
					{
						IL_52D:
						num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								spr\u1CC1 spr_u1CC2;
								if (paragraphStyle.ParagraphFormat.Sprms.ᜇ((int)spr_u1CC2.ᜂ()) == null)
								{
									num = 5;
									continue;
								}
								break;
							}
							case 1:
								goto IL_5DC;
							case 3:
							{
								IEnumerator enumerator2;
								if (!enumerator2.MoveNext())
								{
									num = 6;
									continue;
								}
								spr\u1CC1 spr_u1CC2 = (spr\u1CC1)enumerator2.Current;
								num = 0;
								continue;
							}
							case 5:
							{
								spr\u1CC1 spr_u1CC2;
								paragraphStyle.ParagraphFormat.Sprms.ᜆ(spr_u1CC2);
								num = 4;
								continue;
							}
							case 6:
								num = 1;
								continue;
							}
							IL_579:
							num = 3;
							continue;
							goto IL_579;
						}
						IL_5DC:
						goto IL_3FE;
					}
					finally
					{
						for (;;)
						{
							IEnumerator enumerator2;
							IDisposable disposable2 = enumerator2 as IDisposable;
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 0:
									if (disposable2 != null)
									{
										num = 1;
										continue;
									}
									goto IL_644;
								case 1:
									switch ((1 == 1) ? 1 : 0)
									{
									case 0:
									case 2:
										goto IL_644;
									default:
										if (false)
										{
										}
										disposable2.Dispose();
										num = 2;
										continue;
									}
									break;
								case 2:
									goto IL_642;
								}
								break;
							}
						}
						IL_642:
						IL_644:;
					}
					IL_645:
					num = 37;
					continue;
					IL_6CD:
					num = 5;
					continue;
					IL_727:
					num = 11;
				}
			}
			return;
			IL_2E2:
			return;
			IL_6F5:
			paragraphStyle.CharacterFormat.ImportContainer(this.ᜉ.DefCharFormat);
			return;
		}
		}
	}

	// Token: 0x060013B1 RID: 5041 RVA: 0x0014679C File Offset: 0x0014579C
	private void ᜃ()
	{
		int num = 24;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_3C3;
			case 1:
				goto IL_DD;
			case 2:
				this.ᜇ.ᜁ(this.ᜉ.MacroCommands);
				num = 3;
				continue;
			case 3:
				goto IL_36D;
			case 4:
				this.ᜇ.ᜉ().ᜀ((ushort)this.ᜉ.ViewSetup.ZoomPercent);
				num = 10;
				continue;
			case 5:
				if (this.ᜉ.DOP == null)
				{
					goto IL_499;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_415;
				default:
					if (false)
					{
					}
					num = 6;
					continue;
				}
				break;
			case 6:
				this.ᜇ.ᜀ(this.ᜉ.DOP);
				this.ᜇ.ᜉ().ᜀ(this.ᜇ.\u1716());
				num = 32;
				continue;
			case 7:
				if (this.ᜉ.CustomDocumentProperties != null)
				{
					num = 22;
					continue;
				}
				goto IL_3C3;
			case 8:
				goto IL_2E1;
			case 9:
				goto IL_4C4;
			case 10:
				goto IL_28A;
			case 11:
				this.ᜇ.ᜂ(this.ᜉ.AssociatedStrings);
				num = 16;
				continue;
			case 12:
				if (this.ᜉ.ViewSetup.ZoomType != ZoomType.None)
				{
					num = 18;
					continue;
				}
				goto IL_159;
			case 13:
				if (this.ᜉ.Variables.Count > 0)
				{
					num = 30;
					continue;
				}
				return;
			case 14:
				return;
			case 15:
				if (this.ᜉ.ObjectPool != null)
				{
					num = 23;
					continue;
				}
				goto IL_4C4;
			case 16:
				goto IL_1B6;
			case 17:
				if (true)
				{
				}
				if (this.ᜉ.MacroCommands != null)
				{
					num = 2;
					continue;
				}
				goto IL_36D;
			case 18:
				this.ᜇ.ᜉ().ᜁ((byte)this.ᜉ.ViewSetup.ZoomType);
				num = 29;
				continue;
			case 19:
				goto IL_415;
			case 20:
				this.ᜇ.ᜀ(this.ᜉ.BuiltinDocumentProperties.Clone());
				num = 31;
				continue;
			case 21:
				if (this.ᜉ.GrammarSpellingData != null)
				{
					num = 26;
					continue;
				}
				goto IL_DD;
			case 22:
				this.ᜇ.ᜀ(this.ᜉ.CustomDocumentProperties.Clone());
				num = 0;
				continue;
			case 23:
				this.ᜇ.ᜁ(new MemoryStream(this.ᜉ.ObjectPool));
				num = 9;
				continue;
			case 25:
				if (this.ᜉ.AssociatedStrings != null)
				{
					num = 11;
					continue;
				}
				goto IL_1B6;
			case 26:
				this.ᜇ.ᜀ(this.ᜉ.GrammarSpellingData);
				num = 1;
				continue;
			case 27:
				if (this.ᜉ.ViewSetup.ZoomPercent != 100)
				{
					num = 4;
					continue;
				}
				goto IL_28A;
			case 28:
				if (this.ᜉ.MacrosData != null)
				{
					num = 19;
					continue;
				}
				goto IL_2E1;
			case 29:
				goto IL_159;
			case 30:
				this.ᜇ.ᜀ(this.ᜉ.Variables.ᜀ());
				num = 14;
				continue;
			case 31:
				goto IL_4EF;
			case 32:
				goto IL_499;
			}
			if (this.ᜉ.BuiltinDocumentProperties != null)
			{
				num = 20;
				continue;
			}
			goto IL_4EF;
			IL_DD:
			num = 5;
			continue;
			IL_159:
			num = 27;
			continue;
			IL_1B6:
			this.ᜇ.ᜉ().ᜀ(this.ᜉ.Sections[0].PageSetup.DifferentOddAndEvenPagesHeaderFooter);
			this.ᜇ.ᜃ(this.ᜉ.StandardAsciiFont);
			this.ᜇ.ᜄ(this.ᜉ.StandardFarEastFont);
			this.ᜇ.ᜂ(this.ᜉ.StandardNonFarEastFont);
			this.ᜇ.ᜅ(this.ᜉ.StandardBidiFont);
			this.ᜇ.ᜉ().ᜉ((byte)this.ᜉ.ViewSetup.DocumentViewType);
			num = 12;
			continue;
			IL_28A:
			num = 13;
			continue;
			IL_2E1:
			num = 17;
			continue;
			IL_36D:
			num = 15;
			continue;
			IL_3C3:
			this.ᜇ.ᜀ(this.ᜉ.WriteProtected);
			this.ᜇ.ᜁ(this.ᜉ.HasPicture);
			num = 28;
			continue;
			IL_415:
			this.ᜇ.ᜀ(new MemoryStream(this.ᜉ.MacrosData));
			num = 8;
			continue;
			IL_499:
			num = 25;
			continue;
			IL_4C4:
			num = 21;
			continue;
			IL_4EF:
			num = 7;
		}
	}

	// Token: 0x060013B2 RID: 5042 RVA: 0x00146D04 File Offset: 0x00145D04
	private void ᜂ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				IL_23:
				if (true)
				{
				}
				Background background = this.ᜉ.Background;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_7C:
					this.ᜁ();
					spr\u24E3 spr_u24E = this.ᜉ.Escher;
					this.ᜉ.DOP.\u171C().ᜉ(true);
					spr\u2459 spr_u = new spr\u2459(this.ᜉ);
					spr_u.ᜆ();
					spr\u2459 spr_u2 = spr_u24E.ᜆ();
					int key = spr_u2.ᜅ().ᜀ();
					spr_u.ᜀ(this.ᜉ, background);
					spr\u1DB9 spr_u1DB = spr_u24E.ᜀ(ShapeDocType.Main);
					spr_u1DB.\u1714().Remove(spr_u2);
					spr_u24E.ᜈ().Remove(key);
					spr_u1DB.\u1714().Add(spr_u);
					spr_u24E.ᜈ().Add(key, spr_u);
					num = 0;
					break;
				}
				default:
					if (false)
					{
					}
					num = 1;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (background.Type != BackgroundType.NoBackground)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						goto IL_7A;
					}
					goto IL_23;
				}
				IL_7A:
				goto IL_7C;
			}
			return;
		}
	}

	// Token: 0x060013B3 RID: 5043 RVA: 0x00146E38 File Offset: 0x00145E38
	private void ᜀ(spr\u2459 A_0, spr\u2459 A_1, Background A_2, spr\u24E3 A_3)
	{
		uint num;
		for (;;)
		{
			sprΏ sprΏ = new sprΏ(this.ᜉ);
			sprΏ.ᜀ(A_2.ImageRecord);
			A_0.ᜀ(sprΏ);
			num = A_0.ᜁ(390);
			int num2 = 2;
			for (;;)
			{
				switch (num2)
				{
				case 0:
					goto IL_D6;
				case 1:
					A_3.ᜀ((int)num, sprΏ);
					num2 = 0;
					continue;
				case 2:
					if (num != 4294967295U)
					{
						num2 = 1;
						continue;
					}
					A_3.ᜆ.ᜀ().\u1714().Add(sprΏ);
					num = (uint)A_3.ᜆ.ᜀ().\u1714().Count;
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
						num2 = 3;
						continue;
					}
					break;
				case 3:
					goto IL_C0;
				}
				break;
			}
		}
		IL_C0:
		IL_D6:
		A_0.ᜀ(A_2, (int)num);
	}

	// Token: 0x060013B4 RID: 5044 RVA: 0x00146F28 File Offset: 0x00145F28
	private void ᜁ()
	{
		spr\u24E3 spr_u24E;
		for (;;)
		{
			spr_u24E = this.ᜉ.Escher;
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (spr_u24E.ᜆ.ᜀ() == null)
					{
						num = 7;
						continue;
					}
					return;
				case 1:
					goto IL_10C;
				case 2:
					goto IL_128;
				case 3:
					goto IL_10A;
				case 4:
					if (spr_u24E == null)
					{
						num = 2;
						continue;
					}
					goto IL_50;
				case 5:
					if (true)
					{
					}
					num = 6;
					continue;
				case 6:
					if (spr_u24E.ᜇ.Count != 0)
					{
						num = 1;
						continue;
					}
					goto IL_7C;
				case 7:
					spr_u24E.ᜆ.\u1714().Add(new spr\u2568(this.ᜉ));
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_50;
					default:
						if (false)
						{
						}
						num = 3;
						continue;
					}
					break;
				case 8:
					if (spr_u24E != null)
					{
						num = 5;
						continue;
					}
					goto IL_10C;
				}
				break;
				IL_50:
				num = 0;
				continue;
				IL_10C:
				num = 4;
			}
		}
		IL_7C:
		spr_u24E = new spr\u24E3(this.ᜉ);
		spr_u24E.ᜄ();
		this.ᜉ.Escher = spr_u24E;
		return;
		IL_10A:
		return;
		IL_128:
		goto IL_7C;
	}

	// Token: 0x060013B5 RID: 5045 RVA: 0x00147064 File Offset: 0x00146064
	private void ᜁ(Comment A_0)
	{
		switch (0)
		{
		default:
		{
			int num;
			for (;;)
			{
				IL_5D:
				num = 0;
				int num2 = 3;
				for (;;)
				{
					if (true)
					{
					}
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
						IEnumerator enumerator;
						switch (num2)
						{
						case 0:
							this.ᜀ(A_0.OwnerParagraph);
							this.\u171C().ᜀ(WordChunkType.ParagraphEnd);
							num = (this.\u171C() as sprច).\u171F();
							this.ᜀ(A_0.BodyPart.BodyItems, false);
							num2 = 1;
							continue;
						case 1:
							goto IL_1B4;
						case 2:
							if (A_0.Items.Count > 0)
							{
								num2 = 4;
								continue;
							}
							goto IL_1E1;
						case 3:
							if (A_0.BodyPart != null)
							{
								num2 = 0;
								continue;
							}
							goto IL_1B6;
						case 4:
							goto IL_13F;
						case 5:
							try
							{
								num2 = 1;
								for (;;)
								{
									switch (num2)
									{
									case 0:
										goto IL_F2;
									case 2:
									{
										if (!enumerator.MoveNext())
										{
											num2 = 4;
											continue;
										}
										ParagraphBase a_ = (ParagraphBase)enumerator.Current;
										this.ᜀ(a_, A_0.OwnerParagraph);
										num2 = 3;
										continue;
									}
									case 4:
										num2 = 0;
										continue;
									}
									IL_A9:
									num2 = 2;
									continue;
									goto IL_A9;
								}
								IL_F2:
								goto IL_1E1;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable = enumerator as IDisposable;
									num2 = 2;
									for (;;)
									{
										switch (num2)
										{
										case 0:
											goto IL_13C;
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
											goto IL_13E;
										}
										break;
									}
								}
								IL_13C:
								IL_13E:;
							}
							goto IL_13F;
						}
						goto IL_5D;
						IL_13F:
						num = (this.\u171C() as sprច).\u171F();
						enumerator = A_0.Items.GetEnumerator();
						num2 = 5;
						continue;
					}
					}
					IL_1B6:
					num2 = 2;
				}
			}
			IL_1B4:
			IL_1E1:
			int num3 = (this.\u171C() as sprច).\u171F();
			A_0.Format.BookmarkStartOffset = num3 - num;
			A_0.Format.BookmarkEndOffset = 0;
			return;
		}
		}
	}

	// Token: 0x060013B6 RID: 5046 RVA: 0x00147290 File Offset: 0x00146290
	private void ᜀ(Comment A_0)
	{
		int num = 4;
		for (;;)
		{
			int num2;
			int num3;
			switch (num)
			{
			case 0:
				if (A_0.Format.BookmarkStartOffset == 0)
				{
					num = 3;
					continue;
				}
				A_0.Format.BookmarkEndOffset = 0;
				num = 2;
				continue;
			case 1:
				if (true)
				{
				}
				A_0.Format.BookmarkStartOffset = num2 - num3;
				num = 0;
				continue;
			case 2:
				return;
			case 3:
				goto IL_97;
			case 5:
				goto IL_99;
			case 6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_99;
				default:
					if (false)
					{
					}
					if (num2 != 0)
					{
						num = 1;
						continue;
					}
					return;
				}
				break;
			}
			if (this.\u1713().ContainsKey(A_0.Format.TagBkmk))
			{
				num = 5;
				continue;
			}
			return;
			IL_99:
			DictionaryEntry dictionaryEntry = this.\u1713()[A_0.Format.TagBkmk];
			num3 = (int)dictionaryEntry.Key;
			num2 = (int)dictionaryEntry.Value;
			num = 6;
		}
		IL_97:
		A_0.Format.BookmarkEndOffset = 1;
	}

	// Token: 0x060013B7 RID: 5047 RVA: 0x001473C4 File Offset: 0x001463C4
	internal void \u171E()
	{
		for (;;)
		{
			if (true)
			{
			}
			this.ᜉ = null;
			int num = 31;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (this.ᜌ != null)
					{
						num = 12;
						continue;
					}
					goto IL_282;
				case 1:
					goto IL_17D;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_248;
					default:
						if (false)
						{
						}
						goto IL_E3;
					}
					break;
				case 3:
					if (this.ᜑ != null)
					{
						num = 7;
						continue;
					}
					goto IL_2F2;
				case 4:
					this.ᜎ = null;
					num = 19;
					continue;
				case 5:
					this.\u170D = null;
					num = 21;
					continue;
				case 6:
					this.\u1715.Clear();
					this.\u1715 = null;
					num = 9;
					continue;
				case 7:
					this.ᜑ.Clear();
					this.ᜑ = null;
					num = 17;
					continue;
				case 8:
					this.ᜏ = null;
					num = 25;
					continue;
				case 9:
					return;
				case 10:
					if (this.\u1712 != null)
					{
						num = 24;
						continue;
					}
					goto IL_15A;
				case 11:
					goto IL_120;
				case 12:
					this.ᜌ = null;
					num = 16;
					continue;
				case 13:
					this.\u1714.Clear();
					this.\u1714 = null;
					num = 1;
					continue;
				case 14:
					if (this.\u170D != null)
					{
						num = 5;
						continue;
					}
					goto IL_1E1;
				case 15:
					goto IL_348;
				case 16:
					goto IL_282;
				case 17:
					goto IL_2F2;
				case 18:
					this.ᜋ = null;
					num = 15;
					continue;
				case 19:
					goto IL_248;
				case 20:
					if (this.ᜏ != null)
					{
						num = 8;
						continue;
					}
					goto IL_322;
				case 21:
					goto IL_1E1;
				case 22:
					if (this.ᜐ != null)
					{
						num = 29;
						continue;
					}
					goto IL_E3;
				case 23:
					if (this.\u1715 != null)
					{
						num = 6;
						continue;
					}
					return;
				case 24:
					this.\u1712.Clear();
					this.\u1712 = null;
					num = 28;
					continue;
				case 25:
					goto IL_322;
				case 26:
					if (this.\u1713 != null)
					{
						num = 32;
						continue;
					}
					goto IL_120;
				case 27:
					if (this.\u1714 != null)
					{
						num = 13;
						continue;
					}
					goto IL_17D;
				case 28:
					goto IL_15A;
				case 29:
					this.ᜐ.Clear();
					this.ᜐ = null;
					num = 2;
					continue;
				case 30:
					if (this.ᜎ != null)
					{
						num = 4;
						continue;
					}
					goto IL_248;
				case 31:
					if (this.ᜋ != null)
					{
						num = 18;
						continue;
					}
					goto IL_348;
				case 32:
					this.\u1713.Clear();
					this.\u1713 = null;
					num = 11;
					continue;
				}
				break;
				IL_E3:
				num = 3;
				continue;
				IL_120:
				num = 27;
				continue;
				IL_15A:
				num = 26;
				continue;
				IL_17D:
				num = 23;
				continue;
				IL_1E1:
				num = 30;
				continue;
				IL_248:
				num = 20;
				continue;
				IL_282:
				num = 14;
				continue;
				IL_2F2:
				num = 10;
				continue;
				IL_322:
				num = 22;
				continue;
				IL_348:
				num = 0;
			}
		}
	}

	// Token: 0x060013B8 RID: 5048 RVA: 0x00147764 File Offset: 0x00146764
	private void ᜀ(string A_0, bool A_1)
	{
		int num = 3;
		for (;;)
		{
			sprℵ sprℵ;
			switch (num)
			{
			case 0:
				goto IL_82;
			case 1:
				goto IL_C1;
			case 2:
				goto IL_C1;
			case 4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_82;
				default:
					if (false)
					{
					}
					sprℵ = null;
					num = 8;
					continue;
				}
				break;
			case 5:
				sprℵ = this.\u171C().ᜊ();
				num = 1;
				continue;
			case 6:
				if (this.ᜐ.ContainsKey(A_0))
				{
					num = 4;
					continue;
				}
				return;
			case 7:
				return;
			case 8:
				if (A_1)
				{
					num = 5;
					continue;
				}
				sprℵ = this.\u171C().ᜈ();
				if (true)
				{
				}
				num = 2;
				continue;
			}
			if (A_0 != null)
			{
				num = 0;
				continue;
			}
			break;
			IL_82:
			num = 6;
			continue;
			IL_C1:
			sprℵ.ᜀ((ushort)this.ᜐ[A_0]);
			num = 7;
		}
	}

	// Token: 0x060013B9 RID: 5049 RVA: 0x00147878 File Offset: 0x00146878
	private string ᜀ(Field A_0)
	{
		int a_ = 19;
		string result;
		for (;;)
		{
			for (;;)
			{
				result = string.Empty;
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						MergeField mergeField;
						result = mergeField.Prefix + ClipboardData.b("䍸", a_) + mergeField.FieldName;
						num = 3;
						continue;
					}
					case 1:
						goto IL_83;
					case 2:
					{
						MergeField mergeField;
						if (mergeField.Prefix != "")
						{
							num = 0;
							continue;
						}
						return result;
					}
					case 3:
						return result;
					case 4:
						num = 2;
						continue;
					case 5:
					{
						if (true)
						{
						}
						MergeField mergeField = A_0 as MergeField;
						num = 6;
						continue;
					}
					case 6:
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
							MergeField mergeField;
							if (mergeField.FieldName != null)
							{
								num = 7;
								continue;
							}
							goto IL_83;
						}
						}
						break;
					case 7:
					{
						MergeField mergeField;
						result = mergeField.FieldName;
						num = 1;
						continue;
					}
					case 8:
						if (A_0 is MergeField)
						{
							num = 5;
							continue;
						}
						return result;
					case 9:
					{
						MergeField mergeField;
						if (mergeField.Prefix != null)
						{
							num = 4;
							continue;
						}
						return result;
					}
					}
					break;
					IL_83:
					num = 9;
				}
			}
		}
		return result;
	}

	// Token: 0x060013BA RID: 5050 RVA: 0x001479C4 File Offset: 0x001469C4
	private string ᜀ(Field A_0, string A_1)
	{
		string result;
		for (;;)
		{
			result = string.Empty;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_0.Type != FieldType.FieldEmbed)
					{
						goto IL_92;
					}
					return result;
				case 1:
					if (A_0.Type != FieldType.FieldLink)
					{
						num = 2;
						continue;
					}
					return result;
				case 2:
					num = 0;
					continue;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_92;
					default:
						goto IL_75;
					}
					break;
				case 4:
					if (true)
					{
					}
					result = A_0.Text;
					num = 3;
					continue;
				}
				break;
				IL_92:
				num = 4;
			}
		}
		IL_75:
		if (false)
		{
		}
		return result;
	}

	// Token: 0x060013BB RID: 5051 RVA: 0x00147A74 File Offset: 0x00146A74
	private string ᜀ(Field A_0, string A_1, string A_2, string A_3)
	{
		int a_ = 17;
		string text;
		for (;;)
		{
			text = ClipboardData.b("坶", a_) + spr\u1C8B.ᜀ(A_0.Type) + ClipboardData.b("坶", a_);
			int num = 15;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (A_3.Length != 0)
					{
						num = 17;
						continue;
					}
					goto IL_374;
				case 2:
					text = "";
					num = 5;
					continue;
				case 3:
					num = 18;
					continue;
				case 4:
					text = ClipboardData.b("坶", a_);
					num = 20;
					continue;
				case 5:
					goto IL_1A4;
				case 6:
					goto IL_374;
				case 7:
					num = 11;
					continue;
				case 8:
					goto IL_110;
				case 9:
					num = 30;
					continue;
				case 10:
					if (A_0.Value != null)
					{
						num = 3;
						continue;
					}
					goto IL_39A;
				case 11:
					if (A_1 != A_0.Value)
					{
						num = 25;
						continue;
					}
					goto IL_39A;
				case 12:
					goto IL_39A;
				case 13:
					text = text + ClipboardData.b("坶", a_) + A_0.Value;
					text = text + ClipboardData.b("坶", a_) + A_3;
					text = text + ClipboardData.b("坶", a_) + A_0.LocalReference;
					num = 24;
					continue;
				case 14:
					goto IL_1F8;
				case 15:
					if (A_0.Type == FieldType.FieldExpression)
					{
						num = 2;
						continue;
					}
					goto IL_1A4;
				case 16:
					text = text + ClipboardData.b("坶", a_) + A_3;
					num = 14;
					continue;
				case 17:
					num = 28;
					continue;
				case 18:
					if (A_0.Value.Length != 0)
					{
						num = 7;
						continue;
					}
					goto IL_39A;
				case 19:
					if (A_0.Type != FieldType.FieldHyperlink)
					{
						num = 9;
						continue;
					}
					goto IL_1F8;
				case 20:
					return text;
				case 21:
					if (A_0.Type == FieldType.FieldHyperlink)
					{
						num = 0;
						continue;
					}
					goto IL_374;
				case 22:
					if (true)
					{
					}
					num = 29;
					continue;
				case 23:
					text = text + ClipboardData.b("坶", a_) + A_1;
					num = 8;
					continue;
				case 24:
					goto IL_374;
				case 25:
					text = text + ClipboardData.b("坶", a_) + A_0.Value;
					num = 12;
					continue;
				case 26:
					if (A_1.Length != 0)
					{
						num = 23;
						continue;
					}
					goto IL_110;
				case 27:
					if (text == null)
					{
						num = 4;
						continue;
					}
					return text;
				case 28:
					if (A_0.LocalReference == null)
					{
						goto IL_2B9;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return text;
					default:
						if (false)
						{
						}
						num = 22;
						continue;
					}
					break;
				case 29:
					if (A_0.LocalReference != string.Empty)
					{
						num = 13;
						continue;
					}
					goto IL_2B9;
				case 30:
					if (A_3.Length != 0)
					{
						num = 16;
						continue;
					}
					goto IL_1F8;
				}
				break;
				IL_110:
				num = 10;
				continue;
				IL_1A4:
				num = 21;
				continue;
				IL_1F8:
				num = 27;
				continue;
				IL_2B9:
				text = text + ClipboardData.b("坶", a_) + A_3;
				num = 6;
				continue;
				IL_374:
				num = 26;
				continue;
				IL_39A:
				num = 19;
			}
		}
		return text;
	}

	// Token: 0x060013BC RID: 5052 RVA: 0x00147E6C File Offset: 0x00146E6C
	private void ᜀ(Paragraph A_0, BreakType A_1)
	{
		for (;;)
		{
			Paragraph paragraph = A_0.NextSibling as Paragraph;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (A_1 == BreakType.PageBreak)
					{
						num = 7;
						continue;
					}
					num = 4;
					continue;
				case 1:
					goto IL_B6;
				case 2:
					goto IL_B4;
				case 3:
					if (paragraph != null)
					{
						num = 6;
						continue;
					}
					goto IL_B6;
				case 4:
					if (A_1 == BreakType.ColumnBreak)
					{
						num = 5;
						continue;
					}
					return;
				case 5:
					this.ᜇ.ᜀ(WordChunkType.ColumnBreak);
					num = 2;
					continue;
				case 6:
					spr\u192A.ᜀ(this.\u171C().ᜉ(), paragraph.Format, A_0);
					num = 1;
					continue;
				case 7:
					goto IL_CF;
				}
				break;
				IL_B6:
				num = 0;
			}
		}
		IL_B4:
		return;
		IL_CF:
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
		this.ᜇ.ᜀ(WordChunkType.PageBreak);
	}

	// Token: 0x060013BD RID: 5053 RVA: 0x00147F74 File Offset: 0x00146F74
	private void ᜂ(ListFormat A_0, spr\u2370 A_1)
	{
		for (;;)
		{
			IL_24:
			ListType listType = A_0.ListType;
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_67:
				this.ᜀ(A_0, A_1);
				num = 2;
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
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (listType == ListType.NoList)
					{
						num = 1;
						continue;
					}
					num = 3;
					continue;
				case 1:
					goto IL_65;
				case 2:
					return;
				case 3:
					if (A_0.CustomStyleName != string.Empty)
					{
						num = 4;
						continue;
					}
					return;
				case 4:
					goto IL_9E;
				}
				goto IL_24;
			}
			IL_9E:
			goto IL_67;
		}
		IL_65:
		this.ᜁ(A_0, A_1);
	}

	// Token: 0x060013BE RID: 5054 RVA: 0x0014802C File Offset: 0x0014702C
	private void ᜁ(ListFormat A_0, spr\u2370 A_1)
	{
		int num = 0;
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
				case 1:
					goto IL_79;
				case 2:
					goto IL_5A;
				case 3:
					A_1.ᜉ().\u1715();
					num = 4;
					continue;
				case 4:
					return;
				}
				if (A_0.IsListRemoved)
				{
					num = 2;
					continue;
				}
				num = 1;
				continue;
			}
			IL_79:
			if (!A_0.IsEmptyList)
			{
				return;
			}
			num = 3;
		}
		IL_5A:
		if (true)
		{
		}
		A_1.ᜉ().ᜃ();
	}

	// Token: 0x060013BF RID: 5055 RVA: 0x001480D8 File Offset: 0x001470D8
	private void ᜀ(ListFormat A_0, spr\u2370 A_1)
	{
		bool flag;
		for (;;)
		{
			flag = A_0.CurrentListStyle.IsBuiltInStyle;
			int num = 1;
			for (;;)
			{
				string name;
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
						goto IL_FE;
					}
					break;
				case 1:
					if (flag)
					{
						goto IL_47;
					}
					num = 2;
					continue;
				case 2:
					if (!(this.ᜅ != A_0.CustomStyleName))
					{
						num = 9;
						continue;
					}
					goto IL_73;
				case 3:
					num = 4;
					continue;
				case 4:
					if (sprᣄ.ᜀ().ᜀ(name))
					{
						num = 0;
						continue;
					}
					goto IL_D2;
				case 5:
					if (A_0.IsRestartNumbering)
					{
						num = 8;
						continue;
					}
					goto IL_13A;
				case 6:
					goto IL_4F;
				case 7:
					if (true)
					{
					}
					if (!A_0.IsRestartNumbering)
					{
						num = 3;
						continue;
					}
					goto IL_D2;
				case 8:
					goto IL_73;
				case 9:
					num = 5;
					continue;
				}
				break;
				IL_47:
				num = 6;
				continue;
				IL_73:
				name = A_0.CurrentListStyle.Name;
				num = 7;
			}
		}
		IL_4F:
		A_1.ᜉ().ᜃ();
		return;
		IL_D2:
		this.ᜀ(A_1, A_0, flag);
		return;
		IL_FE:
		if (false)
		{
		}
		this.ᜀ(A_1, A_0);
		return;
		IL_13A:
		this.ᜀ(A_1, A_0);
		this.ᜅ = A_0.CustomStyleName;
	}

	// Token: 0x060013C0 RID: 5056 RVA: 0x00148234 File Offset: 0x00147234
	private void ᜀ(spr\u2370 A_0, ListFormat A_1)
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
		sprហ a_ = this.ᜑ[A_1.CustomStyleName];
		A_0.ᜋ().ᜀ(a_, A_1, A_0.ᜆ());
		this.ᜅ = A_1.CustomStyleName;
	}

	// Token: 0x060013C1 RID: 5057 RVA: 0x001482A0 File Offset: 0x001472A0
	private void ᜀ(spr\u2370 A_0, ListFormat A_1, bool A_2)
	{
		for (;;)
		{
			ListStyle listStyle = this.ᜉ.ListStyles.FindByName(A_1.CustomStyleName);
			int num = 5;
			for (;;)
			{
				sprហ sprហ;
				switch (num)
				{
				case 0:
					sprហ = this.ᜀ(listStyle, A_0.ᜆ(), A_1);
					num = 2;
					continue;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_C0;
					default:
						if (false)
						{
						}
						goto IL_182;
					}
					break;
				case 2:
					if (!this.ᜑ.ContainsKey(A_1.CustomStyleName))
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					this.ᜑ[A_1.CustomStyleName] = sprហ;
					goto IL_C0;
				case 3:
				{
					short num2 = A_0.ᜋ().ᜅ()[A_1.CustomStyleName];
					int a_ = (int)A_0.ᜋ().ᜅ()[A_1.CustomStyleName];
					this.ᜀ(a_, A_0);
					num = 9;
					continue;
				}
				case 4:
					return;
				case 5:
					if (listStyle != null)
					{
						num = 0;
						continue;
					}
					return;
				case 6:
					this.ᜑ.Add(A_1.CustomStyleName, sprហ);
					num = 1;
					continue;
				case 7:
					goto IL_182;
				case 8:
					if (A_2)
					{
						num = 3;
						continue;
					}
					goto IL_112;
				case 9:
					goto IL_112;
				}
				break;
				IL_C0:
				num = 7;
				continue;
				IL_112:
				A_0.ᜋ().ᜀ(sprហ, A_1, A_0.ᜆ(), true);
				this.ᜅ = A_1.CustomStyleName;
				A_1.IsRestartNumbering = false;
				num = 4;
				continue;
				IL_182:
				num = 8;
			}
		}
	}

	// Token: 0x060013C2 RID: 5058 RVA: 0x00148450 File Offset: 0x00147450
	private void ᜀ(int A_0, spr\u2370 A_1)
	{
		for (;;)
		{
			IL_1C:
			int num;
			sprᲵ sprᲵ;
			short num2;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_94:
				num = 0;
				break;
			default:
			{
				if (false)
				{
				}
				int a_ = A_1.ᜇ();
				sprᲵ = A_1.ᜆ().ᜁ(a_);
				num2 = -1;
				num2 = sprᲵ.ᜋ().\u1717();
				num = 4;
				break;
			}
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (num2 != -1)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
				case 1:
					goto IL_78;
				case 2:
					return;
				case 3:
					sprᲵ.ᜋ().ᜊ((short)A_0);
					num = 2;
					continue;
				case 4:
					if ((int)num2 != A_0)
					{
						num = 1;
						continue;
					}
					return;
				}
				goto IL_1C;
			}
			IL_78:
			goto IL_94;
		}
	}

	// Token: 0x060013C3 RID: 5059 RVA: 0x00148518 File Offset: 0x00147518
	private sprហ ᜀ(ListStyle A_0, spr\u2305 A_1, ListFormat A_2)
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
		sprហ sprហ = new sprហ(this.ᜁ, A_0.IsHybrid, A_0.IsSimple);
		sprᣄ.ᜀ().ᜂ().Add(this.ᜁ, A_0.Name);
		sprἹ.ᜀ(A_0, sprហ, A_1);
		this.ᜁ++;
		return sprហ;
	}

	// Token: 0x060013C4 RID: 5060 RVA: 0x001485A0 File Offset: 0x001475A0
	private void ᜀ(spr\u2370 A_0, ParagraphStyle A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			short a_;
			int num3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_AB;
				case 1:
					A_1.ParagraphFormat.Sprms.ᜆ(new spr\u1CC1(17931));
					num = 4;
					continue;
				case 2:
					if (A_1.ParagraphFormat.Sprms.ᜇ(17931) == null)
					{
						num = 1;
						continue;
					}
					goto IL_24E;
				case 3:
					A_1.ParagraphFormat.Sprms.ᜆ(new spr\u1CC1(17931));
					num = 0;
					continue;
				case 4:
					goto IL_D4;
				case 5:
					goto IL_101;
				case 6:
					goto IL_11D;
				case 7:
				{
					string name;
					short num2 = A_0.ᜋ().ᜅ()[name];
					a_ = A_0.ᜋ().ᜅ()[name];
					if (true)
					{
					}
					num = 11;
					continue;
				}
				case 9:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_101;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 10:
				{
					string name;
					if (sprᣄ.ᜀ().ᜀ(name))
					{
						num = 7;
						continue;
					}
					ListStyle currentListStyle = A_1.ListFormat.CurrentListStyle;
					ListFormat listFormat = A_1.ListFormat;
					sprហ sprហ = this.ᜀ(currentListStyle, A_0.ᜆ(), listFormat);
					this.ᜑ.Add(listFormat.CustomStyleName, sprហ);
					num3 = A_0.ᜋ().ᜀ(sprហ, listFormat, A_0.ᜆ(), false);
					num = 2;
					continue;
				}
				case 11:
					if (A_1.ParagraphFormat.Sprms.ᜇ(17931) == null)
					{
						num = 3;
						continue;
					}
					goto IL_69;
				}
				if (A_1.ListFormat.ListType != ListType.NoList)
				{
					num = 9;
					continue;
				}
				return;
				IL_101:
				if (A_1.ListFormat.CurrentListStyle == null)
				{
					num = 6;
				}
				else
				{
					string name = A_1.ListFormat.CurrentListStyle.Name;
					num = 10;
				}
			}
			IL_69:
			A_1.ParagraphFormat.Sprms.ᜇ(17931).ᜀ(a_);
			return;
			IL_AB:
			goto IL_69;
			IL_D4:
			goto IL_24E;
			IL_11D:
			return;
			IL_24E:
			A_1.ParagraphFormat.Sprms.ᜇ(17931).ᜀ((short)num3);
			return;
		}
		}
	}

	// Token: 0x060013C5 RID: 5061 RVA: 0x00148818 File Offset: 0x00147818
	private void ᜀ()
	{
		for (;;)
		{
			IL_1C:
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_92:
				num = 2;
				break;
			default:
				if (false)
				{
				}
				this.ᜅ = null;
				sprᣄ.ᜀ().ᜂ().Clear();
				num = 1;
				break;
			}
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					if (this.ᜑ != null)
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					return;
				case 2:
					if (this.ᜑ.Count > 0)
					{
						num = 4;
						continue;
					}
					return;
				case 3:
					goto IL_78;
				case 4:
					this.ᜑ.Clear();
					num = 0;
					continue;
				}
				goto IL_1C;
			}
			IL_78:
			goto IL_92;
		}
	}

	// Token: 0x040018CC RID: 6348
	private const string ᜀ = "OLE_LINK";

	// Token: 0x040018CD RID: 6349
	private int ᜁ = 1720085641;

	// Token: 0x040018CE RID: 6350
	private int ᜂ = 1;

	// Token: 0x040018CF RID: 6351
	private int ᜃ;

	// Token: 0x040018D0 RID: 6352
	private int ᜄ;

	// Token: 0x040018D1 RID: 6353
	private string ᜅ = string.Empty;

	// Token: 0x040018D2 RID: 6354
	private Paragraph ᜆ;

	// Token: 0x040018D3 RID: 6355
	private sprច ᜇ;

	// Token: 0x040018D4 RID: 6356
	private spr\u2370 ᜈ;

	// Token: 0x040018D5 RID: 6357
	private Document ᜉ;

	// Token: 0x040018D6 RID: 6358
	private ISection ᜊ;

	// Token: 0x040018D7 RID: 6359
	private TextBoxItemCollection ᜋ;

	// Token: 0x040018D8 RID: 6360
	private TextBoxItemCollection ᜌ;

	// Token: 0x040018D9 RID: 6361
	private List<Comment> \u170D;

	// Token: 0x040018DA RID: 6362
	private List<Footnote> ᜎ;

	// Token: 0x040018DB RID: 6363
	private List<Footnote> ᜏ;

	// Token: 0x040018DC RID: 6364
	private Dictionary<string, int> ᜐ = new Dictionary<string, int>();

	// Token: 0x040018DD RID: 6365
	private Dictionary<string, sprហ> ᜑ = new Dictionary<string, sprហ>();

	// Token: 0x040018DE RID: 6366
	private List<string> \u1712 = new List<string>();

	// Token: 0x040018DF RID: 6367
	private Stack<Field> \u1713 = new Stack<Field>();

	// Token: 0x040018E0 RID: 6368
	private Dictionary<int, DictionaryEntry> \u1714;

	// Token: 0x040018E1 RID: 6369
	private List<DocPicture> \u1715;
}
