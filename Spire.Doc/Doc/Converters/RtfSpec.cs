using System;
using System.Text;

namespace Spire.Doc.Converters
{
	// Token: 0x02000080 RID: 128
	public static class RtfSpec
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00009214 File Offset: 0x00008214
		public static int GetCodePage(int charSet)
		{
			for (;;)
			{
				int num = 17;
				for (;;)
				{
					switch (num)
					{
					case 0:
						if (charSet != 222)
						{
							num = 7;
							continue;
						}
						return 874;
					case 1:
						switch ((1 == 1) ? 1 : 0)
						{
						case 0:
						case 2:
							goto IL_199;
						default:
							if (false)
							{
							}
							if (charSet != 204)
							{
								num = 2;
								continue;
							}
							return 1251;
						}
						break;
					case 2:
						num = 10;
						continue;
					case 3:
						switch (charSet)
						{
						case 128:
							return 932;
						case 129:
							return 949;
						case 130:
							return 1361;
						case 131:
						case 132:
						case 133:
						case 135:
							return 0;
						case 134:
							return 936;
						case 136:
							return 950;
						default:
							num = 5;
							continue;
						}
						break;
					case 4:
						num = 12;
						continue;
					case 5:
						num = 15;
						continue;
					case 6:
						num = 26;
						continue;
					case 7:
						num = 13;
						continue;
					case 8:
						goto IL_199;
					case 9:
						num = 14;
						continue;
					case 10:
						goto IL_B1;
					case 11:
						num = 21;
						continue;
					case 12:
						switch (charSet)
						{
						case 254:
							return 437;
						case 255:
							return 850;
						default:
							num = 6;
							continue;
						}
						break;
					case 13:
						if (charSet != 238)
						{
							num = 4;
							continue;
						}
						return 1250;
					case 14:
						if (charSet <= 89)
						{
							num = 27;
							continue;
						}
						num = 3;
						continue;
					case 15:
						switch (charSet)
						{
						case 161:
							return 1253;
						case 162:
							return 1254;
						case 163:
							return 1258;
						default:
							num = 11;
							continue;
						}
						break;
					case 16:
						switch (charSet)
						{
						case 77:
							return 10000;
						case 78:
							return 10001;
						case 79:
							return 10003;
						case 80:
							return 10008;
						case 81:
							return 10002;
						case 82:
							return 0;
						case 83:
							return 10005;
						case 84:
							return 10004;
						case 85:
							return 10006;
						case 86:
							return 10081;
						case 87:
							return 10021;
						case 88:
							return 10029;
						case 89:
							return 10007;
						default:
							num = 18;
							continue;
						}
						break;
					case 17:
						if (charSet <= 163)
						{
							num = 9;
							continue;
						}
						num = 19;
						continue;
					case 18:
						num = 22;
						continue;
					case 19:
						if (charSet <= 204)
						{
							num = 8;
							continue;
						}
						num = 0;
						continue;
					case 20:
						switch (charSet)
						{
						case 0:
							return 1252;
						case 1:
							return 0;
						case 2:
							return 42;
						default:
							num = 23;
							continue;
						}
						break;
					case 21:
						goto IL_C3;
					case 22:
						goto IL_3BC;
					case 23:
						num = 16;
						continue;
					case 24:
						if (true)
						{
						}
						num = 1;
						continue;
					case 25:
						switch (charSet)
						{
						case 177:
							return 1255;
						case 178:
							return 1256;
						case 179:
							return 0;
						case 180:
							return 0;
						case 181:
							return 0;
						case 182:
						case 183:
						case 184:
						case 185:
							return 0;
						case 186:
							return 1257;
						default:
							num = 24;
							continue;
						}
						break;
					case 26:
						goto IL_231;
					case 27:
						num = 20;
						continue;
					}
					break;
					IL_199:
					num = 25;
				}
			}
			return 950;
			IL_B1:
			return 0;
			IL_C3:
			return 0;
			IL_231:
			return 0;
			IL_3BC:
			return 0;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00009614 File Offset: 0x00008614
		// Note: this type is marked as 'beforefieldinit'.
		static RtfSpec()
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
			RtfSpec.AnsiEncoding = Encoding.GetEncoding(1252);
		}

		// Token: 0x04000849 RID: 2121
		public const string TagRtf = "rtf";

		// Token: 0x0400084A RID: 2122
		public const int RtfVersion1 = 1;

		// Token: 0x0400084B RID: 2123
		public const string TagGenerator = "generator";

		// Token: 0x0400084C RID: 2124
		public const string TagViewKind = "viewkind";

		// Token: 0x0400084D RID: 2125
		public const string TagEncodingAnsi = "ansi";

		// Token: 0x0400084E RID: 2126
		public const string TagEncodingMac = "mac";

		// Token: 0x0400084F RID: 2127
		public const string TagEncodingPc = "pc";

		// Token: 0x04000850 RID: 2128
		public const string TagEncodingPca = "pca";

		// Token: 0x04000851 RID: 2129
		public const string TagEncodingAnsiCodePage = "ansicpg";

		// Token: 0x04000852 RID: 2130
		public const int AnsiCodePage = 1252;

		// Token: 0x04000853 RID: 2131
		public const int SymbolFakeCodePage = 42;

		// Token: 0x04000854 RID: 2132
		public const string TagUnicodeSkipCount = "uc";

		// Token: 0x04000855 RID: 2133
		public const string TagUnicodeCode = "u";

		// Token: 0x04000856 RID: 2134
		public const string TagUnicodeAlternativeChoices = "upr";

		// Token: 0x04000857 RID: 2135
		public const string TagUnicodeAlternativeUnicode = "ud";

		// Token: 0x04000858 RID: 2136
		public const string TagFontTable = "fonttbl";

		// Token: 0x04000859 RID: 2137
		public const string TagDefaultFont = "deff";

		// Token: 0x0400085A RID: 2138
		public const string TagFont = "f";

		// Token: 0x0400085B RID: 2139
		public const string TagFontKindNil = "fnil";

		// Token: 0x0400085C RID: 2140
		public const string TagFontKindRoman = "froman";

		// Token: 0x0400085D RID: 2141
		public const string TagFontKindSwiss = "fswiss";

		// Token: 0x0400085E RID: 2142
		public const string TagFontKindModern = "fmodern";

		// Token: 0x0400085F RID: 2143
		public const string TagFontKindScript = "fscript";

		// Token: 0x04000860 RID: 2144
		public const string TagFontKindDecor = "fdecor";

		// Token: 0x04000861 RID: 2145
		public const string TagFontKindTech = "ftech";

		// Token: 0x04000862 RID: 2146
		public const string TagFontKindBidi = "fbidi";

		// Token: 0x04000863 RID: 2147
		public const string TagFontCharset = "fcharset";

		// Token: 0x04000864 RID: 2148
		public const string TagFontPitch = "fprq";

		// Token: 0x04000865 RID: 2149
		public const string TagFontSize = "fs";

		// Token: 0x04000866 RID: 2150
		public const string TagFontDown = "dn";

		// Token: 0x04000867 RID: 2151
		public const string TagFontUp = "up";

		// Token: 0x04000868 RID: 2152
		public const string TagFontSubscript = "sub";

		// Token: 0x04000869 RID: 2153
		public const string TagFontSuperscript = "super";

		// Token: 0x0400086A RID: 2154
		public const string TagFontNoSuperSub = "nosupersub";

		// Token: 0x0400086B RID: 2155
		public const string TagThemeFontLoMajor = "flomajor";

		// Token: 0x0400086C RID: 2156
		public const string TagThemeFontHiMajor = "fhimajor";

		// Token: 0x0400086D RID: 2157
		public const string TagThemeFontDbMajor = "fdbmajor";

		// Token: 0x0400086E RID: 2158
		public const string TagThemeFontBiMajor = "fbimajor";

		// Token: 0x0400086F RID: 2159
		public const string TagThemeFontLoMinor = "flominor";

		// Token: 0x04000870 RID: 2160
		public const string TagThemeFontHiMinor = "fhiminor";

		// Token: 0x04000871 RID: 2161
		public const string TagThemeFontDbMinor = "fdbminor";

		// Token: 0x04000872 RID: 2162
		public const string TagThemeFontBiMinor = "fbiminor";

		// Token: 0x04000873 RID: 2163
		public const int DefaultFontSize = 24;

		// Token: 0x04000874 RID: 2164
		public const string TagCodePage = "cpg";

		// Token: 0x04000875 RID: 2165
		public const string TagColorTable = "colortbl";

		// Token: 0x04000876 RID: 2166
		public const string TagColorRed = "red";

		// Token: 0x04000877 RID: 2167
		public const string TagColorGreen = "green";

		// Token: 0x04000878 RID: 2168
		public const string TagColorBlue = "blue";

		// Token: 0x04000879 RID: 2169
		public const string TagColorForeground = "cf";

		// Token: 0x0400087A RID: 2170
		public const string TagColorBackground = "cb";

		// Token: 0x0400087B RID: 2171
		public const string TagColorBackgroundWord = "chcbpat";

		// Token: 0x0400087C RID: 2172
		public const string TagColorHighlight = "highlight";

		// Token: 0x0400087D RID: 2173
		public const string TagHeader = "header";

		// Token: 0x0400087E RID: 2174
		public const string TagHeaderFirst = "headerf";

		// Token: 0x0400087F RID: 2175
		public const string TagHeaderLeft = "headerl";

		// Token: 0x04000880 RID: 2176
		public const string TagHeaderRight = "headerr";

		// Token: 0x04000881 RID: 2177
		public const string TagFooter = "footer";

		// Token: 0x04000882 RID: 2178
		public const string TagFooterFirst = "footerf";

		// Token: 0x04000883 RID: 2179
		public const string TagFooterLeft = "footerl";

		// Token: 0x04000884 RID: 2180
		public const string TagFooterRight = "footerr";

		// Token: 0x04000885 RID: 2181
		public const string TagFootnote = "footnote";

		// Token: 0x04000886 RID: 2182
		public const string TagDelimiter = ";";

		// Token: 0x04000887 RID: 2183
		public const string TagExtensionDestination = "*";

		// Token: 0x04000888 RID: 2184
		public const string TagTilde = "~";

		// Token: 0x04000889 RID: 2185
		public const string TagHyphen = "-";

		// Token: 0x0400088A RID: 2186
		public const string TagUnderscore = "_";

		// Token: 0x0400088B RID: 2187
		public const string TagPage = "page";

		// Token: 0x0400088C RID: 2188
		public const string TagSection = "sect";

		// Token: 0x0400088D RID: 2189
		public const string TagParagraph = "par";

		// Token: 0x0400088E RID: 2190
		public const string TagLine = "line";

		// Token: 0x0400088F RID: 2191
		public const string TagTabulator = "tab";

		// Token: 0x04000890 RID: 2192
		public const string TagEmDash = "emdash";

		// Token: 0x04000891 RID: 2193
		public const string TagEnDash = "endash";

		// Token: 0x04000892 RID: 2194
		public const string TagEmSpace = "emspace";

		// Token: 0x04000893 RID: 2195
		public const string TagEnSpace = "enspace";

		// Token: 0x04000894 RID: 2196
		public const string TagQmSpace = "qmspace";

		// Token: 0x04000895 RID: 2197
		public const string TagBulltet = "bullet";

		// Token: 0x04000896 RID: 2198
		public const string TagLeftSingleQuote = "lquote";

		// Token: 0x04000897 RID: 2199
		public const string TagRightSingleQuote = "rquote";

		// Token: 0x04000898 RID: 2200
		public const string TagLeftDoubleQuote = "ldblquote";

		// Token: 0x04000899 RID: 2201
		public const string TagRightDoubleQuote = "rdblquote";

		// Token: 0x0400089A RID: 2202
		public const string TagPlain = "plain";

		// Token: 0x0400089B RID: 2203
		public const string TagParagraphDefaults = "pard";

		// Token: 0x0400089C RID: 2204
		public const string TagSectionDefaults = "sectd";

		// Token: 0x0400089D RID: 2205
		public const string TagBold = "b";

		// Token: 0x0400089E RID: 2206
		public const string TagItalic = "i";

		// Token: 0x0400089F RID: 2207
		public const string TagUnderLine = "ul";

		// Token: 0x040008A0 RID: 2208
		public const string TagUnderLineNone = "ulnone";

		// Token: 0x040008A1 RID: 2209
		public const string TagStrikeThrough = "strike";

		// Token: 0x040008A2 RID: 2210
		public const string TagHidden = "v";

		// Token: 0x040008A3 RID: 2211
		public const string TagAlignLeft = "ql";

		// Token: 0x040008A4 RID: 2212
		public const string TagAlignCenter = "qc";

		// Token: 0x040008A5 RID: 2213
		public const string TagAlignRight = "qr";

		// Token: 0x040008A6 RID: 2214
		public const string TagAlignJustify = "qj";

		// Token: 0x040008A7 RID: 2215
		public const string TagStyleSheet = "stylesheet";

		// Token: 0x040008A8 RID: 2216
		public const string TagInfo = "info";

		// Token: 0x040008A9 RID: 2217
		public const string TagInfoVersion = "version";

		// Token: 0x040008AA RID: 2218
		public const string TagInfoRevision = "vern";

		// Token: 0x040008AB RID: 2219
		public const string TagInfoNumberOfPages = "nofpages";

		// Token: 0x040008AC RID: 2220
		public const string TagInfoNumberOfWords = "nofwords";

		// Token: 0x040008AD RID: 2221
		public const string TagInfoNumberOfChars = "nofchars";

		// Token: 0x040008AE RID: 2222
		public const string TagInfoId = "id";

		// Token: 0x040008AF RID: 2223
		public const string TagInfoTitle = "title";

		// Token: 0x040008B0 RID: 2224
		public const string TagInfoSubject = "subject";

		// Token: 0x040008B1 RID: 2225
		public const string TagInfoAuthor = "author";

		// Token: 0x040008B2 RID: 2226
		public const string TagInfoManager = "manager";

		// Token: 0x040008B3 RID: 2227
		public const string TagInfoCompany = "company";

		// Token: 0x040008B4 RID: 2228
		public const string TagInfoOperator = "operator";

		// Token: 0x040008B5 RID: 2229
		public const string TagInfoCategory = "category";

		// Token: 0x040008B6 RID: 2230
		private long[] \u2593\u0080\u00A4\u0093;

		// Token: 0x040008B7 RID: 2231
		public const string TagInfoKeywords = "keywords";

		// Token: 0x040008B8 RID: 2232
		public const string TagInfoComment = "comment";

		// Token: 0x040008B9 RID: 2233
		public const string TagInfoDocumentComment = "doccomm";

		// Token: 0x040008BA RID: 2234
		private long[] \u2593\u00A3\u008A\u0097;

		// Token: 0x040008BB RID: 2235
		public const string TagInfoHyperLinkBase = "hlinkbase";

		// Token: 0x040008BC RID: 2236
		public const string TagInfoCreationTime = "creatim";

		// Token: 0x040008BD RID: 2237
		public const string TagInfoRevisionTime = "revtim";

		// Token: 0x040008BE RID: 2238
		public const string TagInfoPrintTime = "printim";

		// Token: 0x040008BF RID: 2239
		public const string TagInfoBackupTime = "buptim";

		// Token: 0x040008C0 RID: 2240
		public const string TagInfoYear = "yr";

		// Token: 0x040008C1 RID: 2241
		public const string TagInfoMonth = "mo";

		// Token: 0x040008C2 RID: 2242
		public const string TagInfoDay = "dy";

		// Token: 0x040008C3 RID: 2243
		public const string TagInfoHour = "hr";

		// Token: 0x040008C4 RID: 2244
		public const string TagInfoMinute = "min";

		// Token: 0x040008C5 RID: 2245
		public const string TagInfoSecond = "sec";

		// Token: 0x040008C6 RID: 2246
		public const string TagInfoEditingTimeMinutes = "edmins";

		// Token: 0x040008C7 RID: 2247
		public const string TagUserProperties = "userprops";

		// Token: 0x040008C8 RID: 2248
		public const string TagUserPropertyType = "proptype";

		// Token: 0x040008C9 RID: 2249
		public const string TagUserPropertyName = "propname";

		// Token: 0x040008CA RID: 2250
		public const string TagUserPropertyValue = "staticval";

		// Token: 0x040008CB RID: 2251
		public const string TagUserPropertyLink = "linkval";

		// Token: 0x040008CC RID: 2252
		public const int PropertyTypeInteger = 3;

		// Token: 0x040008CD RID: 2253
		private float \u2609\u0093\u0099\u007F;

		// Token: 0x040008CE RID: 2254
		public const int PropertyTypeRealNumber = 5;

		// Token: 0x040008CF RID: 2255
		public const int PropertyTypeDate = 64;

		// Token: 0x040008D0 RID: 2256
		public const int PropertyTypeBoolean = 11;

		// Token: 0x040008D1 RID: 2257
		public const int PropertyTypeText = 30;

		// Token: 0x040008D2 RID: 2258
		public const string TagPicture = "pict";

		// Token: 0x040008D3 RID: 2259
		public const string TagPictureWrapper = "shppict";

		// Token: 0x040008D4 RID: 2260
		public const string TagPictureWrapperAlternative = "nonshppict";

		// Token: 0x040008D5 RID: 2261
		public const string TagPictureFormatEmf = "emfblip";

		// Token: 0x040008D6 RID: 2262
		public const string TagPictureFormatPng = "pngblip";

		// Token: 0x040008D7 RID: 2263
		public const string TagPictureFormatJpg = "jpegblip";

		// Token: 0x040008D8 RID: 2264
		public const string TagPictureFormatPict = "macpict";

		// Token: 0x040008D9 RID: 2265
		public const string TagPictureFormatOs2Metafile = "pmmetafile";

		// Token: 0x040008DA RID: 2266
		public const string TagPictureFormatWmf = "wmetafile";

		// Token: 0x040008DB RID: 2267
		public const string TagPictureFormatWinDib = "dibitmap";

		// Token: 0x040008DC RID: 2268
		public const string TagPictureFormatWinBmp = "wbitmap";

		// Token: 0x040008DD RID: 2269
		private byte \u25D8\u0087\u00A4ª;

		// Token: 0x040008DE RID: 2270
		public const string TagPictureWidth = "picw";

		// Token: 0x040008DF RID: 2271
		public const string TagPictureHeight = "pich";

		// Token: 0x040008E0 RID: 2272
		public const string TagPictureWidthGoal = "picwgoal";

		// Token: 0x040008E1 RID: 2273
		public const string TagPictureHeightGoal = "pichgoal";

		// Token: 0x040008E2 RID: 2274
		public const string TagPictureWidthScale = "picscalex";

		// Token: 0x040008E3 RID: 2275
		private string[] \u2593\u00A8\u009A\u00A0;

		// Token: 0x040008E4 RID: 2276
		public const string TagPictureHeightScale = "picscaley";

		// Token: 0x040008E5 RID: 2277
		public const string TagParagraphNumberText = "pntext";

		// Token: 0x040008E6 RID: 2278
		public const string TagListNumberText = "listtext";

		// Token: 0x040008E7 RID: 2279
		public static readonly Encoding AnsiEncoding;
	}
}
