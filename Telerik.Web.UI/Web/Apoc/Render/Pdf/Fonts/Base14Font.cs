using System;
using Telerik.Pdf;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Render.Pdf.Fonts
{
	// Token: 0x02001684 RID: 5764
	internal abstract class Base14Font : Font
	{
		// Token: 0x0600DEC3 RID: 57027 RVA: 0x0030DF3C File Offset: 0x0030C13C
		public Base14Font(string fontName, string encoding, int capHeight, int ascender, int descender, int firstChar, int lastChar, int[] widths, CodePointMapping mapping)
		{
			this.fontName = fontName;
			this.encoding = encoding;
			this.capHeight = capHeight;
			this.ascender = ascender;
			this.descender = descender;
			this.firstChar = firstChar;
			this.lastChar = lastChar;
			this.widths = widths;
			this.mapping = mapping;
		}

		// Token: 0x1700441E RID: 17438
		// (get) Token: 0x0600DEC4 RID: 57028 RVA: 0x0030DF94 File Offset: 0x0030C194
		public override string Encoding
		{
			get
			{
				return this.mapping.Name;
			}
		}

		// Token: 0x1700441F RID: 17439
		// (get) Token: 0x0600DEC5 RID: 57029 RVA: 0x0030DFA1 File Offset: 0x0030C1A1
		public override string FontName
		{
			get
			{
				return this.fontName;
			}
		}

		// Token: 0x17004420 RID: 17440
		// (get) Token: 0x0600DEC6 RID: 57030 RVA: 0x0030DFA9 File Offset: 0x0030C1A9
		public override PdfFontTypeEnum Type
		{
			get
			{
				return PdfFontTypeEnum.Type1;
			}
		}

		// Token: 0x17004421 RID: 17441
		// (get) Token: 0x0600DEC7 RID: 57031 RVA: 0x0030DFAC File Offset: 0x0030C1AC
		public override PdfFontSubTypeEnum SubType
		{
			get
			{
				return PdfFontSubTypeEnum.Type1;
			}
		}

		// Token: 0x17004422 RID: 17442
		// (get) Token: 0x0600DEC8 RID: 57032 RVA: 0x0030DFAF File Offset: 0x0030C1AF
		public override IFontDescriptor Descriptor
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17004423 RID: 17443
		// (get) Token: 0x0600DEC9 RID: 57033 RVA: 0x0030DFB2 File Offset: 0x0030C1B2
		public override bool MultiByteFont
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17004424 RID: 17444
		// (get) Token: 0x0600DECA RID: 57034 RVA: 0x0030DFB5 File Offset: 0x0030C1B5
		public override int Ascender
		{
			get
			{
				return this.ascender;
			}
		}

		// Token: 0x17004425 RID: 17445
		// (get) Token: 0x0600DECB RID: 57035 RVA: 0x0030DFBD File Offset: 0x0030C1BD
		public override int Descender
		{
			get
			{
				return this.descender;
			}
		}

		// Token: 0x17004426 RID: 17446
		// (get) Token: 0x0600DECC RID: 57036 RVA: 0x0030DFC5 File Offset: 0x0030C1C5
		public override int CapHeight
		{
			get
			{
				return this.capHeight;
			}
		}

		// Token: 0x17004427 RID: 17447
		// (get) Token: 0x0600DECD RID: 57037 RVA: 0x0030DFCD File Offset: 0x0030C1CD
		public override int FirstChar
		{
			get
			{
				return this.firstChar;
			}
		}

		// Token: 0x17004428 RID: 17448
		// (get) Token: 0x0600DECE RID: 57038 RVA: 0x0030DFD5 File Offset: 0x0030C1D5
		public override int LastChar
		{
			get
			{
				return this.lastChar;
			}
		}

		// Token: 0x0600DECF RID: 57039 RVA: 0x0030DFDD File Offset: 0x0030C1DD
		public override int GetWidth(int charIndex)
		{
			return this.widths[charIndex];
		}

		// Token: 0x17004429 RID: 17449
		// (get) Token: 0x0600DED0 RID: 57040 RVA: 0x0030DFE8 File Offset: 0x0030C1E8
		public override int[] Widths
		{
			get
			{
				int[] array = new int[this.LastChar - this.FirstChar + 1];
				Array.Copy(this.widths, this.FirstChar, array, 0, this.LastChar - this.FirstChar + 1);
				return array;
			}
		}

		// Token: 0x0600DED1 RID: 57041 RVA: 0x0030E030 File Offset: 0x0030C230
		public override int MapCharacter(char c)
		{
			int num = this.mapping.MapCharacter(c);
			if (num != 0)
			{
				return num;
			}
			return (int)Convert.ToUInt16('#');
		}

		// Token: 0x04004015 RID: 16405
		public static readonly Font Courier = new Courier();

		// Token: 0x04004016 RID: 16406
		public static readonly Font CourierBold = new CourierBold();

		// Token: 0x04004017 RID: 16407
		public static readonly Font CourierItalic = new CourierOblique();

		// Token: 0x04004018 RID: 16408
		public static readonly Font CourierBoldItalic = new CourierBoldOblique();

		// Token: 0x04004019 RID: 16409
		public static readonly Font Helvetica = new Helvetica();

		// Token: 0x0400401A RID: 16410
		public static readonly Font HelveticaBold = new HelveticaBold();

		// Token: 0x0400401B RID: 16411
		public static readonly Font HelveticaItalic = new HelveticaOblique();

		// Token: 0x0400401C RID: 16412
		public static readonly Font HelveticaBoldItalic = new HelveticaBoldOblique();

		// Token: 0x0400401D RID: 16413
		public static readonly Font Times = new TimesRoman();

		// Token: 0x0400401E RID: 16414
		public static readonly Font TimesBold = new TimesBold();

		// Token: 0x0400401F RID: 16415
		public static readonly Font TimesItalic = new TimesItalic();

		// Token: 0x04004020 RID: 16416
		public static readonly Font TimesBoldItalic = new TimesBoldItalic();

		// Token: 0x04004021 RID: 16417
		public static readonly Font Symbol = new Symbol();

		// Token: 0x04004022 RID: 16418
		public static readonly Font ZapfDingbats = new ZapfDingbats();

		// Token: 0x04004023 RID: 16419
		private string fontName;

		// Token: 0x04004024 RID: 16420
		private string encoding;

		// Token: 0x04004025 RID: 16421
		private int capHeight;

		// Token: 0x04004026 RID: 16422
		private int ascender;

		// Token: 0x04004027 RID: 16423
		private int descender;

		// Token: 0x04004028 RID: 16424
		private int firstChar;

		// Token: 0x04004029 RID: 16425
		private int lastChar;

		// Token: 0x0400402A RID: 16426
		private int[] widths;

		// Token: 0x0400402B RID: 16427
		private CodePointMapping mapping;
	}
}
