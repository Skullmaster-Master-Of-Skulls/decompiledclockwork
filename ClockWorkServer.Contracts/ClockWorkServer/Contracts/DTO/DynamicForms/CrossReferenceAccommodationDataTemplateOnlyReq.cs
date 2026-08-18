using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200063E RID: 1598
	[DataContract(Namespace = "http://tpro.ca")]
	public class CrossReferenceAccommodationDataTemplateOnlyReq : BaseMessageReq
	{
		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x06002093 RID: 8339 RVA: 0x0000ED0D File Offset: 0x0000CF0D
		// (set) Token: 0x06002094 RID: 8340 RVA: 0x0000ED15 File Offset: 0x0000CF15
		[DataMember]
		public DataTable TableWithData { get; set; }

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x06002095 RID: 8341 RVA: 0x0000ED1E File Offset: 0x0000CF1E
		// (set) Token: 0x06002096 RID: 8342 RVA: 0x0000ED26 File Offset: 0x0000CF26
		[DataMember]
		public IList<int> ControlIds { get; set; }
	}
}
