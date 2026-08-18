using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.ClockWorkServerJob;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x02000859 RID: 2137
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateClockWorkServerJobLastRunReq : BaseMessageReq
	{
		// Token: 0x17000F4A RID: 3914
		// (get) Token: 0x06002B8F RID: 11151 RVA: 0x00014AAD File Offset: 0x00012CAD
		// (set) Token: 0x06002B90 RID: 11152 RVA: 0x00014AB5 File Offset: 0x00012CB5
		[DataMember]
		public int JobId { get; set; }

		// Token: 0x17000F4B RID: 3915
		// (get) Token: 0x06002B91 RID: 11153 RVA: 0x00014ABE File Offset: 0x00012CBE
		// (set) Token: 0x06002B92 RID: 11154 RVA: 0x00014AC6 File Offset: 0x00012CC6
		[DataMember]
		public DateTime? LastRunStartDatetime { get; set; }

		// Token: 0x17000F4C RID: 3916
		// (get) Token: 0x06002B93 RID: 11155 RVA: 0x00014ACF File Offset: 0x00012CCF
		// (set) Token: 0x06002B94 RID: 11156 RVA: 0x00014AD7 File Offset: 0x00012CD7
		[DataMember]
		public DateTime? LastRunEndDatetime { get; set; }

		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x06002B95 RID: 11157 RVA: 0x00014AE0 File Offset: 0x00012CE0
		// (set) Token: 0x06002B96 RID: 11158 RVA: 0x00014AE8 File Offset: 0x00012CE8
		[DataMember]
		public eClockWorkServerJobResult LastRunStatus { get; set; }

		// Token: 0x17000F4E RID: 3918
		// (get) Token: 0x06002B97 RID: 11159 RVA: 0x00014AF1 File Offset: 0x00012CF1
		// (set) Token: 0x06002B98 RID: 11160 RVA: 0x00014AF9 File Offset: 0x00012CF9
		[DataMember]
		public string LastRunMessage { get; set; }
	}
}
