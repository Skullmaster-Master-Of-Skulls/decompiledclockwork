using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Accommodations;
using TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests.SelfRegProcessing;
using TechnoPro.Common.Public.Entities.StudentAccommodationRequests.SelfRegProcessing;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentAccommodationRequests
{
	// Token: 0x02000241 RID: 577
	[DataContract(Namespace = "http://tpro.ca")]
	public class ProcessSelfRegRequestReq : BaseMessageReq
	{
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00006027 File Offset: 0x00004227
		// (set) Token: 0x06000D0C RID: 3340 RVA: 0x0000602F File Offset: 0x0000422F
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00006038 File Offset: 0x00004238
		// (set) Token: 0x06000D0E RID: 3342 RVA: 0x00006040 File Offset: 0x00004240
		[DataMember]
		public eSelfRegCoursesAccommodationsStatus StudentIndicatedCoursesAccommodationsStatus { get; set; }

		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00006049 File Offset: 0x00004249
		// (set) Token: 0x06000D10 RID: 3344 RVA: 0x00006051 File Offset: 0x00004251
		[DataMember]
		public IList<SelfRegCourseInfoDTO> LuCourseIdsToApplyTo { get; set; }

		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0000605A File Offset: 0x0000425A
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x00006062 File Offset: 0x00004262
		[DataMember]
		public IList<SelfRegCheckedAccommodationDTO> CheckedAccommodations { get; set; }

		// Token: 0x17000336 RID: 822
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0000606B File Offset: 0x0000426B
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x00006073 File Offset: 0x00004273
		[DataMember]
		public IList<AccommodationDataDTO> HidingAccommodations { get; set; }

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0000607C File Offset: 0x0000427C
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x00006084 File Offset: 0x00004284
		[DataMember]
		public string NoteFromStudent { get; set; }

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0000608D File Offset: 0x0000428D
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x00006095 File Offset: 0x00004295
		[DataMember]
		public string BaseUrl { get; set; }

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0000609E File Offset: 0x0000429E
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x000060A6 File Offset: 0x000042A6
		[DataMember]
		public string StudentPersonIdEncodedForUrl { get; set; }

		// Token: 0x1700033A RID: 826
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x000060AF File Offset: 0x000042AF
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x000060B7 File Offset: 0x000042B7
		[DataMember]
		public string IpAddressForLogging { get; set; }
	}
}
