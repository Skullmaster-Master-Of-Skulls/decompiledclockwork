using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000BE1 RID: 3041
	[DataContract(Namespace = "http://tpro.ca")]
	public class MediaJobStatusDTO
	{
		// Token: 0x170017AC RID: 6060
		// (get) Token: 0x0600402B RID: 16427 RVA: 0x0001F856 File Offset: 0x0001DA56
		// (set) Token: 0x0600402C RID: 16428 RVA: 0x0001F85E File Offset: 0x0001DA5E
		[DataMember]
		public int MediaJobStatusId { get; set; }

		// Token: 0x170017AD RID: 6061
		// (get) Token: 0x0600402D RID: 16429 RVA: 0x0001F867 File Offset: 0x0001DA67
		// (set) Token: 0x0600402E RID: 16430 RVA: 0x0001F86F File Offset: 0x0001DA6F
		[DataMember]
		public string JobStatusName { get; set; }

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x0600402F RID: 16431 RVA: 0x0001F878 File Offset: 0x0001DA78
		// (set) Token: 0x06004030 RID: 16432 RVA: 0x0001F880 File Offset: 0x0001DA80
		[DataMember]
		public string JobStatusDescription { get; set; }

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06004031 RID: 16433 RVA: 0x0001F889 File Offset: 0x0001DA89
		// (set) Token: 0x06004032 RID: 16434 RVA: 0x0001F891 File Offset: 0x0001DA91
		[DataMember]
		public MediaJobStatusGroup JobStatusGroup { get; set; }
	}
}
