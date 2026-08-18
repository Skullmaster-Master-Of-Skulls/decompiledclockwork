using System;
using System.Collections.Generic;
using System.Drawing;

namespace Spire.Xls.Core.Spreadsheet
{
	// Token: 0x02000045 RID: 69
	public interface ISparklineGroup : IList<ISparklines>
	{
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004C3 RID: 1219
		// (set) Token: 0x060004C4 RID: 1220
		bool ShowHorizontalAxis { get; set; }

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060004C5 RID: 1221
		// (set) Token: 0x060004C6 RID: 1222
		bool IsDisplayHidden { get; set; }

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060004C7 RID: 1223
		// (set) Token: 0x060004C8 RID: 1224
		bool PlotRightToLeft { get; set; }

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004C9 RID: 1225
		// (set) Token: 0x060004CA RID: 1226
		bool ShowFirstPoint { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004CB RID: 1227
		// (set) Token: 0x060004CC RID: 1228
		bool ShowLastPoint { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004CD RID: 1229
		// (set) Token: 0x060004CE RID: 1230
		bool ShowLowPoint { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004CF RID: 1231
		// (set) Token: 0x060004D0 RID: 1232
		bool ShowHighPoint { get; set; }

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x060004D1 RID: 1233
		// (set) Token: 0x060004D2 RID: 1234
		bool ShowNegativePoint { get; set; }

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x060004D3 RID: 1235
		// (set) Token: 0x060004D4 RID: 1236
		bool ShowMarkers { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x060004D5 RID: 1237
		// (set) Token: 0x060004D6 RID: 1238
		SparklineType SparklineType { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x060004D7 RID: 1239
		// (set) Token: 0x060004D8 RID: 1240
		bool IsHorizontalDateAxis { get; set; }

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x060004D9 RID: 1241
		// (set) Token: 0x060004DA RID: 1242
		SparklineEmptyCells EmptyCellsType { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x060004DB RID: 1243
		// (set) Token: 0x060004DC RID: 1244
		CellRange HorizontalDateAxisRange { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x060004DD RID: 1245
		// (set) Token: 0x060004DE RID: 1246
		Color HorizontalAxisColor { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x060004DF RID: 1247
		// (set) Token: 0x060004E0 RID: 1248
		Color FirstPointColor { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x060004E1 RID: 1249
		// (set) Token: 0x060004E2 RID: 1250
		Color HighPointColor { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060004E3 RID: 1251
		// (set) Token: 0x060004E4 RID: 1252
		Color LastPointColor { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060004E5 RID: 1253
		// (set) Token: 0x060004E6 RID: 1254
		double LineWeight { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060004E7 RID: 1255
		// (set) Token: 0x060004E8 RID: 1256
		Color LowPointColor { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x060004E9 RID: 1257
		// (set) Token: 0x060004EA RID: 1258
		Color MarkersColor { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060004EB RID: 1259
		// (set) Token: 0x060004EC RID: 1260
		Color NegativePointColor { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060004ED RID: 1261
		// (set) Token: 0x060004EE RID: 1262
		Color SparklineColor { get; set; }

		// Token: 0x060004EF RID: 1263
		SparklineCollection Add();
	}
}
