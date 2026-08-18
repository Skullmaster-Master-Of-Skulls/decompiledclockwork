using System;
using System.Data;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Encryption
{
	// Token: 0x0200060C RID: 1548
	[DataContract(Namespace = "http://tpro.ca")]
	public class EncryptOrDecryptNameDataTableBatchResp
	{
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06001F91 RID: 8081 RVA: 0x0000E593 File Offset: 0x0000C793
		// (set) Token: 0x06001F92 RID: 8082 RVA: 0x0000E59B File Offset: 0x0000C79B
		[DataMember]
		public DataTable Table { get; set; }
	}
}
