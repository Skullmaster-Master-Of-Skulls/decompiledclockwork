using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x0200025F RID: 607
	public class MyStudentAppointment : BusinessBase<int>
	{
		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001245 RID: 4677 RVA: 0x0001896C File Offset: 0x00016B6C
		// (set) Token: 0x06001246 RID: 4678 RVA: 0x0000E258 File Offset: 0x0000C458
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

		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x00018984 File Offset: 0x00016B84
		// (set) Token: 0x06001248 RID: 4680 RVA: 0x0001898C File Offset: 0x00016B8C
		public DateTime StartDateTime { get; set; }

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001249 RID: 4681 RVA: 0x00018995 File Offset: 0x00016B95
		// (set) Token: 0x0600124A RID: 4682 RVA: 0x0001899D File Offset: 0x00016B9D
		public int DurationMinutes { get; set; }

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x0600124B RID: 4683 RVA: 0x000189A6 File Offset: 0x00016BA6
		// (set) Token: 0x0600124C RID: 4684 RVA: 0x000189AE File Offset: 0x00016BAE
		public int AppTypeId { get; set; }
	}
}
