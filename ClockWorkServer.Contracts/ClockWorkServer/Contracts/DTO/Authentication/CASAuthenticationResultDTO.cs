using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication
{
	// Token: 0x020008DE RID: 2270
	[DataContract(Namespace = "http://tpro.ca")]
	public class CASAuthenticationResultDTO
	{
		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x06002E01 RID: 11777 RVA: 0x00015C60 File Offset: 0x00013E60
		// (set) Token: 0x06002E02 RID: 11778 RVA: 0x00015C68 File Offset: 0x00013E68
		[DataMember]
		public bool IsAuthenticated { get; set; }

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x06002E03 RID: 11779 RVA: 0x00015C71 File Offset: 0x00013E71
		// (set) Token: 0x06002E04 RID: 11780 RVA: 0x00015C79 File Offset: 0x00013E79
		[DataMember]
		public string UserName { get; set; }

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x06002E05 RID: 11781 RVA: 0x00015C82 File Offset: 0x00013E82
		// (set) Token: 0x06002E06 RID: 11782 RVA: 0x00015C8A File Offset: 0x00013E8A
		[DataMember]
		public Dictionary<string, string> ReturnAttributes { get; set; }
	}
}
