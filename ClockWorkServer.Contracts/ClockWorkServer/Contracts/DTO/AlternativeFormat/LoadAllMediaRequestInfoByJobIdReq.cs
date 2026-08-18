using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C60 RID: 3168
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllMediaRequestInfoByJobIdReq : BaseReportMessageReq
	{
		// Token: 0x1700185B RID: 6235
		// (get) Token: 0x06004208 RID: 16904 RVA: 0x000203F5 File Offset: 0x0001E5F5
		// (set) Token: 0x06004209 RID: 16905 RVA: 0x000203FD File Offset: 0x0001E5FD
		[DataMember]
		public int JobId { get; set; }
	}
}
