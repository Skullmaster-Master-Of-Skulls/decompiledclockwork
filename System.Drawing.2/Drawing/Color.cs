using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Design;
using System.Globalization;
using System.Text;

namespace System.Drawing
{
	// Token: 0x02000017 RID: 23
	[TypeConverter(typeof(ColorConverter))]
	[DebuggerDisplay("{NameAndARGBValue}")]
	[Editor("System.Drawing.Design.ColorEditor, System.Drawing.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
	[Serializable]
	public struct Color
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000116 RID: 278 RVA: 0x00006E7A File Offset: 0x0000507A
		public static Color Transparent
		{
			get
			{
				return new Color(KnownColor.Transparent);
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00006E83 File Offset: 0x00005083
		public static Color AliceBlue
		{
			get
			{
				return new Color(KnownColor.AliceBlue);
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000118 RID: 280 RVA: 0x00006E8C File Offset: 0x0000508C
		public static Color AntiqueWhite
		{
			get
			{
				return new Color(KnownColor.AntiqueWhite);
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00006E95 File Offset: 0x00005095
		public static Color Aqua
		{
			get
			{
				return new Color(KnownColor.Aqua);
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00006E9E File Offset: 0x0000509E
		public static Color Aquamarine
		{
			get
			{
				return new Color(KnownColor.Aquamarine);
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00006EA7 File Offset: 0x000050A7
		public static Color Azure
		{
			get
			{
				return new Color(KnownColor.Azure);
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00006EB0 File Offset: 0x000050B0
		public static Color Beige
		{
			get
			{
				return new Color(KnownColor.Beige);
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00006EB9 File Offset: 0x000050B9
		public static Color Bisque
		{
			get
			{
				return new Color(KnownColor.Bisque);
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600011E RID: 286 RVA: 0x00006EC2 File Offset: 0x000050C2
		public static Color Black
		{
			get
			{
				return new Color(KnownColor.Black);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006ECB File Offset: 0x000050CB
		public static Color BlanchedAlmond
		{
			get
			{
				return new Color(KnownColor.BlanchedAlmond);
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000120 RID: 288 RVA: 0x00006ED4 File Offset: 0x000050D4
		public static Color Blue
		{
			get
			{
				return new Color(KnownColor.Blue);
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00006EDD File Offset: 0x000050DD
		public static Color BlueViolet
		{
			get
			{
				return new Color(KnownColor.BlueViolet);
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000122 RID: 290 RVA: 0x00006EE6 File Offset: 0x000050E6
		public static Color Brown
		{
			get
			{
				return new Color(KnownColor.Brown);
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00006EEF File Offset: 0x000050EF
		public static Color BurlyWood
		{
			get
			{
				return new Color(KnownColor.BurlyWood);
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000124 RID: 292 RVA: 0x00006EF8 File Offset: 0x000050F8
		public static Color CadetBlue
		{
			get
			{
				return new Color(KnownColor.CadetBlue);
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00006F01 File Offset: 0x00005101
		public static Color Chartreuse
		{
			get
			{
				return new Color(KnownColor.Chartreuse);
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00006F0A File Offset: 0x0000510A
		public static Color Chocolate
		{
			get
			{
				return new Color(KnownColor.Chocolate);
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00006F13 File Offset: 0x00005113
		public static Color Coral
		{
			get
			{
				return new Color(KnownColor.Coral);
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000128 RID: 296 RVA: 0x00006F1C File Offset: 0x0000511C
		public static Color CornflowerBlue
		{
			get
			{
				return new Color(KnownColor.CornflowerBlue);
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00006F25 File Offset: 0x00005125
		public static Color Cornsilk
		{
			get
			{
				return new Color(KnownColor.Cornsilk);
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00006F2E File Offset: 0x0000512E
		public static Color Crimson
		{
			get
			{
				return new Color(KnownColor.Crimson);
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00006F37 File Offset: 0x00005137
		public static Color Cyan
		{
			get
			{
				return new Color(KnownColor.Cyan);
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00006F40 File Offset: 0x00005140
		public static Color DarkBlue
		{
			get
			{
				return new Color(KnownColor.DarkBlue);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600012D RID: 301 RVA: 0x00006F49 File Offset: 0x00005149
		public static Color DarkCyan
		{
			get
			{
				return new Color(KnownColor.DarkCyan);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00006F52 File Offset: 0x00005152
		public static Color DarkGoldenrod
		{
			get
			{
				return new Color(KnownColor.DarkGoldenrod);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00006F5B File Offset: 0x0000515B
		public static Color DarkGray
		{
			get
			{
				return new Color(KnownColor.DarkGray);
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000130 RID: 304 RVA: 0x00006F64 File Offset: 0x00005164
		public static Color DarkGreen
		{
			get
			{
				return new Color(KnownColor.DarkGreen);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00006F6D File Offset: 0x0000516D
		public static Color DarkKhaki
		{
			get
			{
				return new Color(KnownColor.DarkKhaki);
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00006F76 File Offset: 0x00005176
		public static Color DarkMagenta
		{
			get
			{
				return new Color(KnownColor.DarkMagenta);
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000133 RID: 307 RVA: 0x00006F7F File Offset: 0x0000517F
		public static Color DarkOliveGreen
		{
			get
			{
				return new Color(KnownColor.DarkOliveGreen);
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000134 RID: 308 RVA: 0x00006F88 File Offset: 0x00005188
		public static Color DarkOrange
		{
			get
			{
				return new Color(KnownColor.DarkOrange);
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000135 RID: 309 RVA: 0x00006F91 File Offset: 0x00005191
		public static Color DarkOrchid
		{
			get
			{
				return new Color(KnownColor.DarkOrchid);
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00006F9A File Offset: 0x0000519A
		public static Color DarkRed
		{
			get
			{
				return new Color(KnownColor.DarkRed);
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00006FA3 File Offset: 0x000051A3
		public static Color DarkSalmon
		{
			get
			{
				return new Color(KnownColor.DarkSalmon);
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00006FAC File Offset: 0x000051AC
		public static Color DarkSeaGreen
		{
			get
			{
				return new Color(KnownColor.DarkSeaGreen);
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00006FB5 File Offset: 0x000051B5
		public static Color DarkSlateBlue
		{
			get
			{
				return new Color(KnownColor.DarkSlateBlue);
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00006FBE File Offset: 0x000051BE
		public static Color DarkSlateGray
		{
			get
			{
				return new Color(KnownColor.DarkSlateGray);
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00006FC7 File Offset: 0x000051C7
		public static Color DarkTurquoise
		{
			get
			{
				return new Color(KnownColor.DarkTurquoise);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600013C RID: 316 RVA: 0x00006FD0 File Offset: 0x000051D0
		public static Color DarkViolet
		{
			get
			{
				return new Color(KnownColor.DarkViolet);
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600013D RID: 317 RVA: 0x00006FD9 File Offset: 0x000051D9
		public static Color DeepPink
		{
			get
			{
				return new Color(KnownColor.DeepPink);
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006FE2 File Offset: 0x000051E2
		public static Color DeepSkyBlue
		{
			get
			{
				return new Color(KnownColor.DeepSkyBlue);
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600013F RID: 319 RVA: 0x00006FEB File Offset: 0x000051EB
		public static Color DimGray
		{
			get
			{
				return new Color(KnownColor.DimGray);
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006FF4 File Offset: 0x000051F4
		public static Color DodgerBlue
		{
			get
			{
				return new Color(KnownColor.DodgerBlue);
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000141 RID: 321 RVA: 0x00006FFD File Offset: 0x000051FD
		public static Color Firebrick
		{
			get
			{
				return new Color(KnownColor.Firebrick);
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000142 RID: 322 RVA: 0x00007006 File Offset: 0x00005206
		public static Color FloralWhite
		{
			get
			{
				return new Color(KnownColor.FloralWhite);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000143 RID: 323 RVA: 0x0000700F File Offset: 0x0000520F
		public static Color ForestGreen
		{
			get
			{
				return new Color(KnownColor.ForestGreen);
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000144 RID: 324 RVA: 0x00007018 File Offset: 0x00005218
		public static Color Fuchsia
		{
			get
			{
				return new Color(KnownColor.Fuchsia);
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000145 RID: 325 RVA: 0x00007021 File Offset: 0x00005221
		public static Color Gainsboro
		{
			get
			{
				return new Color(KnownColor.Gainsboro);
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000146 RID: 326 RVA: 0x0000702A File Offset: 0x0000522A
		public static Color GhostWhite
		{
			get
			{
				return new Color(KnownColor.GhostWhite);
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007033 File Offset: 0x00005233
		public static Color Gold
		{
			get
			{
				return new Color(KnownColor.Gold);
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000703C File Offset: 0x0000523C
		public static Color Goldenrod
		{
			get
			{
				return new Color(KnownColor.Goldenrod);
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00007045 File Offset: 0x00005245
		public static Color Gray
		{
			get
			{
				return new Color(KnownColor.Gray);
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x0600014A RID: 330 RVA: 0x0000704E File Offset: 0x0000524E
		public static Color Green
		{
			get
			{
				return new Color(KnownColor.Green);
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00007057 File Offset: 0x00005257
		public static Color GreenYellow
		{
			get
			{
				return new Color(KnownColor.GreenYellow);
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x0600014C RID: 332 RVA: 0x00007060 File Offset: 0x00005260
		public static Color Honeydew
		{
			get
			{
				return new Color(KnownColor.Honeydew);
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00007069 File Offset: 0x00005269
		public static Color HotPink
		{
			get
			{
				return new Color(KnownColor.HotPink);
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600014E RID: 334 RVA: 0x00007072 File Offset: 0x00005272
		public static Color IndianRed
		{
			get
			{
				return new Color(KnownColor.IndianRed);
			}
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600014F RID: 335 RVA: 0x0000707B File Offset: 0x0000527B
		public static Color Indigo
		{
			get
			{
				return new Color(KnownColor.Indigo);
			}
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00007084 File Offset: 0x00005284
		public static Color Ivory
		{
			get
			{
				return new Color(KnownColor.Ivory);
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000708D File Offset: 0x0000528D
		public static Color Khaki
		{
			get
			{
				return new Color(KnownColor.Khaki);
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00007096 File Offset: 0x00005296
		public static Color Lavender
		{
			get
			{
				return new Color(KnownColor.Lavender);
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000709F File Offset: 0x0000529F
		public static Color LavenderBlush
		{
			get
			{
				return new Color(KnownColor.LavenderBlush);
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000154 RID: 340 RVA: 0x000070A8 File Offset: 0x000052A8
		public static Color LawnGreen
		{
			get
			{
				return new Color(KnownColor.LawnGreen);
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000070B1 File Offset: 0x000052B1
		public static Color LemonChiffon
		{
			get
			{
				return new Color(KnownColor.LemonChiffon);
			}
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x06000156 RID: 342 RVA: 0x000070BA File Offset: 0x000052BA
		public static Color LightBlue
		{
			get
			{
				return new Color(KnownColor.LightBlue);
			}
		}

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000157 RID: 343 RVA: 0x000070C3 File Offset: 0x000052C3
		public static Color LightCoral
		{
			get
			{
				return new Color(KnownColor.LightCoral);
			}
		}

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000158 RID: 344 RVA: 0x000070CC File Offset: 0x000052CC
		public static Color LightCyan
		{
			get
			{
				return new Color(KnownColor.LightCyan);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000159 RID: 345 RVA: 0x000070D5 File Offset: 0x000052D5
		public static Color LightGoldenrodYellow
		{
			get
			{
				return new Color(KnownColor.LightGoldenrodYellow);
			}
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600015A RID: 346 RVA: 0x000070DE File Offset: 0x000052DE
		public static Color LightGreen
		{
			get
			{
				return new Color(KnownColor.LightGreen);
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600015B RID: 347 RVA: 0x000070E7 File Offset: 0x000052E7
		public static Color LightGray
		{
			get
			{
				return new Color(KnownColor.LightGray);
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600015C RID: 348 RVA: 0x000070F0 File Offset: 0x000052F0
		public static Color LightPink
		{
			get
			{
				return new Color(KnownColor.LightPink);
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600015D RID: 349 RVA: 0x000070F9 File Offset: 0x000052F9
		public static Color LightSalmon
		{
			get
			{
				return new Color(KnownColor.LightSalmon);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007102 File Offset: 0x00005302
		public static Color LightSeaGreen
		{
			get
			{
				return new Color(KnownColor.LightSeaGreen);
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600015F RID: 351 RVA: 0x0000710B File Offset: 0x0000530B
		public static Color LightSkyBlue
		{
			get
			{
				return new Color(KnownColor.LightSkyBlue);
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00007114 File Offset: 0x00005314
		public static Color LightSlateGray
		{
			get
			{
				return new Color(KnownColor.LightSlateGray);
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000161 RID: 353 RVA: 0x0000711D File Offset: 0x0000531D
		public static Color LightSteelBlue
		{
			get
			{
				return new Color(KnownColor.LightSteelBlue);
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00007126 File Offset: 0x00005326
		public static Color LightYellow
		{
			get
			{
				return new Color(KnownColor.LightYellow);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000712F File Offset: 0x0000532F
		public static Color Lime
		{
			get
			{
				return new Color(KnownColor.Lime);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00007138 File Offset: 0x00005338
		public static Color LimeGreen
		{
			get
			{
				return new Color(KnownColor.LimeGreen);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00007141 File Offset: 0x00005341
		public static Color Linen
		{
			get
			{
				return new Color(KnownColor.Linen);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000166 RID: 358 RVA: 0x0000714A File Offset: 0x0000534A
		public static Color Magenta
		{
			get
			{
				return new Color(KnownColor.Magenta);
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00007153 File Offset: 0x00005353
		public static Color Maroon
		{
			get
			{
				return new Color(KnownColor.Maroon);
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000715C File Offset: 0x0000535C
		public static Color MediumAquamarine
		{
			get
			{
				return new Color(KnownColor.MediumAquamarine);
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000169 RID: 361 RVA: 0x00007165 File Offset: 0x00005365
		public static Color MediumBlue
		{
			get
			{
				return new Color(KnownColor.MediumBlue);
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000716E File Offset: 0x0000536E
		public static Color MediumOrchid
		{
			get
			{
				return new Color(KnownColor.MediumOrchid);
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x0600016B RID: 363 RVA: 0x00007177 File Offset: 0x00005377
		public static Color MediumPurple
		{
			get
			{
				return new Color(KnownColor.MediumPurple);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00007180 File Offset: 0x00005380
		public static Color MediumSeaGreen
		{
			get
			{
				return new Color(KnownColor.MediumSeaGreen);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00007189 File Offset: 0x00005389
		public static Color MediumSlateBlue
		{
			get
			{
				return new Color(KnownColor.MediumSlateBlue);
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00007192 File Offset: 0x00005392
		public static Color MediumSpringGreen
		{
			get
			{
				return new Color(KnownColor.MediumSpringGreen);
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600016F RID: 367 RVA: 0x0000719B File Offset: 0x0000539B
		public static Color MediumTurquoise
		{
			get
			{
				return new Color(KnownColor.MediumTurquoise);
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000170 RID: 368 RVA: 0x000071A4 File Offset: 0x000053A4
		public static Color MediumVioletRed
		{
			get
			{
				return new Color(KnownColor.MediumVioletRed);
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000171 RID: 369 RVA: 0x000071AD File Offset: 0x000053AD
		public static Color MidnightBlue
		{
			get
			{
				return new Color(KnownColor.MidnightBlue);
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000172 RID: 370 RVA: 0x000071B6 File Offset: 0x000053B6
		public static Color MintCream
		{
			get
			{
				return new Color(KnownColor.MintCream);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000173 RID: 371 RVA: 0x000071BF File Offset: 0x000053BF
		public static Color MistyRose
		{
			get
			{
				return new Color(KnownColor.MistyRose);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000174 RID: 372 RVA: 0x000071C8 File Offset: 0x000053C8
		public static Color Moccasin
		{
			get
			{
				return new Color(KnownColor.Moccasin);
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000175 RID: 373 RVA: 0x000071D1 File Offset: 0x000053D1
		public static Color NavajoWhite
		{
			get
			{
				return new Color(KnownColor.NavajoWhite);
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000176 RID: 374 RVA: 0x000071DA File Offset: 0x000053DA
		public static Color Navy
		{
			get
			{
				return new Color(KnownColor.Navy);
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000177 RID: 375 RVA: 0x000071E3 File Offset: 0x000053E3
		public static Color OldLace
		{
			get
			{
				return new Color(KnownColor.OldLace);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000178 RID: 376 RVA: 0x000071EC File Offset: 0x000053EC
		public static Color Olive
		{
			get
			{
				return new Color(KnownColor.Olive);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000179 RID: 377 RVA: 0x000071F5 File Offset: 0x000053F5
		public static Color OliveDrab
		{
			get
			{
				return new Color(KnownColor.OliveDrab);
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000071FE File Offset: 0x000053FE
		public static Color Orange
		{
			get
			{
				return new Color(KnownColor.Orange);
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00007207 File Offset: 0x00005407
		public static Color OrangeRed
		{
			get
			{
				return new Color(KnownColor.OrangeRed);
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00007213 File Offset: 0x00005413
		public static Color Orchid
		{
			get
			{
				return new Color(KnownColor.Orchid);
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000721F File Offset: 0x0000541F
		public static Color PaleGoldenrod
		{
			get
			{
				return new Color(KnownColor.PaleGoldenrod);
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000722B File Offset: 0x0000542B
		public static Color PaleGreen
		{
			get
			{
				return new Color(KnownColor.PaleGreen);
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00007237 File Offset: 0x00005437
		public static Color PaleTurquoise
		{
			get
			{
				return new Color(KnownColor.PaleTurquoise);
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00007243 File Offset: 0x00005443
		public static Color PaleVioletRed
		{
			get
			{
				return new Color(KnownColor.PaleVioletRed);
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000181 RID: 385 RVA: 0x0000724F File Offset: 0x0000544F
		public static Color PapayaWhip
		{
			get
			{
				return new Color(KnownColor.PapayaWhip);
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000725B File Offset: 0x0000545B
		public static Color PeachPuff
		{
			get
			{
				return new Color(KnownColor.PeachPuff);
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00007267 File Offset: 0x00005467
		public static Color Peru
		{
			get
			{
				return new Color(KnownColor.Peru);
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00007273 File Offset: 0x00005473
		public static Color Pink
		{
			get
			{
				return new Color(KnownColor.Pink);
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000185 RID: 389 RVA: 0x0000727F File Offset: 0x0000547F
		public static Color Plum
		{
			get
			{
				return new Color(KnownColor.Plum);
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000186 RID: 390 RVA: 0x0000728B File Offset: 0x0000548B
		public static Color PowderBlue
		{
			get
			{
				return new Color(KnownColor.PowderBlue);
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00007297 File Offset: 0x00005497
		public static Color Purple
		{
			get
			{
				return new Color(KnownColor.Purple);
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x06000188 RID: 392 RVA: 0x000072A3 File Offset: 0x000054A3
		public static Color Red
		{
			get
			{
				return new Color(KnownColor.Red);
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000189 RID: 393 RVA: 0x000072AF File Offset: 0x000054AF
		public static Color RosyBrown
		{
			get
			{
				return new Color(KnownColor.RosyBrown);
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600018A RID: 394 RVA: 0x000072BB File Offset: 0x000054BB
		public static Color RoyalBlue
		{
			get
			{
				return new Color(KnownColor.RoyalBlue);
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600018B RID: 395 RVA: 0x000072C7 File Offset: 0x000054C7
		public static Color SaddleBrown
		{
			get
			{
				return new Color(KnownColor.SaddleBrown);
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600018C RID: 396 RVA: 0x000072D3 File Offset: 0x000054D3
		public static Color Salmon
		{
			get
			{
				return new Color(KnownColor.Salmon);
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600018D RID: 397 RVA: 0x000072DF File Offset: 0x000054DF
		public static Color SandyBrown
		{
			get
			{
				return new Color(KnownColor.SandyBrown);
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000072EB File Offset: 0x000054EB
		public static Color SeaGreen
		{
			get
			{
				return new Color(KnownColor.SeaGreen);
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000072F7 File Offset: 0x000054F7
		public static Color SeaShell
		{
			get
			{
				return new Color(KnownColor.SeaShell);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00007303 File Offset: 0x00005503
		public static Color Sienna
		{
			get
			{
				return new Color(KnownColor.Sienna);
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000191 RID: 401 RVA: 0x0000730F File Offset: 0x0000550F
		public static Color Silver
		{
			get
			{
				return new Color(KnownColor.Silver);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000731B File Offset: 0x0000551B
		public static Color SkyBlue
		{
			get
			{
				return new Color(KnownColor.SkyBlue);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000193 RID: 403 RVA: 0x00007327 File Offset: 0x00005527
		public static Color SlateBlue
		{
			get
			{
				return new Color(KnownColor.SlateBlue);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00007333 File Offset: 0x00005533
		public static Color SlateGray
		{
			get
			{
				return new Color(KnownColor.SlateGray);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000733F File Offset: 0x0000553F
		public static Color Snow
		{
			get
			{
				return new Color(KnownColor.Snow);
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000196 RID: 406 RVA: 0x0000734B File Offset: 0x0000554B
		public static Color SpringGreen
		{
			get
			{
				return new Color(KnownColor.SpringGreen);
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00007357 File Offset: 0x00005557
		public static Color SteelBlue
		{
			get
			{
				return new Color(KnownColor.SteelBlue);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007363 File Offset: 0x00005563
		public static Color Tan
		{
			get
			{
				return new Color(KnownColor.Tan);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000736F File Offset: 0x0000556F
		public static Color Teal
		{
			get
			{
				return new Color(KnownColor.Teal);
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600019A RID: 410 RVA: 0x0000737B File Offset: 0x0000557B
		public static Color Thistle
		{
			get
			{
				return new Color(KnownColor.Thistle);
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00007387 File Offset: 0x00005587
		public static Color Tomato
		{
			get
			{
				return new Color(KnownColor.Tomato);
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00007393 File Offset: 0x00005593
		public static Color Turquoise
		{
			get
			{
				return new Color(KnownColor.Turquoise);
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600019D RID: 413 RVA: 0x0000739F File Offset: 0x0000559F
		public static Color Violet
		{
			get
			{
				return new Color(KnownColor.Violet);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000073AB File Offset: 0x000055AB
		public static Color Wheat
		{
			get
			{
				return new Color(KnownColor.Wheat);
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000073B7 File Offset: 0x000055B7
		public static Color White
		{
			get
			{
				return new Color(KnownColor.White);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000073C3 File Offset: 0x000055C3
		public static Color WhiteSmoke
		{
			get
			{
				return new Color(KnownColor.WhiteSmoke);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000073CF File Offset: 0x000055CF
		public static Color Yellow
		{
			get
			{
				return new Color(KnownColor.Yellow);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000073DB File Offset: 0x000055DB
		public static Color YellowGreen
		{
			get
			{
				return new Color(KnownColor.YellowGreen);
			}
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000073E7 File Offset: 0x000055E7
		internal Color(KnownColor knownColor)
		{
			this.value = 0L;
			this.state = Color.StateKnownColorValid;
			this.name = null;
			this.knownColor = (short)knownColor;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000740B File Offset: 0x0000560B
		private Color(long value, short state, string name, KnownColor knownColor)
		{
			this.value = value;
			this.state = state;
			this.name = name;
			this.knownColor = (short)knownColor;
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x0000742B File Offset: 0x0000562B
		public byte R
		{
			get
			{
				return (byte)(this.Value >> 16 & 255L);
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x0000743E File Offset: 0x0000563E
		public byte G
		{
			get
			{
				return (byte)(this.Value >> 8 & 255L);
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00007450 File Offset: 0x00005650
		public byte B
		{
			get
			{
				return (byte)(this.Value & 255L);
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007460 File Offset: 0x00005660
		public byte A
		{
			get
			{
				return (byte)(this.Value >> 24 & 255L);
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007473 File Offset: 0x00005673
		public bool IsKnownColor
		{
			get
			{
				return (this.state & Color.StateKnownColorValid) != 0;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00007484 File Offset: 0x00005684
		public bool IsEmpty
		{
			get
			{
				return this.state == 0;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000748F File Offset: 0x0000568F
		public bool IsNamedColor
		{
			get
			{
				return (this.state & Color.StateNameValid) != 0 || this.IsKnownColor;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000074A7 File Offset: 0x000056A7
		public bool IsSystemColor
		{
			get
			{
				return this.IsKnownColor && (this.knownColor <= 26 || this.knownColor > 167);
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x060001AD RID: 429 RVA: 0x000074CC File Offset: 0x000056CC
		private string NameAndARGBValue
		{
			get
			{
				return string.Format(CultureInfo.CurrentCulture, "{{Name={0}, ARGB=({1}, {2}, {3}, {4})}}", new object[]
				{
					this.Name,
					this.A,
					this.R,
					this.G,
					this.B
				});
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00007530 File Offset: 0x00005730
		public string Name
		{
			get
			{
				if ((this.state & Color.StateNameValid) != 0)
				{
					return this.name;
				}
				if (!this.IsKnownColor)
				{
					return Convert.ToString(this.value, 16);
				}
				string text = KnownColorTable.KnownColorToName((KnownColor)this.knownColor);
				if (text != null)
				{
					return text;
				}
				return ((KnownColor)this.knownColor).ToString();
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000758D File Offset: 0x0000578D
		private long Value
		{
			get
			{
				if ((this.state & Color.StateValueMask) != 0)
				{
					return this.value;
				}
				if (this.IsKnownColor)
				{
					return (long)KnownColorTable.KnownColorToArgb((KnownColor)this.knownColor);
				}
				return Color.NotDefinedValue;
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000075C0 File Offset: 0x000057C0
		private static void CheckByte(int value, string name)
		{
			if (value < 0 || value > 255)
			{
				throw new ArgumentException(SR.GetString("InvalidEx2BoundArgument", new object[]
				{
					name,
					value,
					0,
					255
				}));
			}
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x00007612 File Offset: 0x00005812
		private static long MakeArgb(byte alpha, byte red, byte green, byte blue)
		{
			return (long)((ulong)((int)red << 16 | (int)green << 8 | (int)blue | (int)alpha << 24) & (ulong)-1);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007627 File Offset: 0x00005827
		public static Color FromArgb(int argb)
		{
			return new Color((long)argb & (long)((ulong)-1), Color.StateARGBValueValid, null, (KnownColor)0);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000763C File Offset: 0x0000583C
		public static Color FromArgb(int alpha, int red, int green, int blue)
		{
			Color.CheckByte(alpha, "alpha");
			Color.CheckByte(red, "red");
			Color.CheckByte(green, "green");
			Color.CheckByte(blue, "blue");
			return new Color(Color.MakeArgb((byte)alpha, (byte)red, (byte)green, (byte)blue), Color.StateARGBValueValid, null, (KnownColor)0);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000768E File Offset: 0x0000588E
		public static Color FromArgb(int alpha, Color baseColor)
		{
			Color.CheckByte(alpha, "alpha");
			return new Color(Color.MakeArgb((byte)alpha, baseColor.R, baseColor.G, baseColor.B), Color.StateARGBValueValid, null, (KnownColor)0);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000076C3 File Offset: 0x000058C3
		public static Color FromArgb(int red, int green, int blue)
		{
			return Color.FromArgb(255, red, green, blue);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000076D2 File Offset: 0x000058D2
		public static Color FromKnownColor(KnownColor color)
		{
			if (!ClientUtils.IsEnumValid(color, (int)color, 1, 174))
			{
				return Color.FromName(color.ToString());
			}
			return new Color(color);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x00007704 File Offset: 0x00005904
		public static Color FromName(string name)
		{
			object namedColor = ColorConverter.GetNamedColor(name);
			if (namedColor != null)
			{
				return (Color)namedColor;
			}
			return new Color(Color.NotDefinedValue, Color.StateNameValid, name, (KnownColor)0);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00007734 File Offset: 0x00005934
		public float GetBrightness()
		{
			float num = (float)this.R / 255f;
			float num2 = (float)this.G / 255f;
			float num3 = (float)this.B / 255f;
			float num4 = num;
			float num5 = num;
			if (num2 > num4)
			{
				num4 = num2;
			}
			if (num3 > num4)
			{
				num4 = num3;
			}
			if (num2 < num5)
			{
				num5 = num2;
			}
			if (num3 < num5)
			{
				num5 = num3;
			}
			return (num4 + num5) / 2f;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00007798 File Offset: 0x00005998
		public float GetHue()
		{
			if (this.R == this.G && this.G == this.B)
			{
				return 0f;
			}
			float num = (float)this.R / 255f;
			float num2 = (float)this.G / 255f;
			float num3 = (float)this.B / 255f;
			float num4 = 0f;
			float num5 = num;
			float num6 = num;
			if (num2 > num5)
			{
				num5 = num2;
			}
			if (num3 > num5)
			{
				num5 = num3;
			}
			if (num2 < num6)
			{
				num6 = num2;
			}
			if (num3 < num6)
			{
				num6 = num3;
			}
			float num7 = num5 - num6;
			if (num == num5)
			{
				num4 = (num2 - num3) / num7;
			}
			else if (num2 == num5)
			{
				num4 = 2f + (num3 - num) / num7;
			}
			else if (num3 == num5)
			{
				num4 = 4f + (num - num2) / num7;
			}
			num4 *= 60f;
			if (num4 < 0f)
			{
				num4 += 360f;
			}
			return num4;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007874 File Offset: 0x00005A74
		public float GetSaturation()
		{
			float num = (float)this.R / 255f;
			float num2 = (float)this.G / 255f;
			float num3 = (float)this.B / 255f;
			float result = 0f;
			float num4 = num;
			float num5 = num;
			if (num2 > num4)
			{
				num4 = num2;
			}
			if (num3 > num4)
			{
				num4 = num3;
			}
			if (num2 < num5)
			{
				num5 = num2;
			}
			if (num3 < num5)
			{
				num5 = num3;
			}
			if (num4 != num5)
			{
				float num6 = (num4 + num5) / 2f;
				if ((double)num6 <= 0.5)
				{
					result = (num4 - num5) / (num4 + num5);
				}
				else
				{
					result = (num4 - num5) / (2f - num4 - num5);
				}
			}
			return result;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007912 File Offset: 0x00005B12
		public int ToArgb()
		{
			return (int)this.Value;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000791B File Offset: 0x00005B1B
		public KnownColor ToKnownColor()
		{
			return (KnownColor)this.knownColor;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007924 File Offset: 0x00005B24
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(32);
			stringBuilder.Append(base.GetType().Name);
			stringBuilder.Append(" [");
			if ((this.state & Color.StateNameValid) != 0)
			{
				stringBuilder.Append(this.Name);
			}
			else if ((this.state & Color.StateKnownColorValid) != 0)
			{
				stringBuilder.Append(this.Name);
			}
			else if ((this.state & Color.StateValueMask) != 0)
			{
				stringBuilder.Append("A=");
				stringBuilder.Append(this.A);
				stringBuilder.Append(", R=");
				stringBuilder.Append(this.R);
				stringBuilder.Append(", G=");
				stringBuilder.Append(this.G);
				stringBuilder.Append(", B=");
				stringBuilder.Append(this.B);
			}
			else
			{
				stringBuilder.Append("Empty");
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007A34 File Offset: 0x00005C34
		public static bool operator ==(Color left, Color right)
		{
			return left.value == right.value && left.state == right.state && left.knownColor == right.knownColor && (left.name == right.name || (left.name != null && right.name != null && left.name.Equals(right.name)));
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007AA5 File Offset: 0x00005CA5
		public static bool operator !=(Color left, Color right)
		{
			return !(left == right);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007AB4 File Offset: 0x00005CB4
		public override bool Equals(object obj)
		{
			if (obj is Color)
			{
				Color color = (Color)obj;
				if (this.value == color.value && this.state == color.state && this.knownColor == color.knownColor)
				{
					return this.name == color.name || (this.name != null && color.name != null && this.name.Equals(this.name));
				}
			}
			return false;
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007B34 File Offset: 0x00005D34
		public override int GetHashCode()
		{
			return this.value.GetHashCode() ^ this.state.GetHashCode() ^ this.knownColor.GetHashCode();
		}

		// Token: 0x04000145 RID: 325
		public static readonly Color Empty = default(Color);

		// Token: 0x04000146 RID: 326
		private static short StateKnownColorValid = 1;

		// Token: 0x04000147 RID: 327
		private static short StateARGBValueValid = 2;

		// Token: 0x04000148 RID: 328
		private static short StateValueMask = Color.StateARGBValueValid;

		// Token: 0x04000149 RID: 329
		private static short StateNameValid = 8;

		// Token: 0x0400014A RID: 330
		private static long NotDefinedValue = 0L;

		// Token: 0x0400014B RID: 331
		private const int ARGBAlphaShift = 24;

		// Token: 0x0400014C RID: 332
		private const int ARGBRedShift = 16;

		// Token: 0x0400014D RID: 333
		private const int ARGBGreenShift = 8;

		// Token: 0x0400014E RID: 334
		private const int ARGBBlueShift = 0;

		// Token: 0x0400014F RID: 335
		private readonly string name;

		// Token: 0x04000150 RID: 336
		private readonly long value;

		// Token: 0x04000151 RID: 337
		private readonly short knownColor;

		// Token: 0x04000152 RID: 338
		private readonly short state;
	}
}
