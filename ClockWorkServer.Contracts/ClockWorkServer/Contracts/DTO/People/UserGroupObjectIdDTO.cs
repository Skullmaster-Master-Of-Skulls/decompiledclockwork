using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x020003C3 RID: 963
	[DataContract(Namespace = "http://tpro.ca")]
	public class UserGroupObjectIdDTO
	{
		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0000A0E8 File Offset: 0x000082E8
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x0000A0F0 File Offset: 0x000082F0
		[DataMember]
		public eUserGroupObjectType UserGroupObjectType { get; set; }

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001578 RID: 5496 RVA: 0x0000A0F9 File Offset: 0x000082F9
		// (set) Token: 0x06001579 RID: 5497 RVA: 0x0000A101 File Offset: 0x00008301
		[DataMember]
		public int ObjectId { get; set; }
	}
}
