using System;
using System.Collections;
using Spire.CompoundFile.Doc;
using Spire.Doc.Documents;
using Spire.Doc.Documents.XML;
using Spire.Doc.Fields;

namespace Spire.Doc
{
	// Token: 0x02000098 RID: 152
	public class HeadersFooters : DocumentSerializable, IEnumerable
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000121 RID: 289 RVA: 0x0000E5EC File Offset: 0x0000D5EC
		public HeaderFooter Header
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
				return this.OddHeader;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000122 RID: 290 RVA: 0x0000E630 File Offset: 0x0000D630
		public HeaderFooter Footer
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
				return this.OddFooter;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000E674 File Offset: 0x0000D674
		public HeaderFooter EvenHeader
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
				return this.ᜀ;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000E6B8 File Offset: 0x0000D6B8
		public HeaderFooter OddHeader
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
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000E6FC File Offset: 0x0000D6FC
		public HeaderFooter EvenFooter
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

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000E740 File Offset: 0x0000D740
		public HeaderFooter OddFooter
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

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000127 RID: 295 RVA: 0x0000E784 File Offset: 0x0000D784
		public HeaderFooter FirstPageHeader
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
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000128 RID: 296 RVA: 0x0000E7C8 File Offset: 0x0000D7C8
		public HeaderFooter FirstPageFooter
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
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000129 RID: 297 RVA: 0x0000E80C File Offset: 0x0000D80C
		public bool IsEmpty
		{
			get
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 8;
						continue;
					case 1:
						num = 9;
						continue;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_6A;
						default:
							if (false)
							{
							}
							if (this.ᜂ.ChildObjects.Count == 0)
							{
								num = 6;
								continue;
							}
							return false;
						}
						break;
					case 4:
						goto IL_94;
					case 5:
						num = 3;
						continue;
					case 6:
						if (true)
						{
						}
						num = 7;
						continue;
					case 7:
						goto IL_6A;
					case 8:
						if (this.ᜁ.ChildObjects.Count == 0)
						{
							num = 5;
							continue;
						}
						return false;
					case 9:
						if (this.ᜃ.ChildObjects.Count == 0)
						{
							num = 0;
							continue;
						}
						return false;
					}
					if (this.ᜀ.ChildObjects.Count == 0)
					{
						num = 1;
						continue;
					}
					return false;
					IL_6A:
					if (this.ᜅ.ChildObjects.Count != 0)
					{
						return false;
					}
					num = 4;
				}
				IL_94:
				return this.ᜄ.ChildObjects.Count == 0;
			}
		}

		// Token: 0x17000078 RID: 120
		public HeaderFooter this[int index]
		{
			get
			{
				int a_ = 10;
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						if (index > 5)
						{
							num = 2;
							continue;
						}
						goto IL_A0;
					case 2:
						goto IL_41;
					case 3:
						num = 1;
						continue;
					}
					IL_29:
					if (index >= 0)
					{
						num = 3;
						continue;
					}
					IL_41:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_29;
					default:
						goto IL_57;
					}
				}
				IL_57:
				if (false)
				{
				}
				throw new ArgumentOutOfRangeException(ClipboardData.b("㥯ᱱၳ፵w", a_), ClipboardData.b("㥯ᱱၳ፵w婹ύώꢇ꺍ﲏ뢗ꪙ벛튟芡쎣풥춧쮩\ud8ab쮭슯銱膳", a_));
				IL_A0:
				return this[(HeaderFooterType)index];
			}
		}

		// Token: 0x17000079 RID: 121
		public HeaderFooter this[HeaderFooterType hfType]
		{
			get
			{
				int a_ = 15;
				for (;;)
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_55:
						switch (hfType)
						{
						case HeaderFooterType.HeaderEven:
							goto IL_8B;
						case HeaderFooterType.HeaderOdd:
							goto IL_AD;
						case HeaderFooterType.FooterEven:
							goto IL_84;
						case HeaderFooterType.FooterOdd:
							goto IL_7D;
						case HeaderFooterType.HeaderFirstPage:
							goto IL_A6;
						case HeaderFooterType.FooterFirstPage:
							goto IL_92;
						default:
							num = 0;
							break;
						}
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
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							num = 1;
							continue;
						case 1:
							goto IL_A4;
						case 2:
							goto IL_55;
						}
						break;
					}
				}
				IL_7D:
				return this.OddFooter;
				IL_84:
				return this.EvenFooter;
				IL_8B:
				return this.EvenHeader;
				IL_92:
				return this.FirstPageFooter;
				IL_A4:
				throw new ArgumentException(ClipboardData.b("㱴᥶ླྀ᩺ᅼᙾꎂﶎ뺐杖ﺚ뾞햠\udaa2햤슦", a_), ClipboardData.b("ᵴᅶ⵸ɺർ᩾", a_));
				IL_A6:
				return this.FirstPageHeader;
				IL_AD:
				return this.OddHeader;
			}
			internal set
			{
				int a_ = 19;
				for (;;)
				{
					int num;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						IL_55:
						switch (A_0)
						{
						case HeaderFooterType.HeaderEven:
							goto IL_8D;
						case HeaderFooterType.HeaderOdd:
							goto IL_B2;
						case HeaderFooterType.FooterEven:
							goto IL_85;
						case HeaderFooterType.FooterOdd:
							goto IL_7D;
						case HeaderFooterType.HeaderFirstPage:
							goto IL_AA;
						case HeaderFooterType.FooterFirstPage:
							goto IL_95;
						default:
							num = 0;
							break;
						}
						break;
					default:
						if (true)
						{
						}
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
							num = 2;
							continue;
						case 1:
							goto IL_55;
						case 2:
							goto IL_A8;
						}
						break;
					}
				}
				IL_7D:
				this.ᜁ = value;
				return;
				IL_85:
				this.ᜃ = value;
				return;
				IL_8D:
				this.ᜀ = value;
				return;
				IL_95:
				this.ᜅ = value;
				return;
				IL_A8:
				throw new ArgumentException(ClipboardData.b("へᕺ୼ṾꞆ몔爵펠莢톤\udea6\ud9a8캪", a_), ClipboardData.b("ᅸᵺ⥼پ", a_));
				IL_AA:
				this.ᜄ = value;
				return;
				IL_B2:
				this.ᜂ = value;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000EBE4 File Offset: 0x0000DBE4
		// (set) Token: 0x0600012E RID: 302 RVA: 0x0000EC38 File Offset: 0x0000DC38
		public bool LinkToPrevious
		{
			get
			{
				while (this.ᜈ)
				{
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						continue;
					}
					if (false)
					{
					}
					return this.ᜇ;
				}
				if (true)
				{
				}
				return this.ᜀ();
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
				this.ᜀ(value);
			}
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000EC7C File Offset: 0x0000DC7C
		internal HeadersFooters(Section A_0) : base(A_0.Document, A_0)
		{
			this.ᜀ = new HeaderFooter(A_0, HeaderFooterType.HeaderEven);
			this.ᜂ = new HeaderFooter(A_0, HeaderFooterType.HeaderOdd);
			this.ᜃ = new HeaderFooter(A_0, HeaderFooterType.FooterEven);
			this.ᜁ = new HeaderFooter(A_0, HeaderFooterType.FooterOdd);
			this.ᜅ = new HeaderFooter(A_0, HeaderFooterType.FooterFirstPage);
			this.ᜄ = new HeaderFooter(A_0, HeaderFooterType.HeaderFirstPage);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000ECE4 File Offset: 0x0000DCE4
		protected override void InitXDLSHolder()
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
			base.XDLSHolder.AddElement(ClipboardData.b("ᕯѱᅳᡵ啷ቹ᥻ώ", a_), this.EvenHeader);
			base.XDLSHolder.AddElement(ClipboardData.b("Ὧᙱၳ孵ၷόᵻ᩽", a_), this.OddHeader);
			base.XDLSHolder.AddElement(ClipboardData.b("ᕯѱᅳᡵ啷ᱹ፻ᅽ", a_), this.EvenFooter);
			base.XDLSHolder.AddElement(ClipboardData.b("Ὧᙱၳ孵ṷᕹ፻੽", a_), this.OddFooter);
			base.XDLSHolder.AddElement(ClipboardData.b("ᙯ᭱ٳյ౷坹౻ώꦃ", a_), this.FirstPageHeader);
			base.XDLSHolder.AddElement(ClipboardData.b("ᙯ᭱ٳյ౷坹౻ώꦃ", a_), this.FirstPageFooter);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000EDE4 File Offset: 0x0000DDE4
		internal HeadersFooters ᜃ()
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
			return (HeadersFooters)this.CloneImpl();
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000EE2C File Offset: 0x0000DE2C
		protected override object CloneImpl()
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
			HeadersFooters headersFooters = (HeadersFooters)base.CloneImpl();
			headersFooters.ᜀ = (HeaderFooter)this.ᜀ.Clone();
			headersFooters.ᜂ = (HeaderFooter)this.ᜂ.Clone();
			headersFooters.ᜃ = (HeaderFooter)this.ᜃ.Clone();
			headersFooters.ᜁ = (HeaderFooter)this.ᜁ.Clone();
			headersFooters.ᜅ = (HeaderFooter)this.ᜅ.Clone();
			headersFooters.ᜄ = (HeaderFooter)this.ᜄ.Clone();
			headersFooters.ᜇ = this.LinkToPrevious;
			return headersFooters;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000EF04 File Offset: 0x0000DF04
		private HeadersFooters ᜁ()
		{
			Section section;
			HeadersFooters headersFooters;
			for (;;)
			{
				section = ((base.OwnerBase as Section).PreviousSibling as Section);
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					break;
				default:
					if (false)
					{
					}
					num = 4;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						return headersFooters;
					case 1:
						if (!headersFooters.LinkToPrevious)
						{
							num = 0;
							continue;
						}
						section = (section.PreviousSibling as Section);
						num = 2;
						continue;
					case 2:
						goto IL_AD;
					case 3:
						goto IL_C6;
					case 4:
						if (true)
						{
						}
						goto IL_AD;
					case 5:
						if (section == null)
						{
							num = 3;
							continue;
						}
						headersFooters = section.HeadersFooters;
						num = 1;
						continue;
					}
					break;
					IL_AD:
					num = 5;
				}
			}
			return headersFooters;
			IL_C6:
			return section.HeadersFooters;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000EFE0 File Offset: 0x0000DFE0
		internal void ᜂ()
		{
			for (;;)
			{
				int num = 0;
				int num2 = 1;
				for (;;)
				{
					switch (num2)
					{
					case 0:
						goto IL_4C;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							if (false)
							{
							}
							goto IL_4C;
						}
						break;
					case 2:
					{
						if (num >= 6)
						{
							num2 = 3;
							continue;
						}
						HeaderFooter headerFooter = this[num];
						headerFooter.ᜅ();
						num++;
						num2 = 0;
						continue;
					}
					case 3:
						goto IL_60;
					}
					break;
					IL_4C:
					num2 = 2;
				}
			}
			IL_60:
			if (true)
			{
			}
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000F078 File Offset: 0x0000E078
		public IEnumerator GetEnumerator()
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
			return new HeadersFooters.ᜀ(this);
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000F0BC File Offset: 0x0000E0BC
		private bool ᜀ()
		{
			for (;;)
			{
				Section section = base.OwnerBase as Section;
				int num = 12;
				for (;;)
				{
					int num2;
					switch (num)
					{
					case 0:
						goto IL_73;
					case 1:
						this.ᜇ = false;
						num = 2;
						continue;
					case 2:
						goto IL_17D;
					case 3:
						num2 = 0;
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_10C;
						default:
							if (false)
							{
							}
							num = 7;
							continue;
						}
						break;
					case 4:
					{
						int num3;
						if (num3 == 0)
						{
							num = 1;
							continue;
						}
						num = 6;
						continue;
					}
					case 5:
						goto IL_14A;
					case 6:
					{
						int num3;
						if (num3 > 0)
						{
							num = 3;
							continue;
						}
						goto IL_17F;
					}
					case 7:
						goto IL_130;
					case 8:
						goto IL_10C;
					case 9:
						if (num2 >= 6)
						{
							num = 5;
							continue;
						}
						if (true)
						{
						}
						num = 8;
						continue;
					case 10:
						return false;
					case 11:
						this.ᜇ = false;
						num = 0;
						continue;
					case 12:
					{
						if (section == null)
						{
							num = 10;
							continue;
						}
						this.ᜈ = true;
						int num3 = section.ឯ();
						num = 4;
						continue;
					}
					case 13:
						goto IL_130;
					}
					break;
					IL_10C:
					if (this[num2].Items.Count > 0)
					{
						num = 11;
						continue;
					}
					this.ᜇ = true;
					num2++;
					num = 13;
					continue;
					IL_130:
					num = 9;
				}
			}
			return false;
			IL_73:
			IL_14A:
			IL_17D:
			IL_17F:
			return this.ᜇ;
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000F250 File Offset: 0x0000E250
		internal void ᜀ(bool A_0)
		{
			HeadersFooters headersFooters;
			for (;;)
			{
				Section section = base.OwnerBase as Section;
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_E1;
					case 2:
						goto IL_72;
					case 3:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_72;
						default:
						{
							if (true)
							{
							}
							if (false)
							{
							}
							int num2;
							if (num2 == -1)
							{
								num = 2;
								continue;
							}
							num = 9;
							continue;
						}
						}
						break;
					case 4:
					{
						int num2;
						if (num2 == 0)
						{
							num = 8;
							continue;
						}
						headersFooters = this.ᜁ();
						num = 11;
						continue;
					}
					case 5:
						if (this.ᜀ(headersFooters))
						{
							num = 12;
							continue;
						}
						return;
					case 6:
						num = 5;
						continue;
					case 7:
					{
						if (section == null)
						{
							num = 10;
							continue;
						}
						this.ᜈ = true;
						int num2 = section.ឯ();
						num = 3;
						continue;
					}
					case 8:
						return;
					case 9:
					{
						int num2;
						this.ᜇ = (num2 != 0 && A_0);
						num = 4;
						continue;
					}
					case 10:
						return;
					case 11:
						if (A_0)
						{
							num = 6;
							continue;
						}
						this.ᜂ = new HeaderFooter(section, HeaderFooterType.HeaderOdd);
						this.ᜁ = new HeaderFooter(section, HeaderFooterType.FooterOdd);
						this.ᜀ = new HeaderFooter(section, HeaderFooterType.HeaderEven);
						this.ᜃ = new HeaderFooter(section, HeaderFooterType.FooterEven);
						this.ᜄ = new HeaderFooter(section, HeaderFooterType.HeaderFirstPage);
						this.ᜅ = new HeaderFooter(section, HeaderFooterType.FooterFirstPage);
						num = 1;
						continue;
					case 12:
						goto IL_108;
					}
					break;
					IL_72:
					this.ᜇ = A_0;
					num = 0;
				}
			}
			return;
			IL_E1:
			return;
			IL_108:
			this.ᜂ = headersFooters.OddHeader;
			this.ᜁ = headersFooters.OddFooter;
			this.ᜀ = headersFooters.EvenHeader;
			this.ᜃ = headersFooters.EvenFooter;
			this.ᜄ = headersFooters.FirstPageHeader;
			this.ᜅ = headersFooters.FirstPageFooter;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000F460 File Offset: 0x0000E460
		private bool ᜀ(HeadersFooters A_0)
		{
			for (;;)
			{
				int num = 0;
				int num2 = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num2)
					{
					case 0:
						goto IL_6F;
					case 1:
						return false;
					case 2:
						if (num >= 6)
						{
							num2 = 5;
							continue;
						}
						num2 = 3;
						continue;
					case 3:
						if (!this.ᜀ(A_0[num]))
						{
							num2 = 1;
							continue;
						}
						goto IL_34;
					case 4:
						goto IL_6F;
					case 5:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_34;
						default:
							goto IL_9E;
						}
						break;
					}
					break;
					IL_34:
					num++;
					num2 = 4;
					continue;
					IL_6F:
					num2 = 2;
				}
			}
			return false;
			IL_9E:
			if (false)
			{
			}
			return true;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000F514 File Offset: 0x0000E514
		private bool ᜀ(HeaderFooter A_0)
		{
			int num = 0;
			switch (num)
			{
			default:
			{
				IEnumerator enumerator = A_0.Paragraphs.GetEnumerator();
				bool result;
				try
				{
					num = 0;
					for (;;)
					{
						switch (num)
						{
						case 1:
						{
							if (!enumerator.MoveNext())
							{
								num = 2;
								continue;
							}
							Paragraph paragraph = (Paragraph)enumerator.Current;
							IEnumerator enumerator2 = paragraph.Items.GetEnumerator();
							num = 4;
							continue;
						}
						case 2:
							goto IL_1FC;
						case 3:
							goto IL_208;
						case 4:
							try
							{
								num = 10;
								for (;;)
								{
									ParagraphBase paragraphBase;
									switch (num)
									{
									case 0:
										num = 7;
										continue;
									case 1:
										result = false;
										num = 4;
										continue;
									case 2:
									{
										IEnumerator enumerator2;
										if (!enumerator2.MoveNext())
										{
											num = 0;
											continue;
										}
										paragraphBase = (ParagraphBase)enumerator2.Current;
										num = 11;
										continue;
									}
									case 3:
										num = 6;
										continue;
									case 4:
										goto IL_13D;
									case 5:
										goto IL_171;
									case 6:
										if (paragraphBase is TextBox)
										{
											num = 5;
											continue;
										}
										num = 9;
										continue;
									case 7:
										goto IL_192;
									case 9:
										if (paragraphBase is spr\u248F)
										{
											num = 1;
											continue;
										}
										break;
									case 11:
										if (!(paragraphBase is DocPicture))
										{
											num = 3;
											continue;
										}
										goto IL_171;
									}
									IL_10F:
									num = 2;
									continue;
									goto IL_10F;
									IL_171:
									paragraphBase.ᜁ = true;
									num = 8;
								}
								IL_13D:
								return result;
								IL_192:
								break;
							}
							finally
							{
								for (;;)
								{
									IDisposable disposable;
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
										IEnumerator enumerator2;
										disposable = (enumerator2 as IDisposable);
										num = 0;
										break;
									}
									}
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
											goto IL_1FB;
										case 1:
											goto IL_1F9;
										case 2:
											disposable.Dispose();
											num = 1;
											continue;
										}
										break;
									}
								}
								IL_1F9:
								IL_1FB:;
							}
							goto IL_1FC;
						}
						IL_50:
						num = 1;
						continue;
						goto IL_50;
						IL_1FC:
						num = 3;
					}
					IL_208:
					return true;
				}
				finally
				{
					for (;;)
					{
						IDisposable disposable2 = enumerator as IDisposable;
						num = 0;
						for (;;)
						{
							switch (num)
							{
							case 0:
								if (disposable2 != null)
								{
									num = 2;
									continue;
								}
								goto IL_254;
							case 1:
								goto IL_252;
							case 2:
								disposable2.Dispose();
								num = 1;
								continue;
							}
							break;
						}
					}
					IL_252:
					IL_254:
					if (true)
					{
					}
				}
				return result;
			}
			}
		}

		// Token: 0x04000975 RID: 2421
		private new HeaderFooter ᜀ;

		// Token: 0x04000976 RID: 2422
		private HeaderFooter ᜁ;

		// Token: 0x04000977 RID: 2423
		private HeaderFooter ᜂ;

		// Token: 0x04000978 RID: 2424
		private HeaderFooter ᜃ;

		// Token: 0x04000979 RID: 2425
		private HeaderFooter ᜄ;

		// Token: 0x0400097A RID: 2426
		private HeaderFooter ᜅ;

		// Token: 0x0400097B RID: 2427
		private HeadersFooters.ᜀ ᜆ;

		// Token: 0x0400097C RID: 2428
		private bool ᜇ;

		// Token: 0x0400097D RID: 2429
		private bool ᜈ;

		// Token: 0x02000099 RID: 153
		internal new class ᜀ : IEnumerator
		{
			// Token: 0x0600013A RID: 314 RVA: 0x0000F7B4 File Offset: 0x0000E7B4
			internal ᜀ(HeadersFooters A_0)
			{
				this.ᜁ = A_0;
			}

			// Token: 0x0600013B RID: 315 RVA: 0x0000F7D8 File Offset: 0x0000E7D8
			public object ᜁ()
			{
				while (this.ᜀ >= 0)
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
						return this.ᜁ[this.ᜀ];
					}
				}
				return null;
			}

			// Token: 0x0600013C RID: 316 RVA: 0x0000F834 File Offset: 0x0000E834
			public bool ᜀ()
			{
				while (this.ᜀ < 5)
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
						this.ᜀ++;
						return true;
					}
				}
				return false;
			}

			// Token: 0x0600013D RID: 317 RVA: 0x0000F88C File Offset: 0x0000E88C
			public void ᜂ()
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
				this.ᜀ = -1;
			}

			// Token: 0x0400097E RID: 2430
			private int ᜀ = -1;

			// Token: 0x0400097F RID: 2431
			private HeadersFooters ᜁ;
		}
	}
}
