using System;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x02000517 RID: 1303
	[Serializable]
	public class ClassTest : ClassTestBase
	{
		// Token: 0x060027E1 RID: 10209 RVA: 0x00029C30 File Offset: 0x00027E30
		public ClassTest()
		{
			this.InstructorAcknowledged = new char?(' ');
			this.TestDeliveredMessage = "";
			this.InstructorContactedNote = "";
		}

		// Token: 0x170010A4 RID: 4260
		// (get) Token: 0x060027E2 RID: 10210 RVA: 0x00029C60 File Offset: 0x00027E60
		// (set) Token: 0x060027E3 RID: 10211 RVA: 0x00029C68 File Offset: 0x00027E68
		public DateTime? TestPickedUpDate { get; set; }

		// Token: 0x170010A5 RID: 4261
		// (get) Token: 0x060027E4 RID: 10212 RVA: 0x00029C71 File Offset: 0x00027E71
		// (set) Token: 0x060027E5 RID: 10213 RVA: 0x00029C79 File Offset: 0x00027E79
		public string TestDeliveredMessage { get; set; }

		// Token: 0x060027E6 RID: 10214 RVA: 0x00029C84 File Offset: 0x00027E84
		public bool GetIsTestDelivered()
		{
			return !string.IsNullOrEmpty(this.TestDeliveredMessage);
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x060027E7 RID: 10215 RVA: 0x00029CA4 File Offset: 0x00027EA4
		// (set) Token: 0x060027E8 RID: 10216 RVA: 0x00029CAC File Offset: 0x00027EAC
		public string TestPickedUpNote { get; set; }

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x060027E9 RID: 10217 RVA: 0x00029CB5 File Offset: 0x00027EB5
		// (set) Token: 0x060027EA RID: 10218 RVA: 0x00029CBD File Offset: 0x00027EBD
		public string PrivateNote { get; set; }

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x060027EB RID: 10219 RVA: 0x00029CC6 File Offset: 0x00027EC6
		// (set) Token: 0x060027EC RID: 10220 RVA: 0x00029CCE File Offset: 0x00027ECE
		public DateTime? InstructorContactedDate { get; set; }

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x060027ED RID: 10221 RVA: 0x00029CD7 File Offset: 0x00027ED7
		// (set) Token: 0x060027EE RID: 10222 RVA: 0x00029CDF File Offset: 0x00027EDF
		public string InstructorContactedNote { get; set; }

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x060027EF RID: 10223 RVA: 0x00029CE8 File Offset: 0x00027EE8
		// (set) Token: 0x060027F0 RID: 10224 RVA: 0x00029CF0 File Offset: 0x00027EF0
		public char? InstructorAcknowledged { get; set; }

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x060027F1 RID: 10225 RVA: 0x00029CF9 File Offset: 0x00027EF9
		// (set) Token: 0x060027F2 RID: 10226 RVA: 0x00029D01 File Offset: 0x00027F01
		public string Description { get; set; }
	}
}
