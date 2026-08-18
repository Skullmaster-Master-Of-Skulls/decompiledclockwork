using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000867 RID: 2151
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetClockWorkServerExecutingLogsByJobReq : BaseMessageReq
	{
		// Token: 0x17000F5A RID: 3930
		// (get) Token: 0x06002BBD RID: 11197 RVA: 0x00014BBD File Offset: 0x00012DBD
		// (set) Token: 0x06002BBE RID: 11198 RVA: 0x00014BC5 File Offset: 0x00012DC5
		[DataMember]
		public int JobId { get; set; }

		// Token: 0x17000F5B RID: 3931
		// (get) Token: 0x06002BBF RID: 11199 RVA: 0x00014BCE File Offset: 0x00012DCE
		// (set) Token: 0x06002BC0 RID: 11200 RVA: 0x00014BD6 File Offset: 0x00012DD6
		[DataMember]
		public DateTime StartTime { get; set; }

		// Token: 0x17000F5C RID: 3932
		// (get) Token: 0x06002BC1 RID: 11201 RVA: 0x00014BDF File Offset: 0x00012DDF
		// (set) Token: 0x06002BC2 RID: 11202 RVA: 0x00014BE7 File Offset: 0x00012DE7
		[DataMember]
		public DateTime EndTime { get; set; }
	}
}
