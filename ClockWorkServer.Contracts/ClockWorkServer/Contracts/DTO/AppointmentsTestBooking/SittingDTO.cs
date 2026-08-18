using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009DA RID: 2522
	[DataContract(Namespace = "http://tpro.ca")]
	public class SittingDTO : SittingBaseDTO
	{
		// Token: 0x170012EB RID: 4843
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x00019943 File Offset: 0x00017B43
		// (set) Token: 0x06003488 RID: 13448 RVA: 0x0001994B File Offset: 0x00017B4B
		[DataMember]
		public PersonBaseDTO WhoCreated { get; set; }

		// Token: 0x170012EC RID: 4844
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x00019954 File Offset: 0x00017B54
		// (set) Token: 0x0600348A RID: 13450 RVA: 0x0001995C File Offset: 0x00017B5C
		[DataMember]
		public int InvigilatorConfirmed { get; set; }

		// Token: 0x170012ED RID: 4845
		// (get) Token: 0x0600348B RID: 13451 RVA: 0x00019965 File Offset: 0x00017B65
		// (set) Token: 0x0600348C RID: 13452 RVA: 0x0001996D File Offset: 0x00017B6D
		[DataMember]
		public double RateOfPay { get; set; }

		// Token: 0x170012EE RID: 4846
		// (get) Token: 0x0600348D RID: 13453 RVA: 0x00019976 File Offset: 0x00017B76
		// (set) Token: 0x0600348E RID: 13454 RVA: 0x0001997E File Offset: 0x00017B7E
		[DataMember]
		public string PrivateNotes { get; set; }

		// Token: 0x170012EF RID: 4847
		// (get) Token: 0x0600348F RID: 13455 RVA: 0x00019987 File Offset: 0x00017B87
		// (set) Token: 0x06003490 RID: 13456 RVA: 0x0001998F File Offset: 0x00017B8F
		[DataMember]
		public string InvigilatorNotes { get; set; }

		// Token: 0x170012F0 RID: 4848
		// (get) Token: 0x06003491 RID: 13457 RVA: 0x00019998 File Offset: 0x00017B98
		// (set) Token: 0x06003492 RID: 13458 RVA: 0x000199A0 File Offset: 0x00017BA0
		[DataMember]
		public DateTime? ActualTimeIn { get; set; }

		// Token: 0x170012F1 RID: 4849
		// (get) Token: 0x06003493 RID: 13459 RVA: 0x000199A9 File Offset: 0x00017BA9
		// (set) Token: 0x06003494 RID: 13460 RVA: 0x000199B1 File Offset: 0x00017BB1
		[DataMember]
		public DateTime? ActualTimeOut { get; set; }

		// Token: 0x170012F2 RID: 4850
		// (get) Token: 0x06003495 RID: 13461 RVA: 0x000199BA File Offset: 0x00017BBA
		// (set) Token: 0x06003496 RID: 13462 RVA: 0x000199C2 File Offset: 0x00017BC2
		[DataMember]
		public DateTime? PayDate { get; set; }

		// Token: 0x170012F3 RID: 4851
		// (get) Token: 0x06003497 RID: 13463 RVA: 0x000199CB File Offset: 0x00017BCB
		// (set) Token: 0x06003498 RID: 13464 RVA: 0x000199D3 File Offset: 0x00017BD3
		[DataMember]
		public DateTime DateCreated { get; set; }

		// Token: 0x170012F4 RID: 4852
		// (get) Token: 0x06003499 RID: 13465 RVA: 0x000199DC File Offset: 0x00017BDC
		// (set) Token: 0x0600349A RID: 13466 RVA: 0x000199E4 File Offset: 0x00017BE4
		[DataMember]
		public DateTime? VirtualMinStartDateTimeFromBookings { get; set; }

		// Token: 0x170012F5 RID: 4853
		// (get) Token: 0x0600349B RID: 13467 RVA: 0x000199ED File Offset: 0x00017BED
		// (set) Token: 0x0600349C RID: 13468 RVA: 0x000199F5 File Offset: 0x00017BF5
		[DataMember]
		public DateTime? VirtualMaxEndDateTimeFromBookings { get; set; }
	}
}
