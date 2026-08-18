using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProviderOriginal
{
	// Token: 0x020002DA RID: 730
	[DataContract(Namespace = "http://tpro.ca")]
	public class ServiceProviderAssignmentDTO : ServiceRequestDTO
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600106C RID: 4204 RVA: 0x000079EC File Offset: 0x00005BEC
		// (set) Token: 0x0600106D RID: 4205 RVA: 0x000079F4 File Offset: 0x00005BF4
		[DataMember]
		public StudentCommonInfoDTO StudentCommonInfo { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x000079FD File Offset: 0x00005BFD
		// (set) Token: 0x0600106F RID: 4207 RVA: 0x00007A05 File Offset: 0x00005C05
		[DataMember]
		public ServiceProviderBaseDTO AssignedServiceProvider { get; set; }
	}
}
