using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Data
{
	// Token: 0x020006F4 RID: 1780
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReassignStaffDropListResultDTO
	{
		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x0600245D RID: 9309 RVA: 0x00010990 File Offset: 0x0000EB90
		// (set) Token: 0x0600245E RID: 9310 RVA: 0x00010998 File Offset: 0x0000EB98
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600245F RID: 9311 RVA: 0x000109A1 File Offset: 0x0000EBA1
		// (set) Token: 0x06002460 RID: 9312 RVA: 0x000109A9 File Offset: 0x0000EBA9
		[DataMember]
		public bool WasSuccessful { get; set; }
	}
}
