using System;
using System.Globalization;
using Microsoft.Win32;
using Spire.DataExport.CollectionEditors;

// Token: 0x0200005F RID: 95
internal abstract class spr\u1C2B
{
	// Token: 0x0600031F RID: 799 RVA: 0x0001E464 File Offset: 0x0001D464
	internal static string ᜀ()
	{
		int a_ = 2;
		for (;;)
		{
			int currencyPositivePattern;
			switch ((1 == 1) ? 1 : 0)
			{
			case 0:
			case 2:
				break;
			default:
				if (true)
				{
				}
				if (false)
				{
				}
				currencyPositivePattern = NumberFormatInfo.CurrentInfo.CurrencyPositivePattern;
				break;
			}
			int num = 1;
			for (;;)
			{
				switch (num)
				{
				case 0:
					num = 2;
					continue;
				case 1:
					switch (currencyPositivePattern)
					{
					case 0:
						goto IL_E9;
					case 1:
						goto IL_99;
					case 2:
						goto IL_74;
					case 3:
						goto IL_C4;
					default:
						num = 0;
						continue;
					}
					break;
				case 2:
					goto IL_C2;
				}
				break;
			}
		}
		IL_74:
		return NumberFormatInfo.CurrentInfo.CurrencySymbol + ' ' + HyperlinksCollectionEditor.b("㴝టġܣԥЧऩ༫ḭḯȱг", a_);
		IL_99:
		return HyperlinksCollectionEditor.b("㴝టġܣԥЧऩ༫ḭḯȱг", a_) + NumberFormatInfo.CurrentInfo.CurrencySymbol;
		IL_C2:
		return string.Empty;
		IL_C4:
		return HyperlinksCollectionEditor.b("㴝టġܣԥЧऩ༫ḭḯȱг", a_) + ' ' + NumberFormatInfo.CurrentInfo.CurrencySymbol;
		IL_E9:
		return NumberFormatInfo.CurrentInfo.CurrencySymbol + HyperlinksCollectionEditor.b("㴝టġܣԥЧऩ༫ḭḯȱг", a_);
	}

	// Token: 0x06000321 RID: 801 RVA: 0x0001E59C File Offset: 0x0001D59C
	// Note: this type is marked as 'beforefieldinit'.
	static spr\u1C2B()
	{
		int a_ = 15;
		switch (1 == 1)
		{
		}
		if (true)
		{
		}
		if (false)
		{
		}
		spr\u1C2B.ᡘ = Registry.CurrentUser;
		spr\u1C2B.ᡙ = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;
		spr\u1C2B.ᡚ = DateTimeFormatInfo.CurrentInfo.ShortTimePattern;
		spr\u1C2B.ᡛ = string.Format(HyperlinksCollectionEditor.b("倪ᴬ刮ᄰ䠲д䨶", a_), spr\u1C2B.ᡙ, spr\u1C2B.ᡚ);
		spr\u1C2B.ᡜ = CultureInfo.CurrentCulture.TextInfo.ListSeparator;
		spr\u1C2B.ᡝ = CultureInfo.CurrentCulture.Name;
		spr\u1C2B.ᡞ = new DateTime(1899, 12, 30);
		spr\u1C2B.ᡟ = 86400000L;
	}

	// Token: 0x040000F3 RID: 243
	internal const string ᜀ = "Spire.DataExport";

	// Token: 0x040000F4 RID: 244
	internal const string ᜁ = "2.01";

	// Token: 0x040000F5 RID: 245
	internal const string ᜂ = "(About Spire.DataExport)";

	// Token: 0x040000F6 RID: 246
	internal const string ᜃ = "http://www.e-iceblue.com";

	// Token: 0x040000F7 RID: 247
	internal const string ᜄ = "http://www.e-iceblue.com/dataexport.htm";

	// Token: 0x040000F8 RID: 248
	internal const string ᜅ = "support@e-iceblue.com";

	// Token: 0x040000F9 RID: 249
	internal const string ᜆ = "mailto:support@e-iceblue.com";

	// Token: 0x040000FA RID: 250
	internal const string ᜇ = "http://www.e-iceblue.com/dataexport/exporturchase.htm";

	// Token: 0x040000FB RID: 251
	internal const string ᜈ = "Software\\e-iceblue\\Spire.DataExport\\2.01\\";

	// Token: 0x040000FC RID: 252
	internal const string ᜉ = "License";

	// Token: 0x040000FD RID: 253
	internal const string ᜊ = "RegName";

	// Token: 0x040000FE RID: 254
	internal const string ᜋ = "RegKey";

	// Token: 0x040000FF RID: 255
	internal const string ᜌ = "#,###,##0";

	// Token: 0x04000100 RID: 256
	internal const string \u170D = "#,###,##0.00";

	// Token: 0x04000101 RID: 257
	internal const string ᜎ = "g";

	// Token: 0x04000102 RID: 258
	internal const string ᜏ = "g";

	// Token: 0x04000103 RID: 259
	internal const string ᜐ = "c";

	// Token: 0x04000104 RID: 260
	internal const string ᜑ = "true";

	// Token: 0x04000105 RID: 261
	internal const string \u1712 = "false";

	// Token: 0x04000106 RID: 262
	internal const string \u1713 = "null";

	// Token: 0x04000107 RID: 263
	internal const string \u1714 = "NULL";

	// Token: 0x04000108 RID: 264
	internal const int \u1715 = 8;

	// Token: 0x04000109 RID: 265
	internal const string \u1716 = "GENERAL";

	// Token: 0x0400010A RID: 266
	internal const string \u1717 = "FileName";

	// Token: 0x0400010B RID: 267
	internal const string \u1718 = "ExportType";

	// Token: 0x0400010C RID: 268
	internal const string \u1719 = "OpenView";

	// Token: 0x0400010D RID: 269
	internal const string \u171A = "PrintFile";

	// Token: 0x0400010E RID: 270
	internal const string \u171B = "ExportNoLimited";

	// Token: 0x0400010F RID: 271
	internal const string \u171C = "RecsCount";

	// Token: 0x04000110 RID: 272
	internal const string \u171D = "SkipRecs";

	// Token: 0x04000111 RID: 273
	internal const string \u171E = "AllowTitles";

	// Token: 0x04000112 RID: 274
	internal const string \u171F = "DetectColumnType";

	// Token: 0x04000113 RID: 275
	internal const string ᜠ = "FORMATS";

	// Token: 0x04000114 RID: 276
	internal const string ᜡ = "Integer";

	// Token: 0x04000115 RID: 277
	internal const string ᜢ = "Float";

	// Token: 0x04000116 RID: 278
	internal const string ᜣ = "Date";

	// Token: 0x04000117 RID: 279
	internal const string ᜤ = "Time";

	// Token: 0x04000118 RID: 280
	internal const string ᜥ = "DateTime";

	// Token: 0x04000119 RID: 281
	internal const string ᜦ = "Currency";

	// Token: 0x0400011A RID: 282
	internal const string ᜧ = "BooleanTrue";

	// Token: 0x0400011B RID: 283
	internal const string ᜨ = "BooleanFalse";

	// Token: 0x0400011C RID: 284
	internal const string ᜩ = "NullString";

	// Token: 0x0400011D RID: 285
	internal const string ᜪ = "CUSTOM_FORMATS";

	// Token: 0x0400011E RID: 286
	internal const string ᜫ = "COLUMNS";

	// Token: 0x0400011F RID: 287
	internal const string ᜬ = "Column";

	// Token: 0x04000120 RID: 288
	internal const string ᜭ = "HEADER";

	// Token: 0x04000121 RID: 289
	internal const string ᜮ = "FOOTER";

	// Token: 0x04000122 RID: 290
	internal const string ᜯ = "line";

	// Token: 0x04000123 RID: 291
	internal const string ᜰ = "TITLES";

	// Token: 0x04000124 RID: 292
	internal const string ᜱ = "WIDTH";

	// Token: 0x04000125 RID: 293
	internal const string \u1732 = "ALIGN";

	// Token: 0x04000126 RID: 294
	internal const string \u1733 = "LENGTH";

	// Token: 0x04000127 RID: 295
	internal const string \u1734 = "XLS";

	// Token: 0x04000128 RID: 296
	internal const string \u1735 = "PageHeader";

	// Token: 0x04000129 RID: 297
	internal const string \u1736 = "PageFooter";

	// Token: 0x0400012A RID: 298
	internal const string \u1737 = "WorkSheetTitle";

	// Token: 0x0400012B RID: 299
	internal const string \u1738 = "ItemType";

	// Token: 0x0400012C RID: 300
	internal const string \u1739 = "AutoFitColWidt";

	// Token: 0x0400012D RID: 301
	internal const string \u173A = "PageBackground";

	// Token: 0x0400012E RID: 302
	internal const string \u173B = "CELL_FIELD_";

	// Token: 0x0400012F RID: 303
	internal const string \u173C = "CELL_OPTION_";

	// Token: 0x04000130 RID: 304
	internal const string \u173D = "CELL_OPTION_HEADER";

	// Token: 0x04000131 RID: 305
	internal const string \u173E = "CELL_OPTION_CAPTION";

	// Token: 0x04000132 RID: 306
	internal const string \u173F = "CELL_OPTION_AGGREGATE";

	// Token: 0x04000133 RID: 307
	internal const string ᝀ = "CELL_OPTION_FOOTER";

	// Token: 0x04000134 RID: 308
	internal const string ᝁ = "CELL_STYLE_";

	// Token: 0x04000135 RID: 309
	internal const string ᝂ = "CELL_HYPERLINK_";

	// Token: 0x04000136 RID: 310
	internal const string ᝃ = "CELL_NOTE_";

	// Token: 0x04000137 RID: 311
	internal const string ᝄ = "_SERIES_";

	// Token: 0x04000138 RID: 312
	internal const string ᝅ = "CELL_CHART_";

	// Token: 0x04000139 RID: 313
	internal const string ᝆ = "CELL_CELL_";

	// Token: 0x0400013A RID: 314
	internal const string ᝇ = "CELL_MERGED_CELL_";

	// Token: 0x0400013B RID: 315
	internal const string ᝈ = "Font_Name";

	// Token: 0x0400013C RID: 316
	internal const string ᝉ = "Font_Size";

	// Token: 0x0400013D RID: 317
	internal const string ᝊ = "Font_Color";

	// Token: 0x0400013E RID: 318
	internal const string ᝋ = "Font_Bold";

	// Token: 0x0400013F RID: 319
	internal const string ᝌ = "Font_Italic";

	// Token: 0x04000140 RID: 320
	internal const string ᝍ = "Font_StrikeOut";

	// Token: 0x04000141 RID: 321
	internal const string ᝎ = "Font_Underline";

	// Token: 0x04000142 RID: 322
	internal const string ᝏ = "HorAlignment";

	// Token: 0x04000143 RID: 323
	internal const string ᝐ = "VertAlignment";

	// Token: 0x04000144 RID: 324
	internal const string ᝑ = "Border_Top";

	// Token: 0x04000145 RID: 325
	internal const string \u1752 = "Border_TopColor";

	// Token: 0x04000146 RID: 326
	internal const string \u1753 = "Border_Bottom";

	// Token: 0x04000147 RID: 327
	internal const string \u1754 = "Border_BottomColor";

	// Token: 0x04000148 RID: 328
	internal const string \u1755 = "Border_Left";

	// Token: 0x04000149 RID: 329
	internal const string \u1756 = "Border_LeftColor";

	// Token: 0x0400014A RID: 330
	internal const string \u1757 = "Border_Right";

	// Token: 0x0400014B RID: 331
	internal const string \u1758 = "Border_RightColor";

	// Token: 0x0400014C RID: 332
	internal const string \u1759 = "FillPattern";

	// Token: 0x0400014D RID: 333
	internal const string \u175A = "FillBackground";

	// Token: 0x0400014E RID: 334
	internal const string \u175B = "FillForeground";

	// Token: 0x0400014F RID: 335
	internal const string \u175C = "Aggregate";

	// Token: 0x04000150 RID: 336
	internal const string \u175D = "Column";

	// Token: 0x04000151 RID: 337
	internal const string \u175E = "Row";

	// Token: 0x04000152 RID: 338
	internal const string \u175F = "Style";

	// Token: 0x04000153 RID: 339
	internal const string ᝠ = "Title";

	// Token: 0x04000154 RID: 340
	internal const string ᝡ = "Target";

	// Token: 0x04000155 RID: 341
	internal const string ᝢ = "ToolsTip";

	// Token: 0x04000156 RID: 342
	internal const string ᝣ = "Column";

	// Token: 0x04000157 RID: 343
	internal const string ᝤ = "Row";

	// Token: 0x04000158 RID: 344
	internal const string ᝥ = "LINES";

	// Token: 0x04000159 RID: 345
	internal const string ᝦ = "Font_Name";

	// Token: 0x0400015A RID: 346
	internal const string ᝧ = "Font_Size";

	// Token: 0x0400015B RID: 347
	internal const string ᝨ = "Font_Color";

	// Token: 0x0400015C RID: 348
	internal const string ᝩ = "Font_Bold";

	// Token: 0x0400015D RID: 349
	internal const string ᝪ = "Font_Italic";

	// Token: 0x0400015E RID: 350
	internal const string ᝫ = "Font_StrikeOut";

	// Token: 0x0400015F RID: 351
	internal const string ᝬ = "Font_Underline";

	// Token: 0x04000160 RID: 352
	internal const string \u176D = "HorAlignment";

	// Token: 0x04000161 RID: 353
	internal const string ᝮ = "VertAlignment";

	// Token: 0x04000162 RID: 354
	internal const string ᝯ = "BackgroundColor";

	// Token: 0x04000163 RID: 355
	internal const string ᝰ = "ForegroundColor";

	// Token: 0x04000164 RID: 356
	internal const string \u1771 = "FillType";

	// Token: 0x04000165 RID: 357
	internal const string \u1772 = "Transparency";

	// Token: 0x04000166 RID: 358
	internal const string \u1773 = "Orientation";

	// Token: 0x04000167 RID: 359
	internal const string \u1774 = "Gradient";

	// Token: 0x04000168 RID: 360
	internal const string \u1775 = "DataRangeSheet";

	// Token: 0x04000169 RID: 361
	internal const string \u1776 = "StartCol";

	// Token: 0x0400016A RID: 362
	internal const string \u1777 = "StartRow";

	// Token: 0x0400016B RID: 363
	internal const string \u1778 = "EndCol";

	// Token: 0x0400016C RID: 364
	internal const string \u1779 = "EndRow";

	// Token: 0x0400016D RID: 365
	internal const string \u177A = "Color";

	// Token: 0x0400016E RID: 366
	internal const string \u177B = "Title";

	// Token: 0x0400016F RID: 367
	internal const string \u177C = "Column";

	// Token: 0x04000170 RID: 368
	internal const string \u177D = "DataRangeType";

	// Token: 0x04000171 RID: 369
	internal const string \u177E = "Chart_Placement";

	// Token: 0x04000172 RID: 370
	internal const string \u177F = "Chart_Height";

	// Token: 0x04000173 RID: 371
	internal const string ក = "Chart_Left";

	// Token: 0x04000174 RID: 372
	internal const string ខ = "Chart_Top";

	// Token: 0x04000175 RID: 373
	internal const string គ = "Chart_Width";

	// Token: 0x04000176 RID: 374
	internal const string ឃ = "X1";

	// Token: 0x04000177 RID: 375
	internal const string ង = "Y1";

	// Token: 0x04000178 RID: 376
	internal const string ច = "X2";

	// Token: 0x04000179 RID: 377
	internal const string ឆ = "Y2";

	// Token: 0x0400017A RID: 378
	internal const string ជ = "PositionType";

	// Token: 0x0400017B RID: 379
	internal const string ឈ = "AutoColor";

	// Token: 0x0400017C RID: 380
	internal const string ញ = "LegendPlacement";

	// Token: 0x0400017D RID: 381
	internal const string ដ = "ShowLegend";

	// Token: 0x0400017E RID: 382
	internal const string ឋ = "Style";

	// Token: 0x0400017F RID: 383
	internal const string ឌ = "Title";

	// Token: 0x04000180 RID: 384
	internal const string ឍ = "CategoryLabelsType";

	// Token: 0x04000181 RID: 385
	internal const string ណ = "CategoryLabelsColumn";

	// Token: 0x04000182 RID: 386
	internal const string ត = "CellType";

	// Token: 0x04000183 RID: 387
	internal const string ថ = "Column";

	// Token: 0x04000184 RID: 388
	internal const string ទ = "Row";

	// Token: 0x04000185 RID: 389
	internal const string ធ = "DateTimeFormat";

	// Token: 0x04000186 RID: 390
	internal const string ន = "NumericFormat";

	// Token: 0x04000187 RID: 391
	internal const string ប = "BooleanValue";

	// Token: 0x04000188 RID: 392
	internal const string ផ = "Year";

	// Token: 0x04000189 RID: 393
	internal const string ព = "Month";

	// Token: 0x0400018A RID: 394
	internal const string ភ = "Day";

	// Token: 0x0400018B RID: 395
	internal const string ម = "Hour";

	// Token: 0x0400018C RID: 396
	internal const string យ = "Min";

	// Token: 0x0400018D RID: 397
	internal const string រ = "Sec";

	// Token: 0x0400018E RID: 398
	internal const string ល = "MSec";

	// Token: 0x0400018F RID: 399
	internal const string វ = "Separator";

	// Token: 0x04000190 RID: 400
	internal const string ឝ = "NumericValue";

	// Token: 0x04000191 RID: 401
	internal const string ឞ = "StringValue";

	// Token: 0x04000192 RID: 402
	internal const string ស = "FirstCol";

	// Token: 0x04000193 RID: 403
	internal const string ហ = "FirstRow";

	// Token: 0x04000194 RID: 404
	internal const string ឡ = "LastCol";

	// Token: 0x04000195 RID: 405
	internal const string អ = "LastRow";

	// Token: 0x04000196 RID: 406
	internal const string ឣ = "RTF";

	// Token: 0x04000197 RID: 407
	internal const string ឤ = "PageOrientation";

	// Token: 0x04000198 RID: 408
	internal const string ឥ = "ItemType";

	// Token: 0x04000199 RID: 409
	internal const string ឦ = "RTF_STYLE_";

	// Token: 0x0400019A RID: 410
	internal const string ឧ = "RTF_STYLE_HEADER";

	// Token: 0x0400019B RID: 411
	internal const string ឨ = "RTF_STYLE_CAPTION";

	// Token: 0x0400019C RID: 412
	internal const string ឩ = "RTF_STYLE_DATA";

	// Token: 0x0400019D RID: 413
	internal const string ឪ = "RTF_STYLE_FOOTER";

	// Token: 0x0400019E RID: 414
	internal const string ឫ = "RTF_ITEM_STYLE_";

	// Token: 0x0400019F RID: 415
	internal const string ឬ = "Font_Name";

	// Token: 0x040001A0 RID: 416
	internal const string ឭ = "Font_Size";

	// Token: 0x040001A1 RID: 417
	internal const string ឮ = "Font_Color";

	// Token: 0x040001A2 RID: 418
	internal const string ឯ = "Font_Bold";

	// Token: 0x040001A3 RID: 419
	internal const string ឰ = "Font_Italic";

	// Token: 0x040001A4 RID: 420
	internal const string ឱ = "Font_Underline";

	// Token: 0x040001A5 RID: 421
	internal const string ឲ = "Font_StrikeOut";

	// Token: 0x040001A6 RID: 422
	internal const string ឳ = "BackgroundColor";

	// Token: 0x040001A7 RID: 423
	internal const string \u17B4 = "HighlightColor";

	// Token: 0x040001A8 RID: 424
	internal const string \u17B5 = "AllowHighlight";

	// Token: 0x040001A9 RID: 425
	internal const string \u17B6 = "AllowBackground";

	// Token: 0x040001AA RID: 426
	internal const string \u17B7 = "Alignment";

	// Token: 0x040001AB RID: 427
	internal const string \u17B8 = "HTML";

	// Token: 0x040001AC RID: 428
	internal const string \u17B9 = "Title";

	// Token: 0x040001AD RID: 429
	internal const string \u17BA = "CSS";

	// Token: 0x040001AE RID: 430
	internal const string \u17BB = "CSSFile";

	// Token: 0x040001AF RID: 431
	internal const string \u17BC = "OverwriteCSSFile";

	// Token: 0x040001B0 RID: 432
	internal const string \u17BD = "FileRecCount";

	// Token: 0x040001B1 RID: 433
	internal const string \u17BE = "GenerateIndex";

	// Token: 0x040001B2 RID: 434
	internal const string \u17BF = "IndexLinkTemplate";

	// Token: 0x040001B3 RID: 435
	internal const string \u17C0 = "Navigate_OnTop";

	// Token: 0x040001B4 RID: 436
	internal const string \u17C1 = "Navigate_OnBottom";

	// Token: 0x040001B5 RID: 437
	internal const string \u17C2 = "Index_Title";

	// Token: 0x040001B6 RID: 438
	internal const string \u17C3 = "First_Title";

	// Token: 0x040001B7 RID: 439
	internal const string \u17C4 = "Prior_Title";

	// Token: 0x040001B8 RID: 440
	internal const string \u17C5 = "Next_Title";

	// Token: 0x040001B9 RID: 441
	internal const string \u17C6 = "Last_Title";

	// Token: 0x040001BA RID: 442
	internal const string \u17C7 = "Font_Name";

	// Token: 0x040001BB RID: 443
	internal const string \u17C8 = "Font_Color";

	// Token: 0x040001BC RID: 444
	internal const string \u17C9 = "BackgroundColor";

	// Token: 0x040001BD RID: 445
	internal const string \u17CA = "BackgroundFile";

	// Token: 0x040001BE RID: 446
	internal const string \u17CB = "BodyAdvanced";

	// Token: 0x040001BF RID: 447
	internal const string \u17CC = "CellPadding";

	// Token: 0x040001C0 RID: 448
	internal const string \u17CD = "CellSpacing";

	// Token: 0x040001C1 RID: 449
	internal const string \u17CE = "Border_Width";

	// Token: 0x040001C2 RID: 450
	internal const string \u17CF = "TableBackground";

	// Token: 0x040001C3 RID: 451
	internal const string \u17D0 = "TableAdvanced";

	// Token: 0x040001C4 RID: 452
	internal const string \u17D1 = "HeadBackgroundColor";

	// Token: 0x040001C5 RID: 453
	internal const string \u17D2 = "HeadFontColor";

	// Token: 0x040001C6 RID: 454
	internal const string \u17D3 = "OddRowBackgroundColor";

	// Token: 0x040001C7 RID: 455
	internal const string \u17D4 = "EvenRowBackgroundColor";

	// Token: 0x040001C8 RID: 456
	internal const string \u17D5 = "DataFontColor";

	// Token: 0x040001C9 RID: 457
	internal const string \u17D6 = "LinkColor";

	// Token: 0x040001CA RID: 458
	internal const string \u17D7 = "V_Color";

	// Token: 0x040001CB RID: 459
	internal const string \u17D8 = "A_Color";

	// Token: 0x040001CC RID: 460
	internal const string \u17D9 = "XML";

	// Token: 0x040001CD RID: 461
	internal const string \u17DA = "Standalone";

	// Token: 0x040001CE RID: 462
	internal const string \u17DB = "Encoding";

	// Token: 0x040001CF RID: 463
	internal const string ៜ = "SQL";

	// Token: 0x040001D0 RID: 464
	internal const string \u17DD = "TableName";

	// Token: 0x040001D1 RID: 465
	internal const string \u17DE = "CreateTable";

	// Token: 0x040001D2 RID: 466
	internal const string \u17DF = "CommitRows";

	// Token: 0x040001D3 RID: 467
	internal const string ០ = "CommitAfterScript";

	// Token: 0x040001D4 RID: 468
	internal const string ១ = "CommitStatement";

	// Token: 0x040001D5 RID: 469
	internal const string ២ = "NullValues";

	// Token: 0x040001D6 RID: 470
	internal const string ៣ = "StatementTerm";

	// Token: 0x040001D7 RID: 471
	internal const string ៤ = "TXT";

	// Token: 0x040001D8 RID: 472
	internal const string ៥ = "AutoFitColWidth";

	// Token: 0x040001D9 RID: 473
	internal const string ៦ = "Spacing";

	// Token: 0x040001DA RID: 474
	internal const string ៧ = "CSV";

	// Token: 0x040001DB RID: 475
	internal const string ៨ = "QuoteStrings";

	// Token: 0x040001DC RID: 476
	internal const string ៩ = "Comma";

	// Token: 0x040001DD RID: 477
	internal const string \u17EA = "Quote";

	// Token: 0x040001DE RID: 478
	internal const string \u17EB = "ACCESS";

	// Token: 0x040001DF RID: 479
	internal const string \u17EC = "TableName";

	// Token: 0x040001E0 RID: 480
	internal const string \u17ED = "CreateTable";

	// Token: 0x040001E1 RID: 481
	internal const string \u17EE = "PDF";

	// Token: 0x040001E2 RID: 482
	internal const string \u17EF = "Col_Spacing";

	// Token: 0x040001E3 RID: 483
	internal const string \u17F0 = "Row_Spacing";

	// Token: 0x040001E4 RID: 484
	internal const string \u17F1 = "GridLineWidth";

	// Token: 0x040001E5 RID: 485
	internal const string \u17F2 = "Page_Format";

	// Token: 0x040001E6 RID: 486
	internal const string \u17F3 = "Page_Width";

	// Token: 0x040001E7 RID: 487
	internal const string \u17F4 = "Page_Height";

	// Token: 0x040001E8 RID: 488
	internal const string \u17F5 = "Page_Units";

	// Token: 0x040001E9 RID: 489
	internal const string \u17F6 = "Page_Orientation";

	// Token: 0x040001EA RID: 490
	internal const string \u17F7 = "Page_MarginLeft";

	// Token: 0x040001EB RID: 491
	internal const string \u17F8 = "Page_MarginRight";

	// Token: 0x040001EC RID: 492
	internal const string \u17F9 = "Page_MarginTop";

	// Token: 0x040001ED RID: 493
	internal const string \u17FA = "PageMarginBottom";

	// Token: 0x040001EE RID: 494
	internal const string \u17FB = "PDF_OPTION_HEADER";

	// Token: 0x040001EF RID: 495
	internal const string \u17FC = "PDF_OPTION_CAPTION";

	// Token: 0x040001F0 RID: 496
	internal const string \u17FD = "PDF_OPTION_DATA";

	// Token: 0x040001F1 RID: 497
	internal const string \u17FE = "PDF_OPTION_FOOTER";

	// Token: 0x040001F2 RID: 498
	internal const string \u17FF = "Font_Name";

	// Token: 0x040001F3 RID: 499
	internal const string \u1800 = "Font_Encoding";

	// Token: 0x040001F4 RID: 500
	internal const string \u1801 = "Font_Size";

	// Token: 0x040001F5 RID: 501
	internal const string \u1802 = "Font_Color";

	// Token: 0x040001F6 RID: 502
	internal const string \u1803 = "Font_Name";

	// Token: 0x040001F7 RID: 503
	internal const string \u1804 = "Font_Size";

	// Token: 0x040001F8 RID: 504
	internal const string \u1805 = "Font_Bold";

	// Token: 0x040001F9 RID: 505
	internal const string \u1806 = "Font_Italic";

	// Token: 0x040001FA RID: 506
	internal const string \u1807 = "Font_Underline";

	// Token: 0x040001FB RID: 507
	internal const string \u1808 = "Font_StrikeOut";

	// Token: 0x040001FC RID: 508
	internal const string \u1809 = "Font_Color";

	// Token: 0x040001FD RID: 509
	internal const string \u180A = "Font_Charset";

	// Token: 0x040001FE RID: 510
	internal const string \u180B = "COMMIT WORK;";

	// Token: 0x040001FF RID: 511
	internal const string \u180C = "CREATE TABLE {0}";

	// Token: 0x04000200 RID: 512
	internal const string \u180D = "INSERT INTO {0}";

	// Token: 0x04000201 RID: 513
	internal const string \u180E = "VALUES";

	// Token: 0x04000202 RID: 514
	internal const string \u180F = "Index";

	// Token: 0x04000203 RID: 515
	internal const string ᠐ = "First";

	// Token: 0x04000204 RID: 516
	internal const string ᠑ = "Prior";

	// Token: 0x04000205 RID: 517
	internal const string ᠒ = "Next";

	// Token: 0x04000206 RID: 518
	internal const string ᠓ = "Last";

	// Token: 0x04000207 RID: 519
	internal const string ᠔ = "application/vnd.ms-excel";

	// Token: 0x04000208 RID: 520
	internal const string ᠕ = "application/csv";

	// Token: 0x04000209 RID: 521
	internal const string ᠖ = "application/txt";

	// Token: 0x0400020A RID: 522
	internal const string ᠗ = "application/mdb";

	// Token: 0x0400020B RID: 523
	internal const string ᠘ = "application/dbf";

	// Token: 0x0400020C RID: 524
	internal const string ᠙ = "application/pdf";

	// Token: 0x0400020D RID: 525
	internal const string \u181A = "application/msword";

	// Token: 0x0400020E RID: 526
	internal const string \u181B = "application/dif";

	// Token: 0x0400020F RID: 527
	internal const string \u181C = "application/sylk";

	// Token: 0x04000210 RID: 528
	internal const string \u181D = "application/xml";

	// Token: 0x04000211 RID: 529
	internal const string \u181E = "EncodingType";

	// Token: 0x04000212 RID: 530
	internal const string \u181F = "ExportSource";

	// Token: 0x04000213 RID: 531
	internal const string ᠠ = "CultureName";

	// Token: 0x04000214 RID: 532
	internal const string ᠡ = "NTFIELDS";

	// Token: 0x04000215 RID: 533
	internal const string ᠢ = "ExportType";

	// Token: 0x04000216 RID: 534
	internal const string ᠣ = "Error";

	// Token: 0x04000217 RID: 535
	internal const string ᠤ = "{0} - Component editor";

	// Token: 0x04000218 RID: 536
	internal const string ᠥ = "Hyperlinks";

	// Token: 0x04000219 RID: 537
	internal const string ᠦ = "Hyperlink_{0}";

	// Token: 0x0400021A RID: 538
	internal const string ᠧ = "Notes";

	// Token: 0x0400021B RID: 539
	internal const string ᠨ = "Note_{0}";

	// Token: 0x0400021C RID: 540
	internal const string ᠩ = "Transparency - {0}%";

	// Token: 0x0400021D RID: 541
	internal const string ᠪ = "Charts";

	// Token: 0x0400021E RID: 542
	internal const string ᠫ = "Chart_{0}";

	// Token: 0x0400021F RID: 543
	internal const string ᠬ = "Cells";

	// Token: 0x04000220 RID: 544
	internal const string ᠭ = "Merged Cells";

	// Token: 0x04000221 RID: 545
	internal const string ᠮ = "Series";

	// Token: 0x04000222 RID: 546
	internal const string ᠯ = "Series_{0}";

	// Token: 0x04000223 RID: 547
	internal const string ᠰ = "Confirm";

	// Token: 0x04000224 RID: 548
	internal const string ᠱ = "HEADER";

	// Token: 0x04000225 RID: 549
	internal const string ᠲ = "CAPTION";

	// Token: 0x04000226 RID: 550
	internal const string ᠳ = "DATA";

	// Token: 0x04000227 RID: 551
	internal const string ᠴ = "AGGREGATE";

	// Token: 0x04000228 RID: 552
	internal const string ᠵ = "FOOTER";

	// Token: 0x04000229 RID: 553
	internal const string ᠶ = "HYPERLINK";

	// Token: 0x0400022A RID: 554
	internal const string ᠷ = "NOTE";

	// Token: 0x0400022B RID: 555
	internal const string ᠸ = "Add new sheet";

	// Token: 0x0400022C RID: 556
	internal const string ᠹ = "Define the new sheet title";

	// Token: 0x0400022D RID: 557
	internal const string ᠺ = "Edit sheet";

	// Token: 0x0400022E RID: 558
	internal const string ᠻ = "Edit the sheet title";

	// Token: 0x0400022F RID: 559
	internal const string ᠼ = "Sheet title cannot be empty";

	// Token: 0x04000230 RID: 560
	internal const string ᠽ = "Add field format";

	// Token: 0x04000231 RID: 561
	internal const string ᠾ = "Define name for the column";

	// Token: 0x04000232 RID: 562
	internal const string ᠿ = "Edit field name";

	// Token: 0x04000233 RID: 563
	internal const string ᡀ = "New Sheet {0}";

	// Token: 0x04000234 RID: 564
	internal const string ᡁ = "Cell (Col: {0} Row: {0})";

	// Token: 0x04000235 RID: 565
	internal const string ᡂ = "Merged Cells ({0}, {1}, {2}, {3})";

	// Token: 0x04000236 RID: 566
	internal const string \u1843 = "STYLE_{0}";

	// Token: 0x04000237 RID: 567
	internal const string ᡄ = "Please select or type column name...";

	// Token: 0x04000238 RID: 568
	internal const string ᡅ = "Column with name {0} already exists.";

	// Token: 0x04000239 RID: 569
	internal const string ᡆ = "None";

	// Token: 0x0400023A RID: 570
	internal const string ᡇ = "None";

	// Token: 0x0400023B RID: 571
	internal const string ᡈ = "All item formats will be set default.\n\rWould you like to continue?";

	// Token: 0x0400023C RID: 572
	internal const string ᡉ = "All selected item formats will be set default.\n\rWould you like to continue?";

	// Token: 0x0400023D RID: 573
	internal const string ᡊ = "Question";

	// Token: 0x0400023E RID: 574
	internal const string ᡋ = "{0} - HTML Template Editor";

	// Token: 0x0400023F RID: 575
	internal const string ᡌ = "HTML options";

	// Token: 0x04000240 RID: 576
	internal const string ᡍ = "BkgColor";

	// Token: 0x04000241 RID: 577
	internal const string ᡎ = "FontColor";

	// Token: 0x04000242 RID: 578
	internal const string ᡏ = "LinkColor";

	// Token: 0x04000243 RID: 579
	internal const string ᡐ = "VLinkColor";

	// Token: 0x04000244 RID: 580
	internal const string ᡑ = "ALinkColor";

	// Token: 0x04000245 RID: 581
	internal const string ᡒ = "Table options";

	// Token: 0x04000246 RID: 582
	internal const string ᡓ = "HeaderBkgColor";

	// Token: 0x04000247 RID: 583
	internal const string ᡔ = "HeaderColor";

	// Token: 0x04000248 RID: 584
	internal const string ᡕ = "TableColor";

	// Token: 0x04000249 RID: 585
	internal const string ᡖ = "EvenRowBkgColor";

	// Token: 0x0400024A RID: 586
	internal const string ᡗ = "OddRowBkgColor";

	// Token: 0x0400024B RID: 587
	internal static readonly RegistryKey ᡘ;

	// Token: 0x0400024C RID: 588
	internal static readonly string ᡙ;

	// Token: 0x0400024D RID: 589
	internal static readonly string ᡚ;

	// Token: 0x0400024E RID: 590
	internal static readonly string ᡛ;

	// Token: 0x0400024F RID: 591
	internal static readonly string ᡜ;

	// Token: 0x04000250 RID: 592
	internal static readonly string ᡝ;

	// Token: 0x04000251 RID: 593
	internal static readonly DateTime ᡞ;

	// Token: 0x04000252 RID: 594
	internal static readonly long ᡟ;
}
