using System;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Vets
{
	// Token: 0x02000108 RID: 264
	public class VetsChangeInBenefitApplication : BusinessBase<Guid>
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x0000F0CC File Offset: 0x0000D2CC
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x0000EC6C File Offset: 0x0000CE6C
		public virtual Guid ChangeInBenefitApplicationId
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

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x0000F0E4 File Offset: 0x0000D2E4
		// (set) Token: 0x06000617 RID: 1559 RVA: 0x0000F0EC File Offset: 0x0000D2EC
		public DateTime DateCreated { get; set; }

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x0000F0F5 File Offset: 0x0000D2F5
		// (set) Token: 0x06000619 RID: 1561 RVA: 0x0000F0FD File Offset: 0x0000D2FD
		public PersonBase WhoCreated { get; set; }

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x0000F106 File Offset: 0x0000D306
		// (set) Token: 0x0600061B RID: 1563 RVA: 0x0000F10E File Offset: 0x0000D30E
		public CustomDataSet ChangeInBenefitFormCustomData { get; set; }
	}
}
