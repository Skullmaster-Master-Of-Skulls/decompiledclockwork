using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x02000118 RID: 280
	[DataContract(Namespace = "http://tpro.ca")]
	public class VetsChapterDTO
	{
		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600070D RID: 1805 RVA: 0x0000313A File Offset: 0x0000133A
		// (set) Token: 0x0600070E RID: 1806 RVA: 0x00003142 File Offset: 0x00001342
		[DataMember]
		public virtual Guid ChapterId { get; set; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600070F RID: 1807 RVA: 0x0000314B File Offset: 0x0000134B
		// (set) Token: 0x06000710 RID: 1808 RVA: 0x00003153 File Offset: 0x00001353
		[DataMember]
		public string ChapterTitle { get; set; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000711 RID: 1809 RVA: 0x0000315C File Offset: 0x0000135C
		// (set) Token: 0x06000712 RID: 1810 RVA: 0x00003164 File Offset: 0x00001364
		[DataMember]
		public string ChapterDescription { get; set; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000713 RID: 1811 RVA: 0x0000316D File Offset: 0x0000136D
		// (set) Token: 0x06000714 RID: 1812 RVA: 0x00003175 File Offset: 0x00001375
		[DataMember]
		public Guid AssociatedFormId { get; set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0000317E File Offset: 0x0000137E
		// (set) Token: 0x06000716 RID: 1814 RVA: 0x00003186 File Offset: 0x00001386
		[DataMember]
		public bool IsDisabled { get; set; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0000318F File Offset: 0x0000138F
		// (set) Token: 0x06000718 RID: 1816 RVA: 0x00003197 File Offset: 0x00001397
		[DataMember]
		public int OrderNum { get; set; }
	}
}
