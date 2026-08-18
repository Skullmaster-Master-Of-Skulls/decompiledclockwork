using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000641 RID: 1601
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferenceAccommodationDataTemplateOrCourseSpecificResp
	{
		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x060020A0 RID: 8352 RVA: 0x0000ED62 File Offset: 0x0000CF62
		// (set) Token: 0x060020A1 RID: 8353 RVA: 0x0000ED6A File Offset: 0x0000CF6A
		[DataMember]
		public DataTable Table { get; set; }
	}
}
