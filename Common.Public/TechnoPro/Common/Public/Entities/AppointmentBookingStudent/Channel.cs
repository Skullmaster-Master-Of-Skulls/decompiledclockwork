using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.AppointmentBookingStudent.BookingRequest;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent
{
	// Token: 0x02000563 RID: 1379
	[Serializable]
	public class Channel : ICloneable<Channel>, ICloneable
	{
		// Token: 0x06002C51 RID: 11345 RVA: 0x0000D55A File Offset: 0x0000B75A
		public Channel()
		{
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x00031580 File Offset: 0x0002F780
		public Channel(Channel channel)
		{
			bool flag = channel == null;
			if (!flag)
			{
				this.IsActive = channel.IsActive;
				this.Id = channel.Id;
				this.Title = channel.Title;
				this.Description = channel.Description;
				this.OrderNum = channel.OrderNum;
				this.Availabilities = (from g in channel.Availabilities ?? new List<ChannelAvailability>()
				select g.Clone()).ToList<ChannelAvailability>();
			}
		}

		// Token: 0x17001294 RID: 4756
		// (get) Token: 0x06002C53 RID: 11347 RVA: 0x00031622 File Offset: 0x0002F822
		// (set) Token: 0x06002C54 RID: 11348 RVA: 0x0003162A File Offset: 0x0002F82A
		public bool IsActive { get; set; }

		// Token: 0x17001295 RID: 4757
		// (get) Token: 0x06002C55 RID: 11349 RVA: 0x00031633 File Offset: 0x0002F833
		// (set) Token: 0x06002C56 RID: 11350 RVA: 0x0003163B File Offset: 0x0002F83B
		public string Id { get; set; }

		// Token: 0x17001296 RID: 4758
		// (get) Token: 0x06002C57 RID: 11351 RVA: 0x00031644 File Offset: 0x0002F844
		// (set) Token: 0x06002C58 RID: 11352 RVA: 0x0003164C File Offset: 0x0002F84C
		public string Title { get; set; }

		// Token: 0x17001297 RID: 4759
		// (get) Token: 0x06002C59 RID: 11353 RVA: 0x00031655 File Offset: 0x0002F855
		// (set) Token: 0x06002C5A RID: 11354 RVA: 0x0003165D File Offset: 0x0002F85D
		public string Description { get; set; }

		// Token: 0x17001298 RID: 4760
		// (get) Token: 0x06002C5B RID: 11355 RVA: 0x00031666 File Offset: 0x0002F866
		// (set) Token: 0x06002C5C RID: 11356 RVA: 0x0003166E File Offset: 0x0002F86E
		public IList<ChannelAvailability> Availabilities { get; set; }

		// Token: 0x17001299 RID: 4761
		// (get) Token: 0x06002C5D RID: 11357 RVA: 0x00031677 File Offset: 0x0002F877
		// (set) Token: 0x06002C5E RID: 11358 RVA: 0x0003167F File Offset: 0x0002F87F
		public AppointmentBookingFilterParameters OverrideBookingFilterParameters { get; set; }

		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06002C5F RID: 11359 RVA: 0x00031688 File Offset: 0x0002F888
		// (set) Token: 0x06002C60 RID: 11360 RVA: 0x00031690 File Offset: 0x0002F890
		public int OrderNum { get; set; }

		// Token: 0x06002C61 RID: 11361 RVA: 0x0003169C File Offset: 0x0002F89C
		public Channel Clone()
		{
			return new Channel(this);
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000316B4 File Offset: 0x0002F8B4
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
