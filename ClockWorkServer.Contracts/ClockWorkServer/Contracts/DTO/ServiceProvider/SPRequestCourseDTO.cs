using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000277 RID: 631
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPRequestCourseDTO
	{
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x000070B1 File Offset: 0x000052B1
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x000070B9 File Offset: 0x000052B9
		[DataMember]
		public int SPRequestCourseId { get; set; }

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x06000EF5 RID: 3829 RVA: 0x000070C2 File Offset: 0x000052C2
		// (set) Token: 0x06000EF6 RID: 3830 RVA: 0x000070CA File Offset: 0x000052CA
		[DataMember]
		public LookupCourseBaseDTO Course { get; set; }

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x06000EF7 RID: 3831 RVA: 0x000070D3 File Offset: 0x000052D3
		// (set) Token: 0x06000EF8 RID: 3832 RVA: 0x000070DB File Offset: 0x000052DB
		[DataMember]
		public SPRequestStatusTypeDTO RequestStatus { get; set; }

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x06000EF9 RID: 3833 RVA: 0x000070E4 File Offset: 0x000052E4
		// (set) Token: 0x06000EFA RID: 3834 RVA: 0x000070EC File Offset: 0x000052EC
		[DataMember]
		public SPRequestAssignmentStatusTypeDTO AssignmentStatus { get; set; }

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x06000EFB RID: 3835 RVA: 0x000070F5 File Offset: 0x000052F5
		// (set) Token: 0x06000EFC RID: 3836 RVA: 0x000070FD File Offset: 0x000052FD
		[DataMember]
		public SPUrgencyLevelTypeDTO UrgencyLevel { get; set; }

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x00007106 File Offset: 0x00005306
		// (set) Token: 0x06000EFE RID: 3838 RVA: 0x0000710E File Offset: 0x0000530E
		[DataMember]
		public SPRequestCourseAssignmentDTO Assignment { get; set; }

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x00007117 File Offset: 0x00005317
		// (set) Token: 0x06000F00 RID: 3840 RVA: 0x0000711F File Offset: 0x0000531F
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x00007128 File Offset: 0x00005328
		// (set) Token: 0x06000F02 RID: 3842 RVA: 0x00007130 File Offset: 0x00005330
		[DataMember]
		public bool IsRequired { get; set; }
	}
}
