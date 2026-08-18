using System;

namespace TechnoPro.Common.Public.Entities.AlternativeFormat
{
	// Token: 0x02000571 RID: 1393
	public class AccommodationAltFormatTypesMapping
	{
		// Token: 0x170012CF RID: 4815
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x00031C92 File Offset: 0x0002FE92
		// (set) Token: 0x06002CE0 RID: 11488 RVA: 0x00031C9A File Offset: 0x0002FE9A
		public int AccommodationControlId { get; set; }

		// Token: 0x170012D0 RID: 4816
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x00031CA3 File Offset: 0x0002FEA3
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x00031CAB File Offset: 0x0002FEAB
		public MediaContentFormat[] AltFormatTypes { get; set; }
	}
}
