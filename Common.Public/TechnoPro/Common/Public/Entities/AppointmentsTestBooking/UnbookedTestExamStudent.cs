using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsTestBooking
{
	// Token: 0x0200050C RID: 1292
	public class UnbookedTestExamStudent : BusinessBase<PersonBase, ClassTestBase>
	{
		// Token: 0x1700106D RID: 4205
		// (get) Token: 0x06002767 RID: 10087 RVA: 0x000297BC File Offset: 0x000279BC
		// (set) Token: 0x06002768 RID: 10088 RVA: 0x000297D4 File Offset: 0x000279D4
		public PersonBase Student
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

		// Token: 0x1700106E RID: 4206
		// (get) Token: 0x06002769 RID: 10089 RVA: 0x000297E0 File Offset: 0x000279E0
		// (set) Token: 0x0600276A RID: 10090 RVA: 0x000297F8 File Offset: 0x000279F8
		public ClassTestBase ClassTest
		{
			get
			{
				return this.SecondId;
			}
			set
			{
				this.SecondId = value;
			}
		}

		// Token: 0x1700106F RID: 4207
		// (get) Token: 0x0600276B RID: 10091 RVA: 0x00029803 File Offset: 0x00027A03
		// (set) Token: 0x0600276C RID: 10092 RVA: 0x0002980B File Offset: 0x00027A0B
		public string StudentEmail { get; set; }

		// Token: 0x17001070 RID: 4208
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x00029814 File Offset: 0x00027A14
		// (set) Token: 0x0600276E RID: 10094 RVA: 0x0002981C File Offset: 0x00027A1C
		public DateTime? DateLetterIssued { get; set; }
	}
}
