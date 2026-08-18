using System;

namespace TechnoPro.Common.Public.Entities.Legacy.ServiceProviders
{
	// Token: 0x020002F7 RID: 759
	public class LegacyRequestDetailNotesAndSpecialInstructions : BusinessBase<int>
	{
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0001C230 File Offset: 0x0001A430
		// (set) Token: 0x060016F9 RID: 5881 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int RequestId
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

		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x0001C248 File Offset: 0x0001A448
		// (set) Token: 0x060016FB RID: 5883 RVA: 0x0001C250 File Offset: 0x0001A450
		public string Notes { get; set; }

		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x060016FC RID: 5884 RVA: 0x0001C259 File Offset: 0x0001A459
		// (set) Token: 0x060016FD RID: 5885 RVA: 0x0001C261 File Offset: 0x0001A461
		public string SpecialInstructions { get; set; }
	}
}
