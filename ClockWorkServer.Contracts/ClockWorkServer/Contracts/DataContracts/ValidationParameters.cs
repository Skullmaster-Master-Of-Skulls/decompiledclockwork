using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000E7 RID: 231
	[DataContract(Namespace = "http://tpro.ca")]
	public class ValidationParameters
	{
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000606 RID: 1542 RVA: 0x00002838 File Offset: 0x00000A38
		// (set) Token: 0x06000607 RID: 1543 RVA: 0x00002840 File Offset: 0x00000A40
		[DataMember]
		public string ProductName { get; set; }

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x00002849 File Offset: 0x00000A49
		// (set) Token: 0x06000609 RID: 1545 RVA: 0x00002851 File Offset: 0x00000A51
		[DataMember]
		public string Parameters { get; set; }
	}
}
