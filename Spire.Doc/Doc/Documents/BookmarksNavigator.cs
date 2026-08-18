using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x020004F3 RID: 1267
	public class BookmarksNavigator
	{
		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x060041E1 RID: 16865 RVA: 0x003E3654 File Offset: 0x003E2654
		// (set) Token: 0x060041E2 RID: 16866 RVA: 0x003E3698 File Offset: 0x003E2698
		public IDocument Document
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
				return this.ᜆ;
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
				this.ᜆ = (Document)value;
			}
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x060041E3 RID: 16867 RVA: 0x003E36E0 File Offset: 0x003E26E0
		public Bookmark CurrentBookmark
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
				return this.ᜉ;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x060041E4 RID: 16868 RVA: 0x003E3724 File Offset: 0x003E2724
		private IParagraphBase CurrentParagraphItem
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						if (this.ᜇ >= 0)
						{
							num = 5;
							continue;
						}
						goto IL_60;
					case 1:
						if (this.ᜇ > this.ᜈ.Items.Count - 1)
						{
							num = 4;
							continue;
						}
						goto IL_B4;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_8E;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 3:
						goto IL_8E;
					case 4:
						goto IL_8C;
					case 5:
						num = 1;
						continue;
					}
					if (this.ᜈ != null)
					{
						num = 3;
						continue;
					}
					break;
					IL_8E:
					num = 0;
				}
				IL_60:
				return null;
				IL_8C:
				goto IL_60;
				IL_B4:
				return this.ᜈ[this.ᜇ];
			}
		}

		// Token: 0x060041E5 RID: 16869 RVA: 0x003E37F8 File Offset: 0x003E27F8
		public BookmarksNavigator(IDocument doc)
		{
			this.ᜆ = (Document)doc;
		}

		// Token: 0x060041E6 RID: 16870 RVA: 0x003E3818 File Offset: 0x003E2818
		public void MoveToBookmark(string bookmarkName)
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
			this.MoveToBookmark(bookmarkName, false, false);
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x003E385C File Offset: 0x003E285C
		public void MoveToBookmark(string bookmarkName, bool isStart, bool isAfter)
		{
			int a_ = 14;
			IParagraphBase paragraphBase2;
			for (;;)
			{
				this.ᜊ = isStart;
				this.ᜋ = isAfter;
				string name = bookmarkName.Replace('-', '_');
				int num = 3;
				for (;;)
				{
					IParagraphBase paragraphBase;
					switch (num)
					{
					case 0:
						paragraphBase = this.ᜉ.BookmarkStart;
						goto IL_188;
					case 1:
						if (isAfter)
						{
							num = 8;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_151;
						default:
							goto IL_AA;
						}
						break;
					case 2:
					{
						IParagraphBase bookmarkEnd;
						paragraphBase = bookmarkEnd;
						goto IL_188;
					}
					case 3:
						if (this.ᜆ == null)
						{
							num = 11;
							continue;
						}
						this.ᜉ = this.ᜆ.Bookmarks.FindByName(name);
						num = 5;
						continue;
					case 4:
						goto IL_151;
					case 5:
						if (this.ᜉ != null)
						{
							num = 10;
							continue;
						}
						goto IL_1CE;
					case 6:
						num = 9;
						continue;
					case 7:
					{
						IParagraphBase bookmarkEnd = this.ᜉ.BookmarkEnd;
						num = 2;
						continue;
					}
					case 8:
						goto IL_1B1;
					case 9:
						if (this.ᜉ.BookmarkEnd != null)
						{
							num = 7;
							continue;
						}
						goto IL_1B6;
					case 10:
						num = 4;
						continue;
					case 11:
						goto IL_75;
					}
					break;
					IL_151:
					if (!isStart)
					{
						num = 6;
						continue;
					}
					goto IL_1B6;
					IL_188:
					paragraphBase2 = paragraphBase;
					this.ᜈ = paragraphBase2.OwnerParagraph;
					num = 1;
					continue;
					IL_1B6:
					num = 0;
				}
			}
			IL_75:
			throw new InvalidOperationException(ClipboardData.b("⵳᥵൷婹ύώꊁﲇꪉ曆ﶍ늑킓秊ﮗﮝ캟횡장\udea7쎩쮫쾭쒯\uddb1욳隵쾷펹좻횽꾿럁냃ꇇ꓉ꗋ뫍맏돑룓뿕ꋗ동닛망샟ꛡ诣藥鷧蟩觫胭蓯틱蓳蓵韷諹駻賽瓿笁", a_));
			IL_AA:
			if (false)
			{
			}
			if (true)
			{
			}
			this.ᜇ = this.ᜈ.Items.IndexOf(paragraphBase2);
			return;
			IL_1B1:
			this.ᜇ = this.ᜈ.Items.IndexOf(paragraphBase2) + 1;
			return;
			IL_1CE:
			throw new ArgumentException(ClipboardData.b("❳ٵᵷ᥹ᕻ᡽ꚅﶏﶕ뢗肟쒡쮣펥욧캩", a_));
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x003E3A4C File Offset: 0x003E2A4C
		public ITextRange InsertText(string text)
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
			return this.InsertText(text, true);
		}

		// Token: 0x060041E9 RID: 16873 RVA: 0x003E3A90 File Offset: 0x003E2A90
		public ITextRange InsertText(string text, bool saveFormatting)
		{
			ITextRange textRange;
			for (;;)
			{
				this.ᜀ();
				textRange = (this.ᜉ.BookmarkStart.NextSibling as ITextRange);
				int num = 1;
				for (;;)
				{
					ITextRange textRange2;
					switch (num)
					{
					case 0:
						goto IL_FF;
					case 1:
						if (saveFormatting)
						{
							num = 6;
							continue;
						}
						textRange = (this.InsertParagraphItem(ParagraphItemType.TextRange) as ITextRange);
						textRange.Text = text;
						num = 0;
						continue;
					case 2:
						return textRange;
					case 3:
						if (textRange2 != null)
						{
							num = 4;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_186;
						default:
							if (false)
							{
							}
							this.ᜀ(textRange);
							num = 7;
							continue;
						}
						break;
					case 4:
						goto IL_186;
					case 5:
						if (textRange != null)
						{
							num = 9;
							continue;
						}
						textRange2 = (this.ᜉ.BookmarkStart.PreviousSibling as ITextRange);
						textRange = (this.InsertParagraphItem(ParagraphItemType.TextRange) as ITextRange);
						num = 3;
						continue;
					case 6:
						num = 5;
						continue;
					case 7:
						goto IL_127;
					case 8:
						goto IL_127;
					case 9:
					{
						ITextRange textRange3 = textRange;
						textRange3.Text += text;
						num = 10;
						continue;
					}
					case 10:
						goto IL_DB;
					}
					break;
					IL_127:
					textRange.Text = text;
					num = 2;
					continue;
					IL_186:
					CharacterFormat characterFormat = textRange2.CharacterFormat;
					textRange.CharacterFormat.ImportContainer(characterFormat);
					num = 8;
				}
			}
			IL_DB:
			return textRange;
			IL_FF:
			if (true)
			{
			}
			return textRange;
		}

		// Token: 0x060041EA RID: 16874 RVA: 0x003E3C2C File Offset: 0x003E2C2C
		public void InsertTable(ITable table)
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
			this.ᜀ(table as BodyRegion);
		}

		// Token: 0x060041EB RID: 16875 RVA: 0x003E3C74 File Offset: 0x003E2C74
		public IParagraphBase InsertParagraphItem(ParagraphItemType itemType)
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
			IParagraphBase paragraphBase = this.ᜆ.CreateParagraphItem(itemType);
			this.ᜈ.Items.Insert(this.ᜇ, paragraphBase);
			return paragraphBase;
		}

		// Token: 0x060041EC RID: 16876 RVA: 0x003E3CD4 File Offset: 0x003E2CD4
		public void InsertParagraph(IParagraph paragraph)
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
			this.ᜀ(paragraph as BodyRegion);
		}

		// Token: 0x060041ED RID: 16877 RVA: 0x003E3D1C File Offset: 0x003E2D1C
		public void InsertTextBodyPart(TextBodyPart bodyPart)
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
				if (this.CurrentBookmark.BookmarkStart != null)
				{
					bodyPart.PasteAfter(this.ᜉ.BookmarkStart);
					return;
				}
				break;
			}
		}

		// Token: 0x060041EE RID: 16878 RVA: 0x003E3D78 File Offset: 0x003E2D78
		public TextBodyPart GetBookmarkContent()
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
			BookmarkStart bookmarkStart = this.ᜉ.BookmarkStart;
			BookmarkEnd bookmarkEnd = this.ᜉ.BookmarkEnd;
			TextBodySelection textBodySelection = new TextBodySelection(bookmarkStart, bookmarkEnd);
			textBodySelection.ParagraphItemStartIndex++;
			textBodySelection.ParagraphItemEndIndex--;
			return new TextBodyPart(textBodySelection);
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x003E3DFC File Offset: 0x003E2DFC
		public void DeleteBookmarkContent(bool saveFormatting)
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
			this.ᜀ(saveFormatting, false);
		}

		// Token: 0x060041F0 RID: 16880 RVA: 0x003E3E40 File Offset: 0x003E2E40
		internal void ᜀ(bool A_0, bool A_1)
		{
			int a_ = 7;
			switch (0)
			{
			default:
			{
				int num = 37;
				for (;;)
				{
					TextRange textRange;
					int num2;
					BookmarkEnd bookmarkEnd;
					Paragraph ownerParagraph;
					ParagraphItemCollection items;
					BookmarkStart bookmarkStart;
					switch (num)
					{
					case 0:
						num = 34;
						continue;
					case 1:
						goto IL_267;
					case 2:
						num = 36;
						continue;
					case 3:
						if (textRange != null)
						{
							num = 20;
							continue;
						}
						goto IL_550;
					case 4:
						goto IL_4C2;
					case 5:
						if (num2 == 2)
						{
							num = 14;
							continue;
						}
						goto IL_5F6;
					case 6:
						goto IL_5F6;
					case 7:
						goto IL_267;
					case 8:
						if (bookmarkEnd != null)
						{
							num = 32;
							continue;
						}
						goto IL_5F6;
					case 9:
						goto IL_550;
					case 10:
						goto IL_367;
					case 11:
					{
						Paragraph ownerParagraph2;
						if (ownerParagraph != ownerParagraph2)
						{
							num = 12;
							continue;
						}
						goto IL_367;
					}
					case 12:
						num = 7;
						continue;
					case 13:
						if (A_1)
						{
							num = 0;
							continue;
						}
						goto IL_5F6;
					case 14:
						num = 46;
						continue;
					case 15:
					{
						ownerParagraph.RemoveSelf();
						Paragraph ownerParagraph2;
						int num3;
						ownerParagraph2.Items.RemoveAt(num3);
						num = 30;
						continue;
					}
					case 16:
					{
						int num3;
						if (num3 != 0)
						{
							num = 27;
							continue;
						}
						num = 13;
						continue;
					}
					case 17:
						if (items.Count > num2)
						{
							num = 47;
							continue;
						}
						goto IL_550;
					case 18:
					{
						Paragraph ownerParagraph2;
						if (ownerParagraph != ownerParagraph2)
						{
							num = 19;
							continue;
						}
						goto IL_5F6;
					}
					case 19:
					{
						int num3 = bookmarkEnd.ឯ();
						num = 16;
						continue;
					}
					case 20:
						textRange.Text = "";
						num2++;
						num = 48;
						continue;
					case 21:
					{
						Paragraph ownerParagraph2;
						ownerParagraph2.RemoveSelf();
						num = 31;
						continue;
					}
					case 22:
						goto IL_11A;
					case 23:
						goto IL_115;
					case 24:
					{
						Paragraph ownerParagraph2;
						int num3;
						if (num3 >= ownerParagraph2.Items.Count)
						{
							num = 21;
							continue;
						}
						DocumentObject entity = ownerParagraph2.Items[num3];
						ownerParagraph.Items.Add(entity);
						num = 22;
						continue;
					}
					case 25:
						if (items.Count > num2)
						{
							num = 28;
							continue;
						}
						goto IL_149;
					case 26:
					{
						Paragraph ownerParagraph2;
						if (ownerParagraph.Owner != ownerParagraph2.Owner)
						{
							num = 4;
							continue;
						}
						Body body = (Body)ownerParagraph.Owner;
						BodyRegionCollection bodyRegionCollection = body.Items;
						int num4 = ownerParagraph.ឯ() + 1;
						num = 11;
						continue;
					}
					case 27:
						num = 38;
						continue;
					case 28:
						num = 40;
						continue;
					case 29:
						goto IL_2EA;
					case 30:
						goto IL_3A4;
					case 31:
						goto IL_5F6;
					case 32:
					{
						ownerParagraph = bookmarkStart.OwnerParagraph;
						Paragraph ownerParagraph2 = bookmarkEnd.OwnerParagraph;
						num = 26;
						continue;
					}
					case 33:
						if (this.ᜆ.Bookmarks.InnerList.Contains(this.CurrentBookmark))
						{
							num = 44;
							continue;
						}
						return;
					case 34:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_41E;
						default:
							if (false)
							{
							}
							if (num2 == 1)
							{
								num = 15;
								continue;
							}
							goto IL_3A4;
						}
						break;
					case 35:
						goto IL_149;
					case 36:
					{
						Paragraph ownerParagraph2;
						BodyRegionCollection bodyRegionCollection;
						int num4;
						if (bodyRegionCollection[num4] == ownerParagraph2)
						{
							num = 10;
							continue;
						}
						bodyRegionCollection.RemoveAt(num4);
						num = 1;
						continue;
					}
					case 38:
						goto IL_11A;
					case 39:
						if (A_0)
						{
							if (true)
							{
							}
							num = 49;
							continue;
						}
						goto IL_550;
					case 40:
						if (items[num2] == bookmarkEnd)
						{
							num = 35;
							continue;
						}
						items.RemoveAt(num2);
						num = 43;
						continue;
					case 41:
						this.ᜇ--;
						num = 9;
						continue;
					case 42:
					{
						Paragraph ownerParagraph2;
						int num3;
						ownerParagraph2.Items.RemoveAt(num3);
						BookmarkEnd entity2 = items[0] as BookmarkEnd;
						BodyRegionCollection bodyRegionCollection;
						int num4;
						((Paragraph)bodyRegionCollection[num4]).Items.Insert(0, entity2);
						ownerParagraph.RemoveSelf();
						num = 6;
						continue;
					}
					case 43:
						if (this.ᜇ > 0)
						{
							num = 41;
							continue;
						}
						goto IL_550;
					case 44:
						this.MoveToBookmark(this.CurrentBookmark.Name, this.ᜊ, this.ᜋ);
						num = 29;
						continue;
					case 45:
					{
						BodyRegionCollection bodyRegionCollection;
						int num4;
						if (bodyRegionCollection.Count > num4)
						{
							num = 2;
							continue;
						}
						goto IL_367;
					}
					case 46:
						if (items[0] is BookmarkEnd)
						{
							num = 42;
							continue;
						}
						goto IL_5F6;
					case 47:
						goto IL_41E;
					case 48:
						goto IL_550;
					case 49:
						num = 17;
						continue;
					}
					if (this.CurrentBookmark == null)
					{
						num = 23;
						continue;
					}
					bookmarkStart = this.CurrentBookmark.BookmarkStart;
					bookmarkEnd = this.CurrentBookmark.BookmarkEnd;
					num = 8;
					continue;
					IL_11A:
					num = 24;
					continue;
					IL_149:
					num = 18;
					continue;
					IL_267:
					num = 45;
					continue;
					IL_367:
					items = ownerParagraph.Items;
					num2 = bookmarkStart.ឯ() + 1;
					num = 39;
					continue;
					IL_3A4:
					num = 5;
					continue;
					IL_41E:
					textRange = (items[num2] as TextRange);
					num = 3;
					continue;
					IL_550:
					num = 25;
					continue;
					IL_5F6:
					num = 33;
				}
				IL_115:
				throw new InvalidOperationException();
				IL_2EA:
				return;
				IL_4C2:
				throw new NotSupportedException(ClipboardData.b("⍬nհ卲ٴɶॸ୺ቼൾꞆ朗ﮔ릘햠욢쮤펦覨즪좬\udbae우횲킴\ud9b6馸\ud9ba튼킾ꫀ껂꓄뗆ꋈ룊ꛎ뿐뇔뻖뿘뷚룜귞蓠跢釤쟦駨諪鿬軮雰臲铴蟶釸裺", a_));
			}
			}
		}

		// Token: 0x060041F1 RID: 16881 RVA: 0x003E4480 File Offset: 0x003E3480
		public void ReplaceBookmarkContent(TextBodyPart bodyPart)
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
			this.ᜀ(false, false);
			bodyPart.PasteAfter(this.ᜉ.BookmarkStart);
		}

		// Token: 0x060041F2 RID: 16882 RVA: 0x003E44D4 File Offset: 0x003E34D4
		public void ReplaceBookmarkContent(string text, bool saveFormatting)
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
			this.ᜀ(saveFormatting, false);
			this.InsertText(text, saveFormatting);
		}

		// Token: 0x060041F3 RID: 16883 RVA: 0x003E4520 File Offset: 0x003E3520
		private void ᜀ()
		{
			int a_ = 2;
			int num = 2;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_BA;
				default:
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						if (this.ᜇ < 0)
						{
							num = 6;
							continue;
						}
						return;
					case 1:
						if (this.ᜉ != null)
						{
							num = 5;
							continue;
						}
						goto IL_CE;
					case 3:
						if (this.ᜈ != null)
						{
							num = 4;
							continue;
						}
						goto IL_CE;
					case 4:
						num = 0;
						continue;
					case 5:
						num = 3;
						continue;
					case 6:
						goto IL_98;
					case 7:
						goto IL_70;
					}
					if (this.ᜆ == null)
					{
						if (true)
						{
						}
						num = 7;
					}
					else
					{
						num = 1;
					}
					break;
				}
			}
			IL_70:
			goto IL_BA;
			IL_98:
			goto IL_CE;
			IL_BA:
			throw new InvalidOperationException(ClipboardData.b("ㅧթᥫ乭፯፱ᩳ噵ᙷᕹࡻ幽ꚅ첇ﮍﶏ望횗ﮙ잟쎡킣즥\udaa7誩\udbab잭쒯\udab1\udbb3쎵첷骹햻킽ꦿ뛁귃Ʂ꓇ꏉ뛋ꟍ뻏뗑鋕럗맙꧛돝藟賡郣웥飧飩菫黭闯胱胳迵", a_));
			IL_CE:
			throw new InvalidOperationException(ClipboardData.b("⭧Ὡṫᱭᕯᱱs噵㩷ᕹ፻ᕽꢇﺏ떑뚕ﾙﮝ쎟횡", a_));
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x003E4630 File Offset: 0x003E3630
		private void ᜀ(ITextRange A_0)
		{
			Paragraph ownerParagraph;
			for (;;)
			{
				ownerParagraph = this.ᜉ.BookmarkStart.OwnerParagraph;
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						if (ownerParagraph == null)
						{
							num = 0;
							continue;
						}
						num = 6;
						continue;
					case 2:
						if (ownerParagraph.OwnerTextBody.Paragraphs.Count == 1)
						{
							num = 4;
							continue;
						}
						goto IL_105;
					case 3:
						if (ownerParagraph.OwnerTextBody.DocumentObjectType == DocumentObjectType.TableCell)
						{
							num = 7;
							continue;
						}
						goto IL_105;
					case 4:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_51;
						}
						goto Block_4;
					case 5:
						num = 3;
						continue;
					case 6:
						if (ownerParagraph.OwnerTextBody != null)
						{
							num = 5;
							continue;
						}
						goto IL_105;
					case 7:
						goto IL_51;
					}
					break;
					IL_51:
					num = 2;
				}
			}
			return;
			Block_4:
			if (false)
			{
			}
			TableCell tableCell = ownerParagraph.OwnerTextBody as TableCell;
			A_0.CharacterFormat.ImportContainer(tableCell.CharacterFormat);
			return;
			IL_105:
			if (true)
			{
			}
			A_0.CharacterFormat.ImportContainer(ownerParagraph.BreakCharacterFormat);
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x003E475C File Offset: 0x003E375C
		private void ᜀ(BodyRegion A_0)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					ParagraphBase bookmarkStart = this.CurrentBookmark.BookmarkStart;
					int num = 17;
					for (;;)
					{
						Paragraph paragraph;
						int num2;
						Paragraph ownerParagraph;
						switch (num)
						{
						case 0:
							num = 16;
							continue;
						case 1:
							num = 2;
							continue;
						case 2:
							if (bookmarkStart.NextSibling == null)
							{
								goto IL_24B;
							}
							goto IL_C4;
						case 3:
							num = 4;
							continue;
						case 4:
							if (paragraph.Text == "")
							{
								num = 12;
								continue;
							}
							return;
						case 5:
							num = 7;
							continue;
						case 6:
						{
							bool flag;
							if (flag)
							{
								num = 3;
								continue;
							}
							return;
						}
						case 7:
							if (bookmarkStart.OwnerBase.OwnerBase is Body)
							{
								num = 20;
								continue;
							}
							return;
						case 8:
						{
							bool flag = true;
							num = 14;
							continue;
						}
						case 9:
							return;
						case 10:
							if (bookmarkStart.NextSibling == null)
							{
								num = 8;
								continue;
							}
							goto IL_2A9;
						case 11:
							if (bookmarkStart.OwnerBase != null)
							{
								num = 0;
								continue;
							}
							return;
						case 12:
							num = 11;
							continue;
						case 13:
							if (A_0 is Paragraph)
							{
								num = 1;
								continue;
							}
							goto IL_C4;
						case 14:
							goto IL_2A9;
						case 15:
							goto IL_C4;
						case 16:
							if (bookmarkStart.OwnerBase.OwnerBase != null)
							{
								num = 5;
								continue;
							}
							return;
						case 17:
						{
							if (bookmarkStart == null)
							{
								num = 19;
								continue;
							}
							bool flag = false;
							num2 = bookmarkStart.ឯ();
							ownerParagraph = bookmarkStart.OwnerParagraph;
							paragraph = new Paragraph(ownerParagraph.Document);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_24B;
							default:
								if (false)
								{
								}
								num = 10;
								continue;
							}
							break;
						}
						case 18:
						{
							paragraph = (A_0 as Paragraph);
							bool flag = false;
							num = 15;
							continue;
						}
						case 19:
							goto IL_8F;
						case 20:
						{
							int index = (bookmarkStart.OwnerBase.OwnerBase as Body).Paragraphs.IndexOf(paragraph);
							(bookmarkStart.OwnerBase.OwnerBase as Body).Paragraphs.RemoveAt(index);
							num = 9;
							continue;
						}
						}
						break;
						IL_C4:
						num2++;
						TextBodyPart.ᜀ(ownerParagraph, num2, paragraph);
						int index2 = paragraph.ឯ();
						ownerParagraph.OwnerTextBody.Items.Insert(index2, A_0);
						num = 6;
						continue;
						IL_24B:
						num = 18;
						continue;
						IL_2A9:
						num = 13;
					}
				}
				IL_8F:
				if (true)
				{
				}
				return;
			}
		}

		// Token: 0x04003397 RID: 13207
		private const string ᜀ = "You can not use DocumentNavigator without initializing Document property";

		// Token: 0x04003398 RID: 13208
		private const string ᜁ = "Specified bookmark not found";

		// Token: 0x04003399 RID: 13209
		private bool \u2460\u008A\u008B\u0099;

		// Token: 0x0400339A RID: 13210
		private const string ᜂ = " Document property must be equal this Document property";

		// Token: 0x0400339B RID: 13211
		private const string ᜃ = "Current Bookmark didn't select";

		// Token: 0x0400339C RID: 13212
		private const string ᜄ = "Not supported getting content between bookmarks in different paragraphs";

		// Token: 0x0400339D RID: 13213
		private const string ᜅ = "Not supported deleting content between bookmarks in different paragraphs";

		// Token: 0x0400339E RID: 13214
		private Document ᜆ;

		// Token: 0x0400339F RID: 13215
		private long[] \u25D8ª\u0089\u009B;

		// Token: 0x040033A0 RID: 13216
		private int ᜇ;

		// Token: 0x040033A1 RID: 13217
		private IParagraph ᜈ;

		// Token: 0x040033A2 RID: 13218
		private float \u25D8\u008C\u0088ª;

		// Token: 0x040033A3 RID: 13219
		private string[] \u2609\u009D\u00A6\u0083;

		// Token: 0x040033A4 RID: 13220
		private Bookmark ᜉ;

		// Token: 0x040033A5 RID: 13221
		private long \u2609\u0083\u00A5\u008B;

		// Token: 0x040033A6 RID: 13222
		private bool ᜊ;

		// Token: 0x040033A7 RID: 13223
		private byte \u25D8\u009C\u00A7\u00A8;

		// Token: 0x040033A8 RID: 13224
		private bool ᜋ;
	}
}
