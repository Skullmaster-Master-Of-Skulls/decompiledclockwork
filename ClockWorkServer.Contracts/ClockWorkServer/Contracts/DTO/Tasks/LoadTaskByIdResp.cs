using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001F0 RID: 496
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTaskByIdResp
	{
		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x000053B8 File Offset: 0x000035B8
		// (set) Token: 0x06000B62 RID: 2914 RVA: 0x000053C0 File Offset: 0x000035C0
		[DataMember]
		public TaskDTO Task { get; set; }
	}
}
