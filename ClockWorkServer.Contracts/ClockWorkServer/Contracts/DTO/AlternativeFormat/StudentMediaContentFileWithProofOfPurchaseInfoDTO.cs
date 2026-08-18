using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AlternativeFormat;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B4C RID: 2892
	[DataContract(Namespace = "http://tpro.ca")]
	public class StudentMediaContentFileWithProofOfPurchaseInfoDTO : MediaContentFileWithoutDataDTO
	{
		// Token: 0x170016A9 RID: 5801
		// (get) Token: 0x06003D84 RID: 15748 RVA: 0x0001E3C4 File Offset: 0x0001C5C4
		// (set) Token: 0x06003D85 RID: 15749 RVA: 0x0001E3CC File Offset: 0x0001C5CC
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170016AA RID: 5802
		// (get) Token: 0x06003D86 RID: 15750 RVA: 0x0001E3D5 File Offset: 0x0001C5D5
		// (set) Token: 0x06003D87 RID: 15751 RVA: 0x0001E3DD File Offset: 0x0001C5DD
		[DataMember]
		public eStudentMediaContentFileStatus FileStatus { get; set; }

		// Token: 0x170016AB RID: 5803
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x0001E3E6 File Offset: 0x0001C5E6
		// (set) Token: 0x06003D89 RID: 15753 RVA: 0x0001E3EE File Offset: 0x0001C5EE
		[DataMember]
		public int ProofOfPurchaseId { get; set; }

		// Token: 0x170016AC RID: 5804
		// (get) Token: 0x06003D8A RID: 15754 RVA: 0x0001E3F7 File Offset: 0x0001C5F7
		// (set) Token: 0x06003D8B RID: 15755 RVA: 0x0001E3FF File Offset: 0x0001C5FF
		[DataMember]
		public string StudentCompletionRequestNotes { get; set; }
	}
}
