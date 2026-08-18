using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020004B8 RID: 1208
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridBatchEditingSettings : IStateManager
	{
		// Token: 0x06002AEC RID: 10988 RVA: 0x0008B4ED File Offset: 0x000896ED
		public GridBatchEditingSettings(GridTableView ownerTableView)
		{
			this.ownerTableView = ownerTableView;
		}

		// Token: 0x17000DC6 RID: 3526
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x0008B508 File Offset: 0x00089708
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x0008B531 File Offset: 0x00089731
		[Description("A value determining whether the editing will be made cell by cell or the the entire row will be opened for edit.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(GridBatchEditingType), "Cell")]
		public virtual GridBatchEditingType EditType
		{
			get
			{
				object obj = this.ViewState["EditMode"];
				if (obj != null)
				{
					return (GridBatchEditingType)obj;
				}
				return GridBatchEditingType.Cell;
			}
			set
			{
				this.ViewState["EditMode"] = value;
			}
		}

		// Token: 0x17000DC7 RID: 3527
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x0008B54C File Offset: 0x0008974C
		// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x0008B575 File Offset: 0x00089775
		[NotifyParentProperty(true)]
		[DefaultValue(GridBatchEditingEventType.Click)]
		[Description("A string value determining the event which will cause the cell\row will be opened for edit. The default value is “click”. Examples for event values – “dblclick”, “click”, “mousedown”, “mouseup”, “mouseover”.")]
		public virtual GridBatchEditingEventType OpenEditingEvent
		{
			get
			{
				object obj = this.ViewState["OpenEditEvent"];
				if (obj != null)
				{
					return (GridBatchEditingEventType)obj;
				}
				return GridBatchEditingEventType.Click;
			}
			set
			{
				this.ViewState["OpenEditEvent"] = value;
			}
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x0008B590 File Offset: 0x00089790
		// (set) Token: 0x06002AF2 RID: 10994 RVA: 0x0008B5B9 File Offset: 0x000897B9
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets a value determining if the SaveChanges button will save the changes from the current GridTableView or calls the saveAllChanges function and saves the changes from the all GridTableView's in the grid.")]
		public virtual bool SaveAllHierarchyLevels
		{
			get
			{
				object obj = this.ViewState["SaveAllHierarchyLevels"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["SaveAllHierarchyLevels"] = value;
			}
		}

		// Token: 0x17000DC9 RID: 3529
		// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x0008B5D4 File Offset: 0x000897D4
		// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x0008B5FD File Offset: 0x000897FD
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Description("Gets or sets a value determining whether the deleted row/rows will be physically removed from the table or just marked as deleted")]
		public virtual bool HighlightDeletedRows
		{
			get
			{
				object obj = this.ViewState["HighlightDeletedRows"];
				return obj != null && (bool)obj;
			}
			set
			{
				this.ViewState["HighlightDeletedRows"] = value;
			}
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06002AF5 RID: 10997 RVA: 0x0008B615 File Offset: 0x00089815
		private StateBag ViewState
		{
			get
			{
				return this.stateManager.ViewState;
			}
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x0008B622 File Offset: 0x00089822
		void IStateManager.LoadViewState(object state)
		{
			this.stateManager.LoadViewState(state);
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x0008B630 File Offset: 0x00089830
		object IStateManager.SaveViewState()
		{
			return this.stateManager.SaveViewState();
		}

		// Token: 0x06002AF8 RID: 11000 RVA: 0x0008B63D File Offset: 0x0008983D
		void IStateManager.TrackViewState()
		{
			this.stateManager.TrackViewState();
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06002AF9 RID: 11001 RVA: 0x0008B64A File Offset: 0x0008984A
		bool IStateManager.IsTrackingViewState
		{
			get
			{
				return this.stateManager.IsTrackingViewState;
			}
		}

		// Token: 0x04000B3E RID: 2878
		private readonly GridTableView ownerTableView;

		// Token: 0x04000B3F RID: 2879
		private GridStateManager stateManager = new GridStateManager();
	}
}
