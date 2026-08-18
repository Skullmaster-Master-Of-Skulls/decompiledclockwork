using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities
{
	// Token: 0x02000469 RID: 1129
	[DataContract(Namespace = "http://tpro.ca")]
	public class MailMergeContextDTO
	{
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x0600181A RID: 6170 RVA: 0x0000B293 File Offset: 0x00009493
		// (set) Token: 0x0600181B RID: 6171 RVA: 0x0000B29B File Offset: 0x0000949B
		[DataMember]
		public int PersonId { get; set; }

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600181C RID: 6172 RVA: 0x0000B2A4 File Offset: 0x000094A4
		// (set) Token: 0x0600181D RID: 6173 RVA: 0x0000B2AC File Offset: 0x000094AC
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x0000B2B5 File Offset: 0x000094B5
		// (set) Token: 0x0600181F RID: 6175 RVA: 0x0000B2BD File Offset: 0x000094BD
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0000B2C6 File Offset: 0x000094C6
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x0000B2CE File Offset: 0x000094CE
		[DataMember]
		public List<int> LuCourseIds { get; set; }

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x0000B2D7 File Offset: 0x000094D7
		// (set) Token: 0x06001823 RID: 6179 RVA: 0x0000B2DF File Offset: 0x000094DF
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0000B2E8 File Offset: 0x000094E8
		// (set) Token: 0x06001825 RID: 6181 RVA: 0x0000B2F0 File Offset: 0x000094F0
		[DataMember]
		public int CaseId { get; set; }

		// Token: 0x170007A4 RID: 1956
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0000B2F9 File Offset: 0x000094F9
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x0000B301 File Offset: 0x00009501
		[DataMember]
		public int PerDateId { get; set; }

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0000B30A File Offset: 0x0000950A
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x0000B312 File Offset: 0x00009512
		[DataMember]
		public int WhoAmId { get; set; }

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0000B31B File Offset: 0x0000951B
		// (set) Token: 0x0600182B RID: 6187 RVA: 0x0000B323 File Offset: 0x00009523
		[DataMember]
		public int? CourseId { get; set; }

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x0000B32C File Offset: 0x0000952C
		// (set) Token: 0x0600182D RID: 6189 RVA: 0x0000B334 File Offset: 0x00009534
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0000B33D File Offset: 0x0000953D
		// (set) Token: 0x0600182F RID: 6191 RVA: 0x0000B345 File Offset: 0x00009545
		[DataMember]
		public int ServiceProviderId { get; set; }

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0000B34E File Offset: 0x0000954E
		// (set) Token: 0x06001831 RID: 6193 RVA: 0x0000B356 File Offset: 0x00009556
		[DataMember]
		public string WebSettingContext { get; set; }

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0000B35F File Offset: 0x0000955F
		// (set) Token: 0x06001833 RID: 6195 RVA: 0x0000B367 File Offset: 0x00009567
		[DataMember]
		public string DefaultDateFormat { get; set; }

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001834 RID: 6196 RVA: 0x0000B370 File Offset: 0x00009570
		// (set) Token: 0x06001835 RID: 6197 RVA: 0x0000B378 File Offset: 0x00009578
		[DataMember]
		public string DefaultTimeFormat { get; set; }

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x0000B381 File Offset: 0x00009581
		// (set) Token: 0x06001837 RID: 6199 RVA: 0x0000B389 File Offset: 0x00009589
		[DataMember]
		public string ProductUniqueId { get; set; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x0000B392 File Offset: 0x00009592
		// (set) Token: 0x06001839 RID: 6201 RVA: 0x0000B39A File Offset: 0x0000959A
		[DataMember]
		public int CatalogId { get; set; }

		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x0600183A RID: 6202 RVA: 0x0000B3A3 File Offset: 0x000095A3
		// (set) Token: 0x0600183B RID: 6203 RVA: 0x0000B3AB File Offset: 0x000095AB
		[DataMember]
		public int LoanId { get; set; }

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x0600183C RID: 6204 RVA: 0x0000B3B4 File Offset: 0x000095B4
		// (set) Token: 0x0600183D RID: 6205 RVA: 0x0000B3BC File Offset: 0x000095BC
		[DataMember]
		public int AlternateFormatRequestId { get; set; }

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x0000B3C5 File Offset: 0x000095C5
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x0000B3CD File Offset: 0x000095CD
		[DataMember]
		public int AlternateFormatPublisherId { get; set; }

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x0000B3D6 File Offset: 0x000095D6
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x0000B3DE File Offset: 0x000095DE
		[DataMember]
		public int AlternateFormatVendorId { get; set; }

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x06001842 RID: 6210 RVA: 0x0000B3E7 File Offset: 0x000095E7
		// (set) Token: 0x06001843 RID: 6211 RVA: 0x0000B3EF File Offset: 0x000095EF
		[DataMember]
		public int AltPersonId { get; set; }

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x06001844 RID: 6212 RVA: 0x0000B3F8 File Offset: 0x000095F8
		// (set) Token: 0x06001845 RID: 6213 RVA: 0x0000B400 File Offset: 0x00009600
		[DataMember]
		public Guid AlternateFormatMediaContentId { get; set; }

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x0000B409 File Offset: 0x00009609
		// (set) Token: 0x06001847 RID: 6215 RVA: 0x0000B411 File Offset: 0x00009611
		[DataMember]
		public int PeopleOnlineFormId { get; set; }
	}
}
