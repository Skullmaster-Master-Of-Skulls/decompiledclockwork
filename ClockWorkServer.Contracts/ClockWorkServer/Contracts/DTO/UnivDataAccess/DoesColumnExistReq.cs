using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x0200017B RID: 379
	[DataContract(Namespace = "http://tpro.ca")]
	public class DoesColumnExistReq : BaseMessageReq
	{
		// Token: 0x17000194 RID: 404
		// (get) Token: 0x06000915 RID: 2325 RVA: 0x000040FF File Offset: 0x000022FF
		// (set) Token: 0x06000916 RID: 2326 RVA: 0x00004107 File Offset: 0x00002307
		[DataMember]
		public string TableName { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000917 RID: 2327 RVA: 0x00004110 File Offset: 0x00002310
		// (set) Token: 0x06000918 RID: 2328 RVA: 0x00004118 File Offset: 0x00002318
		[DataMember]
		public string ColumnName { get; set; }
	}
}
