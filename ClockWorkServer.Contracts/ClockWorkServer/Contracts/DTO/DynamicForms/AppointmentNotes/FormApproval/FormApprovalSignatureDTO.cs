using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020006D7 RID: 1751
	[DataContract(Namespace = "http://tpro.ca")]
	public class FormApprovalSignatureDTO
	{
		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x060023D2 RID: 9170 RVA: 0x000105E9 File Offset: 0x0000E7E9
		// (set) Token: 0x060023D3 RID: 9171 RVA: 0x000105F1 File Offset: 0x0000E7F1
		[DataMember]
		public Guid FormApprovalSignatureId { get; set; }

		// Token: 0x17000C3E RID: 3134
		// (get) Token: 0x060023D4 RID: 9172 RVA: 0x000105FA File Offset: 0x0000E7FA
		// (set) Token: 0x060023D5 RID: 9173 RVA: 0x00010602 File Offset: 0x0000E802
		[DataMember]
		public DateTime DateSigned { get; set; }

		// Token: 0x17000C3F RID: 3135
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x0001060B File Offset: 0x0000E80B
		// (set) Token: 0x060023D7 RID: 9175 RVA: 0x00010613 File Offset: 0x0000E813
		[DataMember]
		public BasicPersonDTO WhoSigned { get; set; }

		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x0001061C File Offset: 0x0000E81C
		// (set) Token: 0x060023D9 RID: 9177 RVA: 0x00010624 File Offset: 0x0000E824
		[DataMember]
		public byte[] SignatureImage { get; set; }

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x0001062D File Offset: 0x0000E82D
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x00010635 File Offset: 0x0000E835
		[DataMember]
		public string SignatureText { get; set; }
	}
}
