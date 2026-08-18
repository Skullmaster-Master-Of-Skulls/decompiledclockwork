using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000691 RID: 1681
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadControlIdsOnFormsResp
	{
		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06002219 RID: 8729 RVA: 0x0000F8AA File Offset: 0x0000DAAA
		// (set) Token: 0x0600221A RID: 8730 RVA: 0x0000F8B2 File Offset: 0x0000DAB2
		[DataMember]
		public IList<int> ControlIds { get; set; }
	}
}
