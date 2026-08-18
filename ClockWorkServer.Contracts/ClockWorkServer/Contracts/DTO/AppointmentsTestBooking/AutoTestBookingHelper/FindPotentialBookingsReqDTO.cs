using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.AutoTestBookingHelper
{
	// Token: 0x02000A9F RID: 2719
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindPotentialBookingsReqDTO
	{
		// Token: 0x06003950 RID: 14672 RVA: 0x0001BD13 File Offset: 0x00019F13
		public FindPotentialBookingsReqDTO()
		{
			this.ApplySpecialAccommodationRules = true;
			this.TestBookingType = eTestExamSettingType.Midterm;
		}

		// Token: 0x170014EF RID: 5359
		// (get) Token: 0x06003951 RID: 14673 RVA: 0x0001BD2D File Offset: 0x00019F2D
		// (set) Token: 0x06003952 RID: 14674 RVA: 0x0001BD35 File Offset: 0x00019F35
		[DataMember]
		public bool DebugMode { get; set; }

		// Token: 0x170014F0 RID: 5360
		// (get) Token: 0x06003953 RID: 14675 RVA: 0x0001BD3E File Offset: 0x00019F3E
		// (set) Token: 0x06003954 RID: 14676 RVA: 0x0001BD46 File Offset: 0x00019F46
		[DataMember]
		public eAutoTestBookingContext TestBookingContext { get; set; }

		// Token: 0x170014F1 RID: 5361
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x0001BD4F File Offset: 0x00019F4F
		// (set) Token: 0x06003956 RID: 14678 RVA: 0x0001BD57 File Offset: 0x00019F57
		[DataMember]
		public int Pid { get; set; }

		// Token: 0x170014F2 RID: 5362
		// (get) Token: 0x06003957 RID: 14679 RVA: 0x0001BD60 File Offset: 0x00019F60
		// (set) Token: 0x06003958 RID: 14680 RVA: 0x0001BD68 File Offset: 0x00019F68
		[DataMember]
		public int Lucid { get; set; }

		// Token: 0x170014F3 RID: 5363
		// (get) Token: 0x06003959 RID: 14681 RVA: 0x0001BD71 File Offset: 0x00019F71
		// (set) Token: 0x0600395A RID: 14682 RVA: 0x0001BD79 File Offset: 0x00019F79
		[DataMember]
		public DateTime DayToLookIn { get; set; }

		// Token: 0x170014F4 RID: 5364
		// (get) Token: 0x0600395B RID: 14683 RVA: 0x0001BD82 File Offset: 0x00019F82
		// (set) Token: 0x0600395C RID: 14684 RVA: 0x0001BD8A File Offset: 0x00019F8A
		[DataMember]
		public TestDTO ClassTest { get; set; }

		// Token: 0x170014F5 RID: 5365
		// (get) Token: 0x0600395D RID: 14685 RVA: 0x0001BD93 File Offset: 0x00019F93
		// (set) Token: 0x0600395E RID: 14686 RVA: 0x0001BD9B File Offset: 0x00019F9B
		[DataMember]
		public IList<AccommodationDTO> Accommodations { get; set; }

		// Token: 0x170014F6 RID: 5366
		// (get) Token: 0x0600395F RID: 14687 RVA: 0x0001BDA4 File Offset: 0x00019FA4
		// (set) Token: 0x06003960 RID: 14688 RVA: 0x0001BDAC File Offset: 0x00019FAC
		[DataMember]
		public IList<AssetDTO> AvailableAssets { get; set; }

		// Token: 0x170014F7 RID: 5367
		// (get) Token: 0x06003961 RID: 14689 RVA: 0x0001BDB5 File Offset: 0x00019FB5
		// (set) Token: 0x06003962 RID: 14690 RVA: 0x0001BDBD File Offset: 0x00019FBD
		[DataMember]
		public IList<RoomDTO> AvailableRooms0 { get; set; }

		// Token: 0x170014F8 RID: 5368
		// (get) Token: 0x06003963 RID: 14691 RVA: 0x0001BDC6 File Offset: 0x00019FC6
		// (set) Token: 0x06003964 RID: 14692 RVA: 0x0001BDCE File Offset: 0x00019FCE
		[DataMember]
		public IList<SpecialAccommodationDTO> SpecialAccommodations { get; set; }

		// Token: 0x170014F9 RID: 5369
		// (get) Token: 0x06003965 RID: 14693 RVA: 0x0001BDD7 File Offset: 0x00019FD7
		// (set) Token: 0x06003966 RID: 14694 RVA: 0x0001BDDF File Offset: 0x00019FDF
		[DataMember]
		public IList<TestRuleDTO> Rules { get; set; }

		// Token: 0x170014FA RID: 5370
		// (get) Token: 0x06003967 RID: 14695 RVA: 0x0001BDE8 File Offset: 0x00019FE8
		// (set) Token: 0x06003968 RID: 14696 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		[DataMember]
		public int OverrideRoomAvailabilityPid { get; set; }

		// Token: 0x170014FB RID: 5371
		// (get) Token: 0x06003969 RID: 14697 RVA: 0x0001BDF9 File Offset: 0x00019FF9
		// (set) Token: 0x0600396A RID: 14698 RVA: 0x0001BE01 File Offset: 0x0001A001
		[DataMember]
		public IList<BookingDTO> UnavailableRoomBookings { get; set; }

		// Token: 0x170014FC RID: 5372
		// (get) Token: 0x0600396B RID: 14699 RVA: 0x0001BE0A File Offset: 0x0001A00A
		// (set) Token: 0x0600396C RID: 14700 RVA: 0x0001BE12 File Offset: 0x0001A012
		[DataMember]
		public bool LoadRoomSchedules { get; set; }

		// Token: 0x170014FD RID: 5373
		// (get) Token: 0x0600396D RID: 14701 RVA: 0x0001BE1B File Offset: 0x0001A01B
		// (set) Token: 0x0600396E RID: 14702 RVA: 0x0001BE23 File Offset: 0x0001A023
		[DataMember]
		public bool ApplySpecialAccommodationRules { get; set; }

		// Token: 0x170014FE RID: 5374
		// (get) Token: 0x0600396F RID: 14703 RVA: 0x0001BE2C File Offset: 0x0001A02C
		// (set) Token: 0x06003970 RID: 14704 RVA: 0x0001BE34 File Offset: 0x0001A034
		[DataMember]
		public int AppIdToIgnoreWhenCheckingStudentsSchedule { get; set; }

		// Token: 0x170014FF RID: 5375
		// (get) Token: 0x06003971 RID: 14705 RVA: 0x0001BE3D File Offset: 0x0001A03D
		// (set) Token: 0x06003972 RID: 14706 RVA: 0x0001BE45 File Offset: 0x0001A045
		[DataMember]
		public CustomTestBookingRulesClassDTO CustomTestBookingRules { get; set; }

		// Token: 0x17001500 RID: 5376
		// (get) Token: 0x06003973 RID: 14707 RVA: 0x0001BE4E File Offset: 0x0001A04E
		// (set) Token: 0x06003974 RID: 14708 RVA: 0x0001BE56 File Offset: 0x0001A056
		[DataMember]
		public bool IgnoreTimetable { get; set; }

		// Token: 0x17001501 RID: 5377
		// (get) Token: 0x06003975 RID: 14709 RVA: 0x0001BE5F File Offset: 0x0001A05F
		// (set) Token: 0x06003976 RID: 14710 RVA: 0x0001BE67 File Offset: 0x0001A067
		[DataMember]
		public bool RestrictByCampus { get; set; }

		// Token: 0x17001502 RID: 5378
		// (get) Token: 0x06003977 RID: 14711 RVA: 0x0001BE70 File Offset: 0x0001A070
		// (set) Token: 0x06003978 RID: 14712 RVA: 0x0001BE78 File Offset: 0x0001A078
		[DataMember]
		public int BufferMinutesPre { get; set; }

		// Token: 0x17001503 RID: 5379
		// (get) Token: 0x06003979 RID: 14713 RVA: 0x0001BE81 File Offset: 0x0001A081
		// (set) Token: 0x0600397A RID: 14714 RVA: 0x0001BE89 File Offset: 0x0001A089
		[DataMember]
		public int BufferMinutesPost { get; set; }

		// Token: 0x17001504 RID: 5380
		// (get) Token: 0x0600397B RID: 14715 RVA: 0x0001BE92 File Offset: 0x0001A092
		// (set) Token: 0x0600397C RID: 14716 RVA: 0x0001BE9A File Offset: 0x0001A09A
		[DataMember]
		public IList<int> IgnoreStudentAppointmentIds { get; set; }

		// Token: 0x17001505 RID: 5381
		// (get) Token: 0x0600397D RID: 14717 RVA: 0x0001BEA3 File Offset: 0x0001A0A3
		// (set) Token: 0x0600397E RID: 14718 RVA: 0x0001BEAB File Offset: 0x0001A0AB
		[DataMember]
		public bool IgnoreStudentsSchedule { get; set; }

		// Token: 0x17001506 RID: 5382
		// (get) Token: 0x0600397F RID: 14719 RVA: 0x0001BEB4 File Offset: 0x0001A0B4
		// (set) Token: 0x06003980 RID: 14720 RVA: 0x0001BEBC File Offset: 0x0001A0BC
		[DataMember]
		public bool IgnoreTwoTestsSameCourseSameDay { get; set; }

		// Token: 0x17001507 RID: 5383
		// (get) Token: 0x06003981 RID: 14721 RVA: 0x0001BEC5 File Offset: 0x0001A0C5
		// (set) Token: 0x06003982 RID: 14722 RVA: 0x0001BECD File Offset: 0x0001A0CD
		[DataMember]
		public eTestExamSettingType TestBookingType { get; set; }
	}
}
