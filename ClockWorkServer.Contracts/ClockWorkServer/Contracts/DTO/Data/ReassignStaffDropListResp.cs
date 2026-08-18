using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Data
{
	// Token: 0x020006F3 RID: 1779
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReassignStaffDropListResp
	{
		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x0001097F File Offset: 0x0000EB7F
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x00010987 File Offset: 0x0000EB87
		[DataMember]
		public ReassignStaffDropListResultDTO Result { get; set; }
	}
}
