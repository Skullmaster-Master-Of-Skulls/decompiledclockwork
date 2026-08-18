using System;
using System.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;

namespace Spire.Doc
{
	// Token: 0x0200009C RID: 156
	public class HeaderFooter : Body
	{
		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600016E RID: 366 RVA: 0x000119B0 File Offset: 0x000109B0
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.HeaderFooter;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600016F RID: 367 RVA: 0x000119EC File Offset: 0x000109EC
		// (set) Token: 0x06000170 RID: 368 RVA: 0x00011A30 File Offset: 0x00010A30
		internal HeaderFooterType Type
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

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00011A74 File Offset: 0x00010A74
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00011BAC File Offset: 0x00010BAC
		internal bool WriteWatermark
		{
			get
			{
				for (;;)
				{
					Section section = base.OwnerBase as Section;
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!section.HeadersFooters.OddHeader.ᜃ())
							{
								num = 2;
								continue;
							}
							return false;
						case 1:
							if (section != null)
							{
								num = 7;
								continue;
							}
							return false;
						case 2:
							num = 4;
							continue;
						case 3:
							if (this.ᜁ)
							{
								num = 8;
								continue;
							}
							num = 1;
							continue;
						case 4:
							if (section.HeadersFooters.FirstPageHeader.ᜃ())
							{
								return false;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_E8;
							default:
								if (false)
								{
								}
								num = 9;
								continue;
							}
							break;
						case 5:
							if (!section.HeadersFooters.EvenHeader.ᜃ())
							{
								num = 6;
								continue;
							}
							return false;
						case 6:
							goto IL_E8;
						case 7:
							num = 5;
							continue;
						case 8:
							return true;
						case 9:
							return true;
						}
						break;
						IL_E8:
						if (true)
						{
						}
						num = 0;
					}
				}
				return true;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00011BF0 File Offset: 0x00010BF0
		// (set) Token: 0x06000174 RID: 372 RVA: 0x00011C34 File Offset: 0x00010C34
		public bool LinkToPrevious
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
				return this.ᜁ();
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
				this.ᜀ(value);
			}
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00011C78 File Offset: 0x00010C78
		internal bool ᜃ()
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

		// Token: 0x06000176 RID: 374 RVA: 0x00011CBC File Offset: 0x00010CBC
		internal HeaderFooter(Section A_0, HeaderFooterType A_1) : base(A_0)
		{
			this.ᜀ = A_1;
			this.ᜂ = true;
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00011CE0 File Offset: 0x00010CE0
		private bool ᜁ()
		{
			for (;;)
			{
				int num;
				int num2;
				Section section;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_DA:
					this.ᜂ = true;
					num++;
					num2 = 8;
					break;
				default:
					if (false)
					{
					}
					section = (base.OwnerBase as Section);
					num2 = 7;
					break;
				}
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						int num3;
						if (num3 == 0)
						{
							num2 = 10;
							continue;
						}
						num2 = 1;
						continue;
					}
					case 1:
					{
						int num3;
						if (num3 > 0)
						{
							num2 = 12;
							continue;
						}
						goto IL_16C;
					}
					case 2:
						return false;
					case 3:
						if (num >= 6)
						{
							num2 = 13;
							continue;
						}
						num2 = 6;
						continue;
					case 4:
						goto IL_A1;
					case 5:
						goto IL_16A;
					case 6:
						if (base.Items.Count > 0)
						{
							num2 = 9;
							continue;
						}
						goto IL_DA;
					case 7:
					{
						if (section == null)
						{
							num2 = 2;
							continue;
						}
						int num3 = section.ឯ();
						num2 = 0;
						continue;
					}
					case 8:
						goto IL_11D;
					case 9:
						if (true)
						{
						}
						this.ᜂ = false;
						num2 = 4;
						continue;
					case 10:
						this.ᜂ = false;
						num2 = 5;
						continue;
					case 11:
						goto IL_11D;
					case 12:
						num = 0;
						num2 = 11;
						continue;
					case 13:
						goto IL_137;
					}
					break;
					IL_11D:
					num2 = 3;
				}
			}
			return false;
			IL_A1:
			IL_137:
			IL_16A:
			IL_16C:
			return this.ᜂ;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00011E60 File Offset: 0x00010E60
		private new void ᜀ(bool A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 1;
				for (;;)
				{
					Section section;
					IEnumerator enumerator;
					switch (num)
					{
					case 0:
						base.ChildObjects.Clear();
						(base.OwnerBase as Section).HeadersFooters[this.ᜀ] = new HeaderFooter(base.OwnerBase as Section, this.ᜀ);
						num = 5;
						continue;
					case 2:
						if (section != null)
						{
							num = 3;
							continue;
						}
						goto IL_21F;
					case 3:
						goto IL_189;
					case 4:
						try
						{
							num = 0;
							for (;;)
							{
								switch (num)
								{
								case 1:
									num = 2;
									continue;
								case 2:
								{
									HeaderFooter headerFooter;
									if (this.ᜀ(headerFooter))
									{
										num = 7;
										continue;
									}
									break;
								}
								case 3:
									goto IL_123;
								case 4:
								{
									if (!enumerator.MoveNext())
									{
										num = 8;
										continue;
									}
									HeaderFooter headerFooter = (HeaderFooter)enumerator.Current;
									num = 6;
									continue;
								}
								case 6:
								{
									HeaderFooter headerFooter;
									if (this.ᜀ == headerFooter.ᜀ)
									{
										num = 1;
										continue;
									}
									break;
								}
								case 7:
								{
									HeaderFooter headerFooter;
									headerFooter.m_bodyItems.ᜀ(this.m_bodyItems);
									num = 5;
									continue;
								}
								case 8:
									num = 3;
									continue;
								}
								IL_AA:
								num = 4;
								continue;
								goto IL_AA;
							}
							IL_123:
							goto IL_21F;
						}
						finally
						{
							for (;;)
							{
								IDisposable disposable = enumerator as IDisposable;
								num = 1;
								for (;;)
								{
									switch (num)
									{
									case 0:
										goto IL_186;
									case 1:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_17D;
										default:
											if (false)
											{
											}
											if (disposable != null)
											{
												num = 2;
												continue;
											}
											goto IL_188;
										}
										break;
									case 2:
										disposable.Dispose();
										goto IL_17D;
									}
									break;
									IL_17D:
									num = 0;
								}
							}
							IL_186:
							IL_188:;
						}
						goto IL_189;
					case 5:
						goto IL_1EE;
					}
					if (A_0)
					{
						num = 0;
						continue;
					}
					section = this.ᜀ();
					if (true)
					{
					}
					num = 2;
					continue;
					IL_189:
					enumerator = section.HeadersFooters.GetEnumerator();
					num = 4;
				}
				IL_1EE:
				IL_21F:
				this.ᜂ = A_0;
				return;
			}
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x000120A4 File Offset: 0x000110A4
		private new Section ᜀ()
		{
			Section section;
			for (;;)
			{
				IL_24:
				section = ((base.OwnerBase as Section).PreviousSibling as Section);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_6A:
					num = 2;
					break;
				default:
					if (false)
					{
					}
					num = 1;
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
					{
						HeadersFooters headersFooters;
						if (!headersFooters.LinkToPrevious)
						{
							num = 3;
							continue;
						}
						goto IL_6A;
					}
					case 1:
						goto IL_68;
					case 2:
					{
						if (section == null)
						{
							num = 4;
							continue;
						}
						HeadersFooters headersFooters = section.HeadersFooters;
						num = 0;
						continue;
					}
					case 3:
						return section;
					case 4:
						return section;
					}
					goto IL_24;
				}
				IL_68:
				goto IL_6A;
			}
			return section;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0001215C File Offset: 0x0001115C
		private new bool ᜀ(HeaderFooter A_0)
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = A_0.Paragraphs.GetEnumerator();
				bool result;
				try
				{
					int num = 1;
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
								goto IL_7D;
							case 2:
								goto IL_204;
							case 3:
								try
								{
									num = 8;
									for (;;)
									{
										ParagraphBase paragraphBase;
										switch (num)
										{
										case 0:
											if (paragraphBase is TextBox)
											{
												num = 7;
												continue;
											}
											num = 11;
											continue;
										case 2:
											num = 0;
											continue;
										case 3:
											goto IL_161;
										case 4:
										{
											IEnumerator enumerator2;
											if (!enumerator2.MoveNext())
											{
												num = 10;
												continue;
											}
											paragraphBase = (ParagraphBase)enumerator2.Current;
											num = 5;
											continue;
										}
										case 5:
											if (!(paragraphBase is DocPicture))
											{
												num = 2;
												continue;
											}
											goto IL_195;
										case 6:
											result = false;
											num = 3;
											continue;
										case 7:
											goto IL_195;
										case 9:
											goto IL_1B6;
										case 10:
											num = 9;
											continue;
										case 11:
											if (paragraphBase is spr\u248F)
											{
												num = 6;
												continue;
											}
											break;
										}
										IL_133:
										num = 4;
										continue;
										goto IL_133;
										IL_195:
										paragraphBase.ᜁ = true;
										num = 1;
									}
									IL_161:
									return result;
									IL_1B6:
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
												goto IL_203;
											case 1:
												disposable.Dispose();
												num = 2;
												continue;
											case 2:
												goto IL_201;
											}
											break;
										}
									}
									IL_201:
									IL_203:;
								}
								goto IL_204;
							case 4:
								goto IL_210;
							}
							IL_74:
							num = 0;
							continue;
							goto IL_74;
							IL_204:
							num = 4;
							continue;
						}
						IL_7D:
						if (!enumerator.MoveNext())
						{
							num = 2;
						}
						else
						{
							Paragraph paragraph = (Paragraph)enumerator.Current;
							IEnumerator enumerator2 = paragraph.Items.GetEnumerator();
							num = 3;
						}
					}
					IL_210:
					goto IL_26;
				}
				finally
				{
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
								goto IL_25C;
							case 1:
								goto IL_25A;
							case 2:
								disposable2.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_25A:
					IL_25C:;
				}
				return result;
				IL_26:
				if (true)
				{
				}
				return true;
			}
			}
		}

		// Token: 0x04000988 RID: 2440
		private new HeaderFooterType ᜀ;

		// Token: 0x04000989 RID: 2441
		private int[] \u25D9\u00A9\u0082\u00B0;

		// Token: 0x0400098A RID: 2442
		private long[] \u2609\u00A6\u008Bª;

		// Token: 0x0400098B RID: 2443
		private string[] \u2460\u008B\u00A4\u0084;

		// Token: 0x0400098C RID: 2444
		private float \u2460\u008A\u0093\u009C;

		// Token: 0x0400098D RID: 2445
		private int[] \u25D8\u0087\u0089\u009A;

		// Token: 0x0400098E RID: 2446
		private new bool ᜁ;

		// Token: 0x0400098F RID: 2447
		private new bool ᜂ;
	}
}
