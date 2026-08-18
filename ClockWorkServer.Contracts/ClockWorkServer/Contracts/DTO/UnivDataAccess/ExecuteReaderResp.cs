using System;
using System.Data.SqlClient;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x02000188 RID: 392
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExecuteReaderResp
	{
		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000942 RID: 2370 RVA: 0x0000420F File Offset: 0x0000240F
		// (set) Token: 0x06000943 RID: 2371 RVA: 0x00004217 File Offset: 0x00002417
		[DataMember]
		public SqlDataReader Reader { get; set; }
	}
}
