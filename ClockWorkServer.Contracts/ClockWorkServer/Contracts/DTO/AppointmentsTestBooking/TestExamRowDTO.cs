using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009B5 RID: 2485
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestExamRowDTO
	{
		// Token: 0x1700121C RID: 4636
		// (get) Token: 0x060032C0 RID: 12992 RVA: 0x00018A7E File Offset: 0x00016C7E
		// (set) Token: 0x060032C1 RID: 12993 RVA: 0x00018A86 File Offset: 0x00016C86
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x1700121D RID: 4637
		// (get) Token: 0x060032C2 RID: 12994 RVA: 0x00018A8F File Offset: 0x00016C8F
		// (set) Token: 0x060032C3 RID: 12995 RVA: 0x00018A97 File Offset: 0x00016C97
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x1700121E RID: 4638
		// (get) Token: 0x060032C4 RID: 12996 RVA: 0x00018AA0 File Offset: 0x00016CA0
		// (set) Token: 0x060032C5 RID: 12997 RVA: 0x00018AA8 File Offset: 0x00016CA8
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700121F RID: 4639
		// (get) Token: 0x060032C6 RID: 12998 RVA: 0x00018AB1 File Offset: 0x00016CB1
		// (set) Token: 0x060032C7 RID: 12999 RVA: 0x00018AB9 File Offset: 0x00016CB9
		[DataMember]
		public int AppTypeId { get; set; }

		// Token: 0x17001220 RID: 4640
		// (get) Token: 0x060032C8 RID: 13000 RVA: 0x00018AC2 File Offset: 0x00016CC2
		// (set) Token: 0x060032C9 RID: 13001 RVA: 0x00018ACA File Offset: 0x00016CCA
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17001221 RID: 4641
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x00018AD3 File Offset: 0x00016CD3
		// (set) Token: 0x060032CB RID: 13003 RVA: 0x00018ADB File Offset: 0x00016CDB
		[DataMember]
		public int InvigilatorPid { get; set; }

		// Token: 0x17001222 RID: 4642
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x00018AE4 File Offset: 0x00016CE4
		// (set) Token: 0x060032CD RID: 13005 RVA: 0x00018AEC File Offset: 0x00016CEC
		[DataMember]
		public int RoomPid { get; set; }

		// Token: 0x17001223 RID: 4643
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x00018AF5 File Offset: 0x00016CF5
		// (set) Token: 0x060032CF RID: 13007 RVA: 0x00018AFD File Offset: 0x00016CFD
		[DataMember]
		public int SittingId { get; set; }

		// Token: 0x17001224 RID: 4644
		// (get) Token: 0x060032D0 RID: 13008 RVA: 0x00018B06 File Offset: 0x00016D06
		// (set) Token: 0x060032D1 RID: 13009 RVA: 0x00018B0E File Offset: 0x00016D0E
		[DataMember]
		public int AppCode { get; set; }

		// Token: 0x17001225 RID: 4645
		// (get) Token: 0x060032D2 RID: 13010 RVA: 0x00018B17 File Offset: 0x00016D17
		// (set) Token: 0x060032D3 RID: 13011 RVA: 0x00018B1F File Offset: 0x00016D1F
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x17001226 RID: 4646
		// (get) Token: 0x060032D4 RID: 13012 RVA: 0x00018B28 File Offset: 0x00016D28
		// (set) Token: 0x060032D5 RID: 13013 RVA: 0x00018B30 File Offset: 0x00016D30
		[DataMember]
		public int ExamStatusLookupId { get; set; }

		// Token: 0x17001227 RID: 4647
		// (get) Token: 0x060032D6 RID: 13014 RVA: 0x00018B39 File Offset: 0x00016D39
		// (set) Token: 0x060032D7 RID: 13015 RVA: 0x00018B41 File Offset: 0x00016D41
		[DataMember]
		public string Status { get; set; }

		// Token: 0x17001228 RID: 4648
		// (get) Token: 0x060032D8 RID: 13016 RVA: 0x00018B4A File Offset: 0x00016D4A
		// (set) Token: 0x060032D9 RID: 13017 RVA: 0x00018B52 File Offset: 0x00016D52
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x17001229 RID: 4649
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x00018B5B File Offset: 0x00016D5B
		// (set) Token: 0x060032DB RID: 13019 RVA: 0x00018B63 File Offset: 0x00016D63
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x1700122A RID: 4650
		// (get) Token: 0x060032DC RID: 13020 RVA: 0x00018B6C File Offset: 0x00016D6C
		// (set) Token: 0x060032DD RID: 13021 RVA: 0x00018B74 File Offset: 0x00016D74
		[DataMember]
		public string Student_no { get; set; }

		// Token: 0x1700122B RID: 4651
		// (get) Token: 0x060032DE RID: 13022 RVA: 0x00018B7D File Offset: 0x00016D7D
		// (set) Token: 0x060032DF RID: 13023 RVA: 0x00018B85 File Offset: 0x00016D85
		[DataMember]
		public DateTime ScheduledStartTime { get; set; }

		// Token: 0x1700122C RID: 4652
		// (get) Token: 0x060032E0 RID: 13024 RVA: 0x00018B8E File Offset: 0x00016D8E
		// (set) Token: 0x060032E1 RID: 13025 RVA: 0x00018B96 File Offset: 0x00016D96
		[DataMember]
		public DateTime ScheduledEndTime { get; set; }

		// Token: 0x1700122D RID: 4653
		// (get) Token: 0x060032E2 RID: 13026 RVA: 0x00018B9F File Offset: 0x00016D9F
		// (set) Token: 0x060032E3 RID: 13027 RVA: 0x00018BA7 File Offset: 0x00016DA7
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700122E RID: 4654
		// (get) Token: 0x060032E4 RID: 13028 RVA: 0x00018BB0 File Offset: 0x00016DB0
		// (set) Token: 0x060032E5 RID: 13029 RVA: 0x00018BB8 File Offset: 0x00016DB8
		[DataMember]
		public string Room { get; set; }

		// Token: 0x1700122F RID: 4655
		// (get) Token: 0x060032E6 RID: 13030 RVA: 0x00018BC1 File Offset: 0x00016DC1
		// (set) Token: 0x060032E7 RID: 13031 RVA: 0x00018BC9 File Offset: 0x00016DC9
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17001230 RID: 4656
		// (get) Token: 0x060032E8 RID: 13032 RVA: 0x00018BD2 File Offset: 0x00016DD2
		// (set) Token: 0x060032E9 RID: 13033 RVA: 0x00018BDA File Offset: 0x00016DDA
		[DataMember]
		public string Memo { get; set; }

		// Token: 0x17001231 RID: 4657
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x00018BE3 File Offset: 0x00016DE3
		// (set) Token: 0x060032EB RID: 13035 RVA: 0x00018BEB File Offset: 0x00016DEB
		[DataMember]
		public DateTime ClassStartTime { get; set; }

		// Token: 0x17001232 RID: 4658
		// (get) Token: 0x060032EC RID: 13036 RVA: 0x00018BF4 File Offset: 0x00016DF4
		// (set) Token: 0x060032ED RID: 13037 RVA: 0x00018BFC File Offset: 0x00016DFC
		[DataMember]
		public DateTime ClassEndTime { get; set; }

		// Token: 0x17001233 RID: 4659
		// (get) Token: 0x060032EE RID: 13038 RVA: 0x00018C05 File Offset: 0x00016E05
		// (set) Token: 0x060032EF RID: 13039 RVA: 0x00018C0D File Offset: 0x00016E0D
		[DataMember]
		public bool Cancelled { get; set; }

		// Token: 0x17001234 RID: 4660
		// (get) Token: 0x060032F0 RID: 13040 RVA: 0x00018C16 File Offset: 0x00016E16
		// (set) Token: 0x060032F1 RID: 13041 RVA: 0x00018C1E File Offset: 0x00016E1E
		[DataMember]
		public bool NoShow { get; set; }

		// Token: 0x17001235 RID: 4661
		// (get) Token: 0x060032F2 RID: 13042 RVA: 0x00018C27 File Offset: 0x00016E27
		// (set) Token: 0x060032F3 RID: 13043 RVA: 0x00018C2F File Offset: 0x00016E2F
		[DataMember]
		public bool Tentative { get; set; }

		// Token: 0x17001236 RID: 4662
		// (get) Token: 0x060032F4 RID: 13044 RVA: 0x00018C38 File Offset: 0x00016E38
		// (set) Token: 0x060032F5 RID: 13045 RVA: 0x00018C40 File Offset: 0x00016E40
		[DataMember]
		public bool InstructorSubmitted { get; set; }

		// Token: 0x17001237 RID: 4663
		// (get) Token: 0x060032F6 RID: 13046 RVA: 0x00018C49 File Offset: 0x00016E49
		// (set) Token: 0x060032F7 RID: 13047 RVA: 0x00018C51 File Offset: 0x00016E51
		[DataMember]
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x17001238 RID: 4664
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x00018C5A File Offset: 0x00016E5A
		// (set) Token: 0x060032F9 RID: 13049 RVA: 0x00018C62 File Offset: 0x00016E62
		[DataMember]
		public DateTime CourseStartDate { get; set; }

		// Token: 0x17001239 RID: 4665
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x00018C6B File Offset: 0x00016E6B
		// (set) Token: 0x060032FB RID: 13051 RVA: 0x00018C73 File Offset: 0x00016E73
		[DataMember]
		public DateTime CourseEndDate { get; set; }

		// Token: 0x1700123A RID: 4666
		// (get) Token: 0x060032FC RID: 13052 RVA: 0x00018C7C File Offset: 0x00016E7C
		// (set) Token: 0x060032FD RID: 13053 RVA: 0x00018C84 File Offset: 0x00016E84
		[DataMember]
		public string Department { get; set; }

		// Token: 0x1700123B RID: 4667
		// (get) Token: 0x060032FE RID: 13054 RVA: 0x00018C8D File Offset: 0x00016E8D
		// (set) Token: 0x060032FF RID: 13055 RVA: 0x00018C95 File Offset: 0x00016E95
		[DataMember]
		public string DepartmentEmail { get; set; }

		// Token: 0x1700123C RID: 4668
		// (get) Token: 0x06003300 RID: 13056 RVA: 0x00018C9E File Offset: 0x00016E9E
		// (set) Token: 0x06003301 RID: 13057 RVA: 0x00018CA6 File Offset: 0x00016EA6
		[DataMember]
		public string DepartmentCode { get; set; }

		// Token: 0x1700123D RID: 4669
		// (get) Token: 0x06003302 RID: 13058 RVA: 0x00018CAF File Offset: 0x00016EAF
		// (set) Token: 0x06003303 RID: 13059 RVA: 0x00018CB7 File Offset: 0x00016EB7
		[DataMember]
		public string Term { get; set; }

		// Token: 0x1700123E RID: 4670
		// (get) Token: 0x06003304 RID: 13060 RVA: 0x00018CC0 File Offset: 0x00016EC0
		// (set) Token: 0x06003305 RID: 13061 RVA: 0x00018CC8 File Offset: 0x00016EC8
		[DataMember]
		public string Duration { get; set; }

		// Token: 0x1700123F RID: 4671
		// (get) Token: 0x06003306 RID: 13062 RVA: 0x00018CD1 File Offset: 0x00016ED1
		// (set) Token: 0x06003307 RID: 13063 RVA: 0x00018CD9 File Offset: 0x00016ED9
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x17001240 RID: 4672
		// (get) Token: 0x06003308 RID: 13064 RVA: 0x00018CE2 File Offset: 0x00016EE2
		// (set) Token: 0x06003309 RID: 13065 RVA: 0x00018CEA File Offset: 0x00016EEA
		[DataMember]
		public string Course { get; set; }

		// Token: 0x17001241 RID: 4673
		// (get) Token: 0x0600330A RID: 13066 RVA: 0x00018CF3 File Offset: 0x00016EF3
		// (set) Token: 0x0600330B RID: 13067 RVA: 0x00018CFB File Offset: 0x00016EFB
		[DataMember]
		public string Section { get; set; }

		// Token: 0x17001242 RID: 4674
		// (get) Token: 0x0600330C RID: 13068 RVA: 0x00018D04 File Offset: 0x00016F04
		// (set) Token: 0x0600330D RID: 13069 RVA: 0x00018D0C File Offset: 0x00016F0C
		[DataMember]
		public string TimeOfDay { get; set; }

		// Token: 0x17001243 RID: 4675
		// (get) Token: 0x0600330E RID: 13070 RVA: 0x00018D15 File Offset: 0x00016F15
		// (set) Token: 0x0600330F RID: 13071 RVA: 0x00018D1D File Offset: 0x00016F1D
		[DataMember]
		public string ClassRoom { get; set; }

		// Token: 0x17001244 RID: 4676
		// (get) Token: 0x06003310 RID: 13072 RVA: 0x00018D26 File Offset: 0x00016F26
		// (set) Token: 0x06003311 RID: 13073 RVA: 0x00018D2E File Offset: 0x00016F2E
		[DataMember]
		public string Campus { get; set; }

		// Token: 0x17001245 RID: 4677
		// (get) Token: 0x06003312 RID: 13074 RVA: 0x00018D37 File Offset: 0x00016F37
		// (set) Token: 0x06003313 RID: 13075 RVA: 0x00018D3F File Offset: 0x00016F3F
		[DataMember]
		public string PrimaryInstructor { get; set; }

		// Token: 0x17001246 RID: 4678
		// (get) Token: 0x06003314 RID: 13076 RVA: 0x00018D48 File Offset: 0x00016F48
		// (set) Token: 0x06003315 RID: 13077 RVA: 0x00018D50 File Offset: 0x00016F50
		[DataMember]
		public string PrimaryInstructorEmail { get; set; }

		// Token: 0x17001247 RID: 4679
		// (get) Token: 0x06003316 RID: 13078 RVA: 0x00018D59 File Offset: 0x00016F59
		// (set) Token: 0x06003317 RID: 13079 RVA: 0x00018D61 File Offset: 0x00016F61
		[DataMember]
		public string PrimaryInstructorPhone { get; set; }

		// Token: 0x17001248 RID: 4680
		// (get) Token: 0x06003318 RID: 13080 RVA: 0x00018D6A File Offset: 0x00016F6A
		// (set) Token: 0x06003319 RID: 13081 RVA: 0x00018D72 File Offset: 0x00016F72
		[DataMember]
		public string ExamAccommodations { get; set; }

		// Token: 0x17001249 RID: 4681
		// (get) Token: 0x0600331A RID: 13082 RVA: 0x00018D7B File Offset: 0x00016F7B
		// (set) Token: 0x0600331B RID: 13083 RVA: 0x00018D83 File Offset: 0x00016F83
		[DataMember]
		public string AccommodationGroups { get; set; }

		// Token: 0x1700124A RID: 4682
		// (get) Token: 0x0600331C RID: 13084 RVA: 0x00018D8C File Offset: 0x00016F8C
		// (set) Token: 0x0600331D RID: 13085 RVA: 0x00018D94 File Offset: 0x00016F94
		[DataMember]
		public int TotalBreakMinutes { get; set; }

		// Token: 0x1700124B RID: 4683
		// (get) Token: 0x0600331E RID: 13086 RVA: 0x00018D9D File Offset: 0x00016F9D
		// (set) Token: 0x0600331F RID: 13087 RVA: 0x00018DA5 File Offset: 0x00016FA5
		[DataMember]
		public string AssignedAdvisorFirstName { get; set; }

		// Token: 0x1700124C RID: 4684
		// (get) Token: 0x06003320 RID: 13088 RVA: 0x00018DAE File Offset: 0x00016FAE
		// (set) Token: 0x06003321 RID: 13089 RVA: 0x00018DB6 File Offset: 0x00016FB6
		[DataMember]
		public string AssingedAdvisorLastName { get; set; }

		// Token: 0x1700124D RID: 4685
		// (get) Token: 0x06003322 RID: 13090 RVA: 0x00018DBF File Offset: 0x00016FBF
		// (set) Token: 0x06003323 RID: 13091 RVA: 0x00018DC7 File Offset: 0x00016FC7
		[DataMember]
		public int AssignedAdvisorPersonId { get; set; }

		// Token: 0x1700124E RID: 4686
		// (get) Token: 0x06003324 RID: 13092 RVA: 0x00018DD0 File Offset: 0x00016FD0
		// (set) Token: 0x06003325 RID: 13093 RVA: 0x00018DD8 File Offset: 0x00016FD8
		[DataMember]
		public string Invigilator { get; set; }

		// Token: 0x1700124F RID: 4687
		// (get) Token: 0x06003326 RID: 13094 RVA: 0x00018DE1 File Offset: 0x00016FE1
		// (set) Token: 0x06003327 RID: 13095 RVA: 0x00018DE9 File Offset: 0x00016FE9
		[DataMember]
		public DateTime DateAdded { get; set; }

		// Token: 0x17001250 RID: 4688
		// (get) Token: 0x06003328 RID: 13096 RVA: 0x00018DF2 File Offset: 0x00016FF2
		// (set) Token: 0x06003329 RID: 13097 RVA: 0x00018DFA File Offset: 0x00016FFA
		[DataMember]
		public string WhoBooked { get; set; }

		// Token: 0x17001251 RID: 4689
		// (get) Token: 0x0600332A RID: 13098 RVA: 0x00018E03 File Offset: 0x00017003
		// (set) Token: 0x0600332B RID: 13099 RVA: 0x00018E0B File Offset: 0x0001700B
		[DataMember]
		public int WhoBookedPersonId { get; set; }

		// Token: 0x17001252 RID: 4690
		// (get) Token: 0x0600332C RID: 13100 RVA: 0x00018E14 File Offset: 0x00017014
		// (set) Token: 0x0600332D RID: 13101 RVA: 0x00018E1C File Offset: 0x0001701C
		[DataMember]
		public DateTime? ActualStartTime { get; set; }

		// Token: 0x17001253 RID: 4691
		// (get) Token: 0x0600332E RID: 13102 RVA: 0x00018E25 File Offset: 0x00017025
		// (set) Token: 0x0600332F RID: 13103 RVA: 0x00018E2D File Offset: 0x0001702D
		[DataMember]
		public DateTime? ActualEndTime { get; set; }

		// Token: 0x17001254 RID: 4692
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x00018E36 File Offset: 0x00017036
		// (set) Token: 0x06003331 RID: 13105 RVA: 0x00018E3E File Offset: 0x0001703E
		[DataMember]
		public string TestDelivered { get; set; }

		// Token: 0x17001255 RID: 4693
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x00018E47 File Offset: 0x00017047
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x00018E4F File Offset: 0x0001704F
		[DataMember]
		public DateTime? StudentReportedClassStartTime { get; set; }

		// Token: 0x17001256 RID: 4694
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x00018E58 File Offset: 0x00017058
		// (set) Token: 0x06003335 RID: 13109 RVA: 0x00018E60 File Offset: 0x00017060
		[DataMember]
		public DateTime? StudentReportedClassEndTime { get; set; }

		// Token: 0x17001257 RID: 4695
		// (get) Token: 0x06003336 RID: 13110 RVA: 0x00018E69 File Offset: 0x00017069
		// (set) Token: 0x06003337 RID: 13111 RVA: 0x00018E71 File Offset: 0x00017071
		[DataMember]
		public string AlternateContact { get; set; }

		// Token: 0x17001258 RID: 4696
		// (get) Token: 0x06003338 RID: 13112 RVA: 0x00018E7A File Offset: 0x0001707A
		// (set) Token: 0x06003339 RID: 13113 RVA: 0x00018E82 File Offset: 0x00017082
		[DataMember]
		public string AlternateContactEmail { get; set; }

		// Token: 0x17001259 RID: 4697
		// (get) Token: 0x0600333A RID: 13114 RVA: 0x00018E8B File Offset: 0x0001708B
		// (set) Token: 0x0600333B RID: 13115 RVA: 0x00018E93 File Offset: 0x00017093
		[DataMember]
		public string AlternateContactPhone { get; set; }

		// Token: 0x1700125A RID: 4698
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x00018E9C File Offset: 0x0001709C
		// (set) Token: 0x0600333D RID: 13117 RVA: 0x00018EA4 File Offset: 0x000170A4
		[DataMember]
		public string AlternateContactUsername { get; set; }

		// Token: 0x1700125B RID: 4699
		// (get) Token: 0x0600333E RID: 13118 RVA: 0x00018EAD File Offset: 0x000170AD
		// (set) Token: 0x0600333F RID: 13119 RVA: 0x00018EB5 File Offset: 0x000170B5
		[DataMember]
		public int AlternateContactPermissionLevel { get; set; }

		// Token: 0x1700125C RID: 4700
		// (get) Token: 0x06003340 RID: 13120 RVA: 0x00018EBE File Offset: 0x000170BE
		// (set) Token: 0x06003341 RID: 13121 RVA: 0x00018EC6 File Offset: 0x000170C6
		[DataMember]
		public string InstructorAcknowledged { get; set; }

		// Token: 0x1700125D RID: 4701
		// (get) Token: 0x06003342 RID: 13122 RVA: 0x00018ECF File Offset: 0x000170CF
		// (set) Token: 0x06003343 RID: 13123 RVA: 0x00018ED7 File Offset: 0x000170D7
		[DataMember]
		public string InstructorAcknowledgedOnline { get; set; }

		// Token: 0x1700125E RID: 4702
		// (get) Token: 0x06003344 RID: 13124 RVA: 0x00018EE0 File Offset: 0x000170E0
		// (set) Token: 0x06003345 RID: 13125 RVA: 0x00018EE8 File Offset: 0x000170E8
		[DataMember]
		public DateTime? InstructorAcknolwedgedDate { get; set; }

		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x06003346 RID: 13126 RVA: 0x00018EF1 File Offset: 0x000170F1
		// (set) Token: 0x06003347 RID: 13127 RVA: 0x00018EF9 File Offset: 0x000170F9
		[DataMember]
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x17001260 RID: 4704
		// (get) Token: 0x06003348 RID: 13128 RVA: 0x00018F02 File Offset: 0x00017102
		// (set) Token: 0x06003349 RID: 13129 RVA: 0x00018F0A File Offset: 0x0001710A
		[DataMember]
		public string InstructorContactedNote { get; set; }

		// Token: 0x17001261 RID: 4705
		// (get) Token: 0x0600334A RID: 13130 RVA: 0x00018F13 File Offset: 0x00017113
		// (set) Token: 0x0600334B RID: 13131 RVA: 0x00018F1B File Offset: 0x0001711B
		[DataMember]
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x17001262 RID: 4706
		// (get) Token: 0x0600334C RID: 13132 RVA: 0x00018F24 File Offset: 0x00017124
		// (set) Token: 0x0600334D RID: 13133 RVA: 0x00018F2C File Offset: 0x0001712C
		[DataMember]
		public string TestPickedUpNote { get; set; }

		// Token: 0x17001263 RID: 4707
		// (get) Token: 0x0600334E RID: 13134 RVA: 0x00018F35 File Offset: 0x00017135
		// (set) Token: 0x0600334F RID: 13135 RVA: 0x00018F3D File Offset: 0x0001713D
		[DataMember]
		public string PrivateNote2 { get; set; }

		// Token: 0x17001264 RID: 4708
		// (get) Token: 0x06003350 RID: 13136 RVA: 0x00018F46 File Offset: 0x00017146
		// (set) Token: 0x06003351 RID: 13137 RVA: 0x00018F4E File Offset: 0x0001714E
		[DataMember]
		public string ExamStatus { get; set; }

		// Token: 0x17001265 RID: 4709
		// (get) Token: 0x06003352 RID: 13138 RVA: 0x00018F57 File Offset: 0x00017157
		// (set) Token: 0x06003353 RID: 13139 RVA: 0x00018F5F File Offset: 0x0001715F
		[DataMember]
		public int ColourArgB { get; set; }

		// Token: 0x17001266 RID: 4710
		// (get) Token: 0x06003354 RID: 13140 RVA: 0x00018F68 File Offset: 0x00017168
		// (set) Token: 0x06003355 RID: 13141 RVA: 0x00018F70 File Offset: 0x00017170
		[DataMember]
		public string Sitting { get; set; }

		// Token: 0x17001267 RID: 4711
		// (get) Token: 0x06003356 RID: 13142 RVA: 0x00018F79 File Offset: 0x00017179
		// (set) Token: 0x06003357 RID: 13143 RVA: 0x00018F81 File Offset: 0x00017181
		[DataMember]
		public string SittingRoom { get; set; }

		// Token: 0x17001268 RID: 4712
		// (get) Token: 0x06003358 RID: 13144 RVA: 0x00018F8A File Offset: 0x0001718A
		// (set) Token: 0x06003359 RID: 13145 RVA: 0x00018F92 File Offset: 0x00017192
		[DataMember]
		public string SittingLocation { get; set; }

		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x0600335A RID: 13146 RVA: 0x00018F9B File Offset: 0x0001719B
		// (set) Token: 0x0600335B RID: 13147 RVA: 0x00018FA3 File Offset: 0x000171A3
		[DataMember]
		public string SittingInvigilator { get; set; }
	}
}
