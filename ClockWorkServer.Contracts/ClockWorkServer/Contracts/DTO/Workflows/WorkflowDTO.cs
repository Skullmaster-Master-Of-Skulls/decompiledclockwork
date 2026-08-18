using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Workflows
{
	// Token: 0x020000FD RID: 253
	[DataContract(Namespace = "http://tpro.ca")]
	public class WorkflowDTO
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x00002D02 File Offset: 0x00000F02
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x00002D0A File Offset: 0x00000F0A
		[DataMember]
		public ProgressStepDTO[] ProgressSteps { get; set; }
	}
}
