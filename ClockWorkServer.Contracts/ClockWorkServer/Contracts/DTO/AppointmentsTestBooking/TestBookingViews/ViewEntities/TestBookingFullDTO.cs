using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x02000A46 RID: 2630
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestBookingFullDTO : TestBookingSmallDTO
	{
		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x0600365F RID: 13919 RVA: 0x0001A5B0 File Offset: 0x000187B0
		// (set) Token: 0x06003660 RID: 13920 RVA: 0x0001A5B8 File Offset: 0x000187B8
		[DataMember]
		public int InvigilatorPid { get; set; }

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x06003661 RID: 13921 RVA: 0x0001A5C1 File Offset: 0x000187C1
		// (set) Token: 0x06003662 RID: 13922 RVA: 0x0001A5C9 File Offset: 0x000187C9
		[DataMember]
		public int SittingId { get; set; }

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06003663 RID: 13923 RVA: 0x0001A5D2 File Offset: 0x000187D2
		// (set) Token: 0x06003664 RID: 13924 RVA: 0x0001A5DA File Offset: 0x000187DA
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x06003665 RID: 13925 RVA: 0x0001A5E3 File Offset: 0x000187E3
		// (set) Token: 0x06003666 RID: 13926 RVA: 0x0001A5EB File Offset: 0x000187EB
		[DataMember]
		public bool InstructorSubmitted { get; set; }

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06003667 RID: 13927 RVA: 0x0001A5F4 File Offset: 0x000187F4
		// (set) Token: 0x06003668 RID: 13928 RVA: 0x0001A5FC File Offset: 0x000187FC
		[DataMember]
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06003669 RID: 13929 RVA: 0x0001A605 File Offset: 0x00018805
		// (set) Token: 0x0600366A RID: 13930 RVA: 0x0001A60D File Offset: 0x0001880D
		[DataMember]
		public DateTime? CourseStartDate { get; set; }

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x0600366B RID: 13931 RVA: 0x0001A616 File Offset: 0x00018816
		// (set) Token: 0x0600366C RID: 13932 RVA: 0x0001A61E File Offset: 0x0001881E
		[DataMember]
		public DateTime? CourseEndDate { get; set; }

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x0600366D RID: 13933 RVA: 0x0001A627 File Offset: 0x00018827
		// (set) Token: 0x0600366E RID: 13934 RVA: 0x0001A62F File Offset: 0x0001882F
		[DataMember]
		public string Department { get; set; }

		// Token: 0x170013A9 RID: 5033
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x0001A638 File Offset: 0x00018838
		// (set) Token: 0x06003670 RID: 13936 RVA: 0x0001A640 File Offset: 0x00018840
		[DataMember]
		public string DepartmentEmail { get; set; }

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x0001A649 File Offset: 0x00018849
		// (set) Token: 0x06003672 RID: 13938 RVA: 0x0001A651 File Offset: 0x00018851
		[DataMember]
		public string DepartmentCode { get; set; }

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x0001A65A File Offset: 0x0001885A
		// (set) Token: 0x06003674 RID: 13940 RVA: 0x0001A662 File Offset: 0x00018862
		[DataMember]
		public string PrimaryInstructor { get; set; }

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06003675 RID: 13941 RVA: 0x0001A66B File Offset: 0x0001886B
		// (set) Token: 0x06003676 RID: 13942 RVA: 0x0001A673 File Offset: 0x00018873
		[DataMember]
		public string PrimaryInstructorEmail { get; set; }

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06003677 RID: 13943 RVA: 0x0001A67C File Offset: 0x0001887C
		// (set) Token: 0x06003678 RID: 13944 RVA: 0x0001A684 File Offset: 0x00018884
		[DataMember]
		public string PrimaryInstructorPhone { get; set; }

		// Token: 0x170013AE RID: 5038
		// (get) Token: 0x06003679 RID: 13945 RVA: 0x0001A68D File Offset: 0x0001888D
		// (set) Token: 0x0600367A RID: 13946 RVA: 0x0001A695 File Offset: 0x00018895
		[DataMember]
		public string ExamAccommodations { get; set; }

		// Token: 0x170013AF RID: 5039
		// (get) Token: 0x0600367B RID: 13947 RVA: 0x0001A69E File Offset: 0x0001889E
		// (set) Token: 0x0600367C RID: 13948 RVA: 0x0001A6A6 File Offset: 0x000188A6
		[DataMember]
		public string AccommodationGroups { get; set; }

		// Token: 0x170013B0 RID: 5040
		// (get) Token: 0x0600367D RID: 13949 RVA: 0x0001A6AF File Offset: 0x000188AF
		// (set) Token: 0x0600367E RID: 13950 RVA: 0x0001A6B7 File Offset: 0x000188B7
		[DataMember]
		public int TotalBreakMinutes { get; set; }

		// Token: 0x170013B1 RID: 5041
		// (get) Token: 0x0600367F RID: 13951 RVA: 0x0001A6C0 File Offset: 0x000188C0
		// (set) Token: 0x06003680 RID: 13952 RVA: 0x0001A6C8 File Offset: 0x000188C8
		[DataMember]
		public string AssignedAdvisor { get; set; }

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x06003681 RID: 13953 RVA: 0x0001A6D1 File Offset: 0x000188D1
		// (set) Token: 0x06003682 RID: 13954 RVA: 0x0001A6D9 File Offset: 0x000188D9
		[DataMember]
		public string AssignedAdvisorFirstName { get; set; }

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x0001A6E2 File Offset: 0x000188E2
		// (set) Token: 0x06003684 RID: 13956 RVA: 0x0001A6EA File Offset: 0x000188EA
		[DataMember]
		public string AssignedAdvisorLastName { get; set; }

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x06003685 RID: 13957 RVA: 0x0001A6F3 File Offset: 0x000188F3
		// (set) Token: 0x06003686 RID: 13958 RVA: 0x0001A6FB File Offset: 0x000188FB
		[DataMember]
		public string Invigilator { get; set; }

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x06003687 RID: 13959 RVA: 0x0001A704 File Offset: 0x00018904
		// (set) Token: 0x06003688 RID: 13960 RVA: 0x0001A70C File Offset: 0x0001890C
		[DataMember]
		public string InvigilatorFirstName { get; set; }

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x06003689 RID: 13961 RVA: 0x0001A715 File Offset: 0x00018915
		// (set) Token: 0x0600368A RID: 13962 RVA: 0x0001A71D File Offset: 0x0001891D
		[DataMember]
		public string InvigilatorLastName { get; set; }

		// Token: 0x170013B7 RID: 5047
		// (get) Token: 0x0600368B RID: 13963 RVA: 0x0001A726 File Offset: 0x00018926
		// (set) Token: 0x0600368C RID: 13964 RVA: 0x0001A72E File Offset: 0x0001892E
		[DataMember]
		public DateTime? DateAdded { get; set; }

		// Token: 0x170013B8 RID: 5048
		// (get) Token: 0x0600368D RID: 13965 RVA: 0x0001A737 File Offset: 0x00018937
		// (set) Token: 0x0600368E RID: 13966 RVA: 0x0001A73F File Offset: 0x0001893F
		[DataMember]
		public string WhoBookedFirst { get; set; }

		// Token: 0x170013B9 RID: 5049
		// (get) Token: 0x0600368F RID: 13967 RVA: 0x0001A748 File Offset: 0x00018948
		// (set) Token: 0x06003690 RID: 13968 RVA: 0x0001A750 File Offset: 0x00018950
		[DataMember]
		public string WhoBookedLast { get; set; }

		// Token: 0x170013BA RID: 5050
		// (get) Token: 0x06003691 RID: 13969 RVA: 0x0001A759 File Offset: 0x00018959
		// (set) Token: 0x06003692 RID: 13970 RVA: 0x0001A761 File Offset: 0x00018961
		[DataMember]
		public string WhoBooked { get; set; }

		// Token: 0x170013BB RID: 5051
		// (get) Token: 0x06003693 RID: 13971 RVA: 0x0001A76A File Offset: 0x0001896A
		// (set) Token: 0x06003694 RID: 13972 RVA: 0x0001A772 File Offset: 0x00018972
		[DataMember]
		public DateTime? StudentReportedClassDate { get; set; }

		// Token: 0x170013BC RID: 5052
		// (get) Token: 0x06003695 RID: 13973 RVA: 0x0001A77B File Offset: 0x0001897B
		// (set) Token: 0x06003696 RID: 13974 RVA: 0x0001A783 File Offset: 0x00018983
		[DataMember]
		public DateTime? StudentReportedClassStartTime { get; set; }

		// Token: 0x170013BD RID: 5053
		// (get) Token: 0x06003697 RID: 13975 RVA: 0x0001A78C File Offset: 0x0001898C
		// (set) Token: 0x06003698 RID: 13976 RVA: 0x0001A794 File Offset: 0x00018994
		[DataMember]
		public DateTime? StudentReportedClassEndTime { get; set; }

		// Token: 0x170013BE RID: 5054
		// (get) Token: 0x06003699 RID: 13977 RVA: 0x0001A79D File Offset: 0x0001899D
		// (set) Token: 0x0600369A RID: 13978 RVA: 0x0001A7A5 File Offset: 0x000189A5
		[DataMember]
		public string AlternateContact { get; set; }

		// Token: 0x170013BF RID: 5055
		// (get) Token: 0x0600369B RID: 13979 RVA: 0x0001A7AE File Offset: 0x000189AE
		// (set) Token: 0x0600369C RID: 13980 RVA: 0x0001A7B6 File Offset: 0x000189B6
		[DataMember]
		public string AlternateContactEmail { get; set; }

		// Token: 0x170013C0 RID: 5056
		// (get) Token: 0x0600369D RID: 13981 RVA: 0x0001A7BF File Offset: 0x000189BF
		// (set) Token: 0x0600369E RID: 13982 RVA: 0x0001A7C7 File Offset: 0x000189C7
		[DataMember]
		public string AlternateContactPhone { get; set; }

		// Token: 0x170013C1 RID: 5057
		// (get) Token: 0x0600369F RID: 13983 RVA: 0x0001A7D0 File Offset: 0x000189D0
		// (set) Token: 0x060036A0 RID: 13984 RVA: 0x0001A7D8 File Offset: 0x000189D8
		[DataMember]
		public string AlternateContactUsername { get; set; }

		// Token: 0x170013C2 RID: 5058
		// (get) Token: 0x060036A1 RID: 13985 RVA: 0x0001A7E1 File Offset: 0x000189E1
		// (set) Token: 0x060036A2 RID: 13986 RVA: 0x0001A7E9 File Offset: 0x000189E9
		[DataMember]
		public int AlternateContactPermissionLevel { get; set; }

		// Token: 0x170013C3 RID: 5059
		// (get) Token: 0x060036A3 RID: 13987 RVA: 0x0001A7F2 File Offset: 0x000189F2
		// (set) Token: 0x060036A4 RID: 13988 RVA: 0x0001A7FA File Offset: 0x000189FA
		[DataMember]
		public string InstructorAcknowledgedOnline { get; set; }

		// Token: 0x170013C4 RID: 5060
		// (get) Token: 0x060036A5 RID: 13989 RVA: 0x0001A803 File Offset: 0x00018A03
		// (set) Token: 0x060036A6 RID: 13990 RVA: 0x0001A80B File Offset: 0x00018A0B
		[DataMember]
		public DateTime? InstructorAcknowledgedDate { get; set; }

		// Token: 0x170013C5 RID: 5061
		// (get) Token: 0x060036A7 RID: 13991 RVA: 0x0001A814 File Offset: 0x00018A14
		// (set) Token: 0x060036A8 RID: 13992 RVA: 0x0001A81C File Offset: 0x00018A1C
		[DataMember]
		public bool StudentReportedSameAsDefinition { get; set; }

		// Token: 0x170013C6 RID: 5062
		// (get) Token: 0x060036A9 RID: 13993 RVA: 0x0001A825 File Offset: 0x00018A25
		// (set) Token: 0x060036AA RID: 13994 RVA: 0x0001A82D File Offset: 0x00018A2D
		[DataMember]
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x170013C7 RID: 5063
		// (get) Token: 0x060036AB RID: 13995 RVA: 0x0001A836 File Offset: 0x00018A36
		// (set) Token: 0x060036AC RID: 13996 RVA: 0x0001A83E File Offset: 0x00018A3E
		[DataMember]
		public string InstructorContactedNote { get; set; }

		// Token: 0x170013C8 RID: 5064
		// (get) Token: 0x060036AD RID: 13997 RVA: 0x0001A847 File Offset: 0x00018A47
		// (set) Token: 0x060036AE RID: 13998 RVA: 0x0001A84F File Offset: 0x00018A4F
		[DataMember]
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x170013C9 RID: 5065
		// (get) Token: 0x060036AF RID: 13999 RVA: 0x0001A858 File Offset: 0x00018A58
		// (set) Token: 0x060036B0 RID: 14000 RVA: 0x0001A860 File Offset: 0x00018A60
		[DataMember]
		public string TestPickedUpNote { get; set; }

		// Token: 0x170013CA RID: 5066
		// (get) Token: 0x060036B1 RID: 14001 RVA: 0x0001A869 File Offset: 0x00018A69
		// (set) Token: 0x060036B2 RID: 14002 RVA: 0x0001A871 File Offset: 0x00018A71
		[DataMember]
		public string PrivateNote2 { get; set; }

		// Token: 0x170013CB RID: 5067
		// (get) Token: 0x060036B3 RID: 14003 RVA: 0x0001A87A File Offset: 0x00018A7A
		// (set) Token: 0x060036B4 RID: 14004 RVA: 0x0001A882 File Offset: 0x00018A82
		[DataMember]
		public string Sitting { get; set; }

		// Token: 0x170013CC RID: 5068
		// (get) Token: 0x060036B5 RID: 14005 RVA: 0x0001A88B File Offset: 0x00018A8B
		// (set) Token: 0x060036B6 RID: 14006 RVA: 0x0001A893 File Offset: 0x00018A93
		[DataMember]
		public string SittingRoom { get; set; }

		// Token: 0x170013CD RID: 5069
		// (get) Token: 0x060036B7 RID: 14007 RVA: 0x0001A89C File Offset: 0x00018A9C
		// (set) Token: 0x060036B8 RID: 14008 RVA: 0x0001A8A4 File Offset: 0x00018AA4
		[DataMember]
		public string SittingRoomFirst { get; set; }

		// Token: 0x170013CE RID: 5070
		// (get) Token: 0x060036B9 RID: 14009 RVA: 0x0001A8AD File Offset: 0x00018AAD
		// (set) Token: 0x060036BA RID: 14010 RVA: 0x0001A8B5 File Offset: 0x00018AB5
		[DataMember]
		public string SittingRoomLast { get; set; }

		// Token: 0x170013CF RID: 5071
		// (get) Token: 0x060036BB RID: 14011 RVA: 0x0001A8BE File Offset: 0x00018ABE
		// (set) Token: 0x060036BC RID: 14012 RVA: 0x0001A8C6 File Offset: 0x00018AC6
		[DataMember]
		public string SittingLocation { get; set; }

		// Token: 0x170013D0 RID: 5072
		// (get) Token: 0x060036BD RID: 14013 RVA: 0x0001A8CF File Offset: 0x00018ACF
		// (set) Token: 0x060036BE RID: 14014 RVA: 0x0001A8D7 File Offset: 0x00018AD7
		[DataMember]
		public string SittingInvigilator { get; set; }

		// Token: 0x170013D1 RID: 5073
		// (get) Token: 0x060036BF RID: 14015 RVA: 0x0001A8E0 File Offset: 0x00018AE0
		// (set) Token: 0x060036C0 RID: 14016 RVA: 0x0001A8E8 File Offset: 0x00018AE8
		[DataMember]
		public string SittingInvigilatorFirst { get; set; }

		// Token: 0x170013D2 RID: 5074
		// (get) Token: 0x060036C1 RID: 14017 RVA: 0x0001A8F1 File Offset: 0x00018AF1
		// (set) Token: 0x060036C2 RID: 14018 RVA: 0x0001A8F9 File Offset: 0x00018AF9
		[DataMember]
		public string SittingInvigilatorLast { get; set; }
	}
}
