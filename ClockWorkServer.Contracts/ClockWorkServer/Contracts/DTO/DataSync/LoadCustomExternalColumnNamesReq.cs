using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000725 RID: 1829
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCustomExternalColumnNamesReq : BaseReportMessageReq
	{
		// Token: 0x17000CFF RID: 3327
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x000112CB File Offset: 0x0000F4CB
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x000112D3 File Offset: 0x0000F4D3
		[DataMember]
		public string ClockWorkTableNameWithoutCUSTOMPrefixToStoreDataIn { get; set; }
	}
}
