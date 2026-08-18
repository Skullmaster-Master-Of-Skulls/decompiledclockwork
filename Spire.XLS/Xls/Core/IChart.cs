using System;

namespace Spire.Xls.Core
{
	// Token: 0x020001B7 RID: 439
	public interface IChart
	{
		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001831 RID: 6193
		// (set) Token: 0x06001832 RID: 6194
		ExcelChartType ChartType { get; set; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001833 RID: 6195
		// (set) Token: 0x06001834 RID: 6196
		IXLSRange DataRange { get; set; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001835 RID: 6197
		// (set) Token: 0x06001836 RID: 6198
		bool SeriesDataFromRange { get; set; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001837 RID: 6199
		IChartPageSetup PageSetup { get; }

		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001838 RID: 6200
		// (set) Token: 0x06001839 RID: 6201
		double XPos { get; set; }

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x0600183A RID: 6202
		// (set) Token: 0x0600183B RID: 6203
		double YPos { get; set; }

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x0600183C RID: 6204
		// (set) Token: 0x0600183D RID: 6205
		double Width { get; set; }

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x0600183E RID: 6206
		// (set) Token: 0x0600183F RID: 6207
		double Height { get; set; }

		// Token: 0x170008D7 RID: 2263
		// (get) Token: 0x06001840 RID: 6208
		// (set) Token: 0x06001841 RID: 6209
		string Name { get; set; }

		// Token: 0x170008D8 RID: 2264
		// (get) Token: 0x06001842 RID: 6210
		IChartCategoryAxis PrimaryCategoryAxis { get; }

		// Token: 0x170008D9 RID: 2265
		// (get) Token: 0x06001843 RID: 6211
		IChartValueAxis PrimaryValueAxis { get; }

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06001844 RID: 6212
		IChartSeriesAxis PrimarySerieAxis { get; }

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06001845 RID: 6213
		IChartCategoryAxis SecondaryCategoryAxis { get; }

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06001846 RID: 6214
		IChartValueAxis SecondaryValueAxis { get; }

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06001847 RID: 6215
		IChartFrameFormat ChartArea { get; }

		// Token: 0x170008DE RID: 2270
		// (get) Token: 0x06001848 RID: 6216
		IChartFrameFormat PlotArea { get; }

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001849 RID: 6217
		IChartWallOrFloor Walls { get; }

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x0600184A RID: 6218
		IChartWallOrFloor Floor { get; }

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x0600184B RID: 6219
		IChartDataTable DataTable { get; }

		// Token: 0x170008E2 RID: 2274
		// (get) Token: 0x0600184C RID: 6220
		// (set) Token: 0x0600184D RID: 6221
		bool HasDataTable { get; set; }

		// Token: 0x170008E3 RID: 2275
		// (get) Token: 0x0600184E RID: 6222
		IChartLegend Legend { get; }

		// Token: 0x170008E4 RID: 2276
		// (get) Token: 0x0600184F RID: 6223
		// (set) Token: 0x06001850 RID: 6224
		bool HasLegend { get; set; }

		// Token: 0x170008E5 RID: 2277
		// (get) Token: 0x06001851 RID: 6225
		// (set) Token: 0x06001852 RID: 6226
		int Rotation { get; set; }

		// Token: 0x170008E6 RID: 2278
		// (get) Token: 0x06001853 RID: 6227
		// (set) Token: 0x06001854 RID: 6228
		int Elevation { get; set; }

		// Token: 0x170008E7 RID: 2279
		// (get) Token: 0x06001855 RID: 6229
		// (set) Token: 0x06001856 RID: 6230
		int Perspective { get; set; }

		// Token: 0x170008E8 RID: 2280
		// (get) Token: 0x06001857 RID: 6231
		// (set) Token: 0x06001858 RID: 6232
		int HeightPercent { get; set; }

		// Token: 0x170008E9 RID: 2281
		// (get) Token: 0x06001859 RID: 6233
		// (set) Token: 0x0600185A RID: 6234
		int DepthPercent { get; set; }

		// Token: 0x170008EA RID: 2282
		// (get) Token: 0x0600185B RID: 6235
		// (set) Token: 0x0600185C RID: 6236
		int GapDepth { get; set; }

		// Token: 0x170008EB RID: 2283
		// (get) Token: 0x0600185D RID: 6237
		// (set) Token: 0x0600185E RID: 6238
		bool RightAngleAxes { get; set; }

		// Token: 0x170008EC RID: 2284
		// (get) Token: 0x0600185F RID: 6239
		// (set) Token: 0x06001860 RID: 6240
		bool AutoScaling { get; set; }

		// Token: 0x170008ED RID: 2285
		// (get) Token: 0x06001861 RID: 6241
		// (set) Token: 0x06001862 RID: 6242
		bool WallsAndGridlines2D { get; set; }

		// Token: 0x170008EE RID: 2286
		// (get) Token: 0x06001863 RID: 6243
		// (set) Token: 0x06001864 RID: 6244
		bool HasPlotArea { get; set; }

		// Token: 0x170008EF RID: 2287
		// (get) Token: 0x06001865 RID: 6245
		// (set) Token: 0x06001866 RID: 6246
		ChartPlotEmptyType DisplayBlanksAs { get; set; }

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06001867 RID: 6247
		// (set) Token: 0x06001868 RID: 6248
		bool PlotVisibleOnly { get; set; }

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001869 RID: 6249
		// (set) Token: 0x0600186A RID: 6250
		bool SizeWithWindow { get; set; }

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x0600186B RID: 6251
		// (set) Token: 0x0600186C RID: 6252
		PivotTable PivotTable { get; set; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x0600186D RID: 6253
		// (set) Token: 0x0600186E RID: 6254
		ExcelChartType PivotChartType { get; set; }

		// Token: 0x170008F4 RID: 2292
		// (get) Token: 0x0600186F RID: 6255
		// (set) Token: 0x06001870 RID: 6256
		bool DisplayEntireFieldButtons { get; set; }

		// Token: 0x170008F5 RID: 2293
		// (get) Token: 0x06001871 RID: 6257
		// (set) Token: 0x06001872 RID: 6258
		bool DisplayValueFieldButtons { get; set; }

		// Token: 0x170008F6 RID: 2294
		// (get) Token: 0x06001873 RID: 6259
		// (set) Token: 0x06001874 RID: 6260
		bool DisplayAxisFieldButtons { get; set; }

		// Token: 0x170008F7 RID: 2295
		// (get) Token: 0x06001875 RID: 6261
		// (set) Token: 0x06001876 RID: 6262
		bool DisplayLegendFieldButtons { get; set; }

		// Token: 0x170008F8 RID: 2296
		// (get) Token: 0x06001877 RID: 6263
		// (set) Token: 0x06001878 RID: 6264
		bool ShowReportFilterFieldButtons { get; set; }
	}
}
