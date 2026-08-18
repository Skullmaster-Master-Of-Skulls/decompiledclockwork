using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Intake
{
	// Token: 0x020005EC RID: 1516
	[DataContract(Namespace = "http://tpro.ca")]
	public class IntakeStatusDTO
	{
		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06001EE9 RID: 7913 RVA: 0x0000E0BA File Offset: 0x0000C2BA
		// (set) Token: 0x06001EEA RID: 7914 RVA: 0x0000E0C2 File Offset: 0x0000C2C2
		[DataMember]
		public Guid IntakeStatusId { get; set; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06001EEB RID: 7915 RVA: 0x0000E0CB File Offset: 0x0000C2CB
		// (set) Token: 0x06001EEC RID: 7916 RVA: 0x0000E0D3 File Offset: 0x0000C2D3
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x0000E0DC File Offset: 0x0000C2DC
		// (set) Token: 0x06001EEE RID: 7918 RVA: 0x0000E0E4 File Offset: 0x0000C2E4
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x0000E0ED File Offset: 0x0000C2ED
		// (set) Token: 0x06001EF0 RID: 7920 RVA: 0x0000E0F5 File Offset: 0x0000C2F5
		[DataMember]
		public int BackgroundColor { get; set; }

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x0000E0FE File Offset: 0x0000C2FE
		// (set) Token: 0x06001EF2 RID: 7922 RVA: 0x0000E106 File Offset: 0x0000C306
		[DataMember]
		public bool IsInactive { get; set; }

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x0000E10F File Offset: 0x0000C30F
		// (set) Token: 0x06001EF4 RID: 7924 RVA: 0x0000E117 File Offset: 0x0000C317
		[DataMember]
		public int OrderNum { get; set; }
	}
}
