using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C61 RID: 3169
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllMediaRequestInfoByJobIdResp
	{
		// Token: 0x1700185C RID: 6236
		// (get) Token: 0x0600420B RID: 16907 RVA: 0x00020406 File Offset: 0x0001E606
		// (set) Token: 0x0600420C RID: 16908 RVA: 0x0002040E File Offset: 0x0001E60E
		[DataMember]
		public IList<MediaContentRequestedInfoDTO> MediaContentRequestedList { get; set; }
	}
}
