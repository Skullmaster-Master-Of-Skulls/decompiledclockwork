using System;

namespace Telerik.Pdf.Gdi.Font
{
	// Token: 0x02001626 RID: 5670
	internal class OS2Table : FontTable
	{
		// Token: 0x0600DC7F RID: 56447 RVA: 0x00303176 File Offset: 0x00301376
		public OS2Table(DirectoryEntry entry) : base("OS/2", entry)
		{
		}

		// Token: 0x17004378 RID: 17272
		// (get) Token: 0x0600DC80 RID: 56448 RVA: 0x0030319D File Offset: 0x0030139D
		public bool IsItalic
		{
			get
			{
				return (this.fsSelection & 1) == 1;
			}
		}

		// Token: 0x17004379 RID: 17273
		// (get) Token: 0x0600DC81 RID: 56449 RVA: 0x003031AA File Offset: 0x003013AA
		public bool IsRegular
		{
			get
			{
				return (this.fsSelection & 64) == 64;
			}
		}

		// Token: 0x1700437A RID: 17274
		// (get) Token: 0x0600DC82 RID: 56450 RVA: 0x003031B9 File Offset: 0x003013B9
		public bool IsBold
		{
			get
			{
				return (this.fsSelection & 32) == 32 || this.usWeightClass >= 700;
			}
		}

		// Token: 0x1700437B RID: 17275
		// (get) Token: 0x0600DC83 RID: 56451 RVA: 0x003031DA File Offset: 0x003013DA
		public bool IsMonospaced
		{
			get
			{
				return this.panose[3] == 9;
			}
		}

		// Token: 0x1700437C RID: 17276
		// (get) Token: 0x0600DC84 RID: 56452 RVA: 0x003031E8 File Offset: 0x003013E8
		public bool IsSymbolic
		{
			get
			{
				return this.classID == 12;
			}
		}

		// Token: 0x1700437D RID: 17277
		// (get) Token: 0x0600DC85 RID: 56453 RVA: 0x003031F4 File Offset: 0x003013F4
		public bool IsSerif
		{
			get
			{
				return this.classID == 1 || this.classID == 2 || this.classID == 3 || this.classID == 4 || this.classID == 5 || this.classID == 7;
			}
		}

		// Token: 0x1700437E RID: 17278
		// (get) Token: 0x0600DC86 RID: 56454 RVA: 0x0030322E File Offset: 0x0030142E
		public bool IsScript
		{
			get
			{
				return this.classID == 10;
			}
		}

		// Token: 0x1700437F RID: 17279
		// (get) Token: 0x0600DC87 RID: 56455 RVA: 0x0030323A File Offset: 0x0030143A
		public bool IsSansSerif
		{
			get
			{
				return this.classID == 8;
			}
		}

		// Token: 0x17004380 RID: 17280
		// (get) Token: 0x0600DC88 RID: 56456 RVA: 0x00303245 File Offset: 0x00301445
		public bool IsEmbeddable
		{
			get
			{
				return this.InstallableEmbedding || this.EditableEmbedding || this.PreviewAndPrintEmbedding;
			}
		}

		// Token: 0x17004381 RID: 17281
		// (get) Token: 0x0600DC89 RID: 56457 RVA: 0x0030325F File Offset: 0x0030145F
		public bool InstallableEmbedding
		{
			get
			{
				return this.fsType == 0;
			}
		}

		// Token: 0x17004382 RID: 17282
		// (get) Token: 0x0600DC8A RID: 56458 RVA: 0x0030326A File Offset: 0x0030146A
		public bool RestricedLicenseEmbedding
		{
			get
			{
				return (this.fsType & 2) == 2;
			}
		}

		// Token: 0x17004383 RID: 17283
		// (get) Token: 0x0600DC8B RID: 56459 RVA: 0x00303277 File Offset: 0x00301477
		public bool EditableEmbedding
		{
			get
			{
				return (this.fsType & 8) == 8;
			}
		}

		// Token: 0x17004384 RID: 17284
		// (get) Token: 0x0600DC8C RID: 56460 RVA: 0x00303284 File Offset: 0x00301484
		public bool PreviewAndPrintEmbedding
		{
			get
			{
				return (this.fsType & 4) == 4;
			}
		}

		// Token: 0x17004385 RID: 17285
		// (get) Token: 0x0600DC8D RID: 56461 RVA: 0x00303291 File Offset: 0x00301491
		public bool IsSubsettable
		{
			get
			{
				return (this.fsType & 256) != 256;
			}
		}

		// Token: 0x17004386 RID: 17286
		// (get) Token: 0x0600DC8E RID: 56462 RVA: 0x003032A9 File Offset: 0x003014A9
		public int CapHeight
		{
			get
			{
				return (int)this.sCapHeight;
			}
		}

		// Token: 0x17004387 RID: 17287
		// (get) Token: 0x0600DC8F RID: 56463 RVA: 0x003032B1 File Offset: 0x003014B1
		public int XHeight
		{
			get
			{
				return (int)this.sxHeight;
			}
		}

		// Token: 0x17004388 RID: 17288
		// (get) Token: 0x0600DC90 RID: 56464 RVA: 0x003032B9 File Offset: 0x003014B9
		public int FirstChar
		{
			get
			{
				return this.usFirstCharIndex;
			}
		}

		// Token: 0x17004389 RID: 17289
		// (get) Token: 0x0600DC91 RID: 56465 RVA: 0x003032C1 File Offset: 0x003014C1
		public int LastChar
		{
			get
			{
				return this.usLastCharIndex;
			}
		}

		// Token: 0x0600DC92 RID: 56466 RVA: 0x003032CC File Offset: 0x003014CC
		protected internal override void Read(FontFileReader reader)
		{
			FontFileStream stream = reader.Stream;
			this.version = stream.ReadUShort();
			this.avgCharWidth = stream.ReadShort();
			this.usWeightClass = stream.ReadUShort();
			this.usWidthClass = stream.ReadUShort();
			this.fsType = (stream.ReadUShort() & -2);
			this.subscriptXSize = stream.ReadShort();
			this.subscriptYSize = stream.ReadShort();
			this.subscriptXOffset = stream.ReadShort();
			this.subscriptYOffset = stream.ReadShort();
			this.superscriptXSize = stream.ReadShort();
			this.superscriptYSize = stream.ReadShort();
			this.superscriptXOffset = stream.ReadShort();
			this.superscriptYOffset = stream.ReadShort();
			this.strikeoutSize = stream.ReadShort();
			this.strikeoutPosition = stream.ReadShort();
			short num = stream.ReadShort();
			this.classID = (byte)(num >> 8);
			this.subclassID = (byte)(num & 255);
			stream.Read(this.panose, 0, this.panose.Length);
			this.unicodeRange1 = stream.ReadULong();
			this.unicodeRange2 = stream.ReadULong();
			this.unicodeRange3 = stream.ReadULong();
			this.unicodeRange4 = stream.ReadULong();
			this.vendorID[0] = stream.ReadChar();
			this.vendorID[1] = stream.ReadChar();
			this.vendorID[2] = stream.ReadChar();
			this.vendorID[3] = stream.ReadChar();
			this.fsSelection = stream.ReadUShort();
			this.usFirstCharIndex = stream.ReadUShort();
			this.usLastCharIndex = stream.ReadUShort();
			this.typoAscender = stream.ReadShort();
			this.typoDescender = stream.ReadShort();
			this.typoLineGap = stream.ReadShort();
			this.usWinAscent = stream.ReadUShort();
			this.usWinDescent = stream.ReadUShort();
			this.codePageRange1 = stream.ReadULong();
			this.codePageRange2 = stream.ReadULong();
			this.sxHeight = stream.ReadShort();
			this.sCapHeight = stream.ReadShort();
			this.usDefaultChar = stream.ReadUShort();
			this.usBreakChar = stream.ReadUShort();
			this.usMaxContext = stream.ReadUShort();
		}

		// Token: 0x0600DC93 RID: 56467 RVA: 0x003034E8 File Offset: 0x003016E8
		protected internal override void Write(FontFileWriter writer)
		{
			throw new NotImplementedException("Write is not implemented.");
		}

		// Token: 0x04003E00 RID: 15872
		private const int OldStyleSerifs = 1;

		// Token: 0x04003E01 RID: 15873
		private const int TransitionalSerifs = 2;

		// Token: 0x04003E02 RID: 15874
		private const int ModernSerifs = 3;

		// Token: 0x04003E03 RID: 15875
		private const int ClarendonSerifs = 4;

		// Token: 0x04003E04 RID: 15876
		private const int SlabSerifs = 5;

		// Token: 0x04003E05 RID: 15877
		private const int FreeformSerifs = 7;

		// Token: 0x04003E06 RID: 15878
		private const int SansSerif = 8;

		// Token: 0x04003E07 RID: 15879
		private const int Scripts = 10;

		// Token: 0x04003E08 RID: 15880
		private const int Symbolic = 12;

		// Token: 0x04003E09 RID: 15881
		private int version;

		// Token: 0x04003E0A RID: 15882
		private short avgCharWidth;

		// Token: 0x04003E0B RID: 15883
		private int usWeightClass;

		// Token: 0x04003E0C RID: 15884
		private int usWidthClass;

		// Token: 0x04003E0D RID: 15885
		private int fsType;

		// Token: 0x04003E0E RID: 15886
		private short subscriptXSize;

		// Token: 0x04003E0F RID: 15887
		private short subscriptYSize;

		// Token: 0x04003E10 RID: 15888
		private short subscriptXOffset;

		// Token: 0x04003E11 RID: 15889
		private short subscriptYOffset;

		// Token: 0x04003E12 RID: 15890
		private short superscriptXSize;

		// Token: 0x04003E13 RID: 15891
		private short superscriptYSize;

		// Token: 0x04003E14 RID: 15892
		private short superscriptXOffset;

		// Token: 0x04003E15 RID: 15893
		private short superscriptYOffset;

		// Token: 0x04003E16 RID: 15894
		private short strikeoutSize;

		// Token: 0x04003E17 RID: 15895
		private short strikeoutPosition;

		// Token: 0x04003E18 RID: 15896
		private byte classID;

		// Token: 0x04003E19 RID: 15897
		private byte subclassID;

		// Token: 0x04003E1A RID: 15898
		private byte[] panose = new byte[10];

		// Token: 0x04003E1B RID: 15899
		private int unicodeRange1;

		// Token: 0x04003E1C RID: 15900
		private int unicodeRange2;

		// Token: 0x04003E1D RID: 15901
		private int unicodeRange3;

		// Token: 0x04003E1E RID: 15902
		private int unicodeRange4;

		// Token: 0x04003E1F RID: 15903
		private sbyte[] vendorID = new sbyte[4];

		// Token: 0x04003E20 RID: 15904
		private int fsSelection;

		// Token: 0x04003E21 RID: 15905
		private int usFirstCharIndex;

		// Token: 0x04003E22 RID: 15906
		private int usLastCharIndex;

		// Token: 0x04003E23 RID: 15907
		private short typoAscender;

		// Token: 0x04003E24 RID: 15908
		private short typoDescender;

		// Token: 0x04003E25 RID: 15909
		private short typoLineGap;

		// Token: 0x04003E26 RID: 15910
		private int usWinAscent;

		// Token: 0x04003E27 RID: 15911
		private int usWinDescent;

		// Token: 0x04003E28 RID: 15912
		private int codePageRange1;

		// Token: 0x04003E29 RID: 15913
		private int codePageRange2;

		// Token: 0x04003E2A RID: 15914
		private short sxHeight;

		// Token: 0x04003E2B RID: 15915
		private short sCapHeight;

		// Token: 0x04003E2C RID: 15916
		private int usDefaultChar;

		// Token: 0x04003E2D RID: 15917
		private int usBreakChar;

		// Token: 0x04003E2E RID: 15918
		private int usMaxContext;
	}
}
