using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200066A RID: 1642
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFileFromImageInfoReq : BaseMessageReq
	{
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x0600215B RID: 8539 RVA: 0x0000F23B File Offset: 0x0000D43B
		// (set) Token: 0x0600215C RID: 8540 RVA: 0x0000F243 File Offset: 0x0000D443
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x0600215D RID: 8541 RVA: 0x0000F24C File Offset: 0x0000D44C
		// (set) Token: 0x0600215E RID: 8542 RVA: 0x0000F254 File Offset: 0x0000D454
		[DataMember]
		public int DataId { get; set; }

		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x0600215F RID: 8543 RVA: 0x0000F25D File Offset: 0x0000D45D
		// (set) Token: 0x06002160 RID: 8544 RVA: 0x0000F265 File Offset: 0x0000D465
		[DataMember]
		public string DatabaseTablePostFix { get; set; }
	}
}
