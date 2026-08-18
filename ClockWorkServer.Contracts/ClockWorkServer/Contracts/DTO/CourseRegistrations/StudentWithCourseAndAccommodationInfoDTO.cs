using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000835 RID: 2101
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentWithCourseAndAccommodationInfoDTO
	{
		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x000145B1 File Offset: 0x000127B1
		// (set) Token: 0x06002ADD RID: 10973 RVA: 0x000145B9 File Offset: 0x000127B9
		[DataMember]
		public BasicPersonDTO Student { get; set; }

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06002ADE RID: 10974 RVA: 0x000145C2 File Offset: 0x000127C2
		// (set) Token: 0x06002ADF RID: 10975 RVA: 0x000145CA File Offset: 0x000127CA
		[DataMember]
		public LookupCourseBaseDTO CourseBase { get; set; }

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06002AE0 RID: 10976 RVA: 0x000145D3 File Offset: 0x000127D3
		// (set) Token: 0x06002AE1 RID: 10977 RVA: 0x000145DB File Offset: 0x000127DB
		[DataMember]
		public DateTime? DateLetterIssued { get; set; }

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000145E4 File Offset: 0x000127E4
		// (set) Token: 0x06002AE3 RID: 10979 RVA: 0x000145EC File Offset: 0x000127EC
		[DataMember]
		public DateTime? DateLetterReturned { get; set; }

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000145F5 File Offset: 0x000127F5
		// (set) Token: 0x06002AE5 RID: 10981 RVA: 0x000145FD File Offset: 0x000127FD
		[DataMember]
		public bool SelfRegIsApproved { get; set; }

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06002AE6 RID: 10982 RVA: 0x00014606 File Offset: 0x00012806
		// (set) Token: 0x06002AE7 RID: 10983 RVA: 0x0001460E File Offset: 0x0001280E
		[DataMember]
		public DateTime? AccommodationExpiryDate { get; set; }

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x00014617 File Offset: 0x00012817
		// (set) Token: 0x06002AE9 RID: 10985 RVA: 0x0001461F File Offset: 0x0001281F
		[DataMember]
		public bool NoInstructorViewEnabled { get; set; }
	}
}
