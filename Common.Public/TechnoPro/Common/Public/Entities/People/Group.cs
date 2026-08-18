using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x0200026A RID: 618
	public class Group : BusinessBase<int>, ICloneable<Group>, ICloneable
	{
		// Token: 0x06001290 RID: 4752 RVA: 0x00018C08 File Offset: 0x00016E08
		public Group()
		{
			this.GroupId = 0;
			this.Description = "";
			this.VisibleInCalendar = false;
		}

		// Token: 0x06001291 RID: 4753 RVA: 0x00018C30 File Offset: 0x00016E30
		public Group(Group g)
		{
			bool flag = g == null;
			if (!flag)
			{
				this.GroupId = g.GroupId;
				this.Description = g.Description;
				this.VisibleInCalendar = g.VisibleInCalendar;
				this.FullDescription = g.FullDescription;
				this.OrderNum = g.OrderNum;
			}
		}

		// Token: 0x170007A9 RID: 1961
		// (get) Token: 0x06001292 RID: 4754 RVA: 0x00018C90 File Offset: 0x00016E90
		// (set) Token: 0x06001293 RID: 4755 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int GroupId
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

		// Token: 0x170007AA RID: 1962
		// (get) Token: 0x06001294 RID: 4756 RVA: 0x00018CA8 File Offset: 0x00016EA8
		// (set) Token: 0x06001295 RID: 4757 RVA: 0x00018CB0 File Offset: 0x00016EB0
		public string Description { get; set; }

		// Token: 0x170007AB RID: 1963
		// (get) Token: 0x06001296 RID: 4758 RVA: 0x00018CB9 File Offset: 0x00016EB9
		// (set) Token: 0x06001297 RID: 4759 RVA: 0x00018CC1 File Offset: 0x00016EC1
		public bool VisibleInCalendar { get; set; }

		// Token: 0x170007AC RID: 1964
		// (get) Token: 0x06001298 RID: 4760 RVA: 0x00018CCA File Offset: 0x00016ECA
		// (set) Token: 0x06001299 RID: 4761 RVA: 0x00018CD2 File Offset: 0x00016ED2
		public string FullDescription { get; set; }

		// Token: 0x170007AD RID: 1965
		// (get) Token: 0x0600129A RID: 4762 RVA: 0x00018CDB File Offset: 0x00016EDB
		// (set) Token: 0x0600129B RID: 4763 RVA: 0x00018CE3 File Offset: 0x00016EE3
		public int OrderNum { get; set; }

		// Token: 0x0600129C RID: 4764 RVA: 0x00018CEC File Offset: 0x00016EEC
		public Group Clone()
		{
			return new Group(this);
		}

		// Token: 0x0600129D RID: 4765 RVA: 0x00018D04 File Offset: 0x00016F04
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}
