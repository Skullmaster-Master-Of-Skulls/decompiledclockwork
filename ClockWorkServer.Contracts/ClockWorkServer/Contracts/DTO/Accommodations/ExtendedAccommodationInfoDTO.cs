using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations
{
	// Token: 0x02000C99 RID: 3225
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExtendedAccommodationInfoDTO
	{
		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x0600433D RID: 17213 RVA: 0x000245AA File Offset: 0x000227AA
		// (set) Token: 0x0600433E RID: 17214 RVA: 0x000245B2 File Offset: 0x000227B2
		[DataMember]
		public bool ShowOnLetter { get; set; }

		// Token: 0x170018A9 RID: 6313
		// (get) Token: 0x0600433F RID: 17215 RVA: 0x000245BB File Offset: 0x000227BB
		// (set) Token: 0x06004340 RID: 17216 RVA: 0x000245C3 File Offset: 0x000227C3
		[DataMember]
		public bool Approved { get; set; }

		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x000245CC File Offset: 0x000227CC
		// (set) Token: 0x06004342 RID: 17218 RVA: 0x000245D4 File Offset: 0x000227D4
		[DataMember]
		public bool Offline { get; set; }

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06004343 RID: 17219 RVA: 0x000245DD File Offset: 0x000227DD
		// (set) Token: 0x06004344 RID: 17220 RVA: 0x000245E5 File Offset: 0x000227E5
		[DataMember]
		public DateTime? ExpiryDate { get; set; }

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x06004345 RID: 17221 RVA: 0x000245EE File Offset: 0x000227EE
		// (set) Token: 0x06004346 RID: 17222 RVA: 0x000245F6 File Offset: 0x000227F6
		[DataMember]
		public string Note { get; set; }

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06004347 RID: 17223 RVA: 0x000245FF File Offset: 0x000227FF
		// (set) Token: 0x06004348 RID: 17224 RVA: 0x00024607 File Offset: 0x00022807
		[DataMember]
		public bool RecommendedButDeclined { get; set; }

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x00024610 File Offset: 0x00022810
		// (set) Token: 0x0600434A RID: 17226 RVA: 0x00024618 File Offset: 0x00022818
		[DataMember]
		public string Rationale { get; set; }

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x00024621 File Offset: 0x00022821
		// (set) Token: 0x0600434C RID: 17228 RVA: 0x00024629 File Offset: 0x00022829
		[DataMember]
		public DateTime? SessionDateEntered { get; set; }

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x0600434D RID: 17229 RVA: 0x00024632 File Offset: 0x00022832
		// (set) Token: 0x0600434E RID: 17230 RVA: 0x0002463A File Offset: 0x0002283A
		[DataMember]
		public string RecommendedButDeclinedDetail { get; set; }

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x0600434F RID: 17231 RVA: 0x00024643 File Offset: 0x00022843
		// (set) Token: 0x06004350 RID: 17232 RVA: 0x0002464B File Offset: 0x0002284B
		[DataMember]
		public string LongDescription { get; set; }

		// Token: 0x170018B2 RID: 6322
		// (get) Token: 0x06004351 RID: 17233 RVA: 0x00024654 File Offset: 0x00022854
		// (set) Token: 0x06004352 RID: 17234 RVA: 0x0002465C File Offset: 0x0002285C
		[DataMember]
		public string ShortCode { get; set; }

		// Token: 0x170018B3 RID: 6323
		// (get) Token: 0x06004353 RID: 17235 RVA: 0x00024665 File Offset: 0x00022865
		// (set) Token: 0x06004354 RID: 17236 RVA: 0x0002466D File Offset: 0x0002286D
		[DataMember]
		public eAccommodationGroupDTO Group { get; set; }

		// Token: 0x170018B4 RID: 6324
		// (get) Token: 0x06004355 RID: 17237 RVA: 0x00024676 File Offset: 0x00022876
		// (set) Token: 0x06004356 RID: 17238 RVA: 0x0002467E File Offset: 0x0002287E
		[DataMember]
		public eAccommodationTypeDTO AccommodationType { get; set; }
	}
}
