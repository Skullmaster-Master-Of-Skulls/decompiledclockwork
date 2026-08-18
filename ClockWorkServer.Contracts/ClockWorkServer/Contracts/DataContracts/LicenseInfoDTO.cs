using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DataContracts
{
	// Token: 0x020000DF RID: 223
	[DataContract(Namespace = "http://tpro.ca")]
	public class LicenseInfoDTO
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060005DD RID: 1501 RVA: 0x00002706 File Offset: 0x00000906
		// (set) Token: 0x060005DE RID: 1502 RVA: 0x0000270E File Offset: 0x0000090E
		[DataMember]
		public virtual string ProductName { get; set; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060005DF RID: 1503 RVA: 0x00002717 File Offset: 0x00000917
		// (set) Token: 0x060005E0 RID: 1504 RVA: 0x0000271F File Offset: 0x0000091F
		[DataMember]
		public virtual string LicenseKey { get; set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00002728 File Offset: 0x00000928
		// (set) Token: 0x060005E2 RID: 1506 RVA: 0x00002730 File Offset: 0x00000930
		[DataMember]
		public virtual DateTime IssuedDate { get; set; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x00002739 File Offset: 0x00000939
		// (set) Token: 0x060005E4 RID: 1508 RVA: 0x00002741 File Offset: 0x00000941
		[DataMember]
		public virtual DateTime? ExpiryDate { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x0000274A File Offset: 0x0000094A
		// (set) Token: 0x060005E6 RID: 1510 RVA: 0x00002752 File Offset: 0x00000952
		[DataMember]
		public virtual LicenseType LicenseType { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0000275B File Offset: 0x0000095B
		// (set) Token: 0x060005E8 RID: 1512 RVA: 0x00002763 File Offset: 0x00000963
		[DataMember]
		public virtual int NLicenses { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x0000276C File Offset: 0x0000096C
		// (set) Token: 0x060005EA RID: 1514 RVA: 0x00002774 File Offset: 0x00000974
		[DataMember]
		public virtual string LicensedTo { get; set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x0000277D File Offset: 0x0000097D
		// (set) Token: 0x060005EC RID: 1516 RVA: 0x00002785 File Offset: 0x00000985
		[DataMember]
		public virtual LicenseStatus LicenseStatus { get; set; }
	}
}
