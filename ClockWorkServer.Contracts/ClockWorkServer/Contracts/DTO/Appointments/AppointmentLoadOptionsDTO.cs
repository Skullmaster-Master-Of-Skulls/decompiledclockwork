using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments
{
	// Token: 0x0200092B RID: 2347
	[DataContract(Namespace = "http://tpro.ca")]
	public class AppointmentLoadOptionsDTO
	{
		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06002FA0 RID: 12192 RVA: 0x00016C70 File Offset: 0x00014E70
		// (set) Token: 0x06002FA1 RID: 12193 RVA: 0x00016C78 File Offset: 0x00014E78
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06002FA2 RID: 12194 RVA: 0x00016C81 File Offset: 0x00014E81
		// (set) Token: 0x06002FA3 RID: 12195 RVA: 0x00016C89 File Offset: 0x00014E89
		[DataMember]
		public IList<int> AppointmentTypeIds { get; set; }

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x00016C92 File Offset: 0x00014E92
		// (set) Token: 0x06002FA5 RID: 12197 RVA: 0x00016C9A File Offset: 0x00014E9A
		[DataMember]
		public bool HideCancelledAppointments { get; set; }

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x00016CA3 File Offset: 0x00014EA3
		// (set) Token: 0x06002FA7 RID: 12199 RVA: 0x00016CAB File Offset: 0x00014EAB
		[DataMember]
		public bool LoadPerStudentDataIcons { get; set; }

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06002FA8 RID: 12200 RVA: 0x00016CB4 File Offset: 0x00014EB4
		// (set) Token: 0x06002FA9 RID: 12201 RVA: 0x00016CBC File Offset: 0x00014EBC
		[DataMember]
		public bool LoadPerAnonymousDataIcons { get; set; }

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x06002FAA RID: 12202 RVA: 0x00016CC5 File Offset: 0x00014EC5
		// (set) Token: 0x06002FAB RID: 12203 RVA: 0x00016CCD File Offset: 0x00014ECD
		[DataMember]
		public DateTime StartDateTime { get; set; }

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06002FAC RID: 12204 RVA: 0x00016CD6 File Offset: 0x00014ED6
		// (set) Token: 0x06002FAD RID: 12205 RVA: 0x00016CDE File Offset: 0x00014EDE
		[DataMember]
		public DateTime EndDateTime { get; set; }

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06002FAE RID: 12206 RVA: 0x00016CE7 File Offset: 0x00014EE7
		// (set) Token: 0x06002FAF RID: 12207 RVA: 0x00016CEF File Offset: 0x00014EEF
		[DataMember]
		public IList<int> StudentPersonIdsForTimetableLoad { get; set; }

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x00016CF8 File Offset: 0x00014EF8
		// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x00016D00 File Offset: 0x00014F00
		[DataMember]
		public bool LoadRecurringSchedule { get; set; }

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x00016D09 File Offset: 0x00014F09
		// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x00016D11 File Offset: 0x00014F11
		[DataMember]
		public bool DontLoadHolidays { get; set; }

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x00016D1A File Offset: 0x00014F1A
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x00016D22 File Offset: 0x00014F22
		[DataMember]
		public IDictionary<int, IList<int>> AvailabilityGroupIdsByPersonId { get; set; }
	}
}
