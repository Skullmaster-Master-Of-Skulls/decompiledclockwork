using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000640 RID: 1600
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferenceAccommodationDataTemplateOrCourseSpecificReq : BaseMessageReq
	{
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x0600209B RID: 8347 RVA: 0x0000ED40 File Offset: 0x0000CF40
		// (set) Token: 0x0600209C RID: 8348 RVA: 0x0000ED48 File Offset: 0x0000CF48
		[DataMember]
		public DataTable TableWithData { get; set; }

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x0600209D RID: 8349 RVA: 0x0000ED51 File Offset: 0x0000CF51
		// (set) Token: 0x0600209E RID: 8350 RVA: 0x0000ED59 File Offset: 0x0000CF59
		[DataMember]
		public IList<int> ControlIds { get; set; }
	}
}
