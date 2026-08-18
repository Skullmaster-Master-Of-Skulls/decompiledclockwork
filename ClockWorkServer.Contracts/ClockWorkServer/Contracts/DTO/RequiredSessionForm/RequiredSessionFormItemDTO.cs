using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.RequiredSessionForm
{
	// Token: 0x020002F7 RID: 759
	[DataContract(Namespace = "http://tpro.ca")]
	public class RequiredSessionFormItemDTO
	{
		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06001170 RID: 4464 RVA: 0x000082E0 File Offset: 0x000064E0
		// (set) Token: 0x06001171 RID: 4465 RVA: 0x000082E8 File Offset: 0x000064E8
		[DataMember]
		public int RequiredSessionFormItemId { get; set; }

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06001172 RID: 4466 RVA: 0x000082F1 File Offset: 0x000064F1
		// (set) Token: 0x06001173 RID: 4467 RVA: 0x000082F9 File Offset: 0x000064F9
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06001174 RID: 4468 RVA: 0x00008302 File Offset: 0x00006502
		// (set) Token: 0x06001175 RID: 4469 RVA: 0x0000830A File Offset: 0x0000650A
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001176 RID: 4470 RVA: 0x00008313 File Offset: 0x00006513
		// (set) Token: 0x06001177 RID: 4471 RVA: 0x0000831B File Offset: 0x0000651B
		[DataMember]
		public bool Disabled { get; set; }

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x06001178 RID: 4472 RVA: 0x00008324 File Offset: 0x00006524
		// (set) Token: 0x06001179 RID: 4473 RVA: 0x0000832C File Offset: 0x0000652C
		[DataMember]
		public string Intro { get; set; }

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x0600117A RID: 4474 RVA: 0x00008335 File Offset: 0x00006535
		// (set) Token: 0x0600117B RID: 4475 RVA: 0x0000833D File Offset: 0x0000653D
		[DataMember]
		public TPMailMessageDTO EmailTemplate { get; set; }

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x00008346 File Offset: 0x00006546
		// (set) Token: 0x0600117D RID: 4477 RVA: 0x0000834E File Offset: 0x0000654E
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x0600117E RID: 4478 RVA: 0x00008357 File Offset: 0x00006557
		// (set) Token: 0x0600117F RID: 4479 RVA: 0x0000835F File Offset: 0x0000655F
		[DataMember]
		public string Name { get; set; }
	}
}
