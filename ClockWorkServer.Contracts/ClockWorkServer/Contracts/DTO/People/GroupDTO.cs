using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.People
{
	// Token: 0x0200036A RID: 874
	[DataContract(Namespace = "http://tpro.ca")]
	public class GroupDTO : ICloneable<GroupDTO>, ICloneable
	{
		// Token: 0x06001403 RID: 5123 RVA: 0x000036BD File Offset: 0x000018BD
		public GroupDTO()
		{
		}

		// Token: 0x06001404 RID: 5124 RVA: 0x000095DC File Offset: 0x000077DC
		public GroupDTO(GroupDTO item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.GroupId = item.GroupId;
				this.Description = item.Description;
				this.VisibleInCalendar = item.VisibleInCalendar;
				this.FullDescription = item.FullDescription;
				this.OrderNum = item.OrderNum;
			}
		}

		// Token: 0x17000616 RID: 1558
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x0000963C File Offset: 0x0000783C
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x00009644 File Offset: 0x00007844
		[DataMember]
		public int GroupId { get; set; }

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06001407 RID: 5127 RVA: 0x0000964D File Offset: 0x0000784D
		// (set) Token: 0x06001408 RID: 5128 RVA: 0x00009655 File Offset: 0x00007855
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06001409 RID: 5129 RVA: 0x0000965E File Offset: 0x0000785E
		// (set) Token: 0x0600140A RID: 5130 RVA: 0x00009666 File Offset: 0x00007866
		[DataMember]
		public bool VisibleInCalendar { get; set; }

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x0600140B RID: 5131 RVA: 0x0000966F File Offset: 0x0000786F
		// (set) Token: 0x0600140C RID: 5132 RVA: 0x00009677 File Offset: 0x00007877
		[DataMember]
		public string FullDescription { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x0600140D RID: 5133 RVA: 0x00009680 File Offset: 0x00007880
		// (set) Token: 0x0600140E RID: 5134 RVA: 0x00009688 File Offset: 0x00007888
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x0600140F RID: 5135 RVA: 0x00009694 File Offset: 0x00007894
		public GroupDTO Clone()
		{
			return new GroupDTO(this);
		}

		// Token: 0x06001410 RID: 5136 RVA: 0x000096AC File Offset: 0x000078AC
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
