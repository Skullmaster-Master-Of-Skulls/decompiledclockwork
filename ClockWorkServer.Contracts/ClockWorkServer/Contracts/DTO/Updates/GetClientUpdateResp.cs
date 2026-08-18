using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000175 RID: 373
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClientUpdateResp
	{
		// Token: 0x17000189 RID: 393
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00004044 File Offset: 0x00002244
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x0000404C File Offset: 0x0000224C
		[DataMember]
		public FileSystemStructure File { get; set; }
	}
}
