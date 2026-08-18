using System;
using System.Collections.Generic;
using System.Drawing;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000442 RID: 1090
	public class RichTextString : CommonWrapper, IRichTextString
	{
		// Token: 0x0600417F RID: 16767 RVA: 0x0024C6C8 File Offset: 0x0024B6C8
		internal RichTextString(spr\u1DF5 A_0, object A_1)
		{
			int a_ = 8;
			base..ctor();
			if (A_1 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("丽ℿぁ⅃⡅㱇", a_));
			}
			this.ᜅ = A_1;
			this.SetParents();
		}

		// Token: 0x06004180 RID: 16768 RVA: 0x0024C70C File Offset: 0x0024B70C
		internal RichTextString(spr\u1DF5 A_0, object A_1, bool A_2) : this(A_0, A_1, A_2, false)
		{
		}

		// Token: 0x06004181 RID: 16769 RVA: 0x0024C724 File Offset: 0x0024B724
		internal RichTextString(spr\u1DF5 A_0, object A_1, bool A_2, bool A_3) : this(A_0, A_1)
		{
			this.ᜃ = A_2;
			if (A_3)
			{
				this.ᜁ = new spr\u223A();
			}
		}

		// Token: 0x06004182 RID: 16770 RVA: 0x0024C754 File Offset: 0x0024B754
		internal RichTextString(spr\u1DF5 A_0, object A_1, spr\u223A A_2) : this(A_0, A_1)
		{
			this.ᜁ = A_2;
		}

		// Token: 0x06004183 RID: 16771 RVA: 0x0024C770 File Offset: 0x0024B770
		protected virtual void SetParents()
		{
			int a_ = 15;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_5F;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			this.ᜂ = (XlsObject.FindParent(this.ᜅ, typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.ᜂ != null)
			{
				return;
			}
			IL_5F:
			throw new ArgumentNullException(RecordTableEnumerator.b("ᕄ♆㭈⹊⍌㭎煐㱒㝔㵖㱘㡚⥜罞ɠɢ୤०٨Ὢ䵬൮ᑰ卲፴ᡶ౸ᕺ᥼兾", a_));
		}

		// Token: 0x06004184 RID: 16772 RVA: 0x0024C7F4 File Offset: 0x0024B7F4
		public IFont GetFont(int index)
		{
			int a_ = 11;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 1;
					continue;
				case 1:
					if (index >= this.ᜁ.ᜏ().Length)
					{
						num = 3;
						continue;
					}
					goto IL_A1;
				case 3:
					goto IL_3F;
				}
				goto IL_29;
				IL_2D:
				if (true)
				{
				}
				num = 0;
				continue;
				IL_29:
				if (index >= 0)
				{
					goto IL_2D;
				}
				IL_3F:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2D;
				default:
					goto IL_5F;
				}
			}
			IL_5F:
			if (false)
			{
			}
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⡀ⵂ⅄≆ㅈ", a_));
			IL_A1:
			int iFontIndex = this.ᜁ.ᜆ(index);
			XlsFont fontByIndex = this.GetFontByIndex(iFontIndex);
			return new FontWrapper(fontByIndex, true, false);
		}

		// Token: 0x06004185 RID: 16773 RVA: 0x0024C8C0 File Offset: 0x0024B8C0
		public virtual ExcelFont GetFontByPosition(int index)
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
			return new ExcelFont(this.GetFont(index));
		}

		// Token: 0x06004186 RID: 16774 RVA: 0x0024C908 File Offset: 0x0024B908
		protected internal void SetRichTextFont(int iStartPos, int iEndPos, IFont font)
		{
			switch (0)
			{
			default:
				for (;;)
				{
					this.BeginUpdate();
					int num = this.AddFont(font);
					int num2 = 22;
					for (;;)
					{
						XlsFont xlsFont;
						int length;
						int num3;
						int num4;
						switch (num2)
						{
						case 0:
							goto IL_253;
						case 1:
							xlsFont = (font as XlsFont);
							goto IL_15C;
						case 2:
							xlsFont = (font as FontWrapper).Wrapped;
							goto IL_15C;
						case 3:
							if (length >= iEndPos + 1)
							{
								num2 = 8;
								continue;
							}
							num2 = 23;
							continue;
						case 4:
							goto IL_C8;
						case 5:
							goto IL_C8;
						case 6:
							num = 0;
							num2 = 19;
							continue;
						case 7:
							num2 = 20;
							continue;
						case 8:
							if (true)
							{
							}
							num2 = 14;
							continue;
						case 9:
							goto IL_2A3;
						case 10:
							this.ᜁ.ᜇ()[iEndPos + 1] = num3;
							num2 = 13;
							continue;
						case 11:
							goto IL_1A1;
						case 12:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_2A3;
							default:
								if (false)
								{
								}
								if (iEndPos < this.ᜁ.ᜏ().Length - 1)
								{
									num2 = 15;
									continue;
								}
								goto IL_C8;
							}
							break;
						case 13:
							goto IL_2C2;
						case 14:
							num4 = -1;
							goto IL_1F7;
						case 15:
							this.SetFont(iEndPos + 1, this.ᜁ.ᜏ().Length - 1, this.DefaultFont);
							num2 = 4;
							continue;
						case 16:
							if (num < 0)
							{
								num2 = 6;
								continue;
							}
							this.DefaultFontIndex = num;
							num2 = 24;
							continue;
						case 17:
							num2 = 1;
							continue;
						case 18:
							if (!(font is FontWrapper))
							{
								num2 = 17;
								continue;
							}
							num2 = 2;
							continue;
						case 19:
							goto IL_187;
						case 20:
							if (this.ᜁ.ᜆ() > 0)
							{
								num2 = 9;
								continue;
							}
							num2 = 12;
							continue;
						case 21:
							if (num3 >= 0)
							{
								num2 = 10;
								continue;
							}
							goto IL_2C2;
						case 22:
							if (iStartPos == 0)
							{
								num2 = 7;
								continue;
							}
							this.ᜁ.ᜀ(iStartPos, iEndPos, num);
							num2 = 0;
							continue;
						case 23:
							num4 = this.ᜁ.ᜆ(iEndPos + 1);
							goto IL_1F7;
						case 24:
							goto IL_187;
						}
						break;
						IL_C8:
						num2 = 18;
						continue;
						IL_15C:
						XlsFont defaultFont = xlsFont;
						this.DefaultFont = defaultFont;
						num2 = 16;
						continue;
						IL_187:
						this.ᜁ.ᜀ(iStartPos, iEndPos, num);
						num2 = 11;
						continue;
						IL_1F7:
						num3 = num4;
						int a_;
						this.ᜁ.ᜁ(0, a_);
						num2 = 21;
						continue;
						IL_2A3:
						length = this.ᜁ.ᜏ().Length;
						a_ = this.DefaultFontIndex;
						num2 = 3;
						continue;
						IL_2C2:
						this.ᜁ.ᜀ(iStartPos, iEndPos, num);
						num2 = 5;
					}
				}
				IL_1A1:
				IL_253:
				this.EndUpdate();
				return;
			}
		}

		// Token: 0x06004187 RID: 16775 RVA: 0x0024CC50 File Offset: 0x0024BC50
		public virtual void SetFont(int startIndex, int endIndex, IFont font)
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
			this.SetRichTextFont(startIndex, endIndex, font);
		}

		// Token: 0x06004188 RID: 16776 RVA: 0x0024CC94 File Offset: 0x0024BC94
		public void ClearFormatting()
		{
			int num = 4;
			for (;;)
			{
				switch (num)
				{
				case 0:
					this.BeginUpdate();
					this.ᜁ.ᜉ();
					this.EndUpdate();
					num = 1;
					continue;
				case 1:
					return;
				case 2:
					if (this.IsFormatted)
					{
						num = 0;
						continue;
					}
					return;
				case 3:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 2;
						continue;
					}
					break;
				}
				if (this.ᜁ == null)
				{
					break;
				}
				if (true)
				{
				}
				num = 3;
			}
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06004189 RID: 16777 RVA: 0x0024CD48 File Offset: 0x0024BD48
		// (set) Token: 0x0600418A RID: 16778 RVA: 0x0024CDD4 File Offset: 0x0024BDD4
		public string Text
		{
			get
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
							continue;
						default:
							if (false)
							{
							}
							this.ᜁ = new spr\u223A();
							num = 1;
							continue;
						}
						break;
					case 1:
						goto IL_6F;
					}
					if (true)
					{
					}
					if (this.ᜁ != null)
					{
						break;
					}
					num = 0;
				}
				IL_6F:
				return this.ᜁ.ᜏ();
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
				this.BeginUpdate();
				this.ᜁ.ᜁ(value);
				this.ᜁ.ᜉ();
				this.EndUpdate();
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x0600418B RID: 16779 RVA: 0x0024CE34 File Offset: 0x0024BE34
		public string RtfText
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
				return this.ᜉ();
			}
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x0600418C RID: 16780 RVA: 0x0024CE78 File Offset: 0x0024BE78
		public bool IsFormatted
		{
			get
			{
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_2E;
				}
				if (false)
				{
				}
				if (this.ᜁ == null)
				{
					return false;
				}
				IL_2E:
				if (true)
				{
				}
				return this.ᜁ.ᜆ() > 0;
			}
		}

		// Token: 0x0600418D RID: 16781 RVA: 0x0024CED0 File Offset: 0x0024BED0
		public void Append(string text, IFont font)
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
			this.BeginUpdate();
			int length = this.ᜁ.ᜏ().Length;
			spr\u223A spr_u223A = this.ᜁ;
			spr_u223A.ᜁ(spr_u223A.ᜏ() + text);
			this.SetFont(length, length + text.Length - 1, font);
			this.EndUpdate();
		}

		// Token: 0x0600418E RID: 16782 RVA: 0x0024CF54 File Offset: 0x0024BF54
		internal void ᜁ(int A_0, int A_1)
		{
			for (;;)
			{
				string text = this.ᜁ.ᜏ();
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (A_0 >= text.Length)
						{
							num = 3;
							continue;
						}
						this.ᜁ.ᜅ(A_0);
						num = 1;
						continue;
					case 1:
						goto IL_6B;
					case 2:
						goto IL_AA;
					case 3:
						this.ᜁ.ᜁ(string.Empty);
						this.ClearFormatting();
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
						break;
					case 4:
						if (A_0 > 0)
						{
							num = 5;
							continue;
						}
						goto IL_CD;
					case 5:
						if (true)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_6B:
			IL_AA:
			IL_CD:
			this.ᜁ.ᜂ(this.ᜁ.ᜏ().Length - A_1);
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x0024D04C File Offset: 0x0024C04C
		internal void ᜁ(string A_0)
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
			this.BeginUpdate();
			this.ᜁ.ᜏ();
			this.ᜁ.ᜁ(A_0);
			this.EndUpdate();
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x0024D0AC File Offset: 0x0024C0AC
		public object Parent
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

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x0024D0F0 File Offset: 0x0024C0F0
		internal spr\u1DF5 Application
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
				return this.ᜂ.ReservedHandle;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x0024D138 File Offset: 0x0024C138
		public SizeF StringSize
		{
			get
			{
				switch (0)
				{
				default:
				{
					if (true)
					{
					}
					SizeF result;
					int a_;
					SizeF sizeF;
					for (;;)
					{
						result = new SizeF(0f, 0f);
						a_ = 0;
						int num = 0;
						int num2 = this.ᜁ.ᜆ();
						int num3 = 0;
						for (;;)
						{
							switch (num3)
							{
							case 0:
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									goto IL_92;
								default:
									if (false)
									{
									}
									goto IL_79;
								}
								break;
							case 1:
								goto IL_79;
							case 2:
							{
								if (num >= num2)
								{
									goto IL_92;
								}
								int num4 = this.ᜁ.ᜄ(num);
								sizeF = this.ᜀ(a_, num4);
								result.Width += sizeF.Width;
								result.Height = Math.Max(sizeF.Height, result.Height);
								a_ = num4;
								num++;
								num3 = 1;
								continue;
							}
							case 3:
								goto IL_9E;
							}
							break;
							IL_79:
							num3 = 2;
							continue;
							IL_92:
							num3 = 3;
						}
					}
					IL_9E:
					sizeF = this.ᜀ(a_, this.Text.Length);
					result.Width += sizeF.Width;
					result.Height = Math.Max(sizeF.Height, result.Height);
					return result;
				}
				}
			}
		}

		// Token: 0x17000D25 RID: 3365
		// (get) Token: 0x06004193 RID: 16787 RVA: 0x0024D288 File Offset: 0x0024C288
		// (set) Token: 0x06004194 RID: 16788 RVA: 0x0024D2E0 File Offset: 0x0024C2E0
		public virtual XlsFont DefaultFont
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
				return (XlsFont)this.ᜂ.InnerFonts[this.ᜆ];
			}
			internal set
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
				this.ᜆ = value.Index;
			}
		}

		// Token: 0x17000D26 RID: 3366
		// (get) Token: 0x06004195 RID: 16789 RVA: 0x0024D328 File Offset: 0x0024C328
		internal spr\u223A TextObject
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
		}

		// Token: 0x17000D27 RID: 3367
		// (get) Token: 0x06004196 RID: 16790 RVA: 0x0024D36C File Offset: 0x0024C36C
		internal XlsWorkbook Workbook
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
				return this.ᜂ;
			}
		}

		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06004197 RID: 16791 RVA: 0x0024D3B0 File Offset: 0x0024C3B0
		// (set) Token: 0x06004198 RID: 16792 RVA: 0x0024D3F4 File Offset: 0x0024C3F4
		internal int DefaultFontIndex
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
				if (false)
				{
				}
				if (true)
				{
				}
				this.ᜆ = value;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06004199 RID: 16793 RVA: 0x0024D438 File Offset: 0x0024C438
		// (set) Token: 0x0600419A RID: 16794 RVA: 0x0024D47C File Offset: 0x0024C47C
		internal string ImageRTF
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
				this.ᜇ = value;
			}
		}

		// Token: 0x0600419B RID: 16795 RVA: 0x0024D4C0 File Offset: 0x0024C4C0
		protected virtual int GetFontIndex(int iPosition)
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
			return this.ᜁ.ᜃ(iPosition);
		}

		// Token: 0x0600419C RID: 16796 RVA: 0x0024D508 File Offset: 0x0024C508
		protected virtual XlsFont GetFontByIndex(int iFontIndex)
		{
			XlsFont result;
			for (;;)
			{
				result = null;
				int num;
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					IL_5D:
					result = (XlsFont)this.ᜂ.InnerFonts[iFontIndex];
					num = 3;
					break;
				default:
					if (false)
					{
					}
					num = 5;
					break;
				}
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (this.DefaultFontIndex >= 0)
						{
							if (true)
							{
							}
							num = 1;
							continue;
						}
						goto IL_5D;
					case 1:
						result = this.DefaultFont;
						num = 2;
						continue;
					case 2:
						return result;
					case 3:
						return result;
					case 4:
						num = 0;
						continue;
					case 5:
						if (iFontIndex == 0)
						{
							num = 4;
							continue;
						}
						goto IL_5D;
					}
					break;
				}
			}
			return result;
		}

		// Token: 0x0600419D RID: 16797 RVA: 0x0024D5D4 File Offset: 0x0024C5D4
		public override void BeginUpdate()
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_3E;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			if (this.ᜃ)
			{
				throw new spr\u23DE();
			}
			IL_3E:
			base.BeginUpdate();
		}

		// Token: 0x0600419E RID: 16798 RVA: 0x0024D628 File Offset: 0x0024C628
		public override void EndUpdate()
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
			base.EndUpdate();
		}

		// Token: 0x0600419F RID: 16799 RVA: 0x0024D66C File Offset: 0x0024C66C
		public virtual void CopyFrom(RichTextString source, Dictionary<int, int> dicFontIndexes)
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
			this.BeginUpdate();
			this.ᜁ = source.ᜁ.ᜁ(dicFontIndexes);
			this.EndUpdate();
		}

		// Token: 0x060041A0 RID: 16800 RVA: 0x0024D6C8 File Offset: 0x0024C6C8
		internal virtual void Parse(spr\u223A text, Dictionary<int, int> dicFontIndexes, ExcelParseOptions options)
		{
			int a_ = 0;
			switch (0)
			{
			default:
			{
				int num = 6;
				for (;;)
				{
					int num2;
					XlsFontsCollection innerFonts;
					int num3;
					int num4;
					switch (num)
					{
					case 0:
						return;
					case 1:
						goto IL_14F;
					case 2:
						num2 = 0;
						num = 1;
						continue;
					case 3:
						goto IL_FE;
					case 4:
						if (num2 > innerFonts.Count)
						{
							num = 2;
							continue;
						}
						goto IL_14F;
					case 5:
						goto IL_FE;
					case 7:
						num3 = 0;
						num = 3;
						continue;
					case 8:
						goto IL_69;
					case 9:
						if (num4 > 0)
						{
							num = 7;
							continue;
						}
						return;
					case 10:
						if (num3 >= num4)
						{
							num = 0;
							continue;
						}
						num2 = text.ᜃ(num3);
						num2 = XlsFont.ᜀ(num2, dicFontIndexes, options);
						num = 4;
						continue;
					}
					if (text == null)
					{
						if (true)
						{
						}
						num = 8;
						continue;
					}
					this.ᜁ = text.\u170D();
					num2 = 0;
					innerFonts = this.ᜂ.InnerFonts;
					num4 = text.ᜆ();
					num = 9;
					continue;
					IL_FE:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						return;
					default:
						if (false)
						{
						}
						num = 10;
						continue;
					}
					IL_14F:
					this.ᜁ.ᜂ(num3, num2);
					num3++;
					num = 5;
				}
				IL_69:
				throw new ArgumentNullException(RecordTableEnumerator.b("䈵崷䈹䠻", a_));
			}
			}
		}

		// Token: 0x060041A1 RID: 16801 RVA: 0x0024D854 File Offset: 0x0024C854
		public override object Clone(object parent)
		{
			int a_ = 11;
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
				if (parent == null)
				{
					throw new ArgumentNullException(RecordTableEnumerator.b("ㅀ≂㝄≆❈㽊", a_));
				}
				break;
			}
			RichTextString richTextString = (RichTextString)base.Clone(parent);
			richTextString.ᜅ = parent;
			richTextString.ᜁ = this.ᜁ.\u170D();
			this.SetParents();
			return richTextString;
		}

		// Token: 0x060041A2 RID: 16802 RVA: 0x0024D8E0 File Offset: 0x0024C8E0
		public virtual void Clear()
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
			this.ᜁ = this.ᜁ.\u170D();
			this.ᜁ.ᜉ();
			this.ᜁ.ᜁ(string.Empty);
		}

		// Token: 0x060041A3 RID: 16803 RVA: 0x0024D948 File Offset: 0x0024C948
		protected virtual int AddFont(IFont font)
		{
			XlsFont xlsFont;
			for (;;)
			{
				IInternalFont internalFont = (IInternalFont)font;
				xlsFont = null;
				if (true)
				{
				}
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						xlsFont = (internalFont as FontWrapper).Font;
						num = 1;
						continue;
					case 1:
						goto IL_91;
					case 2:
						if (internalFont is FontWrapper)
						{
							num = 0;
							continue;
						}
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_93;
						default:
							if (false)
							{
							}
							xlsFont = (font as XlsFont);
							num = 3;
							continue;
						}
						break;
					case 3:
						goto IL_78;
					}
					break;
				}
			}
			IL_78:
			IL_91:
			IL_93:
			xlsFont = (this.ᜂ.InnerFonts.Add(xlsFont) as XlsFont);
			return xlsFont.Index;
		}

		// Token: 0x060041A4 RID: 16804 RVA: 0x0024DA08 File Offset: 0x0024CA08
		private void ᜀ(RtfTextWriter A_0, string A_1)
		{
			int a_ = 13;
			switch (0)
			{
			default:
			{
				int num = 7;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_CF;
					case 1:
					{
						int num2;
						XlsFont fontByIndex = this.GetFontByIndex(num2);
						this.ᜀ(fontByIndex, A_0);
						num = 2;
						continue;
					}
					case 2:
						goto IL_7A;
					case 3:
						goto IL_78;
					case 4:
						goto IL_CF;
					case 5:
						goto IL_EB;
					case 6:
						goto IL_DB;
					case 8:
					{
						int num2;
						if (num2 != 0)
						{
							num = 1;
							continue;
						}
						goto IL_7A;
					}
					}
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_DB:
						int num4;
						if (num3 >= num4)
						{
							num = 5;
							continue;
						}
						int num2 = this.ᜁ.ᜃ(num3);
						if (true)
						{
						}
						num = 8;
						continue;
					}
					default:
					{
						if (false)
						{
						}
						if (A_0 == null)
						{
							num = 3;
							continue;
						}
						num3 = 0;
						int num4 = this.ᜁ.ᜆ();
						num = 4;
						continue;
					}
					}
					IL_7A:
					num3++;
					num = 0;
					continue;
					IL_CF:
					num = 6;
				}
				IL_78:
				throw new ArgumentNullException(RecordTableEnumerator.b("㑂㝄⹆㵈⹊㽌", a_));
				IL_EB:
				this.ᜀ(this.DefaultFont, A_0);
				A_0.WriteTag(RtfTags.RtfBegin);
				A_0.WriteFontTable();
				A_0.WriteColorTable();
				A_0.ᜃ(A_1);
				return;
			}
			}
		}

		// Token: 0x060041A5 RID: 16805 RVA: 0x0024DB70 File Offset: 0x0024CB70
		internal void ᜀ(spr\u223A A_0)
		{
			int a_ = 9;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_50;
			}
			if (false)
			{
			}
			if (true)
			{
			}
			if (A_0 == null)
			{
				throw new ArgumentNullException(RecordTableEnumerator.b("尾⹀⹂⡄≆❈㽊᥌⩎⥐❒", a_));
			}
			IL_50:
			this.ᜁ = A_0;
		}

		// Token: 0x060041A6 RID: 16806 RVA: 0x0024DBD4 File Offset: 0x0024CBD4
		private SizeF ᜀ(int A_0, int A_1)
		{
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_64;
			}
			if (true)
			{
			}
			if (false)
			{
			}
			if (A_0 < A_1)
			{
				XlsFont xlsFont = this.ᜃ(A_0);
				int length = A_1 - A_0;
				string text = this.Text.Substring(A_0, length);
				text.IndexOfAny(RichTextString.ᜄ);
				return xlsFont.MeasureStringSpecial(text);
			}
			IL_64:
			return new SizeF(0f, 0f);
		}

		// Token: 0x060041A7 RID: 16807 RVA: 0x0024DC54 File Offset: 0x0024CC54
		internal string ᜉ()
		{
			switch (0)
			{
			default:
			{
				RtfTextWriter rtfTextWriter;
				for (;;)
				{
					this.ᜁ.ᜈ();
					rtfTextWriter = new RtfTextWriter();
					this.ᜀ(rtfTextWriter);
					string text = this.ᜁ.ᜏ();
					int num = this.ᜁ.ᜆ();
					int a_ = 0;
					int num2 = 1;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_12F;
						case 1:
							if (text.Length > 0)
							{
								num2 = 6;
								continue;
							}
							goto IL_167;
						case 2:
							goto IL_9D;
						case 3:
						{
							int num3 = 0;
							num2 = 7;
							continue;
						}
						case 4:
							goto IL_142;
						case 5:
						{
							int num3;
							if (num3 > num)
							{
								num2 = 8;
								continue;
							}
							a_ = this.ᜀ(rtfTextWriter, num3, a_);
							num3++;
							num2 = 4;
							continue;
						}
						case 6:
							num2 = 9;
							continue;
						case 7:
							goto IL_142;
						case 8:
							num2 = 2;
							continue;
						case 9:
							if (true)
							{
							}
							if (num > 0)
							{
								num2 = 3;
								continue;
							}
							this.ᜀ(rtfTextWriter, this.DefaultFont.Index, text);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_12F;
							}
							if (false)
							{
							}
							num2 = 0;
							continue;
						}
						break;
						IL_142:
						num2 = 5;
					}
				}
				IL_9D:
				IL_12F:
				IL_167:
				rtfTextWriter.WriteTag(RtfTags.RtfEnd);
				return rtfTextWriter.ToString();
			}
			}
		}

		// Token: 0x060041A8 RID: 16808 RVA: 0x0024DDD8 File Offset: 0x0024CDD8
		internal string ᜃ(string A_0)
		{
			if (true)
			{
			}
			switch (0)
			{
			default:
			{
				RtfTextWriter rtfTextWriter;
				for (;;)
				{
					this.ᜁ.ᜈ();
					rtfTextWriter = new RtfTextWriter();
					this.ᜀ(rtfTextWriter, A_0);
					string text = this.ᜁ.ᜏ();
					int num = this.ᜁ.ᜆ();
					int a_ = 0;
					int num2 = 5;
					for (;;)
					{
						switch (num2)
						{
						case 0:
							goto IL_142;
						case 1:
							num2 = 9;
							continue;
						case 2:
						{
							int num3;
							if (num3 > num)
							{
								num2 = 1;
								continue;
							}
							a_ = this.ᜀ(rtfTextWriter, num3, a_, A_0);
							num3++;
							num2 = 4;
							continue;
						}
						case 3:
							goto IL_12F;
						case 4:
							goto IL_142;
						case 5:
							if (text.Length > 0)
							{
								num2 = 7;
								continue;
							}
							goto IL_167;
						case 6:
							if (num > 0)
							{
								num2 = 8;
								continue;
							}
							this.ᜀ(rtfTextWriter, this.DefaultFont.Index, text, A_0);
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_12F;
							}
							if (false)
							{
							}
							num2 = 3;
							continue;
						case 7:
							num2 = 6;
							continue;
						case 8:
						{
							int num3 = 0;
							num2 = 0;
							continue;
						}
						case 9:
							goto IL_A6;
						}
						break;
						IL_142:
						num2 = 2;
					}
				}
				IL_A6:
				IL_12F:
				IL_167:
				rtfTextWriter.WriteTag(RtfTags.RtfEnd);
				return rtfTextWriter.ToString();
			}
			}
		}

		// Token: 0x060041A9 RID: 16809 RVA: 0x0024DF5C File Offset: 0x0024CF5C
		internal XlsFont ᜃ(int A_0)
		{
			int a_ = 10;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_9F;
				case 2:
					if (A_0 >= this.ᜁ.ᜏ().Length)
					{
						num = 0;
						continue;
					}
					goto IL_A1;
				case 3:
					num = 2;
					continue;
				}
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
					if (A_0 < 0)
					{
						goto IL_65;
					}
					num = 3;
					break;
				}
			}
			IL_65:
			throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("⤿ቁ⭃㕅ⅇ㹉╋⅍㹏", a_));
			IL_9F:
			goto IL_65;
			IL_A1:
			int iFontIndex = this.ᜁ.ᜆ(A_0);
			return this.GetFontByIndex(iFontIndex);
		}

		// Token: 0x060041AA RID: 16810 RVA: 0x0024E020 File Offset: 0x0024D020
		private int ᜀ(RtfTextWriter A_0, int A_1, int A_2)
		{
			int a_ = 18;
			switch (0)
			{
			default:
			{
				int num = 9;
				int num2;
				string a_2;
				int num3;
				for (;;)
				{
					string text;
					int num4;
					int num5;
					int num6;
					switch (num)
					{
					case 0:
						num = 12;
						continue;
					case 1:
						if (num2 == A_2)
						{
							num = 3;
							continue;
						}
						if (true)
						{
						}
						a_2 = text.Substring(A_2, num2 - A_2);
						num = 15;
						continue;
					case 2:
						if (num3 == 0)
						{
							num = 14;
							continue;
						}
						goto IL_24E;
					case 3:
						return A_2;
					case 4:
						num4 = this.ᜁ.ᜄ(A_1);
						goto IL_1B9;
					case 5:
						if (A_1 > num5)
						{
							num = 17;
							continue;
						}
						text = this.ᜁ.ᜏ();
						num = 13;
						continue;
					case 6:
						goto IL_84;
					case 7:
						goto IL_A6;
					case 8:
						num6 = 0;
						goto IL_12D;
					case 10:
						if (A_1 >= 0)
						{
							num = 18;
							continue;
						}
						goto IL_1D8;
					case 11:
						goto IL_1B4;
					case 12:
						num6 = this.ᜁ.ᜃ(A_1 - 1);
						goto IL_12D;
					case 13:
						if (A_1 != num5)
						{
							num = 16;
							continue;
						}
						num = 7;
						continue;
					case 14:
						num3 = this.DefaultFont.Index;
						num = 11;
						continue;
					case 15:
						if (A_1 != 0)
						{
							num = 0;
							continue;
						}
						num = 8;
						continue;
					case 16:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_A6;
						default:
							if (false)
							{
							}
							num = 4;
							continue;
						}
						break;
					case 17:
						goto IL_16F;
					case 18:
						num = 5;
						continue;
					}
					if (A_0 == null)
					{
						num = 6;
						continue;
					}
					num5 = this.ᜁ.ᜆ();
					num = 10;
					continue;
					IL_12D:
					num3 = num6;
					num = 2;
					continue;
					IL_1B9:
					num2 = num4;
					num = 1;
					continue;
					IL_A6:
					num4 = text.Length;
					goto IL_1B9;
				}
				IL_84:
				throw new ArgumentNullException(RecordTableEnumerator.b("㽇㡉╋㩍㕏⁑", a_));
				IL_16F:
				goto IL_1D8;
				IL_1B4:
				goto IL_24E;
				IL_1D8:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("㩇㽉≋ݍ㹏㙑ㅓ⹕", a_), RecordTableEnumerator.b("㩇㽉≋ݍ㹏㙑ㅓ⹕硗⥙㑛ㅝᕟ๡c䙥੧ཀྵ䱫ͭὯqᅳ噵౷ቹᵻၽꁿꪉ늑벛좟쎡쪣蚥쮧얩\ud9ab삭쒯鲱", a_));
				IL_24E:
				this.ᜀ(A_0, num3, a_2);
				return num2;
			}
			}
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x0024E288 File Offset: 0x0024D288
		private int ᜀ(RtfTextWriter A_0, int A_1, int A_2, string A_3)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 1;
				int num2;
				string a_2;
				int num5;
				for (;;)
				{
					string text;
					int num3;
					int num4;
					int num6;
					switch (num)
					{
					case 0:
						if (num2 == A_2)
						{
							num = 12;
							continue;
						}
						a_2 = text.Substring(A_2, num2 - A_2);
						num = 17;
						continue;
					case 2:
						num3 = 0;
						goto IL_135;
					case 3:
						if (A_1 > num4)
						{
							num = 16;
							continue;
						}
						text = this.ᜁ.ᜏ();
						num = 5;
						continue;
					case 4:
						if (num5 == 0)
						{
							num = 14;
							continue;
						}
						goto IL_24E;
					case 5:
						if (A_1 != num4)
						{
							num = 6;
							continue;
						}
						num = 11;
						continue;
					case 6:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_AE;
						}
						if (false)
						{
						}
						num = 7;
						continue;
					case 7:
						num6 = this.ᜁ.ᜄ(A_1);
						goto IL_1C1;
					case 8:
						if (A_1 >= 0)
						{
							num = 18;
							continue;
						}
						goto IL_1E0;
					case 9:
						goto IL_1BC;
					case 10:
						num3 = this.ᜁ.ᜃ(A_1 - 1);
						goto IL_135;
					case 11:
						goto IL_AE;
					case 12:
						return A_2;
					case 13:
						goto IL_84;
					case 14:
						num5 = this.DefaultFont.Index;
						num = 9;
						continue;
					case 15:
						num = 10;
						continue;
					case 16:
						goto IL_177;
					case 17:
						if (A_1 != 0)
						{
							num = 15;
							continue;
						}
						num = 2;
						continue;
					case 18:
						num = 3;
						continue;
					}
					if (A_0 == null)
					{
						num = 13;
						continue;
					}
					num4 = this.ᜁ.ᜆ();
					num = 8;
					continue;
					IL_135:
					num5 = num3;
					num = 4;
					continue;
					IL_1C1:
					num2 = num6;
					num = 0;
					continue;
					IL_AE:
					num6 = text.Length;
					goto IL_1C1;
				}
				IL_84:
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
				IL_177:
				goto IL_1E0;
				IL_1BC:
				goto IL_24E;
				IL_1E0:
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("䬸为匼瘾⽀❂⁄㽆", a_), RecordTableEnumerator.b("漸娺儼䨾⑀捂♄♆❈╊≌㭎煐ㅒご睖㕘㹚⹜ⱞ䅠ᝢ൤٦ݨ䭪嵬佮ၰᵲᅴ坶Ṹॺ᡼ṾꞆﶈ놐朗횔뾞負莢钤", a_));
				IL_24E:
				this.ᜀ(A_0, num5, a_2, A_3);
				return num2;
			}
			}
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x0024E4F0 File Offset: 0x0024D4F0
		private void ᜀ(RtfTextWriter A_0)
		{
			int a_ = 3;
			switch (0)
			{
			default:
			{
				int num = 8;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_E5;
					case 1:
					{
						if (true)
						{
						}
						int num2;
						if (num2 != 0)
						{
							num = 2;
							continue;
						}
						goto IL_77;
					}
					case 2:
					{
						int num2;
						XlsFont fontByIndex = this.GetFontByIndex(num2);
						this.ᜀ(fontByIndex, A_0);
						num = 5;
						continue;
					}
					case 3:
						goto IL_75;
					case 4:
						goto IL_C9;
					case 5:
						goto IL_77;
					case 6:
						goto IL_D5;
					case 7:
						goto IL_C9;
					}
					int num3;
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
					{
						IL_D5:
						int num4;
						if (num3 >= num4)
						{
							num = 0;
							continue;
						}
						int num2 = this.ᜁ.ᜃ(num3);
						num = 1;
						continue;
					}
					default:
					{
						if (false)
						{
						}
						if (A_0 == null)
						{
							num = 3;
							continue;
						}
						num3 = 0;
						int num4 = this.ᜁ.ᜆ();
						num = 4;
						continue;
					}
					}
					IL_77:
					num3++;
					num = 7;
					continue;
					IL_C9:
					num = 6;
				}
				IL_75:
				throw new ArgumentNullException(RecordTableEnumerator.b("丸䤺吼䬾⑀ㅂ", a_));
				IL_E5:
				this.ᜀ(this.DefaultFont, A_0);
				A_0.WriteTag(RtfTags.RtfBegin);
				A_0.WriteFontTable();
				A_0.WriteColorTable();
				return;
			}
			}
		}

		// Token: 0x060041AD RID: 16813 RVA: 0x0024E64C File Offset: 0x0024D64C
		private void ᜀ(XlsFont A_0, RtfTextWriter A_1)
		{
			int a_ = 17;
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_34;
				case 1:
					if (A_1 == null)
					{
						num = 2;
						continue;
					}
					goto IL_A1;
				case 2:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_2C;
					default:
						goto IL_56;
					}
					break;
				}
				goto IL_29;
				IL_2C:
				num = 0;
				continue;
				IL_29:
				if (A_0 == null)
				{
					goto IL_2C;
				}
				num = 1;
			}
			IL_34:
			throw new ArgumentNullException(RecordTableEnumerator.b("ⅆ♈╊㥌᭎㹐ቒㅔ㍖", a_));
			IL_56:
			if (true)
			{
			}
			if (false)
			{
			}
			throw new ArgumentNullException(RecordTableEnumerator.b("う㭈≊㥌⩎⍐", a_));
			IL_A1:
			Font font = A_0.GenerateNativeFont();
			A_1.AddFont(font);
			A_1.AddColor(A_0.Color);
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x0024E718 File Offset: 0x0024D718
		private void ᜀ(RtfTextWriter A_0, int A_1, string A_2)
		{
			int a_ = 4;
			int num = 2;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_3C;
				case 1:
					goto IL_D7;
				case 3:
					goto IL_59;
				case 4:
					if (A_2.Length == 0)
					{
						num = 3;
						continue;
					}
					goto IL_E6;
				case 5:
					if (A_2 == null)
					{
						num = 1;
						continue;
					}
					num = 4;
					continue;
				}
				IL_31:
				if (A_0 == null)
				{
					num = 0;
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
					num = 5;
					continue;
				}
				goto IL_31;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("䴹主圽㐿❁㙃", a_));
			IL_59:
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("渹夻䘽㐿扁❃❅♇⑉⍋㩍灏けㅓ癕㵗㝙ⱛ⩝ᥟ", a_));
			IL_D7:
			throw new ArgumentNullException(RecordTableEnumerator.b("丹夻䘽㐿", a_));
			IL_E6:
			IFont fontByIndex = this.GetFontByIndex(A_1);
			A_0.WriteText(fontByIndex, A_2);
		}

		// Token: 0x060041AF RID: 16815 RVA: 0x0024E81C File Offset: 0x0024D81C
		private void ᜀ(RtfTextWriter A_0, int A_1, string A_2, string A_3)
		{
			int a_ = 19;
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_59;
				case 2:
					if (A_2 == null)
					{
						num = 4;
						continue;
					}
					num = 3;
					continue;
				case 3:
					if (A_2.Length == 0)
					{
						num = 0;
						continue;
					}
					goto IL_E3;
				case 4:
					goto IL_D7;
				case 5:
					goto IL_3C;
				}
				IL_31:
				if (A_0 == null)
				{
					num = 5;
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
					num = 2;
					continue;
				}
				goto IL_31;
			}
			IL_3C:
			throw new ArgumentNullException(RecordTableEnumerator.b("㹈㥊⑌㭎㑐⅒", a_));
			IL_59:
			if (true)
			{
			}
			throw new ArgumentException(RecordTableEnumerator.b("㩈㽊㽌᭎㑐⭒⅔睖瑘筚⹜⭞፠੢୤f䥨ࡪ౬ŮὰᱲŴ坶᭸Ṻ嵼᩾ﺆ", a_));
			IL_D7:
			throw new ArgumentNullException(RecordTableEnumerator.b("㩈㽊㽌᭎㑐⭒⅔", a_));
			IL_E3:
			IFont fontByIndex = this.GetFontByIndex(A_1);
			A_0.ᜀ(fontByIndex, A_2, this.ImageRTF, A_3);
		}

		// Token: 0x060041B0 RID: 16816 RVA: 0x0024E924 File Offset: 0x0024D924
		internal void ᜁ(string A_0, IFont A_1)
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
			string text = this.ᜁ.ᜏ();
			int length = text.Length;
			spr\u223A spr_u223A = this.ᜁ;
			spr_u223A.ᜁ(spr_u223A.ᜏ() + A_0);
			this.SetFont(length, this.ᜁ.ᜏ().Length - 1, A_1);
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x0024E9A4 File Offset: 0x0024D9A4
		// Note: this type is marked as 'beforefieldinit'.
		static RichTextString()
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
			RichTextString.ᜄ = new char[]
			{
				'0',
				'1',
				'2',
				'3',
				'4',
				'5',
				'6',
				'7',
				'8',
				'9'
			};
		}

		// Token: 0x04001D1D RID: 7453
		private const char ᜀ = 'X';

		// Token: 0x04001D1E RID: 7454
		internal spr\u223A ᜁ;

		// Token: 0x04001D1F RID: 7455
		private float \u2593\u0094\u00B0ª;

		// Token: 0x04001D20 RID: 7456
		private int[] \u25D9\u008A\u00A9\u0098;

		// Token: 0x04001D21 RID: 7457
		internal XlsWorkbook ᜂ;

		// Token: 0x04001D22 RID: 7458
		private bool ᜃ;

		// Token: 0x04001D23 RID: 7459
		private static readonly char[] ᜄ;

		// Token: 0x04001D24 RID: 7460
		private object ᜅ;

		// Token: 0x04001D25 RID: 7461
		private int ᜆ;

		// Token: 0x04001D26 RID: 7462
		private string ᜇ;
	}
}
