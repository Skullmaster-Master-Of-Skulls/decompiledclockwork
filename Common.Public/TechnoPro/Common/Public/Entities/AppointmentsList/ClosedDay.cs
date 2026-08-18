using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.AppointmentsList
{
	// Token: 0x02000557 RID: 1367
	public class ClosedDay : BusinessBase<int>
	{
		// Token: 0x17001270 RID: 4720
		// (get) Token: 0x06002BFE RID: 11262 RVA: 0x000310D8 File Offset: 0x0002F2D8
		// (set) Token: 0x06002BFF RID: 11263 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int Availability2ItemsClosedDaysId
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

		// Token: 0x17001271 RID: 4721
		// (get) Token: 0x06002C00 RID: 11264 RVA: 0x000310F0 File Offset: 0x0002F2F0
		// (set) Token: 0x06002C01 RID: 11265 RVA: 0x000310F8 File Offset: 0x0002F2F8
		public DateTime DateClosed { get; set; }

		// Token: 0x17001272 RID: 4722
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x00031101 File Offset: 0x0002F301
		// (set) Token: 0x06002C03 RID: 11267 RVA: 0x00031109 File Offset: 0x0002F309
		public string Note { get; set; }

		// Token: 0x17001273 RID: 4723
		// (get) Token: 0x06002C04 RID: 11268 RVA: 0x00031112 File Offset: 0x0002F312
		// (set) Token: 0x06002C05 RID: 11269 RVA: 0x0003111A File Offset: 0x0002F31A
		public PersonBase Staff { get; set; }
	}
}
