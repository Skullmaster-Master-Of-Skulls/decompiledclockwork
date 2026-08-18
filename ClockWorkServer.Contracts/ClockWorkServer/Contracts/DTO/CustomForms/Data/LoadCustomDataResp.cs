using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data
{
	// Token: 0x02000765 RID: 1893
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCustomDataResp
	{
		// Token: 0x17000D87 RID: 3463
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x000120E9 File Offset: 0x000102E9
		// (set) Token: 0x060026EF RID: 9967 RVA: 0x000120F1 File Offset: 0x000102F1
		[DataMember]
		public CustomDataSetDTO DataSet { get; set; }
	}
}
