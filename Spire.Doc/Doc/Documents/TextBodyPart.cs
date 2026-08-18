using System;
using System.Collections;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x020004F4 RID: 1268
	public class TextBodyPart
	{
		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x060041F6 RID: 16886 RVA: 0x003E4A3C File Offset: 0x003E3A3C
		public BodyRegionCollection BodyItems
		{
			get
			{
				if (this.ᜀ == null)
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
					return null;
				}
				return this.ᜀ.Items;
			}
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x003E4A90 File Offset: 0x003E3A90
		public TextBodyPart()
		{
		}

		// Token: 0x060041F8 RID: 16888 RVA: 0x003E4AA4 File Offset: 0x003E3AA4
		public TextBodyPart(TextBodySelection textBodySelection)
		{
			this.Copy(textBodySelection);
		}

		// Token: 0x060041F9 RID: 16889 RVA: 0x003E4AC0 File Offset: 0x003E3AC0
		public TextBodyPart(TextSelection textSelection)
		{
			this.Copy(textSelection);
		}

		// Token: 0x060041FA RID: 16890 RVA: 0x003E4ADC File Offset: 0x003E3ADC
		public TextBodyPart(Document doc)
		{
			this.ᜀ(doc);
		}

		// Token: 0x060041FB RID: 16891 RVA: 0x003E4AF8 File Offset: 0x003E3AF8
		public void Clear()
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
			this.ᜀ.Items.Clear();
		}

		// Token: 0x060041FC RID: 16892 RVA: 0x003E4B44 File Offset: 0x003E3B44
		public void Copy(TextSelection textSel)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜀ(textSel.OwnerParagraph.Document);
					TextRange[] ranges = textSel.GetRanges();
					Paragraph paragraph = new Paragraph(this.ᜀ.Document);
					this.ᜀ.Items.Add(paragraph);
					int num = 0;
					int num2 = ranges.Length;
					int num3 = 0;
					for (;;)
					{
						switch (num3)
						{
						case 0:
							goto IL_71;
						case 1:
							if (num >= num2)
							{
								num3 = 3;
								continue;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_71;
							default:
								if (false)
								{
								}
								if (true)
								{
								}
								paragraph.Items.Add(ranges[num].Clone());
								num++;
								num3 = 2;
								continue;
							}
							break;
						case 2:
							goto IL_73;
						case 3:
							return;
						}
						break;
						IL_73:
						num3 = 1;
						continue;
						IL_71:
						goto IL_73;
					}
				}
				return;
			}
		}

		// Token: 0x060041FD RID: 16893 RVA: 0x003E4C34 File Offset: 0x003E3C34
		public void Copy(TextBodySelection textSel)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜀ(textSel.TextBody.Document);
					int itemStartIndex = textSel.ItemStartIndex;
					int itemEndIndex = textSel.ItemEndIndex;
					int num = itemStartIndex;
					int num2 = 18;
					for (;;)
					{
						BodyRegion bodyRegion;
						switch (num2)
						{
						case 0:
							if (num == itemEndIndex)
							{
								num2 = 16;
								continue;
							}
							goto IL_230;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_19A;
							default:
								if (false)
								{
								}
								if (bodyRegion.DocumentObjectType == DocumentObjectType.Paragraph)
								{
									num2 = 7;
									continue;
								}
								goto IL_13D;
							}
							break;
						case 2:
							goto IL_20B;
						case 3:
							if (num == itemStartIndex)
							{
								num2 = 14;
								continue;
							}
							goto IL_13D;
						case 4:
							if (num != itemStartIndex)
							{
								num2 = 20;
								continue;
							}
							goto IL_FB;
						case 5:
							goto IL_19A;
						case 6:
							goto IL_CB;
						case 7:
						{
							Paragraph paragraph = bodyRegion as Paragraph;
							num2 = 0;
							continue;
						}
						case 8:
							goto IL_FB;
						case 9:
							goto IL_CB;
						case 10:
							return;
						case 11:
							goto IL_230;
						case 12:
						{
							int num3;
							if (num3 < 0)
							{
								num2 = 19;
								continue;
							}
							Paragraph paragraph;
							paragraph.Items.InnerList.RemoveAt(num3);
							num3--;
							num2 = 15;
							continue;
						}
						case 13:
							if (num == itemEndIndex)
							{
								num2 = 8;
								continue;
							}
							goto IL_13D;
						case 14:
						{
							if (true)
							{
							}
							int num3 = textSel.ParagraphItemStartIndex - 1;
							num2 = 2;
							continue;
						}
						case 15:
							goto IL_20B;
						case 16:
						{
							int num4 = textSel.ParagraphItemEndIndex + 1;
							num2 = 6;
							continue;
						}
						case 17:
							if (num > itemEndIndex)
							{
								num2 = 10;
								continue;
							}
							bodyRegion = (BodyRegion)textSel.TextBody.Items[num].Clone();
							num2 = 4;
							continue;
						case 18:
							goto IL_19A;
						case 19:
							goto IL_13D;
						case 20:
							num2 = 13;
							continue;
						case 21:
						{
							Paragraph paragraph;
							int num4;
							if (num4 >= paragraph.Items.Count)
							{
								num2 = 11;
								continue;
							}
							paragraph.Items.InnerList.RemoveAt(num4);
							num2 = 9;
							continue;
						}
						}
						break;
						IL_CB:
						num2 = 21;
						continue;
						IL_FB:
						num2 = 1;
						continue;
						IL_13D:
						this.ᜀ.Items.Add(bodyRegion);
						num++;
						num2 = 5;
						continue;
						IL_19A:
						num2 = 17;
						continue;
						IL_20B:
						num2 = 12;
						continue;
						IL_230:
						num2 = 3;
					}
				}
				return;
			}
		}

		// Token: 0x060041FE RID: 16894 RVA: 0x003E4F04 File Offset: 0x003E3F04
		public void Copy(BodyRegion bodyItem, bool clone)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_4D:
				if (true)
				{
				}
				bodyItem = (BodyRegion)bodyItem.Clone();
				num = 1;
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
					goto IL_4D;
				case 1:
					goto IL_6C;
				}
				if (!clone)
				{
					break;
				}
				num = 0;
			}
			IL_6C:
			this.ᜀ(bodyItem.Document);
			this.ᜀ.Items.Add(bodyItem);
		}

		// Token: 0x060041FF RID: 16895 RVA: 0x003E4FA0 File Offset: 0x003E3FA0
		public void Copy(ParagraphBase pItem, bool clone)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_55:
				pItem = (ParagraphBase)pItem.Clone();
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
				switch (num)
				{
				case 0:
					goto IL_55;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_6C;
				}
				if (!clone)
				{
					break;
				}
				num = 0;
			}
			IL_6C:
			this.ᜀ(pItem.Document);
			this.ᜀ.AddParagraph().Items.Add(pItem);
		}

		// Token: 0x06004200 RID: 16896 RVA: 0x003E5040 File Offset: 0x003E4040
		internal void ᜀ(Body A_0, bool A_1)
		{
			for (;;)
			{
				this.ᜀ(A_0.Document);
				if (A_1)
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
					break;
				}
				goto IL_51;
			}
			if (false)
			{
			}
			this.ᜀ = (Body)A_0.Clone();
			return;
			IL_51:
			this.ᜀ = A_0;
		}

		// Token: 0x06004201 RID: 16897 RVA: 0x003E50A8 File Offset: 0x003E40A8
		public void PasteAfter(BodyRegion bodyItem)
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
			int num = bodyItem.ឯ();
			this.PasteAt(bodyItem.OwnerTextBody, num + 1);
		}

		// Token: 0x06004202 RID: 16898 RVA: 0x003E50FC File Offset: 0x003E40FC
		public void PasteAfter(ParagraphBase paragraphItem)
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
			BodyRegion bodyRegion = paragraphItem.Owner as BodyRegion;
			int itemIndex = bodyRegion.ឯ();
			int num = paragraphItem.ឯ();
			this.PasteAt(bodyRegion.OwnerTextBody, itemIndex, num + 1);
		}

		// Token: 0x06004203 RID: 16899 RVA: 0x003E5164 File Offset: 0x003E4164
		public void PasteAt(IBody textBody, int itemIndex)
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
			this.PasteAt(textBody, itemIndex, 0);
		}

		// Token: 0x06004204 RID: 16900 RVA: 0x003E51A8 File Offset: 0x003E41A8
		internal void ᜀ(IBody A_0, int A_1, int A_2, CharacterFormat A_3, bool A_4)
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
			this.ᜄ = A_3;
			this.ᜅ = A_4;
			this.PasteAt(A_0, A_1, A_2);
		}

		// Token: 0x06004205 RID: 16901 RVA: 0x003E51FC File Offset: 0x003E41FC
		public void PasteAt(IBody textBody, int itemIndex, int pItemIndex)
		{
			switch (0)
			{
			default:
			{
				int num = 46;
				Paragraph paragraph;
				Document document;
				string name;
				for (;;)
				{
					Paragraph paragraph2;
					int num2;
					Paragraph paragraph3;
					Paragraph paragraph4;
					int num3;
					int index;
					int num4;
					DocumentObject documentObject;
					int num5;
					DocumentObject documentObject2;
					DocumentObject documentObject3;
					bool flag;
					int num6;
					int num7;
					int num8;
					bool flag2;
					Paragraph paragraph6;
					int num9;
					switch (num)
					{
					case 0:
						goto IL_3C5;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_215;
						default:
							if (false)
							{
							}
							if (paragraph == null)
							{
								num = 6;
								continue;
							}
							goto IL_B9A;
						}
						break;
					case 2:
						goto IL_B9A;
					case 3:
						if (paragraph2 != null)
						{
							num = 9;
							continue;
						}
						goto IL_57E;
					case 4:
						num = 64;
						continue;
					case 5:
						if (this.ᜀ.Items.Count == 1)
						{
							num = 72;
							continue;
						}
						paragraph = (this.ᜁ.Items[itemIndex + this.ᜀ.Items.Count - 2] as Paragraph);
						num = 1;
						continue;
					case 6:
						paragraph = new Paragraph(document);
						this.ᜁ.Items.Add(paragraph);
						num = 2;
						continue;
					case 7:
						return;
					case 8:
						num = 98;
						continue;
					case 9:
						num2--;
						num = 58;
						continue;
					case 10:
						if ((this.ᜁ.Items[itemIndex - 1] as Paragraph).Items.Count == 1)
						{
							goto IL_215;
						}
						return;
					case 11:
						goto IL_C8C;
					case 12:
						num = 10;
						continue;
					case 13:
						if (paragraph3.ListFormat.ListType != ListType.NoList)
						{
							num = 47;
							continue;
						}
						goto IL_3C5;
					case 14:
						num = 74;
						continue;
					case 15:
						paragraph4.ᜀ(paragraph3.ParaStyle);
						paragraph4.BreakCharacterFormat.ImportContainer(paragraph3.BreakCharacterFormat);
						num = 71;
						continue;
					case 16:
						num = 97;
						continue;
					case 17:
					{
						num3 = paragraph4.Items.Count - this.ᜃ;
						index = paragraph4.Items.Count - num3;
						num4 = 0;
						int count = paragraph3.Items.Count;
						num = 95;
						continue;
					}
					case 18:
						this.ᜀ(documentObject as Paragraph);
						num = 26;
						continue;
					case 19:
						num = 13;
						continue;
					case 20:
						if (documentObject is Paragraph)
						{
							num = 18;
							continue;
						}
						goto IL_838;
					case 21:
						num = 82;
						continue;
					case 22:
					{
						Table table;
						if (table.FirstRow.Cells[0].Items.Count == 0)
						{
							num = 87;
							continue;
						}
						goto IL_C8C;
					}
					case 23:
						if ((this.ᜁ.Items[itemIndex - 1] as Paragraph).Items[0] is BookmarkStart)
						{
							num = 35;
							continue;
						}
						return;
					case 24:
						if (this.ᜄ != null)
						{
							num = 59;
							continue;
						}
						goto IL_759;
					case 25:
						if (num2 <= 0)
						{
							num = 89;
							continue;
						}
						num = 60;
						continue;
					case 26:
						goto IL_838;
					case 27:
						goto IL_909;
					case 28:
						if (this.ᜀ.Items[0] is Table)
						{
							num = 92;
							continue;
						}
						return;
					case 29:
					{
						int count2;
						if (num5 >= count2)
						{
							num = 56;
							continue;
						}
						Paragraph paragraph5;
						documentObject2 = paragraph5.Items[num5].Clone();
						num = 73;
						continue;
					}
					case 30:
						goto IL_699;
					case 31:
						goto IL_3C5;
					case 32:
						num = 42;
						continue;
					case 33:
					{
						int count;
						if (num4 >= count)
						{
							num = 57;
							continue;
						}
						documentObject3 = paragraph3.Items[num4].Clone();
						num = 69;
						continue;
					}
					case 34:
						num = 48;
						continue;
					case 35:
						num = 28;
						continue;
					case 36:
						if (itemIndex >= this.ᜁ.Items.Count)
						{
							num = 14;
							continue;
						}
						num = 93;
						continue;
					case 37:
						if (paragraph3.ListFormat.ListType == ListType.NoList)
						{
							num = 17;
							continue;
						}
						goto IL_A41;
					case 38:
						return;
					case 39:
						goto IL_4AE;
					case 40:
						if (flag)
						{
							num = 86;
							continue;
						}
						goto IL_57E;
					case 41:
						goto IL_5D5;
					case 42:
					{
						Table table;
						if (table.FirstRow.Cells.Count > 0)
						{
							num = 49;
							continue;
						}
						goto IL_909;
					}
					case 43:
					{
						Table table;
						if (table.FirstRow != null)
						{
							num = 32;
							continue;
						}
						goto IL_909;
					}
					case 44:
						if (paragraph3 != null)
						{
							num = 67;
							continue;
						}
						goto IL_3C5;
					case 45:
					{
						Table table;
						if (table.FirstRow.Cells[0].Items[0] is Paragraph)
						{
							num = 75;
							continue;
						}
						goto IL_909;
					}
					case 47:
					{
						if (true)
						{
						}
						Paragraph paragraph5 = paragraph4.Clone() as Paragraph;
						paragraph4 = (paragraph3.Clone() as Paragraph);
						num6 = 0;
						num5 = 0;
						int count2 = paragraph5.Items.Count;
						num = 39;
						continue;
					}
					case 48:
						if (this.ᜁ.Items[itemIndex - 1] is Paragraph)
						{
							num = 12;
							continue;
						}
						return;
					case 49:
						num = 22;
						continue;
					case 50:
						num = 23;
						continue;
					case 51:
						num = 68;
						continue;
					case 52:
						num = 96;
						continue;
					case 53:
						num = 24;
						continue;
					case 54:
						goto IL_4AE;
					case 55:
						if (num7 > num8)
						{
							num = 94;
							continue;
						}
						documentObject = this.ᜀ.Items[num7].Clone();
						num = 20;
						continue;
					case 56:
						num = 78;
						continue;
					case 57:
						num = 88;
						continue;
					case 58:
						goto IL_57E;
					case 59:
						(documentObject3 as TextRange).CharacterFormat.ImportContainer(this.ᜄ);
						num = 80;
						continue;
					case 60:
						flag2 = true;
						goto IL_C38;
					case 61:
						if (paragraph4 != null)
						{
							num = 90;
							continue;
						}
						goto IL_57E;
					case 62:
						(documentObject2 as TextRange).CharacterFormat.ImportContainer(this.ᜄ);
						num = 100;
						continue;
					case 63:
						if (!string.IsNullOrEmpty(paragraph3.StyleName))
						{
							num = 66;
							continue;
						}
						goto IL_5D5;
					case 64:
						if (!string.IsNullOrEmpty(paragraph3.StyleName))
						{
							num = 15;
							continue;
						}
						goto IL_4F7;
					case 65:
						if (this.ᜃ >= 0)
						{
							num = 76;
							continue;
						}
						goto IL_57E;
					case 66:
						paragraph4.ᜀ(paragraph3.ParaStyle);
						paragraph4.BreakCharacterFormat.ImportContainer(paragraph3.BreakCharacterFormat);
						num = 41;
						continue;
					case 67:
						num = 101;
						continue;
					case 68:
						if (this.ᜅ)
						{
							num = 53;
							continue;
						}
						goto IL_759;
					case 69:
						if (documentObject3 is TextRange)
						{
							num = 51;
							continue;
						}
						goto IL_759;
					case 70:
						if (itemIndex > 0)
						{
							num = 34;
							continue;
						}
						return;
					case 71:
						goto IL_4F7;
					case 72:
						goto IL_937;
					case 73:
						if (documentObject2 is TextRange)
						{
							num = 8;
							continue;
						}
						goto IL_865;
					case 74:
						paragraph6 = null;
						goto IL_BF9;
					case 75:
					{
						Table table;
						(table.FirstRow.Cells[0].Items[0] as Paragraph).Items.Insert(0, new BookmarkStart(document, name));
						num = 27;
						continue;
					}
					case 76:
						this.ᜀ(paragraph4, paragraph2);
						num9 = 1;
						num = 3;
						continue;
					case 77:
						num = 99;
						continue;
					case 78:
						if (paragraph3.Items.Count == 1)
						{
							num = 16;
							continue;
						}
						goto IL_5D5;
					case 79:
						num = 63;
						continue;
					case 80:
						goto IL_759;
					case 81:
						goto IL_413;
					case 82:
						if (!this.ᜅ)
						{
							num = 4;
							continue;
						}
						goto IL_4F7;
					case 83:
						if (paragraph3 != null)
						{
							num = 52;
							continue;
						}
						goto IL_A41;
					case 84:
						goto IL_699;
					case 85:
						num = 37;
						continue;
					case 86:
						num = 61;
						continue;
					case 87:
					{
						Table table;
						table.FirstRow.Cells[0].Items.Add(new Paragraph(document));
						num = 11;
						continue;
					}
					case 88:
						if (paragraph3.Items.Count == 1)
						{
							num = 21;
							continue;
						}
						goto IL_4F7;
					case 89:
						num = 91;
						continue;
					case 90:
						num = 65;
						continue;
					case 91:
						flag2 = (paragraph3 == null);
						goto IL_C38;
					case 92:
					{
						Paragraph paragraph7 = this.ᜁ.Items[itemIndex - 1] as Paragraph;
						Table table = this.ᜁ.Items[itemIndex] as Table;
						name = (paragraph7.Items[0].Clone() as BookmarkStart).Name;
						document = this.ᜁ.Document;
						document.Bookmarks.Remove(document.Bookmarks[name]);
						paragraph7.RemoveSelf();
						num = 43;
						continue;
					}
					case 93:
						paragraph6 = (this.ᜁ.Items[itemIndex] as Paragraph);
						goto IL_BF9;
					case 94:
						num = 70;
						continue;
					case 95:
						goto IL_413;
					case 96:
						if (paragraph4 != null)
						{
							num = 85;
							continue;
						}
						goto IL_A41;
					case 97:
						if (!this.ᜅ)
						{
							num = 79;
							continue;
						}
						goto IL_5D5;
					case 98:
						if (this.ᜅ)
						{
							num = 77;
							continue;
						}
						goto IL_865;
					case 99:
						if (this.ᜄ != null)
						{
							num = 62;
							continue;
						}
						goto IL_865;
					case 100:
						goto IL_865;
					case 101:
						if (paragraph4 != null)
						{
							num = 19;
							continue;
						}
						goto IL_3C5;
					}
					if (this.ᜀ.Items.Count == 0)
					{
						num = 7;
						continue;
					}
					this.ᜁ = (textBody as Body);
					this.ᜂ = itemIndex;
					this.ᜃ = pItemIndex;
					this.ᜀ();
					paragraph3 = (this.ᜀ.Items[0] as Paragraph);
					paragraph2 = (this.ᜀ.Items[this.ᜀ.Count - 1] as Paragraph);
					num = 36;
					continue;
					IL_215:
					num = 50;
					continue;
					IL_3C5:
					int num10;
					itemIndex += num9 - num10;
					num7 = num10;
					num8 = num2;
					num = 84;
					continue;
					IL_413:
					num = 33;
					continue;
					IL_4AE:
					num = 29;
					continue;
					IL_4F7:
					num10 = 1;
					num9 = 1;
					num = 0;
					continue;
					IL_57E:
					num = 83;
					continue;
					IL_5D5:
					this.ᜁ.Items.RemoveAt(itemIndex);
					this.ᜀ(paragraph4);
					this.ᜁ.Items.Insert(itemIndex, paragraph4);
					num10 = 1;
					num9 = 1;
					num = 31;
					continue;
					IL_699:
					num = 55;
					continue;
					IL_759:
					paragraph4.Items.Insert(index, documentObject3);
					index = paragraph4.Items.Count - num3;
					num4++;
					num = 81;
					continue;
					IL_838:
					this.ᜁ.Items.Insert(itemIndex + num7, documentObject);
					num7++;
					num = 30;
					continue;
					IL_865:
					paragraph4.Items.Insert(num6, documentObject2);
					num6++;
					num5++;
					num = 54;
					continue;
					IL_909:
					num = 5;
					continue;
					IL_A41:
					num = 44;
					continue;
					IL_B9A:
					paragraph.Items.Add(new BookmarkEnd(document, name));
					num = 38;
					continue;
					IL_BF9:
					paragraph4 = paragraph6;
					num9 = 0;
					num10 = 0;
					num2 = this.ᜀ.Items.Count - 1;
					num = 25;
					continue;
					IL_C38:
					flag = flag2;
					num = 40;
					continue;
					IL_C8C:
					num = 45;
				}
				return;
				IL_937:
				paragraph = (this.ᜁ.Items[itemIndex] as Paragraph);
				paragraph.Items.Insert(0, new BookmarkEnd(document, name));
				return;
			}
			}
		}

		// Token: 0x06004206 RID: 16902 RVA: 0x003E6008 File Offset: 0x003E5008
		public void PasteAtEnd(IBody textBody)
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
			this.PasteAt(textBody, ((Body)textBody).Items.Count);
		}

		// Token: 0x06004207 RID: 16903 RVA: 0x003E605C File Offset: 0x003E505C
		internal static void ᜀ(Paragraph A_0, int A_1, Paragraph A_2)
		{
			for (;;)
			{
				for (;;)
				{
					int num = A_0.ឯ();
					A_0.OwnerTextBody.Items.Insert(num + 1, A_2);
					int num2 = 0;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							goto IL_45;
						case 1:
							return;
						case 2:
							goto IL_45;
						case 3:
							if (A_0.Items.Count > A_1)
							{
								A_2.Items.Add(A_0.Items[A_1]);
								num2 = 2;
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
								num2 = 1;
								continue;
							}
							break;
						}
						break;
						IL_45:
						num2 = 3;
					}
				}
			}
		}

		// Token: 0x06004208 RID: 16904 RVA: 0x003E611C File Offset: 0x003E511C
		private void ᜀ()
		{
			int a_ = 16;
			int num = 1;
			Paragraph paragraph;
			for (;;)
			{
				BodyRegion bodyRegion;
				switch (num)
				{
				case 0:
					goto IL_6C;
				case 2:
					num = 11;
					continue;
				case 3:
					if (this.ᜂ >= 0)
					{
						num = 2;
						continue;
					}
					goto IL_C0;
				case 4:
					bodyRegion = this.ᜁ.Items[this.ᜂ];
					goto IL_1E8;
				case 5:
					if (this.ᜃ > paragraph.Items.Count)
					{
						num = 8;
						continue;
					}
					return;
				case 6:
					num = 9;
					continue;
				case 7:
					num = 5;
					continue;
				case 8:
					goto IL_1C3;
				case 9:
					bodyRegion = null;
					goto IL_1E8;
				case 10:
					goto IL_BE;
				case 11:
					if (this.ᜂ > this.ᜁ.Items.Count)
					{
						num = 10;
						continue;
					}
					num = 13;
					continue;
				case 12:
					if (paragraph != null)
					{
						num = 14;
						continue;
					}
					return;
				case 13:
					if (this.ᜁ.Items.Count <= this.ᜂ)
					{
						if (true)
						{
						}
						num = 6;
						continue;
					}
					num = 4;
					continue;
				case 14:
					num = 15;
					continue;
				case 15:
					if (this.ᜃ >= 0)
					{
						num = 7;
						continue;
					}
					goto IL_156;
				}
				if (this.ᜁ == null)
				{
					num = 0;
					continue;
				}
				num = 3;
				continue;
				IL_1E8:
				BodyRegion bodyRegion2 = bodyRegion;
				paragraph = (bodyRegion2 as Paragraph);
				num = 12;
			}
			IL_6C:
			IL_7C:
			throw new ArgumentNullException(ClipboardData.b("ɵᵷɹࡻ㱽ﶃ", a_));
			IL_BE:
			IL_C0:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_7C;
			default:
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(ClipboardData.b("ή౷όᅻ㝽ﺅ", a_), ClipboardData.b("ή౷όᅻ㝽ﺅꢇﾋ꺍ﲏ뢗ﾝ캟芡钣蚥잧\ud8a9貫즭슯ힱ햳습\uddb7좹鲻쪽ꢿꏁ꫃", a_) + this.ᜁ.Items.Count);
			}
			IL_156:
			throw new ArgumentOutOfRangeException(ClipboardData.b("ٵㅷ๹᥻፽쥿", a_), ClipboardData.b("ٵㅷ๹᥻፽쥿ꪉﶍ낏ﺑ몙솟첡蒣隥袧얩\udeab躭힯삱톳ힵ첷\udfb9캻麽뒿꫁ꗃꣅ", a_) + paragraph.Items.Count);
			IL_1C3:
			goto IL_156;
		}

		// Token: 0x06004209 RID: 16905 RVA: 0x003E6384 File Offset: 0x003E5384
		private Paragraph ᜀ(Paragraph A_0, Paragraph A_1)
		{
			int num = 4;
			Paragraph paragraph;
			for (;;)
			{
				if (true)
				{
				}
				switch (num)
				{
				case 0:
					if (A_0.Items.Count <= this.ᜃ)
					{
						num = 8;
						continue;
					}
					paragraph.Items.Add(A_0.Items[this.ᜃ]);
					num = 5;
					continue;
				case 1:
					goto IL_58;
				case 2:
					if (A_0.ParaStyle != null)
					{
						num = 9;
						continue;
					}
					goto IL_58;
				case 3:
					num = 2;
					continue;
				case 5:
					goto IL_C6;
				case 6:
					paragraph = (Paragraph)A_1.Clone();
					this.ᜀ(paragraph);
					num = 10;
					continue;
				case 7:
					goto IL_58;
				case 8:
					return paragraph;
				case 9:
					paragraph.ParaStyle.ᜁ(A_0.ParaStyle.Name);
					num = 7;
					continue;
				case 10:
					if (paragraph.ParaStyle != null)
					{
						num = 3;
						continue;
					}
					goto IL_58;
				case 11:
					goto IL_C6;
				}
				if (A_1 != null)
				{
					num = 6;
					continue;
				}
				goto IL_85;
				IL_58:
				this.ᜁ.Items.Insert(this.ᜂ + 1, paragraph);
				num = 11;
				continue;
				IL_85:
				paragraph = new Paragraph(this.ᜁ.Document);
				num = 1;
				continue;
				IL_C6:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_85;
				default:
					if (false)
					{
					}
					num = 0;
					break;
				}
			}
			return paragraph;
		}

		// Token: 0x0600420A RID: 16906 RVA: 0x003E6530 File Offset: 0x003E5530
		private void ᜀ(Document A_0)
		{
			int num;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				IL_67:
				if (this.ᜀ.Document != A_0)
				{
					goto IL_87;
				}
				if (true)
				{
				}
				num = 3;
				break;
			case 1:
				goto IL_20;
			default:
				goto IL_20;
			}
			for (;;)
			{
				IL_30:
				switch (num)
				{
				case 1:
					num = 2;
					continue;
				case 2:
					goto IL_67;
				case 3:
					goto IL_85;
				}
				if (this.ᜀ == null)
				{
					goto IL_87;
				}
				num = 1;
			}
			IL_85:
			this.Clear();
			return;
			IL_20:
			if (false)
			{
			}
			num = 0;
			goto IL_30;
			IL_87:
			this.ᜀ = new Body(A_0, null);
		}

		// Token: 0x0600420B RID: 16907 RVA: 0x003E65D4 File Offset: 0x003E55D4
		private void ᜀ(Paragraph A_0)
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
				{
					if (A_0 == null)
					{
						num = 6;
						continue;
					}
					IEnumerator enumerator = A_0.Items.GetEnumerator();
					num = 5;
					continue;
				}
				case 1:
					num = 0;
					continue;
				case 3:
					num = 4;
					continue;
				case 4:
					if (this.ᜄ == null)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_37;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				case 5:
					goto IL_19F;
				case 6:
					goto IL_186;
				}
				goto IL_2C;
				IL_37:
				num = 3;
				continue;
				IL_2C:
				if (this.ᜅ)
				{
					goto IL_37;
				}
				break;
			}
			return;
			IL_186:
			return;
			IL_19F:
			try
			{
				num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D8;
					case 1:
						num = 0;
						continue;
					case 2:
					{
						ParagraphBase paragraphBase;
						paragraphBase.ParaItemCharFormat.ImportContainer(this.ᜄ);
						num = 5;
						continue;
					}
					case 3:
					{
						IEnumerator enumerator;
						if (!enumerator.MoveNext())
						{
							num = 1;
							continue;
						}
						ParagraphBase paragraphBase = (ParagraphBase)enumerator.Current;
						num = 4;
						continue;
					}
					case 4:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase is TextRange)
						{
							num = 2;
							continue;
						}
						break;
					}
					}
					IL_72:
					num = 3;
					continue;
					goto IL_72;
				}
				IL_D8:
				return;
			}
			finally
			{
				for (;;)
				{
					IEnumerator enumerator;
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
							goto IL_11D;
						case 1:
							goto IL_11B;
						case 2:
							disposable.Dispose();
							num = 1;
							continue;
						}
						break;
					}
				}
				IL_11B:
				IL_11D:
				if (true)
				{
				}
			}
		}

		// Token: 0x040033A9 RID: 13225
		private byte \u2593\u009D\u0082\u009B;

		// Token: 0x040033AA RID: 13226
		private string[] \u2460\u00A0\u008A\u009D;

		// Token: 0x040033AB RID: 13227
		private Body ᜀ;

		// Token: 0x040033AC RID: 13228
		private Body ᜁ;

		// Token: 0x040033AD RID: 13229
		private int \u2609\u0085\u009Bª;

		// Token: 0x040033AE RID: 13230
		private int ᜂ;

		// Token: 0x040033AF RID: 13231
		private int ᜃ;

		// Token: 0x040033B0 RID: 13232
		private CharacterFormat ᜄ;

		// Token: 0x040033B1 RID: 13233
		private int \u25D8\u00AD\u0084\u0095;

		// Token: 0x040033B2 RID: 13234
		private bool ᜅ;
	}
}
