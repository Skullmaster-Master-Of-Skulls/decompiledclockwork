using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009B6 RID: 2486
	[DataContract(Namespace = "http://tpro.ca")]
	public class TestForEditDTO
	{
		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x0600335D RID: 13149 RVA: 0x00018FAC File Offset: 0x000171AC
		// (set) Token: 0x0600335E RID: 13150 RVA: 0x00018FB4 File Offset: 0x000171B4
		[DataMember]
		public TestDTO Test { get; set; }

		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x0600335F RID: 13151 RVA: 0x00018FBD File Offset: 0x000171BD
		// (set) Token: 0x06003360 RID: 13152 RVA: 0x00018FC5 File Offset: 0x000171C5
		[DataMember]
		public DateTime? StudentReportedClassStartDateTime { get; set; }

		// Token: 0x1700126C RID: 4716
		// (get) Token: 0x06003361 RID: 13153 RVA: 0x00018FCE File Offset: 0x000171CE
		// (set) Token: 0x06003362 RID: 13154 RVA: 0x00018FD6 File Offset: 0x000171D6
		[DataMember]
		public DateTime? StudentReportedClassEndDateTime { get; set; }

		// Token: 0x1700126D RID: 4717
		// (get) Token: 0x06003363 RID: 13155 RVA: 0x00018FDF File Offset: 0x000171DF
		// (set) Token: 0x06003364 RID: 13156 RVA: 0x00018FE7 File Offset: 0x000171E7
		[DataMember]
		public bool? InstructorSubmittedTestInfo { get; set; }

		// Token: 0x1700126E RID: 4718
		// (get) Token: 0x06003365 RID: 13157 RVA: 0x00018FF0 File Offset: 0x000171F0
		// (set) Token: 0x06003366 RID: 13158 RVA: 0x00018FF8 File Offset: 0x000171F8
		[DataMember]
		public string TestNote { get; set; }

		// Token: 0x1700126F RID: 4719
		// (get) Token: 0x06003367 RID: 13159 RVA: 0x00019001 File Offset: 0x00017201
		// (set) Token: 0x06003368 RID: 13160 RVA: 0x00019009 File Offset: 0x00017209
		[DataMember]
		public string BookingNote { get; set; }

		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06003369 RID: 13161 RVA: 0x00019012 File Offset: 0x00017212
		// (set) Token: 0x0600336A RID: 13162 RVA: 0x0001901A File Offset: 0x0001721A
		[DataMember]
		public string PrivateNote { get; set; }

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x0600336B RID: 13163 RVA: 0x00019023 File Offset: 0x00017223
		// (set) Token: 0x0600336C RID: 13164 RVA: 0x0001902B File Offset: 0x0001722B
		[DataMember]
		public string TestDeliveryMethod { get; set; }
	}
}
