using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200033C RID: 828
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportCompileLineWarningOrErrorDTO
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x060012A8 RID: 4776 RVA: 0x00008B1B File Offset: 0x00006D1B
		// (set) Token: 0x060012A9 RID: 4777 RVA: 0x00008B23 File Offset: 0x00006D23
		[DataMember]
		public eReportCompileLineWarningOrErrorType LineType { get; set; }

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060012AA RID: 4778 RVA: 0x00008B2C File Offset: 0x00006D2C
		// (set) Token: 0x060012AB RID: 4779 RVA: 0x00008B34 File Offset: 0x00006D34
		[DataMember]
		public string Message { get; set; }

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060012AC RID: 4780 RVA: 0x00008B3D File Offset: 0x00006D3D
		// (set) Token: 0x060012AD RID: 4781 RVA: 0x00008B45 File Offset: 0x00006D45
		[DataMember]
		public int LineNumber { get; set; }

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060012AE RID: 4782 RVA: 0x00008B4E File Offset: 0x00006D4E
		// (set) Token: 0x060012AF RID: 4783 RVA: 0x00008B56 File Offset: 0x00006D56
		[DataMember]
		public int ColumnNumber { get; set; }

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060012B0 RID: 4784 RVA: 0x00008B5F File Offset: 0x00006D5F
		// (set) Token: 0x060012B1 RID: 4785 RVA: 0x00008B67 File Offset: 0x00006D67
		[DataMember]
		public string Filename { get; set; }
	}
}
