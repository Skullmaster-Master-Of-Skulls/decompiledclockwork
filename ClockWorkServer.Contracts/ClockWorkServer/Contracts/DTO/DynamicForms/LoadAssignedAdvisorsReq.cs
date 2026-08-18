using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200066F RID: 1647
	public class LoadAssignedAdvisorsReq : BaseMessageReq
	{
		// Token: 0x17000B44 RID: 2884
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x0000F2E5 File Offset: 0x0000D4E5
		// (set) Token: 0x06002175 RID: 8565 RVA: 0x0000F2ED File Offset: 0x0000D4ED
		[DataMember]
		public eDynamicFormType FormType { get; set; }

		// Token: 0x17000B45 RID: 2885
		// (get) Token: 0x06002176 RID: 8566 RVA: 0x0000F2F6 File Offset: 0x0000D4F6
		// (set) Token: 0x06002177 RID: 8567 RVA: 0x0000F2FE File Offset: 0x0000D4FE
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000B46 RID: 2886
		// (get) Token: 0x06002178 RID: 8568 RVA: 0x0000F307 File Offset: 0x0000D507
		// (set) Token: 0x06002179 RID: 8569 RVA: 0x0000F30F File Offset: 0x0000D50F
		[DataMember]
		public int[] AssignedAdvisorControlIds { get; set; }
	}
}
