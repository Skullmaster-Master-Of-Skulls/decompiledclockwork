using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009AF RID: 2479
	[DataContract(Namespace = "http://tpro.ca")]
	public class ExamFileDTO
	{
		// Token: 0x06003264 RID: 12900 RVA: 0x00018788 File Offset: 0x00016988
		public ExamFileDTO()
		{
			this.IsVisible = true;
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06003265 RID: 12901 RVA: 0x0001879A File Offset: 0x0001699A
		// (set) Token: 0x06003266 RID: 12902 RVA: 0x000187A2 File Offset: 0x000169A2
		[DataMember]
		public int ExamFileId { get; set; }

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06003267 RID: 12903 RVA: 0x000187AB File Offset: 0x000169AB
		// (set) Token: 0x06003268 RID: 12904 RVA: 0x000187B3 File Offset: 0x000169B3
		[DataMember]
		public int ExamId { get; set; }

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06003269 RID: 12905 RVA: 0x000187BC File Offset: 0x000169BC
		// (set) Token: 0x0600326A RID: 12906 RVA: 0x000187C4 File Offset: 0x000169C4
		[DataMember]
		public BinaryFileDTO File { get; set; }

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x000187CD File Offset: 0x000169CD
		// (set) Token: 0x0600326C RID: 12908 RVA: 0x000187D5 File Offset: 0x000169D5
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x0600326D RID: 12909 RVA: 0x000187DE File Offset: 0x000169DE
		// (set) Token: 0x0600326E RID: 12910 RVA: 0x000187E6 File Offset: 0x000169E6
		[DataMember]
		public int WhoEntered { get; set; }

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x0600326F RID: 12911 RVA: 0x000187EF File Offset: 0x000169EF
		// (set) Token: 0x06003270 RID: 12912 RVA: 0x000187F7 File Offset: 0x000169F7
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x00018800 File Offset: 0x00016A00
		// (set) Token: 0x06003272 RID: 12914 RVA: 0x00018808 File Offset: 0x00016A08
		[DataMember]
		public bool IsVisible { get; set; }
	}
}
