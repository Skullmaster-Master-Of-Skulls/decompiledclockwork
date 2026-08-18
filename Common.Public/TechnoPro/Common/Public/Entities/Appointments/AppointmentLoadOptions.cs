using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004AD RID: 1197
	public class AppointmentLoadOptions
	{
		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06002414 RID: 9236 RVA: 0x00027503 File Offset: 0x00025703
		// (set) Token: 0x06002415 RID: 9237 RVA: 0x0002750B File Offset: 0x0002570B
		public IList<int> PersonIds { get; set; }

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06002416 RID: 9238 RVA: 0x00027514 File Offset: 0x00025714
		// (set) Token: 0x06002417 RID: 9239 RVA: 0x0002751C File Offset: 0x0002571C
		public IList<int> AppointmentTypeIds { get; set; }

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06002418 RID: 9240 RVA: 0x00027525 File Offset: 0x00025725
		// (set) Token: 0x06002419 RID: 9241 RVA: 0x0002752D File Offset: 0x0002572D
		public bool HideCancelledAppointments { get; set; }

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x0600241A RID: 9242 RVA: 0x00027536 File Offset: 0x00025736
		// (set) Token: 0x0600241B RID: 9243 RVA: 0x0002753E File Offset: 0x0002573E
		public bool LoadPerStudentDataIcons { get; set; }

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x00027547 File Offset: 0x00025747
		// (set) Token: 0x0600241D RID: 9245 RVA: 0x0002754F File Offset: 0x0002574F
		public bool LoadPerAnonymousDataIcons { get; set; }

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x00027558 File Offset: 0x00025758
		// (set) Token: 0x0600241F RID: 9247 RVA: 0x00027560 File Offset: 0x00025760
		public DateTime StartDateTime { get; set; }

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x00027569 File Offset: 0x00025769
		// (set) Token: 0x06002421 RID: 9249 RVA: 0x00027571 File Offset: 0x00025771
		public DateTime EndDateTime { get; set; }

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06002422 RID: 9250 RVA: 0x0002757A File Offset: 0x0002577A
		// (set) Token: 0x06002423 RID: 9251 RVA: 0x00027582 File Offset: 0x00025782
		public IList<int> StudentPersonIdsForTimetableLoad { get; set; }

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06002424 RID: 9252 RVA: 0x0002758B File Offset: 0x0002578B
		// (set) Token: 0x06002425 RID: 9253 RVA: 0x00027593 File Offset: 0x00025793
		public bool LoadRecurringSchedule { get; set; }

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06002426 RID: 9254 RVA: 0x0002759C File Offset: 0x0002579C
		// (set) Token: 0x06002427 RID: 9255 RVA: 0x000275A4 File Offset: 0x000257A4
		public bool DontLoadHolidays { get; set; }

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06002428 RID: 9256 RVA: 0x000275AD File Offset: 0x000257AD
		// (set) Token: 0x06002429 RID: 9257 RVA: 0x000275B5 File Offset: 0x000257B5
		public IDictionary<int, IList<int>> AvailabilityGroupIdsByPersonId { get; set; }
	}
}
