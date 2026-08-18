using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001A36 RID: 6710
	[ToolboxItem(false)]
	public class SchedulerResourceContainer : Control, IDataItemContainer, INamingContainer
	{
		// Token: 0x0601047B RID: 66683 RVA: 0x003A382C File Offset: 0x003A1A2C
		public SchedulerResourceContainer(RadScheduler owner)
		{
			this._owner = owner;
		}

		// Token: 0x17004EE6 RID: 20198
		// (get) Token: 0x0601047C RID: 66684 RVA: 0x003A383B File Offset: 0x003A1A3B
		// (set) Token: 0x0601047D RID: 66685 RVA: 0x003A3843 File Offset: 0x003A1A43
		public Resource Resource
		{
			get
			{
				return this._resource;
			}
			set
			{
				this._resource = value;
			}
		}

		// Token: 0x17004EE7 RID: 20199
		// (get) Token: 0x0601047E RID: 66686 RVA: 0x003A384C File Offset: 0x003A1A4C
		// (set) Token: 0x0601047F RID: 66687 RVA: 0x003A3854 File Offset: 0x003A1A54
		public ITemplate Template
		{
			get
			{
				return this._template;
			}
			set
			{
				this._template = value;
			}
		}

		// Token: 0x17004EE8 RID: 20200
		// (get) Token: 0x06010480 RID: 66688 RVA: 0x003A385D File Offset: 0x003A1A5D
		protected RadScheduler Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x06010481 RID: 66689 RVA: 0x003A3865 File Offset: 0x003A1A65
		protected virtual object GetDataItem()
		{
			return this.Resource;
		}

		// Token: 0x17004EE9 RID: 20201
		// (get) Token: 0x06010482 RID: 66690 RVA: 0x003A386D File Offset: 0x003A1A6D
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.GetDataItem();
			}
		}

		// Token: 0x17004EEA RID: 20202
		// (get) Token: 0x06010483 RID: 66691 RVA: 0x003A3875 File Offset: 0x003A1A75
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17004EEB RID: 20203
		// (get) Token: 0x06010484 RID: 66692 RVA: 0x003A3878 File Offset: 0x003A1A78
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x04004954 RID: 18772
		private RadScheduler _owner;

		// Token: 0x04004955 RID: 18773
		private Resource _resource;

		// Token: 0x04004956 RID: 18774
		private ITemplate _template;
	}
}
