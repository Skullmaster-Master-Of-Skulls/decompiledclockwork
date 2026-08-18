using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009CF RID: 2511
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToFindBookingReq : BaseMessageReq
	{
		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x0600340C RID: 13324 RVA: 0x000194C9 File Offset: 0x000176C9
		// (set) Token: 0x0600340D RID: 13325 RVA: 0x000194D1 File Offset: 0x000176D1
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x170012B6 RID: 4790
		// (get) Token: 0x0600340E RID: 13326 RVA: 0x000194DA File Offset: 0x000176DA
		// (set) Token: 0x0600340F RID: 13327 RVA: 0x000194E2 File Offset: 0x000176E2
		[DataMember]
		public bool StaffIsBooking { get; set; }

		// Token: 0x170012B7 RID: 4791
		// (get) Token: 0x06003410 RID: 13328 RVA: 0x000194EB File Offset: 0x000176EB
		// (set) Token: 0x06003411 RID: 13329 RVA: 0x000194F3 File Offset: 0x000176F3
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170012B8 RID: 4792
		// (get) Token: 0x06003412 RID: 13330 RVA: 0x000194FC File Offset: 0x000176FC
		// (set) Token: 0x06003413 RID: 13331 RVA: 0x00019504 File Offset: 0x00017704
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x170012B9 RID: 4793
		// (get) Token: 0x06003414 RID: 13332 RVA: 0x0001950D File Offset: 0x0001770D
		// (set) Token: 0x06003415 RID: 13333 RVA: 0x00019515 File Offset: 0x00017715
		[DataMember]
		public DateTime ClassStartDateTime { get; set; }

		// Token: 0x170012BA RID: 4794
		// (get) Token: 0x06003416 RID: 13334 RVA: 0x0001951E File Offset: 0x0001771E
		// (set) Token: 0x06003417 RID: 13335 RVA: 0x00019526 File Offset: 0x00017726
		[DataMember]
		public int ClassTestDurationInMinutes { get; set; }

		// Token: 0x170012BB RID: 4795
		// (get) Token: 0x06003418 RID: 13336 RVA: 0x0001952F File Offset: 0x0001772F
		// (set) Token: 0x06003419 RID: 13337 RVA: 0x00019537 File Offset: 0x00017737
		[DataMember]
		public IList<int> AccommodationsToUse { get; set; }

		// Token: 0x170012BC RID: 4796
		// (get) Token: 0x0600341A RID: 13338 RVA: 0x00019540 File Offset: 0x00017740
		// (set) Token: 0x0600341B RID: 13339 RVA: 0x00019548 File Offset: 0x00017748
		[DataMember]
		public bool ClearCacheFirst { get; set; }

		// Token: 0x170012BD RID: 4797
		// (get) Token: 0x0600341C RID: 13340 RVA: 0x00019551 File Offset: 0x00017751
		// (set) Token: 0x0600341D RID: 13341 RVA: 0x00019559 File Offset: 0x00017759
		[DataMember]
		public string ClockWorkInstanceNameToUse { get; set; }

		// Token: 0x170012BE RID: 4798
		// (get) Token: 0x0600341E RID: 13342 RVA: 0x00019562 File Offset: 0x00017762
		// (set) Token: 0x0600341F RID: 13343 RVA: 0x0001956A File Offset: 0x0001776A
		[DataMember]
		public bool IgnoreSpecialAccommodations { get; set; }

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x06003420 RID: 13344 RVA: 0x00019573 File Offset: 0x00017773
		// (set) Token: 0x06003421 RID: 13345 RVA: 0x0001957B File Offset: 0x0001777B
		[DataMember]
		public int BookingAlreadyExistsAppointmentId { get; set; }

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x06003422 RID: 13346 RVA: 0x00019584 File Offset: 0x00017784
		// (set) Token: 0x06003423 RID: 13347 RVA: 0x0001958C File Offset: 0x0001778C
		[DataMember]
		public IList<TryToBookAccommodationToUseDTO> AdditionalAccommodationsToUse { get; set; }
	}
}
