using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ClockWorkServerJob
{
	// Token: 0x0200084C RID: 2124
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClockWorkServerJobInfoExDTO
	{
		// Token: 0x17000F3F RID: 3903
		// (get) Token: 0x06002B6C RID: 11116 RVA: 0x000149F2 File Offset: 0x00012BF2
		// (set) Token: 0x06002B6D RID: 11117 RVA: 0x000149FA File Offset: 0x00012BFA
		[DataMember]
		public ClockWorkServerJobInfoDTO JobInfo { get; set; }

		// Token: 0x17000F40 RID: 3904
		// (get) Token: 0x06002B6E RID: 11118 RVA: 0x00014A03 File Offset: 0x00012C03
		// (set) Token: 0x06002B6F RID: 11119 RVA: 0x00014A0B File Offset: 0x00012C0B
		[DataMember]
		public bool IsRunning { get; set; }

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x00014A14 File Offset: 0x00012C14
		// (set) Token: 0x06002B71 RID: 11121 RVA: 0x00014A1C File Offset: 0x00012C1C
		[DataMember]
		public int JobProcessId { get; set; }
	}
}
