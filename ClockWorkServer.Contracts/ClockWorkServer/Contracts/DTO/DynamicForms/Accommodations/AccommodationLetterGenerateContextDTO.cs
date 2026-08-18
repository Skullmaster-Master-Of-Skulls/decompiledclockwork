using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DynamicForms.Accommodations;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.Accommodations
{
	// Token: 0x020006EE RID: 1774
	[DataContract(Namespace = "http://tpro.ca")]
	public class AccommodationLetterGenerateContextDTO
	{
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06002431 RID: 9265 RVA: 0x0001084D File Offset: 0x0000EA4D
		// (set) Token: 0x06002432 RID: 9266 RVA: 0x00010855 File Offset: 0x0000EA55
		[DataMember]
		public int StaffPersonId { get; set; }

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06002433 RID: 9267 RVA: 0x0001085E File Offset: 0x0000EA5E
		// (set) Token: 0x06002434 RID: 9268 RVA: 0x00010866 File Offset: 0x0000EA66
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06002435 RID: 9269 RVA: 0x0001086F File Offset: 0x0000EA6F
		// (set) Token: 0x06002436 RID: 9270 RVA: 0x00010877 File Offset: 0x0000EA77
		[DataMember]
		public int InstructorId { get; set; }

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06002437 RID: 9271 RVA: 0x00010880 File Offset: 0x0000EA80
		// (set) Token: 0x06002438 RID: 9272 RVA: 0x00010888 File Offset: 0x0000EA88
		[DataMember]
		public int AlternateContactId { get; set; }

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06002439 RID: 9273 RVA: 0x00010891 File Offset: 0x0000EA91
		// (set) Token: 0x0600243A RID: 9274 RVA: 0x00010899 File Offset: 0x0000EA99
		[DataMember]
		public IList<int> LuCourseIds { get; set; }

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x0600243B RID: 9275 RVA: 0x000108A2 File Offset: 0x0000EAA2
		// (set) Token: 0x0600243C RID: 9276 RVA: 0x000108AA File Offset: 0x0000EAAA
		[DataMember]
		public int PreferredTemplateId { get; set; }

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x000108B3 File Offset: 0x0000EAB3
		// (set) Token: 0x0600243E RID: 9278 RVA: 0x000108BB File Offset: 0x0000EABB
		[DataMember]
		public eAccommodationLetterGenerationType LetterType { get; set; }

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000108C4 File Offset: 0x0000EAC4
		// (set) Token: 0x06002440 RID: 9280 RVA: 0x000108CC File Offset: 0x0000EACC
		[DataMember]
		public eAccommodationLetterGenerationForWhom WhoGeneratingFor { get; set; }

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06002441 RID: 9281 RVA: 0x000108D5 File Offset: 0x0000EAD5
		// (set) Token: 0x06002442 RID: 9282 RVA: 0x000108DD File Offset: 0x0000EADD
		[DataMember]
		public eAccommodationLetterGenerationOutputType OutputType { get; set; }
	}
}
