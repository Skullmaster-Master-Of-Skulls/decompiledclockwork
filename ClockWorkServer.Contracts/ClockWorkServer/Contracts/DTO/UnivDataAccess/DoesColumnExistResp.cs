using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x0200017C RID: 380
	[DataContract(Namespace = "http://tpro.ca")]
	public class DoesColumnExistResp
	{
		// Token: 0x17000196 RID: 406
		// (get) Token: 0x0600091A RID: 2330 RVA: 0x00004121 File Offset: 0x00002321
		// (set) Token: 0x0600091B RID: 2331 RVA: 0x00004129 File Offset: 0x00002329
		[DataMember]
		public bool ColumnExists { get; set; }
	}
}
