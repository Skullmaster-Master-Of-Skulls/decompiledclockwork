using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BAB RID: 2987
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetActiveMediaJobByIdResp
	{
		// Token: 0x1700174D RID: 5965
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x0001F207 File Offset: 0x0001D407
		// (set) Token: 0x06003F38 RID: 16184 RVA: 0x0001F20F File Offset: 0x0001D40F
		[DataMember]
		public MediaJobDTO MediaJob { get; set; }
	}
}
