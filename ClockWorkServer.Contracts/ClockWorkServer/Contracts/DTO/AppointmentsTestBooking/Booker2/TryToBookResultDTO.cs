using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.Booker2
{
	// Token: 0x02000A90 RID: 2704
	[DataContract(Namespace = "http://tpro.ca")]
	public class TryToBookResultDTO
	{
		// Token: 0x060038BF RID: 14527 RVA: 0x0001B897 File Offset: 0x00019A97
		public TryToBookResultDTO()
		{
			this.RoomIdsConsidered = new List<int>();
			this.AssetsRequiredAtSomePoint = new List<string>();
		}

		// Token: 0x170014AE RID: 5294
		// (get) Token: 0x060038C0 RID: 14528 RVA: 0x0001B8B9 File Offset: 0x00019AB9
		// (set) Token: 0x060038C1 RID: 14529 RVA: 0x0001B8C1 File Offset: 0x00019AC1
		[DataMember]
		public IList<TryToBookFailureDTO> Failures { get; set; }

		// Token: 0x170014AF RID: 5295
		// (get) Token: 0x060038C2 RID: 14530 RVA: 0x0001B8CA File Offset: 0x00019ACA
		// (set) Token: 0x060038C3 RID: 14531 RVA: 0x0001B8D2 File Offset: 0x00019AD2
		[DataMember]
		public IList<TryToBookWarningDTO> Warnings { get; set; }

		// Token: 0x170014B0 RID: 5296
		// (get) Token: 0x060038C4 RID: 14532 RVA: 0x0001B8DB File Offset: 0x00019ADB
		// (set) Token: 0x060038C5 RID: 14533 RVA: 0x0001B8E3 File Offset: 0x00019AE3
		[DataMember]
		public IList<TryToBookPotentialBookingDTO> PotentialBookings { get; set; }

		// Token: 0x170014B1 RID: 5297
		// (get) Token: 0x060038C6 RID: 14534 RVA: 0x0001B8EC File Offset: 0x00019AEC
		// (set) Token: 0x060038C7 RID: 14535 RVA: 0x0001B8F4 File Offset: 0x00019AF4
		[DataMember]
		public IList<string> Messages { get; set; }

		// Token: 0x170014B2 RID: 5298
		// (get) Token: 0x060038C8 RID: 14536 RVA: 0x0001B8FD File Offset: 0x00019AFD
		// (set) Token: 0x060038C9 RID: 14537 RVA: 0x0001B905 File Offset: 0x00019B05
		[DataMember]
		public IList<string> NoticesForAllPotentialBookings { get; set; }

		// Token: 0x170014B3 RID: 5299
		// (get) Token: 0x060038CA RID: 14538 RVA: 0x0001B90E File Offset: 0x00019B0E
		// (set) Token: 0x060038CB RID: 14539 RVA: 0x0001B916 File Offset: 0x00019B16
		[DataMember]
		public bool StudentIsDoubleBooked { get; set; }

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x060038CC RID: 14540 RVA: 0x0001B91F File Offset: 0x00019B1F
		// (set) Token: 0x060038CD RID: 14541 RVA: 0x0001B927 File Offset: 0x00019B27
		[DataMember]
		public bool StudentAlreadyHadAnotherTestBookedForSameDayAndCourse { get; set; }

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x060038CE RID: 14542 RVA: 0x0001B930 File Offset: 0x00019B30
		// (set) Token: 0x060038CF RID: 14543 RVA: 0x0001B938 File Offset: 0x00019B38
		[DataMember]
		public IList<int> RoomIdsConsidered { get; set; }

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x060038D0 RID: 14544 RVA: 0x0001B941 File Offset: 0x00019B41
		// (set) Token: 0x060038D1 RID: 14545 RVA: 0x0001B949 File Offset: 0x00019B49
		[DataMember]
		public IList<string> AssetsRequiredAtSomePoint { get; set; }

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x060038D2 RID: 14546 RVA: 0x0001B952 File Offset: 0x00019B52
		// (set) Token: 0x060038D3 RID: 14547 RVA: 0x0001B95A File Offset: 0x00019B5A
		[DataMember]
		public IList<int> IconIdsToBookWith { get; set; }

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x060038D4 RID: 14548 RVA: 0x0001B963 File Offset: 0x00019B63
		// (set) Token: 0x060038D5 RID: 14549 RVA: 0x0001B96B File Offset: 0x00019B6B
		[DataMember]
		public IList<int> AccommodationCidsForEmail { get; set; }

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x060038D6 RID: 14550 RVA: 0x0001B974 File Offset: 0x00019B74
		// (set) Token: 0x060038D7 RID: 14551 RVA: 0x0001B97C File Offset: 0x00019B7C
		[DataMember]
		public IList<DateTime> StartDateTimesNotUseableBecauseOfTimetableConflict { get; set; }

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x060038D8 RID: 14552 RVA: 0x0001B985 File Offset: 0x00019B85
		// (set) Token: 0x060038D9 RID: 14553 RVA: 0x0001B98D File Offset: 0x00019B8D
		[DataMember]
		public int AppliedBreakMinutes { get; set; }

		// Token: 0x170014BB RID: 5307
		// (get) Token: 0x060038DA RID: 14554 RVA: 0x0001B996 File Offset: 0x00019B96
		// (set) Token: 0x060038DB RID: 14555 RVA: 0x0001B99E File Offset: 0x00019B9E
		[DataMember]
		public IList<string> DebuggingLogItems { get; set; }
	}
}
