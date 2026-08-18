using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Spire.CompoundFile.Doc;
using Spire.Doc.Collections;
using Spire.Doc.Fields;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc.Documents
{
	// Token: 0x020004EE RID: 1262
	public class Paragraph : BodyRegion, spr\u17C8, IParagraph
	{
		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06004122 RID: 16674 RVA: 0x003D9558 File Offset: 0x003D8558
		// (set) Token: 0x06004123 RID: 16675 RVA: 0x003D959C File Offset: 0x003D859C
		internal bool IsStyleApplied
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
				return this.ᜊ;
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
				this.ᜊ = value;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06004124 RID: 16676 RVA: 0x003D95E0 File Offset: 0x003D85E0
		public override DocumentObjectType DocumentObjectType
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
				return DocumentObjectType.Paragraph;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06004125 RID: 16677 RVA: 0x003D961C File Offset: 0x003D861C
		public DocumentObjectCollection ChildObjects
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
				return this.m_pItemColl;
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06004126 RID: 16678 RVA: 0x003D9660 File Offset: 0x003D8660
		public string StyleName
		{
			get
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
					if (this.m_style != null)
					{
						return this.m_style.Name;
					}
					if (true)
					{
					}
					break;
				}
				return null;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06004127 RID: 16679 RVA: 0x003D96B4 File Offset: 0x003D86B4
		// (set) Token: 0x06004128 RID: 16680 RVA: 0x003D96FC File Offset: 0x003D86FC
		public string Text
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
				return this.ᜃ.ToString();
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
				this.Items.Clear();
				ITextRange textRange = this.AppendText(value);
				textRange.CharacterFormat.ImportContainer(this.BreakCharacterFormat);
			}
		}

		// Token: 0x170003F9 RID: 1017
		public ParagraphBase this[int index]
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
				return this.m_pItemColl[index];
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x0600412A RID: 16682 RVA: 0x003D97A4 File Offset: 0x003D87A4
		public ParagraphItemCollection Items
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
				return this.m_pItemColl;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x0600412B RID: 16683 RVA: 0x003D97E8 File Offset: 0x003D87E8
		public ParagraphFormat Format
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
				return this.m_prFormat;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x0600412C RID: 16684 RVA: 0x003D982C File Offset: 0x003D882C
		public CharacterFormat BreakCharacterFormat
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
				return this.ᜇ;
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600412D RID: 16685 RVA: 0x003D9870 File Offset: 0x003D8870
		public ListFormat ListFormat
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_66;
					case 2:
						for (;;)
						{
							this.m_listFormat = new ListFormat(this);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_58;
							}
						}
						IL_58:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					if (true)
					{
					}
					if (this.m_listFormat != null)
					{
						break;
					}
					num = 2;
				}
				IL_66:
				return this.m_listFormat;
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600412E RID: 16686 RVA: 0x003D98F8 File Offset: 0x003D88F8
		public bool IsInCell
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
				return base.Owner is TableCell;
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600412F RID: 16687 RVA: 0x003D9944 File Offset: 0x003D8944
		public bool IsEndOfSection
		{
			get
			{
				while (base.Owner.Owner is Section)
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
						return base.NextSibling == null;
					}
				}
				return false;
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06004130 RID: 16688 RVA: 0x003D99A0 File Offset: 0x003D89A0
		public bool IsEndOfDocument
		{
			get
			{
				while (this.IsEndOfSection)
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
						return (base.Owner.Owner as Section).NextSibling == null;
					}
				}
				return false;
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06004131 RID: 16689 RVA: 0x003D9A00 File Offset: 0x003D8A00
		internal bool HasSDTInlineItem
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
				return this.ᜋ;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06004132 RID: 16690 RVA: 0x003D9A44 File Offset: 0x003D8A44
		int spr\u17C8.Count
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
				return this.WidgetCollection.Count;
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06004133 RID: 16691 RVA: 0x003D9A8C File Offset: 0x003D8A8C
		spr\u1AB8 spr\u17C8.Item
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
				return this.WidgetCollection[index] as spr\u1AB8;
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06004134 RID: 16692 RVA: 0x003D9AD8 File Offset: 0x003D8AD8
		internal IDocumentObjectCollection WidgetCollection
		{
			get
			{
				int num = 3;
				ParagraphItemCollection paragraphItemCollection;
				for (;;)
				{
					bool flag;
					bool flag2;
					switch (num)
					{
					case 0:
						if (this.m_pItemColl.Count > 1)
						{
							num = 2;
							continue;
						}
						num = 1;
						continue;
					case 1:
						flag = false;
						goto IL_138;
					case 2:
						num = 17;
						continue;
					case 4:
						goto IL_74;
					case 5:
						if (this.ᜋ)
						{
							num = 12;
							continue;
						}
						goto IL_24C;
					case 6:
						if (!flag2)
						{
							num = 8;
							continue;
						}
						return paragraphItemCollection;
					case 7:
						if ((this.m_pItemColl[this.m_pItemColl.Count - 1] as Break).BreakType == BreakType.LineBreak)
						{
							num = 13;
							continue;
						}
						goto IL_DC;
					case 8:
						paragraphItemCollection.InnerList.Add(this.ᜄ[0]);
						num = 16;
						continue;
					case 9:
						num = 7;
						continue;
					case 10:
						if (!this.ᜋ)
						{
							num = 14;
							continue;
						}
						goto IL_157;
					case 11:
						num = 10;
						continue;
					case 12:
						goto IL_FD;
					case 13:
						num = 0;
						continue;
					case 14:
						goto IL_23A;
					case 15:
						if (this.m_pItemColl[this.m_pItemColl.Count - 1] is Break)
						{
							num = 9;
							continue;
						}
						goto IL_DC;
					case 16:
						goto IL_125;
					case 17:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6C;
						default:
							if (false)
							{
							}
							flag = !(this.m_pItemColl[this.m_pItemColl.Count - 2] is Break);
							goto IL_138;
						}
						break;
					case 18:
						if (flag2)
						{
							num = 11;
							continue;
						}
						goto IL_157;
					}
					goto IL_5C;
					IL_6C:
					num = 4;
					continue;
					IL_5C:
					if (this.m_pItemColl.Count == 0)
					{
						goto IL_6C;
					}
					num = 15;
					continue;
					IL_DC:
					num = 5;
					continue;
					IL_138:
					flag2 = flag;
					num = 18;
					continue;
					IL_157:
					paragraphItemCollection = this.\u1712();
					num = 6;
				}
				IL_74:
				return this.ᜄ;
				IL_FD:
				return this.\u1712();
				IL_125:
				return paragraphItemCollection;
				IL_23A:
				return this.m_pItemColl;
				IL_24C:
				if (true)
				{
				}
				return this.m_pItemColl;
			}
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06004135 RID: 16693 RVA: 0x003D9D40 File Offset: 0x003D8D40
		// (set) Token: 0x06004136 RID: 16694 RVA: 0x003D9D84 File Offset: 0x003D8D84
		internal bool RemoveEmpty
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

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06004137 RID: 16695 RVA: 0x003D9DC8 File Offset: 0x003D8DC8
		internal ParagraphBase LastItem
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
				return this[this.m_pItemColl.Count - 1];
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06004138 RID: 16696 RVA: 0x003D9E18 File Offset: 0x003D8E18
		internal ParagraphStyle ParaStyle
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
				return this.m_style as ParagraphStyle;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06004139 RID: 16697 RVA: 0x003D9E60 File Offset: 0x003D8E60
		internal bool SectionEndMark
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
				return this.ᜁ();
			}
		}

		// Token: 0x0600413A RID: 16698 RVA: 0x003D9EA4 File Offset: 0x003D8EA4
		public Paragraph(IDocument doc)
		{
			int a_ = 7;
			this.ᜃ = new StringBuilder(1);
			base..ctor((Document)doc);
			this.m_pItemColl = new ParagraphItemCollection(this);
			this.ᜇ = new CharacterFormat(base.Document);
			this.m_prFormat = new ParagraphFormat(base.Document);
			this.m_listFormat = new ListFormat(this);
			this.ᜇ.ᜀ(this);
			this.m_prFormat.ᜀ(this);
			this.m_listFormat.ᜀ(this);
			this.ApplyStyle(ClipboardData.b("⍬nͰṲᑴ᭶", a_));
			this.ᜂ();
		}

		// Token: 0x0600413B RID: 16699 RVA: 0x003D9F4C File Offset: 0x003D8F4C
		public void ApplyStyle(string styleName)
		{
			int a_ = 4;
			ParagraphStyle paragraphStyle;
			for (;;)
			{
				this.IsStyleApplied = true;
				paragraphStyle = (base.Document.Styles.FindByName(styleName, StyleType.ParagraphStyle) as ParagraphStyle);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_61;
					case 1:
						paragraphStyle = (ParagraphStyle)Style.CreateBuiltinStyle(BuiltinStyle.Normal, base.Document);
						num = 0;
						continue;
					case 2:
						if (paragraphStyle == null)
						{
							num = 3;
							continue;
						}
						goto IL_61;
					case 3:
						num = 6;
						continue;
					case 4:
						if (paragraphStyle == null)
						{
							num = 5;
							continue;
						}
						goto IL_100;
					case 5:
						goto IL_77;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							continue;
						default:
							if (false)
							{
							}
							if (styleName == ClipboardData.b("⑩ͫᱭᵯ፱ᡳ", a_))
							{
								num = 1;
								continue;
							}
							goto IL_61;
						}
						break;
					}
					break;
					IL_61:
					num = 4;
				}
			}
			IL_77:
			throw new ArgumentException(ClipboardData.b("㩩൫ᱭѯ፱፳ѵ᥷੹ᑻ幽ﶃꪉﺏ늑望秊몙ﺛﮝ肟쒡쮣펥욧캩芫", a_));
			IL_100:
			if (true)
			{
			}
			this.ᜀ(paragraphStyle);
			this.ᜀ(paragraphStyle);
		}

		// Token: 0x0600413C RID: 16700 RVA: 0x003DA070 File Offset: 0x003D9070
		public void ApplyStyle(BuiltinStyle builtinStyle)
		{
			int a_ = 7;
			IStyle style;
			for (;;)
			{
				this.IsStyleApplied = true;
				bool flag = Style.ᜀ(builtinStyle);
				this.ᜃ();
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if ((style as ParagraphStyle).StyleId > 10)
						{
							num = 3;
							continue;
						}
						goto IL_89;
					case 1:
						style = (IParagraphStyle)Style.CreateBuiltinStyle(builtinStyle, base.Document);
						if (true)
						{
						}
						num = 0;
						continue;
					case 2:
						if (builtinStyle != BuiltinStyle.MacroText)
						{
							num = 6;
							continue;
						}
						goto IL_1AA;
					case 3:
						(style as ParagraphStyle).StyleId = 4094;
						num = 8;
						continue;
					case 4:
						goto IL_13C;
					case 5:
						(style as ParagraphStyle).ApplyBaseStyle(ClipboardData.b("⍬nͰṲᑴ᭶", a_));
						num = 4;
						continue;
					case 6:
						num = 7;
						continue;
					case 7:
						if (builtinStyle != BuiltinStyle.CommentSubject)
						{
							num = 5;
							continue;
						}
						goto IL_1AA;
					case 8:
						goto IL_89;
					case 9:
						goto IL_84;
					case 10:
						if (style == null)
						{
							num = 1;
							continue;
						}
						goto IL_1AA;
					case 11:
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
							if (flag)
							{
								num = 9;
								continue;
							}
							string name = Style.ᜁ(builtinStyle);
							style = (base.Document.Styles.FindByName(name, StyleType.ParagraphStyle) as IParagraphStyle);
							num = 10;
							continue;
						}
						}
						break;
					}
					break;
					IL_89:
					base.Document.Styles.Add(style);
					num = 2;
				}
			}
			IL_84:
			this.ᜀ(builtinStyle);
			return;
			IL_13C:
			IL_1AA:
			this.ᜀ(style as ParagraphStyle);
			this.ᜀ(style as Style);
		}

		// Token: 0x0600413D RID: 16701 RVA: 0x003DA24C File Offset: 0x003D924C
		private new void ᜀ(Style A_0)
		{
			int a_ = 9;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9D;
				case 1:
					return;
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
						num = 4;
						continue;
					}
					break;
				case 4:
					if (A_0.NextStyle == string.Empty)
					{
						num = 6;
						continue;
					}
					return;
				case 5:
					if (A_0.Name != ClipboardData.b("ⅮṰ卲♴ݶᡸ᡺ᑼᅾ", a_))
					{
						num = 0;
						continue;
					}
					goto IL_EF;
				case 6:
					goto IL_108;
				case 7:
					num = 5;
					continue;
				case 8:
					if (!A_0.Name.Contains(ClipboardData.b("⍮ᡰrŴ", a_)))
					{
						num = 7;
						continue;
					}
					goto IL_EF;
				}
				if (A_0.NextStyle != null)
				{
					num = 2;
					continue;
				}
				goto IL_108;
				IL_EF:
				A_0.NextStyle = A_0.Name;
				num = 1;
				continue;
				IL_108:
				num = 8;
			}
			IL_9D:
			if (true)
			{
			}
			A_0.NextStyle = ClipboardData.b("ⅮṰŲᡴᙶᕸ", a_);
		}

		// Token: 0x0600413E RID: 16702 RVA: 0x003DA398 File Offset: 0x003D9398
		public ParagraphStyle GetStyle()
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
			return this.m_style as ParagraphStyle;
		}

		// Token: 0x0600413F RID: 16703 RVA: 0x003DA3E0 File Offset: 0x003D93E0
		public void RemoveAbsPosition()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					for (;;)
					{
						this.m_prFormat.RemovePositioning();
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
					if (true)
					{
					}
					num = 2;
					continue;
				case 2:
					return;
				}
				if (this.m_prFormat == null)
				{
					break;
				}
				num = 0;
			}
		}

		// Token: 0x06004140 RID: 16704 RVA: 0x003DA460 File Offset: 0x003D9460
		public TextRange AppendText(string text)
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
			TextRange textRange = this.ᜀ(ParagraphItemType.TextRange) as TextRange;
			textRange.Text = text;
			return textRange;
		}

		// Token: 0x06004141 RID: 16705 RVA: 0x003DA4B0 File Offset: 0x003D94B0
		public DocPicture AppendPicture(byte[] imageBytes)
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
			DocPicture docPicture = (DocPicture)this.ᜀ(ParagraphItemType.Picture);
			docPicture.LoadImage(imageBytes);
			base.Document.HasPicture = true;
			return docPicture;
		}

		// Token: 0x06004142 RID: 16706 RVA: 0x003DA50C File Offset: 0x003D950C
		public Field AppendField(string fieldName, FieldType fieldType)
		{
			int a_ = 5;
			switch (0)
			{
			default:
			{
				int num = 40;
				Field field;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (fieldType == FieldType.FieldFormTextInput)
						{
							num = 15;
							continue;
						}
						num = 21;
						continue;
					case 1:
						if (fieldType == FieldType.FieldFormDropDown)
						{
							num = 24;
							continue;
						}
						num = 0;
						continue;
					case 2:
						goto IL_194;
					case 3:
						goto IL_2A6;
					case 4:
						num = 9;
						continue;
					case 5:
						if (fieldName.IndexOf(' ') != -1)
						{
							num = 36;
							continue;
						}
						goto IL_3C1;
					case 6:
						field = new MergeField(base.Document)
						{
							FieldName = fieldName
						};
						num = 7;
						continue;
					case 7:
						goto IL_447;
					case 8:
						fieldName = fieldName.Replace(ClipboardData.b("䭪", a_), string.Empty);
						fieldName = fieldName.Replace(ClipboardData.b("䥪", a_), string.Empty);
						fieldName = fieldName.Replace(ClipboardData.b("噪", a_), string.Empty);
						num = 22;
						continue;
					case 9:
						if (field.Type != FieldType.FieldNext)
						{
							num = 41;
							continue;
						}
						goto IL_14A;
					case 10:
						if (field.Type == FieldType.FieldFormula)
						{
							num = 8;
							continue;
						}
						goto IL_208;
					case 11:
						goto IL_279;
					case 12:
						if (fieldType == FieldType.FieldFormCheckBox)
						{
							num = 18;
							continue;
						}
						num = 1;
						continue;
					case 13:
						if (fieldType != FieldType.FieldSequence)
						{
							num = 17;
							continue;
						}
						goto IL_279;
					case 14:
						if (fieldType != FieldType.FieldMergeField)
						{
							num = 32;
							continue;
						}
						return field;
					case 15:
						goto IL_2FC;
					case 16:
						if (true)
						{
						}
						if (fieldType == FieldType.FieldSequence)
						{
							num = 35;
							continue;
						}
						field = new Field(base.Document);
						num = 28;
						continue;
					case 17:
					{
						TextRange textRange = new TextRange(base.Document);
						textRange.Text = fieldName;
						this.m_pItemColl.Add(textRange);
						num = 37;
						continue;
					}
					case 18:
						goto IL_52E;
					case 19:
						if (fieldType == FieldType.FieldMergeField)
						{
							num = 6;
							continue;
						}
						num = 16;
						continue;
					case 20:
						if (fieldType == FieldType.FieldDocVariable)
						{
							num = 31;
							continue;
						}
						goto IL_4D7;
					case 21:
						if (fieldType == FieldType.FieldIndexEntry)
						{
							num = 23;
							continue;
						}
						num = 19;
						continue;
					case 22:
						goto IL_208;
					case 23:
						goto IL_252;
					case 24:
						goto IL_18F;
					case 25:
					{
						TextRange textRange;
						textRange.CharacterFormat.TextColor = Color.Blue;
						textRange.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
						num = 11;
						continue;
					}
					case 26:
						goto IL_4D7;
					case 27:
						goto IL_14A;
					case 28:
						goto IL_447;
					case 29:
						if (field.Type != FieldType.FieldMergeField)
						{
							num = 4;
							continue;
						}
						goto IL_14A;
					case 30:
						goto IL_447;
					case 31:
						field.m_formattingString = ClipboardData.b("㝪䝬佮㱰㙲❴ぶ㱸㵺㉼⵾첀슂톄", a_);
						num = 26;
						continue;
					case 32:
						num = 33;
						continue;
					case 33:
						if (fieldType != FieldType.FieldNext)
						{
							num = 42;
							continue;
						}
						return field;
					case 34:
						field.m_fieldValue = ClipboardData.b("䥪", a_) + fieldName + ClipboardData.b("䥪", a_);
						num = 2;
						continue;
					case 35:
						field = new SequenceField(base.Document);
						num = 30;
						continue;
					case 36:
						num = 39;
						continue;
					case 37:
						if (fieldType == FieldType.FieldHyperlink)
						{
							num = 25;
							continue;
						}
						goto IL_279;
					case 38:
						goto IL_EE;
					case 39:
						if (field.Type != FieldType.FieldIndex)
						{
							num = 34;
							continue;
						}
						goto IL_3C1;
					case 41:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_43F;
						default:
							if (false)
							{
							}
							field.Separator = this.ᜀ(FieldMarkType.FieldSeparator);
							num = 27;
							continue;
						}
						break;
					case 42:
						num = 13;
						continue;
					case 43:
						goto IL_194;
					}
					if (fieldName == null)
					{
						num = 38;
						continue;
					}
					num = 12;
					continue;
					IL_14A:
					num = 14;
					continue;
					IL_194:
					num = 20;
					continue;
					IL_208:
					num = 5;
					continue;
					IL_279:
					FieldMark fieldMark = new FieldMark(base.Document, FieldMarkType.FieldEnd);
					this.m_pItemColl.Add(fieldMark);
					field.End = fieldMark;
					num = 3;
					continue;
					IL_3C1:
					field.m_fieldValue = fieldName;
					num = 43;
					continue;
					IL_447:
					field.Type = fieldType;
					num = 10;
					continue;
					IL_4D7:
					this.m_pItemColl.Add(field);
					num = 29;
				}
				IL_EE:
				throw new ArgumentNullException(ClipboardData.b("൪Ѭ੮ᵰᝲ㭴ᙶᑸṺ", a_));
				IL_18F:
				return this.AppendDropDownFormField(fieldName);
				IL_252:
				goto IL_43F;
				IL_2A6:
				return field;
				IL_2FC:
				return this.AppendTextFormField(fieldName, fieldName);
				IL_43F:
				return this.ᜁ(fieldName);
				IL_52E:
				return this.ᜀ(fieldName, false);
			}
			}
		}

		// Token: 0x06004143 RID: 16707 RVA: 0x003DAAB4 File Offset: 0x003D9AB4
		public Field AppendHyperlink(string link, string text, HyperlinkType type)
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
			return this.ᜀ(link, text, null, type);
		}

		// Token: 0x06004144 RID: 16708 RVA: 0x003DAAFC File Offset: 0x003D9AFC
		public Field AppendHyperlink(string link, DocPicture picture, HyperlinkType type)
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
			return this.ᜀ(link, null, picture, type);
		}

		// Token: 0x06004145 RID: 16709 RVA: 0x003DAB44 File Offset: 0x003D9B44
		public BookmarkStart AppendBookmarkStart(string name)
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
			BookmarkStart bookmarkStart = new BookmarkStart(base.Document, name);
			this.Items.Add(bookmarkStart);
			return bookmarkStart;
		}

		// Token: 0x06004146 RID: 16710 RVA: 0x003DAB9C File Offset: 0x003D9B9C
		public BookmarkEnd AppendBookmarkEnd(string name)
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
			BookmarkEnd bookmarkEnd = new BookmarkEnd(base.Document, name);
			this.Items.Add(bookmarkEnd);
			return bookmarkEnd;
		}

		// Token: 0x06004147 RID: 16711 RVA: 0x003DABF4 File Offset: 0x003D9BF4
		public Comment AppendComment(string text)
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
			Comment comment = (Comment)this.ᜀ(ParagraphItemType.Comment);
			IParagraph paragraph = comment.Body.AddParagraph();
			paragraph.AppendText(text);
			return comment;
		}

		// Token: 0x06004148 RID: 16712 RVA: 0x003DAC54 File Offset: 0x003D9C54
		public Footnote AppendFootnote(FootnoteType type)
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
			Footnote footnote = (Footnote)this.ᜀ(ParagraphItemType.Footnote);
			footnote.FootnoteType = type;
			footnote.ᜂ();
			return footnote;
		}

		// Token: 0x06004149 RID: 16713 RVA: 0x003DACAC File Offset: 0x003D9CAC
		public TextBox AppendTextBox(float width, float height)
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
			TextBox textBox = this.ᜀ(ParagraphItemType.TextBox) as TextBox;
			textBox.Format.Width = width;
			textBox.Format.Height = height;
			return textBox;
		}

		// Token: 0x0600414A RID: 16714 RVA: 0x003DAD10 File Offset: 0x003D9D10
		public CheckBoxFormField AppendCheckBox()
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
			return this.ᜇ();
		}

		// Token: 0x0600414B RID: 16715 RVA: 0x003DAD54 File Offset: 0x003D9D54
		public CheckBoxFormField AppendCheckBox(string checkBoxName, bool defaultCheckBoxValue)
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
			return this.ᜀ(checkBoxName, defaultCheckBoxValue);
		}

		// Token: 0x0600414C RID: 16716 RVA: 0x003DAD98 File Offset: 0x003D9D98
		internal CheckBoxFormField ᜇ()
		{
			int a_ = 18;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string text = ClipboardData.b("㭷ቹ᥻ᵽ\udd81", a_) + Guid.NewGuid().ToString().Replace(ClipboardData.b("啷", a_), ClipboardData.b("❷", a_));
			text = text.Substring(0, 20);
			return this.AppendCheckBox(text, false);
		}

		// Token: 0x0600414D RID: 16717 RVA: 0x003DAE38 File Offset: 0x003D9E38
		internal new CheckBoxFormField ᜀ(string A_0, bool A_1)
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
			CheckBoxFormField checkBoxFormField = base.Document.CreateParagraphItem(ParagraphItemType.CheckBox) as CheckBoxFormField;
			checkBoxFormField.Name = A_0;
			checkBoxFormField.DefaultCheckBoxValue = A_1;
			this.Items.Add(checkBoxFormField);
			return checkBoxFormField;
		}

		// Token: 0x0600414E RID: 16718 RVA: 0x003DAEA4 File Offset: 0x003D9EA4
		public TextFormField AppendTextFormField(string defaultText)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string text = ClipboardData.b("㩭ᕯੱs⥵", a_) + Guid.NewGuid().ToString().Replace(ClipboardData.b("䍭", a_), ClipboardData.b("ㅭ", a_));
			text = text.Substring(0, 20);
			return this.AppendTextFormField(text, defaultText);
		}

		// Token: 0x0600414F RID: 16719 RVA: 0x003DAF44 File Offset: 0x003D9F44
		public TextFormField AppendTextFormField(string formFieldName, string defaultText)
		{
			int a_ = 13;
			TextFormField textFormField;
			for (;;)
			{
				IL_21:
				textFormField = (base.Document.CreateParagraphItem(ParagraphItemType.TextFormField) as TextFormField);
				textFormField.Name = formFieldName;
				for (;;)
				{
					IL_3A:
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
								goto IL_3A;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								textFormField.DefaultText = ClipboardData.b("煒睔畖筘祚", a_);
								num = 2;
								continue;
							}
							break;
						case 1:
							if (defaultText == null)
							{
								num = 0;
								continue;
							}
							textFormField.DefaultText = defaultText;
							num = 3;
							continue;
						case 2:
							goto IL_B0;
						case 3:
							goto IL_8F;
						}
						goto IL_21;
					}
				}
			}
			IL_8F:
			IL_B0:
			this.Items.Add(textFormField);
			return textFormField;
		}

		// Token: 0x06004150 RID: 16720 RVA: 0x003DB014 File Offset: 0x003DA014
		public DropDownFormField AppendDropDownFormField()
		{
			int a_ = 2;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			string text = ClipboardData.b("Ⱨᡩͫṭ⽯", a_) + Guid.NewGuid().ToString().Replace(ClipboardData.b("䕧", a_), ClipboardData.b("㝧", a_));
			text = text.Substring(0, 20);
			return this.AppendDropDownFormField(text);
		}

		// Token: 0x06004151 RID: 16721 RVA: 0x003DB0B4 File Offset: 0x003DA0B4
		public DropDownFormField AppendDropDownFormField(string dropDropDownName)
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
			DropDownFormField dropDownFormField = base.Document.CreateParagraphItem(ParagraphItemType.DropDownFormField) as DropDownFormField;
			dropDownFormField.Name = dropDropDownName;
			this.Items.Add(dropDownFormField);
			return dropDownFormField;
		}

		// Token: 0x06004152 RID: 16722 RVA: 0x003DB118 File Offset: 0x003DA118
		public Symbol AppendSymbol(byte characterCode)
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
			Symbol symbol = (Symbol)this.ᜀ(ParagraphItemType.Symbol);
			symbol.CharacterCode = characterCode;
			return symbol;
		}

		// Token: 0x06004153 RID: 16723 RVA: 0x003DB16C File Offset: 0x003DA16C
		public Break AppendBreak(BreakType breakType)
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
			Break @break = new Break(base.Document, breakType);
			this.Items.Add(@break);
			return @break;
		}

		// Token: 0x06004154 RID: 16724 RVA: 0x003DB1C4 File Offset: 0x003DA1C4
		public TableOfContent AppendTOC(int lowerLevel, int upperLevel)
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			TableOfContent tableOfContent = this.ᜀ(ParagraphItemType.TOC) as TableOfContent;
			tableOfContent.LowerHeadingLevel = lowerLevel;
			tableOfContent.UpperHeadingLevel = upperLevel;
			this.ᜀ(FieldMarkType.FieldSeparator);
			this.AppendText(ClipboardData.b("㩭㽯ㅱ", a_));
			this.ᜀ(FieldMarkType.FieldEnd);
			base.Document.TOC = tableOfContent;
			return tableOfContent;
		}

		// Token: 0x06004155 RID: 16725 RVA: 0x003DB258 File Offset: 0x003DA258
		public DocPicture AppendPicture(Image image)
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
			DocPicture docPicture = this.ᜀ(ParagraphItemType.Picture) as DocPicture;
			docPicture.LoadImage(image);
			base.Document.HasPicture = true;
			return docPicture;
		}

		// Token: 0x06004156 RID: 16726 RVA: 0x003DB2B4 File Offset: 0x003DA2B4
		public void AppendHTML(string html)
		{
			int a_ = 8;
			spr\u2477 spr_u;
			for (;;)
			{
				string text = html.ToLower();
				int num = 10;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (!text.StartsWith(ClipboardData.b("剭兯ᙱ᭳ᕵ౷͹౻᭽", a_)))
						{
							num = 8;
							continue;
						}
						goto IL_6F;
					case 1:
						goto IL_6F;
					case 2:
						num = 0;
						continue;
					case 3:
						if (!text.StartsWith(ClipboardData.b("剭佯ੱᥳ᩵", a_)))
						{
							num = 5;
							continue;
						}
						goto IL_6F;
					case 4:
						if (this.IsStyleApplied)
						{
							num = 9;
							continue;
						}
						goto IL_1BD;
					case 5:
						goto IL_189;
					case 6:
						if (!text.StartsWith(ClipboardData.b("剭ቯᵱၳཱུ", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_6F;
					case 7:
						num = 6;
						continue;
					case 8:
						if (true)
						{
						}
						num = 3;
						continue;
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_189;
						default:
							goto IL_B3;
						}
						break;
					case 10:
						if (!text.StartsWith(ClipboardData.b("剭ᡯٱᥳ᩵", a_)))
						{
							num = 7;
							continue;
						}
						goto IL_6F;
					}
					break;
					IL_6F:
					spr_u = sprᴈ.ᜀ();
					num = 4;
					continue;
					IL_189:
					html = ClipboardData.b("剭ᡯٱᥳ᩵䙷䙹ᑻ᭽몃명ﲇ붑ꪓꪕ랗鍊ﾝ쒟鲡颣쒥잧캩햫邭", a_) + html + ClipboardData.b("剭彯ၱ᭳ትŷ䑹䁻兽뚇", a_);
					num = 1;
				}
			}
			IL_B3:
			if (false)
			{
			}
			spr_u.ᜀ(base.OwnerTextBody, html, base.ឯ(), this.Items.Count, this.ParaStyle, this.ListFormat.CurrentListStyle);
			return;
			IL_1BD:
			spr_u.ᜀ(base.OwnerTextBody, html, base.ឯ(), this.Items.Count, null, this.ListFormat.CurrentListStyle);
		}

		// Token: 0x06004157 RID: 16727 RVA: 0x003DB4A8 File Offset: 0x003DA4A8
		public DocOleObject AppendOleObject(Stream oleStream, DocPicture olePicture, OleObjectType type)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 1;
				DocOleObject result;
				for (;;)
				{
					switch (num)
					{
					case 0:
						try
						{
							string tempFileName;
							File.Delete(tempFileName);
							return result;
						}
						catch (Exception)
						{
							return result;
						}
						goto IL_F1;
					case 2:
						goto IL_F1;
					case 3:
						if (oleStream.Length == 0L)
						{
							num = 7;
							continue;
						}
						num = 4;
						continue;
					case 4:
					{
						if (type == OleObjectType.Package)
						{
							num = 6;
							continue;
						}
						oleStream.Position = 0L;
						string tempFileName = Path.GetTempFileName();
						FileStream fileStream = new FileStream(tempFileName, FileMode.OpenOrCreate);
						num = 5;
						continue;
					}
					case 5:
					{
						try
						{
							byte[] array = new byte[oleStream.Length];
							oleStream.Read(array, 0, array.Length);
							FileStream fileStream;
							fileStream.Write(array, 0, array.Length);
							goto IL_65;
						}
						finally
						{
							num = 2;
							for (;;)
							{
								FileStream fileStream;
								switch (num)
								{
								case 0:
									((IDisposable)fileStream).Dispose();
									num = 1;
									continue;
								case 1:
									goto IL_DB;
								}
								if (fileStream == null)
								{
									break;
								}
								num = 0;
							}
							IL_DB:;
						}
						goto IL_DE;
						IL_65:
						string tempFileName;
						result = this.AppendOleObject(tempFileName, olePicture, type);
						num = 0;
						continue;
					}
					case 6:
						goto IL_146;
					case 7:
						goto IL_113;
					}
					if (true)
					{
					}
					if (oleStream != null)
					{
						num = 2;
						continue;
					}
					break;
					IL_F1:
					num = 3;
				}
				IL_DE:
				return null;
				IL_113:
				goto IL_DE;
				IL_146:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_113;
				default:
					if (false)
					{
					}
					throw new ArgumentException(ClipboardData.b("⡷ᙹ᥻ώꒃﮇ겋쾍ﲗ햙ﮝ삡캣쎥쮧\udea9蒫ﶭ쒯삱톳ힵ햷骹펻튽ꖿ釁냃듅귇ꯉꇋ雑믓뗕裗동뿛ꫝ闟郡臣웥蟧蛩觫뻭駯釱胳菵諷鿹탻\udefd珿瘁瘃漅昇洉Ⰻ栍礏縑焓匕怗渙礛瀝匟䬡䬣䠥ħ਩䄫䬭䐯娱嬳刵ᘷᨹ᰻渽ℿ⅁⽃❅⽇⽉汋㩍⥏≑ㅓ癕ㅗ⥙籛㝝๟ᑡգ੥ŧ๩䱫ݭṯ剱sṵᅷॹ屻ᵽﺉꊋ", a_));
				}
				return result;
			}
			}
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x003DB668 File Offset: 0x003DA668
		public DocOleObject AppendOleObject(byte[] oleBytes, DocPicture olePicture, OleObjectType type)
		{
			int a_ = 0;
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_53;
				case 2:
					if (oleBytes.Length == 0)
					{
						num = 4;
						continue;
					}
					num = 5;
					continue;
				case 3:
					num = 2;
					continue;
				case 4:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_46;
					default:
						goto IL_8C;
					}
					break;
				case 5:
					goto IL_46;
				}
				if (oleBytes != null)
				{
					num = 3;
					continue;
				}
				goto IL_55;
				IL_46:
				if (type != OleObjectType.Package)
				{
					goto IL_A8;
				}
				num = 1;
			}
			IL_53:
			throw new ArgumentException(ClipboardData.b("㙥ѧཀྵ൫ᵭᕯ剱ųյᵷ婹㵻๽잇속늙ﺛ풟잡ﾣﮥ袧얩삫쮭쮱삳펵쮷隹鲻諾꾿ꇁ铃꿅ꯇ뻉맋볍뗏믓뫕뷗諙뗛뷝铟韡難菥쓧쫩鿫髭苯鯱髳釵\ud8f7鳹闻鋽旿䜁簃爅洇搉缋服缏簑㴓㘕甗缙栛瘝伟䘡ਣإࠧ稩䴫䴭嬯匱匳匵ᠷ丹䔻丽┿扁ⵃ㕅桇⍉≋㡍ㅏ㹑㵓㉕硗㍙㉛繝ᑟ੡ൣᕥ䡧३ͫmѯ᝱౳ɵ噷", a_));
			IL_55:
			return null;
			IL_8C:
			if (false)
			{
			}
			goto IL_55;
			IL_A8:
			if (true)
			{
			}
			MemoryStream oleStream = new MemoryStream(oleBytes);
			return this.AppendOleObject(oleStream, olePicture, type);
		}

		// Token: 0x06004159 RID: 16729 RVA: 0x003DB738 File Offset: 0x003DA738
		public DocOleObject AppendOleObject(string pathToFile, DocPicture olePicture, OleObjectType type)
		{
			DocOleObject docOleObject;
			for (;;)
			{
				docOleObject = new DocOleObject(this.m_doc);
				this.Items.Add(docOleObject);
				docOleObject.ᜀ(olePicture);
				docOleObject.ᜀ(OleLinkType.Embed);
				docOleObject.ObjectType = spr\u20F5.ᜀ(type, false);
				docOleObject.OleObjectType = type;
				docOleObject.ᜂ(pathToFile);
				docOleObject.Field.Type = FieldType.FieldEmbed;
				FieldMark fieldMark = new FieldMark(this.m_doc);
				fieldMark.Type = FieldMarkType.FieldSeparator;
				fieldMark.CharacterFormat.CharacterProps.ᜄ(int.Parse(docOleObject.OleStorageName));
				fieldMark.CharacterFormat.CharacterProps.ᜋ(true);
				this.Items.Add(fieldMark);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (olePicture.Owner != null)
						{
							num = 2;
							continue;
						}
						goto IL_117;
					case 1:
						goto IL_115;
					case 2:
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
							olePicture = (olePicture.Clone() as DocPicture);
							num = 1;
							continue;
						}
						break;
					}
					break;
				}
			}
			IL_115:
			IL_117:
			this.Items.Add(olePicture);
			this.ᜀ(FieldMarkType.FieldEnd);
			return docOleObject;
		}

		// Token: 0x0600415A RID: 16730 RVA: 0x003DB874 File Offset: 0x003DA874
		public DocOleObject AppendOleObject(string pathToFile, DocPicture olePicture)
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
			return this.AppendOleObject(pathToFile, olePicture, OleObjectType.Package);
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x003DB8BC File Offset: 0x003DA8BC
		public DocOleObject AppendOleObject(Stream oleStream, DocPicture olePicture, OleLinkType oleLinkType)
		{
			int a_ = 11;
			switch (0)
			{
			default:
			{
				DocOleObject docOleObject;
				for (;;)
				{
					for (;;)
					{
						docOleObject = (this.ᜀ(ParagraphItemType.OleObject) as DocOleObject);
						docOleObject.ᜀ(olePicture);
						docOleObject.ᜀ(oleLinkType);
						int num = -1;
						int num2 = 0;
						for (;;)
						{
							FieldMark fieldMark;
							switch (num2)
							{
							case 0:
								try
								{
									oleStream.Position = 0L;
									sprᤘ sprᤘ = new sprᤘ(oleStream);
									string text = sprᤘ.\u1717()[0].Replace(ClipboardData.b("⹰", a_), string.Empty);
									num = int.Parse(text);
									docOleObject.OleStorageName = text;
									sprᤘ.Close();
									sprᤘ.Dispose();
									goto IL_1FE;
								}
								catch
								{
									docOleObject.ᜀ(oleStream);
									goto IL_1FE;
								}
								goto IL_18F;
								IL_1FE:
								num2 = 2;
								continue;
							case 1:
								goto IL_1E1;
							case 2:
								if (oleLinkType == OleLinkType.Embed)
								{
									num2 = 7;
									continue;
								}
								docOleObject.Field.Type = FieldType.FieldLink;
								num2 = 10;
								continue;
							case 3:
								if (olePicture.Owner != null)
								{
									num2 = 9;
									continue;
								}
								goto IL_21E;
							case 4:
								if (num != -1)
								{
									num2 = 8;
									continue;
								}
								goto IL_103;
							case 5:
								goto IL_103;
							case 6:
								goto IL_18F;
							case 7:
								docOleObject.Field.Type = FieldType.FieldEmbed;
								num2 = 6;
								continue;
							case 8:
								fieldMark.CharacterFormat.CharacterProps.ᜄ(num);
								fieldMark.CharacterFormat.CharacterProps.ᜋ(true);
								spr\u1C2D.ᜀ(oleStream, num, this.m_doc);
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
									num2 = 5;
									continue;
								}
								break;
							case 9:
								olePicture = (olePicture.Clone() as DocPicture);
								num2 = 1;
								continue;
							case 10:
								goto IL_18F;
							}
							break;
							IL_103:
							this.Items.Add(fieldMark);
							num2 = 3;
							continue;
							IL_18F:
							fieldMark = new FieldMark(this.m_doc);
							fieldMark.Type = FieldMarkType.FieldSeparator;
							num2 = 4;
						}
					}
				}
				IL_1E1:
				IL_21E:
				this.Items.Add(olePicture);
				this.ᜀ(FieldMarkType.FieldEnd);
				return docOleObject;
			}
			}
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x003DBB10 File Offset: 0x003DAB10
		public DocOleObject AppendOleObject(byte[] oleBytes, DocPicture olePicture, OleLinkType oleLinkType)
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
			MemoryStream oleStream = new MemoryStream(oleBytes);
			return this.AppendOleObject(oleStream, olePicture, oleLinkType);
		}

		// Token: 0x0600415D RID: 16733 RVA: 0x003DBB5C File Offset: 0x003DAB5C
		public DocOleObject AppendOleObject(byte[] oleBytes, DocPicture olePicture, string fileExtension)
		{
			int a_ = 13;
			DocOleObject docOleObject;
			for (;;)
			{
				docOleObject = new DocOleObject(this.m_doc);
				this.Items.Add(docOleObject);
				docOleObject.ᜀ(olePicture);
				docOleObject.ᜀ(OleLinkType.Embed);
				docOleObject.ObjectType = spr\u20F5.ᜀ(OleObjectType.Package, false);
				docOleObject.OleObjectType = OleObjectType.Package;
				string a_2 = ClipboardData.b("⍲ᑴᑶቸ᩺᩼᩾꾀", a_) + fileExtension.Replace(ClipboardData.b("嵲", a_), string.Empty);
				docOleObject.ᜀ(oleBytes, a_2);
				docOleObject.Field.Type = FieldType.FieldEmbed;
				FieldMark fieldMark = new FieldMark(this.m_doc);
				fieldMark.Type = FieldMarkType.FieldSeparator;
				fieldMark.CharacterFormat.CharacterProps.ᜄ(int.Parse(docOleObject.OleStorageName));
				fieldMark.CharacterFormat.CharacterProps.ᜋ(true);
				this.Items.Add(fieldMark);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (olePicture.Owner != null)
						{
							num = 1;
							continue;
						}
						goto IL_150;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_146;
						default:
							if (false)
							{
							}
							olePicture = (olePicture.Clone() as DocPicture);
							num = 2;
							continue;
						}
						break;
					case 2:
						goto IL_146;
					}
					break;
				}
			}
			IL_146:
			if (true)
			{
			}
			IL_150:
			this.Items.Add(olePicture);
			this.ᜀ(FieldMarkType.FieldEnd);
			return docOleObject;
		}

		// Token: 0x0600415E RID: 16734 RVA: 0x003DBCD0 File Offset: 0x003DACD0
		public DocOleObject AppendOleObject(Stream oleStream, DocPicture olePicture, string fileExtension)
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
			oleStream.Position = 0L;
			byte[] array = new byte[oleStream.Length];
			oleStream.Read(array, 0, array.Length);
			return this.AppendOleObject(array, olePicture, fileExtension);
		}

		// Token: 0x0600415F RID: 16735 RVA: 0x003DBD38 File Offset: 0x003DAD38
		internal new FieldMark ᜀ(FieldMarkType A_0)
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
			FieldMark fieldMark = (FieldMark)this.ᜀ(ParagraphItemType.FieldMark);
			fieldMark.Type = A_0;
			return fieldMark;
		}

		// Token: 0x06004160 RID: 16736 RVA: 0x003DBD88 File Offset: 0x003DAD88
		internal Break ᜃ(string A_0)
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
			Break @break = new Break(base.Document, BreakType.LineBreak);
			@break.TextRange.Text = A_0;
			this.Items.Add(@break);
			return @break;
		}

		// Token: 0x06004161 RID: 16737 RVA: 0x003DBDEC File Offset: 0x003DADEC
		internal new Field ᜀ(string A_0, string A_1, DocPicture A_2, HyperlinkType A_3)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				Field field;
				for (;;)
				{
					field = new Field(base.Document);
					field.Type = FieldType.FieldHyperlink;
					this.Items.Add(field);
					this.ᜀ(FieldMarkType.FieldSeparator);
					int num = 5;
					for (;;)
					{
						Hyperlink hyperlink;
						switch (num)
						{
						case 0:
							hyperlink.FilePath = A_0;
							num = 13;
							continue;
						case 1:
							goto IL_238;
						case 2:
							return field;
						case 3:
						{
							ITextRange textRange = this.AppendText(A_1);
							textRange.CharacterFormat.TextColor = Color.Blue;
							textRange.CharacterFormat.UnderlineStyle = UnderlineStyle.Single;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_E8;
							default:
								if (false)
								{
								}
								num = 11;
								continue;
							}
							break;
						}
						case 4:
							this.Items.Add(A_2);
							num = 8;
							continue;
						case 5:
							if (A_1 != null)
							{
								num = 3;
								continue;
							}
							num = 15;
							continue;
						case 6:
							hyperlink.BookmarkName = A_0;
							num = 2;
							continue;
						case 7:
							return field;
						case 8:
							goto IL_1CA;
						case 9:
							if (A_3 == HyperlinkType.EMailLink)
							{
								goto IL_E8;
							}
							num = 16;
							continue;
						case 10:
							if (true)
							{
							}
							num = 9;
							continue;
						case 11:
							goto IL_1CA;
						case 12:
							goto IL_1CA;
						case 13:
							return field;
						case 14:
							if (hyperlink.Type == HyperlinkType.FileLink)
							{
								num = 0;
								continue;
							}
							return field;
						case 15:
							if (A_2 != null)
							{
								num = 4;
								continue;
							}
							this.AppendText(ClipboardData.b("⹥ᅧᩩ५ᱭᱯ᭱ᩳᵵ", a_));
							num = 12;
							continue;
						case 16:
							if (hyperlink.Type == HyperlinkType.Bookmark)
							{
								num = 6;
								continue;
							}
							num = 14;
							continue;
						case 17:
							if (A_3 != HyperlinkType.WebLink)
							{
								num = 10;
								continue;
							}
							goto IL_238;
						}
						break;
						IL_E8:
						num = 1;
						continue;
						IL_1CA:
						FieldMark entity = new FieldMark(base.Document, FieldMarkType.FieldEnd);
						this.Items.Add(entity);
						hyperlink = new Hyperlink(field);
						hyperlink.Type = A_3;
						num = 17;
						continue;
						IL_238:
						hyperlink.Uri = A_0;
						num = 7;
					}
				}
				return field;
			}
			}
		}

		// Token: 0x06004162 RID: 16738 RVA: 0x003DC070 File Offset: 0x003DB070
		internal new IPicture ᜀ(byte[] A_0, bool A_1, bool A_2)
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
			DocPicture docPicture = this.ᜀ(ParagraphItemType.Picture) as DocPicture;
			HMACSHA1 hmacsha = new HMACSHA1();
			byte[] a_ = hmacsha.ComputeHash(A_0);
			docPicture.ᜀ(A_0, a_, A_1, A_2);
			base.Document.HasPicture = true;
			return docPicture;
		}

		// Token: 0x06004163 RID: 16739 RVA: 0x003DC0E0 File Offset: 0x003DB0E0
		internal new void ᜀ(DocPicture A_0, sprᠾ A_1)
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
			A_0.ᜀ(A_1);
			base.Document.HasPicture = true;
		}

		// Token: 0x06004164 RID: 16740 RVA: 0x003DC130 File Offset: 0x003DB130
		internal Field ᜁ(string A_0)
		{
			int a_ = 19;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			Field field = new Field(base.Document);
			field.Type = FieldType.FieldIndexEntry;
			field.m_formattingString = ClipboardData.b("學", a_) + A_0 + ClipboardData.b("學", a_);
			field.CharacterFormat.FieldVanishComplex = 129;
			this.Items.Add(field);
			FieldMark fieldMark = this.ᜀ(FieldMarkType.FieldEnd);
			fieldMark.CharacterFormat.FieldVanishComplex = 129;
			return field;
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x003DC1E8 File Offset: 0x003DB1E8
		public override TextSelection Find(Regex pattern)
		{
			int a_ = 19;
			int num = 1;
			List<TextSelection> list;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (list.Count <= 0)
					{
						num = 3;
						continue;
					}
					goto IL_A8;
				case 1:
					if (true)
					{
					}
					break;
				case 2:
					goto IL_67;
				case 3:
					goto IL_92;
				}
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_94;
				default:
					if (false)
					{
					}
					if (spr\u1AB5.ᜀ(pattern))
					{
						num = 2;
					}
					else
					{
						list = spr\u25C5.ᜀ().ᜀ(this, pattern, true);
						num = 0;
					}
					break;
				}
			}
			IL_67:
			goto IL_94;
			IL_92:
			return null;
			IL_94:
			throw new ArgumentException(ClipboardData.b("⩸Ṻᱼൾꖄﶈ力뎒膠솢삤螦첨욪\uddac\udbae좰", a_));
			IL_A8:
			return list[0];
		}

		// Token: 0x06004166 RID: 16742 RVA: 0x003DC2A4 File Offset: 0x003DB2A4
		public TextSelection Find(string given, bool caseSensitive, bool wholeWord)
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
			Regex pattern = spr\u1AB5.ᜀ(given, caseSensitive, wholeWord);
			return this.Find(pattern);
		}

		// Token: 0x06004167 RID: 16743 RVA: 0x003DC2F0 File Offset: 0x003DB2F0
		public override int Replace(Regex pattern, string replace)
		{
			int a_ = 3;
			if (!spr\u1AB5.ᜀ(pattern))
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
				{
					if (true)
					{
					}
					if (false)
					{
					}
					spr\u21D6 spr_u21D = spr\u21D6.ᜀ();
					return spr_u21D.ᜁ(this, pattern, replace);
				}
				}
			}
			throw new ArgumentException(ClipboardData.b("㩨๪౬ᵮተ᭲啴Ѷ൸ॺᑼᅾꎂﮎ놐랖ﲘ\ud8a0", a_));
		}

		// Token: 0x06004168 RID: 16744 RVA: 0x003DC364 File Offset: 0x003DB364
		public override int Replace(string given, string replace, bool caseSensitive, bool wholeWord)
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
			Regex pattern = spr\u1AB5.ᜀ(given, caseSensitive, wholeWord);
			return this.Replace(pattern, replace);
		}

		// Token: 0x06004169 RID: 16745 RVA: 0x003DC3B4 File Offset: 0x003DB3B4
		public override int Replace(Regex pattern, TextSelection textSelection)
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
			return this.Replace(pattern, textSelection, false);
		}

		// Token: 0x0600416A RID: 16746 RVA: 0x003DC3F8 File Offset: 0x003DB3F8
		public override int Replace(Regex pattern, TextSelection textSelection, bool saveFormatting)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 4;
				spr\u226E spr_u226E;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (spr_u226E != null)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						return 0;
					case 1:
					{
						List<TextSelection>.Enumerator enumerator = spr_u226E.GetEnumerator();
						num = 2;
						continue;
					}
					case 2:
						goto IL_6B;
					case 3:
						goto IL_4E;
					}
					if (spr\u1AB5.ᜀ(pattern))
					{
						num = 3;
					}
					else
					{
						textSelection.ᜂ();
						spr_u226E = this.FindAll(pattern);
						num = 0;
					}
				}
				IL_4E:
				throw new ArgumentException(ClipboardData.b("⁲ၴᙶ୸᡺ᕼ彾권ﶒﮔ뮚ﾜ爵膠욢좤힦\udda8튪", a_));
				IL_6B:
				try
				{
					num = 7;
					for (;;)
					{
						List<TextSelection>.Enumerator enumerator;
						TextSelection textSelection2;
						CharacterFormat a_2;
						switch (num)
						{
						case 0:
							goto IL_1CA;
						case 1:
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							goto IL_12E;
						case 2:
							num = 0;
							continue;
						case 3:
							if (saveFormatting)
							{
								num = 4;
								continue;
							}
							goto IL_173;
						case 4:
							a_2 = textSelection2.StartTextRange.CharacterFormat;
							num = 9;
							continue;
						case 6:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_12E;
							default:
								if (false)
								{
								}
								if (textSelection2.StartTextRange != null)
								{
									num = 8;
									continue;
								}
								goto IL_173;
							}
							break;
						case 8:
							num = 3;
							continue;
						case 9:
							goto IL_173;
						}
						IL_F7:
						num = 1;
						continue;
						goto IL_F7;
						IL_12E:
						textSelection2 = enumerator.Current;
						a_2 = null;
						num = 6;
						continue;
						IL_173:
						int a_3 = textSelection2.ᜄ();
						Paragraph a_4 = textSelection2.OwnerParagraph;
						textSelection.ᜀ(a_4, a_3, saveFormatting, a_2);
						num = 5;
					}
					IL_1CA:
					goto IL_81;
				}
				finally
				{
					List<TextSelection>.Enumerator enumerator;
					((IDisposable)enumerator).Dispose();
				}
				return 0;
				IL_81:
				return spr_u226E.Count;
			}
			}
		}

		// Token: 0x0600416B RID: 16747 RVA: 0x003DC600 File Offset: 0x003DB600
		public int Replace(string given, TextSelection textSelection, bool caseSensitive, bool wholeWord)
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
			Regex pattern = spr\u1AB5.ᜀ(given, caseSensitive, wholeWord);
			return this.Replace(pattern, textSelection, false);
		}

		// Token: 0x0600416C RID: 16748 RVA: 0x003DC650 File Offset: 0x003DB650
		public int Replace(string given, TextSelection textSelection, bool caseSensitive, bool wholeWord, bool saveFormatting)
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
			Regex pattern = spr\u1AB5.ᜀ(given, caseSensitive, wholeWord);
			return this.Replace(pattern, textSelection, saveFormatting);
		}

		// Token: 0x0600416D RID: 16749 RVA: 0x003DC6A0 File Offset: 0x003DB6A0
		internal new int ᜀ(string A_0, string A_1, bool A_2, bool A_3)
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
			Regex a_ = spr\u1AB5.ᜀ(A_0, A_2, A_3);
			return this.ᜀ(a_, A_1);
		}

		// Token: 0x0600416E RID: 16750 RVA: 0x003DC6F0 File Offset: 0x003DB6F0
		internal new int ᜀ(Regex A_0, string A_1)
		{
			int a_ = 4;
			if (true)
			{
			}
			if (!spr\u1AB5.ᜀ(A_0))
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
					spr\u21D6 spr_u21D = spr\u21D6.ᜀ();
					bool replaceFirst = base.Document.ReplaceFirst;
					base.Document.ReplaceFirst = true;
					int result = spr_u21D.ᜁ(this, A_0, A_1);
					base.Document.ReplaceFirst = replaceFirst;
					return result;
				}
				}
			}
			throw new ArgumentException(ClipboardData.b("㥩५཭ɯᅱᱳ噵୷๹๻᝽ꒃ늑뢗ﾙ풟\udba1", a_));
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x003DC788 File Offset: 0x003DB788
		public Section InsertSectionBreak()
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
			return this.InsertSectionBreak(SectionBreakType.NewPage);
		}

		// Token: 0x06004170 RID: 16752 RVA: 0x003DC7CC File Offset: 0x003DB7CC
		public Section InsertSectionBreak(SectionBreakType breakType)
		{
			int a_ = 8;
			switch (0)
			{
			default:
				for (;;)
				{
					Section section = this.ᜅ();
					int num = 3;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							if (this.ᜈ.Owner is HeaderFooter)
							{
								num = 2;
								continue;
							}
							int num2 = section.ឯ();
							Section section2 = section.ᜂ();
							base.Document.Sections.Insert(num2 + 1, section2);
							section2.BreakCode = breakType;
							int num3 = this.ᜈ.ឯ();
							int count = section.Body.Items.Count;
							int num4 = num3 + 1;
							num = 7;
							continue;
						}
						case 1:
							goto IL_5F;
						case 2:
							goto IL_14E;
						case 3:
							if (section == null)
							{
								num = 1;
								continue;
							}
							num = 0;
							continue;
						case 4:
						{
							Section section2;
							return section2;
						}
						case 5:
						{
							int count;
							int num4;
							if (num4 >= count)
							{
								num = 4;
								continue;
							}
							Section section2;
							int num3;
							section2.Body.Items.Insert(section2.Body.Items.Count, section.Body.Items[num3 + 1]);
							num4++;
							num = 6;
							continue;
						}
						case 6:
							goto IL_E2;
						case 7:
							goto IL_E2;
						}
						break;
						IL_E2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A8;
						default:
							if (false)
							{
							}
							num = 5;
							break;
						}
					}
				}
				IL_5F:
				if (true)
				{
				}
				throw new Exception(ClipboardData.b("Ⅽݯᱱᅳѵ塷ॹ᥻ᵽꢇﺏ﶑뚕流ﾙ벛햟캡좣袥", a_));
				IL_A8:
				throw new NotSupportedException(ClipboardData.b("⵭ᅯᱱᩳ᥵౷婹ᕻၽꢇ黎ﮑﮓ뢗ﮝ솟즡蒣삥잧\ud8a9貫욭햯펱킳펵쪷骹\udabb톽꾿뛁ꇃ듅ꏉ룋ꯍ뷏ꇑ䀘", a_));
				IL_14E:
				goto IL_A8;
			}
		}

		// Token: 0x06004171 RID: 16753 RVA: 0x003DC98C File Offset: 0x003DB98C
		private Section ᜅ()
		{
			DocumentObject documentObject;
			for (;;)
			{
				documentObject = this;
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (documentObject.Owner != null)
						{
							num = 4;
							continue;
						}
						goto IL_ED;
					case 1:
						goto IL_51;
					case 2:
						goto IL_95;
					case 3:
						goto IL_95;
					case 4:
						documentObject = documentObject.Owner;
						if (true)
						{
						}
						num = 3;
						continue;
					case 5:
						if (documentObject is Section)
						{
							num = 6;
							continue;
						}
						goto IL_6E;
					case 6:
						goto IL_B3;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6E;
						default:
							if (false)
							{
							}
							this.ᜈ = (documentObject as Body);
							num = 1;
							continue;
						}
						break;
					case 8:
						if (documentObject is Body)
						{
							num = 7;
							continue;
						}
						goto IL_51;
					}
					break;
					IL_51:
					num = 0;
					continue;
					IL_6E:
					num = 8;
					continue;
					IL_95:
					num = 5;
				}
			}
			IL_B3:
			IL_ED:
			return documentObject as Section;
		}

		// Token: 0x06004172 RID: 16754 RVA: 0x003DCA8C File Offset: 0x003DBA8C
		internal ParagraphItemCollection \u1712()
		{
			ParagraphItemCollection paragraphItemCollection;
			for (;;)
			{
				paragraphItemCollection = new ParagraphItemCollection(this);
				int num = 0;
				int num2 = 4;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						(this.m_pItemColl[num] as sprờ).ᜀ(paragraphItemCollection);
						num2 = 5;
						continue;
					case 1:
						goto IL_D5;
					case 2:
						return paragraphItemCollection;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D5;
						default:
							if (false)
							{
							}
							goto IL_CA;
						}
						break;
					case 4:
						goto IL_CA;
					case 5:
						goto IL_48;
					case 6:
						goto IL_48;
					case 7:
						if (this.m_pItemColl[num] is sprờ)
						{
							num2 = 0;
							continue;
						}
						paragraphItemCollection.InnerList.Add(this.m_pItemColl[num]);
						num2 = 6;
						continue;
					}
					break;
					IL_48:
					num++;
					num2 = 3;
					continue;
					IL_D5:
					if (num >= this.m_pItemColl.Count)
					{
						num2 = 2;
						continue;
					}
					if (true)
					{
					}
					num2 = 7;
					continue;
					IL_CA:
					num2 = 1;
				}
			}
			return paragraphItemCollection;
		}

		// Token: 0x06004173 RID: 16755 RVA: 0x003DCBB4 File Offset: 0x003DBBB4
		internal bool ᜆ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					return true;
				case 2:
					num = 3;
					continue;
				case 3:
					if (this.m_pItemColl[0].DocumentObjectType == DocumentObjectType.BookmarkEnd)
					{
						num = 1;
						continue;
					}
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
					if (true)
					{
					}
					if (this.m_pItemColl.Count != 1)
					{
						return false;
					}
					num = 2;
					break;
				}
			}
			return true;
		}

		// Token: 0x06004174 RID: 16756 RVA: 0x003DCC54 File Offset: 0x003DBC54
		internal void \u170D()
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
			this.Items.InnerList.Clear();
			this.ᜃ = new StringBuilder(1);
		}

		// Token: 0x06004175 RID: 16757 RVA: 0x003DCCAC File Offset: 0x003DBCAC
		internal override spr\u226E FindAll(Regex pattern)
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
			return spr\u25C5.ᜀ().ᜀ(this, pattern, false);
		}

		// Token: 0x06004176 RID: 16758 RVA: 0x003DCCF4 File Offset: 0x003DBCF4
		internal new spr\u226E ᜀ(Regex A_0)
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
			return spr\u25C5.ᜀ().ᜀ(this, A_0, true);
		}

		// Token: 0x06004177 RID: 16759 RVA: 0x003DCD3C File Offset: 0x003DBD3C
		internal new void ᜀ(int A_0, bool A_1)
		{
			int num = 8;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					goto IL_64;
				case 2:
					if (A_0 >= this.Items.Count)
					{
						num = 5;
						continue;
					}
					this.Items.RemoveAt(A_0);
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 7;
						continue;
					}
					break;
				case 3:
					goto IL_9B;
				case 4:
					return;
				case 5:
					return;
				case 6:
					if (A_0 <= -1)
					{
						num = 4;
						continue;
					}
					this.Items.RemoveAt(A_0);
					A_0--;
					if (true)
					{
					}
					num = 3;
					continue;
				case 7:
					goto IL_64;
				}
				if (A_1)
				{
					num = 0;
					continue;
				}
				goto IL_9B;
				IL_64:
				num = 2;
				continue;
				IL_9B:
				num = 6;
			}
		}

		// Token: 0x06004178 RID: 16760 RVA: 0x003DCE3C File Offset: 0x003DBE3C
		internal Paragraph ᜐ()
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
			return this.ᜀ(false);
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x003DCE80 File Offset: 0x003DBE80
		internal new IParagraphBase ᜀ(ParagraphItemType A_0)
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
			IParagraphBase paragraphBase = base.Document.CreateParagraphItem(A_0);
			this.Items.Add(paragraphBase);
			return paragraphBase;
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x003DCED8 File Offset: 0x003DBED8
		internal new void ᜀ(TextRange A_0, string A_1)
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
			this.ᜀ(A_0, A_0.TextLength, A_1);
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x003DCF24 File Offset: 0x003DBF24
		internal new void ᜀ(ParagraphBase A_0, int A_1, string A_2)
		{
			int a_ = 9;
			switch (0)
			{
			default:
				for (;;)
				{
					this.ᜃ.Remove(A_0.StartPos, A_1);
					this.ᜃ.Insert(A_0.StartPos, A_2);
					int num = A_2.Length - A_1;
					int num2 = this.m_pItemColl.IndexOf(A_0);
					int num3 = 4;
					for (;;)
					{
						int num4;
						ParagraphBase paragraphBase;
						switch (num3)
						{
						case 0:
						{
							int count;
							if (num4 >= count)
							{
								num3 = 2;
								continue;
							}
							paragraphBase = this.m_pItemColl[num4];
							num3 = 3;
							continue;
						}
						case 1:
							goto IL_113;
						case 2:
							return;
						case 3:
							goto IL_D1;
						case 4:
						{
							if (num2 < 0)
							{
								num3 = 6;
								continue;
							}
							num4 = num2 + 1;
							int count = this.m_pItemColl.Count;
							num3 = 7;
							continue;
						}
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_D1;
							default:
								if (false)
								{
								}
								paragraphBase.StartPos += num;
								num3 = 8;
								continue;
							}
							break;
						case 6:
							goto IL_99;
						case 7:
							if (true)
							{
							}
							goto IL_113;
						case 8:
							goto IL_A5;
						}
						break;
						IL_A5:
						num4++;
						num3 = 1;
						continue;
						IL_D1:
						if (paragraphBase != null)
						{
							num3 = 5;
							continue;
						}
						goto IL_A5;
						IL_113:
						num3 = 0;
					}
				}
				IL_99:
				throw new InvalidOperationException(ClipboardData.b("Ὦ㡰ݲၴ᩶奸፺ᱼॾꊄꦈ搜ﾐ떔ﺖ뮚ﺞ펠슢스햦좨\udbaa얬辮\ud8b0잲킴\udab6쪸", a_));
			}
		}

		// Token: 0x0600417C RID: 16764 RVA: 0x003DD0A8 File Offset: 0x003DC0A8
		internal new void ᜀ(IParagraphStyle A_0)
		{
			int a_ = 17;
			if (true)
			{
			}
			if (A_0 != null)
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
					this.m_style = A_0;
					this.ᜄ();
					this.ᜀ(A_0 as Style);
					return;
				}
			}
			throw new ArgumentNullException(ClipboardData.b("᥶ᱸ౺⹼୾", a_));
		}

		// Token: 0x0600417D RID: 16765 RVA: 0x003DD120 File Offset: 0x003DC120
		internal new void ᜀ(int A_0, int A_1, string A_2)
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
			int length = A_2.Length;
			this.ᜃ.Remove(A_0, A_1);
			this.ᜃ.Insert(A_0, A_2);
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x003DD180 File Offset: 0x003DC180
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			int num = 13;
			for (;;)
			{
				int num2;
				int count;
				switch (num)
				{
				case 0:
					goto IL_7A;
				case 1:
					goto IL_96;
				case 2:
					return;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18E;
					default:
						if (false)
						{
						}
						this.ᜂ(doc);
						num = 1;
						continue;
					}
					break;
				case 4:
					if (doc.ImportOption != ImportOptions.UseDestinationStyles)
					{
						num = 6;
						continue;
					}
					return;
				case 5:
					this.ᜄ(doc);
					goto IL_18E;
				case 6:
					this.ᜀ(this.m_style);
					num = 2;
					continue;
				case 7:
					if (true)
					{
					}
					if (doc.ImportOption == ImportOptions.MergeFormatting)
					{
						num = 5;
						continue;
					}
					this.ᜃ(doc);
					num = 11;
					continue;
				case 8:
					goto IL_7A;
				case 9:
				{
					if (num2 >= count)
					{
						num = 10;
						continue;
					}
					ParagraphBase paragraphBase = this.Items[num2];
					paragraphBase.CloneRelationsTo(doc, nextOwner);
					num2++;
					num = 8;
					continue;
				}
				case 10:
					num = 4;
					continue;
				case 11:
					goto IL_96;
				case 12:
					goto IL_96;
				}
				if (doc.ImportOption == ImportOptions.UseDestinationStyles)
				{
					num = 3;
					continue;
				}
				num = 7;
				continue;
				IL_7A:
				num = 9;
				continue;
				IL_96:
				this.ᜁ(doc);
				num2 = 0;
				count = this.Items.Count;
				num = 0;
				continue;
				IL_18E:
				num = 12;
			}
		}

		// Token: 0x0600417F RID: 16767 RVA: 0x003DD32C File Offset: 0x003DC32C
		private void ᜄ(Document A_0)
		{
			Paragraph paragraph;
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
					if (false)
					{
					}
					paragraph = A_0.LastParagraph;
					break;
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (paragraph == null)
						{
							num = 1;
							continue;
						}
						goto IL_6F;
					case 1:
						paragraph = new Paragraph(A_0);
						num = 2;
						continue;
					case 2:
						goto IL_6D;
					}
					break;
				}
			}
			IL_6D:
			IL_6F:
			this.Format.ImportContainer(paragraph.Format);
			this.Format.ᜃ(paragraph.Format);
			this.BreakCharacterFormat.ᜂ(paragraph.BreakCharacterFormat);
			this.m_style = paragraph.m_style;
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x003DD3E8 File Offset: 0x003DC3E8
		private void ᜃ(Document A_0)
		{
			int a_ = 19;
			ParagraphStyle paragraphStyle;
			for (;;)
			{
				paragraphStyle = (A_0.Styles.FindByName(ClipboardData.b("㝸ᑺོቾ", a_), StyleType.ParagraphStyle) as ParagraphStyle);
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_B9;
					case 1:
						goto IL_BB;
					case 2:
						paragraphStyle = (ParagraphStyle)Style.CreateBuiltinStyle(BuiltinStyle.Normal, A_0);
						A_0.Styles.Add(paragraphStyle);
						num = 1;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_54;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						this.Format.ᜃ(paragraphStyle.ParagraphFormat);
						this.BreakCharacterFormat.ᜅ(paragraphStyle.CharacterFormat);
						num = 0;
						continue;
					case 4:
						if (A_0.ImportOption == ImportOptions.KeepSourceFormatting)
						{
							num = 3;
							continue;
						}
						goto IL_102;
					case 5:
						if (paragraphStyle == null)
						{
							goto IL_54;
						}
						goto IL_BB;
					}
					break;
					IL_54:
					num = 2;
					continue;
					IL_BB:
					num = 4;
				}
			}
			IL_B9:
			IL_102:
			this.m_style = paragraphStyle;
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x003DD500 File Offset: 0x003DC500
		private void ᜂ(Document A_0)
		{
			int num = 0;
			IStyle style;
			for (;;)
			{
				switch (num)
				{
				case 1:
					(this.m_style as ParagraphStyle).ᜀ(A_0);
					num = 5;
					continue;
				case 2:
					goto IL_3C;
				case 3:
					if (true)
					{
					}
					if (style != null)
					{
						num = 4;
						continue;
					}
					num = 6;
					continue;
				case 4:
					goto IL_EB;
				case 5:
					goto IL_AD;
				case 6:
					if (!(this.m_style is ParagraphStyle))
					{
						goto IL_F7;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_F7;
					default:
						if (false)
						{
						}
						num = 1;
						continue;
					}
					break;
				}
				if (A_0.ImportStyles)
				{
					num = 2;
				}
				else
				{
					style = A_0.Styles.FindByName(this.m_style.Name, StyleType.ParagraphStyle);
					num = 3;
				}
			}
			IL_3C:
			this.ᜀ(A_0);
			return;
			IL_AD:
			goto IL_F7;
			IL_EB:
			this.ᜀ(style as ParagraphStyle);
			return;
			IL_F7:
			ParagraphStyle paragraphStyle = this.m_style.Clone() as ParagraphStyle;
			A_0.Styles.Add(paragraphStyle);
			this.ᜀ(paragraphStyle);
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x003DD62C File Offset: 0x003DC62C
		private void ᜁ(Document A_0)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.ListFormat.LFOStyleName == null)
					{
						num = 11;
						continue;
					}
					num = 7;
					continue;
				case 2:
				{
					ListStyle currentListStyle;
					A_0.ListStyles.Add((ListStyle)currentListStyle.Clone());
					num = 4;
					continue;
				}
				case 3:
				{
					spr\u177D spr_u177D;
					A_0.ListOverrides.ᜀ((spr\u177D)spr_u177D.Clone());
					num = 5;
					continue;
				}
				case 4:
					goto IL_182;
				case 5:
					goto IL_101;
				case 6:
				{
					spr\u177D spr_u177D = base.Document.ListOverrides.ᜀ(this.ListFormat.LFOStyleName);
					goto IL_164;
				}
				case 7:
					if (A_0.ListOverrides.ᜀ(this.ListFormat.LFOStyleName) != null)
					{
						return;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_164;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					break;
				case 8:
				{
					spr\u177D spr_u177D;
					if (spr_u177D != null)
					{
						num = 3;
						continue;
					}
					return;
				}
				case 9:
				{
					if (true)
					{
					}
					ListStyle currentListStyle = this.ListFormat.CurrentListStyle;
					num = 10;
					continue;
				}
				case 10:
				{
					ListStyle currentListStyle;
					if (A_0.ListStyles.FindByName(currentListStyle.Name) == null)
					{
						num = 2;
						continue;
					}
					goto IL_182;
				}
				case 11:
					return;
				}
				if (this.ListFormat.ListType != ListType.NoList)
				{
					num = 9;
					continue;
				}
				goto IL_182;
				IL_164:
				num = 8;
				continue;
				IL_182:
				num = 1;
			}
			IL_101:;
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x003DD7E8 File Offset: 0x003DC7E8
		private new void ᜀ(Document A_0)
		{
			int num = 6;
			IStyle style;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 1:
					goto IL_1C6;
				case 2:
					(this.m_style as Style).ᜁ(A_0);
					num = 9;
					continue;
				case 3:
					if (style is IParagraphStyle)
					{
						num = 7;
						continue;
					}
					return;
				case 4:
					this.m_style = (ParagraphStyle)(this.m_style as Style).ᜀ(A_0, style);
					(this.m_style as ParagraphStyle).ᜀ(A_0);
					this.ᜀ(this.m_style);
					num = 0;
					continue;
				case 5:
					if (true)
					{
					}
					if (A_0.CurClonedSection != null)
					{
						num = 4;
						continue;
					}
					return;
				case 7:
					goto IL_A8;
				case 8:
					style = A_0.Styles.FindByName(this.m_style.Name, StyleType.ParagraphStyle);
					num = 11;
					continue;
				case 9:
					if (this.m_style is ParagraphStyle)
					{
						num = 10;
						continue;
					}
					goto IL_6F;
				case 10:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1C6;
					default:
						if (false)
						{
						}
						(this.m_style as ParagraphStyle).ᜀ(A_0);
						num = 1;
						continue;
					}
					break;
				case 11:
					if (style == null)
					{
						num = 2;
						continue;
					}
					num = 5;
					continue;
				}
				if (this.m_style != null)
				{
					num = 8;
					continue;
				}
				return;
				IL_6F:
				style = A_0.Styles.FindByName(this.m_style.Name, StyleType.ParagraphStyle);
				num = 3;
				continue;
				IL_1C6:
				goto IL_6F;
			}
			IL_A8:
			this.ᜀ(style as IParagraphStyle);
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x003DD9C0 File Offset: 0x003DC9C0
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
			return this.ᜀ(true);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x003DDA04 File Offset: 0x003DCA04
		private new Paragraph ᜀ(bool A_0)
		{
			Paragraph paragraph;
			for (;;)
			{
				paragraph = (Paragraph)base.CloneImpl();
				paragraph.ᜃ = new StringBuilder(this.Text);
				paragraph.m_pItemColl = new ParagraphItemCollection(paragraph);
				int num = 4;
				for (;;)
				{
					IParagraphStyle style;
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
							goto IL_1C1;
						default:
							if (false)
							{
							}
							paragraph.m_listFormat = new ListFormat(this);
							paragraph.m_listFormat.ImportContainer(this.ListFormat);
							paragraph.m_listFormat.ᜀ(paragraph);
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_177;
					case 2:
						goto IL_9C;
					case 3:
						if (paragraph.ListFormat.ListType != ListType.NoList)
						{
							num = 0;
							continue;
						}
						goto IL_9C;
					case 4:
						if (A_0)
						{
							num = 7;
							continue;
						}
						goto IL_C1;
					case 5:
						if (Document.IsCloneParagraphCheckFormat)
						{
							goto IL_1C1;
						}
						goto IL_22B;
					case 6:
						paragraph.m_prFormat.ᜀ(this.Format, style);
						num = 1;
						continue;
					case 7:
						this.m_pItemColl.ᜀ(paragraph.m_pItemColl);
						num = 8;
						continue;
					case 8:
						goto IL_C1;
					case 9:
						goto IL_19D;
					case 10:
					{
						ParagraphStyle a_ = style.Clone() as ParagraphStyle;
						paragraph.ᜀ(a_);
						num = 9;
						continue;
					}
					case 11:
						if (style != null)
						{
							num = 10;
							continue;
						}
						goto IL_19D;
					}
					break;
					IL_9C:
					style = this.GetStyle();
					num = 11;
					continue;
					IL_C1:
					paragraph.ᜇ = new CharacterFormat(base.Document);
					paragraph.m_prFormat = new ParagraphFormat(base.Document);
					paragraph.ᜇ.ImportContainer(this.BreakCharacterFormat);
					paragraph.ᜇ.ᜃ(this.BreakCharacterFormat);
					paragraph.m_prFormat.ImportContainer(this.Format);
					paragraph.m_prFormat.ᜃ(this.Format);
					num = 3;
					continue;
					IL_19D:
					paragraph.ᜂ();
					paragraph.m_prFormat.ᜀ(paragraph);
					num = 5;
					continue;
					IL_1C1:
					num = 6;
				}
			}
			IL_177:
			IL_22B:
			paragraph.ᜇ.ᜀ(paragraph);
			return paragraph;
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x003DDC4C File Offset: 0x003DCC4C
		internal string ᜈ()
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
			string str = this.ᜀ(0, this.m_pItemColl.Count - 1);
			return str + spr\u20E8.\u171F;
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x003DDCA8 File Offset: 0x003DCCA8
		internal new string ᜀ(int A_0, int A_1)
		{
			string text;
			for (;;)
			{
				text = string.Empty;
				int num = A_0;
				int num2 = 16;
				for (;;)
				{
					switch (num2)
					{
					case 0:
					{
						ParagraphBase paragraphBase;
						text += (paragraphBase as Field).ᜐ();
						num2 = 2;
						continue;
					}
					case 1:
					{
						if (num > A_1)
						{
							num2 = 4;
							continue;
						}
						ParagraphBase paragraphBase = this.m_pItemColl[num];
						num2 = 18;
						continue;
					}
					case 2:
						if (base.Document.ᜅ == null)
						{
							num2 = 6;
							continue;
						}
						return text;
					case 3:
					{
						ParagraphBase paragraphBase;
						if ((paragraphBase as Field).End.OwnerParagraph == this)
						{
							num2 = 14;
							continue;
						}
						goto IL_FE;
					}
					case 4:
						return text;
					case 5:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase is Field)
						{
							if (true)
							{
							}
							num2 = 0;
							continue;
						}
						num2 = 12;
						continue;
					}
					case 6:
						num2 = 3;
						continue;
					case 7:
						goto IL_FE;
					case 8:
					{
						ParagraphBase paragraphBase;
						text += (paragraphBase as TextRange).Text;
						num2 = 17;
						continue;
					}
					case 9:
						goto IL_AE;
					case 10:
					{
						ParagraphBase paragraphBase;
						if (!(paragraphBase is Break))
						{
							goto IL_FE;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AE;
						default:
							if (false)
							{
							}
							num2 = 9;
							continue;
						}
						break;
					}
					case 11:
						goto IL_FE;
					case 12:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase is TextRange)
						{
							num2 = 19;
							continue;
						}
						num2 = 10;
						continue;
					}
					case 13:
						goto IL_14D;
					case 14:
					{
						ParagraphBase paragraphBase;
						num = (paragraphBase as Field).End.ឯ();
						num2 = 7;
						continue;
					}
					case 15:
						goto IL_FE;
					case 16:
						goto IL_14D;
					case 17:
						goto IL_FE;
					case 18:
					{
						ParagraphBase paragraphBase;
						if (paragraphBase is MergeField)
						{
							num2 = 8;
							continue;
						}
						num2 = 5;
						continue;
					}
					case 19:
					{
						ParagraphBase paragraphBase;
						text += (paragraphBase as TextRange).Text;
						num2 = 15;
						continue;
					}
					}
					break;
					IL_AE:
					text += spr\u20E8.\u171F;
					num2 = 11;
					continue;
					IL_FE:
					num++;
					num2 = 13;
					continue;
					IL_14D:
					num2 = 1;
				}
			}
			return text;
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x003DDF00 File Offset: 0x003DCF00
		private void ᜄ()
		{
			int num = 10;
			for (;;)
			{
				int num2;
				switch (num)
				{
				case 0:
					goto IL_FF;
				case 1:
				{
					ParagraphBase paragraphBase;
					if (paragraphBase is MergeField)
					{
						num = 7;
						continue;
					}
					num = 9;
					continue;
				}
				case 2:
				{
					int count;
					if (num2 >= count)
					{
						num = 8;
						continue;
					}
					ParagraphBase paragraphBase = this.m_pItemColl[num2];
					paragraphBase.ParaItemCharFormat.ApplyBase(this.m_style.CharacterFormat);
					num = 1;
					continue;
				}
				case 3:
					goto IL_16C;
				case 4:
				{
					this.ᜇ.ApplyBase(this.m_style.CharacterFormat);
					this.m_prFormat.ApplyBase(this.m_style.ParagraphFormat);
					ParagraphBase paragraphBase = null;
					num2 = 0;
					int count = this.m_pItemColl.Count;
					goto IL_15F;
				}
				case 5:
					goto IL_FF;
				case 6:
				{
					ParagraphBase paragraphBase;
					(paragraphBase as sprờ).ᜆ();
					num = 11;
					continue;
				}
				case 7:
				{
					ParagraphBase paragraphBase;
					(paragraphBase as MergeField).ᜁ();
					num = 3;
					continue;
				}
				case 8:
					return;
				case 9:
				{
					ParagraphBase paragraphBase;
					if (paragraphBase is sprờ)
					{
						num = 6;
						continue;
					}
					goto IL_16C;
				}
				case 11:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_15F;
					default:
						if (false)
						{
						}
						goto IL_16C;
					}
					break;
				}
				if (this.m_style != null)
				{
					num = 4;
					continue;
				}
				break;
				IL_FF:
				if (true)
				{
				}
				num = 2;
				continue;
				IL_15F:
				num = 5;
				continue;
				IL_16C:
				num2++;
				num = 0;
			}
		}

		// Token: 0x06004189 RID: 16777 RVA: 0x003DE0A4 File Offset: 0x003DD0A4
		internal void ᜂ(string A_0)
		{
			int a_ = 18;
			for (;;)
			{
				IEnumerator enumerator = base.Document.Sections.GetEnumerator();
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_6E;
					}
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						goto IL_8F;
					case 1:
						try
						{
							num = 2;
							for (;;)
							{
								switch (num)
								{
								case 0:
									goto IL_13C;
								case 1:
									goto IL_ED;
								case 3:
								{
									if (!enumerator.MoveNext())
									{
										num = 4;
										continue;
									}
									Section section = (Section)enumerator.Current;
									num = 5;
									continue;
								}
								case 4:
									num = 0;
									continue;
								case 5:
								{
									Section section;
									if (section.Body.FormFields.ContainsName(A_0))
									{
										num = 1;
										continue;
									}
									break;
								}
								}
								IL_EF:
								num = 3;
								continue;
								goto IL_EF;
							}
							IL_ED:
							throw new ArgumentException(ClipboardData.b("㹷ᕹ๻፽ꁿ겋轢憐ﲓ뚕ﮙﮝ肟股", a_) + A_0 + ClipboardData.b("婷婹ᵻችꪉ憐뢕", a_));
							IL_13C:
							goto IL_66;
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
										goto IL_17F;
									case 1:
										if (disposable != null)
										{
											num = 2;
											continue;
										}
										goto IL_181;
									case 2:
										disposable.Dispose();
										num = 0;
										continue;
									}
									break;
								}
							}
							IL_17F:
							IL_181:;
						}
						goto IL_182;
						IL_66:
						num = 2;
						continue;
					case 2:
						IL_6E:
						if (base.Document.Bookmarks[A_0] != null)
						{
							num = 0;
							continue;
						}
						return;
					}
					break;
				}
			}
			IL_8F:
			IL_182:
			throw new ArgumentException(ClipboardData.b("㭷᭹ቻ好ꊁ낏ﮓﲙﮝ첟욡蒣톥솧\udea9쒫躭銯", a_) + A_0 + ClipboardData.b("婷婹ቻώ뺃ꚅﶏﶕ뢗좟芡힣펥쮧슩貫삭톯\udfb1톳隵\ud9b7횹캻\udbbdꆿꛁ뷃귇닉ꗋ뷍꓏ꇑ䀘", a_));
		}

		// Token: 0x0600418A RID: 16778 RVA: 0x003DE26C File Offset: 0x003DD26C
		private new void ᜀ(BuiltinStyle A_0)
		{
			int a_ = 9;
			ListStyle listStyle;
			ParagraphStyle paragraphStyle;
			for (;;)
			{
				if (true)
				{
				}
				string name = Style.ᜁ(A_0);
				listStyle = base.Document.ListStyles.FindByName(name);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5C;
					case 1:
						paragraphStyle = new ParagraphStyle(base.Document);
						paragraphStyle.Name = name;
						paragraphStyle.ApplyBaseStyle(ClipboardData.b("ⅮṰŲᡴᙶᕸ", a_));
						base.Document.Styles.Add(paragraphStyle);
						num = 2;
						continue;
					case 2:
						goto IL_B0;
					case 3:
						listStyle = (ListStyle)Style.CreateBuiltinStyle(A_0, StyleType.OtherStyle, base.Document);
						base.Document.ListStyles.Add(listStyle);
						num = 5;
						continue;
					case 4:
						if (paragraphStyle != null)
						{
							goto IL_140;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_5C;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					case 5:
						goto IL_B5;
					}
					break;
					IL_5C:
					if (listStyle == null)
					{
						num = 3;
						continue;
					}
					IL_B5:
					paragraphStyle = (base.Document.Styles.FindByName(listStyle.Name) as ParagraphStyle);
					num = 4;
				}
			}
			IL_B0:
			IL_140:
			this.ᜀ(paragraphStyle);
			this.ListFormat.ApplyStyle(listStyle.Name);
		}

		// Token: 0x0600418B RID: 16779 RVA: 0x003DE3D4 File Offset: 0x003DD3D4
		private void ᜃ()
		{
			int a_ = 10;
			for (;;)
			{
				ParagraphStyle paragraphStyle = base.Document.Styles.FindByName(ClipboardData.b("㹯ᵱٳ᭵᥷ᙹ", a_), StyleType.ParagraphStyle) as ParagraphStyle;
				int num = 1;
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_83;
					}
					if (true)
					{
					}
					if (false)
					{
					}
					switch (num)
					{
					case 0:
						IL_83:
						paragraphStyle = (ParagraphStyle)Style.CreateBuiltinStyle(BuiltinStyle.Normal, base.Document);
						base.Document.Styles.Add(paragraphStyle);
						num = 2;
						continue;
					case 1:
						if (paragraphStyle == null)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						return;
					}
					break;
				}
			}
		}

		// Token: 0x0600418C RID: 16780 RVA: 0x003DE498 File Offset: 0x003DD498
		internal override void Close()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_170;
				case 1:
					goto IL_BB;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_170;
					default:
					{
						if (false)
						{
						}
						int num2 = 0;
						num = 10;
						continue;
					}
					}
					break;
				case 3:
					this.m_pItemColl.Clear();
					this.m_pItemColl = null;
					num = 12;
					continue;
				case 5:
					if (this.m_prFormat != null)
					{
						num = 8;
						continue;
					}
					goto IL_BB;
				case 6:
				{
					int num2;
					if (num2 >= this.m_pItemColl.Count)
					{
						num = 3;
						continue;
					}
					ParagraphBase paragraphBase = this.m_pItemColl[num2];
					paragraphBase.Close();
					num2++;
					num = 13;
					continue;
				}
				case 7:
					if (this.ᜇ != null)
					{
						num = 11;
						continue;
					}
					goto IL_1C1;
				case 8:
					this.m_prFormat.Close();
					this.m_prFormat = null;
					num = 1;
					continue;
				case 9:
					if (this.m_pItemColl.Count > 0)
					{
						num = 2;
						continue;
					}
					goto IL_12D;
				case 10:
					goto IL_172;
				case 11:
					this.ᜇ.Close();
					this.ᜇ = null;
					num = 0;
					continue;
				case 12:
					goto IL_12D;
				case 13:
					goto IL_172;
				case 14:
					num = 9;
					continue;
				}
				if (true)
				{
				}
				if (this.m_pItemColl != null)
				{
					num = 14;
					continue;
				}
				goto IL_12D;
				IL_BB:
				num = 7;
				continue;
				IL_12D:
				num = 5;
				continue;
				IL_172:
				num = 6;
			}
			IL_170:
			IL_1C1:
			this.m_listFormat = null;
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x003DE670 File Offset: 0x003DD670
		internal void ᜌ()
		{
			int a_ = 7;
			int num = 6;
			ParagraphStyle paragraphStyle;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_12A;
				case 1:
					if (paragraphStyle == null)
					{
						num = 2;
						continue;
					}
					goto IL_136;
				case 2:
					paragraphStyle = new ParagraphStyle(base.Document);
					paragraphStyle.StyleId = 179;
					paragraphStyle.Name = ClipboardData.b("Ⅼٮɰݲ啴❶ᡸॺᱼ᡾", a_);
					paragraphStyle.NextStyle = ClipboardData.b("Ⅼٮɰݲ啴❶ᡸॺᱼ᡾", a_);
					base.Document.Styles.Add(paragraphStyle);
					goto IL_11F;
				case 3:
					goto IL_CC;
				case 4:
					if ((this.m_style as ParagraphStyle).StyleId == 179)
					{
						num = 3;
						continue;
					}
					goto IL_47;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_11F;
					default:
						if (false)
						{
						}
						num = 4;
						continue;
					}
					break;
				}
				if (this.m_style != null)
				{
					num = 5;
					continue;
				}
				IL_47:
				paragraphStyle = (base.Document.Styles.FindById(179) as ParagraphStyle);
				num = 1;
				continue;
				IL_11F:
				num = 0;
			}
			IL_CC:
			if (true)
			{
			}
			return;
			IL_12A:
			IL_136:
			this.m_style = paragraphStyle;
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x003DE7BC File Offset: 0x003DD7BC
		private void ᜂ()
		{
			int a_ = 7;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			this.ᜄ = new ParagraphItemCollection(this);
			TextRange textRange = (TextRange)base.Document.CreateParagraphItem(ParagraphItemType.TextRange);
			textRange.Text = ClipboardData.b("䵬", a_);
			textRange.CharacterFormat.ApplyBase(this.ᜇ);
			this.ᜄ.ᜀ(textRange);
			textRange.OwnerEmptyParagraph = this;
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x003DE858 File Offset: 0x003DD858
		internal FieldType ᜉ()
		{
			IDocumentObject documentObject;
			for (;;)
			{
				documentObject = this.Items.LastItem;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_9D;
					case 1:
						num = 4;
						continue;
					case 2:
						num = 7;
						continue;
					case 3:
						goto IL_D4;
					case 4:
						if (documentObject is Field)
						{
							num = 5;
							continue;
						}
						goto IL_FC;
					case 5:
						goto IL_61;
					case 6:
						if (documentObject != null)
						{
							num = 2;
							continue;
						}
						goto IL_D4;
					case 7:
						if (documentObject is Field)
						{
							num = 3;
							continue;
						}
						documentObject = documentObject.PreviousSibling;
						num = 8;
						continue;
					case 8:
						goto IL_9D;
					case 9:
						IL_DF:
						if (documentObject != null)
						{
							num = 1;
							continue;
						}
						goto IL_FC;
					}
					break;
					IL_9D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_DF;
					default:
						if (false)
						{
						}
						num = 6;
						continue;
					}
					IL_D4:
					num = 9;
				}
			}
			IL_61:
			return (documentObject as Field).Type;
			IL_FC:
			if (true)
			{
			}
			return FieldType.FieldUnknown;
		}

		// Token: 0x06004190 RID: 16784 RVA: 0x003DE970 File Offset: 0x003DD970
		internal Field ᜑ()
		{
			IDocumentObject documentObject;
			for (;;)
			{
				documentObject = this.Items.LastItem;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (documentObject is Field)
						{
							num = 6;
							continue;
						}
						goto IL_FF;
					case 1:
						num = 4;
						continue;
					case 2:
						if (documentObject != null)
						{
							num = 1;
							continue;
						}
						goto IL_D7;
					case 3:
						goto IL_D7;
					case 4:
						if (documentObject is Field)
						{
							num = 3;
							continue;
						}
						documentObject = documentObject.PreviousSibling;
						num = 8;
						continue;
					case 5:
						goto IL_A0;
					case 6:
						goto IL_61;
					case 7:
						IL_EC:
						if (documentObject != null)
						{
							num = 9;
							continue;
						}
						goto IL_FF;
					case 8:
						if (true)
						{
						}
						goto IL_A0;
					case 9:
						num = 0;
						continue;
					}
					break;
					IL_A0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_EC;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					IL_D7:
					num = 7;
				}
			}
			IL_61:
			return documentObject as Field;
			IL_FF:
			return null;
		}

		// Token: 0x06004191 RID: 16785 RVA: 0x003DEA80 File Offset: 0x003DDA80
		private bool ᜁ()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.NextSibling == null)
					{
						num = 12;
						continue;
					}
					return false;
				case 1:
					num = 15;
					continue;
				case 2:
				{
					Section section;
					if (section != null)
					{
						num = 1;
						continue;
					}
					return false;
				}
				case 4:
				{
					string text;
					if (text.Contains('\r'.ToString()))
					{
						return false;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_18B;
					default:
						if (false)
						{
						}
						num = 9;
						continue;
					}
					break;
				}
				case 5:
				{
					Section section = base.OwnerTextBody.Owner as Section;
					num = 2;
					continue;
				}
				case 6:
				{
					string text = this.ᜀ(this.Text);
					num = 14;
					continue;
				}
				case 7:
					if (base.OwnerTextBody != null)
					{
						num = 13;
						continue;
					}
					return false;
				case 8:
					num = 0;
					continue;
				case 9:
					return true;
				case 10:
					goto IL_18B;
				case 11:
					num = 4;
					continue;
				case 12:
					num = 7;
					continue;
				case 13:
					num = 10;
					continue;
				case 14:
				{
					if (true)
					{
					}
					Section section;
					if (section != null)
					{
						num = 11;
						continue;
					}
					return false;
				}
				case 15:
				{
					Section section;
					if (section.NextSibling != null)
					{
						num = 6;
						continue;
					}
					return false;
				}
				}
				if (this != null)
				{
					num = 8;
					continue;
				}
				return false;
				IL_18B:
				if (base.OwnerTextBody is HeaderFooter)
				{
					return false;
				}
				num = 5;
			}
			return true;
		}

		// Token: 0x06004192 RID: 16786 RVA: 0x003DEC34 File Offset: 0x003DDC34
		private new string ᜀ(string A_0)
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
			char newChar = '\r';
			char oldChar = '\n';
			A_0 = A_0.Replace(Environment.NewLine, newChar.ToString());
			A_0 = A_0.Replace(oldChar, newChar);
			A_0 = A_0.Replace('\a'.ToString(), string.Empty);
			A_0 = A_0.Replace('\b'.ToString(), string.Empty);
			return A_0;
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x003DECC0 File Offset: 0x003DDCC0
		internal override void MakeChanges(bool acceptChanges)
		{
			int num = 9;
			for (;;)
			{
				ParagraphBase paragraphBase;
				int num2;
				switch (num)
				{
				case 0:
					(paragraphBase as Footnote).TextBody.ᜂ(acceptChanges);
					num = 18;
					continue;
				case 1:
					goto IL_1A4;
				case 2:
					if (paragraphBase.IsInsertRevision)
					{
						num = 31;
						continue;
					}
					goto IL_352;
				case 3:
					goto IL_1A4;
				case 4:
					if (this.m_listFormat != null)
					{
						num = 25;
						continue;
					}
					goto IL_250;
				case 5:
					if (paragraphBase is TextBox)
					{
						num = 11;
						continue;
					}
					num = 16;
					continue;
				case 6:
					goto IL_224;
				case 7:
					if (num2 >= this.m_pItemColl.Count)
					{
						num = 22;
						continue;
					}
					paragraphBase = this.m_pItemColl[num2];
					num = 26;
					continue;
				case 8:
					if (!acceptChanges)
					{
						num = 37;
						continue;
					}
					goto IL_3CE;
				case 10:
					if (true)
					{
					}
					num = 35;
					continue;
				case 11:
					(paragraphBase as TextBox).Body.ᜂ(acceptChanges);
					num = 1;
					continue;
				case 12:
					this.m_listFormat.LFOStyleName = this.m_listFormat.NewLfoStyleName;
					num = 23;
					continue;
				case 13:
					goto IL_250;
				case 14:
					goto IL_3CE;
				case 15:
					this.m_listFormat.ListLevelNumber = this.m_listFormat.NewListLevelNumber;
					num = 20;
					continue;
				case 16:
					if (paragraphBase is Footnote)
					{
						num = 0;
						continue;
					}
					goto IL_1A4;
				case 17:
					if (this.m_listFormat.NewStyleName != string.Empty)
					{
						num = 24;
						continue;
					}
					goto IL_28A;
				case 18:
					goto IL_1A4;
				case 19:
					num = 4;
					continue;
				case 20:
					goto IL_1DB;
				case 21:
					goto IL_224;
				case 22:
					return;
				case 23:
					goto IL_2D9;
				case 24:
					this.m_listFormat.ApplyStyle(this.m_listFormat.NewStyleName);
					num = 33;
					continue;
				case 25:
					num = 17;
					continue;
				case 26:
					if (paragraphBase.IsDeleteRevision)
					{
						num = 30;
						continue;
					}
					goto IL_1B5;
				case 27:
					goto IL_423;
				case 28:
					if (!acceptChanges)
					{
						num = 14;
						continue;
					}
					goto IL_352;
				case 29:
					paragraphBase.ឪ();
					num = 27;
					continue;
				case 30:
					num = 8;
					continue;
				case 31:
					num = 28;
					continue;
				case 32:
					if (this.m_listFormat.NewLfoStyleName != null)
					{
						num = 12;
						continue;
					}
					goto IL_2D9;
				case 33:
					goto IL_28A;
				case 34:
					if (paragraphBase.IsChangedCFormat)
					{
						num = 10;
						continue;
					}
					goto IL_423;
				case 35:
					if (acceptChanges)
					{
						goto IL_423;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_321;
					default:
						if (false)
						{
						}
						num = 29;
						continue;
					}
					break;
				case 36:
					if (this.m_listFormat.NewListLevelNumber != -1)
					{
						num = 15;
						continue;
					}
					goto IL_1DB;
				case 37:
					goto IL_1B5;
				}
				if (acceptChanges)
				{
					num = 19;
					continue;
				}
				goto IL_250;
				IL_1A4:
				num2++;
				num = 21;
				continue;
				IL_1B5:
				num = 2;
				continue;
				IL_1DB:
				num = 32;
				continue;
				IL_224:
				num = 7;
				continue;
				IL_250:
				paragraphBase = null;
				num2 = 0;
				num = 6;
				continue;
				IL_28A:
				num = 36;
				continue;
				IL_321:
				num = 13;
				continue;
				IL_2D9:
				this.m_listFormat.OwnerParagraph.Format.ParaProps.ᜪ().ᜆ(9283);
				this.m_listFormat.OwnerParagraph.Format.ParaProps.ᜪ().ᜆ(50757);
				goto IL_321;
				IL_352:
				num = 34;
				continue;
				IL_3CE:
				this.m_pItemColl.RemoveAt(num2);
				num2--;
				num = 3;
				continue;
				IL_423:
				paragraphBase.ឨ();
				num = 5;
			}
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x003DF128 File Offset: 0x003DE128
		internal override void RemoveCFormatChanges()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					this.ᜇ.RemoveChanges();
					goto IL_5D;
				}
				if (true)
				{
				}
				if (this.ᜇ == null)
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
					num = 2;
					continue;
				}
				IL_5D:
				num = 0;
			}
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x003DF1A8 File Offset: 0x003DE1A8
		internal override void RemovePFormatChanges()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					return;
				case 2:
					this.m_prFormat.RemoveChanges();
					goto IL_5D;
				}
				if (true)
				{
				}
				if (this.m_prFormat == null)
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
					num = 2;
					continue;
				}
				IL_5D:
				num = 0;
			}
		}

		// Token: 0x06004196 RID: 16790 RVA: 0x003DF228 File Offset: 0x003DE228
		internal override void AcceptCChanges()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					goto IL_5D;
				case 2:
					this.ᜇ.AcceptChanges();
					goto IL_55;
				}
				if (this.ᜇ == null)
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
					num = 2;
					continue;
				}
				IL_55:
				num = 1;
			}
			IL_5D:
			if (true)
			{
			}
		}

		// Token: 0x06004197 RID: 16791 RVA: 0x003DF2A8 File Offset: 0x003DE2A8
		internal override void AcceptPChanges()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.m_prFormat.AcceptChanges();
					goto IL_5D;
				case 2:
					return;
				}
				if (true)
				{
				}
				if (this.m_prFormat == null)
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
					num = 0;
					continue;
				}
				IL_5D:
				num = 2;
			}
		}

		// Token: 0x06004198 RID: 16792 RVA: 0x003DF328 File Offset: 0x003DE328
		internal override bool CheckChangedPFormat()
		{
			if (this.m_prFormat != null)
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
					return this.m_prFormat.IsChangedFormat;
				}
			}
			return false;
		}

		// Token: 0x06004199 RID: 16793 RVA: 0x003DF37C File Offset: 0x003DE37C
		internal override bool CheckInsertRev()
		{
			if (this.ᜇ != null)
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
					return this.ᜇ.IsInsertRevision;
				}
			}
			return false;
		}

		// Token: 0x0600419A RID: 16794 RVA: 0x003DF3D0 File Offset: 0x003DE3D0
		internal override bool CheckDeleteRev()
		{
			if (this.ᜇ != null)
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
					return this.ᜇ.IsDeleteRevision;
				}
			}
			return false;
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x003DF424 File Offset: 0x003DE424
		internal override bool CheckChangedCFormat()
		{
			if (this.ᜇ != null)
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
					return this.ᜇ.IsChangedFormat;
				}
			}
			if (true)
			{
			}
			return false;
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x003DF478 File Offset: 0x003DE478
		internal bool \u1713()
		{
			switch (0)
			{
			default:
			{
				IEnumerator enumerator = this.m_pItemColl.GetEnumerator();
				bool result;
				try
				{
					int num = 8;
					for (;;)
					{
						switch (num)
						{
						case 0:
						{
							ParagraphBase paragraphBase;
							if (!paragraphBase.IsDeleteRevision)
							{
								num = 5;
								continue;
							}
							break;
						}
						case 1:
							num = 3;
							continue;
						case 2:
							goto IL_AF;
						case 3:
							goto IL_E2;
						case 4:
							result = false;
							num = 2;
							continue;
						case 5:
							num = 6;
							continue;
						case 6:
						{
							ParagraphBase paragraphBase;
							if (!paragraphBase.IsInsertRevision)
							{
								num = 4;
								continue;
							}
							break;
						}
						case 7:
						{
							if (!enumerator.MoveNext())
							{
								num = 1;
								continue;
							}
							ParagraphBase paragraphBase = (ParagraphBase)enumerator.Current;
							num = 0;
							continue;
						}
						}
						IL_60:
						num = 7;
						continue;
						goto IL_60;
					}
					IL_AF:
					return result;
					IL_E2:
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
								if (disposable == null)
								{
									goto IL_14F;
								}
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_14F;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case 2:
								goto IL_145;
							}
							break;
						}
					}
					IL_145:
					if (true)
					{
					}
					IL_14F:;
				}
				return result;
			}
			}
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x003DF5E8 File Offset: 0x003DE5E8
		internal override bool HasTrackedChanges()
		{
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					IEnumerator enumerator;
					switch (num)
					{
					case 0:
						goto IL_1F3;
					case 1:
						num = 4;
						continue;
					case 2:
						goto IL_67;
						try
						{
							bool result;
							for (;;)
							{
								IL_67:
								num = 2;
								for (;;)
								{
									switch (num)
									{
									case 0:
										result = true;
										num = 1;
										continue;
									case 1:
										goto IL_108;
									case 3:
									{
										ParagraphBase paragraphBase;
										if (paragraphBase.ឭ())
										{
											num = 0;
											continue;
										}
										break;
									}
									case 4:
										switch ((1 == 1) ? 1 : 0)
										{
										case 0:
										case 2:
											goto IL_67;
										default:
										{
											if (false)
											{
											}
											if (!enumerator.MoveNext())
											{
												num = 5;
												continue;
											}
											ParagraphBase paragraphBase = (ParagraphBase)enumerator.Current;
											num = 3;
											continue;
										}
										}
										break;
									case 5:
										num = 6;
										continue;
									case 6:
										goto IL_119;
									}
									IL_BF:
									num = 4;
									continue;
									goto IL_BF;
								}
							}
							IL_108:
							return result;
							IL_119:
							return false;
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
										goto IL_160;
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
										goto IL_162;
									}
									break;
								}
							}
							IL_160:
							IL_162:;
						}
						goto IL_163;
					case 3:
						num = 8;
						continue;
					case 4:
						if (base.IsChangedPFormat)
						{
							num = 0;
							continue;
						}
						goto IL_163;
					case 5:
						if (true)
						{
						}
						num = 6;
						continue;
					case 6:
						if (!base.IsDeleteRevision)
						{
							num = 3;
							continue;
						}
						return true;
					case 8:
						if (!base.IsChangedCFormat)
						{
							num = 1;
							continue;
						}
						return true;
					}
					if (!base.IsInsertRevision)
					{
						num = 5;
						continue;
					}
					return true;
					IL_163:
					enumerator = this.m_pItemColl.GetEnumerator();
					num = 2;
				}
				return false;
				IL_1F3:
				return true;
			}
			}
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x003DF7FC File Offset: 0x003DE7FC
		internal override void SetDeleteRev(bool check)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_50;
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
				case 1:
					return;
				case 2:
					goto IL_50;
				}
				if (this.ᜇ != null)
				{
					num = 2;
					continue;
				}
				break;
				IL_50:
				this.ᜇ.IsDeleteRevision = check;
				num = 1;
			}
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x003DF87C File Offset: 0x003DE87C
		internal override void SetInsertRev(bool check)
		{
			int num = 0;
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
						goto IL_50;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					return;
				case 2:
					goto IL_50;
				}
				if (this.ᜇ != null)
				{
					num = 2;
					continue;
				}
				break;
				IL_50:
				this.ᜇ.IsInsertRevision = check;
				num = 1;
			}
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x003DF8FC File Offset: 0x003DE8FC
		internal override void SetChangedCFormat(bool check)
		{
			int num = 1;
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
						goto IL_48;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 2:
					goto IL_48;
				}
				if (this.ᜇ != null)
				{
					num = 2;
					continue;
				}
				break;
				IL_48:
				if (true)
				{
				}
				this.ᜇ.IsChangedFormat = check;
				num = 0;
			}
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x003DF97C File Offset: 0x003DE97C
		internal override void SetChangedPFormat(bool check)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 0:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_50;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 1:
					goto IL_50;
				case 2:
					return;
				}
				if (true)
				{
				}
				if (this.m_prFormat != null)
				{
					num = 1;
					continue;
				}
				break;
				IL_50:
				this.m_prFormat.IsChangedFormat = check;
				num = 2;
			}
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x003DF9FC File Offset: 0x003DE9FC
		internal override BodyRegion GetNextTextBodyItem()
		{
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					if (base.Owner is Body)
					{
						num = 5;
						continue;
					}
					goto IL_166;
				case 1:
					goto IL_90;
				case 3:
					if (base.Owner is TableCell)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
				case 4:
					if (base.OwnerTextBody.Owner is Section)
					{
						num = 8;
						continue;
					}
					goto IL_166;
				case 5:
					num = 7;
					continue;
				case 6:
					goto IL_BF;
				case 7:
					if (base.OwnerTextBody.Owner is TextBox)
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
				case 8:
					goto IL_10F;
				case 9:
					goto IL_48;
				}
				if (base.NextSibling != null)
				{
					num = 9;
				}
				else
				{
					num = 3;
				}
			}
			IL_48:
			return base.NextSibling as BodyRegion;
			IL_4D:
			if (true)
			{
			}
			return (base.OwnerTextBody.Owner as TextBox).ᜄ();
			IL_90:
			goto IL_4D;
			IL_BF:
			return (base.Owner as TableCell).ᜋ();
			IL_10F:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_4D;
			default:
				if (false)
				{
				}
				return base.GetNextInSection(base.OwnerTextBody.Owner as Section);
			}
			IL_166:
			return null;
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x003DFB70 File Offset: 0x003DEB70
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 7;
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
			writer.WriteValue(ClipboardData.b("ᥬ᙮Űᙲ", a_), ClipboardData.b("㵬๮Ͱቲቴնᡸ୺ᕼ", a_));
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x003DFBE0 File Offset: 0x003DEBE0
		protected override void InitXDLSHolder()
		{
			int a_ = 5;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			base.XDLSHolder.AddRefElement(ClipboardData.b("ᡪᥬ᙮ᵰᙲ", a_), this.GetStyle());
			base.XDLSHolder.AddElement(ClipboardData.b("᭪౬ᵮၰᑲݴᙶॸ፺偼᥾ﶈ", a_), this.m_prFormat);
			base.XDLSHolder.AddElement(ClipboardData.b("ࡪլ๮ͰቲᙴͶᱸॺ偼᥾ﶈ", a_), this.ᜇ);
			base.XDLSHolder.AddElement(ClipboardData.b("ݪѬᱮհ干፴ᡶ୸ᙺᱼ୾", a_), this.ListFormat);
			base.XDLSHolder.AddElement(ClipboardData.b("ɪᥬ੮ᱰr", a_), this.m_pItemColl);
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x003DFCC0 File Offset: 0x003DECC0
		protected override void RestoreReference(string name, int index)
		{
			int a_ = 6;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_A4;
				case 2:
					if (true)
					{
					}
					goto IL_4D;
				case 3:
					if (index > -1)
					{
						num = 2;
						continue;
					}
					goto IL_A4;
				case 4:
					num = 3;
					continue;
				}
				if (name == ClipboardData.b("Ὣᩭ९ṱᅳ", a_))
				{
					num = 4;
					continue;
				}
				goto IL_A4;
				IL_4D:
				this.m_style = (base.Document.Styles[index] as ParagraphStyle);
				this.ᜄ();
				num = 0;
				continue;
				IL_A4:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_4D;
				default:
					goto IL_BA;
				}
			}
			IL_BA:
			if (false)
			{
			}
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x003DFD90 File Offset: 0x003DED90
		void spr\u1AB8.Draw(spr\u19E0 dc, sprᦰ ltWidget)
		{
			int a_ = 6;
			switch (0)
			{
			default:
				for (;;)
				{
					base.DrawImpl(dc, ltWidget);
					int num = 11;
					for (;;)
					{
						bool flag;
						bool flag2;
						switch (num)
						{
						case 0:
						{
							Break @break;
							if (@break.BreakType == BreakType.PageBreak)
							{
								num = 19;
								continue;
							}
							goto IL_174;
						}
						case 1:
							goto IL_174;
						case 2:
							num = 12;
							continue;
						case 3:
							if (flag)
							{
								num = 17;
								continue;
							}
							return;
						case 4:
							if (!this.ᜉ)
							{
								goto IL_296;
							}
							goto IL_174;
						case 5:
							flag = true;
							this.ᜉ = true;
							num = 1;
							continue;
						case 6:
							flag2 = false;
							goto IL_194;
						case 7:
						{
							Paragraph paragraph = (ltWidget.ᜂ() as sprᴛ).ᜁ() as Paragraph;
							int count = paragraph.ChildObjects.Count;
							int num2 = 0;
							num = 8;
							continue;
						}
						case 8:
							goto IL_D5;
						case 9:
							if (ltWidget.ᜂ() is sprᴛ)
							{
								num = 22;
								continue;
							}
							goto IL_174;
						case 10:
							if ((ltWidget.ᜂ() as sprᴛ).ᜁ() is Paragraph)
							{
								num = 7;
								continue;
							}
							goto IL_174;
						case 11:
							if (ltWidget.ᜊ().Count > 0)
							{
								num = 23;
								continue;
							}
							num = 6;
							continue;
						case 12:
						{
							Paragraph paragraph;
							int count;
							if (paragraph.ChildObjects[count - 1 - (ltWidget.ᜂ() as sprᴛ).ᜂ()].DocumentObjectType.ToString() == ClipboardData.b("⹫ᱭᕯ፱έ", a_))
							{
								num = 20;
								continue;
							}
							goto IL_174;
						}
						case 13:
							return;
						case 14:
						{
							if (true)
							{
							}
							int count;
							if (count > (ltWidget.ᜂ() as sprᴛ).ᜂ())
							{
								num = 2;
								continue;
							}
							goto IL_174;
						}
						case 15:
						{
							int num2;
							if (num2 >= ltWidget.ᜊ().Count)
							{
								num = 18;
								continue;
							}
							sprᦰ a_2 = ltWidget.ᜊ()[num2];
							dc.ᜅ(a_2);
							num2++;
							num = 21;
							continue;
						}
						case 16:
							flag2 = (ltWidget.ᜊ()[0].ᜂ() == this);
							goto IL_194;
						case 17:
							dc.ᜁ(this, ltWidget);
							num = 13;
							continue;
						case 18:
							num = 14;
							continue;
						case 19:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_296;
							default:
								if (false)
								{
								}
								num = 4;
								continue;
							}
							break;
						case 20:
						{
							Paragraph paragraph;
							int count;
							DocumentObject documentObject = paragraph.ChildObjects[count - 1 - (ltWidget.ᜂ() as sprᴛ).ᜂ()];
							Break @break = documentObject as Break;
							num = 0;
							continue;
						}
						case 21:
							goto IL_D5;
						case 22:
							num = 10;
							continue;
						case 23:
							num = 16;
							continue;
						}
						break;
						IL_D5:
						num = 15;
						continue;
						IL_174:
						num = 3;
						continue;
						IL_194:
						flag = flag2;
						num = 9;
						continue;
						IL_296:
						num = 5;
					}
				}
				return;
			}
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x003E00F8 File Offset: 0x003DF0F8
		protected override void CreateLayoutInfo()
		{
			for (;;)
			{
				this.ᜀ = new Paragraph.ᜀ(this);
				int num = 17;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜀ())
						{
							num = 11;
							continue;
						}
						goto IL_28A;
					case 1:
						if (this.RemoveEmpty)
						{
							num = 3;
							continue;
						}
						goto IL_205;
					case 2:
						num = 7;
						continue;
					case 3:
						this.ᜀ.ᜁ(true);
						num = 5;
						continue;
					case 4:
						if ((base.OwnerTextBody as TableCell).CellFormat.TextDirection != TextDirection.LeftToRight)
						{
							num = 16;
							continue;
						}
						goto IL_230;
					case 5:
						goto IL_205;
					case 6:
						num = 13;
						continue;
					case 7:
						if (this.Format.IsFrame)
						{
							num = 15;
							continue;
						}
						return;
					case 8:
						if (this.IsInCell)
						{
							num = 22;
							continue;
						}
						goto IL_230;
					case 9:
						num = 20;
						continue;
					case 10:
						if (!(base.Owner is TableCell))
						{
							num = 2;
							continue;
						}
						return;
					case 11:
						goto IL_1DF;
					case 12:
						goto IL_28A;
					case 13:
						if (this.BreakCharacterFormat.HasValue(53))
						{
							num = 9;
							continue;
						}
						goto IL_D9;
					case 14:
						goto IL_D9;
					case 15:
						this.ᜀ.ᜇ(true);
						num = 19;
						continue;
					case 16:
						this.ᜀ.ᜆ(true);
						if (true)
						{
						}
						num = 21;
						continue;
					case 17:
						if (this.Items.Count == 0)
						{
							num = 6;
							continue;
						}
						goto IL_D9;
					case 18:
						if (this.Text == string.Empty)
						{
							num = 23;
							continue;
						}
						goto IL_205;
					case 19:
						return;
					case 20:
						if (!this.BreakCharacterFormat.Hidden)
						{
							num = 14;
							continue;
						}
						goto IL_1DF;
					case 21:
						goto IL_230;
					case 22:
						goto IL_1AD;
					case 23:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1AD;
						default:
							if (false)
							{
							}
							num = 1;
							continue;
						}
						break;
					}
					break;
					IL_D9:
					num = 0;
					continue;
					IL_1AD:
					num = 4;
					continue;
					IL_1DF:
					this.ᜀ.ᜁ(true);
					num = 12;
					continue;
					IL_205:
					num = 10;
					continue;
					IL_230:
					num = 18;
					continue;
					IL_28A:
					num = 8;
				}
			}
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x003E03B4 File Offset: 0x003DF3B4
		private new bool ᜀ()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (this.Items.Count == 0)
					{
						num = 5;
						continue;
					}
					return false;
				case 2:
					if ((base.Owner as TableCell).LastParagraph.Equals(this))
					{
						num = 3;
						continue;
					}
					return false;
				case 3:
					num = 4;
					continue;
				case 4:
					if (base.PreviousSibling != null)
					{
						num = 6;
						continue;
					}
					return false;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 6:
					goto IL_72;
				case 7:
					num = 1;
					continue;
				}
				goto IL_30;
				IL_4D:
				num = 7;
				continue;
				IL_30:
				if (true)
				{
				}
				if (this.IsInCell)
				{
					goto IL_4D;
				}
				return false;
			}
			IL_72:
			return base.PreviousSibling is Table;
		}

		// Token: 0x04003381 RID: 13185
		private new const string ᜀ = "Normal";

		// Token: 0x04003382 RID: 13186
		private const int ᜁ = 179;

		// Token: 0x04003383 RID: 13187
		private const int ᜂ = 4094;

		// Token: 0x04003384 RID: 13188
		protected IParagraphStyle m_style;

		// Token: 0x04003385 RID: 13189
		private StringBuilder ᜃ;

		// Token: 0x04003386 RID: 13190
		protected ParagraphFormat m_prFormat;

		// Token: 0x04003387 RID: 13191
		protected ListFormat m_listFormat;

		// Token: 0x04003388 RID: 13192
		protected ParagraphItemCollection m_pItemColl;

		// Token: 0x04003389 RID: 13193
		private new ParagraphItemCollection ᜄ;

		// Token: 0x0400338A RID: 13194
		private bool ᜅ;

		// Token: 0x0400338B RID: 13195
		internal float ᜆ;

		// Token: 0x0400338C RID: 13196
		private CharacterFormat ᜇ;

		// Token: 0x0400338D RID: 13197
		private Body ᜈ;

		// Token: 0x0400338E RID: 13198
		private bool ᜉ;

		// Token: 0x0400338F RID: 13199
		private bool ᜊ;

		// Token: 0x04003390 RID: 13200
		internal bool ᜋ;

		// Token: 0x020004F0 RID: 1264
		internal new class ᜀ : sprℐ
		{
			// Token: 0x060041D3 RID: 16851 RVA: 0x003E04B4 File Offset: 0x003DF4B4
			protected IDocument ᜅ()
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
				return this.ᜀ.Document;
			}

			// Token: 0x060041D4 RID: 16852 RVA: 0x003E04FC File Offset: 0x003DF4FC
			public ᜀ(Paragraph A_0) : base(ChildrenLayoutDirection.Horizontal)
			{
				base.\u1712(true);
				this.ᜀ = A_0;
				this.ᜄ();
				this.ᜀ();
				this.ᜁ();
				this.ᜃ();
				this.ᜂ();
			}

			// Token: 0x060041D5 RID: 16853 RVA: 0x003E053C File Offset: 0x003DF53C
			private void ᜄ()
			{
				int num = 15;
				for (;;)
				{
					switch (num)
					{
					case 0:
						base.\u1717(true);
						num = 12;
						continue;
					case 1:
					{
						float num2 = -num2;
						num = 7;
						continue;
					}
					case 2:
						if ((this.ᜀ.Owner.Owner as TableRow).HeightType == TableRowHeightType.Exactly)
						{
							num = 3;
							continue;
						}
						goto IL_184;
					case 3:
					{
						float num2 = (this.ᜀ.Owner.Owner as TableRow).Height;
						num = 8;
						continue;
					}
					case 4:
					{
						float num2;
						if (num2 > 1f)
						{
							goto IL_1CE;
						}
						return;
					}
					case 5:
					{
						bool flag;
						if (!flag)
						{
							num = 0;
							continue;
						}
						return;
					}
					case 6:
						num = 11;
						continue;
					case 7:
						goto IL_1BB;
					case 8:
					{
						if (true)
						{
						}
						float num2;
						if (num2 < 0f)
						{
							num = 1;
							continue;
						}
						goto IL_1BB;
					}
					case 9:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_1CE;
						default:
						{
							if (false)
							{
							}
							ushort num3 = (ushort)this.ᜀ.Format.FrameHeight;
							bool flag = (num3 & 32768) != 0;
							num = 5;
							continue;
						}
						}
						break;
					case 10:
						if (this.ᜀ != null)
						{
							num = 6;
							continue;
						}
						return;
					case 11:
						if (this.ᜀ.Format.IsFrame)
						{
							num = 9;
							continue;
						}
						return;
					case 12:
						goto IL_1B9;
					case 13:
						num = 2;
						continue;
					case 14:
						goto IL_1D9;
					}
					if (this.ᜀ.IsInCell)
					{
						num = 13;
						continue;
					}
					IL_184:
					num = 10;
					continue;
					IL_1BB:
					num = 4;
					continue;
					IL_1CE:
					num = 14;
				}
				IL_1B9:
				return;
				IL_1D9:
				base.\u1717(true);
			}

			// Token: 0x060041D6 RID: 16854 RVA: 0x003E0738 File Offset: 0x003DF738
			private void ᜃ()
			{
				switch (0)
				{
				default:
				{
					int num = 71;
					for (;;)
					{
						ListLevel listLevel;
						ListFormat listFormat;
						ListLevel listLevel2;
						switch (num)
						{
						case 0:
						{
							ListStyle currentListStyle;
							if (currentListStyle.Levels.Count <= 0)
							{
								num = 57;
								continue;
							}
							num = 46;
							continue;
						}
						case 1:
							goto IL_540;
						case 2:
						{
							ParagraphStyle paragraphStyle;
							base.ᜃ(paragraphStyle.ListFormat.ListLevelNumber);
							num = 51;
							continue;
						}
						case 3:
							base.ᜃ(listLevel.ParagraphFormat.FirstLineIndent);
							num = 53;
							continue;
						case 4:
							num = 56;
							continue;
						case 5:
						{
							ParagraphStyle paragraphStyle;
							base.ᜃ(paragraphStyle.ParagraphFormat.FirstLineIndent);
							num = 43;
							continue;
						}
						case 6:
							goto IL_24F;
						case 7:
							if (this.ᜀ.ListFormat.ListType != ListType.NoList)
							{
								num = 18;
								continue;
							}
							num = 60;
							continue;
						case 8:
							if (base.ᜤ().PropertiesHash.ContainsKey(7))
							{
								num = 78;
								continue;
							}
							goto IL_759;
						case 9:
							if (listFormat.CurrentListStyle != null)
							{
								num = 77;
								continue;
							}
							return;
						case 10:
							num = 68;
							continue;
						case 11:
						{
							ParagraphStyle paragraphStyle;
							if (paragraphStyle.ParagraphFormat.HasValue(5))
							{
								num = 5;
								continue;
							}
							goto IL_4BE;
						}
						case 12:
							if (this.ᜀ.ListFormat.ListType == ListType.NoList)
							{
								num = 59;
								continue;
							}
							goto IL_4BE;
						case 13:
						{
							if (this.ᜀ.SectionEndMark)
							{
								num = 42;
								continue;
							}
							listFormat = null;
							ParagraphStyle paragraphStyle = this.ᜀ.ParaStyle;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_748;
							default:
								if (false)
								{
								}
								num = 7;
								continue;
							}
							break;
						}
						case 14:
							if (listFormat != null)
							{
								num = 25;
								continue;
							}
							return;
						case 15:
							goto IL_6A8;
						case 16:
							if (base.\u171B() < 0f)
							{
								num = 55;
								continue;
							}
							goto IL_415;
						case 17:
							goto IL_95C;
						case 18:
							listFormat = this.ᜀ.ListFormat;
							num = 54;
							continue;
						case 19:
						{
							ParagraphStyle paragraphStyle;
							if (paragraphStyle.ListFormat.HasKey(0))
							{
								num = 2;
								continue;
							}
							goto IL_64A;
						}
						case 20:
							base.ᜰ().ᜂ((double)listLevel.ParagraphFormat.LeftIndent);
							num = 27;
							continue;
						case 21:
							base.ᜃ(this.ᜀ.ListFormat.ListLevelNumber);
							num = 41;
							continue;
						case 22:
						{
							spr\u177D spr_u177D = null;
							num = 47;
							continue;
						}
						case 23:
							if (base.ᜰ().ᜃ() == 0.0)
							{
								num = 73;
								continue;
							}
							goto IL_415;
						case 24:
							if (listLevel.ParagraphFormat.HasValue(5))
							{
								num = 3;
								continue;
							}
							goto IL_185;
						case 25:
							num = 9;
							continue;
						case 26:
							goto IL_93F;
						case 27:
							goto IL_1B8;
						case 28:
						{
							spr\u177D spr_u177D = (this.ᜅ() as Document).ListOverrides.ᜀ(listFormat.LFOStyleName);
							num = 63;
							continue;
						}
						case 29:
							if (this.ᜀ.ListFormat.ListType == ListType.NoList)
							{
								num = 4;
								continue;
							}
							goto IL_540;
						case 30:
						{
							spr\u177D spr_u177D;
							if (spr_u177D != null)
							{
								num = 58;
								continue;
							}
							goto IL_9EA;
						}
						case 31:
						{
							spr\u177D spr_u177D;
							listLevel = spr_u177D.ᜃ().ᜀ(base.\u171D()).OverrideListLevel;
							goto IL_748;
						}
						case 32:
							goto IL_5D5;
						case 33:
						{
							spr\u177D spr_u177D;
							if (spr_u177D.ᜃ().ᜁ(base.\u171D()))
							{
								num = 10;
								continue;
							}
							goto IL_9EA;
						}
						case 34:
							if (this.ᜀ.Format.HasValue(5))
							{
								num = 52;
								continue;
							}
							goto IL_5D5;
						case 35:
						{
							ListStyle currentListStyle;
							if (currentListStyle.ListType != ListType.Numbered)
							{
								num = 61;
								continue;
							}
							goto IL_6A8;
						}
						case 36:
							return;
						case 37:
							this.ᜀ(listLevel);
							num = 70;
							continue;
						case 38:
							if (this.ᜀ.Format.HasValue(2))
							{
								num = 45;
								continue;
							}
							goto IL_95C;
						case 39:
							num = 69;
							continue;
						case 40:
							if (listLevel != null)
							{
								num = 22;
								continue;
							}
							return;
						case 41:
							goto IL_64A;
						case 42:
							return;
						case 43:
							if (true)
							{
							}
							goto IL_4BE;
						case 44:
						{
							ParagraphStyle paragraphStyle;
							listFormat = paragraphStyle.ListFormat;
							num = 6;
							continue;
						}
						case 45:
							base.ᜰ().ᜂ((double)this.ᜀ.Format.LeftIndent);
							num = 17;
							continue;
						case 46:
						{
							ListStyle currentListStyle;
							listLevel2 = currentListStyle.GetNearLevel(base.\u171D());
							goto IL_573;
						}
						case 47:
							if (listFormat.LFOStyleName != null)
							{
								num = 39;
								continue;
							}
							goto IL_4F1;
						case 48:
							num = 13;
							continue;
						case 49:
						{
							ParagraphStyle paragraphStyle;
							base.ᜰ().ᜂ((double)paragraphStyle.ParagraphFormat.LeftIndent);
							num = 1;
							continue;
						}
						case 50:
							goto IL_9EA;
						case 51:
							goto IL_64A;
						case 52:
							base.ᜃ(this.ᜀ.Format.FirstLineIndent);
							num = 32;
							continue;
						case 53:
							goto IL_185;
						case 54:
							goto IL_24F;
						case 55:
							num = 23;
							continue;
						case 56:
						{
							ParagraphStyle paragraphStyle;
							if (paragraphStyle.ParagraphFormat.HasValue(2))
							{
								num = 49;
								continue;
							}
							goto IL_540;
						}
						case 57:
							num = 72;
							continue;
						case 58:
							num = 33;
							continue;
						case 59:
							num = 11;
							continue;
						case 60:
						{
							ParagraphStyle paragraphStyle;
							if (paragraphStyle.ListFormat.ListType != ListType.NoList)
							{
								num = 44;
								continue;
							}
							goto IL_24F;
						}
						case 61:
							num = 64;
							continue;
						case 62:
							goto IL_415;
						case 63:
							goto IL_4F1;
						case 64:
						{
							ListStyle currentListStyle;
							if (currentListStyle.ListType == ListType.Bulleted)
							{
								num = 15;
								continue;
							}
							goto IL_415;
						}
						case 65:
							if (listLevel.FollowCharacter == FollowCharacterType.Tab)
							{
								num = 37;
								continue;
							}
							this.ᜁ(listLevel);
							num = 26;
							continue;
						case 66:
							if (listLevel.ParagraphFormat.HasValue(2))
							{
								num = 20;
								continue;
							}
							goto IL_1B8;
						case 67:
							if (!this.ᜀ.Format.HasValue(2))
							{
								num = 75;
								continue;
							}
							goto IL_415;
						case 68:
						{
							spr\u177D spr_u177D;
							if (spr_u177D.ᜃ().ᜀ(base.\u171D()).OverrideFormatting)
							{
								num = 31;
								continue;
							}
							goto IL_9EA;
						}
						case 69:
							if (listFormat.LFOStyleName.Length > 0)
							{
								num = 28;
								continue;
							}
							goto IL_4F1;
						case 70:
							goto IL_93F;
						case 72:
							listLevel2 = null;
							goto IL_573;
						case 73:
							num = 67;
							continue;
						case 74:
							goto IL_759;
						case 75:
							base.ᜰ().ᜂ((double)Math.Abs(base.\u171B()));
							num = 62;
							continue;
						case 76:
							if (this.ᜀ.ListFormat.HasKey(0))
							{
								num = 21;
								continue;
							}
							num = 19;
							continue;
						case 77:
						{
							ListStyle currentListStyle = listFormat.CurrentListStyle;
							base.ᜃ(0);
							num = 76;
							continue;
						}
						case 78:
							base.ᜤ().UnderlineStyle = UnderlineStyle.None;
							base.ᜤ().PropertiesHash.Remove(7);
							num = 74;
							continue;
						}
						if (!this.ᜀ.ListFormat.IsEmptyList)
						{
							num = 48;
							continue;
						}
						break;
						IL_185:
						num = 12;
						continue;
						IL_1B8:
						num = 29;
						continue;
						IL_24F:
						num = 14;
						continue;
						IL_415:
						base.ᜀ((this.ᜅ() as Document).ᜀ(this.ᜀ, listFormat, listLevel));
						base.ᜀ(new CharacterFormat(this.ᜅ()));
						base.ᜤ().ImportContainer(this.ᜀ.BreakCharacterFormat);
						base.ᜤ().ᜃ(this.ᜀ.BreakCharacterFormat);
						base.ᜤ().ApplyBase(this.ᜀ.BreakCharacterFormat.BaseFormat);
						num = 8;
						continue;
						IL_4BE:
						num = 34;
						continue;
						IL_4F1:
						num = 30;
						continue;
						IL_540:
						num = 38;
						continue;
						IL_573:
						listLevel = listLevel2;
						num = 40;
						continue;
						IL_5D5:
						num = 16;
						continue;
						IL_64A:
						num = 0;
						continue;
						IL_6A8:
						num = 66;
						continue;
						IL_748:
						num = 50;
						continue;
						IL_759:
						this.ᜀ(listLevel.CharacterFormat, base.ᜤ());
						num = 65;
						continue;
						IL_93F:
						base.ᜀ(listLevel.NumberAlignment);
						num = 36;
						continue;
						IL_95C:
						num = 24;
						continue;
						IL_9EA:
						num = 35;
					}
					return;
				}
				}
			}

			// Token: 0x060041D7 RID: 16855 RVA: 0x003E1158 File Offset: 0x003E0158
			private void ᜁ(ListLevel A_0)
			{
				int a_ = 15;
				switch (0)
				{
				default:
				{
					spr\u19E0 spr_u19E;
					float num;
					for (;;)
					{
						spr_u19E = new spr\u19E0();
						SizeF sizeF = spr_u19E.ᜁ(base.ᜡ(), base.ᜤ().Font, null);
						num = 0f;
						int num2 = 3;
						for (;;)
						{
							switch (num2)
							{
							case 0:
								goto IL_A3;
							case 1:
								goto IL_C7;
							case 2:
								if (A_0.FollowCharacter == FollowCharacterType.Space)
								{
									num2 = 1;
									continue;
								}
								goto IL_167;
							case 3:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_F9;
								default:
									if (false)
									{
									}
									if (A_0.NumberAlignment == ListNumberAlignment.Left)
									{
										num2 = 5;
										continue;
									}
									if (true)
									{
									}
									num2 = 7;
									continue;
								}
								break;
							case 4:
								goto IL_F9;
							case 5:
								num = sizeF.Width;
								num2 = 4;
								continue;
							case 6:
								num = sizeF.Width / 2f;
								num2 = 0;
								continue;
							case 7:
								if (A_0.NumberAlignment == ListNumberAlignment.Center)
								{
									num2 = 6;
									continue;
								}
								goto IL_A3;
							}
							break;
							IL_A3:
							num2 = 2;
							continue;
							IL_F9:
							goto IL_A3;
						}
					}
					IL_C7:
					base.ᜅ(num + spr_u19E.ᜁ(ClipboardData.b("啴", a_), base.ᜤ().Font, null).Width);
					return;
					IL_167:
					base.ᜅ(num);
					return;
				}
				}
			}

			// Token: 0x060041D8 RID: 16856 RVA: 0x003E12D4 File Offset: 0x003E02D4
			private void ᜀ(ListLevel A_0)
			{
				Paragraph.ᜁ ᜁ;
				for (;;)
				{
					ᜁ = new Paragraph.ᜁ(this.ᜀ);
					ᜁ.ᜀ(this.ᜀ.ParaStyle.ParagraphFormat);
					ᜁ.ᜀ(A_0.ParagraphFormat);
					spr\u19E0 spr_u19E = new spr\u19E0();
					SizeF sizeF = spr_u19E.ᜁ(base.ᜡ(), base.ᜤ().Font, null);
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.ᜀ(ᜁ, sizeF.Width / 2f);
							num = 1;
							continue;
						case 1:
							goto IL_AD;
						case 2:
							if (A_0.NumberAlignment == ListNumberAlignment.Center)
							{
								num = 0;
								continue;
							}
							this.ᜀ(ᜁ, 0f);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_AD;
							default:
								if (false)
								{
								}
								num = 6;
								continue;
							}
							break;
						case 3:
							this.ᜀ(ᜁ, sizeF.Width);
							num = 5;
							continue;
						case 4:
							if (A_0.NumberAlignment == ListNumberAlignment.Left)
							{
								num = 3;
								continue;
							}
							num = 2;
							continue;
						case 5:
							goto IL_EC;
						case 6:
							goto IL_133;
						}
						break;
					}
				}
				IL_AD:
				goto IL_135;
				IL_EC:
				if (true)
				{
				}
				IL_133:
				IL_135:
				base.ᜀ(ᜁ.ᜃ);
			}

			// Token: 0x060041D9 RID: 16857 RVA: 0x003E1424 File Offset: 0x003E0424
			private void ᜀ(Paragraph.ᜁ A_0, float A_1)
			{
				int num = 7;
				float num3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_1BF;
					case 1:
						num = 14;
						continue;
					case 2:
						goto IL_83;
					case 3:
						num = 11;
						continue;
					case 4:
						num = 5;
						continue;
					case 5:
						if (A_0.ᜃ.ᜂ() != 0f)
						{
							num = 0;
							continue;
						}
						goto IL_85;
					case 6:
						goto IL_97;
					case 8:
						num = 12;
						continue;
					case 9:
						if (A_1 <= Math.Abs(base.\u171B()))
						{
							num = 3;
							continue;
						}
						goto IL_151;
					case 10:
						goto IL_15C;
					case 11:
						if ((this.ᜅ() as Document).UseHangingIndentAsListTab)
						{
							num = 4;
							continue;
						}
						goto IL_151;
					case 12:
						if (A_1 <= Math.Abs(base.\u171B()))
						{
							num = 2;
							continue;
						}
						goto IL_17E;
					case 13:
						goto IL_192;
					case 14:
						if ((this.ᜅ() as Document).UseHangingIndentAsListTab)
						{
							return;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_15C;
						default:
							if (false)
							{
							}
							num = 6;
							continue;
						}
						break;
					}
					if (base.ᜢ() >= A_1)
					{
						num = 1;
						continue;
					}
					IL_97:
					float num2 = (float)((double)A_1 + base.ᜰ().ᜃ() + (double)base.\u171B());
					num3 = (float)A_0.ᜀ((double)num2, null);
					if (true)
					{
					}
					num = 9;
					continue;
					IL_151:
					num = 10;
					continue;
					IL_15C:
					if (A_0.ᜃ.ᜂ() == 0f)
					{
						num = 8;
						continue;
					}
					IL_17E:
					base.ᜅ(A_1 + num3);
					num = 13;
				}
				IL_83:
				base.ᜅ(Math.Min(A_1 + num3, Math.Abs(base.\u171B())));
				return;
				IL_85:
				base.ᜅ(Math.Abs(base.\u171B()));
				return;
				IL_192:
				return;
				IL_1BF:
				base.ᜅ(Math.Min(A_1 + num3, Math.Abs(base.\u171B())));
			}

			// Token: 0x060041DA RID: 16858 RVA: 0x003E1644 File Offset: 0x003E0644
			private void ᜀ(CharacterFormat A_0, CharacterFormat A_1)
			{
				int num = 53;
				for (;;)
				{
					switch (num)
					{
					case 0:
						A_1.TextBackgroundColor = A_0.TextBackgroundColor;
						num = 59;
						continue;
					case 1:
						goto IL_295;
					case 2:
						if (A_0.HasValue(4))
						{
							num = 13;
							continue;
						}
						goto IL_16F;
					case 3:
						A_1.FieldVanish = A_0.FieldVanish;
						num = 15;
						continue;
					case 4:
						goto IL_5EE;
					case 5:
						goto IL_5AD;
					case 6:
						if (A_0.HasValue(59))
						{
							num = 40;
							continue;
						}
						goto IL_339;
					case 7:
						goto IL_22E;
					case 8:
						if (A_0.HasValue(2))
						{
							num = 34;
							continue;
						}
						goto IL_41A;
					case 9:
						if (A_0.HasValue(50))
						{
							num = 12;
							continue;
						}
						goto IL_295;
					case 10:
						goto IL_41A;
					case 11:
						A_1.CharacterSpacing = A_0.CharacterSpacing;
						num = 57;
						continue;
					case 12:
						A_1.IsShadow = A_0.IsShadow;
						num = 1;
						continue;
					case 13:
						A_1.Bold = A_0.Bold;
						num = 52;
						continue;
					case 14:
						A_1.Italic = A_0.Italic;
						num = 7;
						continue;
					case 15:
						goto IL_1EA;
					case 16:
						goto IL_469;
					case 17:
						if (A_0.HasValue(55))
						{
							num = 31;
							continue;
						}
						return;
					case 18:
						if (A_0.HasValue(14))
						{
							num = 47;
							continue;
						}
						goto IL_585;
					case 19:
						goto IL_147;
					case 20:
						goto IL_339;
					case 21:
						goto IL_441;
					case 22:
						if (A_0.HasValue(53))
						{
							num = 46;
							continue;
						}
						goto IL_5AD;
					case 23:
						if (A_0.HasValue(5))
						{
							num = 14;
							continue;
						}
						goto IL_22E;
					case 24:
						A_1.Engrave = A_0.Engrave;
						num = 50;
						continue;
					case 25:
						A_1.SubSuperScript = A_0.SubSuperScript;
						num = 36;
						continue;
					case 26:
						goto IL_311;
					case 27:
						A_1.Bidi = true;
						A_1.FontNameBidi = A_0.FontNameBidi;
						A_1.FontSizeBidi = A_0.FontSizeBidi;
						num = 21;
						continue;
					case 28:
						A_1.UnderlineStyle = A_0.UnderlineStyle;
						num = 26;
						continue;
					case 29:
						if (A_0.HasValue(63))
						{
							num = 48;
							continue;
						}
						goto IL_469;
					case 30:
						if (A_0.HasValue(52))
						{
							num = 24;
							continue;
						}
						goto IL_4D2;
					case 31:
						A_1.IsSmallCaps = A_0.IsSmallCaps;
						if (true)
						{
						}
						num = 45;
						continue;
					case 32:
						if (A_0.HasValue(7))
						{
							num = 28;
							continue;
						}
						goto IL_311;
					case 33:
						A_1.FontSize = A_0.FontSize;
						num = 49;
						continue;
					case 34:
						A_1.FontName = A_0.FontName;
						num = 10;
						continue;
					case 35:
						if (!A_0.Bidi)
						{
							goto IL_441;
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
							num = 27;
							continue;
						}
						break;
					case 36:
						goto IL_631;
					case 37:
						if (A_0.HasValue(1))
						{
							num = 58;
							continue;
						}
						goto IL_5EE;
					case 38:
						goto IL_36B;
					case 39:
						if (A_0.HasValue(51))
						{
							num = 42;
							continue;
						}
						goto IL_147;
					case 40:
						A_1.BoldBidi = A_0.BoldBidi;
						num = 20;
						continue;
					case 41:
						if (A_0.HasValue(10))
						{
							num = 25;
							continue;
						}
						goto IL_631;
					case 42:
						A_1.Emboss = A_0.Emboss;
						num = 19;
						continue;
					case 43:
						if (A_0.HasValue(54))
						{
							num = 51;
							continue;
						}
						goto IL_36B;
					case 44:
						if (A_0.HasValue(9))
						{
							num = 0;
							continue;
						}
						goto IL_11F;
					case 45:
						return;
					case 46:
						A_1.Hidden = A_0.Hidden;
						num = 5;
						continue;
					case 47:
						A_1.DoubleStrike = A_0.DoubleStrike;
						num = 54;
						continue;
					case 48:
						A_1.HighlightColor = A_0.HighlightColor;
						num = 16;
						continue;
					case 49:
						goto IL_4FA;
					case 50:
						goto IL_4D2;
					case 51:
						A_1.AllCaps = A_0.AllCaps;
						num = 38;
						continue;
					case 52:
						goto IL_16F;
					case 54:
						goto IL_585;
					case 55:
						if (A_0.HasValue(109))
						{
							num = 3;
							continue;
						}
						goto IL_1EA;
					case 56:
						if (A_0.HasValue(18))
						{
							num = 11;
							continue;
						}
						goto IL_4AD;
					case 57:
						goto IL_4AD;
					case 58:
						A_1.TextColor = A_0.TextColor;
						num = 4;
						continue;
					case 59:
						goto IL_11F;
					}
					if (A_0.HasValue(3))
					{
						num = 33;
						continue;
					}
					goto IL_4FA;
					IL_11F:
					num = 43;
					continue;
					IL_147:
					num = 30;
					continue;
					IL_16F:
					num = 23;
					continue;
					IL_1EA:
					num = 22;
					continue;
					IL_22E:
					num = 32;
					continue;
					IL_295:
					num = 56;
					continue;
					IL_311:
					num = 29;
					continue;
					IL_339:
					num = 55;
					continue;
					IL_36B:
					num = 35;
					continue;
					IL_41A:
					num = 2;
					continue;
					IL_441:
					num = 6;
					continue;
					IL_469:
					num = 9;
					continue;
					IL_4AD:
					num = 18;
					continue;
					IL_4D2:
					num = 41;
					continue;
					IL_4FA:
					num = 37;
					continue;
					IL_585:
					num = 39;
					continue;
					IL_5AD:
					num = 17;
					continue;
					IL_5EE:
					num = 8;
					continue;
					IL_631:
					num = 44;
				}
			}

			// Token: 0x060041DB RID: 16859 RVA: 0x003E1CC8 File Offset: 0x003E0CC8
			private void ᜂ()
			{
				for (;;)
				{
					Borders borders = this.ᜀ.Format.Borders;
					int num = 4;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if ((this.ᜀ.PreviousSibling as Paragraph).Format.Borders.Bottom.LineWidth > 0f)
							{
								num = 9;
								continue;
							}
							return;
						case 1:
							num = 0;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_299;
							default:
								goto IL_20B;
							}
							break;
						case 3:
							if (this.ᜀ.PreviousSibling is Paragraph)
							{
								num = 7;
								continue;
							}
							return;
						case 4:
							if (!borders.NoBorder)
							{
								num = 10;
								continue;
							}
							goto IL_299;
						case 5:
							num = 6;
							continue;
						case 6:
							if ((this.ᜀ.PreviousSibling as Paragraph).Format.Borders.Bottom.BorderType != BorderStyle.None)
							{
								num = 1;
								continue;
							}
							return;
						case 7:
							num = 8;
							continue;
						case 8:
							if ((this.ᜀ.PreviousSibling as Paragraph).Format.Borders.Bottom.BorderType != BorderStyle.Cleared)
							{
								num = 5;
								continue;
							}
							return;
						case 9:
						{
							spr\u2326 spr_u = base.ᜭ();
							spr_u.ᜁ(spr_u.ᜁ() + (double)(this.ᜀ.PreviousSibling as Paragraph).Format.Borders.Bottom.LineWidth);
							num = 2;
							continue;
						}
						case 10:
							num = 11;
							continue;
						case 11:
							if (!this.ᜀ.SectionEndMark)
							{
								num = 12;
								continue;
							}
							goto IL_299;
						case 12:
						{
							spr\u2326 spr_u2 = base.ᜭ();
							spr_u2.ᜂ(spr_u2.ᜃ() + (double)(borders.Left.LineWidth + borders.Left.Space));
							spr\u2326 spr_u3 = base.ᜭ();
							spr_u3.ᜃ(spr_u3.ᜂ() + (double)(borders.Right.LineWidth + borders.Right.Space));
							spr\u2326 spr_u4 = base.ᜭ();
							spr_u4.ᜁ(spr_u4.ᜁ() + (double)(borders.Top.LineWidth + borders.Top.Space));
							spr\u2326 spr_u5 = base.ᜭ();
							spr_u5.ᜀ(spr_u5.ᜀ() + (double)(borders.Bottom.LineWidth + borders.Bottom.Space));
							num = 13;
							continue;
						}
						case 13:
							goto IL_299;
						}
						break;
						IL_299:
						num = 3;
					}
				}
				IL_20B:
				if (true)
				{
				}
				if (false)
				{
				}
			}

			// Token: 0x060041DC RID: 16860 RVA: 0x003E1FA0 File Offset: 0x003E0FA0
			private void ᜁ()
			{
				switch (0)
				{
				default:
				{
					bool flag3;
					bool flag4;
					ParagraphFormat format;
					for (;;)
					{
						IDocumentObject owner = this.ᜀ.Owner;
						int num = 6;
						for (;;)
						{
							ISection section;
							Body body;
							bool flag;
							bool flag2;
							ISection section2;
							IParagraph paragraph;
							switch (num)
							{
							case 0:
								if (section.BreakCode != SectionBreakType.NewPage)
								{
									num = 8;
									continue;
								}
								goto IL_2ED;
							case 1:
								if (body != null)
								{
									num = 16;
									continue;
								}
								goto IL_48D;
							case 2:
								flag = true;
								goto IL_30B;
							case 3:
								num = 10;
								continue;
							case 4:
								num = 24;
								continue;
							case 5:
								if (this.ᜀ.NextSibling is Table)
								{
									num = 11;
									continue;
								}
								goto IL_422;
							case 6:
								goto IL_E9;
							case 7:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_326;
								default:
									if (false)
									{
									}
									num = 0;
									continue;
								}
								break;
							case 8:
								num = 17;
								continue;
							case 9:
								if (flag2)
								{
									num = 7;
									continue;
								}
								num = 38;
								continue;
							case 10:
								if (owner.DocumentObjectType == DocumentObjectType.Body)
								{
									num = 31;
									continue;
								}
								owner = owner.Owner;
								num = 27;
								continue;
							case 11:
								num = 39;
								continue;
							case 12:
								num = 21;
								continue;
							case 13:
								num = 5;
								continue;
							case 14:
								goto IL_4A6;
							case 15:
								flag = (section.BreakCode == SectionBreakType.NoBreak);
								goto IL_30B;
							case 16:
							{
								flag2 = (body.Paragraphs.IndexOf(this.ᜀ) == body.Paragraphs.Count - 1);
								int num2 = this.ᜅ().Sections.IndexOf(body.Owner);
								num = 29;
								continue;
							}
							case 17:
								if (section.BreakCode != SectionBreakType.Oddpage)
								{
									num = 12;
									continue;
								}
								goto IL_2ED;
							case 18:
								goto IL_454;
							case 19:
								section2 = null;
								goto IL_209;
							case 20:
								if (paragraph != null)
								{
									num = 35;
									continue;
								}
								goto IL_48D;
							case 21:
								if (section.BreakCode != SectionBreakType.EvenPage)
								{
									num = 30;
									continue;
								}
								goto IL_2ED;
							case 22:
								if (true)
								{
								}
								num = 19;
								continue;
							case 23:
								goto IL_454;
							case 24:
								if (!this.ᜀ.IsInCell)
								{
									num = 13;
									continue;
								}
								goto IL_48D;
							case 25:
								goto IL_31A;
							case 26:
								num = 9;
								continue;
							case 27:
								goto IL_E9;
							case 28:
							{
								int num2;
								section2 = this.ᜅ().Sections[num2 + 1];
								goto IL_209;
							}
							case 29:
							{
								int num2;
								if (num2 + 1 >= this.ᜀ.Document.Sections.Count)
								{
									num = 22;
									continue;
								}
								num = 28;
								continue;
							}
							case 30:
								num = 15;
								continue;
							case 31:
								goto IL_129;
							case 32:
								goto IL_326;
							case 33:
								if (section != null)
								{
									num = 26;
									continue;
								}
								goto IL_31A;
							case 34:
								paragraph = (this.ᜀ.NextSibling as Table).Rows[0].Cells[0].Paragraphs[0];
								num = 18;
								continue;
							case 35:
								flag3 = paragraph.Format.PageBreakBefore;
								num = 36;
								continue;
							case 36:
								goto IL_48D;
							case 37:
								if (owner != null)
								{
									num = 3;
									continue;
								}
								goto IL_129;
							case 38:
								flag = false;
								goto IL_30B;
							case 39:
								if ((this.ᜀ.NextSibling as Table).Rows[0].Cells[0].Paragraphs.Count > 0)
								{
									num = 34;
									continue;
								}
								goto IL_422;
							}
							break;
							IL_E9:
							num = 37;
							continue;
							IL_129:
							body = (owner as Body);
							flag4 = false;
							flag3 = false;
							paragraph = null;
							num = 1;
							continue;
							IL_209:
							section = section2;
							num = 33;
							continue;
							IL_2ED:
							num = 2;
							continue;
							IL_30B:
							flag4 = flag;
							num = 25;
							continue;
							IL_31A:
							num = 32;
							continue;
							IL_326:
							if (!flag2)
							{
								num = 4;
								continue;
							}
							goto IL_48D;
							IL_422:
							int num3 = body.Paragraphs.IndexOf(this.ᜀ);
							paragraph = body.Paragraphs[num3 + 1];
							num = 23;
							continue;
							IL_454:
							num = 20;
							continue;
							IL_48D:
							format = this.ᜀ.Format;
							num = 14;
						}
					}
					IL_4A6:
					base.ᜌ(format.PageBreakAfter || flag3 || format.IsColumnBreakAfter || flag4);
					return;
				}
				}
			}

			// Token: 0x060041DD RID: 16861 RVA: 0x003E247C File Offset: 0x003E147C
			private void ᜀ()
			{
				int a_ = 10;
				switch (0)
				{
				default:
					for (;;)
					{
						ParagraphFormat format = this.ᜀ.Format;
						ParagraphStyle paragraphStyle = this.ᜀ.GetStyle();
						int num = 79;
						for (;;)
						{
							ParagraphFormat paragraphFormat;
							switch (num)
							{
							case 0:
								goto IL_373;
							case 1:
							{
								float? num2;
								base.ᜰ().ᜂ((double)num2.Value);
								num = 20;
								continue;
							}
							case 2:
							{
								float? num3;
								base.ᜰ().ᜃ((double)num3.Value);
								num = 109;
								continue;
							}
							case 3:
								num = 119;
								continue;
							case 4:
								num = 21;
								continue;
							case 5:
								goto IL_BEE;
							case 6:
							{
								float? num4;
								if (num4 != null)
								{
									num = 108;
									continue;
								}
								goto IL_2E4;
							}
							case 7:
								if (paragraphStyle != null)
								{
									num = 35;
									continue;
								}
								goto IL_52A;
							case 8:
							{
								float? num4;
								if (num4 == null)
								{
									num = 93;
									continue;
								}
								goto IL_50B;
							}
							case 9:
								if (this.ᜀ.IsInCell)
								{
									num = 82;
									continue;
								}
								goto IL_CF0;
							case 10:
								num = 15;
								continue;
							case 11:
								base.ᜰ().ᜁ(0.0);
								num = 39;
								continue;
							case 12:
								num = 98;
								continue;
							case 13:
								goto IL_E0F;
							case 14:
								if (paragraphFormat.HasValue(9))
								{
									num = 69;
									continue;
								}
								goto IL_50B;
							case 15:
							{
								float? num3;
								if (num3 != null)
								{
									num = 46;
									continue;
								}
								goto IL_DE6;
							}
							case 16:
								goto IL_2BB;
							case 17:
								base.ᜰ().ᜂ(0.0);
								num = 117;
								continue;
							case 18:
							{
								float? num5;
								base.ᜰ().ᜁ((double)num5.Value);
								num = 105;
								continue;
							}
							case 19:
								num = 110;
								continue;
							case 20:
								goto IL_776;
							case 21:
								if ((this.ᜀ.Owner as TableCell).OwnerRow.OwnerTable != null)
								{
									num = 55;
									continue;
								}
								goto IL_CA3;
							case 22:
								if ((this.ᜀ.Owner as TableCell).OwnerRow.OwnerTable.TableStyleName != null)
								{
									num = 99;
									continue;
								}
								goto IL_CA3;
							case 23:
								if (format.HasValue(5))
								{
									num = 38;
									continue;
								}
								goto IL_A24;
							case 24:
								if (this.ᜀ.Format.IsSpacingAfterAuto)
								{
									num = 120;
									continue;
								}
								goto IL_96F;
							case 25:
								goto IL_96F;
							case 26:
								goto IL_437;
							case 27:
								num = 48;
								continue;
							case 28:
								goto IL_6C9;
							case 29:
								goto IL_831;
							case 30:
								return;
							case 31:
								if (paragraphFormat != null)
								{
									num = 59;
									continue;
								}
								goto IL_E0F;
							case 32:
								num = 68;
								continue;
							case 33:
								num = 67;
								continue;
							case 34:
								goto IL_2E4;
							case 35:
								num = 113;
								continue;
							case 36:
								base.ᜀ(HorizontalAlignment.Left);
								num = 26;
								continue;
							case 37:
								num = 56;
								continue;
							case 38:
								base.ᜃ(format.FirstLineIndent);
								num = 0;
								continue;
							case 39:
								goto IL_3B9;
							case 40:
								goto IL_A6F;
							case 41:
							{
								float? num2;
								if (num2 == null)
								{
									num = 107;
									continue;
								}
								goto IL_325;
							}
							case 42:
								goto IL_BEE;
							case 43:
								base.ᜰ().ᜁ(0.0);
								num = 103;
								continue;
							case 44:
								goto IL_6C9;
							case 45:
								if (base.ᜠ() == HorizontalAlignment.Right)
								{
									num = 36;
									continue;
								}
								goto IL_437;
							case 46:
								num = 51;
								continue;
							case 47:
							{
								float? num3;
								if (num3 != null)
								{
									num = 2;
									continue;
								}
								goto IL_58E;
							}
							case 48:
								if (this.ᜀ.Format.IsSpacingBeforeAuto)
								{
									num = 104;
									continue;
								}
								goto IL_CF0;
							case 49:
							{
								float? num3;
								if (num3 == null)
								{
									num = 33;
									continue;
								}
								goto IL_A6F;
							}
							case 50:
								goto IL_50B;
							case 51:
							{
								float? num5;
								if (num5 != null)
								{
									num = 37;
									continue;
								}
								goto IL_DE6;
							}
							case 52:
								if (paragraphFormat.HasValue(8))
								{
									num = 87;
									continue;
								}
								goto IL_2BB;
							case 53:
							{
								float? num5;
								if (num5 == null)
								{
									num = 54;
									continue;
								}
								goto IL_2BB;
							}
							case 54:
								num = 52;
								continue;
							case 55:
								num = 22;
								continue;
							case 56:
							{
								float? num4;
								if (num4 != null)
								{
									num = 13;
									continue;
								}
								goto IL_DE6;
							}
							case 57:
								if (format.HasKey(5))
								{
									num = 118;
									continue;
								}
								goto IL_A24;
							case 58:
								num = 77;
								continue;
							case 59:
								num = 80;
								continue;
							case 60:
								if (format.IsContextualSpacing)
								{
									num = 58;
									continue;
								}
								goto IL_2E4;
							case 61:
								if (this.ᜀ.SectionEndMark)
								{
									num = 73;
									continue;
								}
								return;
							case 62:
								if ((this.ᜀ.PreviousSibling as Paragraph).StyleName == this.ᜀ.StyleName)
								{
									num = 43;
									continue;
								}
								goto IL_79F;
							case 63:
							{
								float? num3 = new float?(paragraphFormat.RightIndent);
								num = 40;
								continue;
							}
							case 64:
								goto IL_373;
							case 65:
								if (paragraphFormat.HasValue(2))
								{
									num = 66;
									continue;
								}
								goto IL_325;
							case 66:
							{
								float? num2 = new float?(paragraphFormat.LeftIndent);
								num = 70;
								continue;
							}
							case 67:
								if (paragraphFormat.HasValue(3))
								{
									num = 63;
									continue;
								}
								goto IL_A6F;
							case 68:
								if ((this.ᜀ.NextSibling as Paragraph).StyleName == this.ᜀ.StyleName)
								{
									num = 116;
									continue;
								}
								goto IL_2E4;
							case 69:
							{
								float? num4 = new float?(paragraphFormat.AfterSpacing);
								num = 50;
								continue;
							}
							case 70:
								goto IL_325;
							case 71:
								goto IL_2E4;
							case 72:
								num = 62;
								continue;
							case 73:
								base.ᜰ().ᜁ(0.0);
								base.ᜰ().ᜀ(0.0);
								num = 30;
								continue;
							case 74:
								if (format.IsBidi)
								{
									num = 106;
									continue;
								}
								goto IL_437;
							case 75:
								if ((this.ᜀ.Owner as TableCell).OwnerRow != null)
								{
									num = 4;
									continue;
								}
								goto IL_CA3;
							case 76:
							{
								float? num5;
								if (num5 != null)
								{
									num = 18;
									continue;
								}
								goto IL_7CC;
							}
							case 77:
								if (this.ᜀ.PreviousSibling != null)
								{
									num = 3;
									continue;
								}
								goto IL_79F;
							case 78:
								if ((this.ᜀ.PreviousSibling as Paragraph).Format.AfterSpacing >= this.ᜀ.Format.BeforeSpacing)
								{
									num = 11;
									continue;
								}
								base.ᜰ().ᜁ((double)(this.ᜀ.Format.BeforeSpacing - (this.ᜀ.PreviousSibling as Paragraph).Format.AfterSpacing));
								num = 94;
								continue;
							case 79:
								if (paragraphStyle == null)
								{
									num = 95;
									continue;
								}
								goto IL_831;
							case 80:
							{
								float? num2;
								if (num2 != null)
								{
									num = 10;
									continue;
								}
								goto IL_DE6;
							}
							case 81:
							{
								float? num2;
								if (num2 != null)
								{
									num = 1;
									continue;
								}
								goto IL_776;
							}
							case 82:
								num = 88;
								continue;
							case 83:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_CEB;
								default:
									if (false)
									{
									}
									base.ᜀ(HorizontalAlignment.Right);
									num = 85;
									continue;
								}
								break;
							case 84:
								if (this.ᜀ.PreviousSibling != null)
								{
									num = 19;
									continue;
								}
								goto IL_3B9;
							case 85:
								goto IL_437;
							case 86:
								base.ᜰ().ᜂ((double)format.LeftIndent);
								num = 5;
								continue;
							case 87:
							{
								float? num5 = new float?(paragraphFormat.BeforeSpacing);
								num = 16;
								continue;
							}
							case 88:
								if (!(this.ᜀ.OwnerTextBody as TableCell).OwnerRow.OwnerTable.IsTextBox)
								{
									num = 111;
									continue;
								}
								goto IL_CF0;
							case 89:
								goto IL_373;
							case 90:
								goto IL_CF0;
							case 91:
								if (this.ᜀ.ListFormat.IsEmptyList)
								{
									num = 17;
									continue;
								}
								base.ᜰ().ᜂ((double)format.LeftIndent);
								num = 42;
								continue;
							case 92:
								if (this.ᜀ.NextSibling == null)
								{
									num = 97;
									continue;
								}
								goto IL_96F;
							case 93:
								num = 14;
								continue;
							case 94:
								goto IL_3B9;
							case 95:
								paragraphStyle = (this.ᜀ.Document.Styles.FindByName(ClipboardData.b("㹯ᵱٳ᭵᥷ᙹ", a_)) as ParagraphStyle);
								num = 29;
								continue;
							case 96:
								if (this.ᜀ.NextSibling != null)
								{
									num = 12;
									continue;
								}
								goto IL_2E4;
							case 97:
								num = 24;
								continue;
							case 98:
								if (this.ᜀ.NextSibling is Paragraph)
								{
									num = 32;
									continue;
								}
								goto IL_2E4;
							case 99:
							{
								float? num2 = null;
								float? num3 = null;
								float? num5 = null;
								float? num4 = null;
								paragraphFormat = format;
								num = 44;
								continue;
							}
							case 100:
								num = 78;
								continue;
							case 101:
								if (this.ᜀ.Owner is TableCell)
								{
									num = 112;
									continue;
								}
								goto IL_CA3;
							case 102:
								base.ᜃ(format.FirstLineIndent);
								num = 89;
								continue;
							case 103:
								goto IL_79F;
							case 104:
								base.ᜰ().ᜁ(0.0);
								num = 90;
								continue;
							case 105:
								goto IL_7CC;
							case 106:
								num = 115;
								continue;
							case 107:
								num = 65;
								continue;
							case 108:
							{
								float? num4;
								base.ᜰ().ᜀ((double)num4.Value);
								num = 34;
								continue;
							}
							case 109:
								goto IL_CEB;
							case 110:
								if (this.ᜀ.PreviousSibling is Paragraph)
								{
									num = 100;
									continue;
								}
								goto IL_3B9;
							case 111:
								num = 92;
								continue;
							case 112:
								num = 75;
								continue;
							case 113:
								if (paragraphStyle.ParagraphFormat != null)
								{
									num = 102;
									continue;
								}
								goto IL_52A;
							case 114:
								if (format.HasValue(2))
								{
									num = 86;
									continue;
								}
								num = 91;
								continue;
							case 115:
								if (base.ᜠ() == HorizontalAlignment.Left)
								{
									num = 83;
									continue;
								}
								num = 45;
								continue;
							case 116:
								base.ᜰ().ᜀ(0.0);
								num = 71;
								continue;
							case 117:
								goto IL_BEE;
							case 118:
								num = 23;
								continue;
							case 119:
								if (this.ᜀ.PreviousSibling is Paragraph)
								{
									num = 72;
									continue;
								}
								goto IL_79F;
							case 120:
								if (true)
								{
								}
								base.ᜰ().ᜀ(0.0);
								num = 25;
								continue;
							case 121:
								if (this.ᜀ.PreviousSibling == null)
								{
									num = 27;
									continue;
								}
								goto IL_CF0;
							}
							break;
							IL_2BB:
							num = 8;
							continue;
							IL_2E4:
							base.ᜎ(format.Keep);
							base.ᜏ(format.KeepFollow);
							num = 57;
							continue;
							IL_325:
							num = 49;
							continue;
							IL_373:
							base.ᜂ(this.ᜀ(this.ᜀ));
							base.ᜀ((HorizontalAlignment)format.HorizontalAlignment);
							num = 74;
							continue;
							IL_3B9:
							num = 9;
							continue;
							IL_437:
							num = 61;
							continue;
							IL_50B:
							paragraphFormat = (paragraphFormat.BaseFormat as ParagraphFormat);
							num = 28;
							continue;
							IL_52A:
							base.ᜃ(format.FirstLineIndent);
							num = 64;
							continue;
							IL_58E:
							num = 76;
							continue;
							IL_CEB:
							goto IL_58E;
							IL_6C9:
							num = 31;
							continue;
							IL_776:
							num = 47;
							continue;
							IL_79F:
							num = 96;
							continue;
							IL_7CC:
							num = 6;
							continue;
							IL_831:
							num = 101;
							continue;
							IL_96F:
							num = 121;
							continue;
							IL_A24:
							num = 7;
							continue;
							IL_A6F:
							num = 53;
							continue;
							IL_BEE:
							base.ᜰ().ᜃ((double)format.RightIndent);
							base.ᜰ().ᜁ((double)format.BeforeSpacing);
							base.ᜰ().ᜀ((double)format.AfterSpacing);
							num = 84;
							continue;
							IL_CA3:
							num = 114;
							continue;
							IL_CF0:
							num = 60;
							continue;
							IL_DE6:
							num = 41;
							continue;
							IL_E0F:
							num = 81;
						}
					}
					return;
				}
			}

			// Token: 0x060041DE RID: 16862 RVA: 0x003E33A0 File Offset: 0x003E23A0
			private float ᜀ(Paragraph A_0)
			{
				float result;
				for (;;)
				{
					IL_20:
					result = 0f;
					spr\u19E0 spr_u19E = new spr\u19E0();
					for (;;)
					{
						IL_2C:
						int num = 2;
						for (;;)
						{
							if (true)
							{
							}
							switch (num)
							{
							case 0:
								return result;
							case 1:
								return result;
							case 2:
								if (A_0.Format.LineSpacingRule == LineSpacingRule.Multiple)
								{
									num = 3;
									continue;
								}
								result = A_0.Format.LineSpacing;
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_2C;
								default:
									if (false)
									{
									}
									num = 1;
									continue;
								}
								break;
							case 3:
							{
								sprᳯ sprᳯ = new sprᳯ(A_0.BreakCharacterFormat.Font, spr_u19E.ᜅ());
								result = (float)(sprᳯ.ᜁ() + sprᳯ.ᜀ()) * (A_0.Format.LineSpacing / 12f);
								num = 0;
								continue;
							}
							}
							goto IL_20;
						}
					}
				}
				return result;
			}

			// Token: 0x04003391 RID: 13201
			private Paragraph ᜀ;
		}

		// Token: 0x020004F1 RID: 1265
		internal class ᜁ : sprḈ
		{
			// Token: 0x060041DF RID: 16863 RVA: 0x003E347C File Offset: 0x003E247C
			public ᜁ(Paragraph A_0) : base(ChildrenLayoutDirection.Horizontal)
			{
				this.ᜀ = (double)A_0.Document.LastSection.PageSetup.DefaultTabWidth;
				this.ᜀ(A_0.Format);
			}

			// Token: 0x060041E0 RID: 16864 RVA: 0x003E34C4 File Offset: 0x003E24C4
			internal new void ᜀ(ParagraphFormat A_0)
			{
				for (;;)
				{
					int num = 0;
					int count = A_0.Tabs.Count;
					int num2 = 10;
					for (;;)
					{
						Tab tab;
						switch (num2)
						{
						case 0:
							if (true)
							{
							}
							if (tab.DeletePosition != 0f)
							{
								num2 = 4;
								continue;
							}
							goto IL_149;
						case 1:
							goto IL_C9;
						case 2:
							num2 = 5;
							continue;
						case 3:
							if (this.ᜀ.Contains(tab.Position))
							{
								goto IL_149;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_AC;
							default:
								if (false)
								{
								}
								num2 = 2;
								continue;
							}
							break;
						case 4:
							this.ᜀ.Add(tab.DeletePosition / 20f);
							num2 = 8;
							continue;
						case 5:
							goto IL_AC;
						case 6:
							goto IL_149;
						case 7:
							return;
						case 8:
							goto IL_149;
						case 9:
							if (num >= count)
							{
								num2 = 7;
								continue;
							}
							tab = A_0.Tabs[num];
							num2 = 3;
							continue;
						case 10:
							goto IL_C9;
						case 11:
							base.ᜀ(tab.Position, (TabJustification)tab.Justification, (TabLeader)tab.TabLeader);
							num2 = 6;
							continue;
						}
						break;
						IL_AC:
						if (tab.DeletePosition == 0f)
						{
							num2 = 11;
							continue;
						}
						num2 = 0;
						continue;
						IL_C9:
						num2 = 9;
						continue;
						IL_149:
						num++;
						num2 = 1;
					}
				}
			}

			// Token: 0x04003392 RID: 13202
			internal new List<float> ᜀ = new List<float>();
		}
	}
}
