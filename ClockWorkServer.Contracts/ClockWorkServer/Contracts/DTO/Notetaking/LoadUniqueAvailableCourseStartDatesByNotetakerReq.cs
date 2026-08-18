using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200044A RID: 1098
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUniqueAvailableCourseStartDatesByNotetakerReq : BaseReportMessageReq
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001788 RID: 6024 RVA: 0x0000AE17 File Offset: 0x00009017
		// (set) Token: 0x06001789 RID: 6025 RVA: 0x0000AE1F File Offset: 0x0000901F
		[DataMember]
		public int NotetakerId { get; set; }
	}
}
