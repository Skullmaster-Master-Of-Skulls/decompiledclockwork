using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.CustomForms.Field
{
	// Token: 0x0200041E RID: 1054
	public class CustomListItemGroup : BusinessBase<Guid>
	{
		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06002018 RID: 8216 RVA: 0x000246A8 File Offset: 0x000228A8
		// (set) Token: 0x06002019 RID: 8217 RVA: 0x000246C0 File Offset: 0x000228C0
		public new virtual Guid Id
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

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x0600201A RID: 8218 RVA: 0x000246CB File Offset: 0x000228CB
		// (set) Token: 0x0600201B RID: 8219 RVA: 0x000246D3 File Offset: 0x000228D3
		public string GroupCaption { get; set; }

		// Token: 0x17000D46 RID: 3398
		// (get) Token: 0x0600201C RID: 8220 RVA: 0x000246DC File Offset: 0x000228DC
		// (set) Token: 0x0600201D RID: 8221 RVA: 0x000246E4 File Offset: 0x000228E4
		public IList<CustomListItem> ListItems { get; set; }
	}
}
