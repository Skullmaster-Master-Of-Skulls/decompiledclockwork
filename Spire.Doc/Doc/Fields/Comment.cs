using System;
using System.Collections;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Fields
{
	// Token: 0x02000513 RID: 1299
	public class Comment : ParagraphBase, ICompositeObject
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x003EE044 File Offset: 0x003ED044
		public DocumentObjectCollection ChildObjects
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
				return this.m_textBody.ChildObjects;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06004319 RID: 17177 RVA: 0x003EE08C File Offset: 0x003ED08C
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
				return DocumentObjectType.Comment;
			}
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x003EE0CC File Offset: 0x003ED0CC
		public Body Body
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
				return this.m_textBody;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x003EE110 File Offset: 0x003ED110
		public CommentFormat Format
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
				return this.m_format;
			}
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x003EE154 File Offset: 0x003ED154
		public ParagraphItemCollection Items
		{
			get
			{
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
							continue;
						default:
							goto IL_65;
						}
						break;
					case 2:
						this.ᜀ = new ParagraphItemCollection(this.m_doc);
						num = 0;
						continue;
					}
					if (this.ᜀ != null)
					{
						goto IL_77;
					}
					num = 2;
				}
				IL_65:
				if (false)
				{
				}
				IL_77:
				return this.ᜀ;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x003EE1E0 File Offset: 0x003ED1E0
		internal bool AppendItems
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
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600431E RID: 17182 RVA: 0x003EE224 File Offset: 0x003ED224
		internal TextBodyPart BodyPart
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
		}

		// Token: 0x0600431F RID: 17183 RVA: 0x003EE268 File Offset: 0x003ED268
		public Comment(IDocument doc) : base((Document)doc)
		{
			this.m_format = new CommentFormat();
			this.m_textBody = new Body(base.Document, this);
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x003EE2A0 File Offset: 0x003ED2A0
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
			Comment comment = (Comment)base.CloneImpl();
			comment.m_format = this.Format.Clone(base.Document);
			comment.m_textBody = (Body)this.Body.Clone();
			this.ᜀ = null;
			this.ᜁ = null;
			return comment;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x003EE324 File Offset: 0x003ED324
		public void Clear()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A6;
				case 1:
					if (this.ᜂ)
					{
						goto IL_78;
					}
					goto IL_BB;
				case 2:
					if (this.ᜀ.Count == 0)
					{
						num = 0;
						continue;
					}
					num = 1;
					continue;
				case 4:
					num = 2;
					continue;
				case 5:
					goto IL_80;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_78:
					num = 5;
					continue;
				}
				if (true)
				{
				}
				if (false)
				{
				}
				if (this.ᜀ == null)
				{
					return;
				}
				num = 4;
			}
			IL_80:
			this.ᜀ.Clear();
			this.ᜁ = null;
			return;
			IL_A6:
			return;
			IL_BB:
			ParagraphBase a_ = this.ᜀ.FirstItem as ParagraphBase;
			ParagraphBase a_2 = this.ᜀ.LastItem as ParagraphBase;
			this.ᜂ(a_, a_2);
			this.Format.BookmarkStartOffset = 0;
			this.Format.BookmarkEndOffset = 1;
			this.ᜀ.Clear();
			this.ᜂ = false;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x003EE440 File Offset: 0x003ED440
		internal void ᜂ(ParagraphBase A_0, ParagraphBase A_1)
		{
			for (;;)
			{
				IL_00:
				int num = 27;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 26;
						continue;
					case 1:
						A_1.OwnerParagraph.Items.Remove(A_1.NextSibling);
						num = 12;
						continue;
					case 2:
						A_0.OwnerParagraph.Items.Remove(A_0.PreviousSibling);
						num = 14;
						continue;
					case 3:
						if (A_0.NextSibling != A_1)
						{
							num = 23;
							continue;
						}
						goto IL_151;
					case 4:
						goto IL_278;
					case 5:
						num = 21;
						continue;
					case 6:
						goto IL_151;
					case 7:
						if (A_0.OwnerParagraph != A_1.OwnerParagraph)
						{
							num = 24;
							continue;
						}
						goto IL_34C;
					case 8:
						num = 38;
						continue;
					case 9:
						if (A_1.PreviousSibling != null)
						{
							num = 8;
							continue;
						}
						goto IL_25D;
					case 10:
						if (A_0.NextSibling is Comment)
						{
							num = 30;
							continue;
						}
						A_1.OwnerParagraph.Items.Remove(A_1.PreviousSibling);
						num = 6;
						continue;
					case 11:
						num = 10;
						continue;
					case 12:
						goto IL_217;
					case 13:
						if (A_0.PreviousSibling is CommentMark)
						{
							num = 2;
							continue;
						}
						goto IL_177;
					case 14:
						goto IL_177;
					case 15:
						num = 35;
						continue;
					case 16:
						if (A_1.NextSibling != null)
						{
							num = 32;
							continue;
						}
						goto IL_217;
					case 17:
						goto IL_34C;
					case 18:
						num = 17;
						continue;
					case 19:
						goto IL_37A;
					case 20:
						goto IL_34C;
					case 21:
						if (!this.ᜀ(A_0.OwnerParagraph.NextTextBodyItem))
						{
							num = 18;
							continue;
						}
						A_0.OwnerParagraph.NextTextBodyItem.RemoveSelf();
						num = 19;
						continue;
					case 22:
						if (A_0.NextSibling != null)
						{
							num = 37;
							continue;
						}
						goto IL_151;
					case 23:
						num = 28;
						continue;
					case 24:
						num = 25;
						continue;
					case 25:
						goto IL_37A;
					case 26:
						goto IL_151;
					case 28:
						if (!(A_0.NextSibling is Comment))
						{
							A_0.OwnerParagraph.Items.Remove(A_0.NextSibling);
							num = 20;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_00;
						default:
							if (false)
							{
							}
							num = 0;
							continue;
						}
						break;
					case 29:
						if (A_0 != A_1)
						{
							num = 31;
							continue;
						}
						goto IL_429;
					case 30:
						goto IL_25D;
					case 31:
						num = 7;
						continue;
					case 32:
						num = 34;
						continue;
					case 33:
						if (A_0.OwnerParagraph.NextTextBodyItem != A_1.OwnerParagraph)
						{
							num = 15;
							continue;
						}
						goto IL_34C;
					case 34:
						if (A_1.NextSibling is CommentMark)
						{
							num = 1;
							continue;
						}
						goto IL_217;
					case 35:
						if (A_0.OwnerParagraph.NextTextBodyItem != null)
						{
							num = 5;
							continue;
						}
						goto IL_34C;
					case 36:
						num = 13;
						continue;
					case 37:
						if (true)
						{
						}
						num = 3;
						continue;
					case 38:
						if (A_1.PreviousSibling != A_0)
						{
							num = 11;
							continue;
						}
						goto IL_25D;
					}
					if (A_0.PreviousSibling != null)
					{
						num = 36;
						continue;
					}
					goto IL_177;
					IL_151:
					num = 9;
					continue;
					IL_177:
					num = 16;
					continue;
					IL_217:
					num = 29;
					continue;
					IL_25D:
					this.ᜁ(A_0, A_1);
					this.ᜀ(A_0, A_1);
					num = 4;
					continue;
					IL_34C:
					num = 22;
					continue;
					IL_37A:
					num = 33;
				}
			}
			IL_278:
			IL_429:
			A_1.RemoveSelf();
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x003EE87C File Offset: 0x003ED87C
		public void Replace(string text)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				if (true)
				{
				}
				TextRange textRange;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_A5;
					default:
					{
						if (false)
						{
						}
						string text2 = this.ᜀ(text);
						textRange = new TextRange(this.m_doc);
						textRange.Text = text;
						int num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (this.Format.TagBkmk == -1)
								{
									num = 4;
									continue;
								}
								goto IL_11C;
							case 1:
								goto IL_14B;
							case 2:
								goto IL_11C;
							case 3:
								if (text2.IndexOf(ClipboardData.b("硴", a_)) != -1)
								{
									num = 1;
									continue;
								}
								goto IL_169;
							case 4:
								this.Format.ᜁ();
								num = 2;
								continue;
							}
							break;
							IL_11C:
							num = 3;
						}
						break;
					}
					}
				}
				IL_A5:
				this.Clear();
				this.ᜂ = false;
				int a_2 = this.Format.TagBkmk;
				int index = base.ឯ();
				CommentMark entity = new CommentMark(base.Document, a_2, CommentMarkType.CommentStart);
				CommentMark entity2 = new CommentMark(base.Document, a_2, CommentMarkType.CommentEnd);
				base.OwnerParagraph.Items.Insert(index, entity2);
				base.OwnerParagraph.Items.Insert(index, textRange);
				base.OwnerParagraph.Items.Insert(index, entity);
				return;
				IL_14B:
				goto IL_A5;
				IL_169:
				this.Clear();
				this.ᜂ = true;
				this.Items.Add(textRange);
				return;
			}
			}
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x003EEA0C File Offset: 0x003EDA0C
		public void Replace(TextBodyPart textBodyPart)
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
			this.Clear();
			this.ᜂ = true;
			this.ᜁ = textBodyPart;
			this.ᜀ();
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x003EEA64 File Offset: 0x003EDA64
		internal override void Attach(Paragraph owner, int itemPos)
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
			base.Attach(owner, itemPos);
			base.Document.Comments.ᜀ(this);
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x003EEAB8 File Offset: 0x003EDAB8
		internal override void Close()
		{
			for (;;)
			{
				IL_14:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_58:
					this.m_textBody.ᜅ();
					this.m_textBody = null;
					num = 2;
					break;
				default:
					if (true)
					{
					}
					if (false)
					{
					}
					base.Close();
					num = 0;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.m_textBody != null)
						{
							num = 1;
							continue;
						}
						goto IL_7E;
					case 1:
						goto IL_56;
					case 2:
						goto IL_72;
					}
					goto IL_14;
				}
				IL_56:
				goto IL_58;
			}
			IL_72:
			IL_7E:
			this.m_format = null;
			this.ᜁ = null;
			this.ᜀ = null;
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x003EEB58 File Offset: 0x003EDB58
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			for (;;)
			{
				IL_14:
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5A:
					this.m_textBody.CloneRelationsTo(doc, nextOwner);
					num = 0;
					break;
				default:
					if (false)
					{
					}
					base.CloneRelationsTo(doc, nextOwner);
					if (true)
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
						if (this.m_textBody != null)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						goto IL_58;
					}
					goto IL_14;
				}
				IL_58:
				goto IL_5A;
			}
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x003EEBE0 File Offset: 0x003EDBE0
		public void AddItem(IParagraphBase paraItem)
		{
			Paragraph ownerParagraph;
			int num2;
			CommentMark commentMark;
			for (;;)
			{
				IL_00:
				switch (0)
				{
				default:
				{
					int num = 11;
					for (;;)
					{
						if (true)
						{
						}
						int a_;
						switch (num)
						{
						case 0:
							goto IL_2EF;
						case 1:
							if (paraItem.OwnerParagraph == null)
							{
								num = 21;
								continue;
							}
							num = 4;
							continue;
						case 2:
							num = 19;
							continue;
						case 3:
							if (this.ᜀ != null)
							{
								num = 13;
								continue;
							}
							goto IL_2B6;
						case 4:
							if (ownerParagraph.Items.Count > num2 + 1)
							{
								num = 2;
								continue;
							}
							goto IL_216;
						case 5:
							goto IL_16F;
						case 6:
							goto IL_EC;
						case 7:
							return;
						case 8:
							a_ = this.m_format.TagBkmk;
							num = 1;
							continue;
						case 9:
							if (commentMark != null)
							{
								num = 17;
								continue;
							}
							goto IL_14A;
						case 10:
							if (!this.ᜀ.Contains(paraItem))
							{
								goto IL_2B6;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								num = 20;
								continue;
							}
							break;
						case 12:
							goto IL_145;
						case 13:
							num = 10;
							continue;
						case 14:
							if (ownerParagraph.Items[num2 - 1] is CommentMark)
							{
								num = 8;
								continue;
							}
							return;
						case 15:
							if (this.m_format.TagBkmk == -1)
							{
								num = 18;
								continue;
							}
							goto IL_2EF;
						case 16:
							if (paraItem == ownerParagraph.Items[commentMark.ឯ() - 1])
							{
								num = 6;
								continue;
							}
							goto IL_14A;
						case 17:
							num = 16;
							continue;
						case 18:
						{
							int num3 = spr\u180D.ᜃ().Next();
							this.m_format.TagBkmk = num3;
							CommentMark commentMark2 = new CommentMark(this.m_doc, num3);
							commentMark2.Type = CommentMarkType.CommentStart;
							CommentMark commentMark3 = new CommentMark(this.m_doc, num3);
							commentMark3.Type = CommentMarkType.CommentEnd;
							ownerParagraph.Items.Insert(num2, commentMark3);
							ownerParagraph.Items.Insert(num2, commentMark2);
							num = 0;
							continue;
						}
						case 19:
							if (paraItem == ownerParagraph.Items[num2 + 1])
							{
								num = 12;
								continue;
							}
							goto IL_216;
						case 20:
							return;
						case 21:
							goto IL_1A4;
						}
						if (base.OwnerParagraph == null)
						{
							num = 7;
							continue;
						}
						num = 3;
						continue;
						IL_14A:
						ParagraphBase a_2 = paraItem.Clone() as ParagraphBase;
						this.ᜀ(ownerParagraph, num2 - 1, a_2);
						num = 5;
						continue;
						IL_216:
						commentMark = this.ᜀ(num2, a_, ownerParagraph.Items);
						num = 9;
						continue;
						IL_2B6:
						ownerParagraph = base.OwnerParagraph;
						num2 = base.ឯ();
						num = 15;
						continue;
						IL_2EF:
						num2 = base.ឯ();
						num = 14;
					}
					break;
				}
				}
			}
			return;
			IL_EC:
			int num4 = commentMark.ឯ();
			ownerParagraph.Items.RemoveAt(num4 - 1);
			this.ᜀ(ownerParagraph, num4, paraItem);
			return;
			IL_145:
			ownerParagraph.Items.RemoveAt(num2 + 1);
			this.ᜀ(ownerParagraph, num2 - 1, paraItem);
			return;
			IL_16F:
			return;
			IL_1A4:
			this.ᜀ(ownerParagraph, num2 - 1, paraItem);
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x003EEF54 File Offset: 0x003EDF54
		protected override void InitXDLSHolder()
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.XDLSHolder.AddElement(ClipboardData.b("୨Ѫ६᙮", a_), this.m_textBody);
			base.XDLSHolder.AddElement(ClipboardData.b("੨ѪlɮᑰᵲŴ婶ὸᑺོቾ", a_), this.m_format);
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x003EEFD8 File Offset: 0x003EDFD8
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 3;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.WriteXmlAttributes(writer);
			writer.WriteValue(ClipboardData.b("ᵨቪᵬ੮", a_), ParagraphItemType.Comment);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x003EF040 File Offset: 0x003EE040
		protected override void CreateLayoutInfo()
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
			this.ᜀ = new spr\u22A8();
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x003EF088 File Offset: 0x003EE088
		private new void ᜀ(Paragraph A_0, int A_1, IParagraphBase A_2)
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
			A_0.Items.Insert(A_1, A_2);
			(A_2 as ParagraphBase).ᜀ(A_0);
			this.Items.Add(A_2);
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x003EF0EC File Offset: 0x003EE0EC
		private new CommentMark ᜀ(int A_0, int A_1, ParagraphItemCollection A_2)
		{
			switch (0)
			{
			default:
			{
				CommentMark result;
				for (;;)
				{
					ParagraphBase paragraphBase = null;
					result = null;
					int num = A_0;
					int num2 = 6;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							num2 = 4;
							continue;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_58;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								if (num <= 0)
								{
									num2 = 9;
									continue;
								}
								paragraphBase = A_2[num];
								num2 = 7;
								continue;
							}
							break;
						case 2:
							return result;
						case 3:
						{
							CommentMark commentMark;
							result = commentMark;
							num2 = 2;
							continue;
						}
						case 4:
						{
							CommentMark commentMark;
							if (commentMark.CommentId == A_1)
							{
								num2 = 3;
								continue;
							}
							goto IL_54;
						}
						case 5:
						{
							CommentMark commentMark = paragraphBase as CommentMark;
							num2 = 10;
							continue;
						}
						case 6:
							goto IL_AD;
						case 7:
							if (paragraphBase is CommentMark)
							{
								num2 = 5;
								continue;
							}
							goto IL_54;
						case 8:
							goto IL_AD;
						case 9:
							return result;
						case 10:
						{
							CommentMark commentMark;
							if (commentMark.Type == CommentMarkType.CommentStart)
							{
								num2 = 0;
								continue;
							}
							goto IL_54;
						}
						}
						break;
						IL_58:
						num2 = 8;
						continue;
						IL_54:
						num--;
						goto IL_58;
						IL_AD:
						num2 = 1;
					}
				}
				return result;
			}
			}
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x003EF234 File Offset: 0x003EE234
		private new bool ᜀ(BodyRegion A_0)
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
				if (!(A_0 is Paragraph))
				{
					return this.ᜁ(A_0 as Table);
				}
				break;
			}
			if (true)
			{
			}
			return this.ᜁ(A_0 as Paragraph);
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x003EF294 File Offset: 0x003EE294
		private new bool ᜁ(Paragraph A_0)
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
				switch (0)
				{
				}
				break;
			}
			IEnumerator enumerator = A_0.Items.GetEnumerator();
			bool result;
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D6;
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
						ParagraphBase paragraphBase = (ParagraphBase)enumerator.Current;
						num = 3;
						continue;
					}
					case 3:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase is Comment)
						{
							num = 5;
							continue;
						}
						break;
					}
					case 5:
						result = false;
						num = 6;
						continue;
					case 6:
						goto IL_CB;
					}
					IL_7C:
					num = 2;
					continue;
					goto IL_7C;
				}
				IL_CB:
				return result;
				IL_D6:
				return true;
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
							goto IL_11F;
						case 2:
							goto IL_11D;
						}
						break;
					}
				}
				IL_11D:
				IL_11F:;
			}
			return result;
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x003EF3D4 File Offset: 0x003EE3D4
		private new bool ᜁ(Table A_0)
		{
			switch (0)
			{
			default:
			{
				bool flag = true;
				IEnumerator enumerator = A_0.Rows.GetEnumerator();
				bool result;
				try
				{
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
							goto IL_2DD;
						case 2:
							try
							{
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										try
										{
											num = 9;
											for (;;)
											{
												switch (num)
												{
												case 0:
													result = false;
													num = 1;
													continue;
												case 1:
													goto IL_1D6;
												case 2:
													num = 10;
													continue;
												case 3:
													if (!flag)
													{
														num = 0;
														continue;
													}
													break;
												case 4:
												{
													IDocumentObject documentObject;
													flag = this.ᜁ(documentObject as Paragraph);
													num = 5;
													continue;
												}
												case 5:
													goto IL_1AA;
												case 6:
													goto IL_1AA;
												case 7:
												{
													IEnumerator enumerator2;
													if (!enumerator2.MoveNext())
													{
														num = 2;
														continue;
													}
													IDocumentObject documentObject = (IDocumentObject)enumerator2.Current;
													num = 8;
													continue;
												}
												case 8:
												{
													IDocumentObject documentObject;
													if (documentObject is Paragraph)
													{
														num = 4;
														continue;
													}
													flag = this.ᜁ(documentObject as Table);
													num = 6;
													continue;
												}
												case 10:
													goto IL_20D;
												}
												goto IL_149;
												IL_1AA:
												num = 3;
												continue;
												IL_1DB:
												num = 7;
												continue;
												IL_149:
												goto IL_1DB;
											}
											IL_1D6:
											return result;
											IL_20D:
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
														goto IL_258;
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
														goto IL_25A;
													}
													break;
												}
											}
											IL_258:
											IL_25A:;
										}
										goto IL_25B;
									case 1:
										goto IL_25B;
									case 3:
									{
										IEnumerator enumerator3;
										if (!enumerator3.MoveNext())
										{
											num = 1;
											continue;
										}
										TableCell tableCell = (TableCell)enumerator3.Current;
										IEnumerator enumerator2 = tableCell.ChildObjects.GetEnumerator();
										num = 0;
										continue;
									}
									case 4:
										goto IL_267;
									}
									IL_EB:
									num = 3;
									continue;
									goto IL_EB;
									IL_25B:
									num = 4;
								}
								IL_267:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator3;
									IDisposable disposable2 = enumerator3 as IDisposable;
									num = 0;
									for (;;)
									{
										switch (num)
										{
										case 0:
											goto IL_293;
										case 1:
											disposable2.Dispose();
											num = 2;
											continue;
										case 2:
											switch ((1 == 1) ? 1 : 0)
											{
											case 0:
											case 2:
												goto IL_293;
											default:
												goto IL_2C8;
											}
											break;
										}
										break;
										IL_293:
										if (disposable2 == null)
										{
											goto IL_2D0;
										}
										num = 1;
									}
								}
								IL_2C8:
								if (false)
								{
								}
								IL_2D0:;
							}
							goto IL_2D1;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							IEnumerator enumerator3 = tableRow.Cells.GetEnumerator();
							num = 2;
							continue;
						}
						case 4:
							goto IL_2D1;
						}
						IL_56:
						num = 3;
						continue;
						goto IL_56;
						IL_2D1:
						num = 1;
					}
					IL_2DD:
					return flag;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable3 = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable3.Dispose();
								num = 1;
								continue;
							case 1:
								goto IL_328;
							case 2:
								if (disposable3 != null)
								{
									num = 0;
									continue;
								}
								goto IL_32A;
							}
							break;
						}
					}
					IL_328:
					IL_32A:
					if (true)
					{
					}
				}
				return result;
			}
			}
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x003EF764 File Offset: 0x003EE764
		private new void ᜁ(ParagraphBase A_0, ParagraphBase A_1)
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_178;
					case 1:
					{
						Table table;
						if (A_0.NextSibling != table)
						{
							num = 13;
							continue;
						}
						return;
					}
					case 3:
					{
						if (A_0.OwnerParagraph.NextSibling == null)
						{
							num = 17;
							continue;
						}
						Paragraph ownerParagraph = A_0.OwnerParagraph;
						Paragraph ownerParagraph2 = A_1.OwnerParagraph;
						Table table = null;
						Table table2 = null;
						num = 8;
						continue;
					}
					case 4:
						num = 18;
						continue;
					case 5:
						goto IL_173;
					case 6:
					{
						if (true)
						{
						}
						Paragraph ownerParagraph2;
						Table table2 = (ownerParagraph2.Owner as TableCell).OwnerRow.OwnerTable;
						num = 11;
						continue;
					}
					case 7:
					{
						Paragraph ownerParagraph2;
						if (ownerParagraph2.Owner is TableCell)
						{
							goto IL_194;
						}
						goto IL_FB;
					}
					case 8:
						if (base.OwnerParagraph.Owner is TableCell)
						{
							num = 14;
							continue;
						}
						goto IL_178;
					case 9:
					{
						Table table2;
						if (A_0.NextSibling == table2)
						{
							num = 15;
							continue;
						}
						(A_0.NextSibling as BodyRegion).RemoveSelf();
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_194;
						default:
							if (false)
							{
							}
							num = 16;
							continue;
						}
						break;
					}
					case 10:
					{
						Paragraph ownerParagraph;
						if (ownerParagraph.NextSibling != A_1.OwnerParagraph)
						{
							num = 4;
							continue;
						}
						return;
					}
					case 11:
						goto IL_FB;
					case 12:
						num = 3;
						continue;
					case 13:
						num = 9;
						continue;
					case 14:
					{
						Table table = (base.OwnerParagraph.Owner as TableCell).OwnerRow.OwnerTable;
						num = 0;
						continue;
					}
					case 15:
						return;
					case 16:
						goto IL_FB;
					case 17:
						goto IL_1D4;
					case 18:
					{
						Paragraph ownerParagraph;
						if (ownerParagraph.NextSibling == null)
						{
							num = 5;
							continue;
						}
						num = 1;
						continue;
					}
					}
					if (A_0.OwnerParagraph.NextSibling != A_1.OwnerParagraph)
					{
						num = 12;
						continue;
					}
					break;
					IL_FB:
					num = 10;
					continue;
					IL_178:
					num = 7;
					continue;
					IL_194:
					num = 6;
				}
				return;
				IL_173:
				return;
				IL_1D4:
				return;
			}
			}
		}

		// Token: 0x06004332 RID: 17202 RVA: 0x003EF9E4 File Offset: 0x003EE9E4
		private new void ᜀ(ParagraphBase A_0, ParagraphBase A_1)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_D9;
			}
			if (false)
			{
			}
			Paragraph ownerParagraph;
			switch (0)
			{
			default:
			{
				Table table;
				for (;;)
				{
					ownerParagraph = A_0.OwnerParagraph;
					int num = ownerParagraph.ឯ();
					int num2 = 8;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_202;
						case 1:
							if (ownerParagraph.Owner is TableCell)
							{
								num2 = 3;
								continue;
							}
							goto IL_202;
						case 2:
							num2 = 15;
							continue;
						case 3:
							table = (ownerParagraph.Owner as TableCell).OwnerRow.OwnerTable;
							num2 = 0;
							continue;
						case 4:
						{
							Paragraph ownerParagraph2;
							if (ownerParagraph2.Owner is TableCell)
							{
								num2 = 6;
								continue;
							}
							goto IL_1D9;
						}
						case 5:
							goto IL_1D0;
						case 6:
						{
							Paragraph ownerParagraph2;
							Table table2 = (ownerParagraph2.Owner as TableCell).OwnerRow.OwnerTable;
							if (true)
							{
							}
							num2 = 10;
							continue;
						}
						case 7:
							goto IL_A4;
						case 8:
							if (num > 0)
							{
								num2 = 7;
								continue;
							}
							table = null;
							num2 = 1;
							continue;
						case 9:
							num2 = 14;
							continue;
						case 10:
							goto IL_1D9;
						case 11:
							goto IL_13B;
						case 12:
						{
							Table table2;
							if (table != table2)
							{
								num2 = 9;
								continue;
							}
							goto IL_222;
						}
						case 13:
						{
							if (table == null)
							{
								num2 = 2;
								continue;
							}
							Paragraph ownerParagraph2 = A_1.OwnerParagraph;
							Table table2 = null;
							num2 = 4;
							continue;
						}
						case 14:
							if (ownerParagraph.Owner == table.FirstRow.Cells[0])
							{
								num2 = 11;
								continue;
							}
							goto IL_222;
						case 15:
							if (ownerParagraph.Items.Count > 1)
							{
								num2 = 5;
								continue;
							}
							goto IL_D9;
						}
						break;
						IL_1D9:
						num2 = 12;
						continue;
						IL_202:
						num2 = 13;
					}
				}
				IL_A4:
				A_0.RemoveSelf();
				return;
				IL_13B:
				table.RemoveSelf();
				return;
				IL_1D0:
				A_0.RemoveSelf();
				return;
				IL_222:
				A_0.RemoveSelf();
				return;
			}
			}
			IL_D9:
			ownerParagraph.RemoveSelf();
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x003EFC1C File Offset: 0x003EEC1C
		private new void ᜀ()
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
				IEnumerator enumerator = this.ᜁ.BodyItems.GetEnumerator();
				try
				{
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 2:
							num = 5;
							continue;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							BodyRegion bodyRegion = (BodyRegion)enumerator.Current;
							num = 4;
							continue;
						}
						case 4:
						{
							BodyRegion bodyRegion;
							if (bodyRegion is Paragraph)
							{
								num = 6;
								continue;
							}
							this.ᜀ(bodyRegion as Table);
							num = 1;
							continue;
						}
						case 5:
							goto IL_E5;
						case 6:
						{
							BodyRegion bodyRegion;
							this.ᜀ(bodyRegion as Paragraph);
							num = 0;
							continue;
						}
						}
						IL_91:
						num = 3;
						continue;
						goto IL_91;
					}
					IL_E5:;
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
								disposable.Dispose();
								if (true)
								{
								}
								num = 1;
								continue;
							case 1:
								goto IL_12D;
							case 2:
								if (disposable != null)
								{
									num = 0;
									continue;
								}
								goto IL_12F;
							}
							break;
						}
					}
					IL_12D:
					IL_12F:;
				}
				break;
			}
			}
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x003EFD6C File Offset: 0x003EED6C
		private new void ᜀ(Paragraph A_0)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				return;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			IEnumerator enumerator = A_0.Items.GetEnumerator();
			try
			{
				int num = 0;
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
						ParagraphBase entity = (ParagraphBase)enumerator.Current;
						this.Items.Add(entity);
						num = 2;
						continue;
					}
					case 3:
						goto IL_A7;
					case 4:
						num = 3;
						continue;
					}
					IL_85:
					num = 1;
					continue;
					goto IL_85;
				}
				IL_A7:;
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
							disposable.Dispose();
							num = 1;
							continue;
						case 1:
							goto IL_E7;
						case 2:
							if (disposable != null)
							{
								num = 0;
								continue;
							}
							goto IL_E9;
						}
						break;
					}
				}
				IL_E7:
				IL_E9:;
			}
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x003EFE74 File Offset: 0x003EEE74
		private new void ᜀ(Table A_0)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = A_0.Rows.GetEnumerator();
				try
				{
					int num = 1;
					for (;;)
					{
						switch (num)
						{
						case 0:
							goto IL_295;
						case 2:
							try
							{
								num = 4;
								for (;;)
								{
									IEnumerator enumerator2;
									IEnumerator enumerator3;
									switch (num)
									{
									case 0:
										goto IL_23B;
									case 1:
										try
										{
											num = 5;
											for (;;)
											{
												switch (num)
												{
												case 0:
													num = 4;
													continue;
												case 1:
												{
													if (!enumerator2.MoveNext())
													{
														num = 0;
														continue;
													}
													IDocumentObject documentObject = (IDocumentObject)enumerator2.Current;
													num = 2;
													continue;
												}
												case 2:
												{
													IDocumentObject documentObject;
													if (documentObject is Paragraph)
													{
														num = 7;
														continue;
													}
													this.ᜀ(documentObject as Table);
													num = 3;
													continue;
												}
												case 4:
													goto IL_19A;
												case 7:
												{
													IDocumentObject documentObject;
													this.ᜀ(documentObject as Paragraph);
													num = 6;
													continue;
												}
												}
												IL_114:
												num = 1;
												continue;
												goto IL_114;
											}
											IL_19A:
											break;
										}
										finally
										{
											for (;;)
											{
												IDisposable disposable = enumerator2 as IDisposable;
												num = 2;
												for (;;)
												{
													switch (num)
													{
													case 0:
														goto IL_201;
													case 1:
														switch ((1 == 1) ? 1 : 0)
														{
														case 0:
														case 2:
															continue;
														default:
															if (false)
															{
															}
															disposable.Dispose();
															num = 0;
															continue;
														}
														break;
													case 2:
														if (disposable != null)
														{
															num = 1;
															continue;
														}
														goto IL_203;
													}
													break;
												}
											}
											IL_201:
											IL_203:;
										}
										goto IL_204;
									case 2:
										num = 0;
										continue;
									case 3:
										if (!enumerator3.MoveNext())
										{
											num = 2;
											continue;
										}
										goto IL_204;
									}
									IL_BD:
									num = 3;
									continue;
									goto IL_BD;
									IL_204:
									TableCell tableCell = (TableCell)enumerator3.Current;
									enumerator2 = tableCell.ChildObjects.GetEnumerator();
									num = 1;
								}
								IL_23B:
								break;
							}
							finally
							{
								for (;;)
								{
									IEnumerator enumerator3;
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
											goto IL_286;
										case 2:
											if (disposable2 != null)
											{
												num = 0;
												continue;
											}
											goto IL_288;
										}
										break;
									}
								}
								IL_286:
								IL_288:;
							}
							goto IL_289;
						case 3:
						{
							if (!enumerator.MoveNext())
							{
								num = 4;
								continue;
							}
							TableRow tableRow = (TableRow)enumerator.Current;
							IEnumerator enumerator3 = tableRow.Cells.GetEnumerator();
							num = 2;
							continue;
						}
						case 4:
							goto IL_289;
						}
						IL_73:
						num = 3;
						continue;
						goto IL_73;
						IL_289:
						num = 0;
					}
					IL_295:;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable3 = enumerator as IDisposable;
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
								disposable3.Dispose();
								num = 1;
								continue;
							case 1:
								goto IL_2DC;
							case 2:
								if (disposable3 != null)
								{
									num = 0;
									continue;
								}
								goto IL_2DE;
							}
							break;
						}
					}
					IL_2DC:
					IL_2DE:;
				}
				return;
			}
			}
		}

		// Token: 0x06004336 RID: 17206 RVA: 0x003F01B8 File Offset: 0x003EF1B8
		private new string ᜀ(string A_0)
		{
			int a_ = 6;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			A_0 = A_0.Replace(ClipboardData.b("慫摭", a_), ClipboardData.b("慫", a_));
			A_0 = A_0.Replace('\n', '\r');
			return A_0;
		}

		// Token: 0x04003560 RID: 13664
		protected Body m_textBody;

		// Token: 0x04003561 RID: 13665
		private byte[] \u2609\u0099\u0093\u0080;

		// Token: 0x04003562 RID: 13666
		private bool[] \u2460\u008B\u008D\u00B0;

		// Token: 0x04003563 RID: 13667
		private string \u2593\u009A\u0091\u00A2;

		// Token: 0x04003564 RID: 13668
		protected CommentFormat m_format;

		// Token: 0x04003565 RID: 13669
		private string[] \u2593\u0081\u009A\u00A2;

		// Token: 0x04003566 RID: 13670
		private new ParagraphItemCollection ᜀ;

		// Token: 0x04003567 RID: 13671
		private long \u2609\u008B\u008A\u00AE;

		// Token: 0x04003568 RID: 13672
		private new TextBodyPart ᜁ;

		// Token: 0x04003569 RID: 13673
		private string \u2609\u009E\u0095\u0094;

		// Token: 0x0400356A RID: 13674
		private bool ᜂ;
	}
}
