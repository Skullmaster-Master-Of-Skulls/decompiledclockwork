using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000DB RID: 219
	[DataContract(Namespace = "http://tpro.ca")]
	public class Credential
	{
		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x000026A8 File Offset: 0x000008A8
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x000026B0 File Offset: 0x000008B0
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000026B9 File Offset: 0x000008B9
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x000026C1 File Offset: 0x000008C1
		[DataMember]
		public string Password { get; set; }
	}
}
