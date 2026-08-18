using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000E68 RID: 3688
	public interface ITimeZoneModel
	{
		// Token: 0x17002C3D RID: 11325
		// (get) Token: 0x06008BED RID: 35821
		// (set) Token: 0x06008BEE RID: 35822
		string TimeZoneId { get; set; }

		// Token: 0x17002C3E RID: 11326
		// (get) Token: 0x06008BEF RID: 35823
		// (set) Token: 0x06008BF0 RID: 35824
		string DisplayName { get; set; }

		// Token: 0x17002C3F RID: 11327
		// (get) Token: 0x06008BF1 RID: 35825
		// (set) Token: 0x06008BF2 RID: 35826
		string StandardName { get; set; }

		// Token: 0x17002C40 RID: 11328
		// (get) Token: 0x06008BF3 RID: 35827
		// (set) Token: 0x06008BF4 RID: 35828
		TimeSpan BaseUtcOffset { get; set; }

		// Token: 0x17002C41 RID: 11329
		// (get) Token: 0x06008BF5 RID: 35829
		// (set) Token: 0x06008BF6 RID: 35830
		bool SupportsDayLightSaving { get; set; }

		// Token: 0x17002C42 RID: 11330
		// (get) Token: 0x06008BF7 RID: 35831
		// (set) Token: 0x06008BF8 RID: 35832
		TimeZoneInfo.AdjustmentRule[] AdjustmentRules { get; set; }

		// Token: 0x06008BF9 RID: 35833
		TimeSpan GetUtcOffset(DateTime date);

		// Token: 0x06008BFA RID: 35834
		TimeSpan GetTransitionDelta(DateTime rangeStart, DateTime rangeEnd);

		// Token: 0x06008BFB RID: 35835
		bool IsTransitionFrame(DateTime start, DateTime end);

		// Token: 0x06008BFC RID: 35836
		bool IsUsingDayLightSaving(DateTime date);

		// Token: 0x06008BFD RID: 35837
		TimeZoneInfo.AdjustmentRule GetAdjustmentRuleForDate(DateTime date);
	}
}
