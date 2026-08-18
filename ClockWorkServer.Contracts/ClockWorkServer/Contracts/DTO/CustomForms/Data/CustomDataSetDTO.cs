using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data
{
	// Token: 0x02000768 RID: 1896
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomDataSetDTO
	{
		// Token: 0x17000D8A RID: 3466
		// (get) Token: 0x060026F7 RID: 9975 RVA: 0x0001211C File Offset: 0x0001031C
		// (set) Token: 0x060026F8 RID: 9976 RVA: 0x00012124 File Offset: 0x00010324
		[DataMember]
		public IList<CustomDataHolderCollectionDTO> Data { get; set; }

		// Token: 0x17000D8B RID: 3467
		// (get) Token: 0x060026F9 RID: 9977 RVA: 0x0001212D File Offset: 0x0001032D
		// (set) Token: 0x060026FA RID: 9978 RVA: 0x00012135 File Offset: 0x00010335
		[DataMember]
		public CustomDataContextDTO Context { get; set; }
	}
}
