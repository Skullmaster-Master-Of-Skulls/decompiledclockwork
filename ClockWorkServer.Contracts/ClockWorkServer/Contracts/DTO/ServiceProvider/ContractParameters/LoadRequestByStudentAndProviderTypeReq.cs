using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002BF RID: 703
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestByStudentAndProviderTypeReq : BaseMessageReq
	{
		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001011 RID: 4113 RVA: 0x000077CC File Offset: 0x000059CC
		// (set) Token: 0x06001012 RID: 4114 RVA: 0x000077D4 File Offset: 0x000059D4
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x000077DD File Offset: 0x000059DD
		// (set) Token: 0x06001014 RID: 4116 RVA: 0x000077E5 File Offset: 0x000059E5
		[DataMember]
		public int SPProviderTypeId { get; set; }

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x000077EE File Offset: 0x000059EE
		// (set) Token: 0x06001016 RID: 4118 RVA: 0x000077F6 File Offset: 0x000059F6
		[DataMember]
		public bool IncludeSubItems { get; set; }
	}
}
