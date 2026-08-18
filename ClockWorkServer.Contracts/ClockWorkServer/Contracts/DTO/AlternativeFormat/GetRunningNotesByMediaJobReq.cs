using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BA8 RID: 2984
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetRunningNotesByMediaJobReq : BaseMessageReq
	{
		// Token: 0x1700174A RID: 5962
		// (get) Token: 0x06003F2E RID: 16174 RVA: 0x0001F1D4 File Offset: 0x0001D3D4
		// (set) Token: 0x06003F2F RID: 16175 RVA: 0x0001F1DC File Offset: 0x0001D3DC
		[DataMember]
		public int MediaJobId { get; set; }
	}
}
