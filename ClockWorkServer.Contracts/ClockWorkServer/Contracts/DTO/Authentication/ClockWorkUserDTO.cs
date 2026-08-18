using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Authentication
{
	// Token: 0x020008E0 RID: 2272
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkUserDTO
	{
		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x00015CD7 File Offset: 0x00013ED7
		// (set) Token: 0x06002E12 RID: 11794 RVA: 0x00015CDF File Offset: 0x00013EDF
		[DataMember]
		public string Username { get; set; }

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x00015CE8 File Offset: 0x00013EE8
		// (set) Token: 0x06002E14 RID: 11796 RVA: 0x00015CF0 File Offset: 0x00013EF0
		[DataMember]
		public string StudentNumber { get; set; }

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x00015CF9 File Offset: 0x00013EF9
		// (set) Token: 0x06002E16 RID: 11798 RVA: 0x00015D01 File Offset: 0x00013F01
		[DataMember]
		public int ClockWorkPid { get; set; }

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06002E17 RID: 11799 RVA: 0x00015D0A File Offset: 0x00013F0A
		// (set) Token: 0x06002E18 RID: 11800 RVA: 0x00015D12 File Offset: 0x00013F12
		[DataMember]
		public int ClockWorkIid { get; set; }

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06002E19 RID: 11801 RVA: 0x00015D1B File Offset: 0x00013F1B
		// (set) Token: 0x06002E1A RID: 11802 RVA: 0x00015D23 File Offset: 0x00013F23
		[DataMember]
		public int ClockWorkNid { get; set; }

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06002E1B RID: 11803 RVA: 0x00015D2C File Offset: 0x00013F2C
		// (set) Token: 0x06002E1C RID: 11804 RVA: 0x00015D34 File Offset: 0x00013F34
		[DataMember]
		public int ClockWorkAltContactId { get; set; }
	}
}
