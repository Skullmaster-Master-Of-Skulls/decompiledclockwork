using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions
{
	// Token: 0x0200012C RID: 300
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClearCacheForUserReq : BaseMessageReq
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x000034BF File Offset: 0x000016BF
		// (set) Token: 0x0600078C RID: 1932 RVA: 0x000034C7 File Offset: 0x000016C7
		[DataMember]
		public int PersonId { get; set; }
	}
}
