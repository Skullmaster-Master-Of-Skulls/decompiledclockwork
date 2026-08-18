using System;
using System.Collections;
using System.ComponentModel;
using System.Security.Permissions;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200054E RID: 1358
	[ToolboxItem(false)]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class DataListItem : WebControl, IDataItemContainer, INamingContainer
	{
		// Token: 0x0600430F RID: 17167 RVA: 0x0011537E File Offset: 0x0011437E
		public DataListItem(int itemIndex, ListItemType itemType)
		{
			this.itemIndex = itemIndex;
			this.itemType = itemType;
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06004310 RID: 17168 RVA: 0x00115394 File Offset: 0x00114394
		// (set) Token: 0x06004311 RID: 17169 RVA: 0x0011539C File Offset: 0x0011439C
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

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06004312 RID: 17170 RVA: 0x001153A5 File Offset: 0x001143A5
		public virtual int ItemIndex
		{
			get
			{
				return this.itemIndex;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x001153AD File Offset: 0x001143AD
		public virtual ListItemType ItemType
		{
			get
			{
				return this.itemType;
			}
		}

		// Token: 0x06004314 RID: 17172 RVA: 0x001153B5 File Offset: 0x001143B5
		protected override Style CreateControlStyle()
		{
			return new TableItemStyle();
		}

		// Token: 0x06004315 RID: 17173 RVA: 0x001153BC File Offset: 0x001143BC
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			if (e is CommandEventArgs)
			{
				DataListCommandEventArgs args = new DataListCommandEventArgs(this, source, (CommandEventArgs)e);
				base.RaiseBubbleEvent(this, args);
				return true;
			}
			return false;
		}

		// Token: 0x06004316 RID: 17174 RVA: 0x001153EC File Offset: 0x001143EC
		public virtual void RenderItem(HtmlTextWriter writer, bool extractRows, bool tableLayout)
		{
			HttpContext context = this.Context;
			if (context != null && context.TraceIsEnabled)
			{
				int bufferedLength = context.Response.GetBufferedLength();
				this.RenderItemInternal(writer, extractRows, tableLayout);
				int bufferedLength2 = context.Response.GetBufferedLength();
				context.Trace.AddControlSize(this.UniqueID, bufferedLength2 - bufferedLength);
				return;
			}
			this.RenderItemInternal(writer, extractRows, tableLayout);
		}

		// Token: 0x06004317 RID: 17175 RVA: 0x0011544C File Offset: 0x0011444C
		private void RenderItemInternal(HtmlTextWriter writer, bool extractRows, bool tableLayout)
		{
			if (!extractRows)
			{
				if (tableLayout)
				{
					this.RenderContents(writer);
					return;
				}
				this.RenderControl(writer);
				return;
			}
			else
			{
				IEnumerator enumerator = this.Controls.GetEnumerator();
				Table table = null;
				bool flag = false;
				while (enumerator.MoveNext())
				{
					flag = true;
					Control control = (Control)enumerator.Current;
					if (control is Table)
					{
						table = (Table)control;
						break;
					}
				}
				if (table != null)
				{
					foreach (object obj in table.Rows)
					{
						TableRow tableRow = (TableRow)obj;
						tableRow.RenderControl(writer);
					}
					return;
				}
				if (flag)
				{
					throw new HttpException(SR.GetString("DataList_TemplateTableNotFound", new object[]
					{
						this.Parent.ID,
						this.itemType.ToString()
					}));
				}
				return;
			}
		}

		// Token: 0x06004318 RID: 17176 RVA: 0x0011551A File Offset: 0x0011451A
		protected internal virtual void SetItemType(ListItemType itemType)
		{
			this.itemType = itemType;
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06004319 RID: 17177 RVA: 0x00115523 File Offset: 0x00114523
		object IDataItemContainer.DataItem
		{
			get
			{
				return this.DataItem;
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x0011552B File Offset: 0x0011452B
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x0600431B RID: 17179 RVA: 0x00115533 File Offset: 0x00114533
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return this.ItemIndex;
			}
		}

		// Token: 0x04002944 RID: 10564
		private int itemIndex;

		// Token: 0x04002945 RID: 10565
		private ListItemType itemType;

		// Token: 0x04002946 RID: 10566
		private object dataItem;
	}
}
