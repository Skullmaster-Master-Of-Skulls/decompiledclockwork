using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000726 RID: 1830
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCustomExternalColumnNamesResp
	{
		// Token: 0x17000D00 RID: 3328
		// (get) Token: 0x060025A2 RID: 9634 RVA: 0x000112DC File Offset: 0x0000F4DC
		// (set) Token: 0x060025A3 RID: 9635 RVA: 0x000112E4 File Offset: 0x0000F4E4
		[DataMember]
		public string[] ExternalColumnNames { get; set; }
	}
}
