using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000B6 RID: 182
	[DataContract(Namespace = "http://tpro.ca")]
	public class OperationData
	{
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000556 RID: 1366 RVA: 0x000024A1 File Offset: 0x000006A1
		// (set) Token: 0x06000557 RID: 1367 RVA: 0x000024A9 File Offset: 0x000006A9
		[DataMember]
		public Token SessionToken { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x000024B2 File Offset: 0x000006B2
		// (set) Token: 0x06000559 RID: 1369 RVA: 0x000024BA File Offset: 0x000006BA
		[DataMember]
		public ClientParametersDTO ClientParameters { get; set; }
	}
}
