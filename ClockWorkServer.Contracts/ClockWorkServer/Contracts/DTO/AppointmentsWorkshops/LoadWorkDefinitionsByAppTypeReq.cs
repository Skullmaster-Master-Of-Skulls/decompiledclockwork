using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x02000911 RID: 2321
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkDefinitionsByAppTypeReq : BaseMessageReq
	{
		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06002F13 RID: 12051 RVA: 0x000165C3 File Offset: 0x000147C3
		// (set) Token: 0x06002F14 RID: 12052 RVA: 0x000165CB File Offset: 0x000147CB
		[DataMember]
		public int AppTypeId { get; set; }
	}
}
