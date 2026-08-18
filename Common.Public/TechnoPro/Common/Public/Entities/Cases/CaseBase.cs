using System;

namespace TechnoPro.Common.Public.Entities.Cases
{
	// Token: 0x02000465 RID: 1125
	public class CaseBase : BusinessBase<int>
	{
		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x0600223F RID: 8767 RVA: 0x000263DC File Offset: 0x000245DC
		// (set) Token: 0x06002240 RID: 8768 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int InfoPcId
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

		// Token: 0x17000E26 RID: 3622
		// (get) Token: 0x06002241 RID: 8769 RVA: 0x000263F4 File Offset: 0x000245F4
		// (set) Token: 0x06002242 RID: 8770 RVA: 0x000263FC File Offset: 0x000245FC
		public string CaseNumber { get; set; }

		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x06002243 RID: 8771 RVA: 0x00026405 File Offset: 0x00024605
		// (set) Token: 0x06002244 RID: 8772 RVA: 0x0002640D File Offset: 0x0002460D
		public string Title { get; set; }
	}
}
