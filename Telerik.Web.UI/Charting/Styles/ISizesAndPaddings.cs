using System;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001778 RID: 6008
	internal interface ISizesAndPaddings
	{
		// Token: 0x17004700 RID: 18176
		// (get) Token: 0x0600EA4E RID: 59982
		// (set) Token: 0x0600EA4F RID: 59983
		bool AutoSize { get; set; }

		// Token: 0x17004701 RID: 18177
		// (get) Token: 0x0600EA50 RID: 59984
		// (set) Token: 0x0600EA51 RID: 59985
		Unit Height { get; set; }

		// Token: 0x17004702 RID: 18178
		// (get) Token: 0x0600EA52 RID: 59986
		// (set) Token: 0x0600EA53 RID: 59987
		Unit Width { get; set; }

		// Token: 0x17004703 RID: 18179
		// (get) Token: 0x0600EA54 RID: 59988
		// (set) Token: 0x0600EA55 RID: 59989
		ChartMargins Margins { get; set; }

		// Token: 0x17004704 RID: 18180
		// (get) Token: 0x0600EA56 RID: 59990
		// (set) Token: 0x0600EA57 RID: 59991
		ChartPaddings Paddings { get; set; }
	}
}
