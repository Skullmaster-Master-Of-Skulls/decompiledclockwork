using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000177 RID: 375
	[DataContract(Namespace = "http://tpro.ca")]
	public class QueryRequestDTO
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00004088 File Offset: 0x00002288
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x00004090 File Offset: 0x00002290
		[DataMember]
		public string Sql { get; set; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x00004099 File Offset: 0x00002299
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x000040A1 File Offset: 0x000022A1
		[DataMember]
		public List<CommonParameterDTO> Parameters { get; set; }
	}
}
