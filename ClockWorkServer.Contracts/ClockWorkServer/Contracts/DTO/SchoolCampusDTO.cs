using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000ED RID: 237
	[DataContract(Namespace = "http://tpro.ca")]
	public class SchoolCampusDTO
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000626 RID: 1574 RVA: 0x00002915 File Offset: 0x00000B15
		// (set) Token: 0x06000627 RID: 1575 RVA: 0x0000291D File Offset: 0x00000B1D
		[DataMember]
		public int CampusId { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000628 RID: 1576 RVA: 0x00002926 File Offset: 0x00000B26
		// (set) Token: 0x06000629 RID: 1577 RVA: 0x0000292E File Offset: 0x00000B2E
		[DataMember]
		public string CampusName { get; set; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x00002937 File Offset: 0x00000B37
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0000293F File Offset: 0x00000B3F
		[DataMember]
		public string Description { get; set; }
	}
}
