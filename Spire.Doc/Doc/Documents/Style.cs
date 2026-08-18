using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Xml;
using Spire.CompoundFile.Doc;
using Spire.Doc.Core.Biff_Records;
using Spire.Doc.Documents.XML;
using Spire.Doc.Formatting;
using Spire.Doc.Interface;

namespace Spire.Doc.Documents
{
	// Token: 0x0200025A RID: 602
	public abstract class Style : DocumentSerializable, IStyle
	{
		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06001E1B RID: 7707 RVA: 0x001DB7F0 File Offset: 0x001DA7F0
		// (set) Token: 0x06001E1C RID: 7708 RVA: 0x001DB834 File Offset: 0x001DA834
		internal byte[] TableStyleData
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
				return this.m_tapx;
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
				this.m_tapx = value;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06001E1D RID: 7709 RVA: 0x001DB878 File Offset: 0x001DA878
		// (set) Token: 0x06001E1E RID: 7710 RVA: 0x001DB8BC File Offset: 0x001DA8BC
		internal WordStyleType TypeCode
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
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (value != WordStyleType.ParagraphStyle)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						goto IL_8A;
					case 1:
						this.\u170D();
						num = 2;
						continue;
					case 2:
						goto IL_6A;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							break;
						}
						break;
					case 4:
						num = 0;
						continue;
					}
					if (this.StyleType != StyleType.ParagraphStyle)
					{
						break;
					}
					num = 4;
				}
				IL_6A:
				IL_8A:
				this.ᜂ = value;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06001E1F RID: 7711 RVA: 0x001DB95C File Offset: 0x001DA95C
		public CharacterFormat CharacterFormat
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
				return this.m_chFormat;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x001DB9A0 File Offset: 0x001DA9A0
		// (set) Token: 0x06001E21 RID: 7713 RVA: 0x001DB9E4 File Offset: 0x001DA9E4
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
				return this.ᜁ;
			}
			set
			{
				int a_ = 15;
				int num = 17;
				for (;;)
				{
					string key;
					switch (num)
					{
					case 0:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2AB;
						default:
							if (false)
							{
							}
							base.Document.Styles.InnerList.Remove(this.m_baseStyle);
							this.\u170D();
							base.Document.ᜄ = true;
							num = 13;
							continue;
						}
						break;
					case 1:
						num = 20;
						continue;
					case 2:
						this.StyleId = this.BuiltinStyleIds[key];
						num = 14;
						continue;
					case 3:
						if (!base.Document.ᜄ)
						{
							num = 0;
							continue;
						}
						goto IL_1D3;
					case 4:
						goto IL_1CE;
					case 5:
						num = 9;
						continue;
					case 6:
						goto IL_2AB;
					case 7:
						num = 12;
						continue;
					case 8:
						num = 3;
						continue;
					case 9:
						if (value.Length == 0)
						{
							num = 7;
							continue;
						}
						goto IL_F4;
					case 10:
						if (this.BuiltinStyleIds.ContainsKey(key))
						{
							num = 2;
							continue;
						}
						this.StyleId = 4094;
						num = 4;
						continue;
					case 11:
						if (!base.Document.ᜈ)
						{
							num = 15;
							continue;
						}
						goto IL_11A;
					case 12:
						if (!base.Document.ᜇ)
						{
							num = 18;
							continue;
						}
						goto IL_F4;
					case 13:
						goto IL_1D3;
					case 14:
						goto IL_239;
					case 15:
						num = 21;
						continue;
					case 16:
						num = 6;
						continue;
					case 18:
						goto IL_190;
					case 19:
						num = 11;
						continue;
					case 20:
						if (base.Document.Styles.FindByName(value, this.StyleType) != null)
						{
							num = 26;
							continue;
						}
						goto IL_11A;
					case 21:
						if (!base.Document.ᜉ)
						{
							num = 25;
							continue;
						}
						goto IL_11A;
					case 22:
						if (base.Document != null)
						{
							num = 1;
							continue;
						}
						goto IL_11A;
					case 23:
						if (this.StyleType == StyleType.ParagraphStyle)
						{
							num = 16;
							continue;
						}
						goto IL_1D3;
					case 24:
						if (!base.Document.ᜇ)
						{
							num = 19;
							continue;
						}
						goto IL_11A;
					case 25:
						num = 22;
						continue;
					case 26:
						goto IL_C7;
					}
					if (value != null)
					{
						num = 5;
						continue;
					}
					goto IL_208;
					IL_F4:
					num = 23;
					continue;
					IL_11A:
					if (true)
					{
					}
					key = value.Replace(ClipboardData.b("啴", a_), string.Empty).ToLower();
					num = 10;
					continue;
					IL_1D3:
					num = 24;
					continue;
					IL_2AB:
					if (!(value == ClipboardData.b("㭴ᡶ୸ᙺᱼ፾", a_)))
					{
						goto IL_1D3;
					}
					num = 8;
				}
				IL_C7:
				throw new ArgumentException(ClipboardData.b("㭴ᙶᑸṺ嵼ၾꎂ꾎ﮖﲜ쒠趢", a_));
				IL_190:
				goto IL_208;
				IL_1CE:
				goto IL_355;
				IL_208:
				throw new ArgumentNullException(ClipboardData.b("㭴ᙶᑸṺ", a_));
				IL_239:
				IL_355:
				this.ᜁ = value;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06001E22 RID: 7714 RVA: 0x001DBD50 File Offset: 0x001DAD50
		internal IStyle BaseStyle
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
				return this.m_baseStyle;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06001E23 RID: 7715 RVA: 0x001DBD94 File Offset: 0x001DAD94
		// (set) Token: 0x06001E24 RID: 7716 RVA: 0x001DBDD8 File Offset: 0x001DADD8
		internal int StyleId
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

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06001E25 RID: 7717
		public abstract StyleType StyleType { get; }

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06001E26 RID: 7718 RVA: 0x001DBE1C File Offset: 0x001DAE1C
		public BuiltinStyle DefaultStyleType
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
				return Style.NameToBuiltIn(this.Name);
			}
		}

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06001E27 RID: 7719 RVA: 0x001DBE64 File Offset: 0x001DAE64
		// (set) Token: 0x06001E28 RID: 7720 RVA: 0x001DBF9C File Offset: 0x001DAF9C
		internal string NextStyle
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (base.Document.Styles.FindByName(this.m_nextStyle) != null)
						{
							num = 2;
							continue;
						}
						goto IL_99;
					case 2:
						goto IL_CE;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_E2;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (base.Document.StyleNameIds.ContainsKey(this.m_nextStyle))
							{
								num = 5;
								continue;
							}
							goto IL_D0;
						}
						break;
					case 4:
						goto IL_E2;
					case 5:
						goto IL_97;
					case 6:
						num = 0;
						continue;
					case 7:
						num = 4;
						continue;
					}
					if (this.m_nextStyle != null)
					{
						num = 7;
						continue;
					}
					goto IL_127;
					IL_E2:
					if (base.Document.DetectedFormatType == FileFormat.Doc)
					{
						num = 6;
					}
					else
					{
						num = 3;
					}
				}
				IL_97:
				return base.Document.StyleNameIds[this.m_nextStyle];
				IL_99:
				return this.Name;
				IL_CE:
				return this.m_nextStyle;
				IL_D0:
				return this.Name;
				IL_127:
				return null;
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
				this.m_nextStyle = value;
			}
		}

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06001E29 RID: 7721 RVA: 0x001DBFE0 File Offset: 0x001DAFE0
		// (set) Token: 0x06001E2A RID: 7722 RVA: 0x001DC024 File Offset: 0x001DB024
		internal string LinkStyle
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
				return this.m_linkStyle;
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
				this.m_linkStyle = value;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06001E2B RID: 7723 RVA: 0x001DC068 File Offset: 0x001DB068
		// (set) Token: 0x06001E2C RID: 7724 RVA: 0x001DC0AC File Offset: 0x001DB0AC
		internal bool IsPrimaryStyle
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
				return this.m_isPrimaryStyle;
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
				this.m_isPrimaryStyle = value;
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x001DC0F0 File Offset: 0x001DB0F0
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x001DC134 File Offset: 0x001DB134
		internal bool IsSemiHidden
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
				return this.m_isSemiHidden;
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
				this.m_isSemiHidden = value;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x001DC178 File Offset: 0x001DB178
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x001DC1BC File Offset: 0x001DB1BC
		internal bool UnhideWhenUsed
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
				return this.m_unhideWhenUsed;
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
				this.m_unhideWhenUsed = value;
			}
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06001E31 RID: 7729 RVA: 0x001DC200 File Offset: 0x001DB200
		// (set) Token: 0x06001E32 RID: 7730 RVA: 0x001DC244 File Offset: 0x001DB244
		internal bool IsCustom
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
				return this.m_isCustom;
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
				this.m_isCustom = value;
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06001E33 RID: 7731 RVA: 0x001DC288 File Offset: 0x001DB288
		internal Dictionary<string, string> BuiltinStyles
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_6A;
					case 1:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_4E;
							}
						}
						IL_4E:
						if (false)
						{
						}
						if (true)
						{
						}
						this.\u1712();
						num = 0;
						continue;
					}
					if (this.ᜃ != null)
					{
						break;
					}
					num = 1;
				}
				IL_6A:
				return this.ᜃ;
			}
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06001E34 RID: 7732 RVA: 0x001DC308 File Offset: 0x001DB308
		internal Dictionary<string, int> BuiltinStyleIds
		{
			get
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_62;
					case 2:
						for (;;)
						{
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_4E;
							}
						}
						IL_4E:
						if (false)
						{
						}
						this.ᜀ();
						num = 0;
						continue;
					}
					if (this.ᜄ != null)
					{
						break;
					}
					num = 2;
				}
				IL_62:
				if (true)
				{
				}
				return this.ᜄ;
			}
		}

		// Token: 0x06001E35 RID: 7733 RVA: 0x001DC388 File Offset: 0x001DB388
		protected Style(Document doc)
		{
			int a_ = 18;
			this.ᜀ = 4094;
			base..ctor(doc, doc);
			this.m_chFormat = new CharacterFormat(base.Document);
			this.m_chFormat.ᜀ(this);
			this.ᜁ = ClipboardData.b("⭷๹ջች", a_) + doc.Styles.Count;
		}

		// Token: 0x06001E36 RID: 7734 RVA: 0x001DC3F8 File Offset: 0x001DB3F8
		public virtual void ApplyBaseStyle(string styleName)
		{
			int a_ = 16;
			for (;;)
			{
				this.m_baseStyle = this.m_doc.Styles.FindByName(styleName, this.StyleType);
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.m_baseStyle = this.m_doc.Styles.FindByName(styleName);
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_4A;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							goto IL_84;
						}
						break;
					case 2:
						goto IL_A2;
					case 3:
						goto IL_4A;
					case 4:
						if (this.m_baseStyle == null)
						{
							num = 2;
							continue;
						}
						goto IL_F6;
					}
					break;
					IL_4A:
					if (this.m_baseStyle == null)
					{
						num = 0;
						continue;
					}
					IL_84:
					num = 4;
				}
			}
			IL_A2:
			throw new ArgumentException(ClipboardData.b("㡵᝷婹ཻ୽ꒃ黎꺍歹ﶗ몙뒛뺝芟", a_) + styleName + ClipboardData.b("呵塷卹屻᝽ꊁﾉﺏ", a_));
			IL_F6:
			this.CharacterFormat.ApplyBase(((Style)this.BaseStyle).CharacterFormat);
		}

		// Token: 0x06001E37 RID: 7735 RVA: 0x001DC518 File Offset: 0x001DB518
		public void ApplyBaseStyle(BuiltinStyle bStyle)
		{
			for (;;)
			{
				IStyle style = this.m_doc.AddStyle(bStyle);
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (style != null)
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
						this.ApplyBaseStyle(style.Name);
						num = 1;
						continue;
					}
					break;
				}
			}
		}

		// Token: 0x06001E38 RID: 7736
		public abstract IStyle Clone();

		// Token: 0x06001E39 RID: 7737 RVA: 0x001DC5A0 File Offset: 0x001DB5A0
		internal void \u170D()
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
			ParagraphStyle paragraphStyle = this as ParagraphStyle;
			paragraphStyle.CharacterFormat.CharacterProps.ᜀ(null);
			paragraphStyle.CharacterFormat.BaseFormat = null;
			paragraphStyle.ParagraphFormat.ParaProps.ᜀ(null);
			paragraphStyle.ParagraphFormat.BaseFormat = null;
			paragraphStyle.m_baseStyle = null;
		}

		// Token: 0x06001E3A RID: 7738 RVA: 0x001DC624 File Offset: 0x001DB624
		internal void ᜁ(string A_0)
		{
			int a_ = 3;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_86;
				case 1:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_86;
					default:
						if (false)
						{
						}
						if (A_0.Length == 0)
						{
							if (true)
							{
							}
							num = 0;
							continue;
						}
						goto IL_92;
					}
					break;
				case 2:
					num = 1;
					continue;
				}
				if (A_0 == null)
				{
					break;
				}
				num = 2;
			}
			IL_36:
			throw new ArgumentNullException(ClipboardData.b("㩨Ὢᑬͮᑰ卲㭴ᙶᑸṺ嵼౾ꮊ뎒릘춠莢쪤햦覨캪사\udfae얰쪲", a_));
			IL_86:
			goto IL_36;
			IL_92:
			this.ᜁ = A_0;
		}

		// Token: 0x06001E3B RID: 7739 RVA: 0x001DC6CC File Offset: 0x001DB6CC
		protected override object CloneImpl()
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
			Style style = (Style)base.CloneImpl();
			style.m_chFormat = new CharacterFormat(base.Document);
			style.m_chFormat.ImportContainer(this.CharacterFormat);
			return style;
		}

		// Token: 0x06001E3C RID: 7740 RVA: 0x001DC738 File Offset: 0x001DB738
		internal override void CloneRelationsTo(Document doc, OwnerHolder nextOwner)
		{
			int num = 11;
			for (;;)
			{
				Dictionary<string, string> dictionary;
				Dictionary<string, string> dictionary2;
				switch (num)
				{
				case 0:
					(this.m_baseStyle as Style).ᜁ(dictionary[this.m_baseStyle.Name]);
					num = 25;
					continue;
				case 1:
					if (doc.ImportStyles)
					{
						num = 18;
						continue;
					}
					this.m_baseStyle = doc.Styles.FindByName(this.m_baseStyle.Name, this.StyleType);
					num = 8;
					continue;
				case 2:
					goto IL_1D1;
				case 3:
					if (doc.Styles.FindByName(this.m_baseStyle.Name, this.StyleType) != null)
					{
						num = 9;
						continue;
					}
					goto IL_11F;
				case 4:
					if (base.Document.StyleNameIds.ContainsValue(this.m_baseStyle.Name))
					{
						num = 6;
						continue;
					}
					goto IL_1D1;
				case 5:
					goto IL_1D1;
				case 6:
					num = 26;
					continue;
				case 7:
					dictionary2 = doc.CurClonedSection.OldParaStylesHolder;
					goto IL_37A;
				case 8:
					goto IL_1D1;
				case 9:
					num = 1;
					continue;
				case 10:
					if (doc.Styles.FindByName(this.m_baseStyle.Name, this.StyleType) == null)
					{
						num = 22;
						continue;
					}
					num = 17;
					continue;
				case 12:
					return;
				case 13:
				{
					string key;
					doc.StyleNameIds.Add(key, this.m_baseStyle.Name);
					goto IL_322;
				}
				case 14:
					num = 20;
					continue;
				case 15:
					num = 10;
					continue;
				case 16:
				{
					string key;
					if (!doc.StyleNameIds.ContainsKey(key))
					{
						num = 13;
						continue;
					}
					goto IL_1D1;
				}
				case 17:
					if (doc.CurClonedSection != null)
					{
						num = 14;
						continue;
					}
					goto IL_1D1;
				case 18:
					goto IL_11F;
				case 19:
					num = 16;
					continue;
				case 20:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_322;
					default:
						if (false)
						{
						}
						if (this.m_baseStyle.StyleType != StyleType.CharacterStyle)
						{
							num = 21;
							continue;
						}
						num = 23;
						continue;
					}
					break;
				case 21:
					num = 7;
					continue;
				case 22:
				{
					doc.Styles.Add(this.m_baseStyle.Clone());
					string key = this.ᜀ(this.m_baseStyle.Name);
					num = 4;
					continue;
				}
				case 23:
					dictionary2 = doc.CurClonedSection.OldCharStylesHolder;
					goto IL_37A;
				case 24:
					if (dictionary.ContainsKey(this.m_baseStyle.Name))
					{
						num = 0;
						continue;
					}
					num = 3;
					continue;
				case 25:
					goto IL_1D1;
				case 26:
					if (!doc.StyleNameIds.ContainsValue(this.m_baseStyle.Name))
					{
						num = 19;
						continue;
					}
					goto IL_1D1;
				}
				if (true)
				{
				}
				if (this.m_baseStyle != null)
				{
					num = 15;
					continue;
				}
				break;
				IL_11F:
				this.m_baseStyle = this.ᜀ(this.m_baseStyle, doc);
				num = 5;
				continue;
				IL_1D1:
				this.CharacterFormat.ApplyBase(((Style)this.BaseStyle).CharacterFormat);
				num = 12;
				continue;
				IL_322:
				num = 2;
				continue;
				IL_37A:
				dictionary = dictionary2;
				num = 24;
			}
		}

		// Token: 0x06001E3D RID: 7741 RVA: 0x001DCAF4 File Offset: 0x001DBAF4
		private IStyle ᜀ(IStyle A_0, Document A_1)
		{
			int a_ = 15;
			switch (0)
			{
			default:
			{
				IStyle style;
				for (;;)
				{
					style = null;
					int num = 17;
					for (;;)
					{
						switch (num)
						{
						case 0:
							return style;
						case 1:
							(style as Style).ᜁ(style.Name.Substring(0, 63));
							num = 14;
							continue;
						case 2:
							goto IL_2E7;
						case 3:
							A_1.CurClonedSection.OldParaStylesHolder.Add(A_0.Name, style.Name);
							num = 16;
							continue;
						case 4:
							goto IL_2E7;
						case 5:
							if (!A_1.CurClonedSection.OldParaStylesHolder.ContainsKey(A_0.Name))
							{
								num = 3;
								continue;
							}
							goto IL_270;
						case 6:
							if (!A_1.CurClonedSection.OldCharStylesHolder.ContainsKey(A_0.Name))
							{
								num = 7;
								continue;
							}
							return style;
						case 7:
							A_1.CurClonedSection.OldCharStylesHolder.Add(A_0.Name, style.Name);
							num = 0;
							continue;
						case 8:
							if (A_0.StyleType == StyleType.CharacterStyle)
							{
								num = 13;
								continue;
							}
							return style;
						case 9:
							style = A_0.Clone();
							num = 12;
							continue;
						case 10:
						{
							string oldValue;
							(style as Style).ᜁ(style.Name.Replace(oldValue, Guid.NewGuid().ToString()));
							num = 4;
							continue;
						}
						case 11:
							if (style.Name.Length > 63)
							{
								num = 1;
								continue;
							}
							goto IL_299;
						case 12:
						{
							if (true)
							{
							}
							string oldValue;
							if (this.ᜀ(style, out oldValue))
							{
								num = 10;
								continue;
							}
							(style as Style).ᜁ(style.Name + ClipboardData.b("⩴", a_) + Guid.NewGuid().ToString());
							num = 2;
							continue;
						}
						case 13:
							num = 6;
							continue;
						case 14:
							goto IL_299;
						case 15:
							if (A_0.StyleType == StyleType.ParagraphStyle)
							{
								num = 19;
								continue;
							}
							goto IL_270;
						case 16:
							return style;
						case 17:
							if (A_0 != null)
							{
								num = 9;
								continue;
							}
							return style;
						case 18:
							num = 15;
							continue;
						case 19:
							num = 5;
							continue;
						case 20:
							if (A_1.CurClonedSection != null)
							{
								num = 18;
								continue;
							}
							return style;
						}
						break;
						IL_270:
						num = 8;
						continue;
						IL_299:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							return style;
						default:
							if (false)
							{
							}
							A_1.Styles.Add(style);
							num = 20;
							continue;
						}
						IL_2E7:
						(style as Style).StyleId = 4094;
						num = 11;
					}
				}
				return style;
			}
			}
		}

		// Token: 0x06001E3E RID: 7742 RVA: 0x001DCE28 File Offset: 0x001DBE28
		private bool ᜀ(IStyle A_0, out string A_1)
		{
			int a_ = 16;
			switch (0)
			{
			default:
				for (;;)
				{
					A_1 = string.Empty;
					char[] separator = new char[]
					{
						'-'
					};
					int num = 12;
					for (;;)
					{
						string[] array;
						switch (num)
						{
						case 0:
							if (array.Length == 5)
							{
								num = 18;
								continue;
							}
							return false;
						case 1:
							if (A_0.Name.Contains(ClipboardData.b("孵", a_)))
							{
								num = 5;
								continue;
							}
							return false;
						case 2:
							if (array[2].Length == 4)
							{
								num = 10;
								continue;
							}
							return false;
						case 3:
							if (array[0].Length == 8)
							{
								if (true)
								{
								}
								num = 9;
								continue;
							}
							return false;
						case 4:
							num = 1;
							continue;
						case 5:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_206;
							default:
							{
								if (false)
								{
								}
								int num2 = A_0.Name.LastIndexOf(ClipboardData.b("⥵", a_)) + 1;
								num = 16;
								continue;
							}
							}
							break;
						case 6:
							if (array[3].Length == 4)
							{
								goto IL_206;
							}
							return false;
						case 7:
							if (array[4].Length == 12)
							{
								num = 14;
								continue;
							}
							return false;
						case 8:
							num = 2;
							continue;
						case 9:
							num = 13;
							continue;
						case 10:
							num = 6;
							continue;
						case 11:
							if (A_1.Length - 4 == 32)
							{
								num = 15;
								continue;
							}
							return false;
						case 12:
							if (A_0.Name.Contains(ClipboardData.b("⥵", a_)))
							{
								num = 4;
								continue;
							}
							return false;
						case 13:
							if (array[1].Length == 4)
							{
								num = 8;
								continue;
							}
							return false;
						case 14:
							return true;
						case 15:
							num = 3;
							continue;
						case 16:
						{
							int num2;
							if (A_0.Name.Length > num2)
							{
								num = 20;
								continue;
							}
							goto IL_294;
						}
						case 17:
							goto IL_294;
						case 18:
							num = 11;
							continue;
						case 19:
							num = 7;
							continue;
						case 20:
						{
							int num2;
							A_1 = A_0.Name.Substring(num2);
							num = 17;
							continue;
						}
						}
						break;
						IL_206:
						num = 19;
						continue;
						IL_294:
						array = A_1.Split(separator);
						num = 0;
					}
				}
				return true;
			}
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x001DD0F8 File Offset: 0x001DC0F8
		internal void ᜁ(Document A_0)
		{
			for (;;)
			{
				IStyle style = this.Clone();
				int num = 16;
				for (;;)
				{
					switch (num)
					{
					case 0:
					{
						string key;
						if (!A_0.StyleNameIds.ContainsKey(key))
						{
							num = 4;
							continue;
						}
						goto IL_20B;
					}
					case 1:
						num = 0;
						continue;
					case 2:
						A_0.CurClonedSection.OldParaStylesHolder.Add(this.Name, this.Name);
						num = 12;
						continue;
					case 3:
						goto IL_288;
					case 4:
					{
						string key;
						A_0.StyleNameIds.Add(key, style.Name);
						num = 9;
						continue;
					}
					case 5:
						num = 18;
						continue;
					case 6:
					{
						A_0.Styles.Add(style);
						string key = this.ᜀ(style.Name);
						num = 7;
						continue;
					}
					case 7:
						if (true)
						{
						}
						if (base.Document.StyleNameIds.ContainsValue(style.Name))
						{
							num = 14;
							continue;
						}
						goto IL_20B;
					case 8:
						num = 13;
						continue;
					case 9:
						goto IL_2AA;
					case 10:
						if (this.StyleType == StyleType.CharacterStyle)
						{
							num = 8;
							continue;
						}
						goto IL_1B9;
					case 11:
						if (this.StyleType == StyleType.ParagraphStyle)
						{
							num = 5;
							continue;
						}
						return;
					case 12:
						goto IL_206;
					case 13:
						if (!A_0.CurClonedSection.OldCharStylesHolder.ContainsKey(this.Name))
						{
							num = 3;
							continue;
						}
						goto IL_1B9;
					case 14:
						num = 19;
						continue;
					case 15:
						if (A_0.CurClonedSection != null)
						{
							num = 17;
							continue;
						}
						return;
					case 16:
						if (A_0.Styles.FindByName(style.Name, style.StyleType) == null)
						{
							num = 6;
							continue;
						}
						goto IL_20B;
					case 17:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_2AA;
						default:
							if (false)
							{
							}
							num = 10;
							continue;
						}
						break;
					case 18:
						if (!A_0.CurClonedSection.OldParaStylesHolder.ContainsKey(this.Name))
						{
							num = 2;
							continue;
						}
						return;
					case 19:
						if (!A_0.StyleNameIds.ContainsValue(style.Name))
						{
							num = 1;
							continue;
						}
						goto IL_20B;
					}
					break;
					IL_1B9:
					num = 11;
					continue;
					IL_20B:
					num = 15;
					continue;
					IL_2AA:
					goto IL_20B;
				}
			}
			IL_206:
			return;
			IL_288:
			A_0.CurClonedSection.OldCharStylesHolder.Add(this.Name, this.Name);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x001DD3B4 File Offset: 0x001DC3B4
		private string ᜀ(string A_0)
		{
			string result = "";
			Dictionary<string, string>.Enumerator enumerator = base.Document.StyleNameIds.GetEnumerator();
			try
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_BF;
					case 1:
					{
						if (!enumerator.MoveNext())
						{
							num = 0;
							continue;
						}
						KeyValuePair<string, string> keyValuePair = enumerator.Current;
						num = 5;
						continue;
					}
					case 2:
						goto IL_BF;
					case 3:
						goto IL_CA;
					case 5:
					{
						KeyValuePair<string, string> keyValuePair;
						if (keyValuePair.Value == A_0)
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
								num = 6;
								continue;
							}
						}
						break;
					}
					case 6:
					{
						KeyValuePair<string, string> keyValuePair;
						result = keyValuePair.Key;
						num = 2;
						continue;
					}
					}
					IL_8C:
					num = 1;
					continue;
					goto IL_8C;
					IL_BF:
					num = 3;
				}
				IL_CA:;
			}
			finally
			{
				if (true)
				{
				}
				((IDisposable)enumerator).Dispose();
			}
			return result;
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x001DD4C0 File Offset: 0x001DC4C0
		internal IStyle ᜀ(Document A_0, IStyle A_1)
		{
			int num = 7;
			IStyle style;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 3;
					continue;
				case 1:
					goto IL_C2;
				case 2:
					if (A_1.StyleType == StyleType.CharacterStyle)
					{
						num = 6;
						continue;
					}
					goto IL_171;
				case 3:
				{
					string text;
					if (text.Length > 0)
					{
						num = 9;
						continue;
					}
					goto IL_168;
				}
				case 4:
				{
					if (true)
					{
					}
					string text = A_0.CurClonedSection.OldCharStylesHolder[A_1.Name];
					num = 8;
					continue;
				}
				case 5:
				{
					string text = string.Empty;
					num = 2;
					continue;
				}
				case 6:
					num = 13;
					continue;
				case 7:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return this;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 8:
					goto IL_C2;
				case 9:
				{
					string text;
					style = A_0.Styles.FindByName(text, A_1.StyleType);
					num = 10;
					continue;
				}
				case 10:
					if (style != null)
					{
						num = 11;
						continue;
					}
					return this;
				case 11:
					return style;
				case 12:
					if (A_0.CurClonedSection.OldParaStylesHolder.ContainsKey(A_1.Name))
					{
						num = 15;
						continue;
					}
					goto IL_C2;
				case 13:
					if (A_0.CurClonedSection.OldCharStylesHolder.ContainsKey(A_1.Name))
					{
						num = 4;
						continue;
					}
					goto IL_171;
				case 14:
				{
					string text;
					if (text != null)
					{
						num = 0;
						continue;
					}
					goto IL_168;
				}
				case 15:
				{
					string text = A_0.CurClonedSection.OldParaStylesHolder[A_1.Name];
					num = 1;
					continue;
				}
				}
				if (this.StyleType == A_1.StyleType)
				{
					num = 5;
					continue;
				}
				return this;
				IL_C2:
				num = 14;
				continue;
				IL_171:
				num = 12;
			}
			return style;
			IL_168:
			return this.ᜀ(this, A_0);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x001DD6C0 File Offset: 0x001DC6C0
		public static Style CreateBuiltinStyle(BuiltinStyle bStyle, Document doc)
		{
			int a_ = 9;
			if (true)
			{
			}
			Style style;
			for (;;)
			{
				style = new ParagraphStyle(doc);
				Style.BuiltinStyleLoader.ᜀ(style, bStyle);
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return style;
					case 1:
						(style as ParagraphStyle).CharacterFormat.LocaleIdASCII = 1033;
						num = 0;
						continue;
					case 2:
						if (style.Name == ClipboardData.b("ⅮṰŲᡴᙶᕸ", a_))
						{
							num = 4;
							continue;
						}
						return style;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_66;
						default:
							if (false)
							{
							}
							if (style.StyleType == StyleType.ParagraphStyle)
							{
								num = 1;
								continue;
							}
							return style;
						}
						break;
					case 4:
						goto IL_66;
					}
					break;
					IL_66:
					num = 3;
				}
			}
			return style;
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x001DD79C File Offset: 0x001DC79C
		internal static IStyle ᜀ(DefaultTableStyle A_0, Document A_1)
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
			IStyle style = new spr\u173A(A_1);
			Style.BuiltinStyleLoader.ᜀ(style, A_0);
			return style;
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x001DD7E8 File Offset: 0x001DC7E8
		public static IStyle CreateBuiltinStyle(BuiltinStyle bStyle, StyleType type, Document doc)
		{
			IStyle style;
			for (;;)
			{
				style = null;
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
							goto IL_A4;
						default:
							goto IL_7B;
						}
						break;
					case 1:
						goto IL_A2;
					case 2:
						num = 1;
						continue;
					case 3:
						goto IL_BE;
					case 4:
						switch (type)
						{
						case StyleType.ParagraphStyle:
							style = new ParagraphStyle(doc);
							num = 0;
							continue;
						case StyleType.TableStyle:
							goto IL_C0;
						case StyleType.CharacterStyle:
							style = new sprᯉ(doc);
							num = 5;
							continue;
						case StyleType.OtherStyle:
							goto IL_A4;
						default:
							num = 2;
							continue;
						}
						break;
					case 5:
						goto IL_95;
					}
					break;
					IL_A4:
					style = new ListStyle(doc);
					if (true)
					{
					}
					num = 3;
				}
			}
			IL_7B:
			if (false)
			{
			}
			IL_95:
			IL_A2:
			IL_BE:
			IL_C0:
			Style.BuiltinStyleLoader.ᜀ(style, bStyle);
			return style;
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x001DD8C0 File Offset: 0x001DC8C0
		internal static string ᜁ(BuiltinStyle A_0)
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
			return Style.BuiltinStyleLoader.ᜃ[(int)A_0];
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x001DD904 File Offset: 0x001DC904
		internal static string ᜀ(DefaultTableStyle A_0)
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
			return Style.BuiltinStyleLoader.ᜄ[(int)A_0];
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x001DD948 File Offset: 0x001DC948
		public static BuiltinStyle NameToBuiltIn(string styleName)
		{
			switch (0)
			{
			default:
			{
				BuiltinStyle result;
				for (;;)
				{
					IL_3D:
					string b;
					int num;
					int num2;
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_85:
						goto IL_C5;
					default:
						if (false)
						{
						}
						b = styleName.Trim();
						result = BuiltinStyle.User;
						num = Style.BuiltinStyleLoader.ᜃ.Length;
						num2 = 0;
						num3 = 1;
						break;
					}
					for (;;)
					{
						IL_1A:
						switch (num3)
						{
						case 0:
							result = (BuiltinStyle)num2;
							if (true)
							{
							}
							num3 = 6;
							continue;
						case 1:
							goto IL_76;
						case 2:
							if (Style.BuiltinStyleLoader.ᜃ[num2] == b)
							{
								num3 = 0;
								continue;
							}
							num2++;
							num3 = 4;
							continue;
						case 3:
							return result;
						case 4:
							goto IL_85;
						case 5:
							if (num2 >= num)
							{
								num3 = 3;
								continue;
							}
							num3 = 2;
							continue;
						case 6:
							return result;
						}
						goto IL_3D;
					}
					IL_76:
					IL_C5:
					num3 = 5;
					goto IL_1A;
				}
				return result;
			}
			}
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x001DDA3C File Offset: 0x001DCA3C
		internal static bool ᜀ(BuiltinStyle A_0)
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
			return Style.BuiltinStyleLoader.ᜀ(A_0);
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x001DDA80 File Offset: 0x001DCA80
		internal virtual void Close()
		{
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_5C;
				case 2:
					IL_2C:
					if (true)
					{
					}
					this.m_chFormat.Close();
					this.m_chFormat = null;
					num = 0;
					continue;
				}
				if (this.m_chFormat != null)
				{
					num = 2;
					continue;
				}
				IL_5C:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2C;
				default:
					goto IL_72;
				}
			}
			IL_72:
			if (false)
			{
			}
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x001DDB08 File Offset: 0x001DCB08
		internal void \u1712()
		{
			int a_ = 10;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜃ = new Dictionary<string, string>();
			this.ᜃ.Add(ClipboardData.b("ṯᵱٳ᭵᥷ᙹ", a_), ClipboardData.b("㹯ᵱٳ᭵᥷ᙹ", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᅷᑹ᭻幽녿", a_), ClipboardData.b("㡯᝱ᕳትᅷᑹ᭻幽녿", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᅷᑹ᭻幽뉿", a_), ClipboardData.b("㡯᝱ᕳትᅷᑹ᭻幽뉿", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᅷᑹ᭻幽덿", a_), ClipboardData.b("㡯᝱ᕳትᅷᑹ᭻幽덿", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᅷᑹ᭻幽둿", a_), ClipboardData.b("㡯᝱ᕳትᅷᑹ᭻幽둿", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᅷᑹ᭻幽땿", a_), ClipboardData.b("㡯᝱ᕳትᅷᑹ᭻幽땿", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᅷᑹ᭻幽뙿", a_), ClipboardData.b("㡯᝱ᕳትᅷᑹ᭻幽뙿", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᅷᑹ᭻幽띿", a_), ClipboardData.b("㡯᝱ᕳትᅷᑹ᭻幽띿", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᅷᑹ᭻幽롿", a_), ClipboardData.b("㡯᝱ᕳትᅷᑹ᭻幽롿", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᅷᑹ᭻幽륿", a_), ClipboardData.b("㡯᝱ᕳትᅷᑹ᭻幽륿", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹䵻", a_), ClipboardData.b("㥯ᱱၳ፵w婹䵻", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹乻", a_), ClipboardData.b("㥯ᱱၳ፵w婹乻", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹佻", a_), ClipboardData.b("㥯ᱱၳ፵w婹佻", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹䡻", a_), ClipboardData.b("㥯ᱱၳ፵w婹䡻", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹䥻", a_), ClipboardData.b("㥯ᱱၳ፵w婹䥻", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹䩻", a_), ClipboardData.b("㥯ᱱၳ፵w婹䩻", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹䭻", a_), ClipboardData.b("㥯ᱱၳ፵w婹䭻", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹䑻", a_), ClipboardData.b("㥯ᱱၳ፵w婹䑻", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹䕻", a_), ClipboardData.b("㥯ᱱၳ፵w婹䕻", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᝳ噵䥷", a_), ClipboardData.b("⑯㵱㝳噵䥷", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᝳ噵䩷", a_), ClipboardData.b("⑯㵱㝳噵䩷", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᝳ噵䭷", a_), ClipboardData.b("⑯㵱㝳噵䭷", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᝳ噵䱷", a_), ClipboardData.b("⑯㵱㝳噵䱷", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᝳ噵䵷", a_), ClipboardData.b("⑯㵱㝳噵䵷", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᝳ噵乷", a_), ClipboardData.b("⑯㵱㝳噵乷", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᝳ噵佷", a_), ClipboardData.b("⑯㵱㝳噵佷", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᝳ噵䁷", a_), ClipboardData.b("⑯㵱㝳噵䁷", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᝳ噵䅷", a_), ClipboardData.b("⑯㵱㝳噵䅷", a_));
			this.ᜃ.Add(ClipboardData.b("ṯᵱٳ᭵᥷ᙹ屻᝽ﲇ", a_), ClipboardData.b("㹯ᵱٳ᭵᥷ᙹ屻㝽ﲇ", a_));
			this.ᜃ.Add(ClipboardData.b("ᙯᵱ᭳ɵᙷᕹࡻ᭽ꁿﺅﲇ", a_), ClipboardData.b("㙯ᵱ᭳ɵᙷᕹࡻ᭽ꁿ횁ﺅﲇ", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᥳ᭵ᵷᑹࡻ幽ﲃ", a_), ClipboardData.b("㍯ᵱᥳ᭵ᵷᑹࡻ幽푿ﲃ", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯ᝱ᕳትᵷࡹ", a_), ClipboardData.b("㡯᝱ᕳትᵷࡹ", a_));
			this.ᜃ.Add(ClipboardData.b("ᙯᵱ᭳ɵᵷࡹ", a_), ClipboardData.b("㙯ᵱ᭳ɵᵷࡹ", a_));
			this.ᜃ.Add(ClipboardData.b("᥯ᱱၳ፵w婹ᑻ᭽", a_), ClipboardData.b("㥯ᱱၳ፵w婹㑻᭽", a_));
			this.ᜃ.Add(ClipboardData.b("፯፱ѳɵᅷᕹቻ", a_), ClipboardData.b("㍯፱ѳɵᅷᕹቻ", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹፻᡽ꁿﶇﶍ", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹፻᡽ꁿ쒁ﶇﶍ", a_));
			this.ᜃ.Add(ClipboardData.b("ᙯᵱ᭳ɵᙷᕹࡻ᭽ꁿ", a_), ClipboardData.b("㙯ᵱ᭳ɵᙷᕹࡻ᭽ꁿ킁", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᥳ᭵ᵷᑹࡻ幽慎", a_), ClipboardData.b("㍯ᵱᥳ᭵ᵷᑹࡻ幽퉿慎", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ᩳ፵塷ᑹॻ፽", a_), ClipboardData.b("㱯᭱ᩳ፵塷㑹ॻ፽", a_));
			this.ᜃ.Add(ClipboardData.b("o፱፳፵塷ᑹॻ፽", a_), ClipboardData.b("⁯፱፳፵塷㑹ॻ፽", a_));
			this.ᜃ.Add(ClipboardData.b("ᕯᱱၳᡵ᝷๹᥻幽慎", a_), ClipboardData.b("㕯ᱱၳᡵ᝷๹᥻幽퉿慎", a_));
			this.ᜃ.Add(ClipboardData.b("ᕯᱱၳᡵ᝷๹᥻幽ﲃ", a_), ClipboardData.b("㕯ᱱၳᡵ᝷๹᥻幽푿ﲃ", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹፻᡽ꁿﺋﮑ", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹፻᡽ꁿ쎁ﺋﮑ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ፱ᝳѵ᝷", a_), ClipboardData.b("㵯፱ᝳѵ᝷婹⡻᭽", a_));
			this.ᜃ.Add(ClipboardData.b("ѯᵱᕳ噵ၷόᵻ᩽", a_), ClipboardData.b("⑯㵱㕳噵ぷόᵻ᩽", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ", a_), ClipboardData.b("㱯᭱ݳɵ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᡹ॻች", a_), ClipboardData.b("㱯᭱ݳɵ塷㡹ॻች", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷ᑹॻ፽", a_), ClipboardData.b("㱯᭱ݳɵ塷㑹ॻ፽", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷䡹", a_), ClipboardData.b("㱯᭱ݳɵ塷䡹", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷䥹", a_), ClipboardData.b("㱯᭱ݳɵ塷䥹", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷乹", a_), ClipboardData.b("㱯᭱ݳɵ塷乹", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷佹", a_), ClipboardData.b("㱯᭱ݳɵ塷佹", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᡹ॻችꚅ몇", a_), ClipboardData.b("㱯᭱ݳɵ塷㡹ॻችꚅ몇", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᡹ॻችꚅ뮇", a_), ClipboardData.b("㱯᭱ݳɵ塷㡹ॻችꚅ뮇", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᡹ॻችꚅ벇", a_), ClipboardData.b("㱯᭱ݳɵ塷㡹ॻችꚅ벇", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᡹ॻችꚅ붇", a_), ClipboardData.b("㱯᭱ݳɵ塷㡹ॻችꚅ붇", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷ᑹॻ፽ꚅ몇", a_), ClipboardData.b("㱯᭱ݳɵ塷㑹ॻ፽ꚅ몇", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷ᑹॻ፽ꚅ뮇", a_), ClipboardData.b("㱯᭱ݳɵ塷㑹ॻ፽ꚅ뮇", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷ᑹॻ፽ꚅ벇", a_), ClipboardData.b("㱯᭱ݳɵ塷㑹ॻ፽ꚅ벇", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷ᑹॻ፽ꚅ붇", a_), ClipboardData.b("㱯᭱ݳɵ塷㑹ॻ፽ꚅ붇", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ᭱s᩵ᵷ", a_), ClipboardData.b("⑯᭱s᩵ᵷ", a_));
			this.ᜃ.Add(ClipboardData.b("፯ṱ᭳յᅷᑹ᭻", a_), ClipboardData.b("㍯ṱ᭳յᅷᑹ᭻", a_));
			this.ᜃ.Add(ClipboardData.b("ͯ᭱፳ᡵ᥷๹ॻ౽", a_), ClipboardData.b("⍯᭱፳ᡵ᥷๹ॻ౽", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯ᝱ታ᝵൷ᙹࡻ幽ﺍ늑秊", a_), ClipboardData.b("㑯᝱ታ᝵൷ᙹࡻ幽큿ﺍ늑튓秊", a_));
			this.ᜃ.Add(ClipboardData.b("ቯᵱၳཱུ塷๹᥻ٽ", a_), ClipboardData.b("㉯ᵱၳཱུ塷⹹᥻ٽ", a_));
			this.ᜃ.Add(ClipboardData.b("ቯᵱၳཱུ塷๹᥻ٽꊁ揄", a_), ClipboardData.b("㉯ᵱၳཱུ塷⹹᥻ٽꊁ춃揄", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᥹፻ၽ", a_), ClipboardData.b("㱯᭱ݳɵ塷㥹፻ၽ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᥹፻ၽꪉ뺋", a_), ClipboardData.b("㱯᭱ݳɵ塷㥹፻ၽꪉ뺋", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᥹፻ၽꪉ뾋", a_), ClipboardData.b("㱯᭱ݳɵ塷㥹፻ၽꪉ뾋", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᥹፻ၽꪉ뢋", a_), ClipboardData.b("㱯᭱ݳɵ塷㥹፻ၽꪉ뢋", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱ݳɵ塷᥹፻ၽꪉ릋", a_), ClipboardData.b("㱯᭱ݳɵ塷㥹፻ၽꪉ릋", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ݳյ᥷ᵹ᥻幽", a_), ClipboardData.b("㵯᝱ݳյ᥷ᵹ᥻幽졿", a_));
			this.ᜃ.Add(ClipboardData.b("ͯݱᙳɵᅷ๹ၻ᭽", a_), ClipboardData.b("⍯ݱᙳɵᅷ๹ၻ᭽", a_));
			this.ᜃ.Add(ClipboardData.b("ͯ፱ᡳ͵౷᭹ࡻ᝽", a_), ClipboardData.b("⍯፱ᡳ͵౷᭹ࡻ᝽", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯ፱s፵", a_), ClipboardData.b("㑯፱s፵", a_));
			this.ᜃ.Add(ClipboardData.b("ቯᵱၳཱུ塷๹᥻ٽꊁ慎黎꺍憐ﲑ", a_), ClipboardData.b("㉯ᵱၳཱུ塷⹹᥻ٽꊁ슃慎黎꺍\ud98fﲑ", a_));
			this.ᜃ.Add(ClipboardData.b("ቯᵱၳཱུ塷๹᥻ٽꊁ慎黎꺍憐ﲑ벛겝", a_), ClipboardData.b("㉯ᵱၳཱུ塷⹹᥻ٽꊁ슃慎黎꺍\ud98fﲑ벛겝", a_));
			this.ᜃ.Add(ClipboardData.b("ṯᵱs፵塷ቹ᥻ώ", a_), ClipboardData.b("㹯ᵱs፵塷㉹᥻ώ", a_));
			this.ᜃ.Add(ClipboardData.b("ቯᵱၳཱུ塷๹᥻ٽꊁ뚃", a_), ClipboardData.b("㉯ᵱၳཱུ塷⹹᥻ٽꊁ뚃", a_));
			this.ᜃ.Add(ClipboardData.b("ቯᵱၳཱུ塷๹᥻ٽꊁ랃", a_), ClipboardData.b("㉯ᵱၳཱུ塷⹹᥻ٽꊁ랃", a_));
			this.ᜃ.Add(ClipboardData.b("ቯᵱၳཱུ塷๹᥻ٽꊁ揄낏ꂑ", a_), ClipboardData.b("㉯ᵱၳཱུ塷⹹᥻ٽꊁ춃揄낏ꂑ", a_));
			this.ᜃ.Add(ClipboardData.b("ቯᵱၳཱུ塷๹᥻ٽꊁ揄낏ꆑ", a_), ClipboardData.b("㉯ᵱၳཱུ塷⹹᥻ٽꊁ춃揄낏ꆑ", a_));
			this.ᜃ.Add(ClipboardData.b("ቯṱ᭳ᕵ፷婹ࡻ᭽", a_), ClipboardData.b("㉯ṱ᭳ᕵ፷婹⡻᭽", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯୱѳ፵੷ᙹᕻၽ", a_), ClipboardData.b("㡯ୱѳ፵੷ᙹᕻၽ", a_));
			this.ᜃ.Add(ClipboardData.b("ᙯᵱᡳ᩵᝷൹᥻᩽ﮁ慎ﮏ", a_), ClipboardData.b("㙯ᵱᡳ᩵᝷൹᥻᩽졿ﮁ慎ﮏ", a_));
			this.ᜃ.Add(ClipboardData.b("ͯٱٳ᥵ᙷᵹ", a_), ClipboardData.b("⍯ٱٳ᥵ᙷᵹ", a_));
			this.ᜃ.Add(ClipboardData.b("ᕯάѳṵ᥷ॹᕻൽ", a_), ClipboardData.b("㕯άѳṵ᥷ॹᕻൽ", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯᵱᝳ͵ᕷόቻ੽ꁿ", a_), ClipboardData.b("㑯ᵱᝳ͵ᕷόቻ੽ꁿ쾁", a_));
			this.ᜃ.Add(ClipboardData.b("oṱᕳήᙷ婹ࡻ᭽", a_), ClipboardData.b("⁯ṱᕳήᙷ婹⡻᭽", a_));
			this.ᜃ.Add(ClipboardData.b("ᕯ影ᥳ᝵ᅷᙹ屻ൽﲇﾉﺋ", a_), ClipboardData.b("㕯影ᥳ᝵ᅷᙹ屻⵽ﲇﾉﺋ", a_));
			this.ᜃ.Add(ClipboardData.b("ṯᵱٳ᭵᥷ᙹ屻噽꾅", a_), ClipboardData.b("㹯ᵱٳ᭵᥷ᙹ屻噽흿꾅", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷᭹ύ౽ﶃ", a_), ClipboardData.b("㡯♱㥳㩵塷㭹ύ౽ﶃ", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷᭹᡻᩽", a_), ClipboardData.b("㡯♱㥳㩵塷㭹᡻᩽", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷᥹ᕻ੽", a_), ClipboardData.b("㡯♱㥳㩵塷㥹ᕻ੽", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷᥹፻᩽", a_), ClipboardData.b("㡯♱㥳㩵塷㥹፻᩽", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷ṹ᥻᡽", a_), ClipboardData.b("㡯♱㥳㩵塷㹹᥻᡽", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷ᅹ᥻ݽ", a_), ClipboardData.b("㡯♱㥳㩵塷ㅹ᥻ݽ", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷੹๻᭽ﺉ", a_), ClipboardData.b("㡯♱㥳㩵塷⩹๻᭽ﺉ", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷ॹᵻ፽", a_), ClipboardData.b("㡯♱㥳㩵塷⥹ᵻ፽", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷๹ջ๽ﲇﺋ", a_), ClipboardData.b("㡯♱㥳㩵塷⹹ջ๽ﲇﺋ", a_));
			this.ᜃ.Add(ClipboardData.b("ᡯٱᥳ᩵塷౹ᵻ౽", a_), ClipboardData.b("㡯♱㥳㩵塷ⱹᵻ౽", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᥳ᭵ᵷᑹࡻ幽", a_), ClipboardData.b("㍯ᵱᥳ᭵ᵷᑹࡻ幽퍿", a_));
			this.ᜃ.Add(ClipboardData.b("ṯᵱ味᩵ᅷॹࡻ", a_), ClipboardData.b("㹯ᵱ味㩵ᅷॹࡻ", a_));
			this.ᜃ.Add(ClipboardData.b("ቯ፱ᡳ᩵᝷ᕹቻ幽ﲃ", a_), ClipboardData.b("㉯፱ᡳ᩵᝷ᕹቻ幽푿ﲃ", a_));
			this.ᜃ.Add(ClipboardData.b("կűᅳѵ", a_), ClipboardData.b("╯űᅳѵ", a_));
			this.ᜃ.Add(ClipboardData.b("ṯᵱݳɵŷᙹ᥻", a_), ClipboardData.b("㹯ᵱ❳ɵŷᙹ᥻", a_));
			this.ᜃ.Add(ClipboardData.b("ṯᵱٳ᭵᥷ᙹ屻੽", a_), ClipboardData.b("㹯ᵱٳ᭵᥷ᙹ屻⩽", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᭻౽", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㭻౽", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ཻᙽ", a_), ClipboardData.b("偯㹱ᵳᅵၷ๹屻⵽", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ཻᙽꪉ望뢗ꮙ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹⽻ᙽꪉ춋望뢗ꮙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ཻᙽꪉ望뢗ꢙ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹⽻ᙽꪉ춋望뢗ꢙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ཻᙽꪉ望뢗ꦙ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹⽻ᙽꪉ춋望뢗ꦙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ཻᙽꪉ望뢗꺙", a_), ClipboardData.b("㱯᭱፳ṵ౷婹⽻ᙽꪉ춋望뢗꺙", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ཻᙽꪉ望뢗꾙", a_), ClipboardData.b("㱯᭱፳ṵ౷婹⽻ᙽꪉ춋望뢗꾙", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ཻᙽꪉ望뢗겙", a_), ClipboardData.b("㱯᭱፳ṵ౷婹⽻ᙽꪉ춋望뢗겙", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ၻ᝽", a_), ClipboardData.b("㱯᭱፳ṵ౷婹ほ᝽", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ၻ᝽ꒃ늑ꖓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹ほ᝽ꒃ입늑ꖓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ၻ᝽ꒃ늑ꚓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹ほ᝽ꒃ입늑ꚓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ၻ᝽ꒃ늑ꞓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹ほ᝽ꒃ입늑ꞓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ၻ᝽ꒃ늑ꂓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹ほ᝽ꒃ입늑ꂓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ၻ᝽ꒃ늑ꆓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹ほ᝽ꒃ입늑ꆓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹ၻ᝽ꒃ늑ꊓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹ほ᝽ꒃ입늑ꊓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹᭻౽", a_), ClipboardData.b("㱯᭱፳ṵ౷婹㭻౽", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹᭻౽ꒃ늑ꖓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹㭻౽ꒃ입늑ꖓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹᭻౽ꒃ늑ꚓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹㭻౽ꒃ입늑ꚓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹᭻౽ꒃ늑ꞓ", a_), ClipboardData.b("偯㹱ᵳᅵၷ๹屻㥽ꚅ즇ﺏ뒓ꖕ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹᭻౽ꒃ늑ꂓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹㭻౽ꒃ입늑ꂓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹᭻౽ꒃ늑ꆓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹㭻౽ꒃ입늑ꆓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᱯ᭱፳ṵ౷婹᭻౽ꒃ늑ꊓ", a_), ClipboardData.b("㱯᭱፳ṵ౷婹㭻౽ꒃ입늑ꊓ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋뾍", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋뾍", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋뾍낏ﶗ뺝醟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋뾍낏펑ﶗ뺝醟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋뾍낏ﶗ뺝銟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋뾍낏펑ﶗ뺝銟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋뾍낏ﶗ뺝鎟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋뾍낏펑ﶗ뺝鎟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋뾍낏ﶗ뺝钟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋뾍낏펑ﶗ뺝钟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋뾍낏ﶗ뺝閟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋뾍낏펑ﶗ뺝閟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋뾍낏ﶗ뺝隟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋뾍낏펑ﶗ뺝隟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋벍", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋벍", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋벍낏ﶗ뺝醟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋벍낏펑ﶗ뺝醟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋벍낏ﶗ뺝銟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋벍낏펑ﶗ뺝銟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋벍낏ﶗ뺝鎟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋벍낏펑ﶗ뺝鎟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋벍낏ﶗ뺝钟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋벍낏펑ﶗ뺝钟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋벍낏ﶗ뺝閟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋벍낏펑ﶗ뺝閟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ൽ겋벍낏ﶗ뺝隟", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻⵽겋벍낏펑ﶗ뺝隟", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ릇", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ릇", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ릇ꪉ望뢗ꮙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ릇ꪉ춋望뢗ꮙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ릇ꪉ望뢗ꢙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ릇ꪉ춋望뢗ꢙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ릇ꪉ望뢗ꦙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ릇ꪉ춋望뢗ꦙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ릇ꪉ望뢗꺙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ릇ꪉ춋望뢗꺙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ릇ꪉ望뢗꾙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ릇ꪉ춋望뢗꾙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ릇ꪉ望뢗겙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ릇ꪉ춋望뢗겙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ몇", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ몇", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ몇ꪉ望뢗ꮙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ몇ꪉ춋望뢗ꮙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ몇ꪉ望뢗ꢙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ몇ꪉ춋望뢗ꢙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ몇ꪉ望뢗ꦙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ몇ꪉ춋望뢗ꦙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ몇ꪉ望뢗꺙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ몇ꪉ춋望뢗꺙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ몇ꪉ望뢗꾙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ몇ꪉ춋望뢗꾙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻ችꚅ몇ꪉ望뢗겙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㉽ꚅ몇ꪉ춋望뢗겙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ릇", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ릇", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ릇ꪉ望뢗ꮙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ릇ꪉ춋望뢗ꮙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ릇ꪉ望뢗ꢙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ릇ꪉ춋望뢗ꢙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ릇ꪉ望뢗ꦙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ릇ꪉ춋望뢗ꦙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ릇ꪉ望뢗꺙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ릇ꪉ춋望뢗꺙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ릇ꪉ望뢗꾙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ릇ꪉ춋望뢗꾙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ릇ꪉ望뢗겙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ릇ꪉ춋望뢗겙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ몇", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ몇", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ몇ꪉ望뢗ꮙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ몇ꪉ춋望뢗ꮙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ몇ꪉ望뢗ꢙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ몇ꪉ춋望뢗ꢙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ몇ꪉ望뢗ꦙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ몇ꪉ춋望뢗ꦙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ몇ꪉ望뢗꺙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ몇ꪉ춋望뢗꺙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ몇ꪉ望뢗꾙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ몇ꪉ춋望뢗꾙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ몇ꪉ望뢗겙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ몇ꪉ춋望뢗겙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ뮇", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ뮇", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ뮇ꪉ望뢗ꮙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ뮇ꪉ춋望뢗ꮙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ뮇ꪉ望뢗ꢙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ뮇ꪉ춋望뢗ꢙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ뮇ꪉ望뢗ꦙ", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ뮇ꪉ춋望뢗ꦙ", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ뮇ꪉ望뢗꺙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ뮇ꪉ춋望뢗꺙", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ뮇ꪉ望뢗꾙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ뮇ꪉ춋望궗", a_));
			this.ᜃ.Add(ClipboardData.b("ᵯ᝱ၳή൷᝹屻᥽ꚅ뮇ꪉ望뢗겙", a_), ClipboardData.b("㵯᝱ၳή൷᝹屻㥽ꚅ뮇ꪉ춋望뢗겙", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯ፱ٳᵵ塷ᙹᕻൽ", a_), ClipboardData.b("㑯፱ٳᵵ塷㙹ᕻൽ", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯ፱ٳᵵ塷ᙹᕻൽꊁ揄낏ꎑ", a_), ClipboardData.b("㑯፱ٳᵵ塷㙹ᕻൽꊁ얃揄낏ꎑ", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯ፱ٳᵵ塷ᙹᕻൽꊁ揄낏ꂑ", a_), ClipboardData.b("㑯፱ٳᵵ塷㙹ᕻൽꊁ얃揄낏ꂑ", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯ፱ٳᵵ塷ᙹᕻൽꊁ揄낏ꆑ", a_), ClipboardData.b("㑯፱ٳᵵ塷㙹ᕻൽꊁ얃揄낏ꆑ", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯ፱ٳᵵ塷ᙹᕻൽꊁ揄낏ꚑ", a_), ClipboardData.b("㑯፱ٳᵵ塷㙹ᕻൽꊁ얃揄낏ꚑ", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯ፱ٳᵵ塷ᙹᕻൽꊁ揄낏ꞑ", a_), ClipboardData.b("㑯፱ٳᵵ塷㙹ᕻൽꊁ얃揄낏ꞑ", a_));
			this.ᜃ.Add(ClipboardData.b("ᑯ፱ٳᵵ塷ᙹᕻൽꊁ揄낏꒑", a_), ClipboardData.b("㑯፱ٳᵵ塷㙹ᕻൽꊁ얃揄낏꒑", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿ", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ톁", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿ낏ﶗ뺝醟", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ톁낏펑ﶗ뺝醟", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿ낏ﶗ뺝銟", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ톁낏펑ﶗ뺝銟", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿ낏ﶗ뺝鎟", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ톁낏펑ﶗ뺝鎟", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿ낏ﶗ뺝钟", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ톁낏펑ﶗ뺝钟", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿ낏ﶗ뺝閟", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ톁낏펑ﶗ뺝閟", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿ낏ﶗ뺝隟", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ톁낏펑ﶗ뺝隟", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿﲇ", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ캁ﲇ", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿﲇꪉ望뢗ꮙ", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ캁ﲇꪉ춋望뢗ꮙ", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿﲇꪉ望뢗ꢙ", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ캁ﲇꪉ춋望뢗ꢙ", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿﲇꪉ望뢗ꦙ", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ캁ﲇꪉ춋望뢗ꦙ", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿﲇꪉ望뢗꺙", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ캁ﲇꪉ춋望뢗꺙", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿﲇꪉ望뢗꾙", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ캁ﲇꪉ춋望뢗꾙", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿﲇꪉ望뢗겙", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ캁ﲇꪉ춋望뢗겙", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿ", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ얁", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿꪉ望뢗ꮙ", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ얁ꪉ춋望뢗ꮙ", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿꪉ望뢗ꢙ", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ얁ꪉ춋望뢗ꢙ", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿꪉ望뢗ꦙ", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ얁ꪉ춋望뢗ꦙ", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿꪉ望뢗꺙", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ얁ꪉ춋望뢗꺙", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿꪉ望뢗꾙", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ얁ꪉ춋望뢗꾙", a_));
			this.ᜃ.Add(ClipboardData.b("፯ᵱᡳ᥵੷ᱹॻችꁿꪉ望뢗겙", a_), ClipboardData.b("㍯ᵱᡳ᥵੷ᱹॻችꁿ얁ꪉ춋望뢗겙", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹佻᩽ꁿﶍ낏ꎑ", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹佻㩽ꁿﶍ낏ꎑ", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹佻᩽ꁿﶍ낏ꂑ", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹佻㩽ꁿﶍ낏ꂑ", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹佻᩽ꁿﶍ낏ꆑ", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹佻㩽ꁿﶍ낏ꆑ", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύችꪉ붋", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ችꪉ붋", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύችꪉ뺋", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ችꪉ뺋", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύችꪉ뾋", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ችꪉ뾋", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύችꪉ뢋", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ችꪉ뢋", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύᅽﶇ겋뾍", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ᅽﶇ겋뾍", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύᅽﶇ겋벍", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ᅽﶇ겋벍", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύᅽﶇ겋붍", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ᅽﶇ겋붍", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύᅽﮇꪉ붋", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ᅽﮇꪉ붋", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύᅽﮇꪉ뺋", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ᅽﮇꪉ뺋", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύᅽﮇꪉ뾋", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ᅽﮇꪉ뾋", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύᅽﮇꪉ뢋", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ᅽﮇꪉ뢋", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύᅽﮇꪉ릋", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ᅽﮇꪉ릋", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ύᅽﺋ", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㽻ᅽﺋ", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᥻ችﲇ", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㥻ችﲇ", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᭻౽ꒃ랅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㭻౽ꒃ랅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᭻౽ꒃ뒅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㭻౽ꒃ뒅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᭻౽ꒃ떅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㭻౽ꒃ떅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᭻౽ꒃ늅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㭻౽ꒃ늅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᭻౽ꒃ뎅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㭻౽ꒃ뎅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᭻౽ꒃ낅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㭻౽ꒃ낅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᭻౽ꒃ놅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㭻౽ꒃ놅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹᭻౽ꒃ뺅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹㭻౽ꒃ뺅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ၻ᝽ꒃ랅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹ほ᝽ꒃ랅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ၻ᝽ꒃ뒅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹ほ᝽ꒃ뒅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ၻ᝽ꒃ떅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹ほ᝽ꒃ떅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ၻ᝽ꒃ늅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹ほ᝽ꒃ늅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ၻ᝽ꒃ뎅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹ほ᝽ꒃ뎅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ၻ᝽ꒃ낅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹ほ᝽ꒃ낅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ၻ᝽ꒃ놅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹ほ᝽ꒃ놅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ၻ᝽ꒃ뺅", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹ほ᝽ꒃ뺅", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹౻౽ﮇﺑ", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹ⱻ౽ﮇﺑ", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ཻ᝽ꢇ뮉", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹⽻᝽ꢇ뮉", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ཻ᝽ꢇ뢉", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹⽻᝽ꢇ뢉", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ཻ᝽ꢇ릉", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹⽻᝽ꢇ릉", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ཻ୽ꢇ뮉", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹⽻୽ꢇ뮉", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ཻ୽ꢇ뢉", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹⽻୽ꢇ뢉", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹ࡻᙽ", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹⡻ᙽ", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹୻᭽ꊁ떃", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹⭻᭽ꊁ떃", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹୻᭽ꊁ뚃", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹⭻᭽ꊁ뚃", a_));
			this.ᜃ.Add(ClipboardData.b("ѯ፱ᙳ᩵ᵷ婹୻᭽ꊁ랃", a_), ClipboardData.b("⑯፱ᙳ᩵ᵷ婹⭻᭽ꊁ랃", a_));
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x001E0148 File Offset: 0x001DF148
		private void ᜀ()
		{
			int a_ = 15;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜄ = new Dictionary<string, int>();
			this.ᜄ.Add(ClipboardData.b("᭴ᡶ୸ᙺᱼ፾", a_), 0);
			this.ᜄ.Add(ClipboardData.b("ᅴቶὸ᩺ࡼ፾ﾌﮒ", a_), 65);
			this.ᜄ.Add(ClipboardData.b("᭴ᡶ੸୺ᱼ᱾", a_), 157);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺᑼᅾ늂", a_), 1);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺᑼᅾ놂", a_), 2);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺᑼᅾ낂", a_), 3);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺᑼᅾ랂", a_), 4);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺᑼᅾ뚂", a_), 5);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺᑼᅾ떂", a_), 6);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺᑼᅾ뒂", a_), 7);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺᑼᅾ뮂", a_), 8);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺᑼᅾ몂", a_), 9);
			this.ᜄ.Add(ClipboardData.b("ŴṶ൸᝺᡼", a_), 62);
			this.ᜄ.Add(ClipboardData.b("ٴɶ᭸ེᑼ୾", a_), 74);
			this.ᜄ.Add(ClipboardData.b("ٴɶ᭸ེᅼ᩾ﲎ", a_), 260);
			this.ᜄ.Add(ClipboardData.b("ၴ᩶ॸ፺ᱼ౾", a_), 88);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶൸Ṻ፼౾ﺌ", a_), 261);
			this.ᜄ.Add(ClipboardData.b("ٴͶ୸ᑺ፼᡾", a_), 87);
			this.ᜄ.Add(ClipboardData.b("Ѵɶᙸེ᡼", a_), 180);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶൸Ṻ፼౾ﶈ", a_), 181);
			this.ᜄ.Add(ClipboardData.b("ٴɶ᭸ེᅼ᩾ﮈ", a_), 262);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶൸Ṻ፼౾力", a_), 263);
			this.ᜄ.Add(ClipboardData.b("᝴ᡶᙸၺॼᙾ", a_), 264);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེർṾﮊ", a_), 179);
			this.ᜄ.Add(ClipboardData.b("ᙴᙶॸེᑼၾ", a_), 34);
			this.ᜄ.Add(ClipboardData.b("᝴Ṷ᭸᝺ᑼၾ", a_), 265);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸䩺", a_), 19);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸䥺", a_), 20);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸䡺", a_), 21);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸佺", a_), 22);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸乺", a_), 23);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸䵺", a_), 24);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸䱺", a_), 25);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸䍺", a_), 26);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸䉺", a_), 27);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶ᩸፺᡼Ṿ", a_), 266);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᡾", a_), 154);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ౾", a_), 158);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ౾ﮔꢘ", a_), 172);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ౾ﮔꮘ", a_), 190);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ౾ﮔꪘ", a_), 204);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ౾ﮔ궘", a_), 218);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ౾ﮔ겘", a_), 232);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ౾ﮔ꾘", a_), 246);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ፾", a_), 159);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ፾ꊒ", a_), 173);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ፾ꆒ", a_), 191);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ፾ꂒ", a_), 205);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ፾Ꞓ", a_), 219);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ፾Ꚓ", a_), 233);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ፾ꖒ", a_), 247);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ᡾", a_), 160);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ᡾ꊒ", a_), 174);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ᡾ꆒ", a_), 192);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ᡾ꂒ", a_), 206);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ᡾Ꞓ", a_), 220);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ᡾Ꚓ", a_), 234);
			this.ᜄ.Add(ClipboardData.b("ᥴṶṸ፺ॼ᡾ꖒ", a_), 248);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뺎", a_), 161);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뺎겜", a_), 175);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뺎꾜", a_), 193);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뺎꺜", a_), 207);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뺎ꦜ", a_), 221);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뺎ꢜ", a_), 235);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뺎ꮜ", a_), 249);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ붎", a_), 162);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ붎겜", a_), 176);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ붎꾜", a_), 194);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ붎꺜", a_), 208);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ붎ꦜ", a_), 222);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ붎ꢜ", a_), 236);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ붎ꮜ", a_), 250);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈", a_), 163);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꚖ", a_), 177);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꖖ", a_), 195);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒ꒖", a_), 209);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꎖ", a_), 223);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꊖ", a_), 237);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꆖ", a_), 251);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈", a_), 164);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꚖ", a_), 182);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꖖ", a_), 196);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒ꒖", a_), 210);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꎖ", a_), 224);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꊖ", a_), 238);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꆖ", a_), 252);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈", a_), 165);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꚖ", a_), 183);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꖖ", a_), 197);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒ꒖", a_), 211);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꎖ", a_), 225);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꊖ", a_), 239);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뢈ﶒꆖ", a_), 253);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈", a_), 166);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꚖ", a_), 184);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꖖ", a_), 198);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒ꒖", a_), 212);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꎖ", a_), 226);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꊖ", a_), 240);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ뮈ﶒꆖ", a_), 254);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ몈", a_), 167);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ몈ﶒꚖ", a_), 185);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ몈ﶒꖖ", a_), 199);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ몈ﶒ꒖", a_), 213);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ몈ﶒꎖ", a_), 227);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ몈ﶒꊖ", a_), 241);
			this.ᜄ.Add(ClipboardData.b("ᡴቶᵸቺࡼቾ몈ﶒꆖ", a_), 255);
			this.ᜄ.Add(ClipboardData.b("ᅴᙶ୸ၺᅼᙾ", a_), 168);
			this.ᜄ.Add(ClipboardData.b("ᅴᙶ୸ၺᅼᙾﮎꂐ", a_), 186);
			this.ᜄ.Add(ClipboardData.b("ᅴᙶ୸ၺᅼᙾﮎꎐ", a_), 200);
			this.ᜄ.Add(ClipboardData.b("ᅴᙶ୸ၺᅼᙾﮎꊐ", a_), 214);
			this.ᜄ.Add(ClipboardData.b("ᅴᙶ୸ၺᅼᙾﮎꖐ", a_), 228);
			this.ᜄ.Add(ClipboardData.b("ᅴᙶ୸ၺᅼᙾﮎ꒐", a_), 242);
			this.ᜄ.Add(ClipboardData.b("ᅴᙶ୸ၺᅼᙾﮎꞐ", a_), 256);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾", a_), 169);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﲘ꺞膠", a_), 187);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﲘ궞", a_), 201);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﲘ겞膠", a_), 215);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﲘꮞ", a_), 229);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﲘꪞ", a_), 243);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﲘꦞ", a_), 257);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾愈ﾊ", a_), 170);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾愈ﾊﮔꢘ", a_), 188);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾愈ﾊﮔꮘ", a_), 202);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾愈ﾊﮔꪘ", a_), 216);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾愈ﾊﮔ궘", a_), 230);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾愈ﾊﮔ겘", a_), 244);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾愈ﾊﮔ꾘", a_), 258);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾", a_), 171);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﮔꢘ", a_), 189);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﮔꮘ", a_), 203);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﮔꪘ", a_), 217);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﮔ궘", a_), 231);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﮔ겘", a_), 245);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᕸᑺོ᥾ﮔ꾘", a_), 259);
			this.ᜄ.Add(ClipboardData.b("᝴ᙶᕸ᝺ቼၾﾆﶈ", a_), 153);
			this.ᜄ.Add(ClipboardData.b("᝴᭶ᙸ᡺ᙼ୾ﮂ", a_), 84);
			this.ᜄ.Add(ClipboardData.b("᝴ᡶᵸɺॼ᩾呂", a_), 66);
			this.ᜄ.Add(ClipboardData.b("᝴ᡶᵸɺॼ᩾呂랄", a_), 80);
			this.ᜄ.Add(ClipboardData.b("᝴ᡶᵸɺॼ᩾呂뚄", a_), 81);
			this.ᜄ.Add(ClipboardData.b("᝴ᡶᵸɺॼ᩾呂ﮈ歷ﾐ練", a_), 77);
			this.ᜄ.Add(ClipboardData.b("᝴ᡶᵸɺॼ᩾呂ﮈ歷ﾐ練ꦚ", a_), 78);
			this.ᜄ.Add(ClipboardData.b("᝴ᡶᵸɺॼ᩾呂ﮎ", a_), 67);
			this.ᜄ.Add(ClipboardData.b("᝴ᡶᵸɺॼ᩾呂ﮎꎐ", a_), 82);
			this.ᜄ.Add(ClipboardData.b("᝴ᡶᵸɺॼ᩾呂ﮎꊐ", a_), 83);
			this.ᜄ.Add(ClipboardData.b("ᙴ᭶ᙸࡺᑼᅾ", a_), 63);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᑸᙺ᡼ᅾ力", a_), 39);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᑸᙺ᡼ᅾﮎ", a_), 106);
			this.ᜄ.Add(ClipboardData.b("ᙴᡶᑸᙺ᡼ᅾﾆﶈ", a_), 30);
			this.ᜄ.Add(ClipboardData.b("ᅴᙶ൸Ṻ", a_), 76);
			this.ᜄ.Add(ClipboardData.b("ᅴᡶ᩸๺ၼ᩾麗", a_), 89);
			this.ᜄ.Add(ClipboardData.b("ၴ婶ᑸ᩺ᑼ፾ﾊﶎ", a_), 91);
			this.ᜄ.Add(ClipboardData.b("ၴ᥶ᵸᕺቼ୾力", a_), 42);
			this.ᜄ.Add(ClipboardData.b("ၴ᥶ᵸᕺቼ୾ﾆﶈ", a_), 43);
			this.ᜄ.Add(ClipboardData.b("ၴ᥶ླྀṺᅼၾ力ﲎ", a_), 36);
			this.ᜄ.Add(ClipboardData.b("ၴ᥶ླྀṺᅼၾﶈﺊﾌ", a_), 37);
			this.ᜄ.Add(ClipboardData.b("፴ᡶᕸ᝺ቼࡾﺆ麗ﾌﶒﺔ", a_), 86);
			this.ᜄ.Add(ClipboardData.b("፴ᡶᙸེ᡼ൾ", a_), 32);
			this.ᜄ.Add(ClipboardData.b("፴ᡶᙸེ፼ၾﾌﾐ", a_), 38);
			this.ᜄ.Add(ClipboardData.b("፴ᡶᙸེ፼ၾﾊ", a_), 29);
			this.ᜄ.Add(ClipboardData.b("ᵴቶᡸὺ᡼ൾ", a_), 31);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺ᱼ᱾ﺆ", a_), 95);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺ᱼ᭾愈", a_), 96);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺Ṽᙾ", a_), 97);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺Ṽၾ", a_), 98);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺᥼᩾ﶈ", a_), 99);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺ᙼ᩾ﮈ", a_), 100);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺ർൾ歷ﮎ", a_), 101);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺๼Ṿ", a_), 102);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺ॼپﾊﶎ", a_), 103);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺୼Ṿ", a_), 104);
			this.ᜄ.Add(ClipboardData.b("ᵴ๶ॸṺོ፾", a_), 85);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ乾", a_), 10);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ䵾", a_), 11);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ䱾", a_), 12);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ䭾", a_), 13);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ䩾", a_), 14);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ䥾", a_), 15);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ䡾", a_), 16);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ䝾", a_), 17);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ䙾", a_), 18);
			this.ᜄ.Add(ClipboardData.b("ᱴ᥶ᵸṺռ᝾", a_), 33);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ᝸Ṻ፼੾", a_), 40);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ", a_), 47);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ佼", a_), 50);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ乼", a_), 51);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ䥼", a_), 52);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ䡼", a_), 53);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེὼ੾", a_), 48);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེὼ੾뮈", a_), 54);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེὼ੾몈", a_), 55);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེὼ੾불", a_), 56);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེὼ੾번", a_), 57);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེṼၾﲈ", a_), 68);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེṼၾﲈ뾌", a_), 69);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེṼၾﲈ뺌", a_), 70);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེṼၾﲈ릌", a_), 71);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེṼၾﲈ뢌", a_), 72);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ፼੾", a_), 49);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ፼੾뮈", a_), 58);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ፼੾몈", a_), 59);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ፼੾불", a_), 60);
			this.ᜄ.Add(ClipboardData.b("ᥴṶ੸ེ፼੾번", a_), 61);
			this.ᜄ.Add(ClipboardData.b("ᡴᙶ᩸ॺቼ୾ﮂ", a_), 45);
			this.ᜄ.Add(ClipboardData.b("ᡴቶ੸ࡺᱼ᡾ﾌ", a_), 73);
			this.ᜄ.Add(ClipboardData.b("᭴ᡶᕸቺ๼୾", a_), 107);
			this.ᜄ.Add(ClipboardData.b("᭴ᡶ୸ᙺᱼ፾ꦀꂈ", a_), 94);
			this.ᜄ.Add(ClipboardData.b("᭴ᡶ୸ᙺᱼ፾ﾊ", a_), 28);
			this.ᜄ.Add(ClipboardData.b("᭴ᡶ൸Ṻᕼ᩾", a_), 79);
			this.ᜄ.Add(ClipboardData.b("մᙶṸṺ፼੾", a_), 41);
			this.ᜄ.Add(ClipboardData.b("մ᭶ᡸ᡺᡼᝾ﮈﾊ", a_), 156);
			this.ᜄ.Add(ClipboardData.b("մ᭶ᡸቺ፼୾ﮂ", a_), 90);
			this.ᜄ.Add(ClipboardData.b("ٴᙶᕸ๺ॼṾ", a_), 75);
			this.ᜄ.Add(ClipboardData.b("ٴṶṸᕺᱼ୾", a_), 64);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼䱾歷ﲎꂐ", a_), 142);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼䱾歷ﲎꎐ", a_), 143);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼䱾歷ﲎꊐ", a_), 144);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾벌", a_), 114);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾뾌", a_), 115);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾뺌", a_), 116);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾릌", a_), 117);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾ﺊ뺎", a_), 118);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾ﺊ붎", a_), 119);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾ﺊ벎", a_), 120);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾벌", a_), 121);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾뾌", a_), 122);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾뺌", a_), 123);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾릌", a_), 124);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾뢌", a_), 125);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᱾ﮊﶎ", a_), 145);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᩾ﾊ", a_), 146);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᡾뚆", a_), 126);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᡾떆", a_), 127);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᡾뒆", a_), 128);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᡾뎆", a_), 129);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᡾늆", a_), 130);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᡾놆", a_), 131);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᡾낆", a_), 132);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼᡾뾆", a_), 133);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼፾뚆", a_), 134);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼፾떆", a_), 135);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼፾뒆", a_), 136);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼፾뎆", a_), 137);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼፾늆", a_), 138);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼፾놆", a_), 139);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼፾낆", a_), 140);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼፾뾆", a_), 141);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼ᅾ", a_), 105);
			this.ᜄ.Add(ClipboardData.b("᭴ᡶ୸ᙺᱼ፾", a_), 105);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼ၾﾌ朗", a_), 44);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼ၾﲈ力ﲎ", a_), 35);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼ཾ愈ﾐ璉", a_), 147);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼౾몊", a_), 111);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼౾릊", a_), 112);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼౾뢊", a_), 113);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼౾몊", a_), 148);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼౾릊", a_), 149);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼୾", a_), 155);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼ࡾ뒄", a_), 150);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼ࡾ랄", a_), 151);
			this.ᜄ.Add(ClipboardData.b("Ŵᙶ᭸᝺᡼ࡾ뚄", a_), 152);
			this.ᜄ.Add(ClipboardData.b("Ŵᡶᡸ፺᡼Ṿ", a_), 46);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺ॼၾ力", a_), 92);
			this.ᜄ.Add(ClipboardData.b("ᵴͶᑸ᝺ὼၾﺒ", a_), 93);
			this.ᜄ.Add(ClipboardData.b("ݴቶླྀቺ๼ᙾ", a_), 178);
			this.ᜄ.Add(ClipboardData.b("ᩴɶ൸᝺ᑼᅾﶈ몊", a_), 108);
			this.ᜄ.Add(ClipboardData.b("ᩴɶ൸᝺ᑼᅾﶈ릊", a_), 109);
			this.ᜄ.Add(ClipboardData.b("ᩴɶ൸᝺ᑼᅾﶈ뢊", a_), 110);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x001E1F74 File Offset: 0x001E0F74
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 14;
			if (true)
			{
			}
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			writer.WriteValue(ClipboardData.b("㩳᝵ᕷό", a_), this.Name);
			writer.WriteValue(ClipboardData.b("❳ɵŷᙹ᥻㝽", a_), this.ᜀ);
			writer.WriteValue(ClipboardData.b("sཱུࡷό", a_), this.StyleType);
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x001E200C File Offset: 0x001E100C
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 14;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜁ = reader.ReadString(ClipboardData.b("㩳᝵ᕷό", a_));
			this.ᜀ = reader.ReadInt(ClipboardData.b("❳ɵŷᙹ᥻㝽", a_));
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x001E2084 File Offset: 0x001E1084
		protected override void InitXDLSHolder()
		{
			int a_ = 17;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			base.XDLSHolder.EnableID = true;
			base.XDLSHolder.AddRefElement(ClipboardData.b("ᕶᡸࡺ᡼", a_), this.m_baseStyle);
			base.XDLSHolder.AddElement(ClipboardData.b("ᑶᅸོ᩺Ṿꒈﶎﲐ", a_), this.m_chFormat);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x001E2114 File Offset: 0x001E1114
		protected override void RestoreReference(string name, int index)
		{
			int num = 1;
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
						goto IL_42;
					}
					if (false)
					{
					}
					break;
				case 2:
					this.m_baseStyle = base.Document.Styles[index];
					num = 0;
					continue;
				}
				IL_42:
				if (index <= -1)
				{
					return;
				}
				num = 2;
			}
			IL_6F:
			if (true)
			{
			}
		}

		// Token: 0x04001FA0 RID: 8096
		protected const int DEF_USER_STYLE_ID = 4094;

		// Token: 0x04001FA1 RID: 8097
		private new int ᜀ;

		// Token: 0x04001FA2 RID: 8098
		private string ᜁ;

		// Token: 0x04001FA3 RID: 8099
		protected IStyle m_baseStyle;

		// Token: 0x04001FA4 RID: 8100
		protected CharacterFormat m_chFormat;

		// Token: 0x04001FA5 RID: 8101
		protected string m_nextStyle;

		// Token: 0x04001FA6 RID: 8102
		protected string m_linkStyle;

		// Token: 0x04001FA7 RID: 8103
		protected bool m_isPrimaryStyle;

		// Token: 0x04001FA8 RID: 8104
		protected bool m_isSemiHidden;

		// Token: 0x04001FA9 RID: 8105
		protected bool m_unhideWhenUsed;

		// Token: 0x04001FAA RID: 8106
		protected bool m_isCustom;

		// Token: 0x04001FAB RID: 8107
		internal WordStyleType ᜂ;

		// Token: 0x04001FAC RID: 8108
		protected byte[] m_tapx;

		// Token: 0x04001FAD RID: 8109
		private Dictionary<string, string> ᜃ;

		// Token: 0x04001FAE RID: 8110
		private Dictionary<string, int> ᜄ;

		// Token: 0x02000495 RID: 1173
		public class BuiltinStyleLoader
		{
			// Token: 0x0600401A RID: 16410 RVA: 0x003B108C File Offset: 0x003B008C
			internal static void ᜀ(IStyle A_0, BuiltinStyle A_1)
			{
				int a_ = 1;
				switch (0)
				{
				default:
				{
					XmlReader xmlReader;
					for (;;)
					{
						Style.BuiltinStyleLoader.ᜀ();
						Style.BuiltinStyleLoader.ᜅ.Position = 0L;
						xmlReader = new XmlTextReader(Style.BuiltinStyleLoader.ᜅ);
						int num = 10;
						for (;;)
						{
							switch (num)
							{
							case 0:
								goto IL_14B;
							case 1:
								if (!(xmlReader.Name != ClipboardData.b("զᱨɪŬ᭮ᡰᵲ塴Ѷ൸ɺᅼ᩾", a_)))
								{
									num = 4;
									continue;
								}
								xmlReader.Read();
								num = 3;
								continue;
							case 2:
								goto IL_D7;
							case 3:
								goto IL_D9;
							case 4:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_8A;
								default:
								{
									if (false)
									{
									}
									xmlReader.Read();
									string b = Style.ᜁ(A_1);
									string a = string.Empty;
									num = 0;
									continue;
								}
								}
								break;
							case 5:
								if (xmlReader.NodeType == XmlNodeType.Element)
								{
									num = 11;
									continue;
								}
								xmlReader.Read();
								num = 6;
								continue;
							case 6:
								goto IL_14B;
							case 7:
							{
								if (true)
								{
								}
								string b;
								string a;
								if (a == b)
								{
									num = 2;
									continue;
								}
								xmlReader.Skip();
								num = 12;
								continue;
							}
							case 8:
								return;
							case 9:
								if (xmlReader.EOF)
								{
									num = 8;
									continue;
								}
								num = 5;
								continue;
							case 10:
								goto IL_D9;
							case 11:
							{
								string a = xmlReader.GetAttribute(ClipboardData.b("⥦ࡨ٪࡬", a_));
								num = 7;
								continue;
							}
							case 12:
								goto IL_8A;
							}
							break;
							IL_D9:
							num = 1;
							continue;
							IL_14B:
							num = 9;
							continue;
							IL_8A:
							goto IL_14B;
						}
					}
					IL_D7:
					XDLSReader xdlsreader = new XDLSReader(xmlReader);
					xdlsreader.ReadChildElement(A_0);
					return;
				}
				}
			}

			// Token: 0x0600401B RID: 16411 RVA: 0x003B1270 File Offset: 0x003B0270
			private static void ᜀ()
			{
				int a_ = 17;
				int num = 5;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (Style.BuiltinStyleLoader.ᜅ == null)
						{
							num = 1;
							continue;
						}
						goto IL_49;
					case 1:
					{
						Assembly executingAssembly = Assembly.GetExecutingAssembly();
						executingAssembly.GetManifestResourceNames();
						Style.BuiltinStyleLoader.ᜅ = executingAssembly.GetManifestResourceStream(ClipboardData.b("⑶ॸቺོ᩾꾀잂ꞈ\ud98aﲎﺐﲘ뎜ﶞ풠쪢즤펦삨얪肬\udcae얰쪲\ud9b4튶쪸閺얼튾귀", a_));
						num = 2;
						continue;
					}
					case 2:
						goto IL_49;
					case 3:
						goto IL_8E;
					case 4:
						if (Style.BuiltinStyleLoader.ᜅ == null)
						{
							num = 3;
							continue;
						}
						return;
					case 6:
						return;
					}
					if (Style.BuiltinStyleLoader.ᜅ != null)
					{
						num = 6;
						continue;
					}
					num = 0;
					continue;
					IL_49:
					if (true)
					{
					}
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_8E;
					default:
						if (false)
						{
						}
						num = 4;
						break;
					}
				}
				return;
				IL_8E:
				throw new Exception(ClipboardData.b("╶ᱸࡺቼ੾Ꞇ놐ﺖ負킢톤\udea6얨캪\udeac膮즰\udeb2\ud9b4鞶ힸ풺즼龾Ꟁ곂냄꧆귈", a_));
			}

			// Token: 0x0600401C RID: 16412 RVA: 0x003B1378 File Offset: 0x003B0378
			internal static bool ᜀ(BuiltinStyle A_0)
			{
				bool result;
				for (;;)
				{
					result = false;
					int num = 0;
					int num2 = 6;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_A4;
						case 1:
							return result;
						case 2:
							if (num >= 10)
							{
								num2 = 3;
								continue;
							}
							num2 = 4;
							continue;
						case 3:
							return result;
						case 4:
							if (A_0.ToString() == ((DefaultListStyle)num).ToString())
							{
								num2 = 5;
								continue;
							}
							num++;
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_68;
							default:
								if (false)
								{
								}
								num2 = 0;
								continue;
							}
							break;
						case 5:
							result = true;
							goto IL_68;
						case 6:
							goto IL_A4;
						}
						break;
						IL_68:
						num2 = 1;
						continue;
						IL_A4:
						if (true)
						{
						}
						num2 = 2;
					}
				}
				return result;
			}

			// Token: 0x0600401D RID: 16413 RVA: 0x003B1450 File Offset: 0x003B0450
			internal static void ᜀ(IStyle A_0, DefaultTableStyle A_1)
			{
				A_0.Name = Style.ᜀ(A_1);
				switch (A_1)
				{
				case DefaultTableStyle.TableNormal:
					Style.BuiltinStyleLoader.ᜬ(A_0);
					return;
				case DefaultTableStyle.TableGrid:
					Style.BuiltinStyleLoader.ᜫ(A_0);
					return;
				case DefaultTableStyle.LightShading:
					Style.BuiltinStyleLoader.ᜄ(A_0, Color.Black, Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 192, 192, 192));
					return;
				case DefaultTableStyle.LightShadingAccent1:
					Style.BuiltinStyleLoader.ᜄ(A_0, Color.FromArgb(255, 54, 95, 145), Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 211, 223, 238));
					return;
				case DefaultTableStyle.LightShadingAccent2:
					Style.BuiltinStyleLoader.ᜄ(A_0, Color.FromArgb(255, 148, 54, 52), Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 239, 211, 210));
					return;
				case DefaultTableStyle.LightShadingAccent3:
					Style.BuiltinStyleLoader.ᜄ(A_0, Color.FromArgb(255, 118, 146, 60), Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 230, 238, 213));
					return;
				case DefaultTableStyle.LightShadingAccent4:
					Style.BuiltinStyleLoader.ᜄ(A_0, Color.FromArgb(255, 95, 73, 122), Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 223, 216, 232));
					return;
				case DefaultTableStyle.LightShadingAccent5:
					Style.BuiltinStyleLoader.ᜄ(A_0, Color.FromArgb(255, 49, 132, 155), Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 210, 234, 241));
					return;
				case DefaultTableStyle.LightShadingAccent6:
					Style.BuiltinStyleLoader.ᜄ(A_0, Color.FromArgb(255, 227, 108, 10), Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 253, 228, 208));
					return;
				case DefaultTableStyle.LightList:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 0, 0, 0), Color.Black);
					return;
				case DefaultTableStyle.LightListAccent1:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 79, 129, 189));
					return;
				case DefaultTableStyle.LightListAccent2:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 192, 80, 77));
					return;
				case DefaultTableStyle.LightListAccent3:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 155, 187, 89));
					return;
				case DefaultTableStyle.LightListAccent4:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 128, 100, 162));
					return;
				case DefaultTableStyle.LightListAccent5:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 75, 172, 198));
					return;
				case DefaultTableStyle.LightListAccent6:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 247, 150, 70));
					return;
				case DefaultTableStyle.LightGrid:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 192, 192, 192));
					return;
				case DefaultTableStyle.LightGridAccent1:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 211, 223, 238));
					return;
				case DefaultTableStyle.LightGridAccent2:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 239, 211, 210));
					return;
				case DefaultTableStyle.LightGridAccent3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 230, 238, 213));
						return;
					}
					break;
				case DefaultTableStyle.LightGridAccent4:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 223, 216, 232));
					return;
				case DefaultTableStyle.LightGridAccent5:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 210, 234, 241));
					return;
				case DefaultTableStyle.LightGridAccent6:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 253, 228, 208));
					return;
				case DefaultTableStyle.MediumShading1:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 64, 64, 64), Color.Black, Color.FromArgb(255, 192, 192, 192));
					return;
				case DefaultTableStyle.MediumShading1Accent1:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 123, 160, 205), Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 211, 223, 238));
					return;
				case DefaultTableStyle.MediumShading1Accent2:
					break;
				case DefaultTableStyle.MediumShading1Accent3:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 179, 204, 130), Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 230, 238, 213));
					return;
				case DefaultTableStyle.MediumShading1Accent4:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 159, 138, 185), Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 223, 216, 232));
					return;
				case DefaultTableStyle.MediumShading1Accent5:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 120, 192, 212), Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 210, 234, 241));
					return;
				case DefaultTableStyle.MediumShading1Accent6:
					Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 249, 176, 116), Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 253, 228, 208));
					return;
				case DefaultTableStyle.MediumShading2:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.Black);
					return;
				case DefaultTableStyle.MediumShading2Accent1:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 79, 129, 189));
					return;
				case DefaultTableStyle.MediumShading2Accent2:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 192, 80, 77));
					return;
				case DefaultTableStyle.MediumShading2Accent3:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 155, 187, 89));
					return;
				case DefaultTableStyle.MediumShading2Accent4:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 128, 100, 162));
					return;
				case DefaultTableStyle.MediumShading2Accent5:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 75, 172, 198));
					return;
				case DefaultTableStyle.MediumShading2Accent6:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 247, 150, 70));
					return;
				case DefaultTableStyle.MediumList1:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 192, 192, 192));
					return;
				case DefaultTableStyle.MediumList1Accent1:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 211, 223, 238));
					return;
				case DefaultTableStyle.MediumList1Accent2:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 239, 211, 210));
					return;
				case DefaultTableStyle.MediumList1Accent3:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 230, 238, 213));
					return;
				case DefaultTableStyle.MediumList1Accent4:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 223, 216, 232));
					return;
				case DefaultTableStyle.MediumList1Accent5:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 210, 234, 241));
					return;
				case DefaultTableStyle.MediumList1Accent6:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 253, 228, 208));
					return;
				case DefaultTableStyle.MediumList2:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 192, 192, 192));
					return;
				case DefaultTableStyle.MediumList2Accent1:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 211, 223, 238));
					return;
				case DefaultTableStyle.MediumList2Accent2:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 239, 211, 210));
					return;
				case DefaultTableStyle.MediumList2Accent3:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 230, 238, 213));
					return;
				case DefaultTableStyle.MediumList2Accent4:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 223, 216, 232));
					return;
				case DefaultTableStyle.MediumList2Accent5:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 210, 234, 241));
					return;
				case DefaultTableStyle.MediumList2Accent6:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 253, 228, 208));
					return;
				case DefaultTableStyle.MediumGrid1:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 64, 64, 64), Color.FromArgb(255, 192, 192, 192), Color.FromArgb(255, 128, 128, 128));
					return;
				case DefaultTableStyle.MediumGrid1Accent1:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 123, 160, 205), Color.FromArgb(255, 211, 223, 238), Color.FromArgb(255, 167, 191, 222));
					return;
				case DefaultTableStyle.MediumGrid1Accent2:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 207, 123, 121), Color.FromArgb(255, 239, 211, 210), Color.FromArgb(255, 223, 167, 166));
					return;
				case DefaultTableStyle.MediumGrid1Accent3:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 179, 204, 130), Color.FromArgb(255, 230, 238, 213), Color.FromArgb(255, 205, 221, 172));
					return;
				case DefaultTableStyle.MediumGrid1Accent4:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 159, 138, 185), Color.FromArgb(255, 223, 216, 232), Color.FromArgb(255, 191, 177, 208));
					return;
				case DefaultTableStyle.MediumGrid1Accent5:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 120, 192, 212), Color.FromArgb(255, 210, 234, 241), Color.FromArgb(255, 165, 213, 226));
					return;
				case DefaultTableStyle.MediumGrid1Accent6:
					Style.BuiltinStyleLoader.ᜂ(A_0, Color.FromArgb(255, 249, 176, 116), Color.FromArgb(255, 253, 228, 208), Color.FromArgb(255, 251, 202, 162));
					return;
				case DefaultTableStyle.MediumGrid2:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 192, 192, 192), Color.FromArgb(255, 230, 230, 230), Color.FromArgb(255, 204, 204, 204), Color.FromArgb(255, 128, 128, 128));
					return;
				case DefaultTableStyle.MediumGrid2Accent1:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 211, 223, 238), Color.FromArgb(255, 237, 242, 248), Color.FromArgb(255, 219, 229, 241), Color.FromArgb(255, 167, 191, 222));
					return;
				case DefaultTableStyle.MediumGrid2Accent2:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 239, 211, 210), Color.FromArgb(255, 248, 237, 237), Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 223, 167, 166));
					return;
				case DefaultTableStyle.MediumGrid2Accent3:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 230, 238, 213), Color.FromArgb(255, 245, 248, 238), Color.FromArgb(255, 234, 241, 221), Color.FromArgb(255, 205, 221, 172));
					return;
				case DefaultTableStyle.MediumGrid2Accent4:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 223, 216, 232), Color.FromArgb(255, 242, 239, 246), Color.FromArgb(255, 229, 223, 236), Color.FromArgb(255, 191, 177, 208));
					return;
				case DefaultTableStyle.MediumGrid2Accent5:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 210, 234, 241), Color.FromArgb(255, 237, 246, 249), Color.FromArgb(255, 218, 238, 243), Color.FromArgb(255, 165, 213, 226));
					return;
				case DefaultTableStyle.MediumGrid2Accent6:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 253, 228, 208), Color.FromArgb(255, 254, 244, 236), Color.FromArgb(255, 253, 233, 217), Color.FromArgb(255, 251, 202, 162));
					return;
				case DefaultTableStyle.MediumGrid3:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 192, 192, 192), Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 128, 128, 128));
					return;
				case DefaultTableStyle.MediumGrid3Accent1:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 211, 223, 238), Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 167, 191, 222));
					return;
				case DefaultTableStyle.MediumGrid3Accent2:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 239, 211, 210), Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 223, 167, 166));
					return;
				case DefaultTableStyle.MediumGrid3Accent3:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 230, 238, 213), Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 205, 221, 172));
					return;
				case DefaultTableStyle.MediumGrid3Accent4:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 223, 216, 232), Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 191, 177, 208));
					return;
				case DefaultTableStyle.MediumGrid3Accent5:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 210, 234, 241), Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 165, 213, 226));
					return;
				case DefaultTableStyle.MediumGrid3Accent6:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 253, 228, 208), Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 251, 202, 162));
					return;
				case DefaultTableStyle.DarkList:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 0, 0, 0));
					return;
				case DefaultTableStyle.DarkListAccent1:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 36, 63, 96), Color.FromArgb(255, 54, 95, 145));
					return;
				case DefaultTableStyle.DarkListAccent2:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 98, 36, 35), Color.FromArgb(255, 148, 54, 52));
					return;
				case DefaultTableStyle.DarkListAccent3:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 78, 97, 40), Color.FromArgb(255, 118, 146, 60));
					return;
				case DefaultTableStyle.DarkListAccent4:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 63, 49, 81), Color.FromArgb(255, 95, 73, 122));
					return;
				case DefaultTableStyle.DarkListAccent5:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 32, 88, 103), Color.FromArgb(255, 49, 132, 155));
					return;
				case DefaultTableStyle.DarkListAccent6:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 151, 71, 6), Color.FromArgb(255, 227, 108, 10));
					return;
				case DefaultTableStyle.ColorfulShading:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 230, 230, 230), Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 153, 153, 153), Color.FromArgb(255, 128, 128, 128));
					return;
				case DefaultTableStyle.ColorfulShadingAccent1:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 79, 129, 189), Color.FromArgb(255, 237, 242, 248), Color.FromArgb(255, 44, 76, 116), Color.FromArgb(255, 184, 204, 228), Color.FromArgb(255, 167, 191, 222));
					return;
				case DefaultTableStyle.ColorfulShadingAccent2:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 248, 237, 237), Color.FromArgb(255, 119, 44, 42), Color.FromArgb(255, 229, 184, 183), Color.FromArgb(255, 223, 167, 166));
					return;
				case DefaultTableStyle.ColorfulShadingAccent3:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 245, 248, 238), Color.FromArgb(255, 94, 117, 48), Color.FromArgb(255, 214, 227, 188), Color.FromArgb(255, 205, 221, 172));
					return;
				case DefaultTableStyle.ColorfulShadingAccent4:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 155, 187, 89), Color.FromArgb(255, 128, 100, 162), Color.FromArgb(255, 242, 239, 246), Color.FromArgb(255, 76, 59, 98), Color.FromArgb(255, 204, 192, 217), Color.FromArgb(255, 191, 177, 208));
					return;
				case DefaultTableStyle.ColorfulShadingAccent5:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 237, 246, 249), Color.FromArgb(255, 39, 106, 124), Color.FromArgb(255, 182, 221, 232), Color.FromArgb(255, 165, 213, 226));
					return;
				case DefaultTableStyle.ColorfulShadingAccent6:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 75, 172, 198), Color.FromArgb(255, 247, 150, 70), Color.FromArgb(255, 254, 244, 236), Color.FromArgb(255, 182, 86, 8), Color.FromArgb(255, 251, 212, 180), Color.FromArgb(255, 251, 202, 162));
					return;
				case DefaultTableStyle.ColorfulList:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 230, 230, 230), Color.FromArgb(255, 158, 58, 56), Color.FromArgb(255, 192, 192, 192), Color.FromArgb(255, 204, 204, 204));
					return;
				case DefaultTableStyle.ColorfulListAccent1:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 237, 242, 248), Color.FromArgb(255, 158, 58, 56), Color.FromArgb(255, 211, 223, 238), Color.FromArgb(255, 219, 229, 241));
					return;
				case DefaultTableStyle.ColorfulListAccent2:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 248, 237, 237), Color.FromArgb(255, 158, 58, 56), Color.FromArgb(255, 239, 211, 210), Color.FromArgb(255, 242, 219, 219));
					return;
				case DefaultTableStyle.ColorfulListAccent3:
					if (true)
					{
					}
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 245, 248, 238), Color.FromArgb(255, 102, 78, 130), Color.FromArgb(255, 230, 238, 213), Color.FromArgb(255, 234, 241, 221));
					return;
				case DefaultTableStyle.ColorfulListAccent4:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 242, 239, 246), Color.FromArgb(255, 126, 156, 64), Color.FromArgb(255, 223, 216, 232), Color.FromArgb(255, 229, 223, 236));
					return;
				case DefaultTableStyle.ColorfulListAccent5:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 237, 246, 249), Color.FromArgb(255, 242, 115, 10), Color.FromArgb(255, 210, 234, 241), Color.FromArgb(255, 218, 238, 243));
					return;
				case DefaultTableStyle.ColorfulListAccent6:
					Style.BuiltinStyleLoader.ᜁ(A_0, Color.FromArgb(255, 254, 244, 236), Color.FromArgb(255, 52, 141, 165), Color.FromArgb(255, 253, 228, 208), Color.FromArgb(255, 253, 233, 217));
					return;
				case DefaultTableStyle.ColorfulGrid:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 204, 204, 204), Color.FromArgb(255, 153, 153, 153), Color.FromArgb(255, 0, 0, 0), Color.FromArgb(255, 128, 128, 128));
					return;
				case DefaultTableStyle.ColorfulGridAccent1:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 219, 229, 241), Color.FromArgb(255, 184, 204, 228), Color.FromArgb(255, 54, 95, 145), Color.FromArgb(255, 167, 191, 222));
					return;
				case DefaultTableStyle.ColorfulGridAccent2:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 242, 219, 219), Color.FromArgb(255, 229, 184, 183), Color.FromArgb(255, 148, 54, 52), Color.FromArgb(255, 223, 167, 166));
					return;
				case DefaultTableStyle.ColorfulGridAccent3:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 234, 241, 221), Color.FromArgb(255, 214, 227, 188), Color.FromArgb(255, 118, 146, 60), Color.FromArgb(255, 205, 221, 172));
					return;
				case DefaultTableStyle.ColorfulGridAccent4:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 229, 223, 236), Color.FromArgb(255, 204, 192, 217), Color.FromArgb(255, 95, 73, 122), Color.FromArgb(255, 191, 177, 208));
					return;
				case DefaultTableStyle.ColorfulGridAccent5:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 218, 238, 243), Color.FromArgb(255, 182, 221, 232), Color.FromArgb(255, 49, 132, 155), Color.FromArgb(255, 165, 213, 226));
					return;
				case DefaultTableStyle.ColorfulGridAccent6:
					Style.BuiltinStyleLoader.ᜀ(A_0, Color.FromArgb(255, 253, 233, 217), Color.FromArgb(255, 251, 212, 180), Color.FromArgb(255, 227, 108, 10), Color.FromArgb(255, 251, 202, 162));
					return;
				case DefaultTableStyle.Table3Deffects1:
					Style.BuiltinStyleLoader.ᜪ(A_0);
					return;
				case DefaultTableStyle.Table3Deffects2:
					Style.BuiltinStyleLoader.ᜩ(A_0);
					return;
				case DefaultTableStyle.Table3Deffects3:
					Style.BuiltinStyleLoader.ᜨ(A_0);
					return;
				case DefaultTableStyle.TableClassic1:
					Style.BuiltinStyleLoader.ᜧ(A_0);
					return;
				case DefaultTableStyle.TableClassic2:
					Style.BuiltinStyleLoader.ᜦ(A_0);
					return;
				case DefaultTableStyle.TableClassic3:
					Style.BuiltinStyleLoader.ᜥ(A_0);
					return;
				case DefaultTableStyle.TableClassic4:
					Style.BuiltinStyleLoader.ᜤ(A_0);
					return;
				case DefaultTableStyle.TableColorful1:
					Style.BuiltinStyleLoader.ᜣ(A_0);
					return;
				case DefaultTableStyle.TableColorful2:
					Style.BuiltinStyleLoader.ᜢ(A_0);
					return;
				case DefaultTableStyle.TableColorful3:
					Style.BuiltinStyleLoader.ᜡ(A_0);
					return;
				case DefaultTableStyle.TableColumns1:
					Style.BuiltinStyleLoader.ᜠ(A_0);
					return;
				case DefaultTableStyle.TableColumns2:
					Style.BuiltinStyleLoader.\u171F(A_0);
					return;
				case DefaultTableStyle.TableColumns3:
					Style.BuiltinStyleLoader.\u171E(A_0);
					return;
				case DefaultTableStyle.TableColumns4:
					Style.BuiltinStyleLoader.\u171D(A_0);
					return;
				case DefaultTableStyle.TableColumns5:
					Style.BuiltinStyleLoader.\u171C(A_0);
					return;
				case DefaultTableStyle.TableContemporary:
					Style.BuiltinStyleLoader.\u171B(A_0);
					return;
				case DefaultTableStyle.TableElegant:
					Style.BuiltinStyleLoader.\u171A(A_0);
					return;
				case DefaultTableStyle.TableGrid1:
					Style.BuiltinStyleLoader.\u1719(A_0);
					return;
				case DefaultTableStyle.TableGrid2:
					Style.BuiltinStyleLoader.\u1718(A_0);
					return;
				case DefaultTableStyle.TableGrid3:
					Style.BuiltinStyleLoader.\u1717(A_0);
					return;
				case DefaultTableStyle.TableGrid4:
					Style.BuiltinStyleLoader.\u1716(A_0);
					return;
				case DefaultTableStyle.TableGrid5:
					Style.BuiltinStyleLoader.\u1715(A_0);
					return;
				case DefaultTableStyle.TableGrid6:
					Style.BuiltinStyleLoader.\u1714(A_0);
					return;
				case DefaultTableStyle.TableGrid7:
					Style.BuiltinStyleLoader.\u1713(A_0);
					return;
				case DefaultTableStyle.TableGrid8:
					Style.BuiltinStyleLoader.\u1712(A_0);
					return;
				case DefaultTableStyle.TableList1:
					Style.BuiltinStyleLoader.ᜑ(A_0);
					return;
				case DefaultTableStyle.TableList2:
					Style.BuiltinStyleLoader.ᜐ(A_0);
					return;
				case DefaultTableStyle.TableList3:
					Style.BuiltinStyleLoader.ᜏ(A_0);
					return;
				case DefaultTableStyle.TableList4:
					Style.BuiltinStyleLoader.ᜎ(A_0);
					return;
				case DefaultTableStyle.TableList5:
					Style.BuiltinStyleLoader.\u170D(A_0);
					return;
				case DefaultTableStyle.TableList6:
					Style.BuiltinStyleLoader.ᜌ(A_0);
					return;
				case DefaultTableStyle.TableList7:
					Style.BuiltinStyleLoader.ᜋ(A_0);
					return;
				case DefaultTableStyle.TableList8:
					Style.BuiltinStyleLoader.ᜊ(A_0);
					return;
				case DefaultTableStyle.TableProfessional:
					Style.BuiltinStyleLoader.ᜉ(A_0);
					return;
				case DefaultTableStyle.TableSimple1:
					Style.BuiltinStyleLoader.ᜈ(A_0);
					return;
				case DefaultTableStyle.TableSimple2:
					Style.BuiltinStyleLoader.ᜇ(A_0);
					return;
				case DefaultTableStyle.TableSimple3:
					Style.BuiltinStyleLoader.ᜆ(A_0);
					return;
				case DefaultTableStyle.TableSubtle1:
					Style.BuiltinStyleLoader.ᜅ(A_0);
					return;
				case DefaultTableStyle.TableSubtle2:
					Style.BuiltinStyleLoader.ᜄ(A_0);
					return;
				case DefaultTableStyle.TableTheme:
					Style.BuiltinStyleLoader.ᜃ(A_0);
					return;
				case DefaultTableStyle.TableWeb1:
					Style.BuiltinStyleLoader.ᜂ(A_0);
					return;
				case DefaultTableStyle.TableWeb2:
					Style.BuiltinStyleLoader.ᜁ(A_0);
					return;
				case DefaultTableStyle.TableWeb3:
					Style.BuiltinStyleLoader.ᜀ(A_0);
					return;
				default:
					return;
				}
				Style.BuiltinStyleLoader.ᜃ(A_0, Color.FromArgb(255, 207, 123, 121), Color.FromArgb(255, 192, 80, 77), Color.FromArgb(255, 239, 211, 210));
			}

			// Token: 0x0600401E RID: 16414 RVA: 0x003B3500 File Offset: 0x003B2500
			private static void ᜬ(IStyle A_0)
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
				(A_0 as Style).IsSemiHidden = true;
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
			}

			// Token: 0x0600401F RID: 16415 RVA: 0x003B35D0 File Offset: 0x003B25D0
			private static void ᜫ(IStyle A_0)
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
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
			}

			// Token: 0x06004020 RID: 16416 RVA: 0x003B3994 File Offset: 0x003B2994
			private static void ᜄ(IStyle A_0, Color A_1, Color A_2, Color A_3)
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
				(A_0 as spr\u173A).CharacterFormat.TextColor = A_1;
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = A_2;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = A_2;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜀ().BeforeSpacing = 0f;
				sprῊ.ᜀ().AfterSpacing = 0f;
				sprῊ.ᜀ().LineSpacing = 12f;
				sprῊ.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Top.Color = A_2;
				sprῊ.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = A_2;
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜀ().BeforeSpacing = 0f;
				sprῊ2.ᜀ().AfterSpacing = 0f;
				sprῊ2.ᜀ().LineSpacing = 12f;
				sprῊ2.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Top.Color = A_2;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Bottom.Color = A_2;
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ(A_3);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ(A_3);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
			}

			// Token: 0x06004021 RID: 16417 RVA: 0x003B4128 File Offset: 0x003B3128
			private static void ᜃ(IStyle A_0, Color A_1, Color A_2)
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
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜀ().BeforeSpacing = 0f;
				sprῊ.ᜀ().AfterSpacing = 0f;
				sprῊ.ᜀ().LineSpacing = 12f;
				sprῊ.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ(A_2);
				sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜀ().BeforeSpacing = 0f;
				sprῊ2.ᜀ().AfterSpacing = 0f;
				sprῊ2.ᜀ().LineSpacing = 12f;
				sprῊ2.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Double;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ5.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ5.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ5.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ5.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Right.Space = 0f;
			}

			// Token: 0x06004022 RID: 16418 RVA: 0x003B4AF0 File Offset: 0x003B3AF0
			private static void ᜂ(IStyle A_0, Color A_1, Color A_2)
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
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜀ().BeforeSpacing = 0f;
				sprῊ.ᜀ().AfterSpacing = 0f;
				sprῊ.ᜀ().LineSpacing = 12f;
				sprῊ.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 2.25f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Vertical.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Vertical.Color = A_1;
				sprῊ.ᜈ().ᜁ().Vertical.Space = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜀ().BeforeSpacing = 0f;
				sprῊ2.ᜀ().AfterSpacing = 0f;
				sprῊ2.ᜀ().LineSpacing = 12f;
				sprῊ2.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Double;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Vertical.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Vertical.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Vertical.Space = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ4.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ4.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ4.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ4.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ4.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ4.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ4.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ4.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ5.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ5.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ5.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ5.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ5.ᜈ().ᜁ(A_2);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Vertical.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Vertical.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Vertical.Space = 0f;
				sprῊ6.ᜈ().ᜁ(A_2);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenRowBanding);
				sprῊ7.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ7.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ7.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ7.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ7.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ7.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ7.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ7.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ7.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ7.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ7.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ7.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ7.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ7.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ7.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ7.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ7.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				sprῊ7.ᜈ().ᜁ().Vertical.LineWidth = 1f;
				sprῊ7.ᜈ().ᜁ().Vertical.Color = A_1;
				sprῊ7.ᜈ().ᜁ().Vertical.Space = 0f;
			}

			// Token: 0x06004023 RID: 16419 RVA: 0x003B5C14 File Offset: 0x003B4C14
			private static void ᜃ(IStyle A_0, Color A_1, Color A_2, Color A_3)
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
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜀ().BeforeSpacing = 0f;
				sprῊ.ᜀ().AfterSpacing = 0f;
				sprῊ.ᜀ().LineSpacing = 12f;
				sprῊ.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ(A_2);
				sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜀ().BeforeSpacing = 0f;
				sprῊ2.ᜀ().AfterSpacing = 0f;
				sprῊ2.ᜀ().LineSpacing = 12f;
				sprῊ2.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Double;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ(A_3);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ(A_3);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenRowBanding);
				sprῊ7.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ7.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
			}

			// Token: 0x06004024 RID: 16420 RVA: 0x003B6608 File Offset: 0x003B5608
			private static void ᜀ(IStyle A_0, Color A_1)
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
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 2.25f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 2.25f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜀ().BeforeSpacing = 0f;
				sprῊ.ᜀ().AfterSpacing = 0f;
				sprῊ.ᜀ().LineSpacing = 12f;
				sprῊ.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Top.LineWidth = 2.25f;
				sprῊ.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 2.25f;
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ(A_1);
				sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜀ().BeforeSpacing = 0f;
				sprῊ2.ᜀ().AfterSpacing = 0f;
				sprῊ2.ᜀ().LineSpacing = 12f;
				sprῊ2.ᜀ().LineSpacingRule = LineSpacingRule.Multiple;
				sprῊ2.CharacterFormat.TextColor = Color.Empty;
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Double;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 2.25f;
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ3.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Bottom.LineWidth = 2.25f;
				sprῊ3.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ(A_1);
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ(A_1);
				sprῊ4.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 216, 216, 216));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ(Color.FromArgb(255, 216, 216, 216));
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ7.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ7.ᜈ().ᜁ().Top.LineWidth = 2.25f;
				sprῊ7.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ7.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ7.ᜈ().ᜁ().Bottom.LineWidth = 2.25f;
				sprῊ7.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ7.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ7.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ7.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ7.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ sprῊ8 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ8.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ8.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ8.ᜈ().ᜁ().Top.LineWidth = 2.25f;
				sprῊ8.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ8.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ8.ᜈ().ᜁ().Bottom.LineWidth = 2.25f;
				sprῊ8.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ8.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ8.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ8.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ8.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
			}

			// Token: 0x06004025 RID: 16421 RVA: 0x003B7170 File Offset: 0x003B6170
			private static void ᜁ(IStyle A_0, Color A_1, Color A_2)
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
				(A_0 as spr\u173A).CharacterFormat.TextColor = Color.Black;
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.TextColor = Color.FromArgb(255, 31, 73, 125);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ4.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ4.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ4.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ4.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ(A_2);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ(A_2);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
			}

			// Token: 0x06004026 RID: 16422 RVA: 0x003B7734 File Offset: 0x003B6734
			private static void ᜀ(IStyle A_0, Color A_1, Color A_2)
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
				(A_0 as spr\u173A).CharacterFormat.TextColor = Color.Black;
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.FontSize = 12f;
				sprῊ.CharacterFormat.FontSizeBidi = 12f;
				sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 3f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = A_1;
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ3.ᜈ().ᜁ().Right.Color = A_1;
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ4.ᜈ().ᜁ().Left.Color = A_1;
				sprῊ4.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ(A_2);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ(A_2);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ7.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ7.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ7.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ8 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ8.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
			}

			// Token: 0x06004027 RID: 16423 RVA: 0x003B8144 File Offset: 0x003B7144
			private static void ᜂ(IStyle A_0, Color A_1, Color A_2, Color A_3)
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
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(A_2);
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 2.25f;
				sprῊ2.ᜈ().ᜁ().Top.Color = A_1;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ(A_3);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ(A_3);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
			}

			// Token: 0x06004028 RID: 16424 RVA: 0x003B879C File Offset: 0x003B779C
			private static void ᜀ(IStyle A_0, Color A_1, Color A_2, Color A_3, Color A_4, Color A_5)
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
				(A_0 as spr\u173A).CharacterFormat.TextColor = Color.Black;
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = A_1;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(A_2);
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.Black;
				sprῊ.ᜈ().ᜁ(A_3);
				sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.TextColor = Color.Black;
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1.5f;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.CharacterFormat.TextColor = Color.Black;
				sprῊ3.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.CharacterFormat.TextColor = Color.Black;
				sprῊ4.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ(A_4);
				sprῊ4.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ(A_5);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Horizontal.LineWidth = 0.75f;
				sprῊ6.ᜈ().ᜁ().Horizontal.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Horizontal.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Vertical.LineWidth = 0.75f;
				sprῊ6.ᜈ().ᜁ().Vertical.Color = A_1;
				sprῊ6.ᜈ().ᜁ().Vertical.Space = 0f;
				sprῊ6.ᜈ().ᜁ(A_5);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ7.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ7.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ7.ᜈ().ᜀ(TextureStyle.TextureNone);
			}

			// Token: 0x06004029 RID: 16425 RVA: 0x003B924C File Offset: 0x003B824C
			private static void ᜁ(IStyle A_0, Color A_1, Color A_2, Color A_3)
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
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(A_1);
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.Italic = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 3f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Vertical.LineWidth = 1f;
				sprῊ.ᜈ().ᜁ().Vertical.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Vertical.Space = 0f;
				sprῊ.ᜈ().ᜁ(A_2);
				sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.Italic = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 5, sprῊ2.CharacterFormat.Italic);
				sprῊ2.CharacterFormat.ItalicBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 60, sprῊ2.CharacterFormat.ItalicBidi);
				sprῊ2.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 3f;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ2.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ2.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Vertical.LineWidth = 1f;
				sprῊ2.ᜈ().ᜁ().Vertical.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ2.ᜈ().ᜁ().Vertical.Space = 0f;
				sprῊ2.ᜈ().ᜁ(A_2);
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.CharacterFormat.Italic = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 5, sprῊ3.CharacterFormat.Italic);
				sprῊ3.CharacterFormat.ItalicBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 60, sprῊ3.CharacterFormat.ItalicBidi);
				sprῊ3.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ3.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ3.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ3.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 3f;
				sprῊ3.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ(A_2);
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.CharacterFormat.Italic = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 5, sprῊ4.CharacterFormat.Italic);
				sprῊ4.CharacterFormat.ItalicBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 60, sprῊ4.CharacterFormat.ItalicBidi);
				sprῊ4.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ4.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Left.LineWidth = 3f;
				sprῊ4.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ4.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ(A_2);
				sprῊ4.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ5.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ5.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ5.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ5.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ5.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ(A_3);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Top.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ6.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Bottom.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ6.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Left.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ6.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Right.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ6.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Horizontal.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Horizontal.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ6.ᜈ().ᜁ().Horizontal.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Vertical.LineWidth = 1f;
				sprῊ6.ᜈ().ᜁ().Vertical.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ6.ᜈ().ᜁ().Vertical.Space = 0f;
				sprῊ6.ᜈ().ᜁ(A_3);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
			}

			// Token: 0x0600402A RID: 16426 RVA: 0x003BA770 File Offset: 0x003B9770
			private static void ᜀ(IStyle A_0, Color A_1, Color A_2, Color A_3)
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
				(A_0 as spr\u173A).CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜊ().ᜁ(A_1);
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 2.25f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 0, 0, 0));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 2.25f;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ2.ᜈ().ᜁ(A_2);
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 2.25f;
				sprῊ3.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ3.ᜈ().ᜁ(A_3);
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Left.LineWidth = 2.25f;
				sprῊ4.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ4.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ4.ᜈ().ᜁ(A_3);
				sprῊ4.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ(A_3);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ6.ᜈ().ᜁ(A_3);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
			}

			// Token: 0x0600402B RID: 16427 RVA: 0x003BB01C File Offset: 0x003BA01C
			private static void ᜀ(IStyle A_0, Color A_1, Color A_2, Color A_3, Color A_4, Color A_5, Color A_6)
			{
				int a_ = 0;
				switch (0)
				{
				default:
					for (;;)
					{
						(A_0 as spr\u173A).CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 0);
						(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
						(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
						(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
						(A_0 as spr\u173A).ᜃ().ᜁ(1L);
						(A_0 as spr\u173A).ᜃ().ᜀ(1L);
						(A_0 as spr\u173A).ᜃ().ᜀ(0f);
						(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
						(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
						(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
						(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
						(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 3f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = A_1;
						(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
						(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.5f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = A_2;
						(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
						(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.5f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = A_2;
						(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
						(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.5f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = A_2;
						(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
						(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.5f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 255, 255, 255);
						(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
						(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.5f;
						(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 255, 255, 255);
						(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
						(A_0 as spr\u173A).ᜊ().ᜁ(A_3);
						(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
						(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
						sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
						sprῊ.CharacterFormat.Bold = true;
						Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
						sprῊ.CharacterFormat.BoldBidi = true;
						Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
						sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
						sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
						sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 3f;
						sprῊ.ᜈ().ᜁ().Bottom.Color = A_1;
						sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
						sprῊ.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
						sprῊ.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
						sprῊ.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
						sprῊ.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
						sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
						sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
						sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
						sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
						sprῊ2.CharacterFormat.Bold = true;
						Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
						sprῊ2.CharacterFormat.BoldBidi = true;
						Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
						sprῊ2.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
						sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
						sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
						sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 255, 255, 255);
						sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
						sprῊ2.ᜈ().ᜁ(A_4);
						sprῊ2.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
						sprῊ2.ᜈ().ᜀ(TextureStyle.TextureNone);
						sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
						sprῊ3.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
						sprῊ3.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
						sprῊ3.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
						sprῊ3.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
						sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
						sprῊ3.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
						sprῊ3.ᜈ().ᜁ().Horizontal.LineWidth = 0.5f;
						sprῊ3.ᜈ().ᜁ().Horizontal.Color = A_4;
						sprῊ3.ᜈ().ᜁ().Horizontal.Space = 0f;
						sprῊ3.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
						sprῊ3.ᜈ().ᜁ(A_4);
						sprῊ3.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
						sprῊ3.ᜈ().ᜀ(TextureStyle.TextureNone);
						sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
						sprῊ4.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
						sprῊ4.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
						sprῊ4.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
						sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
						sprῊ4.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
						sprῊ4.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
						sprῊ4.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
						sprῊ4.ᜈ().ᜁ(A_4);
						sprῊ4.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
						sprῊ4.ᜈ().ᜀ(TextureStyle.TextureNone);
						sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
						sprῊ5.ᜈ().ᜁ(A_5);
						sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
						sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
						sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
						sprῊ6.ᜈ().ᜁ(A_6);
						sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
						sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
						if (true)
						{
						}
						int num = 2;
						for (;;)
						{
							switch (num)
							{
							case 0:
							{
								sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
								sprῊ7.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 0);
								sprῊ sprῊ8 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
								sprῊ8.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 0);
								goto IL_A79;
							}
							case 1:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_A79;
								default:
									goto IL_A9B;
								}
								break;
							case 2:
								if (A_0.Name != ClipboardData.b("╥ݧ٩ͫᱭᙯݱᡳ噵⭷ቹᵻ᩽ꚅ즇ﺏ뒓ꖕ", a_))
								{
									num = 0;
									continue;
								}
								return;
							}
							break;
							IL_A79:
							num = 1;
						}
					}
					IL_A9B:
					if (false)
					{
					}
					return;
				}
			}

			// Token: 0x0600402C RID: 16428 RVA: 0x003BBAD8 File Offset: 0x003BAAD8
			private static void ᜁ(IStyle A_0, Color A_1, Color A_2, Color A_3, Color A_4)
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
				(A_0 as spr\u173A).CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜊ().ᜁ(A_1);
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ(A_2);
				sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.TextColor = A_2;
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1.5f;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ().Top.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Right.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Horizontal.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ().Vertical.BorderType = BorderStyle.Cleared;
				sprῊ5.ᜈ().ᜁ(A_3);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ(A_4);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
			}

			// Token: 0x0600402D RID: 16429 RVA: 0x003BC074 File Offset: 0x003BB074
			private static void ᜀ(IStyle A_0, Color A_1, Color A_2, Color A_3, Color A_4)
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
				(A_0 as spr\u173A).CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜅ().AfterSpacing = 0f;
				(A_0 as spr\u173A).ᜅ().LineSpacing = 12f;
				(A_0 as spr\u173A).ᜅ().LineSpacingRule = LineSpacingRule.Multiple;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(A_1);
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ(A_2);
				sprῊ.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ(A_2);
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ3.ᜈ().ᜁ(A_3);
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ4.ᜈ().ᜁ(A_3);
				sprῊ4.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.ᜈ().ᜁ(A_4);
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ(A_4);
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(TextureStyle.TextureNone);
			}

			// Token: 0x0600402E RID: 16430 RVA: 0x003BC514 File Offset: 0x003BB514
			private static void ᜪ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 128, 0, 128);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 128, 128, 128);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 128, 128, 128);
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 0.75f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ4.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Left.LineWidth = 0.75f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ5.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().Bottom.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Bottom.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().Left.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Left.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ6.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().Bottom.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Bottom.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().Right.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().Right.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Right.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowLastCell);
				sprῊ7.ᜈ().ᜁ().Top.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().Top.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ7.ᜈ().ᜁ().Top.LineWidth = 0f;
				sprῊ7.ᜈ().ᜁ().Left.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().Left.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ7.ᜈ().ᜁ().Left.LineWidth = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ8 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ8.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 128);
				sprῊ8.ᜈ().ᜁ().Top.BorderType = BorderStyle.None;
				sprῊ8.ᜈ().ᜁ().Top.Color = Color.Black;
				sprῊ8.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ8.ᜈ().ᜁ().Top.LineWidth = 0f;
				sprῊ8.ᜈ().ᜁ().Right.BorderType = BorderStyle.None;
				sprῊ8.ᜈ().ᜁ().Right.Color = Color.Black;
				sprῊ8.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ8.ᜈ().ᜁ().Right.LineWidth = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x0600402F RID: 16431 RVA: 0x003BD2F8 File Offset: 0x003BC2F8
			private static void ᜩ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().Bottom.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 128, 128, 128);
				sprῊ2.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Right.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 0.75f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ4.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 128, 128, 128);
				sprῊ4.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ4.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ4.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ5.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 4, sprῊ5.CharacterFormat.Bold);
				sprῊ5.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 59, sprῊ5.CharacterFormat.BoldBidi);
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004030 RID: 16432 RVA: 0x003BDBA0 File Offset: 0x003BCBA0
			private static void ᜨ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().Bottom.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 128, 128, 128);
				sprῊ2.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Right.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 0.75f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ4.CharacterFormat.TextColor = Color.Empty;
				sprῊ4.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				sprῊ4.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenColumnBanding);
				sprῊ5.CharacterFormat.TextColor = Color.Empty;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				sprῊ5.ᜈ().ᜀ(TextureStyle.Texture50Percent);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ6.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 128, 128, 128);
				sprῊ6.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ6.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ6.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 255, 255, 255);
				sprῊ6.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ7.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 4, sprῊ7.CharacterFormat.Bold);
				sprῊ7.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 59, sprῊ7.CharacterFormat.BoldBidi);
				sprῊ7.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004031 RID: 16433 RVA: 0x003BE4F0 File Offset: 0x003BD4F0
			private static void ᜧ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.TextColor = Color.Empty;
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 0.75f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.CharacterFormat.Italic = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 5, sprῊ4.CharacterFormat.Italic);
				sprῊ4.CharacterFormat.ItalicBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 60, sprῊ4.CharacterFormat.ItalicBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ5.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 4, sprῊ5.CharacterFormat.Bold);
				sprῊ5.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 59, sprῊ5.CharacterFormat.BoldBidi);
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004032 RID: 16434 RVA: 0x003BEDB8 File Offset: 0x003BDDB8
			private static void ᜦ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 128, 0, 128));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 128, 0, 128));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ6.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 128);
				sprῊ6.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004033 RID: 16435 RVA: 0x003BF788 File Offset: 0x003BE788
			private static void ᜥ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 128));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 128);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1.5f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 0);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004034 RID: 16436 RVA: 0x003BFFF4 File Offset: 0x003BEFF4
			private static void ᜤ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 128));
				sprῊ.ᜈ().ᜀ(TextureStyle.Texture50Percent);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 128);
				sprῊ2.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ2.ᜈ().ᜀ(TextureStyle.Texture50Percent);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ5.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 128);
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004035 RID: 16437 RVA: 0x003C0A2C File Offset: 0x003BFA2C
			private static void ᜣ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(255, 0, 128, 128));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 5, sprῊ2.CharacterFormat.Italic);
				sprῊ2.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 60, sprῊ2.CharacterFormat.ItalicBidi);
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 128));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.CharacterFormat.Italic = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 5, sprῊ4.CharacterFormat.Italic);
				sprῊ4.CharacterFormat.ItalicBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 60, sprῊ4.CharacterFormat.ItalicBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004036 RID: 16438 RVA: 0x003C13FC File Offset: 0x003C03FC
			private static void ᜢ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(255, 255, 255, 0));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.Texture20Percent);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 128, 0, 0));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 5, sprῊ2.CharacterFormat.Italic);
				sprῊ2.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 60, sprῊ2.CharacterFormat.ItalicBidi);
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.CharacterFormat.Italic = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 5, sprῊ4.CharacterFormat.Italic);
				sprῊ4.CharacterFormat.ItalicBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 60, sprῊ4.CharacterFormat.ItalicBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004037 RID: 16439 RVA: 0x003C1BD0 File Offset: 0x003C0BD0
			private static void ᜡ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 2.25f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 2.25f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 2.25f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 2.25f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 192, 192, 192);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(255, 0, 128, 128));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.Texture25Percent);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 128, 128));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ2.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Left.LineWidth = 4.5f;
				sprῊ2.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Right.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(255, 0, 128, 128));
				sprῊ2.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureSolid);
			}

			// Token: 0x06004038 RID: 16440 RVA: 0x003C2498 File Offset: 0x003C1498
			private static void ᜠ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ((A_0 as spr\u173A).CharacterFormat, 4, (A_0 as spr\u173A).CharacterFormat.Bold);
				(A_0 as spr\u173A).CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ((A_0 as spr\u173A).CharacterFormat, 59, (A_0 as spr\u173A).CharacterFormat.BoldBidi);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Double;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.CharacterFormat.TextColor = Color.Empty;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ5.ᜈ().ᜀ(TextureStyle.Texture25Percent);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenColumnBanding);
				sprῊ6.CharacterFormat.TextColor = Color.Empty;
				sprῊ6.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(255, 255, 255, 0));
				sprῊ6.ᜈ().ᜀ(TextureStyle.Texture25Percent);
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ7.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 4, sprῊ7.CharacterFormat.Bold);
				sprῊ7.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 59, sprῊ7.CharacterFormat.BoldBidi);
				sprῊ7.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ8 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ8.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ8.CharacterFormat, 4, sprῊ8.CharacterFormat.Bold);
				sprῊ8.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ8.CharacterFormat, 59, sprῊ8.CharacterFormat.BoldBidi);
				sprῊ8.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004039 RID: 16441 RVA: 0x003C2FE4 File Offset: 0x003C1FE4
			private static void \u171F(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ((A_0 as spr\u173A).CharacterFormat, 4, (A_0 as spr\u173A).CharacterFormat.Bold);
				(A_0 as spr\u173A).CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ((A_0 as spr\u173A).CharacterFormat, 59, (A_0 as spr\u173A).CharacterFormat.BoldBidi);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 128));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 0);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.CharacterFormat.TextColor = Color.Empty;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ5.ᜈ().ᜀ(TextureStyle.Texture30Percent);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenColumnBanding);
				sprῊ6.CharacterFormat.TextColor = Color.Empty;
				sprῊ6.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(255, 0, 255, 0));
				sprῊ6.ᜈ().ᜀ(TextureStyle.Texture25Percent);
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ7.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 4, sprῊ7.CharacterFormat.Bold);
				sprῊ7.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 59, sprῊ7.CharacterFormat.BoldBidi);
				sprῊ7.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ8 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ8.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ8.CharacterFormat, 4, sprῊ8.CharacterFormat.Bold);
				sprῊ8.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ8.CharacterFormat, 59, sprῊ8.CharacterFormat.BoldBidi);
				sprῊ8.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ8.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x0600403A RID: 16442 RVA: 0x003C3900 File Offset: 0x003C2900
			private static void \u171E(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ((A_0 as spr\u173A).CharacterFormat, 4, (A_0 as spr\u173A).CharacterFormat.Bold);
				(A_0 as spr\u173A).CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ((A_0 as spr\u173A).CharacterFormat, 59, (A_0 as spr\u173A).CharacterFormat.BoldBidi);
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 128));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 128);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.CharacterFormat.TextColor = Color.Empty;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenColumnBanding);
				sprῊ6.CharacterFormat.TextColor = Color.Empty;
				sprῊ6.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ6.ᜈ().ᜀ(TextureStyle.Texture10Percent);
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ7.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 4, sprῊ7.CharacterFormat.Bold);
				sprῊ7.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 59, sprῊ7.CharacterFormat.BoldBidi);
				sprῊ7.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x0600403B RID: 16443 RVA: 0x003C43E4 File Offset: 0x003C33E4
			private static void \u171D(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ4.CharacterFormat.TextColor = Color.Empty;
				sprῊ4.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(Color.FromArgb(255, 0, 128, 128));
				sprῊ4.ᜈ().ᜀ(TextureStyle.Texture50Percent);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenColumnBanding);
				sprῊ5.CharacterFormat.TextColor = Color.Empty;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ5.ᜈ().ᜀ(TextureStyle.Texture10Percent);
			}

			// Token: 0x0600403C RID: 16444 RVA: 0x003C490C File Offset: 0x003C390C
			private static void \u171C(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 128, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 128, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 128, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 128, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 192, 192, 192);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 128, 128, 128);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 128, 128, 128);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddColumnBanding);
				sprῊ5.CharacterFormat.TextColor = Color.Empty;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				sprῊ5.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenColumnBanding);
				sprῊ6.CharacterFormat.TextColor = Color.Empty;
			}

			// Token: 0x0600403D RID: 16445 RVA: 0x003C52DC File Offset: 0x003C42DC
			private static void \u171B(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 2.25f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 2.25f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 255, 255, 255);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.Empty;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ.ᜈ().ᜀ(TextureStyle.Texture20Percent);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ2.CharacterFormat.TextColor = Color.Empty;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ2.ᜈ().ᜀ(TextureStyle.Texture5Percent);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenRowBanding);
				sprῊ3.CharacterFormat.TextColor = Color.Empty;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ3.ᜈ().ᜀ(TextureStyle.Texture20Percent);
			}

			// Token: 0x0600403E RID: 16446 RVA: 0x003C5898 File Offset: 0x003C4898
			private static void \u171A(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Double;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Double;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Double;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Double;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.AllCaps = true;
				sprῊ.CharacterFormat.TextColor = Color.Empty;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x0600403F RID: 16447 RVA: 0x003C5DA8 File Offset: 0x003C4DA8
			private static void \u1719(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ2.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 5, sprῊ2.CharacterFormat.Italic);
				sprῊ2.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 60, sprῊ2.CharacterFormat.ItalicBidi);
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004040 RID: 16448 RVA: 0x003C6400 File Offset: 0x003C5400
			private static void \u1718(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004041 RID: 16449 RVA: 0x003C6AFC File Offset: 0x003C5AFC
			private static void \u1717(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 255, 255, 0));
				sprῊ.ᜈ().ᜀ(TextureStyle.Texture30Percent);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004042 RID: 16450 RVA: 0x003C7264 File Offset: 0x003C6264
			private static void \u1716(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.TextColor = Color.Empty;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 255, 255, 0));
				sprῊ.ᜈ().ᜀ(TextureStyle.Texture30Percent);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.TextColor = Color.Empty;
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(255, 255, 255, 0));
				sprῊ2.ᜈ().ᜀ(TextureStyle.Texture30Percent);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.CharacterFormat.TextColor = Color.Empty;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004043 RID: 16451 RVA: 0x003C7A38 File Offset: 0x003C6A38
			private static void \u1715(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0.75f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004044 RID: 16452 RVA: 0x003C82B0 File Offset: 0x003C72B0
			private static void \u1714(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.TextColor = Color.Empty;
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0.75f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004045 RID: 16453 RVA: 0x003C8B24 File Offset: 0x003C7B24
			private static void \u1713(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ((A_0 as spr\u173A).CharacterFormat, 4, (A_0 as spr\u173A).CharacterFormat.Bold);
				(A_0 as spr\u173A).CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ((A_0 as spr\u173A).CharacterFormat, 59, (A_0 as spr\u173A).CharacterFormat.BoldBidi);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = false;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowFirstCell);
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0.75f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004046 RID: 16454 RVA: 0x003C95D8 File Offset: 0x003C85D8
			private static void \u1712(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 128));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.TextColor = Color.Empty;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.CharacterFormat.TextColor = Color.Empty;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004047 RID: 16455 RVA: 0x003C9DF4 File Offset: 0x003C8DF4
			private static void ᜑ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 128, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ3.CharacterFormat.TextColor = Color.Empty;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				sprῊ3.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenRowBanding);
				sprῊ4.CharacterFormat.TextColor = Color.Empty;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ5.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 4, sprῊ5.CharacterFormat.Bold);
				sprῊ5.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 59, sprῊ5.CharacterFormat.BoldBidi);
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004048 RID: 16456 RVA: 0x003CA7B4 File Offset: 0x003C97B4
			private static void ᜐ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(2L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 128, 128, 128);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 0, 128, 0));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 128, 128));
				sprῊ.ᜈ().ᜀ(TextureStyle.Texture75Percent);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ3.CharacterFormat.TextColor = Color.Empty;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(255, 0, 255, 0));
				sprῊ3.ᜈ().ᜀ(TextureStyle.Texture20Percent);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenRowBanding);
				sprῊ4.CharacterFormat.TextColor = Color.Empty;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ5.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 4, sprῊ5.CharacterFormat.Bold);
				sprῊ5.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 59, sprῊ5.CharacterFormat.BoldBidi);
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004049 RID: 16457 RVA: 0x003CAF90 File Offset: 0x003C9F90
			private static void ᜏ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 128);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1.5f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ3.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 5, sprῊ3.CharacterFormat.Italic);
				sprῊ3.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 60, sprῊ3.CharacterFormat.ItalicBidi);
				sprῊ3.CharacterFormat.TextColor = Color.FromArgb(255, 0, 0, 128);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x0600404A RID: 16458 RVA: 0x003CB650 File Offset: 0x003CA650
			private static void ᜎ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 128, 128, 128));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
			}

			// Token: 0x0600404B RID: 16459 RVA: 0x003CBBF0 File Offset: 0x003CABF0
			private static void \u170D(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x0600404C RID: 16460 RVA: 0x003CC234 File Offset: 0x003CB234
			private static void ᜌ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.Texture50Percent);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Right.LineWidth = 1.5f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ3.ᜈ().ᜀ(TextureStyle.Texture25Percent);
			}

			// Token: 0x0600404D RID: 16461 RVA: 0x003CC990 File Offset: 0x003CB990
			private static void ᜋ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 128, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 128, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 128, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 128, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 128, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 192, 192, 192));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 128, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1.5f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ5.CharacterFormat.TextColor = Color.Empty;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ5.ᜈ().ᜀ(TextureStyle.Texture20Percent);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenRowBanding);
				sprῊ6.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(255, 255, 255, 0));
				sprῊ6.ᜈ().ᜀ(TextureStyle.Texture25Percent);
			}

			// Token: 0x0600404E RID: 16462 RVA: 0x003CD508 File Offset: 0x003CC508
			private static void ᜊ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.Italic = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 5, sprῊ.CharacterFormat.Italic);
				sprῊ.CharacterFormat.ItalicBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 60, sprῊ.CharacterFormat.ItalicBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 255, 255, 0));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ5.CharacterFormat.TextColor = Color.Empty;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 255, 255, 0));
				sprῊ5.ᜈ().ᜀ(TextureStyle.Texture25Percent);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.EvenRowBanding);
				sprῊ6.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ6.ᜈ().ᜀ(Color.FromArgb(255, 255, 0, 0));
				sprῊ6.ᜈ().ᜀ(TextureStyle.Texture50Percent);
			}

			// Token: 0x0600404F RID: 16463 RVA: 0x003CE0AC File Offset: 0x003CD0AC
			private static void ᜉ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.Empty;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
			}

			// Token: 0x06004050 RID: 16464 RVA: 0x003CE640 File Offset: 0x003CD640
			private static void ᜈ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 128, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 128, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 128, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 128, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004051 RID: 16465 RVA: 0x003CEAF4 File Offset: 0x003CDAF4
			private static void ᜇ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 4, sprῊ2.CharacterFormat.Bold);
				sprῊ2.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ2.CharacterFormat, 59, sprῊ2.CharacterFormat.BoldBidi);
				sprῊ2.CharacterFormat.TextColor = Color.Empty;
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 4, sprῊ3.CharacterFormat.Bold);
				sprῊ3.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ3.CharacterFormat, 59, sprῊ3.CharacterFormat.BoldBidi);
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 1.5f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 4, sprῊ4.CharacterFormat.Bold);
				sprῊ4.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ4.CharacterFormat, 59, sprῊ4.CharacterFormat.BoldBidi);
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ4.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Left.LineWidth = 0.75f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ5.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 4, sprῊ5.CharacterFormat.Bold);
				sprῊ5.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 59, sprῊ5.CharacterFormat.BoldBidi);
				sprῊ5.ᜈ().ᜁ().Left.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().Left.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Left.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ6.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ6.CharacterFormat, 4, sprῊ6.CharacterFormat.Bold);
				sprῊ6.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ6.CharacterFormat, 59, sprῊ6.CharacterFormat.BoldBidi);
				sprῊ6.ᜈ().ᜁ().Top.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().Top.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ6.ᜈ().ᜁ().Top.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004052 RID: 16466 RVA: 0x003CF510 File Offset: 0x003CE510
			private static void ᜆ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 1.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 4, sprῊ.CharacterFormat.Bold);
				sprῊ.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ.CharacterFormat, 59, sprῊ.CharacterFormat.BoldBidi);
				sprῊ.CharacterFormat.TextColor = Color.FromArgb(255, 255, 255, 255);
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ.ᜈ().ᜀ(Color.FromArgb(255, 0, 0, 0));
				sprῊ.ᜈ().ᜀ(TextureStyle.TextureSolid);
			}

			// Token: 0x06004053 RID: 16467 RVA: 0x003CF9B8 File Offset: 0x003CE9B8
			private static void ᜅ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1L);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ.ᜈ().ᜁ().Top.LineWidth = 0.75f;
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1.5f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ2.ᜈ().ᜀ(Color.FromArgb(255, 128, 0, 128));
				sprῊ2.ᜈ().ᜀ(TextureStyle.Texture25Percent);
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 1.5f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ4.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Left.LineWidth = 1.5f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.OddRowBanding);
				sprῊ5.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ5.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ5.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ5.ᜈ().ᜁ().Bottom.LineWidth = 0.75f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ5.ᜈ().ᜀ(Color.FromArgb(255, 128, 128, 0));
				sprῊ5.ᜈ().ᜀ(TextureStyle.Texture25Percent);
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ6.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ6.CharacterFormat, 4, sprῊ6.CharacterFormat.Bold);
				sprῊ6.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ6.CharacterFormat, 59, sprῊ6.CharacterFormat.BoldBidi);
				sprῊ6.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ7 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ7.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 4, sprῊ7.CharacterFormat.Bold);
				sprῊ7.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ7.CharacterFormat, 59, sprῊ7.CharacterFormat.BoldBidi);
				sprῊ7.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ7.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004054 RID: 16468 RVA: 0x003D0448 File Offset: 0x003CF448
			private static void ᜄ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.ᜈ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				sprῊ.ᜈ().ᜁ().Bottom.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ.ᜈ().ᜁ().Bottom.Space = 0f;
				sprῊ.ᜈ().ᜁ().Bottom.LineWidth = 1.5f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ2 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRow);
				sprῊ2.ᜈ().ᜁ().Top.BorderType = BorderStyle.Single;
				sprῊ2.ᜈ().ᜁ().Top.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ2.ᜈ().ᜁ().Top.Space = 0f;
				sprῊ2.ᜈ().ᜁ().Top.LineWidth = 1.5f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ2.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ3 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstColumn);
				sprῊ3.ᜈ().ᜁ().Right.BorderType = BorderStyle.Single;
				sprῊ3.ᜈ().ᜁ().Right.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ3.ᜈ().ᜁ().Right.Space = 0f;
				sprῊ3.ᜈ().ᜁ().Right.LineWidth = 1.5f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ3.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ3.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ3.ᜈ().ᜀ(Color.FromArgb(255, 0, 128, 0));
				sprῊ3.ᜈ().ᜀ(TextureStyle.Texture25Percent);
				sprῊ sprῊ4 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastColumn);
				sprῊ4.ᜈ().ᜁ().Left.BorderType = BorderStyle.Single;
				sprῊ4.ᜈ().ᜁ().Left.Color = Color.FromArgb(255, 0, 0, 0);
				sprῊ4.ᜈ().ᜁ().Left.Space = 0f;
				sprῊ4.ᜈ().ᜁ().Left.LineWidth = 1.5f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ4.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ4.ᜈ().ᜁ(Color.FromArgb(255, 255, 255, 255));
				sprῊ4.ᜈ().ᜀ(Color.FromArgb(255, 128, 128, 0));
				sprῊ4.ᜈ().ᜀ(TextureStyle.Texture25Percent);
				sprῊ sprῊ5 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRowLastCell);
				sprῊ5.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 4, sprῊ5.CharacterFormat.Bold);
				sprῊ5.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ5.CharacterFormat, 59, sprῊ5.CharacterFormat.BoldBidi);
				sprῊ5.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ5.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
				sprῊ sprῊ6 = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.LastRowFirstCell);
				sprῊ6.CharacterFormat.Bold = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ6.CharacterFormat, 4, sprῊ6.CharacterFormat.Bold);
				sprῊ6.CharacterFormat.BoldBidi = true;
				Style.BuiltinStyleLoader.ᜀ(sprῊ6.CharacterFormat, 59, sprῊ6.CharacterFormat.BoldBidi);
				sprῊ6.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ6.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004055 RID: 16469 RVA: 0x003D0E04 File Offset: 0x003CFE04
			private static void ᜃ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Single;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.5f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
			}

			// Token: 0x06004056 RID: 16470 RVA: 0x003D1198 File Offset: 0x003D0198
			private static void ᜂ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1f);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				(A_0 as spr\u173A).ᜈ().ᜀ(1f);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.TextColor = Color.Empty;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004057 RID: 16471 RVA: 0x003D169C File Offset: 0x003D069C
			private static void ᜁ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1f);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Inset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Inset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Inset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Inset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Inset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Inset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				(A_0 as spr\u173A).ᜈ().ᜀ(1f);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.TextColor = Color.Empty;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004058 RID: 16472 RVA: 0x003D1BA0 File Offset: 0x003D0BA0
			private static void ᜀ(IStyle A_0)
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
				(A_0 as Style).UnhideWhenUsed = true;
				(A_0 as spr\u173A).ᜃ().ᜁ(1f);
				(A_0 as spr\u173A).ᜃ().ᜀ(0f);
				(A_0 as spr\u173A).ᜃ().ᜈ().Top = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Bottom = 0f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Left = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜈ().Right = 5.4f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.LineWidth = 3f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Top.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.LineWidth = 3f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Bottom.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.LineWidth = 3f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Left.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.LineWidth = 3f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Right.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Horizontal.Space = 0f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.BorderType = BorderStyle.Outset;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.LineWidth = 0.75f;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Color = Color.Black;
				(A_0 as spr\u173A).ᜃ().ᜁ().Vertical.Space = 0f;
				(A_0 as spr\u173A).ᜊ().ᜁ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(Color.FromArgb(0, 255, 255, 255));
				(A_0 as spr\u173A).ᜊ().ᜀ(TextureStyle.TextureNone);
				(A_0 as spr\u173A).ᜈ().ᜀ(1f);
				sprῊ sprῊ = (A_0 as spr\u173A).ᜀ(ConditionalFormattingCode.FirstRow);
				sprῊ.CharacterFormat.TextColor = Color.Empty;
				sprῊ.ᜈ().ᜁ().DiagonalDown.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalDown.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalDown.LineWidth = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.BorderType = BorderStyle.None;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Color = Color.Black;
				sprῊ.ᜈ().ᜁ().DiagonalUp.Space = 0f;
				sprῊ.ᜈ().ᜁ().DiagonalUp.LineWidth = 0f;
			}

			// Token: 0x06004059 RID: 16473 RVA: 0x003D20A4 File Offset: 0x003D10A4
			private static void ᜀ(FormatBase A_0, short A_1, bool A_2)
			{
				int num = 1;
				for (;;)
				{
					if (true)
					{
					}
					byte b;
					byte b2;
					switch (num)
					{
					case 0:
						if (b != 0)
						{
							num = 3;
							continue;
						}
						goto IL_91;
					case 2:
						b2 = 128;
						goto IL_6C;
					case 3:
						A_0.ᜀ(A_1, b);
						num = 5;
						continue;
					case 4:
						b2 = 129;
						goto IL_6C;
					case 5:
						goto IL_91;
					case 6:
						num = 2;
						continue;
					}
					if (!A_2)
					{
						num = 6;
						continue;
					}
					goto IL_82;
					IL_6C:
					b = b2;
					num = 0;
					continue;
					IL_82:
					num = 4;
					continue;
					IL_91:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_82;
					default:
						goto IL_A7;
					}
				}
				IL_A7:
				if (false)
				{
				}
			}

			// Token: 0x0600405B RID: 16475 RVA: 0x003D2174 File Offset: 0x003D1174
			// Note: this type is marked as 'beforefieldinit'.
			static BuiltinStyleLoader()
			{
				int a_ = 15;
				switch (1 == 1)
				{
				}
				if (false)
				{
				}
				if (true)
				{
				}
				Style.BuiltinStyleLoader.ᜃ = new string[]
				{
					ClipboardData.b("㭴ᡶ୸ᙺᱼ፾", a_),
					ClipboardData.b("㵴ቶᡸὺᑼᅾꎂ뒄", a_),
					ClipboardData.b("㵴ቶᡸὺᑼᅾꎂ랄", a_),
					ClipboardData.b("㵴ቶᡸὺᑼᅾꎂ뚄", a_),
					ClipboardData.b("㵴ቶᡸὺᑼᅾꎂ놄", a_),
					ClipboardData.b("㵴ቶᡸὺᑼᅾꎂ낄", a_),
					ClipboardData.b("㵴ቶᡸὺᑼᅾꎂ뎄", a_),
					ClipboardData.b("㵴ቶᡸὺᑼᅾꎂ늄", a_),
					ClipboardData.b("㵴ቶᡸὺᑼᅾꎂ분", a_),
					ClipboardData.b("㵴ቶᡸὺᑼᅾꎂ버", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾낀", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾뎀", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾늀", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾떀", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾뒀", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾란", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾뚀", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾릀", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾뢀", a_),
					ClipboardData.b("ⅴ㡶㩸孺䱼", a_),
					ClipboardData.b("ⅴ㡶㩸孺佼", a_),
					ClipboardData.b("ⅴ㡶㩸孺乼", a_),
					ClipboardData.b("ⅴ㡶㩸孺䥼", a_),
					ClipboardData.b("ⅴ㡶㩸孺䡼", a_),
					ClipboardData.b("ⅴ㡶㩸孺䭼", a_),
					ClipboardData.b("ⅴ㡶㩸孺䩼", a_),
					ClipboardData.b("ⅴ㡶㩸孺䕼", a_),
					ClipboardData.b("ⅴ㡶㩸孺䑼", a_),
					ClipboardData.b("㭴ᡶ୸ᙺᱼ፾ꆀ쪂歷", a_),
					ClipboardData.b("㍴ᡶᙸེ፼ၾꖄ펆歷", a_),
					ClipboardData.b("㙴ᡶᑸᙺ᡼ᅾꎂ톄ﾊ", a_),
					ClipboardData.b("㵴ቶᡸὺ᡼ൾ", a_),
					ClipboardData.b("㍴ᡶᙸེ᡼ൾ", a_),
					ClipboardData.b("㱴᥶ᵸṺռ彾즀", a_),
					ClipboardData.b("㙴ᙶॸེᑼၾ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾ꖄ솆ﶎ", a_),
					ClipboardData.b("㍴ᡶᙸེ፼ၾꖄ햆ﶎﶒ", a_),
					ClipboardData.b("㙴ᡶᑸᙺ᡼ᅾꎂ힄ﾌﾐ", a_),
					ClipboardData.b("㥴Ṷ᝸Ṻ嵼ㅾﮈ", a_),
					ClipboardData.b("╴ᙶṸṺ嵼ㅾﮈ", a_),
					ClipboardData.b("ぴ᥶ᵸᕺቼ୾ꎂ힄ﾌﾐ", a_),
					ClipboardData.b("ぴ᥶ᵸᕺቼ୾ꎂ톄ﾊ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾ꖄ욆ﲈﾊ朗ﺖﲘ", a_),
					ClipboardData.b("㡴ᙶ᩸ॺቼ彾햀ﶄ", a_),
					ClipboardData.b("ⅴ㡶㡸孺㕼᩾", a_),
					ClipboardData.b("㥴Ṷ੸ེ", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㵾ﶈ", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼ㅾﮈ", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼䵾", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼䱾", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼䭾", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼䩾", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㵾ﶈꮊ뾌", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㵾ﶈꮊ뺌", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㵾ﶈꮊ릌", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㵾ﶈꮊ뢌", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼ㅾﮈꮊ뾌", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼ㅾﮈꮊ뺌", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼ㅾﮈꮊ릌", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼ㅾﮈꮊ뢌", a_),
					ClipboardData.b("ⅴṶ൸᝺᡼", a_),
					ClipboardData.b("㙴᭶ᙸࡺᑼᅾ", a_),
					ClipboardData.b("♴ṶṸᕺᱼ୾", a_),
					ClipboardData.b("ㅴቶὸ᩺ࡼ፾ꎂ햄ﮈﶎﶔ랖\udf98", a_),
					ClipboardData.b("㝴ᡶᵸɺ嵼⭾ﮂ", a_),
					ClipboardData.b("㝴ᡶᵸɺ嵼⭾ﮂꞆ삈ﾐ", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㱾ﺊ", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㱾ﺊ꾎ꎐ", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㱾ﺊ꾎ꊐ", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㱾ﺊ꾎ꖐ", a_),
					ClipboardData.b("㥴Ṷ੸ེ嵼㱾ﺊ꾎꒐", a_),
					ClipboardData.b("㡴ቶ੸ࡺᱼ᡾ꎂ춄ﶎ", a_),
					ClipboardData.b("♴ɶ᭸ེᑼ୾", a_),
					ClipboardData.b("♴ᙶᕸ๺ॼṾ", a_),
					ClipboardData.b("ㅴᙶ൸Ṻ", a_),
					ClipboardData.b("㝴ᡶᵸɺ嵼⭾ﮂꞆ쾈ﾌﲎ뎒\udc94練ﶘﺚ", a_),
					ClipboardData.b("㝴ᡶᵸɺ嵼⭾ﮂꞆ쾈ﾌﲎ뎒\udc94練ﶘﺚ膠醢", a_),
					ClipboardData.b("㭴ᡶ൸Ṻ嵼㝾", a_),
					ClipboardData.b("㝴ᡶᵸɺ嵼⭾ﮂꞆ뮈", a_),
					ClipboardData.b("㝴ᡶᵸɺ嵼⭾ﮂꞆ몈", a_),
					ClipboardData.b("㝴ᡶᵸɺ嵼⭾ﮂꞆ삈ﾐ떔ꖖ", a_),
					ClipboardData.b("㝴ᡶᵸɺ嵼⭾ﮂꞆ삈ﾐ떔꒖", a_),
					ClipboardData.b("㝴᭶ᙸ᡺ᙼ彾햀ﶄ", a_),
					ClipboardData.b("㵴๶ॸṺོ፾", a_),
					ClipboardData.b("㍴ᡶᕸ᝺ቼࡾ춄ﺆ麗ﾌﶒﺔ", a_),
					ClipboardData.b("♴Ͷ୸ᑺ፼᡾", a_),
					ClipboardData.b("ぴ᩶ॸ፺ᱼ౾", a_),
					ClipboardData.b("ㅴᡶ᩸๺ၼ᩾ꖄ쪆ﮊ", a_),
					ClipboardData.b("╴᭶ᡸቺ፼彾햀ﶄ", a_),
					ClipboardData.b("ぴ婶ᑸ᩺ᑼ፾ꆀ킂歷搜", a_),
					ClipboardData.b("㭴ᡶ୸ᙺᱼ፾ꆀꮂ튄ꊊ", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼㹾", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼㹾愈", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼㱾", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼㱾", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼㭾ﾊﾐ", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼㑾廒力", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼⽾ﮈﮎ", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼Ȿ", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼⭾ﮈ歷", a_),
					ClipboardData.b("㵴⍶㑸㝺嵼⥾", a_),
					ClipboardData.b("㙴ᡶᑸᙺ᡼ᅾꎂ횄", a_),
					ClipboardData.b("㭴ᡶ奸㝺ᑼ౾", a_),
					ClipboardData.b("㝴ᙶᕸ᝺ቼၾꎂ톄ﾊ", a_),
					ClipboardData.b("⁴Ѷᱸॺ", a_),
					ClipboardData.b("㭴ᡶ⩸ེѼ፾", a_)
				};
				Style.BuiltinStyleLoader.ᜄ = new string[]
				{
					ClipboardData.b("㭴ᡶ୸ᙺᱼ፾ꆀ힂", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾욀", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾튀", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾튀꾎킐붜꺞", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾튀꾎킐붜궞", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾튀꾎킐붜겞", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾튀꾎킐붜ꮞ", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾튀꾎킐붜ꪞ", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾튀꾎킐붜ꦞ", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾춀", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾춀ꦈ쪊ﶒ랖ꢘ", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾춀ꦈ쪊ﶒ랖ꮘ", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾춀ꦈ쪊ﶒ랖ꪘ", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾춀ꦈ쪊ﶒ랖궘", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾춀ꦈ쪊ﶒ랖겘", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾춀ꦈ쪊ﶒ랖꾘", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾욀", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾욀ꦈ쪊ﶒ랖ꢘ", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾욀ꦈ쪊ﶒ랖ꮘ", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾욀ꦈ쪊ﶒ랖ꪘ", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾욀ꦈ쪊ﶒ랖궘", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾욀ꦈ쪊ﶒ랖겘", a_),
					ClipboardData.b("㥴ṶṸ፺ॼ彾욀ꦈ쪊ﶒ랖꾘", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꊒ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꊒ떔횖滛햠莢钤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꊒ떔횖滛햠莢鞤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꊒ떔횖滛햠莢隤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꊒ떔횖滛햠莢醤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꊒ떔횖滛햠莢邤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꊒ떔횖滛햠莢鎤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꆒ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꆒ떔횖滛햠莢钤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꆒ떔횖滛햠莢鞤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꆒ떔횖滛햠莢隤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꆒ떔횖滛햠莢醤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꆒ떔횖滛햠莢邤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ킂놐ꆒ떔횖滛햠莢鎤", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ벌", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ벌꾎킐붜꺞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ벌꾎킐붜궞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ벌꾎킐붜겞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ벌꾎킐붜ꮞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ벌꾎킐붜ꪞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ벌꾎킐붜ꦞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ뾌", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ뾌꾎킐붜꺞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ뾌꾎킐붜궞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ뾌꾎킐붜겞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ뾌꾎킐붜ꮞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ뾌꾎킐붜ꪞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쾂ﶈꮊ뾌꾎킐붜ꦞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ벌", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ벌꾎킐붜꺞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ벌꾎킐붜궞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ벌꾎킐붜겞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ벌꾎킐붜ꮞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ벌꾎킐붜ꪞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ벌꾎킐붜ꦞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뾌", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뾌꾎킐붜꺞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뾌꾎킐붜궞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뾌꾎킐붜겞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뾌꾎킐붜ꮞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뾌꾎킐붜ꪞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뾌꾎킐붜ꦞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뺌", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뺌꾎킐붜꺞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뺌꾎킐붜궞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뺌꾎킐붜겞", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뺌꾎킐붜ꮞ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뺌꾎킐ꢜ", a_),
					ClipboardData.b("㡴ቶᵸቺࡼቾꆀ쒂ꮊ뺌꾎킐붜ꦞ", a_),
					ClipboardData.b("ㅴᙶ୸ၺ嵼㍾", a_),
					ClipboardData.b("ㅴᙶ୸ၺ嵼㍾Ꞇ좈ﾐ떔Ꚗ", a_),
					ClipboardData.b("ㅴᙶ୸ၺ嵼㍾Ꞇ좈ﾐ떔ꖖ", a_),
					ClipboardData.b("ㅴᙶ୸ၺ嵼㍾Ꞇ좈ﾐ떔꒖", a_),
					ClipboardData.b("ㅴᙶ୸ၺ嵼㍾Ꞇ좈ﾐ떔ꎖ", a_),
					ClipboardData.b("ㅴᙶ୸ၺ嵼㍾Ꞇ좈ﾐ떔ꊖ", a_),
					ClipboardData.b("ㅴᙶ୸ၺ嵼㍾Ꞇ좈ﾐ떔ꆖ", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ풆ﾐ", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ풆ﾐ떔횖滛햠莢钤", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ풆ﾐ떔횖滛햠莢鞤", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ풆ﾐ떔횖滛햠莢隤", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ풆ﾐ떔횖滛햠莢醤", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ풆ﾐ떔횖滛햠莢邤", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ풆ﾐ떔횖滛햠莢鎤", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ쮆歷", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ쮆歷꾎킐붜꺞", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ쮆歷꾎킐붜궞", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ쮆歷꾎킐붜겞", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ쮆歷꾎킐붜ꮞ", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ쮆歷꾎킐붜ꪞ", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ쮆歷꾎킐붜ꦞ", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ삆ﮈ", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ삆ﮈ꾎킐붜꺞", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ삆ﮈ꾎킐붜궞", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ삆ﮈ꾎킐붜겞", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ삆ﮈ꾎킐붜ꮞ", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ삆ﮈ꾎킐붜ꪞ", a_),
					ClipboardData.b("㙴ᡶᕸᑺོ᥾ꖄ삆ﮈ꾎킐붜ꦞ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾늀잂ꖄ떔Ꚗ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾늀잂ꖄ떔ꖖ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾늀잂ꖄ떔꒖", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀愈꾎ꂐ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀愈꾎ꎐ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀愈꾎ꊐ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀愈꾎ꖐ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀ﮈ놐ꊒ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀ﮈ놐ꆒ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀ﮈ놐ꂒ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀ﺌ꾎ꂐ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀ﺌ꾎ꎐ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀ﺌ꾎ꊐ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀ﺌ꾎ꖐ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀ﺌ꾎꒐", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾슀ﶌ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾쒀歷", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾욀ꦈ몊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾욀ꦈ릊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾욀ꦈ뢊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾욀ꦈ뾊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾욀ꦈ뺊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾욀ꦈ붊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾욀ꦈ벊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾욀ꦈ뎊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾춀ꦈ몊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾춀ꦈ릊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾춀ꦈ뢊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾춀ꦈ뾊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾춀ꦈ뺊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾춀ꦈ붊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾춀ꦈ벊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾춀ꦈ뎊", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾톀ﺌﺐﶒﮖ", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾튀권뺎", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾튀권붎", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾튀권벎", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾튀권뺎", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾튀권붎", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾햀", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾횀Ꞇ뢈", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾횀Ꞇ뮈", a_),
					ClipboardData.b("ⅴᙶ᭸᝺᡼彾횀Ꞇ몈", a_)
				};
				Style.BuiltinStyleLoader.ᜅ = null;
			}

			// Token: 0x04002FAE RID: 12206
			private float \u2593\u009Eª\u0092;

			// Token: 0x04002FAF RID: 12207
			private const string ᜀ = "Spire.Doc.Resources";

			// Token: 0x04002FB0 RID: 12208
			private const string ᜁ = "builtin-styles";

			// Token: 0x04002FB1 RID: 12209
			private const int ᜂ = 10;

			// Token: 0x04002FB2 RID: 12210
			internal static readonly string[] ᜃ;

			// Token: 0x04002FB3 RID: 12211
			private bool \u2460\u0080\u00A5\u0081;

			// Token: 0x04002FB4 RID: 12212
			private long \u2460\u0095\u0086\u00A7;

			// Token: 0x04002FB5 RID: 12213
			internal static readonly string[] ᜄ;

			// Token: 0x04002FB6 RID: 12214
			[ThreadStatic]
			private static Stream ᜅ;
		}
	}
}
