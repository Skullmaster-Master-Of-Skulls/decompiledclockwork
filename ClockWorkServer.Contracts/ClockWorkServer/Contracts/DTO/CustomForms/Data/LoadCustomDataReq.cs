using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data.Context;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data
{
	// Token: 0x02000764 RID: 1892
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCustomDataReq : BaseMessageReq
	{
		// Token: 0x17000D85 RID: 3461
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x000120C7 File Offset: 0x000102C7
		// (set) Token: 0x060026EA RID: 9962 RVA: 0x000120CF File Offset: 0x000102CF
		[DataMember]
		public CustomDataContextDTO Context { get; set; }

		// Token: 0x17000D86 RID: 3462
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x000120D8 File Offset: 0x000102D8
		// (set) Token: 0x060026EC RID: 9964 RVA: 0x000120E0 File Offset: 0x000102E0
		[DataMember]
		public IList<Guid> DataInstanceIds { get; set; }
	}
}
