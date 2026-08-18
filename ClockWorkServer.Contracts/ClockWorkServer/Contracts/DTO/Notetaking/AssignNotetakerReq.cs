using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000450 RID: 1104
	[DataContract(Namespace = "http://tpro.ca")]
	public class AssignNotetakerReq : BaseReportMessageReq
	{
		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x060017A0 RID: 6048 RVA: 0x0000AEB0 File Offset: 0x000090B0
		// (set) Token: 0x060017A1 RID: 6049 RVA: 0x0000AEB8 File Offset: 0x000090B8
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x060017A2 RID: 6050 RVA: 0x0000AEC1 File Offset: 0x000090C1
		// (set) Token: 0x060017A3 RID: 6051 RVA: 0x0000AEC9 File Offset: 0x000090C9
		[DataMember]
		public int StudentLuCourseId { get; set; }

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x060017A4 RID: 6052 RVA: 0x0000AED2 File Offset: 0x000090D2
		// (set) Token: 0x060017A5 RID: 6053 RVA: 0x0000AEDA File Offset: 0x000090DA
		[DataMember]
		public int NotetakerId { get; set; }

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x060017A6 RID: 6054 RVA: 0x0000AEE3 File Offset: 0x000090E3
		// (set) Token: 0x060017A7 RID: 6055 RVA: 0x0000AEEB File Offset: 0x000090EB
		[DataMember]
		public int NotetakerLuCourseId { get; set; }
	}
}
