using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Room
{
	// Token: 0x0200020B RID: 523
	public class SeatAsset : BusinessBase<string>
	{
		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06000FEE RID: 4078 RVA: 0x000172C0 File Offset: 0x000154C0
		// (set) Token: 0x06000FEF RID: 4079 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string SeatAssetId
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

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06000FF0 RID: 4080 RVA: 0x000172D8 File Offset: 0x000154D8
		// (set) Token: 0x06000FF1 RID: 4081 RVA: 0x000172E0 File Offset: 0x000154E0
		public string Title { get; set; }

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x000172E9 File Offset: 0x000154E9
		// (set) Token: 0x06000FF3 RID: 4083 RVA: 0x000172F1 File Offset: 0x000154F1
		public int Score { get; set; }

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x000172FA File Offset: 0x000154FA
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x00017302 File Offset: 0x00015502
		public bool IsActive { get; set; }

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x0001730B File Offset: 0x0001550B
		// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x00017313 File Offset: 0x00015513
		public IList<SeatAssetAccommodation> AccommodationsBehind { get; set; }
	}
}
