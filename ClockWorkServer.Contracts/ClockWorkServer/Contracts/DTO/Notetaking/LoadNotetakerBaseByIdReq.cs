using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200042F RID: 1071
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotetakerBaseByIdReq : BaseReportMessageReq
	{
		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x0600172B RID: 5931 RVA: 0x0000ABE6 File Offset: 0x00008DE6
		// (set) Token: 0x0600172C RID: 5932 RVA: 0x0000ABEE File Offset: 0x00008DEE
		[DataMember]
		public int ServiceProviderId { get; set; }
	}
}
