using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200069D RID: 1693
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindFormByTitleSubstringMatchResp
	{
		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x0600226E RID: 8814 RVA: 0x0000FBD8 File Offset: 0x0000DDD8
		// (set) Token: 0x0600226F RID: 8815 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		[DataMember]
		public IList<DynamicFormDTO> MatchingForms { get; set; }
	}
}
