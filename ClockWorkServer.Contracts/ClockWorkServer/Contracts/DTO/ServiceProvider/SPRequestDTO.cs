using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.ServiceProvider
{
	// Token: 0x02000275 RID: 629
	[DataContract(Namespace = "http://tpro.ca")]
	public class SPRequestDTO
	{
		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000ED1 RID: 3793 RVA: 0x00006FA1 File Offset: 0x000051A1
		// (set) Token: 0x06000ED2 RID: 3794 RVA: 0x00006FA9 File Offset: 0x000051A9
		[DataMember]
		public int SPRequestId { get; set; }

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000ED3 RID: 3795 RVA: 0x00006FB2 File Offset: 0x000051B2
		// (set) Token: 0x06000ED4 RID: 3796 RVA: 0x00006FBA File Offset: 0x000051BA
		[DataMember]
		public SPProviderTypeDTO ProviderType { get; set; }

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000ED5 RID: 3797 RVA: 0x00006FC3 File Offset: 0x000051C3
		// (set) Token: 0x06000ED6 RID: 3798 RVA: 0x00006FCB File Offset: 0x000051CB
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000ED7 RID: 3799 RVA: 0x00006FD4 File Offset: 0x000051D4
		// (set) Token: 0x06000ED8 RID: 3800 RVA: 0x00006FDC File Offset: 0x000051DC
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00006FE5 File Offset: 0x000051E5
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00006FED File Offset: 0x000051ED
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00006FF6 File Offset: 0x000051F6
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00006FFE File Offset: 0x000051FE
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00007007 File Offset: 0x00005207
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x0000700F File Offset: 0x0000520F
		[DataMember]
		public string SpecialInstructions { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00007018 File Offset: 0x00005218
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00007020 File Offset: 0x00005220
		[DataMember]
		public SPRequestStatusTypeDTO RequestStatus { get; set; }

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000EE1 RID: 3809 RVA: 0x00007029 File Offset: 0x00005229
		// (set) Token: 0x06000EE2 RID: 3810 RVA: 0x00007031 File Offset: 0x00005231
		[DataMember]
		public SPRequestAssignmentStatusTypeDTO AssignmentStatus { get; set; }

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000EE3 RID: 3811 RVA: 0x0000703A File Offset: 0x0000523A
		// (set) Token: 0x06000EE4 RID: 3812 RVA: 0x00007042 File Offset: 0x00005242
		[DataMember]
		public SPUrgencyLevelTypeDTO UrgencyLevel { get; set; }

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x06000EE5 RID: 3813 RVA: 0x0000704B File Offset: 0x0000524B
		// (set) Token: 0x06000EE6 RID: 3814 RVA: 0x00007053 File Offset: 0x00005253
		[DataMember]
		public bool IsActive { get; set; }
	}
}
