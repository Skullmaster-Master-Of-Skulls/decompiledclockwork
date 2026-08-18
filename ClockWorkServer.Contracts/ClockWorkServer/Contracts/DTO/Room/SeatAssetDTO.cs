using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Room
{
	// Token: 0x020002F3 RID: 755
	[DataContract(Namespace = "http://tpro.ca")]
	public class SeatAssetDTO
	{
		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x0600114A RID: 4426 RVA: 0x000081B6 File Offset: 0x000063B6
		// (set) Token: 0x0600114B RID: 4427 RVA: 0x000081BE File Offset: 0x000063BE
		[DataMember]
		public string SeatAssetId { get; set; }

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x0600114C RID: 4428 RVA: 0x000081C7 File Offset: 0x000063C7
		// (set) Token: 0x0600114D RID: 4429 RVA: 0x000081CF File Offset: 0x000063CF
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x0600114E RID: 4430 RVA: 0x000081D8 File Offset: 0x000063D8
		// (set) Token: 0x0600114F RID: 4431 RVA: 0x000081E0 File Offset: 0x000063E0
		[DataMember]
		public int Score { get; set; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001150 RID: 4432 RVA: 0x000081E9 File Offset: 0x000063E9
		// (set) Token: 0x06001151 RID: 4433 RVA: 0x000081F1 File Offset: 0x000063F1
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001152 RID: 4434 RVA: 0x000081FA File Offset: 0x000063FA
		// (set) Token: 0x06001153 RID: 4435 RVA: 0x00008202 File Offset: 0x00006402
		[DataMember]
		public IList<SeatAssetAccommodationDTO> AccommodationsBehind { get; set; }
	}
}
