using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000178 RID: 376
	[DataContract(Namespace = "http://tpro.ca")]
	public class QueryResultDTO
	{
		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x000040AA File Offset: 0x000022AA
		// (set) Token: 0x06000909 RID: 2313 RVA: 0x000040B2 File Offset: 0x000022B2
		[DataMember]
		public DataTable DataTable { get; set; }

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x000040BB File Offset: 0x000022BB
		// (set) Token: 0x0600090B RID: 2315 RVA: 0x000040C3 File Offset: 0x000022C3
		[DataMember]
		public int Id { get; set; }

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x000040CC File Offset: 0x000022CC
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x000040D4 File Offset: 0x000022D4
		[DataMember]
		public string ErrorMessage { get; set; }
	}
}
