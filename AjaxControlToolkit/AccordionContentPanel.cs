using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x02000004 RID: 4
	public class AccordionContentPanel : Panel, IDataItemContainer, INamingContainer
	{
		// Token: 0x06000045 RID: 69 RVA: 0x00002DBD File Offset: 0x00000FBD
		internal AccordionContentPanel()
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002DC5 File Offset: 0x00000FC5
		internal AccordionContentPanel(object dataItem, int dataIndex, AccordionItemType type) : this()
		{
			this.SetDataItemProperties(dataItem, dataIndex, type);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002DD6 File Offset: 0x00000FD6
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002DDE File Offset: 0x00000FDE
		public bool Collapsed
		{
			get
			{
				return this._collapsed;
			}
			set
			{
				this._collapsed = value;
				base.Style[HtmlTextWriterStyle.Display] = (this._collapsed ? "none" : "block");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002E08 File Offset: 0x00001008
		public AccordionItemType ItemType
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002E10 File Offset: 0x00001010
		public object DataItem
		{
			get
			{
				return this._dataItem;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002E18 File Offset: 0x00001018
		public int DataItemIndex
		{
			get
			{
				return this._dataIndex;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004C RID: 76 RVA: 0x00002E20 File Offset: 0x00001020
		public int DisplayIndex
		{
			get
			{
				return this._dataIndex;
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E28 File Offset: 0x00001028
		protected override bool OnBubbleEvent(object source, EventArgs args)
		{
			CommandEventArgs commandEventArgs = args as CommandEventArgs;
			if (commandEventArgs != null)
			{
				AccordionCommandEventArgs args2 = new AccordionCommandEventArgs(this, commandEventArgs.CommandName, commandEventArgs.CommandArgument);
				base.RaiseBubbleEvent(this, args2);
				return true;
			}
			return false;
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E5D File Offset: 0x0000105D
		internal void SetDataItemProperties(object dataItem, int dataIndex, AccordionItemType type)
		{
			this._dataItem = dataItem;
			this._dataIndex = dataIndex;
			this._type = type;
		}

		// Token: 0x04000015 RID: 21
		private bool _collapsed;

		// Token: 0x04000016 RID: 22
		private object _dataItem;

		// Token: 0x04000017 RID: 23
		private int _dataIndex;

		// Token: 0x04000018 RID: 24
		private AccordionItemType _type;
	}
}
