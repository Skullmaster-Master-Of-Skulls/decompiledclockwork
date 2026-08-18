using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking.TestBookingViews.ViewEntities
{
	// Token: 0x02000A45 RID: 2629
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClassTestDefinitionSmallDTO
	{
		// Token: 0x17001394 RID: 5012
		// (get) Token: 0x06003644 RID: 13892 RVA: 0x0001A4A7 File Offset: 0x000186A7
		// (set) Token: 0x06003645 RID: 13893 RVA: 0x0001A4AF File Offset: 0x000186AF
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x17001395 RID: 5013
		// (get) Token: 0x06003646 RID: 13894 RVA: 0x0001A4B8 File Offset: 0x000186B8
		// (set) Token: 0x06003647 RID: 13895 RVA: 0x0001A4C0 File Offset: 0x000186C0
		[DataMember]
		public int LuCourseId { get; set; }

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x06003648 RID: 13896 RVA: 0x0001A4C9 File Offset: 0x000186C9
		// (set) Token: 0x06003649 RID: 13897 RVA: 0x0001A4D1 File Offset: 0x000186D1
		[DataMember]
		public int TestDuration { get; set; }

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x0600364A RID: 13898 RVA: 0x0001A4DA File Offset: 0x000186DA
		// (set) Token: 0x0600364B RID: 13899 RVA: 0x0001A4E2 File Offset: 0x000186E2
		[DataMember]
		public string CourseDescription { get; set; }

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x0600364C RID: 13900 RVA: 0x0001A4EB File Offset: 0x000186EB
		// (set) Token: 0x0600364D RID: 13901 RVA: 0x0001A4F3 File Offset: 0x000186F3
		[DataMember]
		public DateTime DateOfTest { get; set; }

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x0600364E RID: 13902 RVA: 0x0001A4FC File Offset: 0x000186FC
		// (set) Token: 0x0600364F RID: 13903 RVA: 0x0001A504 File Offset: 0x00018704
		[DataMember]
		public DateTime TestStartTime { get; set; }

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06003650 RID: 13904 RVA: 0x0001A50D File Offset: 0x0001870D
		// (set) Token: 0x06003651 RID: 13905 RVA: 0x0001A515 File Offset: 0x00018715
		[DataMember]
		public DateTime TestEndTime { get; set; }

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06003652 RID: 13906 RVA: 0x0001A51E File Offset: 0x0001871E
		// (set) Token: 0x06003653 RID: 13907 RVA: 0x0001A526 File Offset: 0x00018726
		[DataMember]
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06003654 RID: 13908 RVA: 0x0001A52F File Offset: 0x0001872F
		// (set) Token: 0x06003655 RID: 13909 RVA: 0x0001A537 File Offset: 0x00018737
		[DataMember]
		public string InstructorContactedNote { get; set; }

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06003656 RID: 13910 RVA: 0x0001A540 File Offset: 0x00018740
		// (set) Token: 0x06003657 RID: 13911 RVA: 0x0001A548 File Offset: 0x00018748
		[DataMember]
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06003658 RID: 13912 RVA: 0x0001A551 File Offset: 0x00018751
		// (set) Token: 0x06003659 RID: 13913 RVA: 0x0001A559 File Offset: 0x00018759
		[DataMember]
		public string TestPickedUpNote { get; set; }

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x0600365A RID: 13914 RVA: 0x0001A562 File Offset: 0x00018762
		// (set) Token: 0x0600365B RID: 13915 RVA: 0x0001A56A File Offset: 0x0001876A
		[DataMember]
		public eClassTestType TestType { get; set; }

		// Token: 0x170013A0 RID: 5024
		public string this[int index]
		{
			get
			{
				return this._customs[index];
			}
			set
			{
				this._customs[index] = value;
			}
		}

		// Token: 0x04001479 RID: 5241
		private const int MAX_CUSTOM = 20;

		// Token: 0x04001486 RID: 5254
		[DataMember]
		private readonly string[] _customs = new string[20];
	}
}
