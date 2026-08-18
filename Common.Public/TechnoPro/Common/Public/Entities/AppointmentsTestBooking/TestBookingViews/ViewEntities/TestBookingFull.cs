using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x02000525 RID: 1317
	public class TestBookingFull : TestBookingSmall
	{
		// Token: 0x17001117 RID: 4375
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x0002A48C File Offset: 0x0002868C
		// (set) Token: 0x060028D4 RID: 10452 RVA: 0x0002A494 File Offset: 0x00028694
		public int InvigilatorPid { get; set; }

		// Token: 0x17001118 RID: 4376
		// (get) Token: 0x060028D5 RID: 10453 RVA: 0x0002A49D File Offset: 0x0002869D
		// (set) Token: 0x060028D6 RID: 10454 RVA: 0x0002A4A5 File Offset: 0x000286A5
		public int SittingId { get; set; }

		// Token: 0x17001119 RID: 4377
		// (get) Token: 0x060028D7 RID: 10455 RVA: 0x0002A4AE File Offset: 0x000286AE
		// (set) Token: 0x060028D8 RID: 10456 RVA: 0x0002A4B6 File Offset: 0x000286B6
		public int AlternateContactId { get; set; }

		// Token: 0x1700111A RID: 4378
		// (get) Token: 0x060028D9 RID: 10457 RVA: 0x0002A4BF File Offset: 0x000286BF
		// (set) Token: 0x060028DA RID: 10458 RVA: 0x0002A4C7 File Offset: 0x000286C7
		public bool InstructorSubmitted { get; set; }

		// Token: 0x1700111B RID: 4379
		// (get) Token: 0x060028DB RID: 10459 RVA: 0x0002A4D0 File Offset: 0x000286D0
		// (set) Token: 0x060028DC RID: 10460 RVA: 0x0002A4D8 File Offset: 0x000286D8
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x1700111C RID: 4380
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x0002A4E1 File Offset: 0x000286E1
		// (set) Token: 0x060028DE RID: 10462 RVA: 0x0002A4E9 File Offset: 0x000286E9
		public DateTime? CourseStartDate { get; set; }

		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x060028DF RID: 10463 RVA: 0x0002A4F2 File Offset: 0x000286F2
		// (set) Token: 0x060028E0 RID: 10464 RVA: 0x0002A4FA File Offset: 0x000286FA
		public DateTime? CourseEndDate { get; set; }

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x060028E1 RID: 10465 RVA: 0x0002A503 File Offset: 0x00028703
		// (set) Token: 0x060028E2 RID: 10466 RVA: 0x0002A50B File Offset: 0x0002870B
		public string Department { get; set; }

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x060028E3 RID: 10467 RVA: 0x0002A514 File Offset: 0x00028714
		// (set) Token: 0x060028E4 RID: 10468 RVA: 0x0002A51C File Offset: 0x0002871C
		public string DepartmentEmail { get; set; }

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x060028E5 RID: 10469 RVA: 0x0002A525 File Offset: 0x00028725
		// (set) Token: 0x060028E6 RID: 10470 RVA: 0x0002A52D File Offset: 0x0002872D
		public string DepartmentCode { get; set; }

		// Token: 0x17001121 RID: 4385
		// (get) Token: 0x060028E7 RID: 10471 RVA: 0x0002A536 File Offset: 0x00028736
		// (set) Token: 0x060028E8 RID: 10472 RVA: 0x0002A53E File Offset: 0x0002873E
		public string PrimaryInstructor { get; set; }

		// Token: 0x17001122 RID: 4386
		// (get) Token: 0x060028E9 RID: 10473 RVA: 0x0002A547 File Offset: 0x00028747
		// (set) Token: 0x060028EA RID: 10474 RVA: 0x0002A54F File Offset: 0x0002874F
		public string PrimaryInstructorEmail { get; set; }

		// Token: 0x17001123 RID: 4387
		// (get) Token: 0x060028EB RID: 10475 RVA: 0x0002A558 File Offset: 0x00028758
		// (set) Token: 0x060028EC RID: 10476 RVA: 0x0002A560 File Offset: 0x00028760
		public string PrimaryInstructorPhone { get; set; }

		// Token: 0x17001124 RID: 4388
		// (get) Token: 0x060028ED RID: 10477 RVA: 0x0002A569 File Offset: 0x00028769
		// (set) Token: 0x060028EE RID: 10478 RVA: 0x0002A571 File Offset: 0x00028771
		public string ExamAccommodations { get; set; }

		// Token: 0x17001125 RID: 4389
		// (get) Token: 0x060028EF RID: 10479 RVA: 0x0002A57A File Offset: 0x0002877A
		// (set) Token: 0x060028F0 RID: 10480 RVA: 0x0002A582 File Offset: 0x00028782
		public string AccommodationGroups { get; set; }

		// Token: 0x17001126 RID: 4390
		// (get) Token: 0x060028F1 RID: 10481 RVA: 0x0002A58B File Offset: 0x0002878B
		// (set) Token: 0x060028F2 RID: 10482 RVA: 0x0002A593 File Offset: 0x00028793
		public int TotalBreakMinutes { get; set; }

		// Token: 0x17001127 RID: 4391
		// (get) Token: 0x060028F3 RID: 10483 RVA: 0x0002A59C File Offset: 0x0002879C
		// (set) Token: 0x060028F4 RID: 10484 RVA: 0x0002A5A4 File Offset: 0x000287A4
		public string AssignedAdvisor { get; set; }

		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x060028F5 RID: 10485 RVA: 0x0002A5AD File Offset: 0x000287AD
		// (set) Token: 0x060028F6 RID: 10486 RVA: 0x0002A5B5 File Offset: 0x000287B5
		public string AssignedAdvisorFirstName { get; set; }

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x060028F7 RID: 10487 RVA: 0x0002A5BE File Offset: 0x000287BE
		// (set) Token: 0x060028F8 RID: 10488 RVA: 0x0002A5C6 File Offset: 0x000287C6
		public string AssignedAdvisorLastName { get; set; }

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x060028F9 RID: 10489 RVA: 0x0002A5CF File Offset: 0x000287CF
		// (set) Token: 0x060028FA RID: 10490 RVA: 0x0002A5D7 File Offset: 0x000287D7
		public string Invigilator { get; set; }

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x060028FB RID: 10491 RVA: 0x0002A5E0 File Offset: 0x000287E0
		// (set) Token: 0x060028FC RID: 10492 RVA: 0x0002A5E8 File Offset: 0x000287E8
		public string InvigilatorFirstName { get; set; }

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060028FD RID: 10493 RVA: 0x0002A5F1 File Offset: 0x000287F1
		// (set) Token: 0x060028FE RID: 10494 RVA: 0x0002A5F9 File Offset: 0x000287F9
		public string InvigilatorLastName { get; set; }

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x0002A602 File Offset: 0x00028802
		// (set) Token: 0x06002900 RID: 10496 RVA: 0x0002A60A File Offset: 0x0002880A
		public DateTime? DateAdded { get; set; }

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x06002901 RID: 10497 RVA: 0x0002A613 File Offset: 0x00028813
		// (set) Token: 0x06002902 RID: 10498 RVA: 0x0002A61B File Offset: 0x0002881B
		public string WhoBookedFirst { get; set; }

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x06002903 RID: 10499 RVA: 0x0002A624 File Offset: 0x00028824
		// (set) Token: 0x06002904 RID: 10500 RVA: 0x0002A62C File Offset: 0x0002882C
		public string WhoBookedLast { get; set; }

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x06002905 RID: 10501 RVA: 0x0002A635 File Offset: 0x00028835
		// (set) Token: 0x06002906 RID: 10502 RVA: 0x0002A63D File Offset: 0x0002883D
		public string WhoBooked { get; set; }

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x06002907 RID: 10503 RVA: 0x0002A646 File Offset: 0x00028846
		// (set) Token: 0x06002908 RID: 10504 RVA: 0x0002A64E File Offset: 0x0002884E
		public DateTime? StudentReportedClassDate { get; set; }

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x06002909 RID: 10505 RVA: 0x0002A657 File Offset: 0x00028857
		// (set) Token: 0x0600290A RID: 10506 RVA: 0x0002A65F File Offset: 0x0002885F
		public DateTime? StudentReportedClassStartTime { get; set; }

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x0600290B RID: 10507 RVA: 0x0002A668 File Offset: 0x00028868
		// (set) Token: 0x0600290C RID: 10508 RVA: 0x0002A670 File Offset: 0x00028870
		public DateTime? StudentReportedClassEndTime { get; set; }

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x0600290D RID: 10509 RVA: 0x0002A679 File Offset: 0x00028879
		// (set) Token: 0x0600290E RID: 10510 RVA: 0x0002A681 File Offset: 0x00028881
		public string AlternateContact { get; set; }

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x0600290F RID: 10511 RVA: 0x0002A68A File Offset: 0x0002888A
		// (set) Token: 0x06002910 RID: 10512 RVA: 0x0002A692 File Offset: 0x00028892
		public string AlternateContactEmail { get; set; }

		// Token: 0x17001136 RID: 4406
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x0002A69B File Offset: 0x0002889B
		// (set) Token: 0x06002912 RID: 10514 RVA: 0x0002A6A3 File Offset: 0x000288A3
		public string AlternateContactPhone { get; set; }

		// Token: 0x17001137 RID: 4407
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x0002A6AC File Offset: 0x000288AC
		// (set) Token: 0x06002914 RID: 10516 RVA: 0x0002A6B4 File Offset: 0x000288B4
		public string AlternateContactUsername { get; set; }

		// Token: 0x17001138 RID: 4408
		// (get) Token: 0x06002915 RID: 10517 RVA: 0x0002A6BD File Offset: 0x000288BD
		// (set) Token: 0x06002916 RID: 10518 RVA: 0x0002A6C5 File Offset: 0x000288C5
		public int AlternateContactPermissionLevel { get; set; }

		// Token: 0x17001139 RID: 4409
		// (get) Token: 0x06002917 RID: 10519 RVA: 0x0002A6CE File Offset: 0x000288CE
		// (set) Token: 0x06002918 RID: 10520 RVA: 0x0002A6D6 File Offset: 0x000288D6
		public string InstructorAcknowledgedOnline { get; set; }

		// Token: 0x1700113A RID: 4410
		// (get) Token: 0x06002919 RID: 10521 RVA: 0x0002A6DF File Offset: 0x000288DF
		// (set) Token: 0x0600291A RID: 10522 RVA: 0x0002A6E7 File Offset: 0x000288E7
		public DateTime? InstructorAcknowledgedDate { get; set; }

		// Token: 0x1700113B RID: 4411
		// (get) Token: 0x0600291B RID: 10523 RVA: 0x0002A6F0 File Offset: 0x000288F0
		// (set) Token: 0x0600291C RID: 10524 RVA: 0x0002A6F8 File Offset: 0x000288F8
		public bool StudentReportedSameAsDefinition { get; set; }

		// Token: 0x1700113C RID: 4412
		// (get) Token: 0x0600291D RID: 10525 RVA: 0x0002A701 File Offset: 0x00028901
		// (set) Token: 0x0600291E RID: 10526 RVA: 0x0002A709 File Offset: 0x00028909
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x0600291F RID: 10527 RVA: 0x0002A712 File Offset: 0x00028912
		// (set) Token: 0x06002920 RID: 10528 RVA: 0x0002A71A File Offset: 0x0002891A
		public string InstructorContactedNote { get; set; }

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06002921 RID: 10529 RVA: 0x0002A723 File Offset: 0x00028923
		// (set) Token: 0x06002922 RID: 10530 RVA: 0x0002A72B File Offset: 0x0002892B
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06002923 RID: 10531 RVA: 0x0002A734 File Offset: 0x00028934
		// (set) Token: 0x06002924 RID: 10532 RVA: 0x0002A73C File Offset: 0x0002893C
		public string TestPickedUpNote { get; set; }

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06002925 RID: 10533 RVA: 0x0002A745 File Offset: 0x00028945
		// (set) Token: 0x06002926 RID: 10534 RVA: 0x0002A74D File Offset: 0x0002894D
		public string PrivateNote2 { get; set; }

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x06002927 RID: 10535 RVA: 0x0002A756 File Offset: 0x00028956
		// (set) Token: 0x06002928 RID: 10536 RVA: 0x0002A75E File Offset: 0x0002895E
		public string Sitting { get; set; }

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x06002929 RID: 10537 RVA: 0x0002A767 File Offset: 0x00028967
		// (set) Token: 0x0600292A RID: 10538 RVA: 0x0002A76F File Offset: 0x0002896F
		public string SittingRoom { get; set; }

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x0600292B RID: 10539 RVA: 0x0002A778 File Offset: 0x00028978
		// (set) Token: 0x0600292C RID: 10540 RVA: 0x0002A780 File Offset: 0x00028980
		public string SittingRoomFirst { get; set; }

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x0600292D RID: 10541 RVA: 0x0002A789 File Offset: 0x00028989
		// (set) Token: 0x0600292E RID: 10542 RVA: 0x0002A791 File Offset: 0x00028991
		public string SittingRoomLast { get; set; }

		// Token: 0x17001145 RID: 4421
		// (get) Token: 0x0600292F RID: 10543 RVA: 0x0002A79A File Offset: 0x0002899A
		// (set) Token: 0x06002930 RID: 10544 RVA: 0x0002A7A2 File Offset: 0x000289A2
		public string SittingLocation { get; set; }

		// Token: 0x17001146 RID: 4422
		// (get) Token: 0x06002931 RID: 10545 RVA: 0x0002A7AB File Offset: 0x000289AB
		// (set) Token: 0x06002932 RID: 10546 RVA: 0x0002A7B3 File Offset: 0x000289B3
		public string SittingInvigilator { get; set; }

		// Token: 0x17001147 RID: 4423
		// (get) Token: 0x06002933 RID: 10547 RVA: 0x0002A7BC File Offset: 0x000289BC
		// (set) Token: 0x06002934 RID: 10548 RVA: 0x0002A7C4 File Offset: 0x000289C4
		public string SittingInvigilatorFirst { get; set; }

		// Token: 0x17001148 RID: 4424
		// (get) Token: 0x06002935 RID: 10549 RVA: 0x0002A7CD File Offset: 0x000289CD
		// (set) Token: 0x06002936 RID: 10550 RVA: 0x0002A7D5 File Offset: 0x000289D5
		public string SittingInvigilatorLast { get; set; }
	}
}
