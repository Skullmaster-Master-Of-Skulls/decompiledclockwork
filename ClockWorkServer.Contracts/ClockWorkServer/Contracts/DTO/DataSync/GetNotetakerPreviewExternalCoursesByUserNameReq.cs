using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000719 RID: 1817
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetNotetakerPreviewExternalCoursesByUserNameReq : BaseReportMessageReq
	{
		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06002579 RID: 9593 RVA: 0x000111EE File Offset: 0x0000F3EE
		// (set) Token: 0x0600257A RID: 9594 RVA: 0x000111F6 File Offset: 0x0000F3F6
		[DataMember]
		public string UserName { get; set; }
	}
}
