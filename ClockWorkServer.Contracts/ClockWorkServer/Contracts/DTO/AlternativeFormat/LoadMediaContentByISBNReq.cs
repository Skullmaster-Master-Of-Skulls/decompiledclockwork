using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B6E RID: 2926
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadMediaContentByISBNReq : BaseMessageReq
	{
		// Token: 0x170016D7 RID: 5847
		// (get) Token: 0x06003E02 RID: 15874 RVA: 0x0001E6DB File Offset: 0x0001C8DB
		// (set) Token: 0x06003E03 RID: 15875 RVA: 0x0001E6E3 File Offset: 0x0001C8E3
		[DataMember]
		public string ISBN { get; set; }
	}
}
