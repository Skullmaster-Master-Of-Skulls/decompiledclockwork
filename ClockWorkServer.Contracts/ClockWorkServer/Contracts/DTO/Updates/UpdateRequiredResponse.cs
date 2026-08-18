using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000166 RID: 358
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateRequiredResponse
	{
		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00003F12 File Offset: 0x00002112
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x00003F1A File Offset: 0x0000211A
		[DataMember]
		public bool IsUpdateRequired { get; set; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00003F23 File Offset: 0x00002123
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x00003F2B File Offset: 0x0000212B
		[DataMember]
		public string CurrentVersionOnServer { get; set; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00003F34 File Offset: 0x00002134
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x00003F3C File Offset: 0x0000213C
		[DataMember]
		public string UpdateAccessToken { get; set; }
	}
}
