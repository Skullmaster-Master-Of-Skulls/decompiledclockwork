using System;

namespace TechnoPro.Common.Public.Entities.Accommodations
{
	// Token: 0x020005E4 RID: 1508
	[Serializable]
	public class ExtendedAccommodationInfo : ICloneable
	{
		// Token: 0x06003094 RID: 12436 RVA: 0x0000D55A File Offset: 0x0000B75A
		public ExtendedAccommodationInfo()
		{
		}

		// Token: 0x06003095 RID: 12437 RVA: 0x00042118 File Offset: 0x00040318
		public ExtendedAccommodationInfo(ExtendedAccommodationInfo item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.ShowOnLetter = item.ShowOnLetter;
				this.Approved = item.Approved;
				this.Offline = item.Offline;
				this.ExpiryDate = item.ExpiryDate;
				this.Note = item.Note;
				this.RecommendedButDeclined = item.RecommendedButDeclined;
				this.Rationale = item.Rationale;
				this.SessionDateEntered = item.SessionDateEntered;
				this.RecommendedButDeclinedDetail = item.RecommendedButDeclinedDetail;
				this.LongDescription = item.LongDescription;
				this.ShortCode = item.ShortCode;
				this.Group = item.Group;
				this.AccommodationType = item.AccommodationType;
			}
		}

		// Token: 0x170013E7 RID: 5095
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000421E3 File Offset: 0x000403E3
		// (set) Token: 0x06003097 RID: 12439 RVA: 0x000421EB File Offset: 0x000403EB
		public bool ShowOnLetter { get; set; }

		// Token: 0x170013E8 RID: 5096
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x000421F4 File Offset: 0x000403F4
		// (set) Token: 0x06003099 RID: 12441 RVA: 0x000421FC File Offset: 0x000403FC
		public bool Approved { get; set; }

		// Token: 0x170013E9 RID: 5097
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x00042205 File Offset: 0x00040405
		// (set) Token: 0x0600309B RID: 12443 RVA: 0x0004220D File Offset: 0x0004040D
		public bool Offline { get; set; }

		// Token: 0x170013EA RID: 5098
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x00042216 File Offset: 0x00040416
		// (set) Token: 0x0600309D RID: 12445 RVA: 0x0004221E File Offset: 0x0004041E
		public DateTime? ExpiryDate { get; set; }

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x00042227 File Offset: 0x00040427
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x0004222F File Offset: 0x0004042F
		public string Note { get; set; }

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x00042238 File Offset: 0x00040438
		// (set) Token: 0x060030A1 RID: 12449 RVA: 0x00042240 File Offset: 0x00040440
		public bool RecommendedButDeclined { get; set; }

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x00042249 File Offset: 0x00040449
		// (set) Token: 0x060030A3 RID: 12451 RVA: 0x00042251 File Offset: 0x00040451
		public string Rationale { get; set; }

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x0004225A File Offset: 0x0004045A
		// (set) Token: 0x060030A5 RID: 12453 RVA: 0x00042262 File Offset: 0x00040462
		public DateTime? SessionDateEntered { get; set; }

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x0004226B File Offset: 0x0004046B
		// (set) Token: 0x060030A7 RID: 12455 RVA: 0x00042273 File Offset: 0x00040473
		public string RecommendedButDeclinedDetail { get; set; }

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x0004227C File Offset: 0x0004047C
		// (set) Token: 0x060030A9 RID: 12457 RVA: 0x00042284 File Offset: 0x00040484
		public string LongDescription { get; set; }

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x0004228D File Offset: 0x0004048D
		// (set) Token: 0x060030AB RID: 12459 RVA: 0x00042295 File Offset: 0x00040495
		public string ShortCode { get; set; }

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x0004229E File Offset: 0x0004049E
		// (set) Token: 0x060030AD RID: 12461 RVA: 0x000422A6 File Offset: 0x000404A6
		public eAccommodationGroup Group { get; set; }

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x060030AE RID: 12462 RVA: 0x000422AF File Offset: 0x000404AF
		// (set) Token: 0x060030AF RID: 12463 RVA: 0x000422B7 File Offset: 0x000404B7
		public eAccommodationType AccommodationType { get; set; }

		// Token: 0x060030B0 RID: 12464 RVA: 0x000422C0 File Offset: 0x000404C0
		public ExtendedAccommodationInfo Clone()
		{
			return new ExtendedAccommodationInfo(this);
		}

		// Token: 0x060030B1 RID: 12465 RVA: 0x000422D8 File Offset: 0x000404D8
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
