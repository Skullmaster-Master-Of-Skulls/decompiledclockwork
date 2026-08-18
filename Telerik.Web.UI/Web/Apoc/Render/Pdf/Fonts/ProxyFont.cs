using System;
using Telerik.Pdf;
using Telerik.Pdf.Gdi;
using Telerik.Web.Apoc.Layout;

namespace Telerik.Web.Apoc.Render.Pdf.Fonts
{
	// Token: 0x02001691 RID: 5777
	internal class ProxyFont : Font, IFontDescriptor, IDisposable
	{
		// Token: 0x0600DEFE RID: 57086 RVA: 0x00314A47 File Offset: 0x00312C47
		public ProxyFont(FontProperties properties, FontType fontType)
		{
			this.properties = properties;
			this.fontType = fontType;
		}

		// Token: 0x0600DEFF RID: 57087 RVA: 0x00314A60 File Offset: 0x00312C60
		private void LoadIfNecessary()
		{
			if (!this.fontLoaded)
			{
				switch (this.fontType)
				{
				case FontType.Link:
					this.realFont = new TrueTypeFont(this.properties);
					break;
				case FontType.Embed:
				case FontType.Subset:
					this.realFont = this.LoadCIDFont();
					break;
				default:
					throw new Exception("Unknown font type: " + this.fontType.ToString());
				}
				this.fontLoaded = true;
			}
		}

		// Token: 0x0600DF00 RID: 57088 RVA: 0x00314AD8 File Offset: 0x00312CD8
		private Font LoadCIDFont()
		{
			switch (this.fontType)
			{
			case FontType.Embed:
				this.realFont = new Type2CIDFont(this.properties);
				break;
			case FontType.Subset:
				this.realFont = new Type2CIDSubsetFont(this.properties);
				break;
			}
			bool flag = false;
			IFontDescriptor descriptor = this.realFont.Descriptor;
			if (!descriptor.IsEmbeddable)
			{
				ApocDriver.ActiveDriver.FireApocWarning(string.Format("Unable to embed font '{0}' because the license states embedding is not allowed.  Will default to Helvetica.", this.realFont.FontName));
				flag = true;
			}
			if (this.realFont is Type2CIDSubsetFont && !descriptor.IsSubsettable)
			{
				ApocDriver.ActiveDriver.FireApocWarning(string.Format("Unable to subset font '{0}' because the license states subsetting is not allowed..  Will default to Helvetica.", this.realFont.FontName));
				flag = true;
			}
			if (flag)
			{
				if (this.properties.IsBoldItalic)
				{
					this.realFont = Base14Font.HelveticaBoldItalic;
				}
				else if (this.properties.IsBold)
				{
					this.realFont = Base14Font.HelveticaBold;
				}
				else if (this.properties.IsItalic)
				{
					this.realFont = Base14Font.HelveticaItalic;
				}
				else
				{
					this.realFont = Base14Font.Helvetica;
				}
			}
			return this.realFont;
		}

		// Token: 0x17004441 RID: 17473
		// (get) Token: 0x0600DF01 RID: 57089 RVA: 0x00314BF1 File Offset: 0x00312DF1
		public Font RealFont
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont;
			}
		}

		// Token: 0x17004442 RID: 17474
		// (get) Token: 0x0600DF02 RID: 57090 RVA: 0x00314BFF File Offset: 0x00312DFF
		public override PdfFontSubTypeEnum SubType
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.SubType;
			}
		}

		// Token: 0x17004443 RID: 17475
		// (get) Token: 0x0600DF03 RID: 57091 RVA: 0x00314C12 File Offset: 0x00312E12
		public override string FontName
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.FontName;
			}
		}

		// Token: 0x17004444 RID: 17476
		// (get) Token: 0x0600DF04 RID: 57092 RVA: 0x00314C25 File Offset: 0x00312E25
		public override PdfFontTypeEnum Type
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Type;
			}
		}

		// Token: 0x17004445 RID: 17477
		// (get) Token: 0x0600DF05 RID: 57093 RVA: 0x00314C38 File Offset: 0x00312E38
		public override string Encoding
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Encoding;
			}
		}

		// Token: 0x17004446 RID: 17478
		// (get) Token: 0x0600DF06 RID: 57094 RVA: 0x00314C4B File Offset: 0x00312E4B
		public override IFontDescriptor Descriptor
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor;
			}
		}

		// Token: 0x17004447 RID: 17479
		// (get) Token: 0x0600DF07 RID: 57095 RVA: 0x00314C5E File Offset: 0x00312E5E
		public override bool MultiByteFont
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.MultiByteFont;
			}
		}

		// Token: 0x0600DF08 RID: 57096 RVA: 0x00314C71 File Offset: 0x00312E71
		public override int MapCharacter(char c)
		{
			this.LoadIfNecessary();
			return this.realFont.MapCharacter(c);
		}

		// Token: 0x17004448 RID: 17480
		// (get) Token: 0x0600DF09 RID: 57097 RVA: 0x00314C85 File Offset: 0x00312E85
		public override int Ascender
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Ascender;
			}
		}

		// Token: 0x17004449 RID: 17481
		// (get) Token: 0x0600DF0A RID: 57098 RVA: 0x00314C98 File Offset: 0x00312E98
		public override int Descender
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descender;
			}
		}

		// Token: 0x1700444A RID: 17482
		// (get) Token: 0x0600DF0B RID: 57099 RVA: 0x00314CAB File Offset: 0x00312EAB
		public override int CapHeight
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.CapHeight;
			}
		}

		// Token: 0x1700444B RID: 17483
		// (get) Token: 0x0600DF0C RID: 57100 RVA: 0x00314CBE File Offset: 0x00312EBE
		public override int FirstChar
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.FirstChar;
			}
		}

		// Token: 0x1700444C RID: 17484
		// (get) Token: 0x0600DF0D RID: 57101 RVA: 0x00314CD1 File Offset: 0x00312ED1
		public override int LastChar
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.LastChar;
			}
		}

		// Token: 0x0600DF0E RID: 57102 RVA: 0x00314CE4 File Offset: 0x00312EE4
		public override int GetWidth(int charIndex)
		{
			this.LoadIfNecessary();
			return this.realFont.GetWidth(charIndex);
		}

		// Token: 0x1700444D RID: 17485
		// (get) Token: 0x0600DF0F RID: 57103 RVA: 0x00314CF8 File Offset: 0x00312EF8
		public override int[] Widths
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Widths;
			}
		}

		// Token: 0x1700444E RID: 17486
		// (get) Token: 0x0600DF10 RID: 57104 RVA: 0x00314D0B File Offset: 0x00312F0B
		public int Flags
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor.Flags;
			}
		}

		// Token: 0x1700444F RID: 17487
		// (get) Token: 0x0600DF11 RID: 57105 RVA: 0x00314D23 File Offset: 0x00312F23
		public int[] FontBBox
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor.FontBBox;
			}
		}

		// Token: 0x17004450 RID: 17488
		// (get) Token: 0x0600DF12 RID: 57106 RVA: 0x00314D3B File Offset: 0x00312F3B
		public int ItalicAngle
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor.ItalicAngle;
			}
		}

		// Token: 0x17004451 RID: 17489
		// (get) Token: 0x0600DF13 RID: 57107 RVA: 0x00314D53 File Offset: 0x00312F53
		public int StemV
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor.StemV;
			}
		}

		// Token: 0x17004452 RID: 17490
		// (get) Token: 0x0600DF14 RID: 57108 RVA: 0x00314D6B File Offset: 0x00312F6B
		public bool HasKerningInfo
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor.HasKerningInfo;
			}
		}

		// Token: 0x17004453 RID: 17491
		// (get) Token: 0x0600DF15 RID: 57109 RVA: 0x00314D83 File Offset: 0x00312F83
		public bool IsEmbeddable
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor.IsEmbeddable;
			}
		}

		// Token: 0x17004454 RID: 17492
		// (get) Token: 0x0600DF16 RID: 57110 RVA: 0x00314D9B File Offset: 0x00312F9B
		public bool IsSubsettable
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor.IsSubsettable;
			}
		}

		// Token: 0x17004455 RID: 17493
		// (get) Token: 0x0600DF17 RID: 57111 RVA: 0x00314DB3 File Offset: 0x00312FB3
		public byte[] FontData
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor.FontData;
			}
		}

		// Token: 0x17004456 RID: 17494
		// (get) Token: 0x0600DF18 RID: 57112 RVA: 0x00314DCB File Offset: 0x00312FCB
		public GdiKerningPairs KerningInfo
		{
			get
			{
				this.LoadIfNecessary();
				return this.realFont.Descriptor.KerningInfo;
			}
		}

		// Token: 0x0600DF19 RID: 57113 RVA: 0x00314DE3 File Offset: 0x00312FE3
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600DF1A RID: 57114 RVA: 0x00314DF4 File Offset: 0x00312FF4
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				TrueTypeFont trueTypeFont = this.realFont as TrueTypeFont;
				if (trueTypeFont != null)
				{
					trueTypeFont.Dispose();
				}
				Type2CIDFont type2CIDFont = this.realFont as Type2CIDFont;
				if (type2CIDFont != null)
				{
					type2CIDFont.Dispose();
				}
				Type2CIDSubsetFont type2CIDSubsetFont = this.realFont as Type2CIDSubsetFont;
				if (type2CIDSubsetFont != null)
				{
					type2CIDSubsetFont.Dispose();
				}
			}
		}

		// Token: 0x0400404B RID: 16459
		private bool fontLoaded;

		// Token: 0x0400404C RID: 16460
		private FontProperties properties;

		// Token: 0x0400404D RID: 16461
		private Font realFont;

		// Token: 0x0400404E RID: 16462
		private FontType fontType;
	}
}
