using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200063F RID: 1599
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferenceAccommodationDataTemplateOnlyResp
	{
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06002098 RID: 8344 RVA: 0x0000ED2F File Offset: 0x0000CF2F
		// (set) Token: 0x06002099 RID: 8345 RVA: 0x0000ED37 File Offset: 0x0000CF37
		[DataMember]
		public DataTable Table { get; set; }
	}
}
