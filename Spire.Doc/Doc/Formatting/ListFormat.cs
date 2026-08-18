using System;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Interface;

namespace Spire.Doc.Formatting
{
	// Token: 0x02000473 RID: 1139
	public class ListFormat : FormatBase
	{
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06003EBD RID: 16061 RVA: 0x0039F3C8 File Offset: 0x0039E3C8
		// (set) Token: 0x06003EBE RID: 16062 RVA: 0x0039F410 File Offset: 0x0039E410
		public int ListLevelNumber
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
				return (int)base[0];
			}
			set
			{
				int a_ = 2;
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
							goto IL_53;
						default:
							if (false)
							{
							}
							if (value < 0)
							{
								num = 2;
								continue;
							}
							goto IL_85;
						}
						break;
					case 1:
						goto IL_53;
					case 2:
						goto IL_83;
					}
					if (value <= 8)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					break;
					IL_53:
					num = 0;
				}
				IL_3F:
				throw new ArgumentException(ClipboardData.b("⑧ͩὫᩭ偯ṱᅳuᵷᙹ屻፽ꚅ겋뚕ꂗ몙ﶛ쒟芡쎣풥춧쮩\ud8ab쮭슯銱삳\udeb5\uddb7풹鲻躽", a_));
				IL_83:
				goto IL_3F;
				IL_85:
				base[0] = value;
				ListFormat.ᜊ = value;
			}
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06003EBF RID: 16063 RVA: 0x0039F4C0 File Offset: 0x0039E4C0
		public ListType ListType
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
				return (ListType)base[1];
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06003EC0 RID: 16064 RVA: 0x0039F508 File Offset: 0x0039E508
		// (set) Token: 0x06003EC1 RID: 16065 RVA: 0x0039F550 File Offset: 0x0039E550
		public bool IsRestartNumbering
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
				return (bool)base[3];
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
				base[3] = value;
			}
		}

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06003EC2 RID: 16066 RVA: 0x0039F598 File Offset: 0x0039E598
		public string CustomStyleName
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
				return (string)base[2];
			}
		}

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06003EC3 RID: 16067 RVA: 0x0039F5E0 File Offset: 0x0039E5E0
		public ListStyle CurrentListStyle
		{
			get
			{
				if (!((string)base[2] != string.Empty))
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
						return null;
					}
				}
				if (true)
				{
				}
				return base.Document.ListStyles.FindByName(this.CustomStyleName);
			}
		}

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06003EC4 RID: 16068 RVA: 0x0039F650 File Offset: 0x0039E650
		public ListLevel CurrentListLevel
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_40;
					case 2:
						goto IL_9D;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A1;
						default:
							if (true)
							{
							}
							if (false)
							{
							}
							if (this.ListLevelNumber >= this.CurrentListStyle.Levels.Count)
							{
								num = 2;
								continue;
							}
							goto IL_A1;
						}
						break;
					}
					if ((string)base[2] == string.Empty)
					{
						num = 1;
					}
					else
					{
						num = 3;
					}
				}
				IL_40:
				return null;
				IL_9D:
				return null;
				IL_A1:
				return this.CurrentListStyle.Levels[this.ListLevelNumber];
			}
		}

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06003EC5 RID: 16069 RVA: 0x0039F714 File Offset: 0x0039E714
		// (set) Token: 0x06003EC6 RID: 16070 RVA: 0x0039F75C File Offset: 0x0039E75C
		internal string LFOStyleName
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
				return (string)base[4];
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
				base[4] = value;
			}
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06003EC7 RID: 16071 RVA: 0x0039F7A0 File Offset: 0x0039E7A0
		internal Paragraph OwnerParagraph
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
				return (Paragraph)base.OwnerBase;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06003EC8 RID: 16072 RVA: 0x0039F7E8 File Offset: 0x0039E7E8
		// (set) Token: 0x06003EC9 RID: 16073 RVA: 0x0039F82C File Offset: 0x0039E82C
		internal bool IsListRemoved
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
				return this.ᜋ;
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
				this.ᜋ = value;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06003ECA RID: 16074 RVA: 0x0039F870 File Offset: 0x0039E870
		// (set) Token: 0x06003ECB RID: 16075 RVA: 0x0039F8B8 File Offset: 0x0039E8B8
		internal string NewStyleName
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
				return (string)base[6];
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
				base[6] = value;
			}
		}

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06003ECC RID: 16076 RVA: 0x0039F8FC File Offset: 0x0039E8FC
		// (set) Token: 0x06003ECD RID: 16077 RVA: 0x0039F944 File Offset: 0x0039E944
		internal string NewLfoStyleName
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
				return (string)base[7];
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
				base[7] = value;
			}
		}

		// Token: 0x1700033B RID: 827
		// (get) Token: 0x06003ECE RID: 16078 RVA: 0x0039F988 File Offset: 0x0039E988
		// (set) Token: 0x06003ECF RID: 16079 RVA: 0x0039F9D0 File Offset: 0x0039E9D0
		internal int NewListLevelNumber
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
				return (int)base[5];
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
				base[5] = value;
			}
		}

		// Token: 0x1700033C RID: 828
		// (get) Token: 0x06003ED0 RID: 16080 RVA: 0x0039FA18 File Offset: 0x0039EA18
		// (set) Token: 0x06003ED1 RID: 16081 RVA: 0x0039FA5C File Offset: 0x0039EA5C
		internal bool IsEmptyList
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
				return this.ᜌ;
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
				this.ᜌ = value;
			}
		}

		// Token: 0x06003ED2 RID: 16082 RVA: 0x0039FAA0 File Offset: 0x0039EAA0
		public ListFormat(IParagraph owner) : base(owner.Document, (DocumentObject)owner)
		{
		}

		// Token: 0x06003ED3 RID: 16083 RVA: 0x0039FAC0 File Offset: 0x0039EAC0
		public ListFormat(Document doc, ParagraphStyle owner) : base(doc)
		{
			base.ᜀ(owner);
		}

		// Token: 0x06003ED4 RID: 16084 RVA: 0x0039FADC File Offset: 0x0039EADC
		internal ListFormat(Document A_0, spr\u173A A_1) : base(A_0)
		{
			base.ᜀ(A_1);
		}

		// Token: 0x06003ED5 RID: 16085 RVA: 0x0039FAF8 File Offset: 0x0039EAF8
		protected override object GetDefValue(int key)
		{
			int a_ = 1;
			for (;;)
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 0:
						switch (key)
						{
						case 0:
							goto IL_AF;
						case 1:
							goto IL_68;
						case 2:
						case 6:
							goto IL_A9;
						case 3:
							goto IL_61;
						case 4:
						case 7:
							goto IL_76;
						case 5:
							goto IL_6F;
						default:
							num = 2;
							continue;
						}
						break;
					case 1:
						goto IL_9F;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_76;
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
				}
			}
			IL_61:
			return false;
			IL_68:
			return ListType.NoList;
			IL_6F:
			return -1;
			IL_76:
			return null;
			IL_9F:
			if (true)
			{
			}
			throw new ArgumentException(ClipboardData.b("౦౨ቪ䵬ݮၰr啴Ṷ᝸ൺᱼ፾ꖄ", a_));
			IL_A9:
			return string.Empty;
			IL_AF:
			return 0;
		}

		// Token: 0x06003ED6 RID: 16086 RVA: 0x0039FBD0 File Offset: 0x0039EBD0
		protected override void WriteXmlAttributes(IXDLSAttributeWriter writer)
		{
			int a_ = 13;
			for (;;)
			{
				if (true)
				{
				}
				base.WriteXmlAttributes(writer);
				int num = 11;
				for (;;)
				{
					switch (num)
					{
					case 0:
						writer.WriteValue(ClipboardData.b("㽲፴ᡶ⩸ེѼ፾춂", a_), this.LFOStyleName);
						num = 5;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_58;
						default:
							if (false)
							{
							}
							if (base.HasKey(4))
							{
								num = 0;
								continue;
							}
							return;
						}
						break;
					case 2:
						goto IL_136;
					case 3:
						if (base.HasKey(2))
						{
							num = 6;
							continue;
						}
						goto IL_9D;
					case 4:
						goto IL_9D;
					case 5:
						return;
					case 6:
						writer.WriteValue(ClipboardData.b("㵲ᑴ᩶ᱸ", a_), this.CustomStyleName);
						num = 4;
						continue;
					case 7:
						writer.WriteValue(ClipboardData.b("㽲ၴŶᱸ᝺㍼੾", a_), this.ListLevelNumber);
						num = 9;
						continue;
					case 8:
						if (base.HasKey(1))
						{
							num = 10;
							continue;
						}
						goto IL_136;
					case 9:
						goto IL_C1;
					case 10:
						writer.WriteValue(ClipboardData.b("㽲ᱴѶ൸⽺Ѽཾ", a_), this.ListType);
						num = 2;
						continue;
					case 11:
						goto IL_58;
					}
					break;
					IL_58:
					if (base.HasKey(0))
					{
						num = 7;
						continue;
					}
					goto IL_C1;
					IL_9D:
					num = 8;
					continue;
					IL_C1:
					num = 3;
					continue;
					IL_136:
					num = 1;
				}
			}
		}

		// Token: 0x06003ED7 RID: 16087 RVA: 0x0039FD88 File Offset: 0x0039ED88
		protected override void ReadXmlAttributes(IXDLSAttributeReader reader)
		{
			int a_ = 3;
			for (;;)
			{
				base.ReadXmlAttributes(reader);
				int num = 6;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (reader.HasAttribute(ClipboardData.b("╨๪᭬੮ᵰ㵲t᩶᭸Ṻོ", a_)))
						{
							num = 4;
							continue;
						}
						goto IL_9E;
					case 1:
						base[1] = (ListType)reader.ReadEnum(ClipboardData.b("╨ɪṬ᭮╰ੲմቶ", a_), typeof(ListType));
						num = 3;
						continue;
					case 2:
						goto IL_9E;
					case 3:
						return;
					case 4:
						base[0] = reader.ReadInt(ClipboardData.b("╨๪᭬੮ᵰ㵲t᩶᭸Ṻོ", a_));
						num = 2;
						continue;
					case 5:
						goto IL_169;
					case 6:
						goto IL_50;
					case 7:
						if (reader.HasAttribute(ClipboardData.b("❨੪l੮", a_)))
						{
							num = 9;
							continue;
						}
						goto IL_169;
					case 8:
						goto IL_CF;
					case 9:
						base[2] = reader.ReadString(ClipboardData.b("❨੪l੮", a_));
						num = 5;
						continue;
					case 10:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_50;
						}
						if (true)
						{
						}
						if (false)
						{
						}
						if (reader.HasAttribute(ClipboardData.b("╨ɪṬ᭮╰ੲմቶ", a_)))
						{
							num = 1;
							continue;
						}
						return;
					case 11:
						this.LFOStyleName = reader.ReadString(ClipboardData.b("╨൪ɬ㱮հੲᥴቶ㝸᩺ၼ᩾", a_));
						num = 8;
						continue;
					}
					break;
					IL_50:
					if (reader.HasAttribute(ClipboardData.b("╨൪ɬ㱮հੲᥴቶ㝸᩺ၼ᩾", a_)))
					{
						num = 11;
						continue;
					}
					goto IL_CF;
					IL_9E:
					num = 7;
					continue;
					IL_CF:
					num = 0;
					continue;
					IL_169:
					num = 10;
				}
			}
		}

		// Token: 0x06003ED8 RID: 16088 RVA: 0x0039FF90 File Offset: 0x0039EF90
		public void IncreaseIndentLevel()
		{
			int a_ = 1;
			if (ListFormat.ᜊ != 8)
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
					base[0] = ++ListFormat.ᜊ;
					return;
				}
			}
			throw new ArgumentException(ClipboardData.b("⭦hᡪᥬ佮ᵰᙲʹቶᕸ孺ၼ੾ꖄꮊ떔꾖릘漢ﮞ膠쒢힤슦좨\udfaa좬\uddae醰잲\uddb4튶ힸ鮺趼", a_));
		}

		// Token: 0x06003ED9 RID: 16089 RVA: 0x003A000C File Offset: 0x0039F00C
		public void DecreaseIndentLevel()
		{
			int a_ = 9;
			if (ListFormat.ᜊ != 0)
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
					base[0] = --ListFormat.ᜊ;
					return;
				}
			}
			throw new ArgumentException(ClipboardData.b("⍮ᡰrŴ坶ᕸṺ୼᩾ꎂ愈ﾊ권뎒璉붜Ꞟ膠슢쮤쎦覨첪\udfac쪮킰잲킴얶馸쾺햼\udabe꿀", a_));
		}

		// Token: 0x06003EDA RID: 16090 RVA: 0x003A0088 File Offset: 0x0039F088
		public void ContinueListNumbering()
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
			this.ApplyStyle(ListFormat.ᜉ);
			this.ListLevelNumber = ListFormat.ᜊ;
		}

		// Token: 0x06003EDB RID: 16091 RVA: 0x003A00DC File Offset: 0x0039F0DC
		public void ApplyStyle(string styleName)
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
			base[2] = styleName;
			ListFormat.ᜉ = styleName;
			base[1] = this.CurrentListStyle.ListType;
		}

		// Token: 0x06003EDC RID: 16092 RVA: 0x003A013C File Offset: 0x0039F13C
		public void ApplyBulletStyle()
		{
			int a_ = 6;
			switch (1 == 1)
			{
			}
			if (false)
			{
			}
			if (true)
			{
			}
			this.ApplyStyle(ClipboardData.b("⹫᭭ᱯṱᅳɵᵷṹ", a_));
		}

		// Token: 0x06003EDD RID: 16093 RVA: 0x003A0194 File Offset: 0x0039F194
		public void ApplyNumberedStyle()
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
			this.ApplyStyle(ClipboardData.b("㩳͵ᕷ᡹᥻౽", a_));
		}

		// Token: 0x06003EDE RID: 16094 RVA: 0x003A01EC File Offset: 0x0039F1EC
		public void RemoveList()
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
			base[2] = string.Empty;
			ListFormat.ᜉ = string.Empty;
			base[1] = ListType.NoList;
			this.ᜋ = true;
		}

		// Token: 0x04002DC1 RID: 11713
		internal new const int ᜀ = 0;

		// Token: 0x04002DC2 RID: 11714
		private const int ᜁ = 1;

		// Token: 0x04002DC3 RID: 11715
		private new const int ᜂ = 2;

		// Token: 0x04002DC4 RID: 11716
		private new const int ᜃ = 3;

		// Token: 0x04002DC5 RID: 11717
		private float \u25D9\u0088\u009F\u008B;

		// Token: 0x04002DC6 RID: 11718
		private new const int ᜄ = 4;

		// Token: 0x04002DC7 RID: 11719
		private const int ᜅ = 5;

		// Token: 0x04002DC8 RID: 11720
		private const int ᜆ = 6;

		// Token: 0x04002DC9 RID: 11721
		private const int ᜇ = 7;

		// Token: 0x04002DCA RID: 11722
		internal const int ᜈ = 1720085641;

		// Token: 0x04002DCB RID: 11723
		[ThreadStatic]
		private new static string ᜉ;

		// Token: 0x04002DCC RID: 11724
		[ThreadStatic]
		private new static int ᜊ;

		// Token: 0x04002DCD RID: 11725
		private bool ᜋ;

		// Token: 0x04002DCE RID: 11726
		private bool ᜌ;
	}
}
