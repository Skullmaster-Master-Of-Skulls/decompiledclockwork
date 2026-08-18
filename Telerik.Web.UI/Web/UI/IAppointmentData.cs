using System;
using System.Collections.Generic;

namespace Telerik.Web.UI
{
	// Token: 0x02001A08 RID: 6664
	public interface IAppointmentData
	{
		// Token: 0x17004DCF RID: 19919
		// (get) Token: 0x060101F0 RID: 66032
		// (set) Token: 0x060101F1 RID: 66033
		object ID { get; set; }

		// Token: 0x17004DD0 RID: 19920
		// (get) Token: 0x060101F2 RID: 66034
		// (set) Token: 0x060101F3 RID: 66035
		DateTime Start { get; set; }

		// Token: 0x17004DD1 RID: 19921
		// (get) Token: 0x060101F4 RID: 66036
		// (set) Token: 0x060101F5 RID: 66037
		DateTime End { get; set; }

		// Token: 0x17004DD2 RID: 19922
		// (get) Token: 0x060101F6 RID: 66038
		// (set) Token: 0x060101F7 RID: 66039
		string Subject { get; set; }

		// Token: 0x17004DD3 RID: 19923
		// (get) Token: 0x060101F8 RID: 66040
		// (set) Token: 0x060101F9 RID: 66041
		RecurrenceState RecurrenceState { get; set; }

		// Token: 0x17004DD4 RID: 19924
		// (get) Token: 0x060101FA RID: 66042
		// (set) Token: 0x060101FB RID: 66043
		object RecurrenceParentID { get; set; }

		// Token: 0x17004DD5 RID: 19925
		// (get) Token: 0x060101FC RID: 66044
		// (set) Token: 0x060101FD RID: 66045
		string RecurrenceRule { get; set; }

		// Token: 0x17004DD6 RID: 19926
		// (get) Token: 0x060101FE RID: 66046
		// (set) Token: 0x060101FF RID: 66047
		bool Visible { get; set; }

		// Token: 0x17004DD7 RID: 19927
		// (get) Token: 0x06010200 RID: 66048
		// (set) Token: 0x06010201 RID: 66049
		string EncodedID { get; set; }

		// Token: 0x17004DD8 RID: 19928
		// (get) Token: 0x06010202 RID: 66050
		// (set) Token: 0x06010203 RID: 66051
		string TimeZoneID { get; set; }

		// Token: 0x17004DD9 RID: 19929
		// (get) Token: 0x06010204 RID: 66052
		// (set) Token: 0x06010205 RID: 66053
		IList<ResourceData> Resources { get; set; }

		// Token: 0x17004DDA RID: 19930
		// (get) Token: 0x06010206 RID: 66054
		// (set) Token: 0x06010207 RID: 66055
		IDictionary<string, string> Attributes { get; set; }

		// Token: 0x17004DDB RID: 19931
		// (get) Token: 0x06010208 RID: 66056
		// (set) Token: 0x06010209 RID: 66057
		IList<ReminderData> Reminders { get; set; }

		// Token: 0x0601020A RID: 66058
		void CopyFrom(Appointment srcAppointment);

		// Token: 0x0601020B RID: 66059
		void CopyTo(Appointment destAppointment);
	}
}
