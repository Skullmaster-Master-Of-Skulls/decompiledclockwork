using System;
using System.Collections.Generic;

namespace NewBooker.Entities.AutoTestBooking.Booker2
{
	// Token: 0x020000B6 RID: 182
	public class TryToBookWorking
	{
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000D5ED File Offset: 0x0000B7ED
		// (set) Token: 0x06000487 RID: 1159 RVA: 0x0000D5F5 File Offset: 0x0000B7F5
		public TryToBookContext Context { get; set; }

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000D5FE File Offset: 0x0000B7FE
		// (set) Token: 0x06000489 RID: 1161 RVA: 0x0000D606 File Offset: 0x0000B806
		public TryToBookSearchOptions SearchOptions { get; set; }

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000D60F File Offset: 0x0000B80F
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x0000D617 File Offset: 0x0000B817
		public TryToBookCaches Caches { get; set; }

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600048C RID: 1164 RVA: 0x0000D620 File Offset: 0x0000B820
		// (set) Token: 0x0600048D RID: 1165 RVA: 0x0000D628 File Offset: 0x0000B828
		public TryToBookEnvironment Environment { get; set; }

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600048E RID: 1166 RVA: 0x0000D631 File Offset: 0x0000B831
		// (set) Token: 0x0600048F RID: 1167 RVA: 0x0000D639 File Offset: 0x0000B839
		public IList<string> AssetIdsRequired { get; set; }

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000490 RID: 1168 RVA: 0x0000D642 File Offset: 0x0000B842
		// (set) Token: 0x06000491 RID: 1169 RVA: 0x0000D64A File Offset: 0x0000B84A
		public int StudentTestDuration { get; set; }

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x06000492 RID: 1170 RVA: 0x0000D653 File Offset: 0x0000B853
		// (set) Token: 0x06000493 RID: 1171 RVA: 0x0000D65B File Offset: 0x0000B85B
		public IList<TryToBookRoom> AllRooms { get; set; }

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000494 RID: 1172 RVA: 0x0000D664 File Offset: 0x0000B864
		// (set) Token: 0x06000495 RID: 1173 RVA: 0x0000D66C File Offset: 0x0000B86C
		public IList<TryToBookRoom> AllVirtualRooms { get; set; }

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000496 RID: 1174 RVA: 0x0000D675 File Offset: 0x0000B875
		// (set) Token: 0x06000497 RID: 1175 RVA: 0x0000D67D File Offset: 0x0000B87D
		public IList<TryToBookRoom> AllNonVirtualRooms { get; set; }

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x06000498 RID: 1176 RVA: 0x0000D686 File Offset: 0x0000B886
		// (set) Token: 0x06000499 RID: 1177 RVA: 0x0000D68E File Offset: 0x0000B88E
		public int MaxNumberOfPotentialBookings { get; set; }

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x0600049A RID: 1178 RVA: 0x0000D697 File Offset: 0x0000B897
		// (set) Token: 0x0600049B RID: 1179 RVA: 0x0000D69F File Offset: 0x0000B89F
		public IList<TryToBookRoom> RoomsToInvestigate { get; set; }

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x0600049C RID: 1180 RVA: 0x0000D6A8 File Offset: 0x0000B8A8
		// (set) Token: 0x0600049D RID: 1181 RVA: 0x0000D6B0 File Offset: 0x0000B8B0
		public IList<int> IncrementalMinutesToTry { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x0600049E RID: 1182 RVA: 0x0000D6B9 File Offset: 0x0000B8B9
		// (set) Token: 0x0600049F RID: 1183 RVA: 0x0000D6C1 File Offset: 0x0000B8C1
		public IList<TryToBookSpecialAccommodation> SpecialAccommodationsRequired { get; set; }

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x0000D6CA File Offset: 0x0000B8CA
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x0000D6D2 File Offset: 0x0000B8D2
		public IDictionary<eSpecialAccommodationApplyMethod, List<SpecialAccommodationReq>> SpecialAccommodationActionsRequired { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000D6DB File Offset: 0x0000B8DB
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000D6E3 File Offset: 0x0000B8E3
		public TryToBookResult Result { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000D6EC File Offset: 0x0000B8EC
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
		public TryToBookTimeToInvestigate TimeToInvestigate { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000D6FD File Offset: 0x0000B8FD
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0000D705 File Offset: 0x0000B905
		public TryToBookRule CurrentRule { get; set; }
	}
}
