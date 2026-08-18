using System;
using System.Drawing;
using Spire.DataExport.CollectionEditors;

namespace Spire.DataExport.HTML
{
	// Token: 0x02000182 RID: 386
	public abstract class HtmlExportStyles
	{
		// Token: 0x06000AA4 RID: 2724 RVA: 0x0006F5A8 File Offset: 0x0006E5A8
		// Note: this type is marked as 'beforefieldinit'.
		static HtmlExportStyles()
		{
			int a_ = 8;
			switch (1 == 1)
			{
			}
			if (true)
			{
			}
			if (false)
			{
			}
			HtmlExportStyles.Murky = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(49, 118, 118), Color.FromArgb(255, 231, 96), Color.FromArgb(204, 204, 57), Color.FromArgb(255, 255, 0), Color.FromArgb(255, 255, 255), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(58, 147, 147), Color.FromArgb(255, 255, 255), Color.FromArgb(39, 98, 98), Color.FromArgb(255, 231, 96), Color.FromArgb(54, 120, 0));
			HtmlExportStyles.Silver = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(255, 255, 255), Color.Red, Color.Purple, Color.Blue, Color.Black, HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(0, 0, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255), Color.Black, Color.FromArgb(243, 243, 243));
			HtmlExportStyles.DOS = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(51, 51, 153), Color.FromArgb(105, 239, 125), Color.FromArgb(255, 0, 255), Color.FromArgb(0, 255, 0), Color.FromArgb(255, 255, 255), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(255, 0, 0), Color.FromArgb(255, 255, 255), Color.FromArgb(0, 122, 236), Color.FromArgb(255, 255, 255), Color.FromArgb(0, 107, 206));
			HtmlExportStyles.Yellow = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(51, 153, 102), Color.FromArgb(0, 102, 204), Color.FromArgb(146, 63, 193), Color.Blue, Color.White, HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(204, 0, 51), Color.FromArgb(255, 255, 255), Color.FromArgb(255, 255, 255), Color.Black, Color.FromArgb(250, 241, 0));
			HtmlExportStyles.Gray = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(255, 255, 255), Color.Red, Color.Purple, Color.Blue, Color.Black, HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(128, 128, 128), Color.White, Color.White, Color.Black, Color.FromArgb(238, 238, 238));
			HtmlExportStyles.MSMoney = new HtmlExportStyles.RHTMLTemplate(Color.White, Color.Red, Color.Purple, Color.Blue, Color.Black, HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(206, 198, 181), Color.Black, Color.FromArgb(222, 231, 222), Color.Black, Color.FromArgb(255, 251, 240));
			HtmlExportStyles.Olive = new HtmlExportStyles.RHTMLTemplate(Color.White, Color.Red, Color.Purple, Color.Blue, Color.FromArgb(95, 96, 95), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(207, 200, 144), Color.Black, Color.White, Color.FromArgb(95, 96, 95), Color.FromArgb(13631487));
			HtmlExportStyles.Plain = new HtmlExportStyles.RHTMLTemplate(Color.White, Color.Red, Color.Purple, Color.Blue, Color.Black, HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(6, 143, 230), Color.White, Color.White, Color.Black, Color.FromArgb(255, 252, 217));
			HtmlExportStyles.Normal = new HtmlExportStyles.RHTMLTemplate(Color.White, Color.Red, Color.Purple, Color.Blue, Color.FromArgb(0, 64, 128), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(51, 102, 153), Color.White, Color.White, Color.Black, Color.FromArgb(255, 255, 207));
			HtmlExportStyles.Desert = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(160, 140, 104), Color.Red, Color.Purple, Color.Blue, Color.FromArgb(0, 64, 128), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(51, 102, 153), Color.White, Color.White, Color.Black, Color.FromArgb(216, 204, 184));
			HtmlExportStyles.Brick = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(191, 191, 160), Color.Red, Color.Purple, Color.Blue, Color.FromArgb(0, 64, 128), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(51, 102, 153), Color.White, Color.White, Color.Black, Color.FromArgb(223, 223, 207));
			HtmlExportStyles.Lilac = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(88, 80, 176), Color.Red, Color.Purple, Color.Blue, Color.FromArgb(0, 64, 128), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(51, 102, 153), Color.White, Color.White, Color.Black, Color.FromArgb(176, 168, 216));
			HtmlExportStyles.Maple = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(200, 168, 72), Color.Red, Color.Purple, Color.Blue, Color.FromArgb(0, 64, 128), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(51, 102, 153), Color.White, Color.White, Color.Black, Color.FromArgb(235, 216, 168));
			HtmlExportStyles.Marine = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(72, 144, 136), Color.Red, Color.Purple, Color.Blue, Color.FromArgb(0, 64, 128), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(51, 102, 153), Color.White, Color.White, Color.Black, Color.FromArgb(136, 192, 184));
			HtmlExportStyles.Rose = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(160, 96, 112), Color.Red, Color.Purple, Color.Blue, Color.FromArgb(0, 64, 128), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(51, 102, 153), Color.White, Color.White, Color.Black, Color.FromArgb(208, 176, 184));
			HtmlExportStyles.Green = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(88, 152, 104), Color.Red, Color.Purple, Color.Blue, Color.FromArgb(0, 64, 128), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(51, 102, 153), Color.White, Color.White, Color.Black, Color.FromArgb(160, 200, 168));
			HtmlExportStyles.Wheat = new HtmlExportStyles.RHTMLTemplate(Color.FromArgb(192, 188, 4), Color.Red, Color.Purple, Color.Blue, Color.FromArgb(0, 64, 128), HyperlinksCollectionEditor.b("攣吥䄧䬩䀫", a_), Color.FromArgb(51, 102, 153), Color.White, Color.White, Color.Black, Color.FromArgb(224, 224, 160));
		}

		// Token: 0x04000804 RID: 2052
		public static readonly HtmlExportStyles.RHTMLTemplate Murky;

		// Token: 0x04000805 RID: 2053
		public static readonly HtmlExportStyles.RHTMLTemplate Silver;

		// Token: 0x04000806 RID: 2054
		public static readonly HtmlExportStyles.RHTMLTemplate DOS;

		// Token: 0x04000807 RID: 2055
		public static readonly HtmlExportStyles.RHTMLTemplate Yellow;

		// Token: 0x04000808 RID: 2056
		public static readonly HtmlExportStyles.RHTMLTemplate Gray;

		// Token: 0x04000809 RID: 2057
		public static readonly HtmlExportStyles.RHTMLTemplate MSMoney;

		// Token: 0x0400080A RID: 2058
		public static readonly HtmlExportStyles.RHTMLTemplate Olive;

		// Token: 0x0400080B RID: 2059
		public static readonly HtmlExportStyles.RHTMLTemplate Plain;

		// Token: 0x0400080C RID: 2060
		public static readonly HtmlExportStyles.RHTMLTemplate Normal;

		// Token: 0x0400080D RID: 2061
		public static readonly HtmlExportStyles.RHTMLTemplate Desert;

		// Token: 0x0400080E RID: 2062
		public static readonly HtmlExportStyles.RHTMLTemplate Brick;

		// Token: 0x0400080F RID: 2063
		public static readonly HtmlExportStyles.RHTMLTemplate Lilac;

		// Token: 0x04000810 RID: 2064
		public static readonly HtmlExportStyles.RHTMLTemplate Maple;

		// Token: 0x04000811 RID: 2065
		public static readonly HtmlExportStyles.RHTMLTemplate Marine;

		// Token: 0x04000812 RID: 2066
		public static readonly HtmlExportStyles.RHTMLTemplate Rose;

		// Token: 0x04000813 RID: 2067
		public static readonly HtmlExportStyles.RHTMLTemplate Green;

		// Token: 0x04000814 RID: 2068
		public static readonly HtmlExportStyles.RHTMLTemplate Wheat;

		// Token: 0x02000183 RID: 387
		public struct RHTMLTemplate
		{
			// Token: 0x06000AA5 RID: 2725 RVA: 0x0006FE44 File Offset: 0x0006EE44
			public RHTMLTemplate(Color RBackgroundColor, Color RLinkColor, Color RVLinkColor, Color RALinkColor, Color RDefaultTextColor, string RTextFontName, Color RHeadersRowBgColor, Color RHeadersRowFontColor, Color RTableBgColor, Color RTableFontColor, Color ROddRowBgColor)
			{
				this.RBackgroundColor = RBackgroundColor;
				this.RLinkColor = RLinkColor;
				this.RVLinkColor = RVLinkColor;
				this.RALinkColor = RALinkColor;
				this.RDefaultTextColor = RDefaultTextColor;
				this.RTextFontName = RTextFontName;
				this.RHeadersRowBgColor = RHeadersRowBgColor;
				this.RHeadersRowFontColor = RHeadersRowFontColor;
				this.RTableBgColor = RTableBgColor;
				this.RTableFontColor = RTableFontColor;
				this.ROddRowBgColor = ROddRowBgColor;
			}

			// Token: 0x04000815 RID: 2069
			public Color RBackgroundColor;

			// Token: 0x04000816 RID: 2070
			private int[] \u2609\u00A4\u008F\u00AF;

			// Token: 0x04000817 RID: 2071
			public Color RLinkColor;

			// Token: 0x04000818 RID: 2072
			private string \u25D9\u00ACª\u0096;

			// Token: 0x04000819 RID: 2073
			public Color RVLinkColor;

			// Token: 0x0400081A RID: 2074
			private long[] \u2609\u008F\u00ABª;

			// Token: 0x0400081B RID: 2075
			public Color RALinkColor;

			// Token: 0x0400081C RID: 2076
			public Color RDefaultTextColor;

			// Token: 0x0400081D RID: 2077
			public string RTextFontName;

			// Token: 0x0400081E RID: 2078
			public Color RHeadersRowBgColor;

			// Token: 0x0400081F RID: 2079
			public Color RHeadersRowFontColor;

			// Token: 0x04000820 RID: 2080
			private float[] \u2593\u0090\u00A0\u00A6;

			// Token: 0x04000821 RID: 2081
			public Color RTableBgColor;

			// Token: 0x04000822 RID: 2082
			private int[] \u2593\u008Fª\u0080;

			// Token: 0x04000823 RID: 2083
			public Color RTableFontColor;

			// Token: 0x04000824 RID: 2084
			public Color ROddRowBgColor;
		}
	}
}
