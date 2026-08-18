using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000690 RID: 1680
	internal sealed class RegexCode
	{
		// Token: 0x06003E31 RID: 15921 RVA: 0x00100A4C File Offset: 0x000FEC4C
		internal RegexCode(int[] codes, List<string> stringlist, int trackcount, Hashtable caps, int capsize, RegexBoyerMoore bmPrefix, RegexPrefix fcPrefix, int anchors, bool rightToLeft)
		{
			this._codes = codes;
			this._strings = new string[stringlist.Count];
			this._trackcount = trackcount;
			this._caps = caps;
			this._capsize = capsize;
			this._bmPrefix = bmPrefix;
			this._fcPrefix = fcPrefix;
			this._anchors = anchors;
			this._rightToLeft = rightToLeft;
			stringlist.CopyTo(0, this._strings, 0, stringlist.Count);
		}

		// Token: 0x06003E32 RID: 15922 RVA: 0x00100AC4 File Offset: 0x000FECC4
		internal static bool OpcodeBacktracks(int Op)
		{
			Op &= 63;
			switch (Op)
			{
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 31:
			case 32:
			case 33:
			case 34:
			case 35:
			case 36:
			case 38:
				return true;
			}
			return false;
		}

		// Token: 0x06003E33 RID: 15923 RVA: 0x00100B74 File Offset: 0x000FED74
		internal static int OpcodeSize(int Opcode)
		{
			Opcode &= 63;
			switch (Opcode)
			{
			case 0:
			case 1:
			case 2:
			case 3:
			case 4:
			case 5:
			case 6:
			case 7:
			case 8:
			case 28:
			case 29:
			case 32:
				return 3;
			case 9:
			case 10:
			case 11:
			case 12:
			case 13:
			case 23:
			case 24:
			case 25:
			case 26:
			case 27:
			case 37:
			case 38:
			case 39:
				return 2;
			case 14:
			case 15:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 22:
			case 30:
			case 31:
			case 33:
			case 34:
			case 35:
			case 36:
			case 40:
			case 41:
			case 42:
				return 1;
			default:
				throw RegexCode.MakeException(SR.GetString("UnexpectedOpcode", new object[]
				{
					Opcode.ToString(CultureInfo.CurrentCulture)
				}));
			}
		}

		// Token: 0x06003E34 RID: 15924 RVA: 0x00100C65 File Offset: 0x000FEE65
		internal static ArgumentException MakeException(string message)
		{
			return new ArgumentException(message);
		}

		// Token: 0x04002D3A RID: 11578
		internal const int Onerep = 0;

		// Token: 0x04002D3B RID: 11579
		internal const int Notonerep = 1;

		// Token: 0x04002D3C RID: 11580
		internal const int Setrep = 2;

		// Token: 0x04002D3D RID: 11581
		internal const int Oneloop = 3;

		// Token: 0x04002D3E RID: 11582
		internal const int Notoneloop = 4;

		// Token: 0x04002D3F RID: 11583
		internal const int Setloop = 5;

		// Token: 0x04002D40 RID: 11584
		internal const int Onelazy = 6;

		// Token: 0x04002D41 RID: 11585
		internal const int Notonelazy = 7;

		// Token: 0x04002D42 RID: 11586
		internal const int Setlazy = 8;

		// Token: 0x04002D43 RID: 11587
		internal const int One = 9;

		// Token: 0x04002D44 RID: 11588
		internal const int Notone = 10;

		// Token: 0x04002D45 RID: 11589
		internal const int Set = 11;

		// Token: 0x04002D46 RID: 11590
		internal const int Multi = 12;

		// Token: 0x04002D47 RID: 11591
		internal const int Ref = 13;

		// Token: 0x04002D48 RID: 11592
		internal const int Bol = 14;

		// Token: 0x04002D49 RID: 11593
		internal const int Eol = 15;

		// Token: 0x04002D4A RID: 11594
		internal const int Boundary = 16;

		// Token: 0x04002D4B RID: 11595
		internal const int Nonboundary = 17;

		// Token: 0x04002D4C RID: 11596
		internal const int Beginning = 18;

		// Token: 0x04002D4D RID: 11597
		internal const int Start = 19;

		// Token: 0x04002D4E RID: 11598
		internal const int EndZ = 20;

		// Token: 0x04002D4F RID: 11599
		internal const int End = 21;

		// Token: 0x04002D50 RID: 11600
		internal const int Nothing = 22;

		// Token: 0x04002D51 RID: 11601
		internal const int Lazybranch = 23;

		// Token: 0x04002D52 RID: 11602
		internal const int Branchmark = 24;

		// Token: 0x04002D53 RID: 11603
		internal const int Lazybranchmark = 25;

		// Token: 0x04002D54 RID: 11604
		internal const int Nullcount = 26;

		// Token: 0x04002D55 RID: 11605
		internal const int Setcount = 27;

		// Token: 0x04002D56 RID: 11606
		internal const int Branchcount = 28;

		// Token: 0x04002D57 RID: 11607
		internal const int Lazybranchcount = 29;

		// Token: 0x04002D58 RID: 11608
		internal const int Nullmark = 30;

		// Token: 0x04002D59 RID: 11609
		internal const int Setmark = 31;

		// Token: 0x04002D5A RID: 11610
		internal const int Capturemark = 32;

		// Token: 0x04002D5B RID: 11611
		internal const int Getmark = 33;

		// Token: 0x04002D5C RID: 11612
		internal const int Setjump = 34;

		// Token: 0x04002D5D RID: 11613
		internal const int Backjump = 35;

		// Token: 0x04002D5E RID: 11614
		internal const int Forejump = 36;

		// Token: 0x04002D5F RID: 11615
		internal const int Testref = 37;

		// Token: 0x04002D60 RID: 11616
		internal const int Goto = 38;

		// Token: 0x04002D61 RID: 11617
		internal const int Prune = 39;

		// Token: 0x04002D62 RID: 11618
		internal const int Stop = 40;

		// Token: 0x04002D63 RID: 11619
		internal const int ECMABoundary = 41;

		// Token: 0x04002D64 RID: 11620
		internal const int NonECMABoundary = 42;

		// Token: 0x04002D65 RID: 11621
		internal const int Mask = 63;

		// Token: 0x04002D66 RID: 11622
		internal const int Rtl = 64;

		// Token: 0x04002D67 RID: 11623
		internal const int Back = 128;

		// Token: 0x04002D68 RID: 11624
		internal const int Back2 = 256;

		// Token: 0x04002D69 RID: 11625
		internal const int Ci = 512;

		// Token: 0x04002D6A RID: 11626
		internal int[] _codes;

		// Token: 0x04002D6B RID: 11627
		internal string[] _strings;

		// Token: 0x04002D6C RID: 11628
		internal int _trackcount;

		// Token: 0x04002D6D RID: 11629
		internal Hashtable _caps;

		// Token: 0x04002D6E RID: 11630
		internal int _capsize;

		// Token: 0x04002D6F RID: 11631
		internal RegexPrefix _fcPrefix;

		// Token: 0x04002D70 RID: 11632
		internal RegexBoyerMoore _bmPrefix;

		// Token: 0x04002D71 RID: 11633
		internal int _anchors;

		// Token: 0x04002D72 RID: 11634
		internal bool _rightToLeft;
	}
}
