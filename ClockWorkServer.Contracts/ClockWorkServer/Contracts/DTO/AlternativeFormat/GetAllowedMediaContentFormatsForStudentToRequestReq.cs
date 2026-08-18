using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C6A RID: 3178
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAllowedMediaContentFormatsForStudentToRequestReq : BaseReportMessageReq
	{
		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x06004228 RID: 16936 RVA: 0x000204B0 File Offset: 0x0001E6B0
		// (set) Token: 0x06004229 RID: 16937 RVA: 0x000204B8 File Offset: 0x0001E6B8
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x0600422A RID: 16938 RVA: 0x000204C1 File Offset: 0x0001E6C1
		// (set) Token: 0x0600422B RID: 16939 RVA: 0x000204C9 File Offset: 0x0001E6C9
		[DataMember]
		public int SelectedLuCourseId { get; set; }

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x0600422C RID: 16940 RVA: 0x000204D2 File Offset: 0x0001E6D2
		// (set) Token: 0x0600422D RID: 16941 RVA: 0x000204DA File Offset: 0x0001E6DA
		[DataMember]
		public MediaContentIdentifierDTO MediaContentIdentifier { get; set; }
	}
}
