using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form
{
	// Token: 0x02000754 RID: 1876
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateCustomFormResp
	{
		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x060026B3 RID: 9907 RVA: 0x00011F84 File Offset: 0x00010184
		// (set) Token: 0x060026B4 RID: 9908 RVA: 0x00011F8C File Offset: 0x0001018C
		[DataMember]
		public Guid FormId { get; set; }
	}
}
