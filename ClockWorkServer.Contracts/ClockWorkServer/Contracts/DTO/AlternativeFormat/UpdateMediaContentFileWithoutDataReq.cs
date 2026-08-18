using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B5B RID: 2907
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateMediaContentFileWithoutDataReq : BaseMessageReq
	{
		// Token: 0x170016BC RID: 5820
		// (get) Token: 0x06003DB9 RID: 15801 RVA: 0x0001E510 File Offset: 0x0001C710
		// (set) Token: 0x06003DBA RID: 15802 RVA: 0x0001E518 File Offset: 0x0001C718
		[DataMember]
		public MediaContentFileWithoutDataDTO MediaContentFile { get; set; }
	}
}
