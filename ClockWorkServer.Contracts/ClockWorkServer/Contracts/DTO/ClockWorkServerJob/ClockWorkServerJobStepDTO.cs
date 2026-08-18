using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000849 RID: 2121
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerJobStepDTO
	{
		// Token: 0x17000F2B RID: 3883
		// (get) Token: 0x06002B41 RID: 11073 RVA: 0x0001489E File Offset: 0x00012A9E
		// (set) Token: 0x06002B42 RID: 11074 RVA: 0x000148A6 File Offset: 0x00012AA6
		[DataMember]
		public int StepId { get; set; }

		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06002B43 RID: 11075 RVA: 0x000148AF File Offset: 0x00012AAF
		// (set) Token: 0x06002B44 RID: 11076 RVA: 0x000148B7 File Offset: 0x00012AB7
		[DataMember]
		public int JobId { get; set; }

		// Token: 0x17000F2D RID: 3885
		// (get) Token: 0x06002B45 RID: 11077 RVA: 0x000148C0 File Offset: 0x00012AC0
		// (set) Token: 0x06002B46 RID: 11078 RVA: 0x000148C8 File Offset: 0x00012AC8
		[DataMember]
		public string JobType { get; set; }

		// Token: 0x17000F2E RID: 3886
		// (get) Token: 0x06002B47 RID: 11079 RVA: 0x000148D1 File Offset: 0x00012AD1
		// (set) Token: 0x06002B48 RID: 11080 RVA: 0x000148D9 File Offset: 0x00012AD9
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000F2F RID: 3887
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x000148E2 File Offset: 0x00012AE2
		// (set) Token: 0x06002B4A RID: 11082 RVA: 0x000148EA File Offset: 0x00012AEA
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x000148F3 File Offset: 0x00012AF3
		// (set) Token: 0x06002B4C RID: 11084 RVA: 0x000148FB File Offset: 0x00012AFB
		[DataMember]
		public string Parameters { get; set; }

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x00014904 File Offset: 0x00012B04
		// (set) Token: 0x06002B4E RID: 11086 RVA: 0x0001490C File Offset: 0x00012B0C
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x00014915 File Offset: 0x00012B15
		// (set) Token: 0x06002B50 RID: 11088 RVA: 0x0001491D File Offset: 0x00012B1D
		[DataMember]
		public bool IsActive { get; set; }
	}
}
