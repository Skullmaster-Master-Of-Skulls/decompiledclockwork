using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020007EE RID: 2030
	internal interface IScheduler : IAppointmentFactory
	{
		// Token: 0x170016AE RID: 5806
		// (get) Token: 0x06004669 RID: 18025
		// (set) Token: 0x0600466A RID: 18026
		DateTime SelectedDate { get; set; }

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x0600466B RID: 18027
		ResourceCollection Resources { get; }

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x0600466C RID: 18028
		// (set) Token: 0x0600466D RID: 18029
		string ActiveSlotIndex { get; set; }

		// Token: 0x170016B1 RID: 5809
		// (get) Token: 0x0600466E RID: 18030
		DayOfWeek FirstDayOfWeek { get; }

		// Token: 0x170016B2 RID: 5810
		// (get) Token: 0x0600466F RID: 18031
		DayOfWeek LastDayOfWeek { get; }

		// Token: 0x170016B3 RID: 5811
		// (get) Token: 0x06004670 RID: 18032
		TimeSpan DayStartTime { get; }

		// Token: 0x170016B4 RID: 5812
		// (get) Token: 0x06004671 RID: 18033
		TimeSpan DayEndTime { get; }

		// Token: 0x170016B5 RID: 5813
		// (get) Token: 0x06004672 RID: 18034
		TimeSpan WorkDayStartTime { get; }

		// Token: 0x170016B6 RID: 5814
		// (get) Token: 0x06004673 RID: 18035
		TimeSpan WorkDayEndTime { get; }

		// Token: 0x170016B7 RID: 5815
		// (get) Token: 0x06004674 RID: 18036
		RenderMode ResolvedRenderMode { get; }

		// Token: 0x170016B8 RID: 5816
		// (get) Token: 0x06004675 RID: 18037
		TimeZoneProviderBase TimeZonesProvider { get; }

		// Token: 0x170016B9 RID: 5817
		// (get) Token: 0x06004676 RID: 18038
		int MinutesPerRow { get; }

		// Token: 0x170016BA RID: 5818
		// (get) Token: 0x06004677 RID: 18039
		int NumberOfHoveredRows { get; }

		// Token: 0x170016BB RID: 5819
		// (get) Token: 0x06004678 RID: 18040
		bool ReadOnly { get; }

		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06004679 RID: 18041
		bool ShowAllDayRow { get; }

		// Token: 0x170016BD RID: 5821
		// (get) Token: 0x0600467A RID: 18042
		// (set) Token: 0x0600467B RID: 18043
		bool ShowFullTime { get; set; }

		// Token: 0x170016BE RID: 5822
		// (get) Token: 0x0600467C RID: 18044
		bool ShowDateHeaders { get; }

		// Token: 0x170016BF RID: 5823
		// (get) Token: 0x0600467D RID: 18045
		bool ShowResourceHeaders { get; }

		// Token: 0x170016C0 RID: 5824
		// (get) Token: 0x0600467E RID: 18046
		bool ShowHoursColumn { get; }

		// Token: 0x170016C1 RID: 5825
		// (get) Token: 0x0600467F RID: 18047
		bool EnableAdvancedForm { get; }

		// Token: 0x170016C2 RID: 5826
		// (get) Token: 0x06004680 RID: 18048
		bool RecurrenceSupport { get; }

		// Token: 0x170016C3 RID: 5827
		// (get) Token: 0x06004681 RID: 18049
		bool EnableExactTimeRendering { get; }

		// Token: 0x170016C4 RID: 5828
		// (get) Token: 0x06004682 RID: 18050
		bool TimeZonesEnabled { get; }

		// Token: 0x170016C5 RID: 5829
		// (get) Token: 0x06004683 RID: 18051
		bool AllowInsert { get; }

		// Token: 0x170016C6 RID: 5830
		// (get) Token: 0x06004684 RID: 18052
		bool UsingWebServiceBinding { get; }

		// Token: 0x170016C7 RID: 5831
		// (get) Token: 0x06004685 RID: 18053
		// (set) Token: 0x06004686 RID: 18054
		string GroupBy { get; set; }

		// Token: 0x170016C8 RID: 5832
		// (get) Token: 0x06004687 RID: 18055
		GroupingDirection GroupingDirection { get; }

		// Token: 0x170016C9 RID: 5833
		// (get) Token: 0x06004688 RID: 18056
		TimelineViewSettings TimelineView { get; }

		// Token: 0x170016CA RID: 5834
		// (get) Token: 0x06004689 RID: 18057
		WeekViewSettings WeekView { get; }

		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x0600468A RID: 18058
		DayViewSettings DayView { get; }

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x0600468B RID: 18059
		MultiDayViewSettings MultiDayView { get; }

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x0600468C RID: 18060
		MonthViewSettings MonthView { get; }

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x0600468D RID: 18061
		AgendaViewSettings AgendaView { get; }

		// Token: 0x170016CF RID: 5839
		// (get) Token: 0x0600468E RID: 18062
		YearViewSettings YearView { get; }

		// Token: 0x0600468F RID: 18063
		void HandleMove(Appointment appointmentToMove, DateTime start, DateTime end, bool editSeries, ResourceUpdateInfo resourceUpdateInfo);

		// Token: 0x06004690 RID: 18064
		void HandleInsert(Appointment appointmentToInsert);

		// Token: 0x06004691 RID: 18065
		void HandleResize(Appointment appointmentToResize, DateTime appointmentStart, DateTime appointmentEnd, bool editSeries);

		// Token: 0x06004692 RID: 18066
		void NotifyDataPropertyChanged();

		// Token: 0x06004693 RID: 18067
		DateTime DisplayToUtc(DateTime date);

		// Token: 0x06004694 RID: 18068
		DateTime UtcToDisplay(DateTime date);

		// Token: 0x06004695 RID: 18069
		DateTime UtcDayStart(DateTime date);

		// Token: 0x170016D0 RID: 5840
		// (get) Token: 0x06004696 RID: 18070
		IComparer<Appointment> AppointmentComparer { get; }

		// Token: 0x170016D1 RID: 5841
		// (get) Token: 0x06004697 RID: 18071
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		SchedulerStrings Localization { get; }
	}
}
