using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003B1 RID: 945
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveAssignedAdvisorStoredSignatureReq : BaseMessageReq
	{
		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x0600150E RID: 5390 RVA: 0x00009E0D File Offset: 0x0000800D
		// (set) Token: 0x0600150F RID: 5391 RVA: 0x00009E15 File Offset: 0x00008015
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06001510 RID: 5392 RVA: 0x00009E1E File Offset: 0x0000801E
		// (set) Token: 0x06001511 RID: 5393 RVA: 0x00009E26 File Offset: 0x00008026
		[DataMember]
		public DynamicDataDTO Data { get; set; }
	}
}
