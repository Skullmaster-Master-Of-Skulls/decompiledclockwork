using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000E4 RID: 228
	[DataContract(Namespace = "http://tpro.ca")]
	public class LicensingProductStatusResp
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x0000279F File Offset: 0x0000099F
		// (set) Token: 0x060005F2 RID: 1522 RVA: 0x000027A7 File Offset: 0x000009A7
		[DataMember]
		public ProductLicenseStatus LicenseStatus { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x000027B0 File Offset: 0x000009B0
		// (set) Token: 0x060005F4 RID: 1524 RVA: 0x000027B8 File Offset: 0x000009B8
		[DataMember]
		public DateTime? ExpiryDate { get; set; }
	}
}
