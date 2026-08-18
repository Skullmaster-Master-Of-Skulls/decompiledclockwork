using System;
using System.Drawing;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;
using Spire.Layouting;

namespace Spire.Doc.Fields
{
	// Token: 0x02000518 RID: 1304
	public class Footnote : ParagraphBase, spr\u2297, spr\u1D30
	{
		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06004392 RID: 17298 RVA: 0x003F6374 File Offset: 0x003F5374
		// (set) Token: 0x06004393 RID: 17299 RVA: 0x003F63B8 File Offset: 0x003F53B8
		public bool UseAbsolutePos
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
				return this.ᜉ;
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
				this.ᜉ = value;
			}
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06004394 RID: 17300 RVA: 0x003F63FC File Offset: 0x003F53FC
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
				return DocumentObjectType.Footnote;
			}
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06004395 RID: 17301 RVA: 0x003F643C File Offset: 0x003F543C
		// (set) Token: 0x06004396 RID: 17302 RVA: 0x003F6480 File Offset: 0x003F5480
		public FootnoteType FootnoteType
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

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x06004397 RID: 17303 RVA: 0x003F64C4 File Offset: 0x003F54C4
		// (set) Token: 0x06004398 RID: 17304 RVA: 0x003F6508 File Offset: 0x003F5508
		public bool IsAutoNumbered
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
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						this.ᜀ(true);
						this.ᜁ(value);
						num = 3;
						continue;
					case 3:
						goto IL_4D;
					case 4:
						if (!base.Document.ᜇ)
						{
							num = 1;
							continue;
						}
						goto IL_4D;
					case 5:
						if (true)
						{
						}
						num = 4;
						continue;
					}
					goto IL_32;
					IL_3B:
					num = 5;
					continue;
					IL_32:
					if (this.ᜄ != value)
					{
						goto IL_3B;
					}
					break;
					IL_4D:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_3B;
					default:
						if (false)
						{
						}
						this.ᜄ = value;
						num = 0;
						break;
					}
				}
			}
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x06004399 RID: 17305 RVA: 0x003F65CC File Offset: 0x003F55CC
		public Body TextBody
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
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600439A RID: 17306 RVA: 0x003F6610 File Offset: 0x003F5610
		public CharacterFormat MarkerCharacterFormat
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
				return this.m_charFormat;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x0600439B RID: 17307 RVA: 0x003F6654 File Offset: 0x003F5654
		// (set) Token: 0x0600439C RID: 17308 RVA: 0x003F6698 File Offset: 0x003F5698
		public byte SymbolCode
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
				return this.ᜅ;
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_3D;
						default:
							if (false)
							{
							}
							this.ᜀ(true);
							this.ᜁ(value);
							num = 2;
							continue;
						}
						break;
					case 1:
						if (!base.Document.ᜇ)
						{
							num = 0;
							continue;
						}
						goto IL_9C;
					case 2:
						goto IL_55;
					case 3:
						if (true)
						{
						}
						break;
					case 4:
						goto IL_3D;
					}
					if (value != this.ᜅ)
					{
						num = 4;
						continue;
					}
					break;
					IL_3D:
					num = 1;
				}
				IL_55:
				IL_9C:
				this.ᜅ = value;
			}
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x0600439D RID: 17309 RVA: 0x003F6748 File Offset: 0x003F5748
		// (set) Token: 0x0600439E RID: 17310 RVA: 0x003F678C File Offset: 0x003F578C
		internal string SymbolFontName
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
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_42;
						default:
							if (false)
							{
							}
							this.ᜀ(true);
							num = 3;
							continue;
						}
						break;
					case 1:
						goto IL_42;
					case 2:
						if (!base.Document.ᜇ)
						{
							num = 0;
							continue;
						}
						goto IL_9A;
					case 3:
						goto IL_53;
					}
					if (value != this.ᜆ)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
					IL_42:
					num = 2;
				}
				IL_53:
				IL_9A:
				this.ᜆ = value;
			}
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x0600439F RID: 17311 RVA: 0x003F683C File Offset: 0x003F583C
		// (set) Token: 0x060043A0 RID: 17312 RVA: 0x003F6880 File Offset: 0x003F5880
		public string CustomMarker
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
				return this.ᜇ;
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_79;
					case 2:
						if (!this.ᜄ)
						{
							num = 4;
							continue;
						}
						goto IL_CC;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_79;
						default:
							if (false)
							{
							}
							num = 2;
							continue;
						}
						break;
					case 4:
						if (true)
						{
						}
						num = 1;
						continue;
					case 5:
						goto IL_67;
					case 6:
						this.ᜀ(true);
						this.ᜁ(value);
						num = 5;
						continue;
					}
					if (value != this.ᜇ)
					{
						num = 3;
						continue;
					}
					break;
					IL_79:
					if (base.Document.ᜇ)
					{
						break;
					}
					num = 6;
				}
				IL_67:
				IL_CC:
				this.ᜇ = value;
			}
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060043A1 RID: 17313 RVA: 0x003F6960 File Offset: 0x003F5960
		internal bool CustomMarkerIsSymbol
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
				return this.ᜅ > 0;
			}
		}

		// Token: 0x060043A2 RID: 17314 RVA: 0x003F69A4 File Offset: 0x003F59A4
		public Footnote(IDocument doc)
		{
			int a_ = 14;
			this.ᜄ = true;
			this.ᜆ = ClipboardData.b("❳ཱུᕷ᡹፻ች", a_);
			this.ᜇ = string.Empty;
			base..ctor((Document)doc);
			this.ᜃ = new Body(base.Document, this);
			this.m_charFormat = new CharacterFormat(base.Document);
			this.m_charFormat.ᜀ(this);
		}

		// Token: 0x060043A3 RID: 17315 RVA: 0x003F6A1C File Offset: 0x003F5A1C
		internal Footnote(IDocument A_0, string A_1) : this(A_0)
		{
			this.ᜇ = A_1;
			this.ᜄ = false;
		}

		// Token: 0x060043A4 RID: 17316 RVA: 0x003F6A40 File Offset: 0x003F5A40
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

		// Token: 0x060043A5 RID: 17317 RVA: 0x003F6A88 File Offset: 0x003F5A88
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
			Footnote footnote = (Footnote)base.CloneImpl();
			footnote.ᜃ = (Body)this.ᜃ.Clone();
			footnote.ᜃ.ᜀ(footnote);
			return footnote;
		}

		// Token: 0x060043A6 RID: 17318 RVA: 0x003F6AF4 File Offset: 0x003F5AF4
		internal override void OnStateChange(object sender)
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
						goto IL_2E;
					default:
						goto IL_53;
					}
					break;
				case 2:
					goto IL_2E;
				}
				if (sender is CharacterFormat)
				{
					num = 2;
					continue;
				}
				goto IL_5B;
				IL_2E:
				this.ᜀ(true);
				num = 0;
			}
			IL_53:
			if (false)
			{
			}
			IL_5B:
			if (true)
			{
			}
		}

		// Token: 0x060043A7 RID: 17319 RVA: 0x003F6B70 File Offset: 0x003F5B70
		internal override void Close()
		{
			for (;;)
			{
				IL_14:
				if (true)
				{
				}
				base.Close();
				for (;;)
				{
					IL_22:
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
								goto IL_22;
							default:
								if (false)
								{
								}
								this.ᜃ.ᜅ();
								this.ᜃ = null;
								num = 1;
								continue;
							}
							break;
						case 1:
							return;
						case 2:
							if (this.ᜃ != null)
							{
								num = 0;
								continue;
							}
							return;
						}
						goto IL_14;
					}
				}
			}
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x003F6BFC File Offset: 0x003F5BFC
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
			base.XDLSHolder.AddElement(ClipboardData.b("४ɬ୮ࡰ", a_), this.ᜃ);
			base.XDLSHolder.AddElement(ClipboardData.b("٪౬ᵮᩰᙲݴ婶᩸፺ᱼൾﮈꚊﺒ", a_), this.m_charFormat);
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x003F6C80 File Offset: 0x003F5C80
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 13;
			for (;;)
			{
				base.WriteXmlAttributes(writer);
				writer.WriteValue(ClipboardData.b("ݲ౴ݶᱸ", a_), FootnoteType.Footnote);
				writer.WriteValue(ClipboardData.b("㉲tͶᙸ㕺ࡼቾ", a_), this.ᜄ);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_D8;
					case 1:
						writer.WriteValue(ClipboardData.b("ひtѶ൸ᑺၼ㉾ﮈ", a_), this.ᜇ);
						num = 3;
						continue;
					case 2:
						if (this.ᜂ != FootnoteType.Endnote)
						{
							goto IL_164;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_D8;
						default:
							if (false)
							{
							}
							num = 5;
							continue;
						}
						break;
					case 3:
						goto IL_AD;
					case 4:
						if (this.CustomMarkerIsSymbol)
						{
							num = 0;
							continue;
						}
						return;
					case 5:
						writer.WriteValue(ClipboardData.b("㩲ٴ㉶᝸ὺ፼ၾ쒄ﶈ力", a_), true);
						num = 6;
						continue;
					case 6:
						goto IL_164;
					case 7:
						if (this.ᜇ != string.Empty)
						{
							num = 1;
							continue;
						}
						goto IL_AD;
					case 8:
						return;
					}
					break;
					IL_AD:
					num = 4;
					continue;
					IL_D8:
					writer.WriteValue(ClipboardData.b("⁲౴᩶᭸ᑺᅼ㱾", a_), (int)this.ᜅ);
					writer.WriteValue(ClipboardData.b("⁲౴᩶᭸ᑺᅼ㥾즆", a_), this.ᜆ);
					num = 8;
					continue;
					IL_164:
					if (true)
					{
					}
					num = 7;
				}
			}
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x003F6E2C File Offset: 0x003F5E2C
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 2;
			for (;;)
			{
				if (true)
				{
				}
				base.ReadXmlAttributes(reader);
				int num = 14;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_22E;
					case 1:
						this.ᜆ = reader.ReadString(ClipboardData.b("㭧፩ū౭Ὧṱ㉳᥵ᙷ๹㉻ώ", a_));
						num = 13;
						continue;
					case 2:
						this.ᜄ = reader.ReadBoolean(ClipboardData.b("⥧Ὡᡫŭ㹯ݱᥳᑵᵷࡹ᥻᩽", a_));
						num = 4;
						continue;
					case 3:
						goto IL_BF;
					case 4:
						goto IL_1E5;
					case 5:
						if (reader.HasAttribute(ClipboardData.b("㭧፩ū౭Ὧṱ㉳᥵ᙷ๹㉻ώ", a_)))
						{
							num = 1;
							continue;
						}
						return;
					case 6:
						this.ᜅ = reader.ReadByte(ClipboardData.b("㭧፩ū౭Ὧṱ㝳᥵ᱷό", a_));
						num = 0;
						continue;
					case 7:
						if (reader.HasAttribute(ClipboardData.b("Ⅷᥩ⥫mᑯᱱ᭳ɵᵷ㭹ࡻ੽", a_)))
						{
							num = 15;
							continue;
						}
						goto IL_19B;
					case 8:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							if (reader.HasAttribute(ClipboardData.b("㭧፩ū౭Ὧṱ㝳᥵ᱷό", a_)))
							{
								num = 6;
								continue;
							}
							goto IL_22E;
						}
						break;
					case 9:
						this.ᜂ = (reader.ReadBoolean(ClipboardData.b("Ⅷᥩ⥫mᑯᱱ᭳ɵᵷ㭹ࡻ੽", a_)) ? FootnoteType.Endnote : FootnoteType.Footnote);
						num = 12;
						continue;
					case 10:
						if (reader.HasAttribute(ClipboardData.b("⭧ὩὫᩭὯά㥳᝵੷ᅹ᥻౽", a_)))
						{
							num = 11;
							continue;
						}
						goto IL_BF;
					case 11:
						this.ᜇ = reader.ReadString(ClipboardData.b("⭧ὩὫᩭὯά㥳᝵੷ᅹ᥻౽", a_));
						num = 3;
						continue;
					case 12:
						goto IL_19B;
					case 13:
						return;
					case 14:
						if (reader.HasAttribute(ClipboardData.b("⥧Ὡᡫŭ㹯ݱᥳᑵᵷࡹ᥻᩽", a_)))
						{
							num = 2;
							continue;
						}
						goto IL_1E5;
					case 15:
						num = 9;
						continue;
					}
					break;
					IL_BF:
					num = 7;
					continue;
					IL_19B:
					num = 8;
					continue;
					IL_1E5:
					num = 10;
					continue;
					IL_22E:
					num = 5;
				}
			}
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x003F7098 File Offset: 0x003F6098
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			for (;;)
			{
				base.CloneRelationsTo(doc, nextOwner);
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.ᜃ != null)
						{
							num = 2;
							continue;
						}
						return;
					case 1:
						return;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return;
						}
						if (false)
						{
						}
						if (true)
						{
						}
						this.ᜃ.CloneRelationsTo(doc, nextOwner);
						num = 1;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x003F7120 File Offset: 0x003F6120
		private new void ᜁ(bool A_0)
		{
			switch (0)
			{
			default:
			{
				int num = 2;
				string a_;
				string a_2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (string.IsNullOrEmpty(this.ᜇ))
						{
							goto IL_E1;
						}
						goto IL_10E;
					case 1:
						if (true)
						{
						}
						a_ = '\u0002'.ToString();
						a_2 = this.ᜇ;
						num = 9;
						continue;
					case 3:
						num = 6;
						continue;
					case 4:
						goto IL_BC;
					case 5:
						if (!A_0)
						{
							num = 1;
							continue;
						}
						a_ = this.ᜇ;
						a_2 = '\u0002'.ToString();
						num = 4;
						continue;
					case 6:
						if (!this.ᜄ)
						{
							num = 8;
							continue;
						}
						goto IL_10E;
					case 7:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E1;
						default:
							goto IL_103;
						}
						break;
					case 8:
						num = 0;
						continue;
					case 9:
						goto IL_7A;
					}
					if (!base.Document.ᜇ)
					{
						num = 3;
						continue;
					}
					return;
					IL_E1:
					num = 7;
					continue;
					IL_10E:
					a_2 = string.Empty;
					a_ = string.Empty;
					num = 5;
				}
				IL_7A:
				IL_BC:
				goto IL_145;
				IL_103:
				if (false)
				{
				}
				return;
				IL_145:
				this.ᜀ(a_, a_2);
				return;
			}
			}
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x003F727C File Offset: 0x003F627C
		private new void ᜁ(string A_0)
		{
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
						goto IL_85;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				case 2:
					if (string.IsNullOrEmpty(this.ᜇ))
					{
						if (true)
						{
						}
						num = 3;
						continue;
					}
					goto IL_85;
				case 3:
					goto IL_83;
				}
				if (base.Document.ᜇ)
				{
					break;
				}
				num = 1;
			}
			return;
			IL_83:
			return;
			IL_85:
			this.ᜀ(this.ᜇ, A_0);
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x003F731C File Offset: 0x003F631C
		private new void ᜁ(byte A_0)
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					num = 8;
					continue;
				case 2:
					num = 9;
					continue;
				case 3:
					if (A_0 > 0)
					{
						num = 5;
						continue;
					}
					return;
				case 5:
					num = 7;
					continue;
				case 6:
					return;
				case 7:
					if (string.IsNullOrEmpty(this.ᜇ))
					{
						num = 6;
						continue;
					}
					goto IL_10C;
				case 8:
					if (!this.ᜄ)
					{
						num = 0;
						continue;
					}
					return;
				case 9:
					if (this.ᜃ.Items.Count != 0)
					{
						goto IL_D4;
					}
					return;
				}
				if (base.Document.ᜇ)
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
					num = 2;
					continue;
				}
				IL_D4:
				num = 1;
			}
			return;
			IL_10C:
			Paragraph paragraph = this.ᜃ.Items[0] as Paragraph;
			TextSelection a_ = paragraph.Find(this.ᜇ, true, true);
			TextRange a_2 = this.ᜀ(A_0);
			this.ᜀ(a_2, paragraph, a_);
			this.m_charFormat.FontName = this.ᜆ;
			this.ᜀ(false);
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x003F7484 File Offset: 0x003F6484
		internal new void ᜀ(string A_0, string A_1)
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					if (string.IsNullOrEmpty(A_0))
					{
						Paragraph paragraph;
						this.ᜀ(A_1, paragraph);
						num = 5;
						continue;
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					default:
						if (false)
						{
						}
						if (true)
						{
						}
						num = 4;
						continue;
					}
					break;
				case 2:
					goto IL_68;
				case 3:
					return;
				case 4:
				{
					Paragraph paragraph;
					TextSelection a_ = paragraph.Find(A_0, true, true);
					this.ᜀ(a_, A_1);
					num = 2;
					continue;
				}
				case 5:
					goto IL_D6;
				}
				if (this.ᜃ.Items.Count == 0)
				{
					num = 3;
				}
				else
				{
					Paragraph paragraph = this.ᜃ.Items[0] as Paragraph;
					num = 1;
				}
			}
			return;
			IL_68:
			IL_D6:
			this.ᜀ(false);
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x003F7570 File Offset: 0x003F6570
		internal new void ᜁ()
		{
			switch (0)
			{
			default:
			{
				int num = 13;
				for (;;)
				{
					string text;
					string text2;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_251;
						default:
							goto IL_9B;
						}
						break;
					case 1:
						goto IL_125;
					case 2:
						num = 15;
						continue;
					case 3:
						goto IL_CA;
					case 4:
						goto IL_1EC;
					case 5:
						if (this.ᜅ > 0)
						{
							num = 3;
							continue;
						}
						goto IL_18E;
					case 6:
						goto IL_EE;
					case 7:
						if (!this.ᜄ)
						{
							num = 10;
							continue;
						}
						goto IL_18E;
					case 8:
						goto IL_15C;
					case 9:
					{
						Paragraph paragraph;
						this.ᜀ(text, paragraph);
						num = 6;
						continue;
					}
					case 10:
						num = 5;
						continue;
					case 11:
					{
						if (this.ᜃ.Items.Count == 0)
						{
							num = 18;
							continue;
						}
						Paragraph paragraph = this.ᜃ.Items[0] as Paragraph;
						TextSelection textSelection = paragraph.Find(text, true, true);
						num = 16;
						continue;
					}
					case 12:
					{
						if (!this.ᜄ)
						{
							num = 2;
							continue;
						}
						char c = '\u0002';
						num = 17;
						continue;
					}
					case 14:
						text = text.TrimStart(new char[]
						{
							' '
						});
						goto IL_251;
					case 15:
						text2 = this.ᜇ;
						goto IL_1F1;
					case 16:
					{
						TextSelection textSelection;
						if (textSelection == null)
						{
							num = 9;
							continue;
						}
						this.ᜀ(textSelection, text);
						num = 4;
						continue;
					}
					case 17:
					{
						char c;
						text2 = c.ToString();
						goto IL_1F1;
					}
					case 18:
					{
						Paragraph paragraph2 = new Paragraph(this.m_doc);
						this.ᜀ(text, paragraph2);
						this.ᜃ.Items.Insert(0, paragraph2);
						num = 1;
						continue;
					}
					case 19:
						if (text.TrimStart(new char[]
						{
							' '
						}) != string.Empty)
						{
							num = 14;
							continue;
						}
						goto IL_15C;
					}
					if (this.ᜈ <= 0)
					{
						num = 0;
						continue;
					}
					num = 7;
					continue;
					IL_15C:
					num = 11;
					continue;
					IL_18E:
					num = 12;
					continue;
					IL_1F1:
					text = text2;
					num = 19;
					continue;
					IL_251:
					num = 8;
				}
				IL_9B:
				if (false)
				{
				}
				if (true)
				{
				}
				return;
				IL_CA:
				this.ᜀ();
				this.ᜀ(false);
				return;
				IL_EE:
				IL_125:
				IL_1EC:
				this.ᜀ(false);
				return;
			}
			}
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x003F7834 File Offset: 0x003F6834
		private new void ᜀ()
		{
			int a_ = 14;
			switch (0)
			{
			default:
			{
				TextRange textRange;
				Paragraph paragraph;
				TextSelection a_2;
				for (;;)
				{
					textRange = this.ᜀ(this.ᜅ);
					this.m_charFormat.FontName = this.ᜆ;
					if (true)
					{
					}
					int num = 7;
					for (;;)
					{
						string text;
						string text2;
						switch (num)
						{
						case 0:
							text = this.ᜇ;
							goto IL_10E;
						case 1:
						{
							char c;
							text = c.ToString();
							goto IL_10E;
						}
						case 2:
							if (text2 != string.Empty)
							{
								num = 3;
								continue;
							}
							goto IL_1AC;
						case 3:
							a_2 = paragraph.Find(text2, true, true);
							num = 8;
							continue;
						case 4:
						{
							if (!this.ᜄ)
							{
								num = 5;
								continue;
							}
							char c = '\u0002';
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_187;
							default:
								if (false)
								{
								}
								num = 1;
								continue;
							}
							break;
						}
						case 5:
							num = 0;
							continue;
						case 6:
							goto IL_91;
						case 7:
							if (this.ᜃ.Items.Count == 0)
							{
								num = 6;
								continue;
							}
							goto IL_187;
						case 8:
							goto IL_AA;
						}
						break;
						IL_10E:
						text2 = text;
						paragraph = (this.ᜃ.Items[0] as Paragraph);
						a_2 = null;
						num = 2;
						continue;
						IL_187:
						num = 4;
					}
				}
				IL_91:
				Paragraph paragraph2 = new Paragraph(this.m_doc);
				paragraph2.Items.Add(textRange);
				paragraph2.AppendText(ClipboardData.b("味", a_));
				this.ᜃ.Items.Insert(0, paragraph2);
				return;
				IL_AA:
				IL_1AC:
				this.ᜀ(textRange, paragraph, a_2);
				return;
			}
			}
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x003F79F8 File Offset: 0x003F69F8
		private new TextRange ᜀ(byte A_0)
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
			TextRange textRange = new TextRange(base.Document);
			TextRange textRange2 = textRange;
			char c = (char)A_0;
			textRange2.Text = c.ToString();
			textRange.CharacterFormat.ImportContainer(this.m_charFormat);
			textRange.CharacterFormat.FontName = this.ᜆ;
			return textRange;
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x003F7A74 File Offset: 0x003F6A74
		private new void ᜀ(TextRange A_0, Paragraph A_1, TextSelection A_2)
		{
			int a_ = 16;
			if (A_2 == null)
			{
				for (;;)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_24;
					}
				}
				IL_24:
				if (true)
				{
				}
				if (false)
				{
				}
				A_1.Items.Insert(0, A_0);
				TextRange textRange = new TextRange(A_1.Document);
				textRange.Text = ClipboardData.b("噵", a_);
				A_1.Items.Insert(1, textRange);
				return;
			}
			TextRange asOneRange = A_2.GetAsOneRange();
			int index = asOneRange.ឯ();
			A_1.Items.Remove(asOneRange);
			A_1.Items.Insert(index, A_0);
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x003F7B20 File Offset: 0x003F6B20
		internal void ᜂ()
		{
			int num = 8;
			for (;;)
			{
				Style style;
				string text;
				sprᯉ sprᯉ;
				switch (num)
				{
				case 0:
					if (style.StyleId == 38)
					{
						num = 15;
						continue;
					}
					goto IL_1F9;
				case 1:
					return;
				case 2:
					goto IL_EF;
				case 3:
					if (style.StyleId != 39)
					{
						num = 12;
						continue;
					}
					return;
				case 4:
					if (style != null)
					{
						num = 11;
						continue;
					}
					goto IL_1F9;
				case 5:
					if (!string.IsNullOrEmpty(text))
					{
						num = 10;
						continue;
					}
					goto IL_1BA;
				case 6:
					this.m_charFormat.CharStyleName = sprᯉ.Name;
					base.Document.Styles.Add(sprᯉ);
					num = 7;
					continue;
				case 7:
					goto IL_190;
				case 9:
					if (this.ᜂ == FootnoteType.Endnote)
					{
						num = 17;
						continue;
					}
					goto IL_EF;
				case 10:
					style = base.Document.Styles.FindByName(text);
					num = 19;
					continue;
				case 11:
					num = 3;
					continue;
				case 12:
					num = 0;
					continue;
				case 13:
					sprᯉ = (sprᯉ)Style.CreateBuiltinStyle(BuiltinStyle.FootnoteReference, StyleType.CharacterStyle, base.Document);
					num = 2;
					continue;
				case 14:
					if (sprᯉ != null)
					{
						num = 6;
						continue;
					}
					return;
				case 15:
					goto IL_1B5;
				case 16:
					if (this.ᜂ == FootnoteType.Footnote)
					{
						num = 13;
						continue;
					}
					num = 9;
					continue;
				case 17:
					sprᯉ = (sprᯉ)Style.CreateBuiltinStyle(BuiltinStyle.EndnoteReference, StyleType.CharacterStyle, base.Document);
					num = 18;
					continue;
				case 18:
					goto IL_EF;
				case 19:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_1F9;
					default:
						if (true)
						{
						}
						if (false)
						{
						}
						goto IL_1BA;
					}
					break;
				}
				if (base.Document.ᜇ)
				{
					num = 1;
					continue;
				}
				text = this.m_charFormat.CharStyleName;
				style = null;
				num = 5;
				continue;
				IL_EF:
				num = 14;
				continue;
				IL_1BA:
				num = 4;
				continue;
				IL_1F9:
				sprᯉ = null;
				num = 16;
			}
			return;
			IL_190:
			return;
			IL_1B5:;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x003F7D78 File Offset: 0x003F6D78
		private new void ᜀ(bool A_0)
		{
			int num = 1;
			for (;;)
			{
				IL_0A:
				switch (num)
				{
				case 0:
					goto IL_A9;
				case 2:
					goto IL_75;
				case 3:
					num = 4;
					continue;
				case 4:
					while (!A_0)
					{
						this.ᜈ--;
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
							num = 2;
							goto IL_0A;
						}
					}
					num = 0;
					continue;
				}
				if (base.Document.ᜇ)
				{
					break;
				}
				num = 3;
			}
			IL_75:
			return;
			IL_A9:
			this.ᜈ++;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x003F7E30 File Offset: 0x003F6E30
		private new void ᜀ(TextSelection A_0, string A_1)
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
						goto IL_3F;
					default:
						goto IL_67;
					}
					break;
				case 1:
				{
					if (true)
					{
					}
					TextRange asOneRange = A_0.GetAsOneRange();
					asOneRange.Text = A_1;
					goto IL_3F;
				}
				}
				if (A_0 != null)
				{
					num = 1;
					continue;
				}
				return;
				IL_3F:
				num = 0;
			}
			IL_67:
			if (false)
			{
			}
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x003F7EAC File Offset: 0x003F6EAC
		private new void ᜀ(string A_0, Paragraph A_1)
		{
			int a_ = 15;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			TextRange entity = this.ᜀ(A_0);
			A_1.Items.Insert(0, entity);
			TextRange textRange = new TextRange(this.ᜃ.Document);
			textRange.Text = ClipboardData.b("啴", a_);
			A_1.Items.Insert(1, textRange);
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x003F7F38 File Offset: 0x003F6F38
		private new TextRange ᜀ(string A_0)
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
			TextRange textRange = new TextRange(this.m_doc);
			textRange.Text = A_0;
			textRange.CharacterFormat.ImportContainer(this.m_charFormat);
			return textRange;
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x003F7F98 File Offset: 0x003F6F98
		internal override void DrawImpl(spr\u19E0 dc, sprᦰ ltWidget)
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
			base.DrawImpl(dc, ltWidget);
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x003F7FDC File Offset: 0x003F6FDC
		SizeF spr\u2297.Measure(spr\u19E0 dc)
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
			return SizeF.Empty;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060043BB RID: 17339 RVA: 0x003F801C File Offset: 0x003F701C
		// (set) Token: 0x060043BC RID: 17340 RVA: 0x003F805C File Offset: 0x003F705C
		bool spr\u1D30.IsClipped
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
				throw new NotImplementedException();
			}
			set
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060043BD RID: 17341 RVA: 0x003F809C File Offset: 0x003F709C
		// (set) Token: 0x060043BE RID: 17342 RVA: 0x003F80DC File Offset: 0x003F70DC
		bool spr\u1D30.IsVerticalText
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060043BF RID: 17343 RVA: 0x003F811C File Offset: 0x003F711C
		// (set) Token: 0x060043C0 RID: 17344 RVA: 0x003F8158 File Offset: 0x003F7158
		bool spr\u1D30.IsSkip
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060043C1 RID: 17345 RVA: 0x003F8198 File Offset: 0x003F7198
		// (set) Token: 0x060043C2 RID: 17346 RVA: 0x003F81D8 File Offset: 0x003F71D8
		bool spr\u1D30.IsSkipBottomAlign
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060043C3 RID: 17347 RVA: 0x003F8218 File Offset: 0x003F7218
		bool spr\u1D30.IsLineContainer
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060043C4 RID: 17348 RVA: 0x003F8258 File Offset: 0x003F7258
		ChildrenLayoutDirection spr\u1D30.ChildrenLayoutDirection
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060043C5 RID: 17349 RVA: 0x003F8298 File Offset: 0x003F7298
		// (set) Token: 0x060043C6 RID: 17350 RVA: 0x003F82D8 File Offset: 0x003F72D8
		bool spr\u1D30.IsLineBreak
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060043C7 RID: 17351 RVA: 0x003F8318 File Offset: 0x003F7318
		// (set) Token: 0x060043C8 RID: 17352 RVA: 0x003F8358 File Offset: 0x003F7358
		bool spr\u1D30.TextWrap
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
				throw new NotImplementedException();
			}
			set
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060043C9 RID: 17353 RVA: 0x003F8398 File Offset: 0x003F7398
		// (set) Token: 0x060043CA RID: 17354 RVA: 0x003F83D8 File Offset: 0x003F73D8
		bool spr\u1D30.IsPageBreakItem
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
				throw new NotImplementedException();
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060043CB RID: 17355 RVA: 0x003F8418 File Offset: 0x003F7418
		spr\u2326 sprḰ.Paddings
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060043CC RID: 17356 RVA: 0x003F8458 File Offset: 0x003F7458
		spr\u2326 sprḰ.Margins
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
				throw new NotImplementedException();
			}
		}

		// Token: 0x04003599 RID: 13721
		internal new const int ᜀ = 38;

		// Token: 0x0400359A RID: 13722
		internal new const int ᜁ = 39;

		// Token: 0x0400359B RID: 13723
		private string \u2593\u00A9\u0096\u0090;

		// Token: 0x0400359C RID: 13724
		private FootnoteType ᜂ;

		// Token: 0x0400359D RID: 13725
		private Body ᜃ;

		// Token: 0x0400359E RID: 13726
		private new bool ᜄ;

		// Token: 0x0400359F RID: 13727
		private byte ᜅ;

		// Token: 0x040035A0 RID: 13728
		private string ᜆ;

		// Token: 0x040035A1 RID: 13729
		private string ᜇ;

		// Token: 0x040035A2 RID: 13730
		private int ᜈ;

		// Token: 0x040035A3 RID: 13731
		private bool ᜉ;
	}
}
