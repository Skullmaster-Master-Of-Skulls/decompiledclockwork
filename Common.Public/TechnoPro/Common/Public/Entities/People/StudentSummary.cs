using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x02000265 RID: 613
	public class StudentSummary : BusinessBase<int>
	{
		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00018AFC File Offset: 0x00016CFC
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PersonId
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

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x06001278 RID: 4728 RVA: 0x00018B14 File Offset: 0x00016D14
		// (set) Token: 0x06001279 RID: 4729 RVA: 0x00018B1C File Offset: 0x00016D1C
		public IList<BaseExtendedAppointment> Appointments { get; set; }

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600127A RID: 4730 RVA: 0x00018B25 File Offset: 0x00016D25
		// (set) Token: 0x0600127B RID: 4731 RVA: 0x00018B2D File Offset: 0x00016D2D
		public StudentCommonInfo StudentCommonInfo { get; set; }
	}
}
