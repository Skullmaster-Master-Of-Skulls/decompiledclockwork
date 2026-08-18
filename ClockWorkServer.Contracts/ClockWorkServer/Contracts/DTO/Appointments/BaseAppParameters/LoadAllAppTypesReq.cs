using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters
{
	// Token: 0x02000980 RID: 2432
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllAppTypesReq : BaseMessageReq
	{
		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x0001819E File Offset: 0x0001639E
		// (set) Token: 0x06003186 RID: 12678 RVA: 0x000181A6 File Offset: 0x000163A6
		[DataMember]
		public bool IgnoreCache { get; set; }
	}
}
