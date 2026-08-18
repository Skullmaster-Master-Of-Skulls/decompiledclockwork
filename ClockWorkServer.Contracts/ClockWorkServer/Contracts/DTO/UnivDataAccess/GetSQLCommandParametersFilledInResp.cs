using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.UnivDataAccess
{
	// Token: 0x0200017E RID: 382
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSQLCommandParametersFilledInResp
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x06000920 RID: 2336 RVA: 0x00004143 File Offset: 0x00002343
		// (set) Token: 0x06000921 RID: 2337 RVA: 0x0000414B File Offset: 0x0000234B
		[DataMember]
		public string SqlWithParameters { get; set; }
	}
}
