using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200043E RID: 1086
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateNotetakerAccountReq : BaseReportMessageReq
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001760 RID: 5984 RVA: 0x0000AD29 File Offset: 0x00008F29
		// (set) Token: 0x06001761 RID: 5985 RVA: 0x0000AD31 File Offset: 0x00008F31
		[DataMember]
		public SPProviderDTO Notetaker { get; set; }
	}
}
