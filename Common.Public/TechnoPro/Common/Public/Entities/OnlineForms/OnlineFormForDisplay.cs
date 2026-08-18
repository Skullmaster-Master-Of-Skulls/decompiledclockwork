using System;

namespace TechnoPro.Common.Public.Entities.OnlineForms
{
	// Token: 0x02000276 RID: 630
	public class OnlineFormForDisplay : BusinessBase<int>
	{
		// Token: 0x170007D5 RID: 2005
		// (get) Token: 0x060012FD RID: 4861 RVA: 0x000192E4 File Offset: 0x000174E4
		// (set) Token: 0x060012FE RID: 4862 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int OnlineFormId
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

		// Token: 0x170007D6 RID: 2006
		// (get) Token: 0x060012FF RID: 4863 RVA: 0x000192FC File Offset: 0x000174FC
		// (set) Token: 0x06001300 RID: 4864 RVA: 0x00019304 File Offset: 0x00017504
		public string Title { get; set; }

		// Token: 0x170007D7 RID: 2007
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x0001930D File Offset: 0x0001750D
		// (set) Token: 0x06001302 RID: 4866 RVA: 0x00019315 File Offset: 0x00017515
		public string Description { get; set; }

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x06001303 RID: 4867 RVA: 0x0001931E File Offset: 0x0001751E
		// (set) Token: 0x06001304 RID: 4868 RVA: 0x00019326 File Offset: 0x00017526
		public string ShortCode { get; set; }

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x06001305 RID: 4869 RVA: 0x0001932F File Offset: 0x0001752F
		// (set) Token: 0x06001306 RID: 4870 RVA: 0x00019337 File Offset: 0x00017537
		public int ScreenNum { get; set; }
	}
}
