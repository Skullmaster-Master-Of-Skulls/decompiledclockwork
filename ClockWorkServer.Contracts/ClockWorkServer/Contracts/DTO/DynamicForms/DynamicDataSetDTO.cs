using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000671 RID: 1649
	[DataContract(Namespace = "http://tpro.ca")]
	public class DynamicDataSetDTO
	{
		// Token: 0x17000B48 RID: 2888
		// (get) Token: 0x0600217E RID: 8574 RVA: 0x0000F329 File Offset: 0x0000D529
		// (set) Token: 0x0600217F RID: 8575 RVA: 0x0000F331 File Offset: 0x0000D531
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000B49 RID: 2889
		// (get) Token: 0x06002180 RID: 8576 RVA: 0x0000F33A File Offset: 0x0000D53A
		// (set) Token: 0x06002181 RID: 8577 RVA: 0x0000F342 File Offset: 0x0000D542
		[DataMember]
		public List<DynamicDataDTO> Data { get; set; }
	}
}
