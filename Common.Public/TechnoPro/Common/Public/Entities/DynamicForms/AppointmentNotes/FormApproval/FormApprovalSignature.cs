using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.DynamicForms.AppointmentNotes.FormApproval
{
	// Token: 0x020003B7 RID: 951
	public class FormApprovalSignature
	{
		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x00020FA4 File Offset: 0x0001F1A4
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x00020FAC File Offset: 0x0001F1AC
		public Guid FormApprovalSignatureId { get; set; }

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x00020FB5 File Offset: 0x0001F1B5
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x00020FBD File Offset: 0x0001F1BD
		public DateTime DateSigned { get; set; }

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x00020FC6 File Offset: 0x0001F1C6
		// (set) Token: 0x06001D11 RID: 7441 RVA: 0x00020FCE File Offset: 0x0001F1CE
		public BasicPerson WhoSigned { get; set; }

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x00020FD7 File Offset: 0x0001F1D7
		// (set) Token: 0x06001D13 RID: 7443 RVA: 0x00020FDF File Offset: 0x0001F1DF
		public byte[] SignatureImage { get; set; }

		// Token: 0x17000BF9 RID: 3065
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x00020FE8 File Offset: 0x0001F1E8
		// (set) Token: 0x06001D15 RID: 7445 RVA: 0x00020FF0 File Offset: 0x0001F1F0
		public string SignatureText { get; set; }
	}
}
