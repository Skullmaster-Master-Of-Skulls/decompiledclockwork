using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent
{
	// Token: 0x02000564 RID: 1380
	[Serializable]
	public class ChannelAvailability : ICloneable<ChannelAvailability>, ICloneable
	{
		// Token: 0x06002C63 RID: 11363 RVA: 0x0000D55A File Offset: 0x0000B75A
		public ChannelAvailability()
		{
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000316CC File Offset: 0x0002F8CC
		public ChannelAvailability(ChannelAvailability ca)
		{
			bool flag = ca == null;
			if (!flag)
			{
				this.AvailabilityGroupId = ca.AvailabilityGroupId;
				this.IsActive = ca.IsActive;
				this.AppTypeIdToBookWith = ca.AppTypeIdToBookWith;
				this.PersonCollection = (ca.PersonCollection ?? new List<ChannelPersonCollection>()).ToList<ChannelPersonCollection>().ConvertAll<ChannelPersonCollection>((ChannelPersonCollection g) => (g != null) ? g.Clone() : null);
				this.PreBookScreenNum = ca.PreBookScreenNum;
				this.SlotSizeInMinutes = ca.SlotSizeInMinutes;
				this.Title = ca.Title;
				this.UseAssignedAdvisorInsteadOfPersonCollection = ca.UseAssignedAdvisorInsteadOfPersonCollection;
			}
		}

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06002C65 RID: 11365 RVA: 0x00031788 File Offset: 0x0002F988
		// (set) Token: 0x06002C66 RID: 11366 RVA: 0x00031790 File Offset: 0x0002F990
		public bool IsActive { get; set; }

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06002C67 RID: 11367 RVA: 0x00031799 File Offset: 0x0002F999
		// (set) Token: 0x06002C68 RID: 11368 RVA: 0x000317A1 File Offset: 0x0002F9A1
		public int AvailabilityGroupId { get; set; }

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06002C69 RID: 11369 RVA: 0x000317AA File Offset: 0x0002F9AA
		// (set) Token: 0x06002C6A RID: 11370 RVA: 0x000317B2 File Offset: 0x0002F9B2
		public int AppTypeIdToBookWith { get; set; }

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06002C6B RID: 11371 RVA: 0x000317BB File Offset: 0x0002F9BB
		// (set) Token: 0x06002C6C RID: 11372 RVA: 0x000317C3 File Offset: 0x0002F9C3
		public int PreBookScreenNum { get; set; }

		// Token: 0x1700129F RID: 4767
		// (get) Token: 0x06002C6D RID: 11373 RVA: 0x000317CC File Offset: 0x0002F9CC
		// (set) Token: 0x06002C6E RID: 11374 RVA: 0x000317D4 File Offset: 0x0002F9D4
		public int SlotSizeInMinutes { get; set; }

		// Token: 0x170012A0 RID: 4768
		// (get) Token: 0x06002C6F RID: 11375 RVA: 0x000317DD File Offset: 0x0002F9DD
		// (set) Token: 0x06002C70 RID: 11376 RVA: 0x000317E5 File Offset: 0x0002F9E5
		public string Title { get; set; }

		// Token: 0x170012A1 RID: 4769
		// (get) Token: 0x06002C71 RID: 11377 RVA: 0x000317EE File Offset: 0x0002F9EE
		// (set) Token: 0x06002C72 RID: 11378 RVA: 0x000317F6 File Offset: 0x0002F9F6
		public IList<ChannelPersonCollection> PersonCollection { get; set; }

		// Token: 0x170012A2 RID: 4770
		// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000317FF File Offset: 0x0002F9FF
		// (set) Token: 0x06002C74 RID: 11380 RVA: 0x00031807 File Offset: 0x0002FA07
		public bool UseAssignedAdvisorInsteadOfPersonCollection { get; set; }

		// Token: 0x170012A3 RID: 4771
		// (get) Token: 0x06002C75 RID: 11381 RVA: 0x00031810 File Offset: 0x0002FA10
		// (set) Token: 0x06002C76 RID: 11382 RVA: 0x00031818 File Offset: 0x0002FA18
		public int[] UseAssignedAdvisorInsteadOfPersonCollectionOverrideAssignedAdvisorCids { get; set; }

		// Token: 0x06002C77 RID: 11383 RVA: 0x00031824 File Offset: 0x0002FA24
		public ChannelAvailability Clone()
		{
			return new ChannelAvailability(this);
		}

		// Token: 0x06002C78 RID: 11384 RVA: 0x0003183C File Offset: 0x0002FA3C
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
