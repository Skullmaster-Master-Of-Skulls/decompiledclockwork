using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003BF RID: 959
	public class DataGridItem : TableRow, IDataItemContainer, INamingContainer
	{
		// Token: 0x06002E5D RID: 11869 RVA: 0x000981BA File Offset: 0x000963BA
		public DataGridItem(int itemIndex, int dataSetIndex, ListItemType itemType)
		{
			this.itemIndex = itemIndex;
			this.dataSetIndex = dataSetIndex;
			this.itemType = itemType;
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000981D7 File Offset: 0x000963D7
		// (set) Token: 0x06002E5F RID: 11871 RVA: 0x000981DF File Offset: 0x000963DF
		public virtual object DataItem
		{
			get
			{
				return this.dataItem;
			}
			set
			{
				this.dataItem = value;
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x06002E60 RID: 11872 RVA: 0x000981E8 File Offset: 0x000963E8
		public virtual int DataSetIndex
		{
			get
			{
				return this.dataSetIndex;
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000981F0 File Offset: 0x000963F0
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002E62 RID: 11874 RVA: 0x000981F8 File Offset: 0x000963F8
		public virtual ListItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x00098200 File Offset: 0x00096400
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				DataGridCommandEventArgs args = new DataGridCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x0009822E File Offset: 0x0009642E
		protected internal virtual void SetItemType(ListItemType itemType)
		{
			this.itemType = itemType;
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06002E65 RID: 11877 RVA: 0x00098237 File Offset: 0x00096437
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.DataItem;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06002E66 RID: 11878 RVA: 0x0009823F File Offset: 0x0009643F
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.DataSetIndex;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x06002E67 RID: 11879 RVA: 0x00098247 File Offset: 0x00096447
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x04001FED RID: 8173
		private int itemIndex;

		// Token: 0x04001FEE RID: 8174
		private int dataSetIndex;

		// Token: 0x04001FEF RID: 8175
		private ListItemType itemType;

		// Token: 0x04001FF0 RID: 8176
		private object dataItem;
	}
}
