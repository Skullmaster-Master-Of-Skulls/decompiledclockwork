using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc
{
	// Token: 0x020000D7 RID: 215
	public class BookmarkEnd : ParagraphBase, spr\u2297
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00019784 File Offset: 0x00018784
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
				return DocumentObjectType.BookmarkEnd;
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000258 RID: 600 RVA: 0x000197C4 File Offset: 0x000187C4
		public string Name
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
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00019808 File Offset: 0x00018808
		// (set) Token: 0x0600025A RID: 602 RVA: 0x0001984C File Offset: 0x0001884C
		internal bool IsCellGroupBkmk
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
				return this.ᜁ;
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
				this.ᜁ = value;
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00019890 File Offset: 0x00018890
		internal BookmarkEnd(Document A_0) : this(A_0, "")
		{
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000198AC File Offset: 0x000188AC
		public BookmarkEnd(IDocument document, string name) : base((Document)document)
		{
			this.ᜀ(name);
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000198D8 File Offset: 0x000188D8
		internal new void ᜀ(string A_0)
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
			this.ᜀ = A_0.Replace('-', '_');
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00019924 File Offset: 0x00018924
		internal override void Attach(Paragraph owner, int itemPos)
		{
			base.Attach(owner, itemPos);
			if (!base.DeepDetached)
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
				base.Document.Bookmarks.ᜀ(this);
				this.ᜃ = false;
				return;
			}
			this.ᜃ = true;
			this.ᜂ = true;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00019998 File Offset: 0x00018998
		internal override void Detach()
		{
			for (;;)
			{
				IL_1C:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_56:
					num = 1;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					base.Detach();
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_4E;
					case 1:
					{
						Bookmark bookmark = base.Document.Bookmarks.FindByName(this.Name);
						num = 3;
						continue;
					}
					case 2:
						return;
					case 3:
					{
						Bookmark bookmark;
						if (bookmark != null)
						{
							num = 4;
							continue;
						}
						return;
					}
					case 4:
					{
						Bookmark bookmark;
						bookmark.ᜀ(null);
						num = 2;
						continue;
					}
					}
					goto IL_1C;
				}
				IL_4E:
				if (!base.DeepDetached)
				{
					goto IL_56;
				}
				break;
			}
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00019A54 File Offset: 0x00018A54
		internal override void CloneCommit()
		{
			int num = 0;
			for (;;)
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_2A;
				default:
					goto IL_2A;
				}
				IL_4A:
				if (this.ᜃ)
				{
					num = 4;
					continue;
				}
				break;
				goto IL_4A;
				IL_2A:
				if (false)
				{
				}
				switch (num)
				{
				case 1:
					if (this.ᜂ)
					{
						num = 2;
						continue;
					}
					return;
				case 2:
					base.Document.Bookmarks.ᜀ(this);
					this.ᜃ = false;
					if (true)
					{
					}
					num = 3;
					continue;
				case 3:
					return;
				case 4:
					num = 1;
					continue;
				}
				goto IL_4A;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00019B08 File Offset: 0x00018B08
		protected override object CloneImpl()
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
			BookmarkEnd bookmarkEnd = (BookmarkEnd)base.CloneImpl();
			bookmarkEnd.ᜃ = true;
			bookmarkEnd.ᜂ = true;
			return bookmarkEnd;
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00019B60 File Offset: 0x00018B60
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 9;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				writer.WriteValue(ClipboardData.b("᭮ࡰͲၴ", a_), ParagraphItemType.BookmarkEnd);
				writer.WriteValue(ClipboardData.b("⵮ṰᱲṴ᩶ᡸॺᙼㅾ", a_), this.Name);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						writer.WriteValue(ClipboardData.b("♮ɰひၴ᭶ᕸ㱺ོၾ임", a_), this.IsCellGroupBkmk);
						num = 2;
						continue;
					case 1:
						IL_6B:
						if (this.IsCellGroupBkmk)
						{
							num = 0;
							continue;
						}
						goto IL_A4;
					case 2:
						goto IL_A4;
					}
					break;
					IL_A4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6B;
					default:
						goto IL_BA;
					}
				}
			}
			IL_BA:
			if (true)
			{
			}
			if (false)
			{
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00019C38 File Offset: 0x00018C38
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 15;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				this.ᜀ = reader.ReadString(ClipboardData.b("㝴ᡶᙸၺၼṾ쮄", a_));
				base.Document.Bookmarks.ᜀ(this);
				int num = 2;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 0:
						this.IsCellGroupBkmk = reader.ReadBoolean(ClipboardData.b("㱴Ѷ㩸Ṻᅼ፾욀麗즊敖", a_));
						num = 1;
						continue;
					case 1:
						goto IL_B0;
					case 2:
						IL_69:
						if (reader.HasAttribute(ClipboardData.b("㱴Ѷ㩸Ṻᅼ፾욀麗즊敖", a_)))
						{
							num = 0;
							continue;
						}
						goto IL_B0;
					}
					break;
					IL_B0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_69;
					default:
						goto IL_C6;
					}
				}
			}
			IL_C6:
			if (false)
			{
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00019D14 File Offset: 0x00018D14
		protected override void CreateLayoutInfo()
		{
			this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Horizontal);
			if (base.Owner is spr\u1AD2)
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
				this.ᜀ.ᜀ((base.Owner.Owner.Owner as spr\u1AB8).ᜀ().ᜀ());
				return;
			}
			this.ᜀ.ᜀ(((spr\u1AB8)base.OwnerParagraph).ᜀ().ᜀ());
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00019DB0 File Offset: 0x00018DB0
		SizeF spr\u2297.Measure(spr\u19E0 dc)
		{
			int a_ = 10;
			SizeF result;
			for (;;)
			{
				IL_21:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				case 1:
					goto IL_41;
				default:
					goto IL_41;
				}
				int num;
				for (;;)
				{
					IL_0B:
					switch (num)
					{
					case 0:
						return result;
					case 1:
						result.Height = dc.ᜁ(ClipboardData.b("偯", a_), (base.Owner.Owner.Owner as Paragraph).BreakCharacterFormat.Font, null).Height;
						num = 0;
						continue;
					case 2:
						return result;
					case 3:
						if (base.Owner is spr\u1AD2)
						{
							num = 1;
							continue;
						}
						result.Height = dc.ᜁ(ClipboardData.b("偯", a_), base.OwnerParagraph.BreakCharacterFormat.Font, null).Height;
						num = 2;
						continue;
					}
					goto IL_21;
				}
				IL_41:
				if (true)
				{
				}
				if (false)
				{
				}
				result = default(SizeF);
				num = 3;
				goto IL_0B;
			}
			return result;
		}

		// Token: 0x04000C65 RID: 3173
		private new string ᜀ = "";

		// Token: 0x04000C66 RID: 3174
		private new bool ᜁ;

		// Token: 0x04000C67 RID: 3175
		private bool \u2593\u00AF\u00A7\u00AE;

		// Token: 0x04000C68 RID: 3176
		private bool ᜂ;

		// Token: 0x04000C69 RID: 3177
		internal bool ᜃ;
	}
}
