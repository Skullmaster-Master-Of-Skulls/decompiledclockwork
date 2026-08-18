using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B4D RID: 2893
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateMediaContentFileInfoReq : BaseMessageReq
	{
		// Token: 0x170016AD RID: 5805
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x0001E411 File Offset: 0x0001C611
		// (set) Token: 0x06003D8E RID: 15758 RVA: 0x0001E419 File Offset: 0x0001C619
		[DataMember]
		public MediaContentFileWithoutDataDTO MediaContentFile { get; set; }
	}
}
