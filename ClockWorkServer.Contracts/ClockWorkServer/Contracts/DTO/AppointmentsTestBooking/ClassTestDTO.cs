using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009D9 RID: 2521
	[DataContract(Namespace = "http://tpro.ca")]
	public class ClassTestDTO : ClassTestBaseDTO
	{
		// Token: 0x170012E3 RID: 4835
		// (get) Token: 0x06003475 RID: 13429 RVA: 0x00019890 File Offset: 0x00017A90
		// (set) Token: 0x06003476 RID: 13430 RVA: 0x00019898 File Offset: 0x00017A98
		[DataMember]
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x170012E4 RID: 4836
		// (get) Token: 0x06003477 RID: 13431 RVA: 0x000198A1 File Offset: 0x00017AA1
		// (set) Token: 0x06003478 RID: 13432 RVA: 0x000198A9 File Offset: 0x00017AA9
		[DataMember]
		public string TestDeliveredMessage { get; set; }

		// Token: 0x06003479 RID: 13433 RVA: 0x000198B4 File Offset: 0x00017AB4
		public bool GetIsTestDelivered()
		{
			return !string.IsNullOrEmpty(this.TestDeliveredMessage);
		}

		// Token: 0x170012E5 RID: 4837
		// (get) Token: 0x0600347A RID: 13434 RVA: 0x000198D4 File Offset: 0x00017AD4
		// (set) Token: 0x0600347B RID: 13435 RVA: 0x000198DC File Offset: 0x00017ADC
		[DataMember]
		public string TestPickedUpNote { get; set; }

		// Token: 0x170012E6 RID: 4838
		// (get) Token: 0x0600347C RID: 13436 RVA: 0x000198E5 File Offset: 0x00017AE5
		// (set) Token: 0x0600347D RID: 13437 RVA: 0x000198ED File Offset: 0x00017AED
		[DataMember]
		public string PrivateNote { get; set; }

		// Token: 0x170012E7 RID: 4839
		// (get) Token: 0x0600347E RID: 13438 RVA: 0x000198F6 File Offset: 0x00017AF6
		// (set) Token: 0x0600347F RID: 13439 RVA: 0x000198FE File Offset: 0x00017AFE
		[DataMember]
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x170012E8 RID: 4840
		// (get) Token: 0x06003480 RID: 13440 RVA: 0x00019907 File Offset: 0x00017B07
		// (set) Token: 0x06003481 RID: 13441 RVA: 0x0001990F File Offset: 0x00017B0F
		[DataMember]
		public string InstructorContactedNote { get; set; }

		// Token: 0x170012E9 RID: 4841
		// (get) Token: 0x06003482 RID: 13442 RVA: 0x00019918 File Offset: 0x00017B18
		// (set) Token: 0x06003483 RID: 13443 RVA: 0x00019920 File Offset: 0x00017B20
		[DataMember]
		public char? InstructorAcknowledged { get; set; }

		// Token: 0x170012EA RID: 4842
		// (get) Token: 0x06003484 RID: 13444 RVA: 0x00019929 File Offset: 0x00017B29
		// (set) Token: 0x06003485 RID: 13445 RVA: 0x00019931 File Offset: 0x00017B31
		[DataMember]
		public string Description { get; set; }
	}
}
