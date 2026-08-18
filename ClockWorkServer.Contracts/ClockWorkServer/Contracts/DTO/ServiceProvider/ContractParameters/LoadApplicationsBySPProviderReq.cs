using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x0200028D RID: 653
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadApplicationsBySPProviderReq : BaseMessageReq
	{
		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000F7D RID: 3965 RVA: 0x0000748B File Offset: 0x0000568B
		// (set) Token: 0x06000F7E RID: 3966 RVA: 0x00007493 File Offset: 0x00005693
		[DataMember]
		public int SPProviderId { get; set; }

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000F7F RID: 3967 RVA: 0x0000749C File Offset: 0x0000569C
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x000074A4 File Offset: 0x000056A4
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000F81 RID: 3969 RVA: 0x000074AD File Offset: 0x000056AD
		// (set) Token: 0x06000F82 RID: 3970 RVA: 0x000074B5 File Offset: 0x000056B5
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000F83 RID: 3971 RVA: 0x000074BE File Offset: 0x000056BE
		// (set) Token: 0x06000F84 RID: 3972 RVA: 0x000074C6 File Offset: 0x000056C6
		[DataMember]
		public bool IncludeInactiveApplications { get; set; }
	}
}
