using System;

namespace TechnoPro.Common.Public.Entities.OnlineForms
{
	// Token: 0x02000279 RID: 633
	public class OnlineFormStatus : BusinessBase<int>
	{
		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x000193F4 File Offset: 0x000175F4
		// (set) Token: 0x0600131F RID: 4895 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int PeopleOnlineFormStatusId
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

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0001940C File Offset: 0x0001760C
		// (set) Token: 0x06001321 RID: 4897 RVA: 0x00019414 File Offset: 0x00017614
		public string Title { get; set; }

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06001322 RID: 4898 RVA: 0x0001941D File Offset: 0x0001761D
		// (set) Token: 0x06001323 RID: 4899 RVA: 0x00019425 File Offset: 0x00017625
		public eOnlineFormStatusType StatusType { get; set; }
	}
}
