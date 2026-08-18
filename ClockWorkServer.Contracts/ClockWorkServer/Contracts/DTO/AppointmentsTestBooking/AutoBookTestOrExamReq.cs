using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking;
using TechnoPro.Common.Public.Entities.AppointmentsTestBooking.AutoTestBookingHelper;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D1 RID: 2513
	[DataContract(Namespace = "http://tpro.ca")]
	public class AutoBookTestOrExamReq : BaseMessageReq
	{
		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06003428 RID: 13352 RVA: 0x000195A6 File Offset: 0x000177A6
		// (set) Token: 0x06003429 RID: 13353 RVA: 0x000195AE File Offset: 0x000177AE
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x0600342A RID: 13354 RVA: 0x000195B7 File Offset: 0x000177B7
		// (set) Token: 0x0600342B RID: 13355 RVA: 0x000195BF File Offset: 0x000177BF
		[DataMember]
		public eTestExamSettingType TestType { get; set; }

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x0600342C RID: 13356 RVA: 0x000195C8 File Offset: 0x000177C8
		// (set) Token: 0x0600342D RID: 13357 RVA: 0x000195D0 File Offset: 0x000177D0
		[DataMember]
		public eAutoTestBookingContext TestBookingContext { get; set; }

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x0600342E RID: 13358 RVA: 0x000195D9 File Offset: 0x000177D9
		// (set) Token: 0x0600342F RID: 13359 RVA: 0x000195E1 File Offset: 0x000177E1
		[DataMember]
		public int Pid { get; set; }

		// Token: 0x170012C6 RID: 4806
		// (get) Token: 0x06003430 RID: 13360 RVA: 0x000195EA File Offset: 0x000177EA
		// (set) Token: 0x06003431 RID: 13361 RVA: 0x000195F2 File Offset: 0x000177F2
		[DataMember]
		public int Lucid { get; set; }

		// Token: 0x170012C7 RID: 4807
		// (get) Token: 0x06003432 RID: 13362 RVA: 0x000195FB File Offset: 0x000177FB
		// (set) Token: 0x06003433 RID: 13363 RVA: 0x00019603 File Offset: 0x00017803
		[DataMember]
		public DateTime ClassStartDateTime { get; set; }

		// Token: 0x170012C8 RID: 4808
		// (get) Token: 0x06003434 RID: 13364 RVA: 0x0001960C File Offset: 0x0001780C
		// (set) Token: 0x06003435 RID: 13365 RVA: 0x00019614 File Offset: 0x00017814
		[DataMember]
		public DateTime ClassEndDateTime { get; set; }

		// Token: 0x170012C9 RID: 4809
		// (get) Token: 0x06003436 RID: 13366 RVA: 0x0001961D File Offset: 0x0001781D
		// (set) Token: 0x06003437 RID: 13367 RVA: 0x00019625 File Offset: 0x00017825
		[DataMember]
		public bool ClearCacheFirst { get; set; }
	}
}
