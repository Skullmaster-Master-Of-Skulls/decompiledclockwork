using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000E6 RID: 230
	[DataContract(Namespace = "http://tpro.ca")]
	public class Token
	{
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00002827 File Offset: 0x00000A27
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x0000282F File Offset: 0x00000A2F
		[DataMember]
		public string SessionId { get; set; }
	}
}
