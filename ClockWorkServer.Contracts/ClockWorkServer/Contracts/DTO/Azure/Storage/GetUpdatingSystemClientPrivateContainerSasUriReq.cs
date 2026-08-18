using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Azure.Storage
{
	// Token: 0x020008B5 RID: 2229
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetUpdatingSystemClientPrivateContainerSasUriReq : BaseMessageReq
	{
		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x00015575 File Offset: 0x00013775
		// (set) Token: 0x06002D1A RID: 11546 RVA: 0x0001557D File Offset: 0x0001377D
		[DataMember]
		public TokenBasedClientCredentialsDTO ClientCredentials { get; set; }
	}
}
