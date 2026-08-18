using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200084A RID: 2122
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerJobExecutionLogDTO
	{
		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x00014926 File Offset: 0x00012B26
		// (set) Token: 0x06002B53 RID: 11091 RVA: 0x0001492E File Offset: 0x00012B2E
		[DataMember]
		public int ExecutionLogId { get; set; }

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x00014937 File Offset: 0x00012B37
		// (set) Token: 0x06002B55 RID: 11093 RVA: 0x0001493F File Offset: 0x00012B3F
		[DataMember]
		public ClockWorkServerJobStepDTO Step { get; set; }

		// Token: 0x17000F35 RID: 3893
		// (get) Token: 0x06002B56 RID: 11094 RVA: 0x00014948 File Offset: 0x00012B48
		// (set) Token: 0x06002B57 RID: 11095 RVA: 0x00014950 File Offset: 0x00012B50
		[DataMember]
		public eClockWorkServerJobResult Status { get; set; }

		// Token: 0x17000F36 RID: 3894
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x00014959 File Offset: 0x00012B59
		// (set) Token: 0x06002B59 RID: 11097 RVA: 0x00014961 File Offset: 0x00012B61
		[DataMember]
		public DateTime StartTime { get; set; }

		// Token: 0x17000F37 RID: 3895
		// (get) Token: 0x06002B5A RID: 11098 RVA: 0x0001496A File Offset: 0x00012B6A
		// (set) Token: 0x06002B5B RID: 11099 RVA: 0x00014972 File Offset: 0x00012B72
		[DataMember]
		public DateTime? EndTime { get; set; }

		// Token: 0x17000F38 RID: 3896
		// (get) Token: 0x06002B5C RID: 11100 RVA: 0x0001497B File Offset: 0x00012B7B
		// (set) Token: 0x06002B5D RID: 11101 RVA: 0x00014983 File Offset: 0x00012B83
		[DataMember]
		public string Message { get; set; }

		// Token: 0x17000F39 RID: 3897
		// (get) Token: 0x06002B5E RID: 11102 RVA: 0x0001498C File Offset: 0x00012B8C
		// (set) Token: 0x06002B5F RID: 11103 RVA: 0x00014994 File Offset: 0x00012B94
		[DataMember]
		public string ServerIpAddress { get; set; }

		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x06002B60 RID: 11104 RVA: 0x0001499D File Offset: 0x00012B9D
		// (set) Token: 0x06002B61 RID: 11105 RVA: 0x000149A5 File Offset: 0x00012BA5
		[DataMember]
		public Guid TransactionId { get; set; }
	}
}
