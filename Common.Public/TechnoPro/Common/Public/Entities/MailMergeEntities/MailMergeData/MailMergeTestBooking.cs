using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeData
{
	// Token: 0x020002DA RID: 730
	public class MailMergeTestBooking : BusinessBase<int>
	{
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x060015FB RID: 5627 RVA: 0x0001B63C File Offset: 0x0001983C
		// (set) Token: 0x060015FC RID: 5628 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int AppointmentId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x060015FD RID: 5629 RVA: 0x0001B654 File Offset: 0x00019854
		// (set) Token: 0x060015FE RID: 5630 RVA: 0x0001B65C File Offset: 0x0001985C
		public int ExamId { get; set; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x060015FF RID: 5631 RVA: 0x0001B665 File Offset: 0x00019865
		// (set) Token: 0x06001600 RID: 5632 RVA: 0x0001B66D File Offset: 0x0001986D
		public int LuCourseId { get; set; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06001601 RID: 5633 RVA: 0x0001B676 File Offset: 0x00019876
		// (set) Token: 0x06001602 RID: 5634 RVA: 0x0001B67E File Offset: 0x0001987E
		public int PersonId { get; set; }
	}
}
