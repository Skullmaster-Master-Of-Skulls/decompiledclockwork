using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200034F RID: 847
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportUpdateStudentPreviewReq
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x00009103 File Offset: 0x00007303
		// (set) Token: 0x06001364 RID: 4964 RVA: 0x0000910B File Offset: 0x0000730B
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x00009114 File Offset: 0x00007314
		// (set) Token: 0x06001366 RID: 4966 RVA: 0x0000911C File Offset: 0x0000731C
		[DataMember]
		public int ReportId_Preview { get; set; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x00009125 File Offset: 0x00007325
		// (set) Token: 0x06001368 RID: 4968 RVA: 0x0000912D File Offset: 0x0000732D
		[DataMember]
		public int ReportId_Import { get; set; }

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x00009136 File Offset: 0x00007336
		// (set) Token: 0x0600136A RID: 4970 RVA: 0x0000913E File Offset: 0x0000733E
		[DataMember]
		public int ReportId_GetGroups { get; set; }

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x0600136B RID: 4971 RVA: 0x00009147 File Offset: 0x00007347
		// (set) Token: 0x0600136C RID: 4972 RVA: 0x0000914F File Offset: 0x0000734F
		[DataMember]
		public int ReportId_ImportCourses { get; set; }

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x0600136D RID: 4973 RVA: 0x00009158 File Offset: 0x00007358
		// (set) Token: 0x0600136E RID: 4974 RVA: 0x00009160 File Offset: 0x00007360
		[DataMember]
		public string OverridePassword { get; set; }
	}
}
