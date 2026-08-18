using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch
{
	// Token: 0x02000C72 RID: 3186
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetVolumeByIdReq : BaseMessageReq
	{
		// Token: 0x17001884 RID: 6276
		// (get) Token: 0x0600426E RID: 17006 RVA: 0x000206DD File Offset: 0x0001E8DD
		// (set) Token: 0x0600426F RID: 17007 RVA: 0x000206E5 File Offset: 0x0001E8E5
		[DataMember]
		public string Id { get; set; }

		// Token: 0x17001885 RID: 6277
		// (get) Token: 0x06004270 RID: 17008 RVA: 0x000206EE File Offset: 0x0001E8EE
		// (set) Token: 0x06004271 RID: 17009 RVA: 0x000206F6 File Offset: 0x0001E8F6
		[DataMember]
		public eBookSearchProviderType SearchType { get; set; }
	}
}
