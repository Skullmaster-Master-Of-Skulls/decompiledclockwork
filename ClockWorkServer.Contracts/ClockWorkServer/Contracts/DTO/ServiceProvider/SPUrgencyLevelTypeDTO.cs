using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x0200027D RID: 637
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPUrgencyLevelTypeDTO
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000F41 RID: 3905 RVA: 0x00007315 File Offset: 0x00005515
		// (set) Token: 0x06000F42 RID: 3906 RVA: 0x0000731D File Offset: 0x0000551D
		[DataMember]
		public int SPUrgencyLevelTypeId { get; set; }

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06000F43 RID: 3907 RVA: 0x00007326 File Offset: 0x00005526
		// (set) Token: 0x06000F44 RID: 3908 RVA: 0x0000732E File Offset: 0x0000552E
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x00007337 File Offset: 0x00005537
		// (set) Token: 0x06000F46 RID: 3910 RVA: 0x0000733F File Offset: 0x0000553F
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x00007348 File Offset: 0x00005548
		// (set) Token: 0x06000F48 RID: 3912 RVA: 0x00007350 File Offset: 0x00005550
		[DataMember]
		public int Urgency { get; set; }
	}
}
