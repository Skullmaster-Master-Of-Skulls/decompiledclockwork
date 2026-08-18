using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200034D RID: 845
	[DataContract(Namespace = "http://tpro.ca")]
	public class ImportUpdateStudentReq
	{
		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x0600134D RID: 4941 RVA: 0x00009059 File Offset: 0x00007259
		// (set) Token: 0x0600134E RID: 4942 RVA: 0x00009061 File Offset: 0x00007261
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x0000906A File Offset: 0x0000726A
		// (set) Token: 0x06001350 RID: 4944 RVA: 0x00009072 File Offset: 0x00007272
		[DataMember]
		public string FirstName { get; set; }

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001351 RID: 4945 RVA: 0x0000907B File Offset: 0x0000727B
		// (set) Token: 0x06001352 RID: 4946 RVA: 0x00009083 File Offset: 0x00007283
		[DataMember]
		public string MiddleName { get; set; }

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001353 RID: 4947 RVA: 0x0000908C File Offset: 0x0000728C
		// (set) Token: 0x06001354 RID: 4948 RVA: 0x00009094 File Offset: 0x00007294
		[DataMember]
		public string LastName { get; set; }

		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001355 RID: 4949 RVA: 0x0000909D File Offset: 0x0000729D
		// (set) Token: 0x06001356 RID: 4950 RVA: 0x000090A5 File Offset: 0x000072A5
		[DataMember]
		public int ReportId_Preview { get; set; }

		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001357 RID: 4951 RVA: 0x000090AE File Offset: 0x000072AE
		// (set) Token: 0x06001358 RID: 4952 RVA: 0x000090B6 File Offset: 0x000072B6
		[DataMember]
		public int ReportId_Import { get; set; }

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x06001359 RID: 4953 RVA: 0x000090BF File Offset: 0x000072BF
		// (set) Token: 0x0600135A RID: 4954 RVA: 0x000090C7 File Offset: 0x000072C7
		[DataMember]
		public int ReportId_GetGroups { get; set; }

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x000090D0 File Offset: 0x000072D0
		// (set) Token: 0x0600135C RID: 4956 RVA: 0x000090D8 File Offset: 0x000072D8
		[DataMember]
		public int ReportId_ImportCourses { get; set; }

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x000090E1 File Offset: 0x000072E1
		// (set) Token: 0x0600135E RID: 4958 RVA: 0x000090E9 File Offset: 0x000072E9
		[DataMember]
		public string OverridePassword { get; set; }
	}
}
