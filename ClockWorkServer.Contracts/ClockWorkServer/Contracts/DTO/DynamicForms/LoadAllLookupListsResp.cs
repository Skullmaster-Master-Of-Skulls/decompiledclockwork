using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000689 RID: 1673
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllLookupListsResp
	{
		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x060021FB RID: 8699 RVA: 0x0000F7EF File Offset: 0x0000D9EF
		// (set) Token: 0x060021FC RID: 8700 RVA: 0x0000F7F7 File Offset: 0x0000D9F7
		[DataMember]
		public IList<DynamicListGroupDTO> LookupGroups { get; set; }
	}
}
