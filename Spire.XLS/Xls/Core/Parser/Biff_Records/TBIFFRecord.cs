using System;

namespace Spire.Xls.Core.Parser.Biff_Records
{
	// Token: 0x020005EA RID: 1514
	public enum TBIFFRecord
	{
		// Token: 0x04002AFA RID: 11002
		Array = 545,
		// Token: 0x04002AFB RID: 11003
		AutoFilter = 158,
		// Token: 0x04002AFC RID: 11004
		AutoFilterInfo = 157,
		// Token: 0x04002AFD RID: 11005
		BOF = 2057,
		// Token: 0x04002AFE RID: 11006
		BOF2 = 1033,
		// Token: 0x04002AFF RID: 11007
		Backup = 64,
		// Token: 0x04002B00 RID: 11008
		Begin = 4147,
		// Token: 0x04002B01 RID: 11009
		Bitmap = 233,
		// Token: 0x04002B02 RID: 11010
		Blank = 513,
		// Token: 0x04002B03 RID: 11011
		BookBool = 218,
		// Token: 0x04002B04 RID: 11012
		BoolErr = 517,
		// Token: 0x04002B05 RID: 11013
		BottomMargin = 41,
		// Token: 0x04002B06 RID: 11014
		BoundSheet = 133,
		// Token: 0x04002B07 RID: 11015
		CF = 433,
		// Token: 0x04002B08 RID: 11016
		CRN = 90,
		// Token: 0x04002B09 RID: 11017
		CalCount = 12,
		// Token: 0x04002B0A RID: 11018
		CalcMode,
		// Token: 0x04002B0B RID: 11019
		CodeName = 442,
		// Token: 0x04002B0C RID: 11020
		Codepage = 66,
		// Token: 0x04002B0D RID: 11021
		ColumnInfo = 125,
		// Token: 0x04002B0E RID: 11022
		CondFMT = 432,
		// Token: 0x04002B0F RID: 11023
		Continue = 60,
		// Token: 0x04002B10 RID: 11024
		Country = 140,
		// Token: 0x04002B11 RID: 11025
		CustomProperty = 1048,
		// Token: 0x04002B12 RID: 11026
		DBCell = 215,
		// Token: 0x04002B13 RID: 11027
		DCON = 80,
		// Token: 0x04002B14 RID: 11028
		DCONBIN = 437,
		// Token: 0x04002B15 RID: 11029
		DCONNAME = 82,
		// Token: 0x04002B16 RID: 11030
		DCONRef = 81,
		// Token: 0x04002B17 RID: 11031
		DSF = 353,
		// Token: 0x04002B18 RID: 11032
		DV = 446,
		// Token: 0x04002B19 RID: 11033
		DVal = 434,
		// Token: 0x04002B1A RID: 11034
		DateWindow1904 = 34,
		// Token: 0x04002B1B RID: 11035
		DefaultColWidth = 85,
		// Token: 0x04002B1C RID: 11036
		DefaultRowHeight = 549,
		// Token: 0x04002B1D RID: 11037
		Delta = 16,
		// Token: 0x04002B1E RID: 11038
		Dimensions = 512,
		// Token: 0x04002B1F RID: 11039
		EOF = 10,
		// Token: 0x04002B20 RID: 11040
		End = 4148,
		// Token: 0x04002B21 RID: 11041
		ExtSST = 255,
		// Token: 0x04002B22 RID: 11042
		ExtSSTInfoSub = 4095,
		// Token: 0x04002B23 RID: 11043
		ExtendedFormat = 224,
		// Token: 0x04002B24 RID: 11044
		ExtendedFormatCRC = 2172,
		// Token: 0x04002B25 RID: 11045
		ExtendedXFRecord,
		// Token: 0x04002B26 RID: 11046
		ExternCount = 22,
		// Token: 0x04002B27 RID: 11047
		ExternName = 35,
		// Token: 0x04002B28 RID: 11048
		ExternSheet = 23,
		// Token: 0x04002B29 RID: 11049
		FilePass = 47,
		// Token: 0x04002B2A RID: 11050
		FileSharing = 91,
		// Token: 0x04002B2B RID: 11051
		FilterMode = 155,
		// Token: 0x04002B2C RID: 11052
		FnGroupCount,
		// Token: 0x04002B2D RID: 11053
		Font = 49,
		// Token: 0x04002B2E RID: 11054
		Footer = 21,
		// Token: 0x04002B2F RID: 11055
		Format = 1054,
		// Token: 0x04002B30 RID: 11056
		Formula = 6,
		// Token: 0x04002B31 RID: 11057
		Gridset = 130,
		// Token: 0x04002B32 RID: 11058
		Guts = 128,
		// Token: 0x04002B33 RID: 11059
		HasBasic = 211,
		// Token: 0x04002B34 RID: 11060
		HCenter = 131,
		// Token: 0x04002B35 RID: 11061
		HLink = 440,
		// Token: 0x04002B36 RID: 11062
		Header = 20,
		// Token: 0x04002B37 RID: 11063
		HeaderFooterImage = 2150,
		// Token: 0x04002B38 RID: 11064
		HeaderFooter = 2204,
		// Token: 0x04002B39 RID: 11065
		HideObj = 141,
		// Token: 0x04002B3A RID: 11066
		HorizontalPageBreaks = 27,
		// Token: 0x04002B3B RID: 11067
		ImageData = 127,
		// Token: 0x04002B3C RID: 11068
		Index = 523,
		// Token: 0x04002B3D RID: 11069
		InterfaceEnd = 226,
		// Token: 0x04002B3E RID: 11070
		InterfaceHdr = 225,
		// Token: 0x04002B3F RID: 11071
		Iteration = 17,
		// Token: 0x04002B40 RID: 11072
		Label = 516,
		// Token: 0x04002B41 RID: 11073
		LabelRanges = 351,
		// Token: 0x04002B42 RID: 11074
		LabelSST = 253,
		// Token: 0x04002B43 RID: 11075
		LeftMargin = 38,
		// Token: 0x04002B44 RID: 11076
		MMS = 193,
		// Token: 0x04002B45 RID: 11077
		MergeCells = 229,
		// Token: 0x04002B46 RID: 11078
		MSODrawing = 236,
		// Token: 0x04002B47 RID: 11079
		MSODrawingGroup = 235,
		// Token: 0x04002B48 RID: 11080
		MulBlank = 190,
		// Token: 0x04002B49 RID: 11081
		MulRK = 189,
		// Token: 0x04002B4A RID: 11082
		Name = 24,
		// Token: 0x04002B4B RID: 11083
		Note = 28,
		// Token: 0x04002B4C RID: 11084
		Number = 515,
		// Token: 0x04002B4D RID: 11085
		OBJ = 93,
		// Token: 0x04002B4E RID: 11086
		ObjectProtect = 99,
		// Token: 0x04002B4F RID: 11087
		OleSize = 222,
		// Token: 0x04002B50 RID: 11088
		Palette = 146,
		// Token: 0x04002B51 RID: 11089
		Pane = 65,
		// Token: 0x04002B52 RID: 11090
		Password = 19,
		// Token: 0x04002B53 RID: 11091
		PasswordRev4 = 444,
		// Token: 0x04002B54 RID: 11092
		Precision = 14,
		// Token: 0x04002B55 RID: 11093
		PrintedChartSize = 51,
		// Token: 0x04002B56 RID: 11094
		PrinterSettings = 77,
		// Token: 0x04002B57 RID: 11095
		PrintGridlines = 43,
		// Token: 0x04002B58 RID: 11096
		PrintHeaders = 42,
		// Token: 0x04002B59 RID: 11097
		PrintSetup = 161,
		// Token: 0x04002B5A RID: 11098
		Protect = 18,
		// Token: 0x04002B5B RID: 11099
		ProtectionRev4 = 431,
		// Token: 0x04002B5C RID: 11100
		QuickTip = 2048,
		// Token: 0x04002B5D RID: 11101
		RefMode = 15,
		// Token: 0x04002B5E RID: 11102
		RefreshAll = 439,
		// Token: 0x04002B5F RID: 11103
		RightMargin = 39,
		// Token: 0x04002B60 RID: 11104
		RK = 638,
		// Token: 0x04002B61 RID: 11105
		Row = 520,
		// Token: 0x04002B62 RID: 11106
		RString = 214,
		// Token: 0x04002B63 RID: 11107
		SaveRecalc = 95,
		// Token: 0x04002B64 RID: 11108
		ScenProtect = 221,
		// Token: 0x04002B65 RID: 11109
		Selection = 29,
		// Token: 0x04002B66 RID: 11110
		DxGCol = 153,
		// Token: 0x04002B67 RID: 11111
		Setup = 161,
		// Token: 0x04002B68 RID: 11112
		SharedFormula = 188,
		// Token: 0x04002B69 RID: 11113
		SharedFormula2 = 1212,
		// Token: 0x04002B6A RID: 11114
		SheetLayout = 2146,
		// Token: 0x04002B6B RID: 11115
		Sort = 144,
		// Token: 0x04002B6C RID: 11116
		SST = 252,
		// Token: 0x04002B6D RID: 11117
		String = 519,
		// Token: 0x04002B6E RID: 11118
		Style = 659,
		// Token: 0x04002B6F RID: 11119
		SupBook = 430,
		// Token: 0x04002B70 RID: 11120
		TabId = 317,
		// Token: 0x04002B71 RID: 11121
		Table = 54,
		// Token: 0x04002B72 RID: 11122
		Template = 96,
		// Token: 0x04002B73 RID: 11123
		TextObject = 438,
		// Token: 0x04002B74 RID: 11124
		TopMargin = 40,
		// Token: 0x04002B75 RID: 11125
		UseSelFS = 352,
		// Token: 0x04002B76 RID: 11126
		VCenter = 132,
		// Token: 0x04002B77 RID: 11127
		VerticalPageBreaks = 26,
		// Token: 0x04002B78 RID: 11128
		WSBool = 129,
		// Token: 0x04002B79 RID: 11129
		WindowOne = 61,
		// Token: 0x04002B7A RID: 11130
		WindowProtect = 25,
		// Token: 0x04002B7B RID: 11131
		WindowTwo = 574,
		// Token: 0x04002B7C RID: 11132
		WindowZoom = 160,
		// Token: 0x04002B7D RID: 11133
		WriteAccess = 92,
		// Token: 0x04002B7E RID: 11134
		WriteProtection = 134,
		// Token: 0x04002B7F RID: 11135
		XCT = 89,
		// Token: 0x04002B80 RID: 11136
		Unknown = 0,
		// Token: 0x04002B81 RID: 11137
		UnkBegin = 448,
		// Token: 0x04002B82 RID: 11138
		UnkEnd,
		// Token: 0x04002B83 RID: 11139
		UnkMarker = 239,
		// Token: 0x04002B84 RID: 11140
		UnkMacrosDisable = 445,
		// Token: 0x04002B85 RID: 11141
		BookExt = 2147,
		// Token: 0x04002B86 RID: 11142
		ChartDataLabels = 2155,
		// Token: 0x04002B87 RID: 11143
		ChartChart = 4098,
		// Token: 0x04002B88 RID: 11144
		ChartSeries,
		// Token: 0x04002B89 RID: 11145
		ChartDataFormat = 4102,
		// Token: 0x04002B8A RID: 11146
		ChartLineFormat,
		// Token: 0x04002B8B RID: 11147
		ChartMarkerFormat = 4105,
		// Token: 0x04002B8C RID: 11148
		ChartAreaFormat,
		// Token: 0x04002B8D RID: 11149
		ChartPieFormat,
		// Token: 0x04002B8E RID: 11150
		ChartAttachedLabel,
		// Token: 0x04002B8F RID: 11151
		ChartSeriesText,
		// Token: 0x04002B90 RID: 11152
		ChartChartFormat = 4116,
		// Token: 0x04002B91 RID: 11153
		ChartLegend,
		// Token: 0x04002B92 RID: 11154
		ChartSeriesList,
		// Token: 0x04002B93 RID: 11155
		ChartBar,
		// Token: 0x04002B94 RID: 11156
		ChartLine,
		// Token: 0x04002B95 RID: 11157
		ChartPie,
		// Token: 0x04002B96 RID: 11158
		ChartArea,
		// Token: 0x04002B97 RID: 11159
		ChartScatter,
		// Token: 0x04002B98 RID: 11160
		ChartChartLine,
		// Token: 0x04002B99 RID: 11161
		ChartAxis,
		// Token: 0x04002B9A RID: 11162
		ChartTick,
		// Token: 0x04002B9B RID: 11163
		ChartValueRange,
		// Token: 0x04002B9C RID: 11164
		ChartCatserRange,
		// Token: 0x04002B9D RID: 11165
		ChartAxisLineFormat,
		// Token: 0x04002B9E RID: 11166
		ChartFormatLink,
		// Token: 0x04002B9F RID: 11167
		ChartDefaultText = 4132,
		// Token: 0x04002BA0 RID: 11168
		ChartText,
		// Token: 0x04002BA1 RID: 11169
		ChartFontx,
		// Token: 0x04002BA2 RID: 11170
		ChartObjectLink,
		// Token: 0x04002BA3 RID: 11171
		ChartFrame = 4146,
		// Token: 0x04002BA4 RID: 11172
		ChartPlotArea = 4149,
		// Token: 0x04002BA5 RID: 11173
		Chart3D = 4154,
		// Token: 0x04002BA6 RID: 11174
		ChartPicf = 4156,
		// Token: 0x04002BA7 RID: 11175
		ChartDropBar,
		// Token: 0x04002BA8 RID: 11176
		ChartRadar,
		// Token: 0x04002BA9 RID: 11177
		ChartSurface,
		// Token: 0x04002BAA RID: 11178
		ChartRadarArea,
		// Token: 0x04002BAB RID: 11179
		ChartAxisParent,
		// Token: 0x04002BAC RID: 11180
		ChartLegendxn = 4163,
		// Token: 0x04002BAD RID: 11181
		ChartShtprops,
		// Token: 0x04002BAE RID: 11182
		ChartSertocrt,
		// Token: 0x04002BAF RID: 11183
		ChartAxesUsed,
		// Token: 0x04002BB0 RID: 11184
		ChartSbaseref = 4168,
		// Token: 0x04002BB1 RID: 11185
		ChartSerParent = 4170,
		// Token: 0x04002BB2 RID: 11186
		ChartSerAuxTrend,
		// Token: 0x04002BB3 RID: 11187
		ChartIfmt = 4174,
		// Token: 0x04002BB4 RID: 11188
		ChartPos,
		// Token: 0x04002BB5 RID: 11189
		ChartAlruns,
		// Token: 0x04002BB6 RID: 11190
		ChartAI,
		// Token: 0x04002BB7 RID: 11191
		ChartTextPropsStream = 2213,
		// Token: 0x04002BB8 RID: 11192
		ChartSerAuxErrBar = 4187,
		// Token: 0x04002BB9 RID: 11193
		ChartSerFmt = 4189,
		// Token: 0x04002BBA RID: 11194
		Chart3DDataFormat = 4191,
		// Token: 0x04002BBB RID: 11195
		ChartFbi,
		// Token: 0x04002BBC RID: 11196
		ChartBoppop,
		// Token: 0x04002BBD RID: 11197
		ChartAxcext,
		// Token: 0x04002BBE RID: 11198
		ChartDat,
		// Token: 0x04002BBF RID: 11199
		ChartPlotGrowth,
		// Token: 0x04002BC0 RID: 11200
		ChartSiIndex,
		// Token: 0x04002BC1 RID: 11201
		ChartGelFrame,
		// Token: 0x04002BC2 RID: 11202
		ChartBoppCustom,
		// Token: 0x04002BC3 RID: 11203
		ChartShadow,
		// Token: 0x04002BC4 RID: 11204
		ChartUnits = 4097,
		// Token: 0x04002BC5 RID: 11205
		ChartWrapper = 2129,
		// Token: 0x04002BC6 RID: 11206
		ChartAxisDisplayUnits = 2135,
		// Token: 0x04002BC7 RID: 11207
		ChartBegDispUnit = 2132,
		// Token: 0x04002BC8 RID: 11208
		ChartEndDispUnit,
		// Token: 0x04002BC9 RID: 11209
		ChartAxisOffset,
		// Token: 0x04002BCA RID: 11210
		CacheData = 198,
		// Token: 0x04002BCB RID: 11211
		CacheDataEx = 290,
		// Token: 0x04002BCC RID: 11212
		DataItem = 197,
		// Token: 0x04002BCD RID: 11213
		ViewExtendedInfo = 241,
		// Token: 0x04002BCE RID: 11214
		ExternalSourceInfo = 220,
		// Token: 0x04002BCF RID: 11215
		SQLDataTypeId = 443,
		// Token: 0x04002BD0 RID: 11216
		RuleFilter = 242,
		// Token: 0x04002BD1 RID: 11217
		ParsedExpression = 249,
		// Token: 0x04002BD2 RID: 11218
		PivotFormat = 251,
		// Token: 0x04002BD3 RID: 11219
		PivotFormula = 259,
		// Token: 0x04002BD4 RID: 11220
		StreamId = 213,
		// Token: 0x04002BD5 RID: 11221
		RowColumnFieldId = 180,
		// Token: 0x04002BD6 RID: 11222
		LineItemArray,
		// Token: 0x04002BD7 RID: 11223
		PivotName = 246,
		// Token: 0x04002BD8 RID: 11224
		PivotNamePair = 248,
		// Token: 0x04002BD9 RID: 11225
		PageItem = 182,
		// Token: 0x04002BDA RID: 11226
		RuleData = 240,
		// Token: 0x04002BDB RID: 11227
		SelectionInfo = 247,
		// Token: 0x04002BDC RID: 11228
		SheetProtection = 2151,
		// Token: 0x04002BDD RID: 11229
		RangeProtection,
		// Token: 0x04002BDE RID: 11230
		PivotString = 205,
		// Token: 0x04002BDF RID: 11231
		PivotSourceInfo = 208,
		// Token: 0x04002BE0 RID: 11232
		PageItemIndexes = 210,
		// Token: 0x04002BE1 RID: 11233
		PageItemNameCount = 209,
		// Token: 0x04002BE2 RID: 11234
		PivotViewFields = 177,
		// Token: 0x04002BE3 RID: 11235
		PivotViewFieldsEx = 256,
		// Token: 0x04002BE4 RID: 11236
		PivotViewItem = 178,
		// Token: 0x04002BE5 RID: 11237
		PivotViewDefinition = 176,
		// Token: 0x04002BE6 RID: 11238
		PivotViewSource = 227,
		// Token: 0x04002BE7 RID: 11239
		PivotDateTime = 206,
		// Token: 0x04002BE8 RID: 11240
		PivotDouble = 201,
		// Token: 0x04002BE9 RID: 11241
		PivotEmpty = 207,
		// Token: 0x04002BEA RID: 11242
		PivotBoolean = 202,
		// Token: 0x04002BEB RID: 11243
		PivotError,
		// Token: 0x04002BEC RID: 11244
		PivotField = 199,
		// Token: 0x04002BED RID: 11245
		PivotIndexList,
		// Token: 0x04002BEE RID: 11246
		PivotViewAdditionalInfo = 2148,
		// Token: 0x04002BEF RID: 11247
		Compatibility = 2188,
		// Token: 0x04002BF0 RID: 11248
		DBQueryExt = 2051,
		// Token: 0x04002BF1 RID: 11249
		Qsi = 429,
		// Token: 0x04002BF2 RID: 11250
		Qsif = 2055,
		// Token: 0x04002BF3 RID: 11251
		DbOrParamQry = 220,
		// Token: 0x04002BF4 RID: 11252
		QsiSXTag = 2050,
		// Token: 0x04002BF5 RID: 11253
		Feature12 = 2168,
		// Token: 0x04002BF6 RID: 11254
		Qsir = 2054
	}
}
