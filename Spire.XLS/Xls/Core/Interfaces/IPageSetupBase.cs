using System;
using System.Drawing;

namespace Spire.Xls.Core.Interfaces
{
	// Token: 0x0200000C RID: 12
	public interface IPageSetupBase : IExcelApplication
	{
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600008B RID: 139
		// (set) Token: 0x0600008C RID: 140
		bool AutoFirstPageNumber { get; set; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600008D RID: 141
		// (set) Token: 0x0600008E RID: 142
		bool BlackAndWhite { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600008F RID: 143
		// (set) Token: 0x06000090 RID: 144
		double BottomMargin { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000091 RID: 145
		// (set) Token: 0x06000092 RID: 146
		string CenterFooter { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000093 RID: 147
		// (set) Token: 0x06000094 RID: 148
		Image CenterFooterImage { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000095 RID: 149
		// (set) Token: 0x06000096 RID: 150
		string CenterHeader { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000097 RID: 151
		// (set) Token: 0x06000098 RID: 152
		Image CenterHeaderImage { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000099 RID: 153
		// (set) Token: 0x0600009A RID: 154
		bool CenterHorizontally { get; set; }

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x0600009B RID: 155
		// (set) Token: 0x0600009C RID: 156
		bool CenterVertically { get; set; }

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600009D RID: 157
		// (set) Token: 0x0600009E RID: 158
		int Copies { get; set; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600009F RID: 159
		// (set) Token: 0x060000A0 RID: 160
		bool Draft { get; set; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000A1 RID: 161
		// (set) Token: 0x060000A2 RID: 162
		int FirstPageNumber { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000A3 RID: 163
		// (set) Token: 0x060000A4 RID: 164
		double FooterMarginInch { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000A5 RID: 165
		// (set) Token: 0x060000A6 RID: 166
		double HeaderMarginInch { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000A7 RID: 167
		// (set) Token: 0x060000A8 RID: 168
		string LeftFooter { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000A9 RID: 169
		// (set) Token: 0x060000AA RID: 170
		Image LeftFooterImage { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000AB RID: 171
		// (set) Token: 0x060000AC RID: 172
		string LeftHeader { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000AD RID: 173
		// (set) Token: 0x060000AE RID: 174
		Image LeftHeaderImage { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000AF RID: 175
		// (set) Token: 0x060000B0 RID: 176
		double LeftMargin { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000B1 RID: 177
		// (set) Token: 0x060000B2 RID: 178
		OrderType Order { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000B3 RID: 179
		// (set) Token: 0x060000B4 RID: 180
		PageOrientationType Orientation { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000B5 RID: 181
		// (set) Token: 0x060000B6 RID: 182
		PaperSizeType PaperSize { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000B7 RID: 183
		// (set) Token: 0x060000B8 RID: 184
		PrintCommentType PrintComments { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000B9 RID: 185
		// (set) Token: 0x060000BA RID: 186
		PrintErrorsType PrintErrors { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000BB RID: 187
		// (set) Token: 0x060000BC RID: 188
		bool PrintNotes { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000BD RID: 189
		// (set) Token: 0x060000BE RID: 190
		int PrintQuality { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060000BF RID: 191
		// (set) Token: 0x060000C0 RID: 192
		string RightFooter { get; set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060000C1 RID: 193
		// (set) Token: 0x060000C2 RID: 194
		Image RightFooterImage { get; set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C3 RID: 195
		// (set) Token: 0x060000C4 RID: 196
		string RightHeader { get; set; }

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C5 RID: 197
		// (set) Token: 0x060000C6 RID: 198
		Image RightHeaderImage { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060000C7 RID: 199
		// (set) Token: 0x060000C8 RID: 200
		double RightMargin { get; set; }

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060000C9 RID: 201
		// (set) Token: 0x060000CA RID: 202
		double TopMargin { get; set; }

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060000CB RID: 203
		// (set) Token: 0x060000CC RID: 204
		int Zoom { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060000CD RID: 205
		// (set) Token: 0x060000CE RID: 206
		Bitmap BackgoundImage { get; set; }
	}
}
