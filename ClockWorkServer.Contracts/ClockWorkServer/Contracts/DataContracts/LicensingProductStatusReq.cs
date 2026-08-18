using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000E3 RID: 227
	[DataContract(Namespace = "http://tpro.ca")]
	public class LicensingProductStatusReq : BaseMessageReq
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x0000278E File Offset: 0x0000098E
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00002796 File Offset: 0x00000996
		[DataMember]
		public string ProductName { get; set; }
	}
}
