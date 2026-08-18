using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x020006FB RID: 1787
	[DataContract(Namespace = "http://tpro.ca")]
	public class ParseExternalCourseRowPartsReq : BaseReportMessageReq
	{
		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06002467 RID: 9319 RVA: 0x000109D4 File Offset: 0x0000EBD4
		// (set) Token: 0x06002468 RID: 9320 RVA: 0x000109DC File Offset: 0x0000EBDC
		[DataMember]
		public List<DataSyncExternalCourseRowPartDTO> ExternalCourseRowParts { get; set; }
	}
}
