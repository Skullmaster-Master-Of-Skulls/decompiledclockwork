using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkDailyJob
{
	// Token: 0x02000893 RID: 2195
	[DataContract(Namespace = "http://tpro.ca")]
	public class DailyJobTaskResultDTO
	{
		// Token: 0x17000F9F RID: 3999
		// (get) Token: 0x06002C72 RID: 11378 RVA: 0x0001506E File Offset: 0x0001326E
		// (set) Token: 0x06002C73 RID: 11379 RVA: 0x00015076 File Offset: 0x00013276
		[DataMember]
		public int WindowsTaskJobResultId { get; set; }

		// Token: 0x17000FA0 RID: 4000
		// (get) Token: 0x06002C74 RID: 11380 RVA: 0x0001507F File Offset: 0x0001327F
		// (set) Token: 0x06002C75 RID: 11381 RVA: 0x00015087 File Offset: 0x00013287
		[DataMember]
		public int WindowsTaskJobId { get; set; }

		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x06002C76 RID: 11382 RVA: 0x00015090 File Offset: 0x00013290
		// (set) Token: 0x06002C77 RID: 11383 RVA: 0x00015098 File Offset: 0x00013298
		[DataMember]
		public int ReportId { get; set; }

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06002C78 RID: 11384 RVA: 0x000150A1 File Offset: 0x000132A1
		// (set) Token: 0x06002C79 RID: 11385 RVA: 0x000150A9 File Offset: 0x000132A9
		[DataMember]
		public int TaskGroupId { get; set; }

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06002C7A RID: 11386 RVA: 0x000150B2 File Offset: 0x000132B2
		// (set) Token: 0x06002C7B RID: 11387 RVA: 0x000150BA File Offset: 0x000132BA
		[DataMember]
		public DateTime RunStartDate { get; set; }

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06002C7C RID: 11388 RVA: 0x000150C3 File Offset: 0x000132C3
		// (set) Token: 0x06002C7D RID: 11389 RVA: 0x000150CB File Offset: 0x000132CB
		[DataMember]
		public DateTime RunEndDate { get; set; }

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06002C7E RID: 11390 RVA: 0x000150D4 File Offset: 0x000132D4
		// (set) Token: 0x06002C7F RID: 11391 RVA: 0x000150DC File Offset: 0x000132DC
		[DataMember]
		public bool Successful { get; set; }

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06002C80 RID: 11392 RVA: 0x000150E5 File Offset: 0x000132E5
		// (set) Token: 0x06002C81 RID: 11393 RVA: 0x000150ED File Offset: 0x000132ED
		[DataMember]
		public string RunResult { get; set; }
	}
}
