using System;
using System.Drawing;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001777 RID: 6007
	internal struct DefaultValues
	{
		// Token: 0x0400438B RID: 17291
		internal const int DEFAULT_GENERAL_TICK_LENGTH = 5;

		// Token: 0x0400438C RID: 17292
		internal const float DEFAULT_GENERAL_TICK_WIDTH = 1f;

		// Token: 0x0400438D RID: 17293
		internal const int DEFAULT_MINOR_TICK_LENGTH = 2;

		// Token: 0x0400438E RID: 17294
		internal const bool DEFAULT_SERIES_ITEM_CONNECTOR_VISIBLE = false;

		// Token: 0x0400438F RID: 17295
		internal const string DEFAULT_POINT_SHAPE_NAME = "Ellipse";

		// Token: 0x04004390 RID: 17296
		internal const float DEFAULT_POINT_ROTATION_ANGLE = 0f;

		// Token: 0x04004391 RID: 17297
		internal const CornerType DEFAULT_CORNER_TYPE = CornerType.Rectangle;

		// Token: 0x04004392 RID: 17298
		internal const int DEFAULT_ROUND_SIZE = 3;

		// Token: 0x04004393 RID: 17299
		internal const string DEFAULT_STYLE_COLOR_NAME = "";

		// Token: 0x04004394 RID: 17300
		internal const string DEFAULT_PALETTE_ITEM_NAME = "PaletteItem";

		// Token: 0x04004395 RID: 17301
		internal const string DEFAULT_PALETTE_NAME = "Palette";

		// Token: 0x04004396 RID: 17302
		internal const float DEFAULT_POSITION_VALUE = 0f;

		// Token: 0x04004397 RID: 17303
		internal const bool DEFAULT_AUTOSIZE = true;

		// Token: 0x04004398 RID: 17304
		internal const EmtyValuesMode DEFAULT_EMPTY_VALUE_MODE = EmtyValuesMode.Approximation;

		// Token: 0x04004399 RID: 17305
		internal const string DEFAULT_FIGURE_NAME = "CustomRectangle";

		// Token: 0x0400439A RID: 17306
		internal const string DEFAULT_FIGURE_DESRIPTION = "20,20,200,100:20,20,False,0,0,0,0,0:220,20,False,0,0,0,0,0:220,120,False,0,0,0,0,0:20,120,False,0,0,0,0,0:";

		// Token: 0x0400439B RID: 17307
		internal const int DEFAULT_UNIT_VALUE = 0;

		// Token: 0x0400439C RID: 17308
		internal const UnitType DEFAULT_UNIT_TYPE = UnitType.Pixel;

		// Token: 0x0400439D RID: 17309
		internal const AutoTextWrap TEXT_BLOCK_AUTO_WRAP = AutoTextWrap.Auto;

		// Token: 0x0400439E RID: 17310
		internal static Font DEFAULT_TEXT_FONT = new Font("Verdana", 8.25f);

		// Token: 0x0400439F RID: 17311
		internal static Font VERDANA10_BOLD = new Font("Verdana", 10f, FontStyle.Bold);

		// Token: 0x040043A0 RID: 17312
		internal static Font VERDANA15 = new Font("Verdana", 15f);

		// Token: 0x040043A1 RID: 17313
		internal static Color DEFAULT_TEXT_COLOR = Color.FromArgb(51, 51, 51);

		// Token: 0x040043A2 RID: 17314
		internal static Color DEFAULT_ERROR_COLOR = Color.Red;

		// Token: 0x040043A3 RID: 17315
		internal static int DEFAULT_MAX_TEXT_LENGTH = 255;

		// Token: 0x040043A4 RID: 17316
		internal static int DEFAULT_MAX_ITEM_TEXT_LENGTH = 30;

		// Token: 0x040043A5 RID: 17317
		internal static Color DEFAULT_AXIS_TEXT_COLOR = Color.FromArgb(160, 160, 160);

		// Token: 0x040043A6 RID: 17318
		internal static Color DEFAULT_TICK_COLOR = Color.FromArgb(160, 160, 160);

		// Token: 0x040043A7 RID: 17319
		internal static Color DEFAULT_GRIDLINE_COLOR = Color.FromArgb(38, 215, 215, 215);

		// Token: 0x040043A8 RID: 17320
		internal static int DEFAULT_MINOR_TICK_COUNT = 3;

		// Token: 0x040043A9 RID: 17321
		internal static Color DEFAULT_SERIESBORDER_COLOR = Color.FromArgb(153, 209, 248);

		// Token: 0x040043AA RID: 17322
		internal static Color DEFAULT_SERIESTEXT_COLOR = Color.FromArgb(153, 153, 153);

		// Token: 0x040043AB RID: 17323
		internal static Unit ONE_PIXEL = Unit.Pixel(1f);

		// Token: 0x040043AC RID: 17324
		internal static Unit ONE_PERCENTAGE = Unit.Percentage(1.0);

		// Token: 0x040043AD RID: 17325
		internal static Color DEFAULT_TITLE_BORDER_COLOR = Color.FromArgb(199, 199, 199);

		// Token: 0x040043AE RID: 17326
		internal static Color DEFAULT_LEGEND_BORDER_COLOR = Color.FromArgb(156, 156, 156);

		// Token: 0x040043AF RID: 17327
		internal static Color DEFAULT_CHART_BORDER_COLOR = Color.FromArgb(56, 56, 56);

		// Token: 0x040043B0 RID: 17328
		internal static Color DEFAULT_DATATABLE_BORDER_COLOR = Color.FromArgb(150, 150, 150);

		// Token: 0x040043B1 RID: 17329
		internal static Color DEFAULT_SERIES_ITEM_LABEL_CONNECTOR_COLOR = Color.Black;

		// Token: 0x040043B2 RID: 17330
		internal static Color DEFAULT_SCALE_BREAK_COLOR = Color.Gray;

		// Token: 0x040043B3 RID: 17331
		internal static Color DEFAULT_SHADOW_COLOR = Color.Black;

		// Token: 0x040043B4 RID: 17332
		internal static Color DEFAULT_AXIS_COLOR = Color.Black;

		// Token: 0x040043B5 RID: 17333
		internal static Color DEFAULT_STYLE_COLOR = Color.Empty;

		// Token: 0x040043B6 RID: 17334
		internal static Unit DEFAULT_PIXEL_VALUE = Unit.Pixel(0f);

		// Token: 0x040043B7 RID: 17335
		internal static Unit DEFAULT_POINTMARK_PIXEL_VALUE = Unit.Pixel(8f);

		// Token: 0x040043B8 RID: 17336
		internal static Unit DEFAULT_MARKER_PIXEL_VALUE = Unit.Pixel(10f);

		// Token: 0x040043B9 RID: 17337
		internal static Unit DEFAULT_MARGIN_TITLE_TOP = Unit.Percentage(4.0);

		// Token: 0x040043BA RID: 17338
		internal static Unit DEFAULT_MARGIN_TITLE_RIGHT = Unit.Pixel(10f);

		// Token: 0x040043BB RID: 17339
		internal static Unit DEFAULT_MARGIN_TITLE_BOTTOM = Unit.Pixel(14f);

		// Token: 0x040043BC RID: 17340
		internal static Unit DEFAULT_MARGIN_TITLE_LEFT = Unit.Percentage(7.0);

		// Token: 0x040043BD RID: 17341
		internal static Unit DEFAULT_MARGIN_PLOTAREA_TOP = Unit.Percentage(18.0);

		// Token: 0x040043BE RID: 17342
		internal static Unit DEFAULT_MARGIN_PLOTAREA_RIGHT = Unit.Percentage(24.0);

		// Token: 0x040043BF RID: 17343
		internal static Unit DEFAULT_MARGIN_PLOTAREA_BOTTOM = Unit.Percentage(12.0);

		// Token: 0x040043C0 RID: 17344
		internal static Unit DEFAULT_MARGIN_PLOTAREA_LEFT = Unit.Percentage(10.0);

		// Token: 0x040043C1 RID: 17345
		internal static Unit DEFAULT_MARGIN_LEGEND_RIGHT = Unit.Percentage(2.0);

		// Token: 0x040043C2 RID: 17346
		internal static Unit AUTO_MARGIN_PLOTAREA_TOP = Unit.Percentage(8.0);

		// Token: 0x040043C3 RID: 17347
		internal static Unit AUTO_MARGIN_PLOTAREA_BOTTOM = Unit.Percentage(8.0);

		// Token: 0x040043C4 RID: 17348
		internal static Unit AUTO_MARGIN_PLOTAREA_LEFT = Unit.Percentage(6.0);

		// Token: 0x040043C5 RID: 17349
		internal static Unit AUTO_MARGIN_PLOTAREA_RIGHT = Unit.Percentage(6.0);

		// Token: 0x040043C6 RID: 17350
		internal static Unit AUTO_MARGIN_LEGEND = Unit.Percentage(1.0);

		// Token: 0x040043C7 RID: 17351
		internal static Unit AUTO_MARGIN_TITLE = Unit.Percentage(1.0);

		// Token: 0x040043C8 RID: 17352
		internal static Unit AUTO_MARGIN_DATATABLE = Unit.Percentage(1.0);

		// Token: 0x040043C9 RID: 17353
		internal static readonly Unit DEFAULT_PADDING_PIXEL3 = Unit.Pixel(3f);

		// Token: 0x040043CA RID: 17354
		internal static readonly Unit DEFAULT_PADDING_PIXEL5 = Unit.Pixel(5f);

		// Token: 0x040043CB RID: 17355
		internal static readonly Unit DEFAULT_PADDING_PIXEL2 = Unit.Pixel(2f);
	}
}
