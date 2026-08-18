using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006A3 RID: 1699
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDynamicFormsByIdsResp
	{
		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x0000FC2D File Offset: 0x0000DE2D
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x0000FC35 File Offset: 0x0000DE35
		[DataMember]
		public IList<DynamicFormDTO> Forms { get; set; }
	}
}
