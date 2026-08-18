using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Fields;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc
{
	// Token: 0x020000A0 RID: 160
	public class BookmarkStart : ParagraphBase, spr\u2297
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00014588 File Offset: 0x00013588
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
				return DocumentObjectType.BookmarkStart;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001CB RID: 459 RVA: 0x000145C8 File Offset: 0x000135C8
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

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060001CC RID: 460 RVA: 0x0001460C File Offset: 0x0001360C
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00014650 File Offset: 0x00013650
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

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00014694 File Offset: 0x00013694
		// (set) Token: 0x060001CF RID: 463 RVA: 0x000146D8 File Offset: 0x000136D8
		internal int ColumnFirst
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
				if (true)
				{
				}
				if (false)
				{
				}
				this.ᜄ = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x0001471C File Offset: 0x0001371C
		// (set) Token: 0x060001D1 RID: 465 RVA: 0x00014760 File Offset: 0x00013760
		internal int ColumnLast
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

		// Token: 0x060001D2 RID: 466 RVA: 0x000147A4 File Offset: 0x000137A4
		internal BookmarkStart(Document A_0) : this(A_0, "")
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000147C0 File Offset: 0x000137C0
		public BookmarkStart(IDocument doc, string name) : base((Document)doc)
		{
			this.ᜀ(name);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000147FC File Offset: 0x000137FC
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

		// Token: 0x060001D5 RID: 469 RVA: 0x00014848 File Offset: 0x00013848
		internal override void Attach(Paragraph owner, int itemPos)
		{
			base.Attach(owner, itemPos);
			if (!base.DeepDetached)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					break;
				}
				if (false)
				{
				}
				if (true)
				{
				}
				base.Document.Bookmarks.ᜀ(this);
				this.ᜃ = false;
				return;
			}
			this.ᜃ = true;
			this.ᜂ = true;
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000148BC File Offset: 0x000138BC
		internal override void Detach()
		{
			for (;;)
			{
				IL_24:
				Bookmark bookmark;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
				{
					IL_6A:
					bookmark.ᜀ(null);
					BookmarkCollection bookmarks;
					bookmarks.Remove(bookmark);
					num = 2;
					break;
				}
				default:
					if (false)
					{
					}
					base.Detach();
					num = 4;
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
						goto IL_B4;
					case 1:
						if (bookmark != null)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						return;
					case 3:
					{
						BookmarkCollection bookmarks = base.Document.Bookmarks;
						bookmark = bookmarks.FindByName(this.Name);
						num = 1;
						continue;
					}
					case 4:
						if (!base.DeepDetached)
						{
							num = 3;
							continue;
						}
						return;
					}
					goto IL_24;
				}
				IL_B4:
				goto IL_6A;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x00014980 File Offset: 0x00013980
		internal override void CloneCommit()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_68;
				case 1:
					if (true)
					{
					}
					num = 4;
					continue;
				case 2:
					base.Document.Bookmarks.ᜀ(this);
					this.ᜃ = false;
					this.ᜂ = false;
					num = 0;
					continue;
				case 4:
					if (this.ᜂ)
					{
						num = 2;
						continue;
					}
					return;
				}
				if (!this.ᜃ)
				{
					return;
				}
				num = 1;
			}
			IL_68:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (false)
			{
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x00014A40 File Offset: 0x00013A40
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
			BookmarkStart bookmarkStart = (BookmarkStart)base.CloneImpl();
			bookmarkStart.ᜃ = true;
			bookmarkStart.ᜂ = true;
			return bookmarkStart;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00014A98 File Offset: 0x00013A98
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 8;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				writer.WriteValue(ClipboardData.b("ᩭ९ɱᅳ", a_), ParagraphItemType.BookmarkStart);
				writer.WriteValue(ClipboardData.b("ⱭὯᵱέ᭵᥷ࡹ᝻ぽ", a_), this.Name);
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_73;
						default:
							if (false)
							{
							}
							writer.WriteValue(ClipboardData.b("❭ͯㅱᅳ᩵ᑷ㵹๻ᅽ욃", a_), this.IsCellGroupBkmk);
							num = 0;
							continue;
						}
						break;
					case 2:
						goto IL_73;
					}
					break;
					IL_73:
					if (!this.IsCellGroupBkmk)
					{
						return;
					}
					num = 1;
				}
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x00014B70 File Offset: 0x00013B70
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 16;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				this.ᜀ = reader.ReadString(ClipboardData.b("㑵᝷ᕹ᝻፽종", a_));
				base.Document.Bookmarks.ᜀ(this);
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (true)
						{
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_61;
						default:
							if (false)
							{
							}
							this.IsCellGroupBkmk = reader.ReadBoolean(ClipboardData.b("㽵୷㥹᥻ች얁ﶇ憎캋ﶏ撚", a_));
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_61;
					case 2:
						return;
					}
					break;
					IL_61:
					if (!reader.HasAttribute(ClipboardData.b("㽵୷㥹᥻ች얁ﶇ憎캋ﶏ撚", a_)))
					{
						return;
					}
					num = 0;
				}
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x00014C4C File Offset: 0x00013C4C
		protected override void CreateLayoutInfo()
		{
			if (true)
			{
			}
			this.ᜀ = new spr\u22A8(ChildrenLayoutDirection.Horizontal);
			this.ᜀ.ᜂ(true);
			if (base.Owner is spr\u1AD2)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_4F;
					}
				}
				IL_4F:
				if (false)
				{
				}
				this.ᜀ.ᜀ((base.Owner.Owner.Owner as spr\u1AB8).ᜀ().ᜀ());
				return;
			}
			this.ᜀ.ᜀ(((spr\u1AB8)base.OwnerParagraph).ᜀ().ᜀ());
		}

		// Token: 0x060001DC RID: 476 RVA: 0x00014CF4 File Offset: 0x00013CF4
		SizeF spr\u2297.Measure(spr\u19E0 dc)
		{
			int a_ = 14;
			SizeF result;
			for (;;)
			{
				result = default(SizeF);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return result;
					case 1:
						if (true)
						{
						}
						result.Height = dc.ᜁ(ClipboardData.b("味", a_), (base.Owner.Owner.Owner as Paragraph).BreakCharacterFormat.Font, null).Height;
						num = 2;
						continue;
					case 2:
						return result;
					case 3:
						if (base.Owner is spr\u1AD2)
						{
							num = 1;
							continue;
						}
						result.Height = dc.ᜁ(ClipboardData.b("味", a_), base.OwnerParagraph.BreakCharacterFormat.Font, null).Height;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return result;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x04000997 RID: 2455
		private string \u2609\u00A0\u008F\u0089;

		// Token: 0x04000998 RID: 2456
		private new string ᜀ = "";

		// Token: 0x04000999 RID: 2457
		private new bool ᜁ;

		// Token: 0x0400099A RID: 2458
		private bool ᜂ;

		// Token: 0x0400099B RID: 2459
		internal bool ᜃ;

		// Token: 0x0400099C RID: 2460
		private new int ᜄ = -1;

		// Token: 0x0400099D RID: 2461
		private bool \u25D8\u0090\u009C\u008E;

		// Token: 0x0400099E RID: 2462
		private int[] \u2609\u009B\u008D\u009B;

		// Token: 0x0400099F RID: 2463
		private int ᜅ = -1;
	}
}
