using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Workflows
{
	// Token: 0x020000FC RID: 252
	[DataContract(Namespace = "http://tpro.ca")]
	public class ProgressStepDTO
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000667 RID: 1639 RVA: 0x00002C9C File Offset: 0x00000E9C
		// (set) Token: 0x06000668 RID: 1640 RVA: 0x00002CA4 File Offset: 0x00000EA4
		[DataMember]
		public virtual Guid ProgressStepId { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000669 RID: 1641 RVA: 0x00002CAD File Offset: 0x00000EAD
		// (set) Token: 0x0600066A RID: 1642 RVA: 0x00002CB5 File Offset: 0x00000EB5
		[DataMember]
		public eWorkflowType WorkflowType { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600066B RID: 1643 RVA: 0x00002CBE File Offset: 0x00000EBE
		// (set) Token: 0x0600066C RID: 1644 RVA: 0x00002CC6 File Offset: 0x00000EC6
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600066D RID: 1645 RVA: 0x00002CCF File Offset: 0x00000ECF
		// (set) Token: 0x0600066E RID: 1646 RVA: 0x00002CD7 File Offset: 0x00000ED7
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600066F RID: 1647 RVA: 0x00002CE0 File Offset: 0x00000EE0
		// (set) Token: 0x06000670 RID: 1648 RVA: 0x00002CE8 File Offset: 0x00000EE8
		[DataMember]
		public int ProgressStepNumber { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00002CF1 File Offset: 0x00000EF1
		// (set) Token: 0x06000672 RID: 1650 RVA: 0x00002CF9 File Offset: 0x00000EF9
		[DataMember]
		public int ProgressStepTotalCount { get; set; }
	}
}
