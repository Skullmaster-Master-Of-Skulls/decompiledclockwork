using System;
using System.Drawing;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001748 RID: 5960
	internal sealed class DefaultValues
	{
		// Token: 0x0600E8B8 RID: 59576 RVA: 0x00343F7F File Offset: 0x0034217F
		internal static Color GetMainColor(int index)
		{
			return DefaultValues.defaultForeColors[index % DefaultValues.defaultForeColors.Length];
		}

		// Token: 0x0600E8B9 RID: 59577 RVA: 0x00343F99 File Offset: 0x00342199
		internal static Color GetSecondColor(int index)
		{
			return DefaultValues.defaultSecondColors[index % DefaultValues.defaultSecondColors.Length];
		}

		// Token: 0x0600E8BB RID: 59579 RVA: 0x00343FB4 File Offset: 0x003421B4
		// Note: this type is marked as 'beforefieldinit'.
		static DefaultValues()
		{
			DefaultValues.AXIS_ITEM_VALUE = 0m;
			DefaultValues.LABEL_ITEM_CONNECTION_POINT = Point.Empty;
		}

		// Token: 0x040042B3 RID: 17075
		internal const int ROUND_DIGITS = 13;

		// Token: 0x040042B4 RID: 17076
		internal const double MIN_POSSIBLE_STEP = 1E-13;

		// Token: 0x040042B5 RID: 17077
		internal const string PALETTE_NAME = "";

		// Token: 0x040042B6 RID: 17078
		internal const string NO_PALETTE_NAME = "(None)";

		// Token: 0x040042B7 RID: 17079
		internal const string SKIN_NAME = "";

		// Token: 0x040042B8 RID: 17080
		internal const string NO_SKIN_NAME = "(None)";

		// Token: 0x040042B9 RID: 17081
		internal const bool SKINS_OVERRIDE_STYLES = false;

		// Token: 0x040042BA RID: 17082
		internal const string EXCEPTION_MESSAGE = "An Unexpected error has occurred. Please review the InnerException for more information how to resolve the problem.";

		// Token: 0x040042BB RID: 17083
		internal const bool AUTO_TEXT_WRAP = false;

		// Token: 0x040042BC RID: 17084
		internal const string PLOT_AREA_NAME = "";

		// Token: 0x040042BD RID: 17085
		internal const ChartSeriesOrientation PLOT_AREA_SERIES_ORIENTATION = ChartSeriesOrientation.Vertical;

		// Token: 0x040042BE RID: 17086
		internal const bool PLOT_AREA_INTELLIGENT_LABELS_ENABLED = false;

		// Token: 0x040042BF RID: 17087
		internal const string MARKED_ZONE_NAME = "Marked zone";

		// Token: 0x040042C0 RID: 17088
		internal const double MARKED_ZONE_VALUE_START_X = 0.0;

		// Token: 0x040042C1 RID: 17089
		internal const double MARKED_ZONE_VALUE_START_Y = 0.0;

		// Token: 0x040042C2 RID: 17090
		internal const double MARKED_ZONE_VALUE_END_X = 0.0;

		// Token: 0x040042C3 RID: 17091
		internal const double MARKED_ZONE_VALUE_END_Y = 0.0;

		// Token: 0x040042C4 RID: 17092
		internal const ChartYAxisType MARKED_ZONE_Y_AXIS_TYPE = ChartYAxisType.Primary;

		// Token: 0x040042C5 RID: 17093
		internal const bool AXIS_AUTO_SCALE = true;

		// Token: 0x040042C6 RID: 17094
		internal const bool AXIS_IS_ZERO_BASED = true;

		// Token: 0x040042C7 RID: 17095
		internal const double AXIS_STEP = 1.0;

		// Token: 0x040042C8 RID: 17096
		internal const double AXIS_MIN_VALUE = 0.0;

		// Token: 0x040042C9 RID: 17097
		internal const double AXIS_MAX_VALUE = 7.0;

		// Token: 0x040042CA RID: 17098
		internal const int AXIS_MAX_ITEMS_COUNT = 8;

		// Token: 0x040042CB RID: 17099
		internal const int AXIS_LABEL_STEP = 1;

		// Token: 0x040042CC RID: 17100
		internal const ChartAxisVisibleValues AXIS_VISIBLE_VALUES = ChartAxisVisibleValues.All;

		// Token: 0x040042CD RID: 17101
		internal const string AXIS_DATA_LABELS_COLUMN = "";

		// Token: 0x040042CE RID: 17102
		internal const ChartAxisLayoutMode AXIS_LAYOUT_STYLE = ChartAxisLayoutMode.Between;

		// Token: 0x040042CF RID: 17103
		internal const ChartYAxisMode Y_AXIS_MODE = ChartYAxisMode.Normal;

		// Token: 0x040042D0 RID: 17104
		internal const bool Y_AXIS_AUTO_SHRINK = true;

		// Token: 0x040042D1 RID: 17105
		internal const bool Y_AXIS_IS_LOGARITHMIC = false;

		// Token: 0x040042D2 RID: 17106
		internal const double Y_AXIS_LOGARITHM_BASE = 10.0;

		// Token: 0x040042D3 RID: 17107
		internal const decimal AXIS_ITEM_VALUE = 0m;

		// Token: 0x040042D4 RID: 17108
		internal const string AXIS_ITEM_VALUE_STRING = "0";

		// Token: 0x040042D5 RID: 17109
		internal const bool SCALE_BREAK_ENABLED = false;

		// Token: 0x040042D6 RID: 17110
		internal const int SCALE_BREAK_MAX_COUNT = 1;

		// Token: 0x040042D7 RID: 17111
		internal const int SCALE_BREAK_WIDTH = 4;

		// Token: 0x040042D8 RID: 17112
		internal const byte SCALE_BREAK_VALUE_TOLERANCE = 25;

		// Token: 0x040042D9 RID: 17113
		internal const ScaleBreakLineType SCALE_BREAK_LINE_STYLE = ScaleBreakLineType.Sinusoid;

		// Token: 0x040042DA RID: 17114
		internal const double AXIS_SEGMENT_MIN_VALUE = 0.0;

		// Token: 0x040042DB RID: 17115
		internal const double AXIS_SEGMENT_MAX_VALUE = 100.0;

		// Token: 0x040042DC RID: 17116
		internal const double AXIS_SEGMENT_STEP = 10.0;

		// Token: 0x040042DD RID: 17117
		internal const PlacementDirection LABEL_DIRECTION = PlacementDirection.Vertical;

		// Token: 0x040042DE RID: 17118
		internal const bool LABEL_VISIBLE = true;

		// Token: 0x040042DF RID: 17119
		internal const bool LABEL_HIDDEN_VISIBLE = false;

		// Token: 0x040042E0 RID: 17120
		internal const float LABEL_STEP = 1f;

		// Token: 0x040042E1 RID: 17121
		internal const string LABEL_NAME = "";

		// Token: 0x040042E2 RID: 17122
		internal const ChartElementLocation LEGEND_LOCATION = ChartElementLocation.OutsidePlotArea;

		// Token: 0x040042E3 RID: 17123
		internal const AlignedPositions LEGEND_ITEM_POSITION = AlignedPositions.None;

		// Token: 0x040042E4 RID: 17124
		internal const bool LEGEND_ITEM_MARKER_VISIBILITY = true;

		// Token: 0x040042E5 RID: 17125
		internal const string SERIES_NAME = "Series xx";

		// Token: 0x040042E6 RID: 17126
		internal const string SERIES_LABEL_VALUE = "#Y";

		// Token: 0x040042E7 RID: 17127
		internal const string SERIES_ACTIVEREGION_VALUE = "";

		// Token: 0x040042E8 RID: 17128
		internal const bool SERIES_VISIBLE = true;

		// Token: 0x040042E9 RID: 17129
		internal const ChartSeriesType SERIES_TYPE = ChartSeriesType.Bar;

		// Token: 0x040042EA RID: 17130
		internal const string SERIES_DATACOLUMN = "";

		// Token: 0x040042EB RID: 17131
		internal const ChartYAxisType SERIES_AXIS_TYPE = ChartYAxisType.Primary;

		// Token: 0x040042EC RID: 17132
		internal static readonly Color[] defaultForeColors = new Color[]
		{
			Color.FromArgb(213, 247, 255),
			Color.FromArgb(218, 254, 122),
			Color.FromArgb(136, 221, 246),
			Color.FromArgb(163, 222, 78),
			Color.FromArgb(79, 152, 198),
			Color.FromArgb(86, 153, 78)
		};

		// Token: 0x040042ED RID: 17133
		internal static readonly Color[] defaultSecondColors = new Color[]
		{
			Color.FromArgb(157, 217, 238),
			Color.FromArgb(153, 205, 46),
			Color.FromArgb(59, 161, 197),
			Color.FromArgb(102, 181, 3),
			Color.FromArgb(4, 85, 156),
			Color.FromArgb(18, 96, 3)
		};

		// Token: 0x040042EE RID: 17134
		internal static Point LABEL_ITEM_CONNECTION_POINT;
	}
}
