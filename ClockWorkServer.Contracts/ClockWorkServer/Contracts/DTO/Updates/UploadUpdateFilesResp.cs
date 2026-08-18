using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000171 RID: 369
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadUpdateFilesResp
	{
		// Token: 0x17000181 RID: 385
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x00003FBC File Offset: 0x000021BC
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x00003FC4 File Offset: 0x000021C4
		[DataMember]
		public IList<UploadUpdateFileResultDTO> UploadFilesResult { get; set; }
	}
}
