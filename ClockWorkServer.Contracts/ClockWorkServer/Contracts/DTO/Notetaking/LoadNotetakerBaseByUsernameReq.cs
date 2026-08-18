using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200042B RID: 1067
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadNotetakerBaseByUsernameReq : BaseReportMessageReq
	{
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x0600171D RID: 5917 RVA: 0x0000AB91 File Offset: 0x00008D91
		// (set) Token: 0x0600171E RID: 5918 RVA: 0x0000AB99 File Offset: 0x00008D99
		[DataMember]
		public string Username { get; set; }
	}
}
