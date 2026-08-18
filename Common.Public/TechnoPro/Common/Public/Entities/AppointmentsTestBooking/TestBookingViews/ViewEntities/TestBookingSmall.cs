using System;
using TechnoPro.Common.Public.Entities.Adapters;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x02000526 RID: 1318
	public class TestBookingSmall : BusinessBase<int>
	{
		// Token: 0x17001149 RID: 4425
		// (get) Token: 0x06002938 RID: 10552 RVA: 0x0002A7E8 File Offset: 0x000289E8
		// (set) Token: 0x06002939 RID: 10553 RVA: 0x0002A800 File Offset: 0x00028A00
		public virtual int AppointmentId
		{
			get
			{
				return base.Id;
			}
			set
			{
				base.Id = value;
			}
		}

		// Token: 0x1700114A RID: 4426
		// (get) Token: 0x0600293A RID: 10554 RVA: 0x0002A80B File Offset: 0x00028A0B
		// (set) Token: 0x0600293B RID: 10555 RVA: 0x0002A813 File Offset: 0x00028A13
		public int ExamId { get; set; }

		// Token: 0x1700114B RID: 4427
		// (get) Token: 0x0600293C RID: 10556 RVA: 0x0002A81C File Offset: 0x00028A1C
		// (set) Token: 0x0600293D RID: 10557 RVA: 0x0002A824 File Offset: 0x00028A24
		public int PersonId { get; set; }

		// Token: 0x1700114C RID: 4428
		// (get) Token: 0x0600293E RID: 10558 RVA: 0x0002A82D File Offset: 0x00028A2D
		// (set) Token: 0x0600293F RID: 10559 RVA: 0x0002A835 File Offset: 0x00028A35
		public int AppTypeId { get; set; }

		// Token: 0x1700114D RID: 4429
		// (get) Token: 0x06002940 RID: 10560 RVA: 0x0002A83E File Offset: 0x00028A3E
		// (set) Token: 0x06002941 RID: 10561 RVA: 0x0002A846 File Offset: 0x00028A46
		public int LuCourseId { get; set; }

		// Token: 0x1700114E RID: 4430
		// (get) Token: 0x06002942 RID: 10562 RVA: 0x0002A84F File Offset: 0x00028A4F
		// (set) Token: 0x06002943 RID: 10563 RVA: 0x0002A857 File Offset: 0x00028A57
		public int RoomPid { get; set; }

		// Token: 0x1700114F RID: 4431
		// (get) Token: 0x06002944 RID: 10564 RVA: 0x0002A860 File Offset: 0x00028A60
		// (set) Token: 0x06002945 RID: 10565 RVA: 0x0002A868 File Offset: 0x00028A68
		public int AppCode { get; set; }

		// Token: 0x17001150 RID: 4432
		// (get) Token: 0x06002946 RID: 10566 RVA: 0x0002A871 File Offset: 0x00028A71
		// (set) Token: 0x06002947 RID: 10567 RVA: 0x0002A879 File Offset: 0x00028A79
		public int ExamStatusLookupId { get; set; }

		// Token: 0x17001151 RID: 4433
		// (get) Token: 0x06002948 RID: 10568 RVA: 0x0002A882 File Offset: 0x00028A82
		// (set) Token: 0x06002949 RID: 10569 RVA: 0x0002A88A File Offset: 0x00028A8A
		public string Status { get; set; }

		// Token: 0x17001152 RID: 4434
		// (get) Token: 0x0600294A RID: 10570 RVA: 0x0002A893 File Offset: 0x00028A93
		// (set) Token: 0x0600294B RID: 10571 RVA: 0x0002A89B File Offset: 0x00028A9B
		public PersonBase Student { get; set; }

		// Token: 0x17001153 RID: 4435
		// (get) Token: 0x0600294C RID: 10572 RVA: 0x0002A8A4 File Offset: 0x00028AA4
		public string FirstName
		{
			get
			{
				return (this.Student == null) ? "" : (this.Student.FirstName ?? "");
			}
		}

		// Token: 0x17001154 RID: 4436
		// (get) Token: 0x0600294D RID: 10573 RVA: 0x0002A8DC File Offset: 0x00028ADC
		public string LastName
		{
			get
			{
				return (this.Student == null) ? "" : (this.Student.LastName ?? "");
			}
		}

		// Token: 0x17001155 RID: 4437
		// (get) Token: 0x0600294E RID: 10574 RVA: 0x0002A914 File Offset: 0x00028B14
		public string MiddleName
		{
			get
			{
				return (this.Student == null) ? "" : (this.Student.MiddleName ?? "");
			}
		}

		// Token: 0x17001156 RID: 4438
		// (get) Token: 0x0600294F RID: 10575 RVA: 0x0002A94C File Offset: 0x00028B4C
		public string Student_no
		{
			get
			{
				return (this.Student == null) ? "" : (this.Student.Student_no ?? "");
			}
		}

		// Token: 0x17001157 RID: 4439
		// (get) Token: 0x06002950 RID: 10576 RVA: 0x0002A984 File Offset: 0x00028B84
		public string StudentName
		{
			get
			{
				return (this.Student == null) ? "" : this.Student.GetStudentName();
			}
		}

		// Token: 0x17001158 RID: 4440
		// (get) Token: 0x06002951 RID: 10577 RVA: 0x0002A9B0 File Offset: 0x00028BB0
		// (set) Token: 0x06002952 RID: 10578 RVA: 0x0002A9B8 File Offset: 0x00028BB8
		public string Subject { get; set; }

		// Token: 0x17001159 RID: 4441
		// (get) Token: 0x06002953 RID: 10579 RVA: 0x0002A9C1 File Offset: 0x00028BC1
		// (set) Token: 0x06002954 RID: 10580 RVA: 0x0002A9C9 File Offset: 0x00028BC9
		public string Course { get; set; }

		// Token: 0x1700115A RID: 4442
		// (get) Token: 0x06002955 RID: 10581 RVA: 0x0002A9D2 File Offset: 0x00028BD2
		// (set) Token: 0x06002956 RID: 10582 RVA: 0x0002A9DA File Offset: 0x00028BDA
		public string TimeOfDay { get; set; }

		// Token: 0x1700115B RID: 4443
		// (get) Token: 0x06002957 RID: 10583 RVA: 0x0002A9E3 File Offset: 0x00028BE3
		// (set) Token: 0x06002958 RID: 10584 RVA: 0x0002A9EB File Offset: 0x00028BEB
		public string Section { get; set; }

		// Token: 0x1700115C RID: 4444
		// (get) Token: 0x06002959 RID: 10585 RVA: 0x0002A9F4 File Offset: 0x00028BF4
		// (set) Token: 0x0600295A RID: 10586 RVA: 0x0002A9FC File Offset: 0x00028BFC
		public string Classroom { get; set; }

		// Token: 0x1700115D RID: 4445
		// (get) Token: 0x0600295B RID: 10587 RVA: 0x0002AA05 File Offset: 0x00028C05
		// (set) Token: 0x0600295C RID: 10588 RVA: 0x0002AA0D File Offset: 0x00028C0D
		public string Campus { get; set; }

		// Token: 0x1700115E RID: 4446
		// (get) Token: 0x0600295D RID: 10589 RVA: 0x0002AA16 File Offset: 0x00028C16
		// (set) Token: 0x0600295E RID: 10590 RVA: 0x0002AA1E File Offset: 0x00028C1E
		public string CourseDescription { get; set; }

		// Token: 0x1700115F RID: 4447
		// (get) Token: 0x0600295F RID: 10591 RVA: 0x0002AA27 File Offset: 0x00028C27
		// (set) Token: 0x06002960 RID: 10592 RVA: 0x0002AA2F File Offset: 0x00028C2F
		public DateTime? ScheduledDate { get; set; }

		// Token: 0x17001160 RID: 4448
		// (get) Token: 0x06002961 RID: 10593 RVA: 0x0002AA38 File Offset: 0x00028C38
		// (set) Token: 0x06002962 RID: 10594 RVA: 0x0002AA40 File Offset: 0x00028C40
		public DateTime? ScheduledStartTime { get; set; }

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x06002963 RID: 10595 RVA: 0x0002AA49 File Offset: 0x00028C49
		// (set) Token: 0x06002964 RID: 10596 RVA: 0x0002AA51 File Offset: 0x00028C51
		public DateTime? ScheduledEndTime { get; set; }

		// Token: 0x17001162 RID: 4450
		// (get) Token: 0x06002965 RID: 10597 RVA: 0x0002AA5A File Offset: 0x00028C5A
		// (set) Token: 0x06002966 RID: 10598 RVA: 0x0002AA62 File Offset: 0x00028C62
		public string Description { get; set; }

		// Token: 0x17001163 RID: 4451
		// (get) Token: 0x06002967 RID: 10599 RVA: 0x0002AA6B File Offset: 0x00028C6B
		// (set) Token: 0x06002968 RID: 10600 RVA: 0x0002AA73 File Offset: 0x00028C73
		public string Room { get; set; }

		// Token: 0x17001164 RID: 4452
		// (get) Token: 0x06002969 RID: 10601 RVA: 0x0002AA7C File Offset: 0x00028C7C
		// (set) Token: 0x0600296A RID: 10602 RVA: 0x0002AA84 File Offset: 0x00028C84
		public string Location { get; set; }

		// Token: 0x17001165 RID: 4453
		// (get) Token: 0x0600296B RID: 10603 RVA: 0x0002AA8D File Offset: 0x00028C8D
		// (set) Token: 0x0600296C RID: 10604 RVA: 0x0002AA95 File Offset: 0x00028C95
		public string Memo { get; set; }

		// Token: 0x17001166 RID: 4454
		// (get) Token: 0x0600296D RID: 10605 RVA: 0x0002AA9E File Offset: 0x00028C9E
		// (set) Token: 0x0600296E RID: 10606 RVA: 0x0002AAA6 File Offset: 0x00028CA6
		public DateTime ClassDate { get; set; }

		// Token: 0x17001167 RID: 4455
		// (get) Token: 0x0600296F RID: 10607 RVA: 0x0002AAAF File Offset: 0x00028CAF
		// (set) Token: 0x06002970 RID: 10608 RVA: 0x0002AAB7 File Offset: 0x00028CB7
		public DateTime ClassStartTime { get; set; }

		// Token: 0x17001168 RID: 4456
		// (get) Token: 0x06002971 RID: 10609 RVA: 0x0002AAC0 File Offset: 0x00028CC0
		// (set) Token: 0x06002972 RID: 10610 RVA: 0x0002AAC8 File Offset: 0x00028CC8
		public DateTime ClassEndTime { get; set; }

		// Token: 0x17001169 RID: 4457
		// (get) Token: 0x06002973 RID: 10611 RVA: 0x0002AAD1 File Offset: 0x00028CD1
		// (set) Token: 0x06002974 RID: 10612 RVA: 0x0002AAD9 File Offset: 0x00028CD9
		public string ClassLocation { get; set; }

		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x06002975 RID: 10613 RVA: 0x0002AAE2 File Offset: 0x00028CE2
		// (set) Token: 0x06002976 RID: 10614 RVA: 0x0002AAEA File Offset: 0x00028CEA
		public bool Cancelled { get; set; }

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06002977 RID: 10615 RVA: 0x0002AAF3 File Offset: 0x00028CF3
		// (set) Token: 0x06002978 RID: 10616 RVA: 0x0002AAFB File Offset: 0x00028CFB
		public bool NoShow { get; set; }

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06002979 RID: 10617 RVA: 0x0002AB04 File Offset: 0x00028D04
		public bool IsTentative
		{
			get
			{
				return this.AppCode == -1;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x0600297A RID: 10618 RVA: 0x0002AB1F File Offset: 0x00028D1F
		// (set) Token: 0x0600297B RID: 10619 RVA: 0x0002AB27 File Offset: 0x00028D27
		public DateTime? ActualDate { get; set; }

		// Token: 0x1700116E RID: 4462
		// (get) Token: 0x0600297C RID: 10620 RVA: 0x0002AB30 File Offset: 0x00028D30
		// (set) Token: 0x0600297D RID: 10621 RVA: 0x0002AB38 File Offset: 0x00028D38
		public DateTime? ActualStartTime { get; set; }

		// Token: 0x1700116F RID: 4463
		// (get) Token: 0x0600297E RID: 10622 RVA: 0x0002AB41 File Offset: 0x00028D41
		// (set) Token: 0x0600297F RID: 10623 RVA: 0x0002AB49 File Offset: 0x00028D49
		public DateTime? ActualEndTime { get; set; }

		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06002980 RID: 10624 RVA: 0x0002AB52 File Offset: 0x00028D52
		// (set) Token: 0x06002981 RID: 10625 RVA: 0x0002AB5A File Offset: 0x00028D5A
		public DateTime? ProjectedActualEndTime { get; set; }

		// Token: 0x17001171 RID: 4465
		// (get) Token: 0x06002982 RID: 10626 RVA: 0x0002AB63 File Offset: 0x00028D63
		// (set) Token: 0x06002983 RID: 10627 RVA: 0x0002AB6B File Offset: 0x00028D6B
		public bool TestWasDelivered { get; set; }

		// Token: 0x17001172 RID: 4466
		// (get) Token: 0x06002984 RID: 10628 RVA: 0x0002AB74 File Offset: 0x00028D74
		// (set) Token: 0x06002985 RID: 10629 RVA: 0x0002AB7C File Offset: 0x00028D7C
		public string TestDelivered { get; set; }

		// Token: 0x17001173 RID: 4467
		// (get) Token: 0x06002986 RID: 10630 RVA: 0x0002AB85 File Offset: 0x00028D85
		// (set) Token: 0x06002987 RID: 10631 RVA: 0x0002AB8D File Offset: 0x00028D8D
		public string ExamStatus { get; set; }

		// Token: 0x17001174 RID: 4468
		// (get) Token: 0x06002988 RID: 10632 RVA: 0x0002AB96 File Offset: 0x00028D96
		// (set) Token: 0x06002989 RID: 10633 RVA: 0x0002AB9E File Offset: 0x00028D9E
		public int ColourArgB { get; set; }

		// Token: 0x17001175 RID: 4469
		// (get) Token: 0x0600298A RID: 10634 RVA: 0x0002ABA7 File Offset: 0x00028DA7
		// (set) Token: 0x0600298B RID: 10635 RVA: 0x0002ABAF File Offset: 0x00028DAF
		public string Custom1 { get; set; }

		// Token: 0x17001176 RID: 4470
		// (get) Token: 0x0600298C RID: 10636 RVA: 0x0002ABB8 File Offset: 0x00028DB8
		// (set) Token: 0x0600298D RID: 10637 RVA: 0x0002ABC0 File Offset: 0x00028DC0
		public string Custom2 { get; set; }

		// Token: 0x17001177 RID: 4471
		// (get) Token: 0x0600298E RID: 10638 RVA: 0x0002ABC9 File Offset: 0x00028DC9
		// (set) Token: 0x0600298F RID: 10639 RVA: 0x0002ABD1 File Offset: 0x00028DD1
		public string Custom3 { get; set; }

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06002990 RID: 10640 RVA: 0x0002ABDA File Offset: 0x00028DDA
		// (set) Token: 0x06002991 RID: 10641 RVA: 0x0002ABE2 File Offset: 0x00028DE2
		public string Custom4 { get; set; }

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06002992 RID: 10642 RVA: 0x0002ABEB File Offset: 0x00028DEB
		// (set) Token: 0x06002993 RID: 10643 RVA: 0x0002ABF3 File Offset: 0x00028DF3
		public string Custom5 { get; set; }

		// Token: 0x1700117A RID: 4474
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x0002ABFC File Offset: 0x00028DFC
		// (set) Token: 0x06002995 RID: 10645 RVA: 0x0002AC04 File Offset: 0x00028E04
		public string Custom6 { get; set; }

		// Token: 0x1700117B RID: 4475
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x0002AC0D File Offset: 0x00028E0D
		// (set) Token: 0x06002997 RID: 10647 RVA: 0x0002AC15 File Offset: 0x00028E15
		public string Custom7 { get; set; }

		// Token: 0x1700117C RID: 4476
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x0002AC1E File Offset: 0x00028E1E
		// (set) Token: 0x06002999 RID: 10649 RVA: 0x0002AC26 File Offset: 0x00028E26
		public string Custom8 { get; set; }

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x0600299A RID: 10650 RVA: 0x0002AC2F File Offset: 0x00028E2F
		// (set) Token: 0x0600299B RID: 10651 RVA: 0x0002AC37 File Offset: 0x00028E37
		public string Custom9 { get; set; }

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x0002AC40 File Offset: 0x00028E40
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x0002AC48 File Offset: 0x00028E48
		public string Custom10 { get; set; }

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x0002AC51 File Offset: 0x00028E51
		// (set) Token: 0x0600299F RID: 10655 RVA: 0x0002AC59 File Offset: 0x00028E59
		public string Custom11 { get; set; }

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x0002AC62 File Offset: 0x00028E62
		// (set) Token: 0x060029A1 RID: 10657 RVA: 0x0002AC6A File Offset: 0x00028E6A
		public string Custom12 { get; set; }

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x060029A2 RID: 10658 RVA: 0x0002AC73 File Offset: 0x00028E73
		// (set) Token: 0x060029A3 RID: 10659 RVA: 0x0002AC7B File Offset: 0x00028E7B
		public string Custom13 { get; set; }

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x060029A4 RID: 10660 RVA: 0x0002AC84 File Offset: 0x00028E84
		// (set) Token: 0x060029A5 RID: 10661 RVA: 0x0002AC8C File Offset: 0x00028E8C
		public string Custom14 { get; set; }

		// Token: 0x17001183 RID: 4483
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x0002AC95 File Offset: 0x00028E95
		// (set) Token: 0x060029A7 RID: 10663 RVA: 0x0002AC9D File Offset: 0x00028E9D
		public string Custom15 { get; set; }

		// Token: 0x17001184 RID: 4484
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x0002ACA6 File Offset: 0x00028EA6
		// (set) Token: 0x060029A9 RID: 10665 RVA: 0x0002ACAE File Offset: 0x00028EAE
		public string Custom16 { get; set; }

		// Token: 0x17001185 RID: 4485
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x0002ACB7 File Offset: 0x00028EB7
		// (set) Token: 0x060029AB RID: 10667 RVA: 0x0002ACBF File Offset: 0x00028EBF
		public string Custom17 { get; set; }

		// Token: 0x17001186 RID: 4486
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x0002ACC8 File Offset: 0x00028EC8
		// (set) Token: 0x060029AD RID: 10669 RVA: 0x0002ACD0 File Offset: 0x00028ED0
		public string Custom18 { get; set; }

		// Token: 0x17001187 RID: 4487
		// (get) Token: 0x060029AE RID: 10670 RVA: 0x0002ACD9 File Offset: 0x00028ED9
		// (set) Token: 0x060029AF RID: 10671 RVA: 0x0002ACE1 File Offset: 0x00028EE1
		public string Custom19 { get; set; }

		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x0002ACEA File Offset: 0x00028EEA
		// (set) Token: 0x060029B1 RID: 10673 RVA: 0x0002ACF2 File Offset: 0x00028EF2
		public string Custom20 { get; set; }
	}
}
