using System;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Public.Entities.Cards
{
	// Token: 0x0200046C RID: 1132
	public class CardTypeAttribute : Attribute
	{
		// Token: 0x0600225E RID: 8798 RVA: 0x0000EC26 File Offset: 0x0000CE26
		public CardTypeAttribute()
		{
		}

		// Token: 0x0600225F RID: 8799 RVA: 0x000264C9 File Offset: 0x000246C9
		public CardTypeAttribute(params eCoreGroup[] allowedCoreGroups)
		{
			this.AllowedCoreGroups = allowedCoreGroups;
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06002260 RID: 8800 RVA: 0x000264DB File Offset: 0x000246DB
		// (set) Token: 0x06002261 RID: 8801 RVA: 0x000264E3 File Offset: 0x000246E3
		public eCoreGroup[] AllowedCoreGroups { get; set; }

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06002262 RID: 8802 RVA: 0x000264EC File Offset: 0x000246EC
		// (set) Token: 0x06002263 RID: 8803 RVA: 0x000264F4 File Offset: 0x000246F4
		public bool IsDisabled { get; set; }
	}
}
