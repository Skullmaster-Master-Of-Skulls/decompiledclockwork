using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Internal
{
	// Token: 0x020004DF RID: 1247
	internal class IntNativeMethods
	{
		// Token: 0x04003568 RID: 13672
		public const int MaxTextLengthInWin9x = 8192;

		// Token: 0x04003569 RID: 13673
		public const int DT_TOP = 0;

		// Token: 0x0400356A RID: 13674
		public const int DT_LEFT = 0;

		// Token: 0x0400356B RID: 13675
		public const int DT_CENTER = 1;

		// Token: 0x0400356C RID: 13676
		public const int DT_RIGHT = 2;

		// Token: 0x0400356D RID: 13677
		public const int DT_VCENTER = 4;

		// Token: 0x0400356E RID: 13678
		public const int DT_BOTTOM = 8;

		// Token: 0x0400356F RID: 13679
		public const int DT_WORDBREAK = 16;

		// Token: 0x04003570 RID: 13680
		public const int DT_SINGLELINE = 32;

		// Token: 0x04003571 RID: 13681
		public const int DT_EXPANDTABS = 64;

		// Token: 0x04003572 RID: 13682
		public const int DT_TABSTOP = 128;

		// Token: 0x04003573 RID: 13683
		public const int DT_NOCLIP = 256;

		// Token: 0x04003574 RID: 13684
		public const int DT_EXTERNALLEADING = 512;

		// Token: 0x04003575 RID: 13685
		public const int DT_CALCRECT = 1024;

		// Token: 0x04003576 RID: 13686
		public const int DT_NOPREFIX = 2048;

		// Token: 0x04003577 RID: 13687
		public const int DT_INTERNAL = 4096;

		// Token: 0x04003578 RID: 13688
		public const int DT_EDITCONTROL = 8192;

		// Token: 0x04003579 RID: 13689
		public const int DT_PATH_ELLIPSIS = 16384;

		// Token: 0x0400357A RID: 13690
		public const int DT_END_ELLIPSIS = 32768;

		// Token: 0x0400357B RID: 13691
		public const int DT_MODIFYSTRING = 65536;

		// Token: 0x0400357C RID: 13692
		public const int DT_RTLREADING = 131072;

		// Token: 0x0400357D RID: 13693
		public const int DT_WORD_ELLIPSIS = 262144;

		// Token: 0x0400357E RID: 13694
		public const int DT_NOFULLWIDTHCHARBREAK = 524288;

		// Token: 0x0400357F RID: 13695
		public const int DT_HIDEPREFIX = 1048576;

		// Token: 0x04003580 RID: 13696
		public const int DT_PREFIXONLY = 2097152;

		// Token: 0x04003581 RID: 13697
		public const int DIB_RGB_COLORS = 0;

		// Token: 0x04003582 RID: 13698
		public const int BI_BITFIELDS = 3;

		// Token: 0x04003583 RID: 13699
		public const int BI_RGB = 0;

		// Token: 0x04003584 RID: 13700
		public const int BITMAPINFO_MAX_COLORSIZE = 256;

		// Token: 0x04003585 RID: 13701
		public const int SPI_GETICONTITLELOGFONT = 31;

		// Token: 0x04003586 RID: 13702
		public const int SPI_GETNONCLIENTMETRICS = 41;

		// Token: 0x04003587 RID: 13703
		public const int DEFAULT_GUI_FONT = 17;

		// Token: 0x04003588 RID: 13704
		public const int HOLLOW_BRUSH = 5;

		// Token: 0x04003589 RID: 13705
		public const int BITSPIXEL = 12;

		// Token: 0x0400358A RID: 13706
		public const int ALTERNATE = 1;

		// Token: 0x0400358B RID: 13707
		public const int WINDING = 2;

		// Token: 0x0400358C RID: 13708
		public const int SRCCOPY = 13369376;

		// Token: 0x0400358D RID: 13709
		public const int SRCPAINT = 15597702;

		// Token: 0x0400358E RID: 13710
		public const int SRCAND = 8913094;

		// Token: 0x0400358F RID: 13711
		public const int SRCINVERT = 6684742;

		// Token: 0x04003590 RID: 13712
		public const int SRCERASE = 4457256;

		// Token: 0x04003591 RID: 13713
		public const int NOTSRCCOPY = 3342344;

		// Token: 0x04003592 RID: 13714
		public const int NOTSRCERASE = 1114278;

		// Token: 0x04003593 RID: 13715
		public const int MERGECOPY = 12583114;

		// Token: 0x04003594 RID: 13716
		public const int MERGEPAINT = 12255782;

		// Token: 0x04003595 RID: 13717
		public const int PATCOPY = 15728673;

		// Token: 0x04003596 RID: 13718
		public const int PATPAINT = 16452105;

		// Token: 0x04003597 RID: 13719
		public const int PATINVERT = 5898313;

		// Token: 0x04003598 RID: 13720
		public const int DSTINVERT = 5570569;

		// Token: 0x04003599 RID: 13721
		public const int BLACKNESS = 66;

		// Token: 0x0400359A RID: 13722
		public const int WHITENESS = 16711778;

		// Token: 0x0400359B RID: 13723
		public const int CAPTUREBLT = 1073741824;

		// Token: 0x0400359C RID: 13724
		public const int FW_DONTCARE = 0;

		// Token: 0x0400359D RID: 13725
		public const int FW_NORMAL = 400;

		// Token: 0x0400359E RID: 13726
		public const int FW_BOLD = 700;

		// Token: 0x0400359F RID: 13727
		public const int ANSI_CHARSET = 0;

		// Token: 0x040035A0 RID: 13728
		public const int DEFAULT_CHARSET = 1;

		// Token: 0x040035A1 RID: 13729
		public const int OUT_DEFAULT_PRECIS = 0;

		// Token: 0x040035A2 RID: 13730
		public const int OUT_TT_PRECIS = 4;

		// Token: 0x040035A3 RID: 13731
		public const int OUT_TT_ONLY_PRECIS = 7;

		// Token: 0x040035A4 RID: 13732
		public const int CLIP_DEFAULT_PRECIS = 0;

		// Token: 0x040035A5 RID: 13733
		public const int DEFAULT_QUALITY = 0;

		// Token: 0x040035A6 RID: 13734
		public const int DRAFT_QUALITY = 1;

		// Token: 0x040035A7 RID: 13735
		public const int PROOF_QUALITY = 2;

		// Token: 0x040035A8 RID: 13736
		public const int NONANTIALIASED_QUALITY = 3;

		// Token: 0x040035A9 RID: 13737
		public const int ANTIALIASED_QUALITY = 4;

		// Token: 0x040035AA RID: 13738
		public const int CLEARTYPE_QUALITY = 5;

		// Token: 0x040035AB RID: 13739
		public const int CLEARTYPE_NATURAL_QUALITY = 6;

		// Token: 0x040035AC RID: 13740
		public const int OBJ_PEN = 1;

		// Token: 0x040035AD RID: 13741
		public const int OBJ_BRUSH = 2;

		// Token: 0x040035AE RID: 13742
		public const int OBJ_DC = 3;

		// Token: 0x040035AF RID: 13743
		public const int OBJ_METADC = 4;

		// Token: 0x040035B0 RID: 13744
		public const int OBJ_FONT = 6;

		// Token: 0x040035B1 RID: 13745
		public const int OBJ_BITMAP = 7;

		// Token: 0x040035B2 RID: 13746
		public const int OBJ_MEMDC = 10;

		// Token: 0x040035B3 RID: 13747
		public const int OBJ_EXTPEN = 11;

		// Token: 0x040035B4 RID: 13748
		public const int OBJ_ENHMETADC = 12;

		// Token: 0x040035B5 RID: 13749
		public const int BS_SOLID = 0;

		// Token: 0x040035B6 RID: 13750
		public const int BS_HATCHED = 2;

		// Token: 0x040035B7 RID: 13751
		public const int CP_ACP = 0;

		// Token: 0x040035B8 RID: 13752
		public const int FORMAT_MESSAGE_ALLOCATE_BUFFER = 256;

		// Token: 0x040035B9 RID: 13753
		public const int FORMAT_MESSAGE_IGNORE_INSERTS = 512;

		// Token: 0x040035BA RID: 13754
		public const int FORMAT_MESSAGE_FROM_SYSTEM = 4096;

		// Token: 0x040035BB RID: 13755
		public const int FORMAT_MESSAGE_DEFAULT = 4608;

		// Token: 0x02000875 RID: 2165
		public enum RegionFlags
		{
			// Token: 0x0400442E RID: 17454
			ERROR,
			// Token: 0x0400442F RID: 17455
			NULLREGION,
			// Token: 0x04004430 RID: 17456
			SIMPLEREGION,
			// Token: 0x04004431 RID: 17457
			COMPLEXREGION
		}

		// Token: 0x02000876 RID: 2166
		public struct RECT
		{
			// Token: 0x06007173 RID: 29043 RVA: 0x0019F886 File Offset: 0x0019DA86
			public RECT(int left, int top, int right, int bottom)
			{
				this.left = left;
				this.top = top;
				this.right = right;
				this.bottom = bottom;
			}

			// Token: 0x06007174 RID: 29044 RVA: 0x0019F8A5 File Offset: 0x0019DAA5
			public RECT(Rectangle r)
			{
				this.left = r.Left;
				this.top = r.Top;
				this.right = r.Right;
				this.bottom = r.Bottom;
			}

			// Token: 0x06007175 RID: 29045 RVA: 0x0019F8DB File Offset: 0x0019DADB
			public static IntNativeMethods.RECT FromXYWH(int x, int y, int width, int height)
			{
				return new IntNativeMethods.RECT(x, y, x + width, y + height);
			}

			// Token: 0x170018DB RID: 6363
			// (get) Token: 0x06007176 RID: 29046 RVA: 0x0019F8EA File Offset: 0x0019DAEA
			public Size Size
			{
				get
				{
					return new Size(this.right - this.left, this.bottom - this.top);
				}
			}

			// Token: 0x06007177 RID: 29047 RVA: 0x0019F90B File Offset: 0x0019DB0B
			public Rectangle ToRectangle()
			{
				return new Rectangle(this.left, this.top, this.right - this.left, this.bottom - this.top);
			}

			// Token: 0x04004432 RID: 17458
			public int left;

			// Token: 0x04004433 RID: 17459
			public int top;

			// Token: 0x04004434 RID: 17460
			public int right;

			// Token: 0x04004435 RID: 17461
			public int bottom;
		}

		// Token: 0x02000877 RID: 2167
		[StructLayout(LayoutKind.Sequential)]
		public class POINT
		{
			// Token: 0x06007178 RID: 29048 RVA: 0x00002843 File Offset: 0x00000A43
			public POINT()
			{
			}

			// Token: 0x06007179 RID: 29049 RVA: 0x0019F938 File Offset: 0x0019DB38
			public POINT(int x, int y)
			{
				this.x = x;
				this.y = y;
			}

			// Token: 0x0600717A RID: 29050 RVA: 0x0019F94E File Offset: 0x0019DB4E
			public Point ToPoint()
			{
				return new Point(this.x, this.y);
			}

			// Token: 0x04004436 RID: 17462
			public int x;

			// Token: 0x04004437 RID: 17463
			public int y;
		}

		// Token: 0x02000878 RID: 2168
		[StructLayout(LayoutKind.Sequential)]
		public class DRAWTEXTPARAMS
		{
			// Token: 0x0600717B RID: 29051 RVA: 0x0019F961 File Offset: 0x0019DB61
			public DRAWTEXTPARAMS()
			{
			}

			// Token: 0x0600717C RID: 29052 RVA: 0x0019F980 File Offset: 0x0019DB80
			public DRAWTEXTPARAMS(IntNativeMethods.DRAWTEXTPARAMS original)
			{
				this.iLeftMargin = original.iLeftMargin;
				this.iRightMargin = original.iRightMargin;
				this.iTabLength = original.iTabLength;
			}

			// Token: 0x0600717D RID: 29053 RVA: 0x0019F9CC File Offset: 0x0019DBCC
			public DRAWTEXTPARAMS(int leftMargin, int rightMargin)
			{
				this.iLeftMargin = leftMargin;
				this.iRightMargin = rightMargin;
			}

			// Token: 0x04004438 RID: 17464
			private int cbSize = Marshal.SizeOf(typeof(IntNativeMethods.DRAWTEXTPARAMS));

			// Token: 0x04004439 RID: 17465
			public int iTabLength;

			// Token: 0x0400443A RID: 17466
			public int iLeftMargin;

			// Token: 0x0400443B RID: 17467
			public int iRightMargin;

			// Token: 0x0400443C RID: 17468
			public int uiLengthDrawn;
		}

		// Token: 0x02000879 RID: 2169
		[StructLayout(LayoutKind.Sequential)]
		public class LOGBRUSH
		{
			// Token: 0x0400443D RID: 17469
			public int lbStyle;

			// Token: 0x0400443E RID: 17470
			public int lbColor;

			// Token: 0x0400443F RID: 17471
			public int lbHatch;
		}

		// Token: 0x0200087A RID: 2170
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
		public class LOGFONT
		{
			// Token: 0x0600717F RID: 29055 RVA: 0x00002843 File Offset: 0x00000A43
			public LOGFONT()
			{
			}

			// Token: 0x06007180 RID: 29056 RVA: 0x0019F9F8 File Offset: 0x0019DBF8
			public LOGFONT(IntNativeMethods.LOGFONT lf)
			{
				this.lfHeight = lf.lfHeight;
				this.lfWidth = lf.lfWidth;
				this.lfEscapement = lf.lfEscapement;
				this.lfOrientation = lf.lfOrientation;
				this.lfWeight = lf.lfWeight;
				this.lfItalic = lf.lfItalic;
				this.lfUnderline = lf.lfUnderline;
				this.lfStrikeOut = lf.lfStrikeOut;
				this.lfCharSet = lf.lfCharSet;
				this.lfOutPrecision = lf.lfOutPrecision;
				this.lfClipPrecision = lf.lfClipPrecision;
				this.lfQuality = lf.lfQuality;
				this.lfPitchAndFamily = lf.lfPitchAndFamily;
				this.lfFaceName = lf.lfFaceName;
			}

			// Token: 0x04004440 RID: 17472
			public int lfHeight;

			// Token: 0x04004441 RID: 17473
			public int lfWidth;

			// Token: 0x04004442 RID: 17474
			public int lfEscapement;

			// Token: 0x04004443 RID: 17475
			public int lfOrientation;

			// Token: 0x04004444 RID: 17476
			public int lfWeight;

			// Token: 0x04004445 RID: 17477
			public byte lfItalic;

			// Token: 0x04004446 RID: 17478
			public byte lfUnderline;

			// Token: 0x04004447 RID: 17479
			public byte lfStrikeOut;

			// Token: 0x04004448 RID: 17480
			public byte lfCharSet;

			// Token: 0x04004449 RID: 17481
			public byte lfOutPrecision;

			// Token: 0x0400444A RID: 17482
			public byte lfClipPrecision;

			// Token: 0x0400444B RID: 17483
			public byte lfQuality;

			// Token: 0x0400444C RID: 17484
			public byte lfPitchAndFamily;

			// Token: 0x0400444D RID: 17485
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string lfFaceName;
		}

		// Token: 0x0200087B RID: 2171
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		public struct TEXTMETRIC
		{
			// Token: 0x0400444E RID: 17486
			public int tmHeight;

			// Token: 0x0400444F RID: 17487
			public int tmAscent;

			// Token: 0x04004450 RID: 17488
			public int tmDescent;

			// Token: 0x04004451 RID: 17489
			public int tmInternalLeading;

			// Token: 0x04004452 RID: 17490
			public int tmExternalLeading;

			// Token: 0x04004453 RID: 17491
			public int tmAveCharWidth;

			// Token: 0x04004454 RID: 17492
			public int tmMaxCharWidth;

			// Token: 0x04004455 RID: 17493
			public int tmWeight;

			// Token: 0x04004456 RID: 17494
			public int tmOverhang;

			// Token: 0x04004457 RID: 17495
			public int tmDigitizedAspectX;

			// Token: 0x04004458 RID: 17496
			public int tmDigitizedAspectY;

			// Token: 0x04004459 RID: 17497
			public char tmFirstChar;

			// Token: 0x0400445A RID: 17498
			public char tmLastChar;

			// Token: 0x0400445B RID: 17499
			public char tmDefaultChar;

			// Token: 0x0400445C RID: 17500
			public char tmBreakChar;

			// Token: 0x0400445D RID: 17501
			public byte tmItalic;

			// Token: 0x0400445E RID: 17502
			public byte tmUnderlined;

			// Token: 0x0400445F RID: 17503
			public byte tmStruckOut;

			// Token: 0x04004460 RID: 17504
			public byte tmPitchAndFamily;

			// Token: 0x04004461 RID: 17505
			public byte tmCharSet;
		}

		// Token: 0x0200087C RID: 2172
		public struct TEXTMETRICA
		{
			// Token: 0x04004462 RID: 17506
			public int tmHeight;

			// Token: 0x04004463 RID: 17507
			public int tmAscent;

			// Token: 0x04004464 RID: 17508
			public int tmDescent;

			// Token: 0x04004465 RID: 17509
			public int tmInternalLeading;

			// Token: 0x04004466 RID: 17510
			public int tmExternalLeading;

			// Token: 0x04004467 RID: 17511
			public int tmAveCharWidth;

			// Token: 0x04004468 RID: 17512
			public int tmMaxCharWidth;

			// Token: 0x04004469 RID: 17513
			public int tmWeight;

			// Token: 0x0400446A RID: 17514
			public int tmOverhang;

			// Token: 0x0400446B RID: 17515
			public int tmDigitizedAspectX;

			// Token: 0x0400446C RID: 17516
			public int tmDigitizedAspectY;

			// Token: 0x0400446D RID: 17517
			public byte tmFirstChar;

			// Token: 0x0400446E RID: 17518
			public byte tmLastChar;

			// Token: 0x0400446F RID: 17519
			public byte tmDefaultChar;

			// Token: 0x04004470 RID: 17520
			public byte tmBreakChar;

			// Token: 0x04004471 RID: 17521
			public byte tmItalic;

			// Token: 0x04004472 RID: 17522
			public byte tmUnderlined;

			// Token: 0x04004473 RID: 17523
			public byte tmStruckOut;

			// Token: 0x04004474 RID: 17524
			public byte tmPitchAndFamily;

			// Token: 0x04004475 RID: 17525
			public byte tmCharSet;
		}

		// Token: 0x0200087D RID: 2173
		[StructLayout(LayoutKind.Sequential)]
		public class SIZE
		{
			// Token: 0x06007181 RID: 29057 RVA: 0x00002843 File Offset: 0x00000A43
			public SIZE()
			{
			}

			// Token: 0x06007182 RID: 29058 RVA: 0x0019FAB3 File Offset: 0x0019DCB3
			public SIZE(int cx, int cy)
			{
				this.cx = cx;
				this.cy = cy;
			}

			// Token: 0x06007183 RID: 29059 RVA: 0x0019FAC9 File Offset: 0x0019DCC9
			public Size ToSize()
			{
				return new Size(this.cx, this.cy);
			}

			// Token: 0x04004476 RID: 17526
			public int cx;

			// Token: 0x04004477 RID: 17527
			public int cy;
		}
	}
}
