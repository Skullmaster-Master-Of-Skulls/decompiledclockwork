using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000B66 RID: 2918
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailableMediaContentFileByStudentAndMediaContentReq : BaseMessageReq
	{
		// Token: 0x170016CB RID: 5835
		// (get) Token: 0x06003DE2 RID: 15842 RVA: 0x0001E60F File Offset: 0x0001C80F
		// (set) Token: 0x06003DE3 RID: 15843 RVA: 0x0001E617 File Offset: 0x0001C817
		[DataMember]
		public int StudentId { get; set; }

		// Token: 0x170016CC RID: 5836
		// (get) Token: 0x06003DE4 RID: 15844 RVA: 0x0001E620 File Offset: 0x0001C820
		// (set) Token: 0x06003DE5 RID: 15845 RVA: 0x0001E628 File Offset: 0x0001C828
		[DataMember]
		public Guid MediaContentId { get; set; }

		// Token: 0x170016CD RID: 5837
		// (get) Token: 0x06003DE6 RID: 15846 RVA: 0x0001E631 File Offset: 0x0001C831
		// (set) Token: 0x06003DE7 RID: 15847 RVA: 0x0001E639 File Offset: 0x0001C839
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170016CE RID: 5838
		// (get) Token: 0x06003DE8 RID: 15848 RVA: 0x0001E642 File Offset: 0x0001C842
		// (set) Token: 0x06003DE9 RID: 15849 RVA: 0x0001E64A File Offset: 0x0001C84A
		[DataMember]
		public DateTime EndDate { get; set; }
	}
}
