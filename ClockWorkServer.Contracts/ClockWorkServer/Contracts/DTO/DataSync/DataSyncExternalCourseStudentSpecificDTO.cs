using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DataSync
{
	// Token: 0x02000706 RID: 1798
	[DataContract(Namespace = "http://tpro.ca")]
	public class DataSyncExternalCourseStudentSpecificDTO
	{
		// Token: 0x17000C9F RID: 3231
		// (get) Token: 0x060024C0 RID: 9408 RVA: 0x00010C6B File Offset: 0x0000EE6B
		// (set) Token: 0x060024C1 RID: 9409 RVA: 0x00010C73 File Offset: 0x0000EE73
		[DataMember]
		public string GradeLetter { get; set; }

		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060024C2 RID: 9410 RVA: 0x00010C7C File Offset: 0x0000EE7C
		// (set) Token: 0x060024C3 RID: 9411 RVA: 0x00010C84 File Offset: 0x0000EE84
		[DataMember]
		public string InProgressGradeLetter { get; set; }

		// Token: 0x17000CA1 RID: 3233
		// (get) Token: 0x060024C4 RID: 9412 RVA: 0x00010C8D File Offset: 0x0000EE8D
		// (set) Token: 0x060024C5 RID: 9413 RVA: 0x00010C95 File Offset: 0x0000EE95
		[DataMember]
		public double Grade { get; set; }

		// Token: 0x17000CA2 RID: 3234
		// (get) Token: 0x060024C6 RID: 9414 RVA: 0x00010C9E File Offset: 0x0000EE9E
		// (set) Token: 0x060024C7 RID: 9415 RVA: 0x00010CA6 File Offset: 0x0000EEA6
		[DataMember]
		public double InProgressGrade { get; set; }

		// Token: 0x17000CA3 RID: 3235
		// (get) Token: 0x060024C8 RID: 9416 RVA: 0x00010CAF File Offset: 0x0000EEAF
		// (set) Token: 0x060024C9 RID: 9417 RVA: 0x00010CB7 File Offset: 0x0000EEB7
		[DataMember]
		public double TuitionCost { get; set; }

		// Token: 0x17000CA4 RID: 3236
		// (get) Token: 0x060024CA RID: 9418 RVA: 0x00010CC0 File Offset: 0x0000EEC0
		// (set) Token: 0x060024CB RID: 9419 RVA: 0x00010CC8 File Offset: 0x0000EEC8
		[DataMember]
		public DateTime? RegistrationDate { get; set; }

		// Token: 0x17000CA5 RID: 3237
		// (get) Token: 0x060024CC RID: 9420 RVA: 0x00010CD1 File Offset: 0x0000EED1
		// (set) Token: 0x060024CD RID: 9421 RVA: 0x00010CD9 File Offset: 0x0000EED9
		[DataMember]
		public string RegistrationNote { get; set; }
	}
}
