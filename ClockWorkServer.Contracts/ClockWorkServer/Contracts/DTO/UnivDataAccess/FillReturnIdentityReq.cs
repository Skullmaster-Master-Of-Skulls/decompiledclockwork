using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x0200017F RID: 383
	[DataContract(Namespace = "http://tpro.ca")]
	public class FillReturnIdentityReq : BaseMessageReq
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00004154 File Offset: 0x00002354
		// (set) Token: 0x06000924 RID: 2340 RVA: 0x0000415C File Offset: 0x0000235C
		[DataMember]
		public QueryRequestDTO QueryRequest { get; set; }

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x00004165 File Offset: 0x00002365
		// (set) Token: 0x06000926 RID: 2342 RVA: 0x0000416D File Offset: 0x0000236D
		[DataMember]
		public string AutoIncrementColName { get; set; }

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000927 RID: 2343 RVA: 0x00004176 File Offset: 0x00002376
		// (set) Token: 0x06000928 RID: 2344 RVA: 0x0000417E File Offset: 0x0000237E
		[DataMember]
		public string TableName { get; set; }
	}
}
