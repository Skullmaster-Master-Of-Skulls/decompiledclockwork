using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000848 RID: 2120
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerJobInfoDTO
	{
		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x0001476C File Offset: 0x0001296C
		// (set) Token: 0x06002B21 RID: 11041 RVA: 0x00014774 File Offset: 0x00012974
		[DataMember]
		public int JobId { get; set; }

		// Token: 0x17000F1D RID: 3869
		// (get) Token: 0x06002B22 RID: 11042 RVA: 0x0001477D File Offset: 0x0001297D
		// (set) Token: 0x06002B23 RID: 11043 RVA: 0x00014785 File Offset: 0x00012985
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000F1E RID: 3870
		// (get) Token: 0x06002B24 RID: 11044 RVA: 0x0001478E File Offset: 0x0001298E
		// (set) Token: 0x06002B25 RID: 11045 RVA: 0x00014796 File Offset: 0x00012996
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x17000F1F RID: 3871
		// (get) Token: 0x06002B26 RID: 11046 RVA: 0x0001479F File Offset: 0x0001299F
		// (set) Token: 0x06002B27 RID: 11047 RVA: 0x000147A7 File Offset: 0x000129A7
		[DataMember]
		public TimeSpan StartTime { get; set; }

		// Token: 0x17000F20 RID: 3872
		// (get) Token: 0x06002B28 RID: 11048 RVA: 0x000147B0 File Offset: 0x000129B0
		// (set) Token: 0x06002B29 RID: 11049 RVA: 0x000147B8 File Offset: 0x000129B8
		[DataMember]
		public ClockWorkServerJobScheduleDTO JobSchedule { get; set; }

		// Token: 0x17000F21 RID: 3873
		// (get) Token: 0x06002B2A RID: 11050 RVA: 0x000147C1 File Offset: 0x000129C1
		// (set) Token: 0x06002B2B RID: 11051 RVA: 0x000147C9 File Offset: 0x000129C9
		[DataMember]
		public TimeSpan Timeout { get; set; }

		// Token: 0x17000F22 RID: 3874
		// (get) Token: 0x06002B2C RID: 11052 RVA: 0x000147D2 File Offset: 0x000129D2
		// (set) Token: 0x06002B2D RID: 11053 RVA: 0x000147DA File Offset: 0x000129DA
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06002B2E RID: 11054 RVA: 0x000147E3 File Offset: 0x000129E3
		// (set) Token: 0x06002B2F RID: 11055 RVA: 0x000147EB File Offset: 0x000129EB
		[DataMember]
		public Guid JobUniqueId { get; set; }

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x000147F4 File Offset: 0x000129F4
		// (set) Token: 0x06002B31 RID: 11057 RVA: 0x000147FC File Offset: 0x000129FC
		[DataMember]
		public DateTime? LastRunStartDatetime { get; set; }

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x00014805 File Offset: 0x00012A05
		// (set) Token: 0x06002B33 RID: 11059 RVA: 0x0001480D File Offset: 0x00012A0D
		[DataMember]
		public DateTime? LastRunEndDatetime { get; set; }

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06002B34 RID: 11060 RVA: 0x00014816 File Offset: 0x00012A16
		// (set) Token: 0x06002B35 RID: 11061 RVA: 0x0001481E File Offset: 0x00012A1E
		[DataMember]
		public eClockWorkServerJobResult LastRunStatus { get; set; }

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06002B36 RID: 11062 RVA: 0x00014827 File Offset: 0x00012A27
		// (set) Token: 0x06002B37 RID: 11063 RVA: 0x0001482F File Offset: 0x00012A2F
		[DataMember]
		public string LastRunMessage { get; set; }

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x00014838 File Offset: 0x00012A38
		// (set) Token: 0x06002B39 RID: 11065 RVA: 0x00014840 File Offset: 0x00012A40
		[DataMember]
		public IList<ClockWorkServerJobStepDTO> JobSteps { get; set; }

		// Token: 0x17000F29 RID: 3881
		// (get) Token: 0x06002B3A RID: 11066 RVA: 0x00014849 File Offset: 0x00012A49
		// (set) Token: 0x06002B3B RID: 11067 RVA: 0x00014851 File Offset: 0x00012A51
		[DataMember]
		public ClockWorkServerJobInfoDTO.CredentialsDTO Impersonate { get; set; }

		// Token: 0x17000F2A RID: 3882
		// (get) Token: 0x06002B3C RID: 11068 RVA: 0x0001485A File Offset: 0x00012A5A
		// (set) Token: 0x06002B3D RID: 11069 RVA: 0x00014862 File Offset: 0x00012A62
		[DataMember]
		public bool IsSystemJob { get; set; }

		// Token: 0x06002B3E RID: 11070 RVA: 0x0001486B File Offset: 0x00012A6B
		public ClockWorkServerJobInfoDTO()
		{
			this.Init();
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x0001487C File Offset: 0x00012A7C
		[OnDeserializing]
		private void OnDeserializing(StreamingContext ctx)
		{
			this.Init();
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x00014886 File Offset: 0x00012A86
		private void Init()
		{
			this.Timeout = TimeSpan.FromHours(20.0);
		}

		// Token: 0x02000CA6 RID: 3238
		[DataContract(Namespace = "http://tpro.ca")]
		public class CredentialsDTO
		{
			// Token: 0x170018BD RID: 6333
			// (get) Token: 0x06004384 RID: 17284 RVA: 0x000247DC File Offset: 0x000229DC
			// (set) Token: 0x06004385 RID: 17285 RVA: 0x000247E4 File Offset: 0x000229E4
			[DataMember]
			public string Domain { get; set; }

			// Token: 0x170018BE RID: 6334
			// (get) Token: 0x06004386 RID: 17286 RVA: 0x000247ED File Offset: 0x000229ED
			// (set) Token: 0x06004387 RID: 17287 RVA: 0x000247F5 File Offset: 0x000229F5
			[DataMember]
			public string Username { get; set; }

			// Token: 0x170018BF RID: 6335
			// (get) Token: 0x06004388 RID: 17288 RVA: 0x000247FE File Offset: 0x000229FE
			// (set) Token: 0x06004389 RID: 17289 RVA: 0x00024806 File Offset: 0x00022A06
			[DataMember]
			public string Password { get; set; }
		}
	}
}
