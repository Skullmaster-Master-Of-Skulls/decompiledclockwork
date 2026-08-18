using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Academic;
using TechnoPro.Common.Public.Entities.Vets;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Vets
{
	// Token: 0x0200011E RID: 286
	[DataContract(Namespace = "http://tpro.ca")]
	public class VetsStudentCardInfoItemDTO
	{
		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x06000733 RID: 1843 RVA: 0x0000324A File Offset: 0x0000144A
		// (set) Token: 0x06000734 RID: 1844 RVA: 0x00003252 File Offset: 0x00001452
		[DataMember]
		public virtual Guid VetsBenefitApplicationId { get; set; }

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x06000735 RID: 1845 RVA: 0x0000325B File Offset: 0x0000145B
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x00003263 File Offset: 0x00001463
		[DataMember]
		public Guid? ChapterId { get; set; }

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000737 RID: 1847 RVA: 0x0000326C File Offset: 0x0000146C
		// (set) Token: 0x06000738 RID: 1848 RVA: 0x00003274 File Offset: 0x00001474
		[DataMember]
		public string ChapterTitle { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000739 RID: 1849 RVA: 0x0000327D File Offset: 0x0000147D
		// (set) Token: 0x0600073A RID: 1850 RVA: 0x00003285 File Offset: 0x00001485
		[DataMember]
		public SemesterDTO Semester { get; set; }

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600073B RID: 1851 RVA: 0x0000328E File Offset: 0x0000148E
		// (set) Token: 0x0600073C RID: 1852 RVA: 0x00003296 File Offset: 0x00001496
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x0600073D RID: 1853 RVA: 0x0000329F File Offset: 0x0000149F
		// (set) Token: 0x0600073E RID: 1854 RVA: 0x000032A7 File Offset: 0x000014A7
		[DataMember]
		public DateTime? DateLastModified { get; set; }

		// Token: 0x170000DC RID: 220
		// (get) Token: 0x0600073F RID: 1855 RVA: 0x000032B0 File Offset: 0x000014B0
		// (set) Token: 0x06000740 RID: 1856 RVA: 0x000032B8 File Offset: 0x000014B8
		[DataMember]
		public bool StudentAgreeCompleted { get; set; }

		// Token: 0x170000DD RID: 221
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x000032C1 File Offset: 0x000014C1
		// (set) Token: 0x06000742 RID: 1858 RVA: 0x000032C9 File Offset: 0x000014C9
		[DataMember]
		public bool BenAppCompleted { get; set; }

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x06000743 RID: 1859 RVA: 0x000032D2 File Offset: 0x000014D2
		// (set) Token: 0x06000744 RID: 1860 RVA: 0x000032DA File Offset: 0x000014DA
		[DataMember]
		public bool RegistrationCompleted { get; set; }

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000745 RID: 1861 RVA: 0x000032E3 File Offset: 0x000014E3
		// (set) Token: 0x06000746 RID: 1862 RVA: 0x000032EB File Offset: 0x000014EB
		[DataMember]
		public eVetsBenefitApplicationStep? PreferredStep { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000747 RID: 1863 RVA: 0x000032F4 File Offset: 0x000014F4
		// (set) Token: 0x06000748 RID: 1864 RVA: 0x000032FC File Offset: 0x000014FC
		[DataMember]
		public eVetsRequestStatus FinalStatus { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000749 RID: 1865 RVA: 0x00003305 File Offset: 0x00001505
		// (set) Token: 0x0600074A RID: 1866 RVA: 0x0000330D File Offset: 0x0000150D
		[DataMember]
		public Guid CurrentProgressStepId { get; set; }
	}
}
