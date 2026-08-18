using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x02000898 RID: 2200
	[DataContract(Namespace = "http://tpro.ca")]
	public class CaseBaseDTO
	{
		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06002C98 RID: 11416 RVA: 0x000151E5 File Offset: 0x000133E5
		// (set) Token: 0x06002C99 RID: 11417 RVA: 0x000151ED File Offset: 0x000133ED
		[DataMember]
		public virtual int InfoPcId { get; set; }

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06002C9A RID: 11418 RVA: 0x000151F6 File Offset: 0x000133F6
		// (set) Token: 0x06002C9B RID: 11419 RVA: 0x000151FE File Offset: 0x000133FE
		[DataMember]
		public string CaseNumber { get; set; }

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06002C9C RID: 11420 RVA: 0x00015207 File Offset: 0x00013407
		// (set) Token: 0x06002C9D RID: 11421 RVA: 0x0001520F File Offset: 0x0001340F
		[DataMember]
		public string Title { get; set; }
	}
}
