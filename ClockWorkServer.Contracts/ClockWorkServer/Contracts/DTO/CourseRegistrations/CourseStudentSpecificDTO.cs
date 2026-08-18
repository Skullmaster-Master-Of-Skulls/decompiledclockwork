using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000833 RID: 2099
	[DataContract(Namespace = "http://tpro.ca")]
	public class CourseStudentSpecificDTO
	{
		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x00014518 File Offset: 0x00012718
		// (set) Token: 0x06002AC9 RID: 10953 RVA: 0x00014520 File Offset: 0x00012720
		[DataMember]
		public string GradeLetter { get; set; }

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x00014529 File Offset: 0x00012729
		// (set) Token: 0x06002ACB RID: 10955 RVA: 0x00014531 File Offset: 0x00012731
		[DataMember]
		public string InProgressGradeLetter { get; set; }

		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06002ACC RID: 10956 RVA: 0x0001453A File Offset: 0x0001273A
		// (set) Token: 0x06002ACD RID: 10957 RVA: 0x00014542 File Offset: 0x00012742
		[DataMember]
		public double Grade { get; set; }

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06002ACE RID: 10958 RVA: 0x0001454B File Offset: 0x0001274B
		// (set) Token: 0x06002ACF RID: 10959 RVA: 0x00014553 File Offset: 0x00012753
		[DataMember]
		public double InProgressGrade { get; set; }

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06002AD0 RID: 10960 RVA: 0x0001455C File Offset: 0x0001275C
		// (set) Token: 0x06002AD1 RID: 10961 RVA: 0x00014564 File Offset: 0x00012764
		[DataMember]
		public double TuitionCost { get; set; }

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x0001456D File Offset: 0x0001276D
		// (set) Token: 0x06002AD3 RID: 10963 RVA: 0x00014575 File Offset: 0x00012775
		[DataMember]
		public DateTime? RegistrationDate { get; set; }

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x0001457E File Offset: 0x0001277E
		// (set) Token: 0x06002AD5 RID: 10965 RVA: 0x00014586 File Offset: 0x00012786
		[DataMember]
		public string RegistrationNote { get; set; }
	}
}
