using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Surveys
{
	// Token: 0x0200021C RID: 540
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteSurveyQueueItemResp
	{
		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x0000592A File Offset: 0x00003B2A
		// (set) Token: 0x06000C32 RID: 3122 RVA: 0x00005932 File Offset: 0x00003B32
		[DataMember]
		public bool CompletedSuccessfully { get; set; }
	}
}
