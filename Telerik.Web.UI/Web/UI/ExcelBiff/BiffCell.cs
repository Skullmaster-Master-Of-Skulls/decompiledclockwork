using System;
using System.Collections;

namespace Telerik.Web.UI.ExcelBiff
{
	// Token: 0x02000A5D RID: 2653
	internal abstract class BiffCell
	{
		// Token: 0x060066E6 RID: 26342 RVA: 0x00180FEC File Offset: 0x0017F1EC
		static BiffCell()
		{
			BiffCell.PaletteColorRGB.Add(new RGB(0, 0, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, byte.MaxValue, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, 0, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(0, byte.MaxValue, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 0, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, byte.MaxValue, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, 0, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(0, byte.MaxValue, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(128, 0, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 128, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 0, 128));
			BiffCell.PaletteColorRGB.Add(new RGB(128, 128, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(128, 0, 128));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 128, 128));
			BiffCell.PaletteColorRGB.Add(new RGB(192, 192, 192));
			BiffCell.PaletteColorRGB.Add(new RGB(128, 128, 128));
			BiffCell.PaletteColorRGB.Add(new RGB(153, 153, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(153, 51, 102));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, byte.MaxValue, 204));
			BiffCell.PaletteColorRGB.Add(new RGB(204, byte.MaxValue, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(102, 0, 102));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, 128, 128));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 102, 204));
			BiffCell.PaletteColorRGB.Add(new RGB(204, 204, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 0, 128));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, 0, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, byte.MaxValue, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(0, byte.MaxValue, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(128, 0, 128));
			BiffCell.PaletteColorRGB.Add(new RGB(128, 0, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 128, 128));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 0, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 204, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(204, byte.MaxValue, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(204, byte.MaxValue, 204));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, byte.MaxValue, 153));
			BiffCell.PaletteColorRGB.Add(new RGB(153, 204, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, 153, 204));
			BiffCell.PaletteColorRGB.Add(new RGB(204, 153, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, 204, 153));
			BiffCell.PaletteColorRGB.Add(new RGB(51, 102, byte.MaxValue));
			BiffCell.PaletteColorRGB.Add(new RGB(51, 204, 204));
			BiffCell.PaletteColorRGB.Add(new RGB(153, 204, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, 204, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, 153, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(byte.MaxValue, 102, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(102, 102, 153));
			BiffCell.PaletteColorRGB.Add(new RGB(150, 150, 150));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 51, 102));
			BiffCell.PaletteColorRGB.Add(new RGB(51, 153, 102));
			BiffCell.PaletteColorRGB.Add(new RGB(0, 51, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(51, 51, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(153, 51, 0));
			BiffCell.PaletteColorRGB.Add(new RGB(153, 51, 102));
			BiffCell.PaletteColorRGB.Add(new RGB(51, 51, 153));
			BiffCell.PaletteColorRGB.Add(new RGB(51, 51, 51));
		}

		// Token: 0x060066E7 RID: 26343 RVA: 0x001815CD File Offset: 0x0017F7CD
		public BiffCell()
		{
		}

		// Token: 0x060066E8 RID: 26344 RVA: 0x001815D5 File Offset: 0x0017F7D5
		public BiffCell(int xfIndex)
		{
			this.xfIndex = xfIndex;
		}

		// Token: 0x060066E9 RID: 26345 RVA: 0x001815E4 File Offset: 0x0017F7E4
		public virtual IRecord GetRecord(int row, int col)
		{
			return null;
		}

		// Token: 0x170021DF RID: 8671
		// (get) Token: 0x060066EA RID: 26346 RVA: 0x001815E7 File Offset: 0x0017F7E7
		// (set) Token: 0x060066EB RID: 26347 RVA: 0x001815EF File Offset: 0x0017F7EF
		public int XFIndex
		{
			get
			{
				return this.xfIndex;
			}
			set
			{
				this.xfIndex = value;
			}
		}

		// Token: 0x0400190B RID: 6411
		public const ushort DefaultColor = 64;

		// Token: 0x0400190C RID: 6412
		public const uint DefaultFillPattern = 67108864U;

		// Token: 0x0400190D RID: 6413
		public const string DefaultFont = "Arial";

		// Token: 0x0400190E RID: 6414
		public const ushort DefaultFontColor = 8;

		// Token: 0x0400190F RID: 6415
		public const short DefaultFontSize = 10;

		// Token: 0x04001910 RID: 6416
		public static ArrayList PaletteColorRGB = new ArrayList();

		// Token: 0x04001911 RID: 6417
		private int xfIndex;

		// Token: 0x02000A5E RID: 2654
		public enum FontAttributes
		{
			// Token: 0x04001913 RID: 6419
			Italic = 2,
			// Token: 0x04001914 RID: 6420
			Strikeout = 8,
			// Token: 0x04001915 RID: 6421
			Outline = 16,
			// Token: 0x04001916 RID: 6422
			Shadow = 32
		}

		// Token: 0x02000A5F RID: 2655
		public enum FontBoldness
		{
			// Token: 0x04001918 RID: 6424
			None = 400,
			// Token: 0x04001919 RID: 6425
			Bold = 700,
			// Token: 0x0400191A RID: 6426
			Bold_100 = 100,
			// Token: 0x0400191B RID: 6427
			Bold_200 = 200,
			// Token: 0x0400191C RID: 6428
			Bold_300 = 300,
			// Token: 0x0400191D RID: 6429
			Bold_400 = 400,
			// Token: 0x0400191E RID: 6430
			Bold_500 = 500,
			// Token: 0x0400191F RID: 6431
			Bold_600 = 600,
			// Token: 0x04001920 RID: 6432
			Bold_700 = 700,
			// Token: 0x04001921 RID: 6433
			Bold_800 = 800,
			// Token: 0x04001922 RID: 6434
			Bold_900 = 900,
			// Token: 0x04001923 RID: 6435
			Bold_1000 = 1000
		}

		// Token: 0x02000A60 RID: 2656
		public enum FontScripts
		{
			// Token: 0x04001925 RID: 6437
			None,
			// Token: 0x04001926 RID: 6438
			Superscript,
			// Token: 0x04001927 RID: 6439
			Subscript
		}

		// Token: 0x02000A61 RID: 2657
		public enum FontUnderlines
		{
			// Token: 0x04001929 RID: 6441
			None,
			// Token: 0x0400192A RID: 6442
			Single,
			// Token: 0x0400192B RID: 6443
			Double,
			// Token: 0x0400192C RID: 6444
			SingleAccounting = 33,
			// Token: 0x0400192D RID: 6445
			DoubleAccounting
		}

		// Token: 0x02000A62 RID: 2658
		public enum DiagonalDirection
		{
			// Token: 0x0400192F RID: 6447
			Down,
			// Token: 0x04001930 RID: 6448
			Up,
			// Token: 0x04001931 RID: 6449
			Both
		}

		// Token: 0x02000A63 RID: 2659
		public enum HorizontalAlignments
		{
			// Token: 0x04001933 RID: 6451
			General,
			// Token: 0x04001934 RID: 6452
			Left,
			// Token: 0x04001935 RID: 6453
			Middle,
			// Token: 0x04001936 RID: 6454
			Right,
			// Token: 0x04001937 RID: 6455
			Fill,
			// Token: 0x04001938 RID: 6456
			Justify,
			// Token: 0x04001939 RID: 6457
			CenterAcrossSel
		}

		// Token: 0x02000A64 RID: 2660
		public enum VerticalAlignments
		{
			// Token: 0x0400193B RID: 6459
			Top,
			// Token: 0x0400193C RID: 6460
			Center = 16,
			// Token: 0x0400193D RID: 6461
			Bottom = 32,
			// Token: 0x0400193E RID: 6462
			Justify = 48
		}

		// Token: 0x02000A65 RID: 2661
		public enum ReadingOrder
		{
			// Token: 0x04001940 RID: 6464
			Context,
			// Token: 0x04001941 RID: 6465
			Ltr,
			// Token: 0x04001942 RID: 6466
			Rtl
		}

		// Token: 0x02000A66 RID: 2662
		public enum TextRotate
		{
			// Token: 0x04001944 RID: 6468
			Horizontal,
			// Token: 0x04001945 RID: 6469
			Rotate = 46080,
			// Token: 0x04001946 RID: 6470
			Vertical = 65280
		}

		// Token: 0x02000A67 RID: 2663
		public enum HyperLink
		{
			// Token: 0x04001948 RID: 6472
			URL,
			// Token: 0x04001949 RID: 6473
			LOCALFILE,
			// Token: 0x0400194A RID: 6474
			UNC,
			// Token: 0x0400194B RID: 6475
			BOOKMARK
		}
	}
}
