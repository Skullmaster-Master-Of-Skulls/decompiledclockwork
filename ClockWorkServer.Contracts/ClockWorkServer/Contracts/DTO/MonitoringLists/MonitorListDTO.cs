using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MonitoringLists
{
	// Token: 0x02000454 RID: 1108
	[DataContract(Namespace = "http://tpro.ca")]
	public class MonitorListDTO
	{
		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x060017B6 RID: 6070 RVA: 0x0000AF49 File Offset: 0x00009149
		// (set) Token: 0x060017B7 RID: 6071 RVA: 0x0000AF51 File Offset: 0x00009151
		[DataMember]
		public string UniqueName { get; set; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060017B8 RID: 6072 RVA: 0x0000AF5A File Offset: 0x0000915A
		// (set) Token: 0x060017B9 RID: 6073 RVA: 0x0000AF62 File Offset: 0x00009162
		[DataMember]
		public int ReportId { get; set; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060017BA RID: 6074 RVA: 0x0000AF6B File Offset: 0x0000916B
		// (set) Token: 0x060017BB RID: 6075 RVA: 0x0000AF73 File Offset: 0x00009173
		[DataMember]
		public int SubReportId { get; set; }

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060017BC RID: 6076 RVA: 0x0000AF7C File Offset: 0x0000917C
		// (set) Token: 0x060017BD RID: 6077 RVA: 0x0000AF84 File Offset: 0x00009184
		[DataMember]
		public bool IsVisible { get; set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060017BE RID: 6078 RVA: 0x0000AF8D File Offset: 0x0000918D
		// (set) Token: 0x060017BF RID: 6079 RVA: 0x0000AF95 File Offset: 0x00009195
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060017C0 RID: 6080 RVA: 0x0000AF9E File Offset: 0x0000919E
		// (set) Token: 0x060017C1 RID: 6081 RVA: 0x0000AFA6 File Offset: 0x000091A6
		[DataMember]
		public string Title { get; set; }
	}
}
