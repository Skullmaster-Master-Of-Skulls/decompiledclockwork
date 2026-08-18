using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerRecurrenceEditor
{
	// Token: 0x020007FD RID: 2045
	internal interface IRecurrenceEditorView
	{
		// Token: 0x170017CC RID: 6092
		// (get) Token: 0x06004988 RID: 18824
		RecurrenceEditor Owner { get; }

		// Token: 0x170017CD RID: 6093
		// (get) Token: 0x06004989 RID: 18825
		// (set) Token: 0x0600498A RID: 18826
		CheckBox RecurrenceCheckBox { get; set; }

		// Token: 0x170017CE RID: 6094
		// (get) Token: 0x0600498B RID: 18827
		bool IsRecurring { get; }

		// Token: 0x170017CF RID: 6095
		// (get) Token: 0x0600498C RID: 18828
		RecurrenceFrequency Frequency { get; }

		// Token: 0x170017D0 RID: 6096
		// (get) Token: 0x0600498D RID: 18829
		// (set) Token: 0x0600498E RID: 18830
		RecurrenceRangeType RangeType { get; set; }

		// Token: 0x0600498F RID: 18831
		void SetRecurrenceToggle(bool value);

		// Token: 0x06004990 RID: 18832
		void SetRecurrenceFrequency(RecurrenceFrequency frequency);

		// Token: 0x170017D1 RID: 6097
		// (get) Token: 0x06004991 RID: 18833
		// (set) Token: 0x06004992 RID: 18834
		RadioButton RepeatFrequencyHourly { get; set; }

		// Token: 0x170017D2 RID: 6098
		// (get) Token: 0x06004993 RID: 18835
		// (set) Token: 0x06004994 RID: 18836
		RadioButton RepeatFrequencyDaily { get; set; }

		// Token: 0x170017D3 RID: 6099
		// (get) Token: 0x06004995 RID: 18837
		// (set) Token: 0x06004996 RID: 18838
		RadioButton RepeatFrequencyWeekly { get; set; }

		// Token: 0x170017D4 RID: 6100
		// (get) Token: 0x06004997 RID: 18839
		// (set) Token: 0x06004998 RID: 18840
		RadioButton RepeatFrequencyMonthly { get; set; }

		// Token: 0x170017D5 RID: 6101
		// (get) Token: 0x06004999 RID: 18841
		// (set) Token: 0x0600499A RID: 18842
		RadioButton RepeatFrequencyYearly { get; set; }

		// Token: 0x170017D6 RID: 6102
		// (get) Token: 0x0600499B RID: 18843
		// (set) Token: 0x0600499C RID: 18844
		Control HourlyRepeatInterval { get; set; }

		// Token: 0x170017D7 RID: 6103
		// (get) Token: 0x0600499D RID: 18845
		// (set) Token: 0x0600499E RID: 18846
		RadioButton RepeatEveryNthDay { get; set; }

		// Token: 0x170017D8 RID: 6104
		// (get) Token: 0x0600499F RID: 18847
		// (set) Token: 0x060049A0 RID: 18848
		Control DailyRepeatInterval { get; set; }

		// Token: 0x170017D9 RID: 6105
		// (get) Token: 0x060049A1 RID: 18849
		// (set) Token: 0x060049A2 RID: 18850
		RadioButton RepeatEveryWeekday { get; set; }

		// Token: 0x170017DA RID: 6106
		// (get) Token: 0x060049A3 RID: 18851
		// (set) Token: 0x060049A4 RID: 18852
		Control WeeklyRepeatInterval { get; set; }

		// Token: 0x170017DB RID: 6107
		// (get) Token: 0x060049A5 RID: 18853
		// (set) Token: 0x060049A6 RID: 18854
		CheckBox WeeklyWeekDayMonday { get; set; }

		// Token: 0x170017DC RID: 6108
		// (get) Token: 0x060049A7 RID: 18855
		// (set) Token: 0x060049A8 RID: 18856
		CheckBox WeeklyWeekDayTuesday { get; set; }

		// Token: 0x170017DD RID: 6109
		// (get) Token: 0x060049A9 RID: 18857
		// (set) Token: 0x060049AA RID: 18858
		CheckBox WeeklyWeekDayWednesday { get; set; }

		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x060049AB RID: 18859
		// (set) Token: 0x060049AC RID: 18860
		CheckBox WeeklyWeekDayThursday { get; set; }

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x060049AD RID: 18861
		// (set) Token: 0x060049AE RID: 18862
		CheckBox WeeklyWeekDayFriday { get; set; }

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x060049AF RID: 18863
		// (set) Token: 0x060049B0 RID: 18864
		CheckBox WeeklyWeekDaySaturday { get; set; }

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x060049B1 RID: 18865
		// (set) Token: 0x060049B2 RID: 18866
		CheckBox WeeklyWeekDaySunday { get; set; }

		// Token: 0x170017E2 RID: 6114
		// (get) Token: 0x060049B3 RID: 18867
		// (set) Token: 0x060049B4 RID: 18868
		RadioButton RepeatEveryNthMonthOnDate { get; set; }

		// Token: 0x170017E3 RID: 6115
		// (get) Token: 0x060049B5 RID: 18869
		// (set) Token: 0x060049B6 RID: 18870
		Control MonthlyRepeatDate { get; set; }

		// Token: 0x170017E4 RID: 6116
		// (get) Token: 0x060049B7 RID: 18871
		// (set) Token: 0x060049B8 RID: 18872
		Control MonthlyRepeatIntervalForDate { get; set; }

		// Token: 0x170017E5 RID: 6117
		// (get) Token: 0x060049B9 RID: 18873
		// (set) Token: 0x060049BA RID: 18874
		RadioButton RepeatEveryNthMonthOnGivenDay { get; set; }

		// Token: 0x170017E6 RID: 6118
		// (get) Token: 0x060049BB RID: 18875
		// (set) Token: 0x060049BC RID: 18876
		DataBoundControl MonthlyDayOrdinalDropDown { get; set; }

		// Token: 0x170017E7 RID: 6119
		// (get) Token: 0x060049BD RID: 18877
		// (set) Token: 0x060049BE RID: 18878
		DataBoundControl MonthlyDayMaskDropDown { get; set; }

		// Token: 0x170017E8 RID: 6120
		// (get) Token: 0x060049BF RID: 18879
		// (set) Token: 0x060049C0 RID: 18880
		Control MonthlyRepeatIntervalForGivenDay { get; set; }

		// Token: 0x170017E9 RID: 6121
		// (get) Token: 0x060049C1 RID: 18881
		// (set) Token: 0x060049C2 RID: 18882
		Control YearlyRepeatInterval { get; set; }

		// Token: 0x170017EA RID: 6122
		// (get) Token: 0x060049C3 RID: 18883
		// (set) Token: 0x060049C4 RID: 18884
		RadioButton RepeatEveryYearOnDate { get; set; }

		// Token: 0x170017EB RID: 6123
		// (get) Token: 0x060049C5 RID: 18885
		// (set) Token: 0x060049C6 RID: 18886
		DataBoundControl YearlyRepeatMonthForDate { get; set; }

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x060049C7 RID: 18887
		// (set) Token: 0x060049C8 RID: 18888
		Control YearlyRepeatDate { get; set; }

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x060049C9 RID: 18889
		// (set) Token: 0x060049CA RID: 18890
		RadioButton RepeatEveryYearOnGivenDay { get; set; }

		// Token: 0x170017EE RID: 6126
		// (get) Token: 0x060049CB RID: 18891
		// (set) Token: 0x060049CC RID: 18892
		DataBoundControl YearlyDayOrdinalDropDown { get; set; }

		// Token: 0x170017EF RID: 6127
		// (get) Token: 0x060049CD RID: 18893
		// (set) Token: 0x060049CE RID: 18894
		DataBoundControl YearlyDayMaskDropDown { get; set; }

		// Token: 0x170017F0 RID: 6128
		// (get) Token: 0x060049CF RID: 18895
		// (set) Token: 0x060049D0 RID: 18896
		DataBoundControl YearlyRepeatMonthForGivenDay { get; set; }

		// Token: 0x170017F1 RID: 6129
		// (get) Token: 0x060049D1 RID: 18897
		// (set) Token: 0x060049D2 RID: 18898
		RadioButton RepeatIndefinitely { get; set; }

		// Token: 0x170017F2 RID: 6130
		// (get) Token: 0x060049D3 RID: 18899
		// (set) Token: 0x060049D4 RID: 18900
		RadioButton RepeatGivenOccurrences { get; set; }

		// Token: 0x170017F3 RID: 6131
		// (get) Token: 0x060049D5 RID: 18901
		// (set) Token: 0x060049D6 RID: 18902
		Control RangeOccurrences { get; set; }

		// Token: 0x170017F4 RID: 6132
		// (get) Token: 0x060049D7 RID: 18903
		// (set) Token: 0x060049D8 RID: 18904
		RadioButton RepeatUntilGivenDate { get; set; }

		// Token: 0x170017F5 RID: 6133
		// (get) Token: 0x060049D9 RID: 18905
		// (set) Token: 0x060049DA RID: 18906
		Control RangeEndDate { get; set; }

		// Token: 0x170017F6 RID: 6134
		// (get) Token: 0x060049DB RID: 18907
		// (set) Token: 0x060049DC RID: 18908
		string HourlyRepeatIntervalValue { get; set; }

		// Token: 0x170017F7 RID: 6135
		// (get) Token: 0x060049DD RID: 18909
		// (set) Token: 0x060049DE RID: 18910
		string DailyRepeatIntervalValue { get; set; }

		// Token: 0x170017F8 RID: 6136
		// (get) Token: 0x060049DF RID: 18911
		// (set) Token: 0x060049E0 RID: 18912
		string WeeklyRepeatIntervalValue { get; set; }

		// Token: 0x170017F9 RID: 6137
		// (get) Token: 0x060049E1 RID: 18913
		// (set) Token: 0x060049E2 RID: 18914
		string MonthlyRepeatDateValue { get; set; }

		// Token: 0x170017FA RID: 6138
		// (get) Token: 0x060049E3 RID: 18915
		// (set) Token: 0x060049E4 RID: 18916
		string MonthlyRepeatIntervalForDateValue { get; set; }

		// Token: 0x170017FB RID: 6139
		// (get) Token: 0x060049E5 RID: 18917
		// (set) Token: 0x060049E6 RID: 18918
		string MonthlyRepeatIntervalForGivenDayValue { get; set; }

		// Token: 0x170017FC RID: 6140
		// (get) Token: 0x060049E7 RID: 18919
		// (set) Token: 0x060049E8 RID: 18920
		string YearlyRepeatDateValue { get; set; }

		// Token: 0x170017FD RID: 6141
		// (get) Token: 0x060049E9 RID: 18921
		// (set) Token: 0x060049EA RID: 18922
		string RangeOccurrencesValue { get; set; }

		// Token: 0x170017FE RID: 6142
		// (get) Token: 0x060049EB RID: 18923
		// (set) Token: 0x060049EC RID: 18924
		string MonthlyDayOrdinalDropDownSelectedValue { get; set; }

		// Token: 0x170017FF RID: 6143
		// (get) Token: 0x060049ED RID: 18925
		string MonthlyDayMaskDropDownSelectedValue { get; }

		// Token: 0x17001800 RID: 6144
		// (set) Token: 0x060049EE RID: 18926
		int MonthlyDayMaskDropDownSelectedIndex { set; }

		// Token: 0x17001801 RID: 6145
		// (get) Token: 0x060049EF RID: 18927
		// (set) Token: 0x060049F0 RID: 18928
		string YearlyRepeatIntervalValue { get; set; }

		// Token: 0x17001802 RID: 6146
		// (get) Token: 0x060049F1 RID: 18929
		// (set) Token: 0x060049F2 RID: 18930
		string YearlyRepeatMonthForDateSelectedValue { get; set; }

		// Token: 0x17001803 RID: 6147
		// (set) Token: 0x060049F3 RID: 18931
		int YearlyRepeatMonthForDateSelectedIndex { set; }

		// Token: 0x17001804 RID: 6148
		// (get) Token: 0x060049F4 RID: 18932
		// (set) Token: 0x060049F5 RID: 18933
		string YearlyDayOrdinalDropDownSelectedValue { get; set; }

		// Token: 0x17001805 RID: 6149
		// (get) Token: 0x060049F6 RID: 18934
		string YearlyDayMaskDropDownSelectedValue { get; }

		// Token: 0x17001806 RID: 6150
		// (set) Token: 0x060049F7 RID: 18935
		int YearlyDayMaskDropDownSelectedIndex { set; }

		// Token: 0x17001807 RID: 6151
		// (get) Token: 0x060049F8 RID: 18936
		// (set) Token: 0x060049F9 RID: 18937
		string YearlyRepeatMonthForGivenDaySelectedValue { get; set; }

		// Token: 0x17001808 RID: 6152
		// (set) Token: 0x060049FA RID: 18938
		int YearlyRepeatMonthForGivenDaySelectedIndex { set; }

		// Token: 0x17001809 RID: 6153
		// (get) Token: 0x060049FB RID: 18939
		// (set) Token: 0x060049FC RID: 18940
		DateTime? RangeEndDateSelectedDate { get; set; }

		// Token: 0x060049FD RID: 18941
		void CreateControls();
	}
}
