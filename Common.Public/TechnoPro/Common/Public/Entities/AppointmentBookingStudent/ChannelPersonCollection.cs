using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnoPro.Common.Public.Entities.AppointmentBookingStudent
{
	// Token: 0x02000565 RID: 1381
	[Serializable]
	public class ChannelPersonCollection : ICloneable<ChannelPersonCollection>, ICloneable
	{
		// Token: 0x06002C79 RID: 11385 RVA: 0x0000D55A File Offset: 0x0000B75A
		public ChannelPersonCollection()
		{
		}

		// Token: 0x06002C7A RID: 11386 RVA: 0x00031854 File Offset: 0x0002FA54
		public ChannelPersonCollection(ChannelPersonCollection cpc)
		{
			bool flag = cpc == null;
			if (!flag)
			{
				this.Campus = ((cpc.Campus == null) ? null : cpc.Campus.Clone());
				this.ColourArgB = cpc.ColourArgB;
				this.Id = cpc.Id;
				this.IsActive = cpc.IsActive;
				this.Title = cpc.Title;
				this.UnderlyingPeople = (cpc.UnderlyingPeople ?? new List<ChannelUnderlyingPerson>()).ToList<ChannelUnderlyingPerson>().ConvertAll<ChannelUnderlyingPerson>((ChannelUnderlyingPerson g) => (g == null) ? null : g.Clone());
			}
		}

		// Token: 0x170012A4 RID: 4772
		// (get) Token: 0x06002C7B RID: 11387 RVA: 0x00031906 File Offset: 0x0002FB06
		// (set) Token: 0x06002C7C RID: 11388 RVA: 0x0003190E File Offset: 0x0002FB0E
		public bool IsActive { get; set; }

		// Token: 0x170012A5 RID: 4773
		// (get) Token: 0x06002C7D RID: 11389 RVA: 0x00031917 File Offset: 0x0002FB17
		// (set) Token: 0x06002C7E RID: 11390 RVA: 0x0003191F File Offset: 0x0002FB1F
		public string Title { get; set; }

		// Token: 0x170012A6 RID: 4774
		// (get) Token: 0x06002C7F RID: 11391 RVA: 0x00031928 File Offset: 0x0002FB28
		// (set) Token: 0x06002C80 RID: 11392 RVA: 0x00031930 File Offset: 0x0002FB30
		public string Id { get; set; }

		// Token: 0x170012A7 RID: 4775
		// (get) Token: 0x06002C81 RID: 11393 RVA: 0x00031939 File Offset: 0x0002FB39
		// (set) Token: 0x06002C82 RID: 11394 RVA: 0x00031941 File Offset: 0x0002FB41
		public IList<ChannelUnderlyingPerson> UnderlyingPeople { get; set; }

		// Token: 0x170012A8 RID: 4776
		// (get) Token: 0x06002C83 RID: 11395 RVA: 0x0003194A File Offset: 0x0002FB4A
		// (set) Token: 0x06002C84 RID: 11396 RVA: 0x00031952 File Offset: 0x0002FB52
		public int? ColourArgB { get; set; }

		// Token: 0x170012A9 RID: 4777
		// (get) Token: 0x06002C85 RID: 11397 RVA: 0x0003195B File Offset: 0x0002FB5B
		// (set) Token: 0x06002C86 RID: 11398 RVA: 0x00031963 File Offset: 0x0002FB63
		public SchoolCampus Campus { get; set; }

		// Token: 0x06002C87 RID: 11399 RVA: 0x0003196C File Offset: 0x0002FB6C
		public ChannelPersonCollection Clone()
		{
			return new ChannelPersonCollection(this);
		}

		// Token: 0x06002C88 RID: 11400 RVA: 0x00031984 File Offset: 0x0002FB84
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
