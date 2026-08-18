using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B6A RID: 2922
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByIdReq : BaseMessageReq
	{
		// Token: 0x170016D3 RID: 5843
		// (get) Token: 0x06003DF6 RID: 15862 RVA: 0x0001E697 File Offset: 0x0001C897
		// (set) Token: 0x06003DF7 RID: 15863 RVA: 0x0001E69F File Offset: 0x0001C89F
		[DataMember]
		public Guid MediaContentID { get; set; }
	}
}
