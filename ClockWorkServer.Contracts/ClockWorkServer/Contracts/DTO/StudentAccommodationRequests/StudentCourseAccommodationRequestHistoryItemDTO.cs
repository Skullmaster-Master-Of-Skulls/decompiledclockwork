using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x0200025A RID: 602
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentCourseAccommodationRequestHistoryItemDTO
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000DAC RID: 3500 RVA: 0x000066CC File Offset: 0x000048CC
		// (set) Token: 0x06000DAD RID: 3501 RVA: 0x000066D4 File Offset: 0x000048D4
		[DataMember]
		public virtual int PersonId { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000066DD File Offset: 0x000048DD
		// (set) Token: 0x06000DAF RID: 3503 RVA: 0x000066E5 File Offset: 0x000048E5
		[DataMember]
		public virtual int LuCourseId { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000DB0 RID: 3504 RVA: 0x000066EE File Offset: 0x000048EE
		// (set) Token: 0x06000DB1 RID: 3505 RVA: 0x000066F6 File Offset: 0x000048F6
		[DataMember]
		public int StudentCourseAccommodationRequestId { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000DB2 RID: 3506 RVA: 0x000066FF File Offset: 0x000048FF
		// (set) Token: 0x06000DB3 RID: 3507 RVA: 0x00006707 File Offset: 0x00004907
		[DataMember]
		public eStudentCourseAccommodationRequestHistoryItemHowModified HowModified { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000DB4 RID: 3508 RVA: 0x00006710 File Offset: 0x00004910
		// (set) Token: 0x06000DB5 RID: 3509 RVA: 0x00006718 File Offset: 0x00004918
		[DataMember]
		public DateTime DateModified { get; set; }

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000DB6 RID: 3510 RVA: 0x00006721 File Offset: 0x00004921
		// (set) Token: 0x06000DB7 RID: 3511 RVA: 0x00006729 File Offset: 0x00004929
		[DataMember]
		public PersonBaseDTO WhoModified { get; set; }

		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000DB8 RID: 3512 RVA: 0x00006732 File Offset: 0x00004932
		// (set) Token: 0x06000DB9 RID: 3513 RVA: 0x0000673A File Offset: 0x0000493A
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }

		// Token: 0x1700037A RID: 890
		// (get) Token: 0x06000DBA RID: 3514 RVA: 0x00006743 File Offset: 0x00004943
		// (set) Token: 0x06000DBB RID: 3515 RVA: 0x0000674B File Offset: 0x0000494B
		[DataMember]
		public eStudentCourseAccommodationRequestStatus Status { get; set; }

		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06000DBC RID: 3516 RVA: 0x00006754 File Offset: 0x00004954
		// (set) Token: 0x06000DBD RID: 3517 RVA: 0x0000675C File Offset: 0x0000495C
		[DataMember]
		public bool AccommodationChangesRequested { get; set; }

		// Token: 0x1700037C RID: 892
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00006765 File Offset: 0x00004965
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0000676D File Offset: 0x0000496D
		[DataMember]
		public bool AdditionalAccommodationsRequested { get; set; }

		// Token: 0x1700037D RID: 893
		// (get) Token: 0x06000DC0 RID: 3520 RVA: 0x00006776 File Offset: 0x00004976
		// (set) Token: 0x06000DC1 RID: 3521 RVA: 0x0000677E File Offset: 0x0000497E
		[DataMember]
		public string Note1 { get; set; }

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x00006787 File Offset: 0x00004987
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x0000678F File Offset: 0x0000498F
		[DataMember]
		public string Note2 { get; set; }

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x00006798 File Offset: 0x00004998
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x000067A0 File Offset: 0x000049A0
		[DataMember]
		public DateTime DateRequested { get; set; }
	}
}
