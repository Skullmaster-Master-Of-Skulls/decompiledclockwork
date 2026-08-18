using System;
using System.Drawing;
using System.Threading;
using Spire.Xls.Core.Interfaces;
using Spire.Xls.Core.Spreadsheet.Collections;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000616 RID: 1558
	public class AddtionalFormatWrapper : CommonWrapper, IInternalAddtionalFormat, IExtendIndex, IStyle, ICloneParent
	{
		// Token: 0x06005D52 RID: 23890 RVA: 0x003AA2A4 File Offset: 0x003A92A4
		public AddtionalFormatWrapper(XlsWorkbook book)
		{
			this.m_book = book;
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x003AA2C0 File Offset: 0x003A92C0
		public AddtionalFormatWrapper(XlsWorkbook book, int iXFIndex) : this(book)
		{
			this.SetFormatIndex(iXFIndex);
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x003AA2DC File Offset: 0x003A92DC
		internal void ᜄ()
		{
			for (;;)
			{
				ExcelPatternType excelPatternType = this.ᜀ.ᜤ();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_45;
					case 1:
						if (true)
						{
						}
						if (excelPatternType != ExcelPatternType.None)
						{
							num = 4;
							continue;
						}
						goto IL_45;
					case 2:
						if (excelPatternType == ExcelPatternType.Gradient)
						{
							num = 0;
							continue;
						}
						goto IL_81;
					case 3:
						goto IL_81;
					case 4:
						num = 2;
						continue;
					}
					break;
					IL_45:
					this.ᜀ.ᜀ(ExcelPatternType.Solid);
					this.ᜀ.ᜀ(null);
					num = 3;
					continue;
					IL_81:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						goto IL_97;
					}
				}
			}
			IL_97:
			if (false)
			{
			}
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x003AA390 File Offset: 0x003A9390
		public void SetFormatIndex(int index)
		{
			int num = 5;
			XlsFont a_;
			for (;;)
			{
				switch (num)
				{
				case 0:
					goto IL_10D;
				case 1:
					return;
				case 2:
					num = 3;
					continue;
				case 3:
					if (this.ᜀ.ᜠ() == index)
					{
						num = 1;
						continue;
					}
					goto IL_62;
				case 4:
					this.m_font = new ExcelFontWrapper();
					this.m_font.AfterChangeEvent += this.ᜁ;
					num = 0;
					continue;
				case 5:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_10F;
					default:
						if (false)
						{
						}
						break;
					}
					break;
				case 6:
					if (this.m_font == null)
					{
						num = 4;
						continue;
					}
					goto IL_10F;
				}
				if (true)
				{
				}
				if (this.ᜀ != null)
				{
					num = 2;
					continue;
				}
				IL_62:
				this.ᜀ = this.m_book.InnerExtFormats.ᜁ(index);
				int index2 = this.ᜀ.\u173B();
				a_ = (this.m_book.InnerFonts[index2] as XlsFont);
				num = 6;
			}
			return;
			IL_10D:
			IL_10F:
			this.m_font.Wrapped = a_;
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x003AA4C4 File Offset: 0x003A94C4
		public void UpdateFont()
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
			this.m_font.Wrapped = (XlsFont)this.ᜀ.ᜀ();
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x003AA51C File Offset: 0x003A951C
		protected virtual void SetParents(object parent)
		{
			int a_ = 19;
			this.m_book = (XlsObject.FindParent(parent, typeof(XlsWorkbook)) as XlsWorkbook);
			if (this.m_book == null)
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
					break;
				}
				if (true)
				{
				}
				throw new ArgumentNullException(RecordTableEnumerator.b("Ṉ⑊㽌⑎㍐㱒㩔㱖", a_), RecordTableEnumerator.b("᥈⩊㽌⩎㽐❒畔㡖㭘ㅚ㡜㱞ᕠ䍢٤٦ݨժɬ᭮兰ᅲၴ坶ὸᑺࡼᅾ궂", a_));
			}
		}

		// Token: 0x06005D58 RID: 23896 RVA: 0x003AA5A8 File Offset: 0x003A95A8
		protected void SetChanged()
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
			this.m_book.SetChanged();
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x003AA5F0 File Offset: 0x003A95F0
		private void ᜁ(object A_0, EventArgs A_1)
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
			this.FontIndex = this.m_font.FontIndex;
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x003AA63C File Offset: 0x003A963C
		private void ᜀ(object A_0, EventArgs A_1)
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
			this.ᜀ = this.ᜂ.ᜉ();
			this.EndUpdate();
		}

		// Token: 0x06005D5B RID: 23899 RVA: 0x003AA694 File Offset: 0x003A9694
		protected void OnNumberFormatChange()
		{
			int num = 0;
			for (;;)
			{
				switch (num)
				{
				case 1:
					this.ᜃ(this, EventArgs.Empty);
					goto IL_3F;
				case 2:
					goto IL_51;
				}
				if (this.ᜃ != null)
				{
					num = 1;
					continue;
				}
				goto IL_51;
				IL_3F:
				if (true)
				{
				}
				num = 2;
				continue;
				IL_51:
				switch ((1 == 1) ? 1 : 0)
				{
				case 0:
				case 2:
					goto IL_3F;
				default:
					goto IL_67;
				}
			}
			IL_67:
			if (false)
			{
			}
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x003AA718 File Offset: 0x003A9718
		public override object Clone(object parent)
		{
			int a_ = 11;
			AddtionalFormatWrapper addtionalFormatWrapper = (AddtionalFormatWrapper)base.Clone(parent);
			addtionalFormatWrapper.m_book = (XlsObject.FindParent(parent, typeof(XlsWorkbook)) as XlsWorkbook);
			if (addtionalFormatWrapper.m_book == null)
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
					break;
				}
				throw new ArgumentOutOfRangeException(RecordTableEnumerator.b("ㅀ≂㝄≆❈㽊", a_), RecordTableEnumerator.b("ᕀ⭂⁄杆㥈⩊㽌⩎㽐❒畔㡖㭘ㅚ㡜㱞ᕠ䍢٤٦ݨ䭪ͬnհ卲᝴ቶ奸ᵺቼ੾ꮄ", a_));
			}
			addtionalFormatWrapper.ᜁ = null;
			addtionalFormatWrapper.ᜀ = null;
			addtionalFormatWrapper.m_font = null;
			addtionalFormatWrapper.SetFormatIndex(this.ᜀ.ᜠ());
			return addtionalFormatWrapper;
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x003AA7D8 File Offset: 0x003A97D8
		protected virtual void BeforeRead()
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
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x003AA814 File Offset: 0x003A9814
		private IStyle ᜀ()
		{
			int num = 3;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					goto IL_47;
				case 2:
					goto IL_5C;
				case 3:
					if (true)
					{
					}
					break;
				}
				if (!this.ᜀ.ᝇ())
				{
					num = 0;
				}
				else
				{
					num = 1;
				}
			}
			IL_47:
			int num2 = this.ᜀ.ᜯ();
			goto IL_8F;
			IL_5C:
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				goto IL_47;
			default:
				if (false)
				{
				}
				num2 = this.ᜀ.ᜌ();
				break;
			}
			IL_8F:
			int index = num2;
			return this.m_book.InnerStyles.GetByXFIndex(index);
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06005D5F RID: 23903 RVA: 0x003AA8C4 File Offset: 0x003A98C4
		public XlsWorkbook Workbook
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
				return this.m_book;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06005D60 RID: 23904 RVA: 0x003AA908 File Offset: 0x003A9908
		// (set) Token: 0x06005D61 RID: 23905 RVA: 0x003AA954 File Offset: 0x003A9954
		public ExcelPatternType FillPattern
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
				this.BeforeRead();
				return this.ᜀ.ᜤ();
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_59;
					case 2:
						this.BeginUpdate();
						this.ᜀ.ᜀ(value);
						this.EndUpdate();
						goto IL_4F;
					}
					if (true)
					{
					}
					if (this.FillPattern != value)
					{
						num = 2;
						continue;
					}
					goto IL_59;
					IL_4F:
					num = 0;
					continue;
					IL_59:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4F;
					default:
						goto IL_6F;
					}
				}
				IL_6F:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06005D62 RID: 23906 RVA: 0x003AA9E0 File Offset: 0x003A99E0
		public int ExtendedFormatIndex
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
				this.BeforeRead();
				return this.ᜀ.ᜌ();
			}
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06005D63 RID: 23907 RVA: 0x003AAA2C File Offset: 0x003A9A2C
		// (set) Token: 0x06005D64 RID: 23908 RVA: 0x003AAA78 File Offset: 0x003A9A78
		public ExcelColors BackgroundKnownColor
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
				this.BeforeRead();
				return this.ᜀ.\u1739();
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_57;
					case 2:
						this.BeginUpdate();
						this.ᜀ.ᜀ(value);
						this.ᜄ();
						this.EndUpdate();
						goto IL_4D;
					}
					if (this.BackgroundKnownColor != value)
					{
						num = 2;
						continue;
					}
					goto IL_57;
					IL_4D:
					num = 0;
					continue;
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06005D65 RID: 23909 RVA: 0x003AAB0C File Offset: 0x003A9B0C
		// (set) Token: 0x06005D66 RID: 23910 RVA: 0x003AAB58 File Offset: 0x003A9B58
		public Color BackgroundColor
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
				this.BeforeRead();
				return this.ᜀ.ᜨ();
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.BeginUpdate();
						this.ᜀ.ᜀ(value);
						this.ᜄ();
						this.EndUpdate();
						goto IL_52;
					case 1:
						goto IL_5C;
					}
					if (this.BackgroundColor != value)
					{
						num = 0;
						continue;
					}
					goto IL_5C;
					IL_52:
					num = 1;
					continue;
					IL_5C:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_52;
					default:
						goto IL_72;
					}
				}
				IL_72:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06005D67 RID: 23911 RVA: 0x003AABF0 File Offset: 0x003A9BF0
		// (set) Token: 0x06005D68 RID: 23912 RVA: 0x003AAC3C File Offset: 0x003A9C3C
		public ExcelColors ForegroundKnownColor
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
				this.BeforeRead();
				return this.ᜀ.\u1734();
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5F;
					case 1:
						this.BeginUpdate();
						this.ᜀ.ᜃ(value);
						this.ᜄ();
						this.EndUpdate();
						goto IL_4D;
					}
					if (this.ForegroundKnownColor != value)
					{
						num = 1;
						continue;
					}
					goto IL_5F;
					IL_4D:
					if (true)
					{
					}
					num = 0;
					continue;
					IL_5F:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
						goto IL_75;
					}
				}
				IL_75:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06005D69 RID: 23913 RVA: 0x003AACD0 File Offset: 0x003A9CD0
		// (set) Token: 0x06005D6A RID: 23914 RVA: 0x003AAD1C File Offset: 0x003A9D1C
		public Color ForegroundColor
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
				this.BeforeRead();
				return this.ᜀ.ᝍ();
			}
			set
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						goto IL_64;
					case 2:
						this.BeginUpdate();
						this.ᜀ.ᜁ(value);
						this.ᜄ();
						this.EndUpdate();
						goto IL_5A;
					}
					if (this.ForegroundColor != value)
					{
						num = 2;
						continue;
					}
					goto IL_64;
					IL_5A:
					num = 1;
					continue;
					IL_64:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_5A;
					default:
						goto IL_7A;
					}
				}
				IL_7A:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000ED3 RID: 3795
		// (get) Token: 0x06005D6B RID: 23915 RVA: 0x003AADB4 File Offset: 0x003A9DB4
		// (set) Token: 0x06005D6C RID: 23916 RVA: 0x003AAE00 File Offset: 0x003A9E00
		public int NumberFormatIndex
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
				this.BeforeRead();
				return this.ᜀ.ᝊ();
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_57;
					case 2:
						this.BeginUpdate();
						this.ᜀ.ᜀ(value);
						this.EndUpdate();
						this.OnNumberFormatChange();
						goto IL_4D;
					}
					if (this.NumberFormatIndex != value)
					{
						num = 2;
						continue;
					}
					goto IL_57;
					IL_4D:
					num = 0;
					continue;
					IL_57:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4D;
					default:
						goto IL_6D;
					}
				}
				IL_6D:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x17000ED4 RID: 3796
		// (get) Token: 0x06005D6D RID: 23917 RVA: 0x003AAE94 File Offset: 0x003A9E94
		// (set) Token: 0x06005D6E RID: 23918 RVA: 0x003AAEE0 File Offset: 0x003A9EE0
		public HorizontalAlignType HorizontalAlignment
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
				this.BeforeRead();
				return this.ᜀ.ᜋ();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.BeginUpdate();
						this.ᜀ.ᜀ(value);
						this.EndUpdate();
						goto IL_4F;
					case 2:
						goto IL_59;
					}
					if (true)
					{
					}
					if (this.HorizontalAlignment != value)
					{
						num = 1;
						continue;
					}
					goto IL_59;
					IL_4F:
					num = 2;
					continue;
					IL_59:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4F;
					default:
						goto IL_6F;
					}
				}
				IL_6F:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000ED5 RID: 3797
		// (get) Token: 0x06005D6F RID: 23919 RVA: 0x003AAF6C File Offset: 0x003A9F6C
		// (set) Token: 0x06005D70 RID: 23920 RVA: 0x003AAFB8 File Offset: 0x003A9FB8
		public bool IncludeAlignment
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
				this.BeforeRead();
				return this.ᜀ.ᜦ();
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_51;
					case 1:
						this.BeginUpdate();
						this.ᜀ.ᜈ(value);
						this.EndUpdate();
						goto IL_47;
					}
					if (this.IncludeAlignment != value)
					{
						num = 1;
						continue;
					}
					goto IL_51;
					IL_47:
					num = 0;
					continue;
					IL_51:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_47;
					default:
						goto IL_67;
					}
				}
				IL_67:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000ED6 RID: 3798
		// (get) Token: 0x06005D71 RID: 23921 RVA: 0x003AB044 File Offset: 0x003AA044
		// (set) Token: 0x06005D72 RID: 23922 RVA: 0x003AB090 File Offset: 0x003AA090
		public bool IncludeBorder
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
				this.BeforeRead();
				return this.ᜀ.\u1719();
			}
			set
			{
				if (true)
				{
				}
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_59;
					case 2:
						this.BeginUpdate();
						this.ᜀ.ᜊ(value);
						this.EndUpdate();
						goto IL_4F;
					}
					if (this.IncludeBorder != value)
					{
						num = 2;
						continue;
					}
					goto IL_59;
					IL_4F:
					num = 0;
					continue;
					IL_59:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_4F;
					default:
						goto IL_6F;
					}
				}
				IL_6F:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000ED7 RID: 3799
		// (get) Token: 0x06005D73 RID: 23923 RVA: 0x003AB11C File Offset: 0x003AA11C
		// (set) Token: 0x06005D74 RID: 23924 RVA: 0x003AB168 File Offset: 0x003AA168
		public bool IncludeFont
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
				this.BeforeRead();
				return this.ᜀ.ᝀ();
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.BeginUpdate();
						this.ᜀ.ᜉ(value);
						this.EndUpdate();
						goto IL_59;
					case 1:
						goto IL_63;
					}
					if (true)
					{
					}
					if (this.IncludeFont != value)
					{
						num = 0;
						continue;
					}
					goto IL_63;
					IL_59:
					num = 1;
					continue;
					IL_63:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						goto IL_59;
					default:
						goto IL_79;
					}
				}
				IL_79:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000ED8 RID: 3800
		// (get) Token: 0x06005D75 RID: 23925 RVA: 0x003AB1F4 File Offset: 0x003AA1F4
		// (set) Token: 0x06005D76 RID: 23926 RVA: 0x003AB240 File Offset: 0x003AA240
		public bool IncludeNumberFormat
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
				this.BeforeRead();
				return this.ᜀ.\u173D();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						this.BeginUpdate();
						this.ᜀ.ᜃ(value);
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_77;
						}
						break;
					}
					IL_1C:
					if (this.IncludeNumberFormat != value)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06005D77 RID: 23927 RVA: 0x003AB2CC File Offset: 0x003AA2CC
		// (set) Token: 0x06005D78 RID: 23928 RVA: 0x003AB318 File Offset: 0x003AA318
		public bool IncludePatterns
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
				this.BeforeRead();
				return this.ᜀ.\u1753();
			}
			set
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
							break;
						default:
							goto IL_77;
						}
						break;
					case 1:
						if (true)
						{
						}
						this.BeginUpdate();
						this.ᜀ.\u170D(value);
						this.EndUpdate();
						num = 0;
						continue;
					}
					IL_1C:
					if (this.IncludePatterns != value)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06005D79 RID: 23929 RVA: 0x003AB3A4 File Offset: 0x003AA3A4
		// (set) Token: 0x06005D7A RID: 23930 RVA: 0x003AB3F0 File Offset: 0x003AA3F0
		public bool IncludeProtection
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
				this.BeforeRead();
				return this.ᜀ.\u1717();
			}
			set
			{
				int num = 2;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.BeginUpdate();
						this.ᜀ.ᜋ(value);
						this.EndUpdate();
						num = 1;
						continue;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_77;
						}
						break;
					}
					IL_1C:
					if (true)
					{
					}
					if (this.IncludeProtection != value)
					{
						num = 0;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06005D7B RID: 23931 RVA: 0x003AB47C File Offset: 0x003AA47C
		// (set) Token: 0x06005D7C RID: 23932 RVA: 0x003AB4C8 File Offset: 0x003AA4C8
		public int IndentLevel
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
				this.BeforeRead();
				return this.ᜀ.\u171A();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					if (true)
					{
					}
					switch (num)
					{
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_77;
						}
						break;
					case 2:
						this.BeginUpdate();
						this.ᜀ.ᜁ(value);
						this.EndUpdate();
						num = 1;
						continue;
					}
					IL_24:
					if (this.IndentLevel != value)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_24;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06005D7D RID: 23933 RVA: 0x003AB554 File Offset: 0x003AA554
		// (set) Token: 0x06005D7E RID: 23934 RVA: 0x003AB5A0 File Offset: 0x003AA5A0
		public bool FormulaHidden
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
				this.BeforeRead();
				return this.ᜀ.\u1755();
			}
			set
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
							break;
						default:
							goto IL_77;
						}
						break;
					case 2:
						this.BeginUpdate();
						this.ᜀ.ᜆ(value);
						this.EndUpdate();
						if (true)
						{
						}
						num = 0;
						continue;
					}
					IL_1C:
					if (this.FormulaHidden != value)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06005D7F RID: 23935 RVA: 0x003AB62C File Offset: 0x003AA62C
		// (set) Token: 0x06005D80 RID: 23936 RVA: 0x003AB678 File Offset: 0x003AA678
		public bool Locked
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
				this.BeforeRead();
				return this.ᜀ.ᝎ();
			}
			set
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
						this.BeginUpdate();
						this.ᜀ.ᜁ(value);
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_77;
						}
						break;
					}
					IL_1C:
					if (this.Locked != value)
					{
						num = 0;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06005D81 RID: 23937 RVA: 0x003AB704 File Offset: 0x003AA704
		// (set) Token: 0x06005D82 RID: 23938 RVA: 0x003AB750 File Offset: 0x003AA750
		public bool JustifyLast
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
				this.BeforeRead();
				return this.ᜀ.ᜱ();
			}
			set
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
							break;
						default:
							goto IL_77;
						}
						break;
					case 1:
						this.BeginUpdate();
						this.ᜀ.ᜂ(value);
						this.EndUpdate();
						num = 0;
						continue;
					}
					IL_1C:
					if (this.JustifyLast != value)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06005D83 RID: 23939 RVA: 0x003AB7DC File Offset: 0x003AA7DC
		// (set) Token: 0x06005D84 RID: 23940 RVA: 0x003AB828 File Offset: 0x003AA828
		public string NumberFormat
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
				this.BeforeRead();
				return this.ᜀ.\u1715();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.BeginUpdate();
						this.ᜀ.ᜁ(value);
						this.EndUpdate();
						this.OnNumberFormatChange();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_82;
						}
						break;
					}
					IL_1C:
					if (this.NumberFormat != value)
					{
						if (true)
						{
						}
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_82:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06005D85 RID: 23941 RVA: 0x003AB8C0 File Offset: 0x003AA8C0
		// (set) Token: 0x06005D86 RID: 23942 RVA: 0x003AB90C File Offset: 0x003AA90C
		public string NumberFormatLocal
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
				this.BeforeRead();
				return this.ᜀ.\u1737();
			}
			set
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
							break;
						default:
							goto IL_7A;
						}
						break;
					case 1:
						this.BeginUpdate();
						this.ᜀ.ᜀ(value);
						this.EndUpdate();
						this.OnNumberFormatChange();
						num = 0;
						continue;
					}
					IL_1C:
					if (this.NumberFormatLocal != value)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_7A:
				if (false)
				{
				}
				if (true)
				{
				}
			}
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06005D87 RID: 23943 RVA: 0x003AB9A4 File Offset: 0x003AA9A4
		public INumberFormat NumberFormatSettings
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
				this.BeforeRead();
				return this.ᜀ.ᝁ();
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06005D88 RID: 23944 RVA: 0x003AB9F0 File Offset: 0x003AA9F0
		// (set) Token: 0x06005D89 RID: 23945 RVA: 0x003ABA3C File Offset: 0x003AAA3C
		public ReadingOrderType ReadingOrder
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
				this.BeforeRead();
				return this.ᜀ.\u171C();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.BeginUpdate();
						this.ᜀ.ᜀ(value);
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6F;
						}
						break;
					}
					IL_1C:
					if (this.ReadingOrder != value)
					{
						num = 1;
						continue;
					}
					goto IL_77;
					goto IL_1C;
				}
				IL_6F:
				if (false)
				{
				}
				IL_77:
				if (true)
				{
				}
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06005D8A RID: 23946 RVA: 0x003ABAC8 File Offset: 0x003AAAC8
		// (set) Token: 0x06005D8B RID: 23947 RVA: 0x003ABB14 File Offset: 0x003AAB14
		public int Rotation
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
				this.BeforeRead();
				return this.ᜀ.\u171B();
			}
			set
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
							break;
						default:
							goto IL_77;
						}
						break;
					case 2:
						if (true)
						{
						}
						this.BeginUpdate();
						this.ᜀ.ᜅ(value);
						this.EndUpdate();
						num = 1;
						continue;
					}
					IL_1C:
					if (this.Rotation != value)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06005D8C RID: 23948 RVA: 0x003ABBA0 File Offset: 0x003AABA0
		// (set) Token: 0x06005D8D RID: 23949 RVA: 0x003ABBEC File Offset: 0x003AABEC
		public bool ShrinkToFit
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
				this.BeforeRead();
				return this.ᜀ.ᝏ();
			}
			set
			{
				if (true)
				{
				}
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.BeginUpdate();
						this.ᜀ.ᜇ(value);
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_77;
						}
						break;
					}
					IL_24:
					if (this.ShrinkToFit != value)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_24;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06005D8E RID: 23950 RVA: 0x003ABC78 File Offset: 0x003AAC78
		// (set) Token: 0x06005D8F RID: 23951 RVA: 0x003ABCC4 File Offset: 0x003AACC4
		public VerticalAlignType VerticalAlignment
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
				this.BeforeRead();
				return this.ᜀ.\u171D();
			}
			set
			{
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						this.BeginUpdate();
						this.ᜀ.ᜀ(value);
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_6F;
						}
						break;
					}
					IL_1C:
					if (this.VerticalAlignment != value)
					{
						num = 0;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_6F:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06005D90 RID: 23952 RVA: 0x003ABD50 File Offset: 0x003AAD50
		// (set) Token: 0x06005D91 RID: 23953 RVA: 0x003ABD9C File Offset: 0x003AAD9C
		public bool WrapText
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
				this.BeforeRead();
				return this.ᜀ.\u1733();
			}
			set
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
							break;
						default:
							goto IL_77;
						}
						break;
					case 2:
						if (true)
						{
						}
						this.BeginUpdate();
						this.ᜀ.ᜅ(value);
						this.EndUpdate();
						num = 1;
						continue;
					}
					IL_1C:
					if (this.WrapText != value)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06005D92 RID: 23954 RVA: 0x003ABE28 File Offset: 0x003AAE28
		public IFont Font
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
				this.BeforeRead();
				return this.m_font;
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06005D93 RID: 23955 RVA: 0x003ABE70 File Offset: 0x003AAE70
		public IBorders Borders
		{
			get
			{
				for (;;)
				{
					this.BeforeRead();
					int num = 2;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							this.ᜁ = new spr\u2330((spr\u2158)this.ReservedHandle, this, this);
							num = 1;
							continue;
						case 1:
							goto IL_82;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								continue;
							default:
								if (false)
								{
								}
								if (this.ᜁ == null)
								{
									num = 0;
									continue;
								}
								goto IL_84;
							}
							break;
						}
						break;
					}
				}
				IL_82:
				IL_84:
				return this.ᜁ;
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06005D94 RID: 23956 RVA: 0x003ABF08 File Offset: 0x003AAF08
		// (set) Token: 0x06005D95 RID: 23957 RVA: 0x003ABF54 File Offset: 0x003AAF54
		public bool IsFirstSymbolApostrophe
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
				this.BeforeRead();
				return this.ᜀ.\u1713();
			}
			set
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
							break;
						default:
							goto IL_77;
						}
						break;
					case 2:
						this.BeginUpdate();
						this.ᜀ.ᜌ(value);
						this.EndUpdate();
						num = 0;
						continue;
					}
					IL_1C:
					if (this.IsFirstSymbolApostrophe != value)
					{
						if (true)
						{
						}
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06005D96 RID: 23958 RVA: 0x003ABFE0 File Offset: 0x003AAFE0
		// (set) Token: 0x06005D97 RID: 23959 RVA: 0x003AC028 File Offset: 0x003AB028
		public ExcelColors PatternKnownColor
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
				return this.ᜀ.ᝆ();
			}
			set
			{
				int num = 4;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_37;
					case 1:
						if (this.FillPattern == ExcelPatternType.Gradient)
						{
							num = 0;
							continue;
						}
						return;
					case 2:
						if (true)
						{
						}
						num = 1;
						continue;
					case 3:
						return;
					}
					if (this.PatternKnownColor == value)
					{
						num = 2;
						continue;
					}
					IL_37:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.BeginUpdate();
						this.ᜀ.ᜁ(value);
						this.ᜄ();
						this.EndUpdate();
						num = 3;
						break;
					}
				}
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06005D98 RID: 23960 RVA: 0x003AC0E8 File Offset: 0x003AB0E8
		// (set) Token: 0x06005D99 RID: 23961 RVA: 0x003AC134 File Offset: 0x003AB134
		public Color PatternColor
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
				this.BeforeRead();
				return this.ᜀ.\u1732();
			}
			set
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
							break;
						default:
							goto IL_82;
						}
						break;
					case 2:
						this.BeginUpdate();
						this.ᜀ.ᜂ(value);
						this.ᜄ();
						this.EndUpdate();
						num = 0;
						continue;
					}
					IL_1C:
					if (true)
					{
					}
					if (this.PatternColor != value)
					{
						num = 2;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_82:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06005D9A RID: 23962 RVA: 0x003AC1CC File Offset: 0x003AB1CC
		// (set) Token: 0x06005D9B RID: 23963 RVA: 0x003AC218 File Offset: 0x003AB218
		public ExcelColors KnownColor
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
				this.BeforeRead();
				return this.ᜀ.ᜩ();
			}
			set
			{
				int num = 3;
				for (;;)
				{
					switch (num)
					{
					case 0:
						num = 1;
						continue;
					case 1:
						if (true)
						{
						}
						if (this.KnownColor != value)
						{
							num = 2;
							continue;
						}
						return;
					case 2:
						goto IL_3B;
					case 4:
						return;
					}
					if (this.FillPattern != ExcelPatternType.Gradient)
					{
						num = 0;
						continue;
					}
					IL_3B:
					switch ((1 == 1) ? 1 : 0)
					{
					case 0:
					case 2:
						break;
					default:
						if (false)
						{
						}
						this.BeginUpdate();
						this.ᜀ.ᜂ(value);
						this.ᜄ();
						this.EndUpdate();
						num = 4;
						break;
					}
				}
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06005D9C RID: 23964 RVA: 0x003AC2D4 File Offset: 0x003AB2D4
		// (set) Token: 0x06005D9D RID: 23965 RVA: 0x003AC320 File Offset: 0x003AB320
		public Color Color
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
				this.BeforeRead();
				return this.ᜀ.ᜰ();
			}
			set
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
							goto IL_08;
						default:
							goto IL_82;
						}
						break;
					case 1:
						this.BeginUpdate();
						this.ᜀ.ᜃ(value);
						this.ᜄ();
						this.EndUpdate();
						num = 0;
						continue;
					case 2:
						goto IL_08;
					}
					IL_24:
					if (this.Color != value)
					{
						num = 1;
						continue;
					}
					return;
					IL_08:
					if (true)
					{
					}
					goto IL_24;
				}
				IL_82:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06005D9E RID: 23966 RVA: 0x003AC3B8 File Offset: 0x003AB3B8
		public IInterior Interior
		{
			get
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						this.ᜂ = new sprឰ(this.ᜀ);
						this.ᜂ.ᜀ(new EventHandler(this.ᜀ));
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_7E;
						}
						break;
					}
					IL_1C:
					if (this.ᜂ == null)
					{
						num = 1;
						continue;
					}
					goto IL_86;
					goto IL_1C;
				}
				IL_7E:
				if (false)
				{
				}
				IL_86:
				if (true)
				{
				}
				this.BeforeRead();
				return this.ᜂ;
			}
		}

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06005D9F RID: 23967 RVA: 0x003AC460 File Offset: 0x003AB460
		public bool IsModified
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
				this.BeforeRead();
				return this.ᜀ.\u173A();
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06005DA0 RID: 23968 RVA: 0x003AC4AC File Offset: 0x003AB4AC
		// (set) Token: 0x06005DA1 RID: 23969 RVA: 0x003AC4F8 File Offset: 0x003AB4F8
		public int FontIndex
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
				this.BeforeRead();
				return this.ᜀ.\u173B();
			}
			set
			{
				int num = 0;
				for (;;)
				{
					switch (num)
					{
					case 1:
						if (true)
						{
						}
						this.BeginUpdate();
						this.ᜀ.ᜂ(value);
						this.EndUpdate();
						num = 2;
						continue;
					case 2:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							break;
						default:
							goto IL_77;
						}
						break;
					}
					IL_1C:
					if (this.FontIndex != value)
					{
						num = 1;
						continue;
					}
					return;
					goto IL_1C;
				}
				IL_77:
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06005DA2 RID: 23970 RVA: 0x003AC584 File Offset: 0x003AB584
		internal spr\u192F Wrapped
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
				this.BeforeRead();
				return this.ᜀ;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06005DA3 RID: 23971 RVA: 0x003AC5CC File Offset: 0x003AB5CC
		public OColor BottomBorderColor
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
				this.BeforeRead();
				return this.ᜀ.ᜡ();
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06005DA4 RID: 23972 RVA: 0x003AC618 File Offset: 0x003AB618
		public OColor TopBorderColor
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
				this.BeforeRead();
				return this.ᜀ.\u173F();
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06005DA5 RID: 23973 RVA: 0x003AC664 File Offset: 0x003AB664
		public virtual OColor LeftBorderColor
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
				this.BeforeRead();
				return this.ᜀ.ᝅ();
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06005DA6 RID: 23974 RVA: 0x003AC6B0 File Offset: 0x003AB6B0
		public virtual OColor RightBorderColor
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
				this.BeforeRead();
				return this.ᜀ.\u1756();
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06005DA7 RID: 23975 RVA: 0x003AC6FC File Offset: 0x003AB6FC
		public OColor DiagonalBorderColor
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
				this.BeforeRead();
				return this.ᜀ.\u171F();
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x003AC748 File Offset: 0x003AB748
		// (set) Token: 0x06005DA9 RID: 23977 RVA: 0x003AC794 File Offset: 0x003AB794
		public virtual LineStyleType LeftBorderLineStyle
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
				this.BeforeRead();
				return this.ᜀ.ᝉ();
			}
			set
			{
				for (;;)
				{
					IL_00:
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
								goto IL_00;
							default:
								if (true)
								{
								}
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.ᜀ(value);
								this.EndUpdate();
								num = 1;
								continue;
							}
							break;
						case 1:
							return;
						}
						if (this.LeftBorderLineStyle == value)
						{
							return;
						}
						num = 0;
					}
				}
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x003AC820 File Offset: 0x003AB820
		// (set) Token: 0x06005DAB RID: 23979 RVA: 0x003AC86C File Offset: 0x003AB86C
		public virtual LineStyleType RightBorderLineStyle
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
				this.BeforeRead();
				return this.ᜀ.ᜫ();
			}
			set
			{
				for (;;)
				{
					IL_00:
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
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.ᜂ(value);
								this.EndUpdate();
								num = 1;
								continue;
							}
							break;
						case 1:
							return;
						}
						if (true)
						{
						}
						if (this.RightBorderLineStyle == value)
						{
							return;
						}
						num = 0;
					}
				}
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06005DAC RID: 23980 RVA: 0x003AC8F8 File Offset: 0x003AB8F8
		// (set) Token: 0x06005DAD RID: 23981 RVA: 0x003AC944 File Offset: 0x003AB944
		public virtual LineStyleType TopBorderLineStyle
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
				this.BeforeRead();
				return this.ᜀ.\u1738();
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							break;
						case 1:
							return;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.ᜄ(value);
								this.EndUpdate();
								num = 1;
								continue;
							}
							break;
						}
						if (this.TopBorderLineStyle == value)
						{
							return;
						}
						num = 2;
					}
				}
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06005DAE RID: 23982 RVA: 0x003AC9D0 File Offset: 0x003AB9D0
		// (set) Token: 0x06005DAF RID: 23983 RVA: 0x003ACA1C File Offset: 0x003ABA1C
		public virtual LineStyleType BottomBorderLineStyle
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
				this.BeforeRead();
				return this.ᜀ.\u170D();
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 2;
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
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.ᜅ(value);
								this.EndUpdate();
								num = 1;
								continue;
							}
							break;
						case 1:
							return;
						}
						if (this.BottomBorderLineStyle == value)
						{
							return;
						}
						num = 0;
					}
				}
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06005DB0 RID: 23984 RVA: 0x003ACAA8 File Offset: 0x003ABAA8
		// (set) Token: 0x06005DB1 RID: 23985 RVA: 0x003ACAF4 File Offset: 0x003ABAF4
		public LineStyleType DiagonalUpBorderLineStyle
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
				this.BeforeRead();
				return this.ᜀ.\u1736();
			}
			set
			{
				for (;;)
				{
					bool flag = false;
					int num = 7;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (!this.ᜀ.ᜢ())
							{
								num = 6;
								continue;
							}
							goto IL_118;
						case 1:
							this.BeginUpdate();
							this.ᜀ.ᜃ(value);
							flag = true;
							num = 4;
							continue;
						case 2:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_123;
							default:
								goto IL_E7;
							}
							break;
						case 3:
							num = 9;
							continue;
						case 4:
							goto IL_98;
						case 5:
							this.EndUpdate();
							num = 2;
							continue;
						case 6:
							num = 11;
							continue;
						case 7:
							if (this.DiagonalUpBorderLineStyle != value)
							{
								num = 1;
								continue;
							}
							goto IL_98;
						case 8:
							goto IL_123;
						case 9:
							if (!flag)
							{
								num = 10;
								continue;
							}
							goto IL_60;
						case 10:
							this.BeginUpdate();
							num = 12;
							continue;
						case 11:
							if (value != LineStyleType.None)
							{
								num = 3;
								continue;
							}
							goto IL_118;
						case 12:
							goto IL_60;
						case 13:
							goto IL_118;
						}
						break;
						IL_60:
						this.ᜀ.ᜀ(true);
						flag = true;
						num = 13;
						continue;
						IL_98:
						num = 0;
						continue;
						IL_118:
						num = 8;
						continue;
						IL_123:
						if (!flag)
						{
							return;
						}
						num = 5;
					}
				}
				IL_E7:
				if (true)
				{
				}
				if (false)
				{
				}
			}
		}

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06005DB2 RID: 23986 RVA: 0x003ACC70 File Offset: 0x003ABC70
		// (set) Token: 0x06005DB3 RID: 23987 RVA: 0x003ACCBC File Offset: 0x003ABCBC
		public LineStyleType DiagonalDownBorderLineStyle
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
				this.BeforeRead();
				return this.ᜀ.\u173C();
			}
			set
			{
				for (;;)
				{
					bool flag = false;
					int num = 2;
					for (;;)
					{
						switch (num)
						{
						case 0:
							this.BeginUpdate();
							num = 3;
							continue;
						case 1:
							this.EndUpdate();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_136;
							}
							if (true)
							{
							}
							if (false)
							{
							}
							num = 6;
							continue;
						case 2:
							if (this.DiagonalDownBorderLineStyle != value)
							{
								num = 9;
								continue;
							}
							goto IL_83;
						case 3:
							goto IL_58;
						case 4:
							goto IL_110;
						case 5:
							goto IL_136;
						case 6:
							return;
						case 7:
							if (!this.ᜀ.ᜮ())
							{
								num = 10;
								continue;
							}
							goto IL_110;
						case 8:
							goto IL_83;
						case 9:
							this.BeginUpdate();
							this.ᜀ.ᜁ(value);
							flag = true;
							num = 8;
							continue;
						case 10:
							num = 5;
							continue;
						case 11:
							if (flag)
							{
								num = 1;
								continue;
							}
							return;
						}
						break;
						IL_58:
						this.ᜀ.ᜄ(true);
						flag = true;
						num = 4;
						continue;
						IL_136:
						if (!flag)
						{
							num = 0;
							continue;
						}
						goto IL_58;
						IL_83:
						num = 7;
						continue;
						IL_110:
						num = 11;
					}
				}
			}
		}

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06005DB4 RID: 23988 RVA: 0x003ACE18 File Offset: 0x003ABE18
		// (set) Token: 0x06005DB5 RID: 23989 RVA: 0x003ACE64 File Offset: 0x003ABE64
		public bool DiagonalUpVisible
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
				this.BeforeRead();
				return this.ᜀ.ᜢ();
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 2;
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
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.ᜀ(value);
								this.EndUpdate();
								num = 1;
								continue;
							}
							break;
						case 1:
							return;
						}
						if (this.DiagonalUpVisible == value)
						{
							return;
						}
						num = 0;
					}
				}
			}
		}

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06005DB6 RID: 23990 RVA: 0x003ACEF0 File Offset: 0x003ABEF0
		// (set) Token: 0x06005DB7 RID: 23991 RVA: 0x003ACF3C File Offset: 0x003ABF3C
		public bool DiagonalDownVisible
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
				this.BeforeRead();
				return this.ᜀ.ᜮ();
			}
			set
			{
				for (;;)
				{
					IL_00:
					int num = 0;
					for (;;)
					{
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							break;
						case 1:
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_00;
							default:
								if (false)
								{
								}
								this.BeginUpdate();
								this.ᜀ.ᜄ(value);
								this.EndUpdate();
								num = 2;
								continue;
							}
							break;
						case 2:
							return;
						}
						if (this.DiagonalDownVisible == value)
						{
							return;
						}
						num = 1;
					}
				}
			}
		}

		// Token: 0x14000023 RID: 35
		// (add) Token: 0x06005DB8 RID: 23992 RVA: 0x003ACFC8 File Offset: 0x003ABFC8
		// (remove) Token: 0x06005DB9 RID: 23993 RVA: 0x003AD060 File Offset: 0x003AC060
		internal event EventHandler NumberFormatChanged
		{
			add
			{
				for (;;)
				{
					EventHandler eventHandler = this.ᜃ;
					int num = 0;
					for (;;)
					{
						EventHandler eventHandler2;
						switch (num)
						{
						case 0:
							if (true)
							{
							}
							goto IL_2D;
						case 1:
							if (eventHandler != eventHandler2)
							{
								goto IL_2D;
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_4A;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						case 2:
							return;
						}
						break;
						IL_4A:
						num = 1;
						continue;
						IL_2D:
						eventHandler2 = eventHandler;
						EventHandler value2 = (EventHandler)Delegate.Combine(eventHandler2, value);
						eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜃ, value2, eventHandler2);
						goto IL_4A;
					}
				}
			}
			remove
			{
				for (;;)
				{
					EventHandler eventHandler = this.ᜃ;
					int num = 1;
					for (;;)
					{
						EventHandler eventHandler2;
						switch (num)
						{
						case 0:
							if (eventHandler != eventHandler2)
							{
								goto IL_25;
							}
							if (true)
							{
							}
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								goto IL_42;
							default:
								if (false)
								{
								}
								num = 2;
								continue;
							}
							break;
						case 1:
							goto IL_25;
						case 2:
							return;
						}
						break;
						IL_42:
						num = 0;
						continue;
						IL_25:
						eventHandler2 = eventHandler;
						EventHandler value2 = (EventHandler)Delegate.Remove(eventHandler2, value);
						eventHandler = Interlocked.CompareExchange<EventHandler>(ref this.ᜃ, value2, eventHandler2);
						goto IL_42;
					}
				}
			}
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06005DBA RID: 23994 RVA: 0x003AD0F8 File Offset: 0x003AC0F8
		internal spr\u1DF5 ReservedHandle
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
				return this.ᜀ.ReservedHandle;
			}
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06005DBB RID: 23995 RVA: 0x003AD140 File Offset: 0x003AC140
		public object Parent
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
				return this.ᜀ.Parent;
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06005DBC RID: 23996 RVA: 0x003AD188 File Offset: 0x003AC188
		public bool BuiltIn
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
				IStyle style = this.ᜀ();
				return style.BuiltIn;
			}
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06005DBD RID: 23997 RVA: 0x003AD1D0 File Offset: 0x003AC1D0
		public string Name
		{
			get
			{
				int a_ = 15;
				XlsStyle xlsStyle;
				for (;;)
				{
					this.BeforeRead();
					sprỶ sprỶ = this.ᜀ.ᜑ();
					int index = (int)sprỶ.\u1713();
					xlsStyle = this.m_book.InnerStyles.GetByXFIndex(index);
					int num = 2;
					for (;;)
					{
						if (true)
						{
						}
						switch (num)
						{
						case 0:
							for (;;)
							{
								xlsStyle = (this.m_book.InnerStyles[RecordTableEnumerator.b("ୄ⡆㭈♊ⱌ⍎", a_)] as XlsStyle);
								this.ᜀ.ᜄ((int)((ushort)xlsStyle.Index));
								switch ((1 == 1) ? 1 : 0)
								{
								case 0:
								case 2:
									break;
								default:
									goto IL_B1;
								}
							}
							IL_B1:
							if (false)
							{
							}
							num = 1;
							continue;
						case 1:
							goto IL_CC;
						case 2:
							if (xlsStyle == null)
							{
								num = 0;
								continue;
							}
							goto IL_CE;
						}
						break;
					}
				}
				IL_CC:
				IL_CE:
				return xlsStyle.Name;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06005DBE RID: 23998 RVA: 0x003AD2B4 File Offset: 0x003AC2B4
		public bool IsInitialized
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
				this.BeforeRead();
				string name = XlsStyle.DEF_DEFAULT_STYLES[0];
				return !XlsStylesCollection.CompareStyles(this, this.m_book.Styles[name]);
			}
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x003AD318 File Offset: 0x003AC318
		public override void BeginUpdate()
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
			base.BeginUpdate();
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x003AD35C File Offset: 0x003AC35C
		public override void EndUpdate()
		{
			for (;;)
			{
				base.EndUpdate();
				int num = 1;
				for (;;)
				{
					switch (num)
					{
					case 0:
						goto IL_5E;
					case 1:
						if (base.BeginCallsCount == 0)
						{
							num = 2;
							continue;
						}
						goto IL_6A;
					case 2:
						for (;;)
						{
							this.SetChanged();
							switch ((1 == 1) ? 1 : 0)
							{
							case 0:
							case 2:
								break;
							default:
								goto IL_50;
							}
						}
						IL_50:
						if (false)
						{
						}
						num = 0;
						continue;
					}
					break;
				}
			}
			IL_5E:
			IL_6A:
			if (true)
			{
			}
		}

		// Token: 0x04002D61 RID: 11617
		private string \u2593\u009D\u008E\u00AF;

		// Token: 0x04002D62 RID: 11618
		internal spr\u192F ᜀ;

		// Token: 0x04002D63 RID: 11619
		protected XlsWorkbook m_book;

		// Token: 0x04002D64 RID: 11620
		private bool[] \u2460\u00AD\u0081\u0090;

		// Token: 0x04002D65 RID: 11621
		protected FontWrapper m_font;

		// Token: 0x04002D66 RID: 11622
		private float \u2609\u0093\u00ACª;

		// Token: 0x04002D67 RID: 11623
		private int[] \u2609\u00A1\u0091\u00AC;

		// Token: 0x04002D68 RID: 11624
		private long \u2593\u009B\u00AB\u0089;

		// Token: 0x04002D69 RID: 11625
		private XlsBordersCollection ᜁ;

		// Token: 0x04002D6A RID: 11626
		private sprឰ ᜂ;

		// Token: 0x04002D6B RID: 11627
		private EventHandler ᜃ;
	}
}
