using System;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor
{
	// Token: 0x020007F6 RID: 2038
	internal interface IRecurrenceEditorRenderer
	{
		// Token: 0x170017B8 RID: 6072
		// (get) Token: 0x0600491F RID: 18719
		IRecurrenceEditorView View { get; }

		// Token: 0x170017B9 RID: 6073
		// (get) Token: 0x06004920 RID: 18720
		// (set) Token: 0x06004921 RID: 18721
		Panel RecurrenceCheckBoxPanel { get; set; }

		// Token: 0x170017BA RID: 6074
		// (get) Token: 0x06004922 RID: 18722
		// (set) Token: 0x06004923 RID: 18723
		Panel RecurrencePatternPanel { get; set; }

		// Token: 0x170017BB RID: 6075
		// (get) Token: 0x06004924 RID: 18724
		// (set) Token: 0x06004925 RID: 18725
		Panel RecurrencePatternHourlyPanel { get; set; }

		// Token: 0x170017BC RID: 6076
		// (get) Token: 0x06004926 RID: 18726
		// (set) Token: 0x06004927 RID: 18727
		Panel RecurrencePatternDailyPanel { get; set; }

		// Token: 0x170017BD RID: 6077
		// (get) Token: 0x06004928 RID: 18728
		// (set) Token: 0x06004929 RID: 18729
		Panel RecurrencePatternWeeklyPanel { get; set; }

		// Token: 0x170017BE RID: 6078
		// (get) Token: 0x0600492A RID: 18730
		// (set) Token: 0x0600492B RID: 18731
		Panel RecurrencePatternMonthlyPanel { get; set; }

		// Token: 0x170017BF RID: 6079
		// (get) Token: 0x0600492C RID: 18732
		// (set) Token: 0x0600492D RID: 18733
		Panel RecurrencePatternYearlyPanel { get; set; }

		// Token: 0x170017C0 RID: 6080
		// (get) Token: 0x0600492E RID: 18734
		// (set) Token: 0x0600492F RID: 18735
		Panel RangePanel { get; set; }

		// Token: 0x06004930 RID: 18736
		void CreateLayout(WebControl container, bool designMode);

		// Token: 0x06004931 RID: 18737
		void CreateControls();
	}
}
