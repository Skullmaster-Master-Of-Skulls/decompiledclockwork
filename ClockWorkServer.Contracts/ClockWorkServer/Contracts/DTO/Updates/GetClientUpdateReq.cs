using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000174 RID: 372
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClientUpdateReq : BaseHashAuthMessageReq
	{
		// Token: 0x17000187 RID: 391
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x00004022 File Offset: 0x00002222
		// (set) Token: 0x060008F5 RID: 2293 RVA: 0x0000402A File Offset: 0x0000222A
		[DataMember]
		public string FileType { get; set; }

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060008F6 RID: 2294 RVA: 0x00004033 File Offset: 0x00002233
		// (set) Token: 0x060008F7 RID: 2295 RVA: 0x0000403B File Offset: 0x0000223B
		[DataMember]
		public string ClientVersion { get; set; }

		// Token: 0x040001D7 RID: 471
		[DataMember]
		public eAddressSize AddressSize;

		// Token: 0x040001D8 RID: 472
		[DataMember]
		public string IPAddress;
	}
}
