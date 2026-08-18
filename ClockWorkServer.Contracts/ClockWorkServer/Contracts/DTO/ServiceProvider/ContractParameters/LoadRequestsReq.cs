using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider.ContractParameters
{
	// Token: 0x020002C1 RID: 705
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadRequestsReq : BaseMessageReq
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600101B RID: 4123 RVA: 0x00007810 File Offset: 0x00005A10
		// (set) Token: 0x0600101C RID: 4124 RVA: 0x00007818 File Offset: 0x00005A18
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x0600101D RID: 4125 RVA: 0x00007821 File Offset: 0x00005A21
		// (set) Token: 0x0600101E RID: 4126 RVA: 0x00007829 File Offset: 0x00005A29
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x00007832 File Offset: 0x00005A32
		// (set) Token: 0x06001020 RID: 4128 RVA: 0x0000783A File Offset: 0x00005A3A
		[DataMember]
		public bool IncludeSubItems { get; set; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x00007843 File Offset: 0x00005A43
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x0000784B File Offset: 0x00005A4B
		[DataMember]
		public bool IncludeAssigned { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001023 RID: 4131 RVA: 0x00007854 File Offset: 0x00005A54
		// (set) Token: 0x06001024 RID: 4132 RVA: 0x0000785C File Offset: 0x00005A5C
		[DataMember]
		public bool IncludeUnassigned { get; set; }

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001025 RID: 4133 RVA: 0x00007865 File Offset: 0x00005A65
		// (set) Token: 0x06001026 RID: 4134 RVA: 0x0000786D File Offset: 0x00005A6D
		[DataMember]
		public IList<int> SPProviderTypeIds { get; set; }
	}
}
