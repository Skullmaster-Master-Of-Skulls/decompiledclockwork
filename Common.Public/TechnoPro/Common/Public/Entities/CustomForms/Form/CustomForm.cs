using System;

namespace TechnoPro.Common.Public.Entities.CustomForms.Form
{
	// Token: 0x0200041B RID: 1051
	public class CustomForm : BusinessBase<Guid>
	{
		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06001FFD RID: 8189 RVA: 0x000245C4 File Offset: 0x000227C4
		// (set) Token: 0x06001FFE RID: 8190 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid FormId
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

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06001FFF RID: 8191 RVA: 0x000245DC File Offset: 0x000227DC
		// (set) Token: 0x06002000 RID: 8192 RVA: 0x000245E4 File Offset: 0x000227E4
		public string Xml { get; set; }

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002001 RID: 8193 RVA: 0x000245ED File Offset: 0x000227ED
		// (set) Token: 0x06002002 RID: 8194 RVA: 0x000245F5 File Offset: 0x000227F5
		public string Title { get; set; }

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002003 RID: 8195 RVA: 0x000245FE File Offset: 0x000227FE
		// (set) Token: 0x06002004 RID: 8196 RVA: 0x00024606 File Offset: 0x00022806
		public bool IsHidden { get; set; }
	}
}
