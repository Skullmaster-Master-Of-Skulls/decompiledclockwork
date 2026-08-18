using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009BA RID: 2490
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindPotentialBookings2Req : BaseMessageReq
	{
		// Token: 0x1700127E RID: 4734
		// (get) Token: 0x06003389 RID: 13193 RVA: 0x00019122 File Offset: 0x00017322
		// (set) Token: 0x0600338A RID: 13194 RVA: 0x0001912A File Offset: 0x0001732A
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x1700127F RID: 4735
		// (get) Token: 0x0600338B RID: 13195 RVA: 0x00019133 File Offset: 0x00017333
		// (set) Token: 0x0600338C RID: 13196 RVA: 0x0001913B File Offset: 0x0001733B
		[DataMember]
		public bool DebugMode { get; set; }

		// Token: 0x17001280 RID: 4736
		// (get) Token: 0x0600338D RID: 13197 RVA: 0x00019144 File Offset: 0x00017344
		// (set) Token: 0x0600338E RID: 13198 RVA: 0x0001914C File Offset: 0x0001734C
		[DataMember]
		public eAutoTestBookingContext TestBookingContext { get; set; }

		// Token: 0x17001281 RID: 4737
		// (get) Token: 0x0600338F RID: 13199 RVA: 0x00019155 File Offset: 0x00017355
		// (set) Token: 0x06003390 RID: 13200 RVA: 0x0001915D File Offset: 0x0001735D
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001282 RID: 4738
		// (get) Token: 0x06003391 RID: 13201 RVA: 0x00019166 File Offset: 0x00017366
		// (set) Token: 0x06003392 RID: 13202 RVA: 0x0001916E File Offset: 0x0001736E
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17001283 RID: 4739
		// (get) Token: 0x06003393 RID: 13203 RVA: 0x00019177 File Offset: 0x00017377
		// (set) Token: 0x06003394 RID: 13204 RVA: 0x0001917F File Offset: 0x0001737F
		[DataMember]
		public DateTime ClassStartDateTime { get; set; }

		// Token: 0x17001284 RID: 4740
		// (get) Token: 0x06003395 RID: 13205 RVA: 0x00019188 File Offset: 0x00017388
		// (set) Token: 0x06003396 RID: 13206 RVA: 0x00019190 File Offset: 0x00017390
		[DataMember]
		public DateTime ClassEndDateTime { get; set; }

		// Token: 0x17001285 RID: 4741
		// (get) Token: 0x06003397 RID: 13207 RVA: 0x00019199 File Offset: 0x00017399
		// (set) Token: 0x06003398 RID: 13208 RVA: 0x000191A1 File Offset: 0x000173A1
		[DataMember]
		public IList<AccommodationDTO> AccommodationsToUse { get; set; }

		// Token: 0x17001286 RID: 4742
		// (get) Token: 0x06003399 RID: 13209 RVA: 0x000191AA File Offset: 0x000173AA
		// (set) Token: 0x0600339A RID: 13210 RVA: 0x000191B2 File Offset: 0x000173B2
		[DataMember]
		public string OptionalClockWorkSettingsInstanceName { get; set; }

		// Token: 0x17001287 RID: 4743
		// (get) Token: 0x0600339B RID: 13211 RVA: 0x000191BB File Offset: 0x000173BB
		// (set) Token: 0x0600339C RID: 13212 RVA: 0x000191C3 File Offset: 0x000173C3
		[DataMember]
		public bool ClearCacheFirst { get; set; }
	}
}
