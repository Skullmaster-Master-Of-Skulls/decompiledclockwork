using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.Accommodations
{
	// Token: 0x020005E1 RID: 1505
	[Serializable]
	public class AccommodationData : IDynamicDataHoldingObject, ICloneable
	{
		// Token: 0x0600308B RID: 12427 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AccommodationData()
		{
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x00042054 File Offset: 0x00040254
		public AccommodationData(AccommodationData item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.Data = ((item.Data == null) ? null : item.Data.Clone());
				this.Detail = ((item.Detail == null) ? null : item.Detail.Clone());
			}
		}

		// Token: 0x170013E5 RID: 5093
		// (get) Token: 0x0600308D RID: 12429 RVA: 0x000420AD File Offset: 0x000402AD
		// (set) Token: 0x0600308E RID: 12430 RVA: 0x000420B5 File Offset: 0x000402B5
		public DynamicData Data { get; set; }

		// Token: 0x170013E6 RID: 5094
		// (get) Token: 0x0600308F RID: 12431 RVA: 0x000420BE File Offset: 0x000402BE
		// (set) Token: 0x06003090 RID: 12432 RVA: 0x000420C6 File Offset: 0x000402C6
		public ExtendedAccommodationInfo Detail { get; set; }

		// Token: 0x06003091 RID: 12433 RVA: 0x000420D0 File Offset: 0x000402D0
		public DynamicData GetDynamicData()
		{
			return this.Data;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x000420E8 File Offset: 0x000402E8
		public AccommodationData Clone()
		{
			return new AccommodationData(this);
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x00042100 File Offset: 0x00040300
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
