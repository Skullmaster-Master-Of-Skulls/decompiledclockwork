using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Form
{
	// Token: 0x02000750 RID: 1872
	[DataContract(Namespace = "http://tpro.ca")]
	public class CustomFormDTO
	{
		// Token: 0x17000D6B RID: 3435
		// (get) Token: 0x060026A1 RID: 9889 RVA: 0x00011F0D File Offset: 0x0001010D
		// (set) Token: 0x060026A2 RID: 9890 RVA: 0x00011F15 File Offset: 0x00010115
		[DataMember]
		public Guid FormId { get; set; }

		// Token: 0x17000D6C RID: 3436
		// (get) Token: 0x060026A3 RID: 9891 RVA: 0x00011F1E File Offset: 0x0001011E
		// (set) Token: 0x060026A4 RID: 9892 RVA: 0x00011F26 File Offset: 0x00010126
		[DataMember]
		public string Xml { get; set; }

		// Token: 0x17000D6D RID: 3437
		// (get) Token: 0x060026A5 RID: 9893 RVA: 0x00011F2F File Offset: 0x0001012F
		// (set) Token: 0x060026A6 RID: 9894 RVA: 0x00011F37 File Offset: 0x00010137
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000D6E RID: 3438
		// (get) Token: 0x060026A7 RID: 9895 RVA: 0x00011F40 File Offset: 0x00010140
		// (set) Token: 0x060026A8 RID: 9896 RVA: 0x00011F48 File Offset: 0x00010148
		[DataMember]
		public bool IsHidden { get; set; }
	}
}
