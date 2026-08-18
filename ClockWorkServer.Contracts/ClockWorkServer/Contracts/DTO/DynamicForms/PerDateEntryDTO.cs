using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006B6 RID: 1718
	[DataContract(Namespace = "http://tpro.ca")]
	public class PerDateEntryDTO
	{
		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		// (set) Token: 0x060022E2 RID: 8930 RVA: 0x0000FF00 File Offset: 0x0000E100
		[DataMember]
		public int AppointmentId { get; set; }

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x060022E3 RID: 8931 RVA: 0x0000FF09 File Offset: 0x0000E109
		// (set) Token: 0x060022E4 RID: 8932 RVA: 0x0000FF11 File Offset: 0x0000E111
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x060022E5 RID: 8933 RVA: 0x0000FF1A File Offset: 0x0000E11A
		// (set) Token: 0x060022E6 RID: 8934 RVA: 0x0000FF22 File Offset: 0x0000E122
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x060022E7 RID: 8935 RVA: 0x0000FF2B File Offset: 0x0000E12B
		// (set) Token: 0x060022E8 RID: 8936 RVA: 0x0000FF33 File Offset: 0x0000E133
		[DataMember]
		public PersonBaseDTO Student { get; set; }

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x0000FF3C File Offset: 0x0000E13C
		// (set) Token: 0x060022EA RID: 8938 RVA: 0x0000FF44 File Offset: 0x0000E144
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x060022EB RID: 8939 RVA: 0x0000FF4D File Offset: 0x0000E14D
		// (set) Token: 0x060022EC RID: 8940 RVA: 0x0000FF55 File Offset: 0x0000E155
		[DataMember]
		public int ScreenNum { get; set; }
	}
}
