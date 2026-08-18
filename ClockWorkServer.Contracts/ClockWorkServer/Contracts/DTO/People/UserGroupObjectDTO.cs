using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003C2 RID: 962
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserGroupObjectDTO
	{
		// Token: 0x1700069B RID: 1691
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x0000A0A4 File Offset: 0x000082A4
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x0000A0AC File Offset: 0x000082AC
		[DataMember]
		public UserGroupObjectIdDTO ObjectId { get; set; }

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x0000A0B5 File Offset: 0x000082B5
		// (set) Token: 0x06001570 RID: 5488 RVA: 0x0000A0BD File Offset: 0x000082BD
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001571 RID: 5489 RVA: 0x0000A0C6 File Offset: 0x000082C6
		// (set) Token: 0x06001572 RID: 5490 RVA: 0x0000A0CE File Offset: 0x000082CE
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001573 RID: 5491 RVA: 0x0000A0D7 File Offset: 0x000082D7
		// (set) Token: 0x06001574 RID: 5492 RVA: 0x0000A0DF File Offset: 0x000082DF
		[DataMember]
		public PersonBaseDTO Person { get; set; }
	}
}
