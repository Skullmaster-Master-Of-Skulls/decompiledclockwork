using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000940 RID: 2368
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCancelReasonsResp
	{
		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x06003093 RID: 12435 RVA: 0x00017BB5 File Offset: 0x00015DB5
		// (set) Token: 0x06003094 RID: 12436 RVA: 0x00017BBD File Offset: 0x00015DBD
		[DataMember]
		public Forest<AppCancelReasonOrGroupDTO> Forest { get; set; }
	}
}
