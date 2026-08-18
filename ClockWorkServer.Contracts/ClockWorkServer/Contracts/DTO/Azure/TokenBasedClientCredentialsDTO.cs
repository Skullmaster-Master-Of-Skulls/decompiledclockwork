using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Azure
{
	// Token: 0x020008B0 RID: 2224
	[DataContract(Namespace = "http://tpro.ca")]
	public class TokenBasedClientCredentialsDTO
	{
		// Token: 0x17000FD4 RID: 4052
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x0001545B File Offset: 0x0001365B
		// (set) Token: 0x06002CF9 RID: 11513 RVA: 0x00015463 File Offset: 0x00013663
		[DataMember]
		public string ClientId { get; set; }

		// Token: 0x17000FD5 RID: 4053
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x0001546C File Offset: 0x0001366C
		// (set) Token: 0x06002CFB RID: 11515 RVA: 0x00015474 File Offset: 0x00013674
		[DataMember]
		public DateTimeOffset TokenIssuedDateTime { get; set; }

		// Token: 0x17000FD6 RID: 4054
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x0001547D File Offset: 0x0001367D
		// (set) Token: 0x06002CFD RID: 11517 RVA: 0x00015485 File Offset: 0x00013685
		[DataMember]
		public string Token { get; set; }
	}
}
