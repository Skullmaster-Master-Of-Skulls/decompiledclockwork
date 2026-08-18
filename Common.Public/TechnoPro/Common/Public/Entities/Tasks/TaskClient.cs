using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Tasks
{
	// Token: 0x02000176 RID: 374
	public class TaskClient : BusinessBase<int>
	{
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000129D4 File Offset: 0x00010BD4
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int TaskClientId
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

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000129EC File Offset: 0x00010BEC
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x000129F4 File Offset: 0x00010BF4
		public PersonBase Client { get; set; }

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x000129FD File Offset: 0x00010BFD
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00012A05 File Offset: 0x00010C05
		public string Notes { get; set; }
	}
}
