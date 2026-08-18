using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006FC RID: 1788
	[DataContract(Namespace = "http://tpro.ca")]
	public class ParseExternalCourseRowPartsResp
	{
		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x0600246A RID: 9322 RVA: 0x000109E5 File Offset: 0x0000EBE5
		// (set) Token: 0x0600246B RID: 9323 RVA: 0x000109ED File Offset: 0x0000EBED
		[DataMember]
		public IList<DataSyncExternalCourseDTO> ExternalCourses { get; set; }
	}
}
