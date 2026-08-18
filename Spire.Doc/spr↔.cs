using System;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using Spire.CompoundFile.Doc;
using Spire.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

// Token: 0x0200034A RID: 842
internal class spr\u2194
{
	// Token: 0x06002D06 RID: 11526 RVA: 0x002B2770 File Offset: 0x002B1770
	internal HybridDictionary ᜂ()
	{
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
					continue;
				default:
					goto IL_69;
				}
				break;
			case 1:
				this.ᜆ = new HybridDictionary();
				num = 0;
				continue;
			}
			if (true)
			{
			}
			if (this.ᜆ != null)
			{
				goto IL_71;
			}
			num = 1;
		}
		IL_69:
		if (false)
		{
		}
		IL_71:
		return this.ᜆ;
	}

	// Token: 0x06002D08 RID: 11528 RVA: 0x002B2814 File Offset: 0x002B1814
	public string ᜀ(Document A_0)
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
		this.ᜄ = A_0;
		this.ᜃ = true;
		this.ᜀ();
		this.ᜃ = false;
		return this.ᜁ;
	}

	// Token: 0x06002D09 RID: 11529 RVA: 0x002B2870 File Offset: 0x002B1870
	public void ᜀ(TextWriter A_0, IDocument A_1)
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
		this.ᜀ = A_0;
		this.ᜄ = (A_1 as Document);
		this.ᜀ();
	}

	// Token: 0x06002D0A RID: 11530 RVA: 0x002B28C4 File Offset: 0x002B18C4
	public void ᜀ(StreamReader A_0, IDocument A_1)
	{
		int a_ = 16;
		switch (0)
		{
		default:
		{
			string text;
			string[] array;
			for (;;)
			{
				IL_88:
				text = A_0.ReadToEnd();
				array = text.Split(ClipboardData.b("籵", a_).ToCharArray());
				int num = 4;
				for (;;)
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
						int num2;
						int num3;
						switch (num)
						{
						case 0:
							goto IL_275;
						case 1:
						{
							if (num2 >= num3)
							{
								num = 5;
								continue;
							}
							string text2 = array[num2];
							text2 = text2.Trim(ClipboardData.b("筵", a_).ToCharArray());
							num = 9;
							continue;
						}
						case 2:
							if (num2 + 1 < num3)
							{
								num = 16;
								continue;
							}
							goto IL_14A;
						case 3:
							goto IL_1AA;
						case 4:
							if (A_1.LastParagraph == null)
							{
								num = 10;
								continue;
							}
							goto IL_275;
						case 5:
							goto IL_1C6;
						case 6:
							goto IL_14A;
						case 7:
							A_1.LastSection.Body.AddParagraph();
							num = 6;
							continue;
						case 8:
						{
							if (true)
							{
							}
							string text2;
							((IParagraph)A_1.LastParagraph).AppendText(text2);
							num = 2;
							continue;
						}
						case 9:
						{
							string text2;
							if (text2 != string.Empty)
							{
								num = 17;
								continue;
							}
							goto IL_14A;
						}
						case 10:
							num = 11;
							continue;
						case 11:
							if (A_1.LastSection == null)
							{
								goto IL_17D;
							}
							A_1.LastSection.Body.AddParagraph();
							num = 12;
							continue;
						case 12:
							goto IL_275;
						case 13:
							if (array[num2 + 1] != string.Empty)
							{
								num = 7;
								continue;
							}
							goto IL_14A;
						case 14:
							goto IL_1AA;
						case 15:
						{
							string text2;
							if (text2 != ClipboardData.b("筵", a_))
							{
								num = 8;
								continue;
							}
							goto IL_14A;
						}
						case 16:
							num = 13;
							continue;
						case 17:
							num = 15;
							continue;
						case 18:
							A_1.CreateMinialDocument();
							num = 0;
							continue;
						}
						goto IL_88;
						IL_14A:
						num2++;
						num = 3;
						continue;
						IL_1AA:
						num = 1;
						continue;
						IL_275:
						num2 = 0;
						num3 = array.Length;
						num = 14;
						continue;
					}
					}
					IL_17D:
					num = 18;
				}
			}
			IL_1C6:
			this.ᜀ(text, array, A_1);
			return;
		}
		}
	}

	// Token: 0x06002D0B RID: 11531 RVA: 0x002B2B68 File Offset: 0x002B1B68
	public void ᜀ(TextReader A_0, IDocument A_1)
	{
		int a_ = 4;
		switch (0)
		{
		default:
		{
			string text;
			string[] array;
			for (;;)
			{
				text = A_0.ReadToEnd();
				array = text.Split(ClipboardData.b("恩", a_).ToCharArray());
				int num = 3;
				for (;;)
				{
					int num2;
					int num3;
					switch (num)
					{
					case 0:
						goto IL_1A7;
					case 1:
						A_1.LastSection.Body.AddParagraph();
						num = 10;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_96;
						default:
							if (false)
							{
							}
							A_1.CreateMinialDocument();
							num = 14;
							continue;
						}
						break;
					case 3:
						goto IL_96;
					case 4:
						goto IL_1C3;
					case 5:
					{
						string text2;
						((IParagraph)A_1.LastParagraph).AppendText(text2);
						num = 16;
						continue;
					}
					case 6:
					{
						string text2;
						if (text2 != string.Empty)
						{
							num = 17;
							continue;
						}
						goto IL_147;
					}
					case 7:
						num = 18;
						continue;
					case 8:
						goto IL_1A7;
					case 9:
						if (array[num2 + 1] != string.Empty)
						{
							num = 1;
							continue;
						}
						goto IL_147;
					case 10:
						goto IL_147;
					case 11:
					{
						if (num2 >= num3)
						{
							num = 4;
							continue;
						}
						if (true)
						{
						}
						string text2 = array[num2];
						text2 = text2.Trim(ClipboardData.b("杩", a_).ToCharArray());
						num = 6;
						continue;
					}
					case 12:
					{
						string text2;
						if (text2 != ClipboardData.b("杩", a_))
						{
							num = 5;
							continue;
						}
						goto IL_147;
					}
					case 13:
						goto IL_272;
					case 14:
						goto IL_272;
					case 15:
						num = 9;
						continue;
					case 16:
						if (num2 + 1 < num3)
						{
							num = 15;
							continue;
						}
						goto IL_147;
					case 17:
						num = 12;
						continue;
					case 18:
						if (A_1.LastSection == null)
						{
							num = 2;
							continue;
						}
						A_1.LastSection.Body.AddParagraph();
						num = 13;
						continue;
					}
					break;
					IL_96:
					if (A_1.LastParagraph == null)
					{
						num = 7;
						continue;
					}
					goto IL_272;
					IL_147:
					num2++;
					num = 8;
					continue;
					IL_1A7:
					num = 11;
					continue;
					IL_272:
					num2 = 0;
					num3 = array.Length;
					num = 0;
				}
			}
			IL_1C3:
			this.ᜀ(text, array, A_1);
			return;
		}
		}
	}

	// Token: 0x06002D0C RID: 11532 RVA: 0x002B2E08 File Offset: 0x002B1E08
	private void ᜀ(string A_0, string[] A_1, IDocument A_2)
	{
		int a_ = 8;
		switch (0)
		{
		default:
		{
			int num;
			int num2;
			for (;;)
			{
				for (;;)
				{
					num = A_1.Length;
					num2 = 0;
					int num3 = 0;
					int num4 = 2;
					for (;;)
					{
						int num5;
						switch (num4)
						{
						case 0:
							goto IL_193;
						case 1:
							goto IL_7A;
						case 2:
							goto IL_166;
						case 3:
						{
							string a;
							if (a != string.Empty)
							{
								num4 = 13;
								continue;
							}
							goto IL_14F;
						}
						case 4:
							goto IL_1C6;
						case 5:
						{
							if (true)
							{
							}
							string text;
							if (text == string.Empty)
							{
								num4 = 4;
								continue;
							}
							string[] array = text.Split(ClipboardData.b("乭", a_).ToCharArray());
							string[] array2 = array;
							num5 = 0;
							num4 = 11;
							continue;
						}
						case 6:
							goto IL_E3;
						case 7:
							num4 = 5;
							continue;
						case 8:
							goto IL_14F;
						case 9:
						{
							if (num3 >= A_1.Length)
							{
								num4 = 0;
								continue;
							}
							string text = A_1[num3];
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								if (false)
								{
								}
								num4 = 15;
								continue;
							}
							break;
						}
						case 10:
							goto IL_E3;
						case 11:
							goto IL_7A;
						case 12:
						{
							string[] array2;
							if (num5 >= array2.Length)
							{
								num4 = 6;
								continue;
							}
							string a = array2[num5];
							num4 = 3;
							continue;
						}
						case 13:
							num2++;
							num4 = 8;
							continue;
						case 14:
							goto IL_166;
						case 15:
						{
							string text;
							if (!(text == ClipboardData.b("捭", a_)))
							{
								num4 = 7;
								continue;
							}
							goto IL_1C6;
						}
						}
						break;
						IL_7A:
						num4 = 12;
						continue;
						IL_E3:
						num3++;
						num4 = 14;
						continue;
						IL_14F:
						num5++;
						num4 = 1;
						continue;
						IL_166:
						num4 = 9;
						continue;
						IL_1C6:
						num--;
						num4 = 10;
					}
				}
			}
			IL_193:
			A_0 = A_0.Replace(ClipboardData.b("乭", a_), string.Empty);
			A_0 = A_0.Replace(ClipboardData.b("摭", a_), string.Empty);
			A_0 = A_0.Replace(ClipboardData.b("捭", a_), string.Empty);
			A_2.BuiltinDocumentProperties.ParagraphCount = num;
			A_2.BuiltinDocumentProperties.WordCount = num2;
			A_2.BuiltinDocumentProperties.CharCount = A_0.Length;
			return;
		}
		}
	}

	// Token: 0x06002D0D RID: 11533 RVA: 0x002B30A0 File Offset: 0x002B20A0
	protected void ᜁ(Document A_0)
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

	// Token: 0x06002D0E RID: 11534 RVA: 0x002B30DC File Offset: 0x002B20DC
	protected void ᜀ(IBody A_0)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				int num = A_0.ChildObjects.Count - 1;
				BodyRegion bodyRegion = null;
				int num2 = 0;
				int num3 = 13;
				for (;;)
				{
					bool flag;
					switch (num3)
					{
					case 0:
						if (true)
						{
						}
						num3 = 7;
						continue;
					case 1:
						goto IL_E8;
					case 2:
						num3 = 6;
						continue;
					case 3:
						goto IL_15A;
					case 4:
						return;
					case 5:
						goto IL_E8;
					case 6:
						flag = false;
						goto IL_13E;
					case 7:
					{
						DocumentObjectType documentObjectType;
						if (documentObjectType != DocumentObjectType.Table)
						{
							num3 = 12;
							continue;
						}
						this.ᜀ(bodyRegion as ITable);
						num3 = 5;
						continue;
					}
					case 8:
					{
						if (num2 > num)
						{
							num3 = 4;
							continue;
						}
						bodyRegion = (A_0.ChildObjects[num2] as BodyRegion);
						DocumentObjectType documentObjectType = bodyRegion.DocumentObjectType;
						num3 = 9;
						continue;
					}
					case 9:
					{
						DocumentObjectType documentObjectType;
						if (documentObjectType != DocumentObjectType.Paragraph)
						{
							num3 = 0;
							continue;
						}
						num3 = 14;
						continue;
					}
					case 10:
						goto IL_E8;
					case 11:
						flag = true;
						goto IL_13E;
					case 12:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							num3 = 10;
							continue;
						}
						break;
					case 13:
						goto IL_15A;
					case 14:
						if (bodyRegion as Paragraph != this.ᜅ)
						{
							num3 = 2;
							continue;
						}
						num3 = 11;
						continue;
					}
					break;
					IL_E8:
					num2++;
					num3 = 3;
					continue;
					IL_13E:
					bool a_ = flag;
					this.ᜀ(bodyRegion as IParagraph, a_);
					num3 = 1;
					continue;
					IL_15A:
					num3 = 8;
				}
			}
			return;
		}
	}

	// Token: 0x06002D0F RID: 11535 RVA: 0x002B32B0 File Offset: 0x002B22B0
	protected void ᜀ(IParagraph A_0, bool A_1)
	{
		switch (0)
		{
		default:
			for (;;)
			{
				this.ᜀ(A_0);
				int num = 0;
				int count = A_0.Items.Count;
				int num2 = 11;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_7E;
					case 1:
						goto IL_7E;
					case 2:
						goto IL_150;
					case 3:
					{
						IL_1DB:
						IParagraphBase paragraphBase;
						if ((paragraphBase as Break).BreakType == BreakType.LineBreak)
						{
							num2 = 9;
							continue;
						}
						goto IL_7E;
					}
					case 4:
						return;
					case 5:
						goto IL_7E;
					case 6:
						num2 = 12;
						continue;
					case 7:
					{
						if (num >= count)
						{
							num2 = 6;
							continue;
						}
						IParagraphBase paragraphBase = A_0[num];
						DocumentObjectType documentObjectType = paragraphBase.DocumentObjectType;
						num2 = 10;
						continue;
					}
					case 8:
						this.ᜃ();
						num2 = 4;
						continue;
					case 9:
						this.ᜃ();
						num2 = 1;
						continue;
					case 10:
					{
						DocumentObjectType documentObjectType;
						if (documentObjectType != DocumentObjectType.TextRange)
						{
							num2 = 16;
							continue;
						}
						if (true)
						{
						}
						IParagraphBase paragraphBase;
						this.ᜀ(paragraphBase as ITextRange);
						num2 = 0;
						continue;
					}
					case 11:
						goto IL_150;
					case 12:
						if (!A_1)
						{
							num2 = 8;
							continue;
						}
						return;
					case 13:
						goto IL_7E;
					case 14:
					{
						DocumentObjectType documentObjectType;
						switch (documentObjectType)
						{
						case DocumentObjectType.TextBox:
						{
							IParagraphBase paragraphBase;
							this.ᜀ((paragraphBase as TextBox).Body);
							num2 = 5;
							continue;
						}
						case DocumentObjectType.Break:
							num2 = 3;
							continue;
						default:
							num2 = 15;
							continue;
						}
						break;
					}
					case 15:
						num2 = 13;
						continue;
					case 16:
						num2 = 14;
						continue;
					}
					break;
					IL_7E:
					num++;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1DB;
					default:
						if (false)
						{
						}
						num2 = 2;
						continue;
					}
					IL_150:
					num2 = 7;
				}
			}
			return;
		}
	}

	// Token: 0x06002D10 RID: 11536 RVA: 0x002B34C4 File Offset: 0x002B24C4
	protected void ᜀ(ITable A_0)
	{
		switch ((1 == 1) ? 1 : 0)
		{
		case 0:
		case 2:
			goto IL_34;
		}
		if (false)
		{
		}
		switch (0)
		{
		default:
		{
			IL_34:
			IEnumerator enumerator = A_0.Rows.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_164;
					case 1:
						try
						{
							num = 3;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_117;
								case 2:
								{
									IEnumerator enumerator2;
									if (!enumerator2.MoveNext())
									{
										num = 4;
										continue;
									}
									TableCell a_ = (TableCell)enumerator2.Current;
									this.ᜀ(a_);
									num = 1;
									continue;
								}
								case 4:
									num = 0;
									continue;
								}
								IL_D4:
								num = 2;
								continue;
								goto IL_D4;
							}
							IL_117:
							break;
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
										goto IL_161;
									case 1:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_163;
									case 2:
										disposable.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_161:
							IL_163:;
						}
						goto IL_164;
					case 2:
						goto IL_170;
					case 3:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						TableRow tableRow = (TableRow)enumerator.Current;
						IEnumerator enumerator2 = tableRow.Cells.GetEnumerator();
						num = 1;
						continue;
					}
					}
					IL_8D:
					num = 3;
					continue;
					goto IL_8D;
					IL_164:
					num = 2;
				}
				IL_170:;
			}
			finally
			{
				if (true)
				{
				}
				for (;;)
				{
					IDisposable disposable2 = enumerator as IDisposable;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_1BF;
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
							goto IL_1C1;
						}
						break;
					}
				}
				IL_1BF:
				IL_1C1:;
			}
			return;
		}
		}
	}

	// Token: 0x06002D11 RID: 11537 RVA: 0x002B36C8 File Offset: 0x002B26C8
	protected void ᜀ(ISection A_0, bool A_1)
	{
		int a_ = 3;
		int num = 1;
		for (;;)
		{
			switch (num)
			{
			case 0:
				if (true)
				{
				}
				this.ᜀ.WriteLine("");
				num = 5;
				continue;
			case 2:
				goto IL_CD;
			case 3:
				this.ᜁ += ClipboardData.b("摨慪", a_);
				num = 2;
				continue;
			case 4:
				if (!A_1)
				{
					num = 0;
					continue;
				}
				goto IL_CF;
			case 5:
				goto IL_6D;
			}
			if (this.ᜃ)
			{
				num = 3;
			}
			else
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_6D;
				default:
					if (false)
					{
					}
					num = 4;
					break;
				}
			}
		}
		IL_6D:
		IL_CD:
		IL_CF:
		this.ᜂ++;
	}

	// Token: 0x06002D12 RID: 11538 RVA: 0x002B37B4 File Offset: 0x002B27B4
	protected void ᜀ(ITextRange A_0)
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
			if (!this.ᜃ)
			{
				this.ᜀ.Write(A_0.Text);
				return;
			}
			break;
		}
		this.ᜁ += A_0.Text;
	}

	// Token: 0x06002D13 RID: 11539 RVA: 0x002B3824 File Offset: 0x002B2824
	protected void ᜀ(IParagraph A_0)
	{
		int a_ = 16;
		int num = 7;
		for (;;)
		{
			switch (num)
			{
			case 0:
				goto IL_29E;
			case 1:
				goto IL_2A3;
			case 2:
				if (A_0.ListFormat.IsRestartNumbering)
				{
					num = 1;
					continue;
				}
				num = 15;
				continue;
			case 3:
				num = 17;
				continue;
			case 4:
				this.ᜀ(A_0.ListFormat, false);
				num = 14;
				continue;
			case 5:
				goto IL_FD;
			case 6:
				if (A_0.ListFormat.CurrentListStyle.ListType != ListType.Bulleted)
				{
					goto IL_22C;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2A3;
				default:
					if (false)
					{
					}
					num = 3;
					continue;
				}
				break;
			case 8:
				if (this.ᜃ)
				{
					num = 0;
					continue;
				}
				this.ᜀ.Write(A_0.ListFormat.CurrentListLevel.NumberPrefix + Convert.ToString(this.ᜀ(A_0.ListFormat) + 1) + A_0.ListFormat.CurrentListLevel.NumberSufix + ClipboardData.b("噵塷", a_));
				num = 16;
				continue;
			case 9:
				goto IL_27D;
			case 10:
				if (this.ᜃ)
				{
					if (true)
					{
					}
					num = 5;
					continue;
				}
				goto IL_30A;
			case 11:
				if (A_0.ListFormat.CurrentListStyle.ListType != ListType.NoList)
				{
					num = 13;
					continue;
				}
				return;
			case 12:
				num = 6;
				continue;
			case 13:
				num = 2;
				continue;
			case 14:
				goto IL_27D;
			case 15:
				if (A_0.ListFormat.ListLevelNumber == 0)
				{
					num = 4;
					continue;
				}
				goto IL_27D;
			case 16:
				goto IL_227;
			case 17:
				if (A_0.ListFormat.CurrentListLevel.PatternType == ListPatternType.Bullet)
				{
					num = 18;
					continue;
				}
				goto IL_22C;
			case 18:
				num = 10;
				continue;
			}
			if (A_0.ListFormat.CurrentListStyle != null)
			{
				num = 12;
				continue;
			}
			return;
			IL_22C:
			num = 11;
			continue;
			IL_27D:
			num = 8;
			continue;
			IL_2A3:
			this.ᜀ(A_0.ListFormat, true);
			num = 9;
		}
		IL_FD:
		this.ᜁ += ClipboardData.b("屵塷", a_);
		return;
		IL_227:
		return;
		IL_29E:
		string text = this.ᜁ;
		this.ᜁ = string.Concat(new string[]
		{
			text,
			A_0.ListFormat.CurrentListLevel.NumberPrefix,
			Convert.ToString(this.ᜀ(A_0.ListFormat) + 1),
			A_0.ListFormat.CurrentListLevel.NumberSufix,
			ClipboardData.b("噵塷", a_)
		});
		return;
		IL_30A:
		this.ᜀ.Write(ClipboardData.b("屵塷", a_));
	}

	// Token: 0x06002D14 RID: 11540 RVA: 0x002B3B58 File Offset: 0x002B2B58
	protected void ᜃ()
	{
		int a_ = 2;
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
			if (this.ᜃ)
			{
				this.ᜁ += ClipboardData.b("敧恩", a_);
				return;
			}
			break;
		}
		this.ᜀ.WriteLine("");
	}

	// Token: 0x06002D15 RID: 11541 RVA: 0x002B3BD8 File Offset: 0x002B2BD8
	private void ᜁ()
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
			if (this.ᜄ.LastSection.HeadersFooters.Footer.ChildObjects.Count > 0)
			{
				this.ᜅ = (this.ᜄ.LastSection.HeadersFooters.Footer.LastParagraph as Paragraph);
				return;
			}
			break;
		}
		this.ᜅ = this.ᜄ.LastParagraph;
	}

	// Token: 0x06002D16 RID: 11542 RVA: 0x002B3C70 File Offset: 0x002B2C70
	private void ᜀ()
	{
		switch (0)
		{
		default:
			for (;;)
			{
				Section section = null;
				int num = this.ᜄ.Sections.Count - 1;
				this.ᜁ();
				int num2 = 0;
				int num3 = 0;
				for (;;)
				{
					bool flag;
					switch (num3)
					{
					case 0:
						goto IL_6B;
					case 1:
						return;
					case 2:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_92;
						default:
							if (false)
							{
							}
							goto IL_6B;
						}
						break;
					case 3:
						flag = false;
						goto IL_D1;
					case 4:
						if (num2 > num)
						{
							num3 = 1;
							continue;
						}
						goto IL_92;
					case 5:
						flag = true;
						goto IL_D1;
					case 6:
						if (num2 != num)
						{
							num3 = 7;
							continue;
						}
						num3 = 5;
						continue;
					case 7:
						num3 = 3;
						continue;
					}
					break;
					IL_6B:
					num3 = 4;
					continue;
					IL_92:
					section = this.ᜄ.Sections[num2];
					num3 = 6;
					continue;
					IL_D1:
					bool a_ = flag;
					this.ᜀ(this.ᜀ(section, this.ᜂ));
					this.ᜀ(section.Body);
					this.ᜀ(section, a_);
					this.ᜀ(this.ᜁ(section, this.ᜂ - 1));
					num2++;
					num3 = 2;
				}
			}
			return;
		}
	}

	// Token: 0x06002D17 RID: 11543 RVA: 0x002B3DC4 File Offset: 0x002B2DC4
	private IBody ᜁ(Section A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 4;
			for (;;)
			{
				HeaderFooterType headerFooterType;
				HeaderFooter headerFooter;
				HeaderFooterType headerFooterType2;
				switch (num)
				{
				case 0:
					goto IL_134;
				case 1:
					goto IL_E3;
				case 2:
				{
					int num2 = A_1 - 1;
					num = 10;
					continue;
				}
				case 3:
					goto IL_179;
				case 5:
					if (headerFooterType == headerFooter.Type)
					{
						num = 22;
						continue;
					}
					goto IL_E3;
				case 6:
				{
					int num2;
					num2--;
					num = 1;
					continue;
				}
				case 7:
				{
					int num2;
					if (num2 < 0)
					{
						num = 9;
						continue;
					}
					if (true)
					{
					}
					Section section = this.ᜄ.Sections[num2];
					headerFooter = section.HeadersFooters[headerFooterType];
					num = 0;
					continue;
				}
				case 8:
					headerFooterType2 = HeaderFooterType.FooterOdd;
					goto IL_17B;
				case 9:
					goto IL_151;
				case 10:
					goto IL_E3;
				case 11:
					if (A_1 > 0)
					{
						num = 2;
						continue;
					}
					goto IL_151;
				case 12:
					num = 18;
					continue;
				case 13:
					num = 11;
					continue;
				case 14:
					headerFooterType2 = HeaderFooterType.FooterFirstPage;
					goto IL_17B;
				case 15:
					if (A_0.PageSetup.DifferentFirstPageHeaderFooter)
					{
						num = 3;
						continue;
					}
					goto IL_29A;
				case 16:
					if (A_0.HeadersFooters[headerFooterType].LinkToPrevious)
					{
						num = 13;
						continue;
					}
					goto IL_151;
				case 17:
					if (headerFooter.LinkToPrevious)
					{
						goto IL_E3;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_134;
					default:
						if (false)
						{
						}
						num = 19;
						continue;
					}
					break;
				case 18:
					if (headerFooter.LinkToPrevious)
					{
						num = 6;
						continue;
					}
					goto IL_230;
				case 19:
				{
					Section section;
					A_0.HeadersFooters[headerFooterType] = section.HeadersFooters[headerFooterType];
					num = 21;
					continue;
				}
				case 20:
					num = 8;
					continue;
				case 21:
					goto IL_151;
				case 22:
					num = 17;
					continue;
				}
				if (!A_0.PageSetup.DifferentFirstPageHeaderFooter)
				{
					num = 20;
					continue;
				}
				num = 14;
				continue;
				IL_E3:
				num = 7;
				continue;
				IL_134:
				if (headerFooterType == headerFooter.Type)
				{
					num = 12;
					continue;
				}
				goto IL_230;
				IL_151:
				num = 15;
				continue;
				IL_17B:
				headerFooterType = headerFooterType2;
				num = 16;
				continue;
				IL_230:
				num = 5;
			}
			IL_179:
			return A_0.HeadersFooters.FirstPageFooter;
			IL_29A:
			return A_0.HeadersFooters.Footer;
		}
		}
	}

	// Token: 0x06002D18 RID: 11544 RVA: 0x002B4078 File Offset: 0x002B3078
	private IBody ᜀ(Section A_0, int A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 10;
			for (;;)
			{
				HeaderFooterType headerFooterType;
				HeaderFooter headerFooter;
				HeaderFooterType headerFooterType2;
				switch (num)
				{
				case 0:
					num = 14;
					continue;
				case 1:
					num = 11;
					continue;
				case 2:
					goto IL_149;
				case 3:
					goto IL_171;
				case 4:
				{
					int num2;
					num2--;
					num = 8;
					continue;
				}
				case 5:
				{
					Section section;
					A_0.HeadersFooters[headerFooterType] = section.HeadersFooters[headerFooterType];
					num = 2;
					continue;
				}
				case 6:
					if (headerFooterType == headerFooter.Type)
					{
						num = 0;
						continue;
					}
					goto IL_E3;
				case 7:
					headerFooterType2 = HeaderFooterType.HeaderOdd;
					goto IL_173;
				case 8:
					goto IL_E3;
				case 9:
					goto IL_149;
				case 11:
					if (A_1 > 0)
					{
						num = 15;
						continue;
					}
					goto IL_149;
				case 12:
					goto IL_E3;
				case 13:
					if (A_0.PageSetup.DifferentFirstPageHeaderFooter)
					{
						num = 3;
						continue;
					}
					goto IL_292;
				case 14:
					if (headerFooter.LinkToPrevious)
					{
						goto IL_E3;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_12C;
					default:
						if (false)
						{
						}
						num = 5;
						continue;
					}
					break;
				case 15:
				{
					int num2 = A_1 - 1;
					num = 12;
					continue;
				}
				case 16:
					if (A_0.HeadersFooters[headerFooterType].LinkToPrevious)
					{
						num = 1;
						continue;
					}
					goto IL_149;
				case 17:
					goto IL_12C;
				case 18:
				{
					int num2;
					if (num2 < 0)
					{
						num = 9;
						continue;
					}
					Section section = this.ᜄ.Sections[num2];
					headerFooter = section.HeadersFooters[headerFooterType];
					num = 17;
					continue;
				}
				case 19:
					num = 20;
					continue;
				case 20:
					if (headerFooter.LinkToPrevious)
					{
						num = 4;
						continue;
					}
					goto IL_228;
				case 21:
					num = 7;
					continue;
				case 22:
					headerFooterType2 = HeaderFooterType.HeaderFirstPage;
					goto IL_173;
				}
				if (!A_0.PageSetup.DifferentFirstPageHeaderFooter)
				{
					num = 21;
					continue;
				}
				num = 22;
				continue;
				IL_E3:
				num = 18;
				continue;
				IL_12C:
				if (headerFooterType == headerFooter.Type)
				{
					num = 19;
					continue;
				}
				goto IL_228;
				IL_149:
				num = 13;
				continue;
				IL_173:
				headerFooterType = headerFooterType2;
				num = 16;
				continue;
				IL_228:
				num = 6;
			}
			IL_171:
			return A_0.HeadersFooters.FirstPageHeader;
			IL_292:
			if (true)
			{
			}
			return A_0.HeadersFooters.Header;
		}
		}
	}

	// Token: 0x06002D19 RID: 11545 RVA: 0x002B432C File Offset: 0x002B332C
	private int ᜀ(ListFormat A_0)
	{
		switch (0)
		{
		default:
		{
			HybridDictionary hybridDictionary;
			for (;;)
			{
				hybridDictionary = (this.ᜂ()[A_0.CustomStyleName] as HybridDictionary);
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_81;
					case 1:
						goto IL_E2;
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
							if (hybridDictionary == null)
							{
								num = 0;
								continue;
							}
							num = 3;
							continue;
						}
						break;
					case 3:
						if (hybridDictionary[A_0.ListLevelNumber] != null)
						{
							num = 1;
							continue;
						}
						goto IL_13A;
					}
					break;
				}
			}
			IL_81:
			HybridDictionary hybridDictionary2 = new HybridDictionary();
			this.ᜂ().Add(A_0.CustomStyleName, hybridDictionary2);
			ListLevel listLevel = A_0.CurrentListStyle.Levels[A_0.ListLevelNumber];
			hybridDictionary2.Add(A_0.ListLevelNumber, listLevel.StartAt + 1);
			return listLevel.StartAt - 1;
			IL_E2:
			int num2 = (int)hybridDictionary[A_0.ListLevelNumber];
			hybridDictionary[A_0.ListLevelNumber] = num2 + 1;
			return num2 - 1;
			IL_13A:
			ListLevel listLevel2 = A_0.CurrentListStyle.Levels[A_0.ListLevelNumber];
			hybridDictionary.Add(A_0.ListLevelNumber, listLevel2.StartAt + 1);
			return listLevel2.StartAt - 1;
		}
		}
	}

	// Token: 0x06002D1A RID: 11546 RVA: 0x002B44B4 File Offset: 0x002B34B4
	private void ᜀ(ListFormat A_0, bool A_1)
	{
		switch (0)
		{
		default:
		{
			int num = 8;
			for (;;)
			{
				int num2;
				int[] array;
				HybridDictionary hybridDictionary;
				switch (num)
				{
				case 0:
					return;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F5;
					default:
					{
						if (false)
						{
						}
						int count;
						if (num2 >= count)
						{
							num = 5;
							continue;
						}
						num = 2;
						continue;
					}
					}
					break;
				case 2:
					if (!A_1)
					{
						num = 15;
						continue;
					}
					goto IL_1E2;
				case 3:
					if (!A_0.CurrentListStyle.Levels[array[num2]].NoRestartByHigher)
					{
						num = 18;
						continue;
					}
					goto IL_CA;
				case 4:
					goto IL_143;
				case 5:
					goto IL_17F;
				case 6:
					goto IL_18C;
				case 7:
					goto IL_143;
				case 9:
				{
					IEnumerator enumerator;
					if (!enumerator.MoveNext())
					{
						num = 16;
						continue;
					}
					int num3;
					array[num3] = (int)enumerator.Current;
					num3++;
					num = 6;
					continue;
				}
				case 10:
				{
					if (hybridDictionary == null)
					{
						num = 17;
						continue;
					}
					ICollection keys = hybridDictionary.Keys;
					IEnumerator enumerator = keys.GetEnumerator();
					int count = keys.Count;
					array = new int[count];
					int num3 = 0;
					num = 11;
					continue;
				}
				case 11:
					goto IL_18C;
				case 12:
					goto IL_CA;
				case 13:
					if (array[num2] != 0)
					{
						num = 14;
						continue;
					}
					goto IL_CA;
				case 14:
					num = 3;
					continue;
				case 15:
					num = 13;
					continue;
				case 16:
					num2 = 0;
					num = 4;
					continue;
				case 17:
					return;
				case 18:
					goto IL_1E2;
				}
				if (this.ᜆ == null)
				{
					num = 0;
					continue;
				}
				hybridDictionary = (this.ᜂ()[A_0.CustomStyleName] as HybridDictionary);
				goto IL_F5;
				IL_CA:
				num2++;
				num = 7;
				continue;
				IL_F5:
				num = 10;
				continue;
				IL_143:
				num = 1;
				continue;
				IL_18C:
				num = 9;
				continue;
				IL_1E2:
				hybridDictionary[array[num2]] = A_0.CurrentListStyle.Levels[array[num2]].StartAt;
				num = 12;
			}
			return;
			IL_17F:
			if (true)
			{
			}
			return;
		}
		}
	}

	// Token: 0x04002669 RID: 9833
	private TextWriter ᜀ;

	// Token: 0x0400266A RID: 9834
	private string ᜁ = "";

	// Token: 0x0400266B RID: 9835
	private int ᜂ;

	// Token: 0x0400266C RID: 9836
	private bool ᜃ;

	// Token: 0x0400266D RID: 9837
	private Document ᜄ;

	// Token: 0x0400266E RID: 9838
	private Paragraph ᜅ;

	// Token: 0x0400266F RID: 9839
	private HybridDictionary ᜆ;
}
