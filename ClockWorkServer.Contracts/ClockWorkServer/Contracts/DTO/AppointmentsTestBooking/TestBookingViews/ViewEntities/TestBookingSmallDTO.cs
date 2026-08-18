using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Adapters;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x02000A47 RID: 2631
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestBookingSmallDTO
	{
		// Token: 0x170013D3 RID: 5075
		// (get) Token: 0x060036C4 RID: 14020 RVA: 0x0001A90B File Offset: 0x00018B0B
		// (set) Token: 0x060036C5 RID: 14021 RVA: 0x0001A913 File Offset: 0x00018B13
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170013D4 RID: 5076
		// (get) Token: 0x060036C6 RID: 14022 RVA: 0x0001A91C File Offset: 0x00018B1C
		// (set) Token: 0x060036C7 RID: 14023 RVA: 0x0001A924 File Offset: 0x00018B24
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170013D5 RID: 5077
		// (get) Token: 0x060036C8 RID: 14024 RVA: 0x0001A92D File Offset: 0x00018B2D
		// (set) Token: 0x060036C9 RID: 14025 RVA: 0x0001A935 File Offset: 0x00018B35
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x170013D6 RID: 5078
		// (get) Token: 0x060036CA RID: 14026 RVA: 0x0001A93E File Offset: 0x00018B3E
		// (set) Token: 0x060036CB RID: 14027 RVA: 0x0001A946 File Offset: 0x00018B46
		[DataMember]
		public int AppTypeId { get; set; }

		// Token: 0x170013D7 RID: 5079
		// (get) Token: 0x060036CC RID: 14028 RVA: 0x0001A94F File Offset: 0x00018B4F
		// (set) Token: 0x060036CD RID: 14029 RVA: 0x0001A957 File Offset: 0x00018B57
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x170013D8 RID: 5080
		// (get) Token: 0x060036CE RID: 14030 RVA: 0x0001A960 File Offset: 0x00018B60
		// (set) Token: 0x060036CF RID: 14031 RVA: 0x0001A968 File Offset: 0x00018B68
		[DataMember]
		public int RoomPid { get; set; }

		// Token: 0x170013D9 RID: 5081
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x0001A971 File Offset: 0x00018B71
		// (set) Token: 0x060036D1 RID: 14033 RVA: 0x0001A979 File Offset: 0x00018B79
		[DataMember]
		public int AppCode { get; set; }

		// Token: 0x170013DA RID: 5082
		// (get) Token: 0x060036D2 RID: 14034 RVA: 0x0001A982 File Offset: 0x00018B82
		// (set) Token: 0x060036D3 RID: 14035 RVA: 0x0001A98A File Offset: 0x00018B8A
		[DataMember]
		public int ExamStatusLookupId { get; set; }

		// Token: 0x170013DB RID: 5083
		// (get) Token: 0x060036D4 RID: 14036 RVA: 0x0001A993 File Offset: 0x00018B93
		// (set) Token: 0x060036D5 RID: 14037 RVA: 0x0001A99B File Offset: 0x00018B9B
		[DataMember]
		public string Status { get; set; }

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x060036D6 RID: 14038 RVA: 0x0001A9A4 File Offset: 0x00018BA4
		// (set) Token: 0x060036D7 RID: 14039 RVA: 0x0001A9AC File Offset: 0x00018BAC
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x060036D8 RID: 14040 RVA: 0x0001A9B8 File Offset: 0x00018BB8
		public string FirstName
		{
			get
			{
				return (this.Student == null) ? "" : (this.Student.FirstName ?? "");
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x060036D9 RID: 14041 RVA: 0x0001A9F0 File Offset: 0x00018BF0
		public string LastName
		{
			get
			{
				return (this.Student == null) ? "" : (this.Student.LastName ?? "");
			}
		}

		// Token: 0x170013DF RID: 5087
		// (get) Token: 0x060036DA RID: 14042 RVA: 0x0001AA28 File Offset: 0x00018C28
		public string MiddleName
		{
			get
			{
				return (this.Student == null) ? "" : (this.Student.MiddleName ?? "");
			}
		}

		// Token: 0x170013E0 RID: 5088
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x0001AA60 File Offset: 0x00018C60
		public string Student_no
		{
			get
			{
				return (this.Student == null) ? "" : (this.Student.Student_no ?? "");
			}
		}

		// Token: 0x170013E1 RID: 5089
		// (get) Token: 0x060036DC RID: 14044 RVA: 0x0001AA98 File Offset: 0x00018C98
		public string StudentName
		{
			get
			{
				return (this.Student == null) ? "" : this.Student.GetStudentName();
			}
		}

		// Token: 0x170013E2 RID: 5090
		// (get) Token: 0x060036DD RID: 14045 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		// (set) Token: 0x060036DE RID: 14046 RVA: 0x0001AACC File Offset: 0x00018CCC
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x170013E3 RID: 5091
		// (get) Token: 0x060036DF RID: 14047 RVA: 0x0001AAD5 File Offset: 0x00018CD5
		// (set) Token: 0x060036E0 RID: 14048 RVA: 0x0001AADD File Offset: 0x00018CDD
		[DataMember]
		public string Course { get; set; }

		// Token: 0x170013E4 RID: 5092
		// (get) Token: 0x060036E1 RID: 14049 RVA: 0x0001AAE6 File Offset: 0x00018CE6
		// (set) Token: 0x060036E2 RID: 14050 RVA: 0x0001AAEE File Offset: 0x00018CEE
		[DataMember]
		public string TimeOfDay { get; set; }

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x060036E3 RID: 14051 RVA: 0x0001AAF7 File Offset: 0x00018CF7
		// (set) Token: 0x060036E4 RID: 14052 RVA: 0x0001AAFF File Offset: 0x00018CFF
		[DataMember]
		public string Section { get; set; }

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x060036E5 RID: 14053 RVA: 0x0001AB08 File Offset: 0x00018D08
		// (set) Token: 0x060036E6 RID: 14054 RVA: 0x0001AB10 File Offset: 0x00018D10
		[DataMember]
		public string Classroom { get; set; }

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x060036E7 RID: 14055 RVA: 0x0001AB19 File Offset: 0x00018D19
		// (set) Token: 0x060036E8 RID: 14056 RVA: 0x0001AB21 File Offset: 0x00018D21
		[DataMember]
		public string Campus { get; set; }

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x060036E9 RID: 14057 RVA: 0x0001AB2A File Offset: 0x00018D2A
		// (set) Token: 0x060036EA RID: 14058 RVA: 0x0001AB32 File Offset: 0x00018D32
		[DataMember]
		public string CourseDescription { get; set; }

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x060036EB RID: 14059 RVA: 0x0001AB3B File Offset: 0x00018D3B
		// (set) Token: 0x060036EC RID: 14060 RVA: 0x0001AB43 File Offset: 0x00018D43
		[DataMember]
		public DateTime? ScheduledDate { get; set; }

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x060036ED RID: 14061 RVA: 0x0001AB4C File Offset: 0x00018D4C
		// (set) Token: 0x060036EE RID: 14062 RVA: 0x0001AB54 File Offset: 0x00018D54
		[DataMember]
		public DateTime? ScheduledStartTime { get; set; }

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x060036EF RID: 14063 RVA: 0x0001AB5D File Offset: 0x00018D5D
		// (set) Token: 0x060036F0 RID: 14064 RVA: 0x0001AB65 File Offset: 0x00018D65
		[DataMember]
		public DateTime? ScheduledEndTime { get; set; }

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x060036F1 RID: 14065 RVA: 0x0001AB6E File Offset: 0x00018D6E
		// (set) Token: 0x060036F2 RID: 14066 RVA: 0x0001AB76 File Offset: 0x00018D76
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x060036F3 RID: 14067 RVA: 0x0001AB7F File Offset: 0x00018D7F
		// (set) Token: 0x060036F4 RID: 14068 RVA: 0x0001AB87 File Offset: 0x00018D87
		[DataMember]
		public string Room { get; set; }

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x060036F5 RID: 14069 RVA: 0x0001AB90 File Offset: 0x00018D90
		// (set) Token: 0x060036F6 RID: 14070 RVA: 0x0001AB98 File Offset: 0x00018D98
		[DataMember]
		public string Location { get; set; }

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x0001ABA1 File Offset: 0x00018DA1
		// (set) Token: 0x060036F8 RID: 14072 RVA: 0x0001ABA9 File Offset: 0x00018DA9
		[DataMember]
		public string Memo { get; set; }

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x060036F9 RID: 14073 RVA: 0x0001ABB2 File Offset: 0x00018DB2
		// (set) Token: 0x060036FA RID: 14074 RVA: 0x0001ABBA File Offset: 0x00018DBA
		[DataMember]
		public DateTime ClassDate { get; set; }

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x060036FB RID: 14075 RVA: 0x0001ABC3 File Offset: 0x00018DC3
		// (set) Token: 0x060036FC RID: 14076 RVA: 0x0001ABCB File Offset: 0x00018DCB
		[DataMember]
		public DateTime ClassStartTime { get; set; }

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x060036FD RID: 14077 RVA: 0x0001ABD4 File Offset: 0x00018DD4
		// (set) Token: 0x060036FE RID: 14078 RVA: 0x0001ABDC File Offset: 0x00018DDC
		[DataMember]
		public DateTime ClassEndTime { get; set; }

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x060036FF RID: 14079 RVA: 0x0001ABE5 File Offset: 0x00018DE5
		// (set) Token: 0x06003700 RID: 14080 RVA: 0x0001ABED File Offset: 0x00018DED
		[DataMember]
		public string ClassLocation { get; set; }

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x06003701 RID: 14081 RVA: 0x0001ABF6 File Offset: 0x00018DF6
		// (set) Token: 0x06003702 RID: 14082 RVA: 0x0001ABFE File Offset: 0x00018DFE
		[DataMember]
		public bool Cancelled { get; set; }

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x06003703 RID: 14083 RVA: 0x0001AC07 File Offset: 0x00018E07
		// (set) Token: 0x06003704 RID: 14084 RVA: 0x0001AC0F File Offset: 0x00018E0F
		[DataMember]
		public bool NoShow { get; set; }

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x06003705 RID: 14085 RVA: 0x0001AC18 File Offset: 0x00018E18
		public virtual bool IsTentative
		{
			get
			{
				return this.AppCode == -1;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x06003706 RID: 14086 RVA: 0x0001AC33 File Offset: 0x00018E33
		// (set) Token: 0x06003707 RID: 14087 RVA: 0x0001AC3B File Offset: 0x00018E3B
		[DataMember]
		public DateTime? ActualDate { get; set; }

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x06003708 RID: 14088 RVA: 0x0001AC44 File Offset: 0x00018E44
		// (set) Token: 0x06003709 RID: 14089 RVA: 0x0001AC4C File Offset: 0x00018E4C
		[DataMember]
		public DateTime? ActualStartTime { get; set; }

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x0600370A RID: 14090 RVA: 0x0001AC55 File Offset: 0x00018E55
		// (set) Token: 0x0600370B RID: 14091 RVA: 0x0001AC5D File Offset: 0x00018E5D
		[DataMember]
		public DateTime? ActualEndTime { get; set; }

		// Token: 0x170013FA RID: 5114
		// (get) Token: 0x0600370C RID: 14092 RVA: 0x0001AC66 File Offset: 0x00018E66
		// (set) Token: 0x0600370D RID: 14093 RVA: 0x0001AC6E File Offset: 0x00018E6E
		[DataMember]
		public DateTime? ProjectedActualEndTime { get; set; }

		// Token: 0x170013FB RID: 5115
		// (get) Token: 0x0600370E RID: 14094 RVA: 0x0001AC77 File Offset: 0x00018E77
		// (set) Token: 0x0600370F RID: 14095 RVA: 0x0001AC7F File Offset: 0x00018E7F
		[DataMember]
		public bool TestWasDelivered { get; set; }

		// Token: 0x170013FC RID: 5116
		// (get) Token: 0x06003710 RID: 14096 RVA: 0x0001AC88 File Offset: 0x00018E88
		// (set) Token: 0x06003711 RID: 14097 RVA: 0x0001AC90 File Offset: 0x00018E90
		[DataMember]
		public string TestDelivered { get; set; }

		// Token: 0x170013FD RID: 5117
		// (get) Token: 0x06003712 RID: 14098 RVA: 0x0001AC99 File Offset: 0x00018E99
		// (set) Token: 0x06003713 RID: 14099 RVA: 0x0001ACA1 File Offset: 0x00018EA1
		[DataMember]
		public string ExamStatus { get; set; }

		// Token: 0x170013FE RID: 5118
		// (get) Token: 0x06003714 RID: 14100 RVA: 0x0001ACAA File Offset: 0x00018EAA
		// (set) Token: 0x06003715 RID: 14101 RVA: 0x0001ACB2 File Offset: 0x00018EB2
		[DataMember]
		public int ColourArgB { get; set; }

		// Token: 0x170013FF RID: 5119
		// (get) Token: 0x06003716 RID: 14102 RVA: 0x0001ACBB File Offset: 0x00018EBB
		// (set) Token: 0x06003717 RID: 14103 RVA: 0x0001ACC3 File Offset: 0x00018EC3
		[DataMember]
		public string Custom1 { get; set; }

		// Token: 0x17001400 RID: 5120
		// (get) Token: 0x06003718 RID: 14104 RVA: 0x0001ACCC File Offset: 0x00018ECC
		// (set) Token: 0x06003719 RID: 14105 RVA: 0x0001ACD4 File Offset: 0x00018ED4
		[DataMember]
		public string Custom2 { get; set; }

		// Token: 0x17001401 RID: 5121
		// (get) Token: 0x0600371A RID: 14106 RVA: 0x0001ACDD File Offset: 0x00018EDD
		// (set) Token: 0x0600371B RID: 14107 RVA: 0x0001ACE5 File Offset: 0x00018EE5
		[DataMember]
		public string Custom3 { get; set; }

		// Token: 0x17001402 RID: 5122
		// (get) Token: 0x0600371C RID: 14108 RVA: 0x0001ACEE File Offset: 0x00018EEE
		// (set) Token: 0x0600371D RID: 14109 RVA: 0x0001ACF6 File Offset: 0x00018EF6
		[DataMember]
		public string Custom4 { get; set; }

		// Token: 0x17001403 RID: 5123
		// (get) Token: 0x0600371E RID: 14110 RVA: 0x0001ACFF File Offset: 0x00018EFF
		// (set) Token: 0x0600371F RID: 14111 RVA: 0x0001AD07 File Offset: 0x00018F07
		[DataMember]
		public string Custom5 { get; set; }

		// Token: 0x17001404 RID: 5124
		// (get) Token: 0x06003720 RID: 14112 RVA: 0x0001AD10 File Offset: 0x00018F10
		// (set) Token: 0x06003721 RID: 14113 RVA: 0x0001AD18 File Offset: 0x00018F18
		[DataMember]
		public string Custom6 { get; set; }

		// Token: 0x17001405 RID: 5125
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x0001AD21 File Offset: 0x00018F21
		// (set) Token: 0x06003723 RID: 14115 RVA: 0x0001AD29 File Offset: 0x00018F29
		[DataMember]
		public string Custom7 { get; set; }

		// Token: 0x17001406 RID: 5126
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x0001AD32 File Offset: 0x00018F32
		// (set) Token: 0x06003725 RID: 14117 RVA: 0x0001AD3A File Offset: 0x00018F3A
		[DataMember]
		public string Custom8 { get; set; }

		// Token: 0x17001407 RID: 5127
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x0001AD43 File Offset: 0x00018F43
		// (set) Token: 0x06003727 RID: 14119 RVA: 0x0001AD4B File Offset: 0x00018F4B
		[DataMember]
		public string Custom9 { get; set; }

		// Token: 0x17001408 RID: 5128
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x0001AD54 File Offset: 0x00018F54
		// (set) Token: 0x06003729 RID: 14121 RVA: 0x0001AD5C File Offset: 0x00018F5C
		[DataMember]
		public string Custom10 { get; set; }

		// Token: 0x17001409 RID: 5129
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x0001AD65 File Offset: 0x00018F65
		// (set) Token: 0x0600372B RID: 14123 RVA: 0x0001AD6D File Offset: 0x00018F6D
		[DataMember]
		public string Custom11 { get; set; }

		// Token: 0x1700140A RID: 5130
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x0001AD76 File Offset: 0x00018F76
		// (set) Token: 0x0600372D RID: 14125 RVA: 0x0001AD7E File Offset: 0x00018F7E
		[DataMember]
		public string Custom12 { get; set; }

		// Token: 0x1700140B RID: 5131
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x0001AD87 File Offset: 0x00018F87
		// (set) Token: 0x0600372F RID: 14127 RVA: 0x0001AD8F File Offset: 0x00018F8F
		[DataMember]
		public string Custom13 { get; set; }

		// Token: 0x1700140C RID: 5132
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x0001AD98 File Offset: 0x00018F98
		// (set) Token: 0x06003731 RID: 14129 RVA: 0x0001ADA0 File Offset: 0x00018FA0
		[DataMember]
		public string Custom14 { get; set; }

		// Token: 0x1700140D RID: 5133
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x0001ADA9 File Offset: 0x00018FA9
		// (set) Token: 0x06003733 RID: 14131 RVA: 0x0001ADB1 File Offset: 0x00018FB1
		[DataMember]
		public string Custom15 { get; set; }

		// Token: 0x1700140E RID: 5134
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x0001ADBA File Offset: 0x00018FBA
		// (set) Token: 0x06003735 RID: 14133 RVA: 0x0001ADC2 File Offset: 0x00018FC2
		[DataMember]
		public string Custom16 { get; set; }

		// Token: 0x1700140F RID: 5135
		// (get) Token: 0x06003736 RID: 14134 RVA: 0x0001ADCB File Offset: 0x00018FCB
		// (set) Token: 0x06003737 RID: 14135 RVA: 0x0001ADD3 File Offset: 0x00018FD3
		[DataMember]
		public string Custom17 { get; set; }

		// Token: 0x17001410 RID: 5136
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x0001ADDC File Offset: 0x00018FDC
		// (set) Token: 0x06003739 RID: 14137 RVA: 0x0001ADE4 File Offset: 0x00018FE4
		[DataMember]
		public string Custom18 { get; set; }

		// Token: 0x17001411 RID: 5137
		// (get) Token: 0x0600373A RID: 14138 RVA: 0x0001ADED File Offset: 0x00018FED
		// (set) Token: 0x0600373B RID: 14139 RVA: 0x0001ADF5 File Offset: 0x00018FF5
		[DataMember]
		public string Custom19 { get; set; }

		// Token: 0x17001412 RID: 5138
		// (get) Token: 0x0600373C RID: 14140 RVA: 0x0001ADFE File Offset: 0x00018FFE
		// (set) Token: 0x0600373D RID: 14141 RVA: 0x0001AE06 File Offset: 0x00019006
		[DataMember]
		public string Custom20 { get; set; }
	}
}
