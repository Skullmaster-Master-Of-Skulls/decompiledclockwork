using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000708 RID: 1800
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalCourseInstructorDTO
	{
		// Token: 0x17000CAA RID: 3242
		// (get) Token: 0x060024D8 RID: 9432 RVA: 0x00010D26 File Offset: 0x0000EF26
		// (set) Token: 0x060024D9 RID: 9433 RVA: 0x00010D2E File Offset: 0x0000EF2E
		[DataMember]
		public string ExternalInstructorId { get; set; }

		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x060024DA RID: 9434 RVA: 0x00010D37 File Offset: 0x0000EF37
		// (set) Token: 0x060024DB RID: 9435 RVA: 0x00010D3F File Offset: 0x0000EF3F
		[DataMember]
		public LookupInstructorDTO ClockWorkInstructor { get; set; }

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x060024DC RID: 9436 RVA: 0x00010D48 File Offset: 0x0000EF48
		// (set) Token: 0x060024DD RID: 9437 RVA: 0x00010D50 File Offset: 0x0000EF50
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x060024DE RID: 9438 RVA: 0x00010D59 File Offset: 0x0000EF59
		// (set) Token: 0x060024DF RID: 9439 RVA: 0x00010D61 File Offset: 0x0000EF61
		[DataMember]
		public string Email { get; set; }

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x060024E0 RID: 9440 RVA: 0x00010D6A File Offset: 0x0000EF6A
		// (set) Token: 0x060024E1 RID: 9441 RVA: 0x00010D72 File Offset: 0x0000EF72
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x060024E2 RID: 9442 RVA: 0x00010D7B File Offset: 0x0000EF7B
		// (set) Token: 0x060024E3 RID: 9443 RVA: 0x00010D83 File Offset: 0x0000EF83
		[DataMember]
		public string EmployeeId { get; set; }

		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x060024E4 RID: 9444 RVA: 0x00010D8C File Offset: 0x0000EF8C
		// (set) Token: 0x060024E5 RID: 9445 RVA: 0x00010D94 File Offset: 0x0000EF94
		[DataMember]
		public string Phone { get; set; }

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x060024E6 RID: 9446 RVA: 0x00010D9D File Offset: 0x0000EF9D
		// (set) Token: 0x060024E7 RID: 9447 RVA: 0x00010DA5 File Offset: 0x0000EFA5
		[DataMember]
		public bool IsPrimary { get; set; }

		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x00010DAE File Offset: 0x0000EFAE
		// (set) Token: 0x060024E9 RID: 9449 RVA: 0x00010DB6 File Offset: 0x0000EFB6
		[DataMember]
		public int Percentage { get; set; }
	}
}
