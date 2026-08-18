using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Controls
{
	// Token: 0x02000779 RID: 1913
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomControlDataHolderDTO : CustomControlBaseDTO
	{
		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x0600274F RID: 10063 RVA: 0x0001267F File Offset: 0x0001087F
		// (set) Token: 0x06002750 RID: 10064 RVA: 0x00012687 File Offset: 0x00010887
		[DataMember]
		public Guid DataInstanceId { get; set; }

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x00012690 File Offset: 0x00010890
		// (set) Token: 0x06002752 RID: 10066 RVA: 0x00012698 File Offset: 0x00010898
		[DataMember]
		public eCustomControlValidationType ControlValidationStaffType { get; set; }

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002753 RID: 10067 RVA: 0x000126A1 File Offset: 0x000108A1
		// (set) Token: 0x06002754 RID: 10068 RVA: 0x000126A9 File Offset: 0x000108A9
		[DataMember]
		public eCustomControlValidationType ControlValidationStudentType { get; set; }
	}
}
