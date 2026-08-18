using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003AC RID: 940
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStaffSignatureDataResp
	{
		// Token: 0x1700066F RID: 1647
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00009DB8 File Offset: 0x00007FB8
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x00009DC0 File Offset: 0x00007FC0
		[DataMember]
		public DynamicDataDTO Data { get; set; }
	}
}
